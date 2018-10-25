using System;
using System.Collections.Generic;
 
using System.Text;
using Model;

namespace BLL
{
    /// <summary>
    /// 日志消息类
    /// </summary>
    public class EventMessage
    {
        /// <summary>
        /// 写入日志到DB
        /// </summary>
        /// <param name="E_Type">日志类型：日记类型,1:操作日记2:安全日志</param>
        /// <param name="E_Record">日志内容</param>
        public static  void EventWriteDB(int E_Type, string E_Record)
        {
            EventWriteDB(E_Type, E_Record, config.Get_UserName);
        }

        /// <summary>
        /// 写入日志到DB
        /// </summary>
        /// <param name="E_Type">日志类型：日记类型,1:操作日记2:安全日志</param>
        /// <param name="E_Record">日志内容</param>
        /// <param name="userid">关联用户id</param>
        public static void EventWriteDB(int E_Type, string E_Record, string UserName)
        {
            sys_EventTable s_Et = new sys_EventTable();
            s_Et.E_DateTime = DateTime.Now;
            s_Et.E_From = config.GetScriptUrl;
            s_Et.E_IP = config.GetUserIP();
            s_Et.E_Record = E_Record;
          //  s_Et.E_UserID = new UserData().Get_sys_UserTable(UserName).UserID;
            s_Et.E_U_LoginName =UserName;
            s_Et.E_Type = E_Type;
            s_Et.DB_Option_Action_ = "Insert";

            //if (userid == 1)
            //{
            //    s_Et.E_ModuleID = 0; //Moduleid;  //模块ID
            //    s_Et.E_M_ModName = ""; // new Method().sys_ModuleList(PMI.ApplicationID).A_AppName;  //模块名称
            //    s_Et.E_A_PageCode = ""; // PMI.PageCode;
            //    s_Et.E_A_AppName = ""; // new Method().sys_ApplicationDisp(PMI.ApplicationID, PMI.PageCode).M_CName;  //应用名称
            //}

            new Method().sys_EventInsertUpdate(s_Et);
        }
    }
}
