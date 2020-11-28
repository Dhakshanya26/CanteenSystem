using CanteenSystem.Dto.Models;
using System.Collections.Generic;
using System;
using CanteenSystem.Service;

namespace CanteenSystem.Dto
{
    public class LoginService :ILoginService
    {
        public List<UserProfile> GetUserProfile()
        {
            return null;
        }

        public List<UserProfile> GetUserProfiles()
        {
            throw new NotImplementedException();
        }
    }
}
