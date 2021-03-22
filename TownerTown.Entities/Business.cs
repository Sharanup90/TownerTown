using System;
using System.Collections.Generic;

namespace TownerTown.Entities
{
    public class Business
    {
        public int ID { get; set; }
        public virtual string BusinessID { get; set; }
        public virtual string BusinessName { get; set; }
        public virtual string TagLine { get; set; }
        public virtual string RegistrationNumber { get; set; }
        public virtual User BusinessOwner { get; set; }
        public virtual DateTime RegisteredOn { get; set; }
        public virtual string TelephoneNumber { get; set; }
        public virtual List<Contact> Contacts { get; set; }
        public virtual List<FilePath> Images { get; set; }
        public virtual Location Address { get; set; }
        public virtual Timings Timings { get; set; }
        public virtual string About { get; set; }
        public virtual string Products { get; set; }
        public virtual Category Category { get; set; }
        public virtual string WebsiteLink { get; set; }
        public virtual MembershipType Membership { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual List<Review> Reviews { get; set;}
        public virtual int OverallRating { get; set; }
        public bool IsBlocked { get; set; }
        public string BlockReason { get; set; }

    }
}
