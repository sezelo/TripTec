using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRIP.Models;
using MongoDB.Bson;
using TRIP.App_Start;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace TRIP.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer

        private MongoDBContext dbcontext;
        private IMongoCollection<CustomerModel> CustomerCollection;
        private IMongoCollection<WishListModel> WishListCollection;
        private IMongoCollection<DestinationsModel> DestinationsCollection;

        public CustomerController()
        {
            dbcontext = new MongoDBContext();
            CustomerCollection = dbcontext.database.GetCollection<CustomerModel>("Customer");
            WishListCollection = dbcontext.database.GetCollection<WishListModel>("WishList");
            DestinationsCollection = dbcontext.database.GetCollection<DestinationsModel>("Destinations");
        }



        

        public ActionResult DestinationsList()
        {
            List<DestinationsModel> destinations = DestinationsCollection.AsQueryable<DestinationsModel>().ToList();
            return View(destinations);
           
        }


        public ActionResult Index()
        {
            List<CustomerModel> Customer = CustomerCollection.AsQueryable<CustomerModel>().ToList();
            return View(Customer);            
        }

        /*
        public ActionResult Details(string cedulaX)
        {
            //var pacientesId = new ObjectId(id);
            var pacientes = pacienteCollection.AsQueryable<PacienteModel>().SingleOrDefault(x => x.cedula == cedulaX);
            return View(pacientes);
        }*/

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CustomerModel Customer)
        {
            try
            {
                CustomerCollection.InsertOneAsync(Customer);

                return RedirectToAction("Login");
            }
            catch
            {
                return View();
            }
        }

        /*
       
        public ActionResult Edit(string id)
        {
            var CustomerId = new ObjectId(id);
            var Customer = CustomerCollection.AsQueryable<CustomerModel>().SingleOrDefault(x => x.Id == CustomerId);
            return View(Customer);

        }

        [HttpPost]
        public ActionResult Edit(string id, CustomerModel Customer)
        {
            try
            {
                var filter = Builders<CustomerModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<CustomerModel>.Update.Set("Name", Customer.Name);//Se puede agregar mas haciendo un .Set("",) extra
                var result = CustomerCollection.UpdateOne(filter, update);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        } */

        /*
        public ActionResult Delete(string id)
        {
            var CustomerId = new ObjectId(id);
            var Customer = CustomerCollection.AsQueryable<CustomerModel>().SingleOrDefault(x => x.Id == CustomerId);
            return View(Customer);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, CustomerModel customer)
        {
            try
            {
                CustomerCollection.DeleteOne(Builders<CustomerModel>.Filter.Eq("_id", ObjectId.Parse(id)));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        } */

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        //La siguiente funcion recibe el nombre del usuario y la contraseña indicados en la app
        public ActionResult Login(CustomerModel user, string password)
        {
            var usr = CustomerCollection.AsQueryable<CustomerModel>().SingleOrDefault(u => u.UserName == user.UserName && u.Password == password);
            //var usr = funcionarioCollection.FindAsync(u => u.Nombre == user.Nombre && u.Contraseña == user.Contraseña);
            if (usr != null)
            {
                TempData["_id"] = usr._id;

                return RedirectToAction("DestinationsList"); 
            }
            else
            {
                ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos");
                return View();
            }
            

        }





        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(WishListModel WishList)
        {
            try
            {
                WishListCollection.InsertOneAsync(WishList);

                return RedirectToAction("DestinationsList");
            }
            catch
            {
                return View();
            }
        }
    }
}