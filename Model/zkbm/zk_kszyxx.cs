using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 考生志愿信息表
    /// </summary>
    public class Model_zk_kszyxx
    {

        #region 字段
        private bool _sfxxfc = false;

        public bool Sfxxfc
        {
            get { return _sfxxfc; }
            set { _sfxxfc = value; }
        }
        /// <summary>
        /// 流水号
        /// </summary>
        private int _lsh = 0;
        /// <summary>
        /// 报名号
        /// </summary>
        private string _ksh = "";
        /// <summary>
        /// 志愿顺序。
        /// </summary>
        private int _zysx = 0;
        /// <summary>
        /// 学制代码。
        /// </summary>
        private string _xzdm = "";
        /// <summary>
        /// 批次代码。
        /// </summary>
        private string _pcdm = "";
        /// <summary>
        /// 学校代码。
        /// </summary>
        private string _xxdm = "";
        /// <summary>
        /// 专业
        /// </summary>
        private string _zy1 = "";
        /// <summary>
        /// 专业
        /// </summary>
        private string _zy2 = "";
        /// <summary>
        /// 专业
        /// </summary>
        private string _zy3 = "";
        /// <summary>
        /// 专业
        /// </summary>
        private string _zy4 = "";
        /// <summary>
        /// 专业
        /// </summary>
        private string _zy5 = "";
        /// <summary>
        /// 专业
        /// </summary>
        private string _zy6 = "";
        /// <summary>
        /// 专业
        /// </summary>
        private string _zy7 = "";
        /// <summary>
        /// 专业服从。是0否
        /// </summary>
        private bool _zyfc = false;
        /// <summary>
        /// 学校服从。1是0否
        /// </summary>
        private bool _xxfc = false;
        /// <summary>
        /// 录入时间。
        /// </summary>
        private DateTime _lrsj = DateTime.Now;
        /// <summary>
        /// 是否报考。否1是
        /// </summary>
        private bool _sfbk = false;
        /// <summary>
        /// 标识
        /// </summary>
        private string _kjbs = "";
        #endregion
        private string _xqdm = "";

        public string Xqdm
        {
            get { return _xqdm; }
            set { _xqdm = value; }
        }
        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_kszyxx() { }
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
        /// 报名号
        /// </summary>
        public string Ksh
        {
            get { return _ksh; }
            set { _ksh = value; }
        }
        /// <summary>
        /// 志愿顺序。
        /// </summary>
        public int Zysx
        {
            get { return _zysx; }
            set { _zysx = value; }
        }
        /// <summary>
        /// 学制代码。
        /// </summary>
        public string Xzdm
        {
            get { return _xzdm; }
            set { _xzdm = value; }
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
        /// 学校代码。
        /// </summary>
        public string Xxdm
        {
            get { return _xxdm; }
            set { _xxdm = value; }
        }
        /// <summary>
        /// 专业
        /// </summary>
        public string Zy1
        {
            get { return _zy1; }
            set { _zy1 = value; }
        }
        /// <summary>
        /// 专业
        /// </summary>
        public string Zy2
        {
            get { return _zy2; }
            set { _zy2 = value; }
        }
        /// <summary>
        /// 专业
        /// </summary>
        public string Zy3
        {
            get { return _zy3; }
            set { _zy3 = value; }
        }
        /// <summary>
        /// 专业
        /// </summary>
        public string Zy4
        {
            get { return _zy4; }
            set { _zy4 = value; }
        }
        /// <summary>
        /// 专业
        /// </summary>
        public string Zy5
        {
            get { return _zy5; }
            set { _zy5 = value; }
        }
        /// <summary>
        /// 专业
        /// </summary>
        public string Zy6
        {
            get { return _zy6; }
            set { _zy6 = value; }
        }
        /// <summary>
        /// 专业
        /// </summary>
        public string Zy7
        {
            get { return _zy7; }
            set { _zy7 = value; }
        }
        /// <summary>
        /// 专业服从。是0否
        /// </summary>
        public bool Zyfc
        {
            get { return _zyfc; }
            set { _zyfc = value; }
        }
        /// <summary>
        /// 学校服从。1是0否
        /// </summary>
        public bool Xxfc
        {
            get { return _xxfc; }
            set { _xxfc = value; }
        }
        /// <summary>
        /// 录入时间。
        /// </summary>
        public DateTime Lrsj
        {
            get { return _lrsj; }
            set { _lrsj = value; }
        }
        /// <summary>
        /// 是否报考。否1是
        /// </summary>
        public bool Sfbk
        {
            get { return _sfbk; }
            set { _sfbk = value; }
        }
        /// <summary>
        /// 标识
        /// </summary>
        public string Kjbs
        {
            get { return _kjbs; }
            set { _kjbs = value; }
        }
        #endregion


    }

}
