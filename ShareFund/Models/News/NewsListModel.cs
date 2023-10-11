namespace ShareFund.Models.News
{
    public class NewsListModel
    {
        public int ID { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public DateTime DateTime { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }
    }
}
