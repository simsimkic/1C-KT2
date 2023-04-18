using Cake.Core.IO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Serializer;

namespace TravelAgency.Repository
{
    class GuideReviewRepository
    {
        private const string FilePath = "../../../Resources/Data/guideReview.csv";
        private readonly Serializer<TourReview1> _serializer;
        private List<TourReview1> _tourreviews;

        public GuideReviewRepository()
        {
            _serializer = new Serializer<TourReview1>();
            _tourreviews = _serializer.FromCSV(FilePath);
        }

        public TourReview1 Save(TourReview1 review)
        {
            review.Id = NextId();
            _tourreviews = _serializer.FromCSV(FilePath);
            _tourreviews.Add(review);
            _serializer.ToCSV(FilePath, _tourreviews);
            return review;
        }
        public List<TourReview1> GetAll()
        {
            List<TourReview1> reviews = new List<TourReview1>();
            using (StreamReader sr = new StreamReader(FilePath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] fields = line.Split('|');
                    TourReview1 review = new TourReview1();
                    review.Id = Convert.ToInt32(fields[0]);
                    review.TourId = Convert.ToInt32(fields[1]);
                    review.GuidesKnowlege = Convert.ToInt32(fields[2]);
                    review.GuidesLenguage = Convert.ToInt32(fields[3]);
                    review.Overall = Convert.ToInt32(fields[4]);
                    review.Comment = fields[3];
                    reviews.Add(review);

                }
            }
            return reviews;
        }
        public int NextId()
        {
            _tourreviews = _serializer.FromCSV(FilePath);
            if (_tourreviews.Count < 1)
            {
                return 1;
            }
            return _tourreviews.Max(t => t.Id) + 1;
        }

    }
}


