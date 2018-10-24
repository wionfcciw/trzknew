using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{	    
    /// <summary>
    /// 考生会考成绩
    /// </summary>
    public class Model_zk_kshkcj
    {
        #region 字段
        /// <summary>
        /// 
        /// </summary>
        private string _ksh = "";
        /// <summary>
        /// 地理等级
        /// </summary>
        private string _Dldj = "";
        /// <summary>
        /// 生物等级
        /// </summary>
        private string _Swdj = "";
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_kshkcj() { }
        #endregion

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string Ksh
        {
            get { return _ksh; }
            set { _ksh = value; }
        }
        /// <summary>
        /// 地理等级
        /// </summary>
        public string Dldj
        {
            get { return _Dldj; }
            set { _Dldj = value; }
        }
        /// <summary>
        /// 生物等级
        /// </summary>
        public string Swdj
        {
            get { return _Swdj; }
            set { _Swdj = value; }
        }
        #endregion

    }

}
