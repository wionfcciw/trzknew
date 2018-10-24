using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
 
using DAL;
using Model;
namespace BLL 
{
    /// <summary>
    /// 设置报名时间
    /// </summary>
    public class BLL_zk_szbmsj
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
        public bool Insert_zk_szbmsj(Model_zk_szbmsj item)
        {
            string sql = "insert into zk_szbmsj(xqdm,kssj,jssj,kssj_zy,jssj_zy,kssj_ty,jssj_ty,kssj_js,jssj_js) values(@xqdm,@kssj,@jssj,@kssj_zy,@jssj_zy,@kssj_ty,@jssj_ty,@kssj_js,@jssj_js)";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@lsh",item.Lsh),
 			 new SqlParameter("@xqdm",item.Xqdm),
 			
 			 new SqlParameter("@kssj",item.Kssj),
 			 new SqlParameter("@jssj",item.Jssj),
              new SqlParameter("@kssj_zy",item.Kssj_zy),
 			 new SqlParameter("@jssj_zy",item.Jssj_zy),
              new SqlParameter("@kssj_ty",item.Kssj_ty),
 			 new SqlParameter("@jssj_ty",item.Jssj_ty),
              new SqlParameter("@kssj_js",item.Kssj_js),
 			 new SqlParameter("@jssj_js",item.Jssj_js)
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
        public bool Insert_zk_szbmsj(List<Model_zk_szbmsj> Listitem)
        {
            string sql = "insert into zk_szbmsj(xqdm,kssj,jssj) values(@xqdm,@kssj,@jssj)";
            List<SqlParameter> lisP = new List<SqlParameter>();

            SqlParameter Xqdm = new SqlParameter("@xqdm", SqlDbType.VarChar);
           
            SqlParameter Kssj = new SqlParameter("@kssj", SqlDbType.DateTime);
            SqlParameter Jssj = new SqlParameter("@jssj", SqlDbType.DateTime);
            foreach (Model_zk_szbmsj item in Listitem)
            {

                Xqdm.Value = item.Xqdm;
               
                Kssj.Value = item.Kssj;
                Jssj.Value = item.Jssj;
                lisP.Clear();
                lisP.Add(Xqdm);
             
                lisP.Add(Kssj);
                lisP.Add(Jssj);
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
        public DataTable Select_zk_szbmsj(ref string error, ref bool bReturn)
        {
            string sql = "select * from zk_szbmsj";
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);
            return dt;
        }

      /// <summary>
      /// 通过xqdm 查询单个市/区
      /// </summary> 
        public Model_zk_szbmsj Select_zk_szbmsj(string xqdm)
        {
            Model_zk_szbmsj info = new Model_zk_szbmsj();
            string sql = "select * from zk_szbmsj where xqdm=@xqdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",xqdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
                info = _dbHelper.DT2EntityList<Model_zk_szbmsj>(dt)[0];
            return info;
        }

        /// <summary>
        /// 查询大市
        /// </summary> 
        public Model_zk_szbmsj SelectDispBig()
        {
            Model_zk_szbmsj info = new Model_zk_szbmsj();
            string sql = "select * from zk_szbmsj where xqdm like '_00'"; 
            DataTable dt = _dbHelper.selectTab(sql,  ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
                info = _dbHelper.DT2EntityList<Model_zk_szbmsj>(dt)[0];
            return info;
        }

        /// <summary>
        /// 通过xqdm 查询单个市/区
        /// </summary> 
        public bool SelectDisp(string xqdm)
        {
            Model_zk_szbmsj info = new Model_zk_szbmsj();
            string sql = "select * from zk_szbmsj where xqdm=@xqdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",xqdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
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
      /// 自定义修改
      /// </summary>
      /// <param name="set"></param>
      /// <param name="where"></param>
      /// <param name="error"></param>
      /// <param name="bReturn"></param>
      /// <returns></returns>
        public bool update_zk_szbmsj(string set, string where)
        {
            string sql = "update  zk_szbmsj set " + set + " where " + where;
            _dbHelper.ExecuteNonQuery(sql, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }

      /// <summary>
      /// 根据县区填报志愿时间
      /// </summary>
      /// <param name="item"></param>
      /// <param name="error"></param>
      /// <param name="bReturn"></param>
      /// <returns></returns>
        public bool update_zk_szbmsj_zy(Model_zk_szbmsj item)
        {
            string sql = "update  zk_szbmsj set  kssj_zy=@kssj_zy,jssj_zy=@jssj_zy where  xqdm=@xqdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",item.Xqdm),
 	 	 
             new SqlParameter("@kssj_zy",item.Kssj_zy),
 			 new SqlParameter("@jssj_zy",item.Jssj_zy)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 根据县区自主申报时间
        /// </summary>
        /// <param name="item"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool update_zk_szbmsj_sb(Model_zk_szbmsj item)
        {
            string sql = "update  zk_szbmsj set  kssj_sb=@kssj_zy,jssj_sb=@jssj_zy where  xqdm=@xqdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",item.Xqdm),
             new SqlParameter("@kssj_zy",item.Kssj_sb),
 			 new SqlParameter("@jssj_zy",item.Jssj_sb)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 根据县区修改报名时间
        /// </summary>
        /// <param name="item"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool update_zk_szbmsj_bm(Model_zk_szbmsj item)
        {
            string sql = "update  zk_szbmsj set   kssj=@kssj,jssj=@jssj where  xqdm=@xqdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",item.Xqdm),
 	 		 new SqlParameter("@kssj",item.Kssj),
 			 new SqlParameter("@jssj",item.Jssj) 
      
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 根据县区修改体育报名
        /// </summary>
        /// <param name="item"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool update_zk_szbmsjTY(Model_zk_szbmsj item)
        {
            string sql = "update  zk_szbmsj set  kssj_ty=@kssj_ty,jssj_ty =@jssj_ty where  xqdm=@xqdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",item.Xqdm),
 	 		 
                  new SqlParameter("@kssj_ty",item.Kssj_ty),
 			 new SqlParameter("@jssj_ty",item.Jssj_ty)
          
 			 
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 根据县区修改加试报名
        /// </summary>
        /// <param name="item"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool update_zk_szbmsjJS(Model_zk_szbmsj item)
        {
            string sql = "update  zk_szbmsj set  kssj_js=@kssj_js,jssj_js =@jssj_js where  xqdm=@xqdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",item.Xqdm),
 	 		 
                  new SqlParameter("@kssj_js",item.Kssj_js),
 			 new SqlParameter("@jssj_js",item.Jssj_js)
          
 			 
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
        public bool delete_zk_szbmsj(string lsh)
        {
            string sql = "delete  zk_szbmsj where lsh=@lsh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@lsh",lsh)};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }

        /// <summary>
        /// 显示报名时间设置列表
        /// </summary> 
        public DataTable Select(string where)
        {
            string sql="";
            if (where.Length == 0)
            {
                sql = "select xqdm='['+A.xqdm+'] '+B.xqmc ,kssj,jssj,kssj_zy,jssj_zy,kssj_ty,jssj_ty,kssj_js,jssj_js,kssj_sb,jssj_sb  from zk_szbmsj as A inner join  zk_xqdm as B on A.xqdm=B.xqdm order by A.xqdm ";
            }
            else
            {
                sql = "select xqdm='['+A.xqdm+'] '+B.xqmc ,kssj,jssj,kssj_zy,jssj_zy,kssj_ty,jssj_ty,kssj_js,jssj_js,kssj_sb,jssj_sb   from zk_szbmsj as A inner join  zk_xqdm as B on A.xqdm=B.xqdm where " + where + " order by A.xqdm ";
            }
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);

            return dt;
        }
        
    }
}
 