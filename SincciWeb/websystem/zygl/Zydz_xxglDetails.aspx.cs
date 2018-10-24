using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using Model;

using System.Text;
namespace SincciKC.websystem.zysz
{
    public partial class Zydz_xxglDetails : BPage
    {
        #region "加载志愿表格数据"

        /// <summary>
        /// 志愿定制BLL
        /// </summary>
        private BLL_zk_zydz bll = new BLL_zk_zydz();
        /// <summary>
        /// 加载数据
        /// </summary>
        private void ShowLoad(string xqdm, string ksh)
        {

            StringBuilder str = new StringBuilder();
            //大批次总数量
            int dpcNum = 0;
            //小批次总数量
            int xpcNum = 0;
            //志愿数量
            int zyNum = 0;
            //专业数量
            int Num = 0;
            //1200,1202
            // string xqdmbr = "1200,1202";
            DataTable zydt = new DataTable();
            zydt = bll.Select_zy_Num(xqdm);
            zyNum = zydt.Rows.Count;
            if (zydt.Rows.Count > 0)
            {
                if (zydt.Select(" csfZyFc=1").Length > 0)
                {
                    Num = Num + 1;
                }
                if (Convert.ToInt32(zydt.Rows[0]["czySl"]) > 0)
                {
                    Num = Num + 1;
                }
            }
            Num = Num + 1;
            DataTable dpcdt = new DataTable();
            dpcdt = bll.Select_All_DpcIsPass(xqdm);
            dpcNum = dpcdt.Rows.Count;


            //头
            str.Append("<table border=\"1\" cellpadding=\"0\" width=\"847\" class=\"tblcss\"  bordercolor=\"#1D9494\"  cellspacing=\"0\" >");
            str.Append(" <td colspan=\"2\">录取顺序</td><td rowspan=\"2\" width=\"130\" style=\"word-wrap: break-word;\">类别</td>");
            str.Append("<td colspan=\"" + (5) + "\" rowspan=\"2\">平行学校志愿</td></tr>");
            str.Append(" <tr><td style=\"width:20px \">批<br>次</td><td style=\"  \">顺序</td></tr>");

            foreach (DataRow item in dpcdt.Rows) //循环大批次
            {
                DataTable dpctab = bll.Select_zy_ALLNum(Convert.ToInt32(item["dpcId"]));
                DataTable xpcdt = new DataTable();
                xpcdt = bll.Select_All_XpcIsPass(item["dpcId"].ToString());
                xpcNum = xpcdt.Rows.Count;
                if (dpctab.Rows.Count > 0)
                {
                    str.Append("<tr><td rowspan=\"" + (dpctab.Rows.Count + xpcNum) + "\"  width=\"20\" style=\"word-wrap: break-word;\">" + item["dpcMc"] + "</td>");//加了个标题
                }
                int xpcbian = 0; //判断是否是大批次下的第一个小批次
                int shuzi = 1;
                foreach (DataRow dr in xpcdt.Rows) //循环小批次
                {
                    DataTable zdt = bll.Select_All_ZyIsPass(dr["xpcId"].ToString());//小批次下面志愿数量
                    if (xpcbian == 0)
                    {
                        str.Append("<td rowspan=\"" + (zdt.Rows.Count + 1) + "\">" + shuzi + "</td>");
                        xpcbian++;
                    }
                    else
                    {
                        str.Append("<tr><td rowspan=\"" + (zdt.Rows.Count + 1) + "\">" + shuzi + "</td>");
                    }
                    str.Append("<td rowspan=\"" + (zdt.Rows.Count + 1) + "\"  width=\"130\" style=\"word-wrap: break-word;\">" + dr["xpcMc"]);
                    string fuc = "";
                    DataTable dt = bll.Select_kjbs_fc(dr["xpcId"].ToString(), ksh);
                    bool sfbk = false;
                   if (dt.Rows.Count > 0)
                   {
                       sfbk = Convert.ToBoolean(dt.Rows[0]["sfbk"].ToString());
                   }
                    if (sfbk)
                    {
                        fuc = "报考";
                    }
                    else
                    {
                        fuc = "不报考";
                    }

                    str.Append("<br /> <span id=\"xpc1" + dr["xpcId"] + "\" name=\"xpc1" + dr["xpcId"] + "\">" + fuc + "</span>");
                    str.Append(" </td>");
                    shuzi++;

                    int bianl = 0; //变量 =0的时候.第一个小批次加标题

                    foreach (DataRow zydr in zdt.Rows) //循环专业
                    {
                        if (bianl == 0)
                        {
                            if (Convert.ToInt32(zydr["zySl"]) > 0)//是否有专业和服从   专业数量>0
                            {
                                str.Append("<td colspan=\"2\" >学校代码</td><td   >学校名称</td>");

                                if (zdt.Select(" sfZyFc=1").Length > 0)//有服从
                                {
                                    str.Append("<td >专业</td>");
                                    str.Append("<td >是否服从<br>其他专业</td></tr>");
                                }
                                else
                                {
                                    str.Append("<td colspan=\"2\">专业</td></tr>");

                                }

                            }
                            else
                            {
                                str.Append("<td colspan=\"2\" style=\"  \" >学校代码</td><td colspan=\"" + (3) + "\"   >学校名称</td>");
                                str.Append("</tr>");
                            }

                        }
                        bianl++;
                        //zdt[0] 得到最大的专业数量
                        str.Append("<tr><td>" + zydr["zyMc"] + "</td>");  //A
                        DataTable dtxd = bll.Select_kjbs(dr["xpcId"] + "_" + zydr["zyId"], ksh);
                        string xxdm = "";
                        string zsxxmc = "";
                        if (dtxd.Rows.Count > 0)
                        {
                            xxdm = dtxd.Rows[0]["xxdm"].ToString();
                            zsxxmc = dtxd.Rows[0]["zsxxmc"].ToString();
                        }

                        str.Append("<td  style=\" color:Blue\">" + xxdm + "</td>");  //学校代码

                        if (Convert.ToInt32(zydr["zySl"]) > 0)//是否有专业和服从   专业数量>0
                        {

                            str.Append("<td style=\" color:Blue\" width=\"150\" style=\"word-wrap: break-word;\" align=\"left\">" + zsxxmc + "</td>");  //学校名称
                        }
                        else
                        {
                            str.Append("<td  style=\" color:Blue\"  colspan=\"" + (3) + "\"  align=\"left\" >" + zsxxmc + "</td>");  //学校名称
                        }

                        if (Convert.ToInt32(zydr["zySl"]) > 0)
                        {

                            if (zdt.Select(" sfZyFc=1").Length > 0)//有服从
                            {
                                str.Append("<td  >");
                            }
                            else
                            {
                                str.Append("<td  colspan=\"2\">");
                            }
                            str.Append("<table  align=\"left\" cellpadding=\"0\" cellspacing=\"0\">");
                            for (int i = 1; i <= Convert.ToInt32(zydr["zySl"]); i++)
                            {
                                str.Append("<tr><td style=\" text-align:right\">" + "专业" + config.GetDaoXie(i.ToString()) + "：</td>"); //专业
                                string zymc = "";
                                if (dtxd.Rows.Count > 0)
                                {
                                    zymc = dtxd.Rows[0]["zy" + i] + ":" + dtxd.Rows[0]["zy" + i + "_" + i];
                                }
                                
                                if (zymc.Length == 1)
                                    zymc = "";
                                str.Append("<td align=\"left\"  style=\" color:Blue\"> " + zymc + " </td></tr>");

                            }
                            str.Append("</table>");
                            str.Append("</td>");
                            if (zdt.Select(" sfZyFc=1").Length > 0)//有服从
                            {
                               bool fcBool= false;
                               if (dtxd.Rows.Count > 0)
                               {
                                   fcBool = Convert.ToBoolean(dtxd.Rows[0]["zyfc"]);
                               }
                                string zyfc = "";
                                if (fcBool)
                                {
                                    zyfc = "服从";
                                }
                                else
                                {
                                    string fc_zy = "";
                                    if (dtxd.Rows.Count > 0)
                                    {
                                        fc_zy = dtxd.Rows[0]["zy1"].ToString();
                                    }

                                    if (fc_zy == "")
                                    {
                                        zyfc = "";
                                    }
                                    else
                                    {
                                        zyfc = "不服从";
                                    }

                                }
                                str.Append("<td    style=\" color:Blue\"> " + zyfc + "</td></tr>");  //是否有服从
                            }
                            else
                            {
                                str.Append("</tr>");
                            }
                        }
                        else
                        {
                            str.Append("</tr>");
                        }
                    }
                }
            }

            //尾
            str.Append(" </table>");
            zyspan.InnerHtml = str.ToString();
            // ShowLook( ksh);
        }
        #endregion
        
            
        /// <summary>
        /// 考生信息管理
        /// </summary>
        public Model_zk_ksxxgl info = new Model_zk_ksxxgl();
        public string ksh = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ksh = config.sink("ksh", config.MethodType.Get, 255, 1, config.DataType.Str).ToString();
                  
                if (ksh.Length > 0)
                {
                    info = new BLL_zk_ksxxgl().ViewDisp(ksh);
                    string xqdm = bll.Select_zy_xqdm(ksh.Substring(2, 4));
                    ShowLoad(xqdm, ksh);
                }
            }
        }

        //#region "查看"
        ///// <summary>
        ///// 查看.
        ///// </summary>
        //private void ShowLook(string ksh)
        //{

        //    // zydt = bll.Select_zy_Num(xqdm);

        //    //bool Sfbk = false;
        //    //bool Zyfc = false;
        //    //string zyNum = "";
        //    DataTable zydt = new DataTable();


        //    BLL_zk_zydz bllzy = new BLL_zk_zydz();
        //    zydt = bllzy.Select_Viewzy(ksh);
        //    StringBuilder str = new StringBuilder();
        //    str.Append("<script>");
        //    if (zydt.Rows.Count == 0 || zydt == null)
        //    {
        //        return;
        //    }
        //    foreach (DataRow item in zydt.Rows)
        //    {

        //        str.Append("document.getElementById(\"" + item["kjbs"] + "_dm\").innerText=\"" + item["xxdm"] + "\";");

        //        str.Append("document.getElementById(\"" + item["kjbs"] + "_mc\").innerText=\" " + item["zsxxmc"].ToString() + "\";");
        //        if (item["sfbk"].ToString() == "True")
        //        {
        //            str.Append("document.getElementById(\"xpc1" + item["kjbs"].ToString().Split('_')[0] + "\").innerText=\"报考\";");
        //        }
        //        else
        //        {
        //            str.Append("document.getElementById(\"xpc1" + item["kjbs"].ToString().Split('_')[0] + "\").innerText=\"不报考\";");
        //        }
        //        if (item["zyfc"].ToString() == "True")
        //        {
        //            str.Append("document.getElementById(\"" + item["kjbs"] + "_fc\").innerText=\"服从\";");
        //        }
        //        if (item["zy1"].ToString() != "" && item["zyfc"].ToString() != "True")
        //        {
        //            str.Append("document.getElementById(\"" + item["kjbs"] + "_fc\").innerText=\"不服从\";");
        //        }

        //        if (item["xxdm"].ToString() != "" && item["zy1"].ToString() != "" && item["zy1"].ToString() != null)
        //        {
        //            for (int i = 0; i < 7; i++)
        //            {
        //                if (item["zy" + (i + 1)].ToString() == "")
        //                {
        //                    break;
        //                }
        //                //   str.Append("try {");
        //                str.Append("document.getElementById(\"" + item["kjbs"] + "_zy" + i + "\").innerText=\"" + item["zy" + (i + 1)] + ":" + item["zy" + (i + 1) + "_" + (i + 1)] + "\";");

        //                //  str.Append("} catch (e) { }");
        //            }

        //        }

        //    }
        //    str.Append("</script>");

        //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", str.ToString());
        //}
        //#endregion
    }
}