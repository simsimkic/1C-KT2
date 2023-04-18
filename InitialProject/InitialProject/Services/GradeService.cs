using System;
using System.Collections.Generic;
using TravelAgency.Domain.Model;
using TravelAgency.Repository;
using TravelAgency.Repository.GradeRepo;

namespace TravelAgency.Services
{
    internal class GradeService
    {
        private readonly OwnerGradeRepository ownerGradeRepository;
        private readonly ReservationRepository reservationRepository;
        private readonly ReservationService reservationService;
        public GradeService() 
        {
            ownerGradeRepository = new OwnerGradeRepository();
            reservationRepository = new ReservationRepository();
            reservationService = new ReservationService();
        }

        internal OwnerGrade FindOwnerGradeByReservationId(int id)
        {
            List<OwnerGrade> grades = ownerGradeRepository.GetAll();
            foreach(OwnerGrade grade in grades)
            {
                if (grade.ReservationId == id)
                {
                    return grade;
                }
            }
            return null;
        }

        public bool IsOwnerGradeExists(int id)
        {
            List<OwnerGrade> grades = ownerGradeRepository.GetAll();
            foreach (OwnerGrade grade in grades)
            {
                if (grade.ReservationId == id)
                {
                    return true;
                }
            }
            return false;
        }

        public List<OwnerGrade> ShowReviewsForOwner()
        {
            List<Reservation> allReservation = reservationRepository.GetAll();
            List<OwnerGrade> ownerGrades = new List<OwnerGrade>();
            foreach (Reservation reservation in allReservation)
            {
                if (reservation.GradeStatus == "Graded")
                {
                    if (IsOwnerGradeExists(reservation.Id))
                    {
                        ownerGrades.Add(FindOwnerGradeByReservationId(reservation.Id));
                    }
                }
            }
            return ownerGrades;
        }

        public void FindAndLogicalDeleteExpiredReservation(int i)
        {
            List<Reservation> reservations = new List<Reservation>();
            reservations = reservationRepository.GetAll();
            DateTime dateTimeNow = DateTime.Now;

            if (reservations[i].EndDate < dateTimeNow && reservations[i].EndDate.AddDays(5) < dateTimeNow)
            {
                reservationService.LogicalDeleteExpire(reservations[i]);
            }
        }

        public string ShowMessageForGrade(int i)
        {
            List<Reservation> reservations = new List<Reservation>();
            reservations = reservationRepository.GetAll();
            DateTime dateTimeNow = DateTime.Now;
            string message = null;

            if (reservations[i].EndDate < dateTimeNow && reservations[i].EndDate.AddDays(5) > dateTimeNow && reservations[i].GradeStatus == "NotGraded")
            {
                if (dateTimeNow < reservations[i].EndDate)
                {
                    message = "You have " + (5 - DateTime.DaysInMonth(dateTimeNow.Year, dateTimeNow.Month) - (dateTimeNow.Day - reservations[i].EndDate.Day)).ToString() + " days left to grade " + reservations[i].GuestUserName;
                }
                else
                {
                    message = "You have " + (5 - (dateTimeNow.Day - reservations[i].EndDate.Day)).ToString() + " days left to grade " + reservations[i].GuestUserName;
                }
            }
            return message;
        }

        public string FindGuestsForGrade(int i)
        {
            List<Reservation> reservations = reservationRepository.GetAll();
            DateTime dateTimeNow = DateTime.Now;
            if (reservations[i].EndDate < dateTimeNow && reservations[i].EndDate.AddDays(5) > dateTimeNow && reservations[i].GradeStatus == "NotGraded")
            {
                string reservationForm = reservations[i].Id.ToString() + " " + reservations[i].GuestUserName;
                return reservationForm;
            }
            else return null;
        }

        public string FindGradedGuest(int i)
        {
            List<Reservation> reservations = reservationRepository.GetAll();
            if (reservations[i].GradeStatus == "Graded")
            {
                string username = reservations[i].GuestUserName;
                return username;
            }
            else return null;
        }

        public bool IsOwnerAlreadyRatedByGuest(int reservationId, string guestUsername)
        {
            OwnerGrade ownerGrade = ownerGradeRepository.GetByReservationId(reservationId);
            if (ownerGrade != null && ownerGrade.Guest1Username == guestUsername)
            {
                return true;
            }
            return false;
        }

    }
}
