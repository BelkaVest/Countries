using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CountryInfo.Models
{
    public class CountrySerializeModel
    {
        public String name { get; set; }

        public List<String> topLevelDomai { get; set; }
        public String alpha2Code { get; set; }

        public String alpha3Code { get; set; }

        public List<String> callingCodes { get; set; }

        public String capital { get; set; }

        public List<String> altSpellings { get; set; }

        public String region { get; set; }

        public String subregion { get; set; }

        public int population { get; set; }

        public List<Double> latlng { get; set; }

        public String demonym { get; set; }

        public Double area { get; set; }

        public Double gini { get; set; }

        public List<String> timezones { get; set; }

        public List<String> borders { get; set; }

        public String nativeName { get; set; }

        public String numericCode { get; set; }

    }
}