namespace BloomAI.API.DTOs;

public class LocationDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public Guid CompanyId { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public double? RadiusMeters { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateLocationRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public Guid CompanyId { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public double? RadiusMeters { get; set; }
    public string? Notes { get; set; }
}

public class UpdateLocationRequest
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public double? RadiusMeters { get; set; }
    public string? Notes { get; set; }
}
