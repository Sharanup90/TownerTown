using System;

namespace TownerTown.Entities
{
    public class DayToTimeMapping
    {
        public int ID { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool Closed { get; set; }
    }
}