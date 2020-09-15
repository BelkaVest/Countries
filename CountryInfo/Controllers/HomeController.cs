using CountryInfo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CountryInfo.Controllers
{
    public class HomeController : Controller
    {
        CountriesContext db = new CountriesContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveNewCountry(CountriesListElement model)
        {
            try
            {
            Country c = new Country { Name = model.Name, Code = model.Code, Population = model.Population, Area = model.Area };
            var capital = db.Cities.Where(a => a.Name.Equals(model.Capital)).FirstOrDefault();
            if (capital != null)
            {
                c.Capital = capital.Id;
            }
            else
            {
                db.Cities.Add(new City { Name = model.Capital });
                db.SaveChanges();
                c.Capital = db.Cities.Where(b => b.Name.Equals(model.Capital)).FirstOrDefault().Id;
            }
            var region = db.Regions.Where(d => d.Name.Equals(model.Region)).FirstOrDefault();
            if (region != null)
            {
                c.Region = region.Id;
            }
            else
            {
                db.Regions.Add(new Region { Name = model.Region });
                db.SaveChanges();
                c.Region = db.Regions.Where(e => e.Name.Equals(model.Region)).FirstOrDefault().Id;
            }
            var country = db.Countries.Where(f => f.Name.Equals(model.Name)).FirstOrDefault();
            if (country == null)
            {
                db.Countries.Add(c);
                db.SaveChanges();
            }

            }
            catch (Exception)
            {
                ViewBag.Message = "Data wasn't saved";
            }
            return Index();
        }
        [HttpPost]
        public ActionResult GetInfoAboutCountry(String countryName)
        {
            try
            {
                string info=GetInfoFromAPI(countryName);
                CountriesListElement countryObject = GetCountryObject(info);
                return PartialView("InfoAboutCountry",countryObject);
            } catch (Exception)
            {
                ViewBag.Error = "Country with that name wasn't find";
                return PartialView("Error");
            }
        }

        public CountriesListElement GetCountryObject(String info)
        {
            CountriesListElement country = new CountriesListElement();
            CountrySerializeModel model = JsonConvert.DeserializeObject<CountrySerializeModel>(info.Substring(1, info.Length - 2));
            country.Name = model.name;
            country.Code = model.alpha2Code;
            country.Capital = model.capital;
            country.Area = model.area;
            country.Population = model.population;
            country.Region = model.region;
            return country;
        }

        [HttpPost]
        public ActionResult GetListOfCountries()
        {
            try
            {
                var countries = db.Countries.ToList();
                List<CountriesListElement> countriesList = new List<CountriesListElement>();
                foreach (var c in countries)
                    countriesList.Add(new CountriesListElement
                    {
                        Name = c.Name,
                        Area = c.Area,
                        Code = c.Code,
                        Population = c.Population,
                        Region = db.Regions.Where(a => a.Id == c.Region).FirstOrDefault().Name,
                        Capital = db.Cities.Where(b => b.Id == c.Capital).FirstOrDefault().Name
                    });
                return PartialView("CountriesList", countriesList);
            }
            catch (Exception)
            {
                ViewBag.Error = "There is no country information saved in the database";
                return PartialView("Error");
            }
        }
        public string GetInfoFromAPI(string countryName)
        {
            string url = "https://restcountries.eu/rest/v2/name/"+countryName;
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                    return stream.ReadLine();
            }
        }
    }
}