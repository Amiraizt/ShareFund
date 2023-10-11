namespace ShareFund.Models.HomePage
{
    public class FAQAndTeamModel
    {
        public string FAQAR { get; set; } = "";
        public string FAQEN { get; set; } = "";
        public List<MemberListModel> Members { get; set; }=new List<MemberListModel>();

    }
}
