using System;
using System.Collections.Generic;
 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BLL;
using Model;

namespace SincciWeb.system
{
    public partial class RoleAddEdit : BPage
    { 
        #region "Page_Load"
        int Roleid = Convert.ToInt32(config.sink("Roleid", config.MethodType.Get, 255, 1, config.DataType.Int));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Permission();
                if (Roleid > 0)
                {
                    binData(Roleid);
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
        private void binData(int Roleid)
        {
            Sys_rolesTable mt = new Sys_rolesTable();
            mt = new Method().Sys_rolesDisp(Roleid);
            this.txtR_name.Text = mt.R_name;             
            this.txtR_descript.Text = mt.R_descript.ToString();
            this.lblRoleid.Text = mt.Roleid.ToString();
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
            Sys_rolesTable sat = new Sys_rolesTable();
            if (Convert.ToInt32(this.lblRoleid.Text.Trim()) > 0)
            {                
                sat.Roleid = Convert.ToInt32(this.lblRoleid.Text.Trim());
                sat.DataTable_Action_ = "Update";
            }
            else
            {
                sat.DataTable_Action_ = "Insert";
            }
            sat.R_name =config.CheckChar(this.txtR_name.Text.Trim());
            sat.R_descript = config.CheckChar(this.txtR_descript.Text.Trim());


            int fa = new Method().Sys_rolesInsertUpdateDelete(sat);
            string E_record = "修改: 角色管理数据：" + sat.Roleid + "";
            if (fa > 0)
            {
                EventMessage.EventWriteDB(1, E_record);
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