using System;
using System.Collections.Generic;
 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using BLL;
using Model;
namespace SincciKC.system
{
    public partial class EventManage : BPage
    {
        #region "Page_Load"
        public int pagesize = Convert.ToInt32(config.sink("pagesize", config.MethodType.Get, 255, 1, config.DataType.Int));
        public int page = Convert.ToInt32(config.sink("page", config.MethodType.Get, 255, 1, config.DataType.Int));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //页面权限
                Permission();
                //绑定PageSize数据
                this.ddlPageSize.DataSource = new config().PageSizelist();
                this.ddlPageSize.DataBind();
                //初始化PageSize 
                if (pagesize > 0)
                {
                    this.ddlPageSize.SelectedValue = pagesize.ToString();
                }
                else 
                {
                    pagesize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
                }

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
            ////新增
            //if (!new Method().CheckButtonPermission(PopedomType.New))
            //{
            //    this.btnNew.Visible = false;
            //}
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
            ////备用A 初始化密码
            //if (!new Method().CheckButtonPermission(PopedomType.A))
            //{
            //    this.btnReset.Visible = false;
            //}
            //////备用B
            ////if (!new Method().CheckButtonPermission(PopedomType.B))
            ////{
            ////    this.btnB.Enabled = false;
            ////}
        }
        #endregion

        #region "绑定数据"
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="qp"></param>
        private void OnStart()
        {
            if (page == 0)
                page = 1;

            QueryParam qp = new QueryParam();
            qp.Order = " order by EventID desc";
            qp.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            qp.PageIndex = page;
            qp.Where = strwhere();

            int RecordCount = 0;
         
            GridView1.DataSource = new Method().sys_EventList(qp, out RecordCount);
            GridView1.DataBind();
            //分页
            config.CustomInfoHTML(AspNetPager1, RecordCount, qp.PageSize);
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

        /// <summary>
        /// 查询
        /// </summary> 
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            OnStart();
        }

        private string strwhere()
        {
            string str = "";

            if (this.ddlType.SelectedValue.Length > 0)
                str = string.Format(" E_Type={0} and ", this.ddlType.SelectedValue);
            if (this.txtUserName.Text.Trim().Length > 0)
                str = str + string.Format(" E_U_LoginName like '%{0}%' and ", config.CheckChar(this.txtUserName.Text.Trim()));
            #region "管理部门权限"
            int usertype = SincciLogin.Sessionstu().UserType;
            string fanwei = SincciLogin.Sessionstu().U_department;
            string strset = "";
            switch (SincciLogin.Sessionstu().UserType)
            {
                //系统管理员
                case 1:
                    strset = " 1=1 ";
                    break;
                //市招生办
                case 2:
                    strset = " 1=1  ";
                    break;
                //区招生办
                case 3:
                    strset = " E_U_LoginName like '" + fanwei + "%' ";
                    break;
                //学校用户 
                case 4:
                    strset = " E_U_LoginName like '" + fanwei + "%' ";
                    break;
                //班级用户 
                case 5:
                    strset = " E_U_LoginName like '" + fanwei + "%' ";
                    break;
                //考点用户
                case 7:
                    strset = " E_U_LoginName = '" + SincciLogin.Sessionstu().UserName + "' ";
                    break;
                default:
                    strset = " 1<>1";
                    break;
            }

            #endregion
            if (str.Length > 0)
            {
                str = str + strset;
            }
            else
            {
                str = strset;
            }
            return str;

        }


    }
}