namespace KartRacing.Models
{
    public class Passing
    {
        public Kart Kart { get; set; }

        public int Lap { get; set; }

        public TimeSpan PassingTime { get; set; }

        public override string ToString()
        {
            return $"{Kart} Time: {PassingTime} - Lap: { ((Lap == 0) ? "Start" : Lap) }";
        }
    }
}
