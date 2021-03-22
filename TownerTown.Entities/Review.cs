using System;
using System.Collections.Generic;
using System.Text;

namespace TownerTown.Entities
{
    public class Review
    {
        public int ReviewID { get; set; }
        public int Rating { get; set; }
        public string ReviewMessage { get; set; }
        public DateTime ReviewdOn { get; set; }
        public string Name { get; set; }
        public long MobileNumber { get; set; }

    }
}
