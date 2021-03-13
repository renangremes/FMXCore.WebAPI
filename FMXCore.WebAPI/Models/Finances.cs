using System;

namespace FMXCore.WebAPI.Models
{
    public class Finance
    {
        public int Id { get; set; }
        public String Description { get; set; }
        public Nullable<DateTime> Date { get; set;}
        public Nullable<DateTime> DueDate { get; set; }
        public Double Value { get; set; }
        public String Type { get; set; }
        public User User { get; set; }
        public String UserDocument { get; set; }
    }
}
