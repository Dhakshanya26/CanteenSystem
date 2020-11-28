using CanteenSystem.Dto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CanteenSystem.Service
{
    public class CartsService : ICartsService

    {

        public List<Cart> GetDetail()
        {

            return null;
        }

        List<Cart> ICartsService.GetDetail()
        {
            throw new NotImplementedException();
        }
    }




}
