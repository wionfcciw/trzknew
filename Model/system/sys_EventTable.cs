using System;
using System.Collections.Generic;
 
using System.Text;


namespace Model
{
    /// <summary>
    ///  事件实体类
    /// </summary>
    public class sys_EventTable
    {

        #region "Private Variables"
        private string _DB_Option_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除 
        private int _EventID = 0;  // 事件ID号
        private string _E_U_LoginName;  // 用户名
        private int _E_UserID = 0;  // 操作时用户ID与sys_Users中UserID
        private DateTime _E_DateTime; // 事件发生的日期及时间
        private int _E_ModuleID = 0;  // 所属应用程序ID与sys_Module
        private string _E_M_ModName;  // 所属模块名称
        private string _E_A_AppName;  // PageCode应用名称与sys_Applications相同
        private string _E_A_PageCode; //发生事件时应用名称
        private string _E_From;  // 来源
        private int _E_Type = 0;  // 日记类型,1:操作日记2:安全日志3
        private string _E_IP; //客户端IP地址
        private string _E_Record;  // 详细描述
        #endregion

        #region "Public Variables"
        /// <summary>
        /// 操作方法 Insert:增加 Update:修改 Delete:删除 
        /// </summary>
        public string DB_Option_Action_
        {
            set { this._DB_Option_Action_ = value; }
            get { return this._DB_Option_Action_; }
        }

        /// <summary>
        /// 事件ID号
        /// </summary>
        public int EventID
        {
            set { this._EventID = value; }
            get { return this._EventID; }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string E_U_LoginName
        {
            set { this._E_U_LoginName = value; }
            get { return this._E_U_LoginName; }
        }

        /// <summary>
        /// 操作时用户ID与sys_Users中UserID
        /// </summary>
        public int E_UserID
        {
            set { this._E_UserID = value; }
            get { return this._E_UserID; }
        }

        /// <summary>
        /// 事件发生的日期及时间
        /// </summary>
        public DateTime E_DateTime
        {
            set { this._E_DateTime = value; }
            get { return this._E_DateTime; }
        }

        /// <summary>
        /// 所属模块程序ID与sys_module
        /// </summary>
        public int E_ModuleID
        {
            set { this._E_ModuleID = value; }
            get { return this._E_ModuleID; }
        }

        /// <summary>
        /// 所属模块名称
        /// </summary>
        public string E_M_ModName
        {
            set { this._E_M_ModName = value; }
            get { return this._E_M_ModName; }
        }

        /// <summary>
        /// PageCode应用名称与sys_Application相同
        /// </summary>
        public string E_A_AppName
        {
            set { this._E_A_AppName = value; }
            get { return this._E_A_AppName; }
        }

        /// <summary>
        /// 发生事件时应用名称
        /// </summary>
        public string E_A_PageCode
        {
            set { this._E_A_PageCode = value; }
            get { return this._E_A_PageCode; }
        }

        /// <summary>
        /// 来源
        /// </summary>
        public string E_From
        {
            set { this._E_From = value; }
            get { return this._E_From; }
        }

        /// <summary>
        /// 日记类型,1:操作日记2:安全日志3
        /// </summary>
        public int E_Type
        {
            set { this._E_Type = value; }
            get { return this._E_Type; }
        }

        /// <summary>
        /// 客户端IP地址
        /// </summary>
        public string E_IP
        {
            set { this._E_IP = value; }
            get { return this._E_IP; }
        }

        /// <summary>
        /// 详细描述
        /// </summary>
        public string E_Record
        {
            set { this._E_Record = value; }
            get { return this._E_Record; }
        }

        #endregion
    }
}
