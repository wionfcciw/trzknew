using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 指标生信息表
    /// </summary>
    public class Model_zk_zbsxx
    {
        #region 字段
        /// <summary>
        /// 毕业学校代码
        /// </summary>
        private string _xxdm = "";
        /// <summary>
        /// 招生学校代码
        /// </summary>
        private string _zsxxdm = "";
        /// <summary>
        /// 指标生数量
        /// </summary>
        private int _zbssl = 0;
        /// <summary>
        /// 批次代码
        /// </summary>
        private string _pcdm = "";
        /// <summary>
        /// 招生县区代码
        /// </summary>
        private string _xqdm = "";
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_zbsxx() { }
        #endregion

        #region 属性
        /// <summary>
        /// 毕业学校代码
        /// </summary>
        public string Xxdm
        {
            get { return _xxdm; }
            set { _xxdm = value; }
        }
        /// <summary>
        /// 招生学校代码
        /// </summary>
        public string Zsxxdm
        {
            get { return _zsxxdm; }
            set { _zsxxdm = value; }
        }
        /// <summary>
        /// 指标生数量
        /// </summary>
        public int Zbssl
        {
            get { return _zbssl; }
            set { _zbssl = value; }
        }
        /// <summary>
        /// 批次代码
        /// </summary>
        public string Pcdm
        {
            get { return _pcdm; }
            set { _pcdm = value; }
        }
        /// <summary>
        /// 招生县区代码
        /// </summary>
        public string Xqdm
        {
            get { return _xqdm; }
            set { _xqdm = value; }
        }
        #endregion

    }

}
