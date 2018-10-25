using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using LinqToExcel;
using LinqToExcel.Query;
using BLL;
using System.Data;
using System.IO;
using System.Data.Odbc;
using System.Text;
namespace SincciKC.websystem.zklq
{
    public partial class LQZsjh_Manage : BPage
    {
        /// <summary>
        /// 招生计划控制类
        /// </summary>
        BLL_zk_zsjh bllzsjh = new BLL_zk_zsjh();
        /// <summary>
        ///县区代码控制类
        /// </summary>
        BLL_zk_xqdm bllxqdm = new BLL_zk_xqdm();

        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Permission();
                //绑定PageSize数据
                this.ddlPageSize.DataSource = new config().PageSizelist();
                this.ddlPageSize.DataBind();
 

                BinZsxx();
                BindGv();
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGv()
        {

            int RecordCount = 0;
            int pageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);

            string strWhere = createWhere();
            DataTable tab = bllzsjh.ExecuteProc_View_zk_lqjhk(strWhere, pageSize, AspNetPager1.CurrentPageIndex, ref RecordCount);
            this.Repeater1.DataSource = tab;
            this.Repeater1.DataBind();

            //分页
            config.AspNetPagerCustomInfoHTML2(AspNetPager1, RecordCount, pageSize);
        }
        /// <summary>
        /// 加载招生学校数据
        /// </summary>
        private void BinZsxx()
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            this.ddlxqdm.DataSource = bllxqdm.SelectXqdm(Department, UserType);
            this.ddlxqdm.DataTextField = "xqmc";
            this.ddlxqdm.DataValueField = "xqdm";
            this.ddlxqdm.DataBind();
            this.ddlxqdm.Items.Insert(0, new ListItem("-请选择-", ""));
        }

        /// <summary>
        /// 创建查询条件。
        /// </summary>
        /// <returns></returns>
        private string createWhere()
        {
            string keyWord = config.CheckChar(txtName.Text.Trim());
            string xqdm = ddlxqdm.SelectedItem.Value.Trim();
            string result = "";
            string and = "";

            if (!string.IsNullOrEmpty(xqdm))
            {
                result = " xqdm='" + xqdm + "' ";
                and = " And ";
            }
         
            if (!string.IsNullOrEmpty(keyWord))
                result += and + "  xxdm='" + keyWord + "' ";
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            switch (UserType)
            {
                case 3:
                  
                    result += "  and xqdm='" + Department + "' ";
                    break;
                default:
                    break;
            }
            return result;
 
        }

        #region "查询"
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGv();
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
            BindGv();
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
          
        
        }
        #endregion

   

      

        /// <summary>
        /// 上下一页
        /// </summary> 
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            BindGv();
        }
        /// <summary>
        /// 选择招生学校
        /// </summary> 
        protected void ddlzsxx_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGv();
        }
 
     
    }
}