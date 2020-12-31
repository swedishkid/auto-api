using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AutoApi.Sample
{
    public class ItemValidationBuilder
    {
    }
    
    public static class ItemsFilters
    {
        public static IQueryable<Item> WhereTitleIs(this IQueryable<Item> queryable, string title)
        {
            return queryable.Where(x => x.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }
        
        public static IQueryable<Item> WhereTitleContains(this IQueryable<Item> queryable, string title)
        {
            return queryable.Where(x => x.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
        }
        
        public static IQueryable<Item> WhereTitleStartsWith(this IQueryable<Item> queryable, string title)
        {
            return queryable.Where(x => x.Title.StartsWith(title, StringComparison.OrdinalIgnoreCase));
        }
        
        public static IQueryable<Item> WhereTitleEndsWith(this IQueryable<Item> queryable, string title)
        {
            return queryable.Where(x => x.Title.EndsWith(title, StringComparison.OrdinalIgnoreCase));
        }
        
        public static IQueryable<Item> WhereIdIs(this IQueryable<Item> queryable, Guid id)
        {
            return queryable.Where(x => x.Id.Equals(id));
        }
        
        public static IQueryable<Item> FirstOrDefaultWhereTitleContains(this IQueryable<Item> queryable, Guid id)
        {
            return queryable.Where(x => x.Id.Equals(id));
        }
    }
}