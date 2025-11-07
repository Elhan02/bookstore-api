using AutoMapper;
using BookstoreApplication.Models;
using BookstoreApplication.DTOs;

namespace BookstoreApplication.Settings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.YearsOld, opt => opt.MapFrom(src => DateTime.Now.Year - src.PublishedDate.Year));

            CreateMap<Book, BookDetailsDto>();

            CreateMap<RegistrationDto, ApplicationUser>();

            CreateMap<ApplicationUser, ProfileDto>();

            CreateMap<CreateIssueDto, Issue>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<ApiCreateIssueDto, Issue>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ApiId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image.SmallUrl));
        }


    }
}
