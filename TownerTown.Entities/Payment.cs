using System;

namespace TownerTown.Entities
{
    public class Payment
    {
        public int ID { get; set; }
        public string TransactionNumber { get; set; }
        public DateTime PayedOn { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public double Amount { get; set; }
        public string orderId { get; set; }
        public string razorpayKey { get; set; }
        public string currency { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string contactNumber { get; set; }
        public string address { get; set; }
        public string description { get; set; }
    }
}