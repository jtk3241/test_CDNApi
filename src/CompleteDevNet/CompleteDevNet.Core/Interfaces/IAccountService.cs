using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompleteDevNet.Core.Interfaces;

public interface IAccountService
{
    Task<bool> AuthenticateUser(string userName, string password);
}
