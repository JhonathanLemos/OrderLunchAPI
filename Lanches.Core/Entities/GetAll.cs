
namespace Lanches.Application.Dtos
{
    public class GetAll
    {
        public GetAll(string? search, int page, int pageSize)
        {
            Search = search;
            Page = page;
            PageSize = pageSize;
        }

        public GetAll()
        {
            
        }

        public string? Search { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
