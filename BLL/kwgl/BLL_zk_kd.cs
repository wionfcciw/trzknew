using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using LinqToExcel;
using LinqToExcel.Query;
namespace BLL
{
    /// <summary>
    ///考点表控制类
    /// </summary>
    public class BLL_zk_kd
    {
        private SqlDbHelper_1 _dbHelper = new SqlDbHelper_1();
        private string error = "";
        private bool bReturn = false;
        /// <summary>
        /// 试卷需求统计
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable Select_sj_tj(string where)
        {
            string error = "";
            bool bReturn = false;

            Model_zk_xxdm info = new Model_zk_xxdm();
            string sql = "select c.xqdm,c.xqmc,ksnum=COUNT(ksh), kcdm=COUNT(distinct kcdm),kdnum=COUNT(distinct b.kddm) from zk_kczw as a,zk_kd as b,zk_xqdm as c where a.kddm=b.kddm and b.xqdm=c.xqdm and " + @where + " group by  c.xqdm,c.xqmc order by c.xqdm ";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@where",where)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);

            return dt;
        }
        /// <summary>
        /// 试卷需求统计-考点
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable Select_sj_tj_kd(string where)
        {
            string error = "";
            bool bReturn = false;

            Model_zk_xxdm info = new Model_zk_xxdm();
            string sql = "select b.kddm,b.kdmc,ksnum=COUNT(ksh),kcdm=COUNT(distinct kcdm) from zk_kczw as a,zk_kd as b where a.kddm=b.kddm and " + where + " group by  b.kddm,b.kdmc order by b.kddm asc ";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@where",where)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);

            return dt;
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="item">实体类</param>
        /// <returns></returns>
        public bool Insert_zk_kd(Model_zk_kd item, string xxdm,string Bksh,string Eksh,ref string str)
        {
            try
            {

                string sql = "";
                if (item.Lsh > 0)
                {
                    sql = " update  zk_kd set kdmc=@kdmc where kddm=@kddm ";
                }
                else
                {
                    sql = "insert into zk_kd(kddm,kdmc,xqdm,isxx,isbp) values(@kddm,@kdmc,@xqdm,@isxx,@isbp) ";
                }
                List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@kddm",item.Kddm),
 			 new SqlParameter("@kdmc",item.Kdmc),
 			 new SqlParameter("@xqdm",item.Xqdm),
              new SqlParameter("@isxx",item.Isxx),
               new SqlParameter("@isbp",item.Isbp),
			};

                this._dbHelper.BeginTran();
                this._dbHelper.execSql_Tran(sql, lisP);

                sql = " delete  zk_kd_xx where kddm=@kddm ";
                this._dbHelper.execSql_Tran(sql, lisP);

                string[] arry = xxdm.Split(',');
                string[] ArryBksh = Bksh.Split(',');
                string[] ArryEksh = Eksh.Split(',');


                for (int i = 0; i < arry.Length; i++)
                {
                    if (arry[i].Length > 0)
                    {
                        DataTable dt_xx = this._dbHelper.selectTab("select * from zk_kd_xx where  xxdm='" + arry[i] + "'", ref error, ref bReturn);
                        if (dt_xx.Rows.Count > 0)
                        {
                            DataTable dt_ks = this._dbHelper.selectTab(" select MIN(ksh) as minksh ,MAX(ksh) as maxksh   from zk_ksxxgl where ksh between '" + dt_xx.Rows[0]["Bksh"].ToString() + "' and '" + dt_xx.Rows[0]["Eksh"].ToString() + "' and ksh between '" + ArryBksh[i] + "' and '" + ArryEksh[i] + "'", ref error, ref bReturn);
                            if (dt_xx.Rows.Count > 0)
                            {
                                if (dt_ks.Rows.Count > 0)
                                {
                                    if (dt_ks.Rows[0]["minksh"].ToString().Length > 0 || dt_ks.Rows[0]["maxksh"].ToString().Length > 0)
                                    {
                                        str = "起始号：" + dt_ks.Rows[0]["minksh"].ToString() + "<br>结束号：" + dt_ks.Rows[0]["maxksh"].ToString() + "<br>已被按排在考号:" + dt_xx.Rows[0]["kddm"].ToString();
                                        this._dbHelper.EndTran(false);
                                        return false;
                                        break;
                                    }
                                }
                            }
                        }
                        sql += " insert into zk_kd_xx(kddm,xxdm,Bksh,Eksh) values(@kddm,'" + arry[i] + "','" + ArryBksh[i] + "','" + ArryEksh[i] + "'); ";
                    }
                }

                this._dbHelper.execSql_Tran(sql, lisP);
                this._dbHelper.EndTran(true);
                return true;
            }
            catch (Exception exe)
            {
                this._dbHelper.EndTran(false);
            }
            return false;

        }

        /// <summary>
        /// 根据考点代码修改
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool update_zk_kd(Model_zk_kd item)
        {
            string sql = "update  zk_kd set kdmc=@kdmc where kddm=@kddm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@kdmc",item.Kdmc),
 			 new SqlParameter("@kddm",item.Kddm) 
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 根据考点代码删除
        /// </summary>
        /// <param name="kddm"></param>
        /// <returns></returns>
        public bool delete_zk_kd(string kddm)
        {

            try
            {

                string sql = "delete  zk_kd where kddm in(" + kddm + ") ";
             

                this._dbHelper.BeginTran();
                this._dbHelper.execSql_Tran(sql);

                sql = "delete  zk_kd_xx where kddm in(" + kddm + ") ";
                this._dbHelper.execSql_Tran(sql);
                sql = "delete  zk_kd_bmd where kddm in(" + kddm + ") ";
                this._dbHelper.execSql_Tran(sql);
                
                this._dbHelper.EndTran(true);
                return true;
            }
            catch (Exception exe)
            {
                this._dbHelper.EndTran(false);
            }
            return false;
        }

        #region "取消考点编排"
        /// <summary>
        /// 取消考点编排
        /// </summary>
        /// <param name="kddm">考点代码</param>
        /// <returns></returns>
        public bool Canal_zk_kd(string kddm)
        {

            try
            {
                string sql = "";
                this._dbHelper.BeginTran();
                //List<SqlParameter> lisP = new List<SqlParameter>()
                //{
                //   new SqlParameter("@kddm",kddm)
                //};

                sql = " update zk_kd set isxx=0,isbp=0 where kddm in (" + kddm + ")";
                this._dbHelper.execSql_Tran(sql);

                sql = " delete from zk_kczw where kddm  in (" + kddm + ")";
                this._dbHelper.execSql_Tran(sql);

                this._dbHelper.EndTran(true);
                return true;
            }
            catch (Exception exe)
            {
                this._dbHelper.EndTran(false);
            }
            return false;
        }
        #endregion


        #region "考点编排"
        /// <summary>
        /// 考点编排
        /// </summary>
        /// <param name="kddmList">考点代码，多个用,隔开</param>
        /// <param name="type">类型：1、考点为单位进行混编 2、考点内以毕业中学为单位进行混编 3、相同学校不能坐一起混编 4、相同学校相同班级不能坐一起混编</param>
        /// <returns></returns>
        public bool zk_kd_KDBP(string kddmList, int type)
        {

            try
            {

                int NumberType = 2; //试室流水号类型 1:全市唯一流水号 2:县区内唯一流水号 3:考点内唯一流水号

                int iRow = 5; //行数
                int iColumn = 6; //列数 

                int ssdmLength = 3; //试室号几位
                int zwdmLength = 2; //座位号几位
                int iType = 1; //类型:Z型 1 S型 2
                int ialgorithm = 1; //1:随机编排 2:相同学校不能在一起随机编排 3:相同学校相同班级不能在一起随机编排


                if (type == 3)
                    ialgorithm = 2;
                if (type == 4)
                    ialgorithm = 3;


                //试室号
                int ssdm = 0;
                string ssdm_s = "";
                string xqdm = ""; //县区代码

                //定义二维数组 
                string[,] iarray = new string[iRow, iColumn];
                string sql = "";

                this._dbHelper.BeginTran();
                sql = "select kddm,xqdm from  zk_kd where kddm in(" + kddmList + ") and isnull(isbp,0)=0  order by kddm asc ";
                DataTable dtkd = _dbHelper.selectTab(sql, ref error, ref bReturn);

                //循环考点
                for (int ikd = 0; ikd < dtkd.Rows.Count; ikd++)
                {
                    ssdm += 1;
                    for (int i = 0; i < iRow; i++) //行
                    {
                        for (int j = 0; j < iColumn; j++) //列
                        {
                            iarray[i, j] = " ";
                        }
                    }

                    //座位号
                    int zwdm = 1;
                    string zwdm_s = "";

                    if (NumberType == 2) //县区内流水号
                    {
                        if (xqdm != dtkd.Rows[ikd]["xqdm"].ToString())
                        {
                            ssdm_s = "";
                            ssdm = 1;
                        }
                        xqdm = dtkd.Rows[ikd]["xqdm"].ToString();
                    }
                    if (NumberType == 3) //考点内流水号
                    {
                        ssdm_s = "";
                        ssdm = 1;
                    }


                    string kddm = dtkd.Rows[ikd]["kddm"].ToString();  //考点代码


                    sql = "";
                    if (type == 1) //考点为单位进行混编 
                    {
                        sql = " select  ROW_NUMBER() OVER (ORDER BY NEWID()) as xuhao,ksh,bmddm,bjdm,kddm,xqdm  from View_kdbp where kddm='" + kddm + "' ";
                    }
                    if (type == 2) //毕业中学为单位进行混编 
                    {
                        sql = " select  ROW_NUMBER() OVER (ORDER BY NEWID()) as xuhao,ksh,bmddm,bjdm,kddm,xqdm  from View_kdbp where  kddm='" + kddm + "' order by xxdm asc ";
                    }
                    if (type == 3) //同一个学校不能坐一起进行混编 
                    {
                        sql = " select  ROW_NUMBER() OVER (ORDER BY NEWID()) as xuhao,ksh,bmddm,bjdm,kddm,xqdm  from View_kdbp where  kddm='" + kddm + "' ";
                    }
                    if (type == 4) //相同学校相同班级不能坐一起混编 
                    {
                        sql = " select  ROW_NUMBER() OVER (ORDER BY NEWID()) as xuhao,ksh,bmddm,bjdm,kddm,xqdm  from View_kdbp where  kddm='" + kddm + "' ";
                    }

                    //查询考生数据
                    DataTable dtks = _dbHelper.selectTab(sql, ref error, ref bReturn);
                    if (dtks != null && dtks.Rows.Count > 0)
                    {

                        DataRow row = null;
                        //数据行数
                        int dRow = 0;
                        //剩余数量
                        int number = 0;
                        //当有剩余数据时为true
                        bool iFlag = false;
                        //防止死循环
                        int ij = 0;

                        //当前座位号的行号和列号
                        int Row_i = 0;
                        int Column_i = 0;
                        sql = "";

                        while (true)
                        {
                            //如果数据符合条件为true
                            bool bFlag = false;
                            //前面数据
                            string sTop = "";
                            //后面数据
                            string sBack = "";
                            //左边数据
                            string sLeft = "";
                            //右边数据
                            string sRight = "";
                            //当前数据
                            string sNow = "";

                            //没有数据跳出循环
                            if (dtks.Rows.Count < 1)
                            {
                                break;
                            }
                            //取出行的数据
                            row = dtks.Rows[dRow];

                            ssdm_s = ssdm.ToString();
                            zwdm_s = zwdm.ToString();

                            if (ssdm_s.Length < ssdmLength)
                            {
                                string a = "";
                                //for (int si = 0; si < ssdmLength - ssdm_s.Length; si++)
                                //{
                                
                                //  }
                                if (ssdm_s.Length == 1)
                                    a += "00";
                                else if (ssdm_s.Length == 2)
                                    a += "0";
                                ssdm_s = a + ssdm_s;
                             
                            }
                            if (zwdm_s.Length < zwdmLength)
                            {
                                string a = "";
                                for (int si = 0; si < zwdmLength - zwdm_s.Length; si++)
                                {
                                    a += "0";
                                }
                                zwdm_s = a + zwdm_s;
                            }

                            if (ialgorithm == 1)  //随机编排
                                iFlag = true;

                            if (ialgorithm == 2)  //随机编排(相同学校不能坐一起)
                                sNow = row["bmddm"].ToString();

                            if (ialgorithm == 3) //随机编排(相同学校相同班级不能坐一起)
                                sNow = row["bmddm"].ToString() + "_" + row["bjdm"].ToString();


                            //每个试室第一人不用判断
                            if (zwdm == 1)
                                bFlag = true;

                            //剩余都相同数据不用判断。
                            if (iFlag)
                                bFlag = true;

                            //前面数据
                            if (Row_i - 1 >= 0)
                                sTop = iarray[Row_i - 1, Column_i].ToString();
                            //后面数据
                            if (Row_i + 1 - iRow > 0)
                                sBack = iarray[Row_i + 1, Column_i].ToString();
                            //左边数据
                            if (Column_i - 1 >= 0)
                                sLeft = iarray[Row_i, Column_i - 1].ToString();
                            //右边数据
                            if (Column_i + 1 - iColumn > 0)
                                sRight = iarray[Row_i, Column_i + 1].ToString();

                            //当bFlag等于false时 判断前后左右是否符合条件             
                            if (!bFlag)
                            {
                                if (Row_i == 0 && sNow != sLeft) //每列第一人
                                {
                                    bFlag = true;
                                }
                                else
                                {
                                    if ((Column_i + 1) % 2 == 0 && iType == 2) //S型偶数列
                                    {
                                        if (sNow != sBack && sNow != sLeft) //后面和左边
                                            bFlag = true;
                                    }
                                    else //Z型和S型奇数列
                                    {
                                        if (sNow != sTop && sNow != sLeft) //前面和左边
                                            bFlag = true;
                                    }
                                }
                                if (!bFlag)
                                    dRow += 1;
                            }

                            if (bFlag)
                            {

                                //ksh varchar(14) primary key,--报名号
                                //zkzh varchar(10),--考试号 年份代码（2位）+考区代码（2位）+考点代码后两位（2位）+考场代码（2位）+座位代码（2位）
                                //kcdm varchar(8),--考场代码 市（区）代码（4位）+考点代码（2位）+考场号2位
                                //zwh varchar(2),--座位号
                                //kddm varchar(4),--考点代码
                                string ksh = row["ksh"].ToString();
                               // string zkzh = ksh.Substring(0, 2) + kddm + ssdm_s + zwdm_s;
                            
                                string zkzh = kddm + ssdm_s + zwdm_s;
                                string kcdm = row["xqdm"].ToString() + kddm.Substring(2) + ssdm_s;


                                iarray[Row_i, Column_i] = sNow;
                                string kszh = row["bmddm"].ToString().Substring(2, 4) + ssdm_s + zwdm_s;
                                sql += " insert into zk_kczw(ksh,zkzh,kcdm,zwh,kddm) values('" + ksh + "','" + zkzh + "','" + kcdm + "','" + zwdm_s + "','" + kddm + "') ;";
                                //删除数据
                                dtks.Rows.Remove(row);
                                //重头开始循环
                                dRow = 0;

                                //试室号、座位号增加
                                if (zwdm == iRow * iColumn)
                                {
                                    if (dtks.Rows.Count > 0)
                                    {
                                        ssdm += 1;
                                    }
                                    
                                    zwdm = 1;

                                    Row_i = 0;
                                    Column_i = 0;

                                    //清空数组
                                    for (int i = 0; i < iRow; i++) //行
                                    {
                                        for (int j = 0; j < iColumn; j++) //列
                                        {
                                            iarray[i, j] = " ";
                                        }
                                    }
                                }
                                else
                                {
                                    if ((Column_i + 1) % 2 == 0 && iType == 2) //S型偶数列
                                    {
                                        Row_i -= 1;
                                    }
                                    else
                                    {
                                        Row_i += 1;
                                    }
                                    if (zwdm % iRow == 0 && zwdm >= iRow)
                                    {
                                        Column_i += 1;
                                        if ((Column_i + 1) % 2 == 0 && iType == 2) //S型偶数列
                                        {
                                            Row_i = iRow - 1;
                                        }
                                        else
                                        {
                                            Row_i = 0;
                                        }
                                    }
                                    zwdm += 1;
                                }
                            }

                            //如果循环一次还有数据，则重新循环。
                            if (dRow == dtks.Rows.Count)
                            {
                                //重头开始循环
                                dRow = 0;
                                if (number == dtks.Rows.Count && number > 0)
                                    ij += 1;
                                if (ij > 1)
                                    iFlag = true;
                                if (ij > 1000)
                                    break;

                                number = dtks.Rows.Count;
                            }

                            if (sql.Length > 10000)
                            {
                                this._dbHelper.execSql_Tran(sql);
                                sql = "";
                            }

                        }
                        if (sql.Length > 0)
                        {
                            this._dbHelper.execSql_Tran(sql);
                            sql = "";
                        }
                    }
                    this._dbHelper.execSql_Tran(" update zk_kd set isbp=1,isxx=" + type + " where kddm='" + kddm + "' ");
                }

                this._dbHelper.EndTran(true);
                return true;
            }
            catch (Exception exe)
            {
                this._dbHelper.EndTran(false);
            }
            return false;
        }
        #endregion


        /// <summary>
        /// 根据县区代码查询
        /// </summary>
        /// <param name="xqdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable Select_zk_xxdmXQ(string xqdm)
        {
            string error = "";
            bool bReturn = false;

            Model_zk_xxdm info = new Model_zk_xxdm();
            string sql = "select xqdm,xxdm,xxmc='['+xxdm+']'+xxmc  from zk_xxdm where xqdm=@xqdm   order by xxdm ";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",xqdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);

            return dt;
        }


        /// <summary>
        /// 根据毕业中学查询开始结束号
        /// </summary> 
        public DataTable Select_ksh_M(string xxdm)
        {
            string error = "";
            bool bReturn = false;

            Model_zk_xxdm info = new Model_zk_xxdm();
            string sql = " select  min(ksh) as minksh,max(ksh) as maxksh from  zk_ksxxgl where bmddm=@xxdm ";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xxdm",xxdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);

            return dt;
        }

        /// <summary>
        /// 根据毕业中学查询开始结束号
        /// </summary> 
        public DataTable Select_kd_xx(string kddm,string xxdm)
        {
            string error = "";
            bool bReturn = false;

            Model_zk_xxdm info = new Model_zk_xxdm();
            string sql = " select * from  zk_kd_xx where kddm=@kddm and xxdm=@xxdm ";
            List<SqlParameter> lisP = new List<SqlParameter>(){
             new SqlParameter("@kddm",kddm),
 			 new SqlParameter("@xxdm",xxdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);

            return dt;
        }



        /// <summary>
        /// 根据县区代码查询
        /// </summary>
        /// <param name="xqdm"></param> 
        public DataTable Select_zk_kd(string xqdm)
        {
            string error = "";
            bool bReturn = false;

            Model_zk_xxdm info = new Model_zk_xxdm();
            string sql = "select kddm,kdmc='['+kddm+']'+kdmc  from zk_kd where xqdm=@xqdm   order by kddm ";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",xqdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);

            return dt;
        }



        /// <summary>
        /// 按考点代码查询单个考点资料，转换为实体类
        /// </summary>
        /// <param name="kddm">考点代码</param>
        /// <returns></returns>
        public Model_zk_kd ViewDisp(string kddm)
        {
            Model_zk_kd info = new Model_zk_kd();

            string sql = " select * from zk_kd where kddm =@kddm ";
            List<SqlParameter> lisP = new List<SqlParameter>();
            SqlParameter Kddm = new SqlParameter("@kddm", SqlDbType.VarChar);
            Kddm.Value = kddm;
            lisP.Add(Kddm);

            string error = "";
            bool bReturn = false;
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
            {
                info = _dbHelper.DT2EntityList<Model_zk_kd>(dt)[0];
            }
            return info;
        }

        #region 执行分页存储过程，返回记录总数和当前页的数据。
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
            string tabName = " View_kdzs ";
            //要查询的字段
            string reField = " * ";
            //排序字段
            string orderStr = " kddm ";
            //排序标识（0、升序；1、降序）
            int orderType = 0;
            //执行成功与失败的标识（true、表示执行成功；false、表示执行失败）
            bool bFlag = false;
            decimal dec = 0;
            DataSet dst = _dbHelper.ExecuteProc(tabName, reField, orderStr, "", where, pageSize, pageIndex, orderType, 0, ref dec, ref totalRecord, ref bFlag);
            if (bFlag)
            {
                if (dst.Tables.Count > 0)
                {
                    return dst.Tables[0];
                }
            }
            return null;
        }

        #endregion

        #region 执行分页存储过程，返回记录总数和当前页的数据。
        /// <summary>
        /// 执行分页存储过程，返回记录总数和当前页的数据。
        /// </summary>
        /// <param name="where">执行的条件</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="totalRecord">返回的记录总数</param>
        public DataTable ExecuteProc_View_kczwinfo(string where, int pageSize, int pageIndex, ref int totalRecord)
        {
            //string sql="select  from AppraisalPersonnel";

            //数据库表名
            string tabName = " View_kczwinfo ";
            //要查询的字段
            string reField = " * ";
            //排序字段
            string orderStr = " ksh ";
            //排序标识（0、升序；1、降序）
            int orderType = 0;
            //执行成功与失败的标识（true、表示执行成功；false、表示执行失败）
            bool bFlag = false;
            decimal dec = 0;
            DataSet dst = _dbHelper.ExecuteProc(tabName, reField, orderStr, "", where, pageSize, pageIndex, orderType, 0, ref dec, ref totalRecord, ref bFlag);
            if (bFlag)
            {
                if (dst.Tables.Count > 0)
                {
                    return dst.Tables[0];
                }
            }
            return null;
        }

        #endregion



        #region 执行分页存储过程，返回记录总数和当前页的数据。
        /// <summary>
        /// 执行分页存储过程，返回记录总数和当前页的数据。
        /// </summary>
        /// <param name="where">执行的条件</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="totalRecord">返回的记录总数</param>
        public DataTable ExecuteProc_View_kd_bmd(string where, int pageSize, int pageIndex, ref int totalRecord)
        {
            //string sql="select  from AppraisalPersonnel";

            //数据库表名
            string tabName = " View_kd_bmd ";
            //要查询的字段
            string reField = " * ";
            //排序字段
            string orderStr = " ksh ";
            //排序标识（0、升序；1、降序）
            int orderType = 0;
            //执行成功与失败的标识（true、表示执行成功；false、表示执行失败）
            bool bFlag = false;
            decimal dec = 0;
            DataSet dst = _dbHelper.ExecuteProc(tabName, reField, orderStr, "", where, pageSize, pageIndex, orderType, 0, ref dec, ref totalRecord, ref bFlag);
            if (bFlag)
            {
                if (dst.Tables.Count > 0)
                {
                    return dst.Tables[0];
                }
            }
            return null;
        }

        #endregion

        /// <summary>
        /// 查询报名号是否存在。
        /// </summary>        
        public DataTable select_ksh(string where)
        {
            string sql = "";
            sql = " select a.ksh,kddm from zk_ksxxgl as a left outer join zk_kd_bmd as b on a.ksh=b.ksh where " + where;

            DataTable tab = _dbHelper.selectTab(sql, ref error, ref bReturn);
            return tab;
        }

         /// <summary>
        /// 删除考点的考生
        /// </summary> 
        public bool Deleteks(string strwhere)
        {
            string sql = " delete from  zk_kd_bmd where " + strwhere;
             
            _dbHelper.ExecuteNonQuery(sql,  ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        } 

        

        /// <summary>
        /// 添加报名号到考点
        /// </summary> 
        public bool Insert_kd_bmd(string kddm, string ksh)
        {
            string sql = " insert into zk_kd_bmd(ksh,kddm) values(@ksh,@kddm) ";
            List<SqlParameter> lisP = new List<SqlParameter>(){
             new SqlParameter("@kddm",kddm),
 			 new SqlParameter("@ksh",ksh)};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        } 


        #region 执行分页存储过程，返回记录总数和当前页的数据。
        /// <summary>
        /// 执行分页存储过程，返回记录总数和当前页的数据。
        /// </summary>
        /// <param name="where">执行的条件</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="totalRecord">返回的记录总数</param>
        public DataTable ExecuteProc_View_ksz(string where, int pageSize, int pageIndex, ref int totalRecord)
        {
            //string sql="select  from AppraisalPersonnel";

            //数据库表名
            string tabName = " View_ksz ";
            //要查询的字段
            string reField = " * ";
            //排序字段
            string orderStr = " xqdm ";
            //排序标识（0、升序；1、降序）
            int orderType = 0;
            //执行成功与失败的标识（true、表示执行成功；false、表示执行失败）
            bool bFlag = false;
            decimal dec = 0;
            DataSet dst = _dbHelper.ExecuteProc(tabName, reField, orderStr, "", where, pageSize, pageIndex, orderType, 0, ref dec, ref totalRecord, ref bFlag);
            if (bFlag)
            {
                if (dst.Tables.Count > 0)
                {
                    return dst.Tables[0];
                }
            }
            return null;
        }

        #endregion


        /// <summary>
        /// 根据毕业中学查询打印考试证
        /// </summary>
        /// <param name="xqdm"></param> 
        public DataTable Select_ksz(string where)
        {
            string error = "";
            bool bReturn = false;
            string sql = "select *,ROW_NUMBER() over (order by ksh) as 'num' from   View_ksz_print where " + where + " order by ksh asc ";
            DataTable dt = _dbHelper.selectTab(sql,  ref   error, ref   bReturn);

            return dt;
        }

        /// <summary>
        /// 根据考点查询所有考场号
        /// </summary>
        /// <param name="xqdm"></param> 
        public DataTable Select_kc(string where)
        {
            string error = "";
            bool bReturn = false;
            string sql = "select  kcdm,xqdm,kddm,xqmc,kdmc from   View_ksz_print where " + where + "  group by  kcdm ,xqdm,kddm,xqmc,kdmc order by kcdm,xqdm,kddm,xqmc,kdmc asc";
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);

            return dt;
        }

        /// <summary>
        /// 根据考场查询座位
        /// </summary>
        /// <param name="xqdm"></param> 
        public DataTable Select_zwh(string where)
        {
            string error = "";
            bool bReturn = false;
            string sql = "select *,'' as kaoci,ID=Convert(int,zwh),IDSS=Convert(int,right(kcdm,3)) from   View_ksz_print where " + where;
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);

            return dt;
        }

        /// <summary>
        /// 门贴
        /// </summary>
        /// <param name="xqdm"></param> 
        public DataTable Select_mt(string where)
        {
            string error = "";
            bool bReturn = false;
            string sql = " select kcdm,xqdm,kddm,xqmc,kdmc,[MIN],[MAX],kaoci from View_mentie_print  where " + where + " group by kcdm,xqdm,kddm,xqmc,kdmc,[MIN],[MAX],kaoci order by kcdm,xqdm,kddm,xqmc,kdmc,[MIN],[MAX] asc ";
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);

            return dt;
        }

        /// <summary>
        /// 导出DBF
        /// </summary>
        /// <param name="xqdm"></param> 
        public DataTable Select_DBF(string where)
        {
            string error = "";
            bool bReturn = false;
            string sql = "select  * from   View_ksz_print where " + where + "    order by zkzh asc";
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);

            return dt;
        }
        /// <summary>
        /// Excel数据导入到SqlServer数据库方法
        /// </summary>
        /// <param name="excelFilePath">excel文件存放路径</param>
        /// <param name="importSqlCmd">需要用于导入数据的sql语句</param>
        /// <param name="importParams">用于导入数据的sql参数列表</param>
        /// <param name="isZL">是否增量导入</param>
        /// <returns></returns>
        public string ImportExcelData(string excelFilePath,string kddm)
        {
            try
            {
                if (!File.Exists(excelFilePath))
                    return "导入失败，原因是：输入的文件路径不正确或指定的文件不存在。";
                ExcelQueryFactory excelFile = new ExcelQueryFactory(excelFilePath);
                ExcelQueryable<Row> excel = excelFile.Worksheet();
                SqlDbHelper_1 dbHelp = new SqlDbHelper_1();
                StringBuilder resultMsg = new StringBuilder();

                 string sql = " insert into zk_kd_bmd(ksh,kddm) values(@ksh,@kddm) ";
              
                bool bReturn = true;

                int i = 0;
                int lost = 0;
                int finish = 0;

                foreach (var element in excel)
                {
                    if (element.ColumnNames.Count() != 1)
                        return "导入失败，原因是：输入的文件路径格式不对应，目标格式为1列，您输入的是：" + element.ColumnNames.Count() + "列的格式。";

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
                    DataTable dt = select_ksh(" a.ksh='" + element[0].ToString() + "'");
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["kddm"].ToString().Length > 0)
                        {
                            resultMsg.Append("第" + (i + 1) + "行错误，原因：该报名号已编排!\r\n");
                            lost++;
                            i++;
                            continue;
                        }
                    }
                    List<SqlParameter> lisP = new List<SqlParameter>(){
                       new SqlParameter("@kddm",kddm),
 			           new SqlParameter("@ksh",element[0].ToString())};
                    _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
 

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

                return "数据有误!";

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

        /// <summary>
        /// 毕业中学考生来源
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable Select_bmd(string kddm)
        {
            string error = "";
            bool bReturn = false;
            string sql = "select * from  View_ksz where kddm=@kddm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@kddm",kddm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);

            return dt;
        }
        /// <summary>
        /// 县区毕业中学 
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable Select_bmddm(string xqdm)
        {
            string error = "";
            bool bReturn = false;
            string sql = "select bmddm,bmdmc from  View_ksz  where xqdm=@xqdm  group by bmddm,bmdmc order by bmddm asc";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",xqdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);

            return dt;
        }
         /// <summary>
        /// 所有考点
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable Select_allkd(string where)
        {
            string error = "";
            bool bReturn = false;
            string sql = "select * from  View_kdzs where " + where;
           
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);

            return dt;
        }
        /// <summary>
        /// 考试时间
        /// </summary>
      
        /// <returns></returns>
        public Model_zk_kstime zk_kstime()
        {
            Model_zk_kstime info = new Model_zk_kstime();

            string sql = " select * from zk_kstime ";
            string error = "";
            bool bReturn = false;
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
            {
                info = _dbHelper.DT2EntityList<Model_zk_kstime>(dt)[0];
            }
            return info;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="item"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool Insert_zk_kstime(Model_zk_kstime item)
        {

            string sql = "insert into zk_kstime(t1,s1,x1,t2,s2,x2,t3,s3,x3,m1) values(@t1,@s1,@x1,@t2,@s2,@x2,@t3,@s3,@x3,@m1)";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@t1",item.T1),
 			 new SqlParameter("@s1",item.S1),
              new SqlParameter("@x1",item.X1),
                new SqlParameter("@t2",item.T2),
 			 new SqlParameter("@s2",item.S2),
              new SqlParameter("@x2",item.X2),
               new SqlParameter("@t3",item.T3),
 			 new SqlParameter("@s3",item.S3),
              			 new SqlParameter("@m1",item.M1),
              new SqlParameter("@x3",item.X3)    
			};
            int i = _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
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
        /// 修改
        /// </summary>
        /// <param name="item"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool Up_zk_kstime(Model_zk_kstime item)
        {

            string sql = "update  zk_kstime set m1=@m1, t1=@t1,s1=@s1,x1=@x1,t2=@t2,s2=@s2,x2=@x2,t3=@t3,s3=@s3,x3=@x3";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@t1",item.T1),
 			 new SqlParameter("@s1",item.S1),
              new SqlParameter("@x1",item.X1),
                new SqlParameter("@t2",item.T2),
 			 new SqlParameter("@s2",item.S2),
              new SqlParameter("@x2",item.X2),
               new SqlParameter("@t3",item.T3),
 			 new SqlParameter("@s3",item.S3),
              new SqlParameter("@m1",item.M1),
              new SqlParameter("@x3",item.X3)    
			};
            int i = _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
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
        /// 考试时间
        /// </summary>

        /// <returns></returns>
        public DataTable zk_kstime_tab()
        {
            
            string sql = " select * from zk_kstime ";
            string error = "";
            bool bReturn = false;
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);

            return dt;
        }
    }
}
