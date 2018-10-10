using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pogoda.Models;
using Quartz;
using Quartz.Impl;
using static Pogoda.Models.Weather;

namespace Pogoda.Controllers
{
    public class WeathersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        

        // GET: Weathers
        [Authorize]
        public ActionResult Index()
        {
            string username = User.Identity.Name;
            ApplicationUser user = db.Users.FirstOrDefault(u => u.UserName.Equals(username));

            var weatherReports = from w in db.Weather select w;
            weatherReports = weatherReports.Where(x => x.UserId == user.Id);

            return View(weatherReports);
        }

        // GET: Weathers/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Weathers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "CityId")] Weather weather, string listaMiast)
        {
            //wywołanie zapytania do API o pogodę
            weather = GetWeather(listaMiast);

            //przypisanie aktualnie zalogowanego użytkownika
            string username = User.Identity.Name;
            ApplicationUser user = db.Users.FirstOrDefault(u => u.UserName.Equals(username));

            weather.UserId = user.Id;
            weather.ApplicationUser = user;

            SmallWeather smallWeather = new SmallWeather();       
            smallWeather.Temperature = weather.CurrentTemperature;
            smallWeather.Humidity = weather.CurrentHumidity;
            smallWeather.DateOf = weather.DateEntrySet;

            if (ModelState.IsValid)
            {
                db.Weather.Add(weather);
                db.SaveChanges();
                smallWeather.WeatherReportId = db.Weather.Find(weather.ID).ID;
                db.SmallWeathers.Add(smallWeather);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(weather);
        }

        // GET: Weathers/Update/5
        [Authorize]
        public ActionResult Update(int? id)
        {
            string username = User.Identity.Name;
            ApplicationUser user = db.Users.FirstOrDefault(u => u.UserName.Equals(username));

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weather weather = db.Weather.Find(id);
            Weather updatedWeather = GetWeather(weather.CityId.ToString());
            if (weather == null)
            {
                return HttpNotFound();
            }

            weather.CurrentTemperature = updatedWeather.CurrentTemperature;
            weather.CurrentHumidity = updatedWeather.CurrentHumidity;
            weather.DateEntrySet = updatedWeather.DateEntrySet;

            SmallWeather smallWeather = new SmallWeather();
            smallWeather.WeatherReportId = weather.ID;
            smallWeather.Temperature = weather.CurrentTemperature;
            smallWeather.Humidity = weather.CurrentHumidity;
            smallWeather.DateOf = weather.DateEntrySet;

            if (ModelState.IsValid)
            {
                db.SmallWeathers.Add(smallWeather);
                db.Entry(weather).State = EntityState.Modified;
                db.SaveChanges();

            }


            return RedirectToAction("Index");
        }

        // GET: Weathers/UpdateAll/5
        [Authorize]
        public ActionResult UpdateAll()
        {

            string username = User.Identity.Name;
            ApplicationUser user = db.Users.FirstOrDefault(u => u.UserName.Equals(username));
            Weather updatedWeather;

            var weatherReports = from w in db.Weather select w;
            weatherReports = weatherReports.Where(x => x.UserId == user.Id);

            foreach (Weather wr in weatherReports)
            {
                updatedWeather = GetWeather(wr.CityId.ToString());
                wr.CurrentTemperature = updatedWeather.CurrentTemperature;
                wr.CurrentHumidity = updatedWeather.CurrentHumidity;
                wr.DateEntrySet = DateTime.Now;

                Weather weather = db.Weather.Find(wr.ID);
                weather.CurrentTemperature = wr.CurrentTemperature;
                weather.CurrentHumidity = wr.CurrentHumidity;
                weather.DateEntrySet = wr.DateEntrySet;

                SmallWeather smallWeather = new SmallWeather();
                smallWeather.WeatherReportId = weather.ID;
                smallWeather.Temperature = weather.CurrentTemperature;
                smallWeather.Humidity = weather.CurrentHumidity;
                smallWeather.DateOf = weather.DateEntrySet;

                if (ModelState.IsValid)
                {
                    db.Entry(weather).State = EntityState.Modified;
                    db.SmallWeathers.Add(smallWeather);
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: Weathers/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weather weather = db.Weather.Find(id);
            if (weather == null)
            {
                return HttpNotFound();
            }
            return View(weather);
        }

        // POST: Weathers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Weather weather = db.Weather.Find(id);

            var weatherHistory = from wH in db.SmallWeathers where wH.WeatherReportId == weather.ID select wH;
            foreach (SmallWeather element in weatherHistory)
            {
                db.SmallWeathers.Remove(db.SmallWeathers.Find(element.ID));
            }

            db.Weather.Remove(weather);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        //FUNKCJE POMOCNICZNE
        //

        //pobieranie danych z API i zwracanie obiektu pogody
        [Authorize]
        public Weather GetWeather(string cityId)
        {
            string ApiKey = "5b1a75b4a2db25dd1cd6fc347306b4ec";

            HttpWebRequest apiRequest = WebRequest.Create("http://api.openweathermap.org/data/2.5/weather?id=" + cityId.ToString() + "&appid=" + ApiKey + "&units=metric") as HttpWebRequest;
            String apiResponse = "";

            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
            }

            JObject weatherData = JObject.Parse(apiResponse);

            dynamic jsonData = weatherData;
            Weather currentWeatherData = new Weather();

            currentWeatherData.CityId = jsonData.id;
            currentWeatherData.CityName = jsonData.name;
            currentWeatherData.CityCountry = jsonData.sys.country;
            currentWeatherData.CurrentTemperature = jsonData.main.temp;
            currentWeatherData.CurrentHumidity = jsonData.main.humidity;
            currentWeatherData.DateEntrySet = DateTime.Now;

            return currentWeatherData;
        }

        //aktualizacja wszystkich wpisów dla wszystkich użytkowników
        public void UpdateAllWeathersForAllUsers()
        {
            Weather updatedWeather;
            var weatherReports = from w in db.Weather select w;

            foreach (Weather wr in weatherReports)
            {
            
                updatedWeather = GetWeather(wr.CityId.ToString());
                wr.CurrentTemperature = updatedWeather.CurrentTemperature;
                wr.CurrentHumidity = updatedWeather.CurrentHumidity;
                wr.DateEntrySet = DateTime.Now;

                Weather weather = db.Weather.Find(wr.ID);
                weather.CurrentTemperature = wr.CurrentTemperature;
                weather.CurrentHumidity = wr.CurrentHumidity;
                weather.DateEntrySet = wr.DateEntrySet;

                SmallWeather smallWeather = new SmallWeather();
                smallWeather.WeatherReportId = weather.ID;
                smallWeather.Temperature = weather.CurrentTemperature;
                smallWeather.Humidity = weather.CurrentHumidity;
                smallWeather.DateOf = weather.DateEntrySet;

                if (ModelState.IsValid)
                {
                    db.Entry(weather).State = EntityState.Modified;
                    db.SmallWeathers.Add(smallWeather);
                }
            }
            db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
