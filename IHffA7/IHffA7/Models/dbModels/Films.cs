using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models.dbModels
{
    public class Films
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string DescriptionNL { get; set; }
        public string DescriptionEN { get; set; }
        public DateTime Year { get; set; }

        public Films(int id, string title, string director, string descriptionNL, string descriptionEN, DateTime year)
        {
            this.Id = id;
            this.Title = title;
            this.Director = director;
            this.DescriptionNL = descriptionNL;
            this.DescriptionEN = descriptionEN;
            this.Year = year;
        }
        public Films()
        {

        }
    }
}
