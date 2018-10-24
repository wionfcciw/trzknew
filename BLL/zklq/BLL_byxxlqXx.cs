using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Data.SqlClient;

namespace BLL
{
    /// <summary>
    /// 毕业学校被当前招生学校的录取情况。
    /// </summary>
    public class BLL_byxxlqXx
    {
        private SqlDbHelper_1 _dbA = new SqlDbHelper_1();
        private string _error = "";
        private bool _bReturn = false;

        /// <summary>
        /// 执行分页存储过程，返回记录总数和当前页的数据。
        /// </summary>
        /// <param name="where">执行的条件</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="totalRecord">返回的记录总数</param>
        public DataTable ExecuteProcList(string where, int pageSize, int pageIndex, ref int totalRecord)
        {
            //string sql="select  from AppraisalPersonnel";

            //数据库表名
            string tabName = "View_ksxxgl";
            //要查询的字段
            string reField = "kaocimc, ksh,xm,sfzh,xjh,bmdmc,kslbmc,ksqr,xxqr,xqqr,xxdy,bmdxqdm,byzxdm,bjdm,pic,zyksqr,zyxxqr,zyxqqr,zyxxdy ";
            //排序字段
            string orderStr = " ksh";
            //排序标识（0、升序；1、降序）
            int orderType = 0;
            //执行成功与失败的标识（true、表示执行成功；false、表示执行失败）
            bool bFlag = false;
            decimal dec = 0;
            DataSet dst = _dbA.ExecuteProc(tabName, reField, orderStr, "", where, pageSize, pageIndex, orderType, 0, ref dec, ref totalRecord, ref bFlag);
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
        /// 查询当前批次、当前县区、当前招生学校录取的考生信息。
        /// </summary>
        /// <param name="pcdm">批次代码。</param>
        /// <param name="xqdm">县区代码。</param>
        /// <param name="zsxxdm">招生学校代码。</param>
        public DataTable SelectKsLqXx(string pcdm, string xqdm, string zsxxdm,string where)
        {
           
            //sql = String.Format("select b.td_zt,b.td_pc, lqxx,a.ksh,a.xm,xb=case a.xbdm when 1 then '男' else '女' end,a.bmdxqdm,byzxmc,b.cj,zslx=(case b.sf_zbs when 0 then '非指标生' when 1 then '指标生' else '' end),tfgj=(case sf_tfgj when 1 then '同分跟进' else '' end) ,sf_zbs from zk_ksxxgl a right join zk_lqk b on a.ksh=b.ksh where b.pcdm='{0}' and b.lqxx='{1}' order by lqxx,cj DESC,a.ksh ASC,sf_zbs ASC,a.bmdxqdm ASC", pcdm, zsxxdm);
            string sql = "  select * from View_xxLqxx where lqxx=@lqxx and  pcdm=@pcdm and xqdm=@xqdm  and types=0 and " + where + " order by cj desc";
            List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@lqxx", zsxxdm), new SqlParameter("@pcdm", pcdm), new SqlParameter("@xqdm", xqdm) };
            DataTable tab = new DataTable();
            tab = this._dbA.selectTab(sql, lisP, ref this._error, ref this._bReturn);            
            return tab;
        }
        /// <summary>
        /// 录取
        /// </summary>
        /// <param name="pcdm">ksh。 状态</param>
        public bool XX_TD(string where, int xqtype,int xxtype)
        {
            try
            {
                // _dbA.BeginTran();
                string sql = "update zk_lqk set xx_zt=" + xxtype + ",xq_zt=" + xqtype + ",xqbz='' ,xxbz=''  where td_zt=1 and " + where;
                int iCount = this._dbA.ExecuteNonQuery(sql, ref this._error, ref this._bReturn);          
                StringBuilder stb = new StringBuilder();
                if (_bReturn)
                {
                    //    _dbA.EndTran(true);
                    return true;
                }
                // _dbA.EndTran(false);
                return false;
            }
            catch (Exception exe)
            {
                // _dbA.EndTran(false);
                return false;
            }

        }

        /// <summary>
        /// 审核上传。 
        /// </summary>
        /// <param name="pcdm">ksh。 状态</param>
        public bool XX_UP_PASS(string lqxx, string pcdm, string xqdm)
        {
            try
            {
                string sqltab = " select ksh from zk_lqk where xq_zt=5 and lqxx=@lqxx and  pcdm=@pcdm and xqdm=@xqdm"; //录取
                //  string sqltab2 = " select ksh from zk_lqk where xq_zt=2 and lqxx=@lqxx and  pcdm=@pcdm and td_pc=@tdpc"; //不通过变为发档
                string sqltab3 = " select ksh from zk_lqk where xq_zt=1 and lqxx=@lqxx and  pcdm=@pcdm and xqdm=@xqdm"; //预退的通过变为在库

                string sql = "update zk_lqk set td_zt=5,lqtime=GETDATE()    where td_zt!=5 and ksh in (" + sqltab + ")";
                // string sql2 = "update zk_lqk set td_zt=2,xx_zt=2   where ksh in (" + sqltab2 + ")";
                string sql3 = "update zk_lqk set cj=0,td_zt=0 ,lqxx='',lqzy='',pcdm='',zysx='',xx_zt=0,xq_zt=0,zydm='',xqdm='',types=0,jyzy='',td_pc=''  where ksh in (" + sqltab3 + ")";
                _dbA.BeginTran();

                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@lqxx", lqxx), new SqlParameter("@pcdm", pcdm), new SqlParameter("@xqdm", xqdm) };
                //考生轨迹
                string sqlgj = " insert into   zk_kslqgj   (ksh,username,type,times)  select ksh,'" + SincciLogin.Sessionstu().UserName + "',5,getdate() from zk_lqk   where ksh in (" + sqltab + ")  ";
                _dbA.execSql_Tran(sqlgj, lisP);
                string sqlgj2 = " insert into  zk_kslqgj   (ksh,username,type,times)  select ksh,'" + SincciLogin.Sessionstu().UserName + "',0,getdate() from zk_lqk   where ksh in (" + sqltab3 + ")  ";
                _dbA.execSql_Tran(sqlgj2, lisP);

                int iCount = _dbA.execSql_Tran(sql, lisP);

                int iCount3 = _dbA.execSql_Tran(sql3, lisP);

                StringBuilder stb = new StringBuilder();
                if (iCount > 0 || iCount3 > 0)
                {
                    _dbA.EndTran(true);

                    return true;
                }
                _dbA.EndTran(false);
                return false;
            }
            catch (Exception exe)
            {
                _dbA.EndTran(false);
                return false;
            }
            return false;
        }

        /// <summary>
        /// 审核上传。 
        /// </summary>
        /// <param name="pcdm">ksh。 状态</param>
        public bool XX_UP_PASS_ALL()
        {
            try
            {
                string sqltab = "select ksh from zk_lqk where  td_zt=1 and isnull(lqxx,'')!=''";
                string sql = "update zk_lqk set td_zt=5,lqtime=GETDATE(), xx_zt=4,xq_zt=5  where td_zt=1 and isnull(lqxx,'')!=''";
                _dbA.BeginTran();
                string sqlgj = " insert into   zk_kslqgj   (ksh,username,type,times,xxdm)  select ksh,'" + SincciLogin.Sessionstu().UserName + "',5,getdate(),lqxx from zk_lqk   where ksh in (" + sqltab + ")  ";
                _dbA.execSql_Tran(sqlgj);
              
                int iCount = _dbA.execSql_Tran(sql);
                StringBuilder stb = new StringBuilder();
                if (iCount > 0 )
                {
                    _dbA.EndTran(true);

                    return true;
                }
                _dbA.EndTran(false);
                return false;
            }
            catch (Exception exe)
            {
                _dbA.EndTran(false);
                return false;
            }
            return false;
        }

        /// <summary>
        /// 查询当前批次、当前县区、当前招生学校录取的考生信息。
        /// </summary>
        /// <param name="pcdm">批次代码。</param>
        /// <param name="xqdm">县区代码。</param>
        /// <param name="zsxxdm">招生学校代码。</param>
        public DataSet SelectTDLqXx(string pcdm, string xqdm, string zsxxdm)
        {


            string sql = "  select a.* from View_xxLqxx a  where lqxx=@lqxx and  a.pcdm=@pcdm and a.xqdm=@xqdm  and types=0 ";
            List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@lqxx", zsxxdm), new SqlParameter("@pcdm", pcdm), new SqlParameter("@xqdm", xqdm) };
            DataSet tab = new DataSet();
            tab = this._dbA.selectDataSet(sql, lisP, ref this._error, ref this._bReturn);
            return tab;
        }
    }
}
