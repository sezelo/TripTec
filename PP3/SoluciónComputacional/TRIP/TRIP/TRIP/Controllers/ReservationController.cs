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
    public class ReservationController : Controller
    {
        private MongoDBContext dbcontext;
        private IMongoCollection<ReservationModel> ReservationCollection;
        // GET: Reservation


        public ReservationController()
        {
            dbcontext = new MongoDBContext();
            ReservationCollection = dbcontext.database.GetCollection<ReservationModel>("Reservation");
           
        }
        public ActionResult Index()
        {
            List<ReservationModel> Reservation = ReservationCollection.AsQueryable<ReservationModel>().ToList();
            return View(Reservation);

            return View();
        }







        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ReservationModel Reservation)
        {
            try
            {
                ReservationCollection.InsertOneAsync(Reservation);

                return RedirectToAction("Reservation");
            }
            catch
            {
                return View();
            }
        }


        //FUNCION PARA MOSTRAR RESERVACIONES SEGUN EL ID DEL CLIENTE
        public async System.Threading.Tasks.Task<ActionResult> HistorialAsync()
        {
            if (TempData["_id"] != null)
            {
                string _id = (string)TempData["_id"];
                var filter = Builders<ReservationModel>.Filter.Eq("_id", _id);
                List<ReservationModel> result = await ReservationCollection.Find(filter).ToListAsync();
                TempData["cedula2"] = _id;
                return View(result);

            }
            else
            {
                return RedirectToAction("Login","Customer");
            }
        }
    }
}