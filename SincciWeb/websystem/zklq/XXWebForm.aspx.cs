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
    public partial class XXWebForm : BPage
    {
        /// <summary>
        /// 页面加载。
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Permission();
                string Department = SincciLogin.Sessionstu().UserName;
                string xxdm = Department;
                int UserType = SincciLogin.Sessionstu().UserType;

                if (UserType != 8 && UserType != 9)
                {
                    Response.Write("你没有页面查看的权限！");
                    Response.End();
                    return;
                }
                //loadPcInfo(xxdm);
               
                Loadwcl(xxdm);
              
              //  loadToudangInfo();
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
            BLL_xxlq zk = new BLL_xxlq();
            DataTable tab = zk.selectwcl(xxdm);
            this.ddl_wcl.DataSource = tab;
            this.ddl_wcl.DataTextField = "mc";
            this.ddl_wcl.DataValueField = "dm";
            this.ddl_wcl.DataBind();
            this.ddl_wcl.Items.Insert(0, new ListItem("-请选择-", ""));

        }
        ///// <summary>
        ///// 加载批次信息。
        ///// </summary>
        //private void loadPcInfo(string xxdm)
        //{
        //    BLL_xxlq zk = new BLL_xxlq();
        //    DataTable tab = zk.selectPcdm(xxdm);
        //    this.ddlXpcInfo.DataSource = tab;
        //    this.ddlXpcInfo.DataTextField = "xpc_mc";
        //    this.ddlXpcInfo.DataValueField = "pcDm";
        //    this.ddlXpcInfo.DataBind();

        //    Loadzyxx( xxdm);
        //}
        /// <summary>
        /// 加载专业信息
        /// </summary>
        private void Loadzy(string xxdm, string pcdm)
        {
            BLL_xxlq bll = new BLL_xxlq();
            DataTable tab = bll.Select_zk_zykXX(xxdm,pcdm);
            this.ddlzy.DataSource = tab;
            this.ddlzy.DataTextField = "zymc";
            this.ddlzy.DataValueField = "zydm";
            this.ddlzy.DataBind();
        }
        ///// <summary>
        ///// 加载发档批次信息
        ///// </summary>
        //private void Loadzyxx(string xxdm)
        //{
        //    string str = this.ddlXpcInfo.SelectedItem.Text;
        //    int begin = 0;
        //    int end = 0;
        //    begin = str.IndexOf('[');
        //    end = str.IndexOf(']');
        //    string pcdm = str.Substring(begin + 1, end - begin - 1);
        //    BLL_xxlq zk = new BLL_xxlq();
        //    DataTable tab = zk.selectXxFdpc(xxdm, pcdm);
        //    this.ddlfapc.DataSource = tab;
        //    this.ddlfapc.DataTextField = "td_pc";
        //    this.ddlfapc.DataValueField = "td_pc";
        //    this.ddlfapc.DataBind();

        //}
      
        ///// <summary>
        ///// 加载该学校投档信息。
        ///// </summary>
        //private void loadToudangInfo()
        //{
        //    BLL_xxlq zk = new BLL_xxlq();
        //    if (this.ddlXpcInfo.SelectedIndex < 0)
        //    {
        //        return;
        //       // this.tr_head.Visible = true;
        //    }
        //    string Department = SincciLogin.Sessionstu().UserName;
        //    string xxdm = Department;
        //    string tdpc = ddlfapc.SelectedValue.ToString();
        //    string str = this.ddlXpcInfo.SelectedItem.Text;
        //    int begin = 0;
        //    int end = 0;
        //    begin = str.IndexOf('[');
        //    end = str.IndexOf(']');
        //    string pcdm = str.Substring(begin + 1, end - begin - 1);
        //    DataTable tab = new DataTable();

        //    tab = zk.selectksh(xxdm, pcdm, tdpc);
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
        ///// <summary>
        ///// 当改变当前批次时。
        ///// </summary>
        //protected void ddlXpcInfo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string Department = SincciLogin.Sessionstu().UserName;
        //    string xxdm = Department;
        //    Loadzyxx(xxdm);
         
        //}
        /// <summary>
        /// 预录
        /// </summary>
        protected void btn_beginTd_Click(object sender, EventArgs e)
        {
             BLL_xxlq zk = new BLL_xxlq();
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
                 string pcdm = strwcl.Split('_')[0];
                 string tdpc = strwcl.Split('_')[1];
                 loadToudangInfo2(pcdm, tdpc);

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
            BLL_xxlq zk = new BLL_xxlq();
           

            string strksh = "";
            if (Request.Form["CheckBox1"] != null)
            {
                strksh = Request.Form["CheckBox1"].ToString();

                if (strksh.Split(',').Length == 1)
                {
                    strksh = " ksh='" + strksh + "'";
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
                   
                //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>opUp('" + strksh + "', '考生预退') ;</script>");
           
                    //zk.XX_TD(str, 3);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('不能同时对多个考生进行预退操作!');</script>");
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
                string tdpc = strwcl.Split('_')[1];
                loadToudangInfo2(pcdm, tdpc);
            }
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPl_Fd_Click(object sender, EventArgs e)
        {
            BLL_xxlq zk = new BLL_xxlq();
            DataTable tab = new DataTable();
            string Department = SincciLogin.Sessionstu().UserName;
            string xxdm = Department;
            //string tdpc = ddlfapc.SelectedValue.ToString();
            //string str = this.ddlXpcInfo.SelectedItem.Text;
            //int begin = 0;
            //int end = 0;
            //begin = str.IndexOf('[');
            //end = str.IndexOf(']');
            //string pcdm = str.Substring(begin + 1, end - begin - 1);
            string str = ddl_wcl.SelectedValue;
            string pcdm = "";
            string tdpc = "";
            if (str.Length > 0)
            {
                pcdm = str.Split('_')[0];
                tdpc = str.Split('_')[1];
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('无数据上传!');</script>");
                return;
            }
            tab = zk.selectksh(xxdm, pcdm, tdpc);
            if (tab != null)
            {
                if (tab.Rows.Count > 0)
                {
                    if (tab.Select(" xx_zt=2").Length > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('该发档批次还有考生尚未操作!');</script>");
                    }
                    else
                    {
                        if ( zk.XX_UP(xxdm, pcdm, tdpc))
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
            loadToudangInfo2(pcdm, tdpc);
        }
        ///// <summary>
        ///// 发档批次
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void ddlfapc_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    loadToudangInfo();
        //}
        /// <summary>
        /// 建议预录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_jy_Click(object sender, EventArgs e)
        {
            BLL_xxlq zk = new BLL_xxlq();
            
            string Department = SincciLogin.Sessionstu().UserName;
            string xxdm = Department;
            //string tdpc = ddlfapc.SelectedValue.ToString();
            //string str = this.ddlXpcInfo.SelectedItem.Text;
            //int begin = 0;
            //int end = 0;
            //begin = str.IndexOf('[');
            //end = str.IndexOf(']');
            //string pcdm = str.Substring(begin + 1, end - begin - 1);
            string pcdm = "";
            string tdpc = "";
            string strwcl = ddl_wcl.SelectedValue;
            if (strwcl.Length > 0)
            {
                pcdm = strwcl.Split('_')[0];
                tdpc = strwcl.Split('_')[1];

            }
             DataTable tab = new DataTable(); //排序后考生集合
            DataTable tabtop = zk.selectksh_top(xxdm, pcdm, tdpc);//查询出建议专业类型
            
            if (tabtop != null)
            {
                if (tabtop.Rows.Count > 0)
                {
                    if (tabtop.Rows[0]["jyzy"].ToString() == "1")
                    {
                        tab = zk.selectksh_where(xxdm, pcdm, tdpc, " order by cj desc ");

                        for (int i = 0; i < tab.Rows.Count; i++)
                        {
                            DataTable stab = zk.selectksh_zynum(xxdm, pcdm, tab.Rows[i]["zy1"].ToString());//专业1剩余数量
                           
                            if (Convert.ToInt32(stab.Rows[0]["lqzynum"]) < Convert.ToInt32(stab.Rows[0]["jhs"])) //录取
                            {
                                zk.XX_TD(" ksh='" + tab.Rows[i]["ksh"].ToString() + "' ", 4, tab.Rows[i]["zy1"].ToString());
                            }
                            else
                            {
                                if (tab.Rows[i]["zy2"].ToString() != "")
                                {
                                  
                                    stab = zk.selectksh_zynum(xxdm, pcdm, tab.Rows[i]["zy2"].ToString());//专业2剩余数量
                                    if (Convert.ToInt32(stab.Rows[0]["lqzynum"]) < Convert.ToInt32(stab.Rows[0]["jhs"])) //录取
                                    {
                                        zk.XX_TD(" ksh='" + tab.Rows[i]["ksh"].ToString() + "' ", 4, tab.Rows[i]["zy2"].ToString());
                                    }
                                    else 
                                    {
                                        //if (Convert.ToBoolean(tab.Rows[i]["zyfc"])) //其他专业服从
                                        //{
                                        //    stab = zk.selectksh_zynum(xxdm, pcdm);//专业剩余数量
                                        //    if (stab.Rows.Count > 0)
                                        //    {
                                        //        zk.XX_TD(" ksh='" + tab.Rows[i]["ksh"].ToString() + "' ", 4, stab.Rows[0]["zydm"].ToString());
                                        //    }
                                        //}
                                    }
                                }
                            }
                            
                        }

                    }
                    else if (tabtop.Rows[0]["jyzy"].ToString() == "2")
                    {
                        tab = zk.selectksh_where(xxdm, pcdm, tdpc, " order by cj desc ");
                        List<int> wlq = new List<int>(); //第一专业未录取的
                        for (int i = 0; i < tab.Rows.Count; i++)
                        {
                            DataTable stab = zk.selectksh_zynum(xxdm, pcdm, tab.Rows[i]["zy1"].ToString());//专业1剩余数量
                            if (Convert.ToInt32(stab.Rows[0]["lqzynum"]) < Convert.ToInt32(stab.Rows[0]["jhs"])) //录取
                            {
                                zk.XX_TD(" ksh='" + tab.Rows[i]["ksh"].ToString() + "' ", 4, tab.Rows[i]["zy1"].ToString());
                            }
                            else
                            {
                                if (tab.Rows[i]["zy2"].ToString() != "")
                                {
                                    wlq.Add(i);
                                }
                            }

                        }
                        if (wlq.Count > 0) 
                        {
                            for (int i = 0; i < wlq.Count; i++)
                            {
                                DataTable stab = zk.selectksh_zynum(xxdm, pcdm, tab.Rows[i]["zy2"].ToString());//专业2剩余数量
                                if (Convert.ToInt32(stab.Rows[0]["lqzynum"]) < Convert.ToInt32(stab.Rows[0]["jhs"])) //录取
                                {
                                    zk.XX_TD(" ksh='" + tab.Rows[i]["ksh"].ToString() + "' ", 4, tab.Rows[i]["zy2"].ToString());
                                }
                                else
                                {
                                    //if (Convert.ToBoolean(tab.Rows[i]["zyfc"])) //其他专业服从
                                    //{
                                    //    stab = zk.selectksh_zynum(xxdm, pcdm);//专业剩余数量
                                    //    if (stab.Rows.Count > 0)
                                    //    {
                                    //        zk.XX_TD(" ksh='" + tab.Rows[i]["ksh"].ToString() + "' ", 4, stab.Rows[0]["zydm"].ToString());
                                    //    }
                                    //}
                                }
                            }
                        }
                    }
                    else  
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('该发档批次没有建议专业!');</script>");
                    }
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('该发档批次没有建议专业!');</script>");
             
            }
            loadToudangInfo2(pcdm, tdpc);
        }
        /// <summary>
        /// 未处理下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddl_wcl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_wcl.SelectedValue.Length>0)
            {
                string str = ddl_wcl.SelectedValue;
                if (str.Length > 0)
                {
                    string pcdm = str.Split('_')[0];
                    string tdpc = str.Split('_')[1];
                    Loadzy(SincciLogin.Sessionstu().UserName,pcdm);
                    loadToudangInfo2(pcdm, tdpc);
                }
            }
        }

        /// <summary>
        /// 未处理下拉框加载该投档信息。
        /// </summary>
        private void loadToudangInfo2(string pcdm, string tdpc)
        {
            BLL_xxlq zk = new BLL_xxlq();

            string Department = SincciLogin.Sessionstu().UserName;
            string xxdm = Department;
            DataTable tab = new DataTable();

            tab = zk.selectksh(xxdm, pcdm, tdpc);
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
            if (repDisplay.Rows.Count == 0)
            {
                repDisplay.EmptyDataText = "暂无数据";
            }

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

            string str = ddl_wcl.SelectedValue;
            if (str.Length > 0)
            {
                string pcdm = str.Split('_')[0];
                string tdpc = str.Split('_')[1];
                loadToudangInfo2(pcdm, tdpc);
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
            string strwcl = ddl_wcl.SelectedValue;
            if (strwcl.Length > 0)
            {
                string pcdm = strwcl.Split('_')[0];
                string tdpc = strwcl.Split('_')[1];
                loadToudangInfo2(pcdm, tdpc);
            }
        }

        protected void repDisplay_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            repDisplay.PageIndex = e.NewPageIndex;

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (ddl_wcl.SelectedValue.Length > 0)
            {

                int UserType = SincciLogin.Sessionstu().UserType;
                string str = ddl_wcl.SelectedValue;
                if (str.Length > 0)
                {
                    string pcdm = str.Split('_')[0];
                    string tdpc = str.Split('_')[1];
                    BLL_xxlq zk = new BLL_xxlq();

                    string Department = SincciLogin.Sessionstu().UserName;
                    string xxdm = Department;
                    DataTable tab = new DataTable();

                    tab = zk.selectksh2(xxdm, pcdm, tdpc);
              
                    GridView gvOrders = new GridView();
                    Response.Clear();
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.Charset = "GB2312";
                    string name = Department;
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