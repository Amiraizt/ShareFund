namespace ShareFund.Models.HomePage
{
    public class AboutCompanyModel
    {
        public string AboutCompany { get; set; }
        public string CompetitiveAdvantageTitle { get; set; }
        public string Message { get; set; }
        public List<CompetitiveAdvantageListModel> Advantages { get; set; } = new List<CompetitiveAdvantageListModel>();
        public List<CounterListModel> Counters { get; set; } = new List<CounterListModel>();
        public List<OurClientsListModel> Clients { get; set; }=new List<OurClientsListModel>();
        public List<MemberListModel> Members { get; set; } = new List<MemberListModel>();

    }
}
