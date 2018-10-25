using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 批次投档条件（基本条件)
    /// </summary>
    public class Model_zk_Pc_TouDang_jbtj
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
        /// 投档算法(0、平行志愿算法；1、志愿优先算法)。
        /// </summary>
        private int _tdsf = -1;
        /// <summary>
        /// 最低控档线(0、普高最低控档线有效；1、普高最低点控档线无效；2、指定最低分数)
        /// </summary>
        private int _zdkdx = -1;
        /// <summary>
        /// 自定义最低分数。
        /// </summary>
        private decimal _zdy_zdfs = 0;
        /// <summary>
        /// 是否自动发档(0、自动发档；1、手动发档)
        /// </summary>
        private int _sfZdfd = -1;
        /// <summary>
        /// 同一批次同一招生学校在各县区的计划数是否相同(0、是；1、否)(招生计划数是否相同)。
        /// </summary>
        private int _zsjhssfxt = -1;
        /// <summary>
        /// 标识当前对象是否有效(true、表示有效；false、无效)
        /// </summary>
        public bool bFlag = false;
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_Pc_TouDang_jbtj() { }
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
        /// 投档算法(0、平行志愿算法；1、志愿优先算法)。
        /// </summary>
        public int Tdsf
        {
            get { return _tdsf; }
            set { _tdsf = value; }
        }
        /// <summary>
        /// 最低控档线(0、普高最低控档线有效；1、普高最低点控档线无效；2、指定最低分数)
        /// </summary>
        public int Zdkdx
        {
            get { return _zdkdx; }
            set { _zdkdx = value; }
        }
        /// <summary>
        /// 自定义最低分数。
        /// </summary>
        public decimal Zdy_zdfs
        {
            get { return _zdy_zdfs; }
            set { _zdy_zdfs = value; }
        }
        /// <summary>
        /// 是否自动发档(0、自动发档；1、手动发档)
        /// </summary>
        public int SfZdfd
        {
            get { return _sfZdfd; }
            set { _sfZdfd = value; }
        }
        /// <summary>
        /// 同一批次同一招生学校在各县区的计划数是否相同(0、是；1、否)(招生计划数是否相同)。
        /// </summary>
        public int Zsjhssfxt
        {
            get { return _zsjhssfxt; }
            set { _zsjhssfxt = value; }
        }
        #endregion

    }

}
