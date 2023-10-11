using DBContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using ShareFund.Helpers;
using ShareFund.Models.HomePage;
using System.Globalization;
using System.Resources;

namespace ShareFund.Controllers
{
    public class HomePageController : Controller
    {
        CoreService CS;
        AttachmentUploader _uploader;
        protected void Alert(string message, Consts.AdminNotificationType notificationType)
        {
            string msg = "";
            switch (notificationType)
            {
                case Consts.AdminNotificationType.success:
                    //msg = " title='تم';message='" + message + "';type='" + notificationType.ToString().ToLower() + "'; swal({title: title,text:message,type: type});";
                    msg = " title='تم';message='" + message + "';type='" + notificationType.ToString().ToLower() + "'; sweetAlert( title,message,type);";
                    break;
                case Consts.AdminNotificationType.error:
                    //msg = "swal({title: " + "خطأ" + ",text:" + message + ",type: " + notificationType.ToString().ToLower() + "});";
                    //msg = " title='خطأ';message='" + message + "';type='" + notificationType.ToString().ToLower() + "'; swal({title: title,text:message,type: type});";
                    msg = " title='خطأ';message='" + message + "';type='" + notificationType.ToString().ToLower() + "'; sweetAlert( title,message,type);";

                    break;
                case Consts.AdminNotificationType.info:
                    //msg = " swal({title: " + "تنبيه" + ",text:" + message + ",type: " + notificationType.ToString().ToLower() + "});";
                    //msg = " title='تنبيه';message='" + message + "';type='" + notificationType.ToString().ToLower() + "'; swal({title: title,text:message,type: type});";
                    msg = " title='تنبيه';message='" + message + "';type='" + notificationType.ToString().ToLower() + "'; swal({title: title,text:message,type: type});";

                    break;
                case Consts.AdminNotificationType.warning:
                    //msg = "swal({title: " + "تحذير" + ",text:" + message + ",type: " + notificationType.ToString().ToLower() + "});";
                    //msg = " title='تحذير';message='" + message + "';type='" + notificationType.ToString().ToLower() + "'; swal({title: title,text:message,type: type});";
                    msg = " title='تحذير';message='" + message + "';type='" + notificationType.ToString().ToLower() + "'; swal({title: title,text:message,type: type});";

                    break;
            }
            //var msg = "sweetAlert('" + notificationType.ToString().ToUpper() + "', '" + message + "','" + notificationType + "')" + "";
            TempData["notification"] = msg;
        }

        public HomePageController(ApplicationDBContext db, IConfiguration configration)
        {
            CS = new CoreService(db);
            _uploader = new AttachmentUploader(configration, db);
        }
        [Authorize]
        public IActionResult Index()
        {
            var getModel = CS.GetAdminMainPageModel();
            if (!getModel.Result)
            {
            }
            return View(getModel.Model);
        }

        [HttpPost]
        public IActionResult SaveTextSetting(SaveTextSetting model)
        {
            var result = CS.SaveTextSetting(model);
            if (!result)
            {
            }
            if (model.url == null || model.url == "")
                return RedirectToAction(nameof(Index));
            return RedirectToAction(model.url);

        }

        [HttpPost]
        public IActionResult SaveSetting(SaveSetting model)
        {
            var result = CS.SaveSetting(model);
            if (!result)
            {
            }
            if (model.Value == null || model.Value == "")
                return RedirectToAction("Index");
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult SaveSingleTextSetting(SaveTextSetting model)
        {
            var result = CS.SaveTextSetting(model);
            if (!result)
            {
            }
            if (model.url == null || model.url == "")
                return RedirectToAction(nameof(Index));
            return RedirectToAction(model.url);

        }
        [HttpPost]
        public async Task<IActionResult> AddSliderImage(AddSliderImageModel model)
        {
            var uploadResult = await _uploader.UploadImage(model.Image);
            if (!uploadResult.Result)
            {
                Alert(uploadResult.Message, Consts.AdminNotificationType.error);
                return RedirectToAction(nameof(Index));
            }
            var result = CS.AddSliderImage(model, uploadResult.Message);
            if (!result)
            {
                Alert("Error", Consts.AdminNotificationType.error);
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult DeleteSettingByID(int settingID)
        {
            var result = CS.DeleteSettingByID(settingID);
            if (!result)
            {
                Alert("Error", Consts.AdminNotificationType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteServiceByID(int serviceID)
        {
            var result = CS.DeleteServiceByID(serviceID);
            if (!result)
            {
                Alert("Error", Consts.AdminNotificationType.error);
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult DeleteSingleTextByID(int settingID)
        {
            var result = CS.DeleteSingleTextByID(settingID);
            if (!result)
            {
                Alert("Error", Consts.AdminNotificationType.error);
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult DeleteMultipleTextByID(int settingID)
        {
            var result = CS.DeleteMultipleTextByID(settingID);
            if (!result)
            {
                Alert("Error", Consts.AdminNotificationType.error);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> AddService(AddServiceModel model)
        {
            var uploadResult = await _uploader.UploadImage(model.Icon);
            if (!uploadResult.Result)
            {
                Alert(uploadResult.Message, Consts.AdminNotificationType.error);
                return RedirectToAction(nameof(Index));
            }
            var result = CS.AddService(model, uploadResult.Message);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> AddAdvantage(AddServiceAdminModel model)
        {
            var uploadResult = await _uploader.UploadImage(model.Icon);
            if (!uploadResult.Result)
            {
                Alert(uploadResult.Message, Consts.AdminNotificationType.error);
                return RedirectToAction(nameof(Index));
            }
            var result = CS.AddAdvantage(model, uploadResult.Message);
            return RedirectToAction(nameof(AboutCompany));
        }
        [HttpPost]
        public async Task<IActionResult> AddCounter(AddCounterModel model)
        {
            var uploadResult = await _uploader.UploadImage(model.Icon);
            if (!uploadResult.Result)
            {
                Alert(uploadResult.Message, Consts.AdminNotificationType.error);
                return RedirectToAction(nameof(Index));
            }
            var result = CS.AddCounter(model, uploadResult.Message);
            return RedirectToAction(nameof(AboutCompany));
        }
        [HttpPost]
        public async Task<IActionResult> AddClient(IFormFile File)
        {
            var uploadResult = await _uploader.UploadImage(File);
            if (!uploadResult.Result)
            {
                Alert(uploadResult.Message, Consts.AdminNotificationType.error);
                return RedirectToAction(nameof(Index));
            }
            var result = CS.AddClient(uploadResult.Message);
            if (!result)
            {
                Alert("Error", Consts.AdminNotificationType.error);
            }
            return RedirectToAction(nameof(AboutCompany));
        }
        public JsonResult GetFooterInformations(string currentLanguage)
        {


            var getFooterInfos = CS.GetFooterInformations(currentLanguage);
            if (!getFooterInfos.Result)
            {
                return Json(false);
            }
            return Json(getFooterInfos.Footer);
        }


        [Authorize]
        public IActionResult AboutCompany()
        {
            var getAboutUs = CS.GetAboutCompanyAdminModel();
            if (!getAboutUs.Result)
            {
                Alert("Error", Consts.AdminNotificationType.error);
            }
            return View(getAboutUs.Model);
        }
        [Authorize]
        public IActionResult FAQAndTeam()
        {
            var getModel = CS.GetFAQAndTeam();
            if (!getModel.Result)
            {
                Alert("Error", Consts.AdminNotificationType.error);
            }
            return View(getModel.Model);
        }
        [HttpPost]
        public IActionResult SaveFAQ(string contentAR, string contentEN)
        {
            var result = CS.SaveTextSetting(new Models.HomePage.SaveTextSetting()
            {
                Name = Consts.FAQSetting,
                ValueAR = contentAR,
                ValueEN = contentEN
            });
            if (!result)
            {
                Alert("Error", Consts.AdminNotificationType.error);
            }
            return RedirectToAction(nameof(FAQAndTeam));
        }

        public async Task<IActionResult> AddTeamMember(AddTeamMemberModel model)
        {
            var uploadResult = await _uploader.UploadImage(model.Image);
            if (!uploadResult.Result)
            {
                Alert(uploadResult.Message, Consts.AdminNotificationType.error);
                return RedirectToAction(nameof(FAQAndTeam));
            }
            var result = CS.AddTeamMember(model, uploadResult.Message);
            if (!result)
            {
                Alert("Error", Consts.AdminNotificationType.error);
            }
            return RedirectToAction(nameof(FAQAndTeam));

        }
        public IActionResult DeleteTeamMember(int memberID)
        {
            var result = CS.DeleteMember(memberID);
            if (!result)
            {
                Alert("Error", Consts.AdminNotificationType.error);
            }
            return RedirectToAction(nameof(FAQAndTeam));
        }

        [HttpPost]
        public JsonResult SetCulture(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(30) }
            );
            return Json(true);
        }

        [HttpPost]
        public JsonResult SaveContact(string phoneNumber)
        {
            var result = CS.SaveContact(phoneNumber);
            if (!result)
            {
                return Json(false);
            }
            return Json(true);
        }


        [HttpPost]
        public JsonResult SaveSiteMessage(SiteMessageModel model)
        {
            var result = CS.SaveMessage(model);
            if (!result)
            {
                return Json(false);
            }
            return Json(true);
        }
   
        public IActionResult SiteMessagesIndex()
        {
            var result = CS.GetSiteMessages();
            if (!result.Result)
            {
                Alert("Error", Consts.AdminNotificationType.error);

                return View(nameof(SiteMessagesIndex),result.Messages);
            }
            return View(nameof(SiteMessagesIndex), result.Messages);
        }

        public IActionResult ContactNumbersIndex()
        {
            var result = CS.GetContactNumbers();
            if (!result.Result)
            {
                Alert("Error", Consts.AdminNotificationType.error);

                return View(nameof(ContactNumbersIndex), result.Messages);
            }
            return View(nameof(ContactNumbersIndex), result.Messages);
        }
    }
}
