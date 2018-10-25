using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;
using System.Data.SqlClient;
using Model;

namespace BLL
{
    /// <summary>
    /// 基础数据字典。
    /// </summary>
    public class BLL_zk_zdxx
    {
        /// <summary>
        /// 数据库操作控制类。
        /// </summary>
        private SqlDbHelper_1 _dbHelper = new SqlDbHelper_1();
        private string error = "";
        private bool bReturn = false;

        #region 执行分页存储过程，返回记录总数和当前页的数据。
        /// <summary>
        /// 执行分页存储过程，返回记录总数和当前页的数据。
        /// </summary>
        /// <param name="iFlag">查询标识(0、表示需要查询字典的大类别；1、表示查询字典大类别下的子类别)</param>
        /// <param name="where">执行的条件</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="totalRecord">返回的记录总数</param>
        public DataTable ExecuteProc(int iFlag,string where, int pageSize, int pageIndex, ref int totalRecord)
        {
            //查询数据库表名
            string tabName = "";
            //要查询的字段
            string reField = "";
            //排序字段
            string orderStr = "";
            switch (iFlag)
            {
                case 0://查询大类别
                    tabName = "zk_zdxx";
                    reField = "zdlbdm,zdlbmc";
                    orderStr = "zdlbdm";
                    break;
                case 1://查询子类别
                    tabName = "zk_zdxxLB";
                    reField = " * ";
                    orderStr = "zlbdm";
                    break;
                default:
                    return null;
            }
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

        /// <summary>
        /// 查询字典代码。
        /// </summary>
        /// <param name="zddm">字典代码。</param>
        public DataTable selectData(string zddm)
        {
            string sql = "select * from zk_zdxxLB where zdlbdm=@zdlbdm and zlbzt=1  ";
            List<SqlParameter> lisP = new List<SqlParameter>();

            SqlParameter Zdlbdm = new SqlParameter("@zdlbdm",SqlDbType.VarChar);
            Zdlbdm.Value = zddm;

            lisP.Add(Zdlbdm);
            string error = "";
            bool bReturn = false;
            DataTable tab = this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);

            //DataRow dr = tab.NewRow();
            //dr["zlbmc"] = "请选择";
            //dr["zlbdm"] = "0";
            //tab.Rows.InsertAt(dr, 0);
            if (bReturn)
            { 
                return tab;
            }
            else
            {
                return null;
            }
        }
        #endregion

        /// <summary>
        /// 新增数据字典信息
        /// </summary>
        /// <param name="item"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool Insert_zk_zdxx(Model_zk_zdxx item)
        {
            string sql = "insert into zk_zdxx(zdlbdm,zdlbmc) values(@zdlbdm,@zdlbmc)";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			    new SqlParameter("@zdlbdm",item.Zdlbdm),
 			    new SqlParameter("@zdlbmc",item.Zdlbmc)
			};

            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) 
                return true;
            else 
                return false;
        }

        /// <summary>
        /// 根据多个字典代码删除指定数据
        /// </summary>
        /// <param name="xqdms">需要删除的字典代码列表</param>
        /// <returns></returns>
        public bool DeleteDataByZdlbdm(List<string> zdlbdm)
        {
            string inStr = "";

            foreach (var str in zdlbdm)
                inStr += "'" + str + "',";

            string sqlCmd = "Delete zk_zdxx Where zdlbdm In (" + inStr.Substring(0, inStr.Length - 1) + ")";
            _dbHelper.ExecuteNonQuery(sqlCmd, ref error, ref bReturn);

            if (bReturn) return true;
            else return false;
        }

        /// <summary>
        /// 修改数据字典信息
        /// </summary>
        /// <param name="item"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool update_zk_zdxx(Model_zk_zdxx item)
        {
            string sql = "update  zk_zdxx set zdlbmc=@zdlbmc where zdlbdm=@zdlbdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			     new SqlParameter("@zdlbmc",item.Zdlbmc),
 			     new SqlParameter("@zdlbdm",item.Zdlbdm)
			};

            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);

            if (bReturn) 
                return true;
            else 
                return false;
        }

        /// <summary>
        /// 根据字典代码查询 返回实体类
        /// </summary> 
        public Model_zk_zdxx Disp(string zdlbdm)
        {
            Model_zk_zdxx info = new Model_zk_zdxx();
            string sql = "select * from zk_zdxx where zdlbdm=@zdlbdm";
            List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@zdlbdm", zdlbdm) };
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
            if (dt.Rows.Count > 0)
                info = _dbHelper.DT2EntityList<Model_zk_zdxx>(dt)[0];
            return info;
        }

        #region 数据字典子类方法

        /// <summary>
        /// 新增数据字典类别信息
        /// </summary>
        /// <param name="item"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool Insert_zk_zdxxLB(Model_zk_zdxxLB item)
        {
            string sql = "insert into zk_zdxxLB(zdlbdm,zlbdm,zlbmc,zlbpx,zlbzt) values(@zdlbdm,@zlbdm,@zlbmc,(Select isnull(Max(zlbpx),0) + 1 From zk_zdxxLB Where zdlbdm='" + item.Zdlbdm + "'),@zlbzt)";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			    new SqlParameter("@zdlbdm",item.Zdlbdm),
 			    new SqlParameter("@zlbdm",item.Zlbdm),
                new SqlParameter("@zlbmc",item.Zlbmc),
 			    new SqlParameter("@zlbzt",item.Zlbzt)
			};

            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 根据多个县区代码删除指定数据
        /// </summary>
        /// <param name="xqdms">需要删除的县区代码列表</param>
        /// <returns></returns>
        public bool DeleteDataByLsh(List<string> zdlbdm)
        {
            string inStr = "";

            foreach (var str in zdlbdm)
                inStr += str + ",";

            string sqlCmd = "Delete zk_zdxxLB Where Lsh In (" + inStr.Substring(0, inStr.Length - 1) + ")";
            _dbHelper.ExecuteNonQuery(sqlCmd, ref error, ref bReturn);

            if (bReturn) return true;
            else return false;
        }

        /// <summary>
        /// 修改数据字典信息
        /// </summary>
        /// <param name="item"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool update_zk_zdxxLB(Model_zk_zdxxLB item)
        {
            string sql = "update  zk_zdxxLB set zlbdm=@zlbdm,zlbmc=@zlbmc,zlbzt=@zlbzt where lsh=@lsh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			     new SqlParameter("@zlbdm",item.Zlbdm),
 			     new SqlParameter("@zlbmc",item.Zlbmc),
                 new SqlParameter("@zlbzt",item.Zlbzt),
 			     new SqlParameter("@lsh",item.Lsh)
			};

            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);

            if (bReturn)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 根据字典代码查询 返回实体类
        /// </summary> 
        public Model_zk_zdxxLB DispZdlb(string lsh)
        {
            Model_zk_zdxxLB info = new Model_zk_zdxxLB();
            string sql = "select * from zk_zdxxLB where lsh=@lsh";
            List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@lsh", lsh) };
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
            if (dt.Rows.Count > 0)
                info = _dbHelper.DT2EntityList<Model_zk_zdxxLB>(dt)[0];
            return info;
        }

        #endregion

        /// <summary>
        /// 查询字典代码。
        /// </summary>
        /// <param name="zddm">字典代码。</param>
        public DataTable selectZdxx(string zdlbdm)
        {
            string sql = "select b.zlbdm,b.zlbmc from zk_zdxx a,zk_zdxxLB b where a.zdlbdm=b.zdlbdm and b.zlbzt=1 and  a.zdlbdm=@zdlbdm ";
            List<SqlParameter> lisP = new List<SqlParameter>();

            SqlParameter Zdlbdm = new SqlParameter("@zdlbdm", SqlDbType.VarChar);
            Zdlbdm.Value = zdlbdm;
            lisP.Add(Zdlbdm);
            string error = "";
            bool bReturn = false;
            DataTable tab = this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);

            //DataRow dr = tab.NewRow();
            //dr["zlbmc"] = "请选择";
            //dr["zlbdm"] = "0";
            //tab.Rows.InsertAt(dr, 0);
            if (bReturn)
            {
                return tab;
            }
            else
            {
                return null;
            }
        }

    }
}
