namespace TownerTown.Entities
{
    public class Location
    {
        public int ID { get; set; }
        public virtual string AddressLine1 { get; set; }
        public virtual string AddressLine2 { get; set; }
        public virtual string Area { get; set; }
        public virtual string City { get; set; }
        public virtual long PinCode { get; set; }
        public virtual string State { get; set; }
        public virtual string Country { get; set; }
        public virtual string Longitude { get; set; }
        public virtual string Lattitude { get; set; }
    }
}