using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// Model_zk_byxxdm
    /// </summary>
    public class Model_zk_xxdm
    {
        #region 字段
        /// <summary>
        /// 毕业学校代码(主键标识)。
        /// </summary>
        private string _xxdm = "";
        /// <summary>
        /// 引用(区县代码信息表)的主键标识。
        /// </summary>
        private string _xqdm = "";
        /// <summary>
        /// 毕业学校名称。
        /// </summary>
        private string _xxmc = "";
        /// <summary>
        /// 学校类型代码
        /// </summary>
        private string xxlxdm;
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_xxdm() { }
        #endregion

        #region 属性
        /// <summary>
        /// 毕业学校代码(主键标识)。
        /// </summary>
        public string Xxdm
        {
            get { return _xxdm; }
            set { _xxdm = value; }
        }
        /// <summary>
        /// 引用(区县代码信息表)的主键标识。
        /// </summary>
        public string Xqdm
        {
            get { return _xqdm; }
            set { _xqdm = value; }
        }
        /// <summary>
        /// 毕业学校名称。
        /// </summary>
        public string Xxmc
        {
            get { return _xxmc; }
            set { _xxmc = value; }
        }
       
        /// <summary>
        /// 学校类型代码
        /// </summary>
        public string Xxlxdm
        {
            get { return xxlxdm; }
            set { xxlxdm = value; }
        }
        #endregion

    }

}
