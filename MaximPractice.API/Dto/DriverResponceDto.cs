using MaximPractice.Data;

namespace MaximPractice.API.Dto;

public class DriverResponceDto
{
    public int DriverId { get; set; }

    public int DriverX { get; set; }
    public int DriverY { get; set; }

    public int RouteLength { get; set; }
    public List<Point> Route { get; set; } = new();
}
