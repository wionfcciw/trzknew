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
namespace SincciKC.websystem.zysz
{
    public partial class Zsjh_Manage : BPage
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
            DataTable tab = bllzsjh.ExecuteProc(strWhere, pageSize, AspNetPager1.CurrentPageIndex, ref RecordCount);
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
            // 新增
            if (!new Method().CheckButtonPermission(PopedomType.A4))
            {
                this.btnNew.Visible = false;
            }
            //删除
            if (!new Method().CheckButtonPermission(PopedomType.A8))
            {
                this.btnDelete.Visible = false;
            }
            //导入
            if (!new Method().CheckButtonPermission(PopedomType.A16))
            {
                btnExcelFileImport.Visible = false;
            }
                //导chu
            if (!new Method().CheckButtonPermission(PopedomType.A32))
            {
                btnDBF.Visible = false;
                btnExport.Visible = false;
            }
            
            ////排序
            //if (!new Method().CheckButtonPermission(PopedomType.Orderby))
            //{
            //    this.Enabled = false;
            //}
            ////打印
            //if (!new Method().CheckButtonPermission(PopedomType.Print))
            //{
            //    this.btnPrint.Enabled = false;
            //}
            ////备用A
            //if (!new Method().CheckButtonPermission(PopedomType.A))
            //{
            //    this.btnA.Enabled = false;
            //}
            ////备用B
            //if (!new Method().CheckButtonPermission(PopedomType.B))
            //{
            //    this.btnB.Enabled = false;
            //}
        }
        #endregion

        /// <summary>
        /// 点击删除。
        /// </summary>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            bool result = bllzsjh.DeleteDataByIDS(hfDelIDS.Value.Split(',').ToList());
            string E_record = "删除：招生计划:" + hfDelIDS.Value;

            if (result)
            {
                EventMessage.EventWriteDB(1, E_record);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                    "<script>ymPrompt.succeedInfo({message:'操作成功！' ,title:'操作提示'});setTimeout(function(){window.location.href=window.location.href;},1000);</script>");
            }
            else
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                    "<script>ymPrompt.alert({message:'操作失败！' ,title:'提示'});</script>");
        }

      

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

        protected void btnExcelFileImport_Click(object sender, EventArgs e)
        {
            if (fuExcelFileImport.HasFile)
            {
                //判断文件格式
                string fileExtension = Path.GetExtension(fuExcelFileImport.FileName).ToLower();
                string[] allowedExtensions = { ".xls", ".xlsx" };

                if (allowedExtensions.Count(x => x == fileExtension) == 0)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                        "<script>ymPrompt.alert({message:'操作失败！,原因是：上传的文件不是Excel格式文件。' ,title:'提示'});</script>");
                    BindGv();
                    return;
                }

                string path = Server.MapPath("~/tmpUpLoadFile/") + config.FormatDateToStringId(DateTime.Now) + fileExtension;
                fuExcelFileImport.PostedFile.SaveAs(path);

                string result = bllzsjh.ImportExcelData(path );
                string E_record = "导入：招生计划:" + config.FormatDateToStringId(DateTime.Now) + fileExtension;
                EventMessage.EventWriteDB(1, E_record);
              
                msgWindow.InnerHtml = result;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                    "<script>$(function () {if(confirm('导入完成是否需要查看导入日志？'))$('#msgWindow').window('open');});</script>");
            }

            BindGv();
        }

        /// <summary>
        /// 导出数据 xls
        /// </summary> 
        protected void btnExport_Click(object sender, EventArgs e)
        {
            GridView gvOrders = new GridView();
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "GB2312";
            string name = "计划管理" + DateTime.Now.ToLongDateString();
            name = System.Web.HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8);
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + name + ".xls");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");  //设置输出流为简体中文 
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            gvOrders.AllowPaging = false;
            gvOrders.AllowSorting = false;
            gvOrders.DataSource = bllzsjh.ExportData();
            gvOrders.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            gvOrders.DataBind();
            gvOrders.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();

        }
        /// <summary>
        /// DBF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDBF_Click(object sender, EventArgs e)
        {
            #region 创建dbf副本
            string _f = "zsjh";
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

            #endregion

            #region 写入
            DataTable ds = bllzsjh.ExportData();
            //  string sql = "";
            StringBuilder sql = new StringBuilder();
            int num = 1;
            for (int i = 0; i < ds.Rows.Count; i++)
            {

                sql.Append("Insert Into " + table + "(pcdm,xqdm,xqmc,xxdm,zsxxmc,zydm,zymc,xzdm,xzmc,jhs,xxlbdm,bz) Values('"
                    + ds.Rows[i]["pcdm"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Rows[i]["xqdm"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Rows[i]["xqmc"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Rows[i]["xxdm"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Rows[i]["zsxxmc"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Rows[i]["zydm"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Rows[i]["zymc"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                     + ds.Rows[i]["xzdm"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                   + ds.Rows[i]["xzmc"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                     + ds.Rows[i]["jhs"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                   + ds.Rows[i]["xxlbdm"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Rows[i]["bz"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "');");
                if (ds.Rows.Count > 10000)
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
                    if (i == ds.Rows.Count - 1)
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
            string name = "计划管理" + DateTime.Now.ToLongDateString();
            name = System.Web.HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8);
            Response.ContentType = "application/x-zip-compressed";
            Response.Charset = "GB2312";

            Response.AddHeader("Content-Disposition", "attachment;filename=" + name + ".dbf");
            string filename = filetemPath;
            Response.WriteFile(filename);Response.Flush();Response.End();


            #endregion
        }
    }
}