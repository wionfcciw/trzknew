using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;

namespace SincciKC.websystem.bmsz
{
    public partial class Zdybz : BPage
    {
        private BLL_zk_szzdybz bll = new BLL_zk_szzdybz();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Permission();
                BindGv();
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
           
            //修改
            if (!new Method().CheckButtonPermission(PopedomType.A4))
            {
                this.btnEdit.Visible = false;
            }
          

        }
        #endregion
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGv()
        {
           int UserType = SincciLogin.Sessionstu().UserType;
           string Department = SincciLogin.Sessionstu().U_department;
           Repeater1.DataSource = bll.SelectXqdm(Department, UserType);
            Repeater1.DataBind();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (Request.Form["CheckBox1"] != null)
            {
                string[] ids = Request.Form["CheckBox1"].ToString().Trim().Split(',');

                if (ids.Length == 1)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>opdg('" + ids[0].Split('|')[0] + "','" + ids[0].Split('|')[1] + "','修改数据') ;</script>");
                }
                else if (ids.Length > 1)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('不能对多个县区进行修改!');</script>");

                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择需要修改的县区!');</script>");

            }
        }
    }
}