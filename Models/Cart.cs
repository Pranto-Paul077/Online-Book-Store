namespace OnlineBookStore.Models
{
    public class Cart
    {
        public int CartId { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }

        public int Quantity { get; set; }

        public Book Book { get; set; }
    }
}