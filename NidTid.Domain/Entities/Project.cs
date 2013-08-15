using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NidTid.Domain.Entities
{
    public partial class Project {

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Namn")]
        public string Name { get; set; }

        [Display(Name = "Arbetsbeskrivning")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Referens")]
        public string Referens { get; set; }
        
        [Display(Name = "Fastighetsbeteckning")]
        public string FastBtn { get; set; }

        [Display(Name = "Kontosträng")]
        public string KontoStr { get; set; }

        [Display(Name = "Aktivt")]
        public bool Active { get; set; }

        //[Display(Name = "Projektansvarig")]
        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        //public virtual User User { get; set; }
        //public virtual ICollection<Report> Report { get; set; }
        //public virtual ICollection<Bill> Bill { get; set; }
    }
}
