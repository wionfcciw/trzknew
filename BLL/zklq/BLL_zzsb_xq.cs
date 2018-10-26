using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;
using System.Data.SqlClient;

namespace BLL
{
    public class BLL_zzsb_xq
    {
        /// <summary>
        /// 数据库操作控制类。
        /// </summary>
        private SqlDbHelper_1 _dbA = new SqlDbHelper_1();
        /// <summary>
        /// 执行错误时返回的错误信息。
        /// </summary>
        private string error = "";
        /// <summary>
        /// 标识：true、表示执行成功，无报错；false、表示执行时报错。
        /// </summary>
        private bool bReturn = false;
        /// <summary>
        /// 查询当前学校的志愿批次信息。
        /// </summary>
        public DataTable selectPcdm()
        {
            try
            {
                //string sql = "select xpc_id=xpcId+'_'+convert(varchar(20),pcLb),xpc_mc='['+pcdm+']  '+xpcXsMc from zk_zydz_xpcxx";
                string sql = " select pcdm,xpc_mc='{'+b.xqmc+'}'+'['+pcdm+']  '+xpcXsMc from zk_zydz_xpcxx a left join zk_xqdm b on LEFT( a.dpcdm,4)=b.xqdm where  LEFT(dpcDm,4)='500' and pcDm in (21,31)  and pcdm in (select pcdm from zk_lqjhk)";//只做了这2个批次
               
                DataTable tab = this._dbA.selectTab(sql,  ref error, ref bReturn);
                return tab;
            }
            catch (Exception exe)
            {
            }
            return null;
        }

        /// <summary>
        /// 加载录取学校收到的发档批次
        /// </summary>
        public DataTable selectXxFdpc(string lqxx, string pcdm)
        {
            try
            {
                //string sql = "select xpc_id=xpcId+'_'+convert(varchar(20),pcLb),xpc_mc='['+pcdm+']  '+xpcXsMc from zk_zydz_xpcxx";
                string sql = "  select ISNULL( td_pc,0) td_pc  from zk_lqk where lqxx=@lqxx and pcdm=@pcdm and ISNULL(td_pc,0)<>0 group by td_pc   order by td_pc desc";//只做了这2个批次
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@lqxx", lqxx), new SqlParameter("@pcdm", pcdm) };
                DataTable tab = this._dbA.selectTab(sql, lisP, ref error, ref bReturn);
                return tab;
            }
            catch (Exception exe)
            {
            }
            return null;
        }

        /// <summary>
        /// 加载考生信息
        /// </summary>
        public DataTable selectksh(string lqxx, string pcdm)
        {
            try
            {
                string sql = "  select * from View_xxLqxx_zzsb where lqxx=@lqxx and  pcdm=@pcdm and types=1 and td_zt in (3,4) ";
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@lqxx", lqxx), new SqlParameter("@pcdm", pcdm) };
                DataTable tab = this._dbA.selectTab(sql, lisP, ref error, ref bReturn);
                return tab;
            }
            catch (Exception exe)
            {
            }
            return null;
        }

        /// <summary>
        /// 审核。志愿优先
        /// </summary>
        /// <param name="pcdm">ksh。 状态</param>
        public bool XX_TD(string where,int type)
        {
            try
            {
                string sqltab = " select ksh from zk_lqk where td_zt=4 and " + where ; //预录通过变成录取
                string sqltab2 = " select ksh from zk_lqk where td_zt=3 and " + where; //预退通过变成在库
                _dbA.BeginTran();
                string sql = "update zk_lqk set xq_zt=5 ,xqbz=''  where ksh in (" + sqltab + ")";
                string sql2 = "update zk_lqk set xq_zt=0 ,xqbz=''  where ksh in (" + sqltab2 + ");";
                string sql3="update  zk_zzsbtzxx set sbksqr=1 where ksh in (" + sqltab2 + ")";
                int iCount = _dbA.execSql_Tran(sql);
                int iCount2 = _dbA.execSql_Tran(sql2);
                int iCount3 = _dbA.execSql_Tran(sql3);
                StringBuilder stb = new StringBuilder();
                if (iCount > 0 || iCount2 > 0 || iCount3 > 0)
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
        /// 审核上传。志愿优先
        /// </summary>
        /// <param name="pcdm">ksh。 状态</param>
        public bool XX_UP(string lqxx, string pcdm)
        {
            try
            {
                string sqltab = " select ksh from zk_lqk where xq_zt=5 and lqxx=@lqxx and  pcdm=@pcdm and types=1 and td_zt=4"; //录取
                string sqltab2 = " select ksh from zk_lqk where xq_zt=2 and lqxx=@lqxx and  pcdm=@pcdm and types=1 and td_zt in (3,4)"; //不通过变为发档
                string sqltab3 = " select ksh from zk_lqk where xq_zt=0 and lqxx=@lqxx and  pcdm=@pcdm and types=1 and  td_zt=3"; //预退的通过变为在库

                string sql = "update zk_lqk set td_zt=5,lqtime=GETDATE()    where ksh in (" + sqltab + ")";
                string sql2 = "update zk_lqk set td_zt=2,xx_zt=2   where ksh in (" + sqltab2 + ")";
                string sql3 = "update zk_lqk set td_zt=0 ,lqxx='',lqzy='',pcdm='',zysx='',xx_zt=0,xq_zt=0,zydm='',xqdm='',types=0,jyzy='',td_pc=''   where ksh in (" + sqltab3 + ")";
                string sql4 = "update zk_zzsbtzxx set sbksqr=1,sbksqrsj=getdate()  where ksh in (" + sqltab3 + ")"; //修改自主填报状态
                
                _dbA.BeginTran();
             
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@lqxx", lqxx), new SqlParameter("@pcdm", pcdm) };
                //考生轨迹
                string sqlgj = " insert into  zk_kslqgj (ksh,username,type,times)   select ksh,'" + SincciLogin.Sessionstu().UserName + "',5,getdate() from zk_lqk   where ksh in (" + sqltab + ") ";
                _dbA.execSql_Tran(sqlgj, lisP);
                string sqlgj2 = " insert into  zk_kslqgj (ksh,username,type,times)   select ksh,'" + SincciLogin.Sessionstu().UserName + "',2,getdate() from zk_lqk   where ksh in (" + sqltab2 + ") ";
                _dbA.execSql_Tran(sqlgj2, lisP);
                string sqlgj3 = " insert into  zk_kslqgj (ksh,username,type,times)   select ksh,'" + SincciLogin.Sessionstu().UserName + "',0,getdate() from zk_lqk   where ksh in (" + sqltab3 + ") ";
                _dbA.execSql_Tran(sqlgj3, lisP);

                int iCount = _dbA.execSql_Tran(sql, lisP);
                int iCount2 = _dbA.execSql_Tran(sql2, lisP);
                int iCount3 = _dbA.execSql_Tran(sql3, lisP);
                int iCount4 = _dbA.execSql_Tran(sql4, lisP);
                StringBuilder stb = new StringBuilder();
                if (iCount > 0 || iCount2 > 0 || iCount3 > 0 || iCount4 > 0)
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
        /// 学校预 退。志愿优先
        /// </summary>
        /// <param name="pcdm">ksh。 状态</param>
        public bool XX_TD_YT(string where, int type, string xxbz)
        {
            try
            {
                //string sqltab = " select ksh from zk_lqk where td_zt=4 and " + where; //预录通过变成录取
                //string sqltab2 = " select ksh from zk_lqk where td_zt=3 and " + where; //预退通过变成在库
                //_dbA.BeginTran();
                //string sql = "update zk_lqk set xq_zt=5 ,xqbz=''  where ksh in (" + sqltab + ")";
                //string sql2 = "update zk_lqk set xq_zt=0 ,xqbz=''  where ksh in (" + sqltab2 + ")";
                //int iCount = _dbA.execSql_Tran(sql);
                //int iCount2 = _dbA.execSql_Tran(sql2);

                _dbA.BeginTran();
                string sql = "update zk_lqk set xq_zt=" + type + " ,xqbz='" + xxbz + "'  where " + where;
                int iCount = _dbA.execSql_Tran(sql);
                StringBuilder stb = new StringBuilder();
                if (iCount > 0)
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
        /// 根据学校代码查询
        /// </summary>
        /// <param name="xxdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable Select_zk_zykXX(string xxdm)
        {
           
            string sql = "select zydm,zymc='['+zydm+']'+zymc from zk_zyk where xxdm=@xxdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xxdm",xxdm)};
            DataTable dt = _dbA.selectTab(sql, lisP, ref   error, ref   bReturn);

            return dt;
        }

        /// <summary>
        /// 根据批次学校
        /// </summary>
        /// <param name="xxdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable Select_zk_zsxx(string pcdm)
        {

            string sql = "select a.xxdm,b.zsxxdm+b.zsxxmc as zsxxmc from zk_lqjhk a left join zk_zsxxdm b on a.xxdm=b.zsxxdm where a.pcdm=@pcdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@pcdm",pcdm)};
            DataTable dt = _dbA.selectTab(sql, lisP, ref   error, ref   bReturn);

            return dt;
        }

        /// <summary>
        /// 查询未处理批次信息。
        /// </summary>
        public DataTable selectwcl()
        {
            try
            {
                string sql = " select  '批次['+a.pcdm+']学校['+a.lqxx+']:考生总数{'+ CONVERT(varchar(10), a.zs)+'}未处理总数{'+ CONVERT(varchar(10), ISNULL( b.wzs,0))+'}' as 'mc' ,a.lqxx+'_'+ a.pcdm   dm from (  select  lqxx, pcdm, COUNT(ksh) zs from  View_xxLqxx_zzsb where     types=1 and td_zt in (3,4) group by pcdm,lqxx) a  left join (select  lqxx, pcdm, COUNT(ksh) wzs from  View_xxLqxx_zzsb where     types=1 and xq_zt in (3,4) group by pcdm,lqxx )  b on a.pcdm=b.pcdm and a.lqxx=b.lqxx";
              
                DataTable tab = this._dbA.selectTab(sql,  ref error, ref bReturn);
                return tab;
            }
            catch (Exception exe)
            {
            }
            return null;
        }
    }
}
