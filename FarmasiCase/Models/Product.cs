using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FarmasiCase.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement ("name")]
        public string name { get; set; }
        [BsonElement("price")]
        public double price { get; set; }

        public Product(string name, double price)
        {
            this.name = name;
            this.price = price;

        }
    }
}
