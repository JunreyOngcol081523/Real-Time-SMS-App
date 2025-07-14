using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Time_SMS_App
{
    public class Casualty
    {
        public string Type { get; set; }             // e.g., injured, fatality, missing
        public string Name { get; set; }             // e.g., adam silver
        public int Age { get; set; }                 // e.g., 30
        public string Person { get; set; }           // e.g., BFP, Civilian
        public string Cause { get; set; }   
        // e.g., burned arms
    }
    public class CasualtyGroup : ObservableCollection<Casualty>
    {
        public string Type { get; }

        public CasualtyGroup(string type, IEnumerable<Casualty> casualties) : base(casualties)
        {
            Type = type;
        }


    }


}
