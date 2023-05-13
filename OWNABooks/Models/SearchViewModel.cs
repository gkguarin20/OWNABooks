namespace OWNABooks.Models
{
    public class SearchViewModel
    {
        public string Query { get; set; }
        public object Books { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}