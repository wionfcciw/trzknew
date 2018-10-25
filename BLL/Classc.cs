using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using LinqToExcel;
using LinqToExcel.Query;
using System.Web;
namespace BLL
{
    public class Classc
    {
        private SqlDbHelper_1 _dbHelper = new SqlDbHelper_1();
        private string error = "";
        private bool bReturn = false;
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public DataTable Selzk()
        {
            string sql = "SELECT a.*,yw,sx,yy,lkzh,wkzh,dsdj,ty,zhdj,jf,zf,zzf,zgdx FROM dbo.zk_ksxxgl a left join zk_zkcj b on a.ksh=b.ksh WHERE jzfp=1 ";
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);
            return dt;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public DataTable Selzk2()
        {
            string sql = "SELECT a.*,yw,sx,yy,lkzh,wkzh,dsdj,ty,zhdj,jf,zf,zzf,zgdx FROM dbo.zk_ksxxgl a left join zk_zkcj b on a.ksh=b.ksh WHERE a.ksh in (select ksh from zk_lqk where td_zt=0) ";
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);
            return dt;
        }
        /// <summary>
        /// 查询小批次计划学校信息
        /// </summary>
        /// <param name="dpcId"></param>
        /// <returns></returns>
        public DataTable Select_zy_XX(string pcdm, string xqdm, string where)
        {

            string sql = "select distinct zsxxmc , xxdm from View_zk_zsjh where  pcdm=@pcdm and   jhs>0 and xqdm in(" + xqdm + ") and " + where + "  order by xxdm asc  ";
            List<SqlParameter> lisP = new List<SqlParameter>() { 
                   new SqlParameter("@pcdm", pcdm)
               };

            return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);

        }
        /// <summary>
        /// 查询是否满计划学校
        /// </summary>
        /// <param name="dpcId"></param>
        /// <returns></returns>
        public DataTable Select_zy_XX_syjh(string xqdm, string where)
        {

            string sql = "select * from (SELECT a.xxdm,b.zsxxmc FROM zk_zsjh_xq a  LEFT JOIN zk_zsxxdm b ON a.xxdm=b.zsxxdm   WHERE xqdm LIKE ('%" + xqdm + "%') AND jhs>0  and " + where + " ) a order by xxdm asc  ";
            return this._dbHelper.selectTab(sql, ref error, ref bReturn);

        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="Listitem"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool Insert_zk_kszyxx(Model_zk_kszyxx item)
        {
            try
            {
                string sql = "insert into zk_kszyxx(ksh,zysx,xzdm,pcdm,xxdm,zy1,zy2,zy3,zy4,zy5,zy6,zy7,zyfc,xxfc,lrsj,sfbk,kjbs,sfxxfc,xqdm) values(@ksh,@zysx,@xzdm,@pcdm,@xxdm,@zy1,@zy2,@zy3,@zy4,@zy5,@zy6,@zy7,@zyfc,@xxfc,@lrsj,@sfbk,@kjbs,@sfxxfc,@xqdm)";
                this._dbHelper.BeginTran();
                int tr = 0;

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
                    tr = _dbHelper.execSql_Tran(sql, lisP);
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

    }
}
