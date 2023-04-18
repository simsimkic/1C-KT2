using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Serializer;

namespace TravelAgency.Domain.Model
{
    public class OwnerGrade : ISerializable
    {
        public string Guest1Username { get; set; }
        public string OwnerUsername { get; set; }
        public int ReservationId { get; set; }
        public int HotelRating { get; set; }
        public int OwnerRating { get; set; }
        public string Comment { get; set; }

        public OwnerGrade() { }

        public OwnerGrade(string guset1Username, string ownerUsername, int reservationId, int hotelRating, int ownerRating, string comment)
        {
            Guest1Username = guset1Username;
            OwnerUsername = ownerUsername;
            ReservationId = reservationId;
            HotelRating = hotelRating;
            OwnerRating = ownerRating;
            Comment = comment;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Guest1Username, OwnerUsername, ReservationId.ToString(), HotelRating.ToString(), OwnerRating.ToString(), Comment };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Guest1Username = values[0];
            OwnerUsername = values[1];
            ReservationId = Convert.ToInt32(values[2]);
            HotelRating = Convert.ToInt32(values[3]);
            OwnerRating = Convert.ToInt32(values[4]);
            Comment = values[5];
        }

    }
}
