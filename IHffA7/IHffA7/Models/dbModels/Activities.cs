using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models.dbModels
{
    public class Activities
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Price { get; set; }
        public int LocationId { get; set; }

        public Activities(int id, DateTime startTime, DateTime endTime, decimal price, int locationId)
        {
            this.Id = id;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Price = price;
            this.LocationId = locationId;
        }
        public Activities()
        {
                
        }
    }
}
