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
    public partial class XQWebForm : BPage
    {
        /// <summary>
        /// 页面加载。
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int UserType = SincciLogin.Sessionstu().UserType;
                //if (UserType != 1)
                //{
                //    Response.Write("<script language='javascript'>window.parent.location.href='/ht/login.aspx';</script>");
                //    return;
                //}
               // loadPcInfo();
                Loadwcl();
              //  loadToudangInfo();
            }
        }
        /// <summary>
        /// 加载未处理的批次
        /// </summary>
        private void Loadwcl()
        {
            BLL_xqlq zk = new BLL_xqlq();
            DataTable tab = zk.selectwcl(SincciLogin.Sessionstu().U_department);
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
            //BLL_xqlq zk = new BLL_xqlq();
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
            //BLL_xqlq zk = new BLL_xqlq();
            //DataTable tab = zk.Select_zk_zsxx( pcdm);
            //this.dllxx.DataSource = tab;
            //this.dllxx.DataTextField = "zsxxmc";
            //this.dllxx.DataValueField = "xxdm";
            //this.dllxx.DataBind();
            //Loadzyxx(dllxx.SelectedValue.ToString());
        }

        /// <summary>
        /// 加载发档批次信息
        /// </summary>
        private void Loadzyxx(string xxdm)
         {
        //    string str = this.ddlXpcInfo.SelectedItem.Text;
        //    int begin = 0;
        //    int end = 0;
        //    begin = str.IndexOf('[');
        //    end = str.IndexOf(']');
        //    string pcdm = str.Substring(begin + 1, end - begin - 1);
        //    BLL_xqlq zk = new BLL_xqlq();
        //    DataTable tab = zk.selectXxFdpc(xxdm, pcdm);
        //    this.ddlfapc.DataSource = tab;
        //    this.ddlfapc.DataTextField = "td_pc";
        //    this.ddlfapc.DataValueField = "td_pc";
        //    this.ddlfapc.DataBind();
        //    loadToudangInfo();
        }
      
        /// <summary>
        /// 加载该学校投档信息。
        /// </summary>
        private void loadToudangInfo()
        {
            //BLL_xqlq zk = new BLL_xqlq();
            //if (this.ddlXpcInfo.SelectedIndex < 0)
            //{
            //    return;
            //   // this.tr_head.Visible = true;
            //}
            //string xxdm = dllxx.SelectedValue;
            //string tdpc = ddlfapc.SelectedValue.ToString();
            //string str = this.ddlXpcInfo.SelectedItem.Text;
            //int begin = 0;
            //int end = 0;
            //begin = str.IndexOf('[');
            //end = str.IndexOf(']');
            //string pcdm = str.Substring(begin + 1, end - begin - 1);
            //DataTable tab = new DataTable();

            //tab = zk.selectksh(xxdm, pcdm, tdpc);
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
             BLL_xqlq zk = new BLL_xqlq();
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
             string xxdm = ""; string pcdm = ""; string tdpc = "";
             if (strwcl.Length > 0)
             {
                 pcdm = strwcl.Split('_')[1];
                 xxdm = strwcl.Split('_')[0];
                 tdpc = strwcl.Split('_')[2];
                 loadToudangInfo2(xxdm, pcdm, tdpc);
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
            BLL_xqlq zk = new BLL_xqlq();
           

            string strksh = "";
            if (Request.Form["CheckBox1"] != null)
            {
                strksh = Request.Form["CheckBox1"].ToString();

                if (strksh.Split(',').Length == 1)
                {

                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>opUp('" + strksh + "', '考生审核不通过') ;</script>");
           
                    //zk.XX_TD(str, 3);
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
            string xxdm = ""; string pcdm = ""; string tdpc = "";
            if (strwcl.Length > 0)
            {
                pcdm = strwcl.Split('_')[1];
                xxdm = strwcl.Split('_')[0];
                tdpc = strwcl.Split('_')[2];
                loadToudangInfo2(xxdm, pcdm, tdpc);
            }
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPl_Fd_Click(object sender, EventArgs e)
        {
            BLL_xqlq zk = new BLL_xqlq();
            DataTable tab = new DataTable();
            //string xxdm = dllxx.SelectedValue.ToString();
            //string tdpc = ddlfapc.SelectedValue.ToString();
            //string str = this.ddlXpcInfo.SelectedItem.Text;
            //int begin = 0;
            //int end = 0;
            //begin = str.IndexOf('[');
            //end = str.IndexOf(']');
            //string pcdm = str.Substring(begin + 1, end - begin - 1);
            string strwcl = ddl_wcl.SelectedValue;
            string xxdm = ""; string pcdm = ""; string tdpc = "";
            if (strwcl.Length > 0)
            {
                pcdm = strwcl.Split('_')[1];
                xxdm = strwcl.Split('_')[0];
                tdpc = strwcl.Split('_')[2];

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('无数据上传!');</script>");
                return;
            }
            tab = zk.selectksh(SincciLogin.Sessionstu().U_department,xxdm, pcdm, tdpc);
            if (tab != null)
            {
                if (tab.Rows.Count > 0)
                {
                    if (tab.Select(" xq_zt in (3,4)").Length > 0)
                    {
                        if (tab.Select(" xq_zt=2").Length > 0)
                        {
                            if (zk.XX_UP_PASS(xxdm, pcdm, tdpc))
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('上传成功!');</script>");
                                Loadwcl();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('上传失败!');</script>");
                            }
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('该发档批次还有考生尚未操作!');</script>");
                        }
                    }
                    else
                    {
                        if (tab.Select(" xq_zt=2").Length > 0)
                        {
                            if (zk.XX_UP_PASS(xxdm, pcdm, tdpc))
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('上传成功!');</script>");
                                Loadwcl();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('上传失败!');</script>");
                            }
                        }
                        else
                        {
                            if (zk.XX_UP(xxdm, pcdm, tdpc))
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('上传成功!');</script>");
                                //Loadwcl();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('上传失败!');</script>");

                            }
                    
                        }
                    
                    }
                }
            }
          
            loadToudangInfo2(xxdm, pcdm, tdpc);
        }
        /// <summary>
        /// 发档批次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlfapc_SelectedIndexChanged(object sender, EventArgs e)
        {
           // loadToudangInfo();
        }
        /// <summary>
        /// 学校下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dllxx_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  Loadzyxx(dllxx.SelectedValue);
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
                string xxdm=""; string pcdm=""; string tdpc="";
                if (strwcl.Length > 0)
                {
                    pcdm = strwcl.Split('_')[1];
                    xxdm = strwcl.Split('_')[0];
                    tdpc = strwcl.Split('_')[2];
                    loadToudangInfo2(xxdm, pcdm, tdpc);
                }
            }
        }

        /// <summary>
        /// 未处理下拉框加载信息。
        /// </summary>
        private void loadToudangInfo2(string xxdm, string pcdm, string tdpc)
        {
            BLL_xqlq zk = new BLL_xqlq();
           

            DataTable tab = new DataTable();

            tab = zk.selectksh(SincciLogin.Sessionstu().U_department, xxdm, pcdm, tdpc);
            //if (tab != null)
            //{
            //    tab.Columns.Add("序号");
            //    for (int i = 0; i < tab.Rows.Count; i++)
            //    {
            //        tab.Rows[i]["序号"] = (i + 1).ToString();
            //    }
            //}
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

            string strwcl = ddl_wcl.SelectedValue;
            string xxdm = ""; string pcdm = ""; string tdpc = "";
            if (strwcl.Length > 0)
            {
                pcdm = strwcl.Split('_')[1];
                xxdm = strwcl.Split('_')[0];
                tdpc = strwcl.Split('_')[2];
                loadToudangInfo2(xxdm, pcdm, tdpc);
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

        protected void repDisplay_PageIndexChanged(object sender, EventArgs e)
        {
            if (ddl_wcl.SelectedValue.Length > 0)
            {
                string strwcl = ddl_wcl.SelectedValue;
                string xxdm = ""; string pcdm = ""; string tdpc = "";
                if (strwcl.Length > 0)
                {
                    pcdm = strwcl.Split('_')[1];
                    xxdm = strwcl.Split('_')[0];
                    tdpc = strwcl.Split('_')[2];
                    loadToudangInfo2(xxdm, pcdm, tdpc);
                }
            }
        }

        protected void repDisplay_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            repDisplay.PageIndex = e.NewPageIndex;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BLL_xqlq zk = new BLL_xqlq();
            string str = "";
            string strwcl = ddl_wcl.SelectedValue;
            string xxdm = ""; string pcdm = ""; string tdpc = "";
            if (strwcl.Length > 0)
            {
                pcdm = strwcl.Split('_')[1];
                xxdm = strwcl.Split('_')[0];
                tdpc = strwcl.Split('_')[2];
                str = str + "   xqdm='" + SincciLogin.Sessionstu().U_department + "' and lqxx='" + xxdm + "' and  pcdm='" + pcdm + "' and td_pc='" + tdpc + "'  ";
                zk.XX_TD(str, 5);
                loadToudangInfo2(xxdm, pcdm, tdpc);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (ddl_wcl.SelectedValue.Length > 0)
            {

             
                string strwcl = ddl_wcl.SelectedValue;
                string xxdm = ""; string pcdm = ""; string tdpc = "";
                if (strwcl.Length > 0)
                {
                    pcdm = strwcl.Split('_')[1];
                    xxdm = strwcl.Split('_')[0];
                    tdpc = strwcl.Split('_')[2];
                    BLL_xqlq zk = new BLL_xqlq();
                    DataTable tab = new DataTable();

                    tab = zk.selectksh2(SincciLogin.Sessionstu().U_department, xxdm, pcdm, tdpc);

                    GridView gvOrders = new GridView();
                    Response.Clear();
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.Charset = "GB2312";
                    string name = xxdm;
                    name = System.Web.HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8);
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + name + ".xls");
                    Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");  //设置输出流为简体中文 
                    Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
                    gvOrders.AllowPaging = false;
                    gvOrders.AllowSorting = false;
                    gvOrders.DataSource = tab;
                    gvOrders.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                    gvOrders.DataBind();
                    gvOrders.RenderControl(hw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择需要导出的批次!');</script>");

                }

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择需要导出的批次!');</script>");

            }
        }
    }
}