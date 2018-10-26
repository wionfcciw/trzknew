using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using System.Data.Odbc;
using System.Text;
using System.IO;

namespace SincciKC
{
    public partial class ByXxLqInfo : System.Web.UI.Page
    {
        public int page = Convert.ToInt32(config.sink("page", config.MethodType.Get, 255, 1, config.DataType.Int));
        public int pagesize = Convert.ToInt32(config.sink("pagesize", config.MethodType.Get, 255, 1, config.DataType.Int));

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
                if (Request.QueryString["xqdm"] != null && Request.QueryString["xxdm"] != null && Request.QueryString["pcdm"] != null)
                {
                    this.hf_xqdm.Value = Request.QueryString["xqdm"].ToString();
                    this.hf_zsxxdm.Value = Request.QueryString["xxdm"].ToString();
                    this.hf_pcdm.Value = Request.QueryString["pcdm"].ToString();
                    //if (hf_pcdm.Value.Substring(0, 1) == "1")
                    //{
                    //    divs.en = true;
                    //}
                    //else
                    //{
                    //    divs.Visible = false;
                    //}
                    LoadData();
                }
            }
        }
        private BLL_byxxlqXx byxx = new BLL_byxxlqXx();
        /// <summary>
        /// 加载数据当前招生学校在当前县区，当前批次招生的所有考生信息。
        /// </summary>
        private void LoadData()
        {
            string where = ddl_xx.SelectedValue;
            if (where == "0")
            {
                where = " sftzs in (1,2,3)";
            }
            else
                where = " sftzs in (" + ddl_xx.SelectedValue + ")";
            DataTable tab = byxx.SelectKsLqXx(this.hf_pcdm.Value.Trim(), this.hf_xqdm.Value.Trim(), this.hf_zsxxdm.Value.Trim(),where);
            if (ViewState["SortDirection"] == null)
                repDisplay.DataSource = tab;
            else
            {
                tab.DefaultView.Sort = ViewState["SortExpression"].ToString() + " " +
                 ViewState["SortDirection"].ToString() + ",cj desc";
                this.repDisplay.DataSource = tab;
            }
            this.repDisplay.DataSource = tab;
            this.repDisplay.DataBind();
        }

        protected void btn_beginTd_Click(object sender, EventArgs e)
        {
            
            string str = "";

            string strksh = "";
            if (Request.Form["CheckBox1"] != null)
            {
                strksh = Request.Form["CheckBox1"].ToString();

                if (strksh.Length > 0)
                {
                    str = str + "  ksh in(" + strksh + ") ";
                    byxx.XX_TD(str, 5, 4);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择需要录取的考生!');</script>");
            }
            LoadData();
        }

        protected void btnCancel_Td_Click(object sender, EventArgs e)
        {
         
            string str = "";
            string strksh = "";
            if (Request.Form["CheckBox1"] != null)
            {
                strksh = Request.Form["CheckBox1"].ToString();

                if (strksh.Length > 0)
                {
                    str = str + "  ksh in(" + strksh + ") ";
                    byxx.XX_TD(str, 1, 0);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择需要回收的考生!');</script>");
            }
            LoadData();
        }

        protected void btnPl_Fd_Click(object sender, EventArgs e)
        {
            BLL_xqlq zk = new BLL_xqlq();
            DataTable tab = new DataTable();
            string xxdm = ""; string pcdm = ""; string xqdm = "";
            xqdm = Request.QueryString["xqdm"].ToString();
            xxdm = Request.QueryString["xxdm"].ToString();
            pcdm = Request.QueryString["pcdm"].ToString();
            string where = ddl_xx.SelectedValue;
            if (where == "0")
            {
                where = " sftzs in (1,2,3)";
            }else
                where = " sftzs in (" + ddl_xx.SelectedValue + ")";
            tab = byxx.SelectKsLqXx(this.hf_pcdm.Value.Trim(), this.hf_xqdm.Value.Trim(), this.hf_zsxxdm.Value.Trim(), where);
            if (tab != null)
            {
                if (tab.Rows.Count > 0)
                {
                    if (tab.Select(" xq_zt in (0) ").Length > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('还有考生尚未操作!');</script>");
                    }
                    else
                    {
                        if (byxx.XX_UP_PASS(xxdm, pcdm, xqdm))
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
            LoadData();
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

            LoadData();
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
            LoadData();
        }

        protected void repDisplay_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            repDisplay.PageIndex = e.NewPageIndex;

        }

        protected void btn_dbf_Click(object sender, EventArgs e)
        {
            #region 创建dbf副本
            try
            {

                string _f = "lqk_yt";
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
                DataSet ds = byxx.SelectTDLqXx(this.hf_pcdm.Value.Trim(), this.hf_xqdm.Value.Trim(), this.hf_zsxxdm.Value.Trim());
                //  string sql = "";
                StringBuilder sql = new StringBuilder();
                int num = 1;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    sql.Append("Insert Into " + table + " (ksh,xm,xqdm,lqxx,lqzy,pcdm,cj,td_zt,td_pc,zysx,yw,sx,yy,ty,wkzh,dsdj,zhdj,lkzh,zf,sftzs) Values('"
                        + ds.Tables[0].Rows[i]["ksh"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                        + ds.Tables[0].Rows[i]["xm"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                        + ds.Tables[0].Rows[i]["xqdm"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                        + ds.Tables[0].Rows[i]["lqxx"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                        + ds.Tables[0].Rows[i]["lqzy"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                        + ds.Tables[0].Rows[i]["pcdm"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "',"
                        + ds.Tables[0].Rows[i]["cj"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + ","
                        + ds.Tables[0].Rows[i]["td_zt"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + ",'"
                        + ds.Tables[0].Rows[i]["td_pc"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                        + ds.Tables[0].Rows[i]["zysx"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "',"
                        + ds.Tables[0].Rows[i]["yw"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + ","
                        + ds.Tables[0].Rows[i]["sx"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + ","
                        + ds.Tables[0].Rows[i]["yy"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + ","
                           + ds.Tables[0].Rows[i]["ty"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + ",'"
                               + ds.Tables[0].Rows[i]["wkzh"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                                   + ds.Tables[0].Rows[i]["dsdj"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                                       + ds.Tables[0].Rows[i]["zhdj"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "',"
                                                                              + ds.Tables[0].Rows[i]["lkzh"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + ","
                                                                               + ds.Tables[0].Rows[i]["cj"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + ","
                        + ds.Tables[0].Rows[i]["sftzs"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + ");");
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
                string name = this.hf_zsxxdm.Value.Trim() + "投档信息导出" + DateTime.Now.ToLongDateString();
                name = System.Web.HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8);
                Response.ContentType = "application/x-zip-compressed";
                Response.Charset = "GB2312";

                Response.AddHeader("Content-Disposition", "attachment;filename=" + name + ".dbf");
                string filename = filetemPath;
                Response.WriteFile(filename);Response.Flush();Response.End();


                #endregion

                string E_record = "导出:导出投档信息!";
                EventMessage.EventWriteDB(1, E_record);

            }
            catch (Exception rx)
            {


            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string str = "";

            str = " lqxx='" + this.hf_zsxxdm.Value.Trim() + "' and  pcdm='" + this.hf_pcdm.Value.Trim() + "' and xqdm='" + this.hf_xqdm.Value.Trim() + "' ";
            byxx.XX_TD(str, 5, 4);
            LoadData();
        }

        protected void btn_sel_Click(object sender, EventArgs e)
        {
            LoadData();
        }

    }
}