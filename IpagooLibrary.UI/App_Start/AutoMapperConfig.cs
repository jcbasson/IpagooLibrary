using AutoMapper;
using IpagooLibrary.Models.DTO;
using IpagooLibrary.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IpagooLibrary.UI.App_Start
{
    public class MappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<BookSearch, BookFilter>();
            CreateMap<BookDTO, BookViewModel>();
            CreateMap<LibraryDTO, LibraryViewModel>();
        }

        public IMapper GenerateMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            var mapper = mapperConfiguration.CreateMapper();

            return mapper;
        }
    }
}