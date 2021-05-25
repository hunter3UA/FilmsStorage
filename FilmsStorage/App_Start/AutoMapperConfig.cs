using AutoMapper;
using FilmsStorage.Models.DB;
using FilmsStorage.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmsStorage.App_Start
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration Configure()
        {
            MapperConfiguration config = new MapperConfiguration(cfg => {

                cfg.CreateMap<FilmAddModel, Film>()
                    .ForMember(dest => dest.fk_GenreID, opt => opt.MapFrom(src => src.GenreID))
                    .ForMember(dest => dest.fk_UserID, opt => opt.MapFrom(src => src.UserID));

                cfg.CreateMap<RegisterModel, User>();

            });

            return config;
        }
    }
}