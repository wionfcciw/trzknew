using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model;
using System.Data.SqlClient;
using System.Data;
namespace BLL
{
    /// <summary>
    /// 考次代码控制类
    /// </summary>
   public class BLL_zk_kcdm
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
        public bool Insert_zk_kcdm(Model_zk_kcdm item)
        {
            string sql = "insert into zk_kcdm(kcdm,kcmc) values(@kcdm,@kcmc)";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 	
 			 new SqlParameter("@kcdm",item.Kcdm),
 			 new SqlParameter("@kcmc",item.Kcmc)
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
        public bool Insert_zk_kcdm(List<Model_zk_kcdm> Listitem)
        {
            string sql = "insert into zk_kcdm(kcdm,kcmc) values(@kcdm,@kcmc)";
            List<SqlParameter> lisP = new List<SqlParameter>();
           
            SqlParameter Kcdm = new SqlParameter("@kcdm", SqlDbType.VarChar);
            SqlParameter Kcmc = new SqlParameter("@kcmc", SqlDbType.VarChar);
            foreach (Model_zk_kcdm item in Listitem)
            {
              
                Kcdm.Value = item.Kcdm;
                Kcmc.Value = item.Kcmc;
                lisP.Clear();
               
                lisP.Add(Kcdm);
                lisP.Add(Kcmc);
                _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            }
            if (bReturn) return true;
            else return false;
        }
       /// <summary>
       /// 查询所有
       /// </summary>
       /// <param name="error"></param>
       /// <param name="bReturn"></param>
       /// <returns></returns>
        public DataTable Select_zk_kcdm()
        {
            string sql = "select * from zk_kcdm order by kcdm desc ";
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);
            return dt;
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
            string tabName = "zk_kcdm";
            //要查询的字段
            string reField = " * ";
            //排序字段
            string orderStr = " kcdm   ";
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

        #endregion 

       /// <summary>
       /// 根据考次ID查询
       /// </summary>
       /// <param name="kcId"></param>
       /// <param name="error"></param>
       /// <param name="bReturn"></param>
       /// <returns></returns>
        public Model_zk_kcdm Select_zk_kcdmId(string kcId)
        {
            Model_zk_kcdm info = new Model_zk_kcdm();
            string sql = "select * from zk_kcdm where kcId=@kcId";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@kcId",kcId)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
                info = _dbHelper.DT2EntityList<Model_zk_kcdm>(dt)[0];
            return info;
        }
       /// <summary>
       /// 根据考次代码查询
       /// </summary>
       /// <param name="kcdm"></param>
       /// <param name="error"></param>
       /// <param name="bReturn"></param>
       /// <returns></returns>
        public Model_zk_kcdm Select_zk_kcdm(string kcdm)
        {
            Model_zk_kcdm info = new Model_zk_kcdm();
            string sql = "select * from zk_kcdm where kcdm=@kcdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@kcdm",kcdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
                info = _dbHelper.DT2EntityList<Model_zk_kcdm>(dt)[0];
            return info;
        }
       /// <summary>
       /// 自定义修改
       /// </summary>
       /// <param name="set"></param>
       /// <param name="where"></param>
       /// <param name="error"></param>
       /// <param name="bReturn"></param>
       /// <returns></returns>
        public bool update_zk_kcdm(string set, string where)
        {
            string sql = "update  zk_kcdm set " + set + " where " + where;
            _dbHelper.ExecuteNonQuery(sql, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
       /// <summary>
       /// 根据考次ID修改
       /// </summary>
       /// <param name="item"></param>
       /// <param name="error"></param>
       /// <param name="bReturn"></param>
       /// <returns></returns>
        public bool update_zk_kcdmID(Model_zk_kcdm item)
        {
            string sql = "update  zk_kcdm set kcmc=@kcmc,kcdm=@kcdm where kcId=@kcId";
            List<SqlParameter> lisP = new List<SqlParameter>(){
                new SqlParameter("@kcId",item.KcId),
 			    new SqlParameter("@kcdm",item.Kcdm),
 			    new SqlParameter("@kcmc",item.Kcmc),
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
       /// <summary>
       /// 根据考次代码修改
       /// </summary>
       /// <param name="item"></param>
       /// <param name="error"></param>
       /// <param name="bReturn"></param>
       /// <returns></returns>
        public bool update_zk_kcdm(Model_zk_kcdm item)
        {
            string sql = "update  zk_kcdm set kcmc=@kcmc where kcdm=@kcdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@kcmc",item.Kcmc),
 			 new SqlParameter("@kcdm",item.Kcdm)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        } 
       /// <summary>
       /// 根据考次代码删除
       /// </summary>
       /// <param name="kcdm"></param>
       /// <param name="error"></param>
       /// <param name="bReturn"></param>
       /// <returns></returns>
        public bool delete_zk_kcdm(string kcdm)
        {
            string sql = "delete  zk_kcdm where kcdm=@kcdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@kcdm",kcdm)};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }

        /// <summary>
        /// 根据多个考次代码删除指定数据
        /// </summary>
        /// <param name="xqdms">需要删除的县区代码列表</param>
        /// <returns></returns>
        public bool DeleteDataByKcdm(List<string> xqdms)
        {
            string inStr = "";

            foreach (var str in xqdms)
                inStr += "'" + str + "',";

            string sqlCmd = "Delete zk_kcdm Where kcId In (Select kcId From zk_xqdm Where kcdm In(" + inStr.Substring(0, inStr.Length - 1) + "))";
            _dbHelper.ExecuteNonQuery(sqlCmd, ref error, ref bReturn);

            if (bReturn) return true;
            else return false;
        }

        /// <summary>
        /// 根据考次代码查询 返回实体类
        /// </summary> 
        public Model_zk_kcdm Disp(string kcdm)
        {
            Model_zk_kcdm info = new Model_zk_kcdm();
            string sql = "select * from zk_kcdm where kcdm=@kcdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@kcdm",kcdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
                info = _dbHelper.DT2EntityList<Model_zk_kcdm>(dt)[0];
            return info;
        }

        public object kcdm { get; set; }
    }
}
