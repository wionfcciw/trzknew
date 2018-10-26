using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using System.Data;
using System.Data.Odbc;
using System.Text;
using System.IO;
namespace SincciKC.websystem.kwgl
{
    public partial class Kczw_Manage : BPage
    {
        /// <summary>
        /// 县区代码控制类
        /// </summary>
        BLL_zk_xqdm BLL_xqdm = new BLL_zk_xqdm();
        /// <summary>
        /// 考点代码控制类
        /// </summary>
        BLL_zk_kd BLL_kd = new BLL_zk_kd();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Permission();
                //绑定PageSize数据
                this.ddlPageSize.DataSource = new config().PageSizelist();
                this.ddlPageSize.DataBind();

                Loadsq();
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
            DataTable tab = BLL_kd.ExecuteProc_View_kczwinfo(strWhere, pageSize, AspNetPager1.CurrentPageIndex, ref RecordCount);
            this.repDisplay.DataSource = tab;
            this.repDisplay.DataBind();

            //分页
            config.AspNetPagerCustomInfoHTML2(AspNetPager1, RecordCount, pageSize);
        }

        /// <summary>
        /// 加载市区信息
        /// </summary>
        private void Loadsq()
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;

            this.ddlxqdm.DataSource = BLL_xqdm.SelectXqdm(Department, UserType);
            this.ddlxqdm.DataTextField = "xqmc";
            this.ddlxqdm.DataValueField = "xqdm";
            this.ddlxqdm.DataBind();

            this.ddlxqdm.Items.Insert(0, new ListItem("-请选择-", ""));
        }


        #region "查询"
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            BindGv();
        }
        /// <summary>
        /// 创建查询条件。
        /// </summary>
        /// <returns></returns>
        private string createWhere()
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;

            string whereStr = "";

            if (!string.IsNullOrEmpty(ddlxqdm.SelectedItem.Value.Trim()))
            {
                whereStr += " bmdxqdm='" + ddlxqdm.SelectedItem.Value.Trim() + "' And ";
            }
            if (!string.IsNullOrEmpty(this.ddlkd.SelectedItem.Value.Trim()))
            {
                whereStr += " bmddm='" + this.ddlkd.SelectedItem.Value.Trim() + "' And ";
            }

            if (!string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                whereStr += " ( ksh='" + config.CheckChar(txtName.Text.Trim().ToString()) + "' Or xm Like '%" + config.CheckChar(txtName.Text.Trim().ToString()) + "%') and ";
            }

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
                    where = " bmdxqdm = '" + Department + "'  ";
                    break;
                //学校用户 
                case 4:
                    where = " bmddm = '" + Department + "' ";
                    break;
                //班级用户 
                case 5:
                    where = " bjdm = '" + Department.Substring(6) + "' and bmddm='" + Department.Substring(0, 6) + "' ";
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
        /// 通过县区查询毕业中学
        /// </summary> 
        protected void ddlxqdm_SelectedIndexChanged(object sender, EventArgs e)
        {
            string xqdm = this.ddlxqdm.SelectedValue;

            this.ddlkd.DataSource = BLL_kd.Select_bmddm(xqdm);
            this.ddlkd.DataTextField = "bmdmc";
            this.ddlkd.DataValueField = "bmddm";
            this.ddlkd.DataBind();

            this.ddlkd.Items.Insert(0, new ListItem("-请选择-", ""));
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(this.ddlkd.SelectedItem.Value.Trim()))
            {
                string str = this.ddlkd.SelectedItem.Value.Trim();
                

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

        protected void btnksh_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() != "" && txtName.Text.Trim().Length == 12)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >window.parent.addTab2('考试证打印', '/websystem/kwgl/Print_ksz.aspx?ksh=" + txtName.Text + "');</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >alert('报名号有误！');</script>");
            }
        }

        /// <summary>
        /// 导出DBF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btndbf_Click(object sender, EventArgs e)
        {
            BLL_zk_kd bllks = new BLL_zk_kd();
            #region 创建dbf副本
            string _f = "kd";
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
            DataTable ds = bllks.Select_DBF(quanxian());
            //  string sql = "";
            StringBuilder sql = new StringBuilder();
            int num = 1;
            for (int i = 0; i < ds.Rows.Count; i++)
            {

                sql.Append("Insert Into " + table + "(ksh,xm,xbmc,zkh,kddm,kdmc,bmddm,bmdmc) Values('"
                    + ds.Rows[i]["zkzh"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Rows[i]["xm"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Rows[i]["xbmc"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Rows[i]["ksh"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Rows[i]["kddm"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Rows[i]["kdmc"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Rows[i]["bmddm"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                    + ds.Rows[i]["bmdmc"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "');");
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
            string name = "考场编排信息导出" + DateTime.Now.ToLongDateString();
            name = System.Web.HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8);
            Response.ContentType = "application/x-zip-compressed";
            Response.Charset = "GB2312";

            Response.AddHeader("Content-Disposition", "attachment;filename=" + name + ".dbf");
            string filename = filetemPath;
            Response.WriteFile(filename);Response.Flush();Response.End();


            #endregion
        }
        #region "管理部门权限"
        /// <summary>
        /// 管理部门权限
        /// </summary> 
        private string quanxian()
        {
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
                //区招生办
                case 3:
                    where = " bmdxqdm = '" + Department + "'  ";
                    break;
                //学校用户 
                case 4:
                    where = "1<>1 ";
                    break;
                //班级用户 
                case 5:
                    where = "1<>1 ";
                    break;
                default:
                    where = " 1<>1";
                    break;
            }
            return where;
        }
        #endregion
    }
}