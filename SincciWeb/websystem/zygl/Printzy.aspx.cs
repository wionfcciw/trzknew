using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using BLL;
using System.IO;

namespace SincciKC.websystem.zygl
{
    public partial class Printzy : BPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BLL_xxgl bllxxgl = new BLL_xxgl();
                string eree = "";

                bool ispass = false;
                DataTable tabwhere = bllxxgl.seleczk_ksxxdy(" username='" + config.Get_UserName + "'");
                if (tabwhere.Rows.Count > 0)
                {
                    DataTable dtksh = new BLL_zk_zydz().Select_Viewksh(tabwhere.Rows[0]["SelWhere"].ToString());
                    DataTable dt = new DataTable();//总表.
                    DataTable zydt = new DataTable(); //一个ksh的表有48条数据
                    for (int i = 1; i <= 48; i++)
                    {
                        dt.Columns.Add("T" + i);
                        if (i == 21 || i == 22 || i == 23 || i == 37 || i == 38 || i == 39 || i == 40 || i == 41 || i == 42)
                        {
                            dt.Columns.Add("T" + i + "A");
                            dt.Columns.Add("T" + i + "B");
                            dt.Columns.Add("T" + i + "F");
                        }
                    }
                    dt.Columns.Add("T19A");
                    dt.Columns.Add("T20A");
                    dt.Columns.Add("ksh");
                    dt.Columns.Add("bmdmc");
                    dt.Columns.Add("xm");
                    for (int k = 0; k < dtksh.Rows.Count; k++)
                    {
                         zydt = new BLL_zk_zydz().Select_Viewdy(dtksh.Rows[k]["ksh"].ToString());
                        if (zydt.Rows.Count == 48)
                        {
                            
                                DataRow dr = dt.NewRow();
                                for (int i = 0; i < zydt.Rows.Count; i++)
                                {

                                    dr["T" + (i + 1)] = zydt.Rows[i]["xxdm"].ToString() + zydt.Rows[i]["zsxxmc"].ToString();
                                    if (dr["T" + (i + 1)].ToString().Length > 15)
                                    {
                                        dr["T" + (i + 1)] = dr["T" + (i + 1)].ToString().Substring(0, 15);
                                    }
                                    if ((i + 1) == 21 || (i + 1) == 22 || (i + 1) == 23 || (i + 1) == 37 || (i + 1) == 38 || (i + 1) == 39 || (i + 1) == 40 || (i + 1) == 41 || (i + 1) == 42)
                                    {
                                        dr["T" + (i + 1) + "A"] = zydt.Rows[i]["zy1"].ToString() + zydt.Rows[i]["zy1_1"].ToString();
                                        if (dr["T" + (i + 1) + "A"].ToString().Length > 15)
                                        {
                                            dr["T" + (i + 1) + "A"] = dr["T" + (i + 1) + "A"].ToString().Substring(0, 15);
                                        }
                                        dr["T" + (i + 1) + "B"] = zydt.Rows[i]["zy2"].ToString() + zydt.Rows[i]["zy2_2"].ToString();
                                        if (dr["T" + (i + 1) + "B"].ToString().Length > 15)
                                        {
                                            dr["T" + (i + 1) + "B"] = dr["T" + (i + 1) + "B"].ToString().Substring(0, 15);
                                        }

                                        dr["T" + (i + 1) + "F"] = zydt.Rows[i]["zyfc"].ToString();
                                        if (dr["T" + (i + 1) + "F"].ToString().Length > 15)
                                        {
                                            dr["T" + (i + 1) + "F"] = dr["T" + (i + 1) + "F"].ToString().Substring(0, 15);
                                        }
                                    }
                                    if (i == 19 || i == 20)
                                    {
                                        dr["T" + (i + 1) + "A"] = zydt.Rows[i]["zy1"].ToString() + zydt.Rows[i]["zy1_1"].ToString();
                                        if (dr["T" + (i + 1) + "A"].ToString().Length > 15)
                                        {
                                            dr["T" + (i + 1) + "A"] = dr["T" + (i + 1) + "A"].ToString().Substring(0, 15);
                                        }
                                    }
                                }
                                dr["ksh"] = zydt.Rows[0]["ksh"].ToString();
                                dr["bmdmc"] = zydt.Rows[0]["bmdmc"].ToString();
                                dr["xm"] = zydt.Rows[0]["xm"].ToString();
                                dt.Rows.Add(dr);     
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (dtksh.Rows.Count == 0)
                    {
                        this.Export.Visible = false;
                        this.ReportViewer1.Visible = false;
                        this.tb.InnerHtml = "&nbsp;&nbsp; <font color='red'>查询不到考生志愿数据,只能打印考生\"已确认\"的考生数据！</font>";
                    }
                    else
                    {
                        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                        ReportViewer1.LocalReport.ReportPath = "websystem/rdlc/Print_kszy.rdlc";
                        ReportViewer1.LocalReport.SetParameters(new ReportParameter("年份", zydt.Rows[0]["kaocimc"].ToString()));
                    }
                    bllxxgl.deletezk_ksxxdy(config.Get_UserName, ref eree, ref ispass);
                }

            }
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