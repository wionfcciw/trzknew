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
    public partial class NewListManage : BPage
    {
        #region "Page_Load"
        //int pagesize = Convert.ToInt32(config.sink("pagesize", config.MethodType.Get, 255, 1, config.DataType.Int));
        //int categoryID = Convert.ToInt32(config.sink("ID", config.MethodType.Get, 255, 1, config.DataType.Int));
        //int ID = Convert.ToInt32(config.sink("categoryId", config.MethodType.Get, 255, 1, config.DataType.Int));
        //int markpass = Convert.ToInt32(config.sink("markpass", config.MethodType.Get, 255, 1, config.DataType.Int));
        //int editor = Convert.ToInt32(config.sink("editor", config.MethodType.Get, 255, 1, config.DataType.Int));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Permission();

                //绑定PageSize数据
                this.ddlPageSize.DataSource = new config().PageSizelist();
                this.ddlPageSize.DataBind();

                new Method().BindTreeView(this.ddlTree); 
                 
                OnStart();
                
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
            //删除
            if (!new Method().CheckButtonPermission(PopedomType.A4))
            {
                this.btnDelete.Visible = false;
            }
            //////修改
            ////if (!new Method().CheckButtonPermission(PopedomType.Edit))
            ////{
            ////    this.btnEdit.Enabled = false;
            ////}
            ////删除
            //if (!new Method().CheckButtonPermission(PopedomType.Delete))
            //{
            //    this.btnDelete.Visible = false;
            //}
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
        private void OnStart()
        {
            //QueryParam qp = new QueryParam();
            //qp.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            //qp.PageIndex = AspNetPager2.CurrentPageIndex;
            //qp.Order = " order by NewsID desc";
            //if(strWhere().Length>0)
            //    qp.Where=strWhere();
            int PageSize= Convert.ToInt32(this.ddlPageSize.SelectedValue);
            int page= AspNetPager2.CurrentPageIndex;
            int RecordCount = 0;
            string sWhere=strWhere();

            GridView1.DataSource = new BLL.BLL_news().DataTableArticle(sWhere, PageSize, page, ref RecordCount);           
            GridView1.DataBind();
            //分页
            config.AspNetPagerCustomInfoHTML2(AspNetPager2, RecordCount, PageSize);
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
                Label lblTitle = (Label)GridView1.Rows[i].FindControl("lblTitle");


                if (cbox.Checked == true)
                {
                    PE_NewsListTable sat = new PE_NewsListTable();
                    sat.NewsID = Convert.ToInt32(GridView1.DataKeys[i]["NewsID"].ToString());
                    sat.DataTable_Action_ = "Delete";
                    new Method().PE_NewsListInsertUpdateDelete(sat);
                    flag = true;

                    string E_record = sat.DataTable_Action_ + " ： " + lblTitle.Text.Trim().ToString();
 
                    EventMessage.EventWriteDB(1, E_record);
                    OnStart(); 
                }
            }
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.succeedInfo({message:'删除成功！' ,title:'操作提示'});setTimeout(function(){ ymPrompt.close();},1000);</script>");

                
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
        protected void AspNetPager2_PageChanged(object src, EventArgs e)
        { 
            OnStart();
          
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
            OnStart(); 
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

        #region "  RowCommand事件"
        /// <summary>
        ///  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] Arg = e.CommandArgument.ToString().Split('|');
            PE_NewsListTable sat = new PE_NewsListTable();
            sat.NewsID = Convert.ToInt32(Arg[0]);
            sat.DataTable_Action_ = "Update";

            if (e.CommandName.ToLower().Equals("show"))
            {
                sat.MarkType = 1;
                sat.Show = Arg[1].ToString() == "1" ? 0 : 1;
            }
            if (e.CommandName.ToLower().Equals("markpass"))
            {
                sat.MarkType = 2;
                sat.MarkPass = Arg[1].ToString() == "1" ? 0 : 1;
            }
            if (e.CommandName.ToLower().Equals("marktop"))
            {
                sat.MarkType = 3;
                sat.MarkTop = Arg[1].ToString() == "1" ? 0 : 1;
            }
            if (e.CommandName.ToLower().Equals("markimp"))
            {
                sat.MarkType = 4;
                sat.MarkImp = Arg[1].ToString() == "1" ? 0 : 1;
            }
            //if (e.CommandName.ToLower().Equals("gonggao"))
            //{
            //    sat.MarkType = 5;
            //    sat.GongGao = Arg[1].ToString() == "1" ? 0 : 1;
            //}

            new Method().PE_NewsListInsertUpdateDelete(sat);

            OnStart(); 
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
                //重要
                Button btnmarkimp = (Button)e.Row.FindControl("btnmarkimp");
                if (btnmarkimp.Text == "是")
                {
                    btnmarkimp.ForeColor = System.Drawing.Color.Red;
                }
                //置顶
                Button btnmarktop = (Button)e.Row.FindControl("btnmarktop");
                if (btnmarktop.Text == "是")
                {
                    btnmarktop.ForeColor = System.Drawing.Color.Red;
                   // btnmarktop.Style.Add("font-weight", "bold");
                }
                //显示
                Button btnshow = (Button)e.Row.FindControl("btnshow");
                if (btnshow.Text == "是")
                {
                    btnshow.ForeColor = System.Drawing.Color.Red;
                }
                ////公告
                //Button btngonggao = (Button)e.Row.FindControl("btngonggao");
                //if (btngonggao.Text == "是")
                //{
                //    btngonggao.ForeColor = System.Drawing.Color.Red;
                //}
                //已审核
                Button btnmarkpass = (Button)e.Row.FindControl("btnmarkpass");
                if (btnmarkpass.Text == "已审核")
                {
                    btnmarkpass.ForeColor = System.Drawing.Color.Red;
                }
            }
            ////判断修改权限
            //if (!new Method().CheckButtonPermission(PopedomType.Edit))
            //{
            //    e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;
            //}

        }
        #endregion 

        #region  截取字符串
        /// <summary>
        /// 截取指定长度的字符串
        /// </summary>
        /// <param name="title">字符串</param>
        /// <returns></returns>
        public string SetSubString(string title)
        {
            return config.SetSubString(title, 30);
        }
        #endregion

        #region  模块查询
        protected void btnSearch_Click(object sender, EventArgs e)
        { 
            OnStart(); 
        }

        private string strWhere()
        {
            string str = "";
            //标题
            if (txtTitle.Text.Trim()!=string.Empty)
            {
                str = string.Format(" Title like '%{0}%' and ",config.CheckChar( txtTitle.Text.Trim()));
            }

            //类型
            if (this.ddlTree.SelectedItem.Text != "全部栏目" && this.ddlTree.SelectedValue.ToString() != "")
            {
                str += string.Format("  CategoryID ={0} and ", ddlTree.SelectedValue);
            }

            //审核
            if (ddlMarkPass.SelectedItem.Text != "全部" && ddlMarkPass.SelectedValue.ToString() != "")
            {
                str += string.Format("  MarkPass={0} and ", ddlMarkPass.SelectedValue);
            }
            //热点
            if (this.ddlImp.SelectedItem.Text != "全部" && this.ddlImp.SelectedValue.ToString() != "")
            {
                str += string.Format("  MarkImp ={0} and ", ddlImp.SelectedValue);
            }
            ////发布人
            //if (txtAdmin.Text.Trim() != string.Empty)
            //{
            //    str += string.Format("  Editor like '%{0}%' and ", txtAdmin.Text.Trim());
            //}
            if (str.Length > 0)
                str = str + " 1=1 ";
            return str;
        }

        #endregion
    }
}