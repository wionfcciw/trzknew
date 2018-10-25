using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 网站菜单实体类Sys_Menu
    /// </summary>
    [Serializable]
    public class Sys_MenuTable
    {

        #region "Private Variables"
        private string _DataTable_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除
        private Int32 _id = 0; // 自动增长ID
        private String _M_Name = ""; // 菜单名称
        private String _M_Url = ""; // 联接网址
        private Int32 _M_Order = 0; // 排序
        private Int32 _M_Tag = 0; // 0关闭 1开通
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
        /// 自动增长ID
        /// </summary>
        public Int32 id
        {
            set { this._id = value; }
            get { return this._id; }
        }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public String M_Name
        {
            set { this._M_Name = value; }
            get { return this._M_Name; }
        }

        /// <summary>
        /// 联接网址
        /// </summary>
        public String M_Url
        {
            set { this._M_Url = value; }
            get { return this._M_Url; }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public Int32 M_Order
        {
            set { this._M_Order = value; }
            get { return this._M_Order; }
        }

        /// <summary>
        /// 0关闭 1开通
        /// </summary>
        public Int32 M_Tag
        {
            set { this._M_Tag = value; }
            get { return this._M_Tag; }
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
