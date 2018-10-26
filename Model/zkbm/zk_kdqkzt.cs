using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// Model_zk_kdqkzt
    /// </summary>
    public class Model_zk_kdqkzt
    {
        #region 字段
        /// <summary>
        /// 
        /// </summary>
        private int _ID = 0;
        /// <summary>
        /// 
        /// </summary>
        private string _kddm = "";
        /// <summary>
        /// 
        /// </summary>
        private string _kmdm = "";
        /// <summary>
        /// 
        /// </summary>
        private int _type = 0;
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_kdqkzt() { }
        #endregion

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Kddm
        {
            get { return _kddm; }
            set { _kddm = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Kmdm
        {
            get { return _kmdm; }
            set { _kmdm = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }
        #endregion

    }

}
