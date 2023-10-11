using DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShareFund.Helpers;
using ShareFund.Models.HomePage;

namespace ShareFund.Pages
{
    public class IndexModel : PageModel
    {
        public MainPageModel MainModel { get; set; }

        private readonly ILogger<IndexModel> _logger;
        private CoreService CS;
        public IndexModel(ILogger<IndexModel> logger,ApplicationDBContext db)
        {
            _logger = logger;
            CS=new CoreService(db);
        }

        public void OnGet()
        {
            MainModel = new MainPageModel();
            var getModel=CS.GetMainPageModel();
            if (!getModel.Result)
            {

            }
            MainModel = getModel.Model;
        }
    }
}