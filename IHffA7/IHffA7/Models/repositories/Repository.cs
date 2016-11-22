using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHffA7.Models.dbModels;

namespace IHffA7.Models
{
    class Repository : IRepository
    {
        IhffA7Context ctx = new IhffA7Context();

        IEnumerable<Testtabel> IRepository.GetallConten()
        {
            return ctx.Teststabels.ToList();
        }
        public void Addrij(Testtabel rij)
        {
            ctx.Teststabels.Add(rij);
            ctx.SaveChanges();
        }
    }
}
