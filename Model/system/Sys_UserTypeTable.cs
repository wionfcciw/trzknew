using System;
using System.Collections.Generic; 
using System.Text;

namespace Model
{
    ///<summary>
    ///Sys_UserTypeTable实体类(Sys_UserType)
    ///</summary>
  public  class Sys_UserTypeTable
    {
        #region "Private Variables"
        private string _DataTable_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除
        private int _TypeID = 0; // 用户类型ID
        private string _T_Name = ""; // 用户类型
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
        /// 用户类型ID
        /// </summary>
        public int TypeID
        {
            set { this._TypeID = value; }
            get { return this._TypeID; }
        }

        /// <summary>
        /// 用户类型
        /// </summary>
        public string T_Name
        {
            set { this._T_Name = value; }
            get { return this._T_Name; }
        }

        #endregion
    }
}
