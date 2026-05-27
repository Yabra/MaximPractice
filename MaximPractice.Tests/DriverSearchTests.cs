using MaximPractice.Algorithms;
using MaximPractice.Data;
using MaximPractice.Interfaces;

namespace MaximPractice.Tests;

public class DriverSearchTests
{
    public static IEnumerable<IDriverSearchAlgorithm> Algorithms => AlgorithmsRepo.Algorithms;
    private Map _smallMap;

    [SetUp]
    public void Setup()
    {
        SetupSmallMap();
    }

    private void SetupSmallMap()
    {
        _smallMap = new Map(100, 100);
        _smallMap.AddDriver(new Point(10, 10)); // id=0
        _smallMap.AddDriver(new Point(20, 20)); // id=1
        _smallMap.AddDriver(new Point(30, 30)); // id=2
        _smallMap.AddDriver(new Point(40, 40)); // id=3
        _smallMap.AddDriver(new Point(50, 50)); // id=4
        _smallMap.AddDriver(new Point(60, 60)); // id=5
        _smallMap.AddDriver(new Point(65, 65)); // id=6
    }

    [TestCaseSource(nameof(Algorithms))]
    public void SmallMapTest(IDriverSearchAlgorithm a)
    {
        var startPoint = new Point(0, 0);
        var expected = new int[] { 0, 1, 2, 3, 4 };

        var result = a
            .FindNearestDrivers(_smallMap, startPoint)
            .Select(x => x.Id)
            .ToArray();

        Assert.That(result, Is.EqualTo(expected));
    }


    [TestCaseSource(nameof(Algorithms))]
    public void NotEnoughDriversTest(IDriverSearchAlgorithm a)
    {
        var startPoint = new Point(0, 0);
        var expected = new int[] { 0, 1, 2, 3, 4, 5, 6 };

        var result = a
            .FindNearestDrivers(_smallMap, startPoint, 10)
            .Select(x => x.Id)
            .ToArray();

        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCaseSource(nameof(Algorithms))]
    public void UncorrectCountTest(IDriverSearchAlgorithm a)
    {
        var startPoint = new Point(0, 0);
        Assert.Throws<ArgumentException>(
            () => a.FindNearestDrivers(_smallMap, startPoint, 0)
            );

        Assert.Throws<ArgumentException>(
            () => a.FindNearestDrivers(_smallMap, startPoint, -1)
            );
    }
}
