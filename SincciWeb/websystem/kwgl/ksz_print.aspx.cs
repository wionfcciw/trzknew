using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using System.Data;
namespace SincciKC.websystem.kwgl
{
    public partial class ksz_print : BPage
    {
        
        /// <summary>
        /// 考点代码控制类
        /// </summary>
        BLL_zk_kd BLL_kd = new BLL_zk_kd();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //绑定PageSize数据
                this.ddlPageSize.DataSource = new config().PageSizelist();
                this.ddlPageSize.DataBind();
                if (Request.QueryString["kddm"] != null)
                {
                    BindGv();
                }
              
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGv()
        {
            int RecordCount = 0;
            int pageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            string strWhere =  createWhere();
            DataTable tab = BLL_kd.ExecuteProc_View_ksz(strWhere, pageSize, AspNetPager1.CurrentPageIndex, ref RecordCount);
            this.repDisplay.DataSource = tab;
            this.repDisplay.DataBind();

            //分页
            config.AspNetPagerCustomInfoHTML2(AspNetPager1, RecordCount, pageSize);
        }

 
        /// <summary>
        /// 创建查询条件。
        /// </summary>
        /// <returns></returns>
        private string createWhere()
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string whereStr = "kddm='" + Request.QueryString["kddm"].ToString() + "' and ";
            //管理部门权限
            string where = "";
            switch (UserType)
            {
                //系统管理员
                case 1:
                    where = " 1=1 ";
                    break;
                //市招生办
                case 2:
                    where = " 1=1 ";
                    break;
                //区招生办
                case 3:
                    where = " xqdm='" + Department + "' ";
                    break;
                //学校用户 
                case 4:
                    where = " 1<>1";
                    break;
                //班级用户 
                case 5:
                    where = " 1<>1";
                    break;
                default:
                    where = " 1<>1";
                    break;  
            }
            if (whereStr.Length > 0)
            {
                whereStr = whereStr + where;
            }
            else
            {
                whereStr = where;
            }

            return whereStr;
        }
       

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

     


        /// <summary>
        /// 上下一页
        /// </summary> 
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            BindGv();
        }
        /// <summary>
        /// 毕业中学打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
           if (Request.Form["CheckBox1"] != null)
            {

                string str = "";
                if (Request.Form["CheckBox1"] != null)
                {
                    str = Request.Form["CheckBox1"].ToString();
                }
             
                if (str != "")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >window.parent.addTab2('考试证打印', '/websystem/kwgl/Print_ksz.aspx?xxdm=" + str + "');</script>");
         
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >alert('请选择打印条件！');</script>"); 
                }       
           }
           else
           {
               Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >alert('请选择打印条件！');</script>");
           }     
        }
        /// <summary>
        /// 单个考生打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnksh_Click(object sender, EventArgs e)
        {
            if (txtksh.Text != "" && txtksh.Text.Length == 12)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >window.parent.addTab2('考试证打印', '/websystem/kwgl/Print_ksz.aspx?ksh=" + txtksh.Text + "');</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >alert('报名号有误！');</script>");
            }
        }

        
    }
}