using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using BLL;
using Model;


namespace SincciWeb.system
{
    public partial class Admin_center : BPage
    {

        public StringBuilder sb_Menu = new StringBuilder();
        public string UserName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            // new config().Timeout();

            if (!IsPostBack)
            {
                BinData();
                UserName = SincciLogin.Sessionstu().UserName;

            }
        }

        #region "绑定菜单"
        private void BinData()
        {
            //QueryParam qp = new QueryParam();
            //qp.Order = " order by m_order asc ";
            //qp.Where = " M_tag=1 and A_roleid=" + SincciLogin.Sessionstu().UserType;
            //int RecordCount = 0;
            DataTable dt = new Method().SelectModuleList(SincciLogin.Sessionstu().UserType);  //查询模块
            sb_Menu.Append(" <ul id=\"tt1\" class=\"easyui-tree\" animate=\"false\" dnd=\"false\">");

            if (dt.Rows.Count > 0)
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataTable dt2 = new Method().GetPermissionApplSub(int.Parse(dt.Rows[i]["Moduleid"].ToString()));  //查询应用
                    if (dt2.Rows.Count > 0)
                    {
                        sb_Menu.Append("    <li state=\"closed\" >");
                        sb_Menu.Append("        <span>" + dt.Rows[i]["M_modulename"].ToString() + "</span>");
                        sb_Menu.Append("        <ul>");
                        for (int j = 0; j < dt2.Rows.Count; j++)
                        {
                            sb_Menu.Append("            <li><span>");
                            if (dt2.Rows[j]["A_picurl"].ToString().Length > 0)
                            {
                                sb_Menu.Append("<img src='" + dt2.Rows[j]["A_picurl"].ToString() + "' />");
                            }
                            else
                            {
                                sb_Menu.Append("<img src='/images/sysimg/pic25.gif' />");
                            }
                            sb_Menu.AppendFormat(" <a onClick=\"javascript:addTab('{0}','{1}');\">{0}</a></span>", dt2.Rows[j]["A_appname"].ToString(), dt2.Rows[j]["A_url"].ToString());
                            sb_Menu.Append("            </li>");
                        }
                        sb_Menu.Append("        </ul>");
                        sb_Menu.Append("    </li> ");
                    }
                }
            sb_Menu.Append(" </ul>"); 
        }
        #endregion

    }
}