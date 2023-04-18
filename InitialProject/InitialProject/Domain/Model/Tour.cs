using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelAgency.Domain.Model;

public class Tour : TravelAgency.Serializer.ISerializable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Description { get; set; }
    public string Lenguage { get; set; }
    public int MaxNumberOfGuests { get; set; }
    public int CurentNumberOfGuests { get; set; }
    public List<CheckPoint> CheckPoints { get; set; }
    public DateTime StartTime { get; set; }
    public int TourDuration { get; set; }

    public string TourStatus { get; set; }

    public Tour() {
    }
    public Tour(int id, string name, string city, string country, string description, string lenguage, int maxNumberOfGuests, DateTime startTime, int tourDuration, List<CheckPoint> checkPoints)
    {
        Id = id;
        Name = name;
        City = city;
        Country = country;
        Description = description;
        Lenguage = lenguage;
        MaxNumberOfGuests = maxNumberOfGuests;
        CurentNumberOfGuests = 0;
        StartTime = startTime;
        TourDuration = tourDuration;
        CheckPoints = checkPoints;
        TourStatus = "Nezapoceta";

    }

    public string[] ToCSV()
    {
        string CheckPointsList = null;
        int currentIndex = 0;
        foreach (CheckPoint point in CheckPoints) 
        {
            string delimiter = "|";
            if (currentIndex == CheckPoints.Count-1) delimiter = "";
            CheckPointsList = CheckPointsList + point.Id.ToString() + "|" + point.Name + "|" + point.Status + delimiter;
            currentIndex++;
        }
        string[] csvValues = { Id.ToString(), Name, City, Country, Description, Lenguage, MaxNumberOfGuests.ToString(), CurentNumberOfGuests.ToString(), StartTime.ToString(), TourDuration.ToString(), TourStatus, CheckPointsList};
        return csvValues;
    }



    public void FromCSV(string[] values)
    {
        int i = 11;
        int j = 12;
        int k = 13;
        List<CheckPoint> checkPoints = new List<CheckPoint>();
        while (k <= values.Count())
        {
            CheckPoint checkPoint = new CheckPoint();
            checkPoint.Id = Convert.ToInt32(values[i]);
            checkPoint.Name = values[j];
            checkPoint.Status = values[k];
            checkPoints.Add(checkPoint);
            i = i + 3;
            j = j + 3;
            k = k + 3;
        }
        CheckPoints = checkPoints;
        Id = Convert.ToInt32(values[0]);
        Name = values[1];
        City = values[2];
        Country = values[3];
        Description = values[4];
        Lenguage = values[5];
        MaxNumberOfGuests = Convert.ToInt32(values[6]);
        CurentNumberOfGuests = Convert.ToInt32(values[7]);
        StartTime = Convert.ToDateTime(values[8]);
        TourDuration = Convert.ToInt32(values[9]);
        TourStatus = values[10];

    }
}
