using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TRIP.Models
{
    public class CustomerModel
    {
        // GET: Customer
        [BsonElement("_id")]
        public string _id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("UserName")]
        public string UserName { get; set; }
        [BsonElement("Telephone")]
        public int Telephone { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("Birthday")]
        public string Birthday { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; }   

}
}