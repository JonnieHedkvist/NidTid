using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NidTid.Domain.Entities
{
    public class Vehicle
    {
        public Vehicle()
        {
            this.MeterPost = new HashSet<MeterPost>();
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Reg.nummer")]
        public string RegNr { get; set; }

        [Display(Name = "Beskrivning")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    
        public virtual ICollection<MeterPost> MeterPost { get; set; }
    }
}
