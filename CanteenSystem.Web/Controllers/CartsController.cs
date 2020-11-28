using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CanteenSystem.Web.Models;
using Newtonsoft.Json;
using CanteenSystem.Web.ViewModel;

namespace CanteenSystem.Web.Controllers
{
    public class CartsController : Controller
    {
        private readonly CanteenSystemDbContext _context;

        public CartsController(CanteenSystemDbContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index(int userProfileId)
        {
            var canteenSystemDbContext = _context.Carts.Include(c => c.MealMenu).Include(c => c.UserProfile);
            var cartList = await canteenSystemDbContext.ToListAsync();

            var expiredCardList = cartList.Where(x => x.MealAvailableDate < DateTime.Now);

            if (expiredCardList != null && expiredCardList.Any())
            {
                _context.RemoveRange(expiredCardList);
                cartList = await _context.Carts.Include(c => c.MealMenu).Include(c => c.UserProfile).ToListAsync();
                  
            } 

            cartList = cartList.Where(x => x.UserProfileId == userProfileId)?.ToList();
            return View(cartList);
        }

        [Route("Carts/AddToCart/{menuId}/{availabilityDateId}/{userProfileId}")]
        [HttpPost]
        public async Task<IActionResult> AddToCart(int menuId, int availabilityDateId, int userProfileId)
        {
            try
            {
                var canteenSystemDbContext = _context.MealMenus.Where(x => x.Id == menuId &&
                x.MealMenuAvailabilities.Any(y => y.Id == availabilityDateId)).Include(x => x.MealMenuAvailabilities);
                var mealMenu = await canteenSystemDbContext.FirstOrDefaultAsync();
                if (mealMenu != null)
                {
                    var availableDate = mealMenu.MealMenuAvailabilities.
                        Where(x => x.Id == availabilityDateId).FirstOrDefault().AvailabilityDate;

                    var isExistingOrder = _context.Carts.FirstOrDefault(x => x.MealMenuId == menuId &&
                    x.MealAvailableDate == availableDate && x.UserProfileId == userProfileId);
                    if (isExistingOrder == null)
                    {
                        var cart = new Cart
                        {
                            MealMenuId = mealMenu.Id,
                            MealAvailableDate = availableDate,
                            Price = mealMenu.Price,
                            Quantity = 1,
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now,
                            UserProfileId = userProfileId
                        };
                        _context.Add(cart);
                        _context.SaveChanges();
                    }
                    else
                    {
                        return Json(new { Status = false });
                    }
                }

                return Json(new { Status = true });
            }
            catch (Exception ex) {
                //jhgh
            }
            return Json(new { Status = false });
        }


        [Route("Carts/RemoveFromCart/{cartId}")]
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartId)
        {
            try
            {
                var cart = await _context.Carts.FindAsync(cartId);
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync(); 
                return Json(new { Status = true });
            }
            catch (Exception ex)
            {
                return Json(new { Status = false });
            }
        }



        [Route("Carts/UpdateCartQuantity/{cartId}/{selectedQuantity}")]
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartId,int selectedQuantity)
        {
            try
            {
                var cart = await _context.Carts.FindAsync(cartId);
                if (cart != null)
                {
                    var mealMenuAvailabilities =  _context.MealMenuAvailabilities
                        .Where(x=>x.MealMenuId == cart.MealMenuId &&
                        x.AvailabilityDate == cart.MealAvailableDate).FirstOrDefault();
                    if (mealMenuAvailabilities != null)
                    {
                        if (mealMenuAvailabilities.Quantity > 1 && mealMenuAvailabilities.Quantity >= cart.Quantity + selectedQuantity)
                        {
                            cart.Quantity = selectedQuantity;
                            _context.Update(cart);
                            await _context.SaveChangesAsync();
                        }
                        else {
                            return Json(new {Status= false, Message = "Selected quantity is not available." });
                        }
                    }
                }

                return Json(new { Status = true, Message = "Updated the quantity" });
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Unexpected error occurred, please try again later." });
            }
        }

        [Route("carts/confirmOrderAndPayAtTill/{userId}")] 
        
        public async Task<IActionResult> ConfirmOrderAndPayAtTill(int userId)
        {
            try
            {
                Random generator = new Random();
                int r = generator.Next(1000, 10000000);
                var orderReferenceNumber = $"ORDER{r}";
                var cartItems = await _context.Carts.Where(x=>x.UserProfileId == userId).ToListAsync();
                if (cartItems != null)
                {
                  
                    var orderItems = new List<OrderItem>();
                    decimal totalPrice = 0M;
                    cartItems.ForEach(x=>{
                        totalPrice += (decimal)x.Price * x.Quantity;
                              orderItems.Add(new OrderItem {
                           MealMenuId =x.MealMenuId,
                              MealMenuOrderDate = x.MealAvailableDate,
                            Price = x.Price,
                            Quantity = x.Quantity  
                        });

                    });
                    
                    var order = new Order
                    {
                        CreatedDate =  DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        OrderReference = orderReferenceNumber,
                        TotalPrice = (double)totalPrice,
                        UserProfileId = userId ,
                        OrderItems = orderItems
                    };

                    _context.Add(order);
                    _context.SaveChanges();

                    var cartToBeRemoved = await _context.Carts.Where(c=>c.UserProfileId == userId).ToListAsync();
                    _context.Carts.RemoveRange(cartToBeRemoved);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("OrderConfirmation", "Orders",
                    new OrderConfirmationModel (  $"Your order has been confirmed and the reference number is {orderReferenceNumber}." +
                    $"Please pay at till and collect your order" ));
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Unexpected error occurred, please try again later." });
            }
        }

        [Route("Carts/confirmOrderAndPayNow/{userId}")]
        public async Task<IActionResult> ConfirmOrderPayNow(int userId)
        {
            var cartItems = await _context.Carts.Where(x => x.UserProfileId == userId).ToListAsync();
            decimal totalPrice = 0M;
            if (cartItems != null)
            {

                var orderItems = new List<OrderItem>();
              
                cartItems.ForEach(x => {
                    totalPrice += (decimal)x.Price * x.Quantity;
                    orderItems.Add(new OrderItem
                    {
                        MealMenuId = x.MealMenuId,
                        MealMenuOrderDate = x.MealAvailableDate,
                        Price = x.Price,
                        Quantity = x.Quantity
                    });

                });
            }
                return View("confirmOrderAndPayNow",new CardModel { Amount=  totalPrice});
        }


        [Route("Carts/confirmOrderAndPayNow/{userId}")]
        [HttpPost]
        public async Task<IActionResult> ConfirmOrderAndPayNow(int userId)
        {
            try
            {
                Random generator = new Random();
                int r = generator.Next(1000, 10000000);
                var orderReferenceNumber = $"ORDER{r}";
                var cartItems = await _context.Carts.Where(x => x.UserProfileId == userId).ToListAsync();
                if (cartItems != null)
                {

                    var orderItems = new List<OrderItem>();
                    decimal totalPrice = 0M;
                    cartItems.ForEach(x => {
                        totalPrice += (decimal)x.Price * x.Quantity;
                        orderItems.Add(new OrderItem
                        {
                            MealMenuId = x.MealMenuId,
                            MealMenuOrderDate = x.MealAvailableDate,
                            Price = x.Price,
                            Quantity = x.Quantity
                        });

                    });

                    var order = new Order
                    {
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        OrderReference = orderReferenceNumber,
                        TotalPrice = (double)totalPrice,
                        UserProfileId = userId,
                        OrderItems = orderItems
                    };
                    

                    _context.Add(order);
                    _context.SaveChanges();

                    var paymentReferenceNumber = $"PAYMENT{r}";
                    var payment = new Payment
                    {
                        PaymentDate = DateTime.Now,
                        PaymentReference = paymentReferenceNumber,
                        PaymentAmount = (double)totalPrice,
                        OrderId = order.Id 
                    };
                    _context.Add(payment);
                    _context.SaveChanges();
                    var cartToBeRemoved = _context.Carts.Where(c => c.UserProfileId == userId);
                    _context.Carts.RemoveRange(cartToBeRemoved);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("OrderConfirmation", "Orders",
                    new OrderConfirmationModel($"Your order has been confirmed and the reference number is {orderReferenceNumber}. " +
                    $"Please collect your order"));
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Unexpected error occurred, please try again later." });
            }
        }
        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.MealMenu)
                .Include(c => c.UserProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            ViewData["MealMenuId"] = new SelectList(_context.MealMenus, "Id", "MealName");
            ViewData["UserProfileId"] = new SelectList(_context.UserProfiles, "Id", "EmailAddress");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MealMenuId,Quantity,Price,CreatedDate,UpdatedDate,MealAvailableDate,UserProfileId")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MealMenuId"] = new SelectList(_context.MealMenus, "Id", "MealName", cart.MealMenuId);
            ViewData["UserProfileId"] = new SelectList(_context.UserProfiles, "Id", "EmailAddress", cart.UserProfileId);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["MealMenuId"] = new SelectList(_context.MealMenus, "Id", "MealName", cart.MealMenuId);
            ViewData["UserProfileId"] = new SelectList(_context.UserProfiles, "Id", "EmailAddress", cart.UserProfileId);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MealMenuId,Quantity,Price,CreatedDate,UpdatedDate,MealAvailableDate,UserProfileId")] Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MealMenuId"] = new SelectList(_context.MealMenus, "Id", "MealName", cart.MealMenuId);
            ViewData["UserProfileId"] = new SelectList(_context.UserProfiles, "Id", "EmailAddress", cart.UserProfileId);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.MealMenu)
                .Include(c => c.UserProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.Id == id);
        }
    }
}
