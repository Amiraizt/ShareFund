using DBContext;
using Microsoft.EntityFrameworkCore;
using ShareFund.Models.HomePage;
using ShareFund.Models.News;
using System.Globalization;
using System.Reflection;

namespace ShareFund.Helpers
{
    public class CoreService
    {
        ApplicationDBContext _db;
        ExceptionHandler EXH;
        public CoreService(ApplicationDBContext db)
        {
            _db = db;
            EXH = new ExceptionHandler(db);
        }
        public (bool Result, MainPageModel Model) GetMainPageModel()
        {
            try
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

                var sliderImages = _db.SingleValueTests.Where(s => s.Name == Consts.MainPageSliderSetting)
                    .Select(s => new MainPageSlider()
                    {
                        ID = s.ID,
                        Title = currentCulture.TwoLetterISOLanguageName == "ar" ? s.ValueAR : s.ValueEN,
                        Image = s.Image
                    }).ToList();
                if (sliderImages == null)
                {
                    sliderImages = new List<MainPageSlider>();
                }
                var model = new MainPageModel()
                {
                    Sliders = sliderImages
                };

                var priefAboutShareFund = GetSingleValue(Consts.PriefAboutSetting).Value;
                model.PriefAbout = priefAboutShareFund;
                model.Message = GetSingleValue(Consts.MessageSetting).Value;
                model.Vission = GetSingleValue(Consts.VissionSetting).Value;
                model.InstitutionalValues = GetSingleValue(Consts.InvusterialValuesSetting).Value;
                model.FaceBookLink = GetSettingValue(Consts.FaceBookLink);
                model.TwitterLink = GetSettingValue(Consts.TwitterLink);

                var ourServices = _db.OurServices
                    .Select(s => new OurServiceListModel()
                    {
                        Icon = s.Icon,
                        Title = currentCulture.TwoLetterISOLanguageName == "ar" ? s.TitleAr : s.TitleEn,
                        Body = currentCulture.TwoLetterISOLanguageName == "ar" ? s.BodyAr : s.BodyEn,
                        ID = s.ID,
                    }).ToList();
                model.OurServices = ourServices;

                model.Phones = GetSettingValue(Consts.PhonesSetting);
                model.Email = GetSettingValue(Consts.EmailSetting);
                model.Location = GetSingleValue(Consts.LocationSetting).Value;
                return (true, model);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }

        public (bool Result, AdminMainPageModel Model) GetAdminMainPageModel()
        {
            try
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

                var sliderImages = _db.SingleValueTests.Where(s => s.Name == Consts.MainPageSliderSetting)
                    .Select(s => new AdminMainPageSlider()
                    {
                        ID = s.ID,
                        TitleAR = s.ValueAR,
                        TitleEN = s.ValueEN,
                        Image = s.Image
                    }).ToList();
                if (sliderImages == null)
                {
                    sliderImages = new List<AdminMainPageSlider>();
                }
                var model = new AdminMainPageModel()
                {
                    Sliders = sliderImages
                };

                //var priefAboutShareFund = GetSingleValueAR(Consts.PriefAboutSetting).Value;
                model.PriefAboutAr = GetSingleValueAR(Consts.PriefAboutSetting).Value;
                model.PriefAboutEn = GetSingleValueEN(Consts.PriefAboutSetting).Value;
                model.MessageAr = GetSingleValueAR(Consts.MessageSetting).Value;
                model.MessageEn = GetSingleValueEN(Consts.MessageSetting).Value;
                model.VissionAr = GetSingleValueAR(Consts.VissionSetting).Value;
                model.VissionEn = GetSingleValueEN(Consts.VissionSetting).Value;
                model.InstitutionalValuesAr = GetSingleValueAR(Consts.InvusterialValuesSetting).Value;
                model.InstitutionalValuesEn = GetSingleValueEN(Consts.InvusterialValuesSetting).Value;
                model.TwitterLink = GetSettingValue(Consts.TwitterLink);
                model.FaceBookLink = GetSettingValue(Consts.FaceBookLink);

                var ourServices = _db.OurServices
                    .Select(s => new AdminOurServiceListModel()
                    {
                        Icon = s.Icon,
                        TitleAr = s.TitleAr,
                        TitleEn = s.TitleEn,
                        BodyAr = s.BodyAr,
                        BodyEn = s.BodyEn,
                        ID = s.ID,
                    }).ToList();
                model.OurServices = ourServices;

                model.Phones = GetSettingValue(Consts.PhonesSetting);
                model.Email = GetSettingValue(Consts.EmailSetting);
                model.LocationAr = GetSingleValueAR(Consts.LocationSetting).Value;
                model.LocationEn = GetSingleValueEN(Consts.LocationSetting).Value;
                return (true, model);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }
        public bool SaveTextSetting(SaveTextSetting model)
        {
            try
            {
                var setting = _db.SingleValueTests.FirstOrDefault(s => s.Name == model.Name);
                if (setting != null)
                {
                    setting.ValueAR = model.ValueAR;
                    setting.ValueEN = model.ValueEN;
                    _db.SingleValueTests.Update(setting);
                }
                else
                {
                    setting = new SingleValueTest()
                    {
                        Name = model.Name,
                        ValueAR = model.ValueAR,
                        ValueEN = model.ValueEN,
                    };
                    _db.SingleValueTests.Add(setting);
                }

                _db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }


        public bool SaveSetting(SaveSetting model)
        {
            try
            {
                var setting = _db.Settings.FirstOrDefault(s => s.Name == model.SettingName);
                if (setting != null)
                {
                    setting.Value = model.Value;

                    _db.Settings.Update(setting);
                    _db.SaveChanges();
                    return true;
                }

                else
                    return false;


            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }

        public string GetSettingValue(string name)
        {
            try
            {
                var setting = _db.Settings.FirstOrDefault(s => s.Name == name);
                return setting.Value;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return "";
            }
        }
        public (string Value, string Image) GetSingleValue(string name)
        {
            try
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentUICulture;

                var value = _db.SingleValueTests.FirstOrDefault(s => s.Name == name);

                if (currentCulture.TwoLetterISOLanguageName == "ar")
                {
                    return (value.ValueAR, value.Image);
                }
                return (value.ValueEN, value.Image);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return ("", "");
            }
        }

        public (string Value, string Image) GetSingleValueByLanguage(string name, string language)
        {
            try
            {


                var value = _db.SingleValueTests.FirstOrDefault(s => s.Name == name);

                if (language == "ar")
                {
                    return (value.ValueAR, value.Image);
                }
                return (value.ValueEN, value.Image);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return ("", "");
            }
        }
        public (string Value, string Image) GetSingleValueEN(string name)
        {
            try
            {
                var value = _db.SingleValueTests.FirstOrDefault(s => s.Name == name);
                return (value.ValueEN, value.Image);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return ("", "");
            }
        }
        public (string Value, string Image) GetSingleValueAR(string name)
        {
            try
            {

                var value = _db.SingleValueTests.FirstOrDefault(s => s.Name == name);


                return (value.ValueAR, value.Image);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return ("", "");
            }
        }
        public (string Value1, string Value2, string Image) GetMultipleValue(string name)
        {
            try
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

                var value = _db.MultipleValueTexts.FirstOrDefault(s => s.Name == name);

                if (currentCulture.TwoLetterISOLanguageName == "ar")
                {
                    return (value.ValueAR, value.SecondValue, value.Image);
                }
                return (value.ValueEN, value.SecondValue, value.Image);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return ("", "", "");
            }
        }


        public (string Value1, string Value2, string Image) GetMultipleValueAr(string name)
        {
            try
            {

                var value = _db.MultipleValueTexts.FirstOrDefault(s => s.Name == name);


                return (value.ValueAR, value.SecondValue, value.Image);

            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return ("", "", "");
            }
        }

        public (string Value1, string Value2, string Image) GetMultipleValueEn(string name)
        {
            try
            {

                var value = _db.MultipleValueTexts.FirstOrDefault(s => s.Name == name);


                return (value.ValueEN, value.SecondValue, value.Image);

            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return ("", "", "");
            }
        }
        //public bool AddSliderImage(string tilte, string image)
        //{
        //    try
        //    {
        //        var setting = new Setting()
        //        {
        //            SecondValue = image,
        //            Value = tilte,
        //            Name = Consts.MainPageSliderSetting
        //        };
        //        _db.Settings.Add(setting);
        //        _db.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
        //        return false;
        //    }
        //}

        public bool DeleteSettingByID(int id)
        {
            try
            {
                var setting = _db.Settings.Find(id);
                _db.Settings.Remove(setting);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }


        public bool DeleteSingleTextByID(int id)
        {
            try
            {
                var setting = _db.SingleValueTests.Find(id);
                _db.SingleValueTests.Remove(setting);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        public bool DeleteMultipleTextByID(int id)
        {
            try
            {
                var setting = _db.MultipleValueTexts.Find(id);
                _db.MultipleValueTexts.Remove(setting);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }

        public bool AddAdvantage(AddServiceAdminModel model, string icon)
        {
            try
            {
                var setting = new SingleValueTest()
                {
                    Name = Consts.CompetitiveAdvantagesSetting,
                    ValueAR = model.TitleAR,
                    ValueEN = model.TitleEN,
                    //SecondValue = model.Body,
                    Image = icon
                };
                _db.SingleValueTests.Add(setting);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }

        public bool AddSliderImage(AddSliderImageModel model, string icon)
        {
            try
            {
                var setting = new SingleValueTest()
                {
                    Name = Consts.MainPageSliderSetting,
                    ValueAR = model.TitleAr,
                    ValueEN = model.TitleEn,
                    //SecondValue = model.Body,
                    Image = icon
                };
                _db.SingleValueTests.Add(setting);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        public bool AddCounter(AddCounterModel model, string icon)
        {
            try
            {
                var setting = new MultipleValueText()
                {
                    Name = Consts.CountersSetting,
                    ValueAR = model.LabelAR,
                    ValueEN = model.LabelEN,
                    SecondValue = model.Count,
                    Image = icon
                };
                _db.MultipleValueTexts.Add(setting);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }

        public bool AddClient(string image)
        {
            try
            {
                var setting = new Setting()
                {
                    Value = image,
                    Name = Consts.OurClientsSetting
                };
                _db.Settings.Add(setting);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }


        public (bool Result, FooterInformations Footer) GetFooterInformations(string currentLanguage)
        {
            try
            {
                var model = new FooterInformations()
                {
                    Email = GetSettingValue(Consts.EmailSetting),
                    Location = GetSingleValueByLanguage(Consts.LocationSetting, currentLanguage).Value,
                    Phones = GetSettingValue(Consts.PhonesSetting),
                };
                return (true, model);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }

        public (bool Result, MediaInformations Media) GetMediaInformations(string currentLanguage)
        {
            try
            {
                var model = new MediaInformations()
                {
                    FacebookLink = GetSettingValue(Consts.FaceBookLink),
                    TwitterLink = GetSettingValue(Consts.TwitterLink)
                };
                return (true, model);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }

        public (bool Result, AboutCompanyModel Model) GetAboutCompanyModel()
        {
            try
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

                var model = new AboutCompanyModel()
                {
                    AboutCompany = GetSingleValue(Consts.AboutCompanySetting).Value,
                    CompetitiveAdvantageTitle = GetSingleValue(Consts.CompetitiveAdvantageTitleSetting).Value,
                    Message = GetSingleValue(Consts.MessageSetting).Value,
                };
                var advantages = _db.SingleValueTests.Where(s => s.Name == Consts.CompetitiveAdvantagesSetting)
                    .Select(s => new CompetitiveAdvantageListModel()
                    {
                        ID = s.ID,
                        Title = currentCulture.TwoLetterISOLanguageName == "ar" ? s.ValueAR : s.ValueEN,
                        Icon = s.Image,

                    }).ToList();
                model.Advantages = advantages;


                var counters = _db.MultipleValueTexts.Where(s => s.Name == Consts.CountersSetting)
                    .Select(s => new CounterListModel()
                    {
                        ID = s.ID,
                        Label = currentCulture.TwoLetterISOLanguageName == "ar" ? s.ValueAR : s.ValueEN,
                        Count = s.SecondValue,
                        Icon = s.Image,

                    }).ToList();
                model.Counters = counters;
                var clients = _db.Settings.Where(s => s.Name == Consts.OurClientsSetting)
                    .Select(s => new OurClientsListModel()
                    {
                        Image = s.Value,
                        ID = s.ID
                    }).ToList();
                model.Clients = clients;

                var members = _db.Members.Select(m => new MemberListModel()
                {
                    FacebookLink = m.FacebookLink,
                    ID = m.ID,
                    Image = m.Image,
                    SkypeLink = m.SkypeLink,
                    InstgramLink = m.InstagramLink,
                    Name = currentCulture.TwoLetterISOLanguageName == "ar" ? m.NameAR : m.NameEN,
                    JobTitle = currentCulture.TwoLetterISOLanguageName == "ar" ? m.JobTitleAR : m.JobTitleEN,
                    TwitterLink = m.TwitterLink
                }).ToList();
                model.Members = members;
                return (true, model);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }
        public (bool Result, AboutCompanyAdminModel Model) GetAboutCompanyAdminModel()
        {
            try
            {
                var model = new AboutCompanyAdminModel()
                {
                    AboutCompanyAR = GetSingleValueAR(Consts.AboutCompanySetting).Value,
                    AboutCompanyEN = GetSingleValueEN(Consts.AboutCompanySetting).Value,
                    CompetitiveAdvantageTitleAR = GetSingleValueAR(Consts.CompetitiveAdvantageTitleSetting).Value,
                    CompetitiveAdvantageTitleEN = GetSingleValueEN(Consts.CompetitiveAdvantageTitleSetting).Value,
                    Message = GetSingleValue(Consts.MessageSetting).Value,
                };
                var advantages = _db.SingleValueTests.Where(s => s.Name == Consts.CompetitiveAdvantagesSetting)
                    .Select(s => new CompetitiveAdvantageAdminListModel()
                    {
                        ID = s.ID,
                        TitleAR = s.ValueAR,
                        TitleEN = s.ValueEN,
                        Icon = s.Image,

                    }).ToList();
                model.Advantages = advantages;


                var counters = _db.MultipleValueTexts.Where(s => s.Name == Consts.CountersSetting)
                    .Select(s => new CounterAdminListModel()
                    {
                        ID = s.ID,
                        LabelAR = s.ValueAR,
                        LabelEN = s.ValueEN,
                        Count = s.SecondValue,
                        Icon = s.Image,

                    }).ToList();
                model.Counters = counters;
                var clients = _db.Settings.Where(s => s.Name == Consts.OurClientsSetting)
                    .Select(s => new OurClientsListModel()
                    {
                        Image = s.Value,
                        ID = s.ID
                    }).ToList();
                model.Clients = clients;

                var members = _db.Members.Select(m => new MemberAdminListModel()
                {
                    FacebookLink = m.FacebookLink,
                    ID = m.ID,
                    Image = m.Image,
                    SkypeLink = m.SkypeLink,
                    InstgramLink = m.InstagramLink,
                    NameAR = m.NameAR,
                    NameEN = m.NameEN,
                    JobTitleAR = m.JobTitleAR,
                    JobTitleEN = m.JobTitleEN,
                    TwitterLink = m.TwitterLink
                }).ToList();
                model.Members = members;
                return (true, model);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }

        public (bool Result, FAQAndTeamModel Model) GetFAQAndTeam()
        {
            try
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

                var model = new FAQAndTeamModel()
                {
                    FAQAR = GetSingleValueAR(Consts.FAQSetting).Value,
                    FAQEN = GetSingleValueEN(Consts.FAQSetting).Value,
                };
                var members = _db.Members.Select(m => new MemberListModel()
                {
                    FacebookLink = m.FacebookLink,
                    ID = m.ID,
                    Image = m.Image,
                    SkypeLink = m.SkypeLink,
                    InstgramLink = m.InstagramLink,
                    Name = currentCulture.TwoLetterISOLanguageName == "ar" ? m.NameAR : m.NameEN,
                    JobTitle = currentCulture.TwoLetterISOLanguageName == "ar" ? m.JobTitleAR : m.JobTitleEN,
                    TwitterLink = m.TwitterLink
                }).ToList();
                model.Members = members;
                return (true, model);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }
        public bool AddTeamMember(AddTeamMemberModel model, string image)
        {
            try
            {
                var member = new Member()
                {
                    FacebookLink = model.FacebookLink,
                    Image = image,
                    InstagramLink = model.InstagramLink,
                    JobTitleAR = model.JobTitleAR,
                    JobTitleEN = model.JobTitleEN,
                    NameAR = model.NameAR,
                    NameEN = model.NameEN,
                    SkypeLink = model.SkypeLink,
                    TwitterLink = model.TwitterLink,
                };
                _db.Members.Add(member);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        public bool DeleteMember(int memberID)
        {
            try
            {
                var member = _db.Members.Find(memberID);

                _db.Members.Remove(member);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        #region News
        public bool CreateNews(CreateNewsModel model, string image, string secondImage)
        {
            try
            {
                var news = new News()
                {
                    Date = DateTime.Now,
                    SecondImage = secondImage,
                    SummaryAR = model.SummaryAR,
                    SummaryEN = model.SummaryEN,
                    CategoryID = model.Category,
                    ContentAR = model.ContentAR,
                    ContentEN = model.ContentEN,
                    Image = image,
                    TitleAR = model.TitleAR,
                    TitleEN = model.TitleEN,
                };
                _db.News.Add(news);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        public (bool Result, List<CategoryListModel> Categories) GetNewsCategories()
        {
            try
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

                return (true, _db.NewsCategories.Where(s => !s.IsDeleted)
                    .Select(s => new CategoryListModel()
                    {
                        Name = currentCulture.TwoLetterISOLanguageName == "ar" ? s.NameAR : s.NameEN,
                        NameAR = s.NameAR,
                        NameEN = s.NameEN,
                        ID = s.ID
                    }).ToList());
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }
        public (bool Result, List<CustomerCategoriesListModel> Categories) GetNewsCategoriesForCustomer()
        {
            try
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

                var categories = _db.NewsCategories.Where(s => !s.IsDeleted)
                    .Select(s => new CategoryListModel()
                    {
                        Name = currentCulture.TwoLetterISOLanguageName == "ar" ? s.NameAR : s.NameEN,
                        NameAR = s.NameAR,
                        NameEN = s.NameEN,
                        ID = s.ID
                    }).ToList();
                var customerCategories = new List<CustomerCategoriesListModel>();
                categories.ForEach(c =>
                {
                    customerCategories.Add(new CustomerCategoriesListModel()
                    {
                        ID = c.ID,
                        Count = _db.News.Where(n => n.CategoryID == c.ID).Count(),
                        Name = c.Name,

                    });
                });
                return (true, customerCategories);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }
        public (bool Result, List<NewsListModel> News) GetNews()
        {
            try
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

                return (true, _db.News.Where(n => !n.IsDeleted).Include(n => n.Category).Select(s => new NewsListModel
                {
                    Category = currentCulture.TwoLetterISOLanguageName == "ar" ? s.Category.NameAR : s.Category.NameEN,
                    Date = s.Date.ToString("dd-MM-yyyy"),
                    DateTime = s.Date,
                    Title = currentCulture.TwoLetterISOLanguageName == "ar" ? s.TitleAR : s.TitleEN,
                    Summary = currentCulture.TwoLetterISOLanguageName == "ar" ? s.SummaryAR : s.SummaryEN,
                    ID = s.ID,
                    Image = s.Image,
                }).ToList());
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }
        public (bool Result, List<NewsListModel> News) GetNews(int newsID)
        {
            try
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

                return (true, _db.News.Where(n => !n.IsDeleted).Include(n => n.Category).Select(s => new NewsListModel
                {
                    Category = currentCulture.TwoLetterISOLanguageName == "ar" ? s.Category.NameAR : s.Category.NameEN,
                    Date = s.Date.ToString("dd-MM-yyyy"),
                    DateTime = s.Date,
                    Title = currentCulture.TwoLetterISOLanguageName == "ar" ? s.TitleAR : s.TitleEN,
                    ID = s.ID,
                }).ToList());
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }
        public (bool Result, List<NewsListModel> News) GetNewsByCatrgory(int categoryID)
        {
            try
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

                return (true, _db.News.Where(n => !n.IsDeleted && n.CategoryID == categoryID).Include(n => n.Category).Select(s => new NewsListModel
                {
                    Category = currentCulture.TwoLetterISOLanguageName == "ar" ? s.Category.NameAR : s.Category.NameEN,
                    Date = s.Date.ToString("dd-MM-yyyy"),
                    DateTime = s.Date,
                    Title = currentCulture.TwoLetterISOLanguageName == "ar" ? s.TitleAR : s.TitleEN,
                    ID = s.ID,
                }).ToList());
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }
        public (bool Result, EditNewsModel News) GetEditNewsForEdit(int newsID)
        {
            try
            {
                var news = _db.News.Find(newsID);
                var model = new EditNewsModel()
                {
                    ContentAR = news.ContentAR,
                    SummaryEN = news.SummaryEN,
                    SummaryAR = news.SummaryAR,
                    ID = news.ID,
                    ContentEN = news.ContentEN,
                    TitleAR = news.TitleAR,
                    TitleEN = news.TitleEN,

                };
                return (true, model);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }
        public bool EditNews(EditNewsModel model, string image, string secondImage)
        {
            try
            {
                var news = _db.News.Find(model.ID);
                news.TitleAR = model.TitleAR;
                news.TitleEN = model.TitleEN;
                news.SummaryAR = model.SummaryAR;
                news.SummaryEN = model.SummaryEN;
                news.ContentAR = model.ContentAR;
                news.ContentEN = model.ContentEN;
                news.CategoryID = model.Category;
                if (image != "")
                {
                    news.Image = image;
                }
                if (secondImage != "")
                {
                    news.SecondImage = secondImage;
                }
                _db.News.Update(news);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        public bool DeleteNews(int newsID)
        {
            try
            {
                var news = _db.News.Find(newsID);
                news.IsDeleted = true;
                _db.News.Update(news);
                _db.SaveChanges();
                return (true);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        public bool DeleteNewsCategory(int categoryID)
        {
            try
            {
                var category = _db.NewsCategories.Find(categoryID);
                category.IsDeleted = true;
                _db.NewsCategories.Update(category);
                _db.SaveChanges();
                return (true);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        public bool CreateNewsCategories(CreateCategoryModel model)
        {
            try
            {
                var category = new NewsCategory()
                {
                    IsDeleted = false,
                    NameAR = model.NameAR,
                    NameEN = model.NameEN
                };
                _db.NewsCategories.Add(category);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        #endregion


        #region Our Services
        public bool AddService(AddServiceModel model, string icon)
        {
            try
            {
                var service = new OurServices()
                {
                    BodyAr = model.BodyAr,
                    BodyEn = model.BodyEn,
                    TitleAr = model.TitleAr,
                    TitleEn = model.TitleEn,
                    Icon = icon
                };
                _db.OurServices.Add(service);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }

        public bool DeleteServiceByID(int id)
        {
            try
            {
                var service = _db.OurServices.Find(id);
                _db.OurServices.Remove(service);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        #endregion

        #region contact
        public bool SaveContact(string phoneNumber)
        {
            try
            {
                var contact = new ContactNumber()
                {
                    PhoneNumber = phoneNumber
                };
                _db.ContactNumbers.Add(contact);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public (bool Result, List<ContactNumbersModel> Messages) GetContactNumbers()
        {
            try
            {
                var numbers = _db.ContactNumbers.Select(a => new ContactNumbersModel
                {
                    Id = a.ID,
                    PhoneNumber = a.PhoneNumber

                }).ToList();

                return (true, numbers);
            }
            catch (Exception ex)
            {
                return (false, new());
            }
        }
        #endregion

        #region Site Message
        public bool SaveMessage(SiteMessageModel model)
        {
            try
            {
                var message = new SiteMessages()
                {
                    FromEmail = model.FromEmail,
                    FullName = model.FullName,
                    Message = model.Message
                };
                _db.SiteMessages.Add(message);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public (bool Result, List<SiteMessageModel> Messages )GetSiteMessages()
        {
            try
            {
                var messages = _db.SiteMessages.Select(a => new SiteMessageModel
                {
                    FromEmail = a.FromEmail,
                    FullName = a.FullName,
                    Message = a.Message

                }).ToList();
              
                return (true,messages);
            }
            catch (Exception ex)
            {
                return ( false, new ());
            }
        }

        #endregion 
    }
}
