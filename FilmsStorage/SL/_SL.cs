using System.Web.Security;
using FilmsStorage.Login;
using FilmsStorage.Models.DB;
using System.Web.Script.Serialization;
using System;
using System.Configuration;
using System.Web;
using System.IO;


namespace FilmsStorage.SL
{
    public static class _SL
    {
        public static class Users
        {
            public static void SetLoginCookie(User user)
            {
                UserSerializationModel serializationModel = new UserSerializationModel();
                serializationModel.UserID = user.UserID;
          

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string userJSON = serializer.Serialize(serializationModel);
                int LoginCookieInterval=Convert.ToInt32(ConfigurationManager.AppSettings["LoginCookieInterval"]);
                
                DateTime cookieDeathTime = DateTime.Now.AddMinutes(LoginCookieInterval);
                FormsAuthenticationTicket authenticationTicket = new FormsAuthenticationTicket(
                    1,
                    user.UserName,
                    DateTime.Now,
                    cookieDeathTime,
                    true,
                    userJSON                                   
                    );
                WriteTicketToResponse(cookieDeathTime, authenticationTicket);


            }
            public static void ExtendCookieLife(FormsAuthenticationTicket ticket)
            {
                int LoginCookieInterval = Convert.ToInt32(ConfigurationManager.AppSettings["LoginCookieInterval"]);
                
                DateTime cookieDeathTime = DateTime.Now.AddMinutes(LoginCookieInterval);
                FormsAuthenticationTicket authenticationTicket = new FormsAuthenticationTicket(
                   1,
                   ticket.Name,
                   DateTime.Now,
                   DateTime.Now.AddDays(1),
                   true,
                   ticket.UserData
                   );
                WriteTicketToResponse(cookieDeathTime, authenticationTicket);
            }

            private static void WriteTicketToResponse(DateTime cookieDeathTime, FormsAuthenticationTicket authenticationTicket)
            {
                // шифрування об'єкт Ticket 
                string encryptedTicket = FormsAuthentication.Encrypt(authenticationTicket);
                // створення Cookie з об єкта Ticket
                HttpCookie loginCookie = new HttpCookie(FormsAuthentication.FormsCookieName,encryptedTicket);
                
                loginCookie.Expires = cookieDeathTime;
                HttpContext.Current.Response.Cookies.Add(loginCookie);
            }
        }

        public static class Files
        {
            public static FilmFileSaveResult SaveFilm(HttpPostedFileBase postedFile,int uploadedByUserID)
            {
                FilmFileSaveResult fileSaveResult = new FilmFileSaveResult();
                fileSaveResult.IsSaved = false;               
                try
                {
                    string fileSaveFolder = string.Empty;
                    string webSiteFolder = HttpContext.Current.Server.MapPath("~");
                    try
                    {
                        fileSaveFolder = ConfigurationManager.AppSettings["FilmsFolder"];
                    }
                    catch (ConfigurationException)
                    {

                        fileSaveFolder = "FilmFiles";
                    }

                    // створюємо папку для зберігання фільмів 
                    if (!Directory.Exists(fileSaveFolder))
                    {
                        Directory.CreateDirectory(Path.Combine(webSiteFolder, fileSaveFolder));
                    }

                    string userFileFolder = Path.Combine(webSiteFolder, fileSaveFolder, uploadedByUserID.ToString());
                   
                    if (!Directory.Exists(userFileFolder))
                    {
                        Directory.CreateDirectory(userFileFolder);
                    }
                    fileSaveResult.FilePath = userFileFolder;
                    string FileSaveName = Path.GetFileNameWithoutExtension(postedFile.FileName)
                        + "_"
                        + DateTime.Now.Ticks.ToString()
                        + Path.GetExtension(postedFile.FileName);
                    fileSaveResult.FileName = FileSaveName;
                   
                    postedFile.SaveAs(Path.Combine(userFileFolder, FileSaveName));
                    fileSaveResult.IsSaved = true;

                }
                catch(Exception ex)
                {
                    fileSaveResult.Error = ex;
                }                 
                return fileSaveResult;
            }

            public class FilmFileSaveResult
            {
                public bool IsSaved { get; set; }
                public string FileName { get; set; }
                public string FilePath { get; set; }
                public Exception Error { get;  set; }
            }
            
        }

        public static class Tools
        {
            public static string MimeTypeByFileName(string fileName)
            {
                string fileExt = Path.GetExtension(fileName);
                string mimeType = "";
                switch (fileExt)
                {
                    case ".avi":
                        {
                            mimeType = "video/x-msvideo";
                            break;
                        }
                    case ".rtf":
                        {
                            mimeType = "application/rtf";
                            break;
                        }
                    case ".webm":
                        {
                            mimeType = "video/webm";
                            break;
                        }
                    case ".ogv":
                        {
                            mimeType = "video/ogg";
                            break;
                        }
                    case ".mp4":
                    case ".mpg":
                    case ".mpeg":
                        {
                            mimeType = "video/mpeg";
                            break;
                        }
                    case ".mkv":
                        {
                            mimeType = "video/x-matroska";
                            break;
                        }
                }
                return mimeType;
            }
        }
       
    }
}