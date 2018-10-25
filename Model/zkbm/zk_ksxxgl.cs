using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 考生信息管理
    /// </summary>
    public class Model_zk_ksxxgl
    {
        #region 字段
       
        /// <summary>
        /// 考次
        /// </summary>
        private string _kaoci = "";
        /// <summary>
        /// 考次名称
        /// </summary>
        private string _kaocimc = "";
        /// <summary>
        /// 学籍号
        /// </summary>
        private string _xjh = "";
        /// <summary>
        /// 学生编号
        /// </summary>
        private string _xsbh = "";
        /// <summary>
        /// 报名号(泰州叫报名号)
        /// </summary>
        private string _ksh = "";
        /// <summary>
        /// 姓名
        /// </summary>
        private string _xm = "";
        /// <summary>
        /// 曾用名
        /// </summary>
        private string _cym = "";
        /// <summary>
        /// 性别代码查询这个试图dbo.View_xb
        /// </summary>
        private int _xbdm = 0;
        /// <summary>
        /// 性别名称
        /// </summary>
        private string _xbmc = "";

        /// <summary>
        /// 毕业中学县区代码
        /// </summary>
        private string _bmdxqdm = "";
        /// <summary>
        /// 县区名称
        /// </summary>
        private string _xqmc = "";

        /// <summary>
        /// 毕业中学代码查询这个表dbo.zk_xxdm
        /// </summary>
        private string _bmddm = "";
        /// <summary>
        /// 毕业中学名称
        /// </summary>
        private string _bmdmc = "";

        /// <summary>
        /// 毕业中学代码查询这个表dbo.zk_xxdm 没有用其他表示
        /// </summary>
        private string _byzxdm = "";
        /// <summary>
        /// 毕业中学名称
        /// </summary>
        private string _byzxmc = "";

        private string _byzxxqmc = "";

        /// <summary>
        /// 毕业时间
        /// </summary>
        private string _bysj = "";
        /// <summary>
        /// 班级代码
        /// </summary>
        private string _bjdm = "";
        private string _bjmc = "";

        /// <summary>
        /// 民族代码
        /// </summary>
        private string _mzdm = "";
        private string _mzmc = "";
        /// <summary>
        /// 政治面貌
        /// </summary>
        private string _zzmmdm = "";
        private string _zzmmmc = "";
        /// <summary>
        /// 户口性质
        /// </summary>
        private string _hkxz = "";
        private string _hkxzmc = "";
        /// <summary>
        /// 考生类别代码
        /// </summary>
        private string _kslbdm = "";
        private string _kslbmc = "";
        /// <summary>
        /// 户籍地区名称
        /// </summary>
        private string _hjdq = "";
        /// <summary>
        /// 户籍所在区代码
        /// </summary>
        private string _hjdqdm = "";
        /// <summary>
        /// 户籍所在地地址
        /// </summary>
        private string _hjdz = "";
        /// <summary>
        /// 家庭常住地所属地区名称
        /// </summary>
        private string _jtdq = "";
        /// <summary>
        /// 家庭常住地所属地区代码
        /// </summary>
        private string _jtdqdm = "";
        /// <summary>
        /// 家庭常住地详细地址
        /// </summary>
        private string _jtdz = "";
        /// <summary>
        /// 出生日期
        /// </summary>
        private string _csrq = "";
        /// <summary>
        /// 证件类别
        /// </summary>
        private string _zjlb = "";
        /// <summary>
        /// 身份证号
        /// </summary>
        private string _sfzh = "";
        /// <summary>
        /// 通讯地址县区名称
        /// </summary>
        private string _txdzxqmc = "";
        /// <summary>
        /// 录取通知书邮寄地址
        /// </summary>
        private string _txdz = "";
        /// <summary>
        /// 录取通知书收件人
        /// </summary>
        private string _sjr = "";
        /// <summary>
        /// 邮政编码
        /// </summary>
        private string _yzbm = "";
        /// <summary>
        /// 联系电话
        /// </summary>
        private string _lxdh = "";
        /// <summary>
        /// 移动电话
        /// </summary>
        private string _yddh = "";
        /// <summary>
        /// 监护人称谓
        /// </summary>
        private string _jhrcw = "";
        /// <summary>
        /// 监护人姓名
        /// </summary>
        private string _jhrxm = "";
        /// <summary>
        /// 监护人联系电话
        /// </summary>
        private string _jhrlxdh = "";
        /// <summary>
        /// 监护人工作单位
        /// </summary>
        private string _jhrdw = "";
        /// <summary>
        /// 地理成绩
        /// </summary>
        private string _crdl = "";
        /// <summary>
        /// 生物成绩
        /// </summary>
        private string _crsw = "";

        /// <summary>
        /// 道德品质与公民素养  合格  不合格
        /// </summary>
        private string _ddpzgmsy = "";
        /// <summary>
        /// 交流合作能力  合格  不合格
        /// </summary>
        private string _jlhznl = "";
        /// <summary>
        /// 学习习惯学习能力 A    B   C   D
        /// </summary>
        private string _xxxgxxnl = "";
        /// <summary>
        /// 运动健康 A   B   C   D
        /// </summary>
        private string _ydjk = "";
        /// <summary>
        /// 审美表现 A   B   C   D
        /// </summary>
        private string _smbx = "";
        /// <summary>
        /// 创新意识实践能力 A    B   C   D
        /// </summary>
        private string _cxyssjnl = "";
        /// <summary>
        /// 是否有指标生资格0否1是
        /// </summary>
        private int _sfzbs = 0;
        /// <summary>
        /// 备注字段
        /// </summary>
        private string _bz = "";
        /// <summary>
        /// 密码
        /// </summary>
        private string _pwd = "";
        /// <summary>
        /// 考生确认0 未填报1已填报2已确认
        /// </summary>
        private int _ksqr = 0;
        /// <summary>
        /// 考生确认时间
        /// </summary>
        private DateTime _ksqrsj = DateTime.Now;
        /// <summary>
        /// 考生是否照相0未照相1已照相
        /// </summary>
        private int _pic = 0;
        /// <summary>
        /// 照相时间
        /// </summary>
        private DateTime _picsj = DateTime.Now;
        /// <summary>
        /// 学校打印0未打印已打印
        /// </summary>
        private int _xxdy = 0;
        /// <summary>
        /// 学校打印时间
        /// </summary>
        private DateTime _xxdysj = DateTime.Now;
        /// <summary>
        /// 学校确认0未确认1确认
        /// </summary>
        private int _xxqr = 0;
        /// <summary>
        /// 学校确认时间
        /// </summary>
        private DateTime _xxqrsj = DateTime.Now;
        /// <summary>
        /// 县区确认0未确认1确认
        /// </summary>
        private int _xqqr = 0;
        /// <summary>
        /// 县区确认时间
        /// </summary>
        private DateTime _xqqrsj = DateTime.Now;
        /// <summary>
        /// 志愿考生确认0未确认1确认
        /// </summary>
        private int _zyksqr = 0;
        /// <summary>
        /// 考生志愿确认时间
        /// </summary>
        private DateTime _zyksqrsj = DateTime.Now;
        /// <summary>
        /// 志愿学校确认0未确认1确认
        /// </summary>
        private int _zyxxqr = 0;
        /// <summary>
        /// 学校确认时间
        /// </summary>
        private DateTime _zyxxqrsj = DateTime.Now;
        /// <summary>
        /// 志愿县区确认0未确认1确认
        /// </summary>
        private int _zyxqqr = 0;
        /// <summary>
        /// 县区确认时间
        /// </summary>
        private DateTime _zyxqqrsj = DateTime.Now;
        /// <summary>
        /// 志愿学校打印0未打印1打印
        /// </summary>
        private int _zyxxdy = 0;
        /// <summary>
        /// 志愿学校打印时间
        /// </summary>
        private DateTime _zyxxdysj = DateTime.Now;
        /// <summary>
        /// 全球唯一标识
        /// </summary>
        private string _NewId = "";
        /// <summary>
        /// 初二会考号
        /// </summary>
        private string _crhkh = "";

        private int _jzfp = 0;

        public int Jzfp
        {
            get { return _jzfp; }
            set { _jzfp = value; }
        }
     
      
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Model_zk_ksxxgl() { }
        #endregion

        #region 属性
        /// <summary>
        /// 考次
        /// </summary>
        public string Kaoci
        {
            get { return _kaoci; }
            set { _kaoci = value; }
        }
         /// <summary>
        /// 考次名称
        /// </summary>
        public string Kaocimc
        {
            get { return _kaocimc; }
            set { _kaocimc = value; }
        }
        

        /// <summary>
        /// 学籍号
        /// </summary>
        public string Xjh
        {
            get { return _xjh; }
            set { _xjh = value; }
        }
        /// <summary>
        /// 学生编号
        /// </summary>
        public string Xsbh
        {
            get { return _xsbh; }
            set { _xsbh = value; }
        }
        /// <summary>
        /// 报名号(泰州叫报名号)
        /// </summary>
        public string Ksh
        {
            get { return _ksh; }
            set { _ksh = value; }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Xm
        {
            get { return _xm; }
            set { _xm = value; }
        }
        /// <summary>
        /// 曾用名
        /// </summary>
        public string Cym
        {
            get { return _cym; }
            set { _cym = value; }
        }
        /// <summary>
        /// 性别代码查询这个试图dbo.View_xb
        /// </summary>
        public int Xbdm
        {
            get { return _xbdm; }
            set { _xbdm = value; }
        }
        /// <summary>
        /// 性别名称
        /// </summary>
        public string Xbmc
        {
            get { return _xbmc; }
            set { _xbmc = value; }
        }
        

        /// <summary>
        /// 毕业中学县区代码
        /// </summary>
        public string Bmdxqdm
        {
            get { return _bmdxqdm; }
            set { _bmdxqdm = value; }
        }
        /// <summary>
        /// 县区名称
        /// </summary>
        public string xqmc
        {
            get { return _xqmc; }
            set { _xqmc = value; }
        }

        /// <summary>
        /// 毕业中学代码查询这个表dbo.zk_xxdm
        /// </summary>
        public string Bmddm
        {
            get { return _bmddm; }
            set { _bmddm = value; }
        }

        /// <summary>
        /// 毕业中学名称
        /// </summary>
        public string Bmdmc
        {
            get { return _bmdmc; }
            set { _bmdmc = value; }
        }

        /// <summary>
        /// 毕业中学代码查询这个表dbo.zk_xxdm 没有用其他表示
        /// </summary>
        public string Byzxdm
        {
            get { return _byzxdm; }
            set { _byzxdm = value; }
        }
        /// <summary>
        /// 毕业中学名称
        /// </summary>
        public string Byzxmc
        {
            get { return _byzxmc; }
            set { _byzxmc = value; }
        }

        /// <summary>
        /// 毕业中学县区名称
        /// </summary>
        public string Byzxxqmc
        {
            get { return _byzxxqmc; }
            set { _byzxxqmc = value; }
        }

        /// <summary>
        /// 毕业时间
        /// </summary>
        public string Bysj
        {
            get { return _bysj; }
            set { _bysj = value; }
        }
        /// <summary>
        /// 班级代码
        /// </summary>
        public string Bjdm
        {
            get { return _bjdm; }
            set { _bjdm = value; }
        }

        /// <summary>
        /// 班级名称
        /// </summary>
        public string Bjmc
        {
            get { return _bjmc; }
            set { _bjmc = value; }
        }

        /// <summary>
        /// 民族代码
        /// </summary>
        public string Mzdm
        {
            get { return _mzdm; }
            set { _mzdm = value; }
        }
        /// <summary>
        /// 民族名称
        /// </summary>
        public string Mzmc
        {
            get { return _mzmc; }
            set { _mzmc = value; }
        }

        /// <summary>
        /// 政治面貌
        /// </summary>
        public string Zzmmdm
        {
            get { return _zzmmdm; }
            set { _zzmmdm = value; }
        }
        /// <summary>
        /// 政治面貌名称
        /// </summary>
        public string Zzmmmc
        {
            get { return _zzmmmc; }
            set { _zzmmmc = value; }
        }

        /// <summary>
        /// 户口性质
        /// </summary>
        public string Hkxz
        {
            get { return _hkxz; }
            set { _hkxz = value; }
        }
        /// <summary>
        /// 户口性质名称
        /// </summary>
        public string Hkxzmc
        {
            get { return _hkxzmc; }
            set { _hkxzmc = value; }
        }

        /// <summary>
        /// 考生类别代码
        /// </summary>
        public string Kslbdm
        {
            get { return _kslbdm; }
            set { _kslbdm = value; }
        }
        /// <summary>
        /// 考生类别代码
        /// </summary>
        public string Kslbmc
        {
            get { return _kslbmc; }
            set { _kslbmc = value; }
        }

        /// <summary>
        /// 户籍地区名称
        /// </summary>
        public string Hjdq
        {
            get { return _hjdq; }
            set { _hjdq = value; }
        }
        /// <summary>
        /// 户籍地区代码
        /// </summary>
        public string Hjdqdm
        {
            get { return _hjdqdm; }
            set { _hjdqdm = value; }
        }
        /// <summary>
        /// 户籍所在地地址
        /// </summary>
        public string Hjdz
        {
            get { return _hjdz; }
            set { _hjdz = value; }
        }
        /// <summary>
        /// 家庭常住地所属地区名称
        /// </summary>
        public string Jtdq
        {
            get { return _jtdq; }
            set { _jtdq = value; }
        }
        /// <summary>
        /// 家庭常住地所属地区代码
        /// </summary>
        public string Jtdqdm
        {
            get { return _jtdqdm; }
            set { _jtdqdm = value; }
        }
        /// <summary>
        /// 家庭常住地详细地址
        /// </summary>
        public string Jtdz
        {
            get { return _jtdz; }
            set { _jtdz = value; }
        }
        /// <summary>
        /// 出生日期
        /// </summary>
        public string Csrq
        {
            get { return _csrq; }
            set { _csrq = value; }
        }
        /// <summary>
        /// 证件类别
        /// </summary>
        public string Zjlb
        {
            get { return _zjlb; }
            set { _zjlb = value; }
        }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string Sfzh
        {
            get { return _sfzh; }
            set { _sfzh = value; }
        }
        /// <summary>
        /// 通讯地址县区名称
        /// </summary>
        public string Txdzxqmc
        {
            get { return _txdzxqmc; }
            set { _txdzxqmc = value; }
        }
        /// <summary>
        /// 录取通知书邮寄地址
        /// </summary>
        public string Txdz
        {
            get { return _txdz; }
            set { _txdz = value; }
        }
        /// <summary>
        /// 录取通知书收件人
        /// </summary>
        public string Sjr
        {
            get { return _sjr; }
            set { _sjr = value; }
        }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string Yzbm
        {
            get { return _yzbm; }
            set { _yzbm = value; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Lxdh
        {
            get { return _lxdh; }
            set { _lxdh = value; }
        }
        /// <summary>
        /// 移动电话
        /// </summary>
        public string Yddh
        {
            get { return _yddh; }
            set { _yddh = value; }
        }
        /// <summary>
        /// 监护人称谓
        /// </summary>
        public string Jhrcw
        {
            get { return _jhrcw; }
            set { _jhrcw = value; }
        }
        /// <summary>
        /// 监护人姓名
        /// </summary>
        public string Jhrxm
        {
            get { return _jhrxm; }
            set { _jhrxm = value; }
        }
        /// <summary>
        /// 监护人联系电话
        /// </summary>
        public string Jhrlxdh
        {
            get { return _jhrlxdh; }
            set { _jhrlxdh = value; }
        }
        /// <summary>
        /// 监护人工作单位
        /// </summary>
        public string Jhrdw
        {
            get { return _jhrdw; }
            set { _jhrdw = value; }
        }

         /// <summary>
        /// 地理成绩
        /// </summary>
        public string Crdl
        {
            get { return _crdl; }
            set { _crdl = value; }
        }
        /// <summary>
        /// 生物成绩
        /// </summary>
        public string Crsw
        {
            get { return _crsw; }
            set { _crsw = value; }
        } 
        /// <summary>
        /// 道德品质与公民素养  合格  不合格
        /// </summary>
        public string Ddpzgmsy
        {
            get { return _ddpzgmsy; }
            set { _ddpzgmsy = value; }
        }
        /// <summary>
        /// 交流合作能力  合格  不合格
        /// </summary>
        public string Jlhznl
        {
            get { return _jlhznl; }
            set { _jlhznl = value; }
        }
        /// <summary>
        /// 学习习惯学习能力 A    B   C   D
        /// </summary>
        public string Xxxgxxnl
        {
            get { return _xxxgxxnl; }
            set { _xxxgxxnl = value; }
        }
        /// <summary>
        /// 运动健康 A   B   C   D
        /// </summary>
        public string Ydjk
        {
            get { return _ydjk; }
            set { _ydjk = value; }
        }
        /// <summary>
        /// 审美表现 A   B   C   D
        /// </summary>
        public string Smbx
        {
            get { return _smbx; }
            set { _smbx = value; }
        }
        /// <summary>
        /// 创新意识实践能力 A    B   C   D
        /// </summary>
        public string Cxyssjnl
        {
            get { return _cxyssjnl; }
            set { _cxyssjnl = value; }
        }
        /// <summary>
        /// 是否有指标生资格0否1是
        /// </summary>
        public int Sfzbs
        {
            get { return _sfzbs; }
            set { _sfzbs = value; }
        }
        /// <summary>
        /// 备注字段
        /// </summary>
        public string Bz
        {
            get { return _bz; }
            set { _bz = value; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }
        /// <summary>
        /// 考生确认0 未填报1已填报2已确认
        /// </summary>
        public int Ksqr
        {
            get { return _ksqr; }
            set { _ksqr = value; }
        }
        /// <summary>
        /// 考生确认时间
        /// </summary>
        public DateTime Ksqrsj
        {
            get { return _ksqrsj; }
            set { _ksqrsj = value; }
        }
        /// <summary>
        /// 考生是否照相0未照相1已照相
        /// </summary>
        public int Pic
        {
            get { return _pic; }
            set { _pic = value; }
        }
        /// <summary>
        /// 照相时间
        /// </summary>
        public DateTime Picsj
        {
            get { return _picsj; }
            set { _picsj = value; }
        }
        /// <summary>
        /// 学校打印0未打印已打印
        /// </summary>
        public int Xxdy
        {
            get { return _xxdy; }
            set { _xxdy = value; }
        }
        /// <summary>
        /// 学校打印时间
        /// </summary>
        public DateTime Xxdysj
        {
            get { return _xxdysj; }
            set { _xxdysj = value; }
        }
        /// <summary>
        /// 学校确认0未确认1确认
        /// </summary>
        public int Xxqr
        {
            get { return _xxqr; }
            set { _xxqr = value; }
        }
        /// <summary>
        /// 学校确认时间
        /// </summary>
        public DateTime Xxqrsj
        {
            get { return _xxqrsj; }
            set { _xxqrsj = value; }
        }
        /// <summary>
        /// 县区确认0未确认1确认
        /// </summary>
        public int Xqqr
        {
            get { return _xqqr; }
            set { _xqqr = value; }
        }
        /// <summary>
        /// 县区确认时间
        /// </summary>
        public DateTime Xqqrsj
        {
            get { return _xqqrsj; }
            set { _xqqrsj = value; }
        }
        /// <summary>
        /// 志愿考生确认0未确认1确认
        /// </summary>
        public int Zyksqr
        {
            get { return _zyksqr; }
            set { _zyksqr = value; }
        }
        /// <summary>
        /// 考生志愿确认时间
        /// </summary>
        public DateTime Zyksqrsj
        {
            get { return _zyksqrsj; }
            set { _zyksqrsj = value; }
        }
        /// <summary>
        /// 志愿学校确认0未确认1确认
        /// </summary>
        public int Zyxxqr
        {
            get { return _zyxxqr; }
            set { _zyxxqr = value; }
        }
        /// <summary>
        /// 学校确认时间
        /// </summary>
        public DateTime Zyxxqrsj
        {
            get { return _zyxxqrsj; }
            set { _zyxxqrsj = value; }
        }
        /// <summary>
        /// 志愿县区确认0未确认1确认
        /// </summary>
        public int Zyxqqr
        {
            get { return _zyxqqr; }
            set { _zyxqqr = value; }
        }
        /// <summary>
        /// 县区确认时间
        /// </summary>
        public DateTime Zyxqqrsj
        {
            get { return _zyxqqrsj; }
            set { _zyxqqrsj = value; }
        }
        /// <summary>
        /// 志愿学校打印0未打印1打印
        /// </summary>
        public int Zyxxdy
        {
            get { return _zyxxdy; }
            set { _zyxxdy = value; }
        }
        /// <summary>
        /// 志愿学校打印时间
        /// </summary>
        public DateTime Zyxxdysj
        {
            get { return _zyxxdysj; }
            set { _zyxxdysj = value; }
        }
        /// <summary>
        /// 全球唯一标识
        /// </summary>
        public string NewId
        {
            get { return _NewId; }
            set { _NewId = value; }
        }
        /// <summary>
        /// 初二会考号
        /// </summary>
        public string Crhkh
        {
            get { return _crhkh; }
            set { _crhkh = value; }
        }
        #endregion

        private string _zkzh = "";

        public string Zkzh
        {
            get { return _zkzh; }
            set { _zkzh = value; }
        }
        private int _bklb = 0;

        public int Bklb
        {
            get { return _bklb; }
            set { _bklb = value; }
        }
        private int _xjtype = 0;

        public int Xjtype
        {
            get { return _xjtype; }
            set { _xjtype = value; }
        }

    }

}
