
using DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareFund.Helpers
{
    public class ExceptionHandler
    {
        ApplicationDBContext _db;

        public ExceptionHandler(ApplicationDBContext db)
        {
            _db = db;

        }
        public void LogException(Exception exception, string className = "", string methodName = "")
        {
            try
            {
                ExceptionLog exceptionLog = new ExceptionLog
                {
                    ClassName = className,
                    MethodName = methodName,
                    Message = exception?.Message,
                    DateTime = DateTime.Now,
                    InnerException = exception?.InnerException?.Message,
                    StackTrace = exception?.StackTrace
                };
           
                _db.Add<ExceptionLog>(exceptionLog);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
        public void LogException(string body, string title, string className = "", string methodName = "")
        {
            try
            {
                ExceptionLog exceptionLog = new ExceptionLog
                {
                    ClassName = className,
                    MethodName = methodName,
                    Message = body,
                    DateTime = DateTime.Now,
                    InnerException = title,
                    StackTrace = ""
                };

                _db.ExceptionLogs.Add(exceptionLog);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                 var filename = "logger-" + DateTime.Today.Date.Month + "-" + DateTime.Today.Date.Day + ".txt";
                    using (StreamWriter streamWriter = new StreamWriter(filename, true))
                    {
                        try
                        {
                            //_context.HttpContext.Request.Path
                            streamWriter.WriteLine("Error at time : " + DateTime.UtcNow.TimeOfDay + " \n\n" + "message:" + ex.Message + " with message :" + "\n " + ex.InnerException + "\n___________________________________________________________________________________________");
                        }
                        catch (Exception e)
                        {
                            streamWriter.WriteLine("error at time : " + DateTime.UtcNow + ":/n" + e.ToString());
                        }
                    }
                
            }
        }


    }

}
