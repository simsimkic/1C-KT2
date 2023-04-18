using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Model
{
    internal class TourReview1 : TravelAgency.Serializer.ISerializable
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public int GuidesKnowlege { get; set; }
        public int GuidesLenguage { get; set; }
        public int Overall { get; set; }
        public string Comment { get; set; }

        public TourReview1() { }

        public TourReview1(int id, int tourId, int guidesKnowlege, int guidesLenguage, int overall, string comment)
        {
            Id = id;
            TourId = tourId;
            GuidesKnowlege = guidesKnowlege;
            GuidesLenguage = guidesLenguage;
            Overall = overall;
            Comment = comment;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), TourId.ToString(), GuidesKnowlege.ToString(), GuidesLenguage.ToString(), Overall.ToString(), Comment };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourId = Convert.ToInt32(values[1]);
            GuidesKnowlege = Convert.ToInt32(values[2]);
            GuidesLenguage = Convert.ToInt32(values[3]);
            Overall = Convert.ToInt32(values[4]);
            Comment = values[5];
        }

    }
}
