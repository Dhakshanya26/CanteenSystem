using CanteenSystem.Dto.Models;
using System.Collections.Generic;
using System;
using CanteenSystem.Service;

namespace CanteenSystem.Service
{
    public interface IMealMenuAvailability
    {
        List<MealMenuAvailability> GetDetails();
    }
}