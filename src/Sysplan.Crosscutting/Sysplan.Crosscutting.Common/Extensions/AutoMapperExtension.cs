using AutoMapper;
using System;
using System.Linq.Expressions;

namespace Sysplan.Crosscutting.Common.Extensions
{
    public static class AutoMapperExtension
    {
        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> map,
            Expression<Func<TDestination, object>> selector)
        {
            map.ForMember(selector, config => config.Ignore());
            return map;
        }
    }
}