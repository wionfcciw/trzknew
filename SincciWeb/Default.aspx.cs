using System;
using System.Collections.Generic;
 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Configuration;
using BLL.system; 
namespace SincciKC
{
    public partial class Default : System.Web.UI.Page //wf 2018-08-16 修改，单点登录统一控制 System.Web.UI.Page
    {
        private string dltype = ConfigurationManager.AppSettings["dltype"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblname.Text = dltype.ToString() == "0" ? "报名号" : "准考证号";
               
              
                if (Session["kaosheng"] != null)
                {
                    if (BLL_Ks_Session.ksLogCheck())
                    {
                        this.Panel1.Visible = false;
                        this.Panel2.Visible = true;
                        this.lblksh.Text = BLL_Ks_Session.ksSession().ksh;
                        this.lblxm.Text = BLL_Ks_Session.ksSession().xm;
                    }
                    else
                    {
                        this.Panel1.Visible = true;
                        this.Panel2.Visible = false;
                    }
                }
                else
                {
                    this.Panel1.Visible = true;
                    this.Panel2.Visible = false;
                }
                // newsData();
            }
        }

        private void newsData()
        {
            ////考试动态
            //this.gonggao.InnerHtml = new BLL_news().ShowArticle(50, 9, 16).ToString();

            //this.zhengce.InnerHtml = new BLL_news().ShowArticle(51, 9, 16).ToString();

            //this.xuanchuan.InnerHtml = new BLL_news().ShowArticle(52, 9, 16).ToString();

            //this.wenti.InnerHtml = new BLL_news().ShowArticle(54, 9, 16).ToString();
        }
    }
}