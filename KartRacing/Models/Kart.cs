namespace KartRacing.Models
{
    public class Kart
    {
        public int KartNumber { get; }

        // Can be added later
        // public string Driver { get; set; }

        public Kart(int KartNumber) => this.KartNumber = KartNumber;

        public override string ToString()
        {
            return $"Kart: {KartNumber}";
        }
    }
}
