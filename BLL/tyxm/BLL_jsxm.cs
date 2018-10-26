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
  public      class BLL_jsxm
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
        public bool Insert_zk_jsks( string name)
        {
            string sql = "insert into zk_jsks(name) values(@name)";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 
 			 new SqlParameter("@name",name)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        public bool Update_zk_jsks(string id, string name)
        {
            string sql = "update   zk_jsks set name=@name where id=@id";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@id",id),
 			 new SqlParameter("@name",name)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
       /// <summary>
       /// 查询体育库
       /// </summary>
       /// <param name="where"></param>
       /// <returns></returns>
        public DataTable seleczk_jsks(string where)
        {


            string sql = "select * from zk_jsks where  " + where;
            DataTable tab = _dbHelper.selectTab(sql, ref error, ref bReturn);
            return tab;
        }
        /// <summary>
        /// 执行分页存储过程，返回记录总数和当前页的数据。综合评价 
        /// </summary>
        /// <param name="where">执行的条件</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="totalRecord">返回的记录总数</param>
        public DataTable ExecuteProcss(string where, int pageSize, int pageIndex, ref int totalRecord)
        {
            //string sql="select  from AppraisalPersonnel";

            //数据库表名
            string tabName = " zk_jsks ";
            //要查询的字段
            string reField = "  *  ";
            //排序字段
            string orderStr = "   id ";
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
        public bool Delete_zk_jsks(string where)
        {
            string sql = "Delete  zk_jsks where "+ where ;
            
            _dbHelper.ExecuteNonQuery(sql, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
       /// <summary>
       /// 查询体育成绩库
       /// </summary>
       /// <param name="where"></param>
       /// <returns></returns>
        public DataTable selzk_ksjsks(string where)
        {
            string sql = "select * from zk_ksjsks where  " + where;
            DataTable tab = _dbHelper.selectTab(sql, ref error, ref bReturn);
            return tab;
        }
      /// <summary>
      /// 新增考生体育信息
      /// </summary>
      /// <param name="lxId"></param>
      /// <param name="name"></param>
      /// <returns></returns>
        public bool Insert_zk_ksjsks(string ksh, int name)
        {
            string sql = "insert into zk_ksjsks (ksh,bxxm,ksjsqr) values (@ksh,@name,1)";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@ksh",ksh),
 			 new SqlParameter("@name",name)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 联合查询考生体育库
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable selecksh(string ksh)
        {
            string sql = " select * from View_jsks  where ksh='" + ksh + "'";
            DataTable tab = _dbHelper.selectTab(sql, ref error, ref bReturn);
            return tab;
        }

        public bool Update_zk_ksjsks(string bxxm,  string ksh)
        {
            string sql = "update zk_ksjsks set bxxm=@bxxm where ksh=@ksh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@bxxm",bxxm),
 			 new SqlParameter("@ksh",ksh)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }

        public bool Update_ksqr(string ksh)
        {
            string sql = "update zk_ksjsks set ksjsqr=2 where ksh=@ksh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 
 			 new SqlParameter("@ksh",ksh)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 执行分页存储过程，返回记录总数和当前页的数据。会考成绩 
        /// </summary>
        /// <param name="where">执行的条件</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="totalRecord">返回的记录总数</param>
        public DataTable ExecuteProcHKCJ(string where, int pageSize, int pageIndex, ref int totalRecord)
        {
            //string sql="select  from AppraisalPersonnel";

            //数据库表名
            string tabName = " View_jsks ";
            //要查询的字段 A.ksh,A.Dldj ,A.Swdj ,A.xm,B.xxqr
            string reField = " *  ";
            //排序字段
            string orderStr = "  ksh ";
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
        public bool Delete_zk_ksjsks(List<string> ksh)
        {
            string inStr = "";

            foreach (var str in ksh)
            {
                inStr += "'" + str + "',";

            }

            string sqlCmd = "Delete zk_ksjsks Where ksh In (" + inStr.Substring(0, inStr.Length - 1) + ") ";
            _dbHelper.ExecuteNonQuery(sqlCmd, ref error, ref bReturn);

            if (bReturn) return true;
            else return false;
        }

        /// <summary>
        /// 加试导出
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable selecjsDaochu(string where )
        {
            string sql = "select ksh as '报名号',xm as '姓名',bmdmc as '毕业中学学校', bjmc as '班级',isnull(bxmc,'') as '加试专业'  from View_jsks where " + where;
            DataTable tab = _dbHelper.selectTab(sql, ref error, ref bReturn);
            return tab;
        }
    }
}
