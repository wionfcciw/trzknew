using System;
using System.Collections.Generic;
 
using System.Text;

namespace Model
{
    /// <summary>
    /// 用户所属角色实体类
    /// </summary>
    public class sys_UserRolesTable
    {
        #region "Private Variables"
        private string _DB_Option_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除 
        private string _R_UserName = "";  // 用户ID与sys_User表中U_loginname相关
        private int _R_RoleID = 0;  // 用户所属角色ID与Sys_Roles关联
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
        /// 用户ID与sys_User表中U_loginname相关
        /// </summary>
        public string R_UserName
        {
            set { this._R_UserName = value; }
            get { return this._R_UserName; }
        }

        /// <summary>
        /// 用户所属角色ID与Sys_Roles关联
        /// </summary>
        public int R_RoleID
        {
            set { this._R_RoleID = value; }
            get { return this._R_RoleID; }
        }

        #endregion
	

    }
}
