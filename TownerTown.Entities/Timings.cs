namespace TownerTown.Entities
{
    public class Timings
    {
        public int ID { get; set; }
        public virtual DayToTimeMapping Monday {get; set;}
        public virtual DayToTimeMapping Tuesday { get; set; }
        public virtual DayToTimeMapping Wednesday { get; set; }
        public virtual DayToTimeMapping Thursday { get; set; }
        public virtual DayToTimeMapping Friday { get; set; }
        public virtual DayToTimeMapping Saturday { get; set; }
        public virtual DayToTimeMapping Sunday { get; set; }
    }
}