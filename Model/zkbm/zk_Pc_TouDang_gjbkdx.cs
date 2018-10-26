using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 批次投档条件（国际班控档线），当批次类别时“国际班”时有效
    /// </summary>
    public class Model_zk_Pc_TouDang_gjbkdx
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
        /// 学校代码。
        /// </summary>
        private string _xxdm = "";
        /// <summary>
        /// 控档线分数。
        /// </summary>
        private decimal _kdxfs = 0;
        /// <summary>
        /// 标识当前对象是否有效(true、表示有效；false、无效)
        /// </summary>
        public bool bFlag = false;
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_Pc_TouDang_gjbkdx() { }
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
        /// 学校代码。
        /// </summary>
        public string Xxdm
        {
            get { return _xxdm; }
            set { _xxdm = value; }
        }
        /// <summary>
        /// 控档线分数。
        /// </summary>
        public decimal Kdxfs
        {
            get { return _kdxfs; }
            set { _kdxfs = value; }
        }
        #endregion

    }

}
