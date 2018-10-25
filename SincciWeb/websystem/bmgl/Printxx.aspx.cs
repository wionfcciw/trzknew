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

namespace SincciKC.websystem.bmgl
{
    public partial class Printxx : BPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BLL_xxgl bllxxgl = new BLL_xxgl();
                string eree = "";

                bool ispass = false;
                DataTable tabwhere = bllxxgl.seleczk_ksxxdy(" username='" + config.Get_UserName + "'" );
                if (tabwhere.Rows.Count > 0)
                {
                    DataTable dt = bllxxgl.seleckshgrxx(tabwhere.Rows[0]["SelWhere"].ToString() );
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                           
                            string picPath = Server.MapPath("//13//" + item["bmdxqdm"].ToString() + "//"  + item["ksh"].ToString() + ".jpg");
                           
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

                        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                        ReportViewer1.LocalReport.ReportPath = "websystem/rdlc/Print_ntOne.rdlc";
                        ReportViewer1.LocalReport.SetParameters(new ReportParameter("年份", dt.Rows[0]["kaocimc"].ToString()));
                    }
                    else
                    {
                        this.Export.Visible = false;
                        this.ReportViewer1.Visible = false;
                        this.tb.InnerHtml = "&nbsp;&nbsp; <font color='red'>查询不到考生数据,只能打印考生\"已照相\"并且\"已确认\"的考生数据！</font>";
                    }
                    bllxxgl.deletezk_ksxxdy(config.Get_UserName, ref eree, ref ispass);
                }

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