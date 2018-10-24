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
   public class BLL_zk_lqk
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
        public bool Insert_zk_lqk(Model_zk_lqk item)
        {
            //string sql = "insert into zk_lqk(ksh,xm,lqxx,zyxx,pcdm,Type,zf,sfzbs,zhszpj) values(@ksh,@xm,@lqxx,@zyxx,@pcdm,@Type,@zf,@sfzbs,@zhszpj)";
            //List<SqlParameter> lisP = new List<SqlParameter>(){
            // new SqlParameter("@ksh",item.Ksh),
            // new SqlParameter("@xm",item.Xm),
            // new SqlParameter("@lqxx",item.Lqxx),
            // new SqlParameter("@zyxx",item.Zyxx),
            // new SqlParameter("@pcdm",item.Pcdm),
            // new SqlParameter("@Type",item.Type),
            // new SqlParameter("@zf",item.Zf),
            // new SqlParameter("@sfzbs",item.Sfzbs),
            // new SqlParameter("@zhszpj",item.Zhszpj)
            //};
            //_dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            //if (bReturn) return true;
            return false;
        }
       /// <summary>
       /// 批量新增
       /// </summary>
       /// <param name="Listitem"></param>
       /// <param name="error"></param>
       /// <param name="bReturn"></param>
       /// <returns></returns>
        public bool Insert_zk_lqk(List<Model_zk_lqk> Listitem)
        {
            //string sql = "insert into zk_lqk(ksh,xm,lqxx,zyxx,pcdm,Type,zf,sfzbs,zhszpj) values(@ksh,@xm,@lqxx,@zyxx,@pcdm,@Type,@zf,@sfzbs,@zhszpj)";
            //List<SqlParameter> lisP = new List<SqlParameter>();
            //SqlParameter Ksh = new SqlParameter("@ksh", SqlDbType.VarChar);
            //SqlParameter Xm = new SqlParameter("@xm", SqlDbType.VarChar);
            //SqlParameter Lqxx = new SqlParameter("@lqxx", SqlDbType.VarChar);
            //SqlParameter Zyxx = new SqlParameter("@zyxx", SqlDbType.VarChar);
            //SqlParameter Pcdm = new SqlParameter("@pcdm", SqlDbType.VarChar);
            //SqlParameter Type = new SqlParameter("@Type", SqlDbType.VarChar);
            //SqlParameter Zf = new SqlParameter("@zf", SqlDbType.Decimal);
            //SqlParameter Sfzbs = new SqlParameter("@sfzbs", SqlDbType.Bit);
            //SqlParameter Zhszpj = new SqlParameter("@zhszpj", SqlDbType.VarChar);
            //foreach (Model_zk_lqk item in Listitem)
            //{
            //    Ksh.Value = item.Ksh;
            //    Xm.Value = item.Xm;
            //    Lqxx.Value = item.Lqxx;
            //    Zyxx.Value = item.Zyxx;
            //    Pcdm.Value = item.Pcdm;
            //    Type.Value = item.Type;
            //    Zf.Value = item.Zf;
            //    Sfzbs.Value = item.Sfzbs;
            //    Zhszpj.Value = item.Zhszpj;
            //    lisP.Clear();
            //    lisP.Add(Ksh);
            //    lisP.Add(Xm);
            //    lisP.Add(Lqxx);
            //    lisP.Add(Zyxx);
            //    lisP.Add(Pcdm);
            //    lisP.Add(Type);
            //    lisP.Add(Zf);
            //    lisP.Add(Sfzbs);
            //    lisP.Add(Zhszpj);
            //    _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            //}
            //if (bReturn) return true;
            //else
            return false;
        }
       /// <summary>
       /// 查询所有
       /// </summary>
       /// <param name="where"></param>
       /// <param name="pageSize"></param>
       /// <param name="pageIndex"></param>
       /// <param name="totalRecord"></param>
       /// <returns></returns>
        public DataTable Select_zk_lqk(string where, int pageSize, int pageIndex, ref int totalRecord)
        {
            //数据库表名
            string tabName = "zk_lqk";
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
       /// 根据考生查询
       /// </summary>
       /// <param name="ksh"></param>
       /// <param name="error"></param>
       /// <param name="bReturn"></param>
       /// <returns></returns>
        public Model_zk_lqk Select_zk_lqk(string ksh)
        {
            Model_zk_lqk info = new Model_zk_lqk();
            string sql = "select * from zk_lqk where ksh=@ksh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@ksh",ksh)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
                info = _dbHelper.DT2EntityList<Model_zk_lqk>(dt)[0];
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
        public bool update_zk_lqk(string set, string where)
        {
            string sql = "update  zk_lqk set " + set + " where " + where;
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
        public bool update_zk_lqk(Model_zk_lqk item)
        {
            //string sql = "update  zk_lqk set xm=@xm,lqxx=@lqxx,zyxx=@zyxx,pcdm=@pcdm,Type=@Type,zf=@zf,sfzbs=@sfzbs,zhszpj=@zhszpj where ksh=@ksh";
            //List<SqlParameter> lisP = new List<SqlParameter>(){
            // new SqlParameter("@xm",item.Xm),
            // new SqlParameter("@lqxx",item.Lqxx),
            // new SqlParameter("@zyxx",item.Zyxx),
            // new SqlParameter("@pcdm",item.Pcdm),
            // new SqlParameter("@Type",item.Type),
            // new SqlParameter("@zf",item.Zf),
            // new SqlParameter("@sfzbs",item.Sfzbs),
            // new SqlParameter("@zhszpj",item.Zhszpj),
            // new SqlParameter("@ksh",item.Ksh)
            //};
            //_dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            //if (bReturn) return true;
            //else 
            return false;
        }
       /// <summary>
       /// 根据流水号删除
       /// </summary>
       /// <param name="ksh"></param>
       /// <param name="error"></param>
       /// <param name="bReturn"></param>
       /// <returns></returns>
        public bool delete_zk_lqk(string ksh)
        {
            string sql = "delete  zk_lqk where ksh=@ksh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@ksh",ksh)};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
    }
}
