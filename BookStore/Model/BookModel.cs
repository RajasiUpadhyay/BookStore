using Newtonsoft.Json;


namespace BookStore.Model
{
    public class BookModel
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string author { get; set; }
        public string coverimage { get; set; }
        public float price { get; set; }
    }
}
