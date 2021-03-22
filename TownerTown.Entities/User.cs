namespace TownerTown.Entities
{
    public class User
    {
        public int ID { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string MobileNumber { get; set; }
        public virtual string WhatsAppNumber { get; set; }
        public virtual string Email { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Role { get; set; }
        public virtual string Token { get; set; }
    }
}