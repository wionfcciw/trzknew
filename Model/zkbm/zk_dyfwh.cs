using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 打印防伪号(所填志愿打印)
    /// </summary>
    public class Model_zk_dyfwh
    {
        #region 字段
        /// <summary>
        /// 报名号(准考证号)
        /// </summary>
        private string _ksh = "";
        /// <summary>
        /// 打印标识，全球唯一标识。
        /// </summary>
        private string _dybs = "";
        /// <summary>
        /// 打印时间；
        /// </summary>
        private DateTime _dysj = DateTime.Now;
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_dyfwh() { }
        #endregion

        #region 属性
        /// <summary>
        /// 报名号(准考证号)
        /// </summary>
        public string Ksh
        {
            get { return _ksh; }
            set { _ksh = value; }
        }
        /// <summary>
        /// 打印标识，全球唯一标识。
        /// </summary>
        public string Dybs
        {
            get { return _dybs; }
            set { _dybs = value; }
        }
        /// <summary>
        /// 打印时间；
        /// </summary>
        public DateTime Dysj
        {
            get { return _dysj; }
            set { _dysj = value; }
        }
        #endregion

    }


}
