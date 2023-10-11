using DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShareFund.Helpers;

namespace ShareFund.Pages
{
    public class FAQModel : PageModel
    {
        public string Questions { get; set; }
        CoreService CS;
        public string PriefAbout { get; set; }

        public FAQModel(ApplicationDBContext db)
        {
            CS=new CoreService(db);
        }
        public void OnGet()
        {
            var getQuestions = CS.GetSingleValue(Consts.FAQSetting);
            Questions = getQuestions.Value;
            PriefAbout = CS.GetSingleValue(Consts.PriefAboutSetting).Value;
        }
    }
}
