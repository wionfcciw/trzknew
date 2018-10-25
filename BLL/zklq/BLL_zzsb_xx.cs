using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;
using System.Data.SqlClient;

namespace BLL
{
    public class BLL_zzsb_xx
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
        public DataTable selectPcdm(string xxdm)
        {
            try
            {
                //string sql = "select xpc_id=xpcId+'_'+convert(varchar(20),pcLb),xpc_mc='['+pcdm+']  '+xpcXsMc from zk_zydz_xpcxx";
                string sql = " select xpcid,xpc_mc='{'+b.xqmc+'}'+'['+pcdm+']  '+xpcXsMc from zk_zydz_xpcxx a left join zk_xqdm b on LEFT( a.dpcdm,4)=b.xqdm where  LEFT(dpcDm,4)='500' and pcDm in (21,31)  and pcdm in (select pcdm from zk_lqjhk where xxdm=@xxdm)";//只做了这2个批次
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@xxdm", xxdm) };
                DataTable tab = this._dbA.selectTab(sql, lisP, ref error, ref bReturn);
                return tab;
            }
            catch (Exception exe)
            {
            }
            return null;
        }
        /// <summary>
        /// 学校预 退。志愿优先
        /// </summary>
        /// <param name="pcdm">ksh。 状态</param>
        public bool XX_TD_YT(string where, int type, string xxbz, string lqxx)
        {
            try
            {
                _dbA.BeginTran();
                string sql = "update zk_lqk set xx_zt=" + type + " ,xxbz='" + xxbz + "'  where lqxx='" + lqxx + "' and " + where;
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
                string sql = "  select * from View_xxLqxx_zzsb where lqxx=@lqxx and  pcdm=@pcdm  and td_zt=2 and types=1";
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@lqxx", lqxx), new SqlParameter("@pcdm", pcdm)  };
                DataTable tab = this._dbA.selectTab(sql, lisP, ref error, ref bReturn);
                return tab;
            }
            catch (Exception exe)
            {
            }
            return null;
        }

        /// <summary>
        /// 学校预录。志愿优先
        /// </summary>
        /// <param name="pcdm">ksh。 状态</param>
        public bool XX_TD(string where,int type,string lqzy)
        {
            try
            {
                _dbA.BeginTran();
                string sql = "update zk_lqk set xx_zt=" + type + " ,lqzy='" + lqzy + "' ,xxbz=''  where " + where;

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
        /// 学校上传。志愿优先
        /// </summary>
        /// <param name="pcdm">ksh。 状态</param>
        public bool XX_UP(string lqxx, string pcdm)
        {
            try
            {
                _dbA.BeginTran();
                string sql = "update zk_lqk set td_zt=xx_zt,xq_zt=xx_zt   where lqxx=@lqxx and  pcdm=@pcdm and types=1 and td_zt=2";
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@lqxx", lqxx), new SqlParameter("@pcdm", pcdm) };
                //考生轨迹
                string sqlgj = " insert into  zk_kslqgj (ksh,username,type,times)   select ksh,'" + SincciLogin.Sessionstu().UserName + "',xx_zt,getdate() from zk_lqk   where lqxx=@lqxx and  pcdm=@pcdm and types=1 and td_zt=2";
                _dbA.execSql_Tran(sqlgj, lisP);
                
                int iCount = _dbA.execSql_Tran(sql, lisP);
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
        /// 学校预 退。志愿优先
        /// </summary>
        /// <param name="pcdm">ksh。 状态</param>
        public bool XX_TD_YT(string where, int type, string xxbz)
        {
            try
            {
                _dbA.BeginTran();
                string sql = "update zk_lqk set xx_zt=" + type + " ,xxbz='" + xxbz + "'  where " + where;
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
        /// 查询未处理批次信息。
        /// </summary>
        public DataTable selectwcl(string xxdm)
        {
            try
            {

                string sql = " select  '批次['+a.pcdm+']:考生总数{'+ CONVERT(varchar(10), a.zs)+'}未处理总数{'+ CONVERT(varchar(10), ISNULL( b.wzs,0))+'}' as 'mc' , a.pcdm  dm from (  select   pcdm, COUNT(ksh) zs from View_xxLqxx_zzsb where lqxx=@lqxx   and td_zt=2 and types=1 group by pcdm) a  left join (select   pcdm, COUNT(ksh) wzs from View_xxLqxx_zzsb where lqxx=@lqxx   and xx_zt=2 and types=1 group by pcdm )  b on a.pcdm=b.pcdm ";
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@lqxx", xxdm) };
                DataTable tab = this._dbA.selectTab(sql, lisP, ref error, ref bReturn);
                return tab;
            }
            catch (Exception exe)
            {
            }
            return null;
        }
        public DataTable Select_XXXX(string xqdm)
        {

            //string sql="select zsxxdm,zsxxmc,xqdm from zk_zsxxdm left join zk_zsjh on zsxxdm=xxdm"
            string sql = " select '【'+zsxxdm+'】'+zsxxmc as xxxx,zsxxdm from zk_zsxxdm left join zk_zsjh on zsxxdm=xxdm";
            if (xqdm != null)
            {
                sql += " where xqdm=@xqdm";
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@xqdm", xqdm) };
                DataTable tab = this._dbA.selectTab(sql, lisP, ref error, ref bReturn);
                return tab;
            }
            else
            {
                DataTable tab = this._dbA.selectTab(sql, ref error, ref bReturn);
                return tab;
            }
        }
    }
}
