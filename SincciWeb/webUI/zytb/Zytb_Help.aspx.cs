using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using Model;
using BLL.system; 
namespace SincciKC.webUI.zytb
{
    public partial class Zytb_Help : SPage //wf 2018-08-16 修改，单点登录统一控制 System.Web.UI.Page
    {

        public Model_zk_kscj model = new Model_zk_kscj();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["kaosheng"] != null)
                {
                    if (BLL_Ks_Session.ksLogCheck())
                    {
                        //this.lblSysYear.Text =   BLL_Ks_Session.ksSession().kaoci;
                        string ksh = BLL_Ks_Session.ksSession().ksh;
                        string xqdm = BLL_Ks_Session.ksSession().Bmdxqdm;
                        //if (!new Bll_zkbm_Time().zkbm_time(xqdm, 2))
                        //{
                        //    Response.Write("<script>alert('现在不是网上志愿填报时间！'); history.back(); </script>");
                        //}
                        //else
                        //{


                        //  int RecordCount = 0;
                        //    DataTable dt = new BLL_news().DataTableArticle(65, 1, 1, ref  RecordCount);
                        DataTable dtcj = new BLL_zk_kszkcj().zk_kshkcj(ksh);
                        repDisplay.DataSource = dtcj;
                        repDisplay.DataBind();
                        //if (RecordCount > 0)
                        //{
                        //    this.Content.InnerHtml = dt.Rows[0]["content"].ToString();

                        //    // ShowArticle(59, 1, 16).ToString();
                        //}
                        //  }

                    }
                    else
                    {
                        Response.Write("<script>alert('请先登录！'); history.back(); </script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('请先登录！'); history.back(); </script>");
                }
            }
        }



        /// <summary>
        /// 下一页
        /// </summary> 
        protected void btnNext_Click(object sender, EventArgs e)
        {
           // Response.Redirect("Zytb_zsbf.aspx", false);
            Response.Redirect("tbzy.aspx", false);
        }

        ///// <summary>
        ///// 退出系统
        ///// </summary> 
        //protected void btnExit_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("/webUI/Exit.aspx", false);
        //}


    }
}