using KartRacing.Models;

namespace KartRacing.Services
{
    public class RaceService
    {
        private readonly TimingService _timingService;

        public RaceService()
        {
            _timingService = new TimingService();
        }

        public void Start()
        {
            var timings = _timingService.GetTimings();

            // Print the generated timings
            _timingService.PrintMockTimings(timings);

            var winner = FindWinner(timings);
            // Print winner
            Console.WriteLine($"{winner.Kart} won with time {winner.PassingTime}");
            
            // Assuming fastest lap can also be achieved by someone other than winner
            var fastestLap = FindFastestLap(timings);
            // Print fastest lap
            Console.WriteLine($"Fastest Lap: {fastestLap.fastestPassing.Kart} Lap: {fastestLap.fastestPassing.Lap} with time: {fastestLap.fastestTime}");
        }

        // Returns Kart with smallest finish time - start time
        // Used Passing object because all the information needed can be stored in this data type
        public Passing FindWinner(IEnumerable<Passing> timings)
        {
            if (timings == null || !timings.Any())
            {
                return new Passing { Kart = new Kart(0), PassingTime = TimeSpan.Zero };
            }

            var winner = timings
                .GroupBy(p => p.Kart.KartNumber)
                .Select(g => new
                {
                    Kart = new Kart(g.Key),
                    TotalTime = CalculateTotalTime(g.OrderBy(p=> p.PassingTime).ToList())
                })
                .OrderBy(t => t.TotalTime)
                .FirstOrDefault();      // ignored the case of a tie
            
            return new Passing
            {
                Kart = winner.Kart,
                PassingTime = winner.TotalTime
            };
        }

        // Returns Tuple, fastest Passing with its lap time
        public (Passing fastestPassing, TimeSpan fastestTime) FindFastestLap(IEnumerable<Passing> timings)
        {
            // for each kart we need to calculate lap times and return minimum

            if (timings == null || !timings.Any())
            {
                return (new Passing { Kart = new Kart(0), PassingTime = TimeSpan.Zero, Lap = 0 }, TimeSpan.Zero);
            }

            var laps = timings
                .GroupBy(p => p.Kart.KartNumber);

            var bestLapPerKart = new Dictionary<Passing, TimeSpan>();
            foreach (var lap in laps)
            {
                if (lap.Count() < 2)
                {
                    //continue;
                    return (new Passing { Kart = new Kart(0), PassingTime = TimeSpan.Zero, Lap = 0 }, TimeSpan.Zero);
                }

                var times = lap.OrderBy(l => l.PassingTime).ToList();
                var minLapTime = TimeSpan.MaxValue;
                Passing fastestPassing = null;

                for (int i=1; i<times.Count; i++)
                {
                    var lapTime = times[i].PassingTime - times[i - 1].PassingTime;
                    if (lapTime < minLapTime)
                    {
                        minLapTime = lapTime;
                        fastestPassing = times[i];
                    }
                }

                if (fastestPassing != null)
                {
                    bestLapPerKart.Add(fastestPassing, minLapTime);
                }
            }
            var bestLap = bestLapPerKart.OrderBy(t => t.Value).FirstOrDefault();
            return (bestLap.Key, bestLap.Value);
        }

        // Total time for a Kart is FinishTime - StartTime
        private TimeSpan CalculateTotalTime(IEnumerable<Passing> passings)
        {
            if (passings.Count() < 2)
            {
                return TimeSpan.Zero;
            }
            return passings.Last().PassingTime - passings.First().PassingTime;
        }
    }
}
