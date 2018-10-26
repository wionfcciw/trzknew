using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    
    ///<summary>
    ///Sys_rolesTable实体类(Sys_roles)
    ///</summary>
 
    public   class Sys_rolesTable
    {
        #region "Private Variables"
        private string _DataTable_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除
        private int _Roleid=0; // 角色ID标识
        private string _R_name = ""; // 角色名称
        private string _R_descript = ""; // 角色描述
        #endregion

        #region "Public Variables"
        ///<summary>
        /// 操作方法 Insert:增加 Update:修改 Delete:删除
        ///</summary>
        public string DataTable_Action_
        {
            set { this._DataTable_Action_ = value; }
            get { return this._DataTable_Action_; }
        }
        /// <summary>
        /// 角色ID标识
        /// </summary>
        public int  Roleid
        {
            set { this._Roleid = value; }
            get { return this._Roleid; }
        }
            
        /// <summary>
        /// 角色名称
        /// </summary>
        public string R_name
        {
            set { this._R_name = value; }
            get { return this._R_name; }
        }
            
        /// <summary>
        /// 角色描述
        /// </summary>
        public string R_descript
        {
            set { this._R_descript = value; }
            get { return this._R_descript; }
        }
            
        #endregion
    }
 
}
