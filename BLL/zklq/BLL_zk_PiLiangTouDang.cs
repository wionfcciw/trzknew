using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model;
using System.Data;
using System.Data.SqlClient;


namespace BLL
{
    /// <summary>
    /// 批量投档操作控制类。
    /// </summary>
    public class BLL_zk_PiLiangTouDang
    {
        #region 字段。
        /// <summary>
        /// 数据库控制类。
        /// </summary>
        private SqlDbHelper_1 _dbA = new SqlDbHelper_1();
        /// <summary>
        /// 执行失败时返回的错误信息。
        /// </summary>
        private string error = "";
        /// <summary>
        /// 执行成功的标识：true、执行成功；false、表示执行时错误。
        /// </summary>
        private bool bReturn = false;
        /// <summary>
        /// 投档定制条件操作类。
        /// </summary>
        private BLL_zk_Pctd_tj_Info _tddztj = new BLL_zk_Pctd_tj_Info();
        /// <summary>
        /// 当前批次的限制条件。
        /// </summary>
        private Model_zk_Pctd_tj_Info _xztj = null;
        /// <summary>
        /// SQL语句。
        /// </summary>
        StringBuilder _stbSql = new StringBuilder();
        /// <summary>
        /// 非指标生招生计划。
        /// </summary>
        private DataTable tab_f_zbs_zsjh = null;
        /// <summary>
        /// 记录最后一个考试.成绩比较同分跟进。
        /// </summary>
        private DataTable tab_tfgj = null;
        /// <summary>
        /// 县区最低控制线。
        /// </summary>
        private DataTable xq_zdKzx = null;
        /// <summary>
        /// 统招最后分数
        /// </summary>
        private decimal dec = 0;

        private int _iCount = 0;
        #endregion

        #region 构造方法。
        /// <summary>
        /// 构造方法。
        /// </summary>
        public BLL_zk_PiLiangTouDang() { }
        #endregion

        /// <summary>
        /// 加载当前批次投档算法。
        /// </summary>
        /// <param name="xpcId">小批次ID。</param>
        public DataTable select_tdzy_sf(string xpcId)
        {
            string sql = "select tdsf,tdsfmc=(case tdsf when 0 then '平行志愿算法' when 1 then '志愿优先算法' end) from zk_Pc_TouDang_jbtj where xpcId=@xpcId";
            List<SqlParameter> lisP = new List<SqlParameter>() { 
                new SqlParameter("@xpcId",xpcId)
            };

            return this._dbA.selectTab(sql, lisP, ref error, ref bReturn);
        }

        /// <summary>
        /// 加载县区最低分数线控制。
        /// </summary>
        public DataTable selectXqZdFsx()
        {
            string sql = "select yc_Xqdm,yc_XqMc,yc_ZdFensuKzx from zk_Yc_XqYcZdFsx";
            DataTable tab = this._dbA.selectTab(sql, ref error, ref bReturn);
            return tab;
        }
        /// <summary>
        /// 查询片区信息
        /// </summary>
        public DataTable select_lqjh(string xxdm)
        {
            string sql = "SELECT * FROM  dbo.zk_zsjh_xq where xxdm=@xxdm";
            List<SqlParameter> lisP = new List<SqlParameter>() { 
                new SqlParameter("@xxdm",xxdm)
            };
            DataTable tab = this._dbA.selectTab(sql,lisP, ref error, ref bReturn);
            return tab;
        }
        /// <summary>
        /// 
        /// </summary>
        public DataTable select_jhs(string xxdm)
        {
            string sql = "SELECT * FROM validate_view where pcdm='11' and  xxdm=@xxdm";
            List<SqlParameter> lisP = new List<SqlParameter>() { 
                new SqlParameter("@xxdm",xxdm)
            };
            DataTable tab = this._dbA.selectTab(sql,lisP,  ref error, ref bReturn);
            return tab;
        }
        /// <summary>
        /// 根据小批次ID查询当前批次的招生学校名单。
        /// </summary>
        /// <param name="xpcId">批次代码</param>
        public DataTable select_pc_touDang_Info(string pcdm, string xqdm)
        {

            //string sql = "select xxdm,xxmc,jhs,tz_sl=jhs-zbssl,yitd_fzbs_sl,zbssl,yitd_zbs_sl,fzbs_xcsl=jhs-zbssl-yitd_fzbs_sl,zbs_xcsl=zbssl-yitd_zbs_sl,zaosenFsx=0 from validate_view where xpcId=@xpcId order by xxdm ASC";
            //   string sql = "select * from zs_xx_tj where pcdm=@pcdm order by xxdm ASC";
            string sql = "  select isnull(h.xxdm,'') as jqxx, isnull(e.fdnum,0) fdnum ,isnull(f.maxlqfsx,0) maxlqfsx ,isnull(g.minlqfsx,0) minlqfsx,isnull(d.lqnum,0) lqnum, a.*,ISNULL( b.ylnum,0) ylnum,isnull(c.ytnum,0) ytnum from zs_xx_tj a left join  ( select lqxx, COUNT(*) ylnum from zk_lqk where td_zt=4 and pcdm=@pcdm and xqdm=@xqdm  group by lqxx) b  on a.xxdm=b.lqxx left join  ( select lqxx, COUNT(*) ytnum from zk_lqk where td_zt=3 and pcdm=@pcdm  and xqdm=@xqdm  group by lqxx) c on a.xxdm=c.lqxx  left join  ( select lqxx, COUNT(*) lqnum from zk_lqk where td_zt=5 and pcdm=@pcdm  and xqdm=@xqdm   group by lqxx) d on a.xxdm=d.lqxx " +
"   left join  ( select lqxx, COUNT(*) fdnum from zk_lqk where td_zt=2 and pcdm=@pcdm and xqdm=@xqdm " +
 "  group by lqxx) e on a.xxdm=e.lqxx " +
"    left join  ( select lqxx, max(cj) maxlqfsx from zk_lqk where td_zt=5 and isnull(daoru,0)=0 and pcdm=@pcdm  and xqdm=@xqdm " +
 "  group by lqxx) f on a.xxdm=f.lqxx " +
  "  left join  ( select lqxx, min(cj) minlqfsx from zk_lqk where td_zt=5  and isnull(daoru,0)=0 and pcdm=@pcdm  and xqdm=@xqdm " +
  " group by lqxx) g on a.xxdm=g.lqxx left join zk_TJS_XXSD h on a.xxdm=h.xxdm" +
                    " where pcdm=@pcdm  and xqdm=@xqdm order by xxdm ASC";
            List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@pcdm", pcdm), new SqlParameter("@xqdm", xqdm) };
            DataTable tab = this._dbA.selectTab(sql, lisP, ref error, ref bReturn);
            return tab;

        }
        /// <summary>
        /// 根据小批次ID查询当前批次的招生学校名单。 志愿优先
        /// </summary>
        /// <param name="xpcId">批次代码</param>
        public DataTable select_pc_touDang_XxInfo(string pcdm, string zysx)
        {
            try
            {
                //string sql = "select xxdm,xxmc,jhs,tz_sl=jhs-zbssl,yitd_fzbs_sl,zbssl,yitd_zbs_sl,fzbs_xcsl=jhs-zbssl-yitd_fzbs_sl,zbs_xcsl=zbssl-yitd_zbs_sl,zaosenFsx=0 from validate_view where xpcId=@xpcId order by xxdm ASC";
                string sql = "select * from zs_xx_tj where pcdm=@pcdm and zysx=@zysx order by xxdm ASC";
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@pcdm", pcdm), new SqlParameter("@zysx", zysx) };

                //SqlParameter Pcdm = new SqlParameter("@pcdm", SqlDbType.VarChar);
                //Pcdm.Value = pcdm;
                //lisP.Add(Pcdm);

                this._dbA.BeginTran();
                DataTable tab = this._dbA.execReTab_Tran(sql, lisP);
                return tab;
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbA.EndTran(false);
            }
            return null;
        }
        /// <summary>
        /// 开始投档(0、执行成功；1、执行失败；2、当前批次未定制投档条件)
        /// </summary>
        /// <param name="xpcId">小批次的ID。</param>
        /// <param name="pcdm">批次代码。</param>
        /// <param name="tab">各县区最低分数线控制</param>
        public int Start_TouDang(string xpcId, string pcdm, DataTable tab, string zysx, int tdnum, string xxdm, double bil, int type, string xqdm, int sfzbsTD,bool chxq,int sftzs,string fenxq)
        {
            try
            {
                this.xq_zdKzx = tab;
                if (this._xztj == null)
                {
                    //获取当前批次的投档条件。
                    this._xztj = this._tddztj.selectPcTj(xpcId);
                }
                this._dbA.BeginTran();
                /*
                 * 每个投档批次都有一个基本条件的定制，在基本条件中会指定当前批次投档需要什么算法（平行志愿算法、志愿优先算法）
                 */
                if (this._xztj.bFlag && this._xztj.Jbtj.bFlag)
                {
                    //投档算法:0、平行志愿算法；1、志愿优先算法。
                    switch (this._xztj.Jbtj.Tdsf)
                    {
                        case 0://平行志愿算法。
                            PingXinSuanFa(pcdm, tab, xqdm, xxdm, tdnum, bil, type, sfzbsTD, chxq, sftzs, fenxq);
                            break;
                        case 1://志愿优先算法。

                            ZyyxSuanFa(pcdm, tab, xqdm, zysx, tdnum, xxdm, bil, type, sfzbsTD);
                            break;
                    }
                }
                else
                {
                    return 2;
                }
                this._dbA.EndTran(true);
                return 0;
            }
            catch (Exception exe)
            {
                this._dbA.EndTran(false);
            }
            return -1;
        }

        /// <summary>
        /// 创查询条件。
        /// </summary>
        private string createSQL(string pcdm, string xxdm, int sfzbsTD, string xqdm, bool chxq, string fenxq, int sftzs)
        {
            StringBuilder sql = new StringBuilder();
            //sql = String.Format("select a.*,b.cj,b.xqdm,c.sfzbs from zk_kszyxx a left join zk_kszyxx_ks_zcj_view b on a.ksh=b.ksh left join zk_ksxxgl c on a.ksh=c.ksh where sfbk=1 and pcdm=@pcdm {0} and b.cj>=(select MIN(yc_ZdFensuKzx) from zk_Yc_XqYcZdFsx) order by cj DESC,ksh asc ,zysx asc", stb.ToString());

            string fields = "";
            string tabs = "";


            fields = "a.*,b.cj,wh_cj=b.cj,b.xqdm,c.sfzbs,c.bmddm,yw,sx,yy,zf";
            tabs = "zk_kszyxx a left join zk_kszyxx_ks_zcj_view b on a.ksh=b.ksh left join zk_ksxxgl c on a.ksh=c.ksh";

            string where = "";
            if (chxq)
            {
                where = "sfbk=1 and len(xxdm)>0 and pcdm=@pcdm  {0}   order by xqdm,cj DESC, ksh asc ,zysx asc";
            }
            else
            {
                if (sftzs == 2)
                {
                    where = "sfbk=1 and len(xxdm)>0 and pcdm=@pcdm and c.xjtype=0 {0}   order by cj DESC, ksh asc ,zysx asc";
                }
                else
                    where = "sfbk=1 and len(xxdm)>0 and pcdm=@pcdm  {0}   order by cj DESC, ksh asc ,zysx asc";

            }
            //if (pcdm.Substring(0, 1) == "1")//第一批需要加县区
            //{
            //    where = "sfbk=1 and len(xxdm)>0 and pcdm=@pcdm and b.xqdm=@xqdm {0}   order by cj DESC, yw+sx+yy desc,ksh asc ,zysx asc";
            //}
            //else
            //{
            //    where = "sfbk=1 and len(xxdm)>0 and pcdm=@pcdm  {0}   order by cj DESC, yw+sx+yy desc,ksh asc ,zysx asc";
            //}
       
            StringBuilder stb = new StringBuilder();
            if (this._xztj != null)
            {
                #region 基本条件。
                if (this._xztj.Jbtj.bFlag)
                {
                    //最低控档线设置。
                    switch (this._xztj.Jbtj.Zdkdx)
                    {
                        case 1://普高最低控档线无效。
                            if (pcdm.Substring(0, 1) == "1")//第一批需要加县区
                                where = "sfbk=1 and len(xxdm)>0 and pcdm=@pcdm and b.xqdm=@xqdm {0}   order by cj DESC,ksh asc ,zysx asc";
                            else
                                where = "sfbk=1 and len(xxdm)>0 and pcdm=@pcdm  {0}   order by cj DESC,ksh asc ,zysx asc";
                            break;
                        case 2://指定最低分数线。
                            if (pcdm.Substring(0, 1) == "1")//第一批需要加县区
                                where = "sfbk=1 and len(xxdm)>0 and pcdm=@pcdm  and b.xqdm=@xqdm  {0} and b.cj>=" + this._xztj.Jbtj.Zdy_zdfs + " order by cj DESC,ksh asc ,zysx asc";
                            else
                                where = "sfbk=1 and len(xxdm)>0 and pcdm=@pcdm {0} and b.cj>=" + this._xztj.Jbtj.Zdy_zdfs + " order by cj DESC,ksh asc ,zysx asc";

                            break;
                    }
                }
                #endregion
                #region 指标生
                //判断指标生投档条件是否定制有效。
                if (this._xztj.Zbs.bFlag)
                {
                    //是否有指标生。
                    if (this._xztj.Zbs.Ywzbs == 0)
                    {
                        fields = "a.*, cj=(b.cj + isnull(g.fs,0)),wh_cj=b.cj,b.xqdm,c.sfzbs,isnull(g.fs,0) fs,c.bmddm,yw,sx,yy,zf,whzf";
                        tabs = String.Format("{0} left join {1}", tabs, "  zk_TJS_gr g on a.ksh=g.ksh and a.xxdm=g.tjxxdm and a.zysx=1  ");
                    }
                }


                #endregion
                #region 往届生

                if (this._xztj.Szpj.bFlag)
                {
                    //0是无.1是有
                    if (this._xztj.Szpj.Zhszxztj == 1)
                    {
                        fields = "a.*, cj=(b.cj - isnull(j.fs,0)),wh_cj=b.cj,b.xqdm,c.sfzbs,isnull(j.fs,0) fs,c.bmddm,yw,sx,yy,zf,whzf";
                        tabs = String.Format("{0} left join {1}", tabs, "  zk_TJS_wjs j on a.ksh=j.ksh  ");
                    }
                }
                #endregion
                //if (pcdm == "02")
                //{
                //    fields = "a.*, cj=(b.cj + isnull(h.fs,0)),wh_cj=b.cj,b.xqdm,c.sfzbs,isnull(h.fs,0) fs,c.bmddm,yw,sx,yy,zf,whzf";
                //    tabs = String.Format("{0} left join {1}", tabs, "  zk_TJS_yt h on a.ksh=h.ksh ");
                //}
                #region 其他条件

                //if (this._xztj.Qttj.bFlag)
                //{
                //    //加试成绩是否要求合格。
                //    switch (this._xztj.Qttj.Jscjxzif)
                //    {
                //        case 0://艺术类。
                //            tabs = String.Format("{0} left join {1}", tabs, " zk_ksjscj e on a.ksh=e.ksh ");
                //            stb.Append(" and e.ys_sfhg=1");
                //            break;
                //        case 1://师范类。
                //            tabs = String.Format("{0} left join {1}", tabs, " zk_ksjscj e on a.ksh=e.ksh ");
                //            stb.Append(" and e.sf_sfhg=1");
                //            break;
                //        case 2://免费师范(男生)类
                //            tabs = String.Format("{0} left join {1}", tabs, " zk_ksjscj e on a.ksh=e.ksh ");
                //            stb.Append(" and e.mh_sf_ns_sfhg=1");
                //            break;
                //    }
                //    //与文化成绩合并的加试成绩。
                //    switch (this._xztj.Qttj.Ywhcjhb_jscj)
                //    {
                //        case 0://艺术类。
                //            fields = "a.*,cj=(b.cj+f.ys_l),wh_cj=b.cj,b.xqdm,c.sfzbs";
                //            tabs = String.Format("{0} left join {1}", tabs, " zk_ksjscj f on a.ksh=f.ksh ");
                //            break;
                //        case 1://师范类。
                //            fields = "a.*,cj=(b.cj+f.sf_l),wh_cj=b.cj,b.xqdm,c.sfzbs";
                //            tabs = String.Format("{0} left join {1}", tabs, " zk_ksjscj f on a.ksh=f.ksh ");
                //            break;
                //        case 2://免费师范(男生)类
                //            fields = "a.*,cj=(b.cj+f.mh_sf_ns_l),wh_cj=b.cj,b.xqdm,c.sfzbs";
                //            tabs = String.Format("{0} left join {1}", tabs, " zk_ksjscj f on a.ksh=f.ksh ");
                //            break;
                //    }

                //    /*
                //     * 考生会考成绩合格
                //     * 地理、ABCD
                //     * 生物、ABCD
                //     */
                //    //会考成绩是否合格。
                //    switch (this._xztj.Qttj.HkcjhgXz)
                //    {
                //        case 0://会考成绩必须合格。
                //            tabs = String.Format("{0} left join {1}", tabs, "zk_kshkcj g on a.ksh=g.ksh");
                //            //如果当前考生没有会考成绩，则也不符合条件。
                //            stb.Append(" and g.Dldj!='D' and g.Swdj!='D'");
                //            break;
                //    }
                //    //性别限制标识：0、男女不限；1、男生；2、女生
                //    switch (this._xztj.Qttj.Xbxz)
                //    {
                //        case 1://男生
                //            stb.Append(" and a.ksh in(select ksh from zk_ksxxgl where xbdm=1)");
                //            break;
                //        case 2://女生
                //            stb.Append(" and a.ksh in(select ksh from zk_ksxxgl where xbdm=2)");
                //            break;
                //        default:
                //            break;
                //    }
                //}
                #endregion
            }
            if (where.Length > 0)
            {
                if (xxdm != "")
                {
                    stb.Append(" and xxdm='" + xxdm + "'  and a.ksh in (select ksh from zk_lqk where  ISNULL(td_zt,0)=0) ");
                }
                else
                    stb.Append(" and a.ksh in (select ksh from zk_lqk where  ISNULL(td_zt,0)=0) ");
                if (sfzbsTD == 1)
                {
                    stb.Append(" and a.ksh in (select ksh from zk_TJS_gr ) ");
                }
                if (fenxq != "")//分多县区
                {
                    stb.Append(" and left(a.ksh,3) in (" + fenxq + ") ");
                }
                //if (xqdm == "0682" && (pcdm == "12" || pcdm == "14"))
                //{
                //    stb.Append("  and isnull(c.kslbdm,1)=1 ");
                //}
                stb.Append(" and xxdm not in (select xxdm from zk_TJS_XXSD )");
            }
            sql.Append("select ");
            sql.Append(fields);
            sql.Append(" from ");
            sql.Append(tabs);
            sql.Append(" where ");
            sql.Append(String.Format(where, stb.ToString()));
            return sql.ToString();
        }
        /// <summary>
        /// 创查询条件。志愿优先
        /// </summary>
        private string createSQLZYYX(string pcdm, string zysx, string xxdm, int sfzbsTD, string xqdm)
        {
            StringBuilder sql = new StringBuilder();
            //sql = String.Format("select a.*,b.cj,b.xqdm,c.sfzbs from zk_kszyxx a left join zk_kszyxx_ks_zcj_view b on a.ksh=b.ksh left join zk_ksxxgl c on a.ksh=c.ksh where sfbk=1 and pcdm=@pcdm {0} and b.cj>=(select MIN(yc_ZdFensuKzx) from zk_Yc_XqYcZdFsx) order by cj DESC,ksh asc ,zysx asc", stb.ToString());
            string fields = "a.*,b.cj,wh_cj=b.cj,b.xqdm,c.sfzbs,c.bmddm,yw,sx,yy,zf,whzf";
            string tabs = "zk_kszyxx a left join zk_kszyxx_ks_zcj_view b on a.ksh=b.ksh left join zk_ksxxgl c on a.ksh=c.ksh";
            // string where = "sfbk=1 and len(xxdm)>0 and pcdm=@pcdm {0} and b.cj>=(select MIN(yc_ZdFensuKzx) from zk_Yc_XqYcZdFsx) order by cj DESC,ksh asc ,zysx asc";
            string where = "";
            if (pcdm.Substring(0, 1) == "1")//第一批需要加县区
            {
                where = "sfbk=1 and len(xxdm)>0 and pcdm=@pcdm and b.xqdm=@xqdm {0}   order by cj DESC, yw+sx+yy desc,ksh asc ,zysx asc";
            }
            else
            {
                where = "sfbk=1 and len(xxdm)>0 and pcdm=@pcdm  {0}   order by cj DESC, yw+sx+yy desc,ksh asc ,zysx asc";
            }
            if (xqdm == "0601" || xqdm == "0621")
            {
                where = "sfbk=1 and len(xxdm)>0 and pcdm=@pcdm and b.xqdm=@xqdm {0}   order by cj DESC, yw+sx+yy desc, yw+sx desc,sx  desc,ksh asc ,zysx asc";
            }
            else if (xqdm == "0684")
            {
                where = "sfbk=1 and len(xxdm)>0 and pcdm=@pcdm and b.xqdm=@xqdm {0}   order by cj DESC,zf desc,whzf desc, yw+sx+yy desc, yw+sx desc,sx  desc,ksh asc ,zysx asc";
            }
            else if (xqdm == "0681")
            {
                where = "sfbk=1 and len(xxdm)>0 and pcdm=@pcdm and b.xqdm=@xqdm {0}   order by cj DESC, yw+sx+yy desc, yw+sx desc,yw  desc,ksh asc ,zysx asc";
            }
            else if (xqdm == "0683")
            {
                where = "sfbk=1 and len(xxdm)>0 and pcdm=@pcdm and b.xqdm=@xqdm {0}   order by cj DESC, yw+sx+yy desc, yw+sx desc,ksh asc ,zysx asc";
            }

            StringBuilder stb = new StringBuilder();
            if (this._xztj != null)
            {
                #region 基本条件。
                if (this._xztj.Jbtj.bFlag)
                {
                    //最低控档线设置。
                    switch (this._xztj.Jbtj.Zdkdx)
                    {
                        case 1://普高最低控档线无效。
                            if (pcdm.Substring(0, 1) == "1")//第一批需要加县区
                                where = "sfbk=1 and len(xxdm)>0 and pcdm=@pcdm and b.xqdm=@xqdm {0}   order by cj DESC,ksh asc ,zysx asc";
                            else
                                where = "sfbk=1 and len(xxdm)>0 and pcdm=@pcdm  {0}   order by cj DESC,ksh asc ,zysx asc";
                            break;
                        case 2://指定最低分数线。
                            if (pcdm.Substring(0, 1) == "1")//第一批需要加县区
                                where = "sfbk=1 and len(xxdm)>0 and pcdm=@pcdm  and b.xqdm=@xqdm  {0} and b.cj>=" + this._xztj.Jbtj.Zdy_zdfs + " order by cj DESC,ksh asc ,zysx asc";
                            else
                                where = "sfbk=1 and len(xxdm)>0 and pcdm=@pcdm {0} and b.cj>=" + this._xztj.Jbtj.Zdy_zdfs + " order by cj DESC,ksh asc ,zysx asc";

                            break;
                    }
                }
                #endregion

                #region 指标生
                //判断指标生投档条件是否定制有效。
                if (this._xztj.Zbs.bFlag)
                {
                    //是否有指标生。
                    if (this._xztj.Zbs.Ywzbs == 0)
                    {
                        fields = "a.*, cj=(b.cj + isnull(g.fs,0)),wh_cj=b.cj,b.xqdm,c.sfzbs,isnull(g.fs,0) fs,c.bmddm,yw,sx,yy,zf,whzf";
                        tabs = String.Format("{0} left join {1}", tabs, "  zk_TJS_gr g on a.ksh=g.ksh and a.xxdm=g.tjxxdm and a.zysx=1  ");
                    }
                }
                #endregion

                #region 往届生

                if (this._xztj.Szpj.bFlag)
                {
                    //0是无.1是有
                    if (this._xztj.Szpj.Zhszxztj == 1)
                    {
                        fields = "a.*, cj=(b.cj - isnull(j.fs,0)),wh_cj=b.cj,b.xqdm,c.sfzbs,isnull(j.fs,0) fs,c.bmddm,yw,sx,yy,zf,whzf";
                        tabs = String.Format("{0} left join {1}", tabs, "  zk_TJS_wjs j on a.ksh=j.ksh  ");
                    }
                }
                #endregion
                if (pcdm == "02") //提前第二小批有  中考+专业成绩投
                {
                    fields = "a.*, cj=(b.cj + isnull(h.fs,0)),wh_cj=b.cj,b.xqdm,c.sfzbs,isnull(h.fs,0) fs,c.bmddm,yw,sx,yy,zf,whzf";
                    tabs = String.Format("{0} left join {1}", tabs, "  zk_TJS_yt h on a.ksh=h.ksh ");
                }
                #region 其他条件

                if (this._xztj.Qttj.bFlag)
                {
                    ////加试成绩是否要求合格。
                    //switch (this._xztj.Qttj.Jscjxzif)
                    //{
                    //    case 0://艺术类。
                    //        tabs = String.Format("{0} left join {1}", tabs, " zk_ksjscj e on a.ksh=e.ksh ");
                    //        stb.Append(" and e.ys_sfhg=1");
                    //        break;
                    //    case 1://师范类。
                    //        tabs = String.Format("{0} left join {1}", tabs, " zk_ksjscj e on a.ksh=e.ksh ");
                    //        stb.Append(" and e.sf_sfhg=1");
                    //        break;
                    //    case 2://免费师范(男生)类
                    //        tabs = String.Format("{0} left join {1}", tabs, " zk_ksjscj e on a.ksh=e.ksh ");
                    //        stb.Append(" and e.mh_sf_ns_sfhg=1");
                    //        break;
                    //}
                    ////与文化成绩合并的加试成绩。
                    //switch (this._xztj.Qttj.Ywhcjhb_jscj)
                    //{
                    //    case 0://艺术类。
                    //        fields = "a.*,cj=(b.cj+f.ys_l),wh_cj=b.cj,b.xqdm,c.sfzbs";
                    //        tabs = String.Format("{0} left join {1}", tabs, " zk_ksjscj f on a.ksh=f.ksh ");
                    //        break;
                    //    case 1://师范类。
                    //        fields = "a.*,cj=(b.cj+f.sf_l),wh_cj=b.cj,b.xqdm,c.sfzbs";
                    //        tabs = String.Format("{0} left join {1}", tabs, " zk_ksjscj f on a.ksh=f.ksh ");
                    //        break;
                    //    case 2://免费师范(男生)类
                    //        fields = "a.*,cj=(b.cj+f.mh_sf_ns_l),wh_cj=b.cj,b.xqdm,c.sfzbs";
                    //        tabs = String.Format("{0} left join {1}", tabs, " zk_ksjscj f on a.ksh=f.ksh ");
                    //        break;
                    //}

                    ///*
                    // * 考生会考成绩合格
                    // * 地理、ABCD
                    // * 生物、ABCD
                    // */
                    ////会考成绩是否合格。
                    //switch (this._xztj.Qttj.HkcjhgXz)
                    //{
                    //    case 0://会考成绩必须合格。
                    //        tabs = String.Format("{0} left join {1}", tabs, "zk_kshkcj g on a.ksh=g.ksh");
                    //        //如果当前考生没有会考成绩，则也不符合条件。
                    //        stb.Append(" and g.Dldj!='D' and g.Swdj!='D'");
                    //        break;
                    //}
                    ////性别限制标识：0、男女不限；1、男生；2、女生
                    //switch (this._xztj.Qttj.Xbxz)
                    //{
                    //    case 1://男生
                    //        stb.Append(" and a.ksh in(select ksh from zk_ksxxgl where xbdm=1)");
                    //        break;
                    //    case 2://女生
                    //        stb.Append(" and a.ksh in(select ksh from zk_ksxxgl where xbdm=2)");
                    //        break;
                    //    default:
                    //        break;
                    //}
                }
                #endregion
            }
            if (where.Length > 0) //第几志愿
            {
                if (xxdm != "")
                {
                    stb.Append(" and zysx=" + zysx + " and xxdm='" + xxdm + "'  and a.ksh in (select ksh from zk_lqk where  ISNULL(td_zt,0)=0) ");
                }
                else
                    stb.Append(" and zysx=" + zysx + "  and a.ksh in (select ksh from zk_lqk where  ISNULL(td_zt,0)=0) ");

                if (pcdm == "01" && zysx == "1") //查询男合格
                {
                    stb.Append("  and a.ksh in (select ksh from zk_hege where  ISNULL(type,0)=1) ");
                }
                else if (pcdm == "01" && zysx == "2") //查询艺术
                {
                    stb.Append("  and a.ksh in (select ksh from zk_hege where  ISNULL(type,0)=2) ");
                }
                else if (pcdm == "01" && zysx == "3") //查询艺术
                {
                    stb.Append("  and a.ksh in (select ksh from zk_hege where  ISNULL(type,0)=4) ");
                }
                if (sfzbsTD == 1)
                {
                    stb.Append(" and a.ksh in (select ksh from zk_TJS_gr ) ");
                }
                if (xqdm == "0682" && (pcdm == "12" || pcdm == "14"))
                {
                    stb.Append("  and isnull(c.kslbdm,1)=1 ");
                }

                stb.Append(" and xxdm not in (select xxdm from zk_TJS_XXSD )");
            }

            sql.Append("select ");
            sql.Append(fields);
            sql.Append(" from ");
            sql.Append(tabs);
            sql.Append(" where ");
            sql.Append(String.Format(where, stb.ToString()));
            return sql.ToString();
        }
        /// <summary>
        /// 平行志愿算法。
        /// </summary>
        /// <param name="pcdm">各个县区的最低分数控制线。</param>
        private void PingXinSuanFa(string pcdm, DataTable zdFsKzx, string xqdm, string xxdm, int tdnum, double bil, int type, int sfzbsTD,bool chxq,int sftzs,string fenxq)
        {
            //非指标生招生计划(视图：validate_view，测试用)；
            //"select xqdm,zsxxdm=xxdm,f_zbssl=(jhs-zbssl),zydm,yt_sl=yitd_fzbs_sl,zdfs=0.0 from validate_view where pcdm=@pcdm order by xxdm asc";
            string sql = "";

//            if (pcdm == "11")   //配额投档
//            {
//                sql = " select * from  View_dxs  where pcdm=@pcdm   order by xxdm asc";

//            }
//            else if (pcdm == "21")//配转统
//            {
//                if (chxq)
//                {
//                    sql = @"  select  xqdm ,zsxxdm,SUM(sysl) f_zbssl,0 yt_sl,0.0 zdfs  from (
//                       select  xqdm, zsxxdm,  SUM( f_zbssl)-SUM( yt_sl) as sysl   from View_dxs
//                       group by xqdm,zsxxdm 
//                       ) a GROUP BY xqdm,zsxxdm ";
//                }else
//                sql = @" select '500' xqdm ,zsxxdm,SUM(sysl) f_zbssl,0 yt_sl,0.0 zdfs  from (
//                       select   zsxxdm,  SUM( f_zbssl)-SUM( yt_sl) as sysl   from View_dxs
//                       group by zsxxdm 
//                       ) a GROUP BY zsxxdm ";
//            }
//            else
            if (sftzs == 2) //2是配额 3 是配转统
            {
                sql = " select * from  View_dxs  where pcdm=@pcdm   order by xxdm asc";
            }
            else
                sql = "select a.xqdm,zsxxdm=xxdm,f_zbssl=(jhs-zbssl),zydm,yt_sl=yitd_fzbs_sl,zdfs=isnull(zdfs,0.0) from validate_view a left join lq_zdfs_view b on a.xqdm=b.xqdm and a.xxdm=b.lqxx  and a.pcdm=b.pcdm  where a.pcdm=@pcdm   order by xxdm asc";

            //铜仁不需要分县区
            // if (pcdm == "01")
            //   sql = "select a.xqdm,zsxxdm=xxdm,f_zbssl=(jhs-zbssl),zydm,yt_sl=yitd_fzbs_sl,zdfs=isnull(zdfs,0.0) from validate_view a left join lq_zdfs_view b on a.xqdm=b.xqdm and a.xxdm=b.lqxx  and a.pcdm=b.pcdm  where a.pcdm=@pcdm   order by xxdm asc";
            //else
            //    sql = "select a.xqdm,zsxxdm=xxdm,f_zbssl=(jhs-zbssl),zydm,yt_sl=yitd_fzbs_sl,zdfs=isnull(zdfs,0.0) from validate_view a left join lq_zdfs_view b on a.xqdm=b.xqdm and a.xxdm=b.lqxx  and a.pcdm=b.pcdm  where a.pcdm=@pcdm  and a.xqdm=@xqdm order by xxdm asc";
            List<string> pclist = new List<string>() { "11", "21", "31", "41" };//第二批次
            List<string> pclist2 = new List<string>() { "51", "61", "71", "81", "91" };//第二批次
            string pc = "";
            if (pclist.Contains(pcdm))
                pc = "11";
            if (pclist2.Contains(pcdm))
                pc = "21";
            if (pcdm == "01")
            {
                pc = "01";
            }

            string pcdmjh = pcdm;
            if (pcdmjh != "01")
                pcdmjh = "11";
            List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@pcdm", pcdmjh), new SqlParameter("@xqdm", xqdm) };


            //招生学校在各县区各专业招生人数。
            tab_f_zbs_zsjh = this._dbA.execReTab_Tran(sql, lisP);
            tab_tfgj = new DataTable();
            tab_tfgj.Columns.Add("ksh");
            tab_tfgj.Columns.Add("xqdm");
            tab_tfgj.Columns.Add("zsxxdm");
            tab_tfgj.Columns.Add("yw");
            tab_tfgj.Columns.Add("sx");
            tab_tfgj.Columns.Add("yy");
            tab_tfgj.Columns.Add("zf");
            tab_tfgj.Columns.Add("whzf");
            ////判断是否有指标生。
            //if (this._xztj.Zbs.Ywzbs == 0)
            //{
            //    /*
            //     * 查询指标生的招生计划。
            //     */
            //    //sql = "select zsxxdm,byxxdm=xxdm,zbssl,yt_sl=0,zdfs=0.0 from zk_lq_zbs_jhk where pcdm=@pcdm order by zsxxdm ASC";
            //    sql = "select zsxxdm,byxxdm=xxdm,zbssl,yt_sl=ISNULL(b.yt_sl,0),zdfs=ISNULL(b.cj,0.0) from zk_lq_zbs_jhk a left join zbs_yt_xx b on a.pcdm=b.pcdm and a.zsxxdm=b.lqxx and a.xxdm=b.byxxdm where a.pcdm=@pcdm order by zsxxdm ASC";
            //    //sql = "select a.*,yt_sl=isnull(b.yt_sl,0) from zk_lq_zbs_jhk a left join zbs_yt_xx b on a.zsxxdm=b.lqxx and a.xxdm=b.byxxdm where a.pcdm=@pcdm";
            //    this.tab_zbs_zsjh = this._dbA.execReTab_Tran(sql, lisP);
            //}

            //查询当前批次所有已报考的考生按成绩的高低排序。
            sql = createSQL(pcdm, xxdm, sfzbsTD, xqdm, chxq, fenxq, sftzs);
            List<SqlParameter> lisPa = new List<SqlParameter>() { new SqlParameter("@pcdm", pcdm), new SqlParameter("@xqdm", xqdm) };

            //sql = String.Format("select a.*,b.cj,b.xqdm,c.sfzbs from zk_kszyxx a left join zk_kszyxx_ks_zcj_view b on a.ksh=b.ksh left join zk_ksxxgl c on a.ksh=c.ksh where sfbk=1 and pcdm=@pcdm {0} and b.cj>=(select MIN(yc_ZdFensuKzx) from zk_Yc_XqYcZdFsx) order by cj DESC,ksh asc ,zysx asc", strWhere);
            //sql = "select a.*,b.cj,b.xqdm,c.sfzbs from zk_kszyxx a left join zk_kszyxx_ks_zcj_view b on a.ksh=b.ksh left join zk_ksxxgl c on a.ksh=c.ksh where sfbk=1 and pcdm=@pcdm " + strWhere + " order by cj DESC,ksh asc ,zysx asc";
            DataTable ksInfo = this._dbA.execReTab_Tran(sql, lisPa);

            DataRow row = null;
            DataRow[] rows = null;
            //ksInfo;
            #region 平行志愿算法。
            string begin_ksh = "";
            bool bFlag = false;
            int tdsl = 0;//记录已投档的数量
            List<string> listksh = new List<string>();
            /*
             * 算法说明：
             * 1、第一次筛选：查询所有符合基本条件的考生信息；
             * 2、第二次筛选：循环所有考生信息，从第一个考生开始进行，判断当前考生的分数是否在最低控制线以下；
             * 3、第三次筛选：判断当前考生是否已被录取；
             */
            while (true)
            {
                //判断当前考生信息中是否还有未投档的考生。
                if (ksInfo.Rows.Count < 1)
                {
                    //如果没有则结束当前批次投档。
                    break;
                }
                _iCount++;
                //如果还有，则提取第一索引位置的考生信息。
                row = ksInfo.Rows[0];

                if (sftzs == 2)
                {
                    if (tab_f_zbs_zsjh.Select(" xxdm='" + row["bmddm"].ToString() + "' and zsxxdm='" + row["xxdm"].ToString() + "'").Length == 0)
                    {
                        ksInfo.Rows.Remove(row);
                        continue;
                    }
                }

                //判断上一个考生信息是否投档成功；如果成功，则判断当前考生的报名号是否与上一个考生的报名号相同。
                if (listksh.Contains(row["ksh"].ToString()))
                {
                    ksInfo.Rows.Remove(row);
                    continue;
                }
                if (bFlag && begin_ksh == row["ksh"].ToString())
                {
                    //如果两个条件都成立，则表示当前考生是成功投档，不能再重复投档，移除当前考生这条记录。
                    ksInfo.Rows.Remove(row);
                    continue;
                }
                else
                {
                    //如果两个条件中有一个不成立，则将标识置为false，表示是一个新的考生信息。
                    bFlag = false;
                }
                //提取当前新的考生信息的报名号。
                begin_ksh = row["ksh"].ToString();

                //判断最低控档线是否有效：1、表示无效。
                if (this._xztj.Jbtj.Zdkdx != 1)
                {
                    //查询当前县区的最低分数线。
                    rows = zdFsKzx.Select("yc_Xqdm='" + ksInfo.Rows[0]["xqdm"].ToString() + "'");

                    //判断当前考生的总成绩是否小于当前县区的最低分数线；不满足条件，则移除当前考生的这一条数据（一个考生可能有多条信息，一个志愿一条数据）。
                    if (rows.Length < 1 || decimal.Parse(row["cj"].ToString()) < decimal.Parse(rows[0]["yc_ZdFensuKzx"].ToString()))
                    {
                        bFlag = true;
                        ksInfo.Rows.Remove(row);
                        continue;
                    }
                }
                //开始进行投档判断。
                if (tdsl == tdnum)
                {
                    //   break;
                }
                
                //开始进行投档判断。
                if (f_zbs_t_d_XueXiao(row, "00", tdnum, bil, type, pcdmjh, xqdm, chxq, sftzs, pc))
                {
                    tdsl++;
                    //如果投档成功，则将标识置为true，表示这一个考生已成功投档，如果下一个考生的信息与当前这个考生信息相同，则不需要再次投档。
                    bFlag = true;
                }
                if (bFlag)
                {
                    listksh.Add(row["ksh"].ToString());

                }
                else
                    ksInfo.Rows.Remove(row);

                continue;
           
            }
            #endregion
            if (sftzs == 2)
            {
                this._stbSql.Append(" update  zk_lqk set xqdm='500' where  pcdm='" + pcdmjh + "';");
            }
            if (chxq)
                this._stbSql.Append(" update  zk_lqk set xqdm='500' where  pcdm='" + pcdmjh + "';");
            if (this._stbSql.Length > 0)
            {
                this._dbA.execSql_Tran(this._stbSql.ToString());
                this._stbSql.Clear();
            }
            //  this._dbA.execSql_Tran("update zk_kszkcj set state =1 where kmdm='88' and ksh in(select ksh from zk_lqk where pcdm='" + pcdm + "')");
            return;
        }
        /// <summary>
        ///志愿优先算法。
        /// </summary>
        /// <param name="pcdm">各个县区的最低分数控制线。</param>
        private void ZyyxSuanFa(string pcdm, DataTable zdFsKzx, string xqdm, string zysx, int tdnum, string xxdm, double bil, int type, int sfzbsTD)
        {
            //非指标生招生计划(视图：validate_view，测试用)；
            //"select xqdm,zsxxdm=xxdm,f_zbssl=(jhs-zbssl),zydm,yt_sl=yitd_fzbs_sl,zdfs=0.0 from validate_view where pcdm=@pcdm order by xxdm asc";
            string sql = "";
            if (pcdm == "01")
                sql = "select a.xqdm,zsxxdm=xxdm,f_zbssl=(jhs-zbssl),zydm,yt_sl=yitd_fzbs_sl,zdfs=isnull(zdfs,0.0) from validate_view a left join lq_zdfs_view b on a.xqdm=b.xqdm and a.xxdm=b.lqxx  and a.pcdm=b.pcdm  where a.pcdm=@pcdm   order by xxdm asc";
            else
                sql = "select a.xqdm,zsxxdm=xxdm,f_zbssl=(jhs-zbssl),zydm,yt_sl=yitd_fzbs_sl,zdfs=isnull(zdfs,0.0) from validate_view a left join lq_zdfs_view b on a.xqdm=b.xqdm and a.xxdm=b.lqxx  and a.pcdm=b.pcdm  where a.pcdm=@pcdm  and a.xqdm=@xqdm  order by xxdm asc";

            List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@pcdm", pcdm), new SqlParameter("@xqdm", xqdm) };

            //招生学校在各县区各专业招生人数。
            tab_f_zbs_zsjh = this._dbA.execReTab_Tran(sql, lisP);
            tab_tfgj = new DataTable();
            tab_tfgj.Columns.Add("ksh");
            tab_tfgj.Columns.Add("xqdm");
            tab_tfgj.Columns.Add("zsxxdm");
            tab_tfgj.Columns.Add("yw");
            tab_tfgj.Columns.Add("sx");
            tab_tfgj.Columns.Add("yy");
            tab_tfgj.Columns.Add("zf");
            tab_tfgj.Columns.Add("whzf");
            //判断是否有指标生。
            //if (this._xztj.Zbs.Ywzbs == 0)
            //{
            //    /*
            //     * 查询指标生的招生计划。
            //     */
            //    //sql = "select zsxxdm,byxxdm=xxdm,zbssl,yt_sl=0,zdfs=0.0 from zk_lq_zbs_jhk where pcdm=@pcdm order by zsxxdm ASC";
            //    sql = "select zsxxdm,byxxdm=xxdm,zbssl,yt_sl=ISNULL(b.yt_sl,0),zdfs=ISNULL(b.cj,0.0) from zk_lq_zbs_jhk a left join zbs_yt_xx b on a.pcdm=b.pcdm and a.zsxxdm=b.lqxx and a.xxdm=b.byxxdm where a.pcdm=@pcdm order by zsxxdm ASC";
            //    //sql = "select a.*,yt_sl=isnull(b.yt_sl,0) from zk_lq_zbs_jhk a left join zbs_yt_xx b on a.zsxxdm=b.lqxx and a.xxdm=b.byxxdm where a.pcdm=@pcdm";
            //    this.tab_zbs_zsjh = this._dbA.execReTab_Tran(sql, lisP);
            //}

            //查询当前批次所有已报考的考生按成绩的高低排序。

            sql = createSQLZYYX(pcdm, zysx, xxdm, sfzbsTD, xqdm);

            //sql = String.Format("select a.*,b.cj,b.xqdm,c.sfzbs from zk_kszyxx a left join zk_kszyxx_ks_zcj_view b on a.ksh=b.ksh left join zk_ksxxgl c on a.ksh=c.ksh where sfbk=1 and pcdm=@pcdm {0} and b.cj>=(select MIN(yc_ZdFensuKzx) from zk_Yc_XqYcZdFsx) order by cj DESC,ksh asc ,zysx asc", strWhere);
            //sql = "select a.*,b.cj,b.xqdm,c.sfzbs from zk_kszyxx a left join zk_kszyxx_ks_zcj_view b on a.ksh=b.ksh left join zk_ksxxgl c on a.ksh=c.ksh where sfbk=1 and pcdm=@pcdm " + strWhere + " order by cj DESC,ksh asc ,zysx asc";
            DataTable ksInfo = this._dbA.execReTab_Tran(sql, lisP);

            DataRow row = null;
            DataRow[] rows = null;
            //ksInfo;
            #region 平行志愿算法。
            string begin_ksh = "";
            bool bFlag = false;
            int tdsl = 0;//记录已投档的数量
            List<string> listksh = new List<string>();
            /*
             * 算法说明：
             * 1、第一次筛选：查询所有符合基本条件的考生信息；
             * 2、第二次筛选：循环所有考生信息，从第一个考生开始进行，判断当前考生的分数是否在最低控制线以下；
             * 3、第三次筛选：判断当前考生是否已被录取；
             */
            while (true)
            {
                //判断当前考生信息中是否还有未投档的考生。
                if (ksInfo.Rows.Count < 1)
                {
                    //如果没有则结束当前批次投档。
                    break;
                }
                _iCount++;
                //如果还有，则提取第一索引位置的考生信息。
                row = ksInfo.Rows[0];
                //判断上一个考生信息是否投档成功；如果成功，则判断当前考生的报名号是否与上一个考生的报名号相同。
                if (listksh.Contains(row["ksh"].ToString()))
                {
                    ksInfo.Rows.Remove(row);
                    continue;
                }
                if (bFlag && begin_ksh == row["ksh"].ToString())
                {
                    //如果两个条件都成立，则表示当前考生是成功投档，不能再重复投档，移除当前考生这条记录。
                    ksInfo.Rows.Remove(row);
                    continue;
                }
                else
                {
                    //如果两个条件中有一个不成立，则将标识置为false，表示是一个新的考生信息。
                    bFlag = false;
                }
                //提取当前新的考生信息的报名号。
                begin_ksh = row["ksh"].ToString();

                //判断最低控档线是否有效：1、表示无效。
                if (this._xztj.Jbtj.Zdkdx != 1)
                {
                    //查询当前县区的最低分数线。
                    rows = zdFsKzx.Select("yc_Xqdm='" + ksInfo.Rows[0]["xqdm"].ToString() + "'");

                    //判断当前考生的总成绩是否小于当前县区的最低分数线；不满足条件，则移除当前考生的这一条数据（一个考生可能有多条信息，一个志愿一条数据）。
                    if (rows.Length < 1 || decimal.Parse(row["cj"].ToString()) < decimal.Parse(rows[0]["yc_ZdFensuKzx"].ToString()))
                    {
                        bFlag = true;
                        ksInfo.Rows.Remove(row);
                        continue;
                    }
                }
                //开始进行投档判断。
                //if (tdsl == tdnum)
                //{
                //     break;
                //}
                if (f_zbs_t_d_XueXiao_ZYYX(row, "00", zysx, tdnum, bil, type, pcdm, xqdm))
                {
                    tdsl++;
                    //如果投档成功，则将标识置为true，表示这一个考生已成功投档，如果下一个考生的信息与当前这个考生信息相同，则不需要再次投档。
                    bFlag = true;
                }

                if (bFlag)
                {
                    listksh.Add(row["ksh"].ToString());

                }
                else
                    ksInfo.Rows.Remove(row);
                continue;
                #region 原来根据考生报考的专业进行录取的判断。
                /*
                 * 原意：判断在当前批次报考的招生学校是否有专业。
                 */
                //if (row["zy1"] == null || row["zy1"].ToString().Trim().Length < 1)
                //{
                //    if (f_zbs_t_d_XueXiao(row, "00"))
                //    {
                //        bFlag = true;
                //    }
                //    else
                //    {
                //        ksInfo.Rows.Remove(row);
                //        continue;
                //    }
                //}
                //else
                //{
                //    if (f_zbs_t_d_XueXiao(row, row["zy1"].ToString()))
                //    {
                //        bFlag = true;
                //        ksInfo.Rows.Remove(row);
                //        continue;
                //    }
                //    else
                //    {
                //        ksInfo.Rows.Remove(row);
                //        continue;
                //    }
                //    if (row["zy2"] != null)
                //    {
                //        if (f_zbs_t_d_XueXiao(row, row["zy2"].ToString()))
                //        {
                //            bFlag = true;
                //            ksInfo.Rows.Remove(row);
                //            continue;
                //        }
                //        else
                //        {
                //            ksInfo.Rows.Remove(row);
                //            continue;
                //        }
                //        if (row["zy3"] != null)
                //        {
                //            if (f_zbs_t_d_XueXiao(row, row["zy3"].ToString()))
                //            {
                //                bFlag = true;
                //                ksInfo.Rows.Remove(row);
                //                continue;
                //            }
                //            else
                //            {
                //                ksInfo.Rows.Remove(row);
                //                continue;
                //            }
                //            if (row["zy4"] != null)
                //            {
                //                if (f_zbs_t_d_XueXiao(row, row["zy4"].ToString()))
                //                {
                //                    bFlag = true;
                //                    ksInfo.Rows.Remove(row);
                //                    continue;
                //                }
                //                else
                //                {
                //                    ksInfo.Rows.Remove(row);
                //                    continue;
                //                }
                //                if (row["zy5"] != null)
                //                {
                //                    if (f_zbs_t_d_XueXiao(row, row["zy5"].ToString()))
                //                    {
                //                        bFlag = true;
                //                        ksInfo.Rows.Remove(row);
                //                        continue;
                //                    }
                //                    else
                //                    {
                //                        ksInfo.Rows.Remove(row);
                //                        continue;
                //                    }
                //                    if (row["zy6"] != null)
                //                    {
                //                        if (f_zbs_t_d_XueXiao(row, row["zy6"].ToString()))
                //                        {
                //                            bFlag = true;
                //                            ksInfo.Rows.Remove(row);
                //                            continue;
                //                        }
                //                        else
                //                        {
                //                            ksInfo.Rows.Remove(row);
                //                            continue;
                //                        }
                //                        if (row["zy7"] != null)
                //                        {
                //                            if (f_zbs_t_d_XueXiao(row, row["zy7"].ToString()))
                //                            {
                //                                bFlag = true;
                //                                ksInfo.Rows.Remove(row);
                //                                continue;
                //                            }
                //                            else
                //                            {
                //                                ksInfo.Rows.Remove(row);
                //                                continue;
                //                            }
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}
                #endregion
            }
            #endregion

            if (pcdm == "01")
            {
                this._stbSql.Append(" update  zk_lqk set xqdm='500' where  pcdm='01';");
            }
            if (this._stbSql.Length > 0)
            {
                this._dbA.execSql_Tran(this._stbSql.ToString());
                this._stbSql.Clear();
            }
            // this._dbA.execSql_Tran("update zk_kszkcj set state =1 where kmdm='88' and ksh in(select ksh from zk_lqk where pcdm='" + pcdm + "')");
            return;
        }
        /// <summary>
        /// 非指标生投档：将考生向学校投档(true、已投档；false、未投档)
        /// </summary>
        /// <param name="row">考生信息</param>
        /// <param name="zuanyedm">考生报考的专业代码</param>
        private bool f_zbs_t_d_XueXiao(DataRow row, string zuanyedm, int tdnum, double bil, int type, string pcdm, string tdxqdm, bool chxq,int sftzs,string sfpc)
        {
            if (row["ksh"] == null || row["ksh"].ToString().Length < 1 || row["cj"] == null || row["cj"].ToString().Length < 1)
            {
                return false;
            }
            /*
             * 判断当前考生是否符合其它限制条件，此条件在查询考生信息时，已进行过滤。
             */
            //if (!Filter_Qttj(row["ksh"].ToString()))
            //{
            //    return false;
            //}
            //查询当前学校的当前专业在当前县区已投档数量。
            //string sql = "select COUNT(1) from zk_toudang_kaosen_info where pcdm='"+row["pcdm"].ToString()+"' and lqxx='"+row["xxdm"].ToString()+"' and xianQudm='"+row["xqdm"].ToString()+"' and zuanyeDm='"+zuanyedm+"'";

            //select xqdm,xxdm,fzbssl=jhs-zbssl,zbssl,zydm,yt_sl=0 from Xq_Zsxx_jh_view where pcdm=@pcdm order by pcdm,xxdm,xqdm asc
            //查找已投档数量。
            //DataRow[] rows = tab_jhs.Select("xxdm='" + row["xxdm"].ToString() + "' and xqdm='" + row["xqdm"].ToString() + "' and zydm='" + zuanyedm + "'");
            //DataRow[] rows = this.tab_f_zbs_zsjh.Select("zsxxdm='" + row["xxdm"].ToString() + "' and zydm='" + zuanyedm + "'");

            /*
             * 查询当前招生学校非指标生招生计划。
             * 从招生计划中查询招生学校的信息，只有一个条件就是当前考生是否报考了对应的招生学校。
             */
            DataRow[] tz_rows;

            if (sftzs == 2) //配额
            {
                tz_rows = this.tab_f_zbs_zsjh.Select(String.Format("zsxxdm='{0}' and xqdm='{1}' and xxdm='{2}'", row["xxdm"].ToString(), row["xqdm"].ToString(), row["bmddm"].ToString()));
                if (tz_rows == null || tz_rows.Length < 1)
                {
                    return false;
                    //tz_rows = this.tab_f_zbs_zsjh.Select(String.Format("zsxxdm='{0}'", row["xxdm"].ToString()));
                    //if (tz_rows == null || tz_rows.Length < 1)
                    //{
                    //    return false;
                    //}
                }
            }
            else
            {
                if (chxq)
                {
                    tz_rows = this.tab_f_zbs_zsjh.Select(String.Format("zsxxdm='{0}' and xqdm='{1}' ", row["xxdm"].ToString(), row["xqdm"].ToString()));
                    if (tz_rows == null || tz_rows.Length < 1)
                    {
                        tz_rows = this.tab_f_zbs_zsjh.Select(String.Format("zsxxdm='{0}'", row["xxdm"].ToString()));
                        if (tz_rows == null || tz_rows.Length < 1)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    tz_rows = this.tab_f_zbs_zsjh.Select(String.Format("zsxxdm='{0}' and xqdm='{1}'", row["xxdm"].ToString(), row["xqdm"].ToString()));
                    if (tz_rows == null || tz_rows.Length < 1)
                    {
                        tz_rows = this.tab_f_zbs_zsjh.Select(String.Format("zsxxdm='{0}'", row["xxdm"].ToString()));
                        if (tz_rows == null || tz_rows.Length < 1)
                        {
                            return false;
                        }
                    }
                }
            }
            //int index = 0;
            ////如果查询的招生学校记录有多条，则表示当前招生学校可能面对各个县区有相应的招生计划。
            //if (tz_rows.Length > 1)
            //{
            //    //筛选当前考生所在县区的招生计划。
            //    for (int i = 0; i < tz_rows.Length; i++)
            //    {
            //        if (tz_rows[i]["xqdm"].ToString().Trim() == row["xqdm"].ToString().Trim())
            //        {
            //            index = i;
            //            break;
            //        }
            //    }
            //}

            int jhsl = 0;
            int ytdsl = 0;
            decimal fzbs_zdfs = 0;
       

            //标识当前考生录取类型：0、统招生；1、是指标生。
            int sf_zbs = 0;
            //0、不是同分跟进；1、同分跟进。
            int sf_tfgj = 0;

            if (tz_rows.Length < 1)
            {
                return false;
            }

            DataRow tz_row = tz_rows[0];
            //计划数量(当前学校非指标生总计划数)
            jhsl = int.Parse(tz_row["f_zbssl"].ToString());
            //取出已投档数量
          
            ytdsl = int.Parse(tz_row["yt_sl"].ToString());
            //最低分数(初始0)。
            fzbs_zdfs = decimal.Parse(tz_row["zdfs"].ToString());
           
            //(统招)判断计划数与当前已投档数。
            int bianliang = 0;
            if (type == 1)
            {
                bianliang = Convert.ToInt32(jhsl * bil);
            }
            else
            {
                if (sftzs == 2)
                    bianliang = jhsl;
                else
                bianliang = tdnum;
            }
            
            if (ytdsl >= bianliang)
            {
                //查询当前已执行的最低分数。
                if (decimal.Parse(row["cj"].ToString()) < fzbs_zdfs)
                {
                    return false;
                    ////判断指标生投档条件是否定制有效。
                    //if (!this._xztj.Zbs.bFlag)
                    //{
                    //    return false;
                    //}
                    ////当前批次是否有指标生。
                    //if (this._xztj.Zbs.Ywzbs == 1)
                    //{
                    //    return false;
                    //}
                    ////判断考生是否是指标生：0、非指标生；1、是指标生。
                    //if (row["sfzbs"].ToString() == "0")
                    //{
                    //    return false;
                    //}
                    ////判断指标生在自定义分数的条件下，不小于统招分数线下多少分。
                    //if (this._xztj.Zbs.Zbslqfsxz == 1)
                    //{
                    //    if (decimal.Parse(row["cj"].ToString()) + this._xztj.Zbs.Zdyfs > fzbs_zdfs)
                    //    {
                    //        return false;
                    //    }
                    //}
                    //判断是否符合指标生条件()。
                    #region 指标生录取条件
                    //DataRow[] zbs_rows = this.tab_zbs_zsjh.Select(String.Format("zsxxdm='{0}' and byxxdm='{1}'",row["xxdm"].ToString(),row["ksh"].ToString().Substring(2, 6)));

                    //if (zbs_rows.Length < 1)
                    //{
                    //    return false;
                    //}

                    //DataRow zbs_row = zbs_rows[0];
                    ////计划数量(当前学校非指标生总计划数)
                    //jhsl = int.Parse(zbs_row["zbssl"].ToString());
                    ////当前招生学校在考生毕业学校的指标生已投档数量
                    //ytdsl = int.Parse(zbs_row["yt_sl"].ToString());
                    ////最低分数(初始0)。
                    //zbs_zdfs = decimal.Parse(zbs_row["zdfs"].ToString());
                    //if (ytdsl >= jhsl)
                    //{
                    //    //判断指标生同分跟进
                    //    if (decimal.Parse(row["cj"].ToString()) < zbs_zdfs)
                    //    {
                    //        return false;
                    //    }
                    //    else
                    //    {
                    //        sf_zbs = 1;
                    //        sf_tfgj = 1;
                    //        zbs_row["yt_sl"] = (int.Parse(zbs_row["yt_sl"].ToString()) + 1).ToString();
                    //        //zbs_row["zdfs"] = row["cj"].ToString();
                    //    }
                    //}
                    //else
                    //{
                    //    sf_zbs = 1;
                    //    sf_tfgj = 0;
                    //    zbs_row["yt_sl"] = (int.Parse(zbs_row["yt_sl"].ToString()) + 1).ToString();
                    //    zbs_row["zdfs"] = row["cj"].ToString();
                    //}
                    #endregion
                }
                else
                {
                    if (!this._xztj.Tfgj.bFlag)
                    {
                        return false;
                    }
                    else
                    {
                        switch (this._xztj.Tfgj.Sftfgj)
                        {
                            case 0:
                                return false;

                            case 1:
                                sf_zbs = 0;
                                sf_tfgj = 1;
                                tz_row["yt_sl"] = (int.Parse(tz_row["yt_sl"].ToString()) + 1).ToString();

                                break;
                            case 2:

                                if (tab_tfgj.Rows.Count > 0)
                                {
                                    DataRow[] tabbj = tab_tfgj.Select(String.Format("zsxxdm='{0}' and xqdm='{1}'", row["xxdm"].ToString(), row["xqdm"].ToString()));
                                    if (tabbj == null || tabbj.Length < 1)
                                    {
                                        tabbj = this.tab_tfgj.Select(String.Format("zsxxdm='{0}'", row["xxdm"].ToString()));
                                    }


                                    if (tabbj.Length > 0)//计较成绩
                                    {
                                        float oldhe = 0;
                                        float newhe = 0;
                                        #region    海门
                                        if (tdxqdm == "0684") //海门 
                                        {
                                            oldhe = Convert.ToSingle(tabbj[tabbj.Length - 1]["zf"]);
                                            newhe = Convert.ToSingle(row["zf"]);
                                            if (oldhe > newhe)
                                            {
                                                return false;
                                            }
                                            else if (oldhe == newhe)//相同.继续比较
                                            {
                                                oldhe = Convert.ToSingle(tabbj[tabbj.Length - 1]["whzf"]);
                                                newhe = Convert.ToSingle(row["whzf"]);
                                                if (oldhe > newhe)
                                                {
                                                    return false;
                                                }
                                                else if (oldhe == newhe)//相同.继续比较
                                                {

                                                }
                                                else
                                                {
                                                    sf_zbs = 0;
                                                    sf_tfgj = 1;
                                                    // tz_row["yt_sl"] = (int.Parse(tz_row["yt_sl"].ToString()) + 1).ToString();
                                                    this._stbSql.Append(String.Format("update zk_lqk set td_zt=0 ,lqxx='',lqzy='',pcdm='',zysx='',xx_zt=0,xq_zt=0,zydm='',xqdm='',types=0,jyzy='',td_pc='' where ksh='{0}';", tabbj[tabbj.Length - 1]["ksh"].ToString()));
                                                    tab_tfgj.Clear();
                                                    DataRow dr = tab_tfgj.NewRow();
                                                    dr["ksh"] = row["ksh"].ToString();
                                                    dr["xqdm"] = row["xqdm"].ToString();
                                                    dr["zsxxdm"] = row["xxdm"].ToString();
                                                    dr["yw"] = row["yw"].ToString();
                                                    dr["sx"] = row["sx"].ToString();
                                                    dr["yy"] = row["yy"].ToString();
                                                    dr["zf"] = row["zf"].ToString();
                                                    dr["whzf"] = row["whzf"].ToString();
                                                    tab_tfgj.Rows.Add(dr);
                                                    break;
                                                }

                                            }
                                            else
                                            {
                                                sf_zbs = 0;
                                                sf_tfgj = 1;
                                                // tz_row["yt_sl"] = (int.Parse(tz_row["yt_sl"].ToString()) + 1).ToString();
                                                this._stbSql.Append(String.Format("update zk_lqk set td_zt=0 ,lqxx='',lqzy='',pcdm='',zysx='',xx_zt=0,xq_zt=0,zydm='',xqdm='',types=0,jyzy='',td_pc='' where ksh='{0}';", tabbj[tabbj.Length - 1]["ksh"].ToString()));
                                                tab_tfgj.Clear();
                                                DataRow dr = tab_tfgj.NewRow();
                                                dr["ksh"] = row["ksh"].ToString();
                                                dr["xqdm"] = row["xqdm"].ToString();
                                                dr["zsxxdm"] = row["xxdm"].ToString();
                                                dr["yw"] = row["yw"].ToString();
                                                dr["sx"] = row["sx"].ToString();
                                                dr["yy"] = row["yy"].ToString();
                                                dr["zf"] = row["zf"].ToString();
                                                dr["whzf"] = row["whzf"].ToString();
                                                tab_tfgj.Rows.Add(dr);
                                                break;
                                            }
                                        }
                                        #endregion

                                        oldhe = Convert.ToSingle(tabbj[tabbj.Length - 1]["yw"]) + Convert.ToSingle(tabbj[tabbj.Length - 1]["sx"]) + Convert.ToSingle(tabbj[tabbj.Length - 1]["yy"]);
                                        newhe = Convert.ToSingle(row["yw"]) + Convert.ToSingle(row["sx"]) + Convert.ToSingle(row["yy"]);
                                        #region    语数英

                                        if (oldhe > newhe)
                                        {
                                            return false;
                                        }
                                        else if (oldhe == newhe)//相同.继续比较
                                        {
                                            if (tdxqdm == "0623" || tdxqdm == "0682") //如东
                                            {
                                                sf_zbs = 0;
                                                sf_tfgj = 1;
                                                tz_row["zdfs"] = row["cj"].ToString();
                                                tz_row["yt_sl"] = (int.Parse(tz_row["yt_sl"].ToString()) + 1).ToString();
                                                break;
                                            }

                                            oldhe = Convert.ToSingle(tabbj[tabbj.Length - 1]["yw"]) + Convert.ToSingle(tabbj[tabbj.Length - 1]["sx"]);
                                            newhe = Convert.ToSingle(row["yw"]) + Convert.ToSingle(row["sx"]);
                                            if (oldhe > newhe)
                                            {
                                                return false;
                                            }
                                            else if (oldhe == newhe)//相同.继续比较
                                            {
                                                if (tdxqdm == "0621" || tdxqdm == "0601" || tdxqdm == "0684") //海安
                                                {
                                                    oldhe = Convert.ToSingle(tabbj[tabbj.Length - 1]["sx"]);
                                                    newhe = Convert.ToSingle(row["sx"]);
                                                }
                                                else if (tdxqdm == "0681") //启东
                                                {
                                                    oldhe = Convert.ToSingle(tabbj[tabbj.Length - 1]["yw"]);
                                                    newhe = Convert.ToSingle(row["yw"]);
                                                }
                                                else if (tdxqdm == "0683") //通州
                                                {
                                                    sf_zbs = 0;
                                                    sf_tfgj = 1;
                                                    tz_row["zdfs"] = row["cj"].ToString();
                                                    tz_row["yt_sl"] = (int.Parse(tz_row["yt_sl"].ToString()) + 1).ToString();
                                                    break;
                                                }
                                                if (oldhe > newhe)
                                                {
                                                    return false;
                                                }
                                                else if (oldhe == newhe)//相同.继续比较
                                                {
                                                    sf_zbs = 0;
                                                    sf_tfgj = 1;
                                                    tz_row["zdfs"] = row["cj"].ToString();
                                                    tz_row["yt_sl"] = (int.Parse(tz_row["yt_sl"].ToString()) + 1).ToString();
                                                }
                                                else
                                                {
                                                    sf_zbs = 0;
                                                    sf_tfgj = 1;
                                                    this._stbSql.Append(String.Format("update zk_lqk set td_zt=0 ,lqxx='',lqzy='',pcdm='',zysx='',xx_zt=0,xq_zt=0,zydm='',xqdm='',types=0,jyzy='',td_pc='' where ksh='{0}';", tabbj[tabbj.Length - 1]["ksh"].ToString()));
                                                    tab_tfgj.Clear();
                                                    DataRow dr = tab_tfgj.NewRow();
                                                    dr["ksh"] = row["ksh"].ToString();
                                                    dr["xqdm"] = row["xqdm"].ToString();
                                                    dr["zsxxdm"] = row["xxdm"].ToString();
                                                    dr["yw"] = row["yw"].ToString();
                                                    dr["sx"] = row["sx"].ToString();
                                                    dr["yy"] = row["yy"].ToString();
                                                    dr["zf"] = row["zf"].ToString();
                                                    dr["whzf"] = row["whzf"].ToString();
                                                    tab_tfgj.Rows.Add(dr);
                                                }
                                            }
                                            else
                                            {
                                                sf_zbs = 0;
                                                sf_tfgj = 1;
                                                this._stbSql.Append(String.Format("update zk_lqk set td_zt=0 ,lqxx='',lqzy='',pcdm='',zysx='',xx_zt=0,xq_zt=0,zydm='',xqdm='',types=0,jyzy='',td_pc='' where ksh='{0}';", tabbj[tabbj.Length - 1]["ksh"].ToString()));
                                                tab_tfgj.Clear();
                                                DataRow dr = tab_tfgj.NewRow();
                                                dr["ksh"] = row["ksh"].ToString();
                                                dr["xqdm"] = row["xqdm"].ToString();
                                                dr["zsxxdm"] = row["xxdm"].ToString();
                                                dr["yw"] = row["yw"].ToString();
                                                dr["sx"] = row["sx"].ToString();
                                                dr["yy"] = row["yy"].ToString();
                                                dr["zf"] = row["zf"].ToString();
                                                dr["whzf"] = row["whzf"].ToString();
                                                tab_tfgj.Rows.Add(dr);
                                            }
                                        }
                                        else
                                        {
                                            sf_zbs = 0;
                                            sf_tfgj = 1;
                                            // tz_row["yt_sl"] = (int.Parse(tz_row["yt_sl"].ToString()) + 1).ToString();
                                            this._stbSql.Append(String.Format("update zk_lqk set td_zt=0 ,lqxx='',lqzy='',pcdm='',zysx='',xx_zt=0,xq_zt=0,zydm='',xqdm='',types=0,jyzy='',td_pc='' where ksh='{0}';", tabbj[tabbj.Length - 1]["ksh"].ToString()));
                                            tab_tfgj.Clear();
                                            DataRow dr = tab_tfgj.NewRow();
                                            dr["ksh"] = row["ksh"].ToString();
                                            dr["xqdm"] = row["xqdm"].ToString();
                                            dr["zsxxdm"] = row["xxdm"].ToString();
                                            dr["yw"] = row["yw"].ToString();
                                            dr["sx"] = row["sx"].ToString();
                                            dr["yy"] = row["yy"].ToString();
                                            dr["zf"] = row["zf"].ToString();
                                            dr["whzf"] = row["whzf"].ToString();
                                            tab_tfgj.Rows.Add(dr);
                                            //回收成绩低的

                                        }
                                        #endregion
                                    }
                                }
                                break;
                            default:
                                return false;

                        }
                    }
                    //tz_row["zdfs"] = row["cj"].ToString();
                }
            }
            else
            {
                if (this._xztj.Tfgj.bFlag)
                {
                    if (this._xztj.Tfgj.Sftfgj == 2)
                    {
                        if (ytdsl == bianliang - 1)//预投数最后一个.
                        {

                            DataRow dr = tab_tfgj.NewRow();
                            dr["ksh"] = row["ksh"].ToString();
                            dr["xqdm"] = row["xqdm"].ToString();
                            dr["zsxxdm"] = row["xxdm"].ToString();
                            dr["yw"] = row["yw"].ToString();
                            dr["sx"] = row["sx"].ToString();
                            dr["yy"] = row["yy"].ToString();
                            dr["zf"] = row["zf"].ToString();
                            dr["whzf"] = row["whzf"].ToString();
                            tab_tfgj.Rows.Add(dr);
                        }
                    }
                }
                sf_zbs = 0;
                sf_tfgj = 0;
                tz_row["zdfs"] = decimal.Parse(row["cj"].ToString());
                tz_row["yt_sl"] = (int.Parse(tz_row["yt_sl"].ToString()) + 1).ToString();
            }

            this._stbSql.Append(String.Format("update zk_lqk set xqdm='{0}',lqxx='{1}',pcdm='{2}',zydm='{3}',td_zt=1,cj={5},sf_zbs={6},sf_tfgj={7},zysx={8},daoru=0,sftzs={9},lqzy='',sfpc='{10}'  where ksh='{4}' and isnull(td_zt,0)=0; ", tz_row["xqdm"].ToString(), row["xxdm"].ToString(), pcdm, zuanyedm, row["ksh"].ToString(), row["wh_cj"].ToString(), sf_zbs, sf_tfgj, row["zysx"].ToString(), sftzs, sfpc));

            if (this._stbSql.Length > 10000)
            {
                this._dbA.execSql_Tran(this._stbSql.ToString());
                this._stbSql.Clear();
            }
            return true;
        }
        /// <summary>
        /// 非指标生投档：将考生向学校投档(true、已投档；false、未投档) 志愿优先
        /// </summary>
        /// <param name="row">考生信息</param>
        /// <param name="zuanyedm">考生报考的专业代码</param>
        /// 志愿顺序  本次投档的NUM
        private bool f_zbs_t_d_XueXiao_ZYYX(DataRow row, string zuanyedm, string zysx, int tdnum, double bil, int type, string pcdm, string tdxqdm)
        {
            if (row["ksh"] == null || row["ksh"].ToString().Length < 1 || row["cj"] == null || row["cj"].ToString().Length < 1)
            {
                return false;
            }
            /*
             * 判断当前考生是否符合其它限制条件，此条件在查询考生信息时，已进行过滤。
             */
            //if (!Filter_Qttj(row["ksh"].ToString()))
            //{
            //    return false;
            //}
            //查询当前学校的当前专业在当前县区已投档数量。
            //string sql = "select COUNT(1) from zk_toudang_kaosen_info where pcdm='"+row["pcdm"].ToString()+"' and lqxx='"+row["xxdm"].ToString()+"' and xianQudm='"+row["xqdm"].ToString()+"' and zuanyeDm='"+zuanyedm+"'";

            //select xqdm,xxdm,fzbssl=jhs-zbssl,zbssl,zydm,yt_sl=0 from Xq_Zsxx_jh_view where pcdm=@pcdm order by pcdm,xxdm,xqdm asc
            //查找已投档数量。
            //DataRow[] rows = tab_jhs.Select("xxdm='" + row["xxdm"].ToString() + "' and xqdm='" + row["xqdm"].ToString() + "' and zydm='" + zuanyedm + "'");
            //DataRow[] rows = this.tab_f_zbs_zsjh.Select("zsxxdm='" + row["xxdm"].ToString() + "' and zydm='" + zuanyedm + "'");

            /*
             * 查询当前招生学校非指标生招生计划。
             * 从招生计划中查询招生学校的信息，只有一个条件就是当前考生是否报考了对应的招生学校。
             */
            //if (row["ksh"].ToString() == "140682100655")
            //{

            //}
            int zy2 = 0; //提前批zy2
            int zy2xh = 0;
            DataRow[] tz_rows;
        start:
            if (pcdm == "01")
            {
                if (zy2 == 0)
                {
                    tz_rows = this.tab_f_zbs_zsjh.Select(String.Format("zsxxdm='{0}' and xqdm='{1}' and zydm='{2}'", row["xxdm"].ToString(), row["bmddm"].ToString(), row["zy1"].ToString()));
                }
                else
                    tz_rows = this.tab_f_zbs_zsjh.Select(String.Format("zsxxdm='{0}' and xqdm='{1}' and zydm='{2}'", row["xxdm"].ToString(), row["bmddm"].ToString(), row["zy2"].ToString()));
                if (tz_rows == null || tz_rows.Length < 1)
                {
                    tz_rows = this.tab_f_zbs_zsjh.Select(String.Format("zsxxdm='{0}'", row["xxdm"].ToString()));
                    if (tz_rows == null || tz_rows.Length < 1)
                    {
                        return false;
                    }
                }
            }
            else
            {
                tz_rows = this.tab_f_zbs_zsjh.Select(String.Format("zsxxdm='{0}' and xqdm='{1}'", row["xxdm"].ToString(), row["xqdm"].ToString()));
                if (tz_rows == null || tz_rows.Length < 1)
                {
                    tz_rows = this.tab_f_zbs_zsjh.Select(String.Format("zsxxdm='{0}'", row["xxdm"].ToString()));
                    if (tz_rows == null || tz_rows.Length < 1)
                    {
                        return false;
                    }
                }
            }
            //int index = 0;
            ////如果查询的招生学校记录有多条，则表示当前招生学校可能面对各个县区有相应的招生计划。
            //if (tz_rows.Length > 1)
            //{
            //    //筛选当前考生所在县区的招生计划。
            //    for (int i = 0; i < tz_rows.Length; i++)
            //    {
            //        if (tz_rows[i]["xqdm"].ToString().Trim() == row["xqdm"].ToString().Trim())
            //        {
            //            index = i;
            //            break;
            //        }
            //    }
            //}

            int jhsl = 0;
            int ytdsl = 0;
            decimal fzbs_zdfs = 0;
            decimal zbs_zdfs = 0;

            //标识当前考生录取类型：0、统招生；1、是指标生。
            int sf_zbs = 0;
            //0、不是同分跟进；1、同分跟进。
            int sf_tfgj = 0;

            if (tz_rows.Length < 1)
            {
                return false;
            }

            DataRow tz_row = tz_rows[0];
            //计划数量(当前学校非指标生总计划数)
            jhsl = int.Parse(tz_row["f_zbssl"].ToString());
            //取出已投档数量
            ytdsl = int.Parse(tz_row["yt_sl"].ToString());
            //最低分数(初始0)。
            fzbs_zdfs = decimal.Parse(tz_row["zdfs"].ToString());

            //(统招)判断计划数与当前已投档数。
            int bianliang = 0;
            if (type == 1)
            {
                bianliang = Convert.ToInt32(jhsl * bil);
            }
            else
            {
                bianliang = tdnum;
            }

            if (ytdsl >= bianliang)
            {
                if (Convert.ToInt32(zysx) > 1 && tab_tfgj.Rows.Count == 0 && pcdm != "01")
                {
                    return false;
                }
                else
                {
                    if (tab_tfgj.Rows.Count > 0)
                    {
                        if (tab_tfgj.Rows[0]["zsxxdm"].ToString() != row["xxdm"].ToString())
                        {
                            return false;
                        }
                    }
                }

                //查询当前已执行的最低分数。
                if (decimal.Parse(row["cj"].ToString()) < fzbs_zdfs)
                {
                    if (pcdm == "01" && row["zy2"].ToString() != "" && zy2xh == 0)
                    {
                        zy2 = 1;
                        zy2xh = 1;
                        goto start;
                    }
                    return false;
                    ////判断指标生投档条件是否定制有效。
                    //if (!this._xztj.Zbs.bFlag)
                    //{
                    //    return false;
                    //}
                    ////当前批次是否有指标生。
                    //if (this._xztj.Zbs.Ywzbs == 1)
                    //{
                    //    return false;
                    //}
                    ////判断考生是否是指标生：0、非指标生；1、是指标生。
                    //if (row["sfzbs"].ToString() == "0")
                    //{
                    //    return false;
                    //}
                    ////判断指标生在自定义分数的条件下，不小于统招分数线下多少分。
                    //if (this._xztj.Zbs.Zbslqfsxz == 1)
                    //{
                    //    if (decimal.Parse(row["cj"].ToString()) + this._xztj.Zbs.Zdyfs > fzbs_zdfs)
                    //    {
                    //        return false;
                    //    }
                    //}
                    //判断是否符合指标生条件()。
                    //#region 指标生录取条件
                    //DataRow[] zbs_rows = this.tab_zbs_zsjh.Select(String.Format("zsxxdm='{0}' and byxxdm='{1}'", row["xxdm"].ToString(), row["ksh"].ToString().Substring(2, 6)));

                    //if (zbs_rows.Length < 1)
                    //{
                    //    return false;
                    //}

                    //DataRow zbs_row = zbs_rows[0];
                    ////计划数量(当前学校非指标生总计划数)
                    //jhsl = int.Parse(zbs_row["zbssl"].ToString());
                    ////当前招生学校在考生毕业学校的指标生已投档数量
                    //ytdsl = int.Parse(zbs_row["yt_sl"].ToString());
                    ////最低分数(初始0)。
                    //zbs_zdfs = decimal.Parse(zbs_row["zdfs"].ToString());
                    //if (ytdsl >= jhsl)
                    //{
                    //    //判断指标生同分跟进
                    //    if (decimal.Parse(row["cj"].ToString()) < zbs_zdfs)
                    //    {
                    //        return false;
                    //    }
                    //    else
                    //    {
                    //        sf_zbs = 1;
                    //        sf_tfgj = 1;
                    //        zbs_row["yt_sl"] = (int.Parse(zbs_row["yt_sl"].ToString()) + 1).ToString();
                    //        //zbs_row["zdfs"] = row["cj"].ToString();
                    //    }
                    //}
                    //else
                    //{
                    //    sf_zbs = 1;
                    //    sf_tfgj = 0;
                    //    zbs_row["yt_sl"] = (int.Parse(zbs_row["yt_sl"].ToString()) + 1).ToString();
                    //    zbs_row["zdfs"] = row["cj"].ToString();
                    //}
                    //#endregion
                }
                else
                {
                    if (!this._xztj.Tfgj.bFlag)
                    {
                        return false;
                    }
                    else
                    {
                        switch (this._xztj.Tfgj.Sftfgj)
                        {
                            case 0:
                                return false;

                            case 1:
                                sf_zbs = 0;
                                sf_tfgj = 1;
                                tz_row["yt_sl"] = (int.Parse(tz_row["yt_sl"].ToString()) + 1).ToString();

                                break;
                            case 2:

                                if (tab_tfgj.Rows.Count > 0)
                                {
                                    DataRow[] tabbj = tab_tfgj.Select(String.Format("zsxxdm='{0}' and xqdm='{1}'", row["xxdm"].ToString(), row["xqdm"].ToString()));
                                    if (tabbj == null || tabbj.Length < 1)
                                    {
                                        tabbj = this.tab_tfgj.Select(String.Format("zsxxdm='{0}'", row["xxdm"].ToString()));
                                    }


                                    if (tabbj.Length > 0)//计较成绩
                                    {
                                        float oldhe = 0;
                                        float newhe = 0;
                                        #region    海门
                                        if (tdxqdm == "0684") //海门 
                                        {
                                            oldhe = Convert.ToSingle(tabbj[tabbj.Length - 1]["zf"]);
                                            newhe = Convert.ToSingle(row["zf"]);
                                            if (oldhe > newhe)
                                            {
                                                return false;
                                            }
                                            else if (oldhe == newhe)//相同.继续比较
                                            {
                                                oldhe = Convert.ToSingle(tabbj[tabbj.Length - 1]["whzf"]);
                                                newhe = Convert.ToSingle(row["whzf"]);
                                                if (oldhe > newhe)
                                                {
                                                    return false;
                                                }
                                                else if (oldhe == newhe)//相同.继续比较
                                                {

                                                }
                                                else
                                                {
                                                    sf_zbs = 0;
                                                    sf_tfgj = 1;
                                                    // tz_row["yt_sl"] = (int.Parse(tz_row["yt_sl"].ToString()) + 1).ToString();
                                                    this._stbSql.Append(String.Format("update zk_lqk set td_zt=0 ,lqxx='',lqzy='',pcdm='',zysx='',xx_zt=0,xq_zt=0,zydm='',xqdm='',types=0,jyzy='',td_pc='' where ksh='{0}';", tabbj[tabbj.Length - 1]["ksh"].ToString()));
                                                    tab_tfgj.Clear();
                                                    DataRow dr = tab_tfgj.NewRow();
                                                    dr["ksh"] = row["ksh"].ToString();
                                                    dr["xqdm"] = row["xqdm"].ToString();
                                                    dr["zsxxdm"] = row["xxdm"].ToString();
                                                    dr["yw"] = row["yw"].ToString();
                                                    dr["sx"] = row["sx"].ToString();
                                                    dr["yy"] = row["yy"].ToString();
                                                    dr["zf"] = row["zf"].ToString();
                                                    dr["whzf"] = row["whzf"].ToString();
                                                    tab_tfgj.Rows.Add(dr);
                                                    break;
                                                }

                                            }
                                            else
                                            {
                                                sf_zbs = 0;
                                                sf_tfgj = 1;
                                                // tz_row["yt_sl"] = (int.Parse(tz_row["yt_sl"].ToString()) + 1).ToString();
                                                this._stbSql.Append(String.Format("update zk_lqk set td_zt=0 ,lqxx='',lqzy='',pcdm='',zysx='',xx_zt=0,xq_zt=0,zydm='',xqdm='',types=0,jyzy='',td_pc='' where ksh='{0}';", tabbj[tabbj.Length - 1]["ksh"].ToString()));
                                                tab_tfgj.Clear();
                                                DataRow dr = tab_tfgj.NewRow();
                                                dr["ksh"] = row["ksh"].ToString();
                                                dr["xqdm"] = row["xqdm"].ToString();
                                                dr["zsxxdm"] = row["xxdm"].ToString();
                                                dr["yw"] = row["yw"].ToString();
                                                dr["sx"] = row["sx"].ToString();
                                                dr["yy"] = row["yy"].ToString();
                                                dr["zf"] = row["zf"].ToString();
                                                dr["whzf"] = row["whzf"].ToString();
                                                tab_tfgj.Rows.Add(dr);
                                                break;
                                            }
                                        }
                                        #endregion

                                        oldhe = Convert.ToSingle(tabbj[tabbj.Length - 1]["yw"]) + Convert.ToSingle(tabbj[tabbj.Length - 1]["sx"]) + Convert.ToSingle(tabbj[tabbj.Length - 1]["yy"]);
                                        newhe = Convert.ToSingle(row["yw"]) + Convert.ToSingle(row["sx"]) + Convert.ToSingle(row["yy"]);
                                        #region    语数英

                                        if (oldhe > newhe)
                                        {
                                            return false;
                                        }
                                        else if (oldhe == newhe)//相同.继续比较
                                        {
                                            if (tdxqdm == "0623" || tdxqdm == "0682") //如东
                                            {
                                                sf_zbs = 0;
                                                sf_tfgj = 1;
                                                tz_row["yt_sl"] = (int.Parse(tz_row["yt_sl"].ToString()) + 1).ToString();
                                                break;
                                            }

                                            oldhe = Convert.ToSingle(tabbj[tabbj.Length - 1]["yw"]) + Convert.ToSingle(tabbj[tabbj.Length - 1]["sx"]);
                                            newhe = Convert.ToSingle(row["yw"]) + Convert.ToSingle(row["sx"]);
                                            if (oldhe > newhe)
                                            {
                                                return false;
                                            }
                                            else if (oldhe == newhe)//相同.继续比较
                                            {
                                                if (tdxqdm == "0621" || tdxqdm == "0601" || tdxqdm == "0684") //海安
                                                {
                                                    oldhe = Convert.ToSingle(tabbj[tabbj.Length - 1]["sx"]);
                                                    newhe = Convert.ToSingle(row["sx"]);
                                                }
                                                else if (tdxqdm == "0681") //启东
                                                {
                                                    oldhe = Convert.ToSingle(tabbj[tabbj.Length - 1]["yw"]);
                                                    newhe = Convert.ToSingle(row["yw"]);
                                                }
                                                else if (tdxqdm == "0683") //通州
                                                {
                                                    sf_zbs = 0;
                                                    sf_tfgj = 1;
                                                    tz_row["yt_sl"] = (int.Parse(tz_row["yt_sl"].ToString()) + 1).ToString();
                                                    break;
                                                }
                                                if (oldhe > newhe)
                                                {
                                                    return false;
                                                }
                                                else if (oldhe == newhe)//相同.继续比较
                                                {
                                                    sf_zbs = 0;
                                                    sf_tfgj = 1;
                                                    tz_row["yt_sl"] = (int.Parse(tz_row["yt_sl"].ToString()) + 1).ToString();
                                                }
                                                else
                                                {
                                                    sf_zbs = 0;
                                                    sf_tfgj = 1;
                                                    this._stbSql.Append(String.Format("update zk_lqk set td_zt=0 ,lqxx='',lqzy='',pcdm='',zysx='',xx_zt=0,xq_zt=0,zydm='',xqdm='',types=0,jyzy='',td_pc='' where ksh='{0}';", tabbj[tabbj.Length - 1]["ksh"].ToString()));
                                                    tab_tfgj.Clear();
                                                    DataRow dr = tab_tfgj.NewRow();
                                                    dr["ksh"] = row["ksh"].ToString();
                                                    dr["xqdm"] = row["xqdm"].ToString();
                                                    dr["zsxxdm"] = row["xxdm"].ToString();
                                                    dr["yw"] = row["yw"].ToString();
                                                    dr["sx"] = row["sx"].ToString();
                                                    dr["yy"] = row["yy"].ToString();
                                                    dr["zf"] = row["zf"].ToString();
                                                    dr["whzf"] = row["whzf"].ToString();
                                                    tab_tfgj.Rows.Add(dr);
                                                }
                                            }
                                            else
                                            {
                                                sf_zbs = 0;
                                                sf_tfgj = 1;
                                                this._stbSql.Append(String.Format("update zk_lqk set td_zt=0 ,lqxx='',lqzy='',pcdm='',zysx='',xx_zt=0,xq_zt=0,zydm='',xqdm='',types=0,jyzy='',td_pc='' where ksh='{0}';", tabbj[tabbj.Length - 1]["ksh"].ToString()));
                                                tab_tfgj.Clear();
                                                DataRow dr = tab_tfgj.NewRow();
                                                dr["ksh"] = row["ksh"].ToString();
                                                dr["xqdm"] = row["xqdm"].ToString();
                                                dr["zsxxdm"] = row["xxdm"].ToString();
                                                dr["yw"] = row["yw"].ToString();
                                                dr["sx"] = row["sx"].ToString();
                                                dr["yy"] = row["yy"].ToString();
                                                dr["zf"] = row["zf"].ToString();
                                                dr["whzf"] = row["whzf"].ToString();
                                                tab_tfgj.Rows.Add(dr);
                                            }
                                        }
                                        else
                                        {
                                            sf_zbs = 0;
                                            sf_tfgj = 1;
                                            // tz_row["yt_sl"] = (int.Parse(tz_row["yt_sl"].ToString()) + 1).ToString();
                                            this._stbSql.Append(String.Format("update zk_lqk set td_zt=0 ,lqxx='',lqzy='',pcdm='',zysx='',xx_zt=0,xq_zt=0,zydm='',xqdm='',types=0,jyzy='',td_pc='' where ksh='{0}';", tabbj[tabbj.Length - 1]["ksh"].ToString()));
                                            tab_tfgj.Clear();
                                            DataRow dr = tab_tfgj.NewRow();
                                            dr["ksh"] = row["ksh"].ToString();
                                            dr["xqdm"] = row["xqdm"].ToString();
                                            dr["zsxxdm"] = row["xxdm"].ToString();
                                            dr["yw"] = row["yw"].ToString();
                                            dr["sx"] = row["sx"].ToString();
                                            dr["yy"] = row["yy"].ToString();
                                            dr["zf"] = row["zf"].ToString();
                                            dr["whzf"] = row["whzf"].ToString();
                                            tab_tfgj.Rows.Add(dr);
                                            //回收成绩低的

                                        }
                                        #endregion
                                    }
                                }
                                break;
                            default:
                                return false;

                        }
                    }
                    //tz_row["zdfs"] = row["cj"].ToString();
                }
            }
            else
            {
                if (this._xztj.Tfgj.bFlag)
                {
                    if (this._xztj.Tfgj.Sftfgj == 2)
                    {
                        if (ytdsl == bianliang - 1)//预投数最后一个.
                        {
                            DataRow dr = tab_tfgj.NewRow();
                            dr["ksh"] = row["ksh"].ToString();
                            dr["xqdm"] = row["xqdm"].ToString();
                            dr["zsxxdm"] = row["xxdm"].ToString();
                            dr["yw"] = row["yw"].ToString();
                            dr["sx"] = row["sx"].ToString();
                            dr["yy"] = row["yy"].ToString();
                            dr["zf"] = row["zf"].ToString();
                            dr["whzf"] = row["whzf"].ToString();
                            tab_tfgj.Rows.Add(dr);
                        }
                    }
                }
                sf_zbs = 0;
                sf_tfgj = 0;
                tz_row["zdfs"] = row["cj"].ToString();
                tz_row["yt_sl"] = (int.Parse(tz_row["yt_sl"].ToString()) + 1).ToString();
            }
            //增加tdpc,zysx

            this._stbSql.Append(String.Format("update zk_lqk set xqdm='{0}',lqxx='{1}',pcdm='{2}',zydm='{3}',td_zt=1,cj={5},sf_zbs={6},sf_tfgj={7},zysx={8},daoru=0  where ksh='{4}' and isnull(td_zt,0)=0;;", tz_row["xqdm"].ToString(), row["xxdm"].ToString(), row["pcdm"].ToString(), zuanyedm, row["ksh"].ToString(), row["wh_cj"].ToString(), sf_zbs, sf_tfgj, zysx));

            if (this._stbSql.Length > 10000)
            {
                this._dbA.execSql_Tran(this._stbSql.ToString());
                this._stbSql.Clear();
            }
            return true;
        }
        /// <summary>
        /// 指标生投档：将考生向学校投档(true、已投档；false、未投档)
        /// </summary>
        /// <param name="row">考生信息</param>
        /// <param name="tab_jhs">指标生执行计划</param>
        /// <param name="zuanyedm">考生招考折专业代码</param>
        private bool zbs_t_d_xuexiao(DataRow row, DataTable tab_jhs, string zuanyedm)
        {
            if (!Filter_Qttj(row["ksh"].ToString()))
            {
                return false;
            }

            //xxdm,zsxxdm,zbssl,yt_sl=0,zdfs=0.0
            //查找已投档数量。
            DataRow[] rows = tab_jhs.Select(String.Format("xxdm='{0}' and xqdm='{1}' and zydm='{2}'", row["xxdm"].ToString(), row["xqdm"].ToString(), zuanyedm));
            DataRow rowTemp = rows[0];
            int jhsl = 0;
            int ytdsl = 0;
            decimal zdfs = 0;
            if (rows.Length > 0)
            {
                //计划数量
                jhsl = int.Parse(rows[0]["zbssl"].ToString());
                //取出已投档数量
                ytdsl = int.Parse(rows[0]["yt_sl"].ToString());
                //最低分数(初始0)。
                zdfs = decimal.Parse(rows[0]["zdfs"].ToString());
                if (jhsl <= ytdsl)
                {
                    if (decimal.Parse(row["cj"].ToString()) != zdfs)
                    {
                        return false;
                    }
                }
                rowTemp["zdfs"] = row["cj"].ToString();
                rowTemp["yt_sl"] = (int.Parse(rows[0]["yt_sl"].ToString()) + 1).ToString();
                this._stbSql.Append(String.Format("insert into zk_toudang_kaosen_info(xianQudm,lqxx,pcdm,zuanyeDm,kaoSen_BiaoShiHuao,currentState,cj) values('{0}','{1}','{2}','{3}','{4}',0,{5})", row["xqdm"].ToString(), row["xxdm"].ToString(), row["pcdm"].ToString(), zuanyedm, row["ksh"].ToString(), row["cj"].ToString()));
                this._stbSql.Append(";");
                if (this._stbSql.Length > 10000)
                {
                    this._dbA.execSql_Tran(this._stbSql.ToString());
                    this._stbSql.Clear();
                }
            }

            return false;
        }


        /// <summary>
        /// 志愿优先算法。
        /// </summary>
        private void ZhiYuanYouXuanSuanFa(string pcdm)
        {
            string sql = "select a.*,b.cj from zk_kszyxx a left join zk_kszkcj b on a.ksh=b.ksh where pcdm=@pcdm and sfbk=1 and kmdm='88' order by zysx,cj DESC";
        }

        #region 条件过滤。
        /// <summary>
        /// 根据批次、报名号验证是否符合基本条件的限制条件(true、符合条件；false、不符合条件)。
        /// </summary>
        /// <param name="pcdm">批次代码。</param>
        /// <param name="ksh">报名号</param>
        private bool Filter_Jbtj(string pcdm, string ksh)
        {

            return false;
        }
        /// <summary>
        /// 根据批次、报名号验证是否符合同分跟进的限制条件(true、符合条件；false、不符合条件)。
        /// </summary>
        /// <param name="pcdm">批次代码。</param>
        /// <param name="ksh">报名号</param>
        private bool Filter_Tfgj(string pcdm, string ksh)
        {
            return false;
        }
        /// <summary>
        /// 根据批次是否有指标生(true、符合条件；false、不符合条件)。
        /// </summary>
        /// <param name="pcdm">批次代码。</param>
        /// <param name="ksh">报名号</param>
        private bool Filter_Zbs(string pcdm, string ksh)
        {
            return false;
        }
        /// <summary>
        /// 根据批次、报名号验证是否符合素质评价的限制条件(true、符合条件；false、不符合条件)。
        /// </summary>
        /// <param name="pcdm">批次代码。</param>
        /// <param name="ksh">报名号</param>
        private bool Filter_Szpj(string pcdm, string ksh)
        {
            return false;
        }
        /// <summary>
        /// 根据批次、报名号验证是否符合其他条件的限制条件(true、符合条件；false、不符合条件)。
        /// </summary>
        /// <param name="pcdm">批次代码。</param>
        /// <param name="ksh">报名号</param>
        private bool Filter_Qttj(string ksh)
        {
            if (this._xztj.Qttj.bFlag)
            {
                if (this._xztj.Qttj.Xbxz == 2)
                {
                    return true;
                }
                //性别判断。
                string sql = "select xbdm from zk_Ks_qt_Info where ksh='" + ksh + "'";
                object obj = this._dbA.ExecuteScalar_Tran(sql);
                if (obj == null)
                {
                    return true;
                }
                if (obj.ToString() == (this._xztj.Qttj.Xbxz + 1).ToString())
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 根据批次、报名号验证是否符合国际班控档线的限制条件(true、符合条件；false、不符合条件)。
        /// </summary>
        /// <param name="pcdm">批次代码。</param>
        /// <param name="ksh">报名号</param>
        private bool Filter_Qjbkdx(string pcdm, string ksh)
        {
            return false;
        }
        #endregion

        /// <summary>
        /// 导出当前
        /// </summary>
        /// <param name="pcdm"></param>
        /// <returns></returns>
        public bool Import_pc_fhtj_ksxx(string pcdm)
        {
            return false;
        }
        #region 取消投档
        /// <summary>
        /// 取消批次投档。
        /// </summary>
        /// <param name="pcdm">批次代码。</param>
        public bool Cancel_PC_TD(string pcdm)
        {
            SqlDbHelper_1 dbA = new SqlDbHelper_1();
            Sys_SessionEntity user = SincciLogin.Sessionstu();
            try
            {
                string sql = "update zk_lqk set td_zt=0,xqdm='',lqxx='',pcdm='',zydm='',zysx='',sftzs=0,sf_tfgj=0,sfpc='' where  pcdm=@pcdm and td_zt=1 ";
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@pcdm", pcdm) };

                dbA.BeginTran();

                int iCount = dbA.execSql_Tran(sql, lisP);
                StringBuilder stb = new StringBuilder();
                //sql = "delete from zk_lqk where pcdm=@pcdm";
                //int count = dbA.execSql_Tran(sql, lisP);
                if (iCount > 0)
                {
                    dbA.EndTran(true);
                    stb.Append(String.Format("{0}取消了第{1}批次的投档状态，总共取消了{2}名考生的投档状态！", user.UserName, pcdm, iCount));
                    EventMessage.EventWriteDB(1, stb.ToString(), user.UserName);
                    return true;
                }

                dbA.EndTran(false);
                return false;
            }
            catch (Exception exe)
            {
            }
            return false;
        }


        /// <summary>
        /// 取消批次投档。
        /// </summary>
        /// <param name="pcdm">批次代码。</param>
        public bool Cancel_PC_TD_XX(string pcdm,string xxdm)
        {
            SqlDbHelper_1 dbA = new SqlDbHelper_1();
            Sys_SessionEntity user = SincciLogin.Sessionstu();
            try
            {
                string xx = "";
                if (xxdm != "")
                {
                    xx = " and lqxx='" + xxdm + "'";
                }
                string sql = "update zk_lqk set td_zt=0,xqdm='',lqxx='',pcdm='',zydm='',zysx='',sftzs=0,sf_tfgj=0,sfpc='' where  pcdm=@pcdm and td_zt=1 " + xx;
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@pcdm", pcdm) };

                dbA.BeginTran();

                int iCount = dbA.execSql_Tran(sql, lisP);
                StringBuilder stb = new StringBuilder();
                //sql = "delete from zk_lqk where pcdm=@pcdm";
                //int count = dbA.execSql_Tran(sql, lisP);
                if (iCount > 0)
                {
                    dbA.EndTran(true);
                    stb.Append(String.Format("{0}取消了第{1}批次的投档状态，总共取消了{2}名考生的投档状态！", user.UserName, pcdm, iCount));
                    EventMessage.EventWriteDB(1, stb.ToString(), user.UserName);
                    return true;
                }

                dbA.EndTran(false);
                return false;
            }
            catch (Exception exe)
            {
            }
            return false;
        }
        /// <summary>
        /// 取消批次投档。志愿优先
        /// </summary>
        /// <param name="pcdm">批次代码。</param>
        public bool Cancel_PC_TD(string pcdm, string zysx)
        {
            SqlDbHelper_1 dbA = new SqlDbHelper_1();
            Sys_SessionEntity user = SincciLogin.Sessionstu();
            try
            {
                string sql = "update zk_kszkcj set state=0 where ksh in(select ksh from zk_lqk where pcdm=@pcdm and td_zt=1 and zysx=@zysx)";
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@pcdm", pcdm), new SqlParameter("@zysx", zysx) };

                dbA.BeginTran();

                int iCount = dbA.execSql_Tran(sql, lisP);
                StringBuilder stb = new StringBuilder();
                sql = "delete from zk_lqk where pcdm=@pcdm  and td_zt=1 and zysx=@zysx";
                int count = dbA.execSql_Tran(sql, lisP);
                if (iCount == count)
                {
                    dbA.EndTran(true);
                    stb.Append(String.Format("{0}取消了第{1}批次的投档状态，总共取消了{2}名考生的投档状态！", user.UserName, pcdm, iCount));
                    EventMessage.EventWriteDB(1, stb.ToString(), user.UserName);
                    return true;
                }

                dbA.EndTran(false);
                return false;
            }
            catch (Exception exe)
            {
            }
            return false;
        }
        #endregion

        /// <summary>
        /// 发档。志愿优先
        /// </summary>
        /// <param name="pcdm">批次代码。</param>
        public bool FA_PC_TD(string pcdm, string zysx)
        {
            SqlDbHelper_1 dbA = new SqlDbHelper_1();
            Sys_SessionEntity user = SincciLogin.Sessionstu();
            try
            {


                string sqltab = "select ISNULL( td_pc,0) td_pc  from zk_lqk  group by td_pc   order by td_pc desc";
                dbA.BeginTran();
                DataTable tab = dbA.execReTab_Tran(sqltab);
                int tdpc = Convert.ToInt32(tab.Rows[0]["td_pc"].ToString());
                tdpc++;
                //xx_zt 相当于学校端看到的考生状态,需要操作完该发档批次后.才修改td_zt
                string sql = "update zk_lqk set td_zt=2,xx_zt=2, td_pc=" + tdpc + "  where pcdm=@pcdm and td_zt=1  and zysx=@zysx";
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@pcdm", pcdm), new SqlParameter("@zysx", zysx) };



                int iCount = dbA.execSql_Tran(sql, lisP);
                StringBuilder stb = new StringBuilder();

                if (iCount > 0)
                {
                    dbA.EndTran(true);
                    stb.Append(String.Format("{0}发档了第{1}批次，总共发档了{2}名考生！", user.UserName, pcdm, iCount));
                    EventMessage.EventWriteDB(1, stb.ToString(), user.UserName);
                    return true;
                }

                dbA.EndTran(false);
                return false;
            }
            catch (Exception exe)
            {
                dbA.EndTran(false);
                return false;
            }
            return false;
        }


        /// <summary>
        /// 发档。志愿优先
        /// </summary>
        /// <param name="pcdm">批次代码。</param>
        public bool FA_PC_TD(string pcdm)
        {
            SqlDbHelper_1 dbA = new SqlDbHelper_1();
            Sys_SessionEntity user = SincciLogin.Sessionstu();
            try
            {


                string sqltab = "select CONVERT(int, ISNULL(td_pc,0)) td_pc  from zk_lqk  group by td_pc   order by td_pc desc";
                dbA.BeginTran();
                DataTable tab = dbA.execReTab_Tran(sqltab);
                int tdpc = Convert.ToInt32(tab.Rows[0]["td_pc"].ToString());
                tdpc++;
                //xx_zt 相当于学校端看到的考生状态,需要操作完该发档批次后.才修改td_zt
                string sql = "update zk_lqk set td_zt=2,xx_zt=2, td_pc=" + tdpc + "  where pcdm=@pcdm and td_zt=1  ";

                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@pcdm", pcdm) };
                //考生轨迹
                string sqlgj = " insert into  zk_kslqgj (ksh,username,type,times)   select ksh,'" + SincciLogin.Sessionstu().UserName + "',2,getdate() from zk_lqk  where pcdm=@pcdm and td_zt=1 ";
                dbA.execSql_Tran(sqlgj, lisP);
                int iCount = dbA.execSql_Tran(sql, lisP);

                StringBuilder stb = new StringBuilder();

                if (iCount > 0)
                {
                    dbA.EndTran(true);

                    stb.Append(String.Format("{0}发档了第{1}批次，总共发档了{2}名考生！", user.UserName, pcdm, iCount));
                    EventMessage.EventWriteDB(1, stb.ToString(), user.UserName);
                    return true;
                }

                dbA.EndTran(false);
                return false;
            }
            catch (Exception exe)
            {
                dbA.EndTran(false);
                return false;
            }
            return false;
        }
        /// <summary>
        /// 发档。单校
        /// </summary>
        /// <param name="pcdm">批次代码。建议专业0.分数优先 1 专业</param>
        public bool FA_PC_TD_XX(string pcdm, string lqxx, string jy)
        {
            SqlDbHelper_1 dbA = new SqlDbHelper_1();
            Sys_SessionEntity user = SincciLogin.Sessionstu();
            try
            {
                string sqltab = "select CONVERT(int, ISNULL(td_pc,0)) td_pc  from zk_lqk  group by td_pc   order by td_pc desc";
                dbA.BeginTran();
                DataTable tab = dbA.execReTab_Tran(sqltab);
                int tdpc = Convert.ToInt32(tab.Rows[0]["td_pc"].ToString());
                tdpc++;
                //xx_zt 相当于学校端看到的考生状态,需要操作完该发档批次后.才修改td_zt
                string sql = "update zk_lqk set td_zt=2,xx_zt=2, td_pc=" + tdpc + ",jyzy=" + jy + "  where pcdm=@pcdm and lqxx=@lqxx and td_zt=1  ";
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@pcdm", pcdm), new SqlParameter("@lqxx", lqxx) };

                //考生轨迹
                string sqlgj = " insert into  zk_kslqgj (ksh,username,type,times)   select ksh,'" + SincciLogin.Sessionstu().UserName + "',2,getdate() from zk_lqk    where pcdm=@pcdm and lqxx=@lqxx and td_zt=1  ";
                dbA.execSql_Tran(sqlgj, lisP);
                int iCount = dbA.execSql_Tran(sql, lisP);
                StringBuilder stb = new StringBuilder();

                if (iCount > 0)
                {
                    dbA.EndTran(true);

                    stb.Append(String.Format("{0}发档了第{1}批次，总共发档了{2}名考生！", user.UserName, pcdm, iCount));
                    EventMessage.EventWriteDB(1, stb.ToString(), user.UserName);
                    return true;
                }

                dbA.EndTran(false);
                return false;
            }
            catch (Exception exe)
            {
                dbA.EndTran(false);
                return false;
            }
            return false;
        }
        /// <summary>
        /// 修改县区最低分数线控制。
        /// </summary>
        public bool UPXqZdFsx(DataTable dt)
        {
            SqlDbHelper_1 dbA = new SqlDbHelper_1();

            try
            {


                int iCount = 0;
                if (dt != null)
                {
                    dbA.BeginTran();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string sql = "update  zk_Yc_XqYcZdFsx set yc_ZdFensuKzx='" + dt.Rows[i]["yc_ZdFensuKzx"] + "' where yc_Xqdm='" + dt.Rows[i]["yc_Xqdm"] + "'";
                        iCount = dbA.execSql_Tran(sql);
                        iCount += iCount;
                    }
                }
                if (iCount > 0)
                {
                    dbA.EndTran(true);
                    return true;
                }

                dbA.EndTran(false);
                return false;
            }
            catch (Exception exe)
            {
                dbA.EndTran(false);
                return false;
            }
            return false;
        }

        /// <summary>
        /// 查询该批次未发档学校
        /// </summary>
        /// <param name="xpcId">批次代码</param>
        public DataTable select_fd_noxx(string pcdm)
        {
            try
            {
                //string sql = "select xxdm,xxmc,jhs,tz_sl=jhs-zbssl,yitd_fzbs_sl,zbssl,yitd_zbs_sl,fzbs_xcsl=jhs-zbssl-yitd_fzbs_sl,zbs_xcsl=zbssl-yitd_zbs_sl,zaosenFsx=0 from validate_view where xpcId=@xpcId order by xxdm ASC";
                string sql = " select a.lqxx,a.lqxx+b.zsxxmc  as zsxxmc from (  select lqxx from zk_lqk where td_zt=1 and pcdm=@pcdm group by lqxx ) a left join zk_zsxxdm b on a.lqxx = b.zsxxdm";
                List<SqlParameter> lisP = new List<SqlParameter>();

                SqlParameter Pcdm = new SqlParameter("@pcdm", SqlDbType.VarChar);
                Pcdm.Value = pcdm;
                lisP.Add(Pcdm);

                this._dbA.BeginTran();
                DataTable tab = this._dbA.execReTab_Tran(sql, lisP);
                return tab;
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbA.EndTran(false);
            }
            return null;
        }

        /// <summary>
        /// 更新计划库
        /// </summary>
        /// <param name="pcdm">ksh。 状态</param>
        public bool gxjhk()
        {
            try
            {

                _dbA.BeginTran();
                string sql = " truncate table zk_xxzysyjh ; insert into zk_xxzysyjh  select a.pcdm,a.xxdm,a.zsxxmc,a.zydm,a.zymc,a.jhs, (a.jhs - ISNULL(b.lqnum,0)) as syjh  from View_zsjh a" +
                    " left join ( select  pcdm,lqxx,lqzy,COUNT(lqzy)   as lqnum from zk_lqk where  pcdm in ('21','31') and " +
                    " td_zt=5 group by lqzy,lqxx,pcdm) b on a.xxdm=b.lqxx and a.zydm=b.lqzy and a.pcdm=b.pcdm " +
                    " where  (a.jhs - ISNULL(b.lqnum,0))>0 and a.pcdm in ('21','31') and  xqdm in('500') ";
                string sql2 = "  truncate table zk_xxsyjh;  insert into zk_xxsyjh select a.pcdm,a.xxdm,a.zsxxmc,ISNULL( b.jhs,0)-isnull(c.lqnum,0) as syjh  " +
                    " from (  select distinct zsxxmc , xxdm,pcdm from View_zk_zsjh where " +
                    "  pcdm in ('21','31') and   xqdm in('500') ) a " +
                    "   left join (select jhs,xxdm,pcdm from zs_xx_tj where pcdm in ('21','31')) b  " +
                    "   on a.xxdm=b.xxdm and a.pcdm=b.pcdm  left join ( select pcdm, lqxx,COUNT(lqzy) as lqnum  " +
                    "   from zk_lqk where    td_zt=5 and pcdm in ('21','31') group by lqxx,pcdm) c on " +
                    "   a.xxdm=c.lqxx and a.pcdm=c.pcdm where (ISNULL( b.jhs,0)-isnull(c.lqnum,0))>0 ";
                string sql3 = "  truncate table zk_lqjhk;  insert into zk_lqjhk  select pcdm,xxdm,xqdm,'00','', sum (jhs) jhs from zk_zsjh where pcdm in ('21','31') group by xqdm,xxdm,pcdm ";
                int iCount2 = _dbA.execSql_Tran(sql2);
                int iCount = _dbA.execSql_Tran(sql);
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
        /// 更新配转统计划数
        /// </summary>
        /// <returns></returns>
        public bool Gxpzt(string pcdm)
        {
            try
            {

                _dbA.BeginTran();
                string sql = @"  update a set jhs=a.jhs-isnull(b.lqnum,0)  from zk_zsjh_xq a join (
                       select   lqxx,pcdm,COUNT(1) lqnum   from zk_lqk  
                       group by  lqxx,pcdm 
                      ) b  on  a.xxdm=b.lqxx AND b.pcdm = a.pcdm
where a.pcdm='" + pcdm + "'  and   a.xxdm BETWEEN 017 AND 038  OR a.xxdm IN (041,042,043,044)";
                  
                int iCount = _dbA.execSql_Tran(sql);
             
                StringBuilder stb = new StringBuilder();
                if (iCount > 0  )
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
        /// 更新配转统计划数
        /// </summary>
        /// <returns></returns>
        public bool Gxpzt2(string pcdm)
        {
            try
            {

                _dbA.BeginTran();
                string sql1 = "TRUNCATE TABLE zk_zsjh_gx;  insert into zk_zsjh_gx EXECUTE proc_tj ;";
                string sql = @"  update a set a.jhs=b.qe   FROM    zk_zsjh_xq a join  zk_zsjh_gx b 
on  a.xxdm=b.lqxx AND a.xqdm=b.name
where a.pcdm='" + pcdm + "'  and  xxdm BETWEEN 005 AND 014   ";
                _dbA.execSql_Tran(sql1);
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


        public bool Gxpzt004(string pcdm)
        {
            try
            {

                _dbA.BeginTran();
                string sql1 = "delete zk_zsjh_xq WHERE xxdm BETWEEN 005 AND 014;";
                string sql = "INSERT INTO zk_zsjh_xq SELECT * FROM zk_zsjh_xq_5_14_gx";
                int iCount2 = _dbA.execSql_Tran(sql1);
                int iCount = _dbA.execSql_Tran(sql);

                StringBuilder stb = new StringBuilder();
                if (iCount > 0 && iCount2 > 0)
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
        /// 更新是否分县区
        /// </summary>
        /// <returns></returns>
        public bool sfxq(int sfxq, string pcdm)
        {
            try
            {

                _dbA.BeginTran();
                string sql = @" update zk_Pc_TouDang_jbtj set sfxq=" + sfxq + " where right(xpcid,2)='" + pcdm + "'";

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
           
        }
        /// <summary>
        /// 加载是否分县区
        /// </summary>
        /// <param name="xpcId">小批次ID。</param>
        public DataTable select_sfxq(string pcdm)
        {
            string sql = "select * from zk_Pc_TouDang_jbtj where right(xpcid,2)=@pcdm";
            List<SqlParameter> lisP = new List<SqlParameter>() { 
                new SqlParameter("@pcdm",pcdm)
            };

            return this._dbA.selectTab(sql, lisP, ref error, ref bReturn);
        }

        public DataTable GetTableDatas(bool sf)
        {
            string sql = "";
            if (sf)
            {
                sql = @"select  xqdm ,zsxxdm,SUM(sysl) f_zbssl  from (
                      select  xqdm, zsxxdm,  SUM( f_zbssl)-SUM( yt_sl) as sysl   from View_dxs
      group by xqdm,zsxxdm    ) a GROUP BY xqdm,zsxxdm";
            }
            else
            {
                sql = @"select '500' xqdm ,zsxxdm,SUM(sysl) f_zbssl  from (
                      select   zsxxdm,  SUM( f_zbssl)-SUM( yt_sl) as sysl   from View_dxs
                      group by zsxxdm 
                      ) a GROUP BY zsxxdm";
            }
            return this._dbA.selectTab(sql,   ref error, ref bReturn);
        }

        /// <summary>
        /// 更新没录取的考生志愿到下一批次
        /// </summary>
        /// <returns></returns>
        public bool GxpZY(string pcdm)
        {
            try
            {
                int c = Convert.ToInt32(pcdm) + 1;
                _dbA.BeginTran();
                string sql = @" UPDATE zk_kszyxx SET pcdm='" + c + "'+'1',kjbs='500'+'" + c + "'+'1_500'+'" + c + "'+'11' WHERE pcdm='" + pcdm + "'+'1'" +
               " AND ksh IN (SELECT ksh FROM zk_lqk WHERE ISNULL(lqxx,'')='' )";

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
        /// 
        /// </summary>
        public DataTable select_tzfsx()
        {
            string sql = @" exec [proc_tj]  ";
           
            DataTable tab = this._dbA.selectTab(sql,   ref error, ref bReturn);
            return tab;
        }


        /// <summary>
        /// 修改县区最低分数线控制。
        /// </summary>
        public bool UPXqZdFsx_2(DataRow[] dr,int fs)
        {
            SqlDbHelper_1 dbA = new SqlDbHelper_1();

            try
            {

                int iCount = 0;
                if (dr != null)
                {
                    dbA.BeginTran();
                   
                    for (int i = 0; i < dr.Length; i++)
                    {
                        string xqdm = dr[i]["name"].ToString();
                        decimal fsx = Convert.ToDecimal(dr[i]["n"].ToString()) - fs;
                        for (int j = 0; j < xqdm.Split(',').Length; j++)
                        {
                            string sql = "update  zk_Yc_XqYcZdFsx set yc_ZdFensuKzx='" + fsx + "' where yc_Xqdm='" + xqdm.Split(',')[j] + "'";
                            iCount = dbA.execSql_Tran(sql);
                            iCount += iCount;
                        }
                     
                    }
                }
                if (iCount > 0)
                {
                    dbA.EndTran(true);
                    return true;
                }

                dbA.EndTran(false);
                return false;
            }
            catch (Exception exe)
            {
                dbA.EndTran(false);
                return false;
            }
            return false;
        }

    }

}
