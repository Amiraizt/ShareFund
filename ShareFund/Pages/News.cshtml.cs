using DBContext;
using DBContext.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShareFund.Helpers;
using ShareFund.Models.News;

namespace ShareFund.Pages
{
    public class NewsModel : PageModel
    {
        CoreService CS;
        public List<NewsListModel> News;
        public List<CustomerCategoriesListModel> Categories;
        public NewsModel(ApplicationDBContext db)
        {
            CS = new CoreService(db);
        }
        public void OnGet(int categoryID = 0)
        {
            if (categoryID > 0)
            {
                var getNews = CS.GetNewsByCatrgory(categoryID);
                if (!getNews.Result)
                {

                }
                News = getNews.News;
            }
            else
            {
                var getNews = CS.GetNews();
                if (!getNews.Result)
                {

                }
                News = getNews.News;
            }
            var getCategories = CS.GetNewsCategoriesForCustomer();
            if (!getCategories.Result)
            {

            }
            Categories = getCategories.Categories;
        }

    }
}
