using System;
using System.Collections.Generic;
using Cake.Core.IO;
using System.IO;
using TravelAgency.Domain.Model;
using TravelAgency.Serializer;

namespace TravelAgency.Repository.GradeRepo
{
    internal class GradeGuest1Repository
    {
        private const string FilePathGuestRatingde = "../../../Resources/Data/GuestRating.csv";
        private readonly Serializer<GuestGrade> _serializer;
        private List<GuestGrade> grades;

        public GradeGuest1Repository()
        {
            _serializer = new Serializer<GuestGrade>();
            grades = _serializer.FromCSV(FilePathGuestRatingde);
        }

        public GuestGrade Save(GuestGrade grade)
        {
            grades = _serializer.FromCSV(FilePathGuestRatingde);
            grades.Add(grade);
            _serializer.ToCSV(FilePathGuestRatingde, grades);
            return grade;
        }
        public List<GuestGrade> GetAll()
        {    
            return grades;
        }
    }
}
