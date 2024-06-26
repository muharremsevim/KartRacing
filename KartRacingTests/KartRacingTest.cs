using KartRacing.Models;
using KartRacing.Services;

namespace KartRacingTests;

public class KartRacingTest
{
    [Fact]
    public void Test1()
    {
        var kartTimes = new List<Passing>
        {
            new Passing { Kart = new Kart(1), Lap = 0, PassingTime = new TimeSpan(hours:0, minutes:0, seconds:0) },
            new Passing { Kart = new Kart(2), Lap = 0, PassingTime = new TimeSpan(hours:0, minutes:0, seconds:1) },
            new Passing { Kart = new Kart(3), Lap = 0, PassingTime = new TimeSpan(hours:0, minutes:0, seconds:2) },
            new Passing { Kart = new Kart(4), Lap = 0, PassingTime = new TimeSpan(hours:0, minutes:0, seconds:0) },
            new Passing { Kart = new Kart(5), Lap = 0, PassingTime = new TimeSpan(hours:0, minutes:0, seconds:3) },

            new Passing { Kart = new Kart(1), Lap = 1, PassingTime = new TimeSpan(hours:0, minutes:1, seconds: 12) },
            new Passing { Kart = new Kart(2), Lap = 1, PassingTime = new TimeSpan(hours:0, minutes:1, seconds: 05) },
            new Passing { Kart = new Kart(3), Lap = 1, PassingTime = new TimeSpan(hours:0, minutes:1, seconds: 30) },
            new Passing { Kart = new Kart(4), Lap = 1, PassingTime = new TimeSpan(hours:0, minutes:1, seconds: 20) },
            new Passing { Kart = new Kart(5), Lap = 1, PassingTime = new TimeSpan(hours:0, minutes:1, seconds: 10) },

            new Passing { Kart = new Kart(1), Lap = 2, PassingTime = new TimeSpan(hours:0, minutes:2, seconds: 22) },
            new Passing { Kart = new Kart(2), Lap = 2, PassingTime = new TimeSpan(hours:0, minutes:2, seconds: 02) },
            new Passing { Kart = new Kart(3), Lap = 2, PassingTime = new TimeSpan(hours:0, minutes:2, seconds: 50) },
            new Passing { Kart = new Kart(4), Lap = 2, PassingTime = new TimeSpan(hours:0, minutes:2, seconds: 42) },
            new Passing { Kart = new Kart(5), Lap = 2, PassingTime = new TimeSpan(hours:0, minutes:2, seconds: 37) },

            new Passing { Kart = new Kart(1), Lap = 3, PassingTime = new TimeSpan(hours:0, minutes:3, seconds: 03) }, // lap 3 (fastest) = lap 2 + 41s
            new Passing { Kart = new Kart(2), Lap = 3, PassingTime = new TimeSpan(hours:0, minutes:3, seconds: 15) },
            new Passing { Kart = new Kart(3), Lap = 3, PassingTime = new TimeSpan(hours:0, minutes:3, seconds: 32) },
            new Passing { Kart = new Kart(4), Lap = 3, PassingTime = new TimeSpan(hours:0, minutes:4, seconds: 0) },
            new Passing { Kart = new Kart(5), Lap = 3, PassingTime = new TimeSpan(hours:0, minutes:3, seconds: 30) },

            new Passing { Kart = new Kart(1), Lap = 4, PassingTime = new TimeSpan(hours:0, minutes:4, seconds: 17) },
            new Passing { Kart = new Kart(2), Lap = 4, PassingTime = new TimeSpan(hours:0, minutes:4, seconds: 10) }, // winner Min(Finish-Start)
            new Passing { Kart = new Kart(3), Lap = 4, PassingTime = new TimeSpan(hours:0, minutes:4, seconds: 40) },
            new Passing { Kart = new Kart(4), Lap = 4, PassingTime = new TimeSpan(hours:0, minutes:5, seconds: 10) },
            new Passing { Kart = new Kart(5), Lap = 4, PassingTime = new TimeSpan(hours:0, minutes:4, seconds: 57) },
        };

        var raceService = new RaceService();

        var winner = raceService.FindWinner(kartTimes);
        Assert.Equal(2, winner.Kart.KartNumber);
        Assert.Equal(new TimeSpan(0,4,9), winner.PassingTime);

        var fastest = raceService.FindFastestLap(kartTimes);
        Assert.Equal(1, fastest.fastestPassing.Kart.KartNumber);
        Assert.Equal(3, fastest.fastestPassing.Lap);
        Assert.Equal(new TimeSpan(0,0,41), fastest.fastestTime);
    }

    [Fact]
    public void Test2()
    {
        var kartTimes = new List<Passing>();

        var raceService = new RaceService();

        var winner = raceService.FindWinner(kartTimes);
        Assert.Equal(0, winner.Kart.KartNumber);
        Assert.Equal(TimeSpan.Zero, winner.PassingTime);

        var fastest = raceService.FindFastestLap(kartTimes);
        Assert.Equal(0, fastest.fastestPassing.Kart.KartNumber);
        Assert.Equal(0, fastest.fastestPassing.Lap);
        Assert.Equal(TimeSpan.Zero, fastest.fastestTime);
    }
}