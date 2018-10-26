using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using System.Text;

namespace SincciKC
{
  
    /// <summary>
    /// 阅档
    /// </summary>
    public partial class ZCXQWebForm : BPage
    {
        /// <summary>
        /// 页面加载。
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int UserType = SincciLogin.Sessionstu().UserType;
                if (UserType != 1)
                {
                    Response.Write("<script language='javascript'>window.parent.location.href='/ht/login.aspx';</script>");
                    return;
                }
                Loadwcl();
             //  loadToudangInfo();
            }
        }

        /// <summary>
        /// 加载未处理的批次
        /// </summary>
        private void Loadwcl()
        {
            BLL_zc_xq zk = new BLL_zc_xq();
            DataTable tab = zk.selectxx();
            this.ddl_wcl.DataSource = tab;
            this.ddl_wcl.DataTextField = "lqxxmc";
            this.ddl_wcl.DataValueField = "lqxx";
            this.ddl_wcl.DataBind();
            this.ddl_wcl.Items.Insert(0, new ListItem("-请选择-", ""));

        }
       
        /// <summary>
        /// 查询条件 
        /// </summary>
        /// <returns></returns>
        private string creatWhere()
        {
            string str = "";
          
            str = str+ " 1=1 ";
       
            return str;
        }
        /// <summary>
        /// 上传条件 
        /// </summary>
        /// <returns></returns>
        private string creatWhere2()
        {
            string str = "";
         
            if (str.Length == 0)
            {
                str = " 1=1 ";
            }
            return str;
        }
        /// <summary>
        /// 加载该学校投档信息。
        /// </summary>
        private void loadToudangInfo(string xxdm)
        {
            BLL_zc_xq zk = new BLL_zc_xq();
           
        
            DataTable tab = new DataTable();

            tab = zk.selectksh(" a.lqxx='" + xxdm + "' ");
            if (ViewState["SortDirection"] == null)
                repDisplay.DataSource = tab;
            else
            {
                tab.DefaultView.Sort = ViewState["SortExpression"].ToString() + " " +
                 ViewState["SortDirection"].ToString() + ",cj desc";
                this.repDisplay.DataSource = tab;
            }
            this.repDisplay.DataBind();
            string dm = ddl_wcl.SelectedValue;
            Loadwcl();
            try
            {
                ddl_wcl.SelectedValue = dm;
            }
            catch (Exception)
            {
            }
        }
      
        /// <summary>
        /// 通过
        /// </summary>
        protected void btn_beginTd_Click(object sender, EventArgs e)
        {
             BLL_zc_xq zk = new BLL_zc_xq();
             string str = "";
            
             string strksh="";
             if (Request.Form["CheckBox1"] != null)
             {
                 strksh = Request.Form["CheckBox1"].ToString();

                 if (strksh.Length > 0)
                 {
                     str = str + "  ksh in(" + strksh + ") ";
                     zk.XX_TD(str, 5);
                 }
             }
             else
             {
                 Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择考生!');</script>");
             }
             string strwcl = ddl_wcl.SelectedValue;
             string xxdm = "";  
             if (strwcl.Length > 0)
             {
                 xxdm = strwcl;
                 loadToudangInfo(xxdm);
             }
           
        }

        /// <summary>
        /// 点击导出所有考生当前投档状态信息。
        /// </summary>
        protected void btnImport_td_ksxx_Click(object sender, EventArgs e)
        {
            //string str = this.ddlXpcInfo.SelectedItem.Text;
            //int begin = 0;
            //int end = 0;
            //begin = str.IndexOf('[');
            //end = str.IndexOf(']');
            //string pcdm = str.Substring(begin + 1, end - 1);

            //string where = String.Format(" where pcdm='{0}'", pcdm);
            BLL_LQK_Ks_Xx lqk = new BLL_LQK_Ks_Xx();
            if (!lqk.Import_lqk())
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'导出数据失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
            }

        }
        /// <summary>
        /// 预退
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Td_Click(object sender, EventArgs e)
        {
            BLL_zc_xq zk = new BLL_zc_xq();

            string str = "";
            string strksh = "";
            if (Request.Form["CheckBox1"] != null)
            {
                strksh = Request.Form["CheckBox1"].ToString();

                if (strksh.Length >= 1)
                {
                    str = str + "  ksh in(" + strksh + ") ";
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>opUp('" + strksh + "', '考生审核不通过') ;</script>");
           
                     zk.XX_TD(str, 1);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('不能同时对多个考生进行操作!');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择考生!');</script>");
            }

            string strwcl = ddl_wcl.SelectedValue;
            string xxdm = "";
            if (strwcl.Length > 0)
            {
                xxdm = strwcl;
                loadToudangInfo(xxdm);
            }
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPl_Fd_Click(object sender, EventArgs e)
        {
            BLL_zc_xq zk = new BLL_zc_xq();
            DataTable tab = new DataTable();
           // string xxdm = dllxx.SelectedValue.ToString();
            
            //string str = this.ddlXpcInfo.SelectedItem.Text;
            //int begin = 0;
            //int end = 0;
            //begin = str.IndexOf('[');
            //end = str.IndexOf(']');
            //string pcdm = str.Substring(begin + 1, end - begin - 1);
            tab = zk.selectksh(creatWhere());
            if (tab != null)
            {
                if (tab.Rows.Count > 0)
                {
                    
                    if (zk.XX_UP(creatWhere()))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('上传成功!');</script>");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('上传失败!');</script>");

                    }
                }
            }
            string strwcl = ddl_wcl.SelectedValue;
            string xxdm = "";
            if (strwcl.Length > 0)
            {
                xxdm = strwcl;
                loadToudangInfo(xxdm);
            }
        }
       

        protected void dllxx_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strwcl = ddl_wcl.SelectedValue;
            string xxdm = "";
            if (strwcl.Length > 0)
            {
                xxdm = strwcl;
                loadToudangInfo(xxdm);
            }
        }

        protected void repDisplay_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (ViewState["SortDirection"] == null)
                ViewState["SortDirection"] = "DESC";
            if (ViewState["SortDirection"].ToString() == "ASC")
                ViewState["SortDirection"] = "DESC";
            else
                ViewState["SortDirection"] = "ASC";
            ViewState["SortExpression"] = e.SortExpression;
            string strwcl = ddl_wcl.SelectedValue;
            string xxdm = "";
            if (strwcl.Length > 0)
            {
                xxdm = strwcl;
                loadToudangInfo(xxdm);
            }
        }

        protected void repDisplay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "if(this!=prev){c=this.style.backgroundColor;this.style.backgroundColor='#D8F3C6'}");//当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseout", "if(this!=prev){this.style.backgroundColor=c}");//当鼠标移开时还原背景色
                e.Row.Attributes["style"] = "Cursor:hand";//设置悬浮鼠标指针形状为"小手"
                e.Row.Attributes.Add("onclick", "RowClick(this); ");
            }
        }

        protected void ddl_wcl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_wcl.SelectedValue.Length > 0)
            {
                string strwcl = ddl_wcl.SelectedValue;
                string xxdm = "";
                if (strwcl.Length > 0)
                {
                    xxdm = strwcl;
                    loadToudangInfo(xxdm);
                }
            }
        }

       
        //      /// <summary>
        ///// 加载该学校投档信息。
        ///// </summary>
        //private void loadToudangInfo2(string xxdm,string pcdm)
        //{
        //    BLL_zc_xq zk = new BLL_zc_xq();
           
             
        //    DataTable tab = new DataTable();

        //    tab = zk.selectksh(creatWhere());
        //    if (ViewState["SortDirection"] == null)
        //        repDisplay.DataSource = tab;
        //    else
        //    {
        //        tab.DefaultView.Sort = ViewState["SortExpression"].ToString() + " " +
        //         ViewState["SortDirection"].ToString() + ",cj desc";
        //        this.repDisplay.DataSource = tab;
        //    }
        //    this.repDisplay.DataBind();
           
        //}
 
       
    }
}