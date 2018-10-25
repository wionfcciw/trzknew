using System;
using System.Collections.Generic;
 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
namespace SincciKC.webUI
{
    public partial class Ks_PwdEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["kaosheng"] != null)
                {
                    if (BLL_Ks_Session.ksSession().ksh.Length > 0)
                    {
                        this.lblksh.Text = BLL_Ks_Session.ksSession().ksh;
                        this.lblxm.Text = BLL_Ks_Session.ksSession().xm;
                    }
                    else
                    {
                        Response.Write("<script>alert('超时请重新登录！'); window.location.href='/' </script>");
                    }
                }
                else
                { 
                   Response.Write("<script>alert('超时请重新登录！'); window.location.href='/' </script>"); 
                }
            }
        }

        /// <summary>
        /// 修改考生密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
             if (Session["kaosheng"] != null)
                {
                    if (BLL_Ks_Session.ksSession().ksh.Length > 0)
                    {
                        string oldpwd = config.CheckChar(this.txtOld.Text.Trim().ToString());
                        string pwd = config.CheckChar(this.txtpwd.Text.Trim().ToString());
                        string pwd1 = config.CheckChar(this.txtpwd1.Text.Trim().ToString());
                        if (oldpwd == pwd)
                        {
                            Response.Write("<script>alert('新密码不能与旧密码相同，请重新输入！');  history.back(); </script>");
                            return;
                        }
                        if (pwd != pwd1)
                        {
                            Response.Write("<script>alert('输入两次密码不相同，请重新输入！');  history.back(); </script>");
                        }
                        else
                        {
                            int i=BLL_Ks_PwdEdit.Ks_PwdEdit(oldpwd,pwd, BLL_Ks_Session.ksSession().ksh);
                            if (i==0)
                            {
                                Model.Model_zk_ksSession ksSession = BLL_Ks_Session.ksSession();
                                ksSession.Flag = true;
                                ksSession.Pwd = pwd1;
                                Session.Add("kaosheng", ksSession);

                                Response.Write("<script>alert('修改密码成功！'); window.location.href='/' </script>");
                            }
                            else if (i == -1)
                            {
                                Response.Write("<script>alert('原密码错，请重新输入！'); history.back(); </script>");
                            }
                            else
                            {
                                Response.Write("<script>alert('修改密码失败！'); history.back();</script>");
                            }

                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('超时请重新登录！'); window.location.href='/' </script>");
                    }
                }
             else
             {
                 Response.Write("<script>alert('超时请重新登录！'); window.location.href='/' </script>");
             }
        }
    }
}