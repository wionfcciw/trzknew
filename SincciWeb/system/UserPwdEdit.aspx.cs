using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using BLL;
using Model;
namespace SincciWeb.system
{
    public partial class UserPwdEdit : BPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Permission();
            }

        }

        #region "判断页面权限"
        /// <summary>
        /// 页面权限
        /// </summary>
        private void Permission()
        {
            //查看
            if (!new Method().CheckButtonPermission(PopedomType.A2))
            {
                Response.Write("你没有页面查看的权限！");
                Response.End();
            }
        }
        #endregion

        /// <summary>
        /// 保存数据
        /// </summary> 
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string oldpwd = config.CheckChar(this.txtOldpwd.Text.Trim().ToString());
            string pwd = config.CheckChar(this.txtU_password.Text.Trim().ToString());
            string pwd2 = config.CheckChar(this.txtU_password2.Text.Trim().ToString());
            if (oldpwd == pwd)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'新密码不能与旧密码相同！' ,title:'提示'});</script>");
                return;
            }
            if (pwd != pwd2)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'新密码与确认密码不相同！' ,title:'提示'});</script>");
                return;
            }

            if (oldpwd == new Method().sys_UserDisp(config.Get_UserName).U_Password)
            {

                //登录成功记录ip和时间
                int i = new Method().Update_Table_Fileds("Sys_users",
                       string.Format(" U_password='{0}' ", pwd),
                       string.Format(" U_loginname='{0}'", config.Get_UserName));
                if (i >= 0)
                {
                    string E_record = "用户" + config.Get_UserName + " 修改密码！";
                    EventMessage.EventWriteDB(1, E_record);

                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'操作成功！' ,title:'提示'});</script>");
                    if (Request.QueryString["type"] == "1")
                        Response.Redirect("/system/Admin_center.aspx", false);
                }
                else
                {

                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'操作失败！' ,title:'提示'});</script>");
                }
            }
            else
            {

                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'旧密码有误，请重新输入！' ,title:'提示'});</script>");
            }
        }

        /// <summary>
        /// 刷新时密码不会丢失 重写OnPreRender事件
        /// </summary>
        /// <param name="args"></param>
        protected override void OnPreRender(EventArgs args)
        {
            base.OnPreRender(args);
            this.txtOldpwd.Attributes["value"] = txtOldpwd.Text;
            this.txtU_password.Attributes["value"] = txtU_password.Text;
            this.txtU_password2.Attributes["value"] = txtU_password2.Text;
        }
    }
}