using AutoMapper;
using BloomAI.API.DTOs;
using BloomAI.API.Models;

namespace BloomAI.API.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<CreateUserRequest, User>();
        CreateMap<UpdateUserRequest, User>();

        CreateMap<Project, ProjectDto>();
        CreateMap<CreateProjectRequest, Project>();
        CreateMap<UpdateProjectRequest, Project>();

        CreateMap<TaskItem, TaskDto>();
        CreateMap<CreateTaskRequest, TaskItem>();
        CreateMap<UpdateTaskRequest, TaskItem>();

        CreateMap<Location, LocationDto>();
        CreateMap<CreateLocationRequest, Location>();
        CreateMap<UpdateLocationRequest, Location>();
    }
}
