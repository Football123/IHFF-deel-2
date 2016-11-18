using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models.dbModels
{
    public class Activiteit
    {
        public int Id { get; set; }
        public DateTime BeginTijd { get; set; }
        public DateTime EindTijd { get; set; }
        public decimal Prijs { get; set; }
        public int locatieId { get; set; }

        public Activiteit(int id, DateTime beginTijd, DateTime eindTijd, decimal prijs, int locatieId)
        {
            this.Id = id;
            this.BeginTijd = beginTijd;
            this.EindTijd = eindTijd;
            this.Prijs = prijs;
            this.locatieId = locatieId;
        }
    }
}
