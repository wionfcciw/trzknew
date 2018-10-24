
using System;
using System.Collections.Generic;
using System.Text;
 

namespace Model
{
    ///<summary>
    ///Sys_PermissionTable实体类(Sys_Permission)
    ///</summary>
    [Serializable]
    public partial class Sys_PermissionTable
    {
        #region "Private Variables"
        private string _DataTable_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除
        private Int32 _id=0; // 自动增长ID
        private String _PageCode=""; // 页面代码
        private String _PermissionName=""; // 权限名称
        private Int32 _PermissionValue=0; // 权限值
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
        /// 自动增长ID
        /// </summary>
        public Int32  id
        {
            set { this._id = value; }
            get { return this._id; }
        }
            
        /// <summary>
        /// 页面代码
        /// </summary>
        public String  PageCode
        {
            set { this._PageCode = value; }
            get { return this._PageCode; }
        }
            
        /// <summary>
        /// 权限名称
        /// </summary>
        public String  PermissionName
        {
            set { this._PermissionName = value; }
            get { return this._PermissionName; }
        }
            
        /// <summary>
        /// 权限值
        /// </summary>
        public Int32  PermissionValue
        {
            set { this._PermissionValue = value; }
            get { return this._PermissionValue; }
        }
            
        #endregion
    }
}
  