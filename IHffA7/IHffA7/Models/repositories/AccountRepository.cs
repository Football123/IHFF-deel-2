using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IHffA7.Models.repositories
{
    public class AccountRepository : IAccountRepository
    {
        private WhatsUp1617S_martinstinsGenerated ctx = new WhatsUp1617S_martinstinsGenerated();

        public Accounts GetAccount(string emailadres, string wachtwoord)
        {
            return ctx.Accounts.SingleOrDefault(e => e.Emailadres == emailadres && e.Wachtwoord == wachtwoord);
        }

        public void AddAccount(Accounts account)
        {
            //Database add
            ctx.Accounts.Add(account);
            ctx.SaveChanges();
        }
    }
}