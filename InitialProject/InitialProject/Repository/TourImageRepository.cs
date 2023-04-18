using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Serializer;

namespace TravelAgency.Repository
{
    internal class TourImageRepository
    {
        private const string FilePath = "../../../Resources/Data/tours.csv";
        private const string FilePathForImages = "../../../Resources/Data/toursImg.csv";

        private readonly Serializer<HotelImage> _serializer;

        private List<HotelImage> toursImages;
        private List<HotelImage> tours;

        public TourImageRepository()
        {
            _serializer = new Serializer<HotelImage>();
            toursImages = _serializer.FromCSV(FilePathForImages);
            tours = _serializer.FromCSV(FilePath);
        }

        public HotelImage Save(HotelImage tourImage)
        {
            tourImage.HotelId = NextId();
            toursImages = _serializer.FromCSV(FilePathForImages);
            toursImages.Add(tourImage);
            _serializer.ToCSV(FilePathForImages, toursImages);
            return tourImage;
        }

        public int NextId()
        {
            tours = _serializer.FromCSV(FilePath);
            if (tours.Count < 1)
            {
                return 1;
            }
            return tours.Count;
        }

    }
}

