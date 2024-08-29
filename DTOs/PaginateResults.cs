using System.Collections.Generic;

namespace Enwage_API.DTOs
{
    public class PaginatedResult<T>
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; }
    }
}