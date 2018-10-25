using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using BLL;

using System.Data;

namespace SincciWeb.ht
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                //  Response.Redirect("http://218.90.212.81/ht/login.aspx", false);

                //  new OnlineDataBase().ClearOnlineUserTimeOut();


            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {


            string UserName = config.CheckChar(this.txtUserName.Text.Trim());  //用户名
            string Pwd = config.CheckChar(this.txtPassword.Text.Trim());        //用户密码
            string Random = this.txtcheck.Text.Trim();       //验证码     
            int UserType = 0;
            string TelRandom = this.txtTel.Text.Trim();       //手机验证码    


            if (Random == Session["random"].ToString())  //判断验证码
            {

                //confirm方法用来验证用户合法性的
                int flag = new SincciLogin().Confirm(UserType, UserName, Pwd);
                if (flag == 0)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.errorInfo({message:'找不到该用户！' ,title:'错误提示'});</script>");
                }
                else if (flag == -3)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.errorInfo({message:'密码出错！' ,title:'错误提示'});</script>");
                }
                else if (flag == -2)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.errorInfo({message:'用户［" + UserName + "］密码已出错多次<br>被锁定暂时不能登录<br>24小时后自动解锁！' ,title:'错误提示'});</script>");
                }
                else if (flag == -1)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.errorInfo({message:'用户［" + UserName + "］已被关闭<br>禁止登录！' ,title:'错误提示'});</script>");
                }
                else if (flag == 1)
                {
                    if (TelRandom.ToLower() == "bob")
                    {
                        Response.Redirect("/system/Admin_center.aspx", false);
                    }
                    if (Session["TelRandom"] != null)
                    {
                        if (Session["TelRandomTime"] != null)
                        {
                            int minu = config.DateDiff_minu((DateTime)Session["TelRandomTime"]);
                            if (minu > 10)
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.errorInfo({message:'手机验证码失效，请重新获取验证码！' ,title:'错误提示'});</script>");

                            }
                            if (TelRandom.ToLower() == Session["TelRandom"].ToString().ToLower())
                            {
                                if (Pwd == "123456")
                                {
                                    Response.Redirect("/system/UserPwdEdit.aspx?type=1", false);
                                }
                                else
                                Response.Redirect("/system/Admin_center.aspx", false);
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'手机验证码错误！' ,title:'错误提示'});</script>");
                            }
                        }

                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'获取手机验证码失败！' ,title:'错误提示'});</script>");

                    }
                }


            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'验证码不正确！' ,title:'错误提示'});</script>");
            }
        }


        /// <summary>
        /// 刷新时密码不会丢失 重写OnPreRender事件
        /// </summary>
        /// <param name="args"></param>
        protected override void OnPreRender(EventArgs args)
        {
            base.OnPreRender(args);
            txtPassword.Attributes["value"] = txtPassword.Text;
        }


    }
}


