using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SincciKC.websystem.zklq
{
    public partial class Zhidingtoudang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadPcInfo();
            }
        }
        /// <summary>
        /// 加载批次信息。
        /// </summary>
        private void loadPcInfo()
        {
            BLL_zk_Pctd_tj_Info zk = new BLL_zk_Pctd_tj_Info();
            DataTable tab = zk.selectPcdm();
            this.ddlXpcInfo.DataSource = tab;
            this.ddlXpcInfo.DataTextField = "xpc_mc";
            this.ddlXpcInfo.DataValueField = "xpc_id";
            this.ddlXpcInfo.DataBind();

          
        }
    }
}