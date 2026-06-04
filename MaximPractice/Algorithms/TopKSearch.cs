using MaximPractice.Data;
using MaximPractice.Interfaces;

namespace MaximPractice.Algorithms;

public class TopKSearch : IDriverSearchAlgorithm
{
    public Driver[] FindNearestDrivers(Map map, Point startPosition, int count = 5)
    {
        if (count <= 0)
        {
            throw new ArgumentException("Count must be positive");
        }

        var topDistances = new int[count];
        var topDrivers = new Driver?[count];

        for (int i = 0; i < count; i++)
        {
            topDistances[i] = int.MaxValue;
        }

        foreach (var driver in map.GetAllDrivers())
        {
            var distance = GetSquaredDistance(startPosition, driver.Position);

            for (int i = 0; i < count; i++)
            {
                if (distance < topDistances[i])
                {
                    for (int j = count - 1; j > i; j--)
                    {
                        topDistances[j] = topDistances[j - 1];
                        topDrivers[j] = topDrivers[j - 1];
                    }

                    topDistances[i] = distance;
                    topDrivers[i] = driver;

                    break;
                }
            }
        }

        return topDrivers
            .Where(v => v != null)
            .Cast<Driver>()
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
        return "TopKSearch";
    }
}
