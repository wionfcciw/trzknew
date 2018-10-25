using System;
using System.Collections.Generic;
 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BLL;
using Model;
namespace SincciWeb.system
{
    public partial class UserTypeAddEdit : BPage
    {

        #region "Page_Load"
        int TypeID = Convert.ToInt32(config.sink("TypeID", config.MethodType.Get, 255, 1, config.DataType.Int));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Permission();

                if (TypeID > 0)
                {
                    binData(TypeID);
                }

            }
        }
        #endregion

        #region "判断页面权限"
        /// <summary>
        /// 页面权限
        /// </summary>
        private void Permission()
        {
            ////查看
            //if (!new Method().CheckButtonPermission(PopedomType.List))
            //{
            //    Response.Write("你没有页面查看的权限！");
            //    Response.End();
            //}
        }
        #endregion

        #region "绑定修改数据"
        /// <summary>
        /// 绑定修改数据
        /// </summary>
        /// <param name="Moduleid"></param>
        private void binData(int TypeID)
        {
            Sys_UserTypeTable mt = new Sys_UserTypeTable();
            mt = new Method().Sys_UserTypeDisp(TypeID);
            this.txtT_Name.Text = mt.T_Name;
           
        }
        #endregion

        #region "保存数据"
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Sys_UserTypeTable sat = new Sys_UserTypeTable();
            if (TypeID > 0)
            {
                sat.TypeID = TypeID;
                sat.DataTable_Action_ = "Update";
            }
            else
            {
                sat.DataTable_Action_ = "Insert";
            }
            sat.T_Name = this.txtT_Name.Text.Trim();
            int fa = new Method().Sys_UserTypeInsertUpdateDelete(sat);
            if (fa > 0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.succeedInfo({message:'操作成功！' ,title:'操作提示'});setTimeout(function(){window.parent.location.href=window.parent.location.href;window.parent.ymPrompt.close();},1000);</script>");

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'操作失败！' ,title:'提示'});</script>");
            }

        }
        #endregion

    }
}