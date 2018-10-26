using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Web;

namespace BLL
{
    /// <summary>
    /// 上传类 
    /// </summary>
   public class UploadClass
   {
       #region "文件上传"
       /// <summary>
       /// 保存上传的文件
       /// </summary>
       /// <param name="FileUpload1"></param>
       /// <param name="flag">判断上传是否成功</param>
       /// <returns>返回地址URL</returns>
       public static string SaveFile(FileUpload FileUpload1, out bool flag)
       {
           //extTable.Add("file", "doc,docx,xls,xlsx,ppt,txt,zip,rar ");

           string ErrMessage = ""; //错误信息  
           string url = ""; //图片地址
           ErrMessage = CheckFile(FileUpload1, out  flag);
           string saveUrl = CheckFileUrl(out url) + ErrMessage; //保存地址
           string SaveUrlSmall = saveUrl ;

           if (flag) //检查成功，保存文件
           {
               FileUpload1.SaveAs(saveUrl); //保存文件
               ErrMessage = url + ErrMessage;
           }

           return ErrMessage;
       }

       private static string CheckFile(FileUpload FileUpload1, out bool flag)
       {
           string ErrMessage = ""; //错误信息     
           flag = true;  //成功标志
           int Size = 10;  //上传图片大小 5M            
           try
           {
               int intsize = 1024 * 1024 * Size;   //大小多少M

               string fileContentType = FileUpload1.PostedFile.ContentType; //获取文件类型
               string[] allowedExtensions = { ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".txt", ".zip", ".rar" };  //允许上传的文件类型
               string fileExtension = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower();  //判断文件后缀

               ErrMessage = fileExtension;

               if (FileUpload1.PostedFile.ContentLength > intsize)
               {
                   ErrMessage = "·上传文件不能超过" + Size + "M ";
                   flag = false;
               }
               else
               {
                   for (int i = 0; i < allowedExtensions.Length; i++)
                   {
                       if (fileExtension == allowedExtensions[i])
                       {
                           ErrMessage = fileExtension;
                           flag = true;
                           break;
                       }
                       else
                       {
                           ErrMessage = "·上传文件扩展名是不允许的扩展名。";
                           flag = false;
                       }
                   } 
               } 
           }
           catch
           {
               flag = false;
               ErrMessage = "·上传文件格式有误！";
           }
           return ErrMessage;
       }

       /// <summary>
       /// 生成图片保存地址
       /// </summary>
       /// <returns></returns>
       private static string CheckFileUrl(out string url)
       {
           string FileFolder = HttpContext.Current.Server.MapPath("\\UploadFile\\file\\" + DateTime.Now.ToString("yyyyMMdd")); //原图
          string FileUrl = "";
           string FileName = DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + config.rndSix();
           if (!System.IO.Directory.Exists(FileFolder))
           {
               System.IO.Directory.CreateDirectory(FileFolder);
           } 
           FileUrl = FileFolder + "\\" + FileName;
           url = "/UploadFile/file/" + DateTime.Now.ToString("yyyyMMdd") + "/" + FileName;
           return FileUrl;
       }


       #endregion

       #region "图片上传 上传格式：.gif,.jpg,.jpeg,.png,.bmp"
       /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="FileUpload1"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static string SavePhoto(FileUpload FileUpload1, out bool flag)
        {
            string ErrMessage = ""; //错误信息  
            string url = ""; //图片地址
            ErrMessage = CheckPhoto(FileUpload1, out  flag);
            string saveUrl = CheckUrl(out url) + ErrMessage; //保存地址
            string SaveUrlSmall = saveUrl.Replace("\\image\\", "\\smallimage\\");

            if (flag) //检查成功，保存图片
            {
                FileUpload1.SaveAs(saveUrl); //原图

               new ThumbClass().MakeThumbnail(saveUrl, SaveUrlSmall, 350, 257, "HW"); //生成缩略图

                ErrMessage = url + ErrMessage;
            }

            return ErrMessage;

        }

        /// <summary>
        /// 生成图片保存地址
        /// </summary>
        /// <returns></returns>
        private static string CheckUrl(out string url)
        {
            string FileFolder = HttpContext.Current.Server.MapPath("\\UploadFile\\image\\" + DateTime.Now.ToString("yyyyMMdd")); //原图
            string FileFolderSmall = HttpContext.Current.Server.MapPath("\\UploadFile\\smallimage\\" + DateTime.Now.ToString("yyyyMMdd")); //缩略图
            string FileUrl = "";
            string FileName = DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + config.rndSix();
            if (!System.IO.Directory.Exists(FileFolder))
            {
                System.IO.Directory.CreateDirectory(FileFolder);
            }

            if (!System.IO.Directory.Exists(FileFolderSmall))
            {
                System.IO.Directory.CreateDirectory(FileFolderSmall);
            }

            FileUrl = FileFolder + "\\" + FileName;

            url = "/UploadFile/image/" + DateTime.Now.ToString("yyyyMMdd") + "/" + FileName;
            return FileUrl;
        }


        /// <summary>
        /// 检查图片
        /// </summary>
        /// <param name="FileUpload1"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        private static string CheckPhoto(FileUpload FileUpload1, out bool flag)
        {
            string ErrMessage = ""; //错误信息     
            flag = true;  //成功标志
            int Size = 5;  //上传图片大小 5M            
            try
            {
                int intsize = 1024 * 1024 * Size;   //大小多少M
                Stream stream = FileUpload1.PostedFile.InputStream;  //转为流
                System.Drawing.Image image = System.Drawing.Image.FromStream(stream);  //画成图判断宽、高

                string fileContentType = FileUpload1.PostedFile.ContentType; //获取文件类型
                string[] allowedExtensions = { ".gif", ".jpg", ".jpeg", ".png", ".bmp" };  //允许上传的图片类型
                string fileExtension = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower();  //判断文件后缀

                ErrMessage = fileExtension;

                if (FileUpload1.PostedFile.ContentLength > intsize)
                {
                    ErrMessage = "·上传图片不能超过" + Size + "M\\n";
                    flag = false;
                }
                else
                {

                    if (fileContentType.Substring(0, fileContentType.IndexOf("/")) != "image")
                    {
                        ErrMessage = "·不是图片格式\n";
                        flag = false;
                    }
                    else
                    {
                        for (int i = 0; i < allowedExtensions.Length; i++)
                        {
                            if (fileExtension == allowedExtensions[i])
                            {
                                ErrMessage = fileExtension;
                                flag = true;
                                break;
                            }
                            else
                            {
                                ErrMessage = "·后缀不是图片格式\\n";
                                flag = false;
                            }
                        }
                    }
                }

            }
            catch
            {
                flag = false;
                ErrMessage = "·上传图片格式有误！";
            }
            return ErrMessage;
        }

        #endregion

        #region "友情链接图片上传 上传格式：.gif,.jpg,.jpeg,.png,.bmp"
        /// <summary>
        /// 保存友情链接图片
        /// </summary>
        /// <param name="FileUpload1"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static string SaveLinkPhoto(FileUpload FileUpload1, out bool flag)
        {
            string ErrMessage = ""; //错误信息  
            string url = ""; //图片地址
            ErrMessage = CheckPhoto(FileUpload1, out  flag);
            string saveUrl = CheckLinkUrl(out url) + ErrMessage; //保存地址
            string SaveUrlSmall = saveUrl.Replace("\\Link\\", "\\smallLink\\");

            if (flag) //检查成功，保存图片
            {
                FileUpload1.SaveAs(saveUrl); //原图

               new ThumbClass().MakeThumbnail(saveUrl, SaveUrlSmall, 306, 280, "HW"); //生成缩略图

                ErrMessage = url + ErrMessage;
            }

            return ErrMessage;

        }

        /// <summary>
        /// 生成图片保存地址
        /// </summary>
        /// <returns></returns>
        private static string CheckLinkUrl(out string url)
        {
            string FileFolder = HttpContext.Current.Server.MapPath("\\UploadFile\\Link\\" + DateTime.Now.ToString("yyyyMMdd")); //原图
            string FileFolderSmall = HttpContext.Current.Server.MapPath("\\UploadFile\\smallLink\\" + DateTime.Now.ToString("yyyyMMdd")); //缩略图
            string FileUrl = "";
            string FileName = DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + config.rndSix();
            if (!System.IO.Directory.Exists(FileFolder))
            {
                System.IO.Directory.CreateDirectory(FileFolder);
            }

            if (!System.IO.Directory.Exists(FileFolderSmall))
            {
                System.IO.Directory.CreateDirectory(FileFolderSmall);
            }

            FileUrl = FileFolder + "\\" + FileName;

            url = "/UploadFile/Link/" + DateTime.Now.ToString("yyyyMMdd") + "/" + FileName;
            return FileUrl;
        }

        #endregion
    }
}
