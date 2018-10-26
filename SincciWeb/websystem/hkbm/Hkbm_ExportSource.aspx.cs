using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;
using System.Text;
using System.Data;
using BLL;
using System.IO;

namespace SincciKC.websystem.hkbm
{
    public partial class Hkbm_ExportSource : BPage
    {
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
            return where;
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                if (!IsPostBack)
                {
                    BLL_zk_ksxxgl bllks = new BLL_zk_ksxxgl();
                    #region 创建dbf副本
                    string _f = "hkxx";
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
                    DataSet ds = bllks.ExportHKDBFALL(quanxian());
                    //  string sql = "";
                    StringBuilder sql = new StringBuilder();
                    int num = 1;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        sql.Append("Insert Into " + table + "(ksh,kaoci,xjh,xm,sfzh,bmdxqdm,bmddm,bmdmc,xsbh,bjdm,bz) Values('"
                            + ds.Tables[0].Rows[i]["ksh"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                            + ds.Tables[0].Rows[i]["kaoci"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                            + ds.Tables[0].Rows[i]["xjh"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                            + ds.Tables[0].Rows[i]["xm"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                            + ds.Tables[0].Rows[i]["sfzh"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                            + ds.Tables[0].Rows[i]["bmdxqdm"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                            + ds.Tables[0].Rows[i]["bmddm"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                            + ds.Tables[0].Rows[i]["bmdmc"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                            + ds.Tables[0].Rows[i]["xsbh"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                            + ds.Tables[0].Rows[i]["bjdm"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "','"
                            + ds.Tables[0].Rows[i]["bz"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "');");
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
                    string name = "考生信息导出" + DateTime.Now.ToLongDateString();
                    name = System.Web.HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8);
                    Response.ContentType = "application/x-zip-compressed";
                    Response.Charset = "GB2312";

                    Response.AddHeader("Content-Disposition", "attachment;filename=" + name + ".dbf");
                    string filename = filetemPath;
                    Response.WriteFile(filename);Response.Flush();Response.End();


                    #endregion

                    string E_record = "导出:导出数据!";
                    EventMessage.EventWriteDB(1, E_record);
                    div.InnerHtml = "导出完成!";
                }
            }
            catch (Exception ex)
            {
                DAL.SqlDbHelper_1 db = new DAL.SqlDbHelper_1();
                db.writeErrorInfo(ex.Message);
                div.InnerHtml = "导出失败!";
            }
        }
    }
}