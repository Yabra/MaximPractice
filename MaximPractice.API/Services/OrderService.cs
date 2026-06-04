using MaximPractice.API.Dto;
using MaximPractice.Data;
using MaximPractice.Interfaces;

namespace MaximPractice.API.Services;

public class OrderService
{
    private readonly Map _map;
    private readonly RandomService _randomService;
    private readonly IDriverSearchAlgorithm _algorythm;

    public OrderService(Map map, RandomService randomService, IDriverSearchAlgorithm algorythm)
    {
        _map = map;
        _randomService = randomService;
        _algorythm = algorythm;
    }

    public async Task<DriverResponceDto> GetDriverForOrder(int orderId, int x, int y)
    {
        var orderPoint = new Point(x, y);

        if (!_map.IsInsideBounds(orderPoint))
        {
            throw new ArgumentException("Координаты некорректны");
        }

        var drivers = _map.GetAllDrivers();

        if (drivers.Length == 0)
        {
            throw new ArgumentException("Свободных водителей нет");
        }

        var nearestDrivers = _algorythm.FindNearestDrivers(_map, orderPoint, 5);
        var index = await _randomService.GetRandomNumberAsync(nearestDrivers.Length);
        var selectedDriver = nearestDrivers[index];
        var route = CreateRoute(selectedDriver.Position, orderPoint);

        return new DriverResponceDto
        {
            DriverId = selectedDriver.Id,
            DriverX = selectedDriver.Position.X,
            DriverY = selectedDriver.Position.Y,
            RouteLength = route.Count,
            Route = route
        };
    }

    private List<Point> CreateRoute(Point driverPos, Point orderPos)
    {
        var route = new List<Point>();

        var x = driverPos.X;
        var y = driverPos.Y;

        route.Add(new Point(x, y));

        while (x != orderPos.X)
        {
            x += Math.Sign(orderPos.X - x);
            route.Add(new Point(x, y));
        }

        while (y != orderPos.Y)
        {
            y += Math.Sign(orderPos.Y - y);
            route.Add(new Point(x, y));
        }

        return route;
    }
}
