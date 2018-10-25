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
    public class BLL_tyxm
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
        public bool Insert_zk_tyks(string lxId, string name)
        {
            string sql = "insert into zk_tyks(lxId,name) values(@lxId,@name)";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@lxId",lxId),
 			 new SqlParameter("@name",name)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        public bool Update_zk_tyks(string id, string name)
        {
            string sql = "update   zk_tyks set name=@name where id=@id";
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
        public DataTable seleczk_tyks(string where)
        {


            string sql = "select * from zk_tyks where  " + where;
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
            string tabName = " zk_tyks ";
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
        public bool Delete_zk_tyks(string where)
        {
            string sql = "Delete  zk_tyks where " + where;

            _dbHelper.ExecuteNonQuery(sql, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 查询体育成绩库
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable selzk_kstyks(string where)
        {
            string sql = "select * from zk_kstyks where  " + where;
            DataTable tab = _dbHelper.selectTab(sql, ref error, ref bReturn);
            return tab;
        }
        /// <summary>
        /// 新增考生体育信息
        /// </summary>
        /// <param name="lxId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Insert_zk_kstyks(string ksh, int name)
        {
            string sql = "insert into zk_kstyks (ksh,cdxm,kstyqr) values (@ksh,@name,1)";
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
            string sql = " select * from View_tyxmks  where ksh='" + ksh + "'";
            DataTable tab = _dbHelper.selectTab(sql, ref error, ref bReturn);
            return tab;
        }

        public bool Update_zk_kstyks(string bxxm, string bxm, string zxxm,string zxxm3, string ksh)
        {
            string sql = "update zk_kstyks set bxxm=@bxxm,bxm=@bxm,zxxm=@zxxm,zxxm3=@zxxm3,kstyqr=1 where ksh=@ksh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@bxxm",bxxm),
 			 new SqlParameter("@bxm",bxm),
              new SqlParameter("@zxxm",zxxm),
                new SqlParameter("@zxxm3",zxxm3),
 			 new SqlParameter("@ksh",ksh)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }

        /// <summary>
        /// 考生确认
        /// </summary>
        /// <param name="ksh">报名号</param>
        /// <returns></returns>
        public bool Update_ksqr(string ksh)
        {
            string sql = "update zk_kstyks set kstyqr=2,ksqrsj=getdate() where ksh=@ksh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 
 			 new SqlParameter("@ksh",ksh)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }


        /// <summary>
        /// 学校、县区确认
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public bool Update_xxqr(string strset,string where)
        {
            string sql = "update zk_kstyks set "+strset+"  where  " + where + " ";

            _dbHelper.ExecuteNonQuery(sql, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }

        /// <summary>
        /// 重置,确认考生状态
        /// </summary> 
        public bool ResetTag(string set, string where)
        {
            string sql = "update zk_kstyks set " + set + " where " + where;
            _dbHelper.ExecuteNonQuery(sql, ref error, ref bReturn);
            if (bReturn)
            {
                return true;
            }
            return false;
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
            string tabName = " View_tyxmks ";
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
        public bool Delete_zk_kstyks(List<string> ksh)
        {
            string inStr = "";

            foreach (var str in ksh)
            {
                inStr += "'" + str + "',";

            }

            string sqlCmd = "Delete zk_kstyks Where ksh In (" + inStr.Substring(0, inStr.Length - 1) + ") ";
            _dbHelper.ExecuteNonQuery(sqlCmd, ref error, ref bReturn);

            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 体育导出
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable selectyDaochu(string where)
        {
            string sql = "  select ksh as '报名号',xm as '姓名',bmdmc as '毕业中学学校', bjmc as '班级',isnull(zxmc,'') as '自选项目1',isnull(bmc,'') as '自选项目2',isnull(zxmc3,'') as '自选项目3'  from View_tyxmks  where " + where;
            DataTable tab = _dbHelper.selectTab(sql, ref error, ref bReturn);
            return tab;
        }
    }
}