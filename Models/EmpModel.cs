using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataTableDemo.Models
{
    public class EmpModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }


    }
}