using BLL;
using BLL.system;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SincciKC.webUI
{
    public partial class Defalt : SPage //wf 2018-08-16 修改，单点登录统一控制 System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int RecordCount = 0;
                DataTable dt = new BLL_news().DataTableArticle(67, 1, 1, ref  RecordCount);

                if (RecordCount > 0)
                {
                    this.Content1.InnerHtml = dt.Rows[0]["content"].ToString();
                }

            }
        }
    }
}