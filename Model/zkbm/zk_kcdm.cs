using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 考次代码
    /// </summary>
    public class Model_zk_kcdm
    {
        #region 字段
        /// <summary>
        /// 考次数据标识
        /// </summary>
        private int _kcId = 0;
        /// <summary>
        /// 考次代码(主键标识)。
        /// </summary>
        private string _kcdm = "";
        /// <summary>
        /// 考次名称。
        /// </summary>
        private string _kcmc = "";
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_kcdm() { }
        #endregion

        #region 属性
        /// <summary>
        /// 考次数据标识
        /// </summary>
        public int KcId
        {
            get { return _kcId; }
            set { _kcId = value; }
        }

        /// <summary>
        /// 考次代码(主键标识)。
        /// </summary>
        public string Kcdm
        {
            get { return _kcdm; }
            set { _kcdm = value; }
        }
        /// <summary>
        /// 考次名称。
        /// </summary>
        public string Kcmc
        {
            get { return _kcmc; }
            set { _kcmc = value; }
        }
        #endregion

    }

}
