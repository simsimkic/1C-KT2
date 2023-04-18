using System.Collections.Generic;
using System.Linq;
using TravelAgency.Domain.Model;
using TravelAgency.Repository.GradeRepo;
using TravelAgency.Repository.UserRepo;

namespace TravelAgency.Services
{
    internal class OwnerService
    {
        private readonly OwnerGradeRepository ownerGradeRepository;
        private readonly UserRepository userRepository;
        public OwnerService()
        {
            ownerGradeRepository = new OwnerGradeRepository();
            userRepository = new UserRepository();
        }

        public int CountGradesFromOwnerRating(string OwnerUserName)
        {
            int count = 0;
            List<OwnerGrade> grades = ownerGradeRepository.GetAll();
            foreach(OwnerGrade grade in grades)
            {
                if (grade.OwnerUsername == OwnerUserName)
                    count++;
            }
            return count;
        }
        public int GetAverageOwnerRating(string OwnerUserName)
        {
            int Grade = 0;
            List<OwnerGrade> grades = ownerGradeRepository.GetAll();
            foreach (OwnerGrade grade in grades)
            {
                if (grade.OwnerUsername == OwnerUserName)
                    Grade = Grade + grade.OwnerRating;
            }
            return Grade / CountGradesFromOwnerRating(OwnerUserName);
        }

        public string SuperOwner(string username)
        {
            if (CountGradesFromOwnerRating(username) >= 50)
            {
                if (GetAverageOwnerRating(username) < 9.5)
                {
                    return "Owner";
                }
                else
                {
                    return "Super-Owner";
                }
            }
            else
            {
                return "Owner";
            }

        }

        public List<User> GetAllOwners()
        {
            List<User> users = userRepository.GetAll();
            List<User> owners = new List<User>();
            foreach (User user in users)
            {
                if(user.LoginRole == "Owner")
                    owners.Add(user);
            }
            return owners;

        }

        public List<User> GetAllSuperOwners()
        {
            List<User> owners = GetAllOwners();
            List<User> superOwners = new List<User>();
            foreach (User owner in owners)
            {
                if (SuperOwner(owner.Username) == "Super-Owner")
                    superOwners.Add(owner);
            }
            return superOwners.Distinct().ToList();

        }
    }
}
