using System;
using System.Collections.Generic;
 
using System.Web;
using System.Web.SessionState;
using System.Text;
using System.IO;
using Model;
using BLL;
 
namespace SincciKC.websystem.bmsz
{
    /// <summary>
    /// HandleUpPhoto 的摘要说明 处理上传图片
    /// </summary>
    public class HandleUpPhoto : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// 相片剪切
        /// </summary>
        ThumbClass TC = new ThumbClass();         
        /// <summary>
        /// 考生信息管量控制类
        /// </summary>
        BLL_zk_ksxxgl bllks = new BLL_zk_ksxxgl();
        /// <summary>
        /// 考生实体类
        /// </summary>
        Model_zk_ksxxgl modks = new Model_zk_ksxxgl();
        /// <summary>
        /// 判断报名号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string isKsh(string str)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;

            switch (UserType)
            {
                //系统管理员
                case 1:
                    return "";
                //市招生办
                case 2:
                    return "";
                //区招生办
                case 3:
                    if (Department == str.Trim().Substring(2, 4))
                    {
                        return "";
                    }
                    else
                    {
                        return "考生不属于您所属县区.";
                    }

                //学校用户 
                case 4:
                    if (Department == str.Trim().Substring(2, 6))
                    {
                        return "";
                    }
                    else
                    {
                        return "考生不属于您所属学校.";
                    }
                default:
                    return "*";
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            if (SincciLogin.Sessionstu().Flag)
            {
                try
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("utf-8");

 
                    HttpPostedFile file = context.Request.Files["Filedata"];
                    if (file != null)
                    { 
                        //日志文件
                        string txtFileName = HttpContext.Current.Server.MapPath("~\\logfile\\UpPhotoLog\\") + config.Get_UserName + ".txt";  //原图路径   + strDatetime + ".txt";
                        //报名号
                        string ksh = file.FileName.ToLower().Replace(".jpg", "");

                        //相片不是jpg格式
                        if (System.IO.Path.GetExtension(file.FileName).ToLower() == ".jpg")
                        {
                            modks = bllks.zk_ksxxglDisp(ksh);

                            //相片命名有误，找不到该报名号
                            if (modks.Ksh.Length > 0)
                            {
                                string strc = isKsh(modks.Ksh);
                                if (strc != "")
                                {
                                    new config().WritTxt("[" + ksh + "]" + strc + "上传时间：" + DateTime.Now.ToString() + "", txtFileName);
                                    context.Response.Write("0");
                                }
                                else
                                {
                                    //已打印不能修改
                                    if (modks.Xxdy == 1)
                                    {
                                        new config().WritTxt("[" + ksh + "]考生已打印确认，不能修改相片。上传时间：" + DateTime.Now.ToString() + "", txtFileName);
                                        context.Response.Write("0");
                                    }
                                    else
                                    {
                                        if (CheckUpPhoto(modks, file, ksh, txtFileName))
                                        {
                                            if (type == 1)
                                                new config().WritTxt("[" + ksh + "]相片上传成功[覆盖]。上传时间：" + DateTime.Now.ToString() + "", txtFileName);
                                            else
                                            new config().WritTxt("[" + ksh + "]相片上传成功。上传时间：" + DateTime.Now.ToString() + "", txtFileName);
                                            context.Response.Write("1");
                                        }
                                        else
                                        {
                                            context.Response.Write("0");
                                            new config().WritTxt("[" + ksh + "]相片上传失败。上传时间：" + DateTime.Now.ToString() + "", txtFileName);
                                        }
                                       
                                    }
                                }
                            }
                            else
                            {
                                new config().WritTxt("[" + ksh + "]相片命名有误，找不到该报名号。上传时间：" + DateTime.Now.ToString() + "", txtFileName);
                                context.Response.Write("0");
                            }
                        }
                        else
                        {
                            new config().WritTxt("[" + ksh + "]相片不是jpg格式。上传时间：" + DateTime.Now.ToString() + "", txtFileName);
                            context.Response.Write("0");
                        }
                    }
                    else
                    {
                        context.Response.Write("0");
                    }

                }
                catch (Exception ex)
                {
                    context.Response.Write("0");
                }
            }
        }
        private int type = 0;
        /// <summary>
        /// 处理相片
        /// </summary>
        /// <returns></returns>
        private bool CheckUpPhoto(Model_zk_ksxxgl info, HttpPostedFile file, string ksh, string txtFileName)
        {
            try
            {
                string temp = config.Get_UserName;
                int Owidth = 150;
                int Oheight = 200;

                //临时文件夹               
                string uploadPath = HttpContext.Current.Server.MapPath("~\\pictemp\\" + temp + "\\");  //原图路径
                //正式文件夹
             //   string strPath = "z:\\13\\" + info.Bmdxqdm + "\\";
                string strPath = HttpContext.Current.Server.MapPath("//13//" + info.Bmdxqdm + "//");  //缩略图路径

                //检查临时文件夹是否存在
                if (!Directory.Exists(uploadPath))  //判断要保存的路径是否存在
                {
                    Directory.CreateDirectory(uploadPath);  //不存在就创建
                }
                //保存到临时文件夹
                uploadPath += file.FileName;
                file.SaveAs(uploadPath);

                //检查正式文件夹是否存在
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);  //不存在就创建
                }
                strPath += file.FileName;

                //检查是否保存成功
                if (File.Exists(HttpContext.Current.Server.MapPath(String.Format("~\\pictemp\\" + temp + "\\{0}", file.FileName))))
                {

                    System.Drawing.Image img = System.Drawing.Image.FromFile(uploadPath);
                    int width = img.Width;
                    int height = img.Height;
                    if (width > Owidth || height > Oheight)
                    {
                        int oheight = height * Owidth / width;
                        int owidth = width * Oheight / height;
                        if (File.Exists(strPath))
                        {
                            type = 1;
                        }
                        //按比例剪切图片
                        if (width / Owidth > height / Oheight)
                        {
                            TC.MakeThumbnail(uploadPath, strPath, Owidth, oheight, "HW");  //裁切图片尺寸：宽168，高240 168*240
                        }
                        else
                        {
                            TC.MakeThumbnail(uploadPath, strPath, owidth, Oheight, "HW");  //裁切图片尺寸：宽168，高240 168*240
                        }
                        if (new BLL_zk_ksxxgl().KsPhoto(ksh))
                        {  
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        File.Copy(uploadPath, strPath, true);
                        if (new BLL_zk_ksxxgl().KsPhoto(ksh))
                        { 
                            return true;
                        }
                        else
                        {
                            return false;
                        } 
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}