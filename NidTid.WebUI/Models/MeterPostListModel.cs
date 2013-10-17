using NidTid.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NidTid.WebUI.Models
{
    public class MeterPostListModel
    {
        public IEnumerable<MeterPost> MeterPosts { get; set; }
        public string TableTitle { get; set; }
    }
}