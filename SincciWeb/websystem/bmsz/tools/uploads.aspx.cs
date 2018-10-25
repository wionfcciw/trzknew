using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using Model;
using BLL;
namespace SincciKC.websystem.bmsz.tools
{
    public partial class uploads : BPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack && (base.Request.Form["action"] != "saves"))
            {
                string result = "";
                string filename = "";
                if (UpLoad_File("Filedata", "", out result, "jpg|gif|png|bmp|jpeg", out filename))
                {
                    string pPath = base.Server.MapPath(result);

                    string ksh = filename.Substring(filename.LastIndexOf('/') + 1, filename.IndexOf('.') - (filename.LastIndexOf('/') + 1));
                    string xqdm = ksh.Substring(2, 4);
                    string bmddm = ksh.Substring(2, 6);

                    if (new BLL_zk_ksxxgl().KsPhoto(ksh))
                    { 
                        result = "99"; 
                    }
                    else
                    {
                        result = "06"; 
                    }
                }

                Response.Write("doaction.aspx?f=" + filename + "&e=" + result + "");
                Response.End();
            }
        }

        public static bool UpLoad_File(string UpLoadFile, string path, out string result, string UpFileType, out string filename)
        {
            result = "";
            filename = "";
            if (HttpContext.Current.Request.Files[UpLoadFile] == null)
            {
                result = "01"; // "你还没有选择上传的我文件或者未知错误";
                return false;
            }
            string fileName = HttpContext.Current.Request.Files[UpLoadFile].FileName;
            if (!(fileName != ""))
            {
                result = "01"; //"你还没有选择上传的我文件";
                return false;
            }
            string str2 = path;
            path = ConfigurationSettings.AppSettings["PathInfo"] + path;
            //if (path.Length == 0)
            //{
            //    result = "你还没有选择上传的我文件";
            //    return false;
            //}
            int num = fileName.LastIndexOf(".");
            string str3 = fileName.Substring(num + 1).ToLower();
            string str4 = null;
            int num2 = 0x400;
            bool flag = false;
            if (UpFileType != null)
            {
                foreach (string str5 in UpFileType.Split(new char[] { '|' }))
                {
                    if (str5 == str3)
                    {
                        flag = true;
                        break;
                    }
                }
            }
            else
            {
                flag = true;
            }
            if (flag)
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(path)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
                }
                Random random = new Random();
                DateTime now = DateTime.Now;
                string str6 = HttpContext.Current.Request.Form["fileName"];
                if (str6 == null || str6.Length == 0)
                {
                    //str4 = string.Concat(new object[] { now.ToString("yyMMddHHmmss"), random.Next(10, 0x63), ".", str3 });
                    result = "02"; // "请选择要照相的考生！";
                    return false;
                }
                else
                {
                    str4 = str6 + ".jpg";

                    path = path + "/" + str4;
                    path = path.Replace("//", "/");
                    int contentLength = HttpContext.Current.Request.Files[UpLoadFile].ContentLength;
                    if (contentLength > (num2 * 0x400))
                    {
                        result = "03"; //string.Concat(new object[] { "你上传的文件超过", num2, "K！", contentLength.ToString() });
                        return false;
                    }
                    else
                    {
                        HttpContext.Current.Request.Files[UpLoadFile].SaveAs(HttpContext.Current.Server.MapPath(path));
                        result = path;
                        filename = str4;
                        return true;
                    }
                }
            }
            result = "04"; //str3 + "你上传的文件格式已经被管理员禁止！";
            return false;
        }
    }
}