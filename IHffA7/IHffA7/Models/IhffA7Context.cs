using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace IHffA7.Models
{
    class IhffA7Context : DbContext
    {
        public IhffA7Context() : base("iHFF1617S_A7")
        {
            
        }
        //public DbSet<>
    }
}
