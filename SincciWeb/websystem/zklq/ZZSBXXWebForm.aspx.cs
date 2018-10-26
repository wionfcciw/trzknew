using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using System.Text;
using Model;

namespace SincciKC
{
  
    /// <summary>
    /// 阅档
    /// </summary>
    public partial class ZZSBXXWebForm : BPage
    {
        /// <summary>
        /// 页面加载。
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Permission();
                int UserType = SincciLogin.Sessionstu().UserType;
                if (UserType != 8 && UserType != 9)
                {
                    Response.Write("你没有页面查看的权限！");
                    Response.End();
                    return;
                }
                string Department = SincciLogin.Sessionstu().UserName;
                string xxdm = Department;
               // loadPcInfo(xxdm);
                Loadzy(xxdm);
                Loadwcl(xxdm);
               // loadToudangInfo();
            }
        }
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
        /// 加载未处理的批次
        /// </summary>
        private void Loadwcl(string xxdm)
        {
            BLL_zzsb_xx zk = new BLL_zzsb_xx();
            DataTable tab = zk.selectwcl(xxdm);
            this.ddl_wcl.DataSource = tab;
            this.ddl_wcl.DataTextField = "mc";
            this.ddl_wcl.DataValueField = "dm";
            this.ddl_wcl.DataBind();
            this.ddl_wcl.Items.Insert(0, new ListItem("-请选择-", ""));

        }
        /// <summary>
        /// 加载批次信息。
        /// </summary>
        private void loadPcInfo(string xxdm)
        {
            //BLL_zzsb_xx zk = new BLL_zzsb_xx();
            //DataTable tab = zk.selectPcdm(xxdm);
            //this.ddlXpcInfo.DataSource = tab;
            //this.ddlXpcInfo.DataTextField = "xpc_mc";
            //this.ddlXpcInfo.DataValueField = "xpcid";
            //this.ddlXpcInfo.DataBind();

            
        }
        /// <summary>
        /// 加载专业信息
        /// </summary>
        private void Loadzy(string xxdm)
        {
            BLL_zzsb_xx bll = new BLL_zzsb_xx();
            DataTable tab = bll.Select_zk_zykXX(xxdm);
            this.ddlzy.DataSource = tab;
            this.ddlzy.DataTextField = "zymc";
            this.ddlzy.DataValueField = "zydm";
            this.ddlzy.DataBind();
        }
       
      
        /// <summary>
        /// 加载该学校投档信息。
        /// </summary>
        private void loadToudangInfo()
        {
            //BLL_zzsb_xx zk = new BLL_zzsb_xx();
            //if (this.ddlXpcInfo.SelectedIndex < 0)
            //{
            //    return;
            //    // this.tr_head.Visible = true;
            //}
            //string Department = SincciLogin.Sessionstu().UserName;
            //string xxdm = Department;

            //string str = this.ddlXpcInfo.SelectedItem.Text;
            //int begin = 0;
            //int end = 0;
            //begin = str.IndexOf('[');
            //end = str.IndexOf(']');
            //string pcdm = str.Substring(begin + 1, end - begin - 1);
            //DataTable tab = new DataTable();

            //tab = zk.selectksh(xxdm, pcdm);
            //if (tab != null)
            //{
            //    tab.Columns.Add("序号");
            //    for (int i = 0; i < tab.Rows.Count; i++)
            //    {
            //        tab.Rows[i]["序号"] = (i + 1).ToString();
            //    }
            //}
            //this.repDisplay.DataSource = tab;
            //this.repDisplay.DataBind();
            //return;
        }
       
        /// <summary>
        /// 预录
        /// </summary>
        protected void btn_beginTd_Click(object sender, EventArgs e)
        {
             BLL_zzsb_xx zk = new BLL_zzsb_xx();
             string str = "";
            
             string strksh="";
             if (Request.Form["CheckBox1"] != null)
             {
                 strksh = Request.Form["CheckBox1"].ToString();

                 if (strksh.Length > 0)
                 {
                      str = str + "  ksh in(" + strksh + ") ";
                      zk.XX_TD(str, 4, ddlzy.SelectedValue);
                 }
             }
             else
             {
                 Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择需要预录的考生!');</script>");
             }

             string strwcl = ddl_wcl.SelectedValue;
             if (strwcl.Length > 0)
             {
                 string pcdm = strwcl;
                 loadToudangInfo2(pcdm);
             }
        }

        
        /// <summary>
        /// 预退
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Td_Click(object sender, EventArgs e)
        {
            BLL_zzsb_xx zk = new BLL_zzsb_xx();
            string strksh = "";
            if (Request.Form["CheckBox1"] != null)
            {
                strksh = Request.Form["CheckBox1"].ToString();

                if (strksh.Split(',').Length > 0)
                {
                    strksh = " ksh in (" + strksh + ")";
                    string Department = SincciLogin.Sessionstu().UserName;
                    string xxdm = Department;
                    if (txtyij.Text.Trim().Length != 0)
                    {
                        zk.XX_TD_YT(strksh, 3, txtyij.Text, xxdm);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请输入预退原因!');</script>");

                    }  
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择需要预退的考生!');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择需要预退的考生!');</script>");
            }

            string strwcl = ddl_wcl.SelectedValue;
            if (strwcl.Length > 0)
            {
                string pcdm = strwcl.Split('_')[0];
                loadToudangInfo2(pcdm);
            }
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPl_Fd_Click(object sender, EventArgs e)
        {
            BLL_zzsb_xx zk = new BLL_zzsb_xx();
            DataTable tab = new DataTable();
            string Department = SincciLogin.Sessionstu().UserName;
            string xxdm = Department;
          
            //string str = this.ddlXpcInfo.SelectedItem.Text;
            //int begin = 0;
            //int end = 0;
            //begin = str.IndexOf('[');
            //end = str.IndexOf(']');
            //string pcdm = str.Substring(begin + 1, end - begin - 1);
            string strwcl = ddl_wcl.SelectedValue;
            string pcdm = "";
            if (strwcl.Length > 0)
            {
                pcdm = strwcl;
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('无数据上传!');</script>");
                return;
            }
            tab = zk.selectksh(xxdm, pcdm);
            if (tab != null)
            {
                if (tab.Rows.Count > 0)
                {
                    if (tab.Select(" xx_zt=2").Length > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('还有考生尚未操作!');</script>");
                    }
                    else
                    {
                        if ( zk.XX_UP(xxdm, pcdm))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('上传成功!');</script>");
                           // Loadwcl(xxdm);
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('上传失败!');</script>");
             
                        }
                    
                    }
                }
            }
          
            loadToudangInfo2(pcdm);
        }
        

        protected void ddlXpcInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //loadToudangInfo();
        }
        /// <summary>
        /// 未处理下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddl_wcl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_wcl.SelectedValue.Length > 0)
            {
                string strwcl = ddl_wcl.SelectedValue;
                string pcdm = "";
                if (strwcl.Length > 0)
                {
                    pcdm = strwcl;
                    loadToudangInfo2(pcdm);
                }
            }
        }

        /// <summary>
        /// 加载该学校投档信息。
        /// </summary>
        private void loadToudangInfo2(string pcdm)
        {
            BLL_zzsb_xx zk = new BLL_zzsb_xx();
        
            string Department = SincciLogin.Sessionstu().UserName;
            string xxdm = Department;

            //string str = this.ddlXpcInfo.SelectedItem.Text;
            //int begin = 0;
            //int end = 0;
            //begin = str.IndexOf('[');
            //end = str.IndexOf(']');
            //string pcdm = str.Substring(begin + 1, end - begin - 1);
            DataTable tab = new DataTable();

            tab = zk.selectksh(xxdm, pcdm);
            
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
            Loadwcl(xxdm);
            try
            {
                ddl_wcl.SelectedValue = dm;
            }
            catch (Exception)
            {
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
            string pcdm = "";
            if (strwcl.Length > 0)
            {
                pcdm = strwcl;
                loadToudangInfo2(pcdm);
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
    }
}