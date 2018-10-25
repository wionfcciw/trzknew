using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 专业库
    /// </summary>
    public class Model_zk_zyk
    {
        #region 字段
        /// <summary>
        /// 专业代码
        /// </summary>
        private string _zydm = "";
        /// <summary>
        /// 专业名称
        /// </summary>
        private string _zymc = "";
        /// <summary>
        /// 学校代码
        /// </summary>
        private string _xxdm = "";
        private string _bz = "";
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_zyk() { }
        #endregion

        #region 属性
        /// <summary>
        /// 专业代码
        /// </summary>
        public string Zydm
        {
            get { return _zydm; }
            set { _zydm = value; }
        }
        /// <summary>
        /// 专业名称
        /// </summary>
        public string Zymc
        {
            get { return _zymc; }
            set { _zymc = value; }
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
        /// 备注
        /// </summary>
        public string Bz
        {
            get { return _bz; }
            set { _bz = value; }
        }
        #endregion

    }

}
