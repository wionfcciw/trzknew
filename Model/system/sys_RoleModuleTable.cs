using System;
using System.Collections.Generic;
 
using System.Text;

namespace Model
{
    /// <summary>
    /// 角色模块实体类
    /// </summary>
    public class sys_RoleModuleTable
    {
        #region "Private Variables"
        private string _DB_Option_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除 
        private int _A_RoleID = 0;  // 角色ID 
        private int _A_moduleid = 0;  // 模块ID
        private string _A_moduleids = "";  //多个模块ID 用“,”隔开
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
        /// 角色ID 
        /// </summary>
        public int A_RoleID
        {
            set { this._A_RoleID = value; }
            get { return this._A_RoleID; }
        }

        /// <summary>
        /// 模块ID
        /// </summary>
        public int A_moduleid
        {
            set { this._A_moduleid = value; }
            get { return this._A_moduleid; }
        }

        /// <summary>
        /// 多个模块ID 用“,”隔开
        /// </summary>
        public string A_moduleids
        {
            set { this._A_moduleids = value; }
            get { return this._A_moduleids; }
        }

        #endregion
    }
}
