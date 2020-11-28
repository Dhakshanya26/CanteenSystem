using CanteenSystem.Dto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CanteenSystem.Service
{
    public interface IDiscountsService
    {
        List<Discount> GetDetail();
    }

}
