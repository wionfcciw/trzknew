using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.IO;
using BLL;
using Model;

namespace SincciKC.websystem.kwgl
{
    public partial class Print_ksz : BPage
    {
        private BLL_zk_kd bll = new BLL_zk_kd();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
             
                string xxdm = "";
                string ksh = "";
                DataTable dt = new DataTable();
                if (Request.QueryString["xxdm"] != null)
                {
                    xxdm = Request.QueryString["xxdm"].ToString();
                    string bmddm = "";
                    for (int i = 0; i < xxdm.Split(',').Length; i++)
                    {
                        bmddm = bmddm + "'" + xxdm.Split(',')[i] + "',";
                    }
                    if (bmddm.Length > 0)
                    {
                        bmddm = bmddm.Remove(bmddm.Length - 1);
                    }
                    dt = bll.Select_ksz(" bmddm in (" + bmddm + ")");
                }
                if (Request.QueryString["ksh"] != null)
                {
                    ksh = Request.QueryString["ksh"].ToString();
                    dt = bll.Select_ksz(" ksh ='" + ksh + "'");
                }

                //DataTable dt1 = dt.Clone();
                //DataTable dt2 = dt.Clone();
                //DataTable dt3 = dt.Clone();



                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        string picPath = Server.MapPath("/pic/" + item["bmdxqdm"].ToString() + "/" + item["bmddm"].ToString() + "/" + item["ksh"].ToString() + ".jpg");
                        if (File.Exists(picPath))
                        {
                            FileStream st = new FileStream(picPath, FileMode.Open, FileAccess.ReadWrite);
                            item["kaoci"] = Convert.ToBase64String(StreamToBytes(st));
                        }
                        else
                        {
                            item["kaoci"] = "";
                        }
                    }
                    //int a = 0;
                    //int b = 1;
                    //for (int i = 0; i < dt.Rows.Count; i++) //循环给3个dt
                    //{
                    //    if (a + b == i + 1)
                    //    {
                    //        switch (a)
                    //        {
                    //            case 0: //dt1
                    //                dt1.ImportRow(dt.Rows[i]);
                    //                break;
                    //            case 1://dt2
                    //                dt2.ImportRow(dt.Rows[i]);
                    //                break;
                    //            case 2://dt3
                    //                dt3.ImportRow(dt.Rows[i]);
                    //                break;
                    //            default:
                    //                break;
                    //        }
                    //        a++;

                    //    }
                    //    if (a == 3)
                    //    {
                    //        b = b + 3;
                    //        a = 0;
                    //    }

                    //}


                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                    //ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dt2));
                    //ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", dt3));
                    ReportViewer1.LocalReport.ReportPath = "websystem/rdlc/Print_kszkz.rdlc";
                  
                    StreamReader subReport = File.OpenText(Server.MapPath(String.Format("~/websystem/rdlc/Print_kszkz_zbb.rdlc")));

                    ReportViewer1.LocalReport.LoadSubreportDefinition("DataSet1", subReport);
                    ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("年份", dt.Rows[0]["kaocimc"].ToString()));
                    
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
                    this.Export.Visible = false;
                    this.ReportViewer1.Visible = false;
                    this.tb.InnerHtml = "&nbsp;&nbsp; <font color='red'>查询不到数据！</font>";
                }
           
            }
        }
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
         
            ReportDataSource rds1 = new ReportDataSource("DataSet1", bll.zk_kstime_tab());
            e.DataSources.Add(rds1);
               
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

        /// <summary>
        /// 导出PDF格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Export_Click(object sender, EventArgs e)
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


// Response.Write(Server.MapPath("/ExportPDF/" + DateTime.Now.ToString("yyyyMMddHHssmmffff") + ".pdf"));



//Warning[] warnings;
//string[] streamids;
//string mimeType;
//string encoding;
//string extension;

//byte[] bytes = ReportViewer1.LocalReport.Render(
//   "pdf", null, out mimeType, out encoding,
//    out extension,
// out streamids, out warnings);

//FileStream fs = new FileStream(@"E:\Print.pdf",
//  FileMode.Create);
//fs.Write(bytes, 0, bytes.Length);
//fs.Close();


//DataTable dt = new DataTable();
//ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dt));
//ReportViewer1.LocalReport.ReportPath = @"Print.rdlc";
//ReportViewer1.LocalReport.SetParameters(new ReportParameter("年份", "2012"));




//dt.Columns.Add("kaoci");
//DataRow dr = dt.NewRow();
//dr["kaoci"] = @"E:\404.jpg";