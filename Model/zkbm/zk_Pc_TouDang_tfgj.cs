using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 批次投档条件（同分跟进)
    /// </summary>
    public class Model_zk_Pc_TouDang_tfgj
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
        /// 是否同分跟进(0、同分不跟进；1、同分直接跟进；2、同分时比较各科成绩；)
        /// </summary>
        private int _sftfgj = -1;
        /// <summary>
        /// 比较科目；注：当“是否同分跟进”的条件是‘同分时比较各科成绩时有效’
        /// </summary>
        private List<Model_zk_Pc_TouDang_tfgj_bjkm> _bjkms = new List<Model_zk_Pc_TouDang_tfgj_bjkm>();
        /// <summary>
        /// 标识当前对象是否有效(true、表示有效；false、无效)
        /// </summary>
        public bool bFlag = false;
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_Pc_TouDang_tfgj() { }
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
        /// 是否同分跟进(0、同分不跟进；1、同分直接跟进；2、同分时比较各科成绩；)
        /// </summary>
        public int Sftfgj
        {
            get { return _sftfgj; }
            set { _sftfgj = value; }
        }
        /// <summary>
        /// 比较科目；注：当“是否同分跟进”的条件是‘同分时比较各科成绩时有效’
        /// </summary>
        public List<Model_zk_Pc_TouDang_tfgj_bjkm> Bjkms
        {
            get { return _bjkms; }
            set { _bjkms = value; }
        }
        #endregion

    }

}
