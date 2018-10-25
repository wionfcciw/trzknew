using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DAL;
using Model;
namespace BLL
{
   public class BLL_zk_kszyxx
    {
       private SqlDbHelper_1 _dbHelper = new SqlDbHelper_1();
        private string error = "";
        private bool bReturn = false;
       /// <summary>
       /// 新增
       /// </summary>
       /// <param name="item"></param>
       /// <param name="error"></param>
       /// <param name="bReturn"></param>
       /// <returns></returns>
        public bool Insert_zk_kszyxx(Model_zk_kszyxx item)
        {
            string sql = "insert into zk_kszyxx(ksh,zysx,xzdm,pcdm,xxdm,zy1,zy2,zy3,zy4,zy5,zy6,zy7,zyfc,xxfc,lrsj,sfbk) values(@ksh,@zysx,@xzdm,@pcdm,@xxdm,@zy1,@zy2,@zy3,@zy4,@zy5,@zy6,@zy7,@zyfc,@xxfc,@lrsj,@sfbk)";
            List<SqlParameter> lisP = new List<SqlParameter>(){

 			 new SqlParameter("@ksh",item.Ksh),
 			 new SqlParameter("@zysx",item.Zysx),
 			 new SqlParameter("@xzdm",item.Xzdm),
 			 new SqlParameter("@pcdm",item.Pcdm),
 			 new SqlParameter("@xxdm",item.Xxdm),
 			 new SqlParameter("@zy1",item.Zy1),
 			 new SqlParameter("@zy2",item.Zy2),
 			 new SqlParameter("@zy3",item.Zy3),
 			 new SqlParameter("@zy4",item.Zy4),
 			 new SqlParameter("@zy5",item.Zy5),
 			 new SqlParameter("@zy6",item.Zy6),
 			 new SqlParameter("@zy7",item.Zy7),
 			 new SqlParameter("@zyfc",item.Zyfc),
 			 new SqlParameter("@xxfc",item.Xxfc),
 			 new SqlParameter("@lrsj",item.Lrsj),
 			 new SqlParameter("@sfbk",item.Sfbk)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="Listitem"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool Insert_zk_kszyxx(List<Model_zk_kszyxx> Listitem, string ip)
        {
            try
            {


                string sql = "insert into zk_kszyxx(ksh,zysx,xzdm,pcdm,xxdm,zy1,zy2,zy3,zy4,zy5,zy6,zy7,zyfc,xxfc,lrsj,sfbk,kjbs,sfxxfc,xqdm) values(@ksh,@zysx,@xzdm,@pcdm,@xxdm,@zy1,@zy2,@zy3,@zy4,@zy5,@zy6,@zy7,@zyfc,@xxfc,@lrsj,@sfbk,@kjbs,@sfxxfc,@xqdm)";
                
                this._dbHelper.BeginTran();
                int tr = 0;
             
                foreach (Model_zk_kszyxx item in Listitem)
                {
                    List<SqlParameter> lisP = new List<SqlParameter>() {
                    new SqlParameter("@ksh", item.Ksh),
                    new SqlParameter("@zysx", item.Zysx),
                    new SqlParameter("@xzdm",item.Xzdm),
                    new SqlParameter("@pcdm", item.Pcdm),
                    new SqlParameter("@xxdm", item.Xxdm),
                    new SqlParameter("@zy1", item.Zy1),
                    new SqlParameter("@zy2", item.Zy2),
                    new SqlParameter("@zy3", item.Zy3),
                    new SqlParameter("@zy4", item.Zy4),
                    new SqlParameter("@zy5", item.Zy5),
                    new SqlParameter("@zy6", item.Zy6),
                    new SqlParameter("@zy7", item.Zy7),
                    new SqlParameter("@zyfc", item.Zyfc),
                    new SqlParameter("@xxfc", item.Xxfc),
                    new SqlParameter("@lrsj", item.Lrsj),
                    new SqlParameter("@sfbk", item.Sfbk),
                    new SqlParameter("@kjbs", item.Kjbs),
                    new SqlParameter("@sfxxfc", item.Sfxxfc),
                    new SqlParameter("@xqdm", item.Xqdm)
                    };
                    if (item.Xxdm != "")
                    {
                        string sql2 = "delete  zk_kszyxx where ksh=@ksh and pcdm=@pcdm";
                        _dbHelper.execSql_Tran(sql2, lisP);
                        tr = _dbHelper.execSql_Tran(sql, lisP);
                        string sqlgj = " insert into  zk_kslqgj  (ksh,type,times,ip,xxdm,pcdm) values ('" + item.Ksh + "',1,GETDATE(),'" + ip + "','" + item.Xxdm + "','" + item.Pcdm + "')";
                        _dbHelper.execSql_Tran(sqlgj);
                    }
                }
                if (tr > 0)
                {
                    this._dbHelper.EndTran(true);
                    return true;
                }
                else
                {
                    this._dbHelper.EndTran(false);
                    return false;
                }
          
            }
            catch (Exception)
            {
                this._dbHelper.EndTran(false);
            }
            return false;

        }

        /// <summary>
        /// 批量新增zk_zzsbxx
        /// </summary>
        /// <param name="Listitem"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool Insert_zk_zzsbxx(List<Model_zk_kszyxx> Listitem)
        {
            try
            {


                string sql = "insert into zk_zzsbxx(ksh,zysx,xzdm,pcdm,xxdm,zy1,zy2,zy3,zy4,zy5,zy6,zy7,zyfc,xxfc,lrsj,sfbk,kjbs,sfxxfc) values(@ksh,@zysx,@xzdm,@pcdm,@xxdm,@zy1,@zy2,@zy3,@zy4,@zy5,@zy6,@zy7,@zyfc,@xxfc,@lrsj,@sfbk,@kjbs,@sfxxfc)";
                List<SqlParameter> lisP = new List<SqlParameter>();

                SqlParameter Ksh = new SqlParameter("@ksh", SqlDbType.VarChar);
                SqlParameter Zysx = new SqlParameter("@zysx", SqlDbType.Int);
                SqlParameter Xzdm = new SqlParameter("@xzdm", SqlDbType.VarChar);
                SqlParameter Pcdm = new SqlParameter("@pcdm", SqlDbType.VarChar);
                SqlParameter Xxdm = new SqlParameter("@xxdm", SqlDbType.VarChar);
                SqlParameter Zy1 = new SqlParameter("@zy1", SqlDbType.VarChar);
                SqlParameter Zy2 = new SqlParameter("@zy2", SqlDbType.VarChar);
                SqlParameter Zy3 = new SqlParameter("@zy3", SqlDbType.VarChar);
                SqlParameter Zy4 = new SqlParameter("@zy4", SqlDbType.VarChar);
                SqlParameter Zy5 = new SqlParameter("@zy5", SqlDbType.VarChar);
                SqlParameter Zy6 = new SqlParameter("@zy6", SqlDbType.VarChar);
                SqlParameter Zy7 = new SqlParameter("@zy7", SqlDbType.VarChar);
                SqlParameter Zyfc = new SqlParameter("@zyfc", SqlDbType.Bit);
                SqlParameter Xxfc = new SqlParameter("@xxfc", SqlDbType.Bit);
                SqlParameter Lrsj = new SqlParameter("@lrsj", SqlDbType.DateTime);
                SqlParameter Sfbk = new SqlParameter("@sfbk", SqlDbType.Bit);
                SqlParameter Kjbs = new SqlParameter("@kjbs", SqlDbType.VarChar);
                SqlParameter Sfxxfc = new SqlParameter("@sfxxfc", SqlDbType.Bit);
                this._dbHelper.BeginTran();
                foreach (Model_zk_kszyxx item in Listitem)
                {

                    Ksh.Value = item.Ksh;
                    Zysx.Value = item.Zysx;
                    Xzdm.Value = item.Xzdm;
                    Pcdm.Value = item.Pcdm;
                    Xxdm.Value = item.Xxdm;
                    Zy1.Value = item.Zy1;
                    Zy2.Value = item.Zy2;
                    Zy3.Value = item.Zy3;
                    Zy4.Value = item.Zy4;
                    Zy5.Value = item.Zy5;
                    Zy6.Value = item.Zy6;
                    Zy7.Value = item.Zy7;
                    Zyfc.Value = item.Zyfc;
                    Xxfc.Value = item.Xxfc;
                    Lrsj.Value = item.Lrsj;
                    Sfbk.Value = item.Sfbk;
                    Kjbs.Value = item.Kjbs;
                    Sfxxfc.Value = item.Sfxxfc;
                    lisP.Clear();

                    lisP.Add(Ksh);
                    lisP.Add(Zysx);
                    lisP.Add(Xzdm);
                    lisP.Add(Pcdm);
                    lisP.Add(Xxdm);
                    lisP.Add(Zy1);
                    lisP.Add(Zy2);
                    lisP.Add(Zy3);
                    lisP.Add(Zy4);
                    lisP.Add(Zy5);
                    lisP.Add(Zy6);
                    lisP.Add(Zy7);
                    lisP.Add(Zyfc);
                    lisP.Add(Xxfc);
                    lisP.Add(Lrsj);
                    lisP.Add(Sfbk);
                    lisP.Add(Kjbs);
                    lisP.Add(Sfxxfc);
                    _dbHelper.execSql_Tran(sql, lisP);
                }
                this._dbHelper.EndTran(true);
                return true;
            }
            catch (Exception)
            {
                this._dbHelper.EndTran(false);
            }
            return false;

        }
       /// <summary>
       /// 查询全部
       /// </summary>
       /// <param name="error"></param>
       /// <param name="bReturn"></param>
       /// <returns></returns>
        public DataTable Select_zk_kszyxx(string where, int pageSize, int pageIndex, ref int totalRecord)
        {
            //数据库表名
            string tabName = "zk_kszyxx";
            //要查询的字段
            string reField = " *";
            //排序字段
            string orderStr = " ksh";
            //排序标识（0、升序；1、降序）
            int orderType = 1;
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
       /// <summary>
       /// 根据流水号查询
       /// </summary>
       /// <param name="lsh"></param>
       /// <param name="error"></param>
       /// <param name="bReturn"></param>
       /// <returns></returns>
        public Model_zk_kszyxx Select_zk_kszyxxLsh(string lsh)
        {
            Model_zk_kszyxx info = new Model_zk_kszyxx();
            string sql = "select * from zk_kszyxx where lsh=@lsh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@lsh",lsh)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
                info = _dbHelper.DT2EntityList<Model_zk_kszyxx>(dt)[0];
            return info;
        }
       /// <summary>
       /// 根据报名号查询
       /// </summary>
       /// <param name="ksh"></param>
       /// <param name="error"></param>
       /// <param name="bReturn"></param>
       /// <returns></returns>
        public DataTable Select_zk_kszyxxKsh(string ksh)
        {
            Model_zk_kszyxx info = new Model_zk_kszyxx();
            string sql = "select * from zk_kszyxx where ksh=@ksh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@ksh",ksh)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            return dt;
        }
        /// <summary>
        /// 自定义修改
        /// </summary>
        /// <param name="item"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool update_zk_kszyxx(string set, string where)
        {
            string sql = "update  zk_kszyxx set " + set + " where " + where;
            _dbHelper.ExecuteNonQuery(sql, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
    /// <summary>
    /// 根据流水号修改
    /// </summary>
    /// <param name="item"></param>
    /// <param name="error"></param>
    /// <param name="bReturn"></param>
    /// <returns></returns>
        public bool update_zk_kszyxx(Model_zk_kszyxx item)
        {
            string sql = "update  zk_kszyxx set ksh=@ksh,zysx=@zysx,xzdm=@xzdm,pcdm=@pcdm,xxdm=@xxdm,zy1=@zy1,zy2=@zy2,zy3=@zy3,zy4=@zy4,zy5=@zy5,zy6=@zy6,zy7=@zy7,zyfc=@zyfc,xxfc=@xxfc,lrsj=@lrsj,sfbk=@sfbk where lsh=@lsh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@ksh",item.Ksh),
 			 new SqlParameter("@zysx",item.Zysx),
 			 new SqlParameter("@xzdm",item.Xzdm),
 			 new SqlParameter("@pcdm",item.Pcdm),
 			 new SqlParameter("@xxdm",item.Xxdm),
 			 new SqlParameter("@zy1",item.Zy1),
 			 new SqlParameter("@zy2",item.Zy2),
 			 new SqlParameter("@zy3",item.Zy3),
 			 new SqlParameter("@zy4",item.Zy4),
 			 new SqlParameter("@zy5",item.Zy5),
 			 new SqlParameter("@zy6",item.Zy6),
 			 new SqlParameter("@zy7",item.Zy7),
 			 new SqlParameter("@zyfc",item.Zyfc),
 			 new SqlParameter("@xxfc",item.Xxfc),
 			 new SqlParameter("@lrsj",item.Lrsj),
 			 new SqlParameter("@sfbk",item.Sfbk),
 			 new SqlParameter("@lsh",item.Lsh)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
       /// <summary>
       /// 根据流水号删除
       /// </summary>
       /// <param name="lsh"></param>
       /// <param name="error"></param>
       /// <param name="bReturn"></param>
       /// <returns></returns>
        public bool delete_zk_kszyxx(string lsh)
        {
            string sql = "delete  zk_kszyxx where lsh=@lsh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@lsh",lsh)};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 导出志愿数据
        /// </summary>
        /// <param name="ksh"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable ExportData(string where)
        {

            string sql = "select ksh,pcdm,xxdm,zy1,zy2,zysx,kjbs from zk_kszyxx  where " + where + " order by ksh,pcdm,zysx ";

            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);
            return dt;
        }
        /// <summary>
        /// 导出自主申报数据
        /// </summary>
        /// <param name="ksh"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable ExportData_zzsb(string where)
        {

            string sql = "select ksh,pcdm,xxdm,zy1,zy2,zysx,kjbs from zk_zzsbxx  where " + where + " order by ksh,pcdm,zysx ";

            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);
            return dt;
        }
        /// <summary>
        /// 导出志愿表多少个考生
        /// </summary>
        /// <param name="ksh"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable ExportKsh()
        {

            string sql = "select ksh from zk_kszyxx  group by ksh";
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);
            return dt;
        }
    }
}
