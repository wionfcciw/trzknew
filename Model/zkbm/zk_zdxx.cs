using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 字典信息表
    /// </summary>
    public class Model_zk_zdxx
    {
        #region 字段
        /// <summary>
        /// 字典类别代码(主键标识)。
        /// </summary>
        private string _zdlbdm = "";
        /// <summary>
        /// 字典名称。
        /// </summary>
        private string _zdlbmc = "";
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_zdxx() { }
        #endregion

        #region 属性
        /// <summary>
        /// 字典类别代码(主键标识)。
        /// </summary>
        public string Zdlbdm
        {
            get { return _zdlbdm; }
            set { _zdlbdm = value; }
        }
        /// <summary>
        /// 字典名称。
        /// </summary>
        public string Zdlbmc
        {
            get { return _zdlbmc; }
            set { _zdlbmc = value; }
        }
        #endregion

    }

}
