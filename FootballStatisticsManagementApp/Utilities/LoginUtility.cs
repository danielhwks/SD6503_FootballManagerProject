using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FootballStatisticsManagementApp.Utilities
{
    public static class LoginUtility
    {
        public static bool CheckAuthenticated(ISession session)
        {
            var auth = session.GetInt32("auth");
            if (auth == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
