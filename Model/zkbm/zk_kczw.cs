using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model 
{
    /// <summary>
    /// 考场座位表
    /// </summary>
    public class Model_zk_kczw
    {
        #region 字段
        /// <summary>
        /// 报名号
        /// </summary>
        private string _ksh = "";
        /// <summary>
        /// 考试号
        /// </summary>
        private string _zkzh = "";
        /// <summary>
        /// 考场代码
        /// </summary>
        private string _kcdm = "";
        /// <summary>
        /// 座位号
        /// </summary>
        private string _zwh = "";
        /// <summary>
        /// 考点代码
        /// </summary>
        private string _kddm = "";
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_kczw() { }
        #endregion

        #region 属性
        /// <summary>
        /// 报名号
        /// </summary>
        public string Ksh
        {
            get { return _ksh; }
            set { _ksh = value; }
        }
        /// <summary>
        /// 考试号
        /// </summary>
        public string Zkzh
        {
            get { return _zkzh; }
            set { _zkzh = value; }
        }
        /// <summary>
        /// 考场代码
        /// </summary>
        public string Kcdm
        {
            get { return _kcdm; }
            set { _kcdm = value; }
        }
        /// <summary>
        /// 座位号
        /// </summary>
        public string Zwh
        {
            get { return _zwh; }
            set { _zwh = value; }
        }
        /// <summary>
        /// 考点代码
        /// </summary>
        public string Kddm
        {
            get { return _kddm; }
            set { _kddm = value; }
        }
        #endregion

    }
}
