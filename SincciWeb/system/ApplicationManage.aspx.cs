using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BLL;
using Model;


namespace SincciWeb.system
{
    public partial class ApplicationManage : BPage
    {
        #region "Page_Load"
        int pagesize = Convert.ToInt32(config.sink("pagesize", config.MethodType.Get, 255, 1, config.DataType.Int));
        int A_moduleid = Convert.ToInt32(config.sink("A_moduleid", config.MethodType.Get, 255, 1, config.DataType.Int));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            { 
                //页面权限
                Permission();
                //绑定PageSize数据
                this.ddlPageSize.DataSource = new config().PageSizelist();
                this.ddlPageSize.DataBind();
                //绑定模块数据
                Moduleddl();

                //查询条件
                QueryParam qp = new QueryParam();
                //qp.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
                if (pagesize>0)
                {
                    qp.PageSize = pagesize;
                    this.ddlPageSize.SelectedValue = pagesize.ToString();
                }
                else
                {
                    qp.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
                }

                if (A_moduleid>0)
                {
                    qp.Where = " A_moduleid=" + A_moduleid + "";
                    this.ddlModule.SelectedValue = A_moduleid.ToString();
                }

                //绑定数据
                OnStart(qp);
                


            }
        }
        #endregion

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
            //新增
            if (!new Method().CheckButtonPermission(PopedomType.A4))
            {
                this.btnNew.Visible = false;
            }
            //修改
            if (!new Method().CheckButtonPermission(PopedomType.A8))
            {
                GridView1.Columns[12].Visible = false;
            }
            //删除
            if (!new Method().CheckButtonPermission(PopedomType.A16))
            {
                this.btnDelete.Visible = false;
            }
            //设置权限
            if (!new Method().CheckButtonPermission(PopedomType.A32))
            {
                GridView1.Columns[11].Visible = false;
            }
            ////排序
            //if (!new Method().CheckButtonPermission(PopedomType.Orderby))
            //{
            //    this.Enabled = false;
            //}
            ////打印
            //if (!new Method().CheckButtonPermission(PopedomType.Print))
            //{
            //    this.btnPrint.Enabled = false;
            //}
            ////备用A
            //if (!new Method().CheckButtonPermission(PopedomType.A))
            //{
            //    this.btnA.Enabled = false;
            //}
            ////备用B
            //if (!new Method().CheckButtonPermission(PopedomType.B))
            //{
            //    this.btnB.Enabled = false;
            //}
        }
        #endregion

        #region "绑定数据"
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="qp"></param>
        private void OnStart(QueryParam qp)
        {
            qp.Order = " order by A_moduleid asc,A_order asc";
            int RecordCount = 0;
          
            GridView1.DataSource = new Method().sys_applicationsList(qp, out RecordCount); 
            GridView1.DataBind();

            //分页
            config.CustomInfoHTML(AspNetPager1, RecordCount, qp.PageSize);

        } 
        
        /// <summary>
        /// 绑定模块数据
        /// </summary>
        private void Moduleddl()
        {
            QueryParam qp = new QueryParam();
            qp.Order = " order by M_order asc ";
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
            this.ddlModule.Items.Insert(0, new ListItem("-全选择-", ""));

        }
        #endregion      

        #region "删除数据 btnDelete操作"
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            bool flag = false;
            string str = "";
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
                if (cbox.Checked == true)
                {
                    // PKname += this.GridView1.Rows[i].Cells[2].Text + ",";
                    Sys_applicationsTable sat = new Sys_applicationsTable();
                    sat.Applicationid = Convert.ToInt32(this.GridView1.Rows[i].Cells[2].Text);
                    sat.DataTable_Action_ = "Delete";

                    new Method().Sys_applicationsInsertUpdateDelete(sat);
                    flag = true;
                    //if (sat.ApplicationID != 1)
                    //{
                    //    //删除应用表
                    //    BusinessFacade.sys_ApplicationsInsertUpdate(sat);
                    //    //删除模块表
                    //    BusinessFacade.sys_Module_DeleteFormAppID(S_ID);
                    //    //删除角色应用权限表
                    //    BusinessFacade.sys_RolePermission_DeleteFromAppid(S_ID);
                    //    //删除角色应用表
                    //    BusinessFacade.sys_RoleApplication_DeleteFormAppid(S_ID);

                    //}
                    str = str + sat.Applicationid + ",";
                }
            }
            string E_record = "删除:应用管理数据：" + str + "";
            if (flag)
            {
                EventMessage.EventWriteDB(1, E_record);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.succeedInfo({message:'删除成功！' ,title:'操作提示'});setTimeout(function(){ ymPrompt.close();},1000);</script>");

                //操作完绑定数据
                QueryParam qp = new QueryParam();
                qp.PageIndex = AspNetPager1.CurrentPageIndex;
                qp.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
                if (this.ddlModule.SelectedValue.Length > 0)
                {
                    qp.Where = " A_moduleid=" + this.ddlModule.SelectedValue + "";
                }
                OnStart(qp);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请选择信息！' ,title:'提示'});setTimeout(function(){ ymPrompt.close();},1000);</script>");

            }

        }
        #endregion

        #region "PageChanged PageChanged事件"
        /// <summary>
        /// PageChanged 事件
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            QueryParam qp = new QueryParam();
            qp.PageIndex = AspNetPager1.CurrentPageIndex;
            qp.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            if (this.ddlModule.SelectedValue.Length > 0)
            {
                qp.Where = " A_moduleid=" + this.ddlModule.SelectedValue + "";
            }
            OnStart(qp);
        }
        #endregion

        #region "选择 PageSize SelectedIndexChanged事件"
        /// <summary>
        /// 选择 PageSize 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //QueryParam qp = new QueryParam();
            //qp.PageIndex = 1;
            //qp.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            //AspNetPager1.CurrentPageIndex = 1;
            //if (this.ddlModule.SelectedValue.Length > 0)
            //{
            //    qp.Where = " A_moduleid=" + this.ddlModule.SelectedValue + "";
            //}
            //OnStart(qp);

            Response.Redirect(config.GetScriptName + "?page=1&pagesize=" + this.ddlPageSize.SelectedValue.ToString() + "");

        }
        #endregion

        #region "全选/全不选 CheckedChanged事件"
        /// <summary>
        /// 全选/全不选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckbFull_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
                if (this.ckbFull.Checked)
                {
                    cbox.Checked = true;
                }
                else
                {
                    cbox.Checked = false;
                }
            }
        }
        #endregion

        #region "排序 RowCommand事件"
        /// <summary>
        /// 排序 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            Sys_applicationsTable sat = new Sys_applicationsTable();
            sat.Applicationid = Convert.ToInt32(e.CommandArgument.ToString());
            sat.DataTable_Action_ = "Order";
            if (e.CommandName.Equals("Up"))
            {
                sat.OrderType = 1;
            }
            if (e.CommandName.Equals("Down"))
            {
                sat.OrderType = 2;
            }

          
            new Method().Sys_applicationsInsertUpdateDelete(sat);

            //操作完绑定数据
            QueryParam qp = new QueryParam();
            qp.PageIndex = AspNetPager1.CurrentPageIndex;
            qp.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            if (this.ddlModule.SelectedValue.Length > 0)
            {
                qp.Where = " A_moduleid=" + this.ddlModule.SelectedValue + "";
            }
            OnStart(qp);
        }
        #endregion

        #region "字段转换 RowDataBound事件"
        /// <summary>
        /// 字段转换 RowDataBound事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (e.Row.Cells[10].Text.Equals("0"))
                {
                    e.Row.Cells[10].Text = "关闭";
                    e.Row.Cells[10].ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    e.Row.Cells[10].Text = "开通";
                }
            }
            ////修改权限
            //if (!new Method().CheckButtonPermission(PopedomType.Edit))
            //{
            //    e.Row.Cells[11].Visible = false;
            //}
        }
        #endregion

        #region "选择模块列表 ddlModule_SelectedIndexChanged事件"
        /// <summary>
        /// 选择模块列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            //QueryParam qp = new QueryParam();
            //qp.PageIndex = 1;
            //AspNetPager1.CurrentPageIndex = 1;
            //qp.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            //if (this.ddlModule.SelectedValue.Length > 0)
            //{
            //    qp.Where = " A_moduleid=" + this.ddlModule.SelectedValue + "";
            //}
            //OnStart(qp);
            if (this.ddlModule.SelectedValue.Length > 0)
            {
                Response.Redirect(config.GetScriptName + "?page=1&pagesize=" + this.ddlPageSize.SelectedValue.ToString() + "&A_moduleid=" + this.ddlModule.SelectedValue + "");
            }
            else
            {
                Response.Redirect(config.GetScriptName + "?page=1&pagesize=" + this.ddlPageSize.SelectedValue.ToString() + "");
            }
        }
        #endregion

    }
}