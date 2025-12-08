using AdventOfCode2025;
using AdventOfCode2025.Common;

bool useTestData = true;
List<Problem> problems = [
    new DayOne(useTestData), new DayOne(),        /* 0:01 */
    new DayTwo(useTestData), new DayTwo(),        /* 0:03 */
    new DayThree(useTestData), new DayThree(),    /* 0:01 */
    new DayFour(useTestData), new DayFour(),      /* 0:08 (with Dict), originally 10+min (Lists and multiple foreaches. I didn't wait to see final time), then 3:57 (Lists and a single for loop)*/
	new DayFive(useTestData), new DayFive(),      /* 0:01 */
	new DaySix(useTestData), new DaySix(),        /* 0:01 */
	new DaySeven(useTestData), new DaySeven(),    /* 0:01 */
	new DayEight(useTestData), new DayEight(),    /* 0:00 */
	//new DayNine(useTestData), new DayNine(),      /* 0:00 */
	//new DayTen(useTestData), new DayTen(),        /* 0:00 */
	//new DayEleven(useTestData), new DayEleven(),  /* 0:00 */
	//new DayTwelve(useTestData), new DayTwelve(),  /* 0:00 */
	];

foreach (var problem in problems)
{
    Console.WriteLine(problem);
}
Console.WriteLine("Press any key to close...");
Console.Read();
