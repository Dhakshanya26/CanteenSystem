using CanteenSystem.Dto.Models;
using CanteenSystem.Web;
using System;
using System.Collections.Generic;
using System.Text;

namespace CanteenSystem.Service
{
   

    public interface ILoginService
    {
        List<UserProfile> GetUserProfiles();
    }
}
