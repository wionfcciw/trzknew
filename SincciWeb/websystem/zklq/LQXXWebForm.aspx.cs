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
using System.Data.Odbc;
using System.IO;

namespace SincciKC
{
  
    /// <summary>
    /// 录取查询
    /// </summary>
    public partial class LQXXWebForm : BPage
    {
        public int page = Convert.ToInt32(config.sink("page", config.MethodType.Get, 255, 1, config.DataType.Int));
        public int pagesize = Convert.ToInt32(config.sink("pagesize", config.MethodType.Get, 255, 1, config.DataType.Int));
        /// <summary>
        /// 页面加载。
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Permission();
                //绑定PageSize数据
                //this.ddlPageSize.DataSource = new config().PageSizelist();
                //this.ddlPageSize.DataBind();

                //if (pagesize > 0)
                //{
                //    this.ddlPageSize.SelectedValue = pagesize.ToString();
                //}
                //else
                //{
                //    pagesize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
                //}
                loadPcInfo();
              
               loadToudangInfo();
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
            //回收
            if (!new Method().CheckButtonPermission(PopedomType.A4))
            {
                btn_hs.Visible = false;
            }
            //指录
            if (!new Method().CheckButtonPermission(PopedomType.A8))
            {
                btn_lq.Visible = false;
            }
            //打印录取
            if (!new Method().CheckButtonPermission(PopedomType.A16))
            {
                btn_print_lq.Visible = false;   
            }
            //打印审核
            if (!new Method().CheckButtonPermission(PopedomType.A32))
            {
                btn_spb.Visible = false;
            }
            //导出
            if (!new Method().CheckButtonPermission(PopedomType.A64))
            {
                btn_daoclq.Visible = false;
            }
            //学校统计
            if (!new Method().CheckButtonPermission(PopedomType.A128))
            {
                btn_xxtj.Visible = false;
            }
             //导出所有
            if (!new Method().CheckButtonPermission(PopedomType.A256))
            {
                btn_all.Visible = false;
            }
            //查看轨迹
            if (!new Method().CheckButtonPermission(PopedomType.A512))
            {
                btn_selgj.Visible = false;
            }
        }
        #endregion
        /// <summary>
        /// 加载批次信息。
        /// </summary>
        private void loadPcInfo( )
        {
            BLL_LQK_Ks_Xx zk = new BLL_LQK_Ks_Xx();
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string where = "";
            switch (UserType)
            {
                //系统管理员
                case 1:
                    where = where + " xqdm='500'   ";
                    break;
                //市招生办
                case 2:

                    where = " 1=1 ";

                    break;
                case 3:
                    where = where + " xqdm='500'   ";
                    break;
                //招生学校
                case 9:
                case 8:
                  where = where + " xqdm='500'   ";      break;
                default:
                    where = " 1<>1";
                    break;
            }

            DataTable tab = zk.selectPcdm(where, Cwhere());
            this.ddlXpcInfo.DataSource = tab;
            this.ddlXpcInfo.DataTextField = "xpc_mc";
            this.ddlXpcInfo.DataValueField = "xpcid";
            this.ddlXpcInfo.DataBind();
            this.ddlXpcInfo.Items.Insert(0, new ListItem("-请选择-", ""));
        }
        /// <summary>
        /// 权限
        /// </summary>
        /// <returns></returns>
        private string Cwhere()
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().UserName;
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
                case 3:
                    where = " 1=1 ";
                    break;
                //招生学校
                case 9:
                case 8:
                    where = "  xxdm='" + Department + "' ";
                    break;
                default:
                    where = " 1<>1";
                    break;
            }
            return where;
        }

        /// <summary>
        /// 权限
        /// </summary>
        /// <returns></returns>
        private string Wwhere()
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().UserName;
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
                case 3:
                    where = " xqdm='" + Department + "' ";
                    break;
                //招生学校
                case 9:
                case 8:
                    where = "  xxdm='" + Department + "' ";
                    break;
                default:
                    where = " 1<>1";
                    break;
            }
            return where;
        }
        /// <summary>
        /// 加载该学校投档信息。
        /// </summary>
        private void loadToudangInfo()
        {

            BLL_LQK_Ks_Xx zk = new BLL_LQK_Ks_Xx();
            string str = "";
            string pcdm = "";
            string xxdm = dllxx.SelectedValue;
            string type = ddlzt.SelectedValue; //状态
            string lx = ddllx.SelectedValue; //类型
            string strksh = "";
            if (ddlXpcInfo.SelectedValue.Length > 0)
            {
                string s = this.ddlXpcInfo.SelectedItem.Text;
                int begin = 0;
                int end = 0;
                begin = s.IndexOf('[');
                end = s.IndexOf(']');
                pcdm = s.Substring(begin + 1, end - begin - 1);
            }
            if (xxdm.Length > 0)
            {
                str += " lqxx='" + xxdm + "' and ";
            }
            if (type.Length > 0)
            {
                str += " td_zt=" + type + " and ";
            }
            if (lx.Length > 0)
            {
                str += " sftzs=" + lx + " and ";
             
            }
            if (pcdm.Length > 0)
            {
                str += " pcdm='" + pcdm + "' and ";
            }
            if (txtName.Text.Trim().Length > 0)
            {
                str += " (ksh='" + txtName.Text.Trim() + "' or zkzh='" + txtName.Text.Trim() + "') and ";
            }
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().UserName;
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
             
                case 3:

                    where = "  xqdm='" + SincciLogin.Sessionstu().U_department + "'  ";
                    break;
                //招生学校
                case 9:
                case 8:
                    where = "  lqxx='" + Department + "' and td_zt<>1 ";
                    break;
                default:
                    where = " 1<>1";
                    break;
            }
            str = str + where;

            DataTable tab = new DataTable();
            //if (page == 0)
            //    page = 1;
            //int RecordCount = 0;

            //pagesi ze = Convert.ToInt32(this.ddlPageSize.SelectedValue);

            tab = zk.selectksh(str);
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
                 ViewState["SortDirection"].ToString();
                this.repDisplay.DataSource = tab;
            }
            this.repDisplay.DataBind();
            //分页

        // config.AspNetPagerCustomInfoHTML2(AspNetPager1, RecordCount, pagesize);
           
        }
       
        /// <summary>
        /// 查询
        /// </summary>
        protected void btn_beginTd_Click(object sender, EventArgs e)
        {
            

             loadToudangInfo();
        }

         
       
      

        protected void ddlXpcInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlXpcInfo.SelectedValue.Length > 0)
            {
                Loadxx();
            }
        }
        /// <summary>
        /// 加载学校信息
        /// </summary>
        private void Loadxx()
        {
            string str = this.ddlXpcInfo.SelectedItem.Text;
            int begin = 0;
            int end = 0;
            begin = str.IndexOf('[');
            end = str.IndexOf(']');
            string pcdm = str.Substring(begin + 1, end - begin - 1);
            BLL_LQK_Ks_Xx zk = new BLL_LQK_Ks_Xx();
            DataTable tab = zk.Select_zk_zsxx(pcdm, Wwhere());
            this.dllxx.DataSource = tab;
            this.dllxx.DataTextField = "zsxxmc";
            this.dllxx.DataValueField = "xxdm";
            this.dllxx.DataBind();
            this.dllxx.Items.Insert(0, new ListItem("-请选择-", ""));
        }

        //protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        //{
        //    loadToudangInfo();
        //}

        //protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    pagesize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
        //    loadToudangInfo();
        //}

      
        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_hs_Click(object sender, EventArgs e)
        {
            if (Request.Form["CheckBox1"] != null)
            {
                string ids = Request.Form["CheckBox1"].ToString();
                if (ids.Length > 0)
                {
                    BLL_LQK_Ks_Xx zk = new BLL_LQK_Ks_Xx();
                    string where =  "  ksh in(" + ids + ")";
                    if (zk.ksh_hs(where,where))
                    {
                          string   E_record = "回收: 考生数据：" + ids + "";
                        EventMessage.EventWriteDB(1, E_record);
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('操作成功！')</script>");
                        loadToudangInfo();
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('操作失败！')</script>");
                    }

                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择您所需要回收的用户！')</script>");

            }
            loadToudangInfo();
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
            loadToudangInfo();
        }

      

        protected void btn_lq_Click(object sender, EventArgs e)
        {
          
            if (Request.Form["CheckBox1"] != null)
            {
                string[] ids = Request.Form["CheckBox1"].ToString().Split(',');

                if (ids.Length == 1)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>opLq('" + ids[0] + "', '指录') ;</script>");
                    
                }
                else if (ids.Length > 1)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('不能对多个考生进行修改指录!');</script>");
                }

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择需要指录的考生!');</script>");

            }
        }
        /// <summary>
        /// 打印录取名册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_print_lq_Click(object sender, EventArgs e)
        {
           
            if (ddlXpcInfo.SelectedValue.Length > 0)
            {
                string s = this.ddlXpcInfo.SelectedItem.Text;
                int begin = 0;
                int end = 0;
                begin = s.IndexOf('[');
                end = s.IndexOf(']');
                string pcdm = s.Substring(begin + 1, end - begin - 1);
                if (dllxx.SelectedValue.Length > 0)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script> hideTest('录取名册打印','" + pcdm + "','" + dllxx.SelectedValue + "','" + ddllx.SelectedValue + "','" + listpc.SelectedValue + "');</script>");

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择需要打印的学校!');</script>");

                }
            
            }
      
        }

        protected void btn_spb_Click(object sender, EventArgs e)
        {

            if (ddlXpcInfo.SelectedValue.Length > 0)
            {
                string s = this.ddlXpcInfo.SelectedItem.Text;
                int begin = 0;
                int end = 0;
                begin = s.IndexOf('[');
                end = s.IndexOf(']');
                string pcdm = s.Substring(begin + 1, end - begin - 1);
                if (dllxx.SelectedValue.Length > 0)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script> hideTest2('审批表打印','" + pcdm + "','" + dllxx.SelectedValue + "');</script>");

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择需要打印的学校!');</script>");

                }

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择需要打印的学校!');</script>");

            }
        }
        /// <summary>
        /// 导出名册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_daoclq_Click(object sender, EventArgs e)
        {
            if (ddlXpcInfo.SelectedValue.Length > 0)
            {

                int UserType = SincciLogin.Sessionstu().UserType;
                string xq = "";
                switch (UserType)
                {
                    case 3:
                        xq = " and xqdm='" + SincciLogin.Sessionstu().U_department + "'";
                        break;

                    default:
                        xq = " and xqdm='500'";
                        break;
                }
                string s = this.ddlXpcInfo.SelectedItem.Text;
                int begin = 0;
                int end = 0;
                begin = s.IndexOf('[');
                end = s.IndexOf(']');
                string pcdm = s.Substring(begin + 1, end - begin - 1);

                if (dllxx.SelectedValue.Length > 0)
                {
                    string lx = "";
                    if (ddllx.SelectedValue!="")
                    {
                        lx = " and sftzs=" + ddllx.SelectedValue;
                    }
                    string sfpc = "";
                    if (listpc.SelectedValue!="")
                    {
                        sfpc = " and sfpc='" + listpc.SelectedValue+"'";
                    }
                    string strDate1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                    BLL_LQK_Ks_Xx zk = new BLL_LQK_Ks_Xx();
                    string where = " pcdm='" + pcdm + "' and lqxx='" + dllxx.SelectedValue + "' and td_zt=5 " + xq + lx + sfpc;
                    DataTable dt = zk.Select_Print_lq(where);
                    if (dt == null)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('暂无录取信息!');</script>");
                        return;
                    }
                    if (dt.Rows.Count == 0)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('暂无录取信息!');</script>");
                        return;
                    }
                    GridView gvOrders = new GridView();
                    Response.Clear();
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.Charset = "GB2312";
                    string name = dllxx.SelectedItem.Text;
                    name = System.Web.HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8);
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + name + ".xls");
                    Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");  //设置输出流为简体中文 
                    Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
                    gvOrders.AllowPaging = false;
                    gvOrders.AllowSorting = false;
                    gvOrders.DataSource = dt;
                    gvOrders.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                    gvOrders.DataBind();
                    gvOrders.RenderControl(hw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择需要导出的学校!');</script>");

                }

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择需要导出的学校!');</script>");

            }
       
        }

        protected void repDisplay_PageIndexChanged(object sender, EventArgs e)
        {
            loadToudangInfo();
        }

        protected void repDisplay_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            repDisplay.PageIndex = e.NewPageIndex;
        }

        protected void btn_xxtj_Click(object sender, EventArgs e)
        {
            if (ddlXpcInfo.SelectedValue.Length > 0)
            {
                string s = this.ddlXpcInfo.SelectedItem.Text;
                int begin = 0;
                int end = 0;
                begin = s.IndexOf('[');
                end = s.IndexOf(']');
                string pcdm = s.Substring(begin + 1, end - begin - 1);
                if (dllxx.SelectedValue.Length > 0)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script> hideTest3('" + dllxx.SelectedValue + "学校统计信息','" + pcdm + "','" + dllxx.SelectedValue + "');</script>");

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择需要查看的学校!');</script>");

                }

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择需要查看的学校!');</script>");

            }
        }
        /// <summary>
        /// 导出所有
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_all_Click(object sender, EventArgs e)
        {
            #region 创建dbf副本
            try
            {

            string _f = "lqk";
            string tbl = Server.MapPath("/Template/" + _f + ".dbf");
            string fileTemName = _f + "_" + DateTime.Now.ToString("ffff");
            string filetemPath = Server.MapPath("/Temp/" + fileTemName + ".dbf");
            File.Copy(tbl, filetemPath, true);

            #endregion

            #region 创建连接
            OdbcConnection conn = new System.Data.Odbc.OdbcConnection();
            string table = filetemPath;
            string connStr = @"Driver={Microsoft Visual FoxPro Driver};SourceType=DBF;SourceDB=" + table + ";Exclusive=No;NULL=NO;Collate=Machine;BACKGROUNDFETCH=NO;DELETED=NO";
            conn.ConnectionString = connStr;
            conn.Open();
            // string strOdbcConn = @" DRIVER=Microsoft Visual FoxProDriver;UID=;Collate=Machine;BackgroundFetch=Yes;      Exclusive=No;SourceType=DBF;SourceDB=" + strFilePath + ";";
            //OdbcConnection odbcConn = new OdbcConnection(strOdbcConn);
            //string sqlInsert = "Insert Into table1(DateFrom, Num) Values({^2005-09-10},10)";
            //OdbcCommand odbcComm = new OdbcCommand(sqlInsert, odbcConn);
            //odbcComm.Connection.Open();
            //odbcComm.ExecuteNonQuery();
            //odbcConn.Close();
            #endregion

            #region 写入
            BLL_LQK_Ks_Xx zk = new BLL_LQK_Ks_Xx();
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
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

                case 3:

                    where = "  LEFT( ksh,3)='" + SincciLogin.Sessionstu().U_department + "'  ";
                    break;
              
                default:
                    where = " 1<>1";
                    break;
            }

            DataSet ds = zk.ExportDBFALL(where );
            //  string sql = "";
            StringBuilder sql = new StringBuilder();
            int num = 1;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                sql.Append("Insert Into " + table + " (ksh,zkzh,xm,xbmc,bmdxqdm,bmddm,bmdmc,sfzh,lqxx,lqxxmc,yw,sx,yy,lkzh,wkzh,dsdj,ty,zhdj,jf,zf,sftzs, xjtype,jzfp,bklb,kslbmc,pcdm) Values('"
                    + ds.Tables[0].Rows[i]["ksh"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Tables[0].Rows[i]["zkzh"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Tables[0].Rows[i]["xm"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Tables[0].Rows[i]["xbmc"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Tables[0].Rows[i]["bmdxqdm"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Tables[0].Rows[i]["bmddm"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Tables[0].Rows[i]["bmdmc"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Tables[0].Rows[i]["sfzh"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Tables[0].Rows[i]["lqxx"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Tables[0].Rows[i]["lqxxmc"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "',"
                    + ds.Tables[0].Rows[i]["yw"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + ","
                    + ds.Tables[0].Rows[i]["sx"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + ","
                    + ds.Tables[0].Rows[i]["yy"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + ","
                    + ds.Tables[0].Rows[i]["lkzh"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + ",'"
                    + ds.Tables[0].Rows[i]["wkzh"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Tables[0].Rows[i]["dsdj"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "',"
                    + ds.Tables[0].Rows[i]["ty"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + ",'"
                    + ds.Tables[0].Rows[i]["zhdj"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "',"
                    + ds.Tables[0].Rows[i]["jf"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + ","
                    + ds.Tables[0].Rows[i]["zf"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + ",'"
                    + ds.Tables[0].Rows[i]["sftzs"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Tables[0].Rows[i]["xjtype"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Tables[0].Rows[i]["jzfp"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "',"
                    + ds.Tables[0].Rows[i]["bklb"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + ",'"
                     + ds.Tables[0].Rows[i]["kslbmc"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Tables[0].Rows[i]["sfpc"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "');");
            if (ds.Tables[0].Rows.Count > 10000)
                {
                    if (i == num * 10000)
                    {
                        num++;
                        OdbcCommand cmd = new OdbcCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = sql.ToString();
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        sql = new StringBuilder();
                    }
                    if (i == ds.Tables[0].Rows.Count - 1)
                    {
                        OdbcCommand cmd = new OdbcCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = sql.ToString();
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        sql = new StringBuilder();
                    }
                }
                else
                {
                    OdbcCommand cmd = new OdbcCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = sql.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    sql = new StringBuilder();
                }




            }

            ds.Clear();
            ds.Dispose();
            conn.Close();
            conn.Dispose();
            #endregion

            #region 弹出导出对话框
            string name = "录取信息导出" + DateTime.Now.ToLongDateString();
            name = System.Web.HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8);
            Response.ContentType = "application/x-zip-compressed";
            Response.Charset = "GB2312";

            Response.AddHeader("Content-Disposition", "attachment;filename=" + name + ".dbf");
            string filename = filetemPath;
            Response.WriteFile(filename);Response.Flush();Response.End();


            #endregion

            string E_record = "导出:导出录取库!";
            EventMessage.EventWriteDB(1, E_record);

            }
            catch (Exception rx)
            {


            }
        }
        /// <summary>
        /// 查看轨迹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_selgj_Click(object sender, EventArgs e)
        {

            if (Request.Form["CheckBox1"] != null)
            {
                string[] ids = Request.Form["CheckBox1"].ToString().Split(',');

                if (ids.Length == 1)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script> hideTest4('查看考生轨迹','" + ids[0] + "');</script>");

                }
                else if (ids.Length > 1)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('不能多个考生!');</script>");
                }

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择需要查看的考生!');</script>");

            }
         
        }

      
     
    }
}