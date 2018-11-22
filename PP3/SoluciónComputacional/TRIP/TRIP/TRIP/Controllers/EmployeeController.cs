using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using TRIP.App_Start;
using TRIP.Models;

namespace TRIP.Controllers
{
    public class EmployeeController : Controller
    {

        private MongoDBContext dbcontext;
        private IMongoCollection<EmployeeModel> EmployeeCollection;

        public EmployeeController()
        {
            dbcontext = new MongoDBContext();
            EmployeeCollection = dbcontext.database.GetCollection<EmployeeModel>("Employee");
        }

        // GET: Employee
        public ActionResult Index()
        {
            List<EmployeeModel> Employee =EmployeeCollection.AsQueryable<EmployeeModel>().ToList();
            return View(Employee);
            
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
        public ActionResult Login(EmployeeModel user, string password)
        {
            var usr = EmployeeCollection.AsQueryable<EmployeeModel>().SingleOrDefault(u => u.UserName == user.UserName && u.Password == password);
            //var usr = funcionarioCollection.FindAsync(u => u.Nombre == user.Nombre && u.Contraseña == user.Contraseña);

            if (usr != null)
            {
                TempData["_id"] = usr._id;

                return RedirectToAction("Index", "Destinations"); //crear una nueva vista para redirigir despues de accesar
            }
            else
            {
                ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos");
                return View();
            } 
            

        }
    }
}