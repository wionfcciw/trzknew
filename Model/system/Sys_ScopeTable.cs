using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{ ///<summary>
    ///Sys_ScopeTable实体类(Scope_Tree)
    ///</summary>
    [Serializable]
    public partial class Sys_ScopeTable
    {
        #region "Private Variables"
        private string _DataTable_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除
        private int _ScopeID = 0; // ID 递增
        private string _S_Name=""; // 名称
 
        private string _S_Code=""; // 代码
        private int _S_ParentID=0; // 父级ID
        private int _S_Depth=0; // 深度
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
        /// ScopeID 递增
        /// </summary>
        public int ScopeID
        {
            set { this._ScopeID = value; }
            get { return this._ScopeID; }
        }
            
        /// <summary>
        /// 名称
        /// </summary>
        public string S_Name
        {
            set { this._S_Name = value; }
            get { return this._S_Name; }
        }
            
            
        /// <summary>
        /// 代码
        /// </summary>
        public string S_Code
        {
            set { this._S_Code = value; }
            get { return this._S_Code; }
        }
            
        /// <summary>
        /// 父级ID
        /// </summary>
        public int S_ParentID
        {
            set { this._S_ParentID = value; }
            get { return this._S_ParentID; }
        }
            
        /// <summary>
        /// 深度
        /// </summary>
        public int S_Depth
        {
            set { this._S_Depth = value; }
            get { return this._S_Depth; }
        }
            
        #endregion
    }
}