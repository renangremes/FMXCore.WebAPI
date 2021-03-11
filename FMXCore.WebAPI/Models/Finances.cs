using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMXCore.WebAPI.Models
{
    public class Finance
    {
        public int Id { get; set; }
        public String Description { get; set; }
        public DateTime Date { get; set; }
        public Decimal Value { get; set; }
        public String Type { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
