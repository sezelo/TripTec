using Neo4j.Driver.V1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRIP.Models;

namespace TRIP.Controllers
{
    public class CAdministradorController : Controller
    {
        private IDriver driver;

        public CAdministradorController()
        {
            driver = GraphDatabase.Driver("bolt://localhost:11004", AuthTokens.Basic("neo4j", "1234"));
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult historial(string name)
        {
            using (ISession session = driver.Session())
            {
                var ReservationModels = new List<ReservationModel2>();

                session.ReadTransaction(tx =>
                {
                    var result = tx.Run("MATCH(n: Reservation) -[:Reservation_Customer]->(m: Customer) where m.Name = \"" + name + "\" return n");
                    foreach (var record in result)
                    {
                        var nodeProps = JsonConvert.SerializeObject(record[0].As<INode>().Properties);

                        ReservationModels.Add(JsonConvert.DeserializeObject<ReservationModel2>(nodeProps));

                    }
                });
                return View(ReservationModels);
            }
        }

        public ActionResult TodosLosSitios()
        {
            using (ISession session = driver.Session())
            {
                var DestinationModels = new List<DestinationModel>();

                session.ReadTransaction(tx =>
                {
                    var result = tx.Run("MATCH (r:Reservation) -[:Reservation_Destination]-> (d:Destination) return d");
                    foreach (var record in result)
                    {
                        var nodeProps = JsonConvert.SerializeObject(record[0].As<INode>().Properties);

                        DestinationModels.Add(JsonConvert.DeserializeObject<DestinationModel>(nodeProps));

                    }
                });
                return View(DestinationModels);
            }
        }

        public ActionResult Sitios5()
        {
            using (ISession session = driver.Session())
            {
                var DestinationModels = new List<DestinationModel>();

                session.ReadTransaction(tx =>
                {
                    var result = tx.Run("MATCH (n:Destination)-[b:Reservation_Destination]-(r:Reservation) RETURN distinct n, COUNT(b) ORDER BY COUNT(b) DESC LIMIT 5");
                    foreach (var record in result)
                    {
                        var nodeProps = JsonConvert.SerializeObject(record[0].As<INode>().Properties);

                        DestinationModels.Add(JsonConvert.DeserializeObject<DestinationModel>(nodeProps));

                    }
                });
                return View(DestinationModels);
            }
        }

        public ActionResult Common(string Customer)
        {
            string zzz = Customer;
            using (ISession session = driver.Session())
            {
                var CustomerModels = new List<CustomerModel2>();

                session.ReadTransaction(tx =>
                {
                    var result = tx.Run("MATCH (e:Destination)<-[:Reservation_Destination]-(n:Reservation{CustomerN:\"" + zzz + "\"}),(d:Destination)<-[:Reservation_Destination]-(f:Reservation)-[:Reservation_Customer]->(c:Customer) WHERE e._id = d._id return c");
                    foreach (var record in result)
                    {
                        var nodeProps = JsonConvert.SerializeObject(record[0].As<INode>().Properties);

                        CustomerModels.Add(JsonConvert.DeserializeObject<CustomerModel2>(nodeProps));

                    }
                });
                return View(CustomerModels);
            }
        }

    }
}