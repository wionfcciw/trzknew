using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 用户实体类
    /// </summary>
    [Serializable]
    public class sys_UserTable
    {
        #region "Private Variables"
        private string _DB_Option_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除 Reset:初始化密码
        private int _Uid = 0;  // 用户ID号
        private string _U_xm;  // 姓名
        private string _U_xb;  // 性别 
        private string _U_phone; //联系电话
        private string _U_loginname = "";  // 登录名
        private string _U_Password; //密码md5加密字符
        private string _U_key; //密匙号
        private string _U_city="01"; //市代码   默认广州市
        private string _U_area = "0104"; //区代码  默认越秀区
        private string _U_jigou = "01"; //所属机构代码  默认中山大学
        private string _U_department; //部门 管辖范围
        private int _U_usertype; //用户类型
        private int _U_tag; //用户状态 1：开通，0：锁定，-1： 关闭
        private string _U_ip; //登录ip
        private DateTime? _U_datetime; // 用户登录时间
        private int _U_errnumber; //密码错误次数

        
        #endregion

        #region "Public Variables"
        /// <summary>
        /// 操作方法 Insert:增加 Update:修改 Delete:删除 Reset:初始化密码
        /// </summary>
        public string DB_Option_Action_
        {
            set { this._DB_Option_Action_ = value; }
            get { return this._DB_Option_Action_; }
        }

        /// <summary>
        /// 用户ID号
        /// </summary>
        public int UserID
        {
            set { this._Uid = value; }
            get { return this._Uid; }
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public string U_xm
        {
            set { this._U_xm = value; }
            get { return this._U_xm; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public string U_xb
        {
            set { this._U_xb = value; }
            get { return this._U_xb; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string U_phone
        {
            set { this._U_phone = value; }
            get { return this._U_phone; }
        }

        /// <summary>
        /// 登录名
        /// </summary>
        public string U_LoginName
        {
            set { this._U_loginname = value; }
            get { return this._U_loginname; }
        }

        /// <summary>
        /// 密码md5加密字符
        /// </summary>
        public string U_Password
        {
            set { this._U_Password = value; }
            get { return this._U_Password; }
        }

        /// <summary>
        /// 密匙号
        /// </summary>
        public string U_key
        {
            set { this._U_key = value; }
            get { return this._U_key; }
        }


        /// <summary>
        /// 市代码
        /// </summary>
        public string U_city
        {
            set { this._U_city = value; }
            get { return this._U_city; }
        }

        /// <summary>
        /// 区代码
        /// </summary>
        public string U_area
        {
            set { this._U_area = value; }
            get { return this._U_area; }
        }

        /// <summary>
        /// 所属机构代码
        /// </summary>
        public string U_jigou
        {
            set { this._U_jigou = value; }
            get { return this._U_jigou; }
        }

        /// <summary>
        /// 部门管辖范围
        /// </summary>
        public string U_department
        {
            set { this._U_department = value; }
            get { return this._U_department; }
        }

        /// <summary>
        /// 用户类型
        /// </summary>
        public int U_usertype
        {
            set { this._U_usertype = value; }
            get { return this._U_usertype; }
        }

        /// <summary>
        /// 用户状态 1：开通，0：锁定，-1： 关闭
        /// </summary>
        public int U_tag
        {
            set { this._U_tag = value; }
            get { return this._U_tag; }
        }

        /// <summary>
        /// 登录ip
        /// </summary>
        public string U_ip
        {
            set { this._U_ip = value; }
            get { return this._U_ip; }
        }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? U_datetime
        {
            set { this._U_datetime = value; }
            get { return this._U_datetime; }
        }

        /// <summary>
        /// 密码错误次数
        /// </summary>
        public int U_errnumber
        {
            set { this._U_errnumber = value; }
            get { return this._U_errnumber; }
        }
        

        #endregion

        private string _Lxr = "";

        public string Lxr
        {
            get { return _Lxr; }
            set { _Lxr = value; }
        }
        private string _Lxdh2 = "";

        public string Lxdh2
        {
            get { return _Lxdh2; }
            set { _Lxdh2 = value; }
        }
        private string _Lxdh3 = "";

        public string Lxdh3
        {
            get { return _Lxdh3; }
            set { _Lxdh3 = value; }
        }
        private string _Txdz = "";

        public string Txdz
        {
            get { return _Txdz; }
            set { _Txdz = value; }
        }
        private string _Yb = "";

        public string Yb
        {
            get { return _Yb; }
            set { _Yb = value; }
        }
        private string _Zw = "";

        public string Zw
        {
            get { return _Zw; }
            set { _Zw = value; }
        }
    }
}
