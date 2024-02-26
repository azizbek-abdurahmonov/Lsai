namespace Lsai.Domain.Common.Filters;

public class PaginationOptions
{
    public int PageToken { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}
