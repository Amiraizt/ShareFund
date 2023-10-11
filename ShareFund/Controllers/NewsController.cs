using DBContext;
using K4os.Hash.xxHash;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShareFund.Helpers;
using ShareFund.Models.News;
using System.Reflection;

namespace ShareFund.Controllers
{
    public class NewsController : Controller
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

        public NewsController(ApplicationDBContext db, IConfiguration configration)
        {
            CS = new CoreService(db);
            _uploader = new AttachmentUploader(configration, db);
        }
        [Authorize]
        public IActionResult Index()
        {
            var getNews=CS.GetNews();
            if (!getNews.Result)
            {
                Alert("Error", Consts.AdminNotificationType.error);
            }
            return View(getNews.News);
        }
        [Authorize]
        public IActionResult Create()
        {
            var getCategories=CS.GetNewsCategories();
            if (!getCategories.Result)
            {
                Alert("Error", Consts.AdminNotificationType.error);
            }
            ViewBag.Categories = getCategories.Categories;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateNewsModel model) {
            var uploadResult = await _uploader.UploadImage(model.Image);
            if (!uploadResult.Result)
            {
                Alert(uploadResult.Message, Consts.AdminNotificationType.error);
                var getCategories = CS.GetNewsCategories();
                if (!getCategories.Result)
                {
                    Alert("Error", Consts.AdminNotificationType.error);
                }
                ViewBag.Categories = getCategories.Categories;
                return View(model);
            }
            string image = uploadResult.Message;
            string secondImage = "";
            if (model.SecondImage != null)
            {
                uploadResult = await _uploader.UploadImage(model.SecondImage);
                if (!uploadResult.Result)
                {
                    Alert(uploadResult.Message, Consts.AdminNotificationType.error);
                    var getCategories = CS.GetNewsCategories();
                    if (!getCategories.Result)
                    {
                        Alert("Error", Consts.AdminNotificationType.error);
                    }
                    ViewBag.Categories = getCategories.Categories;
                    return View(model);
                }
                secondImage = uploadResult.Message;
            }
            var reuslt = CS.CreateNews(model, image,secondImage);
            if (!reuslt)
            {
                Alert("Error", Consts.AdminNotificationType.error);
                var getCategories = CS.GetNewsCategories();
                if (!getCategories.Result)
                {
                    Alert("Error", Consts.AdminNotificationType.error);
                }
                ViewBag.Categories = getCategories.Categories;
                return View(model);
            }
            return Redirect(nameof(Index));
        }
        [Authorize]
        public IActionResult Edit(int newsID)
        {
            var getCategories = CS.GetNewsCategories();
            if (!getCategories.Result)
            {
                Alert("Error", Consts.AdminNotificationType.error);
            }
            ViewBag.Categories = getCategories.Categories;
            var getNews = CS.GetEditNewsForEdit(newsID);
            if (!getNews.Result)
            {
                Alert("Error", Consts.AdminNotificationType.error);
            }
            return View(getNews.News);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditNewsModel model) {
            var image = "";
            if (model.Image != null)
            {
                var uploadResult = await _uploader.UploadImage(model.Image);
                if (!uploadResult.Result)
                {
                    Alert(uploadResult.Message, Consts.AdminNotificationType.error);
                    var getCategories=CS.GetNewsCategories();
            if (!getCategories.Result)
            {
                Alert("Error", Consts.AdminNotificationType.error);
            }
            ViewBag.Categories = getCategories.Categories;
                    return View(model);
                }
                image = uploadResult.Message;
            }
            string secondImage = "";
            if (model.SecondImage != null)
            {
                var uploadResult = await _uploader.UploadImage(model.SecondImage);
                if (!uploadResult.Result)
                {
                    Alert(uploadResult.Message, Consts.AdminNotificationType.error);
                    var getCategories = CS.GetNewsCategories();
                    if (!getCategories.Result)
                    {
                        Alert("Error", Consts.AdminNotificationType.error);
                    }
                    ViewBag.Categories = getCategories.Categories;
                    return View(model);
                }
                secondImage = uploadResult.Message;
            }
            var result=CS.EditNews(model,image,secondImage);
            if (!result)
            {
                var getCategories = CS.GetNewsCategories();
                if (!getCategories.Result)
                {
                    Alert("Error", Consts.AdminNotificationType.error);
                }
                ViewBag.Categories = getCategories.Categories;
                return View(model);
            }
            return Redirect(nameof(Index));
        }
        public IActionResult Delete(int newsID)
        {
            var result=CS.DeleteNews(newsID);
            if (!result)
            {
                Alert("Error", Consts.AdminNotificationType.error);
            }
            return Redirect(nameof(Index));
        }
        [Authorize]
        public IActionResult Categories()
        {
            var getCategories=CS.GetNewsCategories();
            if (!getCategories.Result)
            {
                Alert("Error", Consts.AdminNotificationType.error);
            }
            return View(getCategories.Categories);
        }
        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryModel model)
        {
            var result = CS.CreateNewsCategories(model);
            if (!result)
            {
                Alert("Error", Consts.AdminNotificationType.error);
            }
            return Redirect(nameof(Categories));
        }
        public IActionResult DeleteCategoryByID(int categoryID)
        {
            var result = CS.DeleteNewsCategory(categoryID);
            if (!result)
            {
                Alert("Error", Consts.AdminNotificationType.error);
            }
            return RedirectToAction(nameof(Categories));
        }
    }
}
