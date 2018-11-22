using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRIP.App_Start;
using MongoDB.Driver;

using TRIP.Models;

namespace TRIP.Controllers
{
    public class DestinationsController : Controller
    {
        private MongoDBContext dbcontext;
        private IMongoCollection<DestinationsModel> DestinationsCollection;

        // GET: Destinations
        public DestinationsController()
        {
            dbcontext = new MongoDBContext();
            DestinationsCollection = dbcontext.database.GetCollection<DestinationsModel>("Destinations");
        }
        public ActionResult Index()
        {
            List<DestinationsModel> Destinations = DestinationsCollection.AsQueryable<DestinationsModel>().ToList();
            return View(Destinations);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DestinationsModel Destinations)
        {
            try
            {
                DestinationsCollection.InsertOneAsync(Destinations);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }




        public ActionResult Edit(string id)
        {
           // var DestinationsId = new string(id);
            var Destinations = DestinationsCollection.AsQueryable<DestinationsModel>().SingleOrDefault(x => x._id == id);
            return View(Destinations);

        }

        [HttpPost]
        public ActionResult Edit(string id, DestinationsModel Destinations)
        {
            try
            {
                var filter = Builders<DestinationsModel>.Filter.Eq("_id", id);
                var update = Builders<DestinationsModel>.Update.Set("Lat", Destinations.Lat).Set("Long",Destinations.Long).
                    Set("Direction", Destinations.Direction).Set("Description", Destinations.Description).Set("Activities", Destinations.Activities).
                    Set("PricePP", Destinations.PricePP);//Se puede agregar mas haciendo un .Set("",) extra
                var result = DestinationsCollection.UpdateOne(filter, update);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }





        public ActionResult Delete(string id)
        {
            var DestinationsId = id;
            var Destinations = DestinationsCollection.AsQueryable<DestinationsModel>().SingleOrDefault(x => x._id == DestinationsId);
            return View(Destinations);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, DestinationsModel Destinations)
        {
            try
            {
                DestinationsCollection.DeleteOne(Builders<DestinationsModel>.Filter.Eq("_id", id));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}