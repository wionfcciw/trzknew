using BLL;
using BLL.system;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SincciKC.webUI.zytb
{
    public partial class tbzyPwd : SPage //wf 2018-08-16 修改，单点登录统一控制 System.Web.UI.Page
    {
        public string kshwd = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                Text1.Value = BLL_Ks_Session.ksSession().Pwd;

            }
        }
    }
}