using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IHffA7.Models.dbModels
{
    public class Testtabel
    {
        public int? Id { get; set; }
        public string Naam { get; set; }

        public Testtabel(int Id, string naam)
        {
            this.Id = Id;
            this.Naam = naam;

        }

        public Testtabel(string naam)
        {
            this.Naam = naam;

        }
        public Testtabel()
        {

        }
    }
}
