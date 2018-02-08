using AutoMapper;
using Repository;
using ServerSide_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Configuration
{
    public static class AutoMapperConfig
    {
        public static void Configure() => Mapper.Initialize(cfg =>
            {
                //AddProfile
                cfg.AddProfile(new ToAuthorProfile());
                cfg.AddProfile(new ToBookProfile());
                cfg.AddProfile(new ToGenreProfile());

                cfg.AddProfile(new FromAuthorProfile());
                cfg.AddProfile(new FromBookProfile());
                cfg.AddProfile(new FromGenreProfile());
            });
    }


    public class ToBookProfile : Profile
    {
        public ToBookProfile()
        {
            CreateMap<BOOK, Book>().ForMember(m => m.Authors, opt => opt.Ignore());
        }
    }

    public class FromBookProfile : Profile
    {
        public FromBookProfile()
        {
            CreateMap<Book, BOOK>();
        }
    }

    public class ToAuthorProfile : Profile
    {
        public ToAuthorProfile()
        {
            CreateMap<AUTHOR, Author>();
        }
    }

    public class FromAuthorProfile : Profile
    {
        public FromAuthorProfile()
        {
            CreateMap<Author, AUTHOR>();
        }
    }

    public class ToGenreProfile : Profile
    {
        public ToGenreProfile()
        {
            CreateMap<CLASSIFICATION, Genre>();
        }
    }

    public class FromGenreProfile : Profile
    {
        public FromGenreProfile()
        {
            CreateMap<Genre, CLASSIFICATION>();
        }
    }
}
