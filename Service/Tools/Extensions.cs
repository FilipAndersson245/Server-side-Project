using AutoMapper;
using PagedList;
using System.Collections.Generic;

namespace Service.Tools
{
    public static class Extensions
    {
        //Allows mapping of a pagedlist from a database class to a model class
        public static IPagedList<TDestination> ToMappedPagedList<TSource, TDestination>(this IPagedList<TSource> list) //Source: https://stackoverflow.com/questions/2070850/can-automapper-map-a-paged-list/12463289
        {
            return new StaticPagedList<TDestination>(Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(list), list.GetMetaData());
        }
    }
}