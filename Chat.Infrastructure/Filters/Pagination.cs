namespace Chat.Infrastructure.Filters;

public class Pagination
{
    public Pagination() { }

    public Pagination(int pageNumber, int pageSize)
    {
        if (pageNumber <= 0 || pageSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageNumber));

        Skip = (pageNumber - 1) * pageSize;
        Take = pageSize;
    }

    public int Skip { get; set; }
    public int Take { get; set; }
}
