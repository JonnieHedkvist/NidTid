using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NidTid.Domain.Entities
{
    public class MeterPost
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int VehicleId { get; set; }

        [Display(Name = "Mätarställning")]
        public int CurrentMeter { get; set; }

        [Display(Name = "Notering")]    
        public string Notes { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime Date { get; set; }

        public virtual User User { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
