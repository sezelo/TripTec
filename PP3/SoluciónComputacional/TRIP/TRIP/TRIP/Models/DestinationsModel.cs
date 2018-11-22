using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TRIP.Models
{
    public class DestinationsModel
    {
        [BsonElement("_id")]
        public string _id { get; set; }
        [BsonElement("Lat")]
        public double Lat { get; set; }
        [BsonElement("Long")]
        public double Long { get; set; }
        [BsonElement("Direction")]
        public string Direction { get; set; }
        [BsonElement("Description")]
        public string Description { get; set; }
        [BsonElement("Activities")]
        public string Activities { get; set; }
        [BsonElement("PricePP")]
        public string PricePP { get; set; }
    }
}