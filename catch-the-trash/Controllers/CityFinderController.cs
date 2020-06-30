using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using catch_the_trash.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace catch_the_trash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityFinderController : ControllerBase
    {
        // GET: api/CityFinder
        [HttpGet]
        public IActionResult Get(double lon, double lat)
        {
            var json = GetCityNameByLocation(lon, lat);
            City city = ParseJson(json);

            return Ok(city);
        }

        private string GetCityNameByLocation(double longitude, double latitude)
        {
            var specifier = "G";

            WebClient client = new WebClient();
            client.Headers.Set("X-RapidAPI-Key", "43111ac966mshcb4bd94f4969cbep1ffcf1jsn1a3994027332");
            client.Headers.Set("X-RapidAPI-Host", "wft-geo-db.p.rapidapi.com");

            string url = "https://wft-geo-db.p.rapidapi.com/v1/geo/locations/" + longitude.ToString(specifier, CultureInfo.InvariantCulture) + "+" + latitude.ToString(specifier, CultureInfo.InvariantCulture) + "/nearbyCities?radius=100";
            var json = client.DownloadString(url);


            return json;
        }

        private City ParseJson(string json)
        {
            var city = new City();

            var jo = JObject.Parse(json);


            city.CityName = jo["data"][0]["city"].ToString();
            city.Region = jo["data"][0]["region"].ToString();
            city.Country = jo["data"][0]["country"].ToString();

            return city;
        }



    }
}
