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
    /// 预测最低分数控制类。
    /// </summary>
    public class BLL_Yc_XqYcZdFsx
    {
        /// <summary>
        /// 数据库操作类。
        /// </summary>
        private SqlDbHelper_1 _dbA = new SqlDbHelper_1();
        /// <summary>
        /// 返回的错误信息。
        /// </summary>
        private string error = "";
        /// <summary>
        /// 执行成功的标识：true、表示执行成功；false、表示执行失败。
        /// </summary>
        bool bReturn = false;

        /// <summary>
        /// 构造方法。
        /// </summary>
        public BLL_Yc_XqYcZdFsx() { }

        /// <summary>
        /// 查询预测最低分数线的县区信息。
        /// </summary>
        public DataTable selectYcXqInfo()
        {
            try
            {
                string sql = "";
                this._dbA.BeginTran();
                //查询各县区预测最低分数线数据。
                sql = "select yc_Xqdm,yc_XqMc,yc_ZdFensuKzx,yc_Bl,yc_Fangsi=(case yc_Fangsi when 0 then '按计划' else '按学校' end),yc_ZsJhx,Sf_Hb from zk_Yc_XqYcZdFsx";
                DataTable tab = this._dbA.execReTab_Tran(sql);
                if (tab.Rows.Count < 1)
                {
                    //判断如果没有数据，则初始化各县区预测最低分数线。
                    sql = "insert into zk_Yc_XqYcZdFsx(yc_Xqdm,yc_XqMc,yc_ZsJhx,yc_Bl) select xqdm,xqmc,0,100 from zk_xqdm where right(xqdm,len(xqdm)-2) not in('00','99') and left(xqdm,2)!='32'";
                    this._dbA.execSql_Tran(sql);
                    //再次查询各县区预测最低分数线。
                    sql = "select yc_Xqdm,yc_XqMc,yc_ZdFensuKzx,yc_Bl,yc_Fangsi,yc_ZsJhx from zk_Yc_XqYcZdFsx";
                    tab = this._dbA.execReTab_Tran(sql);
                    if (tab.Rows.Count < 1)
                    {
                        return null;
                    }
                }
                else
                {
                }
                this._dbA.EndTran(true);
                return tab;
            }
            catch (Exception exe)
            {
                this._dbA.EndTran(false);
            }
            return null;
        }

        /// <summary>
        /// 修改县区的计划数和预测比例。
        /// </summary>
        /// <param name="xqdm">县区代码。</param>
        /// <param name="bl">预测比例。</param>
        /// <param name="jhs">县区计划数</param>
        /// <param name="hbxqdm">合并县区代码</param>
        /// <param name="bFlag">标识：true、表示当前县区在合并县区中；false、表示不存合并县区中</param>
        public bool updateYcInfo(string xqdm, int bl, int jhs, string hbxqdm, bool bFlag)
        {
            try
            {
                string sql = "";

                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter Yc_Bl = new SqlParameter("@yc_Bl", SqlDbType.Int);
                Yc_Bl.Value = bl;
                lisP.Add(Yc_Bl);
                this._dbA.BeginTran();

                //修改当前合并县区标识。
                if (hbxqdm.Length < 1)
                {
                    sql = "update zk_Yc_XqYcZdFsx set Sf_Hb=0";
                    this._dbA.execSql_Tran(sql);
                }
                else
                {
                    sql = "update zk_Yc_XqYcZdFsx set Sf_Hb=1 where yc_Xqdm in (" + hbxqdm + ")";
                    this._dbA.execSql_Tran(sql);
                    sql = "update zk_Yc_XqYcZdFsx set Sf_Hb=0 where yc_Xqdm  not in(" + hbxqdm + ")";
                    this._dbA.execSql_Tran(sql);
                }
                //bFlag:true、表示当前修改的县区在合并县区中。
                if (bFlag)
                {
                    //将合并县区的预测比例修改为新的预测比例。
                    sql = "update zk_Yc_XqYcZdFsx set yc_Bl=@yc_Bl where Sf_Hb=1";
                    this._dbA.execSql_Tran(sql, lisP);
                }
                //修改当前县区的招生计划数和预测比例。
                sql = "update zk_Yc_XqYcZdFsx set yc_Bl=@yc_Bl,yc_ZsJhx=@yc_ZsJhx where yc_Xqdm=@yc_Xqdm";


                SqlParameter Yc_ZsJhx = new SqlParameter("@yc_ZsJhx", SqlDbType.Int);
                SqlParameter Yc_Xqdm = new SqlParameter("@yc_Xqdm", SqlDbType.VarChar);

                Yc_ZsJhx.Value = jhs;
                Yc_Xqdm.Value = xqdm;

                lisP.Add(Yc_ZsJhx);
                lisP.Add(Yc_Xqdm);


                int iCount = this._dbA.execSql_Tran(sql, lisP);
                if (iCount > 0)
                {
                    this._dbA.EndTran(true);
                    return true;
                }
                this._dbA.EndTran(false);
            }
            catch (Exception exe)
            {
                this._dbA.EndTran(false);
            }
            return false;
        }

        /// <summary>
        /// 开始预测最低控件线(-1、表示无县区信息；-2、有异常；1、执行成功；2、合并县区的预测比例不相同)。
        /// 预测说明：
        /// </summary>
        public int StartCountZdKzx(string val)
        {
            try
            {
                string[] strs = val.Split(',');
                StringBuilder stb123 = new StringBuilder();
                for (int i = 0; i < strs.Length; i++)
                {
                    if (stb123.Length > 0)
                    {
                        stb123.Append(",");
                    }
                    stb123.Append("'");
                    stb123.Append(strs[i].Trim());
                    stb123.Append("'");
                }
                string sql = String.Format("select yc_Xqdm,yc_ZsJhx,yc_Bl from zk_Yc_XqYcZdFsx where yc_Xqdm in({0})", stb123.ToString());
                this._dbA.BeginTran();
                DataTable tab = this._dbA.execReTab_Tran(sql);
                int jhs = -1;
                decimal bl = 0;
                StringBuilder stb = new StringBuilder();
                /*
                 * 预测计算说明：
                 * 一、如果有合并县区，则先进行合并县区的控档线预测计算
                 * 预测方式：
                 *    1、查询合并县区的招生计划总数，将合并县区的招生计划计算总和；
                 *    2、查询合并县区所有已报考普高类的考生，按分数的高到低进行降序排序；
                 *    3、将招生计划之和与预测比例相乘得到需要预测的招生计划总数；
                 *    4、从考生查询的表中查找位于招生计划总数索引减1的考生，获取当前考生的分数即为最低分数控档线；
                 *       如果报考普高类的考生数量小于招生计划总数，则以考生的最低分为准得到合并全区的最低分数控档线；
                 *    5、将此分数更新到数据库中，此分数是所有合并县区的最低分数控档线。
                 * 二、没有合并的县区的最低控档线计算
                 * 预测方法：
                 *    1、查询当前县区的招生计划数和预测比例。
                 *    2、根据招生计划数和预测比例进行计算，得到按比例后的招生计划数。
                 *    3、查询当前县区已报考普高类的考生，按分数从高到低进行排序。
                 *    4、从查询出来的考生信息表中查找位于招生计划数-1的索引位置的考生，获取其分数，即为当前县区的最低分数控档线；
                 *       如果当前县区报考普高类的考生总数小于当前县区的招生计划数，则以已报考普高类考生的最低分为当前县区最低分数控档线。
                 *    5、将其更新到数据库中。
                 */


                //合并县区的最低分数控档线预测。
                if (tab.Rows.Count > 0)
                {
                    //计算合并县区。
                    int iJhsTemp = 0;
                    decimal decTemp = 0;
                    for (int i = 0; i < tab.Rows.Count; i++)
                    {
                        //当前计划数
                        int.TryParse(tab.Rows[i]["yc_ZsJhx"].ToString(), out iJhsTemp);
                        //jhs += iJhsTemp;
                        jhs = iJhsTemp;
                        //当前预测比例
                        decimal.TryParse(tab.Rows[i]["yc_Bl"].ToString(), out decTemp);
                        if (bl > 0)
                        {
                            if (bl != decTemp)
                            {
                                return 2;
                            }
                        }
                        if (stb.Length > 0)
                        {
                            stb.Append(",");
                        }
                        stb.Append("'");
                        stb.Append(tab.Rows[i]["yc_Xqdm"].ToString());
                        stb.Append("'");

                        bl = decTemp;
                    }
                    jhs = (int)(jhs * bl) / 100;
                    UpdateYcFsx(stb.ToString(), jhs);
                }

                //没有合并的县区最低分数控档线预测。
                sql = String.Format("select yc_Xqdm,yc_ZsJhx,yc_Bl from zk_Yc_XqYcZdFsx where yc_Xqdm not in({0})", stb123.ToString());
                tab = this._dbA.execReTab_Tran(sql);
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    jhs = -1;
                    //计算招生计划数。
                    int.TryParse(tab.Rows[i]["yc_ZsJhx"].ToString(), out jhs);
                    decimal.TryParse(tab.Rows[i]["yc_Bl"].ToString(), out bl);

                    //等于0时不计算。
                    if (jhs == 0)
                    {
                        continue;
                    }
                    jhs = (int)(jhs * bl) / 100;
                    UpdateYcFsx(tab.Rows[i]["yc_Xqdm"].ToString(), jhs);
                }
                this._dbA.EndTran(true);
                return 1;
            }
            catch (Exception exe)
            {
                this._dbA.EndTran(false);
            }
            return -2;
        }

        /// <summary>
        /// 进行县区最低分数控档线预测的计划。
        /// </summary>
        /// <param name="xqdms">县区代码，可多个，多个县区代码采用['代码1','代码2','代码3','代码4']</param>
        /// <param name="jhs"></param>
        private void UpdateYcFsx(string xqdms,int jhs)
        {
            //根据县区代码查询所有报考普高志愿
            /*
             * 查询所有符合条件的考生成绩：
             * 1、查询关联表（zk_kszyxx、zk_kszkcj、zk_zydz_xpcxx）；
             * 2、查询报考了批次为普高批次所属志愿的所有考生。
             * 3、按成绩从高到你排序。
             * 4、普高批次标识与批次类型标识无关。
             */
            decimal dec = 0;
            string sql = "select cj from PgKaoSenInfo_end_View where xqdm in(" + xqdms + ") order by cj DESC";
            DataTable tab_2 = this._dbA.execReTab_Tran(sql);
            if (tab_2.Rows.Count < jhs)
            {
                dec = decimal.Parse(tab_2.Rows[tab_2.Rows.Count - 1]["cj"].ToString());
            }
            else
            {
                dec = decimal.Parse(tab_2.Rows[jhs]["cj"].ToString());
            }
            sql = "update zk_Yc_XqYcZdFsx set yc_ZdFensuKzx=" + dec.ToString() + " where yc_Xqdm in(" + xqdms + ")";
            this._dbA.execSql_Tran(sql);
        }
    }
}
