using AutoMapper;
using Repository;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Service.Configuration
{
    public static class AutoMapperConfig
    {
        public static void Configure() => Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new ToAuthorProfile());
                cfg.AddProfile(new ToBookProfile());
                cfg.AddProfile(new ToClassificationProfile());
                cfg.AddProfile(new ToAdminProfile());

                cfg.AddProfile(new FromAuthorProfile());
                cfg.AddProfile(new FromBookProfile());
                cfg.AddProfile(new FromClassificationProfile());
                cfg.AddProfile(new FromAdminProfile());
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

    public class ToClassificationProfile : Profile
    {
        public ToClassificationProfile()
        {
            CreateMap<CLASSIFICATION, Classification>();
        }
    }

    public class FromClassificationProfile : Profile
    {
        public FromClassificationProfile()
        {
            CreateMap<Classification, CLASSIFICATION>();
        }
    }

    public class ToAdminProfile : Profile
    {
        public ToAdminProfile()
        {
            CreateMap<ADMIN, Admin>();
        }
    }

    public class FromAdminProfile : Profile
    {
        public FromAdminProfile()
        {
            CreateMap<Admin, ADMIN>();
        }
    }
}
