using MaximPractice.Data;
using MaximPractice.Interfaces;

namespace MaximPractice.Algorithms;

public class RadiusSearch : IDriverSearchAlgorithm
{
    public Driver[] FindNearestDrivers(Map map, Point startPosition, int count = 5)
    {
        if (map == null)
        {
            throw new ArgumentNullException(nameof(map));
        }

        if (startPosition == null)
        {
            throw new ArgumentNullException(nameof(startPosition));
        }

        if (count <= 0)
        {
            throw new ArgumentException("Count must be positive");
        }

        var candidates = new List<(Driver driver, int distance)>();
        int r = 0;
        var allDrivers = map.GetAllDrivers();
        var addedDriversIds = new HashSet<int>();

        while (candidates.Count < count)
        {
            foreach (var driver in allDrivers)
            {
                var dx = driver.Position.X - startPosition.X;
                var dy = driver.Position.Y - startPosition.Y;

                if (Math.Abs(dx) <= r && Math.Abs(dy) <= r)
                {
                    if (addedDriversIds.Add(driver.Id))
                    {
                        var distance = dx * dx + dy * dy;
                        candidates.Add((driver, distance));
                    }
                }
            }

            r++;

            if (candidates.Count == allDrivers.Length)
            {
                break;
            }
        }

        return candidates
            .OrderBy(x => x.distance)
            .ThenBy(x => x.driver.Id)
            .Take(count)
            .Select(x => x.driver)
            .ToArray();
    }

    public override string ToString()
    {
        return "RadiusSearch";
    }
}
