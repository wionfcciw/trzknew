using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 字典详细类别信息
    /// </summary>
    public class Model_zk_zdxxLB
    {
        #region 字段

        /// <summary>
        /// 流水号 
        /// </summary>
        private int _lsh = 0;

        /// <summary>
        /// 字典类别关联名称
        /// </summary>
        private string _zdlbdm = "";

        /// <summary>
        /// 字典详细类别代码 
        /// </summary>
        private string _zlbdm = "";
        /// <summary>
        /// 字典详细类别名称。
        /// </summary>
        private string _zlbmc = "";
        /// <summary>
        /// 字典详细类别的排序号
        /// </summary>
        private int _zlbpx = 0;
        /// <summary>
        /// 类别启用状态(0、未启用；、已启用)
        /// </summary>
        private int _zlbzt = 0;
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_zdxxLB() { }
        #endregion

        #region 属性

         
        /// <summary>
        /// 流水号(主键标识)。
        /// </summary>
        public int Lsh
        {
            get { return _lsh; }
            set { _lsh = value; }
        }

        /// <summary>
        /// 字典类别关联名称
        /// </summary>
        public string Zdlbdm
        {
            get { return _zdlbdm; }
            set { _zdlbdm = value; }
        }

        /// <summary>
        /// 字典详细类别代码
        /// </summary>
        public string Zlbdm
        {
            get { return _zlbdm; }
            set { _zlbdm = value; }
        }
        /// <summary>
        /// 字典详细类别名称。
        /// </summary>
        public string Zlbmc
        {
            get { return _zlbmc; }
            set { _zlbmc = value; }
        }
        /// <summary>
        /// 字典详细类别的排序号
        /// </summary>
        public int Zlbpx
        {
            get { return _zlbpx; }
            set { _zlbpx = value; }
        }
        /// <summary>
        /// 类别启用状态(0、未启用；、已启用)
        /// </summary>
        public int Zlbzt
        {
            get { return _zlbzt; }
            set { _zlbzt = value; }
        }
        #endregion

    }

}
