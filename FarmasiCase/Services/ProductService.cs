using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using FarmasiCase.Models;
using MongoDB.Driver;

namespace FarmasiCase.Services
{
    public class ProductService
    {

        private readonly IMongoCollection<Product> Products;

        public ProductService(IOptions<DatabaseSettings> settings)
        {
            Products = new MongoClient(settings.Value.ConnectionString).GetDatabase(settings.Value.DatabaseName).GetCollection<Product>("Products");
        }

        public List<Product> Get()
        {
            return Products.Find(FilterDefinition<Product>.Empty).ToList(); 
        }

        public Product GetById(string id)
        {
            return Products.Find(p => p.Id == id).FirstOrDefault();
        }

        public void Create( Product newProduct)
        {
            Products.InsertOne(newProduct);
        }

        public void Update (string Id ,Product updatedProduct)
        {
            Products.ReplaceOne(p => p.Id == Id,updatedProduct);
        }

        public void Delete(string Id)
        {
            Products.DeleteOne(p => p.Id == Id);
        }
    }
}
