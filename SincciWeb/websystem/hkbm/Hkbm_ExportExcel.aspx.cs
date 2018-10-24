using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Text;
using System.Data.OleDb;
using BLL;
using System.Collections;
namespace SincciKC.websystem.hkbm
{
    public partial class Hkbm_ExportExcel : BPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string mubiao = "";
            string ss = DateTime.Now.ToString().Replace(":", "").Replace("-", "").Replace(" ", "");
            string moban = Server.MapPath(String.Format("/Template/daochuksh.xls"));
            mubiao = Server.MapPath(String.Format("/tmpUpLoadFile/" + ss + ".xls"));
            File.Copy(moban, mubiao, true);
            //  string path = Server.MapPath(String.Format("/Template/daochuksh.xls"));
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + mubiao + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            try
            {
                conn.Open();
                DataTable dt = new BLL_Hkbm().ExportEXCELKsh(quanxian()).Tables[0];
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
                string name = "会考考生信息导出" + DateTime.Now.ToLongDateString();
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
    }
}