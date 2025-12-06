using AdventOfCode2025;
using AdventOfCode2025.Common;

bool useTestData = true;
List<Problem> problems = [
    new DayOne(useTestData), new DayOne(),
    new DayTwo(useTestData), new DayTwo(),
    new DayThree(useTestData), new DayThree(),
    new DayFour(useTestData), new DayFour(),
	//new DayFive(useTestData), new DayFive(),
	//new DaySix(useTestData), new DaySix(),
	//new DaySeven(useTestData), new DaySeven(),
	//new DayEight(useTestData), new DayEight(),
	//new DayNine(useTestData), new DayNine(),
	//new DayTen(useTestData), new DayTen(),
	//new DayEleven(useTestData), new DayEleven(),
	//new DayTwelve(useTestData), new DayTwelve(),
	];

foreach (var problem in problems)
{
    Console.WriteLine(problem);
}