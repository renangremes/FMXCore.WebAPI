using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMXCore.WebAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public DateTime BirthDate { get; set; }
        public String Adress { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Country { get; set; }
    }
}
