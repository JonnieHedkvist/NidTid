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

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Referens { get; set; }
        public bool Active { get; set; }
        public string FastBtn { get; set; }
        public string KontoStr { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        //public virtual User User { get; set; }
        //public virtual ICollection<Report> Report { get; set; }
        //public virtual ICollection<Bill> Bill { get; set; }
    }
}
