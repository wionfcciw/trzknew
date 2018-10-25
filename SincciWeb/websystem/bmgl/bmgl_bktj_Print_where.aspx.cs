using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.IO;
using BLL;
namespace SincciKC.websystem.bmgl
{
    public partial class bmgl_bktj_Print_where : BPage
    {
        /// <summary>
        /// 志愿定制BLL
        /// </summary>
        private BLL_xxgl bll = new BLL_xxgl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                string leix = "";
                if (Request.QueryString["type"] == "1") //县区
                {
                    dt = xqPrint();
                    leix = "县区";
                }
                else if (Request.QueryString["type"] == "2")//学校
                {
                    dt = xxPrint();
                    leix = "学校";
                }
                else if (Request.QueryString["type"] == "3")//班级
                {
                    dt = bjPrint();
                    leix = "班级";
                }
                else
                {
                    Response.Write("你没有页面查看的权限！");
                    Response.End();
                }
                if (dt.Rows.Count > 0)
                {
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                    ReportViewer1.LocalReport.ReportPath = "websystem/rdlc/Print_where.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("年份", "20" + Request.QueryString["kaoci"] + "年"));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("类型", leix));
                  
                }
            }
        }
        /// <summary>
        /// 权限控制
        /// </summary>
        /// <param name="fanwei">管理范围</param>
        /// <param name="UserType">用户类型</param>
        /// <returns></returns>
        public string whereRoleXX(string fanwei, int UserType)
        {
            if (fanwei.Length >= 6)
            {
                fanwei = fanwei.Substring(0, 6);
            }
            string where = "";
            switch (UserType)
            {
                //系统管理员
                case 1:
                    where = "";
                    break;
                //市招生办
                case 2:
                    where = "";
                    break;
                //区招生办
                case 3:
                    where = "";
                    break;
                //学校用户 
                case 4:
                    where = fanwei;
                    break;
                //班级用户 
                case 5:
                    where = "*";
                    break;
                default:
                    where = "*";
                    break;
            }
            return where;
        }
        /// <summary>
        /// 权限控制
        /// </summary>
        /// <param name="fanwei">管理范围</param>
        /// <param name="UserType">用户类型</param>
        /// <returns></returns>
        public string whereRoleBJ(string fanwei, int UserType)
        {
            if (fanwei.Length >= 8)
            {
                fanwei = fanwei.Substring(6);
            }
            string where = "";
            switch (UserType)
            {
                //系统管理员
                case 1:
                    where = "";
                    break;
                //市招生办
                case 2:
                    where = "";
                    break;
                //区招生办
                case 3:
                    where = "";
                    break;
                //学校用户 
                case 4:
                    where = "";
                    break;
                //班级用户 
                case 5:
                    where = fanwei;
                    break;
                default:
                    where = "*";
                    break;
            }
            return where;
        }
        /// <summary>
        /// 班级
        /// </summary>
        /// <returns></returns>
        private DataTable bjPrint()
        {
            string kaoci = Request.QueryString["kaoci"];
            DataTable dtLast = new DataTable();
            string xqdm = Request.QueryString["xqdm"];
            string bmddm = Request.QueryString["bmddm"];
            string where = "";
            if (Request.QueryString["where"].Length > 0)
            {
                where = Request.QueryString["where"].Replace("_", "'");
            }
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string str = whereRoleBJ(Department, UserType);
            if (str.IndexOf("*") == -1)
            {
                if (str == "")
                {
                    dtLast = bll.Select_bjtj_Where(where, xqdm, bmddm, kaoci);
                  
                }
                else
                {
                    DataTable dt = bll.Select_bjtj_Where(where, xqdm, bmddm, kaoci);
                    DataTable dtnew = dt.Clone();
                    DataRow[] dr = dt.Select(" bjdm='" + str + "'");
                    foreach (DataRow item in dr)
                    {
                        dtnew.ImportRow(item);
                    }
                    dtLast = dtnew;
                  
                }
            }
            dtLast.Columns.Add("bmdmc");
            dtLast.Columns.Add("xqmc");
            return dtLast;
        }
        /// <summary>
        /// 县区
        /// </summary>
        /// <returns></returns>
        private DataTable xqPrint()
        {
            string kaoci = Request.QueryString["kaoci"];
            DataTable dtLast = new DataTable();
            string where = "";
            if (Request.QueryString["where"].Length > 0)
            {
                where = Request.QueryString["where"].Replace("_", "'");
            }
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string str = whereRole(Department, UserType);
            if (str.IndexOf("*") == -1)
            {
                if (str == "")
                {
                    dtLast = bll.Select_xqtj_Where(where, kaoci);
                }
                else
                {
                    DataTable dt = bll.Select_xqtj_Where(where, kaoci);
                    DataTable dtnew = dt.Clone();
                    DataRow[] dr = dt.Select(" xqdm='" + str + "'");
                    foreach (DataRow item in dr)
                    {
                        dtnew.ImportRow(item);
                    }
                    dtLast = dtnew;
                  
                }
            }

            dtLast.Columns.Add("bmdmc");
            dtLast.Columns.Add("bjmc");
           

            return dtLast;
        }

        /// <summary>
        /// 学校
        /// </summary>
        /// <returns></returns>
        private DataTable xxPrint()
        {
            string xqdm = Request.QueryString["xqdm"];
            string kaoci = Request.QueryString["kaoci"];
            string where = "";
            if (Request.QueryString["where"].Length > 0)
            {
                where = Request.QueryString["where"].Replace("_", "'");
            }
            DataTable dtLast = new DataTable();
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string str = whereRoleXX(Department, UserType);
            if (str.IndexOf("*") == -1)
            {
                if (str == "")
                {
                    dtLast = bll.Select_xxtj_Where(where, xqdm, kaoci);
                    
                }
                else
                {
                    DataTable dt = bll.Select_xxtj_Where(where, xqdm, kaoci);
                    DataTable dtnew = dt.Clone();
                    DataRow[] dr = dt.Select(" bmddm='" + str + "'");
                    foreach (DataRow item in dr)
                    {
                        dtnew.ImportRow(item);
                    }
                    dtLast = dtnew;
                  
                }
            }
            dtLast.Columns.Add("xqmc");
            dtLast.Columns.Add("bjmc");

            return dtLast;
        }

        /// <summary>
        /// 权限控制
        /// </summary>
        /// <param name="fanwei">管理范围</param>
        /// <param name="UserType">用户类型</param>
        /// <returns></returns>
        public string whereRole(string fanwei, int UserType)
        {
            if (fanwei.Length >= 4)
            {
                fanwei = fanwei.Substring(0, 4);
            }
            string where = "";
            switch (UserType)
            {
                //系统管理员
                case 1:
                    where = "";
                    break;
                //市招生办
                case 2:
                    where = "";
                    break;
                //区招生办
                case 3:
                    where = fanwei;
                    break;
                //学校用户 
                case 4:
                    where = fanwei;
                    break;
                //班级用户 
                case 5:
                    where = "*";
                    break;
                default:
                    where = "*";
                    break;
            }
            return where;
        }
        //相片转换为二进制。
        public static Byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            if (stream.CanRead)
            {
                stream.Read(bytes, 0, (int)stream.Length);
            }
            stream.Close();
            return bytes;
        }

        protected void btndaoc_Click(object sender, EventArgs e)
        {
            ExportPDF(this.ReportViewer1);
        }
        /// <summary>
        /// 生成PDF
        /// </summary>
        /// <param name="rt"></param>
        private void ExportPDF(ReportViewer rt)
        {
            //生成PDF
            Microsoft.Reporting.WebForms.Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            string FilePath = Server.MapPath("/ExportPDF/" + DateTime.Now.ToString("yyyyMMddHHssmmffff") + ".pdf");

            byte[] bytes = rt.LocalReport.Render(
            "PDF", null, out mimeType, out encoding, out extension,
            out streamids, out warnings);

            FileStream fs = new FileStream(FilePath, FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();

            //下载PDF
            FileInfo fi = new FileInfo(FilePath);
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.ClearHeaders();
            System.Web.HttpContext.Current.Response.Buffer = false;
            System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(Path.GetFileName(FilePath), System.Text.Encoding.UTF8));
            System.Web.HttpContext.Current.Response.AppendHeader("Content-Length", fi.Length.ToString());
            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
            System.Web.HttpContext.Current.Response.WriteFile(FilePath);
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();

        }
  
    }
}