using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models.repositories
{
    interface IAccountRepository
    {
        Accounts GetAccount(string inlognaam, string wachtwoord);
        void AddAccount(Accounts account);
    }
}
