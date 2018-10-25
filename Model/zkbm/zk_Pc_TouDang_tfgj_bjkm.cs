using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 批次投档条件（同分跟进(比较科目))
    /// </summary>
    public class Model_zk_Pc_TouDang_tfgj_bjkm
    {
        #region 字段
        /// <summary>
        /// 当前条件关联的小批次ID。
        /// </summary>
        private string _xpcId = "";
        /// <summary>
        /// 科目代码。
        /// </summary>
        private string _kmdm = "";
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_Pc_TouDang_tfgj_bjkm() { }
        #endregion

        #region 属性
        /// <summary>
        /// 当前条件关联的小批次ID。
        /// </summary>
        public string XpcId
        {
            get { return _xpcId; }
            set { _xpcId = value; }
        }
        /// <summary>
        /// 科目代码。
        /// </summary>
        public string Kmdm
        {
            get { return _kmdm; }
            set { _kmdm = value; }
        }
        #endregion

    }

}
