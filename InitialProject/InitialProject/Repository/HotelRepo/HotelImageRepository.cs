using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Serializer;

namespace TravelAgency.Repository.HotelRepo
{
    internal class HotelImageRepository
    {
        private const string FilePath = "../../../Resources/Data/hotels.csv";
        private const string FilePathForImages = "../../../Resources/Data/hotelsImg.csv";

        private readonly Serializer<HotelImage> _serializer;

        private List<HotelImage> hotelImages;
        private List<HotelImage> hotels;

        public HotelImageRepository()
        {
            _serializer = new Serializer<HotelImage>();
            hotelImages = _serializer.FromCSV(FilePathForImages);
            hotels = _serializer.FromCSV(FilePath);
        }

        public HotelImage Save(HotelImage hotelImage)
        {
            hotelImage.HotelId = NextId();
            hotelImages = _serializer.FromCSV(FilePathForImages);
            hotelImages.Add(hotelImage);
            _serializer.ToCSV(FilePathForImages, hotelImages);
            return hotelImage;
        }

        public void Delete(HotelImage hotelImage)
        {
            hotelImages = _serializer.FromCSV(FilePathForImages);
            HotelImage founded = hotelImages.Find(hi => hi.Url == hotelImage.Url);
            hotelImages.Remove(founded);
            _serializer.ToCSV(FilePathForImages, hotelImages);
        }

        public int NextId()
        {
            hotels = _serializer.FromCSV(FilePath);
            if (hotels.Count < 1)
            {
                return 1;
            }
            return hotels.Count + 1;
        }

        public List<HotelImage> GetAll()
        {
            return hotelImages;
        }
    }
}
