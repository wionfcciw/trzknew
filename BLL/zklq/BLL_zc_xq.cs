using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;
using System.Data.SqlClient;

namespace BLL
{
    public class BLL_zc_xq
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
        public DataTable selectksh(string where )
        {
            try
            {
                //zcxx 1在库 2,发档  5 录取 6 上传录取
                string sql = "   SELECT a.lqxx+e.zsxxmc as zclqxx,a.types as lqzt, c.td_zt, a.ksh,b.xm,c.cj, a.lqzy+ d.zymc as zclqzy,c.types,c.lqxx+g.zsxxmc lqxx ,c.lqzy+f.zymc lqzy FROM  zk_zcxx a left join zk_ksxxgl b  on a.ksh=b.ksh left join zk_lqk c on a.ksh=c.ksh  left join zk_zyk d  on a.lqxx=d.xxdm and a.lqzy=d.zydm left join zk_zsxxdm  e on a.lqxx=e.zsxxdm left join zk_zyk f   on c.lqxx=f.xxdm and c.lqzy=f.zydm left join zk_zsxxdm g on c.lqxx=g.zsxxdm  where   a.types in (1,2,5 ) and ISNULL(c.types,0)<>2  and " + where;
              
                DataTable tab = this._dbA.selectTab(sql, ref error, ref bReturn);
                return tab;
            }
            catch (Exception exe)
            {
            }
            return null;
        }
        /// <summary>
        /// 加载学校
        /// </summary>
        public DataTable selectxx()
        {
            try
            {
                //zcxx 1在库 2,发档  5 录取 6 上传录取
                string sql = "   SELECT  a.lqxx,  a.lqxx+e.zsxxmc+'{'+CONVERT(varchar(20), COUNT(*))+'}' as lqxxmc   FROM  zk_zcxx a left join zk_ksxxgl b  on a.ksh=b.ksh left join zk_lqk c on a.ksh=c.ksh  left join zk_zyk d  on a.lqxx=d.xxdm and a.lqzy=d.zydm left join zk_zsxxdm  e on a.lqxx=e.zsxxdm left join zk_zyk f   on c.lqxx=f.xxdm and c.lqzy=f.zydm left join zk_zsxxdm g on c.lqxx=g.zsxxdm  where   a.types in (1,2,5 ) and ISNULL(c.types,0)<>2   group by a.lqxx,a.lqxx+e.zsxxmc   order by lqxx asc ";

                DataTable tab = this._dbA.selectTab(sql, ref error, ref bReturn);
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
          
                string sql = "update zk_zcxx set types=" + type + "  where " + where;

                int iCount = _dbA.ExecuteNonQuery(sql, ref error, ref bReturn);

                if (bReturn)
                {
                    return true;
                }
                return false;

             
        }
        /// <summary>
        /// 审核上传。志愿优先
        /// </summary>
        /// <param name="pcdm">ksh。 状态</param>
        public bool XX_UP(string where)
        {
            try
            {
                string sqltab = " select a.* from zk_zcxx a left join zk_lqk c on a.ksh=c.ksh where a.types=5 and  " + where; //录取
                string sqltab2 = " select a.ksh from zk_zcxx a left join zk_lqk c on a.ksh=c.ksh where a.types=1  and  " + where; //不通过变为在库
            //    string sqltab3 = " select ksh from zk_zcxx where types=0 and lqxx=@lqxx  "; //不通过变为在库


             
                string sql3 = "delete zk_zcxx   where ksh in (" + sqltab2 + ")";
              //  string sql3 = "update zk_lqk set types=0  where ksh in (" + sqltab3 + ")";
                _dbA.BeginTran();

                //考生轨迹
                string sqlgj = " insert into   zk_kslqgj (ksh,username,type,times)  (ksh,username,type,times)   select a.ksh,'" + SincciLogin.Sessionstu().UserName + "',5,getdate() from zk_zcxx   a left join zk_lqk c on a.ksh=c.ksh where a.types=5 and  " + where;
                _dbA.execSql_Tran(sqlgj);

                string sqlgj2 = " insert into   zk_kslqgj (ksh,username,type,times)  (ksh,username,type,times)   select a.ksh,'" + SincciLogin.Sessionstu().UserName + "',0,getdate() from zk_zcxx   a left join zk_lqk c on a.ksh=c.ksh where a.types=1 and  " + where;
                _dbA.execSql_Tran(sqlgj2);
                DataTable tab = this._dbA.selectTab(sqltab,  ref error, ref bReturn);
                int iCount = 0;
                if (tab != null)
                {
                    for (int i = 0; i < tab.Rows.Count; i++)
                    {
                        string sql = "update zk_lqk set xqdm='500',lqxx='" + tab.Rows[i]["lqxx"] + "',pcdm='31',zydm='00',td_zt=5,sf_zbs=0,sf_tfgj=0,zysx=1,lqzy='" + tab.Rows[i]["lqzy"] + "',types=2,lqtime=GETDATE()  where ksh='" + tab.Rows[i]["ksh"] + "'";
                         iCount = _dbA.execSql_Tran(sql);
                         iCount += iCount;
                         string sql2 = "update zk_zcxx set types=6  where ksh='" + tab.Rows[i]["ksh"] + "'"; //上传录取后的.之后不再上传
                         _dbA.execSql_Tran(sql2);
                    }
                }
                
              
                //int iCount2 = _dbA.execSql_Tran(sql2);
                int iCount3 = _dbA.execSql_Tran(sql3);
                StringBuilder stb = new StringBuilder();
                if (iCount > 0 || iCount3>0)
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
    }
}
