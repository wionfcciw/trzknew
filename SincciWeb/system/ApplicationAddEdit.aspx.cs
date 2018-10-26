using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using BLL;
using Model;


namespace SincciWeb.system
{
    public partial class ApplicationAddEdit : BPage
    { 
        #region "Page_Load"
        int Applicationid = Convert.ToInt32(config.sink("Applicationid", config.MethodType.Get, 255, 1, config.DataType.Int));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Permission();

                if (Applicationid > 0)
                {
                    binData(Applicationid);
                }
                QueryParam qp = new QueryParam();
                Moduleddl(qp);
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
        private void binData(int Applicationid)
        {
            Sys_applicationsTable mt = new Sys_applicationsTable();
            mt = new Method().Sys_applicationsDisp(Applicationid);          
            
            this.lblA_order.Text = mt.A_order.ToString();
            this.lblApplicationid.Text = mt.Applicationid.ToString();

            this.txtA_appname.Text = mt.A_appname;
            this.txtA_pagecode.Text = mt.A_pagecode;
            this.txtA_picurl.Text = mt.A_picurl;
            this.txtA_url.Text = mt.A_url;

            this.ddlTag.SelectedValue = mt.A_tag;
            this.ddlModule.SelectedValue = mt.A_moduleid.ToString();
            
        }
        /// <summary>
        /// 绑定模块
        /// </summary>
        /// <param name="qp"></param>
        private void Moduleddl(QueryParam qp)
        {
            qp.Order = " order by M_order asc";
            qp.OrderId = " Moduleid ";
            qp.PageIndex = 1;
            qp.PageSize = int.MaxValue;
            qp.ReturnFields = " Moduleid,M_modulename ";
            qp.Where = " M_tag=1 ";
            int RecordCount = 0;
            this.ddlModule.DataSource = new Method().sys_ModuleList(qp, out RecordCount);
            this.ddlModule.DataTextField = "M_modulename";
            this.ddlModule.DataValueField = "Moduleid";
            this.ddlModule.DataBind();
           // this.ddlModule.Items.Insert(0, new ListItem("请选择", ""));

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
            Sys_applicationsTable sat = new Sys_applicationsTable();
            if (Convert.ToInt32(this.lblApplicationid.Text.Trim()) > 0)
            {
                sat.A_order = Convert.ToInt32(this.lblA_order.Text.Trim());
                sat.Applicationid = Convert.ToInt32(this.lblApplicationid.Text.Trim());
                sat.DataTable_Action_ = "Update";
            }
            else
            {
                sat.DataTable_Action_ = "Insert";
            }
            sat.A_appname = this.txtA_appname.Text.Trim();
            sat.A_pagecode = this.txtA_pagecode.Text.Trim();
            sat.A_picurl = this.txtA_picurl.Text.Trim();
            sat.A_url = this.txtA_url.Text.Trim();

            sat.A_tag = this.ddlTag.SelectedValue;
            sat.A_moduleid = Convert.ToInt32(this.ddlModule.SelectedValue);

            int fa = new Method().Sys_applicationsInsertUpdateDelete(sat);
            string E_record = "修改: 应用管理数据：" + sat.Applicationid + "";
          
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