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

namespace SincciKC.websystem.kwgl
{
    public partial class Print_zp : BPage
    {
        private BLL_zk_kd bll = new BLL_zk_kd();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                string kcdm = "";
               
               
                if (Request.QueryString["kcdm"] != null)
                {
                    kcdm = Request.QueryString["kcdm"].ToString();
                    string bmddm = "";
                    for (int i = 0; i < kcdm.Split(',').Length; i++)
                    {
                        bmddm = bmddm + "'" + kcdm.Split(',')[i] + "',";
                    }
                    if (bmddm.Length > 0)
                    {
                        bmddm = bmddm.Remove(bmddm.Length - 1);
                    }
                    if (Request.QueryString["type"] != null)
                    {
                        switch (Request.QueryString["type"].ToString())
                        {
                            case "1"://照片对应表
                                print1(bmddm, kcdm);
                                break;
                            case "2"://签到表
                                print2(bmddm, kcdm);
                                break;
                            case "3"://门贴
                                print3(bmddm, kcdm);
                                break;
                            case "4"://桌贴
                                print4(bmddm, kcdm);
                                break;
                            default:
                                break;
                        }
                       
                    }
                }
                else
                {
                    this.Export.Visible = false;
                    this.ReportViewer1.Visible = false;
                    this.tb.InnerHtml = "&nbsp;&nbsp; <font color='red'>查询不到数据！</font>";
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
        /// 照片对应表
        /// </summary>
        /// <param name="bmddm"></param>
        private void print1(string bmddm, string kcdm)
        {
            DataTable dt = bll.Select_zwh(" kcdm in (" + bmddm + ") order by kcdm,zwh asc");
            DataTable dt1 = bll.Select_zwh(" kcdm in (" + bmddm + ") and zwh between '01' and '06'  order by kcdm,zwh asc");
            DataTable dt2 = bll.Select_zwh(" kcdm in (" + bmddm + ") and zwh between '07' and '12'  order by kcdm,zwh asc");
            DataTable dt3 = bll.Select_zwh(" kcdm in (" + bmddm + ") and zwh between '13' and '18'  order by kcdm,zwh asc");
            DataTable dt4 = bll.Select_zwh(" kcdm in (" + bmddm + ") and zwh between '19' and '24'  order by kcdm,zwh asc");
            DataTable dt5 = bll.Select_zwh(" kcdm in (" + bmddm + ") and zwh between '25' and '30'  order by kcdm,zwh asc");

            int ssnum = kcdm.Split(',').Length; //考场数量
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", rtTab(dt1, ssnum, 1, "asc")));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", rtTab(dt2, ssnum, 7, "asc")));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", rtTab(dt3, ssnum, 13, "asc")));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", rtTab(dt4, ssnum, 19, "asc")));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", rtTab(dt5, ssnum, 25, "asc")));
            ReportViewer1.LocalReport.ReportPath = "websystem/rdlc/Print_kcbp.rdlc";
            ReportViewer1.LocalReport.SetParameters(new ReportParameter("年份", dt.Rows[0]["kaocimc"].ToString()));
            if (dt1.Rows.Count > 0)
            {
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("县区", dt1.Rows[0]["xqmc"].ToString()));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("考点", dt1.Rows[0]["kdmc"].ToString()));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("考场", kcdm));
            }
        }

        /// <summary>
        /// 签到表
        /// </summary>
        /// <param name="bmddm"></param>
        private void print2(string bmddm, string kcdm)
        {
            DataTable dt = bll.Select_zwh(" kcdm in (" + bmddm + ") order by kcdm,zwh asc");
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
            ReportViewer1.LocalReport.ReportPath = "websystem/rdlc/Print_dzb.rdlc";
            if (dt.Rows.Count > 0)
            {
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("年份", dt.Rows[0]["kaocimc"].ToString()));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("县区", dt.Rows[0]["xqmc"].ToString()));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("考点", dt.Rows[0]["kdmc"].ToString()));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("考场", kcdm));
            }
        }
        /// <summary>
        /// 门贴
        /// </summary>
        /// <param name="bmddm"></param>
        private void print3(string bmddm, string kcdm)
        {
            DataTable dt = bll.Select_mt(" kcdm in (" + bmddm + ") ");
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
            ReportViewer1.LocalReport.ReportPath = "websystem/rdlc/Print_ment.rdlc";
            System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
            ps.Margins.Bottom = 0;
            ps.Margins.Left = 0;
            ps.Margins.Right = 0;
            ps.Margins.Top = 0;
            ps.Landscape = true;
            ReportViewer1.SetPageSettings(ps);
        }
        /// <summary>
        /// 桌贴
        /// </summary>
        /// <param name="bmddm"></param>
        private void print4(string bmddm, string kcdm)
        {
            DataTable dt = bll.Select_zwh(" kcdm in (" + bmddm + ") order by kcdm,zwh asc");
            DataTable dt1 = dt.Clone();
            DataTable dt2 = dt.Clone();
            DataTable dt3 = dt.Clone();
            int a = 0;
            int b = 1;
            for (int i = 0; i < dt.Rows.Count; i++) //循环给3个dt
            {
                if (a + b == i + 1)
                {
                    switch (a)
                    {
                        case 0: //dt1
                            dt1.ImportRow(dt.Rows[i]);
                            break;
                        case 1://dt2
                            dt2.ImportRow(dt.Rows[i]);
                            break;
                        case 2://dt3
                            dt3.ImportRow(dt.Rows[i]);
                            break;
                        default:
                            break;
                    }
                    a++;

                }
                if (a == 3)
                {
                    b = b + 3;
                    a = 0;
                }

            }
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt1));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dt2));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", dt3));
            ReportViewer1.LocalReport.ReportPath = "websystem/rdlc/Print_zhuot.rdlc";
            if (dt.Rows.Count > 0)
            {
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("年份", dt.Rows[0]["kaocimc"].ToString()));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("县区", dt.Rows[0]["xqmc"].ToString()));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("考点", dt.Rows[0]["kdmc"].ToString()));
                //string newkcdm = "";
                //for (int i = 0; i < kcdm.Split(',').Length; i++)
                //{
                //    newkcdm = newkcdm + kcdm.Split(',')[i] + ",";
                //    newkcdm = newkcdm + kcdm.Split(',')[i] + ",";
                //    newkcdm = newkcdm + kcdm.Split(',')[i] + ","; ;
                //}
                //if (newkcdm.Length > 0)
                //    newkcdm = newkcdm.Remove(newkcdm.Length - 1);
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("考场", kcdm));
            }
        }
        /// <summary>
        /// 返回填充后排序tab
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ssnum"></param>
        /// <param name="zwnum"></param>
        /// <returns></returns>
        private DataTable rtTab(DataTable dt, int ssnum, int zwnum, string paixu)
        {
            foreach (DataRow item in dt.Rows)
            {
                string picPath = Server.MapPath("//13//" + item["bmdxqdm"].ToString() + "//" + item["ksh"].ToString() + ".jpg");
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
            //int num = ssnum * 6 - dt.Rows.Count;
            //int[] intnum = new int[6];
            //intnum = intsz(zwnum);
            //for (int i = 0; i < num; i++)
            //{
            //    DataRow dr = dt.NewRow();

            //    if (intnum[6 - num + i].ToString().Length == 1)
            //    {
            //        dr["zwh"] = "0" + intnum[6 - num + i];
            //    }
            //    else
            //    {
            //        dr["zwh"] = intnum[6 - num + i];
            //    }
            //    dr["ID"] = intnum[6 - num + i];
            //    dr["IDSS"] = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["IDSS"]);
            //    dr["zkzh"] = " ";
            //    dr["xm"] = " ";
            //    dt.Rows.Add(dr);
            //}
            //DataView dv4 = new DataView(dt);
            //dv4.Sort = "IDSS asc,ID " + paixu;
            //return dv4.ToTable();
            return dt;
        }
        /// <summary>
        /// 返回座位编号
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private int[] intsz(int num)
        {
            int[] intnum = new int[6];
            for (int i = 0; i < intnum.Length; i++)
            {
                intnum[i] = num;
                num++;
            }
            return intnum;
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
