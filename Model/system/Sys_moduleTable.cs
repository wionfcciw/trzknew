using System;
using System.Collections.Generic;
 
using System.Text;

namespace Model
{
    /// <summary>
    /// 模块实体类
    /// </summary>
    public class Sys_moduleTable
    {
        #region "Private Variables"
        private string _DataTable_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除
        private int _Moduleid = 0; // 功能模块ID标识
        private string _M_modulename = ""; // 模块名称
        private int _M_order = 0; // 模块排序
        private string _M_tag = ""; // 模块开启标识,1开启,0关闭,默认1
        private int _OrderType = 0; //排序类别 1向上,2向下
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
        /// 功能模块ID标识
        /// </summary>
        public int Moduleid
        {
            set { this._Moduleid = value; }
            get { return this._Moduleid; }
        }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string M_modulename
        {
            set { this._M_modulename = value; }
            get { return this._M_modulename; }
        }

        /// <summary>
        /// 模块排序
        /// </summary>
        public int M_order
        {
            set { this._M_order = value; }
            get { return this._M_order; }
        }

        /// <summary>
        /// 模块开启标识,1开启,0关闭,默认1
        /// </summary>
        public string M_tag
        {
            set { this._M_tag = value; }
            get { return this._M_tag; }
        }
        /// <summary>
        /// 排序类别 1向上,2向下
        /// </summary>
        public int OrderType
        {
            set { this._OrderType = value; }
            get { return this._OrderType; }
        }

        #endregion
    }
}
