using CompleteDevNet.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompleteDevNet.Infrastructure;

public class AccountService : IAccountService
{
    /// <summary>
    /// dummy authentication routine
    /// </summary>
    /// <returns>true if ok, false if not</returns>
    public async Task<bool> AuthenticateUser(string userName, string password)
    {
        bool bReturn = false;
        switch (userName)
        {
            case "admin":
            case "user1":
            case "user2":
                bReturn = string.Equals(userName, password);
                break;
        }
        return bReturn;
    }
}
