using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Data;
using System.Text;
 
using Model;

namespace BLL
{
    /// <summary>
    /// 在线用户数据库处理类
    /// </summary>
    public class OnlineDataBase
    {
        //定时器
        Timer _ClearTimeOutUser;

        //毫秒(5分钟)
        private int runtime = 60000 * config.OnlineMinute;
        /// <summary>
        /// 构造函数
        /// </summary>
        public OnlineDataBase()
        {
            _ClearTimeOutUser = new Timer(new TimerCallback(statTimer_Elapsed), null, 0, runtime);
        }

        /// <summary>
        /// 定时运行删除操作
        /// </summary>
        /// <param name="o"></param>
        private void statTimer_Elapsed(object o)
        {
            if (_ClearTimeOutUser != null)
            {
                _ClearTimeOutUser.Change(Timeout.Infinite, runtime);
                ClearOnlineUserTimeOut();
                _ClearTimeOutUser.Change(runtime, runtime);

            }
            else
            {
                _ClearTimeOutUser = new Timer(new TimerCallback(statTimer_Elapsed), null, 0, runtime);
            }
        }

        /// <summary>
        /// 插入用户
        /// </summary>
        /// <param name="username">用户名</param>
        public void InsertOnlineUser(string username)
        {
            new Method().InsertMemberOnline(username, config.GetSessionID);
        }

        /// <summary>
        /// 检测用户是否在线
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>是/否</returns>
        public bool OnlineCheck(string username)
        {
            if (new Method().sys_OnlineDisp(username).OnlineID == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 更新用户最后访问时间
        /// </summary>
        /// <param name="username">用户名</param>
        public void OnlineAccess(string username)
        {
            sys_OnlineTable online = new Method().sys_OnlineDisp(username);
            online.O_LastTime = DateTime.Now;
            online.O_LastUrl = config.GetScriptUrl;
            online.DB_Option_Action_ = "Update";
            new Method().sys_OnlineInsertUpdate(online);
        }
        /// <summary>
        /// 移除在线用户
        /// </summary>
        /// <param name="username">用户名</param>
        public void OnlineRemove(string username)
        {
            sys_OnlineTable online = new Method().sys_OnlineDisp(username);
            online.DB_Option_Action_ = "Delete";
            new Method().sys_OnlineInsertUpdate(online);
        }
        /// <summary>
        /// 获得在线用户信息
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public OnlineUser<string> GetOnlineMember(string username)
        {
            sys_OnlineTable online = new Method().sys_OnlineDisp(username);
            OnlineUser<string> ou = new OnlineUser<string>();
            ou.U_Guid = online.O_SessionID;
            ou.U_LastIP = online.O_Ip;
            ou.U_LastTime = online.O_LastTime;
            ou.U_LastUrl = online.O_LastUrl;
            ou.U_Name = online.O_UserName;
            ou.U_StartTime = online.O_LoginTime;
            TimeSpan ts = ou.U_LastTime - ou.U_StartTime;
            ou.U_OnlineSeconds = ts.TotalSeconds;
            ou.U_Type = true;

            return ou;
        }

        /// <summary>
        /// 清除超时在线用户
        /// </summary>
        public void ClearOnlineUserTimeOut()
        {
            QueryParam qp = new QueryParam();

           // qp.Where = string.Format("  O_LastTime<='{0}'", config.FormatDateToString(DateTime.Now.AddMinutes((config.OnlineMinute * -1))));

            qp.PageSize = int.MaxValue;
            int rInt = 0;
            DataTable dt = new Method().sys_OnlineList(qp, out rInt);
            if (dt.Rows.Count > 0)
            { 
                List<sys_OnlineTable> lst = new Method().DT2EntityList<sys_OnlineTable>(dt);

                for (int i = 0; i < lst.Count; i++)
                {
                    sys_OnlineTable var = lst[i];

                    if (DateDiff_minu(var.O_LastTime))
                    {
                        var.DB_Option_Action_ = "Delete";
                        new Method().sys_OnlineInsertUpdate(var);
                    }
                }
            }
        }

        /// <summary>
        /// //5分钟没刷新就自动清除在线
        /// </summary> 
        public bool DateDiff_minu(DateTime dt)
        {
            int minu=5 ; //5分钟
            if (config.DateDiff_minu(dt) > minu)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获得在线用户总数
        /// </summary>
        public int GetOnlineUserNum
        {
            get
            {
                int rInt = 0;
                new Method().sys_OnlineList(new QueryParam(), out rInt);
                return rInt;
            }
        }
        /// <summary>
        /// 获得在线用户名表
        /// </summary>
        /// <param name="pageindex">页码</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="totalnum">记录总数</param>
        /// <returns>列表集合</returns>
        public List<OnlineUser<string>> GetOnlineList(int pageindex, int pagesize, out int totalnum)
        {
            List<OnlineUser<string>> lst = new List<OnlineUser<string>>();
            QueryParam qp = new QueryParam();
            qp.PageIndex = pageindex;
            qp.PageSize = pagesize;
            int RecordCount = 0;
            DataTable dt = new Method().sys_OnlineList(qp, out RecordCount);

            if (RecordCount > 0)
            {
                List<sys_OnlineTable> lsta = new Method().DT2EntityList<sys_OnlineTable>(dt);

                for (int i = 0; i < lsta.Count; i++)
                {
                    sys_OnlineTable var = lsta[i];

                    OnlineUser<string> ou = new OnlineUser<string>();
                    ou.U_Guid = var.O_SessionID;
                    ou.U_LastIP = var.O_Ip;
                    ou.U_LastTime = var.O_LastTime;
                    ou.U_LastUrl = var.O_LastUrl;
                    ou.U_Name = var.O_UserName;
                    ou.U_StartTime = var.O_LoginTime;
                    TimeSpan ts = ou.U_LastTime - ou.U_StartTime;
                    ou.U_OnlineSeconds = ts.TotalSeconds;
                    ou.U_Type = true;
                    lst.Add(ou);
                }
            }
            totalnum = RecordCount;
            return lst;
        }


        /// <summary>
        /// 检测当前用户是否在线
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="sessionid">用户唯一标识</param>
        /// <returns>是/否</returns>
        public static bool   CheckUserInOnline(string username, string sessionid)
        {
            if (new Method().sys_OnlineDisp(username, sessionid).OnlineID == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// 删除在线用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="sessionid">用户sessionid</param>
        public void OnlineRemove(string username, string sessionid)
        {
            sys_OnlineTable so = new Method().sys_OnlineDisp(username, sessionid);
            so.DB_Option_Action_ = "Delete";
            new Method().sys_OnlineInsertUpdate(so);
        }



        #region "更新登录时间"
        /// <summary>
        /// 更新登录时间
        /// </summary>
        public static void UpDatetime()
        {
            if (CheckUserInOnline(config.Get_UserName, config.GetSessionID))
            {
                //登录成功记录ip和时间
                new Method().Update_Table_Fileds("sys_Online",
                    string.Format(" O_LastTime='{0}' ", DateTime.Now),
                    string.Format(" O_UserName='{0}' and O_SessionID='{1}'", config.Get_UserName, config.GetSessionID));

            }
            else
            {
                new Method().InsertMemberOnline(config.Get_UserName, config.GetSessionID);
            }
        }
        #endregion 



        /// <summary>
        /// 用户基础类
        /// </summary>
        public class OnlineUser<TKey>
        {
            #region "Private Variables"
            private TKey _U_Guid;
            private string _U_Name = "游客";
            private DateTime _U_StartTime = DateTime.Now;
            private DateTime _U_LastTime = DateTime.Now;
            private string _U_LastIP = config.GetUserIP();
            private bool _U_Type = false;
            private string _U_LastUrl;
            private double _U_OnlineSeconds;
            #endregion

            #region "Public Variables"
            /// <summary>
            /// 用户标识值
            /// </summary>
            public TKey U_Guid
            {
                get
                {
                    return _U_Guid;
                }
                set
                {
                    _U_Guid = value;
                }
            }
            /// <summary>
            /// 用户名
            /// </summary>
            public string U_Name
            {
                get
                {
                    return _U_Name;
                }
                set
                {
                    _U_Name = value;
                }
            }
            /// <summary>
            /// 开始访问时间
            /// </summary>
            public DateTime U_StartTime
            {
                get
                {
                    return _U_StartTime;
                }
                set
                {
                    _U_StartTime = value;
                }
            }
            /// <summary>
            /// 最后访问时间
            /// </summary>
            public DateTime U_LastTime
            {
                get
                {
                    return _U_LastTime;
                }
                set
                {
                    _U_LastTime = value;
                }
            }
            /// <summary>
            /// 是否会员(True会员False游客)
            /// </summary>
            public bool U_Type
            {
                get
                {
                    return _U_Type;
                }
                set
                {
                    _U_Type = value;
                }
            }
            /// <summary>
            /// 用户IP
            /// </summary>
            public string U_LastIP
            {
                get
                {
                    return _U_LastIP;
                }
                set
                {
                    _U_LastIP = value;
                }
            }
            /// <summary>
            /// 最后请求网址
            /// </summary>
            public string U_LastUrl
            {
                get
                {
                    return _U_LastUrl;
                }
                set
                {
                    _U_LastUrl = value;
                }
            }
            /// <summary>
            /// 在线时长（秒）
            /// </summary>
            public double U_OnlineSeconds
            {
                get
                {
                    return _U_OnlineSeconds;
                }
                set
                {
                    _U_OnlineSeconds = value;
                }
            }
            #endregion

        }
    }
 }
