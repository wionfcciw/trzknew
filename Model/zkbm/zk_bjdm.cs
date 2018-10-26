using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 班级代码
    /// </summary>
    public class Model_zk_bjdm
    {
        #region 字段

        /// <summary>
        /// 自动增长ID
        /// </summary>
        private int _lsh = 0;

        /// <summary>
        /// 县区代码
        /// </summary>
        private string _xqdm = "";

        /// <summary>
        /// 学校代码
        /// </summary>
        private string _xxdm = "";

        /// <summary>
        /// 班级代码 
        /// </summary>
        private string _bjdm = "";     
        /// <summary>
        /// 班级名称。
        /// </summary>
        private string _bjmc = "";
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_bjdm() { }
        #endregion

        #region 属性

        /// <summary>
        /// 自动增长ID
        /// </summary>
        public int Lsh
        {
            get { return _lsh; }
            set { _lsh = value; }
        }

        /// <summary>
        ///  县区代码
        /// </summary>
        public string xqdm
        {
            get { return _xqdm; }
            set { _xqdm = value; }
        }
        /// <summary>
        /// 学校代码
        /// </summary>
        public string xxdm
        {
            get { return _xxdm; }
            set { _xxdm = value; }
        }

        /// <summary>
        /// 班级代码(主键标识)。
        /// </summary>
        public string Bjdm
        {
            get { return _bjdm; }
            set { _bjdm = value; }
        }
       
        /// <summary>
        /// 班级名称。
        /// </summary>
        public string Bjmc
        {
            get { return _bjmc; }
            set { _bjmc = value; }
        }
        #endregion

    }

}
