using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CountryInfo.Models
{
    public class CountriesListElement
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Capital { get; set; }
        public double Area { get; set; }
        public int Population { get; set; }
        public string Region { get; set; }
    }
}