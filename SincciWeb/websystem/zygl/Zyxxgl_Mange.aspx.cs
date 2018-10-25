using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
using System.Text;
using System.Data;

namespace SincciKC.websystem.zygl
{

    public partial class Zyxxgl_Mange : BPage
    {
        #region "加载志愿表格数据"
      
        /// <summary>
        /// 志愿定制BLL
        /// </summary>
        private BLL_zk_zydz bll = new BLL_zk_zydz();
        public Model_zk_ksxxgl info = new Model_zk_ksxxgl();
        
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
            string pcdm = Request.QueryString["pcdm"].ToString().Trim();
            dpcdt = bll.Select_All_DpcIsPass_2(xqdm, pcdm.Substring(0, 1));
            ////判断大批次的填报时间
            //for (int i = 0; i < dpcdt.Rows.Count; i++)
            //{
            //    if (!(config.DateTimeCompare(DateTime.Now, Convert.ToDateTime(dpcdt.Rows[i]["stime"])) > 0 && config.DateTimeCompare(DateTime.Now, Convert.ToDateTime(dpcdt.Rows[i]["etime"])) < 0))
            //    {
            //        dpcdt.Rows.Remove(dpcdt.Rows[i]);
            //        i--;
            //    }
            //}
            dpcNum = dpcdt.Rows.Count;
            DataTable dtZyxx = bll.Select_Viewzyxx(ksh, xqdm);
          

            int UserType = SincciLogin.Sessionstu().UserType;
            DataTable lqktab = new DataTable();//查询lqk.招生学生.只能看到录取批次的志愿信息
            if (UserType == 8 || UserType == 9)
            {
                BLL_LQK_Ks_Xx lqk = new BLL_LQK_Ks_Xx();
                lqktab = lqk.selectksh(" ksh='" + info.Ksh + "'");
            }
            if (dpcdt.Rows.Count == 0)
            {
                str.Append("该考生尚未填报!");
                divShow.Visible = false;
                divYin.Visible = true;
                return;
            }
            //头
            if (dtZyxx == null || dtZyxx.Rows.Count == 0 || dtZyxx.Select("pcdm='" + dpcdt.Rows[0]["dpcDm"].ToString()+"1" + "'").Length==0)  //判断是否有数据
            {
                str.Append("该考生尚未填报!");
                divShow.Visible = false;
                divYin.Visible = true;
            }
            else
            {
                divYin.Visible = false;
                divShow.Visible = true;
                //头
                str.Append("<table   border=\"1\" cellpadding=\"0\" width=\"700\" class=\"tblcss\"  bordercolor=\"#1D9494\"  cellspacing=\"0\" >");
                str.Append(" <tr><td style=\"width:20px \">批次</td> <td   width=\"100\" style=\"word-wrap: break-word;\">小批</td>");
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
                           // str.Append("<td rowspan=\"" + (zdt.Rows.Count + 1) + "\">" + shuzi + "</td>");
                            xpcbian++;
                        }
                        else
                        {
                            str.Append("<tr>");
                        }
                        str.Append("<td rowspan=\"" + (zdt.Rows.Count + 1) + "\"  width=\"100\"  >" + dr["xpcMc"]);
                        //string fuc = "";
                        //if (Convert.ToBoolean(bll.Select_kjbs_fc(dr["xpcId"].ToString(), ksh).Rows[0]["sfbk"]))
                        //{
                        //    fuc = "报考";
                        //}
                        //else
                        //{
                        //    fuc = "不报考";
                        //}
                        //str.Append("<br /> <span id=\"xpc1" + dr["xpcId"] + "\" name=\"xpc1" + dr["xpcId"] + "\">" + fuc + "</span>");

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

                                        str.Append("<td   style=\"width:100px \"  >是否服从其他专业</td>");
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
                            str.Append("<tr><td style=\"width:15px \">" + zydr["zyMc"] + "</td>");  //A
                            DataTable dtxd = bll.Select_kjbs(dr["xpcId"] + "_" + zydr["zyId"], ksh);
                            string xxdm = dtxd.Rows[0]["xxdm"].ToString();
                            string zsxxmc = dtxd.Rows[0]["zsxxmc"].ToString();
                            if (lqktab.Rows.Count > 0)
                            {
                                if (lqktab.Rows[0]["pcdm"].ToString() == dtxd.Rows[0]["pcdm"].ToString())
                                {
                                    str.Append("<td  style=\" color:Blue;width:30px\">" + xxdm + "</td>");  //学校代码
                                }
                                else
                                {
                                    str.Append("<td  style=\" color:Blue;width:30px\"></td>");  //学校代码
                                }
                            }
                            else
                            {
                                str.Append("<td  style=\" color:Blue;width:30px\">" + xxdm + "</td>");  //学校代码
                            }
                            if (lqktab.Rows.Count > 0)
                            {
                                if (lqktab.Rows[0]["pcdm"].ToString() == dtxd.Rows[0]["pcdm"].ToString())
                                {
                                    if (Convert.ToInt32(zydr["zySl"]) > 0)//是否有专业和服从   专业数量>0
                                    {

                                        str.Append("<td style=\" color:Blue\"  align=\"left\"   width=\"120px\">" + zsxxmc + "</td>");  //学校名称
                                    }
                                    else
                                    {

                                        str.Append("<td  style=\" color:Blue\"  colspan=\"" + 4 + "\"  align=\"left\" >" + zsxxmc + "</td>");  //学校名称
                                    }
                                }
                                else
                                {
                                    if (Convert.ToInt32(zydr["zySl"]) > 0)//是否有专业和服从   专业数量>0
                                    {

                                        str.Append("<td style=\" color:Blue\"   align=\"left\"   width=\"120px\"></td>");  //学校名称
                                    }
                                    else
                                    {

                                        str.Append("<td  style=\" color:Blue\"  colspan=\"" + 4 + "\"  align=\"left\" ></td>");  //学校名称
                                    }
                                }
                            }
                            else
                            {
                                if (Convert.ToInt32(zydr["zySl"]) > 0)//是否有专业和服从   专业数量>0
                                {

                                    str.Append("<td style=\" color:Blue\"   align=\"left\"   width=\"120px\" >" + zsxxmc + "</td>");  //学校名称
                                }
                                else
                                {

                                    str.Append("<td  style=\" color:Blue\"  colspan=\"" + 4 + "\"  align=\"left\" >" + zsxxmc + "</td>");  //学校名称
                                }
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
                                str.Append("<table  align=\"left\" cellpadding=\"0\" cellspacing=\"0\" >");



                                for (int i = 1; i <= Convert.ToInt32(zydr["zySl"]); i++)
                                {
                                    str.Append("<tr><td style=\" text-align:right\"  width=\"50px\">" + "专业" + config.GetDaoXie(i.ToString()) + "：</td>"); //专业
                                    string zymc = dtxd.Rows[0]["zy" + i] + ":" + dtxd.Rows[0]["zy" + i + "_" + i];
                                    if (zymc.Length == 1)
                                        zymc = "";

                                    if (lqktab.Rows.Count > 0)
                                    {
                                        if (lqktab.Rows[0]["pcdm"].ToString() == dtxd.Rows[0]["pcdm"].ToString())
                                        {
                                            if (zymc.Length > 15)
                                            {
                                                str.Append("<td align=\"left\"  style=\" color:Blue\"   width=\"200px\"> " + zymc.Substring(0, 15) + " </td></tr>");
                                            }
                                            else
                                            {
                                                str.Append("<td align=\"left\"  style=\" color:Blue\"  width=\"200px\"> " + zymc + " </td></tr>");
                                            }
                                        }
                                        else
                                        {

                                            str.Append("<td align=\"left\"  style=\" color:Blue\"    width=\"200px\">   </td></tr>");
                                    
                                        }
                                    }
                                    else
                                    {
                                        if (zymc.Length > 15)
                                        {
                                            str.Append("<td align=\"left\"  style=\" color:Blue\"   width=\"200px\"> " + zymc.Substring(0, 15) + " </td></tr>");
                                        }
                                        else
                                        {
                                            str.Append("<td align=\"left\"  style=\" color:Blue\"   width=\"200px\"> " + zymc + " </td></tr>");
                                        }
                                    }
                                 
                                  
                                }
                                str.Append("</table>");
                                str.Append("</td>");
                                if (zdt.Select(" sfZyFc=1").Length > 0)//有服从
                                {
                                    string zyfc = "";
                                    if (Convert.ToBoolean(dtxd.Rows[0]["zyfc"]))
                                        zyfc = "服从";
                                    else
                                    {
                                        if (dtxd.Rows[0]["zy1"].ToString() == "")
                                        {
                                            zyfc = "";
                                        }
                                        else
                                        {
                                            zyfc = "不服从";
                                        }

                                    }
                                    if (lqktab.Rows.Count > 0)
                                    {
                                        if (lqktab.Rows[0]["pcdm"].ToString() == dtxd.Rows[0]["pcdm"].ToString())
                                        {
                                            str.Append("<td     style=\" color:Blue\"> " + zyfc + "</td>");  //是否有服从
                                        }
                                        else
                                        {
                                            str.Append("<td     style=\" color:Blue\">  </td>");  //是否有服从
                                        }
                                    }
                                    else
                                    {
                                        str.Append("<td     style=\" color:Blue\"> " + zyfc + "</td>");  //是否有服从
                                    }
                                  
                                }

                            }
                            string sfxxfc = dtxd.Rows[0]["sfxxfc"].ToString();
                            DataTable tabsfbk = bll.Select_kjbs_fc(dr["xpcId"].ToString(), ksh);
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
                                if (lqktab.Rows.Count > 0)
                                {
                                    if (lqktab.Rows[0]["pcdm"].ToString() == dtxd.Rows[0]["pcdm"].ToString())
                                    {
                                        str.Append("<td   rowspan=\"" + rows + "\" style=\" color:Blue\">" + sfxxfc + "</td>");  //是否有学校服从
                        
                                    }
                                    else
                                    {
                                        str.Append("<td   rowspan=\"" + rows + "\" style=\" color:Blue\"> </td>");  //是否有学校服从
                        
                                    }
                                }
                                else
                                {
                                    str.Append("<td   rowspan=\"" + rows + "\" style=\" color:Blue\">" + sfxxfc + "</td>");  //是否有学校服从
                        
                                }
                              }
                            str.Append("</tr>");

                        }
                    }
                }

                //尾
                str.Append(" </table>");
                
            }
            zyspan.InnerHtml = str.ToString();
            // ShowLook( ksh);
        }
        #endregion

        /// <summary>
        /// 考生信息管理
        /// </summary>

        public string ksh = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
                if (Request.QueryString["ksh"] != null)
                {
                    ksh = Request.QueryString["ksh"].ToString().Trim();
                    int UserType = SincciLogin.Sessionstu().UserType;
                    string Department = SincciLogin.Sessionstu().U_department;
                    info = new BLL_zk_ksxxgl().ViewDisp(ksh);
                    switch (UserType)
                    {
                        //系统管理员
                        case 1:
                        case 9:
                        case 8:
                            break;
                        //市招生办
                        case 2:
                            break;
                        //区招生办
                        case 3:
                            if (Department != ksh.Trim().Substring(0, 3))
                            {
                                Response.Write("你没有页面查看的权限！");
                                Response.End();
                            }
                            break;
                        //学校用户 
                        case 4:
                            if (Department != info.Bmddm)
                            {
                                Response.Write("你没有页面查看的权限！");
                                Response.End();
                            }
                            break;
                        default:
                            Response.Write("你没有页面查看的权限！");
                            Response.End();
                            break;
                    }
                    string type = "";
                    if (Request.QueryString["type"] != null)
                    {
                        type = Request.QueryString["type"].ToString().Trim();
                        if (type == "1")
                        {
                            
                         //   divprint.Visible = false;
                        }
                        else if (type == "2")
                        {
                          
                            //divprint.Visible = true;
                        }
                        else
                        {
                            Response.Write("<script>alert('您的操作有误！');</script>");
                        }


                    }
                    else
                        Response.Write("<script>alert('您的操作有误！');</script>");

                    string xqdm = bll.Select_zy_xqdm(ksh.Substring(0, 3));

                  

                    ShowLoad(xqdm, ksh);
                }
                else
                {
                    Response.Write("<script>alert('您的操作有误！');</script>");

                }
            }
        }







    }
}