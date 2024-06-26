using KartRacing.Services;

var raceService = new RaceService();

char key;
do
{
    raceService.Start();
    Console.WriteLine("Do you want to start a new race (y/n)");
    key = Console.ReadKey().KeyChar;
    Console.WriteLine();
} while (key == 'y' || key == 'Y');

Console.WriteLine("Press any key to continue..");
Console.ReadKey();

