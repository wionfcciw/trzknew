using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Model;
using BLL;
using System.Data;
using DAL;
using System.Net;
namespace SincciWeb.ht
{
    public partial class Loginzxc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckUserSSOLogin();
            }
            //if (!Page.IsPostBack)
            //{
            //    txtUserName.Focus();

            //    //  new OnlineDataBase().ClearOnlineUserTimeOut();


            //}
        }


        private  void CheckUserSSOLogin()
        {
            if(SincciLogin.Sessionstu().UserName==null)
            {
                string html ="http://"+HttpContext.Current.Request.Url.Authority;
                SqlDbHelper_1 helper = new SqlDbHelper_1();
                string error = "";
                bool bReturn = false;
                string sql = "select redirect_website from Sys_SSOAddress where redirect_code='getcode' ";
                //重定向地址,进行认证，认证结束后，仍然需要返回一个特征标志，用来认证是否成功。然后才能获取session。
                //session中需要加上token的值以及token的时间
                string address=helper.ExecuteScalar(sql,ref error,ref bReturn).ToString();
                address = html + address;
                //为了验证而产生的一个session
                Response.Redirect("http://openapi.tredu.gov.cn/authApi/auth/authorize?client_id=1d98bbaa-0507-49f4-a3dc-ddd51f479d86&response_type=code&redirect_uri=" + address);
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {


            string UserName = config.CheckChar(this.txtUserName.Text.Trim());  //用户名
            string Pwd = config.CheckChar(this.txtPassword.Text.Trim());        //用户密码
            string Random = this.txtcheck.Text.Trim();       //验证码     
            int UserType = 0;
            if (Session["random"] != null)
            {


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
                        //登录成功  

                        //if (HttpContext.Current.Request["ReturnUrl"] != null)
                        //{
                        //    HttpContext.Current.Response.Redirect(HttpContext.Current.Request["ReturnUrl"]); // 重定向到用户申请的初始页面
                        //}
                        //else
                        //{
                        if (Pwd == "123456")
                        {
                            Response.Redirect("/system/UserPwdEdit.aspx?type=1", false);
                        }
                        else
                            Response.Redirect("/system/Admin_center.aspx", false);
                        // }

                        //  Response.Write("<script language='javascript'>window.parent.location.href='/system/Admin_center.aspx';</script>"); 

                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'验证码不正确！' ,title:'错误提示'});</script>");
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


  