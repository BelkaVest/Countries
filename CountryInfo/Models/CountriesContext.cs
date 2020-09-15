using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
namespace CountryInfo.Models
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class CountriesContext : DbContext
        {
        public CountriesContext() : base("CountriesContext") { }

            public DbSet<Region> Regions { get; set; }
            public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        }
}