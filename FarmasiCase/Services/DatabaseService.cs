using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using FarmasiCase.Models;


namespace FarmasiCase.Services
{
    public class DatabaseService
    {
        private readonly IMongoDatabase DatabaseCollection;
            

        public DatabaseService(IOptions<DatabaseSettings> settings)
        {
            DatabaseCollection = new MongoClient(settings.Value.ConnectionString).GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return DatabaseCollection.GetCollection<T>(name);
        }

    }
}
