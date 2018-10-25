using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 招生学校代码
    /// </summary>
    public class Model_zk_zsxxdm
    {
        #region 字段
        /// <summary>
        /// 招生学校代码(主键标识)。
        /// </summary>
        private string _zsxxdm = "";
        /// <summary>
        /// 招生学校名称。
        /// </summary>
        private string _zsxxmc = "";
        /// <summary>
        /// 批次代码。
        /// </summary>
        private string _bz = "";
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_zsxxdm() { }
        #endregion

        #region 属性
        /// <summary>
        /// 招生学校代码(主键标识)。
        /// </summary>
        public string Zsxxdm
        {
            get { return _zsxxdm; }
            set { _zsxxdm = value; }
        }
        /// <summary>
        /// 招生学校名称。
        /// </summary>
        public string Zsxxmc
        {
            get { return _zsxxmc; }
            set { _zsxxmc = value; }
        }
        /// <summary>
        /// 批次代码。
        /// </summary>
        public string Bz
        {
            get { return _bz; }
            set { _bz = value; }
        }
        #endregion


    }

}
