namespace ShareFund.Models.HomePage
{
    public class AdminMainPageModel
    {
        public string PriefAboutAr { get; set; }
        public string PriefAboutEn { get; set; }
        public string VissionAr { get; set; }
        public string VissionEn { get; set; }
        public string MessageAr { get; set; }
        public string MessageEn { get; set; }
        public string InstitutionalValuesAr { get; set; }
        public string InstitutionalValuesEn { get; set; }
        public List<AdminMainPageSlider> Sliders { get; set;}
        public List<AdminOurServiceListModel> OurServices { get; set;}

        public string Phones { get; set; }
        public string Email { get; set; }
        public string LocationAr { get; set; }
        public string LocationEn { get; set; }
        public string FaceBookLink { get; set; }
        public string TwitterLink { get; set; }

    }
}
