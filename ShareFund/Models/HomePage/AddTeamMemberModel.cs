namespace ShareFund.Models.HomePage
{
    public class AddTeamMemberModel
    {
        public string NameAR { get; set; }
        public string NameEN { get; set; }
        public string JobTitleAR { get; set; }
        public string JobTitleEN { get; set; }
        public string FacebookLink { get; set; }
        public string InstagramLink { get; set; }
        public string SkypeLink { get; set; }
        public string TwitterLink { get; set; }
        public IFormFile Image { get; set; }
    }
}
