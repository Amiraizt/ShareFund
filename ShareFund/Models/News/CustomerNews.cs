namespace ShareFund.Models.News
{
    public class CustomerNews
    {
        public string MainHeader { get; set; }
public List<CustomerCategoriesListModel> CategoriesListModels { get; set; }=new List<CustomerCategoriesListModel>();
        public List<NewsListModel> News { get; set; }
    }
}
