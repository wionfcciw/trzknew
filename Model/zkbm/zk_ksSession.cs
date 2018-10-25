using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 考生登录session
    /// </summary>
    [Serializable]
    public class Model_zk_ksSession
    {
        #region 字段
        private string _zkzh = "";
        private string _ipaddress = "";
        private string _pwd = "";
        private string _kslbdm = "";
        private string _wkzh = "";
        private int _cj = 0;
        private int _xjtype = 0;

        public int Xjtype
        {
            get { return _xjtype; }
            set { _xjtype = value; }
        }
        public int Cj
        {
            get { return _cj; }
            set { _cj = value; }
        }
        public string Wkzh
        {
            get { return _wkzh; }
            set { _wkzh = value; }
        }
        private string _dsdj = "";

        public string Dsdj
        {
            get { return _dsdj; }
            set { _dsdj = value; }
        }
        private string _zhdj = "";

        public string Zhdj
        {
            get { return _zhdj; }
            set { _zhdj = value; }
        }
            
        public string Kslbdm
        {
            get { return _kslbdm; }
            set { _kslbdm = value; }
        }
        private int _bklb = 0;

        public int Bklb
        {
            get { return _bklb; }
            set { _bklb = value; }
        }


        private int _jzfp = 0;

        public int Jzfp
        {
            get { return _jzfp; }
            set { _jzfp = value; }
        }

      
        private string _mzdm = "";

        public string Mzdm
        {
            get { return _mzdm; }
            set { _mzdm = value; }
        }
        
        public string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }
        public string Ipaddress
        {
            get { return _ipaddress; }
            set { _ipaddress = value; }
        }
        public string Zkzh
        {
            get { return _zkzh; }
            set { _zkzh = value; }
        }
        /// <summary>
        /// 准考证号
        /// </summary>
        private string _ksh = "";
        /// <summary>
        /// 姓名
        /// </summary>
        private string _xm = "";

        /// <summary>
        /// 考次
        /// </summary>
        private string _kaoci = "";

        /// <summary>
        /// 登录成功标识
        /// </summary>
        private bool _Flag = false;

        private string bmddm = "";

        public string Bmddm
        {
            get { return bmddm; }
            set { bmddm = value; }
        }
        private string bmdxqdm = "";

        public string Bmdxqdm
        {
            get { return bmdxqdm; }
            set { bmdxqdm = value; }
        }

        #endregion



        #region 属性
        /// <summary>
        /// 准考证号 
        /// </summary>
        public string ksh
        {
            get { return _ksh; }
            set { _ksh = value; }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string xm
        {
            get { return _xm; }
            set { _xm = value; }
        }

        /// <summary>
        /// 考次
        /// </summary>
        public string kaoci
        {
            get { return _kaoci; }
            set { _kaoci = value; }
        }

        /// <summary>
        /// 登录成功标识
        /// </summary>
        public bool Flag
        {
            get { return _Flag; }
            set { _Flag = value; }
        }

        #endregion
    }
}
