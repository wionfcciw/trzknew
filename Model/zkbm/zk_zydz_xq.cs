using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 志愿定制县区信息。
    /// </summary>
    public class Model_zk_zydz_zydzxq
    {
        #region 字段
        /// <summary>
        /// 区县代码(主键标识)。
        /// </summary>
        private string _xqdm = "";
        /// <summary>
        /// 区县名称。
        /// </summary>
        private string _xqmc = "";
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_zydz_zydzxq() { }
        #endregion

        #region 属性
        /// <summary>
        /// 区县代码(主键标识)。
        /// </summary>
        public string Xqdm
        {
            get { return _xqdm; }
            set { _xqdm = value; }
        }
        /// <summary>
        /// 区县名称。
        /// </summary>
        public string Xqmc
        {
            get { return _xqmc; }
            set { _xqmc = value; }
        }
        #endregion

    }

}
