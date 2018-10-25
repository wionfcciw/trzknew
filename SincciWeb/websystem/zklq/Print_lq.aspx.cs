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
using Model;

namespace SincciKC.websystem.zklq
{
    public partial class Print_lq : BPage
    {   
        
        private BLL_xxgl bll = new BLL_xxgl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.pdf.Visible = false;
              
            }
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
            string lqxx = Request.QueryString["lqxx"].ToString();
            string FilePath = Server.MapPath("/ExportPDF/" + lqxx + "-" + DateTime.Now.ToString("yyyyMMddHHssmmffff") + ".pdf");

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

        protected void btn_sel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (Request.QueryString["pcdm"] != null && Request.QueryString["lqxx"] != null)
            {
                BLL_LQK_Ks_Xx zk = new BLL_LQK_Ks_Xx();
                string pcdm = Request.QueryString["pcdm"].ToString();
                string lqxx = Request.QueryString["lqxx"].ToString();
                string leix = Request.QueryString["leix"].ToString();
                string sfpc = Request.QueryString["sfpc"].ToString();
                if (leix != "")
                {
                    leix = " and sftzs=" + leix;
                }
                if (sfpc != "")
                {
                    sfpc = " and sfpc=" + sfpc;
                }
                string where = " pcdm='" + pcdm + "' and lqxx='" + lqxx + "' and td_zt=5 " + leix +sfpc;
                if (StartTime.Text.Length > 0)
                {
                    where = where + " and lqtime>='" + StartTime.Text.Trim() + "' and lqtime<='" + EndTime.Text.Trim() + "' ";
                }
                dt = zk.Select_Print_lq(where);
                Model_zk_zsxxdm model = new BLL_zk_zsxxdm().Disp(lqxx);

                if (dt.Rows.Count > 0)
                {
                    this.pdf.Visible = true;
                    tb.Visible = false;
                    string nian = "";
                    if (pcdm == "01")
                    {
                        nian = "精准扶贫生、民族生";
                    }
                    else
                    {
                        if (leix == "")
                        {
                            nian = "统招生、配额生、配转统";
                        }
                        else if (leix == "1")
                        {
                            nian = "统招生";
                        }
                        else if (leix == "2")
                        {
                            nian = "配额生";
                        }
                        else if (leix == "3")
                        {
                            nian = "配转统";
                        }
                        else if (leix == "4")
                        {
                            nian = "特长生";
                        }
                    }
                  
                    //else if (pcdm == "22")
                    //{
                    //    nian = dt.Rows[0]["kaocimc"].ToString() + "铜仁市五年制高职";
                    //}
                    //else if (pcdm == "23")
                    //{
                    //    nian = dt.Rows[0]["kaocimc"].ToString() + "铜仁市高技、现代职教体系“3+3”试点项目、中职对口单招";
                    //}
                    //else if (pcdm == "01")
                    //{
                    //    nian = dt.Rows[0]["kaocimc"].ToString() + "铜仁市师范配额生";
                    //}
                    //else if (pcdm == "02")
                    //{
                    //    nian = dt.Rows[0]["kaocimc"].ToString() + "铜仁市师范艺体类统招生";
                    //}
                    //else if (pcdm == "03")
                    //{
                    //    nian = dt.Rows[0]["kaocimc"].ToString() + "铜仁市现代职教体系“3+4”试点项目";
                    //}
                    //else if (pcdm == "31")
                    //{
                    //    nian = dt.Rows[0]["kaocimc"].ToString() + "铜仁市中职";
                    //}
                    ReportViewer ReportViewer1 = new ReportViewer();
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                    ReportViewer1.LocalReport.ReportPath = "websystem/rdlc/Print_lq.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("年份", dt.Rows[0]["kaocimc"].ToString()));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("学校", model.Zsxxdm + model.Zsxxmc));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("批次", nian));
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("学制", dt.Rows[0]["xzmc"].ToString()));
                    ExportPDF(ReportViewer1);
                    
                    //System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
                    //ps.Margins.Bottom = 0;
                    //ps.Margins.Left = 0;
                    //ps.Margins.Right = 0;
                    //ps.Margins.Top = 0;
                    //ps.Landscape = true;
                    //ReportViewer1.SetPageSettings(ps);
                }
                else
                {
                    this.pdf.Visible = false;
                    this.tb.InnerHtml = "&nbsp;&nbsp; <font color='red'>查询不到数据！</font>";
                }
            }
        }
    }
}