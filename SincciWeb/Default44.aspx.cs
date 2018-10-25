using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL; 
namespace SincciKC
{
    public partial class Default2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

               
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
            txtpwd.Attributes["value"] = txtpwd.Text;
        }

        private void newsData()
        {
            ////考试动态
            //this.gonggao.InnerHtml = new BLL_news().ShowArticle(50, 9, 16).ToString();

            //this.zhengce.InnerHtml = new BLL_news().ShowArticle(51, 9, 16).ToString();

            //this.xuanchuan.InnerHtml = new BLL_news().ShowArticle(52, 9, 16).ToString();

            //this.wenti.InnerHtml = new BLL_news().ShowArticle(54, 9, 16).ToString();
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            string ksh = config.CheckChar(txtksh.Text.Trim());
            string xm = config.CheckChar(txtxm.Text.Trim());
            string pwd = config.CheckChar(txtpwd.Text.Trim());
            string yzm = config.CheckChar(txtcheck.Text.Trim());
            if (ksh.Length == 0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.errorInfo({message:'请输入报名号！' ,title:'提示'});</script>");
            }
            if (xm.Length==0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.errorInfo({message:'请输入姓名！' ,title:'提示'});</script>");
            }
            if (pwd.Length == 0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.errorInfo({message:'请输入密码！' ,title:'提示'});</script>");
            }
            if (yzm.Length == 0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.errorInfo({message:'请输入验证码！' ,title:'提示'});</script>");
            }
            if (yzm == Session["random"].ToString())
            {
                BLL_Ks_Login ks = new BLL_Ks_Login();
                int sa = ks.KS_Login(ksh, xm, pwd);

                if (sa == 1)
                {
                  //  Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.errorInfo({message:'登录成功！' ,title:'提示'});</script>");
                  
                    if (ksh == pwd)
                    {
                        Response.Redirect("/webUI/Ks_PwdEdit.aspx");
                    }
                    else
                    {
                        Response.Redirect("/");
                    }

                }
                else
                {
                    if (sa == 0)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.errorInfo({message:'报名号有误！' ,title:'提示'});</script>");
                    }
                    else if (sa == -1)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.errorInfo({message:'姓名有误！' ,title:'提示'});</script>");
                    }
                    else if (sa == -2)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.errorInfo({message:'密码有误！' ,title:'提示'});</script>");
                    }
                    txtcheck.Text = "";
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'验证码不正确！' ,title:'错误提示'});</script>");
         
            }

        }
    }
}