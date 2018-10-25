using System;
using System.Collections.Generic;
 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BLL;
using Model;


namespace SincciWeb.system
{
    public partial class ModuleAddEdit : BPage
    {
        #region "Page_Load"
        int Moduleid = Convert.ToInt32(config.sink("Moduleid", config.MethodType.Get, 255, 1, config.DataType.Int));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Permission();
                if (Moduleid > 0)
                {
                    binData(Moduleid);
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
        private void binData(int Moduleid)
        { 
            Sys_moduleTable mt = new Sys_moduleTable();
            mt = new Method().sys_ModuleDisp(Moduleid);
            this.txtModuleName.Text = mt.M_modulename;
            this.ddlTag.SelectedValue = mt.M_tag;
            this.lblM_order.Text = mt.M_order.ToString(); 
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
            Sys_moduleTable sat = new Sys_moduleTable();
            if (Moduleid > 0)
            {
                sat.M_order = Convert.ToInt32(this.lblM_order.Text.Trim());
                sat.Moduleid = Moduleid;
                sat.DataTable_Action_ = "Update";
            }
            else
            {
                sat.DataTable_Action_ = "Insert";
            }
            sat.M_modulename = this.txtModuleName.Text.Trim();
            sat.M_tag = this.ddlTag.SelectedValue;
            int fa = new Method().sys_ModuleInsertUpdate(sat);
            string E_record = "修改: 模块管理数据：" + sat.Moduleid + "";
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