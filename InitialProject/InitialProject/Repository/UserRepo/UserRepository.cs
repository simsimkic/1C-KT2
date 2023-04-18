using TravelAgency.Serializer;
using System.Collections.Generic;
using System.Linq;
using TravelAgency.Domain.Model;
using System.IO;
using System;

namespace TravelAgency.Repository.UserRepo
{
    public class UserRepository
    {
        private const string FilePath = "../../../Resources/Data/users.csv";

        private readonly Serializer<User> _serializer;

        private List<User> users;

        public UserRepository()
        {
            _serializer = new Serializer<User>();
            users = _serializer.FromCSV(FilePath);
        }

        public User GetByUsername(string username)
        {
            users = _serializer.FromCSV(FilePath);
            return users.FirstOrDefault(u => u.Username == username);
        }

        public List<User> GetAll() 
        {
            return users;
        }
    }
}
