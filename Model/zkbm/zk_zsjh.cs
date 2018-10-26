using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// Model_zk_zsjh
    /// </summary>
    public class Model_zk_zsjh
    {
        #region 字段
        /// <summary>
        /// 流水号
        /// </summary>
        private int _lsh = 0;
        /// <summary>
        /// 招生区县代码。
        /// </summary>
        private string _xqdm = "";
        /// <summary>
        /// 学校代码。
        /// </summary>
        private string _xxdm = "";
        /// <summary>
        /// 专业代码
        /// </summary>
        private string _zydm = "";
        /// <summary>
        /// 学值代码
        /// </summary>
        private string _xzdm = "";
        /// <summary>
        /// 招生计划数。
        /// </summary>
        private int _jhs = 0;
        /// <summary>
        /// 批次代码。
        /// </summary>
        private string _pcdm = "";
        /// <summary>
        /// 学校类别代码
        /// </summary>
        private string _xxlbdm = "";
        /// <summary>
        /// -备注
        /// </summary>
        private string _bz = "";
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_zsjh() { }
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
        /// 招生区县代码。
        /// </summary>
        public string Xqdm
        {
            get { return _xqdm; }
            set { _xqdm = value; }
        }
        /// <summary>
        /// 学校代码。
        /// </summary>
        public string Xxdm
        {
            get { return _xxdm; }
            set { _xxdm = value; }
        }
        /// <summary>
        /// 专业代码
        /// </summary>
        public string Zydm
        {
            get { return _zydm; }
            set { _zydm = value; }
        }
        /// <summary>
        /// 学值代码
        /// </summary>
        public string Xzdm
        {
            get { return _xzdm; }
            set { _xzdm = value; }
        }
        /// <summary>
        /// 招生计划数。
        /// </summary>
        public int Jhs
        {
            get { return _jhs; }
            set { _jhs = value; }
        }
        /// <summary>
        /// 批次代码。
        /// </summary>
        public string Pcdm
        {
            get { return _pcdm; }
            set { _pcdm = value; }
        }
        /// <summary>
        /// 学校类别代码
        /// </summary>
        public string Xxlbdm
        {
            get { return _xxlbdm; }
            set { _xxlbdm = value; }
        }
        /// <summary>
        /// -备注
        /// </summary>
        public string Bz
        {
            get { return _bz; }
            set { _bz = value; }
        }
        #endregion

    }

}
