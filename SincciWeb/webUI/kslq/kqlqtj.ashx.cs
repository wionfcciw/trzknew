using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

using Newtonsoft.Json;
using System.Configuration;
namespace SincciKC.webUI.kslq
{
    /// <summary>
    /// kqlqtj 的摘要说明
    /// </summary>
    public class kqlqtj : IHttpHandler, IRequiresSessionState 
    {

        public void ProcessRequest(HttpContext context)
        {
          
            context.Response.Charset = "gb2312";
            if (null != context.Request["action"])
            {
                string action = context.Request["action"].ToString();
                switch (action)
                {
                    case "selectLoad":
                        SelectVillage(context);
                        break;
                }
            }
        }
        private BLL_zk_zydz bll = new BLL_zk_zydz();
   
        public void SelectVillage(HttpContext context)
        {
            Tjclass info = new Tjclass();
            if (BLL_Ks_Session.ksLogCheck())
            {

                Model_zk_ksSession model = BLL_Ks_Session.ksSession();
                string Zkzh = model.Zkzh;
                string bmddm = model.Bmddm;
                string xqdm = model.Bmdxqdm;
                string ksh = model.ksh;
                string str = config.CheckChar(context.Request.QueryString["zkzh"].ToString()).Trim();
                string xxdm =  config.CheckChar(context.Request.QueryString["xxdm"].ToString());
                string pcdm = config.CheckChar(context.Request.QueryString["pcdm"].ToString());
                if (Zkzh == str)
                {
                    DataTable dt2 = bll.Select_lqjg(ksh);//是否被录取
                    if (dt2.Rows.Count > 0)
                    {
                        info.Dt = dt2;
                        info.Status = "success";
                        info.Type = 1;
                        context.Response.Write(JsonConvert.SerializeObject(info, Formatting.Indented));
                    }
                    else
                    {
                        if (pcdm == "0")
                        {
                            DataTable dt = bll.SelectKshPm_xx(ksh, pcdm, xxdm, bmddm, xqdm); //排名
                            info.Dt = dt;
                            info.Status = "success";
                            info.Type = 2;
                            info.Cj = model.Cj;
                            context.Response.Write(JsonConvert.SerializeObject(info, Formatting.Indented));
                        }
                        else
                        {
                            DataTable tab = bll.Select_syjh(xxdm, model.Bmdxqdm); //是否满计划
                            if (tab.Rows.Count > 0)
                            {
                                int jhs = Convert.ToInt32(tab.Rows[0]["jhs"].ToString());
                                if (jhs <= 0)
                                {
                                    info.Status = "man";
                                    info.Msg = "您所填报的招生学校计划已满，请选择其它学校填报！";
                                    context.Response.Write(JsonConvert.SerializeObject(info, Formatting.Indented));
                                    //lblshow.Text = "（您所填报的招生学校计划已满，请选择其它学校填报！）<br/>";
                                }
                                else
                                {
                                    int refpzt = 0;//是否配转统
                                    DataTable dt = bll.SelectKshPm_xx_2(ksh, pcdm, xxdm, bmddm, xqdm, ref refpzt); //排名
                                    if (refpzt != 0)
                                    {
                                        string pesfsx = ConfigurationManager.AppSettings["pesfsx"];
                                        info.Msg = pesfsx;
                                        info.Pzt = xxdm + "*" + xqdm;
                                      
                                        //if (pesfsx != "")
                                        //{
                                        //    string[] array = pesfsx.Split(new char[1] { ';' });
                                        //    for (int i = 0; i < array.Length; i++)
                                        //    {
                                        //        string s1 = xxdm + "*" + xqdm;
                                        //        if (array[i].Split(new char[1] { '=' })[0] == s1)
                                        //        {
                                        //            info.Msg = array[i].Split(new char[1] { '=' })[1];
                                        //        }
                                        //    }
                                        //}
                                    }
                                    info.Cj = model.Cj;
                                    info.Dt = dt;
                                    info.Status = "success";
                                    info.Type = 2;
                                    context.Response.Write(JsonConvert.SerializeObject(info, Formatting.Indented));
                                }
                            }
                        }
                    }
                }
                else
                {
                    info.Status = "error";
                    info.Msg = "准考证号有误！";
                    context.Response.Write(JsonConvert.SerializeObject(info, Formatting.Indented));
                }
            }
            else
            {
           
                info.Status = "error";
                info.Msg = "登录超时，请重新登录！";
                context.Response.Write(JsonConvert.SerializeObject(info, Formatting.Indented));
            }
        
        }
        private string DataTable2Json(System.Data.DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            if (dt != null)
            {

                if (dt.Rows.Count > 0)
                {
                    jsonBuilder.Append("[");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        jsonBuilder.Append("{");
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            jsonBuilder.Append("\"");
                            jsonBuilder.Append(dt.Columns[j].ColumnName);
                            jsonBuilder.Append("\":\"");
                            jsonBuilder.Append(dt.Rows[i][j].ToString());
                            jsonBuilder.Append("\",");
                        }
                        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                        jsonBuilder.Append("},");
                    }
                    jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                    jsonBuilder.Append("]");
                }

            }
            return jsonBuilder.ToString();
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}