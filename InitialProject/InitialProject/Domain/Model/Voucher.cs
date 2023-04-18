using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Model
{
    internal class Voucher : TravelAgency.Serializer.ISerializable
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Voucher() { }
        public Voucher(int id, string name, DateTime expirationDate)
        {
            Id = id;
            Name = name;
            ExpirationDate = expirationDate;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, ExpirationDate.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            ExpirationDate = Convert.ToDateTime(values[2]);
        }


    }
}
