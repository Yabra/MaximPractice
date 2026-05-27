using MaximPractice.Data;

namespace MaximPractice.Interfaces;

public interface IDriverSearchAlgorithm
{
    Driver[] FindNearestDrivers(
        Map map,
        Point startPosition,
        int count = 5);
}
