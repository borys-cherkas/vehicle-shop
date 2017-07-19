using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VehicleShop.DataLayer.Entities;

namespace VehicleShop.DataLayer.Extensions
{
    internal static class RepositoryExtensions
    {
        public static IQueryable<T> ProcessQuery<T>(this IQueryable<T> queryable,
            Func<IQueryable<T>, IQueryable<T>> queryFunc)
        {
            if (queryFunc == null)
            {
                return queryable;
            }

            return queryFunc(queryable);
        }
    }
}