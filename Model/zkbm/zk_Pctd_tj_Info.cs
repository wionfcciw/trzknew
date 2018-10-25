using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 批次投档条件。
    /// </summary>
    public class Model_zk_Pctd_tj_Info
    {
        #region 字段。
        /// <summary>
        /// 基本条件。
        /// </summary>
        private Model_zk_Pc_TouDang_jbtj _jbtj = new Model_zk_Pc_TouDang_jbtj();
        /// <summary>
        /// 同分跟进。
        /// </summary>
        private Model_zk_Pc_TouDang_tfgj _tfgj = new Model_zk_Pc_TouDang_tfgj();
        /// <summary>
        /// 指标生。
        /// </summary>
        private Model_zk_Pc_TouDang_zbs _zbs = new Model_zk_Pc_TouDang_zbs();
        /// <summary>
        /// 素质评价。
        /// </summary>
        private Model_zk_Pc_TouDang_szpj _szpj = new Model_zk_Pc_TouDang_szpj();
        /// <summary>
        /// 其他条件。
        /// </summary>
        private Model_zk_Pc_TouDang_qttj _qttj = new Model_zk_Pc_TouDang_qttj();
        /// <summary>
        /// 国际班控档线。
        /// </summary>
        private List<Model_zk_Pc_TouDang_gjbkdx> _gjbkdx = new List<Model_zk_Pc_TouDang_gjbkdx>();
        /// <summary>
        /// 标识当前对象是否有效(true、表示有效；false、无效)
        /// </summary>
        public bool bFlag = false;
        #endregion

        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_Pctd_tj_Info() { }
        #region 属性。
        /// <summary>
        /// 基本条件。
        /// </summary>
        public Model_zk_Pc_TouDang_jbtj Jbtj
        {
            get { return _jbtj; }
            set { _jbtj = value; }
        }
        /// <summary>
        /// 同分跟进。
        /// </summary>
        public Model_zk_Pc_TouDang_tfgj Tfgj
        {
            get { return _tfgj; }
            set { _tfgj = value; }
        }
        /// <summary>
        /// 指标生。
        /// </summary>
        public Model_zk_Pc_TouDang_zbs Zbs
        {
            get { return _zbs; }
            set { _zbs = value; }
        }
        /// <summary>
        /// 素质评价。
        /// </summary>
        public Model_zk_Pc_TouDang_szpj Szpj
        {
            get { return _szpj; }
            set { _szpj = value; }
        }
        /// <summary>
        /// 其他条件。
        /// </summary>
        public Model_zk_Pc_TouDang_qttj Qttj
        {
            get { return _qttj; }
            set { _qttj = value; }
        }
        /// <summary>
        /// 国际班控档线。
        /// </summary>
        public List<Model_zk_Pc_TouDang_gjbkdx> Gjbkdx
        {
            get { return _gjbkdx; }
            set { _gjbkdx = value; }
        }
        #endregion
    }
}
