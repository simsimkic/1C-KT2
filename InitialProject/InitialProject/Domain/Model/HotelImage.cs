using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Serializer;

namespace TravelAgency.Domain.Model
{
    internal class HotelImage : ISerializable
    {
        public int HotelId { get; set; }
        public string Url { get; set; }

        public HotelImage() { }

        public HotelImage(int hotelId, string url)
        {
            HotelId = hotelId;
            Url = url;
        }

        public void FromCSV(string[] values)
        {
            HotelId = Convert.ToInt32(values[0]);
            Url = values[1];
        }

        public string[] ToCSV()
        {
            string[] csvValues = { HotelId.ToString(), Url };
            return csvValues;
        }

    }
}
