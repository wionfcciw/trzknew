using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 大批次信息。
    /// </summary>
    public class Model_zk_zydz_dpcxx
    {
        #region 字段
        /// <summary>
        /// 大批次标识Id.
        /// </summary>
        private string dpcId = "";
        /// <summary>
        /// 县区代码(0表示所有县区使用一套定制)。
        /// </summary>
        private string _xqdm = "";
        /// <summary>
        /// 大批次ID(主键标识)
        /// </summary>
        private string _dpcDm = "";
        /// <summary>
        /// 大批次名称
        /// </summary>
        private string _dpcMc = "";
        /// <summary>
        /// 显示名称
        /// </summary>
        private string _dpcXsMc = "";
        /// <summary>
        /// 小批次数量
        /// </summary>
        private int _xpcSl = 0;
        /// <summary>
        /// 是否启用(0、未启用；、已启用)
        /// </summary>
        private bool _sfqy = false;
        /// <summary>
        /// 备注
        /// </summary>
        private string _dpcBz = "";
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_zydz_dpcxx() { }
        #endregion

        #region 属性
        /// <summary>
        /// 大批次标识Id.
        /// </summary>
        public string DpcId
        {
            get { return dpcId; }
            set { dpcId = value; }
        }
        /// <summary>
        /// 县区代码(0表示所有县区使用一套定制)。
        /// </summary>
        public string Xqdm
        {
            get { return _xqdm; }
            set { _xqdm = value; }
        }
        /// <summary>
        /// 大批次ID(主键标识)
        /// </summary>
        public string DpcDm
        {
            get { return _dpcDm; }
            set { _dpcDm = value; }
        }
        /// <summary>
        /// 大批次名称
        /// </summary>
        public string DpcMc
        {
            get { return _dpcMc; }
            set { _dpcMc = value; }
        }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DpcXsMc
        {
            get { return _dpcXsMc; }
            set { _dpcXsMc = value; }
        }
        /// <summary>
        /// 小批次数量
        /// </summary>
        public int XpcSl
        {
            get { return _xpcSl; }
            set { _xpcSl = value; }
        }
        /// <summary>
        /// 是否启用(0、未启用；、已启用)
        /// </summary>
        public bool Sfqy
        {
            get { return _sfqy; }
            set { _sfqy = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string DpcBz
        {
            get { return _dpcBz; }
            set { _dpcBz = value; }
        }
        private DateTime _stime;

        public DateTime Stime
        {
            get { return _stime; }
            set { _stime = value; }
        }
        private DateTime _etime;

        public DateTime Etime
        {
            get { return _etime; }
            set { _etime = value; }
        }
        #endregion

    }

}
