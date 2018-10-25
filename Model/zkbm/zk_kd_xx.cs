using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model 
{

    /// <summary>
    /// 考点学校表 
    /// </summary>
    public class Model_zk_kd_xx
    {
        #region 字段
        /// <summary>
        /// 考点代码
        /// </summary>
        private string _kddm = "";
        /// <summary>
        /// 学校代码
        /// </summary>
        private string _xxdm = "";
        private string _bksh = "";
        private string _eksh = "";
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_kd_xx() { }
        #endregion

        #region 属性
        /// <summary>
        /// 考点代码
        /// </summary>
        public string Kddm
        {
            get { return _kddm; }
            set { _kddm = value; }
        }
        /// <summary>
        /// 学校代码
        /// </summary>
        public string Xxdm
        {
            get { return _xxdm; }
            set { _xxdm = value; }
        }
        /// <summary>
        /// 开始报名号
        /// </summary>
        public string Bksh
        {
            get { return _bksh; }
            set { _bksh = value; }
        }
        /// <summary>
        /// 结束报名号
        /// </summary>
        public string Eksh
        {
            get { return _eksh; }
            set { _eksh = value; }
        }
        #endregion

    }
}
