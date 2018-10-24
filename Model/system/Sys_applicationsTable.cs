using System;
using System.Collections.Generic;
 
using System.Text;

namespace Model
{
    /// <summary>
    /// 应用实体类
    /// </summary>
    public class Sys_applicationsTable
    {
        #region "Private Variables"
        private string _DataTable_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除
        private int _Applicationid = 0; // 应用ID标识
        private int _A_moduleid = 0; // 所属模块ID
        private string _A_appname = ""; // 应用名称
        private string _A_url = ""; // 该应用的URL地址
        private int _A_order = 0; // 应用排序
        private string _A_picurl = ""; // 应用的图标地址
        private string _A_pagecode = ""; // 该应用的页面权限代码
        private string _A_tag = ""; // 应用开启标识,1开启,0关闭
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
        /// 应用ID标识
        /// </summary>
        public int Applicationid
        {
            set { this._Applicationid = value; }
            get { return this._Applicationid; }
        }

        /// <summary>
        /// 所属模块ID
        /// </summary>
        public int A_moduleid
        {
            set { this._A_moduleid = value; }
            get { return this._A_moduleid; }
        }

        /// <summary>
        /// 应用名称
        /// </summary>
        public string A_appname
        {
            set { this._A_appname = value; }
            get { return this._A_appname; }
        }

        /// <summary>
        /// 该应用的URL地址
        /// </summary>
        public string A_url
        {
            set { this._A_url = value; }
            get { return this._A_url; }
        }

        /// <summary>
        /// 应用排序
        /// </summary>
        public int A_order
        {
            set { this._A_order = value; }
            get { return this._A_order; }
        }

        /// <summary>
        /// 应用的图标地址
        /// </summary>
        public string A_picurl
        {
            //get
            //{
            //    if (string.IsNullOrEmpty(this.A_picurl))
            //        return "~/images/tree_file.gif";
            //    else
            //        return this.A_picurl;
            //}
            //set
            //{
            //    this.A_picurl = value;
            //}

            set { this._A_picurl = value; }
            get { return this._A_picurl; }
        }

        /// <summary>
        /// 该应用的页面权限代码
        /// </summary>
        public string A_pagecode
        {
            set { this._A_pagecode = value; }
            get { return this._A_pagecode; }
        }

        /// <summary>
        /// 应用开启标识,1开启,0关闭
        /// </summary>
        public string A_tag
        {
            set { this._A_tag = value; }
            get { return this._A_tag; }
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
