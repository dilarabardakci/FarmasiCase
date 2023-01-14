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

        private readonly IMongoCollection<User> Users;

        public UserService(IOptions<DatabaseSettings> settings)
        {
            Users = new MongoClient(settings.Value.ConnectionString).GetDatabase(settings.Value.DatabaseName).GetCollection<User>("Users");
        }

        public User Authenticate(string username,string password)
        {
            //Empty kullanimi: bir filtre koymuyorsun
            List<User> kullanicilar = Users.Find(FilterDefinition<User>.Empty).ToList();
            //singleofDefault find ve firstordefaultun birleşmiş hali diye dusunebliriz
            User kullanici = kullanicilar.SingleOrDefault(u => u.username == username && u.password == password);
            return kullanici;
        }

        public User Register (string username,string password)
        {
            List<User> _users = Users.Find(FilterDefinition<User>.Empty).ToList();
            var user = _users.SingleOrDefault(u => u.username == username);
            if (user==null)
            {
                Users.InsertOne(new User(username, password));
                return Authenticate(username, password);
            }
            else
            {
                return null;
            }
        }
    }
}
