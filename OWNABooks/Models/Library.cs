namespace OWNABooks.Models
{
    public class Library
    {
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
