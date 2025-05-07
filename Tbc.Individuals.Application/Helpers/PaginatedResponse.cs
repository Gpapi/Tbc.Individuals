namespace Tbc.Individuals.Application.Helpers;

public record PaginatedResponse<T>
{
    public int TotalCount { get; init; }
    public int PageSize { get; init; }
    public int PageNumber { get; init; }
    public IEnumerable<T> Data { get; init; } = [];
    public PaginatedResponse(int totalCount, int pageSize, int pageNumber, IEnumerable<T> data)
    {
        TotalCount = totalCount;
        PageSize = pageSize;
        PageNumber = pageNumber;
        Data = data;
    }
}
