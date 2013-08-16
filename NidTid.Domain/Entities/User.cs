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
            //this.MeterPost = new HashSet<MeterPost>();
            this.Project = new HashSet<Project>();
            //this.Report = new HashSet<Report>();
            //this.Salary1 = new HashSet<Salary>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string PersNr { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public string PostNr { get; set; }
        public string PostOrt { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Nullable<decimal> Salary { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public bool IsAdmin { get; set; }
    
        //public virtual ICollection<MeterPost> MeterPost { get; set; }
        public virtual ICollection<Project> Project { get; set; }
        public virtual ICollection<Report> Report { get; set; }
        //public virtual ICollection<Salary> Salary1 { get; set; }
    }
}

