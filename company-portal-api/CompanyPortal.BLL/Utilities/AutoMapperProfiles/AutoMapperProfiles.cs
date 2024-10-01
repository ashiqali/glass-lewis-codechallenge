using CompanyPortal.DAL.Entities;
using AutoMapper;
using CompanyPortal.DTO.DTOs.Company;
using CompanyPortal.DTO.DTOs.User;

namespace CompanyPortal.BLL.Utilities.AutoMapperProfiles;

public static class AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserToAddDTO>().ReverseMap();
            CreateMap<User, UserToUpdateDTO>().ReverseMap();
            CreateMap<User, UserToRegisterDTO>().ReverseMap();
            CreateMap<User, UserToReturnDTO>().ReverseMap();
            CreateMap<Company, CompanyDTO>().ReverseMap();
        }
    }
}
