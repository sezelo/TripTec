using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace TRIP.Models
{
    public class ManagerModel
    {
        [BsonId]
        public string _id { get; set; }
        [BsonElement("UserName")]
        public string UserName { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; }



    }
}