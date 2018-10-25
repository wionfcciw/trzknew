using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Data.SqlClient;
using System.Data;

namespace BLL
{
    /// <summary>
    /// 批次投档条件设置操作类。
    /// </summary>
    public class BLL_zk_Pctd_tj_Info
    {
        #region 字段。
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
        #endregion

        /// <summary>
        /// 构造方法。
        /// </summary>
        public BLL_zk_Pctd_tj_Info() { }

        /// <summary>
        /// 查询志愿批次信息。
        /// </summary>
        public DataTable selectPcdm()
        {
            try
            {
                //string sql = "select xpc_id=xpcId+'_'+convert(varchar(20),pcLb),xpc_mc='['+pcdm+']  '+xpcXsMc from zk_zydz_xpcxx";
                //管理部门权限
                int UserType = SincciLogin.Sessionstu().UserType;
                string Department = SincciLogin.Sessionstu().U_department;
                string where = "   xqdm='500'  ";
            
                string sql = " select xpc_id=xpcId+'_'+convert(varchar(20),pcLb),xpc_mc='{'+b.xqmc+'}'+'['+pcdm+']  '+xpcXsMc from zk_zydz_xpcxx a left join zk_xqdm b on LEFT( a.dpcdm,3)=b.xqdm where   " + where;
               
                DataTable tab = this._dbA.selectTab(sql,  ref error, ref bReturn); 
                return tab;
            }
            catch (Exception exe)
            {
            }
            return null;
        }

        #region 查询批次投档定制的条件。
        /// <summary>
        /// 查询基本条件。
        /// </summary>
        /// <param name="xpcId">小批次Id </param>
        public Model_zk_Pc_TouDang_jbtj Select_Jbtj(string xpcId)
        {
            Model_zk_Pc_TouDang_jbtj info = new Model_zk_Pc_TouDang_jbtj(); 
            
            string sql = "select xpcId,pcdm,tdsf,zdkdx,zdy_zdfs,sfZdfd,zsjhssfxt from zk_Pc_TouDang_jbtj where xpcId =@xpcId";
            List<SqlParameter> lisP = new List<SqlParameter>();
            SqlParameter XpcId = new SqlParameter("@xpcId", SqlDbType.VarChar);
            XpcId.Value = xpcId;
            lisP.Add(XpcId);

            DataTable tab = this._dbA.selectTab(sql, lisP, ref error, ref bReturn);
            if (tab != null && tab.Rows.Count > 0)
            {
                info = this._dbA.DT2EntityList<Model_zk_Pc_TouDang_jbtj>(tab)[0];
                info.bFlag = true;
                return info;
            }
            return null;
        }
        
        /// <summary>
        /// 查询同分跟进。
        /// </summary>
        /// <param name="xpcId">小批次Id </param>
        public Model_zk_Pc_TouDang_tfgj Select_Tfgj(string xpcId)
        {
            Model_zk_Pc_TouDang_tfgj info = new Model_zk_Pc_TouDang_tfgj();
            try
            {
                string sql = "select xpcId,pcdm,sftfgj from zk_Pc_TouDang_tfgj where xpcId =@xpcId";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter XpcId = new SqlParameter("@xpcId", SqlDbType.VarChar);
                XpcId.Value = xpcId;
                lisP.Add(XpcId); 

                this._dbA.BeginTran();
                DataTable tab = this._dbA.execReTab_Tran(sql,lisP);
                if (tab != null && tab.Rows.Count > 0)
                {
                    info = this._dbA.DT2EntityList<Model_zk_Pc_TouDang_tfgj>(tab)[0];

                    if (info.Sftfgj == 2)
                    {
                        sql = "select xpcId,kmdm from zk_Pc_TouDang_tfgj_bjkm where xpcId =@xpcId";

                        tab = this._dbA.execReTab_Tran(sql, lisP);
                        if (tab != null && tab.Rows.Count > 0)
                        {
                            info.Bjkms = this._dbA.DT2EntityList<Model_zk_Pc_TouDang_tfgj_bjkm>(tab);
                        }
                        else
                        {
                            return null;
                        }
                    }
                    info.bFlag = true;
                    return info;
                }

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
        /// 查询指标生。
        /// </summary>
        /// <param name="xpcId">小批次Id </param>
        public Model_zk_Pc_TouDang_zbs Select_Zbs(string xpcId)
        {
            Model_zk_Pc_TouDang_zbs info = new Model_zk_Pc_TouDang_zbs();

            string sql = "select xpcId,pcdm,ywzbs,zbslqfsxz,zdyfs,syzbscl from zk_Pc_TouDang_zbs where xpcId =@xpcId";

            List<SqlParameter> lisP = new List<SqlParameter>();
            SqlParameter XpcId = new SqlParameter("@xpcId", SqlDbType.VarChar);
            XpcId.Value = xpcId;
            lisP.Add(XpcId);

            DataTable tab = this._dbA.selectTab(sql, lisP, ref error, ref bReturn);
            if (tab != null && tab.Rows.Count > 0)
            {
                info = this._dbA.DT2EntityList<Model_zk_Pc_TouDang_zbs>(tab)[0];
                info.bFlag = true;
                return info;
            }
            return null;
        }
        
        /// <summary>
        /// 查询素质评价。
        /// </summary>
        /// <param name="xpcId">小批次Id </param>
        public Model_zk_Pc_TouDang_szpj Select_Szpj(string xpcId)
        {
            Model_zk_Pc_TouDang_szpj info = new Model_zk_Pc_TouDang_szpj();

            string sql = "select xpcId,pcdm,zhszxztj,tjlx,sl,pjdenji from zk_Pc_TouDang_szpj where xpcId =@xpcId";
            List<SqlParameter> lisP = new List<SqlParameter>();
            SqlParameter XpcId = new SqlParameter("@xpcId", SqlDbType.VarChar);
            XpcId.Value = xpcId;
            lisP.Add(XpcId);

            DataTable tab = this._dbA.selectTab(sql, lisP, ref error, ref bReturn);
            if (tab != null && tab.Rows.Count > 0)
            {
                info = this._dbA.DT2EntityList<Model_zk_Pc_TouDang_szpj>(tab)[0];
                info.bFlag = true;
                return info;
            }
            return null;
        }
        
        /// <summary>
        /// 查询其他条件。
        /// </summary>
        /// <param name="xpcId">小批次Id </param>
        public Model_zk_Pc_TouDang_qttj Select_Qttj(string xpcId)
        {
            Model_zk_Pc_TouDang_qttj info = new Model_zk_Pc_TouDang_qttj();

            string sql = "select xpcId,pcdm,jscjxzif,ywhcjhb_jscj,hkcjhgXz,xbxz from zk_Pc_TouDang_qttj where xpcId =@xpcId";
            List<SqlParameter> lisP = new List<SqlParameter>();
            SqlParameter XpcId = new SqlParameter("@xpcId", SqlDbType.VarChar);
            XpcId.Value = xpcId;
            lisP.Add(XpcId);

            DataTable tab = this._dbA.selectTab(sql, lisP, ref error, ref bReturn);
            if (tab != null && tab.Rows.Count > 0)
            {
                info = this._dbA.DT2EntityList<Model_zk_Pc_TouDang_qttj>(tab)[0];
                info.bFlag = true;
                return info;
            }
            return null;
        }
        
        /// <summary>
        /// 查询国际班控档线。
        /// </summary>
        /// <param name="xpcId">小批次Id </param>
        public List<Model_zk_Pc_TouDang_gjbkdx> Select_Gjbkdx(string xpcId)
        {
            List<Model_zk_Pc_TouDang_gjbkdx> info = new List<Model_zk_Pc_TouDang_gjbkdx>();

            string sql = "select xpcId,pcdm,xxdm,kdxfs from zk_Pc_TouDang_gjbkdx where xpcId =@xpcId";
            List<SqlParameter> lisP = new List<SqlParameter>();
            SqlParameter XpcId = new SqlParameter("@xpcId", SqlDbType.VarChar);
            XpcId.Value = xpcId;
            lisP.Add(XpcId);

            DataTable tab = this._dbA.selectTab(sql, lisP, ref error, ref bReturn);
            if (tab != null && tab.Rows.Count > 0)
            {
                info = this._dbA.DT2EntityList<Model_zk_Pc_TouDang_gjbkdx>(tab);
                return info;
            }
            return null;
        }
        #endregion

        #region 新增批次投档定制的条件。
        /// <summary>
        /// 新增批次投档控制的基本条件。
        /// </summary>
        /// <param name="info">需要新增的信息。 </param>
        public bool Insert_Jbtj(Model_zk_Pc_TouDang_jbtj info) 
        {
            try
            {
                string sql = "delete from zk_Pc_TouDang_jbtj where xpcId=@xpcId";
                                
                List<SqlParameter> lisP = new List<SqlParameter>();
                
                SqlParameter XpcId = new SqlParameter("@xpcId", SqlDbType.VarChar);
                XpcId.Value = info.XpcId;
                lisP.Add(XpcId);

                this._dbA.BeginTran();
                this._dbA.execSql_Tran(sql, lisP);

                sql = "insert into zk_Pc_TouDang_jbtj(xpcId,pcdm,tdsf,zdkdx,zdy_zdfs,sfZdfd,zsjhssfxt) values(@xpcId,@pcdm,@tdsf,@zdkdx,@zdy_zdfs,@sfZdfd,@zsjhssfxt)";

                SqlParameter Pcdm = new SqlParameter("@pcdm", SqlDbType.VarChar);
                SqlParameter Tdsf = new SqlParameter("@tdsf", SqlDbType.Int);
                SqlParameter Zdkdx = new SqlParameter("@zdkdx", SqlDbType.Int);
                SqlParameter Zdy_zdfs = new SqlParameter("@zdy_zdfs", SqlDbType.Decimal);
                SqlParameter SfZdfd = new SqlParameter("@sfZdfd", SqlDbType.Int);
                SqlParameter Zsjhssfxt = new SqlParameter("@zsjhssfxt", SqlDbType.Int);
                
                Pcdm.Value = info.Pcdm;
                Tdsf.Value = info.Tdsf;
                Zdkdx.Value = info.Zdkdx;
                Zdy_zdfs.Value = info.Zdy_zdfs;
                SfZdfd.Value = info.SfZdfd;
                Zsjhssfxt.Value = info.Zsjhssfxt;

                lisP.Add(Pcdm);
                lisP.Add(Tdsf);
                lisP.Add(Zdkdx);
                lisP.Add(Zdy_zdfs);
                lisP.Add(SfZdfd);
                lisP.Add(Zsjhssfxt);

                int iCount = this._dbA.execSql_Tran(sql, lisP);
                this._dbA.EndTran(true);
                return true;
            }
            catch (Exception exe)
            {
                this._dbA.EndTran(false);
            }
            return false;
        }

        /// <summary>
        /// 新增批次投档控制的同分跟进。
        /// </summary>
        /// <param name="info">需要新增的信息。</param>
        public bool Insert_Tfgj(Model_zk_Pc_TouDang_tfgj info)
        {
            try
            {
                string sql = "delete from zk_Pc_TouDang_tfgj where xpcId=@xpcId";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter XpcId = new SqlParameter("@xpcId", SqlDbType.VarChar);
                XpcId.Value = info.XpcId;
                lisP.Add(XpcId);

                this._dbA.BeginTran();

                this._dbA.execSql_Tran(sql, lisP);
                sql = "delete from zk_Pc_TouDang_tfgj_bjkm where xpcId=@xpcId";
                this._dbA.execSql_Tran(sql, lisP);

                sql = "insert into zk_Pc_TouDang_tfgj(xpcId,pcdm,sftfgj) values(@xpcId,@pcdm,@sftfgj)";

                SqlParameter Pcdm = new SqlParameter("@pcdm", SqlDbType.VarChar);
                SqlParameter Sftfgj = new SqlParameter("@sftfgj", SqlDbType.Int);

                Pcdm.Value = info.Pcdm;
                Sftfgj.Value = info.Sftfgj;

                lisP.Add(Pcdm);
                lisP.Add(Sftfgj);

                int iCount = this._dbA.execSql_Tran(sql, lisP);
                if (info.Sftfgj == 2)
                {
                    if (info.Bjkms.Count < 0)
                    {
                        return false;
                    }

                    sql = "insert into zk_Pc_TouDang_tfgj_bjkm(xpcId,kmdm) values(@xpcId,@kmdm)";
                    SqlParameter Kmdm = new SqlParameter("@kmdm", SqlDbType.VarChar);
                    foreach (Model_zk_Pc_TouDang_tfgj_bjkm item in info.Bjkms)
                    {
                        Kmdm.Value = item.Kmdm;

                        lisP.Clear();
                        lisP.Add(XpcId);
                        lisP.Add(Kmdm);

                        this._dbA.execSql_Tran(sql, lisP);
                    }
                }
                this._dbA.EndTran(true);
                return true;
            }
            catch (Exception exe)
            {
                this._dbA.EndTran(false);
            }
            return false;
        }

        /// <summary>
        /// 查询指标生。
        /// </summary>
        /// <param name="info">需要新增的信息。</param>
        public bool Insert_Zbs(Model_zk_Pc_TouDang_zbs info)
        {
            try
            {
                string sql = "delete from zk_Pc_TouDang_zbs where xpcId=@xpcId";

                List<SqlParameter> lisP = new List<SqlParameter>();

                SqlParameter XpcId = new SqlParameter("@xpcId", SqlDbType.VarChar);
                XpcId.Value = info.XpcId;
                lisP.Add(XpcId);

                this._dbA.BeginTran();
                this._dbA.execSql_Tran(sql, lisP);


                sql = "insert into zk_Pc_TouDang_zbs(xpcId,pcdm,ywzbs,zbslqfsxz,zdyfs,syzbscl) values(@xpcId,@pcdm,@ywzbs,@zbslqfsxz,@zdyfs,@syzbscl)";

                SqlParameter Pcdm = new SqlParameter("@pcdm", SqlDbType.VarChar);
                SqlParameter Ywzbs = new SqlParameter("@ywzbs", SqlDbType.Int);
                SqlParameter Zbslqfsxz = new SqlParameter("@zbslqfsxz", SqlDbType.Int);
                SqlParameter Zdyfs = new SqlParameter("@zdyfs", SqlDbType.Decimal);
                SqlParameter Syzbscl = new SqlParameter("@syzbscl", SqlDbType.Int);

                Pcdm.Value = info.Pcdm;
                Ywzbs.Value = info.Ywzbs;
                Zbslqfsxz.Value = info.Zbslqfsxz;
                Zdyfs.Value = info.Zdyfs;
                Syzbscl.Value = info.Syzbscl;

                lisP.Add(Pcdm);
                lisP.Add(Ywzbs);
                lisP.Add(Zbslqfsxz);
                lisP.Add(Zdyfs);
                lisP.Add(Syzbscl);

                int iCount = this._dbA.execSql_Tran(sql, lisP);
                this._dbA.EndTran(true);
                return true;
            }
            catch (Exception exe)
            {
                this._dbA.EndTran(false);
            }
            return false;
        }

        /// <summary>
        /// 新增批次投档控制素质评价。
        /// </summary>
        /// <param name="info">需要新增的信息。</param>
        public bool Insert_Szpj(Model_zk_Pc_TouDang_szpj info)
        {
            try
            {
                string sql = "delete from zk_Pc_TouDang_szpj where xpcId=@xpcId";

                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter XpcId = new SqlParameter("@xpcId", SqlDbType.VarChar);
                XpcId.Value = info.XpcId;
                lisP.Add(XpcId);

                this._dbA.BeginTran();
                this._dbA.execSql_Tran(sql, lisP);


                sql = "insert into zk_Pc_TouDang_szpj(xpcId,pcdm,zhszxztj,tjlx,sl,pjdenji) values(@xpcId,@pcdm,@zhszxztj,@tjlx,@sl,@pjdenji)";

                SqlParameter Pcdm = new SqlParameter("@pcdm", SqlDbType.VarChar);
                SqlParameter Zhszxztj = new SqlParameter("@zhszxztj", SqlDbType.Int);
                SqlParameter Tjlx = new SqlParameter("@tjlx", SqlDbType.Int);
                SqlParameter Sl = new SqlParameter("@sl", SqlDbType.Int);
                SqlParameter Pjdenji = new SqlParameter("@pjdenji", SqlDbType.VarChar);

                Pcdm.Value = info.Pcdm;
                Zhszxztj.Value = info.Zhszxztj;
                Tjlx.Value = info.Tjlx;
                Sl.Value = info.Sl;
                Pjdenji.Value = info.Pjdenji;

                lisP.Add(Pcdm);
                lisP.Add(Zhszxztj);
                lisP.Add(Tjlx);
                lisP.Add(Sl);
                lisP.Add(Pjdenji);

                int iCount = this._dbA.execSql_Tran(sql, lisP);
                this._dbA.EndTran(true);
                return true;
            }
            catch (Exception exe)
            {
                this._dbA.EndTran(false);
            }
            return false;
        }

        /// <summary>
        /// 新增批次投档控制其他条件。
        /// </summary>
        /// <param name="info">需要新增的信息。</param>
        public bool Insert_Qttj(Model_zk_Pc_TouDang_qttj info)
        {
            try
            {
                string sql = "delete from zk_Pc_TouDang_qttj where xpcId=@xpcId";

                List<SqlParameter> lisP = new List<SqlParameter>();

                SqlParameter XpcId = new SqlParameter("@xpcId", SqlDbType.VarChar);
                XpcId.Value = info.XpcId;
                lisP.Add(XpcId);

                this._dbA.BeginTran();
                this._dbA.execSql_Tran(sql, lisP);

                sql = "insert into zk_Pc_TouDang_qttj(xpcId,pcdm,jscjxzif,ywhcjhb_jscj,hkcjhgXz,xbxz) values(@xpcId,@pcdm,@jscjxzif,@ywhcjhb_jscj,@hkcjhgXz,@xbxz)";

                SqlParameter Pcdm = new SqlParameter("@pcdm", SqlDbType.VarChar);
                SqlParameter Jscjxzif = new SqlParameter("@jscjxzif", SqlDbType.Int);
                SqlParameter Ywhcjhb_jscj = new SqlParameter("@ywhcjhb_jscj", SqlDbType.Int);
                SqlParameter HkcjhgXz = new SqlParameter("@hkcjhgXz", SqlDbType.Int);
                SqlParameter Xbxz = new SqlParameter("@xbxz", SqlDbType.Int);

                Pcdm.Value = info.Pcdm;
                Jscjxzif.Value = info.Jscjxzif;
                Ywhcjhb_jscj.Value = info.Ywhcjhb_jscj;
                HkcjhgXz.Value = info.HkcjhgXz;
                Xbxz.Value = info.Xbxz;

                lisP.Add(Pcdm);
                lisP.Add(Jscjxzif);
                lisP.Add(Ywhcjhb_jscj);
                lisP.Add(HkcjhgXz);
                lisP.Add(Xbxz);

                int iCount = this._dbA.execSql_Tran(sql, lisP);
                this._dbA.EndTran(true);
                return true;
            }
            catch (Exception exe)
            {
                this._dbA.EndTran(false);
            }
            return false;
        }

        /// <summary>
        /// 新增批次投档控制国际班控档线。
        /// </summary>
        /// <param name="info">需要新增的信息。</param>
        public bool Insert_Gjbkdx(List<Model_zk_Pc_TouDang_gjbkdx> info)
        {
            try
            {
                if (info==null || info.Count < 1)
                {
                    return false;
                }
                string sql = "delete from zk_Pc_TouDang_gjbkdx where xpcId=@xpcId";

                List<SqlParameter> lisP = new List<SqlParameter>();

                SqlParameter XpcId = new SqlParameter("@xpcId", SqlDbType.VarChar);
                XpcId.Value = info[0].XpcId;
                lisP.Add(XpcId);

                this._dbA.BeginTran();
                this._dbA.execSql_Tran(sql, lisP);


                sql = "insert into zk_Pc_TouDang_gjbkdx(xpcId,pcdm,xxdm,kdxfs) values(@xpcId,@pcdm,@xxdm,@kdxfs)";
                
                SqlParameter Pcdm = new SqlParameter("@pcdm", SqlDbType.VarChar);
                SqlParameter Xxdm = new SqlParameter("@xxdm", SqlDbType.VarChar);
                SqlParameter Kdxfs = new SqlParameter("@kdxfs", SqlDbType.Decimal);
                foreach (Model_zk_Pc_TouDang_gjbkdx item in info)
                {
                    Pcdm.Value = item.Pcdm;
                    Xxdm.Value = item.Xxdm;
                    Kdxfs.Value = item.Kdxfs;

                    lisP.Clear();
                    lisP.Add(XpcId);
                    lisP.Add(Pcdm);
                    lisP.Add(Xxdm);
                    lisP.Add(Kdxfs);
                    int iCount = this._dbA.execSql_Tran(sql, lisP);
                }
                this._dbA.EndTran(true);
                return true;
            }
            catch (Exception exe)
            {
                this._dbA.EndTran(false);
            }
            return false;
        }
        #endregion

        /// <summary>
        /// 根据批次代码返回对应的国际班学校信息。
        /// </summary>
        /// <param name="pcdm">小批次代码。</param>
        public DataTable selectPc_gjb(string pcdm)
        {
            string sql = "select a.xxdm,b.zsxxmc from zk_zsjh a left join zk_zsxxdm b on a.xxdm=b.zsxxdm where pcdm='" + pcdm + "'";
            return this._dbA.selectTab(sql, ref error, ref bReturn);
        }

        /// <summary>
        /// 查询指定批次的限制条件。
        /// </summary>
        /// <param name="xpcId">小批次的标识ID。</param>
        public Model_zk_Pctd_tj_Info selectPcTj(string xpcId)
        {
            Model_zk_Pctd_tj_Info info = new Model_zk_Pctd_tj_Info();
            //基本条件。
            info.Jbtj = Select_Jbtj(xpcId);
            if (info.Jbtj == null)
            {
                info.Jbtj = new Model_zk_Pc_TouDang_jbtj();
            }

            //同分跟进。
            info.Tfgj = Select_Tfgj(xpcId);
            if (info.Tfgj == null)
            {
                info.Tfgj = new Model_zk_Pc_TouDang_tfgj();
            }
            
            //指标生。
            info.Zbs = Select_Zbs(xpcId);
            if (info.Zbs == null)
            {
                info.Zbs = new Model_zk_Pc_TouDang_zbs();
            }

            //素质评价。
            info.Szpj = Select_Szpj(xpcId);
            if (info.Szpj == null)
            {
                info.Szpj = new Model_zk_Pc_TouDang_szpj();
            }

            ////其他条件。
            //info.Qttj = Select_Qttj(xpcId);
            //if (info.Qttj == null)
            //{
            //    info.Qttj = new Model_zk_Pc_TouDang_qttj();
            //}

            //国际班控档线。
            info.Gjbkdx = Select_Gjbkdx(xpcId);
            if (info.Gjbkdx == null)
            {
                info.Gjbkdx = new List<Model_zk_Pc_TouDang_gjbkdx>();
            }
            if (info.Jbtj.bFlag && info.Tfgj.bFlag && info.Zbs.bFlag && info.Szpj.bFlag )
            {
                info.bFlag = true;
            }

            return info;
        }
    }
}
