using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IHffA7.Models
{
    public class EditActivityViewModel
    {
        public int OldActivityId { get; set; }

        public int NewActivityId { get; set; }

        public int ActionId { get; set; }

        public string Action { get; set; }

        public int NumberOfPersons { get; set; }

        public EditActivityViewModel(int oldActivityId, int newActivityId, int actionId, string action)
        {
            OldActivityId = oldActivityId;
            ActionId = actionId;
            Action = action;
            NewActivityId = newActivityId;
        }

        public EditActivityViewModel(int oldActivityId, int actionId, string action, int numberOfPersons)
        {
            OldActivityId = oldActivityId;
            ActionId = actionId;
            Action = action;
            NewActivityId = -1;
            NumberOfPersons = numberOfPersons;
        }
        public EditActivityViewModel()
        {
                
        }
    }
}