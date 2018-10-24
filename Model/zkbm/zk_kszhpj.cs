using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 考生综合评价
    /// </summary>
    public class zk_kszhpj
    {
        private string _ksh = "";

        public string Ksh
        {
            get { return _ksh; }
            set { _ksh = value; }
        }
        /// <summary>
        /// 道德品质与公民素养  合格  不合格
        /// </summary>
        private string _ddpzgmsy = "";

        public string Ddpzgmsy
        {
            get { return _ddpzgmsy; }
            set { _ddpzgmsy = value; }
        }
        /// <summary>
        /// 交流合作能力  合格  不合格
        /// </summary>
        private string _jlhznl = "";

        public string Jlhznl
        {
            get { return _jlhznl; }
            set { _jlhznl = value; }
        }
        /// <summary>
        /// 学习习惯学习能力 A    B   C   D
        /// </summary>
        private string _xxxgxxnl = "";

        public string Xxxgxxnl
        {
            get { return _xxxgxxnl; }
            set { _xxxgxxnl = value; }
        }
        /// <summary>
        /// 运动健康 A   B   C   D
        /// </summary>
        private string _ydjk = "";

        public string Ydjk
        {
            get { return _ydjk; }
            set { _ydjk = value; }
        }
        /// <summary>
        /// 审美表现 A   B   C   D
        /// </summary>
        private string _smbx = "";

        public string Smbx
        {
            get { return _smbx; }
            set { _smbx = value; }
        }
        /// <summary>
        /// 创新意识实践能力 A    B   C   D
        /// </summary>
        private string _cxyssjnl = "";

        public string Cxyssjnl
        {
            get { return _cxyssjnl; }
            set { _cxyssjnl = value; }
        }
    }
}
