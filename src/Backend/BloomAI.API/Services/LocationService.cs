using AutoMapper;
using BloomAI.API.Data;
using BloomAI.API.DTOs;
using BloomAI.API.Models;

namespace BloomAI.API.Services;

public class LocationService : ILocationService
{
    private readonly BloomAIDbContext _context;
    private readonly IMapper _mapper;
    private readonly IAuditService _auditService;

    public LocationService(BloomAIDbContext context, IMapper mapper, IAuditService auditService)
    {
        _context = context;
        _mapper = mapper;
        _auditService = auditService;
    }

    public async Task<LocationDto> GetLocationByIdAsync(Guid id)
    {
        var location = await _context.Locations.FindAsync(id);
        return location == null ? throw new InvalidOperationException("Location not found") : _mapper.Map<LocationDto>(location);
    }

    public async Task<LocationDto> CreateLocationAsync(CreateLocationRequest request, Guid currentUserId)
    {
        var location = _mapper.Map<Location>(request);
        location.CreatedAt = DateTime.UtcNow;
        _context.Locations.Add(location);
        await _context.SaveChangesAsync();
        await _auditService.LogAsync(currentUserId, "Create", "Location", location.Id);
        return _mapper.Map<LocationDto>(location);
    }

    public async Task<LocationDto> UpdateLocationAsync(Guid id, UpdateLocationRequest request, Guid currentUserId)
    {
        var location = await _context.Locations.FindAsync(id)
            ?? throw new InvalidOperationException("Location not found");

        if (request.Name != null) location.Name = request.Name;
        if (request.Address != null) location.Address = request.Address;
        if (request.Latitude != null) location.Latitude = request.Latitude;
        if (request.Longitude != null) location.Longitude = request.Longitude;
        if (request.RadiusMeters != null) location.RadiusMeters = request.RadiusMeters;
        if (request.Notes != null) location.Notes = request.Notes;
        location.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        await _auditService.LogAsync(currentUserId, "Update", "Location", location.Id);
        return _mapper.Map<LocationDto>(location);
    }

    public async Task<List<LocationDto>> GetCompanyLocationsAsync(Guid companyId)
    {
        var locations = await _context.Locations
            .Where(l => l.CompanyId == companyId && l.IsActive)
            .ToListAsync();
        return _mapper.Map<List<LocationDto>>(locations);
    }

    public async Task DeleteLocationAsync(Guid id, Guid currentUserId)
    {
        var location = await _context.Locations.FindAsync(id)
            ?? throw new InvalidOperationException("Location not found");
        location.IsActive = false;
        location.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        await _auditService.LogAsync(currentUserId, "Delete", "Location", location.Id);
    }
}
