using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;

namespace Disty.Model.MySql
{
    public static class AutoMapperExtensions
    {
        public static async Task<List<TDestination>> ToListAsync<TDestination>(
            this IProjectionExpression projectionExpression)
        {
            return await projectionExpression.To<TDestination>().ToListAsync();
        }

        public static async Task<TDestination> ToFirstOrDefaultAsync<TDestination>(
            this IProjectionExpression projectionExpression)
        {
            return await projectionExpression.To<TDestination>().FirstOrDefaultAsync();
        }
    }
}