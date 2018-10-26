using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model 
{
    /// <summary>
    /// 考点表 
    /// </summary>
    public class Model_zk_kd
    {
        #region 字段

        /// <summary>
        /// 流水号
        /// </summary>
        private int _lsh = 0;
        /// <summary>
        /// 考点代码
        /// </summary>
        private string _kddm = "";
        /// <summary>
        /// 考点名称
        /// </summary>
        private string _kdmc = "";
        /// <summary>
        /// 考区代码
        /// </summary>
        private string _xqdm = "";
        /// <summary>
        /// 是否已选择毕业中学
        /// </summary>
        private int _isxx = 0;
        /// <summary>
        /// 是否已编排
        /// </summary>
        private int _isbp = 0;
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_kd() { }
        #endregion

        #region 属性

        /// <summary>
        /// 流水号
        /// </summary>
        public int Lsh
        {
            get { return _lsh; }
            set { _lsh = value; }
        }

        /// <summary>
        /// 考点代码
        /// </summary>
        public string Kddm
        {
            get { return _kddm; }
            set { _kddm = value; }
        }
        /// <summary>
        /// 考点名称
        /// </summary>
        public string Kdmc
        {
            get { return _kdmc; }
            set { _kdmc = value; }
        }
        /// <summary>
        /// 考区代码
        /// </summary>
        public string Xqdm
        {
            get { return _xqdm; }
            set { _xqdm = value; }
        }
        /// <summary>
        /// 是否已选择毕业中学
        /// </summary>
        public int Isxx
        {
            get { return _isxx; }
            set { _isxx = value; }
        }
        /// <summary>
        /// 是否已编排
        /// </summary>
        public int Isbp
        {
            get { return _isbp; }
            set { _isbp = value; }
        }
        #endregion


    }
}
