using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NidTid.Domain.Entities
{
    public class Report
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public Nullable<decimal> Deb { get; set; }

        public Nullable<decimal> EjDeb { get; set; }

        public Nullable<decimal> Km { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime Date { get; set; }

        public int UserId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Nullable<int> SalaryId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public Nullable<int> BillId { get; set; }

        //public virtual Bill Bill { get; set; }
        public virtual Project Project { get; set; }
        //public virtual Salary Salary { get; set; }
        public virtual User User { get; set; }
    }
}
