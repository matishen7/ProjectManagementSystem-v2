using AutoMapper;
using ProjectManagementSystem.Application.Contracts.Features.Project.Commands;
using ProjectManagementSystem.Application.Contracts.Features.Project.Queries;
using ProjectManagementSystem.Application.Contracts.Features.Task.Commands;
using ProjectManagementSystem.Application.Contracts.Features.Task.Queries;
using ProjectManagementSystem.Application.Contracts.Features.User.Commands;
using ProjectManagementSystem.Application.Contracts.Features.User.Queries;
using ProjectManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            //Users
            CreateMap<UserDto, UserEntity>().ReverseMap();
            CreateMap<UpdateUserCommand, UserEntity>().ReverseMap();
            CreateMap<CreateUserCommand, UserEntity>().ReverseMap();
            //Projects
            CreateMap<ProjectDto, Project>().ReverseMap();
            CreateMap<CreateProjectCommand, Project>().ReverseMap();
            CreateMap<UpdateProjectCommand, Project>().ReverseMap();

            //ProjectTasks
            CreateMap<ProjectTaskDto, ProjectTask>().ReverseMap();
            CreateMap<CreateProjectTaskCommand, ProjectTask>().ReverseMap();
            CreateMap<UpdateProjectTaskCommand, ProjectTask>().ReverseMap();
            CreateMap<ProjectTaskDto, ProjectTask>().ReverseMap();
        }
    }
}
