using DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShareFund.Helpers;
using ShareFund.Models.HomePage;

namespace ShareFund.Pages
{
    public class AboutUsModel : PageModel
    {
        private CoreService CS;
        public AboutCompanyModel AboutCompanyModel;
        public AboutUsModel( ApplicationDBContext db)
        {
            CS = new CoreService(db);
        }
        public void OnGet()
        {
            var getAboutUs = CS.GetAboutCompanyModel();
            if (!getAboutUs.Result)
            {
            }
            AboutCompanyModel = getAboutUs.Model;
        }
    }
}
