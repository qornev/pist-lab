namespace webapp.Models;

public class Place
{
    public Guid Id { get; set; } = Guid.Empty;
    public string? Name { get; set; }
    public string? Category { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }

    public BaseModelValidationResult Validate()
    {
        var validationResult = new BaseModelValidationResult();

        if (string.IsNullOrWhiteSpace(Name)) validationResult.Append($"Name cannot be empty");
        if (string.IsNullOrWhiteSpace(Category)) validationResult.Append($"Category cannot be empty");
        if (!(Latitude >= -90 && Latitude <= 90)) validationResult.Append($"Latitude {Latitude} is out of range (-90..90)");
        if (!(Longitude >= -90 && Longitude <= 90)) validationResult.Append($"Longitude {Longitude} is out of range (-90..90)");
        
        return validationResult;
    }

    public override string ToString()
    {
        return $"{Category} {Name} at {Latitude} {Longitude}";
    }
}
