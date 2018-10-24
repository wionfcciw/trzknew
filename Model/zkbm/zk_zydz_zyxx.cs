using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 批次志愿信息。
    /// </summary>
    public class Model_zk_zydz_zyxx
    {
        #region 字段
        /// <summary>
        /// 志愿ID。
        /// </summary>
        private string _zyId = "";
        /// <summary>
        /// 志愿ID(主键标识)
        /// </summary>
        private string _zyDm = "";
        /// <summary>
        /// 小批次信息表(主键标识)
        /// </summary>
        private string _xpcDm = "";
        /// <summary>
        /// 志愿名称
        /// </summary>
        private string _zyMc = "";
        /// <summary>
        /// 显示名称
        /// </summary>
        private string _zyXsmc = "";
        /// <summary>
        /// 专业数量
        /// </summary>
        private int _zySl = 0;

        private bool _sfxxfc = false;

        public bool Sfxxfc
        {
            get { return _sfxxfc; }
            set { _sfxxfc = value; }
        }
        /// <summary>
        /// 有无专业服从(0、无；1、有)
        /// </summary>
        private bool _sfZyFc = false;

        public bool SfZyFc
        {
            get { return _sfZyFc; }
            set { _sfZyFc = value; }
        }
        /// <summary>
        /// 是否启用(0、未启用；1、已启用)
        /// </summary>
        private bool _sfqy = false;
        /// <summary>
        /// 备注。
        /// </summary>
        private string _zyBz = "";
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_zydz_zyxx() { }
        #endregion

        #region 属性
        /// <summary>
        /// 志愿ID。
        /// </summary>
        public string ZyId
        {
            get { return _zyId; }
            set { _zyId = value; }
        }
        /// <summary>
        /// 志愿ID(主键标识)
        /// </summary>
        public string ZyDm
        {
            get { return _zyDm; }
            set { _zyDm = value; }
        }
        /// <summary>
        /// 小批次信息表(主键标识)
        /// </summary>
        public string XpcDm
        {
            get { return _xpcDm; }
            set { _xpcDm = value; }
        }
        /// <summary>
        /// 志愿名称
        /// </summary>
        public string ZyMc
        {
            get { return _zyMc; }
            set { _zyMc = value; }
        }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string ZyXsmc
        {
            get { return _zyXsmc; }
            set { _zyXsmc = value; }
        }
        /// <summary>
        /// 专业数量
        /// </summary>
        public int ZySl
        {
            get { return _zySl; }
            set { _zySl = value; }
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
        /// 备注。
        /// </summary>
        public string ZyBz
        {
            get { return _zyBz; }
            set { _zyBz = value; }
        }
        #endregion

    }

}
