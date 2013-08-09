using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace NidTid.Domain.Entities
{
    public class Customer
    {
        [Display(Name = "Kundnr")]
        public int Id { get; set; }

        [Display(Name = "Namn")]
        public string Name { get; set; }

        [Display(Name = "Org.nr")]
        public string OrgNr { get; set; }

        public string Adress { get; set; }

        [Display(Name = "Postnr")]
        public string PostNr { get; set; }

        [Display(Name = "Postort")]
        public string PostOrt { get; set; }

        [Display(Name = "Tel")]
        public string Phone1 { get; set; }

        [Display(Name = "Tel2")]
        public string Phone2 { get; set; }

        public string Email { get; set; }

        public Nullable<decimal> Moms { get; set; }
    }
}
