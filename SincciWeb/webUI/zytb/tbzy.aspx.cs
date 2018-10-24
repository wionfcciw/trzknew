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

namespace SincciKC.webUI.zytb
{
    public partial class tbzy : SPage //wf 2018-08-16 修改，单点登录统一控制 System.Web.UI.Page
    {

        #region "加载志愿表格数据"
        /// <summary>
        /// //判断该考生是否存在数据
        /// </summary>
        private bool isZyxx = false;
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
            //志愿定制表的县区
            string xqdmbr = xqdm;
            DataTable zydt = new DataTable();
            zydt = bll.Select_zy_Num(xqdmbr);
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
            dpcdt = bll.Select_All_DpcIsPass(xqdmbr);
            //判断大批次的填报时间
            for (int i = 0; i < dpcdt.Rows.Count; i++)
            {
                if (!(config.DateTimeCompare(DateTime.Now, Convert.ToDateTime(dpcdt.Rows[i]["stime"])) > 0 && config.DateTimeCompare(DateTime.Now, Convert.ToDateTime(dpcdt.Rows[i]["etime"])) < 0))
                {
                    dpcdt.Rows.Remove(dpcdt.Rows[i]);
                    i--;
                }
            }
            if (dpcdt.Rows.Count == 0)
            {

                Response.Write("<script>alert('现在不是网上志愿填报时间！'); history.back(); </script>");
                return;
            }

            dpcNum = dpcdt.Rows.Count;
            DataTable dtZyxx = bll.Select_Viewzyxx(ksh, xqdmbr);
            if (dtZyxx.Rows.Count > 0)
            {
                hidpcdm.Value = dtZyxx.Rows[0]["pcdm"].ToString();
                btn_quxiao.Visible = true;
                isZyxx = true;
            }
            else
            {
                btn_quxiao.Visible = false;
            }
          
            //头
            str.Append("<table border=\"1\" cellpadding=\"0\" width=\"847\" class=\"tblcss\"  bordercolor=\"#1D9494\"  cellspacing=\"0\" >");
            str.Append(" <tr><td style=\"width:20px \">批次</td> <td   width=\"130\" style=\"word-wrap: break-word;\">小批</td>");
            str.Append("<td colspan=\"" + 6 + "\" >志愿信息</td></tr>");
            //str.Append(" <tr><td style=\"width:20px \">批次</td><td style=\"  \">顺序</td></tr>");

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
                    str.Append("<td rowspan=\"" + (zdt.Rows.Count + 1) + "\"  width=\"130\" style=\"word-wrap: break-word;\" id=\"mc_" + dr["xpcId"] + "\">" + dr["xpcMc"]);
                    string zynum = "";//存储zyid
                    for (int i = 0; i < zdt.Rows.Count; i++)
                    {
                        zynum = zynum + zdt.Rows[i]["zyId"].ToString() + ",";
                    }
                    if (zynum.Length > 0)
                    {
                        zynum = zynum.Remove(zynum.Length - 1);
                    }
                    if (isZyxx)
                    {
                        if (dtZyxx.Select(" xpcid='" + dr["xpcId"] + "'" + "and sfbk=1 ").Length > 0)
                        {
                            if (dtZyxx.Select(" xpcid='" + dr["xpcId"] + "'" + "and sfbk=1 ")[0]["kjbs"].ToString() != "")
                            {
                                str.Append("<input id=\"pd_" + dr["xpcId"] + "\" value=\"" + zynum + "\" type=\"hidden\" />");
                                str.Append("<br/><input  checked=\"checked\" onclick='cheakbk(\"" + dr["xpcId"] + "\",1)' id=\"xpc1" + dr["xpcId"] + "\" type=\"radio\" value=\"1\" name=\"xpc" + dr["xpcMc"] + "\" />报考&nbsp;&nbsp;<input onclick='cheakbk(\"" + dr["xpcId"] + "\",0)' id=\"xpc2" + dr["xpcId"] + "\"  name=\"xpc" + dr["xpcMc"] + "\" type=\"radio\" value=\"0\"/>不报考</td>");
                              
                            }
                            else
                            {
                                str.Append("<br/><input id=\"pd_" + dr["xpcId"] + "\" value=\"" + zynum + "\" type=\"hidden\" /><input onclick='cheakbk(\"" + dr["xpcId"] + "\",1)' id=\"xpc1" + dr["xpcId"] + "\" type=\"radio\" value=\"1\" name=\"xpc" + dr["xpcMc"] + "\" />报考&nbsp;&nbsp;<input onclick='cheakbk(\"" + dr["xpcId"] + "\",0)' id=\"xpc2" + dr["xpcId"] + "\"  name=\"xpc" + dr["xpcMc"] + "\" type=\"radio\" value=\"0\" checked=\"checked\" />不报考</td>");

                            }
                        }
                        else if (dtZyxx.Select(" xpcid='" + dr["xpcId"] + "'" + "and sfbk=0").Length > 0)
                        {
                            if (dtZyxx.Select(" xpcid='" + dr["xpcId"] + "'" + "and sfbk=0 ")[0]["kjbs"].ToString() != "")
                            {
                                str.Append("<input id=\"pd_" + dr["xpcId"] + "\" value=\"" + zynum + "\" type=\"hidden\" />");
                                str.Append("<br/><input onclick='cheakbk(\"" + dr["xpcId"] + "\",1)' id=\"xpc1" + dr["xpcId"] + "\" type=\"radio\" value=\"1\" name=\"xpc" + dr["xpcMc"] + "\" />报考&nbsp;&nbsp;<input onclick='cheakbk(\"" + dr["xpcId"] + "\",0)' id=\"xpc2" + dr["xpcId"] + "\"  name=\"xpc" + dr["xpcMc"] + "\" type=\"radio\" value=\"0\" checked=\"checked\" />不报考</td>");

                            }
                            else
                            {
                                str.Append("<br/><input id=\"pd_" + dr["xpcId"] + "\" value=\"" + zynum + "\" type=\"hidden\" /><input onclick='cheakbk(\"" + dr["xpcId"] + "\",1)' id=\"xpc1" + dr["xpcId"] + "\" type=\"radio\" value=\"1\" name=\"xpc" + dr["xpcMc"] + "\" />报考&nbsp;&nbsp;<input onclick='cheakbk(\"" + dr["xpcId"] + "\",0)' id=\"xpc2" + dr["xpcId"] + "\"  name=\"xpc" + dr["xpcMc"] + "\" type=\"radio\" value=\"0\" checked=\"checked\" />不报考</td>");

                            }
                        }
                        else
                        {
                            str.Append("<br/><input id=\"pd_" + dr["xpcId"] + "\" value=\"" + zynum + "\" type=\"hidden\" /><input onclick='cheakbk(\"" + dr["xpcId"] + "\",1)' id=\"xpc1" + dr["xpcId"] + "\" type=\"radio\" value=\"1\" name=\"xpc" + dr["xpcMc"] + "\" />报考&nbsp;&nbsp;<input onclick='cheakbk(\"" + dr["xpcId"] + "\",0)' id=\"xpc2" + dr["xpcId"] + "\"  name=\"xpc" + dr["xpcMc"] + "\" type=\"radio\" value=\"0\" checked=\"checked\" />不报考</td>");

                        }
                    }
                    else
                    {
                        str.Append("<br/><input id=\"pd_" + dr["xpcId"] + "\" value=\"" + zynum + "\" type=\"hidden\" /><input onclick='cheakbk(\"" + dr["xpcId"] + "\",1)' id=\"xpc1" + dr["xpcId"] + "\" type=\"radio\" value=\"1\" name=\"xpc" + dr["xpcMc"] + "\" />报考&nbsp;&nbsp;<input onclick='cheakbk(\"" + dr["xpcId"] + "\",0)' id=\"xpc2" + dr["xpcId"] + "\"  name=\"xpc" + dr["xpcMc"] + "\" type=\"radio\" value=\"0\" checked=\"checked\" />不报考</td>");
                    }

                    shuzi++;

                    int bianl = 0; //变量 =0的时候.第一个小批次加标题

                    foreach (DataRow zydr in zdt.Rows) //循环专业
                    {
                        if (bianl == 0)
                        {


                            if (Convert.ToInt32(zydr["zySl"]) > 0)//是否有专业和服从   专业数量>0
                            {
                                str.Append("<td colspan=\"2\" >学校代码</td><td>学校名称</td>");

                                if (zdt.Select(" sfZyFc=1").Length > 0)//有服从
                                {

                                    str.Append("<td >专业</td>");
                                    str.Append("<td  >是否服从<br/>其他专业</td>");
                                }
                                else
                                {

                                    str.Append("<td colspan=\"3\">专业</td>");

                                }

                            }
                            else
                            {

                                str.Append("<td colspan=\"2\" style=\"  \" >学校代码</td><td colspan=\"" + 4 + "\"   >学校名称</td>");

                            }
                            if (Convert.ToBoolean(zydr["sfxxfc"]))
                            {
                                str.Append("<td >其他学校<br/>是否服从</td>");
                            }
                            str.Append("</tr>");
                        }
                        bianl++;
                        //zdt[0] 得到最大的专业数量
                        str.Append("<tr><td width=\"50\">" + zydr["zyMc"] + "</td>");  //A
                        if (isZyxx)
                        {
                            if (dtZyxx.Select(" kjbs='" + dr["xpcId"] + "_" + zydr["zyId"] + "'").Length > 0)
                            {
                                if (dtZyxx.Select(" kjbs='" + dr["xpcId"] + "_" + zydr["zyId"] + "'")[0]["xxdm"].ToString() != "")
                                {
                                    str.Append("<td  width=\"120\"> <input  style=\" color:Blue\"  class=\"input1\" readonly=\"readonly\" type=\"text\" value=\"" + dtZyxx.Select(" kjbs='" + dr["xpcId"] + "_" + zydr["zyId"] + "'")[0]["xxdm"] + "\" class=\"btn\"  onclick=\"showPop('" + dr["xpcMc"] + "','tbzy_xx.aspx?xqdm=" + xqdmbr + "&pcdm=" + dr["pcDm"] + "&zyId=" + dr["xpcId"] + "_" + zydr["zyId"] + "', 460, 370, MessageBox,true,true,'" + dr["xpcId"] + "_" + zydr["zyId"] + "');\" id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_dm\" name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_dm\" /></td>");  //学校代码
                                    hidxxdm.Value = dtZyxx.Select(" kjbs='" + dr["xpcId"] + "_" + zydr["zyId"] + "'")[0]["xxdm"].ToString();
                                }
                                else
                                {
                                    if (dtZyxx.Select(" xpcid='" + dr["xpcId"] + "' and sfbk=1").Length > 0)
                                    {
                                        str.Append("<td width=\"120\"> <input   style=\" color:Blue\" class=\"input1\" readonly=\"readonly\" type=\"text\" value=\"" + dtZyxx.Select(" kjbs='" + dr["xpcId"] + "_" + zydr["zyId"] + "'")[0]["xxdm"] + "\" class=\"btn\"  onclick=\"showPop('" + dr["xpcMc"] + "','tbzy_xx.aspx?xqdm=" + xqdmbr + "&pcdm=" + dr["pcDm"] + "&zyId=" + dr["xpcId"] + "_" + zydr["zyId"] + "', 460, 370, MessageBox,true,true,'" + dr["xpcId"] + "_" + zydr["zyId"] + "');\" id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_dm\" name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_dm\"/></td>");  //学校代码
                                    }
                                    else
                                        str.Append("<td width=\"120\"> <input  style=\" color:Blue\" class=\"input2\"  disabled=\"disabled\"   readonly=\"readonly\" type=\"text\" value=\"" + dtZyxx.Select(" kjbs='" + dr["xpcId"] + "_" + zydr["zyId"] + "'")[0]["xxdm"] + "\" class=\"btn\"  onclick=\"showPop('" + dr["xpcMc"] + "','tbzy_xx.aspx?xqdm=" + xqdmbr + "&pcdm=" + dr["pcDm"] + "&zyId=" + dr["xpcId"] + "_" + zydr["zyId"] + "', 460, 370, MessageBox,true,true,'" + dr["xpcId"] + "_" + zydr["zyId"] + "');\" id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_dm\" name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_dm\"/></td>");  //学校代码
                                }
                            }
                            else
                            {
                                str.Append("<td width=\"120\">  <input style=\" color:Blue\" class=\"input2\"   disabled=\"disabled\"  readonly=\"readonly\" type=\"text\" value=\"\" class=\"btn\"  onclick=\"showPop('" + dr["xpcMc"] + "','tbzy_xx.aspx?xqdm=" + xqdmbr + "&pcdm=" + dr["pcDm"] + "&zyId=" + dr["xpcId"] + "_" + zydr["zyId"] + "', 460, 370, MessageBox,true,true,'" + dr["xpcId"] + "_" + zydr["zyId"] + "');\" id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_dm\" name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_dm\"/></td>");  //学校代码

                            }
                        }
                        else
                        {
                            str.Append("<td width=\"120\">  <input style=\" color:Blue\" class=\"input2\"  disabled=\"disabled\"  readonly=\"readonly\" type=\"text\" value=\"\" class=\"btn\"  onclick=\"showPop('" + dr["xpcMc"] + "','tbzy_xx.aspx?xqdm=" + xqdmbr + "&pcdm=" + dr["pcDm"] + "&zyId=" + dr["xpcId"] + "_" + zydr["zyId"] + "', 460, 370, MessageBox,true,true,'" + dr["xpcId"] + "_" + zydr["zyId"] + "');\" id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_dm\" name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_dm\"/></td>");  //学校代码

                        }

                        if (Convert.ToInt32(zydr["zySl"]) > 0)//是否有专业和服从   专业数量>0
                        {
                            if (isZyxx)
                            {
                                if (dtZyxx.Select(" kjbs='" + dr["xpcId"] + "_" + zydr["zyId"] + "'").Length > 0)
                                {


                                    str.Append("<td  width=\"150\"  style=\" color:Blue\" style=\"word-wrap: break-word;\" id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_mc\" >" + dtZyxx.Select(" kjbs='" + dr["xpcId"] + "_" + zydr["zyId"] + "'")[0]["zsxxmc"] + "</td>");  //学校名称

                                }
                                else
                                {
                                    str.Append("<td  width=\"150\"  style=\" color:Blue\" style=\"word-wrap: break-word;\" id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_mc\"  ></td>");  //学校名称

                                }
                            }
                            else
                            {


                                str.Append("<td   width=\"150\" style=\" color:Blue\" style=\"word-wrap: break-word;\" id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_mc\"  ></td>");  //学校名称
                            }

                        }
                        else
                        {
                            if (isZyxx)
                            {
                                if (dtZyxx.Select(" kjbs='" + dr["xpcId"] + "_" + zydr["zyId"] + "'").Length > 0)
                                {
                                    str.Append("<td  style=\" color:Blue\" colspan=\"" + 4 + "\" id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_mc\"  >" + dtZyxx.Select(" kjbs='" + dr["xpcId"] + "_" + zydr["zyId"] + "'")[0]["zsxxmc"] + "</td>");  //学校名称
                                }
                                else
                                {
                                    str.Append("<td  style=\" color:Blue\" colspan=\"" + 4 + "\" id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_mc\"  ></td>");  //学校名称

                                }

                            }
                            else
                            {
                                str.Append("<td style=\" color:Blue\"  colspan=\"" + 4 + "\" id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_mc\"  ></td>");  //学校名称
                            }

                        }

                        if (Convert.ToInt32(zydr["zySl"]) > 0)
                        {

                            if (zdt.Select(" sfZyFc=1").Length > 0)//有服从
                            {

                                str.Append("<td     >");
                            }
                            else
                            {
                                str.Append("<td    colspan=\"3\">");
                            }
                            str.Append("<table  align=\"left\" cellpadding=\"0\" cellspacing=\"0\">");
                            for (int i = 1; i <= Convert.ToInt32(zydr["zySl"]); i++)
                            {
                                str.Append("<tr><td style=\"width:60px; text-align:right\">" + "专业" + config.GetDaoXie(i.ToString()) + ":</td>"); //专业

                                if (isZyxx)
                                {
                                    if (dtZyxx.Select(" kjbs='" + dr["xpcId"] + "_" + zydr["zyId"] + "'").Length > 0)
                                    {
                                        if (dtZyxx.Select(" kjbs='" + dr["xpcId"] + "_" + zydr["zyId"] + "'")[0]["zy" + i].ToString() != "")
                                        {
                                            string name = dtZyxx.Select(" kjbs='" + dr["xpcId"] + "_" + zydr["zyId"] + "'")[0]["zy" + i] + ":" + dtZyxx.Select(" kjbs='" + dr["xpcId"] + "_" + zydr["zyId"] + "'")[0]["zy" + i + "_" + i];
                                            str.Append("<td ><input class=\"input1\"  readonly=\"readonly\" type=\"text\" value=\"" + name + "\" name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_zy" + (i - 1) + "\" id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_zy" + (i - 1) + "\"  class=\"btn\"   onclick=\"ShowPc('" + dr["xpcMc"] + "','tbzy_xx.aspx?xqdm=" + xqdmbr + "&pcdm=" + dr["pcDm"] + "&zyId=" + dr["xpcId"] + "_" + zydr["zyId"] + "_zy" + (i - 1) + "', 460, 370, MessageBoxZY,'" + dr["xpcId"] + "_" + zydr["zyId"] + "');\"  /></td></tr>");
                                        }
                                        else
                                        {
                                            //string name = dtZyxx.Select(" kjbs='" + dr["xpcId"] + "_" + zydr["zyId"] + "'")[0]["zy" + i] + ":" + dtZyxx.Select(" kjbs='" + dr["xpcId"] + "_" + zydr["zyId"] + "'")[0]["zy" + i + "_" + i];
                                            if (i > 1)
                                            {
                                                if (dtZyxx.Select(" kjbs='" + dr["xpcId"] + "_" + zydr["zyId"] + "'")[0]["zy" + (i - 1)].ToString() != "")
                                                {
                                                    str.Append("<td ><input class=\"input1\"  readonly=\"readonly\" type=\"text\" value=\"\" name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_zy" + (i - 1) + "\" id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_zy" + (i - 1) + "\"  class=\"btn\"   onclick=\"ShowPc('" + dr["xpcMc"] + "','tbzy_xx.aspx?xqdm=" + xqdmbr + "&pcdm=" + dr["pcDm"] + "&zyId=" + dr["xpcId"] + "_" + zydr["zyId"] + "_zy" + (i - 1) + "', 460, 370, MessageBoxZY,'" + dr["xpcId"] + "_" + zydr["zyId"] + "');\"  /></td></tr>");
                                                }
                                                else
                                                {
                                                    str.Append("<td ><input class=\"input2\" disabled=\"disabled\" readonly=\"readonly\" type=\"text\" value=\"\" name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_zy" + (i - 1) + "\" id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_zy" + (i - 1) + "\"  class=\"btn\"   onclick=\"ShowPc('" + dr["xpcMc"] + "','tbzy_xx.aspx?xqdm=" + xqdmbr + "&pcdm=" + dr["pcDm"] + "&zyId=" + dr["xpcId"] + "_" + zydr["zyId"] + "_zy" + (i - 1) + "', 460, 370, MessageBoxZY,'" + dr["xpcId"] + "_" + zydr["zyId"] + "');\"  /></td></tr>");

                                                }
                                            }
                                            else
                                            {
                                                str.Append("<td ><input class=\"input2\" disabled=\"disabled\" readonly=\"readonly\" type=\"text\" value=\"\" name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_zy" + (i - 1) + "\" id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_zy" + (i - 1) + "\"  class=\"btn\"   onclick=\"ShowPc('" + dr["xpcMc"] + "','tbzy_xx.aspx?xqdm=" + xqdmbr + "&pcdm=" + dr["pcDm"] + "&zyId=" + dr["xpcId"] + "_" + zydr["zyId"] + "_zy" + (i - 1) + "', 460, 370, MessageBoxZY,'" + dr["xpcId"] + "_" + zydr["zyId"] + "');\"  /></td></tr>");

                                            }

                                        }
                                    }
                                    else
                                    {
                                        str.Append("<td > <input class=\"input2\" disabled=\"disabled\" readonly=\"readonly\" type=\"text\" value=\"\" name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_zy" + (i - 1) + "\" id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_zy" + (i - 1) + "\"  class=\"btn\"   onclick=\"ShowPc('" + dr["xpcMc"] + "','tbzy_xx.aspx?xqdm=" + xqdmbr + "&pcdm=" + dr["pcDm"] + "&zyId=" + dr["xpcId"] + "_" + zydr["zyId"] + "_zy" + (i - 1) + "', 460, 370, MessageBoxZY,'" + dr["xpcId"] + "_" + zydr["zyId"] + "');\"  /></td></tr>");

                                    }
                                }
                                else
                                {

                                    str.Append("<td > <input class=\"input2\" disabled=\"disabled\" readonly=\"readonly\" type=\"text\" value=\"\" name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_zy" + (i - 1) + "\" id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_zy" + (i - 1) + "\"  class=\"btn\"   onclick=\"ShowPc('" + dr["xpcMc"] + "','tbzy_xx.aspx?xqdm=" + xqdmbr + "&pcdm=" + dr["pcDm"] + "&zyId=" + dr["xpcId"] + "_" + zydr["zyId"] + "_zy" + (i - 1) + "', 460, 370, MessageBoxZY,'" + dr["xpcId"] + "_" + zydr["zyId"] + "');\"  /></td></tr>");

                                }

                            }
                            str.Append("</table>");
                            str.Append("</td>");

                            if (zdt.Select(" sfZyFc=1").Length > 0)//有服从
                            {
                                if (isZyxx)
                                {
                                    if (dtZyxx.Select(" kjbs='" + dr["xpcId"] + "_" + zydr["zyId"] + "'").Length > 0)
                                    {

                                        if (dtZyxx.Select(" kjbs='" + dr["xpcId"] + "_" + zydr["zyId"] + "'")[0]["zyfc"].ToString() == "True")
                                        {

                                            str.Append("<td align=\"left\"><input checked=\"checked\"  id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_fc1\" type=\"radio\" value=\"1\" name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_fc\" />是<br /><input  id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_fc2\"   name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_fc\" type=\"radio\" value=\"0\"   />否</td>");  //是否有服从
                                        }
                                        else
                                        {

                                            if (dtZyxx.Select(" kjbs='" + dr["xpcId"] + "_" + zydr["zyId"] + "' and sfbk=1 and zy1<>''").Length > 0)
                                            {

                                                str.Append("<td align=\"left\"><input   id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_fc1\" type=\"radio\" value=\"1\" name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_fc\" />是<br /><input    id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_fc2\"   name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_fc\" type=\"radio\" value=\"0\" checked=\"checked\"  />否</td>");  //是否有服从

                                            }
                                            else
                                            {
                                                str.Append("<td align=\"left\"><input disabled=\"disabled\" id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_fc1\" type=\"radio\" value=\"1\" name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_fc\" />是<br /><input disabled=\"disabled\"  id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_fc2\"   name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_fc\" type=\"radio\" value=\"0\" checked=\"checked\"  />否</td>");  //是否有服从


                                            }
                                        }
                                    }
                                    else
                                    {
                                        str.Append("<td align=\"left\"><input  disabled=\"disabled\" id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_fc1\" type=\"radio\" value=\"1\" name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_fc\" />是<br /><input disabled=\"disabled\" id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_fc2\"   name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_fc\" type=\"radio\" value=\"0\" checked=\"checked\"  />否</td>");  //是否有服从

                                    }
                                }
                                else
                                {
                                    str.Append("<td align=\"left\"><input  disabled=\"disabled\" id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_fc1\" type=\"radio\" value=\"1\" name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_fc\" />是<br /><input disabled=\"disabled\" id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_fc2\"   name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_fc\" type=\"radio\" value=\"0\" checked=\"checked\"  />否</td>");  //是否有服从

                                }
                            }

                        }
                        int rows = zdt.Rows.Count;
                        if (isZyxx)
                        {
                            if (Convert.ToBoolean(zydr["sfxxfc"]))//是否有学校服从
                            {
                                if (dtZyxx.Select(" xpcid='" + dr["xpcId"] + "' and sfbk=1").Length > 0)
                                {
                                    if (Convert.ToBoolean(dtZyxx.Select(" kjbs='" + dr["xpcId"] + "_" + zydr["zyId"] + "'")[0]["sfxxfc"])) //学校的服从状态
                                        str.Append("<td align=\"left\" rowspan=\"" + rows + "\"><input checked=\"checked\"   id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_xxfc1\" type=\"radio\" value=\"1\" name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_xxfc\" />是<br /><input   id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_xxfc2\"   name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_xxfc\" type=\"radio\" value=\"0\"   />否</td>");  //是否有学校服从
                                    else
                                        str.Append("<td align=\"left\" rowspan=\"" + rows + "\"><input   id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_xxfc1\" type=\"radio\" value=\"1\" name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_xxfc\" />是<br /><input checked=\"checked\"   id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_xxfc2\"   name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_xxfc\" type=\"radio\" value=\"0\"   />否</td>");  //是否有学校服从

                                }
                                else
                                {
                                    str.Append("<td align=\"left\" rowspan=\"" + rows + "\"><input disabled=\"disabled\"  id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_xxfc1\" type=\"radio\" value=\"1\" name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_xxfc\" />是<br /><input checked=\"checked\" disabled=\"disabled\" id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_xxfc2\"   name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_xxfc\" type=\"radio\" value=\"0\"   />否</td>");  //是否有学校服从

                                }

                            }
                        }
                        else
                        {
                            if (Convert.ToBoolean(zydr["sfxxfc"]))//是否有学校服从
                            {
                                str.Append("<td align=\"left\" rowspan=\"" + rows + "\"><input disabled=\"disabled\"  id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_xxfc1\" type=\"radio\" value=\"1\" name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_xxfc\" />是<br /><input checked=\"checked\" disabled=\"disabled\" id=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_xxfc2\"   name=\"" + dr["xpcId"] + "_" + zydr["zyId"] + "_xxfc\" type=\"radio\" value=\"0\"   />否</td>");  //是否有学校服从
                            }
                        }
                        str.Append("</tr>");

                    }
                }
            }

            //尾
            str.Append(" </table>");
            zyspan.InnerHtml = str.ToString();
            ShowLook(xqdmbr, ksh);
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

                        Model_zk_ksSession model = BLL_Ks_Session.ksSession();
                        ksh = model.ksh;
                        string xqdm = "500"; //bll.Select_zy_xqdm(model.Bmdxqdm);

                        //if (!new Bll_zkbm_Time().zkbm_time(xqdm, 2))
                        //{
                        //    Response.Write("<script>alert('现在不是网上志愿填报时间！'); history.back(); </script>");
                        //}
                        //else
                        //{
                        info = new BLL_zk_ksxxgl().ViewDisp(ksh);
                        if (info.Xxdy == 1)
                        {
                            if (info.Zyksqr == 2)
                            {
                                Response.Redirect("tbzyLook.aspx", false);
                            }

                            this.lblKaocimc.Text = info.Kaocimc;
                            this.lblBmdmc.Text = info.Bmdmc;
                            this.lblKsh.Text = info.Ksh;
                            this.lblXm.Text = info.Xm;
                            lblxqmc.Text = info.xqmc;
                            Model_zk_lqk lqk = new BLL_zk_lqk().Select_zk_lqk(ksh);
                            if (lqk.Td_zt == 5)
                            {
                                Response.Write("<script>alert('您已被录取,无法填报志愿！'); history.back(); </script>");
                                return;
                            }

                            ShowLoad(xqdm, ksh);
                        }
                        else
                        {
                            Response.Write("<script>alert('您的状态有误,不能填报志愿！'); history.back(); </script>");

                        }
                        //  }
                    }
                    else
                    {
                        Response.Write("<script>alert('超时请重新登录！'); window.parent.location.href='/' </script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('超时请重新登录！'); window.parent.location.href='/' </script>");
                }

            }
        }

        #region "保存数据"
        /// <summary>
        /// 保存数据
        /// </summary> 
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Session["kaosheng"] != null)
            {
                if (BLL_Ks_Session.ksLogCheck())
                {
                    Model_zk_ksSession modelksh = BLL_Ks_Session.ksSession();
                    ksh = modelksh.ksh;
                    if (ksh != lblKsh.Text)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >alert('操作有误,请关闭浏览器重新填报！');</script>");
               
                        //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'操作有误,请关闭浏览器重新填报!' ,title:'操作提示'}); </script>");
                        return;
                    }
                    string bmdmxqdm = modelksh.Bmdxqdm;
                    string xqdm = bll.Select_zy_xqdm(bmdmxqdm);
                    DataTable zydt = new DataTable();
                    zydt = bll.Select_zy_Num(xqdm);
                    bool Sfbk = false;
                    bool Zyfc = false;
                    string zyNum = "";
                    List<Model_zk_kszyxx> listmod = new List<Model_zk_kszyxx>();
                    string kshstr = ksh; //测试考生
                    bool sfxxfc = false;
                    bool issfbk = false;//标示是否至少报考了一个     
                    bool isxx = false;//是否有选择学校
                    int isxxnum = 0;
                    bool xxpd = true;
                    for (int i = 0; i < zydt.Rows.Count; i++)
                    {
                        if (!(config.DateTimeCompare(DateTime.Now, Convert.ToDateTime(zydt.Rows[i]["stime"])) > 0 && config.DateTimeCompare(DateTime.Now, Convert.ToDateTime(zydt.Rows[i]["etime"])) < 0))
                        {
                            zydt.Rows.Remove(zydt.Rows[i]);
                            i--;
                        }
                    }
                    if (zydt.Rows.Count == 0)
                    {
                        Response.Write("<script>alert('现在不是网上志愿填报时间！'); history.back(); </script>");
                        Response.Redirect("Zytb_Help.aspx", false);
                        return;
                    }
                    foreach (DataRow item in zydt.Rows)
                    {

                        Model_zk_kszyxx mod = new Model_zk_kszyxx();
                        Sfbk = Convert.ToBoolean(Convert.ToInt32(Request.Form["xpc" + item["xpcMc"]]));
                        mod.Ksh = ksh;
                        mod.Zysx = Convert.ToInt32(item["zyDm"]);
                        //mod.Xzdm = "";
                        mod.Pcdm = item["pcDm"].ToString();
                        if (Sfbk)
                        {


                            try
                            {
                                mod.Xxdm = Request.Form[item["xpcId"].ToString() + "_" + item["zyId"].ToString() + "_dm"].Trim();

                                if (mod.Xxdm == null || mod.Xxdm == "")
                                {
                                    mod.Xxdm = "";
                                    Response.Write("<script>alert('保存失败,选择了报考类型时,至少要填报一个学校!！'); </script>");                         
                                    return;
                                }
                            }
                            catch (Exception)
                            {
                            }
                            
                        }
                        else
                        {
                            mod.Xxdm = "";
                        }
                        if (item["csfZyFc"].ToString() == "True")
                        {

                            Zyfc = Convert.ToBoolean(Convert.ToInt32(Request.Form[item["xpcId"].ToString() + "_" + item["zyId"].ToString() + "_fc"]));
                            mod.Zyfc = Zyfc;

                        }
                        if (item["sfxxfc"].ToString() == "True")
                        {
                            sfxxfc = Convert.ToBoolean(Convert.ToInt32(Request.Form[item["xpcId"].ToString() + "_" + item["zyId"].ToString() + "_xxfc"]));
                            mod.Sfxxfc = sfxxfc;
                        }
                        //  mod.Xxfc = true;
                        mod.Lrsj = DateTime.Now;
                        mod.Sfbk = Sfbk;
                        mod.Xqdm = bmdmxqdm;
                        if (!issfbk)
                        {
                            issfbk = Sfbk;
                        }
                        mod.Kjbs = item["xpcId"].ToString() + "_" + item["zyId"].ToString();
                        listmod.Add(mod);
                    }
                    BLL_zk_kszyxx bkszy = new BLL_zk_kszyxx();
                    if (!issfbk)
                    {
                        //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'保存失败,至少填报一个类别！' ,title:'操作提示'}); </script>");

                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >alert('保存失败,至少填报一个类别！');</script>");
               
                    }
                    else
                    {
                     
                        if (!xxpd)
                        {
                            // Response.Write("<script>alert('保存失败,有学校专业未填报！' ); history.back(); </script>");
                         //   Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'保存失败,有学校专业未填报！' ,title:'操作提示'}); </script>");
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >alert('保存失败,有学校专业未填报！');</script>");
               
                            // return;
                        }
                        else
                        {
                            //bll.delete_zk_kszyxx(listmod);
                            string ip = BLL_Ks_Session.ksSession().Ipaddress;
                            if (bkszy.Insert_zk_kszyxx(listmod, ip))
                            {
                                bll.zk_ksxxglZyksqr(ksh, 1);
                                Response.Redirect("tbzyLook.aspx");
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >alert('保存失败,数据有错！');</script>");
                                return;
                            }
                        }
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
        #endregion

        #region "查看"
        /// <summary>
        /// 查看.
        /// </summary>
        private void ShowLook(string xqdm, string ksh)
        {


            DataTable zydt = new DataTable();
            zydt = bll.Select_zy_Num(xqdm);
            //bool Sfbk = false;
            //bool Zyfc = false;
            //string zyNum = "";
            BLL_zk_zydz bllzy = new BLL_zk_zydz();
            DataTable dpcdt = new DataTable();
            dpcdt = bll.Select_All_DpcIsPass(xqdm); //可填报批次
            for (int i = 0; i < dpcdt.Rows.Count; i++)
            {
                if (!(config.DateTimeCompare(DateTime.Now, Convert.ToDateTime(dpcdt.Rows[i]["stime"])) > 0 && config.DateTimeCompare(DateTime.Now, Convert.ToDateTime(dpcdt.Rows[i]["etime"])) < 0))
                {
                    dpcdt.Rows.Remove(dpcdt.Rows[i]);
                    i--;
                }
            }
            zydt = bllzy.Select_Viewzy(ksh);
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            if (zydt.Rows.Count == 0 || zydt == null)
            {
                return;
            }
            string xpcis = "";
            int savenum = 0;
            foreach (DataRow item in zydt.Rows)
            {
                if (dpcdt.Select("dpcDm='" + item["pcdm"].ToString().Substring(0,1) + "'").Length == 0)
                {
                    continue;
                }
                //str.Append("var xpcid = \"" + item["kjbs"].ToString().Split('_')[0] + "\";");
                //str.Append("var zyidSum = document.getElementById(\"pd_\" + xpcid).value.split(\",\").length;");
                //str.Append("var zyid = document.getElementById(\"pd_\" + xpcid).value;");
                //str.Append("if (\"" + item["sfbk"] + "\"==\"True\") { ");
                //str.Append("for (var i = 0; i < zyidSum; i++) {");
                //str.Append("document.getElementById(xpcid + \"_\" + zyid.split(\",\")[i] + \"_dm\").disabled = \"\";");
                //str.Append("  document.getElementById(xpcid + \"_\" + zyid.split(\",\")[i] + \"_dm\").className = \"input1\";");
                //str.Append("for (var j = 0; j < 7; j++) {");
                //str.Append("try { document.getElementById(xpcid + \"_\" + zyid.split(\",\")[i] + \"_zy\" + j).disabled = \"\";");
                //str.Append("  document.getElementById(xpcid + \"_\" + zyid.split(\",\")[i] + \"_zy\" + j).className = \"input1\";");
                //str.Append(" } catch (e) {  break;  }  } try {");
                //str.Append(" document.getElementById(xpcid + \"_\" + zyid.split(\",\")[i] + \"_fc1\").disabled = \"\";");
                //str.Append(" document.getElementById(xpcid + \"_\" + zyid.split(\",\")[i] + \"_fc2\").disabled = \"\";");
                //str.Append("} catch (e) { }");
                //str.Append("} } else {");
                //str.Append("for (var i = 0; i < zyidSum; i++) {");
                //str.Append("document.getElementById(xpcid + \"_\" + zyid.split(\",\")[i] + \"_dm\").disabled = \"disabled\";");
                //str.Append("document.getElementById(xpcid + \"_\" + zyid.split(\",\")[i] + \"_dm\").className = \"input2\";");
                //str.Append("for (var j = 0; j < 7; j++) {");
                //str.Append("try { document.getElementById(xpcid + \"_\" + zyid.split(\",\")[i] + \"_zy\" + j).disabled = \"disabled\";");
                //str.Append("  document.getElementById(xpcid + \"_\" + zyid.split(\",\")[i] + \"_zy\" + j).className = \"input2\";");
                //str.Append("} catch (e) {  break; } } ");
                //str.Append("  try { document.getElementById(xpcid + \"_\" + zyid.split(\",\")[i] + \"_fc1\").disabled = \"disabled\";");
                //str.Append("  document.getElementById(xpcid + \"_\" + zyid.split(\",\")[i] + \"_fc2\").disabled = \"disabled\";");
                //str.Append("} catch (e) { }");
                //str.Append("} }");
                if (!isZyxx)
                {


                    //str.Append("var xpcid = \"" + item["kjbs"].ToString().Split('_')[0] + "\";");
                    //str.Append("var zyidSum = document.getElementById(\"pd_\" + xpcid).value.split(\",\").length;");
                    //str.Append(" var zyid = document.getElementById(\"pd_\" + xpcid).value;");
                    //str.Append("if (\"" + item["sfbk"].ToString() + "\"== \"True\"){");
                    //str.Append("for (var i = 0; i < zyidSum; i++) {");
                    //str.Append("document.getElementById(xpcid + \"_\" + zyid.split(\",\")[i] + \"_dm\").disabled = \"\";");
                    //str.Append("document.getElementById(xpcid + \"_\" + zyid.split(\",\")[i] + \"_dm\").className = \"input1\";");
                    //str.Append("} }else { for (var i = 0; i < zyidSum; i++) {");
                    //str.Append("document.getElementById(xpcid + \"_\" + zyid.split(\",\")[i] + \"_dm\").disabled = \"disabled\";");
                    //str.Append("document.getElementById(xpcid + \"_\" + zyid.split(\",\")[i] + \"_dm\").className = \"input2\";");
                    //str.Append("document.getElementById(xpcid + \"_\" + zyid.split(\",\")[i] + \"_dm\").innerText = \"\";");
                    //str.Append("document.getElementById(xpcid + \"_\" + zyid.split(\",\")[i] + \"_mc\").innerText = \"\";");
                    //str.Append(" for (var j = 0; j < 7; j++) {");
                    //str.Append("try { document.getElementById(xpcid + \"_\" + zyid.split(\",\")[i] + \"_zy\" + j).disabled = \"disabled\";");
                    //str.Append("document.getElementById(xpcid + \"_\" + zyid.split(\",\")[i] + \"_zy\" + j).className = \"input2\";");
                    //str.Append("document.getElementById(xpcid + \"_\" + zyid.split(\",\")[i] + \"_zy\" + j).innerText = \"\";");
                    //str.Append("} catch (e) { break; } } try {");
                    //str.Append("document.getElementById(xpcid + \"_\" + zyid.split(\",\")[i] + \"_fc1\").disabled = \"disabled\";");
                    //str.Append("document.getElementById(xpcid + \"_\" + zyid.split(\",\")[i] + \"_fc2\").disabled = \"disabled\";");
                    //str.Append("document.getElementById(xpcid + \"_\" + zyid.split(\",\")[i] + \"_fc2\").checked = \"checked\";");
                    //str.Append(" } catch (e) { } } } ");
                    //str.Append("");

                    //str.Append("document.getElementById(\"" + item["kjbs"] + "_dm\").innerText=\"" + item["xxdm"] + "\";");


                    //str.Append("document.getElementById(\"" + item["kjbs"] + "_mc\").innerText=\" " + item["zsxxmc"].ToString() + "\";");
                    //if (item["sfbk"].ToString() == "True")
                    //{
                    //    str.Append("document.getElementById(\"xpc1" + item["kjbs"].ToString().Split('_')[0] + "\").checked=\"checked\";");
                    //    if (!xpcis.Contains(item["kjbs"].ToString().Split('_')[0]))
                    //    {
                    //        xpcis = xpcis + item["kjbs"].ToString().Split('_')[0] + ",";
                    //    }

                    //}
                    //else
                    //{
                    //    str.Append("document.getElementById(\"xpc2" + item["kjbs"].ToString().Split('_')[0] + "\").checked=\"checked\";");
                    //}
                    //if (item["zyfc"].ToString() == "True")
                    //{
                    //    str.Append("document.getElementById(\"" + item["kjbs"] + "_fc1\").checked=\"checked\";");
                    //}
                    ////else
                    ////{
                    ////    str.Append("document.getElementById(\"" + item["kjbs"] + "_fc2\").checked=\"checked\";");
                    ////}
                    ////if (item["kjbs"].ToString()=="120022_1200221")
                    ////{

                    ////}
                    //if (item["xxdm"].ToString() != "" && item["zy1"].ToString() != "" && item["zy1"].ToString() != null)
                    //{
                    //    for (int i = 0; i < 7; i++)
                    //    {
                    //        str.Append("try {");
                    //        if (item["zy" + (i + 1)].ToString() != "")
                    //        {
                    //            str.Append("document.getElementById(\"" + item["kjbs"] + "_zy" + i + "\").innerText=\"" + item["zy" + (i + 1)] + ":" + item["zy" + (i + 1) + "_" + (i + 1)] + "\";");
                    //        }
                    //        str.Append("document.getElementById(\"" + item["kjbs"] + "_zy" + i + "\").disabled=\"\";");
                    //        str.Append("document.getElementById(\"" + item["kjbs"] + "_zy" + i + "\").className=\"input1\";");
                    //        str.Append("} catch (e) { }");
                    //    }
                    //    str.Append("document.getElementById(\"" + item["kjbs"] + "_fc1\").disabled=\"\";");
                    //    str.Append("document.getElementById(\"" + item["kjbs"] + "_fc2\").disabled=\"\";");

                    //}
                    //if (item["xxdm"].ToString() != "")
                    //{
                    //    str.Append(" document.getElementById(\"isSave\").value = \"Save\";");
                    //}

                }
                else
                {
                    if (item["sfbk"].ToString() == "True")
                    {
                        if (!xpcis.Contains(item["kjbs"].ToString().Split('_')[0]))
                        {
                            xpcis = xpcis + item["kjbs"].ToString().Split('_')[0] + ",";
                        }
                    }
                    if (item["xxdm"].ToString() != "" && savenum == 0)
                    {
                        str.Append(" document.getElementById(\"isSave\").value = \"Save\";");
                        savenum++;
                    }
                }
                //str.Append("try {");

                //if (item["zy1"].ToString() != "" && item["zy1"].ToString() != null && item["xxdm"].ToString() != "")
                //{
                //    str.Append("document.getElementById(\"" + item["kjbs"] + "_zy0\").innerText=\"" + item["zy1"] + ":" + item["zy1_1"] + "\";");
                //    str.Append("document.getElementById(\"" + item["kjbs"] + "_zy0\").disabled=\"\";");
                //    str.Append("document.getElementById(\"" + item["kjbs"] + "_zy0\").className=\"input1\";");
                //}
                //if (item["zy2"].ToString() != "" && item["zy2"].ToString() != null && item["xxdm"].ToString() != "")
                //{
                //    str.Append("document.getElementById(\"" + item["kjbs"] + "_zy1\").innerText=\"" + item["zy2"] + ":" + item["zy2_2"] + "\";");
                //    str.Append("document.getElementById(\"" + item["kjbs"] + "_zy1\").disabled=\"\";");
                //    str.Append("document.getElementById(\"" + item["kjbs"] + "_zy1\").className=\"input1\";");
                //}
                //if (item["zy3"].ToString() != "" && item["zy3"].ToString() != null && item["xxdm"].ToString() != "")
                //{
                //    str.Append("document.getElementById(\"" + item["kjbs"] + "_zy2\").innerText=\"" + item["zy3"] + ":" + item["zy3_3"] + "\";");
                //    str.Append("document.getElementById(\"" + item["kjbs"] + "_zy2\").disabled=\"\";");
                //    str.Append("document.getElementById(\"" + item["kjbs"] + "_zy2\").className=\"input1\";");
                //}
                //if (item["zy4"].ToString() != "" && item["zy4"].ToString() != null && item["xxdm"].ToString() != "")
                //{
                //    str.Append("document.getElementById(\"" + item["kjbs"] + "_zy3\").innerText=\"" + item["zy4"] + ":" + item["zy4_4"] + "\";");
                //    str.Append("document.getElementById(\"" + item["kjbs"] + "_zy3\").disabled=\"\";");
                //    str.Append("document.getElementById(\"" + item["kjbs"] + "_zy3\").className=\"input1\";");
                //}
                //if (item["zy5"].ToString() != "" && item["zy5"].ToString() != null && item["xxdm"].ToString() != "")
                //{
                //    str.Append("document.getElementById(\"" + item["kjbs"] + "_zy4\").innerText=\"" + item["zy5"] + ":" + item["zy5_5"] + "\";");
                //    str.Append("document.getElementById(\"" + item["kjbs"] + "_zy4\").disabled=\"\";");
                //    str.Append("document.getElementById(\"" + item["kjbs"] + "_zy4\").className=\"input1\";");
                //}
                //if (item["zy6"].ToString() != "" && item["zy6"].ToString() != null && item["xxdm"].ToString() != "")
                //{
                //    str.Append("document.getElementById(\"" + item["kjbs"] + "_zy5\").innerText=\"" + item["zy6"] + ":" + item["zy6_6"] + "\";");
                //    str.Append("document.getElementById(\"" + item["kjbs"] + "_zy5\").disabled=\"\";");
                //    str.Append("document.getElementById(\"" + item["kjbs"] + "_zy5\").className=\"input1\";");
                //}
                //if (item["zy7"].ToString() != "" && item["zy7"].ToString() != null && item["xxdm"].ToString() != "")
                //{
                //    str.Append("document.getElementById(\"" + item["kjbs"] + "_zy6\").innerText=\"" + item["zy7"] + ":" + item["zy7_7"] + "\";");
                //    str.Append("document.getElementById(\"" + item["kjbs"] + "_zy6\").disabled=\"\";");
                //    str.Append("document.getElementById(\"" + item["kjbs"] + "_zy6\").className=\"input1\";");
                //}
                //str.Append("} catch (e) { }");

                //  str.Append("document.getElementById(\"" + item["kjbs"] + "_dm\").innerText=\"" + item["xxdm"] + "\";");
            }
            str.Append("document.getElementById(\"xpcIs\").value=\"" + xpcis + "\";"); //小批次ID集合用于判断填报状态时.是否有选择学校
            str.Append("</script>");

            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", str.ToString());
        }
        #endregion

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }

        protected void btn_quxiao_Click(object sender, EventArgs e)
        {

            Model_zk_kszyxx model = new Model_zk_kszyxx();
            model.Ksh =  BLL_Ks_Session.ksSession().ksh;
            model.Xxdm = hidxxdm.Value;
            model.Pcdm = hidpcdm.Value;
            model.Zysx = 1;
            string ip = BLL_Ks_Session.ksSession().Ipaddress;
            if (bll.delete_zk_kszyxx(model, ip))
            {

                bll.zk_ksxxglZyksqr(model.Ksh, 0);
              //  Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'取消志愿成功！' ,title:'操作提示'}; setTimeout(function(){ ymPrompt.close();},3000);); </script>");
             //   Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >alert('取消志愿成功！');</script>");
                Response.Redirect("Zytb_Help.aspx", false);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >alert('取消志愿失败！');</script>");
               
             //   Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'取消志愿失败！' ,title:'操作提示'});   </script>");
                return;
            }
        }
    }
}