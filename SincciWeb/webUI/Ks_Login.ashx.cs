using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState; 
using BLL;
using Model;
using Model;
namespace SincciKC.webUI
{
    /// <summary>
    /// Ks_Login 的摘要说明
    /// </summary>
    public class Ks_Login : IHttpHandler, IRequiresSessionState 
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "gb2312";
            try
            {
                string ksh = config.CheckChar(context.Request["ksh"].Trim().ToString());
                string xm = config.CheckChar(context.Request["xm"].Trim().ToString());
                string pwd = config.CheckChar(context.Request["pwd"].Trim().ToString());
                string code = context.Request["code"].ToString();

                Model_zk_ksSession ksSession = new Model_zk_ksSession();

                if (context.Session["random"] != null)
                {
                    if (code == context.Session["random"].ToString())
                    {
                        BLL_Ks_Login ks = new BLL_Ks_Login();

                        int sa = ks.KS_Login(ksh, xm, pwd);

                        if (sa == 1)
                        {
                            context.Response.Write("success");
                            context.Session["success"] = "success";
                        }
                        else if (sa == 2)
                        {
                            context.Response.Write("success1");
                            context.Session["success"] = "success1";
                        }
                        else if (sa == 3)
                        {
                            context.Response.Write("success3");
                            context.Session["success"] = "success3";
                        }
                        else
                        {
                            context.Response.Write(sa); //验证失败
                        }
                    }
                    else
                    {
                        context.Response.Write("fail1"); //验证码有误
                    }
                   
                }
                else
                {
                    context.Response.Write("fail1"); //验证码有误
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(ex);
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