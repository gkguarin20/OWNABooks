namespace OWNABooks.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }
    }
}