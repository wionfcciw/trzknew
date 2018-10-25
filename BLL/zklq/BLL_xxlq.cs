using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;
using System.Data.SqlClient;

namespace BLL
{
  public  class BLL_xxlq
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
                string sql = " select pcDm,xpc_mc='{'+b.xqmc+'}'+'['+pcdm+']  '+xpcXsMc from zk_zydz_xpcxx a left join zk_xqdm b on LEFT( a.dpcdm,3)=b.xqdm where  LEFT(dpcDm,3)='500'    and pcdm in (select pcdm from zk_lqjhk where xxdm=@xxdm)";//只做了这2个批次
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
        public DataTable selectksh(string lqxx, string pcdm, string tdpc)
        {
            try
            {
                string sql = "  select * from View_xxLqxx where lqxx=@lqxx and  pcdm=@pcdm and td_pc=@tdpc and td_zt=2 and types=0";
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@lqxx", lqxx), new SqlParameter("@pcdm", pcdm), new SqlParameter("@tdpc", tdpc) };
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
               // _dbA.BeginTran();
                string sql = "update zk_lqk set xx_zt=" + type + " ,lqzy='" + lqzy + "' ,xxbz=''  where " + where;

                int iCount = _dbA.ExecuteNonQuery(sql,ref error,ref bReturn);
                StringBuilder stb = new StringBuilder();
                if (bReturn)
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
        /// 学校上传。志愿优先
        /// </summary>
        /// <param name="pcdm">ksh。 状态</param>
        public bool XX_UP(string lqxx, string pcdm, string tdpc)
        {
            try
            {
                _dbA.BeginTran();
                string sql = "update zk_lqk set td_zt=xx_zt,xq_zt=xx_zt   where lqxx=@lqxx and  pcdm=@pcdm and td_pc=@tdpc";
               
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@lqxx", lqxx), new SqlParameter("@pcdm", pcdm), new SqlParameter("@tdpc", tdpc) };
                //考生轨迹
                string sqlgj = " insert into   zk_kslqgj (ksh,username,type,times)  (ksh,username,type,times)   select ksh,'" + SincciLogin.Sessionstu().UserName + "',xx_zt,getdate() from zk_lqk   where lqxx=@lqxx and  pcdm=@pcdm and td_pc=@tdpc ";
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
        public bool XX_TD_YT(string where, int type, string xxbz,string lqxx)
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
        /// 根据学校代码查询
        /// </summary>
        /// <param name="xxdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable Select_zk_zykXX(string xxdm,string pcdm)
        {
            string sql = "  select zydm,zymc='['+zydm+']'+zymc from View_zsjh where xxdm=@xxdm and pcdm=@pcdm  group by zydm,zymc";
          //  string sql = "select zydm,zymc='['+zydm+']'+zymc from zk_zyk where xxdm=@xxdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xxdm",xxdm), new SqlParameter("@pcdm",pcdm)};
            DataTable dt = _dbA.selectTab(sql, lisP, ref   error, ref   bReturn);

            return dt;
        }

        /// <summary>
        /// 查询一条考生信息获取建议专业类型
        /// </summary>
        public DataTable selectksh_top(string lqxx, string pcdm, string tdpc)
        {
            try
            {
                string sql = "  select top 1 * from View_xxLqxx where lqxx=@lqxx and  pcdm=@pcdm and td_pc=@tdpc and td_zt=2 ";
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@lqxx", lqxx), new SqlParameter("@pcdm", pcdm), new SqlParameter("@tdpc", tdpc) };
                DataTable tab = this._dbA.selectTab(sql, lisP, ref error, ref bReturn);
                return tab;
            }
            catch (Exception exe)
            {
            }
            return null;
        }
        /// <summary>
        /// 排序考生信息
        /// </summary>
        public DataTable selectksh_where(string lqxx, string pcdm, string tdpc,string where)
        {
            try
            {
                string sql = "  select * from View_xxLqxx where lqxx=@lqxx and  pcdm=@pcdm and td_pc=@tdpc and td_zt=2 " + where;
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@lqxx", lqxx), new SqlParameter("@pcdm", pcdm), new SqlParameter("@tdpc", tdpc) };
                DataTable tab = this._dbA.selectTab(sql, lisP, ref error, ref bReturn);
                return tab;
            }
            catch (Exception exe)
            {
            }
            return null;
        }

        /// <summary>
        /// 查询该考生专业的剩余数量
        /// </summary>
        public DataTable selectksh_zynum(string lqxx, string pcdm, string zydm )
        {
            try
            {
                string sql = "  select a.*,ISNULL( b.lqzynum,0) lqzynum from  zk_zsjh a left join  ( select  lqxx,lqzy,COUNT( lqzy) as lqzynum   from zk_lqk where lqxx=@lqxx  and xx_zt=4  group by lqzy,lqxx) b on a.xxdm=b.lqxx and a.zydm=b.lqzy where xxdm=@lqxx and pcdm=@pcdm and zydm=@zydm ";
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@lqxx", lqxx), new SqlParameter("@pcdm", pcdm), new SqlParameter("@zydm", zydm) };
                DataTable tab = this._dbA.selectTab(sql, lisP, ref error, ref bReturn);
                return tab;
            }
            catch (Exception exe)
            {
            }
            return null;
        }
        /// <summary>
        /// 查询 未录满的专业
        /// </summary>
        public DataTable selectksh_zynum(string lqxx, string pcdm)
        {
            try
            {
                string sql = "  select a.*,ISNULL( b.lqzynum,0) lqzynum from  zk_zsjh a left join  ( select  lqxx,lqzy,COUNT( lqzy) as lqzynum   from zk_lqk where lqxx=@lqxx group by lqzy,lqxx) b on a.xxdm=b.lqxx and a.zydm=b.lqzy where xxdm=@lqxx and pcdm=@pcdm  and (jhs-ISNULL( b.lqzynum,0))>0  ";
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
        /// 查询未处理批次信息。
        /// </summary>
        public DataTable selectwcl(string xxdm)
        {
            try
            {

                string sql = "select '批次['+a.pcdm+']发档批次['+ CONVERT(varchar(10), a.td_pc)+']:考生总数{'+ CONVERT(varchar(10), a.zs)+'}未处理总数{'+ CONVERT(varchar(10), ISNULL( b.wzs,0))+'}' as 'mc'  ,a.pcdm+'_'+a.td_pc dm from (  select pcdm, td_pc,COUNT(ksh) zs from View_xxLqxx where lqxx=@lqxx  and td_zt=2  and types=0 group by td_pc,pcdm) a  left join (select pcdm, td_pc,COUNT(ksh) wzs from View_xxLqxx where lqxx=@lqxx  and td_zt=2 and xx_zt=2  and types=0 group by td_pc,pcdm)  b on a.pcdm=b.pcdm and a.td_pc=b.td_pc";
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@lqxx", xxdm) };
                DataTable tab = this._dbA.selectTab(sql, lisP, ref error, ref bReturn);
                return tab;
            }
            catch (Exception exe)
            {
            }
            return null;
        }

        /// <summary>
        /// 统计学校专业录取数
        /// </summary>
        public DataTable tj_xx(string where ,string where2)
        {
            try
            {
                string sql = "select  a.xxdm,a.zsxxmc,a.zydm,a.zymc,a.jhs,ISNULL( b.lqzynum,0) lqzynum, ISNULL(d.lqnum,0) lqnum,CONVERT(nvarchar(10), ISNULL( e.maxcj,0)) maxcj,CONVERT(nvarchar(10), ISNULL( f.mincj,0)) mincj from  View_zsjh a left join  " +
 " ( select  lqxx,lqzy,COUNT( lqzy) as lqzynum  from zk_lqk where " + where + " and xx_zt=4 group by lqzy,lqxx) " +
"  b on a.xxdm=b.lqxx  and a.zydm=b.lqzy " +
" left join ( select  lqxx,lqzy,COUNT(lqzy)  as lqnum,td_zt  from zk_lqk where " + where + " and td_zt=5 group by lqzy,lqxx,td_zt) d" +
" on a.xxdm=d.lqxx and a.zydm=d.lqzy  " +
" left join ( select  lqzy,max(cj) maxcj from zk_lqk where " + where + " group by lqzy  ) e" +
" on a.zydm=e.lqzy" +
" left join ( select  lqzy,min(cj) mincj from zk_lqk where " + where + " group by lqzy  ) f" +
" on a.zydm=f.lqzy" +
" where " + where2 +
"    union all " +
"   select '','合计','','',SUM(g.jhs),SUM(g.lqzynum),SUM(g.lqnum),'','' from ( " +
 " select  a.xxdm,a.zsxxmc,a.zydm,a.zymc,a.jhs,ISNULL( b.lqzynum,0) lqzynum, ISNULL(d.lqnum,0) lqnum,CONVERT(nvarchar(10), ISNULL( e.maxcj,0)) maxcj,CONVERT(nvarchar(10), ISNULL( f.mincj,0)) mincj from  View_zsjh a left join  " +
 " ( select  lqxx,lqzy,COUNT( lqzy) as lqzynum  from zk_lqk where " + where + " and xx_zt=4 group by lqzy,lqxx) " +
"  b on a.xxdm=b.lqxx  and a.zydm=b.lqzy " +
" left join ( select  lqxx,lqzy,COUNT(lqzy)  as lqnum,td_zt  from zk_lqk where " + where + " and td_zt=5 group by lqzy,lqxx,td_zt) d" +
" on a.xxdm=d.lqxx and a.zydm=d.lqzy  " +
" left join ( select  lqzy,max(cj) maxcj from zk_lqk where " + where + " group by lqzy  ) e" +
" on a.zydm=e.lqzy" +
" left join ( select  lqzy,min(cj) mincj from zk_lqk where " + where + " group by lqzy  ) f" +
" on a.zydm=f.lqzy" +
" where " + where2 + " ) g ";
 
                DataTable tab = this._dbA.selectTab(sql,  ref error, ref bReturn);
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
        public DataTable selectksh2(string lqxx, string pcdm, string tdpc)
        {
            try
            {
                string sql = "  select ksh,xm,xbdm,lxdh,yddh, yw, sx, yy, jf, zgdx,cj,zysx,zy1mc,zy2mc,CASE zyfc when 1 THEN '1' else '0' end as zyfc from View_xxLqxx where lqxx=@lqxx and  pcdm=@pcdm and td_pc=@tdpc and td_zt=2 and types=0";
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@lqxx", lqxx), new SqlParameter("@pcdm", pcdm), new SqlParameter("@tdpc", tdpc) };
                DataTable tab = this._dbA.selectTab(sql, lisP, ref error, ref bReturn);
                return tab;
            }
            catch (Exception exe)
            {
            }
            return null;
        }
    }
}
