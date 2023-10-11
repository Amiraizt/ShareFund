using System.ComponentModel.DataAnnotations;

namespace ShareFund.Models.News
{
    public class CreateNewsModel
    {
        public IFormFile Image { get; set; }
        public string TitleAR { get; set; }
        public string TitleEN { get; set; }
        public string SummaryAR { get; set; }
        public string SummaryEN { get; set; }
        public IFormFile SecondImage { get; set; }
        public string ContentAR { get; set; }
        public string ContentEN { get; set; }
        public int Category { get; set; }
    }
}
