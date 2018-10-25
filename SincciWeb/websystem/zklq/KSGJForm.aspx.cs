using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using System.Text;

namespace SincciKC
{
  
    /// <summary>
    /// 阅档
    /// </summary>
    public partial class KSGJForm : BPage
    {
        /// <summary>
        /// 页面加载。
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int UserType = SincciLogin.Sessionstu().UserType;
                if (UserType != 1)
                {
                    Response.Write("<script language='javascript'>window.parent.location.href='/ht/login.aspx';</script>");
                    return;
                }

                GetData();
            }
        }
        //加载页面默认数据
        private void GetData()
        {
         
            if (Request.QueryString["ksh"] != "")
            {
                BLL_xqlq zk = new BLL_xqlq();
                DataTable tab = new DataTable();

                tab = zk.selectlqgj(Request.QueryString["ksh"].ToString());
                repDisplay.DataSource = tab;
                this.repDisplay.DataBind();
            }
        }
     

      
    }
}