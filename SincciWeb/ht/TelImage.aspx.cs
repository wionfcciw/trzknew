using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Drawing.Drawing2D;
using System.Drawing;
using BLL;
using Model;
namespace SincciWeb.ht
{
    public partial class TelImage : System.Web.UI.Page
    {
        public string username = config.sink("userName", config.MethodType.Get, 255, 1, config.DataType.Str).ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (username.Length > 0)
                {
                    GetValidate();
                    this.SetValidate();
                }
            }
        }
        private string GetValidate()
        {
            string strRanNum = null;
            string[] strNum = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            Random rd = new Random();
            for (int i = 0; i < 6; i++)
            {
                strRanNum += strNum[rd.Next(strNum.Length)];
            }
            Session.Add("asd", strRanNum);
            return strRanNum;
        }

        string strRandom;
        public void SetValidate()
        {
            strRandom = this.GetValidate();

            string name = "中招录取系统动态登录口令:" + strRandom + "，此口令10分钟内有效,请尽快登录![铜仁中考]";
            name = System.Web.HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8);
            QueryParam qp = new QueryParam();
            qp.Where = " U_loginname='" + username + "'";
            int RecordCount = 0;
            DataTable dt = new UserData().UserArrayList(qp, out RecordCount);
            if (dt.Rows.Count > 0)
            {
                string destmobile = dt.Rows[0]["U_phone"].ToString();
                if (destmobile.Length > 0)
                {
                    Session["TelRandom"] = strRandom;//保存验证码
                    Session["TelRandomTime"] = DateTime.Now;//保存验证码
                 Response.Redirect("http://sms2.sincci.net/receive.aspx?destmobile=" + destmobile + "&comefrom=sincci&sendmsg=" + name + "&Rurl=http://www.sincci.net&sincci=2327e1c0-3f0e-473a-9e0a-19d2ac283cd8");
                  //  Response.Redirect("http://sms3.sincci.net/receive.aspx?destmobile=" + destmobile + "&comefrom=sincci&sendmsg=" + name + "&Rurl=http://www.sincci.net&sincci=2327e1c0-3f0e-473a-9e0a-19d2ac283cd8");
                   
                  //  Response.Redirect("http://61.143.153.101:805/receive.aspx?destmobile=" + destmobile + "&comefrom=sincci&sendmsg=" + name + "&Rurl=http://www.sincci.net&sincci=2327e1c0-3f0e-473a-9e0a-19d2ac283cd8");
           
                }
            }
        }


    }
}
