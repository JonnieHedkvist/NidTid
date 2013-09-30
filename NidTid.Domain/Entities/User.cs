using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NidTid.Domain.Entities
{
    public class User {
        public User()
        {
            this.MeterPost = new HashSet<MeterPost>();
            this.Project = new HashSet<Project>();
            this.Report = new HashSet<Report>();
            //this.Salary1 = new HashSet<Salary>();
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Namn")]
        public string Name { get; set; }
        
        [Display(Name = "Personnummer")]
        public string PersNr { get; set; }

        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [Display(Name = "Adress")]
        public string Adress { get; set; }

        [Display(Name = "Postnummer")]
        public string PostNr { get; set; }

        [Display(Name = "Ort")]
        public string PostOrt { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Lösenord")]
        public string Password { get; set; }

        [Display(Name = "Lön")]
        public Nullable<decimal> Salary { get; set; }

        [Display(Name = "Skattesats")]
        public Nullable<decimal> Tax { get; set; }

        [Display(Name = "Användarroll")]
        public string Role { get; set; }
    
        public virtual ICollection<MeterPost> MeterPost { get; set; }
        public virtual ICollection<Project> Project { get; set; }
        public virtual ICollection<Report> Report { get; set; }
        //public virtual ICollection<Salary> Salary { get; set; }
    }
}

