using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DAL;
using Model;
using System.Configuration;

namespace BLL
{

    /// <summary>
    /// 考生登录控制类。
    /// </summary>
    public class BLL_Ks_Login
    {
        private string dltype = ConfigurationManager.AppSettings["dltype"];
        private BLL_zk_kscj bllcj = new BLL_zk_kscj();
        private BLL_zk_ksxxgl bllxx = new BLL_zk_ksxxgl();
        /// <summary>
        /// 考生登录
        /// </summary>
        /// <param name="ksh">报名号</param>
        /// <param name="xm">姓名</param>
        /// <param name="pwd">密码</param>        
        /// <returns>-2 密码有误 -1 姓名有误  0 找不到报名号  1登录成功</returns>
        public int KS_Login(string ksh, string xm, string pwd)
        {
            int flag = 1;
            Model_zk_ksSession ksSession = new Model_zk_ksSession();
            Model_zk_ksxxgl ksinfo = new Model_zk_ksxxgl();
            DataTable dd = bllxx.ViewDisp_Login(ksh, dltype);
            if (dd.Rows.Count > 0)
            {
                ksinfo = new SqlDbHelper_1().DT2EntityList<Model_zk_ksxxgl>(dd)[0];
                if (ksinfo.Xm == "")
                {
                    return 3;
                }
            }
            else
            {
                return 0;
            }
            //if (dltype == "0")
            //{
            //    ksinfo = bllxx.ViewDisp(ksh);
            //}
            //else
            //    ksinfo = bllxx.ViewDisp_zkzh(ksh);

            if (ksinfo.Ksh.Length > 0)
            {

                //if (xm != ksinfo.Xm)
                //{
                //    flag = -1;
                //}
                //else 
                if (pwd != ksinfo.Pwd)
                {

                    flag = -2;

                }
            }
            else
            {
                flag = 0;
            }
            if (flag == 1)
            {
                if (ksinfo.Sfzh.Length == 18)
                {
                    if (ksinfo.Sfzh.Substring(ksinfo.Sfzh.Length - 6) == pwd)
                    {
                        flag = 2;
                        ksSession.Flag = false;
                    }
                    else
                    {
                        ksSession.Flag = true;
                    }
                }
                else
                {
                    if ("123456" == pwd)
                    {
                        flag = 2;
                        ksSession.Flag = false;
                    }
                    else
                    {
                        ksSession.Flag = true;
                    }
                }
                ksSession.ksh = ksinfo.Ksh;
                ksSession.xm = ksinfo.Xm;
                ksSession.kaoci = ksinfo.Kaoci + "年";
                ksSession.Bmdxqdm = ksinfo.Bmdxqdm;
                ksSession.Bmddm = ksinfo.Bmddm;
                ksSession.Pwd = pwd;
                ksSession.Kslbdm = ksinfo.Kslbdm;
                ksSession.Bklb = ksinfo.Bklb;
                ksSession.Jzfp = ksinfo.Jzfp;
                ksSession.Mzdm = ksinfo.Mzdm;
                ksSession.Xjtype = ksinfo.Xjtype;
                DataTable dt = bllcj.zk_cj(ksinfo.Ksh);
                if (dt.Rows.Count > 0)
                {
                    ksSession.Wkzh = dt.Rows[0]["wkzh"].ToString();
                    ksSession.Dsdj = dt.Rows[0]["dsdj"].ToString();
                    ksSession.Zhdj = dt.Rows[0]["zhdj"].ToString();
                    ksSession.Cj = Convert.ToInt32(dt.Rows[0]["zzf"]);
                }
                ksSession.Ipaddress = config.GetUserIP();
                
                    ksSession.Zkzh = ksinfo.Zkzh;
               

                System.Web.HttpContext.Current.Session.Add("kaosheng", ksSession);

            }

            return flag;
        }
    }
}
