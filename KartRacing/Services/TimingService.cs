using KartRacing.Models;

namespace KartRacing.Services
{
    class TimingService
    {
        private const string FILE_PATH = @"Data/karttimes.csv";
        private const int NUMBER_OF_KARTS = 5;
        private const int NUMBER_OF_LAPS = 4;

        // Returns timings either from the file specified or mock data
        public IEnumerable<Passing> GetTimings()
        {
            // If there is csv file under the given path, use it
            if (File.Exists(FILE_PATH))
            {
                return GetTimingsFromFile();
            } else
            {
                Console.WriteLine("No .csv file found! Generating mock timings!");
                // else generate mock timings
                return GenerateTimings();
            }
        }

        // Returns generated timings for given no. of karts and no. of laps
        private IEnumerable<Passing> GenerateTimings()
        {
            var passings = new List<Passing>();

            // for each kart    // can be parameterized
            for (int i = 0; i < NUMBER_OF_KARTS; i++)
            {
                var kart = new Kart(i + 1);

                // create timespan = 12:00:0X, seconds will differ
                var time = new TimeSpan(12, 0, new Random().Next(3));

                // add initial passing from start line
                passings.Add(new Passing
                {
                    Kart = kart,
                    PassingTime = time
                });

                // create lap   // can be parameterized
                for (int j = 0; j < NUMBER_OF_LAPS+1; j++)
                {
                    // add between 40-60 seconds for each lap, this will define the winner
                    var seconds = new TimeSpan(hours:0, minutes:0, seconds:new Random().Next(40, 80));
                    time += seconds;
                    var passing = new Passing
                    {
                        Kart = kart,
                        Lap = j+1,  // regarding first pass from the start line
                        PassingTime = time
                    };
                    passings.Add(passing);
                }
            }

            return passings;
        }

        // Prints timings to the console to visualize the race
        public void PrintMockTimings(IEnumerable<Passing> passings)
        {
            var laps = passings
                .OrderBy(p => p.PassingTime)
                .GroupBy(p => p.Lap);

            foreach (var lap in laps)
            {
                foreach (var passing in lap)
                {
                    Console.WriteLine(passing);
                }
                Console.WriteLine("-----------------------------");
            }
        }

        // Parse timing values from the given file path
        private IEnumerable<Passing> GetTimingsFromFile()
        {
            var passings = new List<Passing>();
            var lapCounters = new Dictionary<int, int>();

            using (var reader = new StreamReader(FILE_PATH))
            {
                // Skip the header line
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line?.Split(',');
                    var kartNumber = int.Parse(values[0]);
                    lapCounters.TryGetValue(kartNumber, out var count);
                    lapCounters[kartNumber] = ++count;
                    var passing = new Passing
                    {
                        Kart = new Kart(kartNumber),
                        Lap = lapCounters[kartNumber]-1,
                        PassingTime = TimeSpan.Parse(values[1])
                    };
                    passings.Add(passing);
                }
                return passings;
            }
        }
    }
}
