using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using TravelAgency.Serializer;
using System.IO;

namespace TravelAgency.Domain.Model
{
    public class Hotel : Serializer.ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string TypeOfHotel { get; set; }
        public int MaxNumberOfGuests { get; set; }
        public int MinNumberOfDays { get; set; }
        public int NumberOfDaysToCancel { get; set; }
        public string OwnerUsername { get; set; }

        public Hotel() { }

        public Hotel(int id, string name, string city, string country, string typeOfHotel, int maxNumberOfGuests, int minNumberOfDays, int numberOfDaysToCancel)
        {
            Id = id;
            Name = name;
            City = city;
            Country = country;
            TypeOfHotel = typeOfHotel;
            MaxNumberOfGuests = maxNumberOfGuests;
            MinNumberOfDays = minNumberOfDays;
            NumberOfDaysToCancel = numberOfDaysToCancel;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, City, Country, TypeOfHotel, MaxNumberOfGuests.ToString(), MinNumberOfDays.ToString(), NumberOfDaysToCancel.ToString(), OwnerUsername };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            City = values[2];
            Country = values[3];
            TypeOfHotel = values[4];
            MaxNumberOfGuests = Convert.ToInt32(values[5]);
            MinNumberOfDays = Convert.ToInt32(values[6]);
            NumberOfDaysToCancel = Convert.ToInt32(values[7]);
            OwnerUsername = values[8];
        }
    }
}
