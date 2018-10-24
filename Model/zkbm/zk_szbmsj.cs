using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 设置报名时间
    /// </summary>
    public class Model_zk_szbmsj
    {
        #region 字段
        /// <summary>
        /// 流水号
        /// </summary>
        private int _lsh = 0;
        /// <summary>
        /// 引用(区县代码信息表)的主键标识。
        /// </summary>
        private string _xqdm = "";
        /// <summary>
        /// 毕业中学代码(主键标识)。
        /// </summary>
        private string _bmddm = "";
        /// <summary>
        /// 开始时间
        /// </summary>
        private DateTime _kssj = DateTime.Now;
        /// <summary>
        /// -结结束时间
        /// </summary>
        private DateTime _jssj = DateTime.Now;

        /// <summary>
        /// 开始时间_志愿
        /// </summary>
        private DateTime _kssj_zy = DateTime.Now;
        /// <summary>
        /// -结结束时间_志愿
        /// </summary>
        private DateTime _jssj_zy = DateTime.Now;

        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_szbmsj() { }
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
        /// 引用(区县代码信息表)的主键标识。
        /// </summary>
        public string Xqdm
        {
            get { return _xqdm; }
            set { _xqdm = value; }
        }
        /// <summary>
        /// 毕业中学代码(主键标识)。
        /// </summary>
        public string Bmddm
        {
            get { return _bmddm; }
            set { _bmddm = value; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime Kssj
        {
            get { return _kssj; }
            set { _kssj = value; }
        }
        /// <summary>
        /// -结结束时间
        /// </summary>
        public DateTime Jssj
        {
            get { return _jssj; }
            set { _jssj = value; }
        }

        /// <summary>
        /// 开始时间_志愿
        /// </summary>
        public DateTime Kssj_zy
        {
            get { return _kssj_zy; }
            set { _kssj_zy = value; }
        }
        /// <summary>
        /// -结结束时间_志愿
        /// </summary>
        public DateTime Jssj_zy
        {
            get { return _jssj_zy; }
            set { _jssj_zy = value; }
        }

        #endregion
        private DateTime _kssj_ty = DateTime.Now;

        public DateTime Kssj_ty
        {
            get { return _kssj_ty; }
            set { _kssj_ty = value; }
        }
        private DateTime _jssj_ty = DateTime.Now;

        public DateTime Jssj_ty
        {
            get { return _jssj_ty; }
            set { _jssj_ty = value; }
        }
        private DateTime _kssj_js = DateTime.Now;

        public DateTime Kssj_js
        {
            get { return _kssj_js; }
            set { _kssj_js = value; }
        }
        private DateTime _jssj_js = DateTime.Now;

        public DateTime Jssj_js
        {
            get { return _jssj_js; }
            set { _jssj_js = value; }
        }
        private DateTime _kssj_sb = DateTime.Now;

        public DateTime Kssj_sb
        {
            get { return _kssj_sb; }
            set { _kssj_sb = value; }
        }

        private DateTime _jssj_sb = DateTime.Now;

        public DateTime Jssj_sb
        {
            get { return _jssj_sb; }
            set { _jssj_sb = value; }
        }

    }


}
