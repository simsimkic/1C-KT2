using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TravelAgency.Domain.Model;
using TravelAgency.Serializer;

namespace TravelAgency.Repository
{
    internal class ReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/reservations.csv";

        private readonly Serializer<Reservation> _serializer;

        private List<Reservation> reservations;

        public ReservationRepository()
        {
            _serializer = new Serializer<Reservation>();
            reservations = _serializer.FromCSV(FilePath);
        }

        public Reservation Save(Reservation reservation)
        {
            reservations = _serializer.FromCSV(FilePath);
            reservations.Add(reservation);
            _serializer.ToCSV(FilePath, reservations);
            return reservation;
        }

        public int NextId()
        {
            reservations = _serializer.FromCSV(FilePath);
            if (reservations.Count < 1)
            {
                return 1;
            }
            return reservations.Max(r => r.Id) + 1;
        }

        public List<Reservation> GetAll()
        {
            return reservations;
        }

        public void Delete(Reservation reservation)
        {
            reservations = _serializer.FromCSV(FilePath);
            Reservation founded = reservations.Find(c => c.Id == reservation.Id);
            reservations.Remove(founded);
            _serializer.ToCSV(FilePath, reservations);
        }

        public Reservation Update(Reservation reservation)
        {
            reservations = _serializer.FromCSV(FilePath);
            Reservation current = reservations.Find(c => c.Id == reservation.Id);
            int index = reservations.IndexOf(current);
            reservations.Remove(current);
            reservations.Insert(index, reservation);
            _serializer.ToCSV(FilePath, reservations);
            return reservation;
        }        
    }
}
