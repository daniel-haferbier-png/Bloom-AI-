using BloomAI.API.DTOs;

namespace BloomAI.API.Services;

public interface ILocationService
{
    Task<LocationDto> GetLocationByIdAsync(Guid id);
    Task<LocationDto> CreateLocationAsync(CreateLocationRequest request, Guid currentUserId);
    Task<LocationDto> UpdateLocationAsync(Guid id, UpdateLocationRequest request, Guid currentUserId);
    Task<List<LocationDto>> GetCompanyLocationsAsync(Guid companyId);
    Task DeleteLocationAsync(Guid id, Guid currentUserId);
}
