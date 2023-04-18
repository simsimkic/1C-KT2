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
    class CheckPointRepository
    {
        private const string FilePath = "../../../Resources/Data/checkPoints.csv";

        private readonly Serializer<CheckPoint> _serializer;

        private List<CheckPoint> _checkPoints;

        public CheckPointRepository()
        {
            _serializer = new Serializer<CheckPoint>();
            _checkPoints = _serializer.FromCSV(FilePath);
        }

        public List<CheckPoint> ReadFromCheckPointsCsv(string FileName)
        {
            List<CheckPoint> checkPoints = new List<CheckPoint>();

            using (StreamReader sr = new StreamReader(FileName))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] fields = line.Split('|');
                    CheckPoint checkPoint = new CheckPoint();
                    checkPoint.Id = Convert.ToInt32(fields[0]);
                    checkPoint.Name = fields[1];
                    checkPoint.Status = fields[2];


                    checkPoints.Add(checkPoint);

                }
            }
            return checkPoints;
        }

        public CheckPoint GetByName(string FileName,string name)
        {
            using (StreamReader sr = new StreamReader(FileName))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] fields = line.Split('|');
                    CheckPoint checkPoint = new CheckPoint();
                    checkPoint.Id = Convert.ToInt32(fields[0]);
                    checkPoint.Name = fields[1];
                    checkPoint.Status = fields[2];  

                    if(checkPoint.Name == name)
                    return checkPoint;

                }
            }
            return null;
        }

        public CheckPoint Update(CheckPoint checkPoint)
        {
            _checkPoints = _serializer.FromCSV(FilePath);
            CheckPoint current = _checkPoints.Find(c => c.Id == checkPoint.Id);
            int index = _checkPoints.IndexOf(current);
            _checkPoints.Remove(current);
            _checkPoints.Insert(index, checkPoint);
            _serializer.ToCSV(FilePath, _checkPoints);
            return checkPoint;
        }
    }
}
