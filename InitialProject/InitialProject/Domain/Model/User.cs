using TravelAgency.Serializer;
using System;
using System.DirectoryServices.ActiveDirectory;

namespace TravelAgency.Domain.Model
{
    public class User : ISerializable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string LoginRole { get; set; }
        public int Age { get; set; }

        public User() { }

        public User(string username, string password, string loginRole, int age)
        {
            Username = username;
            Password = password;
            LoginRole = loginRole;
            Age = age;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, LoginRole, Age.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            LoginRole = values[3];
            Age = Convert.ToInt32(values[4]);
        }
    }
}
