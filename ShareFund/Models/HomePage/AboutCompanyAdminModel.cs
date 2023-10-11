namespace ShareFund.Models.HomePage
{
    public class AboutCompanyAdminModel
    {
        public string AboutCompanyAR { get; set; }
        public string AboutCompanyEN { get; set; }
        public string CompetitiveAdvantageTitleAR { get; set; }
        public string CompetitiveAdvantageTitleEN { get; set; }
        public string Message { get; set; }
        public List<CompetitiveAdvantageAdminListModel> Advantages { get; set; } = new List<CompetitiveAdvantageAdminListModel>();
        public List<CounterAdminListModel> Counters { get; set; } = new List<CounterAdminListModel>();
        public List<OurClientsListModel> Clients { get; set; }=new List<OurClientsListModel>();
        public List<MemberAdminListModel> Members { get; set; } = new List<MemberAdminListModel>();

    }
}
