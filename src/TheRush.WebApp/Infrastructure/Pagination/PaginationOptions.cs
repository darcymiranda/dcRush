namespace TheRush.WebApp.Infrastructure.Pagination
{
    public class PaginationOptions
    {
        public const int DefaultPage = 1;
        public const int DefaultPageSize = 10;
        
        public int Page { get; }
        public int PageSize { get;  }

        public PaginationOptions(int page = DefaultPage, int pageSize = DefaultPageSize)
        {
            // TODO: Validate bounds
            Page = page;
            PageSize = pageSize;
        }
    }
}