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
using BLL.system;

namespace SincciKC.webUI.zzsb
{
    public partial class zzsb_xx : SPage //wf 2018-08-16 修改，单点登录统一控制 System.Web.UI.Page
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

                        string ksh = BLL_Ks_Session.ksSession().ksh;
                        string xqdm = ksh.Substring(2, 4);

                        string zyId = config.CheckChar2(Request.QueryString["zyId"].Trim().ToString());
                        string xxdm = "";
                        if (Request.QueryString["xxdm"] != null)
                            xxdm = config.CheckChar2(Request.QueryString["xxdm"].Trim().ToString());
                        string pcdm = config.CheckChar2(Request.QueryString["pcdm"].Trim().ToString());
                        string where = Selwhere(pcdm, zyId, xqdm, ksh);
                        ShowLoad(zyId, xqdm, xxdm, pcdm, where);
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
        private string Selwhere(string pcdm, string zyId, string xqdm, string ksh)
        {
            string where = "";

            switch (pcdm.Substring(0, 1))
            {
                case "0":
                    if (pcdm == "02" || pcdm == "03")
                    {
                        if (!hege(ksh, 1))
                        {
                            where = " 1<>1 ";
                        }  
                    }
                    break;
                case "1":
                    if (xqdm == "0681") //启东
                    {
                        if (!hege(ksh, 3))
                        {
                            where = " 1<>1 ";
                        }
                        else
                        {
                            if (pcdm == "12")
                            {
                                switch (zyId.Substring(zyId.Length - 1))
                                {
                                    case "1":
                                        where = " xxdm=8101 ";
                                        break;
                                    case "2":
                                        where = " xxdm=8102 ";
                                        break;
                                    case "3":
                                        where = " xxdm=8103 ";
                                        break;
                                    default:
                                        break;
                                }
                            }
                            
                        }
                    }else if (xqdm == "0682"){//如皋
                         
                        switch (pcdm)
                        {
                            case "12":
                                if (!hege(ksh, 4))
                                {
                                    where = " 1<>1 ";
                                }  
                                break;
                            case "13":
                                if (!hege(ksh, 5))
                                {
                                    where = " 1<>1 ";
                                }
                                break;
                            case "14":
                                if (!hege(ksh, 6))
                                {
                                    where = " 1<>1 ";
                                }
                                break;
                            case "15":
                            case "16":
                                if (!hege(ksh, 7))
                                {
                                    where = " 1<>1 ";
                                }
                                break;
                            case "17":
                                if (!hege(ksh, 8))
                                {
                                    where = " 1<>1 ";
                                }
                                break;
                            default:
                                break;
                        }



                    }
                    else if (xqdm == "0684") //海门
                    {
                        if (!hege(ksh, 9))
                        {
                            where = " 1<>1 ";
                        }
                        else
                        {
                            if (pcdm == "12")
                            {
                                switch (zyId.Substring(zyId.Length - 1))
                                {
                                    case "1":
                                        where = " xxdm=8406 ";
                                        break;
                                    case "2":
                                        where = " xxdm!=8406 ";
                                        break;

                                    default:
                                        break;
                                }
                            }
                           
                        }
                        
                    }
                    else
                    {
                        if (!hege(ksh, 2))
                        {
                            where = " 1<>1 ";
                        }
                    }
                    
                    break;
                default:
                    where = " 1=1 ";
                    break;
            }
            if (where == "")
            {
                where = " 1=1 ";
            }

            return where;
        }
        private bool hege(string ksh,int type)
        {
            DataTable dt = bllcj.zk_cj(ksh);
            if (dt.Rows.Count > 0)
            {
                // #region    #endregion
                switch (type)
                {
                  
                    case 1: //提前 第二,三小批  综评合格+ 2A2B以上
                        #region "大市"
                        if (dt.Rows[0]["zp1"].ToString() == "合格" && dt.Rows[0]["zp2"].ToString() == "合格")
                        {
                            string[] str = new string[4];
                            int b = 0;
                            for (int i = 0; i < str.Length; i++)
                            {
                                str[i] = dt.Rows[0]["zp" + (i + 3)].ToString().Trim();
                                if (str[i] == "")
                                {
                                    return false;
                                }
                                else
                                {
                                    if (str[i].ToUpper() == "C" || str[i].ToUpper() == "D")
                                    {
                                        return false;
                                    }
                                    if (str[i].ToUpper() == "B")
                                    {
                                        b++;
                                    }
                                }
                            }
                            if (b > 2) //超过2B就不合格
                            {
                                  return false;
                            }
                            //string a = dt.Rows[0]["zp3"].ToString().Trim();
                            //string b = dt.Rows[0]["zp4"].ToString().Trim();
                            //string c = dt.Rows[0]["zp5"].ToString().Trim();
                            //string d = dt.Rows[0]["zp6"].ToString().Trim();
                           
                        }
                        else
                        {
                            return false;
                        }
                        #endregion
                        break;
                    
                    case 2: //第一批  综评合格+ 2B2C以上
                        #region "大市"
                        if (dt.Rows[0]["zp1"].ToString() == "合格" && dt.Rows[0]["zp2"].ToString() == "合格")
                        {
                            string[] str = new string[4];
                            int b = 0;
                            for (int i = 0; i < str.Length; i++)
                            {
                                str[i] = dt.Rows[0]["zp" + (i + 3)].ToString().Trim();
                                if (str[i] == "")
                                {
                                    return false;
                                }
                                else
                                {
                                    if (str[i].ToUpper() == "D")
                                    {
                                        return false;
                                    }
                                    if (str[i].ToUpper() == "C")
                                    {
                                        b++;
                                    }
                                }
                            }
                            if (b > 2) //超过2C就不合格
                            {
                                return false;
                            }
                         

                        }
                        else
                        {
                            return false;
                        }
   #endregion    
                        break;
                    case 3: //第一批  综评合格+ 2B2C以上 ,地理生物2C以上//启东
                        #region
                        if (dt.Rows[0]["dl7"].ToString().Trim() == "" || dt.Rows[0]["sw8"].ToString().Trim() == "")
                        {
                            return false;
                        }
                        if (dt.Rows[0]["dl7"].ToString().Trim().ToUpper() == "D" || dt.Rows[0]["sw8"].ToString().Trim().ToUpper() == "D")
                        {
                            return false;
                        }
                        if (dt.Rows[0]["zp1"].ToString() == "合格" && dt.Rows[0]["zp2"].ToString() == "合格")
                        {
                            string[] str = new string[4];
                            int b = 0;
                            for (int i = 0; i < str.Length; i++)
                            {
                                str[i] = dt.Rows[0]["zp" + (i + 3)].ToString().Trim();
                                if (str[i] == "")
                                {
                                    return false;
                                }
                                else
                                {
                                    if (str[i].ToUpper() == "D")
                                    {
                                        return false;
                                    }
                                    if (str[i].ToUpper() == "C")
                                    {
                                        b++;
                                    }
                                }
                            }
                            if (b > 2) //超过2C就不合格
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                        #endregion
                        break;
                    case 4: //第一批 第一小批A 综评合格+ 1B3B以上 ,地理生物2B以上// 如皋
                        #region
                        if (dt.Rows[0]["dl7"].ToString().Trim() == "" || dt.Rows[0]["sw8"].ToString().Trim() == "")
                        {
                            return false;
                        }
                        if (dt.Rows[0]["dl7"].ToString().Trim().ToUpper() == "D" || dt.Rows[0]["sw8"].ToString().Trim().ToUpper() == "D" || dt.Rows[0]["dl7"].ToString().Trim().ToUpper() == "C" || dt.Rows[0]["sw8"].ToString().Trim().ToUpper() == "C")
                        {
                            return false;
                        }
                        if (dt.Rows[0]["zp1"].ToString() == "合格" && dt.Rows[0]["zp2"].ToString() == "合格")
                        {
                            string[] str = new string[4];
                            int b = 0;
                            for (int i = 0; i < str.Length; i++)
                            {
                                str[i] = dt.Rows[0]["zp" + (i + 3)].ToString().Trim();
                                if (str[i] == "")
                                {
                                    return false;
                                }
                                else
                                {
                                    if (str[i].ToUpper() == "D" || str[i].ToUpper() == "C")
                                    {
                                        return false;
                                    }
                                    if (str[i].ToUpper() == "B")
                                    {
                                        b++;
                                    }
                                }
                            }
                            if (b > 3) //超过3B就不合格
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                        #endregion
                        break;
                    case 5: //第一批 第一小批B 综评合格+ 4B以上 ,地理生物2C以上// 如皋
                        #region
                        if (dt.Rows[0]["dl7"].ToString().Trim() == "" || dt.Rows[0]["sw8"].ToString().Trim() == "")
                        {
                            return false;
                        }
                        if (dt.Rows[0]["dl7"].ToString().Trim().ToUpper() == "D" || dt.Rows[0]["sw8"].ToString().Trim().ToUpper() == "D" )
                        {
                            return false;
                        }
                        if (dt.Rows[0]["zp1"].ToString() == "合格" && dt.Rows[0]["zp2"].ToString() == "合格")
                        {
                            string[] str = new string[4];
                            int b = 0;
                            for (int i = 0; i < str.Length; i++)
                            {
                                str[i] = dt.Rows[0]["zp" + (i + 3)].ToString().Trim();
                                if (str[i] == "")
                                {
                                    return false;
                                }
                                else
                                {
                                    if (str[i].ToUpper() == "D" || str[i].ToUpper() == "C")
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            return false;
                        }
                        #endregion
                        break;
                    case 6: //第一批 第二小批A 综评合格+ 4B以上 ,地理生物2B以上// 如皋
                        #region
                        if (dt.Rows[0]["dl7"].ToString().Trim() == "" || dt.Rows[0]["sw8"].ToString().Trim() == "")
                        {
                            return false;
                        }
                        if (dt.Rows[0]["dl7"].ToString().Trim().ToUpper() == "D" || dt.Rows[0]["sw8"].ToString().Trim().ToUpper() == "D" || dt.Rows[0]["dl7"].ToString().Trim().ToUpper() == "C" || dt.Rows[0]["sw8"].ToString().Trim().ToUpper() == "C")
                        {
                            return false;
                        }
                        if (dt.Rows[0]["zp1"].ToString() == "合格" && dt.Rows[0]["zp2"].ToString() == "合格")
                        {
                            string[] str = new string[4];
                            int b = 0;
                            for (int i = 0; i < str.Length; i++)
                            {
                                str[i] = dt.Rows[0]["zp" + (i + 3)].ToString().Trim();
                                if (str[i] == "")
                                {
                                    return false;
                                }
                                else
                                {
                                    if (str[i].ToUpper() == "D" || str[i].ToUpper() == "C")
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            return false;
                        }
                        #endregion
                        break;
                    case 7: //第一批 第二小批B,第三小批A 综评合格+ 3B1C以上 ,地理生物2C以上// 如皋 
                        #region
                        if (dt.Rows[0]["dl7"].ToString().Trim() == "" || dt.Rows[0]["sw8"].ToString().Trim() == "")
                        {
                            return false;
                        }
                        if (dt.Rows[0]["dl7"].ToString().Trim().ToUpper() == "D" || dt.Rows[0]["sw8"].ToString().Trim().ToUpper() == "D" )
                        {
                            return false;
                        }
                        if (dt.Rows[0]["zp1"].ToString() == "合格" && dt.Rows[0]["zp2"].ToString() == "合格")
                        {
                            string[] str = new string[4];
                            int b = 0;
                            for (int i = 0; i < str.Length; i++)
                            {
                                str[i] = dt.Rows[0]["zp" + (i + 3)].ToString().Trim();
                                if (str[i] == "")
                                {
                                    return false;
                                }
                                else
                                {
                                    if (str[i].ToUpper() == "D")
                                    {
                                        return false;
                                    }
                                    if (str[i].ToUpper() == "C")
                                    {
                                        b++;
                                    }
                                }
                            }
                            if (b > 1) //超过1C就不合格
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                        #endregion
                        break;
                    case 8: //第一批 第三小批B 综评合格+ 2B2C以上 ,地理生物2C以上// 如皋 
                        #region
                        if (dt.Rows[0]["dl7"].ToString().Trim() == "" || dt.Rows[0]["sw8"].ToString().Trim() == "")
                        {
                            return false;
                        }
                        if (dt.Rows[0]["dl7"].ToString().Trim().ToUpper() == "D" || dt.Rows[0]["sw8"].ToString().Trim().ToUpper() == "D")
                        {
                            return false;
                        }
                        if (dt.Rows[0]["zp1"].ToString() == "合格" && dt.Rows[0]["zp2"].ToString() == "合格")
                        {
                            string[] str = new string[4];
                            int b = 0;
                            for (int i = 0; i < str.Length; i++)
                            {
                                str[i] = dt.Rows[0]["zp" + (i + 3)].ToString().Trim();
                                if (str[i] == "")
                                {
                                    return false;
                                }
                                else
                                {
                                    if (str[i].ToUpper() == "D")
                                    {
                                        return false;
                                    }
                                    if (str[i].ToUpper() == "C")
                                    {
                                        b++;
                                    }
                                }
                            }
                            if (b > 2) //超过2C就不合格
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                        #endregion
                        break;

                    case 9: //第一批  综评合格+ 2B2C以上 地理生物2C以上// 海门
                        #region 

                         if (dt.Rows[0]["dl7"].ToString().Trim() == "" || dt.Rows[0]["sw8"].ToString().Trim() == "")
                        {
                            return false;
                        }
                        if (dt.Rows[0]["dl7"].ToString().Trim().ToUpper() == "D" || dt.Rows[0]["sw8"].ToString().Trim().ToUpper() == "D")
                        {
                            return false;
                        }
                        if (dt.Rows[0]["zp1"].ToString() == "合格" && dt.Rows[0]["zp2"].ToString() == "合格")
                        {
                            string[] str = new string[4];
                            int b = 0;
                            for (int i = 0; i < str.Length; i++)
                            {
                                str[i] = dt.Rows[0]["zp" + (i + 3)].ToString().Trim();
                                if (str[i] == "")
                                {
                                    return false;
                                }
                                else
                                {
                                    if (str[i].ToUpper() == "D")
                                    {
                                        return false;
                                    }
                                    if (str[i].ToUpper() == "C")
                                    {
                                        b++;
                                    }
                                }
                            }
                            if (b > 2) //超过2C就不合格
                            {
                                return false;
                            }


                        }
                        else
                        {
                            return false;
                        }
                        #endregion
                        break;
                    default:
                        break;
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// 合格名单
        /// </summary>
        private BLL_zk_hege bllhege = new BLL_zk_hege();
        /// <summary>
        /// 显示数据
        /// </summary>
        private void ShowLoad(string zyId, string xqdm, string xxdm, string pcdm,string where )
        {
            StringBuilder str = new StringBuilder();
            string ksh = BLL_Ks_Session.ksSession().ksh;
            string one = xqdm;
            string two = xqdm.Substring(0, 2) + "00";
            one = "'" + one + "'";  
            two = "'" + two + "'";
            string name = ""; //显示名称
          
            
            DataTable dt = new DataTable();
            str.Append(" <table  width=\"100%\" border=\"1\"  style=\"border-collapse:collapse\" align=\"center\" cellpadding=\"4\" cellspacing=\"0\" bordercolor=\"#E7E4E0\" bgcolor=\"#FFFFFF\">");
           
            if (pcdm == "01" || pcdm == "02" || pcdm == "03") //要按户籍县区查计划
            {
                Model_zk_ksxxgl model = new BLL_zk_ksxxgl().ViewDisp(ksh);
                one = model.Hjdqdm;
                if (pcdm == "01")
                {
                    if (model.Kslbdm.Trim() != "1" || model.Xbdm == 2)
                    {
                        where = " 1<>1 ";
                    }
                }
            }
            if (xxdm == null || xxdm == "")
            {
                name = "学校名称";
                str.Append("<tr  bgcolor=\"#CEE7FF\"><td align=\"center\"><b>序号</b></td><td align=\"center\"><b>" + name + "</b></td><td align=\"center\"><b>选择</b></td></tr>");

                dt = bll.Select_zy_XX_zzsb(pcdm, one + "," + two, where);
                
                
                int i = 0;
                foreach (DataRow item in dt.Rows)
                {
                    i += 1;
                    str.Append("<tr><td align=\"center\">" + i + "</td> <td> " + item["xxdm"] + item["zsxxmc"] + "[剩余计划" + item["syjh"] + "个]");
                    str.Append("</td> <td align=\"center\"> ");
                    str.Append(" <input type=\"button\" value=\"选择\" onclick='myfunction(\"" + item["xxdm"].ToString().Trim() + "\",\"" + item["zsxxmc"].ToString().Trim() + "\",\"" + zyId + "\")' />");
                    str.Append("</td></tr>");
                }

            }
            else
            {

                name = "专业名称";
                str.Append("<tr bgcolor=\"#CEE7FF\" ><td align=\"center\"><b>序号</b></td><td align=\"center\"><b>" + name + "</b></td><td align=\"center\"><b>选择</b></td></tr>");

                dt = bll.Select_zy_XXZY(pcdm, one + "," + two, xxdm, where);
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
                    str.Append("<tr><td colspan=\"3\" align=\"center\"> [此项无数据可供填报]</td> </tr>");
            }
         
            str.Append("<tr><td></td><td>取消填报</td><td align=\"center\">  <input  type=\"button\" value=\"选择\"  onclick='myfunction(\"quxiao\",\"" + zyId + "\")'  /> </td></tr>");
            str.Append(" </table>");
            xxSpan.InnerHtml = str.ToString();
        }
    }
}