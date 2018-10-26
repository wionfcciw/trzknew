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
    public partial class ZZSBXQWebForm : BPage
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
             //  loadPcInfo();
                Loadwcl();
              //  loadToudangInfo();
            }
        }
        /// <summary>
        /// 加载未处理的批次
        /// </summary>
        private void Loadwcl()
        {
            BLL_zzsb_xq zk = new BLL_zzsb_xq();
            DataTable tab = zk.selectwcl();
            this.ddl_wcl.DataSource = tab;
            this.ddl_wcl.DataTextField = "mc";
            this.ddl_wcl.DataValueField = "dm";
            this.ddl_wcl.DataBind();
            this.ddl_wcl.Items.Insert(0, new ListItem("-请选择-", ""));

        }
        /// <summary>
        /// 加载批次信息。
        /// </summary>
        private void loadPcInfo()
        {
            //BLL_zzsb_xq zk = new BLL_zzsb_xq();
            //DataTable tab = zk.selectPcdm();
            //this.ddlXpcInfo.DataSource = tab;
            //this.ddlXpcInfo.DataTextField = "xpc_mc";
            //this.ddlXpcInfo.DataValueField = "pcdm";
            //this.ddlXpcInfo.DataBind();
            //Loadxx();
           // Loadzyxx( xxdm);
        }

        /// <summary>
        /// 加载学校信息
        /// </summary>
        private void Loadxx()
        {
            //string str = this.ddlXpcInfo.SelectedItem.Text;
            //int begin = 0;
            //int end = 0;
            //begin = str.IndexOf('[');
            //end = str.IndexOf(']');
            //string pcdm = str.Substring(begin + 1, end - begin - 1);
            //BLL_zzsb_xq zk = new BLL_zzsb_xq();
            //DataTable tab = zk.Select_zk_zsxx( pcdm);
            //this.dllxx.DataSource = tab;
            //this.dllxx.DataTextField = "zsxxmc";
            //this.dllxx.DataValueField = "xxdm";
            //this.dllxx.DataBind();
        
        }

        
      
        ///// <summary>
        ///// 加载该学校投档信息。
        ///// </summary>
        //private void loadToudangInfo()
        //{
        //    BLL_zzsb_xq zk = new BLL_zzsb_xq();
        //    if (this.ddlXpcInfo.SelectedIndex < 0)
        //    {
        //        return;
        //       // this.tr_head.Visible = true;
        //    }
        //    string xxdm = dllxx.SelectedValue;
            
        //    string str = this.ddlXpcInfo.SelectedItem.Text;
        //    int begin = 0;
        //    int end = 0;
        //    begin = str.IndexOf('[');
        //    end = str.IndexOf(']');
        //    string pcdm = str.Substring(begin + 1, end - begin - 1);
        //    DataTable tab = new DataTable();

        //    tab = zk.selectksh(xxdm, pcdm);
        //    if (tab != null)
        //    {
        //        tab.Columns.Add("序号");
        //        for (int i = 0; i < tab.Rows.Count; i++)
        //        {
        //            tab.Rows[i]["序号"] = (i + 1).ToString();
        //        }
        //    }
        //    this.repDisplay.DataSource = tab;
        //    this.repDisplay.DataBind();
        //    return;
        //}
        /// <summary>
        /// 当改变当前批次时。
        /// </summary>
        protected void ddlXpcInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Loadxx();
         
        }
        /// <summary>
        /// 通过
        /// </summary>
        protected void btn_beginTd_Click(object sender, EventArgs e)
        {
             BLL_zzsb_xq zk = new BLL_zzsb_xq();
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
             string xxdm = ""; string pcdm = "";  
             if (strwcl.Length > 0)
             {
                 pcdm = strwcl.Split('_')[1];
                 xxdm = strwcl.Split('_')[0];
                 loadToudangInfo2(xxdm, pcdm);
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
            BLL_zzsb_xq zk = new BLL_zzsb_xq();
           

            string strksh = "";
            if (Request.Form["CheckBox1"] != null)
            {
                strksh = Request.Form["CheckBox1"].ToString();

                if (strksh.Length > 0)
                {
                    string str = " ksh in (" + strksh + ")";
                    //修改数据
                    BLL_xqlq zkc = new BLL_xqlq();
                    if (txtyij.Text.Trim().Length != 0)
                    {
                        bool result = zkc.XX_TD_YT(str, 2, txtyij.Text);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请输入不通过原因!');</script>");

                    }
                
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>opUp('" + strksh + "', '考生审核不通过') ;</script>");
                    
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择考生!');</script>");
         
                    //   Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('不能同时对多个考生进行操作!');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择考生!');</script>");
            }

            string strwcl = ddl_wcl.SelectedValue;
            string xxdm = ""; string pcdm = "";
            if (strwcl.Length > 0)
            {
                pcdm = strwcl.Split('_')[1];
                xxdm = strwcl.Split('_')[0];
                loadToudangInfo2(xxdm, pcdm);
            }
 
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPl_Fd_Click(object sender, EventArgs e)
        {
            BLL_zzsb_xq zk = new BLL_zzsb_xq();
            DataTable tab = new DataTable();
            //string xxdm = dllxx.SelectedValue.ToString();
            
            //string str = this.ddlXpcInfo.SelectedItem.Text;
            //int begin = 0;
            //int end = 0;
            //begin = str.IndexOf('[');
            //end = str.IndexOf(']');
            //string pcdm = str.Substring(begin + 1, end - begin - 1);
            string strwcl = ddl_wcl.SelectedValue;
            string xxdm = ""; string pcdm = "";
            if (strwcl.Length > 0)
            {
                pcdm = strwcl.Split('_')[1];
                xxdm = strwcl.Split('_')[0];
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
                    if (tab.Select(" xq_zt in (3,4)").Length > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('该发档批次还有考生尚未无操作!');</script>");
                    }
                    else
                    {
                        if ( zk.XX_UP(xxdm, pcdm))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('上传成功!');</script>");
                           
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('上传失败!');</script>");
             
                        }
                    
                    }
                }
            }
            loadToudangInfo2(xxdm, pcdm);
        }
       

        protected void dllxx_SelectedIndexChanged(object sender, EventArgs e)
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
                string str = ddl_wcl.SelectedValue;
                if (str.Length > 0)
                {
                    string xxdm = str.Split('_')[0];
                    string pcdm = str.Split('_')[1];
                    loadToudangInfo2(xxdm, pcdm);
                }
            }
        }

        /// <summary>
        /// 加载该学校投档信息。
        /// </summary>
        private void loadToudangInfo2(string xxdm, string pcdm)
        {
            BLL_zzsb_xq zk = new BLL_zzsb_xq();
          
            //string xxdm = dllxx.SelectedValue;

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
            Loadwcl();
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

            string str = ddl_wcl.SelectedValue;
            if (str.Length > 0)
            {
                string xxdm = str.Split('_')[0];
                string pcdm = str.Split('_')[1];
                loadToudangInfo2(xxdm, pcdm);
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