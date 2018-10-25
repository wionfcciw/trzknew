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
   public class BLL_zk_zbsxx
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
        public bool Insert_zk_zbsxx(Model_zk_zbsxx item)
        {
            string sql = "insert into zk_zbsxx(xxdm,zsxxdm,zbssl,pcdm,xqdm) values(@xxdm,@zsxxdm,@zbssl,@pcdm,@xqdm)";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xxdm",item.Xxdm),
 			 new SqlParameter("@zsxxdm",item.Zsxxdm),
 			 new SqlParameter("@zbssl",item.Zbssl),
 			 new SqlParameter("@pcdm",item.Pcdm),
 			 new SqlParameter("@xqdm",item.Xqdm)
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
        public bool Insert_zk_zbsxx(List<Model_zk_zbsxx> Listitem)
        {
            string sql = "insert into zk_zbsxx(xxdm,zsxxdm,zbssl,pcdm,xqdm) values(@xxdm,@zsxxdm,@zbssl,@pcdm,@xqdm)";
            List<SqlParameter> lisP = new List<SqlParameter>();
            SqlParameter Xxdm = new SqlParameter("@xxdm", SqlDbType.VarChar);
            SqlParameter Zsxxdm = new SqlParameter("@zsxxdm", SqlDbType.VarChar);
            SqlParameter Zbssl = new SqlParameter("@zbssl", SqlDbType.Int);
            SqlParameter Pcdm = new SqlParameter("@pcdm", SqlDbType.VarChar);
            SqlParameter Xqdm = new SqlParameter("@xqdm", SqlDbType.VarChar);
            foreach (Model_zk_zbsxx item in Listitem)
            {
                Xxdm.Value = item.Xxdm;
                Zsxxdm.Value = item.Zsxxdm;
                Zbssl.Value = item.Zbssl;
                Pcdm.Value = item.Pcdm;
                Xqdm.Value = item.Xqdm;
                lisP.Clear();
                lisP.Add(Xxdm);
                lisP.Add(Zsxxdm);
                lisP.Add(Zbssl);
                lisP.Add(Pcdm);
                lisP.Add(Xqdm);
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
        public DataTable Select_zk_zbsxx(ref string error, ref bool bReturn)
        {
            string sql = "select * from zk_zbsxx";
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);
            return dt;
        }
      /// <summary>
      /// 根据毕业学校代码查询
      /// </summary>
      /// <param name="xxdm"></param>
      /// <param name="error"></param>
      /// <param name="bReturn"></param>
      /// <returns></returns>
        public DataTable Select_zk_zbsxxBY(string xxdm)
        {
            Model_zk_zbsxx info = new Model_zk_zbsxx();
            string sql = "select * from zk_zbsxx where xxdm=@xxdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xxdm",xxdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
           
            return dt;
        }
       /// <summary>
       /// 根据招生学校查询
       /// </summary>
       /// <param name="zsxxdm"></param>
       /// <param name="error"></param>
       /// <param name="bReturn"></param>
       /// <returns></returns>
        public DataTable Select_zk_zbsxxZS(string zsxxdm)
        {
            Model_zk_zbsxx info = new Model_zk_zbsxx();
            string sql = "select * from zk_zbsxx where zsxxdm=@zsxxdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@zsxxdm",zsxxdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            
            return dt;
        }
       /// <summary>
       /// 根据批次代码
       /// </summary>
       /// <param name="pcdm"></param>
       /// <param name="error"></param>
       /// <param name="bReturn"></param>
       /// <returns></returns>
        public DataTable Select_zk_zbsxxPC(string pcdm)
        {
            Model_zk_zbsxx info = new Model_zk_zbsxx();
            string sql = "select * from zk_zbsxx where pcdm=@pcdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@pcdm",pcdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
           
            return dt;
        }
       /// <summary>
       /// 根据县区代码查询
       /// </summary>
       /// <param name="xqdm"></param>
       /// <param name="error"></param>
       /// <param name="bReturn"></param>
       /// <returns></returns>
        public DataTable Select_zk_zbsxxXQ(string xqdm)
        {
            Model_zk_zbsxx info = new Model_zk_zbsxx();
            string sql = "select * from zk_zbsxx where xqdm=@xqdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",xqdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
           
            return dt;
        }
       /// <summary>
       /// 自定义修改
       /// </summary>
       /// <param name="set"></param>
       /// <param name="where"></param>
       /// <param name="error"></param>
       /// <param name="bReturn"></param>
       /// <returns></returns>
        public bool update_zk_zbsxx(string set, string where)
        {
            string sql = "update  zk_zbsxx set " + set + " where " + where;
            _dbHelper.ExecuteNonQuery(sql, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
       /// <summary>
       /// 根据批次代码修改
       /// </summary>
       /// <param name="item"></param>
       /// <param name="error"></param>
       /// <param name="bReturn"></param>
       /// <returns></returns>
        public bool update_zk_zbsxx(Model_zk_zbsxx item)
        {
            string sql = "update  zk_zbsxx set xxdm=@xxdm,zsxxdm=@zsxxdm,zbssl=@zbssl,xqdm=@xqdm where pcdm=@pcdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xxdm",item.Xxdm),
 			 new SqlParameter("@zsxxdm",item.Zsxxdm),
 			 new SqlParameter("@zbssl",item.Zbssl),
 			 new SqlParameter("@xqdm",item.Xqdm),
 			 new SqlParameter("@pcdm",item.Pcdm)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
       /// <summary>
       /// 根据批次代码删除
       /// </summary>
       /// <param name="pcdm"></param>
       /// <param name="error"></param>
       /// <param name="bReturn"></param>
       /// <returns></returns>
        public bool delete_zk_zbsxxPC(string pcdm)
        {
            string sql = "delete  zk_zbsxx where pcdm=@pcdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@pcdm",pcdm)};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
       /// <summary>
       /// 根据毕业学校删除
       /// </summary>
       /// <param name="xxdm"></param>
       /// <param name="error"></param>
       /// <param name="bReturn"></param>
       /// <returns></returns>
        public bool delete_zk_zbsxxBY(string xxdm)
        {
            string sql = "delete  zk_zbsxx where xxdm=@xxdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xxdm",xxdm)};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
       /// <summary>
       /// 根据招生学校删除
       /// </summary>
       /// <param name="zsxxdm"></param>
       /// <param name="error"></param>
       /// <param name="bReturn"></param>
       /// <returns></returns>
        public bool delete_zk_zbsxxZS(string zsxxdm)
        {
            string sql = "delete  zk_zbsxx where zsxxdm=@zsxxdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@zsxxdm",zsxxdm)};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
       /// <summary>
       /// 根据县区代码删除
       /// </summary>
       /// <param name="xqdm"></param>
       /// <param name="error"></param>
       /// <param name="bReturn"></param>
       /// <returns></returns>
        public bool delete_zk_zbsxxXQ(string xqdm)
        {
            string sql = "delete  zk_zbsxx where xqdm=@xqdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",xqdm)};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
    }
}
