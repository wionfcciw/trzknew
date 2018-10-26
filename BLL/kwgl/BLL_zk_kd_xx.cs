using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Data.SqlClient;
using System.Data;
namespace BLL
{
    /// <summary>
    /// 考点学校表控制类
    /// </summary>
    public class BLL_zk_kd_xx
    {
        private SqlDbHelper_1 _dbHelper = new SqlDbHelper_1();
        private string error = "";
        private bool bReturn = false;

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="item">实体类</param>
        /// <returns></returns>
        public bool Insert_zk_kd_xx(Model_zk_kd_xx item)
        {
            string sql = " insert into zk_kd_xx(kddm,xxdm) values(@kddm,@xxdm) ";

            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@kddm",item.Kddm),
 			 new SqlParameter("@kddm",item.Xxdm)
 		 
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
        public bool delete_zk_kd_xx(string kddm)
        {
            string sql = " delete  zk_kd_xx where kddm=@kddm ";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@kddm",kddm)};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }


        /// <summary>
        /// 根据考点代码查询毕业中学学校
        /// </summary>
        /// <param name="kddm">考点代码</param>       
        public DataTable Select(string kddm)
        {
            string sql = "select a.xxdm,b.xxmc from  zk_kd_xx as a,zk_xxdm as b where a.xxdm=b.xxdm and kddm=@kddm order by a.xxdm desc ";           
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@kddm",kddm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            return dt;
        }


    }
}
