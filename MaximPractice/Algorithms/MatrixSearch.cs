using MaximPractice.Data;
using MaximPractice.Interfaces;

namespace MaximPractice.Algorithms;

public class MatrixSearch : IDriverSearchAlgorithm
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
        var allDrivers = map.GetAllDriversByPosition();
        var maxCount = allDrivers.Values.Count();

        while (candidates.Count < count)
        {
            if (r == 0)
            {
                TryAddDriver(allDrivers, candidates, startPosition, 0, 0);
            }

            else
            {
                for (var x = -r; x <= r; x++)
                {
                    TryAddDriver(allDrivers, candidates, startPosition, x, -r);
                    TryAddDriver(allDrivers, candidates, startPosition, x, r);
                }

                for (var y = -r + 1; y <= r - 1; y++)
                {
                    TryAddDriver(allDrivers, candidates, startPosition, -r, y);
                    TryAddDriver(allDrivers, candidates, startPosition, r, y);
                }
            }

            if (candidates.Count == maxCount)
            {
                break;
            }

            r++;
        }

        return candidates
            .OrderBy(x => x.distance)
            .ThenBy(x => x.driver.Id)
            .Take(count)
            .Select(x => x.driver)
            .ToArray();
    }

    private void TryAddDriver(
        IReadOnlyDictionary<Point, Driver> allDrivers,
        List<(Driver driver, int distance)> candidates,
        Point startPosition,
        int dx,
        int dy)
    {
        if (allDrivers.TryGetValue(new Point(startPosition.X + dx, startPosition.Y + dy), out var driver))
        {
            var distance = dx * dx + dy * dy;
            candidates.Add((driver, distance));
        }
    }

    public override string ToString()
    {
        return "MatrixSearch";
    }
}
