using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudApı.Models
{
    public class WorkTable
    {
        public int WorkId { get; set; }
        public int Id { get; set; }
        public string Tittle { get; set; }
        public string Explanation { get; set; }
        public string Category { get; set; }
        public string Hıw { get; set; }
        public string EducationStatus { get; set; }
        public string Experience { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPhone { get; set; }
        public string Address { get; set; }
        public double AddressLongitude { get; set; }
        public double AddressLatitude { get; set; }
        public string InfoEmployer { get; set; }
    }
}