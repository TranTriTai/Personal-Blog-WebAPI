using System;
using AutoMapper;
using DataModel.Infrastructure.Models;
using TranTriTaiBlog.DTOs.Requests;
using TranTriTaiBlog.DTOs.Responses;

namespace TranTriTaiBlog.Infrastructures.AutoMappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDetailResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.Languages, opt => opt.MapFrom(src => src.Languages))
                .ForMember(dest => dest.YearExperience, opt => opt.MapFrom(src => src.YearExperience))
                .ForMember(dest => dest.Hobbies, opt => opt.MapFrom(src => src.Hobbies))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Password, opt => opt.Ignore());
            CreateMap<Skill, CreateSkillResponse>()
                .ForMember(dest => dest.SkillId, opt => opt.MapFrom(src => src.Id));
            CreateMap<CreateSkillRequest, Skill>().ReverseMap();
            CreateMap<Skill, SkillsResponse>()
                .ForMember(dest => dest.SkillId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Domain, opt => opt.MapFrom(src => src.Domain));
            CreateMap<UserSkill, UserSkillsResponse>()
                .ForMember(dest => dest.SkillId, opt => opt.MapFrom(src => src.SkillId));
            CreateMap<UpdateSkillRequest, Skill>().ReverseMap();
            CreateMap<Skill, UpdateSkillResponse>()
                .ForMember(dest => dest.SkillId, opt => opt.MapFrom(src => src.Id));
            CreateMap<CreateUserSkillRequest, UserSkill>()
                .ForMember(dest => dest.SkillId, opt => opt.MapFrom(src => src.SkillId))
                .ForMember(dest => dest.SkillLevel, opt => opt.MapFrom(src => src.Level))
                .ReverseMap();
            CreateMap<UpdateUserDetailRequest, User>();
            CreateMap<User, UpdateUserDetailResponse>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
            CreateMap<UserRegistrationRequest, User>();
            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
            CreateMap<CreateCategoryRequest, Category>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Category))
                .ReverseMap();
            CreateMap<Category, CreateCategoryResponse>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id));
            CreateMap<Category, CategoryResponse>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Name));
            CreateMap<UpdateCategoryRequest, Category>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Category)).ReverseMap();
            CreateMap<Category, UpdateCategoryResponse>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id));
            CreateMap<AddPostRequest, Post>()
                .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Tags)).ReverseMap();
            CreateMap<Post, AddPostResponse>()
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.Id));
            CreateMap<UpdatePostRequest, Post>().ReverseMap();
            CreateMap<Post, UpdatePostResponse>()
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.Id));
            CreateMap<Post, PostResponse>()
                .ForMember(x => x.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(x => x.Tag, opt => opt.MapFrom(src => src.Tag))
                .ForMember(x => x.DefaultImageUrl, opt => opt.MapFrom(src => src.DefaultImageUrl))
                .ForMember(x => x.ReadingDuration, opt => opt.MapFrom(src => src.ReadingDuration))
                .ForMember(x => x.OwnerId, opt => opt.MapFrom(src => src.OwnerId))
                .ForMember(x => x.OwnerName, opt => opt.MapFrom(src => src.Owner.Name))
                .ForMember(x => x.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<Post[], ListPostResponse>()
                .ForMember(x => x.Posts, opt => opt.MapFrom(src => src));
        }
    }
}

