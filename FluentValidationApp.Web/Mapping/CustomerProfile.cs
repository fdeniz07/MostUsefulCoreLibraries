using AutoMapper;
using FluentValidationApp.Web.DTOs;
using FluentValidationApp.Web.Models;

namespace FluentValidationApp.Web.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            //CreateMap<Customer, CustomerDto>().ReverseMap(); // Tam tersi olayini da ReverseMap sayesinde yapabiliyoruz.(CreateMap<CustomerDto, Customer>();)

            CreateMap<CreditCard, CustomerDto>(); // Eger IncludeMembers metoduna kullanmak istersek, bir dönüstürme islemi daha yapmamiz gerekli.

            CreateMap<Customer, CustomerDto>().IncludeMembers(x=>x.CreditCard)
                .ForMember(dest => dest.FullName2, opt => opt.MapFrom(x => x.FullName2()));
            //.ForMember(dest => dest.CCNumber, opt => opt.MapFrom(x => x.CreditCard.Number))
            //.ForMember(dest => dest.CCValidDate, opt => opt.MapFrom(x => x.CreditCard.ValidDate));


            //CreateMap<Customer, CustomerDto>()
            //    .ForMember(dest => dest.Isim, opt => opt.MapFrom(x => x.Name))
            //    .ForMember(dest => dest.Eposta, opt => opt.MapFrom(x => x.Email))
            //    .ForMember(dest => dest.Yas, opt => opt.MapFrom(x => x.Age))
            //    ;
        }
    }
}
