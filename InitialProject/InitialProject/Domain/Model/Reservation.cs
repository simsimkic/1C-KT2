using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kiota.Abstractions;

namespace TravelAgency.Domain.Model
{
    internal class Reservation : Serializer.ISerializable
    {
        public int Id { get; set; }
        public string GuestUserName { get; set; }
        public string HotelName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfDays { get; set; }
        public int NumberOfGuests { get; set; }
        public string GradeStatus { get; set; }

        public Reservation() { }

        public Reservation(int id, string guestUserName, string hotelName, DateTime startDate, DateTime endDate, int numberOfDays, int numberOfGuests)
        {
            Id = id;
            GuestUserName = guestUserName;
            HotelName = hotelName;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfDays = numberOfDays;
            NumberOfGuests = numberOfGuests;
            GradeStatus = "NotGraded";
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), GuestUserName, HotelName, StartDate.ToString(), EndDate.ToString(), NumberOfDays.ToString(), NumberOfGuests.ToString(), GradeStatus };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuestUserName = values[1];
            HotelName = values[2];
            StartDate = Convert.ToDateTime(values[3]);
            EndDate = Convert.ToDateTime(values[4]);
            NumberOfDays = Convert.ToInt32(values[5]);
            NumberOfGuests = Convert.ToInt32(values[6]);
            GradeStatus = values[7];
        }
    }
}
