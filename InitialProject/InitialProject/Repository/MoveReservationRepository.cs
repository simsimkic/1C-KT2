using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Serializer;

namespace TravelAgency.Repository
{
    internal class MoveReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/moveReservationRequest.csv";
        private readonly Serializer<MoveReservation> _serializer;
        private List<MoveReservation> reservations;

        public MoveReservationRepository() 
        {
            _serializer = new Serializer<MoveReservation>();
            reservations = _serializer.FromCSV(FilePath);
        }

        public List<MoveReservation> GetAll()
        {
            return reservations;
        }

        public MoveReservation GetById(int id)
        {
            List<MoveReservation> reservations = GetAll();
            foreach(MoveReservation reservation in reservations)
            {
                if (reservation.ReservationId == id)
                    return reservation;
            }
            return null;
        }

        public void Delete(MoveReservation reservation)
        {
            reservations = _serializer.FromCSV(FilePath);
            MoveReservation founded = reservations.Find(c => c.ReservationId == reservation.ReservationId);
            reservations.Remove(founded);
            _serializer.ToCSV(FilePath, reservations);
        }

        public MoveReservation Save(MoveReservation moveReservation)
        {
            reservations = _serializer.FromCSV(FilePath);
            reservations.Add(moveReservation);
            _serializer.ToCSV(FilePath, reservations);
            return moveReservation;
        }
    }
}
