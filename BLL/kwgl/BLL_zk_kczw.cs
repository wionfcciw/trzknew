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
    /// 考场座位表 控制类
    /// </summary>
    public class BLL_zk_kczw
    {
        private SqlDbHelper_1 _dbHelper = new SqlDbHelper_1();
        private string error = "";
        private bool bReturn = false;


        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="item">实体类</param>
        /// <returns></returns>
        public bool Insert_zk_kczw(Model_zk_kczw item)
        {
            string sql="insert into zk_kczw(ksh,zkzh,kcdm,zwh,kddm) values(@ksh,@zkzh,@kcdm,@zwh,@kddm)";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@ksh",item.Ksh),
 			 new SqlParameter("@zkzh",item.Zkzh),
 			 new SqlParameter("@kcdm",item.Kcdm),
              new SqlParameter("@zwh",item.Zwh),
               new SqlParameter("@kddm",item.Kddm),
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
       


    }
}
