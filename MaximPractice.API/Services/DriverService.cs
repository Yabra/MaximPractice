using MaximPractice.Data;

namespace MaximPractice.API.Services;

public class DriverService
{
    private readonly ILogger<DriverService> _logger;
    private readonly Map _map;

    public DriverService(Map map, ILogger<DriverService> logger)
    {
        _map = map;
        _logger = logger;
    }

    public string AddOrUpdateDriver(int id, int x, int y)
    {
        var point = new Point(x, y);

        bool exists = _map.ContainsDriver(id);

        if (!_map.IsInsideBounds(point))
        {
            if (exists)
            {
                _map.RemoveDriver(id);
            }

            _logger.LogWarning($"При создании/перемещении водителя id={id} даны некорректные координаты x={x}, y={y}");
            throw new ArgumentException("Координаты некорректны");
        }

        if (_map.IsPositionOccuped(point))
        {
            _logger.LogWarning($"Попытка создании/перемещения водителя id={id} на занятые координаты x={x}, y={y}");
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
