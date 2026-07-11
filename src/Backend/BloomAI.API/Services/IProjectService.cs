using BloomAI.API.DTOs;

namespace BloomAI.API.Services;

public interface IProjectService
{
    Task<ProjectDto> GetProjectByIdAsync(Guid id);
    Task<ProjectDto> CreateProjectAsync(CreateProjectRequest request, Guid currentUserId);
    Task<ProjectDto> UpdateProjectAsync(Guid id, UpdateProjectRequest request, Guid currentUserId);
    Task<List<ProjectDto>> GetCompanyProjectsAsync(Guid companyId);
    Task<List<ProjectDto>> GetUserProjectsAsync(Guid userId);
}
