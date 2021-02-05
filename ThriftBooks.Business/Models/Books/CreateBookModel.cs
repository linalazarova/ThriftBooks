namespace ThriftBooks.Business.Models.Books
{
    public class CreateBookModel : BaseModel
    {

        public string Title { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }

        //[Min(0)]
        public int Quantity { get; set; }
    }
}
