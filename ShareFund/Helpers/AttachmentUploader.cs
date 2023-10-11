using DBContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ShareFund.Helpers
{
    public class AttachmentUploader
    {
        IConfiguration _configuration;
        ExceptionHandler EXH;
        ApplicationDBContext _db;
        string applicationPath = "";
        public AttachmentUploader(IConfiguration configuration, ApplicationDBContext db)
        {
            _configuration = configuration;
            EXH = new ExceptionHandler(db);
            _db = db;
            applicationPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot");
            //applicationPath = Directory.GetCurrentDirectory();
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

        public async Task<(bool Result,string Message)> UploadImages(List<IFormFile> files)
        {
            try
            {
                List<string> allowedExtentions = new List<string>() { ".png", ".jpg", ".jpeg" };
                var imagesPath = GetSettingValue(Consts.ImagesUploadPath);
              
                _db.SaveChanges();
                return await UploadFilesToPath(files, imagesPath, allowedExtentions);
               
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false,ex.Message);
            }
        }
        public async Task<(bool Result,string Message)> UploadImage(IFormFile file)
        {
            try
            {
                List<string> allowedExtentions = new List<string>() { ".png", ".jpg", ".jpeg" };
                var imagesPath = GetSettingValue(Consts.ImagesUploadPath);
              
                _db.SaveChanges();
                return await UploadFileToPath(file, imagesPath, allowedExtentions);
               
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false,ex.Message);
            }
        }
   
        
        private async Task<(bool Result,string Message)> UploadFileToPath(IFormFile file, string path, List<string> allowedExtentions)
        {
            try
            {
                var fileName = "";
                 
                var extention = Path.GetExtension(file.FileName);
                if (!allowedExtentions.Contains(extention.ToLower()))
                {
                    return (false,Consts.NotAllowedExtention);
                }
                fileName = Guid.NewGuid().ToString() + extention;
                path = Path.Combine(applicationPath,path,fileName);
                using (var stream = new FileStream(path, FileMode.CreateNew))
                {
                    await file.CopyToAsync(stream);
                }   
                return (true,fileName);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false,ex.Message);
            }
        }
        private async Task<(bool Result,string Message)> UploadFilesToPath(List<IFormFile> files, string path, List<string> allowedExtentions)
        {
            try
            {
                List<string> fileNames=new List<string>();
                foreach (var file in files)
                {
                    var fileName = "";

                    var extention = Path.GetExtension(file.FileName);
                    if (!allowedExtentions.Contains(extention.ToLower()))
                    {
                        return (false, Consts.NotAllowedExtention);
                    }
                    fileName = Guid.NewGuid().ToString() + extention;
                    var uploadpath = Path.Combine(applicationPath, path, fileName);
                    using (var stream = new FileStream(uploadpath, FileMode.CreateNew))
                    {
                        await file.CopyToAsync(stream);
                    }
                    fileNames.Add(fileName);
                }
                return (true,string.Join(",",fileNames));
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false,ex.Message);
            }
        }

    }
   
}
