using AutoMapper;
using BloomAI.API.Data;
using BloomAI.API.DTOs;
using BloomAI.API.Models;

namespace BloomAI.API.Services;

public class UserService : IUserService
{
    private readonly BloomAIDbContext _context;
    private readonly IMapper _mapper;
    private readonly IAuditService _auditService;

    public UserService(BloomAIDbContext context, IMapper mapper, IAuditService auditService)
    {
        _context = context;
        _mapper = mapper;
        _auditService = auditService;
    }

    public async Task<UserDto> GetUserByIdAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id) ?? throw new InvalidOperationException("User not found");
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> UpdateUserAsync(Guid id, UpdateUserRequest request)
    {
        var user = await _context.Users.FindAsync(id) ?? throw new InvalidOperationException("User not found");

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.LanguagePreference = request.LanguagePreference;
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        await _auditService.LogAsync(id, "Update", "User", user.Id);

        return _mapper.Map<UserDto>(user);
    }

    public async Task<List<UserDto>> GetCompanyEmployeesAsync(Guid companyId)
    {
        var employees = await _context.Users
            .Where(u => u.CompanyId == companyId && u.IsActive)
            .ToListAsync();
        return _mapper.Map<List<UserDto>>(employees);
    }

    public async Task<UserDto> CreateUserAsync(CreateUserRequest request)
    {
        if (await _context.Users.AnyAsync(u => u.Email == request.Email))
        {
            throw new InvalidOperationException("Email already exists");
        }

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = request.Role,
            CompanyId = request.CompanyId
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        await _auditService.LogAsync(user.Id, "Create", "User", user.Id);

        return _mapper.Map<UserDto>(user);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id) ?? throw new InvalidOperationException("User not found");
        user.IsActive = false;
        user.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        await _auditService.LogAsync(id, "Delete", "User", user.Id);
    }
}
