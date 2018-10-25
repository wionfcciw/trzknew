using DAL;
using Newtonsoft.Json;
using SincciKC.SsoLogin.TokenJsonEntityPack.SSOModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;


namespace BLL
{
    /// <summary>
    ///SysPage（基类）
    /// </summary>
    public partial class BPage : System.Web.UI.Page
    { 
        /// <summary>
        /// session超时处理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            //wangf 2018-08-16 新增访问控制，集成于平台，与平台的token保持一致，故每次都访问
            WebClient MyWebClient = new WebClient();
            string token = Session["token"].ToString();
            string refreshhtml = "http://openapi.tredu.gov.cn/authApi/auth/checkAccessToken/" + token;
            Byte[] pageData2 = MyWebClient.DownloadData(refreshhtml);
            string json = Encoding.UTF8.GetString(pageData2);
            TokenJsonEntity tje = JsonConvert.DeserializeObject<TokenJsonEntity>(json);
            if(!(tje.responseEntity.expires_in>0))
            {
                SincciLogin.Sessionstu().Flag = false;
            }
            //end
            if (!SincciLogin.Sessionstu().Flag)
            {
                //wangf 2018-08-16 修改原来的方式，不只是子系统注销，整个系统的密钥都进行销毁。
                //Response.Write("<script language='javascript'>window.parent.location.href='/ht/Loginzxc.aspx'3</script>");
                LogoutSSO();
            }
            base.OnLoad(e); 
        }

        //退出，进行单点登录 wf 2018-08-16 add
        private static void LogoutSSO()
        {
            string addresstitle = "http://" + HttpContext.Current.Request.Url.Authority;
            SqlDbHelper_1 helper = new SqlDbHelper_1();
            string error = "";
            bool bReturn = false;
            string logoutsql = "select redirect_website from Sys_SSOAddress where redirect_code='logout'";
            string address = helper.ExecuteScalar(logoutsql, ref error, ref bReturn).ToString();
            address = addresstitle + address;
            System.Web.HttpContext.Current.Response.Write("<script>window.location='http://openapi.tredu.gov.cn/authApi/auth/logout?redirect_uri=" + address + "';</script>");
        }
        //end
    }

}
