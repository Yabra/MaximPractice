namespace MaximPractice.Data;

public class Map
{
    private int _nextDriverId;
    public int Width { get; }
    public int Height { get; }

    private readonly Dictionary<int, Driver> _driversById = new();
    private readonly Dictionary<Point, Driver> _driversByPosition = new();

    public Map(int width, int height)
    {
        if (width <= 0)
        {
            throw new ArgumentException("Width must be positive");
        }

        if (height <= 0)
        {
            throw new ArgumentException("Height must be positive");
        }

        Width = width;
        Height = height;
        _nextDriverId = 0;
    }

    public int AddDriver(Point newDriverPosition)
    {
        ValidateBounds(newDriverPosition);
        ValidatePositionAvailability(newDriverPosition);

        var newId = _nextDriverId;
        _nextDriverId++;

        var newDriver = new Driver(newId, newDriverPosition);

        _driversById[newId] = newDriver;
        _driversByPosition[newDriverPosition] = newDriver;

        return newId;
    }

    public void UpdateDriverPosition(int driverId, Point newDriverPosition)
    {
        if (!_driversById.TryGetValue(driverId, out var driver))
        {
            throw new ArgumentException($"Driver id={driverId} not found");
        }

        if (driver.Position == newDriverPosition)
        {
            return;
        }

        ValidateBounds(newDriverPosition);
        ValidatePositionAvailability(newDriverPosition);

        _driversByPosition.Remove(driver.Position);
        driver.UpdatePosition(newDriverPosition);
        _driversByPosition[newDriverPosition] = driver;
    }

    public Driver[] GetAllDrivers()
    {
        return _driversById.Values.ToArray();
    }

    public IReadOnlyDictionary<Point, Driver> GetAllDriversByPosition()
    {
        return _driversByPosition;
    }

    private void ValidateBounds(Point position)
    {
        if (position.X < 0 || position.X >= Width)
        {
            throw new ArgumentOutOfRangeException(
                nameof(position.X),
                position.X,
                $"X must be between 0 and {Width - 1}"
                );
        }

        if (position.Y < 0 || position.Y >= Height)
        {
            throw new ArgumentOutOfRangeException(
                nameof(position.Y),
                position.Y,
                $"Y must be between 0 and {Height - 1}"
                );
        }
    }

    private void ValidatePositionAvailability(Point position)
    {
        if (_driversByPosition.ContainsKey(position))
        {
            throw new ArgumentException($"Position ({position.X}, {position.Y}) is taken by other driver");
        }
    }
}
