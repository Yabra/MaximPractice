namespace MaximPractice.Data;

public class Map
{
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
    }

    public Driver? GetDriverById(int id)
        => _driversById.GetValueOrDefault(id);

    public Driver? GetDriverByPosition(Point position)
        => _driversByPosition.GetValueOrDefault(position);

    public bool ContainsDriver(int id)
        =>_driversById.ContainsKey(id);

    public bool IsPositionOccuped(Point position)
        => _driversByPosition.ContainsKey(position);

    public Driver[] GetAllDrivers()
        => _driversById.Values.ToArray();

    public IReadOnlyDictionary<Point, Driver> GetAllDriversByPosition()
        => _driversByPosition;

    public void AddDriver(int id, Point newDriverPosition)
    {
        if (!IsInsideBounds(newDriverPosition))
        {
            throw new ArgumentOutOfRangeException(nameof(newDriverPosition));
        }

        if (IsPositionOccuped(newDriverPosition))
        {
            throw new InvalidOperationException($"Position {newDriverPosition} is occuped");
        }

        if (_driversById.ContainsKey(id))
        {
            throw new InvalidOperationException($"Driver with id={id} already exists");
        }

        var newDriver = new Driver(id, newDriverPosition);

        _driversById[id] = newDriver;
        _driversByPosition[newDriverPosition] = newDriver;
    }

    public void UpdateDriver(int id, Point newDriverPosition)
    {
        if (!IsInsideBounds(newDriverPosition))
        {
            throw new ArgumentOutOfRangeException(nameof(newDriverPosition));
        }

        if (!_driversById.TryGetValue(id, out var driver))
        {
            throw new ArgumentException($"Driver with id={id} not found");
        }

        if (driver.Position == newDriverPosition)
        {
            return;
        }

        if (IsPositionOccuped(newDriverPosition))
        {
            throw new InvalidOperationException($"Position {newDriverPosition} is occuped");
        }

        _driversByPosition.Remove(driver.Position);
        driver.UpdatePosition(newDriverPosition);
        _driversByPosition[newDriverPosition] = driver;
    }

    public void RemoveDriver(int id)
    {
        if (!_driversById.TryGetValue(id, out var driver))
        {
            throw new ArgumentException($"Driver id={id} not found");
        }

        _driversById.Remove(id);
        _driversByPosition.Remove(driver.Position);
    }

    public bool IsInsideBounds(Point position)
    {
        return position.X >= 0
            && position.X < Width
            && position.Y >= 0
            && position.Y < Height;
    }
}
