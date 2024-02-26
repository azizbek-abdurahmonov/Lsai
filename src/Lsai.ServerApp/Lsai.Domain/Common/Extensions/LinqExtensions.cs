using Lsai.Domain.Common.Filters;

namespace Lsai.Domain.Common.Extensions;

public static class LinqExtensions
{
    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> source, PaginationOptions paginationOptions)
        => source.Skip((paginationOptions.PageToken - 1) * paginationOptions.PageSize).Take(paginationOptions.PageSize);

    public static IEnumerable<T> ApplyPagination<T>(this IEnumerable<T> source, PaginationOptions paginationOptions)
           => source.Skip((paginationOptions.PageToken - 1) * paginationOptions.PageSize).Take(paginationOptions.PageSize);
}