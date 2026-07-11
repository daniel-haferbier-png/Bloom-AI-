using AutoMapper;
using BloomAI.API.Data;
using BloomAI.API.DTOs;
using BloomAI.API.Models;

namespace BloomAI.API.Services;

public class ProjectService : IProjectService
{
    private readonly BloomAIDbContext _context;
    private readonly IMapper _mapper;
    private readonly IAuditService _auditService;

    public ProjectService(BloomAIDbContext context, IMapper mapper, IAuditService auditService)
    {
        _context = context;
        _mapper = mapper;
        _auditService = auditService;
    }

    public async Task<ProjectDto> GetProjectByIdAsync(Guid id)
    {
        var project = await _context.Projects
            .Include(p => p.Company)
            .Include(p => p.Tasks)
            .FirstOrDefaultAsync(p => p.Id == id);
        return project == null ? throw new InvalidOperationException("Project not found") : _mapper.Map<ProjectDto>(project);
    }

    public async Task<ProjectDto> CreateProjectAsync(CreateProjectRequest request, Guid currentUserId)
    {
        var project = _mapper.Map<Project>(request);
        project.CreatedAt = DateTime.UtcNow;
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        await _auditService.LogAsync(currentUserId, "Create", "Project", project.Id);
        return _mapper.Map<ProjectDto>(project);
    }

    public async Task<ProjectDto> UpdateProjectAsync(Guid id, UpdateProjectRequest request, Guid currentUserId)
    {
        var project = await _context.Projects.FindAsync(id)
            ?? throw new InvalidOperationException("Project not found");

        if (request.Name != null) project.Name = request.Name;
        if (request.Description != null) project.Description = request.Description;
        if (request.Status != null) project.Status = request.Status;
        if (request.EndDate != null) project.EndDate = request.EndDate;
        if (request.Budget != null) project.Budget = request.Budget;
        project.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        await _auditService.LogAsync(currentUserId, "Update", "Project", project.Id);
        return _mapper.Map<ProjectDto>(project);
    }

    public async Task<List<ProjectDto>> GetCompanyProjectsAsync(Guid companyId)
    {
        var projects = await _context.Projects
            .Where(p => p.CompanyId == companyId)
            .ToListAsync();
        return _mapper.Map<List<ProjectDto>>(projects);
    }

    public async Task<List<ProjectDto>> GetUserProjectsAsync(Guid userId)
    {
        var projects = await _context.Projects
            .Where(p => p.Tasks.Any(t => t.AssignedToId == userId))
            .ToListAsync();
        return _mapper.Map<List<ProjectDto>>(projects);
    }
}
