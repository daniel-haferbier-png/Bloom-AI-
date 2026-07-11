using BloomAI.API.DTOs;

namespace BloomAI.API.Services;

public interface IUserService
{
    Task<UserDto> GetUserByIdAsync(Guid id);
    Task<UserDto> UpdateUserAsync(Guid id, UpdateUserRequest request);
    Task<List<UserDto>> GetCompanyEmployeesAsync(Guid companyId);
    Task<UserDto> CreateUserAsync(CreateUserRequest request);
    Task DeleteUserAsync(Guid id);
}
