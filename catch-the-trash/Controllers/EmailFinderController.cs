using System;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace catch_the_trash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailFinderController : ControllerBase
    {
        // GET: api/EmailFinder
        [HttpGet]
        public async Task<IActionResult> Get(string city)
        {
            var email = await Email(city);

            return Ok(email);
        }

        private async Task<string> Email(string keyword)
        {
            var url = $"http://töosz.hu/szolgaltatasaink/onkormanyzati-adatbazis/?kulcsszo={keyword}";

            string email = await GetCityCouncilEmailAddress(keyword, url);

            if (email == null)
            {
                url = await GetNewUrl(keyword, url);
            }

            email = await GetCityCouncilEmailAddress(keyword, url);

            return email;
        }

        private async Task<string> GetCityCouncilEmailAddress(string keyword, string url)
        {
            var email = "";

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var htmlBody = await response.Content.ReadAsStringAsync();

                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(htmlBody);

                    try
                    {
                        email = GetEmail(htmlDoc);
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
            return email;
        }

        private async Task<string> GetNewUrl(string keyword, string url)
        {
            var urlNew = "";

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var htmlBody = await response.Content.ReadAsStringAsync();

                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(htmlBody);

                    try
                    {
                        urlNew = GetCityPage(keyword, htmlDoc);
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
            return urlNew;
        }

        private string GetCityPage(string keyword, HtmlDocument htmlDoc)
        {
            var cities = htmlDoc.DocumentNode.SelectNodes("//p[contains(@class, 'searchresultmsg')]")[0].NextSibling.NextSibling.ChildNodes;
            var urlNew = "";
            foreach (var city in cities)
            {
                if (city.FirstChild.InnerHtml.Contains($"{keyword} ("))
                {
                    urlNew = city.FirstChild.GetAttributeValue("href", "default");
                }
            }

            return urlNew;
        }

        private string GetEmail(HtmlDocument htmlDoc)
        {
            var email = "";
            var councilData = htmlDoc.DocumentNode.SelectNodes("//*[contains(text(),'Önkormányzat:')]")[0].ParentNode.Elements("a");

            foreach (var data in councilData)
                email = data.InnerHtml;

            return email;
        }
    }
}