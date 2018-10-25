using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 区县代码
    /// </summary>
    public class Model_zk_xqdm
    {
        #region 字段
        /// <summary>
        /// 自动增长ID
        /// </summary>
        private int _qxId = 0;
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
        public Model_zk_xqdm() { }
        #endregion

        #region 属性
        /// <summary>
        /// 自动增长ID
        /// </summary>
        public int QxId
        {
            get { return _qxId; }
            set { _qxId = value; }
        }
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
