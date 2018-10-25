using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 考生中考成绩
    /// </summary>
    public class Model_zk_kszkcj
    {
        #region 字段
        /// <summary>
        /// 报名号
        /// </summary>
        private string _ksh = "";
        /// <summary>
        /// 科目代码
        /// </summary>
        private string _kmdm = "";
        /// <summary>
        /// 成绩一位小数点
        /// </summary>
        private decimal _cj = 0;
        /// <summary>
        /// 状态0未录取录取(录取的时候用到)
        /// </summary>
        private int _state = 0;

        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_kszkcj() { }
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
        /// 科目代码
        /// </summary>
        public string Kmdm
        {
            get { return _kmdm; }
            set { _kmdm = value; }
        }
        /// <summary>
        /// 成绩一位小数点
        /// </summary>
        public decimal Cj
        {
            get { return _cj; }
            set { _cj = value; }
        }
        /// <summary>
        /// 状态0未录取录取(录取的时候用到)
        /// </summary>
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }
        #endregion

    }

}
