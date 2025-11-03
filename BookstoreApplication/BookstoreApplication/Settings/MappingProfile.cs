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
        }


    }
}
