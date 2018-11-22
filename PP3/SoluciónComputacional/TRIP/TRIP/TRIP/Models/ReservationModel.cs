using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TRIP.Models
{
    public class ReservationModel
    {

        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("Qty")]
        public string Qty { get; set; }
        [BsonElement("CheckIn")]
        public string CheckIn { get; set; }
        [BsonElement("CheckOut")]
        public string CheckOut { get; set; }
        [BsonElement("SpecialNeeds")]
        public string SpecialNeeds{ get; set; }
        [BsonElement("Services")]
        public string Services { get; set; }
        [BsonElement("Destination")]
        public string Destination { get; set; }

        [BsonElement("TotalPrice")]
        public string TotalPrice { get; set; }

        [BsonElement("Idc")]
        public string Idc { get; set; }



    }
}