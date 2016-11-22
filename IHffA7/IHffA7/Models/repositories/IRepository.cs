using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHffA7.Models.dbModels;

namespace IHffA7.Models
{
    interface IRepository
    {
        IEnumerable<Testtabel> GetallConten();
        void Addrij(Testtabel rij);
    }
}
