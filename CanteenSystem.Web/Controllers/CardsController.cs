using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CanteenSystem.Web.ViewModel;
using CanteenSystem.Dal;
using CanteenSystem.Service;

namespace CanteenSystem.Web.Controllers
{
    public class CardsController : Controller
    {
        private readonly CanteenSystemDbContext _context;
        private readonly ICardsService _cardsservice;
        public CardsController(CanteenSystemDbContext context, ICardsService cardsservice)
        {
            _context = context;
            _cardsservice = cardsservice;
        }


        // GET: Cards
        public async Task<IActionResult> Student(int? id,string message = null)
        {

            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Cards
                .Include(c => c.UserProfile)
                .FirstOrDefaultAsync(m => m.UserProfileId == id);
            if (card == null)
            {
                return NotFound();
            }

            ViewBag.Status = message;
            card.UserProfileId = id.Value;
            return View(card);
        }

        // GET: Cards
        public async Task<IActionResult> Parent(int? id, string message = null)
        {

            if (id == null)
            {
                return NotFound();
            }

            var parentDetail = await _context.ParentMapping
               .Include(c => c.StudentUserProfile)
               .FirstOrDefaultAsync(m => m.ParentId == id);

            if (parentDetail == null)
            {
                return NotFound();
            }

            var card = await _context.Cards
                .Include(c => c.UserProfile)
                .FirstOrDefaultAsync(m => m.UserProfileId == parentDetail.StudentId);
            if (card == null)
            {
                return NotFound();
            }
            ViewBag.Status = message;
            card.UserProfileId = id.Value;
            return View(card);
        }

       [Route("addstudentfund/{id}/{userProfileId}")]
        public async Task<IActionResult> AddStudentFund(int id,int userProfileId)
        {
            var card = await _context.Cards
               .FirstOrDefaultAsync(m => m.Id == id);

            if (card == null)
            {
                return NotFound();
            }

            return View(new CardModel()
            {
                CardId = card.Id,
                UserProfileId = userProfileId
            }) ;
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentFund(CardModel model)
        {
            if (model.CardId != 0)
            {
                var card = await _context.Cards
                   .FirstOrDefaultAsync(m => m.Id == model.CardId);

                if (card == null)
                { 
                    ModelState.AddModelError("error", "Unable Add funs! Please contact admin");

                    return View("AddStudentFund", model);
                }
                card.AvailableBalance = card.AvailableBalance+ model.Amount;
                _context.Update(card);
                _context.SaveChanges();
                return RedirectToAction("Student", new { id = model.UserProfileId ,message= $"{model.Amount} added to the fund" }); 
            }

            return View(model);
        }
        [Route("AddParentFund/{id}/{userProfileId}")]
        public async Task<IActionResult> AddParentFund(int id, int userProfileId)
        {
            var card = await _context.Cards
               .FirstOrDefaultAsync(m => m.Id == id);

            if (card == null)
            {
                return NotFound();
            }

            return View(new CardModel()
            {
                CardId = card.Id,
                UserProfileId = userProfileId
            });
        }


        [HttpPost]
        public async Task<IActionResult> AddParentFund(CardModel model)
        {
            if (model.CardId != 0)
            {
                var card = await _context.Cards
                   .FirstOrDefaultAsync(m => m.Id == model.CardId);

                if (card == null)
                {
                    ModelState.AddModelError("error", "Unable Add funs! Please contact admin");

                    return View("AddStudentFund", model);
                }
                card.AvailableBalance = card.AvailableBalance+ model.Amount;
                _context.Update(card);
                _context.SaveChanges();

                return RedirectToAction("Parent", new { id = model.UserProfileId, message = $"{model.Amount} added to the fund" });
            }

            return View(model);
        }

    }
}
