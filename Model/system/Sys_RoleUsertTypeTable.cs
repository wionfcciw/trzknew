using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    ///<summary>
    ///角色用户类型 Sys_RoleUsertTypeEntity实体类(Sys_RoleUsertType) 
    ///</summary>
    [Serializable]
    public partial class Sys_RoleUsertTypeTable
    {
        #region "Private Variables"
        private string _DataTable_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除
        private Int32 _id = 0; // id
        private Int32 _A_roleid = 0; // A_roleid
        private Int32 _A_UserTypeID = 0; // A_UserTypeID
        private string _A_UserTypes = ""; // A_UserTypeID多个用,隔开
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
        /// id
        /// </summary>
        public Int32 id
        {
            set { this._id = value; }
            get { return this._id; }
        }

        /// <summary>
        /// 色角ID
        /// </summary>
        public Int32 A_roleid
        {
            set { this._A_roleid = value; }
            get { return this._A_roleid; }
        }

        /// <summary>
        /// 用户类型ID
        /// </summary>
        public Int32 A_UserTypeID
        {
            set { this._A_UserTypeID = value; }
            get { return this._A_UserTypeID; }
        }
         /// <summary>
        /// 用户类型ID A_UserTypeID多个用,隔开
        /// </summary>
        public string A_UserTypes
        {
            set { this._A_UserTypes = value; }
            get { return this._A_UserTypes; }
        } 

        #endregion
    }
}
