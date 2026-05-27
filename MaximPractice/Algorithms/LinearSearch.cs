using MaximPractice.Data;
using MaximPractice.Interfaces;

namespace MaximPractice.Algorithms;

public class LinearSearch : IDriverSearchAlgorithm
{
    public Driver[] FindNearestDrivers(Map map, Point startPosition, int count = 5)
    {
        if (map == null)
        {
            throw new ArgumentNullException(nameof(map), "Can't be null");
        }

        if (count <= 0)
        {
            throw new ArgumentException("Count must be positive");
        }

        return map
            .GetAllDrivers()
            .OrderBy(driver => GetSquaredDistance(startPosition, driver.Position))
            .ThenBy(driver => driver.Id)
            .Take(count)
            .ToArray();
    }

    private int GetSquaredDistance(Point one, Point two)
    {
        var dx = one.X - two.X;
        var dy = one.Y - two.Y;
        return dx * dx + dy * dy;
    }

    public override string ToString()
    {
        return "LinearSearch";
    }
}
