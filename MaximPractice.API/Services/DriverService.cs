using MaximPractice.Data;

namespace MaximPractice.API.Services;

public class DriverService
{
    private readonly Map _map;

    public DriverService(Map map)
    {
        _map = map;
    }

    public string AddOrUpdateDriver(int id, int x, int y)
    {
        var point = new Point(x, y);

        bool exists = _map.ContainsDriver(id);

        if (x < 0
            || x >= _map.Width
            || y < 0
            || y >= _map.Height
            )
        {
            if (exists)
            {
                _map.RemoveDriver(id);
            }

            throw new ArgumentException("Координаты некорректны");
        }

        if (_map.IsPositionOccuped(point))
        {
            throw new ArgumentException("Здесь уже находится другой водитель");
        }

        if (exists)
        {
            _map.UpdateDriver(id, point);
            return "Координаты успешно изменены";
        }

        _map.AddDriver(id, point);
        return "Координаты успешно добавлены";
    }
}
