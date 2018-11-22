using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TRIP.Models
{
    public class WishListModel
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("Customer_id")]
        public string Customer_id { get; set; }
        [BsonElement("Destination_id")]
        public string Destination_id { get; set; }
    }

    
}