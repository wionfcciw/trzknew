using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 批次投档条件（素质评价)
    /// </summary>
    public class Model_zk_Pc_TouDang_szpj
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
        /// 综合素质评价限制条件(0、不限制录取；1、录取要满足指定的评价等级)(值为时，后面个字段有效)
        /// </summary>
        private int _zhszxztj = 0;
        /// <summary>
        /// 条件类型(0、大于；1、小于；2、等于[=]；3、不等于[!=]；4、大于等于[>=]；5、小于等于[<=])
        /// </summary>
        private int _tjlx = -1;
        /// <summary>
        /// 数量(几个)。
        /// </summary>
        private int _sl = -1;
        /// <summary>
        /// A、B、C、D、
        /// </summary>
        private string _pjdenji = "";
        /// <summary>
        /// 标识当前对象是否有效(true、表示有效；false、无效)
        /// </summary>
        public bool bFlag = false;
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_Pc_TouDang_szpj() { }
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
        /// 综合素质评价限制条件(0、不限制录取；1、录取要满足指定的评价等级)(值为时，后面个字段有效)
        /// </summary>
        public int Zhszxztj
        {
            get { return _zhszxztj; }
            set { _zhszxztj = value; }
        }
        /// <summary>
        /// 条件类型(0、大于；1、小于；2、等于[=]；3、不等于[!=]；4、大于等于[>=]；5、小于等于)
        /// </summary>
        public int Tjlx
        {
            get { return _tjlx; }
            set { _tjlx = value; }
        }
        /// <summary>
        /// 数量(几个)。
        /// </summary>
        public int Sl
        {
            get { return _sl; }
            set { _sl = value; }
        }
        /// <summary>
        /// A、B、C、D、
        /// </summary>
        public string Pjdenji
        {
            get { return _pjdenji; }
            set { _pjdenji = value; }
        }
        #endregion

    }

}
