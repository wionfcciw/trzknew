/*
 wf 新增 2018-08-16 
 * 考生单点登录类，所有页面统一控制
 */

using DAL;
using Model;
using Newtonsoft.Json;
using SincciKC.SsoLogin.TokenJsonEntityPack.SSOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace BLL.system
{
    public partial class SPage:System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            //wangf 2018-08-16 新增访问控制，集成于平台，与平台的token保持一致，故每次都访问
            WebClient MyWebClient = new WebClient();
            string token = Session["token"].ToString();
            string refreshhtml = "http://test.openapi.cslearning.cn/authApi/auth/checkAccessToken/" + token;
            Byte[] pageData2 = MyWebClient.DownloadData(refreshhtml);
            string json = Encoding.UTF8.GetString(pageData2);
            TokenJsonEntity tje = JsonConvert.DeserializeObject<TokenJsonEntity>(json);
            Model_zk_ksSession ks = new Model_zk_ksSession();
            ks = System.Web.HttpContext.Current.Session["kaosheng"] as Model_zk_ksSession;
            int expires_in = 0;
            bool ksFlag = false;
            //空值验证，否则会报错
            if(ks==null)
            {
                ksFlag = false;
            }
            else
            {
                ksFlag = ks.Flag;
            }
            //空值验证，否则会报错
            if(tje.responseEntity==null)
            {
                expires_in = 0;
            }
            else
            {
                expires_in = tje.responseEntity.expires_in;
            }
            if(!(expires_in>0))
            {
                ksFlag = false;
            }
            if(!ksFlag)
            {
                LogoutSSO();
            }
            base.OnLoad(e);
        }

        //退出，进行单点登录 wf 2018-08-16 add
        private static void LogoutSSO()
        {
            SqlDbHelper_1 helper = new SqlDbHelper_1();
            string error = "";
            bool bReturn = false;
            string logoutsql = "select redirect_website from Sys_SSOAddress where redirect_code='logout'";
            string address = helper.ExecuteScalar(logoutsql, ref error, ref bReturn).ToString();
            System.Web.HttpContext.Current.Response.Write("<script>window.location='http://test.openapi.cslearning.cn/authApi/auth/logout?redirect_uri=" + address + "';</script>");
        }
        //end
    }
}
