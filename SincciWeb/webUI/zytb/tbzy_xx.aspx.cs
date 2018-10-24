using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Threading;
using System.Drawing;
using BLL;
using System.Text;
using System.Data;
using Model;
using System.Configuration;
using BLL.system;

namespace SincciKC
{
    public partial class WebForm2 : SPage //wf 2018-08-16 修改，单点登录统一控制 System.Web.UI.Page
    {
        private BLL_zk_kscj bllcj = new BLL_zk_kscj();
        /// <summary>
        /// 志愿定制BLL
        /// </summary>
        private BLL_zk_zydz bll = new BLL_zk_zydz();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["kaosheng"] != null)
                {
                    if (BLL_Ks_Session.ksLogCheck())
                    {
                   

                        string zyId = config.CheckChar2(Request.QueryString["zyId"].Trim().ToString());
                        string xxdm = "";
                        if (Request.QueryString["xxdm"] != null)
                            xxdm = config.CheckChar2(Request.QueryString["xxdm"].Trim().ToString());
                        string pcdm = config.CheckChar2(Request.QueryString["pcdm"].Trim().ToString());
                        string where = " 1=1 ";
                        ShowLoad(zyId,  xxdm, pcdm, where);
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

        private bool hege(string ksh, int type, string xqdm, Model_zk_ksSession model)
        {
           
                // #region    #endregion
                switch (type)
                {
                   
                    case 9: //第一批    2C以上 
                        #region
                        if (true)
                        {
                            string[] str = new string[4];
                            int b = 0;
                            for (int i = 0; i < 3; i++)
                            {
                                string name = "";
                                switch (i)
                                {
                                    case 2:
                                        name = model.Wkzh;
                                        break;
                                    case 0:
                                        name = model.Dsdj;
                                        break;
                                    case 1:
                                        name = model.Zhdj;
                                        break;
                                    default:
                                        break;
                                }
                                str[i] = name;
                                if (str[i] == "")
                                {
                                    return false;
                                }
                                else
                                {
                                    if (str[i].ToUpper() == "D")
                                    {
                                        b++;
                                    }
                                }
                            }
                            if (b > 0)
                            {
                                return false;
                            }
                        }

                        #endregion
                        break;
                    case 10: //第一批    2B以上 
                        #region
                        if (true)
                        {
                            string[] str = new string[4];
                            int b = 0;
                            for (int i = 0; i < 3; i++)
                            {
                                string name = "";
                                switch (i)
                                {
                                    case 2:
                                        name = model.Wkzh;
                                        break;
                                    case 0:
                                        name = model.Dsdj;
                                        break;
                                    case 1:
                                        name = model.Zhdj;
                                        break;
                                    default:
                                        break;
                                }
                                str[i] = name;
                                if (str[i] == "")
                                {
                                    return false;
                                }
                                else
                                {
                                    if (str[i].ToUpper() == "D" || str[i].ToUpper() == "C")
                                    {
                                        b++;
                                    }
                                }
                            }
                            if (b > 0) 
                            {
                                return false;
                            }
                        }
                        
                        #endregion
                        break;
                    default:
                        break;
                }
                return true;
        }
        /// <summary>
        /// 合格名单
        /// </summary>
        private BLL_zk_hege bllhege = new BLL_zk_hege();
        private BLL_zk_zdxx zdxx = new BLL_zk_zdxx();

        private void ShowLoad(string zyId, string xxdm, string pcdm, string where)
        {
            StringBuilder str = new StringBuilder();
            Model_zk_ksSession model = BLL_Ks_Session.ksSession();
            string ksh = model.ksh;
            string xqdm = model.Bmdxqdm;

            string two = "500";
            string name = ""; //显示名称
            DataTable dt = new DataTable();
            str.Append(" <table  width=\"100%\" border=\"1\"  style=\"border-collapse:collapse\" align=\"center\" cellpadding=\"4\" cellspacing=\"0\" bordercolor=\"#E7E4E0\" bgcolor=\"#FFFFFF\">");
            bool ispass = true; //是否合格
            int type = 1; //1代表要查询是否满计划学校//2往届生//3扶贫生
            #region 合格名单
            List<string> pclist = new List<string>() { "11", "21", "31", "41" };//第二批次
            List<string> pclist2 = new List<string>() { "51", "61", "71", "81", "91" };//第二批次
            string pc = "";
            if (pclist.Contains(pcdm))
                pc = "11";
            if (pclist2.Contains(pcdm))
                pc = "21";
            if (pcdm == "01")
            {
                pc = "01";
            }

            string kslbdm = model.Kslbdm;
            string bklb = model.Bklb.ToString();
            string jzfp = model.Jzfp.ToString();
            string mzdm = "";
            string fsx = model.Cj.ToString();
            string xj = model.Xjtype.ToString();
            if (model.Mzdm=="汉族")
            {
                mzdm = "0";
            }
            else
            {
                mzdm = "1";
            }
            //string f = ConfigurationManager.AppSettings["fsx"].ToString();
            //int cj1 = Convert.ToInt32(f.Split(',')[0]);
            //int cj2 = Convert.ToInt32(f.Split(',')[1]);
            //if (model.Cj >= cj1) //省级
            //{
            //    fsx = "2";
            //}
            //else if (model.Cj >= cj2 && model.Cj < cj1) //普通
            //{
            //    fsx = "1";
            //}
            //else
            //{
            //    fsx = "0";
            //}
            //if (xqdm == "504")
            //{
            //    if (bklb == "1")
            //    {
            //        if (model.Zhdj == "D")
            //        {
            //            model.Zhdj = "C";
            //        }
            //        if (model.Dsdj == "D")
            //        {
            //            model.Dsdj = "C";
            //        }
            //    }
            //    else if (bklb == "3")
            //    {
            //        if (model.Zhdj == "D")
            //        {
            //            model.Zhdj = "C";
            //        }
            //        if (model.Dsdj == "D")
            //        {
            //            model.Dsdj = "C";
            //        }
            //    }
            //    else if (bklb == "7")
            //    {
            //        if (model.Zhdj == "D")
            //        {
            //            model.Zhdj = "C";
            //        }
            //        if (model.Dsdj == "D")
            //        {
            //            model.Dsdj = "C";
            //        }
            //    }
            //}

            ispass = hege(ksh, 10, xqdm, model);//3B
            string dj = "0";
            if (ispass)
                dj = "1";
            else
            {
                ispass = hege(ksh, 9, xqdm, model); //3C
                if (ispass)
                {
                    dj = "2";
                }
                else
                {
                    dj = "0";
                }
            }
            //if (model.Cj < 400) //特殊003降分
            //{
            //    if (ispass)
            //    {
            //        where = where + " and xxdm  in ('003') ";
            //    }
            //    else
            //    {
            //        where = where + " and 1<>1  ";
            //    }

            //}
          
           
            #endregion

            if (xxdm == null || xxdm == "")
            {
                name = "学校名称";
                str.Append("<tr  bgcolor=\"#CEE7FF\"><td align=\"center\"><b>序号</b></td><td align=\"center\"><b>" + name + "</b></td><td align=\"center\"><b>选择</b></td></tr>");

                dt = bll.Select_zy_XX_syjh2(pc, xqdm, bklb, mzdm, kslbdm, jzfp, fsx, dj, xj);
               
                int i = 0;
                foreach (DataRow item in dt.Rows)
                {
                    i += 1;
                    str.Append("<tr><td align=\"center\">" + i + "</td> <td> " + item["xxdm"] + item["zsxxmc"]);
                    str.Append("</td> <td align=\"center\"> ");
                    str.Append(" <input type=\"button\" value=\"选择\" onclick='myfunction(\"" + item["xxdm"].ToString().Trim() + "\",\"" + item["zsxxmc"].ToString().Trim() + "\",\"" + zyId + "\")' />");
                    str.Append("</td></tr>");
                }
            }
            else
            {

                name = "专业名称";
                str.Append("<tr bgcolor=\"#CEE7FF\" ><td align=\"center\"><b>序号</b></td><td align=\"center\"><b>" + name + "</b></td><td align=\"center\"><b>选择</b></td></tr>");
                where = " 1=1";
                dt = bll.Select_zy_XXZY_zy(pcdm, two, xxdm, where);
                int i = 0;
                foreach (DataRow item in dt.Rows)
                {
                    i += 1;
                    str.Append("<tr><td align=\"center\">" + i + "</td><td> " + item["zydm"] + item["zymc"]);
                    str.Append("</td> <td align=\"center\"> ");
                    str.Append("<input  type=\"button\" value=\"选择\"  onclick='myfunction(\"" + item["zydm"].ToString().Trim() + "\",\"" + item["zymc"] + "\",\"" + zyId + "\")' />");
                    str.Append("</td></tr>");
                }
            }
            if (dt == null || dt.Rows.Count == 0)
            {
                if (!ispass)
                {
                    str.Append("<tr><td colspan=\"3\" align=\"center\">[只有成绩达标的考生才能填报]</td> </tr>");
                }
                else
                {
                    if (type == 2)
                    {
                        str.Append("<tr><td colspan=\"3\" align=\"center\">[您的报考类别无法填报该志愿]</td> </tr>");
                    }
                    else
                    {
                        str.Append("<tr><td colspan=\"3\" align=\"center\">[此项无数据可供填报]</td> </tr>");
                    }
                }
                str.Append("<tr><td colspan=\"3\"> <input id=\"txtlab\" type=\"text\"  runat=\"server\" value=\"" + zyId + "\" style=\" display:none\" /></td> </tr>");

            }
            else
            {
                str.Append("<tr><td></td><td>取消填报</td><td align=\"center\">  <input  type=\"button\" value=\"选择\"  onclick='myfunction(\"quxiao\",\"" + zyId + "\")'  /> </td></tr>");
            }
            //if (dt == null || dt.Rows.Count == 0)
            //{    
            //        str.Append("<tr><td colspan=\"3\" align=\"center\"> [此项无数据可供填报]</td> </tr>");
            //}

            //str.Append("<tr><td></td><td>取消填报</td><td align=\"center\">  <input  type=\"button\" value=\"选择\"  onclick='myfunction(\"quxiao\",\"" + zyId + "\")'  /> </td></tr>");
            str.Append(" </table>");
            xxSpan.InnerHtml = str.ToString();
        }









        ///// <summary>
        ///// 显示数据
        ///// </summary>
        //private void ShowLoad(string zyId,   string xxdm, string pcdm,string where )
        //{
        //    StringBuilder str = new StringBuilder();
        //    Model_zk_ksSession model = BLL_Ks_Session.ksSession();
        //    string ksh = model.ksh;
        //    string xqdm = model.Bmdxqdm;
           
        //    string two = "500";
        //    string name = ""; //显示名称
        //    DataTable dt = new DataTable();
        //    str.Append(" <table  width=\"100%\" border=\"1\"  style=\"border-collapse:collapse\" align=\"center\" cellpadding=\"4\" cellspacing=\"0\" bordercolor=\"#E7E4E0\" bgcolor=\"#FFFFFF\">");
        //    bool ispass = true; //是否合格
        //    int type = 1; //1代表要查询是否满计划学校//2往届生//3扶贫生
        //    #region 合格名单
        //    List<string> pclist = new List<string>() { "11", "21", "31", "41" };//第二批次
        //    List<string> pclist2 = new List<string>() { "51", "61", "71", "81", "91" };//第二批次
        //    if (pclist.Contains(pcdm))
        //    {
        //        if (xqdm == "501" || xqdm == "509") //外县是片区 
        //        {
        //            where = where + " and (xxdm  BETWEEN 001 AND 017 ) and xxdm not in ('003','004','015') and xqdm!='" + xqdm + "' ";
        //        }
        //        else
        //        {
        //            where = where + " and (xxdm  BETWEEN 001 AND 017 ) and xqdm!='" + xqdm + "' ";
        //        }
             
        //    }
        //    else
        //    {
        //        if (pcdm != "01")
        //        {
        //            if (xqdm == "501" || xqdm == "509") //外县是片区 
        //                where = where + " and (xxdm  BETWEEN 003 AND 037 ) and  (xxdm  in ('003','004','015') or xqdm='" + xqdm + "') ";
        //            else
        //                where = where + " and (xxdm  BETWEEN 003 AND 037 ) and xqdm='" + xqdm + "' ";
        //        }
              
        //    }

        //    if (model.Kslbdm == "2")
        //    {
        //        where = where + " and xxdm not in ('001','002') ";
        //    }
        //    if (model.Bklb != 1 && model.Bklb != 7 && model.Bklb != 3)
        //    {
        //        type = 2;
        //        where = where + " and 1<>1 ";
        //    }
        //    else if (model.Bklb == 7 || model.Bklb == 3)
        //    {
        //        where = where + " and (xxdm  BETWEEN 014 AND 037 )  ";
        //    }
        //    ispass = hege(ksh, 10, xqdm, model);//3B
        //    //if (model.Cj < 400) //特殊003降分
        //    //{
        //    //    if (ispass)
        //    //    {
        //    //        where = where + " and xxdm  in ('003') ";
        //    //    }
        //    //    else
        //    //    {
        //    //        where = where + " and 1<>1  ";
        //    //    }
               
        //    //}

        //    if (!ispass && pcdm != "01") //达不到3B 
        //    {
        //        where = where + " and (xxdm  BETWEEN 014 AND 037)  ";
        //    }
        //    ispass = hege(ksh, 9, xqdm, model); //3C
        //    if (!ispass) //达不到3C就表示所有学校不能填报
        //    {
        //        type = 2;
        //        where = where + " and 1<>1 ";
        //    }
        //    switch (pcdm)  //扶贫生
        //    {
        //        case "01":
        //            if (model.Jzfp == 1 && model.Mzdm != "汉族")
        //            {
        //                if (xqdm != "504") //只有沿河有015
        //                    where = where + " and xxdm in ('001','004','038','039','002')";
        //                else
        //                    where = where + " and xxdm in ('001','004','015','038','039','002')";
        //            }
        //            else if (model.Mzdm != "汉族"&&model.Jzfp == 0)
        //            {
        //                where = where + " and xxdm in ('039')";
        //            }
        //            else if (model.Mzdm == "汉族" && model.Jzfp == 1)
        //            {
        //                if (xqdm != "504") //只有沿河有015
        //                    where = where + " and xxdm in ('001','004','002','038')";
        //                else
        //                    where = where + " and xxdm in ('001','004','015','002','038')";
                       
        //            }
        //            else if (model.Jzfp == 0 && model.Mzdm == "汉族")
        //            {
        //                where = "1<>1";
        //            }
        //           type = 3;
        //            break;
        //        default:
                   
        //            break;
        //    }
        //    #endregion

        //    if (xxdm == null || xxdm == "")
        //    {
        //        name = "学校名称";
        //        str.Append("<tr  bgcolor=\"#CEE7FF\"><td align=\"center\"><b>序号</b></td><td align=\"center\"><b>" + name + "</b></td><td align=\"center\"><b>选择</b></td></tr>");
        //        if (type == 1)
        //        {
        //            dt = bll.Select_zy_XX_syjh(xqdm, where);
        //        }
        //        else if (type == 2)
        //        {

        //        }
        //        else
        //        {
        //            dt = bll.Select_zy_XX(pcdm, two, where);
        //        }      
        //        int i = 0;
        //        foreach (DataRow item in dt.Rows)
        //        {
        //            i += 1;
        //            str.Append("<tr><td align=\"center\">" + i + "</td> <td> " + item["xxdm"] + item["zsxxmc"]);
        //            str.Append("</td> <td align=\"center\"> ");
        //            str.Append(" <input type=\"button\" value=\"选择\" onclick='myfunction(\"" + item["xxdm"].ToString().Trim() + "\",\"" + item["zsxxmc"].ToString().Trim() + "\",\"" + zyId + "\")' />");
        //            str.Append("</td></tr>");
        //        }
        //    }
        //    else
        //    {

        //        name = "专业名称";
        //        str.Append("<tr bgcolor=\"#CEE7FF\" ><td align=\"center\"><b>序号</b></td><td align=\"center\"><b>" + name + "</b></td><td align=\"center\"><b>选择</b></td></tr>");
        //            where = " 1=1";
        //        dt = bll.Select_zy_XXZY_zy(pcdm,  two, xxdm, where);
        //        int i = 0;
        //        foreach (DataRow item in dt.Rows)
        //        {
        //            i += 1;
        //            str.Append("<tr><td align=\"center\">" + i + "</td><td> " + item["zydm"] + item["zymc"]);
        //            str.Append("</td> <td align=\"center\"> ");
        //            str.Append("<input  type=\"button\" value=\"选择\"  onclick='myfunction(\"" + item["zydm"].ToString().Trim() + "\",\"" + item["zymc"] + "\",\"" + zyId + "\")' />");
        //            str.Append("</td></tr>");
        //        }
        //    }
        //    if (dt == null || dt.Rows.Count == 0)
        //    {
        //        if (!ispass)
        //        {
        //            str.Append("<tr><td colspan=\"3\" align=\"center\">[只有成绩达标的考生才能填报]</td> </tr>");
        //        }
        //        else
        //        {
        //            if (type == 2)
        //            {
        //                str.Append("<tr><td colspan=\"3\" align=\"center\">[您的报考类别无法填报该志愿]</td> </tr>");
        //            }
        //            else
        //            {
        //                str.Append("<tr><td colspan=\"3\" align=\"center\">[此项无数据可供填报]</td> </tr>");
        //            }
        //        }
        //        str.Append("<tr><td colspan=\"3\"> <input id=\"txtlab\" type=\"text\"  runat=\"server\" value=\"" + zyId + "\" style=\" display:none\" /></td> </tr>");

        //    }
        //    else
        //    {
        //       str.Append("<tr><td></td><td>取消填报</td><td align=\"center\">  <input  type=\"button\" value=\"选择\"  onclick='myfunction(\"quxiao\",\"" + zyId + "\")'  /> </td></tr>");
        //    }
        //    //if (dt == null || dt.Rows.Count == 0)
        //    //{    
        //    //        str.Append("<tr><td colspan=\"3\" align=\"center\"> [此项无数据可供填报]</td> </tr>");
        //    //}
         
        //    //str.Append("<tr><td></td><td>取消填报</td><td align=\"center\">  <input  type=\"button\" value=\"选择\"  onclick='myfunction(\"quxiao\",\"" + zyId + "\")'  /> </td></tr>");
        //    str.Append(" </table>");
        //    xxSpan.InnerHtml = str.ToString();
        //}
    }
}