using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models
{
    interface IRepository
    {
        void GetHTLMContentNL(string pagina);
        void GetHTLMContentEN(string pagina);
    }
}
