namespace Test.Controllers
{
    public class Article
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public string timeStamp { get; set; }

        public List<Comment> comments { get; set; }
        
    }
}
