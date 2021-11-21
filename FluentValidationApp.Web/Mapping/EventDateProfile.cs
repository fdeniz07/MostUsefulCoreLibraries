using AutoMapper;
using FluentValidationApp.Web.DTOs;
using FluentValidationApp.Web.Models;
using System;

namespace FluentValidationApp.Web.Mapping
{
    public class EventDateProfile : Profile
    {
        public EventDateProfile()
        {
            CreateMap<EventDateDto, EventDate>()
                .ForMember(x => x.Date, opt => opt.MapFrom(x => new DateTime(x.Year, x.Month, x.Day)));

            //Custom islemlerde ReverseMap() metodu calismaz. O yüzden ters islemler manuel olarak yazilmasi gerekir

            CreateMap<EventDate, EventDateDto>()
                .ForMember(x => x.Year, opt => opt.MapFrom(x => x.Date.Year))
                .ForMember(x => x.Month, opt => opt.MapFrom(x => x.Date.Month))
                .ForMember(x => x.Day, opt => opt.MapFrom(x => x.Date.Day));
        }
    }
}
