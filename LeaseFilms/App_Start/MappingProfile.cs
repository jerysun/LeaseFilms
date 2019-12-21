using AutoMapper;
using LeaseFilms.Dtos;
using LeaseFilms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaseFilms.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();

            CreateMap<Movie, MovieDto>();
            CreateMap<MovieDto, Movie>();

            CreateMap<MembershipType, MembershipTypeDto>();
        }
    }
}