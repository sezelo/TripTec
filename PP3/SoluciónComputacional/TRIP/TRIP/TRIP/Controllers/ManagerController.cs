using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using TRIP.App_Start;
using TRIP.Models;

namespace TRIP.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
      
        private MongoDBContext dbcontext;
        private IMongoCollection<ManagerModel> ManagerCollection;

        public ManagerController()
        {
            dbcontext = new MongoDBContext();
            ManagerCollection = dbcontext.database.GetCollection<ManagerModel>("Manager");
        }

        // GET: Employee
        public ActionResult Index()
        {
            List<ManagerModel> Manager = ManagerCollection.AsQueryable<ManagerModel>().ToList();
            return View(Manager);

        }

        public ActionResult Logged()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(ManagerModel user, string password)
        {
             return RedirectToAction("Index", "CAdministrador"); //crear una nueva vista para redirigir despues de accesar
        }
    }
}