using System;
using System.Collections.Generic;
using System.Text;
using CanteenSystem.Dto.Models;

namespace CanteenSystem.Service
{
    public class DiscountsService : IDiscountsService

    {

        public List<Discount> GetDetail()
        {

            return null;
        }

        List<Discount> IDiscountsService.GetDetail()
        {
            throw new NotImplementedException();
        }
    }

}
