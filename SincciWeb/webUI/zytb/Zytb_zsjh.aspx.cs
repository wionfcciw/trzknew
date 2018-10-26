using BLL.system;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SincciKC.webUI.zytb
{
    public partial class Zytb_zsjh : SPage //wf 2018-08-16 修改，单点登录统一控制 System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnter_Click(object sender, EventArgs e)
        {
            Response.Redirect("Zytb_zsbf.aspx", false);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>opdg('1', '提前批高师计划') ;</script>");
            
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>opdg('3', '第二批高职计划') ;</script>");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>opdg('4', '第三批中职1计划') ;</script>");
    
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>opdg('5', '第三批中职2计划') ;</script>");
    
        }
    }
}