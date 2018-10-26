using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BLL;
using Model;
using System.Data;
using DAL;


namespace BLL
{
    /// <summary>
    /// 登录类
    /// </summary>
    public class SincciLogin
    {
        #region "登录验证"
        /// <summary>
        /// 登录验证 0:找不到用户 -3:密码出错 -2用户锁住 -1:用户关闭 1:登录成功
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>0:找不到用户 -1:用户关闭 -2用户锁住 -3:密码出错 1:登录成功  </returns>
        public int Confirm(int UserType, string UserName, string password)
        { 
                return ConfirmUser(UserName, password, UserType);
        }
        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="UserType">用户类型</param>
        /// <returns>0:找不到用户 -1:用户关闭 -2用户锁住 -3:密码出错 1:登录成功  </returns>
        private int ConfirmUser(string UserName, string password, int UserType)
        { 

            int flag = 0;
            string Meg = "";
            QueryParam qp = new QueryParam();
            qp.TableName = "Sys_users";
            qp.ReturnFields = "*";
            qp.Where = string.Format(" U_loginname='{0}' ", config.CheckChar(UserName), UserType);
            qp.OrderId = "Userid"; 

            int iInt = 0;
            DataSet ds2 = new DBcon().Sys_UserList(qp, out iInt);//.UserList(qp, out iInt);

            if (iInt > 0)
            {
                //转换为实体类
                sys_UserTable UserTable = new Method().DT2EntityList<sys_UserTable>(ds2.Tables[0])[0];

                if (UserTable.U_LoginName != null)
                {

                    if (UserTable.U_tag == -1)  //用户关闭
                    {
                        Meg = "（" + UserName + "）被关闭，禁止登录！";
                        //插入操作日志
                        EventMessage.EventWriteDB(2, Meg, UserTable.U_LoginName);
                        flag = -1;
                    }
                    else if (pwderr(UserTable.U_errnumber, UserTable.U_datetime))  //判断用户输入密码出错次数
                    {
                        Meg = "（" + UserName + "/" + password + "）用户密码出错多次！";
                        //插入操作日志
                        EventMessage.EventWriteDB(2, Meg, UserTable.U_LoginName);
                        flag = -2;
                    }
                    else if (UserTable.U_Password == password) //md5 加密后对比
                    {
                        flag = 1;
                        Meg = "欢迎您" + UserName + "，成功登入。您的IP为：" + config.GetUserIP() + "！";
                        //插入操作日志
                        EventMessage.EventWriteDB(2, Meg, UserTable.U_LoginName);

                        //登录成功记录ip和时间
                        new Method().Update_Table_Fileds("Sys_users",
                            string.Format(" U_errnumber=0,U_tag=1,U_datetime='{0}',U_ip='{1}'",DateTime.Now, config.GetUserIP()),
                            string.Format(" u_loginname='{0}'", UserTable.U_LoginName));


                        //检查是否在线
                       checkOnline(UserTable.U_LoginName.ToLower(), UserTable.U_Password, config.GetSessionID);
                        //登录成功进入系统
                        //Signin(UserTable.U_LoginName.ToString());
                        // lst

                        Sys_SessionEntity stu = new Sys_SessionEntity();
                        stu.UserName = UserTable.U_LoginName;
                        stu.UserType = UserTable.U_usertype;
                        stu.Name = UserTable.U_xm;
                        stu.UserType = UserTable.U_usertype;
                        stu.U_department = UserTable.U_department;
                        stu.Flag = true;
                        HttpContext.Current.Session.Add("stu", stu);


                    }
                    else
                    {
                        //记录密码出错次数
                        new Method().Update_Table_Fileds("Sys_users",
                            string.Format("U_errnumber=U_errnumber+1,U_datetime='{0}',U_ip='{1}'", DateTime.Now, config.GetUserIP()),
                            string.Format("u_loginname='{0}'", UserTable.U_LoginName));

                        if (UserTable.U_errnumber + 1 == config.pwderr())
                        {
                            //锁定用户
                            new Method().Update_Table_Fileds("Sys_users", " U_tag=0 ", string.Format("u_loginname='{0}'", UserTable.U_LoginName));
                        }

                        Meg = "用户名/密码(" + UserName + "/" + password + ")密码错误！";
                        //插入操作日志
                        EventMessage.EventWriteDB(2, Meg, UserTable.U_LoginName);
                        flag = -3;

                    }

                }

            }

            return flag;
        }

        #region "考场登录"
        /// <summary>
        /// 考场登录
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="password">密码</param>       
        /// <returns>0:找不到用户 -1:用户关闭 -2用户锁住 -3:密码出错 1:登录成功  </returns>
        private int ConfirmKC(string UserName, string password)
        {
            int flag = -2;
            //string Meg = "";

            ////QueryParam qp = new QueryParam();
            ////qp.TableName = "T_ExamRoom";
            ////qp.ReturnFields = "*";
            ////qp.Where = string.Format(" E_Code='{0}' ", config.CheckChar(UserName));
            ////qp.OrderId = "ExamRoomid";

            ////int iInt = 0;
            ////ArrayList lst = new Method().T_ExamRoomList(qp, out iInt); //.UserList(qp, out iInt);

            ////if (iInt > 0)
            ////{
            ////    //转换为实体类
            ////    T_ExamRoomTable UserTable = (T_ExamRoomTable)lst[0];

            ////    if (UserTable.E_Code != null)
            ////    {
            ////          if (UserTable.E_Password == password) //md5 加密后对比
            ////        {
            ////            if (UserTable.E_tag ==0 )  //用户关闭
            ////            {
            ////                Meg = "（" + UserName + "）被关闭，禁止登录！";
            ////                //插入操作日志
            ////                new EventMessage().EventWriteDB(2, Meg);
            ////                flag = -1;
            ////            }
            ////            else
            ////            {
            ////                flag = 1;
            ////                Meg = " 欢迎您" + UserName + "，成功登入。您的IP为：" + config.GetUserIP() + "！";
            ////                //插入操作日志
            ////                new EventMessage().EventWriteDB(2, Meg);

            ////                //登录成功记录ip和时间
            ////                new Method().Update_Table_Fileds("T_ExamRoom",
            ////                    string.Format(" E_LogTime='{0}',E_LogIp='{1}' ", DateTime.Now, config.GetUserIP()),
            ////                    string.Format(" E_Code='{0}' ", UserTable.E_Code));

            ////                //检查是否在线
            ////                checkOnline(UserTable.E_Code.ToLower(), UserTable.E_Password, config.GetSessionID);
            ////                //登录成功进入系统                       
            ////                Signin(UserTable.E_Code);
            ////            }
            ////        }
            ////        else
            ////        { 

            ////            Meg = "用户名/密码(" + UserName + "/" + password + ")密码错误！";
            ////            //插入操作日志
            ////            new EventMessage().EventWriteDB(2, Meg);
            ////            flag = -3;

            ////        }

            ////    }

            //}

            return flag;
        }
        #endregion

        #endregion

        #region "判断24小时内密码出错次数"
        /// <summary>
        /// 判断24小时内密码出错次数
        /// </summary>
        /// <param name="errnumber"></param>
        /// <param name="U_datetime"></param>
        /// <returns></returns>
        private bool pwderr(int errnumber, DateTime? U_datetime)
        {
            bool flag = false;
            if (U_datetime.HasValue)
            {
                DateTime du = (DateTime)U_datetime;

                if (errnumber >= config.pwderr())
                    if (config.DateDiff(du, DateTime.Now) < 1)
                        flag = true;
            }

            return flag;
        }
        #endregion

        #region "用户成功登录"
        /// <summary>
        /// 用户成功登录 
        /// </summary>
        /// <param name="UserName">用户登录名</param>
        private void Signin(string UserName)
        {
            FormsAuthenticationTicket Ticket = new FormsAuthenticationTicket(1, UserName, DateTime.Now,
                            DateTime.Now.AddMinutes(30), false, "/ ", "/ ");   //建立身份验证票对象 
            string HashTicket = FormsAuthentication.Encrypt(Ticket);   //加密序列化验证票为字符串 
            HttpCookie UserCookie = new HttpCookie(FormsAuthentication.FormsCookieName, HashTicket);//生成Cookie                 
            HttpContext.Current.Response.Cookies.Add(UserCookie);   //输出Cookie 


            if (HttpContext.Current.Request["ReturnUrl"] != null)
            {
                HttpContext.Current.Response.Redirect(HttpContext.Current.Request["ReturnUrl"]); // 重定向到用户申请的初始页面
            }
            else
            {
                FormsAuthentication.RedirectFromLoginPage(UserName, true); //转向Default.aspx页面
            }
        }
        #endregion

        #region "检测在线用户表"
        /// <summary>
        /// 检测在线用户表
        /// </summary>
        /// <param name="U_LoginName">用户名</param>
        /// <param name="U_Password">密码</param>
        /// <param name="SessionID">用户sessionID</param>
        /// <param name="userkey">用户Key</param>
        private void checkOnline(string U_LoginName, string U_Password, string SessionID)
        {
            if (!new OnlineDataBase().OnlineCheck(U_LoginName))
            {  
                new OnlineDataBase().InsertOnlineUser(U_LoginName); 
            }
        }
        #endregion

        #region "用户退出"
        /// <summary>
        /// 用户退出
        /// </summary>
        public static  void UserOut()
        {
            //FrameWorkPermission.UserOnlineList.Remove(Common.Get_UserID);
            //写退出日志
           // EventMessage.EventWriteDB(2, "用户退出", config.Get_UserID);
            //退出操作
            if (HttpContext.Current.Session["stu"] != null)
            {
                new OnlineDataBase().OnlineRemove(new UserData().GetUserDate.U_LoginName, config.GetSessionID);
            }
            HttpContext.Current.Session["stu"] = null;

           // System.Web.Security.FormsAuthentication.SignOut();
            //wf 2018-08-14 平台统一退出，注销登录
            //System.Web.HttpContext.Current.Response.Write("<script language='javascript'>window.parent.location.href='/ht/Loginzxc.aspx';</script>");   //("/ht/Login.aspx", false);
            LogoutSSO();
        }

        private static void LogoutSSO()
        {
            SqlDbHelper_1 helper=new SqlDbHelper_1();
            string error="";
            bool bReturn=false;
            string logoutsql = "select redirect_website from Sys_SSOAddress where redirect_code='logout'";
            string address = helper.ExecuteScalar(logoutsql, ref error, ref bReturn).ToString();
            System.Web.HttpContext.Current.Response.Write("<script>window.location='http://test.openapi.cslearning.cn/authApi/auth/logout?redirect_uri="+address+"';</script>");
        }

        #endregion

        /// <summary>
        /// 判断用户Session是否存在，返回Session实体类
        /// </summary> 
        public static Sys_SessionEntity Sessionstu()
        {
            Sys_SessionEntity stu = new Sys_SessionEntity();
            if (HttpContext.Current.Session["stu"] != null)
            { 
                stu = HttpContext.Current.Session["stu"] as Sys_SessionEntity; 
            }
            else
            {
               UserOut(); 
            }
            return stu;
        }

        ////记录密码出错次数
        //public void sys_UserErrPwd(string Table_FiledsValue,string Wheres)
        //{
        //    string TableName = "Sys_users";

        //    new Method().Update_Table_Fileds(TableName, Table_FiledsValue, Wheres);
        //}


    }
}
