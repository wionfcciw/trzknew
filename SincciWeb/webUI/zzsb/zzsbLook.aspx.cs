using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using BLL;
using System.Data;

using Model;
using BLL.system;

namespace SincciKC.webUI.zzsb
{
    public partial class zzsbLook : SPage //wf 2018-08-16 修改，单点登录统一控制 System.Web.UI.Page
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
            zydt = bll.Select_zy_Num("500");
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
            dpcdt = bll.Select_All_DpcIsPass("500");
            dpcNum = dpcdt.Rows.Count;
            DataTable dtZyxx = bll.Select_Viewzzsbxx(ksh, "500");

            //头
            str.Append("<table border=\"1\" cellpadding=\"0\" width=\"847\" class=\"tblcss\"  bordercolor=\"#1D9494\"  cellspacing=\"0\" >");
            str.Append(" <tr><td style=\"width:20px \">批次</td> <td   width=\"130\" style=\"word-wrap: break-word;\">小批</td>");
            str.Append("<td colspan=\"" + 6 + "\" >志愿信息</td></tr>");

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
                    //    str.Append("<td rowspan=\"" + (zdt.Rows.Count + 1) + "\">" + shuzi + "</td>");
                        xpcbian++;
                    }
                    else
                    {
                        str.Append("<tr>");
                    }
                    str.Append("<td rowspan=\"" + (zdt.Rows.Count + 1) + "\"  width=\"130\" style=\"word-wrap: break-word;\">" + dr["xpcMc"]);
                    string fuc = "";
                    if (Convert.ToBoolean(bll.Select_kjbs_fc_zzsb(dr["xpcId"].ToString(), ksh).Rows[0]["sfbk"]))
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
                                 
                                    str.Append("<td    >是否服从<br/>其他专业</td>");
                                }
                                else
                                {
                                    str.Append("<td colspan=\"3\">专业</td> ");

                                }

                            }
                            else
                            {
                                
                                    str.Append("<td colspan=\"2\" style=\"  \" >学校代码</td><td colspan=\"" + 4 + "\"   >学校名称</td>");
    
                               
                            }
                            if (Convert.ToBoolean(zydr["sfxxfc"]))
                                str.Append("<td>其他学校<br/>是否服从</td>");
                            str.Append("</tr>");
                        }
                        bianl++;
                        //zdt[0] 得到最大的专业数量
                        str.Append("<tr><td>" + zydr["zyMc"] + "</td>");  //A
                        DataTable dtxd = bll.Select_kjbs_zzsb(dr["xpcId"] + "_" + zydr["zyId"], ksh);
                        string xxdm = dtxd.Rows[0]["xxdm"].ToString();
                        string zsxxmc = dtxd.Rows[0]["zsxxmc"].ToString();
                        str.Append("<td  style=\" color:Blue\">" + xxdm + "</td>");  //学校代码
                         
                        if (Convert.ToInt32(zydr["zySl"]) > 0)//是否有专业和服从   专业数量>0
                        {

                            str.Append("<td style=\" color:Blue\" width=\"150\" style=\"word-wrap: break-word;\" align=\"left\">" + zsxxmc + "</td>");  //学校名称
                        }
                        else
                        {
                            
                                str.Append("<td  style=\" color:Blue\"  colspan=\"" + 4 + "\"  align=\"left\" >" + zsxxmc + "</td>");  //学校名称
                         }

                        if (Convert.ToInt32(zydr["zySl"]) > 0)
                        {

                            if (zdt.Select(" sfZyFc=1").Length > 0)//有服从
                            {
                               
                                    str.Append("<td  >");
                                
                            }
                            else
                            {
                                str.Append("<td  colspan=\"3\">");
                            }
                            str.Append("<table  align=\"left\" cellpadding=\"0\" cellspacing=\"0\">");
                            for (int i = 1; i <= Convert.ToInt32(zydr["zySl"]); i++)
                            {
                                str.Append("<tr><td style=\" text-align:right\">" + "专业" +config.GetDaoXie( i.ToString())+ "：</td>"); //专业
                                string zymc = dtxd.Rows[0]["zy" + i] + ":" + dtxd.Rows[0]["zy" + i + "_" + i];
                                if (zymc.Length == 1)
                                    zymc = "";
                                str.Append("<td align=\"left\"  style=\" color:Blue\"> " + zymc + " </td></tr>");

                            }
                            str.Append("</table>");
                            str.Append("</td>");
                            if (zdt.Select(" sfZyFc=1").Length > 0)//有服从
                            {
                                 string zyfc="";
                                 if (Convert.ToBoolean(dtxd.Rows[0]["zyfc"]))
                                     zyfc = "服从";
                                 else
                                 {
                                     if (dtxd.Rows[0]["zy1"].ToString()=="")
                                     {
                                         zyfc = "";
                                     }
                                     else
                                     {
                                         zyfc = "不服从";
                                     }
                                  
                                 }
                                 str.Append("<td     style=\" color:Blue\"> " + zyfc + "</td>");  //是否有服从
                            }
                          
                        }
                        string sfxxfc = dtxd.Rows[0]["sfxxfc"].ToString();
                        DataTable tabsfbk = bll.Select_kjbs_fc_zzsb(dr["xpcId"].ToString(), ksh);
                        if (Convert.ToBoolean(tabsfbk.Rows[0]["sfbk"]))
                        {
                            if (dtZyxx.Select(" xpcid='" + dr["xpcId"] + "' and sfbk=1 and sfxxfc=1").Length > 0)
                                sfxxfc = "服从";
                            else
                                sfxxfc = "不服从";
                        }
                        else
                            sfxxfc = "";

                        int rows = zdt.Rows.Count;
                        if (Convert.ToBoolean(zydr["sfxxfc"]))//是否有学校服从
                        {
                            str.Append("<td   rowspan=\""+rows+"\" style=\" color:Blue\">" + sfxxfc + "</td>");  //是否有学校服从
                        }
                            str.Append("</tr>");

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
                if (Session["kaosheng"] != null)
                {
                    if (BLL_Ks_Session.ksLogCheck())
                    {

                        ksh = BLL_Ks_Session.ksSession().ksh;
                       
                        string xqdm = bll.Select_zy_xqdm(ksh.Substring(2, 4));
                        if (!new Bll_zkbm_Time().zkbm_time(xqdm, 5))
                        {
                            Response.Write("<script>alert('现在不是网上自主申报时间！'); history.back(); </script>");
                        }
                        else
                        {
                            info = new BLL_zk_ksxxgl().ViewDisp(ksh);

                            DataTable zzsbtab = bll.Select_zk_zzsbtzxx(ksh);
                            if (Convert.ToInt32(zzsbtab.Rows[0]["sbksqr"]) == 1)
                            {
                                this.btnKSQueren.Visible = true;
                            }
                            if (Convert.ToInt32(zzsbtab.Rows[0]["sbksqr"]) == 2)
                            {
                                this.btnBack.Visible = false;
                            }
                            ShowLoad(xqdm, ksh);
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('超时请重新登录！'); window.location.href='/' </script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('超时请重新登录！'); window.location.href='/' </script>");
                }

            }
        }

        

        #region "查看"
        /// <summary>
        /// 查看.
        /// </summary>
        private void ShowLook(string ksh)
        {

            // zydt = bll.Select_zy_Num(xqdm);

            //bool Sfbk = false;
            //bool Zyfc = false;
            //string zyNum = "";
            DataTable zydt = new DataTable();
          

            BLL_zk_zydz bllzy = new BLL_zk_zydz();
            zydt = bllzy.Select_Viewzy(ksh);
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            if (zydt.Rows.Count == 0 || zydt == null)
            {
                return;
            }
            foreach (DataRow item in zydt.Rows)
            { 

                str.Append("document.getElementById(\"" + item["kjbs"] + "_dm\").innerText=\"" + item["xxdm"] + "\";");

                str.Append("document.getElementById(\"" + item["kjbs"] + "_mc\").innerText=\" " + item["zsxxmc"].ToString() + "\";");
                if (item["sfbk"].ToString() == "True")
                {
                    str.Append("document.getElementById(\"xpc1" + item["kjbs"].ToString().Split('_')[0] + "\").innerText=\"报考\";");
                }
                else
                {
                    str.Append("document.getElementById(\"xpc1" + item["kjbs"].ToString().Split('_')[0] + "\").innerText=\"不报考\";");
                }
                if (item["zyfc"].ToString() == "True")
                {
                    str.Append("document.getElementById(\"" + item["kjbs"] + "_fc\").innerText=\"服从\";");
                }
                if (item["zy1"].ToString() != "" && item["zyfc"].ToString() != "True")
                {
                    str.Append("document.getElementById(\"" + item["kjbs"] + "_fc\").innerText=\"不服从\";");
                }
                
                if (item["xxdm"].ToString() != "" && item["zy1"].ToString() != "" && item["zy1"].ToString() != null)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        if (item["zy" + (i + 1)].ToString() == "")
                        {
                            break;
                        }
                     //   str.Append("try {");
                        str.Append("document.getElementById(\"" + item["kjbs"] + "_zy" + i + "\").innerText=\"" + item["zy" + (i + 1)] + ":" + item["zy" + (i + 1) + "_" + (i + 1)] + "\";");

                      //  str.Append("} catch (e) { }");
                    }

                }
                 
            }
            str.Append("</script>");

            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", str.ToString());
        }
        #endregion

      
        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("zzsb.aspx", false);
        }

       

        protected void btnKSQueren_Click(object sender, EventArgs e)
        {
         

            if (Session["kaosheng"] != null)
            {
                if (BLL_Ks_Session.ksLogCheck())
                {
                    ksh = BLL_Ks_Session.ksSession().ksh;
                    if (bll.zk_ksxxglsbksqr(ksh, 2))
                    {
                        DataTable zzsbtab = bll.Select_zzsbxx(ksh);

                        if (bll.update_lqk(zzsbtab.Rows[0]["xxdm"].ToString(), zzsbtab.Rows[0]["pcdm"].ToString(), zzsbtab.Rows[0]["zysx"].ToString(), ksh))
                        {
                            Response.Write("<script>alert('确认志愿信息成功！'); window.location.href='zzsbLook.aspx' </script>");

                        }
                        else
                        {
                            Response.Write("<script>alert('确认志愿信息失败！'); window.location.href='zzsbLook.aspx'</script>");

                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('确认志愿信息失败！'); window.location.href='zzsbLook.aspx'</script>");
                    }

                }
                else
                {
                    Response.Write("<script>alert('超时请重新登录！'); window.location.href='/' </script>");
                }
            }
            else
            {
                Response.Write("<script>alert('超时请重新登录！'); window.location.href='/' </script>");
            }
        }
    }
}