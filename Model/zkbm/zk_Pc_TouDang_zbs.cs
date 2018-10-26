using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 批次投档条件（指标生)
    /// </summary>
    public class Model_zk_Pc_TouDang_zbs
    {
        #region 字段
        /// <summary>
        /// 当前条件关联的小批次ID。
        /// </summary>
        private string _xpcId = "";
        /// <summary>
        /// 小批次代码加上大批次代码。
        /// </summary>
        private string _pcdm = "";
        /// <summary>
        /// 有无指标生：0、有指标生；1、无指标生
        /// </summary>
        private int _ywzbs = -1;
        /// <summary>
        /// 指标生录取分数限制(0、无分数限制；1、不小于统招线下多少分)
        /// </summary>
        private int _zbslqfsxz = -1;
        /// <summary>
        /// 自定义不小于统招线下的分数(当录取分数限制条件为时有效)。
        /// </summary>
        private decimal _zdyfs = 0;
        /// <summary>
        /// 剩余指标生处理条件(0、无剩余指标生；1、可用其他中学生剩余指标)
        /// </summary>
        private int _syzbscl = -1;
        /// <summary>
        /// 标识当前对象是否有效(true、表示有效；false、无效)
        /// </summary>
        public bool bFlag = false;
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_Pc_TouDang_zbs() { }
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
        /// 小批次代码加上大批次代码。
        /// </summary>
        public string Pcdm
        {
            get { return _pcdm; }
            set { _pcdm = value; }
        }
        /// <summary>
        /// 有无指标生：0、有指标生；1、无指标生
        /// </summary>
        public int Ywzbs
        {
            get { return _ywzbs; }
            set { _ywzbs = value; }
        }
        /// <summary>
        /// 指标生录取分数限制(0、无分数限制；1、不小于统招线下多少分)
        /// </summary>
        public int Zbslqfsxz
        {
            get { return _zbslqfsxz; }
            set { _zbslqfsxz = value; }
        }
        /// <summary>
        /// 自定义不小于统招线下的分数(当录取分数限制条件为时有效)。
        /// </summary>
        public decimal Zdyfs
        {
            get { return _zdyfs; }
            set { _zdyfs = value; }
        }
        /// <summary>
        /// 剩余指标生处理条件(0、无剩余指标生；1、可用其他中学生剩余指标)
        /// </summary>
        public int Syzbscl
        {
            get { return _syzbscl; }
            set { _syzbscl = value; }
        }
        #endregion

    }

}
