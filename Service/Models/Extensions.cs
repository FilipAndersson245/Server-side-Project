using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using AutoMapper;

namespace ServerSide_Project.Models
{
    public static class Extensions
    {

        public static IPagedList<TDestination> ToMappedPagedList<TSource, TDestination>(this IPagedList<TSource> list) //Source: https://stackoverflow.com/questions/2070850/can-automapper-map-a-paged-list/12463289
        {
            return new StaticPagedList<TDestination>(Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(list), list.GetMetaData());
        }

    }
}
