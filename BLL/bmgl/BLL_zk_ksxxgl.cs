using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model;
using System.Data.SqlClient;
using System.Data;
using BLL;
using System.IO;
using LinqToExcel;
using LinqToExcel.Query;

namespace BLL
{
    /// <summary>
    /// 考生信息管理
    /// </summary>
    public class BLL_zk_ksxxgl
    {
        SqlDbHelper_1 SqlDb = new SqlDbHelper_1();
        string error = "";
        bool bReturn = false;
        /// <summary>
        /// 执行分页存储过程，返回记录总数和当前页的数据。
        /// </summary>
        /// <param name="where">执行的条件</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="totalRecord">返回的记录总数</param>
        public DataTable ExecuteProc(string where, int pageSize, int pageIndex, ref int totalRecord)
        {
            //string sql="select  from AppraisalPersonnel";

            //数据库表名
            string tabName = "zk_ksxxgl";
            //要查询的字段
            string reField = " * ";
            //排序字段
            string orderStr = " ksh ";
            //排序标识（0、升序；1、降序）
            int orderType = 1;
            //执行成功与失败的标识（true、表示执行成功；false、表示执行失败）
            bool bFlag = false;
            decimal dec = 0;
            DataSet dst = SqlDb.ExecuteProc(tabName, reField, orderStr, "", where, pageSize, pageIndex, orderType, 0, ref dec, ref totalRecord, ref bFlag);
            if (bFlag)
            {
                if (dst.Tables.Count > 0)
                {
                    return dst.Tables[0];
                }
            }
            return null;
        }

        /// <summary>
        /// 按报名号查询一位考生，转换为实体类
        /// </summary>
        /// <param name="ksh">报名号</param>
        /// <returns></returns>
        public Model_zk_ksxxgl zk_ksxxglDisp(string ksh)
        {
            Model_zk_ksxxgl info = new Model_zk_ksxxgl();

            string sql = " select * from zk_ksxxgl where ksh =@ksh ";
            List<SqlParameter> lisP = new List<SqlParameter>();
            SqlParameter Ksh = new SqlParameter("@ksh", SqlDbType.VarChar);
            Ksh.Value = ksh;
            lisP.Add(Ksh);

            string error = "";
            bool bReturn = false;
            DataTable dt = SqlDb.selectTab(sql, lisP, ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
            {
                info = SqlDb.DT2EntityList<Model_zk_ksxxgl>(dt)[0];
            }
            return info;
        }

        /// <summary>
        /// 按报名号查询一位考生(试图查询)，转换为实体类
        /// </summary>
        /// <param name="ksh">报名号</param>
        /// <returns></returns>
        public Model_zk_ksxxgl ViewDisp(string ksh)
        {
            Model_zk_ksxxgl info = new Model_zk_ksxxgl();
            string sql = " select * from View_ksxxBx where ksh =@ksh ";
            List<SqlParameter> lisP = new List<SqlParameter>();
            SqlParameter Ksh = new SqlParameter("@ksh", SqlDbType.VarChar);
            Ksh.Value = ksh;
            lisP.Add(Ksh);

            string error = "";
            bool bReturn = false;
            DataTable dt = SqlDb.selectTab(sql, lisP, ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
            {
                info = SqlDb.DT2EntityList<Model_zk_ksxxgl>(dt)[0];
            }
            return info;
        }
        /// <summary>
        /// 按报名号查询一位考生(试图查询)，转换为实体类
        /// </summary>
        /// <param name="ksh">报名号</param>
        /// <returns></returns>
        public Model_zk_ksxxgl ViewDisp2(string ksh)
        {
            Model_zk_ksxxgl info = new Model_zk_ksxxgl();
            string sql = " select kslbdm,bklb,jzfp,mzdm from zk_ksxxgl where ksh =@ksh ";
            List<SqlParameter> lisP = new List<SqlParameter>();
            SqlParameter Ksh = new SqlParameter("@ksh", SqlDbType.VarChar);
            Ksh.Value = ksh;
            lisP.Add(Ksh);

            string error = "";
            bool bReturn = false;
            DataTable dt = SqlDb.selectTab(sql, lisP, ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
            {
                info = SqlDb.DT2EntityList<Model_zk_ksxxgl>(dt)[0];
            }
            return info;
        }


        /// <summary>
        /// 按报名号查询一位考生(试图查询)，转换为实体类
        /// </summary>
        /// <param name="ksh">报名号</param>
        /// <returns></returns>
        public Model_zk_ksxxgl ViewDisp_zkzh(string ksh)
        {
            Model_zk_ksxxgl info = new Model_zk_ksxxgl();

            string sql = " select * from View_ksxxBx where zkzh =@ksh ";
            List<SqlParameter> lisP = new List<SqlParameter>();
            SqlParameter Ksh = new SqlParameter("@ksh", SqlDbType.VarChar);
            Ksh.Value = ksh;
            lisP.Add(Ksh);

            string error = "";
            bool bReturn = false;
            DataTable dt = SqlDb.selectTab(sql, lisP, ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
            {
                info = SqlDb.DT2EntityList<Model_zk_ksxxgl>(dt)[0];
            }
            return info;
        }
        #region "按报名号查询一位考生(试图查询)，转换为实体类 加上管理部门权限"

        /// <summary>
        /// 按报名号查询一位考生(试图查询)，转换为实体类 加上管理部门权限
        /// </summary>
        /// <param name="ksh">报名号</param>
        /// <returns></returns>
        public Model_zk_ksxxgl ViewDisp(string ksh, string fanwei, int UserType)
        {
            string sql = "";
            string where = whereRole(fanwei, UserType);
            if (where.Length == 0)
            {
                sql = " select * from View_ksxxNew where ksh =@ksh   ";
            }
            else
            {
                sql = " select * from View_ksxxNew where ksh =@ksh and " + where + "  ";
            }

            Model_zk_ksxxgl info = new Model_zk_ksxxgl();

            List<SqlParameter> lisP = new List<SqlParameter>();
            SqlParameter Ksh = new SqlParameter("@ksh", SqlDbType.VarChar);
            Ksh.Value = ksh;
            lisP.Add(Ksh);

            string error = "";
            bool bReturn = false;
            DataTable dt = SqlDb.selectTab(sql, lisP, ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
            {
                info = SqlDb.DT2EntityList<Model_zk_ksxxgl>(dt)[0];
            }
            return info;
        }

        /// <summary>
        /// 管理部门权限控制
        /// </summary>
        /// <param name="fanwei">管理范围</param>
        /// <param name="UserType">用户类型</param>
        /// <returns></returns>
        public string whereRole(string fanwei, int UserType)
        {
            string where = "";
            switch (UserType)
            {
                //系统管理员
                case 1:
                    where = "";
                    break;
                //市招生办
                case 2:
                    where = "";
                    break;
                //区招生办
                case 3:
                    where = "  bmdxqdm='" + fanwei + "' ";
                    break;
                //学校用户 
                case 4:
                    where = " bmddm = '" + fanwei + "' ";
                    break;
                //班级用户 
                case 5:
                    where = " bmddm = '" + fanwei.Substring(0, 6) + "' and bjdm='" + fanwei.Substring(6) + "'  ";

                    break;
            }
            return where;
        }
        #endregion

        /// <summary>
        /// 修改考生密码
        /// </summary>
        /// <param name="ksh">报名号</param>
        /// <returns></returns>
        public bool zk_ksxxglEditPwd(string ksh, string pwd)
        {
            List<SqlParameter> lisP = new List<SqlParameter>();
            SqlParameter Ksh = new SqlParameter("@ksh", SqlDbType.VarChar);
            Ksh.Value = ksh;
            lisP.Add(Ksh);

            SqlParameter Pwd = new SqlParameter("@pwd", SqlDbType.VarChar, 50);
            Pwd.Value = pwd;
            lisP.Add(Pwd);


            string error = "";
            bool bReturn = false;
            string sql = "update zk_ksxxgl set pwd=@pwd where ksh=@ksh ";

            int i = SqlDb.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (i == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 修改考生报名填写资料
        /// </summary>
        /// <param name="item">实体类</param>
        /// <returns></returns>
        public bool zk_ksxxglEdit(Model_zk_ksxxgl item)
        {
            string error = "";
            bool bReturn = false;

            string sql = "update  zk_ksxxgl set xm=@xm,zjlb=@zjlb,sfzh=@sfzh, zzmmdm=@zzmmdm,xbdm=@xbdm,mzdm=@mzdm, ";
            sql = sql + " csrq=@csrq,lxdh=@lxdh,yddh=@yddh, xjh=@xjh,byzxdm=@byzxdm,byzxmc=@byzxmc,bjdm=@bjdm,kslbdm=@kslbdm, ";
            sql = sql + " hjdq=@hjdq,hjdqdm=@hjdqdm,hjdz=@hjdz,jtdq=@jtdq,jtdqdm=@jtdqdm,jtdz=@jtdz,sjr=@sjr,yzbm=@yzbm,";
            sql = sql + " ksqr=@ksqr,ksqrsj=@ksqrsj,bz=@bz,crhkh=@crhkh where ksh=@ksh ";

            List<SqlParameter> lisP = new List<SqlParameter>(){
           
            new SqlParameter("@xm", item.Xm),
            new SqlParameter("@zjlb", item.Zjlb),
            new SqlParameter("@sfzh", item.Sfzh),
            new SqlParameter("@zzmmdm", item.Zzmmdm),
            new SqlParameter("@xbdm", item.Xbdm),
            new SqlParameter("@mzdm", item.Mzdm),

            new SqlParameter("@csrq", item.Csrq),
            new SqlParameter("@lxdh", item.Lxdh),
            new SqlParameter("@yddh", item.Yddh),
            new SqlParameter("@xjh", item.Xjh),
            new SqlParameter("@byzxdm", item.Byzxdm),
            new SqlParameter("@byzxmc", item.Byzxmc),
            new SqlParameter("@bjdm", item.Bjdm),
             new SqlParameter("@kslbdm", item.Kslbdm),           

            new SqlParameter("@hjdq", item.Hjdq),
            new SqlParameter("@hjdqdm", item.Hjdqdm),
            new SqlParameter("@hjdz", item.Hjdz),
            new SqlParameter("@jtdq", item.Jtdq),
            new SqlParameter("@jtdqdm", item.Jtdqdm),
            new SqlParameter("@jtdz", item.Jtdz),

            new SqlParameter("@sjr", item.Sjr),
            new SqlParameter("@yzbm", item.Yzbm),
            new SqlParameter("@bz", item.Bz),
            new SqlParameter("@crhkh", item.Crhkh),

            new SqlParameter("@ksqr", item.Ksqr),
            new SqlParameter("@ksqrsj", DateTime.Now) ,
            new SqlParameter("@ksh", item.Ksh)
            };
            int i = SqlDb.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (i == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 插入考生基本资料
        /// </summary>
        /// <param name="item">实体类</param>
        /// <returns></returns>
        public bool zk_ksxxglAdd(Model_zk_ksxxgl item)
        {
            string error = "";
            bool bReturn = false;

            string sql = "insert into zk_ksxxgl(kaoci,xjh,xsbh,ksh,xm,cym,xbdm,bmddm,bmdxqdm,byzxdm,bysj,bjdm,pwd) values(@kaoci,@xjh,@xsbh,@bmdxq,@ksh,@xm,@cym,@xbdm,@bmddm,@bmdxqdm,@byzxdm,@bysj,@bjdm,@pwd)";
            List<SqlParameter> lisP = new List<SqlParameter>();
            SqlParameter Kaoci = new SqlParameter("@kaoci", SqlDbType.VarChar);
            SqlParameter Xjh = new SqlParameter("@xjh", SqlDbType.VarChar);
            SqlParameter Xsbh = new SqlParameter("@xsbh", SqlDbType.VarChar);
            // SqlParameter Bmdxq = new SqlParameter("@bmdxq", SqlDbType.VarChar);
            SqlParameter Ksh = new SqlParameter("@ksh", SqlDbType.VarChar);
            SqlParameter Xm = new SqlParameter("@xm", SqlDbType.VarChar);
            SqlParameter Cym = new SqlParameter("@cym", SqlDbType.VarChar);
            SqlParameter Xbdm = new SqlParameter("@xbdm", SqlDbType.Int);
            SqlParameter Bmddm = new SqlParameter("@bmddm", SqlDbType.VarChar);
            SqlParameter Bmdxqdm = new SqlParameter("@bmdxqdm", SqlDbType.VarChar);
            SqlParameter Byzxdm = new SqlParameter("@byzxdm", SqlDbType.VarChar);
            SqlParameter Bysj = new SqlParameter("@bysj", SqlDbType.DateTime);
            SqlParameter Bjdm = new SqlParameter("@bjdm", SqlDbType.VarChar);

            SqlParameter Pwd = new SqlParameter("@pwd", SqlDbType.VarChar);
            Kaoci.Value = item.Kaoci;
            Xjh.Value = item.Xjh;
            Xsbh.Value = item.Xsbh;
            //  Bmdxq.Value = item.Bmdxq;
            Ksh.Value = item.Ksh;
            Xm.Value = item.Xm;
            Cym.Value = item.Cym;
            Xbdm.Value = item.Xbdm;
            Bmddm.Value = item.Bmddm;
            Bmdxqdm.Value = item.Bmdxqdm;
            Byzxdm.Value = item.Byzxdm;
            Bysj.Value = item.Bysj;
            Bjdm.Value = item.Bjdm;
            Pwd.Value = item.Pwd;



            lisP.Add(Kaoci);
            lisP.Add(Xjh);
            lisP.Add(Xsbh);
            //lisP.Add(Bmdxq);
            lisP.Add(Ksh);
            lisP.Add(Xm);
            lisP.Add(Cym);
            lisP.Add(Xbdm);
            lisP.Add(Bmddm);
            lisP.Add(Bmdxqdm);
            lisP.Add(Byzxdm);
            lisP.Add(Bysj);
            lisP.Add(Bjdm);

            lisP.Add(Pwd);



            int i = SqlDb.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (i == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 检查身份证是否存在
        /// </summary>
        /// <param name="sfzh">身份证号</param>
        /// <returns>true 存在</returns>
        public bool checksfzh(string sfzh, string ksh)
        {
            string sql = " select ksh from zk_ksxxgl where ksh<>@ksh and sfzh =@sfzh ";
            List<SqlParameter> lisP = new List<SqlParameter>();
            SqlParameter Sfzh = new SqlParameter("@sfzh", SqlDbType.VarChar);
            Sfzh.Value = sfzh;
            lisP.Add(Sfzh);

            SqlParameter Ksh = new SqlParameter("@ksh", SqlDbType.VarChar);
            Ksh.Value = ksh;
            lisP.Add(Ksh);

            string error = "";
            bool bReturn = false;
            DataTable dt = SqlDb.selectTab(sql, lisP, ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 检测录取通知书邮寄地址 相同个数
        /// </summary> 
        public int CheckTxdzNumber(string txdz)
        {
            List<Model_zk_ksxxgl> ks = new List<Model_zk_ksxxgl>();

            string sql = " select ksh from zk_ksxxgl where  txdz=@txdz ";
            List<SqlParameter> lisP = new List<SqlParameter>();
            SqlParameter Txdz = new SqlParameter("@txdz", SqlDbType.VarChar);
            Txdz.Value = txdz;
            lisP.Add(Txdz);

            string error = "";
            bool bReturn = false;
            DataTable dt = SqlDb.selectTab(sql, lisP, ref   error, ref   bReturn);
            return dt.Rows.Count;

        }

        /// <summary>
        /// 联系电话 最多相同个数
        /// </summary> 
        public int CheckLxdhNumber(string lxdh)
        {
            List<Model_zk_ksxxgl> ks = new List<Model_zk_ksxxgl>();

            string sql = " select ksh from zk_ksxxgl where  lxdh=@lxdh ";
            List<SqlParameter> lisP = new List<SqlParameter>();
            SqlParameter Lxdh = new SqlParameter("@lxdh", SqlDbType.VarChar);
            Lxdh.Value = lxdh;
            lisP.Add(Lxdh);

            string error = "";
            bool bReturn = false;
            DataTable dt = SqlDb.selectTab(sql, lisP, ref   error, ref   bReturn);
            return dt.Rows.Count;

        }

        /// <summary>
        /// 考生确认资料
        /// </summary> 
        public bool KsQueren(string ksh)
        {
            List<Model_zk_ksxxgl> ks = new List<Model_zk_ksxxgl>();

            string sql = " update zk_ksxxgl set ksqr=2,ksqrsj=GETDATE() where ksh=@ksh and ksqr=1 ";
            List<SqlParameter> lisP = new List<SqlParameter>();
            SqlParameter Ksh = new SqlParameter("@ksh", SqlDbType.VarChar);
            Ksh.Value = ksh;
            lisP.Add(Ksh);

            string error = "";
            bool bReturn = false;
            int i = SqlDb.ExecuteNonQuery(sql, lisP, ref   error, ref   bReturn);
            if (i == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 考生照相
        /// </summary> 
        public bool KsPhoto(string ksh)
        {
            List<Model_zk_ksxxgl> ks = new List<Model_zk_ksxxgl>();

            string sql = " update zk_ksxxgl set pic=1,picsj=GETDATE() where ksh=@ksh and isnull(Xxdy,0)!=1  ";
            List<SqlParameter> lisP = new List<SqlParameter>();
            SqlParameter Ksh = new SqlParameter("@ksh", SqlDbType.VarChar);
            Ksh.Value = ksh;
            lisP.Add(Ksh);

            string error = "";
            bool bReturn = false;
            int i = SqlDb.ExecuteNonQuery(sql, lisP, ref   error, ref   bReturn);
            if (i == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 考生综合评价
        /// </summary> 
        public bool KsZhpj(Model_zk_ksxxgl model)
        {
            string sql = " update zk_ksxxgl set Ddpzgmsy=@Ddpzgmsy,Jlhznl=@Jlhznl,Xxxgxxnl=@Xxxgxxnl,Ydjk=@Ydjk,Smbx=@Smbx,Cxyssjnl=@Cxyssjnl,Sfzbs=@Sfzbs where ksh=@ksh ";
            List<SqlParameter> lisP = new List<SqlParameter>() { 
                new SqlParameter("@ksh", model.Ksh),
                new SqlParameter("@Ddpzgmsy", model.Ddpzgmsy),
                new SqlParameter("@Jlhznl", model.Jlhznl),
                new SqlParameter("@Xxxgxxnl", model.Xxxgxxnl),
                new SqlParameter("@Ydjk", model.Ydjk),
                new SqlParameter("@Smbx", model.Smbx),
                new SqlParameter("@Cxyssjnl", model.Cxyssjnl),
                new SqlParameter("@Sfzbs", model.Sfzbs)
            };
            string error = "";
            bool bReturn = false;
            int i = SqlDb.ExecuteNonQuery(sql, lisP, ref   error, ref   bReturn);
            if (i == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #region "导出数据DBF"
        /// <summary>
        /// 导出数据DBF
        /// </summary> 
        public DataSet ExportDBF(string where)
        {
            string sql = " select  ksh,xm,xbdm from zk_ksxxgl where " + where + " ";
            return SqlDb.selectDataSet(sql, ref error, ref bReturn);
        }


        #endregion


        #region Excel数据导入到SqlServer数据库方法

        /// <summary>
        /// Excel数据导入到SqlServer数据库方法
        /// </summary>
        /// <param name="excelFilePath">excel文件存放路径</param>
        /// <param name="importSqlCmd">需要用于导入数据的sql语句</param>
        /// <param name="importParams">用于导入数据的sql参数列表</param>
        /// <param name="isZL">是否增量导入</param>
        /// <returns></returns>
        public string ImportExcelData(string excelFilePath)
        {
            try
            {
                if (!File.Exists(excelFilePath))
                    return "导入失败，原因是：输入的文件路径不正确或指定的文件不存在。";

                ExcelQueryFactory excelFile = new ExcelQueryFactory(excelFilePath);
                ExcelQueryable<Row> excel = excelFile.Worksheet();
                SqlDbHelper_1 dbHelp = new SqlDbHelper_1();
                StringBuilder resultMsg = new StringBuilder();

                string sqlCmd = " update zk_ksxxgl_zhpj set Ddpzgmsy=@Ddpzgmsy,Jlhznl=@Jlhznl,Xxxgxxnl=@Xxxgxxnl,Ydjk=@Ydjk,Smbx=@Smbx,Cxyssjnl=@Cxyssjnl where ksh=@ksh ";
                string checkSql = "insert into zk_ksxxgl_zhpj(ksh,ddpzgmsy,jlhznl,xxxgxxnl,ydjk,smbx,cxyssjnl) values(@ksh,@ddpzgmsy,@jlhznl,@xxxgxxnl,@ydjk,@smbx,@cxyssjnl)";
                bool bReturn = true;

                int i = 0;
                int lost = 0;
                int finish = 0;

                foreach (var element in excel)
                {
                    if (element.ColumnNames.Count() != 7)
                        return "导入失败，原因是：输入的文件格式不对应，目标格式为7列，您输入的是：" + element.ColumnNames.Count() + "列的格式。";

                    string errMsg = "";
                    string baseStr = "系统时间：" + DateTime.Now.ToString("yyyy年MM月dd日hh时mm分ss秒") + "。";

                    //空数据则跳过
                    if (string.IsNullOrEmpty(element[0].ToString().Trim()))
                        continue;
                    if (element[0].ToString().Trim().Length != 12)
                    {
                        resultMsg.Append("第" + (i + 1) + "行错误，原因：报名号【" + element[0].ToString() + "】不是12位。\r\n");
                        lost++;
                        i++;
                        continue;
                    }
                    else
                    {
                        string strc = isKsh(element[0].ToString());
                        if (strc != "")
                        {
                            resultMsg.Append("第" + (i + 1) + "行错误，原因：" + strc + "\r\n");
                            lost++;
                            i++;
                            continue;
                        }
                    }

                    if (element[1].ToString().Trim().Length == 0 || (element[1].ToString().Trim() != "合格" && element[1].ToString().Trim() != "不合格"))
                    {
                        resultMsg.Append("第" + (i + 1) + "行错误，原因：道德品质【" + element[1].ToString() + "】有误。\r\n");
                        lost++;
                        i++;
                        continue;
                    }
                    if (element[2].ToString().Trim().Length == 0 || (element[2].ToString().Trim() != "合格" && element[2].ToString().Trim() != "不合格"))
                    {
                        resultMsg.Append("第" + (i + 1) + "行错误，原因：交流合作【" + element[2].ToString() + "】有误。\r\n");
                        lost++;
                        i++;
                        continue;
                    }
                    if (element[3].ToString().Trim().Length == 0 || !checzhjp(element[3].ToString().Trim().ToUpper()))
                    {
                        resultMsg.Append("第" + (i + 1) + "行错误，原因：学习能力【" + element[3].ToString() + "】有误。\r\n");
                        lost++;
                        i++;
                        continue;
                    }
                    if (element[4].ToString().Trim().Length == 0 || !checzhjp(element[4].ToString().Trim().ToUpper()))
                    {
                        resultMsg.Append("第" + (i + 1) + "行错误，原因：运动健康【" + element[4].ToString() + "】有误。\r\n");
                        lost++;
                        i++;
                        continue;
                    }
                    if (element[5].ToString().Trim().Length == 0 || !checzhjp(element[5].ToString().Trim().ToUpper()))
                    {
                        resultMsg.Append("第" + (i + 1) + "行错误，原因：审美表现【" + element[5].ToString() + "】有误。\r\n");
                        lost++;
                        i++;
                        continue;
                    }
                    if (element[6].ToString().Trim().Length == 0 || !checzhjp(element[6].ToString().Trim().ToUpper()))
                    {
                        resultMsg.Append("第" + (i + 1) + "行错误，原因：创新实践【" + element[6].ToString() + "】有误。\r\n");
                        lost++;
                        i++;
                        continue;
                    }
                    DataTable tmpDataSet = Seleczhpj(element[0].ToString().Trim());
                    List<SqlParameter> dataParams = new List<SqlParameter> {
                                              new SqlParameter("@ksh",element[0].ToString().Trim()),
                                             
                                                new SqlParameter("@Ddpzgmsy",element[1].ToString().Trim()),
                                                new SqlParameter("@Jlhznl",element[2].ToString().Trim()),
                                                new SqlParameter("@Xxxgxxnl",element[3].ToString().Trim()),
                                                new SqlParameter("@Ydjk",element[4].ToString().Trim()),
                                                new SqlParameter("@Smbx",element[5].ToString().Trim()),
                                                new SqlParameter("@Cxyssjnl",element[6].ToString().Trim()) };
                    if (tmpDataSet.Rows.Count > 0)
                    {
                        SqlDb.ExecuteNonQuery(sqlCmd, dataParams, ref   errMsg, ref   bReturn);

                    }
                    else
                    {
                        SqlDb.ExecuteNonQuery(checkSql, dataParams, ref   errMsg, ref   bReturn);
                    }


                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        resultMsg.Append(baseStr + "第" + (i + 1) + "行数据导入时发生错误，原因是：\r\n");
                        resultMsg.Append("      " + errMsg + "\r\n");

                        lost++;
                    }
                    else
                    {
                        resultMsg.Append(baseStr + "第" + (i + 1) + "行数据导入成功。\r\n");
                        finish++;
                    }

                    i++;
                }

                resultMsg.Append("共处理:" + i + "数据导入完毕。共成功导入：" + finish + "条数据。导入失败：" + lost + "条数据。\r\n");

                File.Delete(excelFilePath);

                return resultMsg.ToString();
            }
            catch (Exception)
            {

                return "数据有错!";
            }
        }

        private bool checzhjp(string value)
        {
            switch (value)
            {
                case "A":
                    return true;
                case "B":
                    return true;
                case "C":
                    return true;
                case "D":
                    return true;
                default:
                    return false;
            }
        }
        /// <summary>
        /// 判断报名号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string isKsh(string str)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            if (new BLL_zk_kcdm().Select_zk_kcdm().Select(" kcdm='" + str.Substring(0, 2) + "'").Length == 0)
            {
                return "报名号【" + str + "】的考次信息,尚未定义.\r\n";
            }

            else if (new BLL_zk_xxdm().Select_zk_xxdm().Select(" xxdm='" + str.Substring(2, 6) + "'").Length == 0)
            {
                return "报名号【" + str + "】的学校信息,尚未定义.\r\n";
            }
            else
            {
                switch (UserType)
                {
                    //系统管理员
                    case 1:
                        return "";
                    //市招生办
                    case 2:
                        return "";
                    //区招生办
                    case 3:
                        if (Department == str.Trim().Substring(2, 4))
                        {
                            return "";
                        }
                        else
                        {
                            return "导入的考生不属于您所属县区.";
                        }

                    //学校用户 
                    case 4:
                        if (Department == str.Trim().Substring(2, 6))
                        {
                            return "";
                        }
                        else
                        {
                            return "导入的考生不属于您所属学校.";
                        }
                    default:
                        return "*";
                }

            }
        }
        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="item"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool Insert_ksxx(Model_zk_ksxxgl item)
        {
            try
            {


                string sql = "insert into zk_ksxxgl(kaoci,xjh,xsbh,ksh,xm,xbdm,bmddm,bmdxqdm,byzxdm,Byzxmc,bjdm,sfzh,Kslbdm,pwd) values(@kaoci,@xjh,@xsbh,@ksh,@xm,@xbdm,@bmddm,@bmdxqdm,@byzxdm,@Byzxmc,@bjdm,@sfzh,@Kslbdm,@pwd)";
                List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@kaoci",item.Kaoci),
 			 new SqlParameter("@xjh",item.Xjh),
              new SqlParameter("@xsbh",item.Xsbh),
               new SqlParameter("@ksh",item.Ksh),
                new SqlParameter("@xm",item.Xm),
                 new SqlParameter("@xbdm",item.Xbdm),
                  new SqlParameter("@bmddm",item.Bmddm),
                   new SqlParameter("@bmdxqdm",item.Bmdxqdm),
                    new SqlParameter("@byzxdm",item.Byzxdm),
                     new SqlParameter("@Byzxmc",item.Byzxmc),
                      new SqlParameter("@bjdm",item.Bjdm),
                       new SqlParameter("@sfzh",item.Sfzh),
                         new SqlParameter("@Kslbdm",item.Kslbdm),
                          new SqlParameter("@pwd",item.Ksh)
                       
                      
			};
                this.SqlDb.BeginTran();
                this.SqlDb.execSql_Tran(sql, lisP);
                this.SqlDb.EndTran(true);
                return true;
            }
            catch (Exception exe)
            {
                this.SqlDb.EndTran(false);
            }
            return false;
        }
        #region "导出数据DBF,excel"
        /// <summary>
        /// 导出数据DBF
        /// </summary> 
        public DataSet ExportDBFKsh(string where)
        {
            string sql = "select  kaoci,xjh,bmdxqdm,ksh,xm,(case xbdm when  1 then '男' else '女'  end) as xb,sfzh,bmddm,ISNULL( b.xxmc,'') as bmdmc, byzxdm,byzxmc,bjdm,ISNULL( xsbh,'') as xsbh, ISNULL(bz,'') as bz  from zk_ksxxgl a left join zk_xxdm b on a.bmddm=b.xxdm where " + where + " order by a.ksh asc ";

            return SqlDb.selectDataSet(sql, ref error, ref bReturn);
        }
        /// <summary>
        /// 导出数据excel
        /// </summary> 
        public DataSet ExportEXCELKsh(string where)
        {

            string sql = "select ksh as '报名号', kaoci as '考次',xjh as '学籍号',sfzh as '身份证号',xm as '姓名',bmdxqdm as '县区代码(毕业中学所在县区)' ,bmddm as '学校(毕业中学)',bjdm as '班级',ISNULL( xsbh,'') as '学生编码', ISNULL(bz,'') as '备注' from zk_ksxxgl  where " + where + " order by ksh asc ";

            return SqlDb.selectDataSet(sql, ref error, ref bReturn);
        }
        /// <summary>
        /// 导出数据全部DBF
        /// </summary> 
        public DataSet ExportHKDBFALL(string where)
        {
            string sql = "select ksh,kaoci,xjh,xm,sfzh,bmdxqdm,bmddm,bmdmc,xsbh,bjdm,bz from View_zk_kshkxxgl  where " + where + "  order by ksh asc ";

            return SqlDb.selectDataSet(sql, ref error, ref bReturn);
        }

        /// <summary>
        /// 导出数据全部DBF
        /// </summary> 
        public DataSet ExportDBFALL(string where)
        {
            string sql = "select * from View_ksxxNew  where " + where + "  order by ksh asc ";

            return SqlDb.selectDataSet(sql, ref error, ref bReturn);
        }
        #endregion
        /// <summary>
        /// 查询综合评价表考生是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable Seleczhpj(string ksh)
        {
            string sql = "";

            sql = "select * from zk_ksxxgl_zhpj where ksh='" + ksh + "'";

            DataTable tab = SqlDb.selectTab(sql, ref error, ref bReturn);
            return tab;
        }
        /// <summary>
        /// 导出未填报志愿数据excel
        /// </summary> 
        public DataSet ExportNoZy(string where,string pcdm)
        {
            string sql = @"select  ksh as '报名号',xm as '姓名',bmdmc '毕业中学学校',bjdm as '班级',lxdh as '联系电话' from View_ksxxgl where ksh not in (
 select a.ksh from zk_ksxxgl a  left join zk_kszyxx b on a.ksh=b.ksh 
 where isnull(b.xxdm,'')<>'' and pcdm='" + pcdm + @"')
 and ksh not in (
 select ksh from zk_lqk  where isnull(td_zt,0)=5) and " + where + " order by ksh asc";
            string error = "";
            bool bReturn = false;
            return SqlDb.selectDataSet(sql, ref error, ref bReturn);
        }
        /// <summary>
        ///  
        /// </summary>
        /// <param name="ksh">报名号</param>
        /// <returns></returns>
        public DataTable ViewDisp_Login(string ksh, string type)
        {
           
            string sql = "";
            if (type == "0")//考生号
            {
                sql = "SELECT b.ksh,b.xm,b.pwd,Kaoci ,Bmdxqdm,Bmddm,Kslbdm,Bklb,Jzfp,Mzdm,b.Zkzh,Xjtype,sfzh FROM zk_ksxxgl_login a left join zk_ksxxgl b on a.ksh=b.ksh where a.ksh =@ksh ";
            }
            else
            {
                sql = "SELECT b.ksh,b.xm,b.pwd,Kaoci ,Bmdxqdm,Bmddm,Kslbdm,Bklb,Jzfp,Mzdm,b.Zkzh,Xjtype,sfzh FROM zk_ksxxgl_login a left join zk_ksxxgl b on a.ksh=b.ksh  where a.zkzh =@ksh ";
            }
            
            List<SqlParameter> lisP = new List<SqlParameter>();
            SqlParameter Ksh = new SqlParameter("@ksh", SqlDbType.VarChar);
            Ksh.Value = ksh;
            lisP.Add(Ksh);
            string error = "";
            bool bReturn = false;
            DataTable dt = SqlDb.selectTab(sql, lisP, ref   error, ref   bReturn);

            return dt;
        }
    }
}
