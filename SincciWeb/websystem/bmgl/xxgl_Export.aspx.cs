using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Collections;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Text;
using System.Data.OleDb;
namespace SincciKC.websystem.bmgl
{
    public partial class xxgl_Export : BPage
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
           

        }
       
        protected void btnEnter_Click(object sender, EventArgs e)
        {
             
                try
                {
                   

                    if (rdoexcel.Checked)
                    {
                       
                        string mubiao = "";
                        string ss = DateTime.Now.ToString().Replace(":", "").Replace("-", "").Replace(" ", "").Replace("/", "");
                        string moban = Server.MapPath(String.Format("/Template/daochuksh.xls"));
                        mubiao = Server.MapPath(String.Format("/tmpUpLoadFile/" + ss + ".xls"));
                        File.Copy(moban, mubiao, true);
                        //  string path = Server.MapPath(String.Format("/Template/daochuksh.xls"));
                        string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + mubiao + ";" + "Extended Properties=Excel 8.0;";
                        OleDbConnection conn = new OleDbConnection(strConn);
                        try
                        {
                            conn.Open();
                            DataTable dt = new BLL_zk_ksxxgl().ExportEXCELKsh(quanxian()).Tables[0];
                            StringBuilder sql = new StringBuilder();
                            int num = 1;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                sql.Append("INSERT INTO [sheet1$]  VALUES('" + dt.Rows[i]["报名号"] + "','" + dt.Rows[i]["考次"] + "','" + dt.Rows[i]["学籍号"] + "','" + dt.Rows[i]["身份证号"] + "','" + dt.Rows[i]["姓名"] + "','" + dt.Rows[i]["县区代码(毕业中学所在县区)"] + "','" + dt.Rows[i]["学校(毕业中学)"] + "','" + dt.Rows[i]["班级"] + "','" + dt.Rows[i]["学生编码"] + "','" + dt.Rows[i]["备注"].ToString().Replace("\n", "").Replace("\r", "").Replace("'", "’") + "')");
                            
                                    OleDbCommand cmd = new OleDbCommand();
                                    cmd.Connection = conn;
                                    cmd.CommandText = sql.ToString();
                                    cmd.CommandType = CommandType.Text;
                                    cmd.ExecuteNonQuery();
                                    sql = new StringBuilder();
                             
                            }
                            conn.Close();
                            conn.Dispose();
                         //   Response.Redirect("/tmpUpLoadFile/" + ss + ".xls");
                            string name = "考生信息导出" + DateTime.Now.ToLongDateString();
                            name = System.Web.HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8);
                            Response.ContentType = "application/x-excel";
                            Response.Charset = "GB2312";

                            Response.AddHeader("Content-Disposition", "attachment;filename=" + name + ".xls");
                            string filename = mubiao;
                            Response.WriteFile(filename);Response.Flush();Response.End();
                        }
                        catch (System.Data.OleDb.OleDbException ex)
                        {
                            System.Diagnostics.Debug.WriteLine("写入Excel发生错误：" + ex.Message);
                        }
                        finally
                        {
                            conn.Close();
                            conn.Dispose();
                        }     

                    }
                    else
                    {
                        //路径不能包含括号
                        BLL_zk_ksxxgl bllks = new BLL_zk_ksxxgl();
                      
                        string _f = "ydxlxxk";
                        string tbl = Server.MapPath("/Template/" + _f + ".dbf");
                        string fileTemName = _f + "_" + DateTime.Now.ToString("ffff");
                        string filetemPath = Server.MapPath("/Temp/" + fileTemName + ".dbf");
                        File.Copy(tbl, filetemPath, true);
                        OdbcConnection conn = new System.Data.Odbc.OdbcConnection();
                        string table = filetemPath;
                        string connStr = @"Driver={Microsoft Visual FoxPro Driver};SourceType=DBF;SourceDB=" + table + ";Exclusive=No;NULL=NO;Collate=Machine;BACKGROUNDFETCH=NO;DELETED=NO";
                        try
                        {
                         
                           
                            DataSet ds = bllks.ExportDBFKsh(quanxian());
                            //  string sql = "";
                            StringBuilder sql = new StringBuilder();
                            int num = 1;
                            conn.ConnectionString = connStr;
                            conn.Open();
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {

                                sql.Append("Insert Into " + table + "(kaoci,xjh,bmdxqdm,ksh,xm,xb,sfzh,bmddm,bmdmc,byzxdm,byzxmc,bjdm,xsbh,bz) Values('"
                                    + ds.Tables[0].Rows[i]["kaoci"] + "','"
                                    + ds.Tables[0].Rows[i]["xjh"] + "','"
                                    + ds.Tables[0].Rows[i]["bmdxqdm"] + "','"
                                    + ds.Tables[0].Rows[i]["ksh"] + "','"
                                    + ds.Tables[0].Rows[i]["xm"] + "','"
                                    + ds.Tables[0].Rows[i]["xb"] + "','"
                                    + ds.Tables[0].Rows[i]["sfzh"] + "','"
                                    + ds.Tables[0].Rows[i]["bmddm"] + "','"
                                    + ds.Tables[0].Rows[i]["bmdmc"] + "','"
                                    + ds.Tables[0].Rows[i]["byzxdm"] + "','"
                                    + ds.Tables[0].Rows[i]["byzxmc"] + "','"
                                    + ds.Tables[0].Rows[i]["bjdm"] + "','"
                                    + ds.Tables[0].Rows[i]["xsbh"] + "','"
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
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        finally
                        {
                            conn.Close();
                            conn.Dispose();
                        }
                      
                    
                        // string strOdbcConn = @" DRIVER=Microsoft Visual FoxProDriver;UID=;Collate=Machine;BackgroundFetch=Yes;      Exclusive=No;SourceType=DBF;SourceDB=" + strFilePath + ";";
                        //OdbcConnection odbcConn = new OdbcConnection(strOdbcConn);
                        //string sqlInsert = "Insert Into table1(DateFrom, Num) Values({^2005-09-10},10)";
                        //OdbcCommand odbcComm = new OdbcCommand(sqlInsert, odbcConn);
                        //odbcComm.Connection.Open();
                        //odbcComm.ExecuteNonQuery();
                        //odbcConn.Close();
 
                     
                    

                        #region 弹出导出对话框

                     //   Response.Redirect("/Temp/" + fileTemName + ".dbf");
                        string name = "考生信息导出" + DateTime.Now.ToLongDateString();
                        name = System.Web.HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8);
                        Response.ContentType = "application/x-zip-compressed";
                        Response.Charset = "GB2312";

                        Response.AddHeader("Content-Disposition", "attachment;filename=" + name + ".dbf");
                        string filename = filetemPath;
                        Response.WriteFile(filename);Response.Flush();Response.End();


                        #endregion
                    }
                    string E_record = "导出:导出数据(报名号)";
                    EventMessage.EventWriteDB(1, E_record);
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