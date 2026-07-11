using BloomAI.API.DTOs;

namespace BloomAI.API.Services;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
    Task LogoutAsync(Guid userId);
}
