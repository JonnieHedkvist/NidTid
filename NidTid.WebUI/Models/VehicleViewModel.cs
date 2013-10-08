using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NidTid.WebUI.Models
{
    public class VehicleViewModel
    {
        public string RegNr { get; set; }
        public string Description { get; set; }
        public int CurrentMeter { get; set; }
        public string User { get; set; }
        public string Date { get; set; }

    }
}