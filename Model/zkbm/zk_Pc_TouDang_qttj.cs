using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 批次投档条件（其他条件)
    /// </summary>
    public class Model_zk_Pc_TouDang_qttj
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
        /// 加试成绩合格限制(0、艺术类；1、师范类；2、免费师范(男生)类；3、不限制)
        /// </summary>
        private int _jscjxzif = -1;
        /// <summary>
        /// 与文化成绩合并的加试成绩(0、艺术类；1、师范类；2、免费师范(男生)类；3、不限制)
        /// </summary>
        private int _ywhcjhb_jscj = -1;
        /// <summary>
        /// 会考成绩合格限制条件(0、会考成绩必须合格；1、不限制)
        /// </summary>
        private int _hkcjhgXz = -1;
        /// <summary>
        /// 性别限制条件(0、男女不限；1、必须是男生；2、必须是女生)
        /// </summary>
        private int _xbxz = -1;
        /// <summary>
        /// 标识当前对象是否有效(true、表示有效；false、无效)
        /// </summary>
        public bool bFlag = false; 
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_Pc_TouDang_qttj() { }
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
        /// 加试成绩合格限制(0、艺术类；1、师范类；2、免费师范(男生)类；3、不限制)
        /// </summary>
        public int Jscjxzif
        {
            get { return _jscjxzif; }
            set { _jscjxzif = value; }
        }
        /// <summary>
        /// 与文化成绩合并的加试成绩(0、艺术类；1、师范类；2、免费师范(男生)类；3、不限制)
        /// </summary>
        public int Ywhcjhb_jscj
        {
            get { return _ywhcjhb_jscj; }
            set { _ywhcjhb_jscj = value; }
        }
        /// <summary>
        /// 会考成绩合格限制条件(0、会考成绩必须合格；1、不限制)
        /// </summary>
        public int HkcjhgXz
        {
            get { return _hkcjhgXz; }
            set { _hkcjhgXz = value; }
        }
        /// <summary>
        /// 性别限制条件(0、男女不限；1、必须是男生；2、必须是女生)
        /// </summary>
        public int Xbxz
        {
            get { return _xbxz; }
            set { _xbxz = value; }
        }
        #endregion

    }

}
