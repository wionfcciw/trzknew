using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Newtonsoft.Json;
using System.Data;
using SincciKC.SsoLogin.UserJsonEntityPack.SSOModel;
using SincciKC.SsoLogin.TokenJsonEntityPack.SSOModel;
using BLL;
using Model;
using System.Collections.Specialized;
using System.IO;
using SincciKC.SsoLogin.SSOModel.UserRoleAndDeptJsonEntityPack;
using System.Configuration;
using SincciKC.SsoLogin.SSOModel.SchoolAdminJsonEntityPack;
using SincciKC.SsoLogin.SSOMethod;
namespace SincciKC.SsoLogin
{
    public partial class GetCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string addresstitle = "http://" + HttpContext.Current.Request.Url.Authority;
            SqlDbHelper_1 helper = new SqlDbHelper_1();
            WebClient MyWebClient = new WebClient();
            string error = "";
            bool bReturn = false;
            //此处修改从配置文件中取
            //string sql = "select redirect_website from Sys_SSOAddress where redirect_code='gettoken'";
            //string address = helper.ExecuteScalar(sql, ref error, ref bReturn).ToString();
            string address = ConfigurationManager.AppSettings["tokenOfloginPlatform"];
            address = addresstitle + address;
            //此处必然会有一个code,因为经过了重定向，直接生成。
            string code = Request.QueryString["code"];
            string token="";
            string html = "http://openapi.tredu.gov.cn/authApi/auth/accessToken?client_id=" + ConfigurationManager.AppSettings["Client-Id"] + "&client_secret=" + ConfigurationManager.AppSettings["Client-Secret"] + "&grant_type=authorization_code&redirect_uri=" + address + "&code=" + code;
            //Response.Redirect(html,false);

            Byte[] pageData = MyWebClient.DownloadData(html);
            string json = Encoding.UTF8.GetString(pageData);
            TokenJsonEntity tje = JsonConvert.DeserializeObject<TokenJsonEntity>(json);
            //如果能获取到秘钥
            if (tje.responseEntity.access_token != null)
            {
                token = tje.responseEntity.access_token;
                string html2 = "http://openapi.tredu.gov.cn/authApi/auth/userInfo/" + token;
                Byte[] pageData2 = MyWebClient.DownloadData(html2);
                string json2 = Encoding.UTF8.GetString(pageData2);
                UserJsonEntity uje = JsonConvert.DeserializeObject<UserJsonEntity>(json2);
                string userid = uje.responseEntity.userId;
                //通过userid来取用户的信息，判断是否有权限进行管理，当role_code为SCHOOL_Manager的时候才可以进行访问。
                //返回的school_id与表中的school_id对应。即可完成学校端的登录。
                //管理员端的目前有问题，后续仍需要再次处理。
                string htmltemp = "http://openapi.tredu.gov.cn/basedataApi/relRoleMember/getRoleByUserId/" + userid;
                GetDataByPlatform gt = new GetDataByPlatform();
                //使用方法
                string jsontemp=gt.GetDataByOnlyAddressInGet(htmltemp);
                //HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(htmltemp);
                //request1.Method = "GET";
                ////request1.Headers["Access-Token"] = token;
                //request1.Headers["Client-Id"] = "1d98bbaa-0507-49f4-a3dc-ddd51f479d86";
                //request1.Headers["Client-Secret"] = "f60fb940-d7f2-459f-ab08-dc110f9502a3";
                //HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
                //StreamReader responseStream1 = new StreamReader(response1.GetResponseStream());
                //string jsontemp = responseStream1.ReadToEnd();
                SchoolAdminJsonEntity saje = JsonConvert.DeserializeObject<SchoolAdminJsonEntity>(jsontemp);
                if (saje.pageInfo.list.Count > 0)
                {
                    //证明是学校管理员,只有orgIdList里面有数据，这才证明是学校管理员，这里问了技术对接那边，学校管理员和教育局管理员不同时存在。
                    if (saje.pageInfo.list.Where(o => (o.roleCode == "SCHOOL_MANAGER")).ToList()[0].orgIdList.Count > 0)
                    {
                        string schoolid = saje.pageInfo.list.Where(o => (o.roleCode == "SCHOOL_MANAGER")).ToList()[0].orgIdList[0];
                        //然后利用学校信息对照表，将学校和系统中的用户一一对应，数据查好，手动添加到数据库里面。
                        string getschoolusersql = "select sys_name from Sys_SSOUserContrast where organization_id='" + schoolid + "' and type=2";
                        string sys_name = helper.ExecuteScalar(getschoolusersql, ref error, ref bReturn).ToString();
                        string checkstatesql = "select U_tag from Sys_Users where U_loginname='" + sys_name + "'";
                        int statecode = Convert.ToInt32(helper.ExecuteScalar(checkstatesql, ref error, ref bReturn));
                        if (sys_name.Length != 0 && statecode == 1)
                        {
                            string Meg = "";
                            string sysusersql = "select * from Sys_Users where U_loginname='" + sys_name + "'";
                            DataTable dt = helper.selectTab(sysusersql, ref error, ref bReturn);
                            sys_UserTable UserTable = new Method().DT2EntityList<sys_UserTable>(dt)[0];
                            Meg = "欢迎您" + sys_name + "，成功登入。您的IP为：" + config.GetUserIP() + "！";
                            //插入操作日志
                            EventMessage.EventWriteDB(2, Meg, UserTable.U_LoginName);

                            //登录成功记录ip和时间
                            new Method().Update_Table_Fileds("Sys_users",
                                string.Format(" U_errnumber=0,U_tag=1,U_datetime='{0}',U_ip='{1}'", DateTime.Now, config.GetUserIP()),
                                string.Format(" u_loginname='{0}'", UserTable.U_LoginName));
                            //检查是否在线
                            checkOnline(UserTable.U_LoginName.ToLower(), UserTable.U_Password, config.GetSessionID);
                            //登录成功进入系统
                            Sys_SessionEntity stu = new Sys_SessionEntity();
                            stu.UserName = UserTable.U_LoginName;
                            stu.UserType = UserTable.U_usertype;
                            stu.Name = UserTable.U_xm;
                            stu.UserType = UserTable.U_usertype;
                            stu.U_department = UserTable.U_department;
                            stu.Flag = true;
                            HttpContext.Current.Session.Add("stu", stu);
                            Session["token"] = token;
                            Response.Redirect("/system/Admin_center.aspx", false);
                        }
                        //如果用户被禁止或者用户名不存在等情况，则注销当前token，返回到登录界面
                        else
                        {
                            LogoutSSO();
                        }
                    }
                }
                //证明查出来的json是空的了，也就是说明，userid不是管理员，那么有可能是学生或者是招生办相关人员。
                //此处先处理招生办的内容。学生另行考虑
                else
                {
                    //对照用户平台表中的数据，如果该用户的id在此表中有数据，并且用户的type为1，那么可以允许当做管理员登录。
                    string edusql = "select sys_name from Sys_SSOUserContrast where organization_id='" + userid + "' and type=1";
                    //教育局人员用户名，相当于属于是哪个县区/教育局的。
                    string eduusername = helper.ExecuteScalar(edusql, ref error, ref bReturn).ToString();
                    //检查账号的状态，是否有封死的情况
                    string checkstatesql = "select U_tag from Sys_Users where U_loginname='" + eduusername + "'";
                    int statecode = Convert.ToInt32(helper.ExecuteScalar(checkstatesql, ref error, ref bReturn));
                    //如果教育局的本地系统用户名存在且账号没有封死，则允许登录
                    if (eduusername.Length > 0 && statecode == 1)
                    {
                        string Meg = "";
                        string sysusersql = "select * from Sys_Users where U_loginname='" + eduusername + "'";
                        DataTable dt = helper.selectTab(sysusersql, ref error, ref bReturn);
                        sys_UserTable UserTable = new Method().DT2EntityList<sys_UserTable>(dt)[0];
                        Meg = "欢迎您" + eduusername + "，成功登入。您的IP为：" + config.GetUserIP() + "！";
                        //插入操作日志
                        EventMessage.EventWriteDB(2, Meg, UserTable.U_LoginName);

                        //登录成功记录ip和时间
                        new Method().Update_Table_Fileds("Sys_users",
                            string.Format(" U_errnumber=0,U_tag=1,U_datetime='{0}',U_ip='{1}'", DateTime.Now, config.GetUserIP()),
                            string.Format(" u_loginname='{0}'", UserTable.U_LoginName));
                        //检查是否在线
                        checkOnline(UserTable.U_LoginName.ToLower(), UserTable.U_Password, config.GetSessionID);
                        //登录成功进入系统
                        Sys_SessionEntity stu = new Sys_SessionEntity();
                        stu.UserName = UserTable.U_LoginName;
                        stu.UserType = UserTable.U_usertype;
                        stu.Name = UserTable.U_xm;
                        stu.UserType = UserTable.U_usertype;
                        stu.U_department = UserTable.U_department;
                        stu.Flag = true;
                        HttpContext.Current.Session.Add("stu", stu);
                        Session["token"] = token;
                        Response.Redirect("/system/Admin_center.aspx", false);
                    }
                }
                //需要加密访问才能够获取用户的具体信息，所以此处访问方式不同
                //string html3 = "http://openapi.tredu.gov.cn/basedataApi/user/getOrgInfoByUserId/" + userid;
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(html3);
                //request.Method = "GET";
                //request.Headers["Access-Token"] = token;
                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //StreamReader responseStream = new StreamReader(response.GetResponseStream());
                //string json3 = responseStream.ReadToEnd();
                //UserRoleAndDeptJsonEntity urdj = JsonConvert.DeserializeObject<UserRoleAndDeptJsonEntity>(json3);
                //int usertype = urdj.responseEntity.userSwitch.accountType;
                //如果是老师
                //if (usertype == 2)
                //{
                //    //用户对照表，根据接口的要求可能后续会改表的结构，也会更改代码
                //    string username = uje.responseEntity.userLoginName;
                //    string contrastsql = "select sys_name from Sys_SSOUserContrast where user_id='" + userid + "'";
                //    string sys_name = helper.ExecuteScalar(contrastsql, ref error, ref bReturn).ToString();
                //    string checkstatesql = "select U_tag from Sys_Users where U_loginname='" + sys_name + "'";
                //    int statecode=Convert.ToInt32(helper.ExecuteScalar(checkstatesql, ref error, ref bReturn));
                //    if (sys_name.Length != 0&&statecode==1)
                //    {
                //        string Meg = "";
                //        string sysusersql = "select * from Sys_Users where U_loginname='" + sys_name + "'";
                //        DataTable dt = helper.selectTab(sysusersql, ref error, ref bReturn);
                //        sys_UserTable UserTable = new Method().DT2EntityList<sys_UserTable>(dt)[0];
                //        Meg = "欢迎您" + username + "，成功登入。您的IP为：" + config.GetUserIP() + "！";
                //        //插入操作日志
                //        EventMessage.EventWriteDB(2, Meg, UserTable.U_LoginName);

                //        //登录成功记录ip和时间
                //        new Method().Update_Table_Fileds("Sys_users",
                //            string.Format(" U_errnumber=0,U_tag=1,U_datetime='{0}',U_ip='{1}'", DateTime.Now, config.GetUserIP()),
                //            string.Format(" u_loginname='{0}'", UserTable.U_LoginName));


                //        //检查是否在线
                //        checkOnline(UserTable.U_LoginName.ToLower(), UserTable.U_Password, config.GetSessionID);
                //        //登录成功进入系统
                //        //Signin(UserTable.U_LoginName.ToString());
                //        // lst

                //        Sys_SessionEntity stu = new Sys_SessionEntity();
                //        stu.UserName = UserTable.U_LoginName;
                //        stu.UserType = UserTable.U_usertype;
                //        stu.Name = UserTable.U_xm;
                //        stu.UserType = UserTable.U_usertype;
                //        stu.U_department = UserTable.U_department;
                //        stu.Flag = true;
                //        HttpContext.Current.Session.Add("stu", stu);
                //        Session["token"] = token;
                //        //Response.Redirect("/system/Admin_center.aspx", false);
                //        Response.Redirect("/ssologin/autoaddstudent.aspx", false);
                //    }
                //    //如果用户被禁止或者用户名不存在等情况，则注销当前token，返回到登录界面
                //    else
                //    {
                //        LogoutSSO();
                //    }
                //}
                ////如果是学生

                //else if (usertype == 3)
                //{
                //    string ksh = "";
                //    string studentcontrastsql = "select ksh from Sys_SSOStuContrast where user_id='" + userid + "'";
                //    ksh = helper.ExecuteScalar(studentcontrastsql, ref error, ref bReturn).ToString();
                //    string dltype = ConfigurationManager.AppSettings["dltype"];
                //    if(ksh.Length>0)
                //    {
                //        if (dltype == "0")
                //        {
                //            StudentLogin(ksh, dltype,token);
                //        }
                //        else if(dltype=="1")
                //        {
                //            string zkzh = "";
                //            string getzkzhsql = "select zkzh from zk_ksxxgl_login where ksh='" + ksh + "'";
                //            zkzh = helper.ExecuteScalar(getzkzhsql, ref error, ref bReturn).ToString();
                //            if(zkzh.Length>0)
                //            {
                //                StudentLogin(zkzh, dltype,token);
                //            }
                //        }
                //    }
                //}
            }

        }


        private static void LogoutSSO()
        {
            string addresstitle = "http://" + HttpContext.Current.Request.Url.Authority;
            //此处修改，用配置文件来进行取得
            //SqlDbHelper_1 helper = new SqlDbHelper_1();
            //string error = "";
            //bool bReturn = false;
            //string logoutsql = "select redirect_website from Sys_SSOAddress where redirect_code='logout'";
            //string address = helper.ExecuteScalar(logoutsql, ref error, ref bReturn).ToString();
            string address = ConfigurationManager.AppSettings["logoutPlatform"];
            address = addresstitle + address;
            System.Web.HttpContext.Current.Response.Write("<script>window.location='http://openapi.tredu.gov.cn/authApi/auth/logout?redirect_uri=" + address + "';</script>");
        }

        //private void StudentLogin(string ksh,string dltype,string token)
        //{
        //    BLL_zk_ksxxgl bllxx = new BLL_zk_ksxxgl();
        //    Model_zk_ksSession ksSession = new Model_zk_ksSession();
        //    Model_zk_ksxxgl ksinfo = new Model_zk_ksxxgl();
        //    BLL_zk_kscj bllcj = new BLL_zk_kscj();
            
        //    DataTable dd = bllxx.ViewDisp_Login(ksh, dltype);
        //    if(dd.Rows.Count==0||dd.Rows[0]["xm"]=="")
        //    {
        //        //返回，不允许登录，注销当前秘钥。
        //        LogoutSSO();
        //    }
        //    else
        //    {
        //        ksinfo = new SqlDbHelper_1().DT2EntityList<Model_zk_ksxxgl>(dd)[0];
        //        ksSession.Flag = true;
        //        ksSession.ksh = ksinfo.Ksh;
        //        ksSession.xm = ksinfo.Xm;
        //        ksSession.kaoci = ksinfo.Kaoci + "年";
        //        ksSession.Bmdxqdm = ksinfo.Bmdxqdm;
        //        ksSession.Bmddm = ksinfo.Bmddm;
        //        ksSession.Kslbdm = ksinfo.Kslbdm;
        //        ksSession.Bklb = ksinfo.Bklb;
        //        ksSession.Jzfp = ksinfo.Jzfp;
        //        ksSession.Mzdm = ksinfo.Mzdm;
        //        ksSession.Xjtype = ksinfo.Xjtype;
        //        DataTable dt = bllcj.zk_cj(ksinfo.Ksh);
        //        if (dt.Rows.Count > 0)
        //        {
        //            ksSession.Wkzh = dt.Rows[0]["wkzh"].ToString();
        //            ksSession.Dsdj = dt.Rows[0]["dsdj"].ToString();
        //            ksSession.Zhdj = dt.Rows[0]["zhdj"].ToString();
        //            ksSession.Cj = Convert.ToInt32(dt.Rows[0]["zzf"]);
        //        }
        //        ksSession.Ipaddress = config.GetUserIP();
        //        ksSession.Zkzh = ksinfo.Zkzh; 
        //        System.Web.HttpContext.Current.Session.Add("kaosheng", ksSession);
        //        Session["token"] = token;
        //        Response.Redirect("/Default.aspx", false);
        //    }
        //}

        private void checkOnline(string U_LoginName, string U_Password, string SessionID)
        {
            if (!new OnlineDataBase().OnlineCheck(U_LoginName))
            {
                new OnlineDataBase().InsertOnlineUser(U_LoginName);
            }
            
        }

        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }
    }
}