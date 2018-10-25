using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Model;
using BLL;

namespace SincciKC.Controls
{
    public partial class MenuControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.MenuContent.InnerHtml = MenuList();
            }
        }

        private string MenuList()
        {
            string content = "";

            QueryParam qp = new QueryParam();
            qp.Order = "order by M_Order ";
            qp.Where = " M_Tag=1 ";
            int RecordCount = 0;
            DataTable dt = new Method().Sys_MenuList(qp, out RecordCount);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    if (i == 0 && config.GetScriptName.ToLower() == "/default.aspx")
                    {
                        content += "<li  class=\"selected\"><a href='" + dt.Rows[i]["M_Url"].ToString() + "'>" + dt.Rows[i]["M_Name"].ToString() + "</a></li>";
                    }
                    else
                    {
                        content += "<li " + CssUrl(dt.Rows[i]["M_Url"].ToString()) + "><a href='" + dt.Rows[i]["M_Url"].ToString() + "'>" + dt.Rows[i]["M_Name"].ToString() + "</a></li>";
                    }
                }
            }
            return content;

        }
        private string CssUrl(string url)
        {
            string strurl = config.GetScriptName;


            if (config.GetScriptNameQueryString.Length > 0)
                strurl = strurl + "?" + config.GetScriptNameQueryString;

            if (strurl == url)
            {
                return "class=\"selected\"";
            }
            else
            {
                return "";
            }
        }
    }
}