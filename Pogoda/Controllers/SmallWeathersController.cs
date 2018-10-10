using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pogoda.Models;

namespace Pogoda.Controllers
{
    public class SmallWeathersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SmallWeathers
        public ActionResult Index(int? id)
        {
            return View(db.SmallWeathers.Where(x => x.WeatherReportId == id).ToList());
        }

        // GET: SmallWeathers/Graph/5
        public ActionResult Graph(int? id)
        {
            var weatherData = db.SmallWeathers.Where(x => x.WeatherReportId == id).ToList();

            return View(weatherData);
        }

        /*// GET: SmallWeathers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SmallWeathers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,WeatherReportId,Temperature,Humidity,DateOf")] SmallWeather smallWeather)
        {
            if (ModelState.IsValid)
            {
                db.SmallWeathers.Add(smallWeather);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(smallWeather);
        }

        // GET: SmallWeathers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SmallWeather smallWeather = db.SmallWeathers.Find(id);
            if (smallWeather == null)
            {
                return HttpNotFound();
            }
            return View(smallWeather);
        }

        // POST: SmallWeathers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,WeatherReportId,Temperature,Humidity,DateOf")] SmallWeather smallWeather)
        {
            if (ModelState.IsValid)
            {
                db.Entry(smallWeather).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(smallWeather);
        }

        // GET: SmallWeathers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SmallWeather smallWeather = db.SmallWeathers.Find(id);
            if (smallWeather == null)
            {
                return HttpNotFound();
            }
            return View(smallWeather);
        }

        // POST: SmallWeathers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SmallWeather smallWeather = db.SmallWeathers.Find(id);
            db.SmallWeathers.Remove(smallWeather);
            db.SaveChanges();
            return RedirectToAction("Index");
        }*/

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
