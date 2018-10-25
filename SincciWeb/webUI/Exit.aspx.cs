using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BLL.system; 
namespace SincciKC.webUI
{
    public partial class Exit : SPage //wf 2018-08-16 修改，单点登录统一控制 System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //退出系统
                BLL_Ks_Session.Logout_Member();
            }
        }
    }
}