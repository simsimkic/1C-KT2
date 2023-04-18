using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Model
{
    internal class GuestGrade : Serializer.ISerializable
    {
        public string GuestUserName { get; set; }
        public int Cleanliness { get; set; }
        public int Respecting { get; set; }
        public string CommentText { get; set; }
        public int ReservationId { get; set; }

        public GuestGrade() { }

        public GuestGrade(string guestUserName, int cleanilness, int respecting, string commentText, int reservationId)
        {
            GuestUserName = guestUserName;
            Cleanliness = cleanilness;
            Respecting = respecting;
            CommentText = commentText;
            ReservationId = reservationId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { GuestUserName, Cleanliness.ToString(), Respecting.ToString(), CommentText, ReservationId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            GuestUserName = values[0];
            Cleanliness = Convert.ToInt32(values[1]);
            Respecting = Convert.ToInt32(values[2]);
            CommentText = values[3];
            ReservationId = Convert.ToInt32(values[4]);
        }
    }
}
