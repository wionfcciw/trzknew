using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Data.OleDb;
using Wuqi.Webdiyer;
using System.Reflection;
using System.Security.Cryptography;

namespace BLL
{
    /// <summary>
    /// config 的摘要说明
    /// </summary>
    public class config
    {
        //public static string SiteName = "揭阳中考网上报考系统";
        //public static string CopyRight = "版权所有&copy; 广州市社会保障卡的管理机构<br>联系电话：83366288 &nbsp;&nbsp;Email: card-service@gz.gov.cn <br>技术支持：广州市信驰智能科技有限公司&nbsp;&nbsp;粤ICP备10240498号-3";

        //public static string Company = "广州市信驰智能科技有限公司";
        //public static string CompanyPhone = "83366288";

        //public static string dsdm = "52";
        //public static string YearSys = "2011";

        // Gmail 帐号配置
        const string ADDRESS_FROM = "guangzhoushebao@gmail.com";
        // const string ADDRESS_TO = "49185669@qq.com";
        const string USER_ID = "guangzhoushebao";
        const string PASSWORD = "sincci+++++";
        const string SMTP_SERVER = "smtp.gmail.com";
        const int PORT = 587;

        //const string ADDRESS_FROM = "348416843@qq.com";
        //const string ADDRESS_TO = "348416843@qq.com";
        //const string USER_ID = "348416843@qq.com";
        //const string PASSWORD = "gztsnl1985";
        //const string SMTP_SERVER = "smtp.qq.com";
        //const int PORT = 587;

        //public config()
        //{
        //    //
        //    // TODO: 在此处添加构造函数逻辑
        //    //



        //}

        public static string key = "ADRRHWQE";


        #region 系统基本信息设置
        /// <summary>
        /// 系统基本信息设置
        /// </summary>
        /// <returns></returns>
        public string[] tblSystem()
        {
            DataSet ds = new DataSet();
            DBcon objDBcon = new DBcon();
            string[] tblSystem = new string[12];

            //系统基本信息设置 技术服务信息设置
            ds = objDBcon.DataAdapterSearch("select *  from tblSystem ", "tblSystem");
            if (ds.Tables.Count > 0 && ds.Tables["tblSystem"].Rows.Count > 0)
            {
                tblSystem[0] = ds.Tables["tblSystem"].Rows[0]["SysYear"].ToString().Trim();              //系统年份
                tblSystem[1] = ds.Tables["tblSystem"].Rows[0]["SysName"].ToString().Trim();              //系统名称
                tblSystem[2] = ds.Tables["tblSystem"].Rows[0]["CompanyName"].ToString().Trim();          //单位名称
                tblSystem[3] = ds.Tables["tblSystem"].Rows[0]["CompanyAdress"].ToString().Trim();        //单位地址
                tblSystem[4] = ds.Tables["tblSystem"].Rows[0]["Dsdm"].ToString().Trim();                 //地市代码
                tblSystem[5] = ds.Tables["tblSystem"].Rows[0]["Years"].ToString().Trim();                //使用年限
                tblSystem[6] = ds.Tables["tblSystem"].Rows[0]["KFPhone"].ToString().Trim();              //客服联系电话
                tblSystem[7] = ds.Tables["tblSystem"].Rows[0]["KFQQ"].ToString().Trim();                 //客服联系QQ
                tblSystem[8] = ds.Tables["tblSystem"].Rows[0]["JSPhone"].ToString().Trim();              //技术联系电话
                tblSystem[9] = ds.Tables["tblSystem"].Rows[0]["JSQQ"].ToString().Trim();                 //技术联系QQ

                tblSystem[10] = ds.Tables["tblSystem"].Rows[0]["MaxYear"].ToString().Trim();                 //最大年龄

                tblSystem[11] = ds.Tables["tblSystem"].Rows[0]["MinYear"].ToString().Trim();                 //最小年龄


            }

            return tblSystem;
        }
        #endregion

        #region 返回MD5加密
        /// <summary>
        /// 返回MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string md5(string str, int code)
        {
            if (code == 16)   //16位MD5加密（取32位加密的9~25字符） 
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
            }
            else//32位加密 
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
            }
        }
        #endregion

        #region 返回随机数
        /// <summary>
        /// 返回随机数
        /// </summary>
        /// <param name="VcodeNum"></param>
        /// <returns></returns>
        public string RndNum(int VcodeNum)
        {

            string Vchar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";

            string[] VcArray = Vchar.Split(',');

            string VNum = "";//由于字符串很短，就不用StringBuilder了

            int temp = -1;//记录上次随机数值，尽量防止生产多个一样的随机数

            //采用一个基本的算法以保证生成随机数的不同

            Random rand = new Random();

            for (int i = 1; i < VcodeNum + 1; i++)
            {

                if (temp != -1)
                {

                    rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));

                }

                //int t =  rand.Next(35) ;

                int t = rand.Next(35);

                if (temp != -1 && temp == t)
                {

                    return RndNum(VcodeNum);

                }

                temp = t;

                VNum += VcArray[t];

            }

            return VNum;

        }
        #endregion

        #region 发送邮件

        //const string strSmtpServer = "smtp.qq.com"; //
        //const string strFrom = "2283057757@qq.com"; //
        //const string strFromPass = "123456a"; // 

        /// <summary>
        ///  QQ邮箱
        /// </summary>
        /// <param name="strSmtpServer">第一个参数是邮箱服务器</param>
        /// <param name="strFrom">第二个参数发件人的帐号</param>
        /// <param name="strFromPass">第三个参数发件人密码</param>
        /// <param name="strto">第四个参数收件人帐号</param>
        /// <param name="strSubject">第五个参数主题</param>
        /// <param name="strBody">第六个参数内容.</param>
        public static void SendSMTPEMail(string strSmtpServer, string strFrom, string strFromPass, string strto, string strSubject, string strBody)
        {
            System.Net.Mail.SmtpClient client = new SmtpClient(strSmtpServer);
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(strFrom, strFromPass);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            System.Net.Mail.MailMessage message = new MailMessage(strFrom, strto, strSubject, strBody);
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            client.Send(message);
        }






        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="ADDRESS_TO"></param>
        /// <param name="subject"></param>
        /// <param name="mailbody"></param>
        public static void SendMail(string ADDRESS_TO, string subject, string mailbody)
        {
            try
            {
                SmtpClient mailClient = new SmtpClient(SMTP_SERVER, PORT);
                mailClient.EnableSsl = true;
                NetworkCredential crendetial = new NetworkCredential(USER_ID, PASSWORD);

                mailClient.Credentials = crendetial;
                MailMessage message = new MailMessage(ADDRESS_FROM, ADDRESS_TO, subject, mailbody);

                mailClient.Send(message);
                // Console.WriteLine("Mail has been sent to '{0}'", ADDRESS_TO);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
                //  Response.Write("<script language='javascript'>alert('登录失败！');</script>");
            }
        }
        #endregion

        #region 用于过滤用户输入非法字符
       
        /// <summary>
        /// 用于过滤用户输入非法字符
        /// </summary>
        /// <param name="strUserInput">带过滤的字符</param>
        /// <returns>返回过滤的字符</returns>
        public static string CheckChar(string strUserInput)
        {
            strUserInput = strUserInput.Replace("<", "〈");
            strUserInput = strUserInput.Replace(">", "〉");
            strUserInput = strUserInput.Replace("'", "’");
            strUserInput = strUserInput.Replace("%", "％");
            strUserInput = strUserInput.Replace(";", "；");
            strUserInput = strUserInput.Replace("(", "（");
            strUserInput = strUserInput.Replace(")", "）");
            strUserInput = strUserInput.Replace("&", "＆ ");
            strUserInput = strUserInput.Replace("+", " ＋");
            strUserInput = strUserInput.Replace("_", "— ");
            strUserInput = strUserInput.Replace("#", "＃");
            // strUserInput = strUserInput.Replace("@", "＠");
            // strUserInput = strUserInput.Replace(" ", "&nbsp;");
            strUserInput = strUserInput.Replace("cookie", "/cookie");
            strUserInput = strUserInput.Replace("document", "/document");
            strUserInput = strUserInput.Replace("javascript", "/javascript");
            strUserInput = strUserInput.Replace("select", " ");
            strUserInput = strUserInput.Replace("delete", " ");
            strUserInput = strUserInput.Replace("del", " ");
            strUserInput = strUserInput.Replace("insert", " ");
            strUserInput = strUserInput.Replace("update", " ");
            strUserInput = strUserInput.Replace("truncate", " ");
            strUserInput = strUserInput.Replace("table", " ");

            strUserInput = strUserInput.Replace("or", " ");
            strUserInput = strUserInput.Replace("and", " ");
            strUserInput = strUserInput.Replace("=", " ");

            strUserInput = strUserInput.Replace("execute", " ");
            strUserInput = strUserInput.Replace("exec", " ");
            strUserInput = strUserInput.Replace("like", " ");

            return strUserInput;
        }


        /// <summary>
        /// 用于过滤用户输入非法字符
        /// </summary>
        /// <param name="strUserInput">带过滤的字符</param>
        /// <returns>返回过滤的字符</returns>
        public static string CheckChar2(string strUserInput)
        {
            strUserInput = strUserInput.Replace("<", "〈");
            strUserInput = strUserInput.Replace(">", "〉");
            strUserInput = strUserInput.Replace("'", "’");
            strUserInput = strUserInput.Replace("%", "％");
           // strUserInput = strUserInput.Replace(";", "；");
            strUserInput = strUserInput.Replace("(", "（");
            strUserInput = strUserInput.Replace(")", "）");
            strUserInput = strUserInput.Replace("&", "＆ ");
            strUserInput = strUserInput.Replace("+", " ＋");
           // strUserInput = strUserInput.Replace("_", "— ");
            strUserInput = strUserInput.Replace("#", "＃");
            // strUserInput = strUserInput.Replace("@", "＠");
            // strUserInput = strUserInput.Replace(" ", "&nbsp;");
            strUserInput = strUserInput.Replace("cookie", "/cookie");
            strUserInput = strUserInput.Replace("document", "/document");
            strUserInput = strUserInput.Replace("javascript", "/javascript");
            strUserInput = strUserInput.Replace("select", " ");
            strUserInput = strUserInput.Replace("delete", " ");
            strUserInput = strUserInput.Replace("del", " ");
            strUserInput = strUserInput.Replace("insert", " ");
            strUserInput = strUserInput.Replace("update", " ");
            strUserInput = strUserInput.Replace("truncate", " ");
            strUserInput = strUserInput.Replace("table", " ");
            strUserInput = strUserInput.Replace("or", " ");
            strUserInput = strUserInput.Replace("and", " ");
            strUserInput = strUserInput.Replace("=", " ");
            strUserInput = strUserInput.Replace("execute", " ");
            strUserInput = strUserInput.Replace("exec", " ");
            strUserInput = strUserInput.Replace("like", " ");

            return strUserInput;
        }
        #endregion

        #region "HTMLEncode 转换"
        public string unHTMLEncode(string fString)
        {
            if (fString.Length > 0)
            {
                fString = fString.Replace("&gt;", ">");
                fString = fString.Replace("&lt;", "<");
                fString = fString.Replace("&nbsp;", System.Convert.ToChar(32).ToString());
                fString = fString.Replace("　　", System.Convert.ToChar(9).ToString());
                fString = fString.Replace("&quot;", System.Convert.ToChar(34).ToString());
                fString = fString.Replace("&#39;", System.Convert.ToChar(39).ToString());
                // fString = fString.Replace("", System.Convert.ToChar(13).ToString());
                fString = fString.Replace("</P><P>", System.Convert.ToChar(10).ToString() + "&" + System.Convert.ToChar(10).ToString());
                fString = fString.Replace("<BR>", System.Convert.ToChar(10).ToString());

            }
            return fString;
        }


        public string dvHTMLEncode(string fString)
        {
            if (fString.Length > 0)
            {
                fString = fString.Replace(">", "&gt;");
                fString = fString.Replace("<", "&lt;");

                fString = fString.Replace(System.Convert.ToChar(32).ToString(), "&nbsp;");
                fString = fString.Replace(System.Convert.ToChar(9).ToString(), "　　");
                fString = fString.Replace(System.Convert.ToChar(34).ToString(), "&quot;");
                fString = fString.Replace(System.Convert.ToChar(39).ToString(), "&#39;");
                //  fString = fString.Replace(System.Convert.ToChar(13).ToString(), "");
                fString = fString.Replace(System.Convert.ToChar(10).ToString() + "&" + System.Convert.ToChar(10).ToString(), "</P><P> ");
                fString = fString.Replace(System.Convert.ToChar(10).ToString(), "<BR>");


            }
            return fString;
        }
        #endregion

        #region 获取客户端用户ip
        /// <summary>
        /// 获取客户端用户ip
        /// </summary>
        /// <returns></returns>
        public static string GetUserIP()
        {
            string userIP = string.Empty;
            //if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] == null)
            //{
            userIP = System.Web.HttpContext.Current.Request.UserHostAddress;
            //}
            //else
            //{
            //    userIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            //}
            return userIP;
        }
        #endregion

        #region 检测相片
        /// <summary>
        /// 检测相片
        /// </summary>
        /// <param name="fudpicture"></param>
        /// <param name="Size">大小</param>
        /// <param name="Width">宽</param>
        /// <param name="Height">高</param>
        /// <param name="GetPixelx1">检测底色</param>
        /// <param name="GetPixely1"></param>
        /// <param name="GetPixelx2"></param>
        /// <param name="GetPixely2"></param>
        /// <param name="IntColor"></param>
        /// <param name="AllowedType"></param>
        /// <param name="Resolution"></param>
        /// <returns></returns>
        public string CheckPhoto(FileUpload fudpicture, int Size, int Width, int Height, int GetPixelx1, int GetPixely1, int GetPixelx2, int GetPixely2, int IntColor, string AllowedType, int Resolution)
        {


            string ErrMessage = ""; //错误信息      
            int intsize = 1024 * Size;   //大小多少K
            Stream stream = fudpicture.PostedFile.InputStream;  //转为流
            System.Drawing.Image image = System.Drawing.Image.FromStream(stream);  //画成图判断宽、高    
            float horizontalResolution = image.HorizontalResolution;//获取上传文件的水平分辨率（以“像素/英寸”为单位）。 
            float verticalResolution = image.VerticalResolution;//获取上传文件的垂直分辨率（以“像素/英寸”为单位）。
            string fileContentType = fudpicture.PostedFile.ContentType; //获取文件类型
            string[] allowedExtensions = { AllowedType };  //允许上传的文件类型
            string fileExtension = System.IO.Path.GetExtension(fudpicture.PostedFile.FileName).ToLower();  //判断文件后缀

            Bitmap bitmap1 = new Bitmap(stream);  //判断相片底色
            if (GetPixelx1 > 0 && GetPixely1 > 0)
            {
                Color color1 = bitmap1.GetPixel(GetPixelx1, GetPixely1);
                if (color1.A < IntColor || color1.B < IntColor || color1.G < IntColor || color1.R < IntColor)
                {
                    ErrMessage = ErrMessage + "·相片底色不正确\\n";
                    // ErrTrue = false;
                }
            }
            if (GetPixelx2 > 0 && GetPixely2 > 0)
            {
                Color color2 = bitmap1.GetPixel(GetPixelx2, GetPixely2);
                if (color2.A < IntColor || color2.B < IntColor || color2.G < IntColor || color2.R < IntColor)
                {
                    ErrMessage = ErrMessage.Replace("·相片底色不正确\\n", "");
                    ErrMessage = ErrMessage + "·相片底色不正确\\n";
                    // ErrTrue = false;
                }
            }
            if (fudpicture.PostedFile.ContentLength > intsize)
            {
                ErrMessage = ErrMessage + "·上传图片不能超过" + Size + "K\\n";
                // ErrTrue = false;
            }
            if (image.Width != Width || image.Height != Height)
            {
                ErrMessage = ErrMessage + "·相片宽高不正确，相片必须是宽" + Width + "px 高" + Height + "px\\n";
                // ErrTrue = false;
            }
            if (horizontalResolution != Resolution || verticalResolution != Resolution)
            {
                ErrMessage = ErrMessage + "·相片分辨率不正确，相片分辨率必须是" + Resolution + "相素\\n";
                // ErrTrue = false;
            }
            if (fileContentType != "image/pjpeg")
            {
                ErrMessage = ErrMessage + "·不是相片格式\n";
                // ErrTrue = false;
            }
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension != allowedExtensions[i])
                {
                    ErrMessage = ErrMessage + "·相片后缀不是" + AllowedType + "格式\\n";
                    // ErrTrue = false;
                }
            }
            return ErrMessage;

        }
        #endregion

        #region 打开弹出下载页面
        /// <summary>
        /// 打开弹出下载页面
        /// </summary>
        /// <param name="FileLocation">文件地址</param>
        /// <param name="FileName">文件名称</param>
        public void DownloadFile(string FileLocation, string FileName)
        {
            FileStream fs = new FileStream(FileLocation, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";//通知浏览器下载文件而不是打开
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8));
            System.Web.HttpContext.Current.Response.BinaryWrite(bytes);
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();

        }
        #endregion

        #region 删除文件文件或图片
        //// <summary>
        /// 删除文件文件或图片
        /// </summary>
        /// <param name="path">当前文件的路径</param>
        /// <returns>是否删除成功</returns>
        public static bool FilePicDelete(string path)
        {
            bool ret = false;
            System.IO.FileInfo file = new System.IO.FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
                ret = true;
            }
            return ret;
        }
        #endregion

        #region 验证身份证号码
        /// <summary>
        /// 验证身份证号码
        /// </summary>
        /// <param name="Id">身份证号码</param>
        /// <returns>验证成功为True，否则为False</returns>
        public static bool CheckIDCard(string Id)
        {
            if (Id.Length == 18)
            {
                bool check = CheckIDCard18(Id);
                return check;
            }
            else if (Id.Length == 15)
            {
                bool check = CheckIDCard15(Id);
                return check;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 身份证号码验证

        /// <summary>
        /// 验证15位身份证号
        /// </summary>
        /// <param name="Id">身份证号</param>
        /// <returns>验证成功为True，否则为False</returns>
        private static bool CheckIDCard18(string Id)
        {
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准
        }

        /// <summary>
        /// 验证18位身份证号
        /// </summary>
        /// <param name="Id">身份证号</param>
        /// <returns>验证成功为True，否则为False</returns>
        private static bool CheckIDCard15(string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            return true;//符合15位身份证标准
        }
        #endregion

        #region 写入日志上传图片
        /// <summary>
        /// 写入日志上传图片
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="FileName"></param>
        public void  WritTxt(string Content, string LogPath)
        { 
            if (!File.Exists(LogPath))
            {
                StreamWriter sw = File.CreateText(LogPath);
                sw.WriteLine(Content);
                sw.Flush();
                sw.Close();

            }
            else
            {
                StreamWriter sw = File.AppendText(LogPath);
                sw.WriteLine(Content);
                sw.Flush();
                sw.Close(); 
            } 
        }
        #endregion

        #region 写入txt
        /// <summary>
        /// 写入txt
        /// </summary>
        /// <param name="Content">内容</param>
        /// <param name="Path">文件地址</param>
        /// <param name="FileName">文件名称</param>
        public void WritTxt(string Content, string Path, string FileName)
        {
            //StreamWriter sw = new StreamWriter();

            if (!File.Exists(HttpContext.Current.Server.MapPath("~\\" + Path + "\\" + FileName + "")))
            {
                StreamWriter sw = File.CreateText(HttpContext.Current.Server.MapPath("~\\" + Path + "\\" + FileName + ""));
                sw.WriteLine(Content);
                sw.Flush();
                sw.Close();

            }
            else
            {
                StreamWriter sw = File.AppendText(HttpContext.Current.Server.MapPath("~\\" + Path + "\\" + FileName + ""));
                sw.WriteLine(Content);
                sw.Flush();
                sw.Close();

            }


        }
        #endregion

        #region 系统出错写入错误日志
        /// <summary>
        /// 系统出错写入错误日志
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="FileName"></param>
        public void WritTxtErr(string Content, string FileName)
        {
            //StreamWriter sw = new StreamWriter();

            if (!File.Exists(HttpContext.Current.Server.MapPath("~\\logfile\\err\\" + FileName + ".txt")))
            {
                StreamWriter sw = File.CreateText(HttpContext.Current.Server.MapPath("~\\logfile\\err\\" + FileName + ".txt"));
                sw.WriteLine(Content);
                sw.Flush();
                sw.Close();

            }
            else
            {
                StreamWriter sw = File.AppendText(HttpContext.Current.Server.MapPath("~\\logfile\\err\\" + FileName + ".txt"));
                sw.WriteLine(Content);
                sw.Flush();
                sw.Close();

            }


        }
        #endregion

        #region 点击下载.txt格式文件
        /// <summary>
        /// 点击下载.txt格式文件
        /// </summary>
        /// <param name="destFileName"></param>
        public void DownloadTxt(string destFileName)
        {
            if (File.Exists(destFileName))
            {
                destFileName = HttpContext.Current.Server.UrlDecode(destFileName);



                FileInfo fi = new FileInfo(destFileName);
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.ClearHeaders();
                System.Web.HttpContext.Current.Response.Buffer = false;
                System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(Path.GetFileName(destFileName), System.Text.Encoding.UTF8));
                System.Web.HttpContext.Current.Response.AppendHeader("Content-Length", fi.Length.ToString());
                System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                System.Web.HttpContext.Current.Response.WriteFile(destFileName);
                System.Web.HttpContext.Current.Response.Flush();
                System.Web.HttpContext.Current.Response.End();

            }
        }
        #endregion

        #region "生成随机数"
        /// <summary>
        /// 生成时间加随机数
        /// </summary>
        /// <returns></returns>
        public static string rndSix()
        {
            Random rnd = new Random((int)(DateTime.Now.Ticks));
            return rnd.Next(999999).ToString();
        }
        #endregion

        #region "动态生成.dbf 文件"
        /// <summary>
        /// 动态生成.dbf 文件
        /// </summary>
        /// <param name="Columns">字段</param>
        /// <param name="Filename">文件夹</param>
        /// <param name="tblName">表名</param>
        public void CreateDBF(string Columns, string Filename, string tblName)
        {
            if (File.Exists(HttpContext.Current.Server.MapPath("~\\" + Filename + "\\" + tblName + ".dbf")))
            {
                File.Delete(HttpContext.Current.Server.MapPath("~\\" + Filename + "\\" + tblName + ".dbf"));
            }
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Server.MapPath("~\\" + Filename + "\\") + ";Ex" + "tended Properties=dBASE IV;User ID=Admin;Password=";
            OleDbConnection oldcon = new OleDbConnection(strConn);
            OleDbCommand cmd = new OleDbCommand();
            oldcon.Open();
            System.Data.OleDb.OleDbCommand cmd1 = new System.Data.OleDb.OleDbCommand("create table " + tblName + "(" + Columns + ")", oldcon);
            cmd1.ExecuteNonQuery();
            oldcon.Close();

        }
        #endregion

        #region "超时处理"
        /// <summary>
        /// 超时处理
        /// </summary>
        public void Timeout()
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                HttpContext.Current.Response.Redirect("/Exit.aspx");
            }
            if (HttpContext.Current.User.Identity.Name.Length == 0)
            {
                HttpContext.Current.Response.Redirect("/Exit.aspx");
            }

        }
        #endregion

        #region "获取页面url"
        /// <summary>
        /// 获取当前访问页面地址 /Index.aspx 
        /// </summary>
        public static string GetScriptName
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"].ToString();
            }
        }

        /// <summary>
        /// 检测当前url是否包含指定的字符
        /// </summary>
        /// <param name="sChar">要检测的字符</param>
        /// <returns></returns>
        public static bool CheckScriptNameChar(string sChar)
        {
            bool rBool = false;
            if (GetScriptName.ToLower().LastIndexOf(sChar) >= 0)
                rBool = true;
            return rBool;
        }

        /// <summary>
        /// 获取当前页面的扩展名
        /// </summary>
        public static string GetScriptNameExt
        {
            get
            {
                return GetScriptName.Substring(GetScriptName.LastIndexOf(".") + 1);
            }
        }

        /// <summary>
        /// 获取当前访问页面地址参数
        /// </summary>
        public static string GetScriptNameQueryString
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["QUERY_STRING"].ToString();
            }
        }

        /// <summary>
        /// 获得页面文件名和参数名
        /// </summary>
        public static string GetScriptNameUrl
        {
            get
            {
                string Script_Name = config.GetScriptName;
                Script_Name = Script_Name.Substring(Script_Name.LastIndexOf("/") + 1);
                Script_Name += "?" + GetScriptNameQueryString;
                return Script_Name;
            }
        }

        /// <summary>
        /// 获取当前访问页面Url
        /// </summary>
        public static string GetScriptUrl
        {
            get
            {
                return config.GetScriptNameQueryString == "" ? config.GetScriptName : string.Format("{0}?{1}", config.GetScriptName, config.GetScriptNameQueryString);
            }
        }

        /// <summary>
        /// 返回当前页面目录的url
        /// </summary>
        /// <param name="FileName">文件名</param>
        /// <returns></returns>
        public static string GetHomeBaseUrl(string FileName)
        {
            string Script_Name = config.GetScriptName;
            return string.Format("{0}/{1}", Script_Name.Remove(Script_Name.LastIndexOf("/")), FileName);
        }

        /// <summary>
        /// 返回当前网站网址
        /// </summary>
        /// <returns></returns>
        public static string GetHomeUrl()
        {
            return HttpContext.Current.Request.Url.Authority;
        }

        /// <summary>
        /// 获取当前访问文件物理目录
        /// </summary>
        /// <returns>路径</returns>
        public static string GetScriptPath
        {
            get
            {
                string Paths = HttpContext.Current.Request.ServerVariables["PATH_TRANSLATED"].ToString();
                return Paths.Remove(Paths.LastIndexOf("\\"));
            }
        }
        #endregion

        #region "允许密码出错次数"
        /// <summary>
        /// 允许密码出错次数
        /// </summary>
        /// <returns></returns>
        public static int pwderr()
        {
            return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["pwdErr"].ToString());

        }
        #endregion

        #region "获取Pagesize"
        /// <summary>
        /// 获取Pagesize 数据
        /// </summary>
        /// <returns></returns>
        public ArrayList PageSizelist()
        {
            ArrayList list = new ArrayList();
            //list.Add("3");
            list.Add("15");
            list.Add("20");
            list.Add("30");
            list.Add("40");
            list.Add("50");
            list.Add("100");
            // list = (ArrayList)System.Configuration.ConfigurationManager.GetSection("Pagesize");

            return list;
        }
        #endregion

        #region "两个时间进行对比"

        /// <summary>
        /// 两个时间比较，第一个早于、等于或者晚于等二时间
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public static int DateTimeCompare(DateTime dt1, DateTime dt2)
        {
            return DateTime.Compare(dt1, dt2);
        }

        /// <summary>
        /// 两个时间进行对比，返回天数
        /// </summary>
        /// <param name="DateTime1">较早的日期和时间</param>
        /// <param name="DateTime2">较迟的日期和时间</param>
        /// <returns></returns>
        public static int DateDiff(DateTime DateTime1, DateTime DateTime2)
        {

            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();

            return ts.Days;
        }

        /// <summary>
        ///两个时间进行对比，返回分钟,与当前时间进行对比。
        /// </summary>
        /// <param name="DateTimeOld">较早的日期和时间</param>
        public static int DateDiff_minu(DateTime DateTimeOld)
        {
            // int aa= DateTime.Compare(DateTime.Now, DateTimeOld);

            TimeSpan ts1 = new TimeSpan(DateTimeOld.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            int minu = ts.Days * 1440 + ts.Hours * 60 + ts.Minutes;
            //  int minu = ts.Minutes;
            return minu;
        }

        /// <summary>
        ///两个时间进行对比，返回小时,与当前时间进行对比。
        /// </summary>
        /// <param name="DateTimeOld">较早的日期和时间</param>
        public static int DateDiff_hours(DateTime DateTimeOld)
        {
            // int aa= DateTime.Compare(DateTime.Now, DateTimeOld);

            TimeSpan ts1 = new TimeSpan(DateTimeOld.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            int hours = ts.Days * 24 + ts.Hours;
            //  int minu = ts.Minutes;
            return hours;
        }
        #endregion

        #region "获取登录用户UserName"
        /// <summary>
        /// 获取登录用户UserName,如果未登录为""
        /// </summary>
        public static string Get_UserName
        {
            get
            {
                return BLL.SincciLogin.Sessionstu().UserName;
            }

        }


        #endregion

        #region "用户在线过期时间"
        /// <summary>
        /// 用户在线过期时间 (分)默认30分 如果用户在当前设定的时间内没有任何操作,将会被系统自动退出
        /// </summary>
        public static int OnlineMinute
        {
            get
            {
                try
                {
                    int _onlineminute = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["OnlineMinute"]);
                    if (_onlineminute == 0)
                        return 10000;
                    else
                        return _onlineminute;
                }
                catch
                {
                    return 30;
                }
            }
        }
        #endregion

        #region "格式化日期"


        /// <summary>
        /// 格式化日期 17位 yyyyMMddHHmmssfff
        /// </summary>
        /// <returns></returns>
        public static string FormatDateToStringId(DateTime d)
        {
            return d.ToString("yyyyMMddHHmmssfff");
        }

        /// <summary>
        /// 格式化日期24小时制为字符串如:2008/12/12 21:22:33
        /// </summary>
        /// <param name="d">日期</param>
        /// <returns>字符</returns>
        public static string FormatDateToString(DateTime d)
        {
            return d.ToString("yyyy/MM/dd HH:mm:ss");
        }
        /// <summary>
        /// string转换成datetime 转换为日期
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static DateTime FormatStringToDate(string str)
        {
            return Convert.ToDateTime(str);
        }
        /// <summary>
        /// 格式化日期24小时制为字符串如:2007-4-16
        /// </summary>
        /// <param name="d">日期</param>
        /// <returns>字符</returns>
        public static string FormatDateToString2(DateTime d)
        {
            return d.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 今天星期几
        /// </summary>
        /// <returns></returns>
        public static string Week()
        {
            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = weekdays[Convert.ToInt32(DateTime.Now.DayOfWeek)];

            return week;
        }



        #endregion

        #region "获得sessionid"
        /// <summary>
        /// 获得sessionid
        /// </summary>
        public static string GetSessionID
        {
            get
            {
                return HttpContext.Current.Session.SessionID;
            }
        }
        #endregion

        #region  AspNetPager定制信息显示 (显示内容和顺序 可自由定制)
        /// <summary>
        ///  AspNetPager定制信息显示 (显示内容和顺序 可自由定制)
        /// </summary>
        /// <param name="AspNetPager1"></param>
        /// <returns></returns>
        public static void CustomInfoHTML(AspNetPager AspNetPager1, int RecordCount, int PageSize)
        {
            AspNetPager1.RecordCount = RecordCount;  //得出总的记录条数
            AspNetPager1.CustomInfoHTML = "<div style='padding-top:6px'>页码：<font color='red'><b>%CurrentPageIndex%</b></font>/<font color='red'><b>%PageCount%</b></font>页 总记录：<font color='red'><b>%RecordCount%</b></font>条</div>";
            AspNetPager1.FirstPageText = "首页";
            AspNetPager1.LastPageText = "尾页";
            AspNetPager1.PrevPageText = "上一页";
            AspNetPager1.NextPageText = "下一页";
            AspNetPager1.PageSize = PageSize;
            AspNetPager1.TextBeforePageIndexBox = "转到";
            AspNetPager1.TextAfterPageIndexBox = "页";
            AspNetPager1.AlwaysShow = true;
            AspNetPager1.ShowCustomInfoSection = ShowCustomInfoSection.Left;
            AspNetPager1.UrlPageSizeName = "pagesize";
            AspNetPager1.ShowPageIndexBox = ShowPageIndexBox.Always;
            AspNetPager1.UrlPaging = true;
            AspNetPager1.PageIndexBoxType = PageIndexBoxType.DropDownList;
            // AspNetPager1.CssClass = "paginator";
            // AspNetPager1.HorizontalAlign = HorizontalAlign.Right;
            // AspNetPager1.Width = 720;


        }

        /// <summary>
        ///  AspNetPager定制信息显示 (显示内容和顺序 可自由定制)
        /// </summary>
        /// <param name="AspNetPager1"></param>
        /// <returns></returns>
        public static void CustomInfoHTML_List(AspNetPager AspNetPager1, int RecordCount, int PageSize)
        {
            AspNetPager1.RecordCount = RecordCount;  //得出总的记录条数
            AspNetPager1.CustomInfoHTML = "<div style='padding-top:0px'>页码：<font color='red'><b>%CurrentPageIndex%</b></font>/<font color='red'><b>%PageCount%</b></font>页 总记录：<font color='red'><b>%RecordCount%</b></font>条</div>";
            AspNetPager1.FirstPageText = "[首页]";
            AspNetPager1.LastPageText = "[尾页]";
            AspNetPager1.PrevPageText = "[上一页]";
            AspNetPager1.NextPageText = "[下一页]";
            AspNetPager1.PageSize = PageSize;

            //AspNetPager1.TextBeforePageIndexBox = "转到";
            // AspNetPager1.TextAfterPageIndexBox = "页";
            AspNetPager1.AlwaysShow = true;
            AspNetPager1.ShowCustomInfoSection = ShowCustomInfoSection.Left;
            AspNetPager1.UrlPageSizeName = "pagesize";
            //  AspNetPager1.ShowPageIndexBox = ShowPageIndexBox.Always;
            AspNetPager1.UrlPaging = true;
            // AspNetPager1.PageIndexBoxType = PageIndexBoxType.DropDownList;
            // AspNetPager1.CssClass = "paginator";
            // AspNetPager1.HorizontalAlign = HorizontalAlign.Right;
            // AspNetPager1.Width = 720;


        }
        #endregion

        #region "获取用户Form提交字段值"
        /// <summary>
        /// 获取post和get提交值
        /// </summary>
        /// <param name="InputName">字段名</param>
        /// <param name="Method">post和get</param>
        /// <param name="MaxLen">最大允许字符长度 0为不限制</param>
        /// <param name="MinLen">最小字符长度 0为不限制</param>
        /// <param name="DataType">字段数值类型 int 和str和dat不限为为空</param>
        /// <returns></returns>
        public static object sink(string InputName, MethodType Method, int MaxLen, int MinLen, DataType DataType)
        {
            HttpContext rq = HttpContext.Current;
            string TempValue = "";

            if (Method == MethodType.Post)
            {
                if (rq.Request.Form[InputName] != null)
                {
                    TempValue = config.CheckChar(rq.Request.Form[InputName].ToString());
                }
            }
            else if (Method == MethodType.Get)
            {
                if (rq.Request.QueryString[InputName] != null)
                {
                    TempValue = config.CheckChar(rq.Request.QueryString[InputName].ToString());
                }
            }
            if (TempValue != "")
            {
                return TempValue;
            }
            else
            {
                switch (DataType)
                {
                    case DataType.Int:
                        return 0;
                    case DataType.Dat:
                        return null;
                    case DataType.Long:
                        return long.MinValue;
                    case DataType.Double:
                        return 0.0f;
                    default:
                        return TempValue;
                }
            }
        }
        #endregion

        #region "英文双引号替换成中文双引号"
        /// <summary>
        /// 英文双引号替换成中文双引号
        /// </summary> 
        public static string strReplace(string str)
        {
            return str.Replace("\"", "”");
        }
        #endregion

        #region "获得xls表数据"
        /// <summary>
        /// 获得xls表
        /// </summary>
        /// <param name="FilePath">文件地址</param>
        /// <param name="number">第几个表名 0表示第一个表名</param>
        /// <returns></returns>
        public DataTable GetXlsTableName(string FilePath, int number)
        {
            string myConn = "Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = '" + FilePath + "';Extended Properties=Excel 8.0";
            OleDbConnection cnnxls = new OleDbConnection(myConn);
            DataSet myDataSet = new DataSet();   //创建DataSet对象  
            cnnxls.Open();
            DataTable dt = cnnxls.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string StyleSheet = dt.Rows[number][2].ToString().Trim();    //.xls的第N个表名

            string StrSql = string.Format("SELECT * FROM [{0}]", StyleSheet);
            OleDbDataAdapter myCommand = new OleDbDataAdapter(StrSql, myConn);
            myCommand.Fill(myDataSet, string.Format("[{0}]", StyleSheet));
            myCommand.Dispose();
            DataTable DT = myDataSet.Tables[string.Format("[{0}]", StyleSheet)];

            cnnxls.Close();
            cnnxls.Dispose();
            return DT;
        }
        #endregion

        #region "导出数据生成 xls 表"
        /// <summary>
        /// 导出数据 xls
        /// </summary>
        /// <param name="list">数据源</param>
        /// <param name="fileName">文件名称</param>
        /// 列的数量
        public string ExportTableXls(DataSet lst, string fileName)
        {
            //StringBuilder sb = new StringBuilder();
            //if (list.Count > 0)
            //{
            //    sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\">");
            //    sb.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");

            //    //写出列名 标题
            //    sb.AppendLine("<tr style=\"font-weight: bold; white-space: nowrap;\">");
            //    PropertyInfo[] propertys = list[0].GetType().GetProperties();
            //    foreach (PropertyInfo pi in propertys)
            //    {
            //        sb.AppendLine("<td>" + pi.Name + "</td>");

            //    }
            //    sb.AppendLine("</tr>");

            //    //写出数据 
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        sb.Append("<tr>");
            //        foreach (PropertyInfo pi in propertys)
            //        {
            //            string getvalue = "";
            //            try
            //            {
            //                getvalue = Convert.ToString(pi.GetValue(list[i], null));
            //                sb.Append("<td style=\"vnd.ms-excel.numberformat:@\">" + getvalue + "</td>");
            //            }
            //            catch
            //            {
            //            }
            //        }
            //        sb.AppendLine("</tr>");
            //    }
            //}

            //return sb.ToString();

            #region "dataset"
            StringBuilder sb = new StringBuilder();
            //data = ds.DataSetName + "\n"; 
            int count = 0;

            foreach (DataTable tb in lst.Tables)
            {
                //data += tb.TableName + "\n"; 
                sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\">");
                sb.AppendLine("<table cellspacing=\"0\"  rules=\"all\" border=\"1\">");
                sb.AppendLine("<tr><td colspan=\""+tb.Columns.Count+"\" style=\"font-weight: bold; font-size:30px; text-align:center\">学籍号-报名号对照表</td></tr>");
                sb.AppendLine("<tr><td colspan=\"" + tb.Columns.Count + "\" style=\"text-align:right\">" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "</td></tr>");
                //写出列名 
                sb.AppendLine("<tr style=\"font-weight: bold; white-space: nowrap;\">");
                foreach (DataColumn column in tb.Columns)
                {
                    sb.AppendLine("<td>" + column.ColumnName + "</td>");
                }
                sb.AppendLine("</tr>");

                //写出数据 
                foreach (DataRow row in tb.Rows)
                {
                    sb.Append("<tr>");
                    foreach (DataColumn column in tb.Columns)
                    {
                        // if (column.ColumnName.Equals("sfzh") || column.ColumnName.Equals("ksh") || column.ColumnName.Equals("pwd"))

                        sb.Append("<td style=\"vnd.ms-excel.numberformat:@\">" + row[column].ToString() + "</td>");

                        // else
                        //   sb.Append("<td>" + row[column].ToString() + "</td>");
                    }
                    sb.AppendLine("</tr>");
                    count++;
                }


                sb.AppendLine("</table>");
            }

            return sb.ToString();
            #endregion

        }
        #endregion
        
        #region "树型"


        /**/
        /// <summary>
        /// 添加其他节点
        /// </summary>
        /// <param name="Pading">空格</param>
        /// <param name="DirId">父路径ID</param>
        /// <param name="datatable">返回的datatable</param>
        /// <param name="deep">树形的深度</param> 
        public static System.Web.UI.WebControls.DropDownList addOtherDll(System.Web.UI.WebControls.DropDownList droplist, string Pading, int DirId, DataTable datatable, int deep)
        {
            DataRow[] rowlist = datatable.Select("ParentID='" + DirId + "'");
            foreach (DataRow row in rowlist)
            {
                string strPading = "";
                for (int j = 0; j < deep; j++)
                {
                    strPading += "　";         //用全角的空格
                }
                //添加节点
                ListItem li = new ListItem(strPading + "|--" + row["Name"].ToString(), row["ID"].ToString());
                droplist.Items.Add(li);
                // this.item.Items.Add(li);
                //递归调用addOtherDll函数，在函数中把deep加1
                addOtherDll(droplist, strPading, Convert.ToInt32(row["ID"]), datatable, deep + 1);
            }
            return droplist;
        }
        #endregion

        #region  AspNetPager定制信息显示 (显示内容和顺序 可自由定制)

        public static void AspNetPagerCustomInfoHTML(AspNetPager AspNetPager1, int RecordCount, int PageSize)
        {
            AspNetPager1.RecordCount = RecordCount;  //得出总的记录条数
            AspNetPager1.CustomInfoHTML = "<div style='padding-top:6px'>页码：<font color='red'><b>%CurrentPageIndex%</b></font>/<font color='red'><b>%PageCount%</b></font>页 总记录：<font color='red'><b>%RecordCount%</b></font>条</div>";
            AspNetPager1.FirstPageText = "[首页]";
            AspNetPager1.LastPageText = "[尾页]";
            AspNetPager1.PrevPageText = "[上一页]";
            AspNetPager1.NextPageText = "[下一页]";
            AspNetPager1.PageSize = PageSize;
            AspNetPager1.TextBeforePageIndexBox = "转到";
            AspNetPager1.TextAfterPageIndexBox = "页";
            AspNetPager1.AlwaysShow = true;
            AspNetPager1.ShowCustomInfoSection = ShowCustomInfoSection.Left;
            AspNetPager1.UrlPageSizeName = "pagesize";
            AspNetPager1.ShowPageIndexBox = ShowPageIndexBox.Always;
            AspNetPager1.UrlPaging = true;
            AspNetPager1.PageIndexBoxType = PageIndexBoxType.DropDownList;
            AspNetPager1.CssClass = "paginator";
            // AspNetPager1.HorizontalAlign = HorizontalAlign.Right;
            AspNetPager1.Width = 680;
        }
        /// <summary>
        /// 自定义分页信息
        /// </summary>
        /// <param name="AspNetPager1"></param>
        /// <param name="RecordCount"></param>
        /// <param name="PageSize"></param>
        public static void AspNetPagerCustomInfoHTML2(AspNetPager AspNetPager1, int RecordCount, int PageSize)
        {
            AspNetPager1.RecordCount = RecordCount;  //得出总的记录条数
            AspNetPager1.CustomInfoHTML = "<div style='padding-top:6px'>页码：<font color='red'><b>%CurrentPageIndex%</b></font>/<font color='red'><b>%PageCount%</b></font>页 总记录：<font color='red'><b>%RecordCount%</b></font>条</div>";
            AspNetPager1.FirstPageText = "[首页]";
            AspNetPager1.LastPageText = "[尾页]";
            AspNetPager1.PrevPageText = "[上一页]";
            AspNetPager1.NextPageText = "[下一页]";
            AspNetPager1.PageSize = PageSize;
            AspNetPager1.TextBeforePageIndexBox = "转到";
            AspNetPager1.TextAfterPageIndexBox = "页";
            AspNetPager1.AlwaysShow = true;
            AspNetPager1.ShowCustomInfoSection = ShowCustomInfoSection.Left;

            AspNetPager1.NumericButtonCount = 5;
            AspNetPager1.ShowPageIndexBox = ShowPageIndexBox.Always;

            AspNetPager1.PageIndexBoxType = PageIndexBoxType.DropDownList;

            AspNetPager1.CssClass = "paginator";
            // AspNetPager1.HorizontalAlign = HorizontalAlign.Right;

        }
        #endregion

        #region  截取指定数量的字符串
        /// <summary>
        /// 截取指定数量的字符串
        /// </summary>
        /// <param name="title">字符</param>
        /// <param name="length">截取长度</param>
        /// <returns></returns>
        public static string SetSubString(string title, int length)
        {
            if (title.Length > length)
            {
                title = title.Substring(0, length) + "…";
            }
            return title;
        }
        #endregion

        #region "文件夹"
        /// <summary>   
        /// 创建文件夹   
        /// </summary>   
        /// <param name="Path"></param>   
        public static void FolderCreate(string Path)
        {
            // 判断目标目录是否存在如果不存在则新建之   
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);
        }
        #endregion

        #region "URL加密\解密 "
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encode(string str )
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            provider.Key = Encoding.ASCII.GetBytes(key.Substring(0, 8));
            provider.IV = Encoding.ASCII.GetBytes(key.Substring(0, 8));
            byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(str);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            StringBuilder builder = new StringBuilder();
            foreach (byte num in stream.ToArray())
            {
                builder.AppendFormat("{0:X2}", num);
            }
            stream.Close();
            return builder.ToString();
        }

        /// <summary>
        /// Des 解密 GB2312
        /// </summary>
        /// <param name="str">Desc string</param>
        /// <param name="key">Key ,必须为8位 </param>
        /// <returns></returns>
        public static string Decode(string str )
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            provider.Key = Encoding.ASCII.GetBytes(key.Substring(0, 8));
            provider.IV = Encoding.ASCII.GetBytes(key.Substring(0, 8));
            byte[] buffer = new byte[str.Length / 2];
            for (int i = 0; i < (str.Length / 2); i++)
            {
                int num2 = Convert.ToInt32(str.Substring(i * 2, 2), 0x10);
                buffer[i] = (byte)num2;
            }
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            stream.Close();
            return Encoding.GetEncoding("GB2312").GetString(stream.ToArray());
        }

        #endregion


        #region "完成状态"
        /// <summary>
        /// 完成状态
        /// </summary>
        public static string FinishTag(int tag)
        {
            switch (tag)
            {
                case 0:
                    return "<font color=black>未进行</font>";
                case 1:
                    return "<font color=green>已完成且正常</font>";
                case 2:
                    return "<font color=#FF9900>已完成有异常</font>";
                case 4:
                    return "<font color=red>未完成</font>";
                default:
                    return "";
            }
        }
        #endregion

        #region "交易状态"
        /// <summary>
        /// 交易状态
        /// </summary>
        public static string TradeOder(int tag)
        {
            switch (tag)
            {
                case 0:
                    return "<font color=red>未付款</font>";
                case 1:
                    return "已付款";
                case -1:
                    return "<font color=#FF9900>付款失败</font>";               
                default:
                    return "";
            }
        }
        #endregion

        #region "获取方式"
        /// <summary>
        /// 获取方式
        /// </summary>
        public enum MethodType
        {
            /// <summary>
            /// Post方式
            /// </summary>
            Post = 1,
            /// <summary>
            /// Get方式
            /// </summary>
            Get = 2
        }
        #endregion

        #region "获取数据类型 "
        /// <summary>
        /// 获取数据类型
        /// </summary>
        public enum DataType
        {
            /// <summary>
            /// 字符
            /// </summary>
            Str = 1,
            /// <summary>
            /// 日期
            /// </summary>
            Dat = 2,
            /// <summary>
            /// 整型
            /// </summary>
            Int = 3,
            /// <summary>
            /// 长整型
            /// </summary>
            Long = 4,
            /// <summary>
            /// 双精度小数
            /// </summary>
            Double = 5,
            /// <summary>
            /// 只限字符和数字
            /// </summary>
            CharAndNum = 6,
            /// <summary>
            /// 只限邮件地址
            /// </summary>
            Email = 7,
            /// <summary>
            /// 只限字符和数字和中文
            /// </summary>
            CharAndNumAndChinese = 8

        }
        #endregion

        #region "1-9转换为Ｏ、九"
        /// <summary>
        ///  1-9转换为Ｏ、九
        /// </summary>
        /// <param name="zhifu"></param>
        /// <returns></returns>
        public static string GetDaoXie(string zhifu)
        {
            zhifu = zhifu.Replace("0", "Ｏ");
            zhifu = zhifu.Replace("1", "一");
            zhifu = zhifu.Replace("2", "二");
            zhifu = zhifu.Replace("3", "三");
            zhifu = zhifu.Replace("4", "四");
            zhifu = zhifu.Replace("5", "五");
            zhifu = zhifu.Replace("6", "六");
            zhifu = zhifu.Replace("7", "七");
            zhifu = zhifu.Replace("8", "八");
            zhifu = zhifu.Replace("9", "九");
            return zhifu;
        }
        #endregion
        /// <summary>
        /// 加密方法再封装
        /// </summary>
        /// <param name="value">待加密字符串</param>
        /// <returns>加密后的字符</returns>
        public static string Encrypt(string value)
        {
            DataEncrypt.DataEncrypt encrypt = new DataEncrypt.DataEncrypt();
            return encrypt.EncryptData(value);
        }

        /// <summary>
        /// 解密方法在封装
        /// </summary>
        /// <param name="value">待解密字符串</param>
        /// <returns>解密后字符串</returns>
        public static string Decrypt(string value)
        {
            DataEncrypt.DataEncrypt decrypt = new DataEncrypt.DataEncrypt();
            return decrypt.DecryptData(value);
        }
    }
}