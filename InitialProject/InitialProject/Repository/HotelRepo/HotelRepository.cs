using TravelAgency.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using TravelAgency.Domain.Model;


namespace TravelAgency.Repository.HotelRepo
{
    public class HotelRepository
    {
        private const string FilePath = "../../../Resources/Data/hotels.csv";

        private readonly Serializer<Hotel> _serializer;

        private List<Hotel> hotels;

        public HotelRepository()
        {
            _serializer = new Serializer<Hotel>();
            hotels = _serializer.FromCSV(FilePath);
        }

        public Hotel Save(Hotel hotel)
        {
            hotel.Id = NextId();
            hotels = _serializer.FromCSV(FilePath);
            hotels.Add(hotel);
            _serializer.ToCSV(FilePath, hotels);
            return hotel;
        }

        public int NextId()
        {
            hotels = _serializer.FromCSV(FilePath);
            if (hotels.Count < 1)
            {
                return 1;
            }
            return hotels.Max(h => h.Id) + 1;
        }

        public List<Hotel> GetAll()
        {
            return hotels;
        }
    }
}

