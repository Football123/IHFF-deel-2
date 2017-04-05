using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IHffA7.Models
{
    public class EditActivityViewModel
    {
        public int OldActivityId { get; set; }

        public int NewActivityId { get; set; }

        public int ActionId { get; set; }

        public string Action { get; set; }

        [Required]
        [Display(Name = "Aantal personen")]
        public int NumberOfPersons { get; set; }


        public EditActivityViewModel(int oldActivityId, int actionId, string action, int numberOfPersons)
        {
            OldActivityId = oldActivityId;
            ActionId = actionId;
            Action = action;
            NewActivityId = -1;
            NumberOfPersons = numberOfPersons;
        }

        public EditActivityViewModel(string action)
        {
            OldActivityId = -1;
            ActionId = 0;
            Action = action;
            NewActivityId = -1;
            //default value of 
            NumberOfPersons = 1;
        }
        public EditActivityViewModel()
        {
                
        }
    }
}