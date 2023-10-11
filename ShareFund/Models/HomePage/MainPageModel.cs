namespace ShareFund.Models.HomePage
{
    public class MainPageModel
    {
        public string PriefAbout { get; set; }
        public string Vission { get; set; }
        public string Message { get; set; }
        public string InstitutionalValues { get; set; }
        public List<MainPageSlider> Sliders { get; set;}=new List<MainPageSlider>();
        public List<OurServiceListModel> OurServices { get; set;}=new List<OurServiceListModel>();

        public string Phones { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string FaceBookLink { get; set; }
        public string TwitterLink { get; set; }

    }
}
