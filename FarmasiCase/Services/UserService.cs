using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmasiCase.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace FarmasiCase.Services
{
    public class UserService
    {

        private readonly IMongoCollection<User> UserCollection;

        public UserService(IOptions<DatabaseSettings> settings)
        {
            UserCollection = new MongoClient(settings.Value.ConnectionString).GetDatabase(settings.Value.DatabaseName).GetCollection<User>("UserCollection");
        }

        public User Authenticate(string username,string password)
        {
            //Empty kullanimi: bir filtre koymuyorsun
            List<User> users = UserCollection.Find(FilterDefinition<User>.Empty).ToList();
            //singleofDefault find ve firstordefaultun birleşmiş hali diye dusunebliriz
            User user = users.SingleOrDefault(u => u.username == username && u.password == password);
            return user;
        }

        public User Register (string username,string password)
        {
            List<User> _users = UserCollection.Find(FilterDefinition<User>.Empty).ToList();
            var user = _users.SingleOrDefault(u => u.username == username);
            if (user==null)
            {
                UserCollection.InsertOne(new User(username, password));
                return Authenticate(username, password);
            }
            else
            {
                return null;
            }
        }
    }
}
