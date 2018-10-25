using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
 

namespace SincciWeb.websystem.News
{
    public partial class NewsTypeManage : BPage
    {
        #region "Page_Load"
        int pagesize = Convert.ToInt32(config.sink("pagesize", config.MethodType.Get, 255, 1, config.DataType.Int));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Permission();

                //绑定PageSize数据
                this.ddlPageSize.DataSource = new config().PageSizelist();
                this.ddlPageSize.DataBind();

                //初始化PageSize
                QueryParam qp = new QueryParam();

                if (pagesize > 0)
                {
                    // qp.PageSize = pagesize;
                    this.ddlPageSize.SelectedValue = pagesize.ToString();
                }
                //else
                //{
                //    qp.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
                //}

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
        
            if (!new Method().CheckButtonPermission(PopedomType.A8))
            {
                this.btnDelete.Visible = false;
            }
            //////排序
            ////if (!new Method().CheckButtonPermission(PopedomType.Orderby))
            ////{
            ////    this.Enabled = false;
            ////}
            //////打印
            ////if (!new Method().CheckButtonPermission(PopedomType.Print))
            ////{
            ////    this.btnPrint.Enabled = false;
            ////}
            //////备用A
            ////if (!new Method().CheckButtonPermission(PopedomType.A))
            ////{
            ////    this.btnA.Enabled = false;
            ////}
            //////备用B
            ////if (!new Method().CheckButtonPermission(PopedomType.B))
            ////{
            ////    this.btnB.Enabled = false;
            ////}
        }
        #endregion

        #region "绑定数据"
        /// <summary>
        /// 重新绑定数据
        /// </summary>
        private void RefleshData()
        {
            //操作完绑定数据
            QueryParam qp = new QueryParam();
            qp.PageIndex = AspNetPager1.CurrentPageIndex;
            // qp.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            OnStart(qp);
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="qp"></param>
        private void OnStart(QueryParam qp)
        {
            qp.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            
            int RecordCount = 0;

            GridView1.DataSource = new Method().PE_NewsCategoryList(qp, out RecordCount);
            GridView1.DataBind();
            //分页
            config.CustomInfoHTML(AspNetPager1, RecordCount, qp.PageSize);
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
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
                 
                if (cbox.Checked == true)
                {
                    PE_NewsCategoryTable sat = new PE_NewsCategoryTable();
                    sat.PCID = Convert.ToInt32(GridView1.DataKeys[i]["PCID"].ToString());
                    sat.DataTable_Action_ = "Delete";
                    new Method().PE_NewsCategoryInsertUpdateDelete(sat);
                    flag = true;

                    string E_record = sat.DataTable_Action_ + " ： " + this.GridView1.Rows[i].Cells[2].Text.ToString();                     
                    EventMessage.EventWriteDB(1, E_record);
                }
            }

            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.succeedInfo({message:'删除成功！' ,title:'操作提示'});setTimeout(function(){ ymPrompt.close();},1000);</script>");

                RefleshData();
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
            RefleshData();
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


        #region "字段转换 RowDataBound事件"
        /// <summary>
        /// 字段转换 RowDataBound事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            ////判断修改权限
            //if (!new Method().CheckButtonPermission(PopedomType.Edit))
            //{
            //    e.Row.Cells[e.Row.Cells.Count-1].Visible = false;
            //}

        }
        #endregion

    }
}