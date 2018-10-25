using System;
using System.Collections.Generic;
using System.Web; 
using System.Text;
using Model;
using DAL;
namespace BLL 
{
    /// <summary>
    /// 考生Session控制类。
    /// </summary>
    public class BLL_Ks_Session
    {
        #region "考生Session"
        /// <summary>
        /// 判断考生Session是否存在，返回Session实体类
        /// </summary> 
        public static Model_zk_ksSession ksSession()
        {
            Model_zk_ksSession ks = new Model_zk_ksSession();
            ks = System.Web.HttpContext.Current.Session["kaosheng"] as Model_zk_ksSession;
            return ks;
        }
        /// <summary>
        /// 判断考生Session是否存在，返回Session实体类
        /// </summary> 
        public static Model_zk_ksSession ksSessionZY()
        {
            Model_zk_ksSession ks = new Model_zk_ksSession();
            ks = System.Web.HttpContext.Current.Session["kszy"] as Model_zk_ksSession;
            return ks;
        }
        /// <summary>
        /// 考生退出系统
        /// </summary> 
        public static void Logout_Member()
        {
            System.Web.HttpContext.Current.Session["kaosheng"] = null;
            //wf 2018-08-15 修改为单点登录方式，故注释掉
            //System.Web.HttpContext.Current.Response.Write("<script>window.location.href='/';</script>");
            LogoutSSO();
        }
        /// <summary>
        /// 检查是否有登录
        /// </summary>
        /// <returns></returns>
        public static bool ksLogCheck()
        {
            bool flag = false;
            if (System.Web.HttpContext.Current.Session["kaosheng"] != null)
            {
                if (ksSession().Flag)
                {
                    flag = true;
                }
            }
            else
            { 
                System.Web.HttpContext.Current.Response.Write("<script>alert('超时请重新登录！'); window.location.href='/' </script>");

            }
            return flag;
        }

        /// <summary>
        /// 新增单点登陆退出方法 wf 2018-8-15
        /// </summary>
        private static void LogoutSSO()
        {
            SqlDbHelper_1 helper = new SqlDbHelper_1();
            string error = "";
            bool bReturn = false;
            string logoutsql = "select redirect_website from Sys_SSOAddress where redirect_code='logout'";
            string address = helper.ExecuteScalar(logoutsql, ref error, ref bReturn).ToString();
            System.Web.HttpContext.Current.Response.Write("<script>window.location='http://test.openapi.cslearning.cn/authApi/auth/logout?redirect_uri=" + address + "';</script>");
        }
        #endregion
    }
}
