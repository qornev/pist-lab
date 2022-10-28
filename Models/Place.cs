namespace webapp;

public class Place
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }

    public string? Category { get; set; }

    public float Latitude { get; set; }

    public float Longitude { get; set; }
}
