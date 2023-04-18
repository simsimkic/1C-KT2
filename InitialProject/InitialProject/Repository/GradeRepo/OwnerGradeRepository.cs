using System;
using System.Collections.Generic;
using System.IO;
using TravelAgency.Domain.Model;
using TravelAgency.Serializer;

namespace TravelAgency.Repository.GradeRepo
{
    internal class OwnerGradeRepository
    {
        private const string FilePath = "../../../Resources/Data/OwnerRating.csv";
        private Serializer<OwnerGrade> _serializer;
        private List<OwnerGrade> ownerGrades;

        public OwnerGradeRepository()
        {
            _serializer = new Serializer<OwnerGrade>();
            ownerGrades = _serializer.FromCSV(FilePath);

        }

        public List<OwnerGrade> GetAll()
        {

            return ownerGrades;
        }

        public OwnerGrade GetByReservationId(int id)
        {
            foreach (OwnerGrade grade in ownerGrades)
            {
                if (grade.ReservationId == id)
                {
                    return grade;
                }
            }
            return null;
        }

        public OwnerGrade Save(OwnerGrade grade)
        {
            ownerGrades = _serializer.FromCSV(FilePath);
            ownerGrades.Add(grade);
            _serializer.ToCSV(FilePath, ownerGrades);
            return grade;
        }
    }

}
