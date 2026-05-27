namespace MaximPractice.Data;

public class Driver
{
    public int Id { get; }
    public Point Position { get; private set; }

    public Driver(int id, Point position)
    {
        Id = id;
        Position = position;
    }

    public void UpdatePosition(Point newPosition)
    {
        Position = newPosition;
    }
}
