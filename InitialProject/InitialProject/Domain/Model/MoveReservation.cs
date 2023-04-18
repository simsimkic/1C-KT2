using Microsoft.Graph.Drives.Item.Items.Item.GetActivitiesByIntervalWithStartDateTimeWithEndDateTimeWithInterval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Serializer;

namespace TravelAgency.Domain.Model
{
    public class MoveReservation : ISerializable
    {
        public int ReservationId { get; set; }
        public string HotelName { get; set; }
        public string GuestUsername { get; set; }
        public DateTime OldStartDate { get; set; }
        public DateTime OldEndDate { get; set; }
        public DateTime NewStartDate { get; set; }
        public DateTime NewEndDate { get; set; }

        public MoveReservation() { }
        public MoveReservation(int reservationId, string hotelName, string guestUsername, DateTime oldStartDate, DateTime odlEndDate, DateTime newStartDate, DateTime newEndDate)
        {
            ReservationId = reservationId;
            HotelName = hotelName;
            GuestUsername = guestUsername;
            OldStartDate = oldStartDate;
            OldEndDate = odlEndDate;
            NewStartDate = newStartDate;
            NewEndDate = newEndDate;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {ReservationId.ToString(), HotelName, GuestUsername, OldStartDate.ToString(), OldEndDate.ToString(), NewStartDate.ToString(), NewEndDate.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            ReservationId = Convert.ToInt32(values[0]);
            HotelName = values[1];
            GuestUsername = values[2];
            OldStartDate = Convert.ToDateTime(values[3]);
            OldEndDate = Convert.ToDateTime(values[4]);
            NewStartDate = Convert.ToDateTime(values[5]);
            NewEndDate = Convert.ToDateTime(values[6]);
        }
    }
}
