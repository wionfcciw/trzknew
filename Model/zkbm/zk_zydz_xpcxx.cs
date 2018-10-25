using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{	/// <summary>
    /// 小批次的信息。
    /// </summary>
    public class Model_zk_zydz_xpcxx
    {
        #region 字段
        /// <summary>
        /// 主键标识。
        /// </summary>
        private string _xpcId = "";
        /// <summary>
        /// 批次代码(大批次代码加上小批次代码)
        /// </summary>
        private string _pcDm = "";
        /// <summary>
        /// 小批次ID(主键标识)
        /// </summary>
        private string _xpcDm = "";
        /// <summary>
        /// 大批次信息表ID(主键标识)
        /// </summary>
        private string _dpcDm = "";
        /// <summary>
        /// 小批次名称
        /// </summary>
        private string _xpcMc = "";
        /// <summary>
        /// 显示名称
        /// </summary>
        private string _xpcXsMc = "";
        /// <summary>
        /// 志愿数量
        /// </summary>
        private int _zySl = 0;
        /// <summary>
        /// 学校服从
        /// </summary>
        private string _xxFc = "";
        /// <summary>
        /// 是否启用(0、未启用；1、已启用)
        /// </summary>
        private bool _sfqy = false;
        /// <summary>
        /// 普高批次(0、否；1、是)
        /// </summary>
        private bool _pgPc = false;
        /// <summary>
        /// 批次类别(1、统招；2、择校；3、国际班；4、其它)
        /// </summary>
        private int _pcLb = 0;
        /// <summary>
        /// 批次类别(1、统招；2、择校；3、国际班；4、其它)
        /// </summary>
        private string _pcLbName = "";
        /// <summary>
        /// 备注
        /// </summary>
        private string _xpcBz = "";
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_zydz_xpcxx() { }
        #endregion

        #region 属性
        /// <summary>
        /// 主键标识(主键标识)。
        /// </summary>
        public string XpcId
        {
            get { return _xpcId; }
            set { _xpcId = value; }
        }
        /// <summary>
        /// 批次代码(大批次代码加上小批次代码)
        /// </summary>
        public string PcDm
        {
            get { return _pcDm; }
            set { _pcDm = value; }
        }
        /// <summary>
        /// 小批次代码
        /// </summary>
        public string XpcDm
        {
            get { return _xpcDm; }
            set { _xpcDm = value; }
        }
        /// <summary>
        /// 大批次信息表ID(主键标识)
        /// </summary>
        public string DpcDm
        {
            get { return _dpcDm; }
            set { _dpcDm = value; }
        }
        /// <summary>
        /// 小批次名称
        /// </summary>
        public string XpcMc
        {
            get { return _xpcMc; }
            set { _xpcMc = value; }
        }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string XpcXsMc
        {
            get { return _xpcXsMc; }
            set { _xpcXsMc = value; }
        }
        /// <summary>
        /// 志愿数量
        /// </summary>
        public int ZySl
        {
            get { return _zySl; }
            set { _zySl = value; }
        }
        /// <summary>
        /// 学校服从
        /// </summary>
        public string XxFc
        {
            get { return _xxFc; }
            set { _xxFc = value; }
        }
        /// <summary>
        /// 是否启用(0、未启用；1、已启用)
        /// </summary>
        public bool Sfqy
        {
            get { return _sfqy; }
            set { _sfqy = value; }
        }
        /// <summary>
        /// 普高批次(0、否；1、是)
        /// </summary>
        public bool PgPc
        {
            get { return _pgPc; }
            set { _pgPc = value; }
        }
        /// <summary>
        /// 批次类别(1、统招；2、择校；3、国际班；4、其它)
        /// </summary>
        public int PcLb
        {
            get { return _pcLb; }
            set { _pcLb = value; }
        }
        /// <summary>
        /// 批次类别(1、统招；2、择校；3、国际班；4、其它)
        /// </summary>
        public string PcLbName
        {
            get { return _pcLbName; }
            set { _pcLbName = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string XpcBz
        {
            get { return _xpcBz; }
            set { _xpcBz = value; }
        }
        #endregion

    }

}
