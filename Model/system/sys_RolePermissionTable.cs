using System;
using System.Collections.Generic;
 
using System.Text;

namespace Model
{
    /// <summary>
    /// 角色应用权限实体类
    /// </summary>
    [Serializable]
    public class sys_RolePermissionTable
    { 
        #region "Private Variables"
        private string _DB_Option_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除 
        private int _Pid = 0;  // 角色应用权限自动ID
        private int _P_RoleID = 0;  // 角色ID与sys_Roles表中RoleID相
        private int _P_moduleid = 0;  // 角色所属应用ID与sys_module
        private string _P_PageCode; //角色应用中页面权限代码
        private int _P_Value = 0;  // 权限值
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
        /// 角色应用权限自动ID
        /// </summary>
        public int Pid
        {
            set { this._Pid = value; }
            get { return this._Pid; }
        }

        /// <summary>
        /// 角色ID与sys_Roles表中RoleID相
        /// </summary>
        public int P_RoleID
        {
            set { this._P_RoleID = value; }
            get { return this._P_RoleID; }
        }

        /// <summary>
        /// 角色所属应用ID与sys_Module
        /// </summary>
        public int P_moduleid
        {
            set { this._P_moduleid = value; }
            get { return this._P_moduleid; }
        }

        /// <summary>
        /// 角色应用中页面权限代码
        /// </summary>
        public string P_PageCode
        {
            set { this._P_PageCode = value; }
            get { return this._P_PageCode; }
        }

        /// <summary>
        /// 权限值
        /// </summary>
        public int P_Value
        {
            set { this._P_Value = value; }
            get { return this._P_Value; }
        }

        #endregion
    }
}
