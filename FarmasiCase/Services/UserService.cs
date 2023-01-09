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
        //create -> register için,
        //sifre kontrolu authenticate fonksiyonu yazılıcak
        private readonly IMongoCollection<User> Users;

        public UserService(IOptions<DatabaseSettings> settings)
        {
            Users = new MongoClient(settings.Value.ConnectionString).GetDatabase(settings.Value.DatabaseName).GetCollection<User>("Users");
        }
        //user? nullable 
        public User Authenticate(string username,string password)
        {
            //Empty kullanimi: bir filtre koymuyorsun
            List<User> kullanicilar = Users.Find(FilterDefinition<User>.Empty).ToList();
            //singleofDefault find ve firstordefaultun birleşmiş hali diye dusunebliriz
            User? kullanici = kullanicilar.SingleOrDefault(u => u.username == username && u.password == password);
            return kullanici;
        }



    }
}
