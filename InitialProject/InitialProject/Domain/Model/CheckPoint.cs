using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Model
{
    public class CheckPoint : Serializer.ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Status { get; set; }

        public CheckPoint() { }
        public CheckPoint(int id, string name)
        {

            Id = id;
            Name = name;
            Status = "Neaktivna";
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, Status };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Status = values[2];
        }

        public override string ToString()
        {
            return Id + "|" + Name + "|" + Status;
        }
    }
}
