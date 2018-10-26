using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DAL;
using Model;
namespace BLL
{
    /// <summary>
    /// 志愿定制。
    /// </summary>
    public class BLL_zk_zydz
    {
        /// <summary>
        /// 数据库操作类。
        /// </summary>
        private SqlDbHelper_1 _dbHelper = new SqlDbHelper_1();
        private string error = "";
        private bool bReturn = false;

        #region 新增操作。
        /// <summary>
        /// 新增县区信息。
        /// </summary>
        /// <param name="item">需要写入数据库的数据。</param>
        public bool Insert_ZydzXq(Model_zk_zydz_zydzxq item)
        {
            try
            {
                string sql = "insert into zk_zydz_xq(xqdm,xqmc) values(@xqdm,@xqmc)";

                List<SqlParameter> lisP = new List<SqlParameter>();

                SqlParameter Xpdm = new SqlParameter("@xqdm", SqlDbType.VarChar);
                SqlParameter Xpmc = new SqlParameter("@xqmc", SqlDbType.VarChar);

                Xpdm.Value = item.Xqdm;
                Xpmc.Value = item.Xqmc;

                lisP.Add(Xpdm);
                lisP.Add(Xpmc);

                this._dbHelper.BeginTran();
                this._dbHelper.execSql_Tran(sql, lisP);
                this._dbHelper.EndTran(true);
                return true;
            }
            catch (Exception exe)
            {
                this._dbHelper.EndTran(false);
            }
            return false;
        }
        /// <summary>
        /// 新增大批次
        /// </summary>
        /// <param name="item">需要新增的数据。</param>
        public bool Insert_Dpc(Model_zk_zydz_dpcxx item)
        {
            try
            {
                string sql = "insert into zk_zydz_dpcxx(dpcId,xqdm,dpcDm,dpcMc,dpcXsMc,xpcSl,sfqy,dpcBz) values(@dpcId,@xqdm,@dpcDm,@dpcMc,@dpcXsMc,@xpcSl,@sfqy,@dpcBz)";

                List<SqlParameter> lisP = new List<SqlParameter>();

                SqlParameter DpcId = new SqlParameter("@dpcId", SqlDbType.VarChar);
                SqlParameter Xqdm = new SqlParameter("@xqdm", SqlDbType.VarChar);
                SqlParameter DpDm = new SqlParameter("@dpcDm", SqlDbType.VarChar);
                SqlParameter DpMc = new SqlParameter("@dpcMc", SqlDbType.VarChar);
                SqlParameter DpXsMc = new SqlParameter("@dpcXsMc", SqlDbType.VarChar);
                SqlParameter XpcSl = new SqlParameter("@xpcSl", SqlDbType.Int);
                SqlParameter Sfqy = new SqlParameter("@sfqy", SqlDbType.Bit);
                SqlParameter DpcBz = new SqlParameter("@dpcBz", SqlDbType.VarChar);

                DpcId.Value = item.DpcId;
                Xqdm.Value = item.Xqdm;
                DpDm.Value = item.DpcDm;
                DpMc.Value = item.DpcMc;
                DpXsMc.Value = item.DpcXsMc;
                XpcSl.Value = item.XpcSl;
                Sfqy.Value = item.Sfqy;
                DpcBz.Value = item.DpcBz;

                lisP.Add(DpcId);
                lisP.Add(Xqdm);
                lisP.Add(DpDm);
                lisP.Add(DpMc);
                lisP.Add(DpXsMc);
                lisP.Add(XpcSl);
                lisP.Add(Sfqy);
                lisP.Add(DpcBz);

                this._dbHelper.BeginTran();
                this._dbHelper.execSql_Tran(sql, lisP);
                this._dbHelper.EndTran(true);
                return true;
            }
            catch (Exception exe)
            {
                this._dbHelper.EndTran(false);
            }
            return false;
        }
        /// <summary>
        /// 新增小批次
        /// </summary>
        /// <param name="item">需要新增的数据。</param>
        public bool Insert_Xpc(Model_zk_zydz_xpcxx item)
        {
            try
            {
                string sql = "insert into zk_zydz_xpcxx(xpcId,pcDm,xpcDm,dpcDm,xpcMc,xpcXsMc,zySl,xxFc,sfqy,xpcBz,pgPc,pcLb) values(@xpcId,@pcDm,@xpcDm,@dpcDm,@xpcMc,@xpcXsMc,@zySl,@xxFc,@sfqy,@xpcBz,@pgPc,@pcLb)";

                List<SqlParameter> lisP = new List<SqlParameter>();

                SqlParameter XpcId = new SqlParameter("@xpcId", SqlDbType.VarChar);
                SqlParameter PcDm = new SqlParameter("@pcDm", SqlDbType.VarChar);
                SqlParameter XpcDm = new SqlParameter("@xpcDm", SqlDbType.VarChar);
                SqlParameter DpcDm = new SqlParameter("@dpcDm", SqlDbType.VarChar);
                SqlParameter XpcMc = new SqlParameter("@xpcMc", SqlDbType.VarChar);
                SqlParameter XpcXsMc = new SqlParameter("@xpcXsMc", SqlDbType.VarChar);
                SqlParameter ZySl = new SqlParameter("@zySl", SqlDbType.Int);
                SqlParameter XxFc = new SqlParameter("@xxFc", SqlDbType.VarChar);
                SqlParameter Sfqy = new SqlParameter("@sfqy", SqlDbType.Bit);
                SqlParameter XpcBz = new SqlParameter("@xpcBz", SqlDbType.VarChar);
                SqlParameter PgPc = new SqlParameter("@pgPc", SqlDbType.Bit);
                SqlParameter PcLb = new SqlParameter("@pcLb", SqlDbType.VarChar);

                XpcId.Value = item.XpcId;
                PcDm.Value = item.PcDm;
                XpcDm.Value = item.XpcDm;
                DpcDm.Value = item.DpcDm;
                XpcMc.Value = item.XpcMc;
                XpcXsMc.Value = item.XpcXsMc;
                ZySl.Value = item.ZySl;
                XxFc.Value = item.XxFc;
                Sfqy.Value = item.Sfqy;
                XpcBz.Value = item.XpcBz;
                PgPc.Value = item.PgPc;
                PcLb.Value = item.PcLb;

                lisP.Add(XpcId);
                lisP.Add(PcDm);
                lisP.Add(XpcDm);
                lisP.Add(DpcDm);
                lisP.Add(XpcMc);
                lisP.Add(XpcXsMc);
                lisP.Add(ZySl);
                lisP.Add(XxFc);
                lisP.Add(Sfqy);
                lisP.Add(XpcBz);
                lisP.Add(PgPc);
                lisP.Add(PcLb);

                this._dbHelper.BeginTran();
                this._dbHelper.execSql_Tran(sql, lisP);
                this._dbHelper.EndTran(true);
                return true;
            }
            catch (Exception exe)
            {
                this._dbHelper.EndTran(false);
            }
            return false;
        }
        /// <summary>
        /// 新增志愿书
        /// </summary>
        /// <param name="item">需要新增的数据。</param>
        public bool Insert_Zy(Model_zk_zydz_zyxx item)
        {
            try
            {
                string sql = "insert into zk_zydz_zyxx(zyId,zyDm,xpcDm,zyMc,zyXsmc,zySl,sfZyFc,sfqy,zyBz) values(@zyId,@zyDm,@xpcDm,@zyMc,@zyXsmc,@zySl,@sfZyFc,@sfqy,@zyBz)";

                List<SqlParameter> lisP = new List<SqlParameter>();

                SqlParameter ZyId = new SqlParameter("@zyId", SqlDbType.VarChar);
                SqlParameter ZyDm = new SqlParameter("@zyDm", SqlDbType.VarChar);
                SqlParameter XpcDm = new SqlParameter("@xpcDm", SqlDbType.VarChar);
                SqlParameter ZyMc = new SqlParameter("@zyMc", SqlDbType.VarChar);
                SqlParameter ZyXsmc = new SqlParameter("@zyXsmc", SqlDbType.VarChar);
                SqlParameter ZySl = new SqlParameter("@zySl", SqlDbType.Int);
                SqlParameter SfZyFc = new SqlParameter("@sfZyFc", SqlDbType.Bit);
                SqlParameter Sfqy = new SqlParameter("@sfqy", SqlDbType.Bit);
                SqlParameter ZyBz = new SqlParameter("@zyBz", SqlDbType.VarChar);

                ZyId.Value = item.ZyId;
                ZyDm.Value = item.ZyDm;
                XpcDm.Value = item.XpcDm;
                ZyMc.Value = item.ZyMc;
                ZyXsmc.Value = item.ZyXsmc;
                ZySl.Value = item.ZySl;
                SfZyFc.Value = item.SfZyHc;
                Sfqy.Value = item.Sfqy;
                ZyBz.Value = item.ZyBz;

                lisP.Add(ZyId);
                lisP.Add(ZyDm);
                lisP.Add(XpcDm);
                lisP.Add(ZyMc);
                lisP.Add(ZyXsmc);
                lisP.Add(ZySl);
                lisP.Add(SfZyFc);
                lisP.Add(Sfqy);
                lisP.Add(ZyBz);

                this._dbHelper.BeginTran();
                this._dbHelper.execSql_Tran(sql, lisP);
                this._dbHelper.EndTran(true);

                return true;
            }
            catch (Exception exe)
            {
                this._dbHelper.EndTran(false);
            }
            return false;
        }
        #endregion

        #region 修改操作。
        /// <summary>
        /// 修改批次定制中的县区信息。
        /// </summary>
        /// <param name="item">需要修改的数据。</param>
        public bool Update_ZyDzXq(Model_zk_zydz_zydzxq item)
        {
            try
            {
                string sql = "update zk_zydz_xq set xqmc=@xqmc where xqdm=@xqdm";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter Xqmc = new SqlParameter("@xqmc", SqlDbType.VarChar);
                SqlParameter Xqdm = new SqlParameter("@xqdm", SqlDbType.VarChar);
                Xqmc.Value = item.Xqmc;
                Xqdm.Value = item.Xqdm;
                lisP.Add(Xqmc);
                lisP.Add(Xqdm);

                this._dbHelper.BeginTran();
                this._dbHelper.execSql_Tran(sql, lisP);
                this._dbHelper.EndTran(true);
                return true;
            }
            catch (Exception exe)
            {
                this._dbHelper.EndTran(false);
            }
            return false;
        }
        /// <summary>
        /// 修改大批次
        /// </summary>
        /// <param name="item">需要新增的数据。</param>
        public bool Update_Dpc(Model_zk_zydz_dpcxx item)
        {
            try
            {
                string sql = "update zk_zydz_dpcxx set dpcMc=@dpcMc,dpcXsMc=@dpcXsMc,xpcSl=@xpcSl,sfqy=@sfqy,dpcBz=@dpcBz where dpcId=@dpcId";

                List<SqlParameter> lisP = new List<SqlParameter>();

                SqlParameter DpcMc = new SqlParameter("@dpcMc", SqlDbType.VarChar);
                SqlParameter DpXsMc = new SqlParameter("@dpcXsMc", SqlDbType.VarChar);
                SqlParameter XpcSl = new SqlParameter("@xpcSl", SqlDbType.Int);
                SqlParameter Sfqy = new SqlParameter("@sfqy", SqlDbType.Bit);
                SqlParameter DpcBz = new SqlParameter("@dpcBz", SqlDbType.VarChar);
                SqlParameter DpcId = new SqlParameter("@dpcId", SqlDbType.VarChar);

                DpcMc.Value = item.DpcMc;
                DpXsMc.Value = item.DpcXsMc;
                XpcSl.Value = item.XpcSl;
                Sfqy.Value = item.Sfqy;
                DpcBz.Value = item.DpcBz;
                DpcId.Value = item.DpcId;

                lisP.Add(DpcMc);
                lisP.Add(DpXsMc);
                lisP.Add(XpcSl);
                lisP.Add(Sfqy);
                lisP.Add(DpcBz);
                lisP.Add(DpcId);


                this._dbHelper.BeginTran();
                this._dbHelper.execSql_Tran(sql, lisP);
                this._dbHelper.EndTran(true);
                return true;
            }
            catch (Exception exe)
            {
                this._dbHelper.EndTran(false);
            }
            return false;
        }
        /// <summary>
        /// 修改小批次
        /// </summary>
        /// <param name="item">需要新增的数据。</param>
        public bool Update_Xpc(Model_zk_zydz_xpcxx item)
        {
            try
            {
                string sql = "update zk_zydz_xpcxx set xpcMc=@xpcMc,xpcXsMc=@xpcXsMc,zySl=@zySl,xxFc=@xxFc,sfqy=@sfqy,xpcBz=@xpcBz,pgPc=@pgPc,pcLb=@pcLb where xpcId=@xpcId";

                List<SqlParameter> lisP = new List<SqlParameter>();

                SqlParameter XpcMc = new SqlParameter("@xpcMc", SqlDbType.VarChar);
                SqlParameter XpcXsMc = new SqlParameter("@xpcXsMc", SqlDbType.VarChar);
                SqlParameter ZySl = new SqlParameter("@zySl", SqlDbType.Int);
                SqlParameter XxFc = new SqlParameter("@xxFc", SqlDbType.VarChar);
                SqlParameter Sfqy = new SqlParameter("@sfqy", SqlDbType.Bit);
                SqlParameter XpcBz = new SqlParameter("@xpcBz", SqlDbType.VarChar);
                SqlParameter XpcDm = new SqlParameter("@xpcDm", SqlDbType.VarChar);
                SqlParameter XpcId = new SqlParameter("@xpcId", SqlDbType.VarChar);
                SqlParameter PgPc = new SqlParameter("@pgPc", SqlDbType.Bit);
                SqlParameter PcLb = new SqlParameter("@pcLb", SqlDbType.VarChar);

                XpcMc.Value = item.XpcMc;
                XpcXsMc.Value = item.XpcXsMc;
                ZySl.Value = item.ZySl;
                XxFc.Value = item.XxFc;
                Sfqy.Value = item.Sfqy;
                XpcBz.Value = item.XpcBz;
                XpcDm.Value = item.XpcDm;
                XpcId.Value = item.XpcId;
                PgPc.Value = item.PgPc;
                PcLb.Value = item.PcLb;

                lisP.Add(XpcMc);
                lisP.Add(XpcXsMc);
                lisP.Add(ZySl);
                lisP.Add(XxFc);
                lisP.Add(Sfqy);
                lisP.Add(XpcBz);
                lisP.Add(XpcDm);
                lisP.Add(XpcId);
                lisP.Add(PgPc);
                lisP.Add(PcLb);

                this._dbHelper.BeginTran();
                this._dbHelper.execSql_Tran(sql, lisP);
                this._dbHelper.EndTran(true);
                return true;
            }
            catch (Exception exe)
            {
                this._dbHelper.EndTran(false);
            }
            return false;
        }
        /// <summary>
        /// 修改志愿书
        /// </summary>
        /// <param name="item">需要新增的数据。</param>
        public bool Update_Zy(Model_zk_zydz_zyxx item)
        {
            try
            {
                string sql = "update zk_zydz_zyxx set zyMc=@zyMc,zyXsmc=@zyXsmc,zySl=@zySl,sfZyFc=@sfZyFc,sfqy=@sfqy,zyBz=@zyBz where zyId=@zyId";

                List<SqlParameter> lisP = new List<SqlParameter>();

                SqlParameter ZyMc = new SqlParameter("@zyMc", SqlDbType.VarChar);
                SqlParameter ZyXsmc = new SqlParameter("@zyXsmc", SqlDbType.VarChar);
                SqlParameter ZySl = new SqlParameter("@zySl", SqlDbType.Int);
                SqlParameter SfZyFc = new SqlParameter("@sfZyFc", SqlDbType.Bit);
                SqlParameter Sfqy = new SqlParameter("@sfqy", SqlDbType.Bit);
                SqlParameter ZyBz = new SqlParameter("@zyBz", SqlDbType.VarChar);
                SqlParameter ZyId = new SqlParameter("@zyId", SqlDbType.VarChar);

                ZyMc.Value = item.ZyMc;
                ZyXsmc.Value = item.ZyXsmc;
                ZySl.Value = item.ZySl;
                SfZyFc.Value = item.SfZyHc;
                Sfqy.Value = item.Sfqy;
                ZyBz.Value = item.ZyBz;
                ZyId.Value = item.ZyId;

                lisP.Add(ZyMc);
                lisP.Add(ZySl);
                lisP.Add(ZyXsmc);
                lisP.Add(SfZyFc);
                lisP.Add(Sfqy);
                lisP.Add(ZyBz);
                lisP.Add(ZyId);

                this._dbHelper.BeginTran();
                this._dbHelper.execSql_Tran(sql, lisP);
                this._dbHelper.EndTran(true);

                return true;
            }
            catch (Exception exe)
            {
                this._dbHelper.EndTran(false);
            }
            return false;
        }
        #endregion

        #region 根据标识ID查询记录。
        /// <summary>
        /// 查询志愿定制中的县区信息。
        /// </summary>
        public Model_zk_zydz_zydzxq Select_Zydz(string xqdm)
        {
            Model_zk_zydz_zydzxq info = new Model_zk_zydz_zydzxq();
            try
            {
                string sql = "select xqdm,xqmc from zk_zydz_xq where xpdm=@xqdm";

                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter Xqdm = new SqlParameter("@xqdm", SqlDbType.VarChar);
                Xqdm.Value = xqdm;
                lisP.Add(Xqdm);

                this._dbHelper.BeginTran();
                DataTable tab = this._dbHelper.execReTab_Tran(sql, lisP);
                if (tab != null && tab.Rows.Count > 0)
                {
                    info = this._dbHelper.DT2EntityList<Model_zk_zydz_zydzxq>(tab)[0];
                    return info;
                }
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbHelper.EndTran(false);
            }
            return null;
        }
        /// <summary>
        /// 查询大批次信息s。
        /// </summary>
        /// <param name="xqdm">县区代码。</param>
        public Model_zk_zydz_dpcxx Select_Dpc(string dpcdm)
        {
            Model_zk_zydz_dpcxx info = new Model_zk_zydz_dpcxx();
            try
            {
                string sql = "select dpcDm,dpcMc,dpcXsMc,xpcSl,sfqy,dpcBz from zk_zydz_dpcxx where dpcId =@dpcId";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter DpcId = new SqlParameter("@dpcId", SqlDbType.VarChar);
                DpcId.Value = dpcdm;
                lisP.Add(DpcId);

                this._dbHelper.BeginTran();
                DataTable tab = this._dbHelper.execReTab_Tran(sql, lisP);
                if (tab != null && tab.Rows.Count > 0)
                {
                    info = this._dbHelper.DT2EntityList<Model_zk_zydz_dpcxx>(tab)[0];
                    return info;
                }
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbHelper.EndTran(false);
            }
            return null;
        }
        /// <summary>
        /// 查询小批次
        /// </summary>
        /// <param name="xpcdm">小批次代码。</param>
        public Model_zk_zydz_xpcxx Select_Xpc(string xpcdm)
        {
            Model_zk_zydz_xpcxx info = new Model_zk_zydz_xpcxx();
            try
            {
                string sql = "select xpcDm,xpcMc,xpcXsMc,zySl,xxFc,sfqy,xpcBz from zk_zydz_xpcxx where xpcId =@xpcId";

                List<SqlParameter> lisP = new List<SqlParameter>();

                SqlParameter XpcId = new SqlParameter("@xpcId", SqlDbType.VarChar);

                XpcId.Value = xpcdm;
                lisP.Add(XpcId);

                this._dbHelper.BeginTran();
                DataTable tab = this._dbHelper.execReTab_Tran(sql, lisP);
                if (tab != null && tab.Rows.Count > 0)
                {
                    info = this._dbHelper.DT2EntityList<Model_zk_zydz_xpcxx>(tab)[0];
                    return info;
                }
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbHelper.EndTran(false);
            }
            return null;
        }
        /// <summary>
        /// 查询志愿信息
        /// </summary>
        /// <param name="zydm">小批次代码。</param>
        public Model_zk_zydz_zyxx Select_Zy(string zydm)
        {
            Model_zk_zydz_zyxx info = new Model_zk_zydz_zyxx();
            try
            {
                string sql = "select zyDm,zyMc,zyXsmc,zySl,sfZyFc,sfqy,zyBz from zk_zydz_zyxx where zyId =@zyId";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter ZyId = new SqlParameter("@zyId", SqlDbType.VarChar);
                ZyId.Value = zydm;
                lisP.Add(ZyId);


                this._dbHelper.BeginTran();
                DataTable tab = this._dbHelper.execReTab_Tran(sql, lisP);
                if (tab != null && tab.Rows.Count > 0)
                {
                    info = this._dbHelper.DT2EntityList<Model_zk_zydz_zyxx>(tab)[0];
                    return info;
                }
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbHelper.EndTran(false);
            }
            return null;
        }
        #endregion

        #region 根据条件查询所有内容操作。
        /// <summary>
        /// 查询志愿定制中的县区信息。
        /// </summary>
        public DataTable Select_All_ZydzXq()
        {
            try
            {
                string sql = "select xqdm,xqmc from zk_zydz_xq";

                this._dbHelper.BeginTran();
                return this._dbHelper.execReTab_Tran(sql);
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbHelper.EndTran(false);
            }
            return null;
        }
        /// <summary>
        /// 查询大批次信息s。
        /// </summary>
        /// <param name="xqdm">县区代码。</param>
        public DataTable Select_All_Dpc(string xqdm)
        {
            try
            {
                string sql = "select dpcId,dpcDm,dpcMc,dpcXsMc,xpcSl,sfqy,dpcBz from zk_zydz_dpcxx where xqdm =@xqdm";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter Xqdm = new SqlParameter("@xqdm", SqlDbType.VarChar);
                Xqdm.Value = xqdm;
                lisP.Add(Xqdm);

                this._dbHelper.BeginTran();
                return this._dbHelper.execReTab_Tran(sql, lisP);
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbHelper.EndTran(false);
            }
            return null;
        }
        /// <summary>
        /// 查询小批次
        /// </summary>
        /// <param name="dpcdm">大批次代码。</param>
        public DataTable Select_All_Xpc(string dpcdm)
        {
            try
            {
                string sql = "select xpcId,xpcDm,xpcMc,xpcXsMc,zySl,xxFc,sfqy,xpcBz from zk_zydz_xpcxx where dpcDm =@dpcDm";

                List<SqlParameter> lisP = new List<SqlParameter>();

                SqlParameter DpcDm = new SqlParameter("@dpcDm", SqlDbType.VarChar);

                DpcDm.Value = dpcdm;

                lisP.Add(DpcDm);

                this._dbHelper.BeginTran();
                return this._dbHelper.execReTab_Tran(sql, lisP);
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbHelper.EndTran(false);
            }
            return null;
        }
        /// <summary>
        /// 查询志愿信息
        /// </summary>
        /// <param name="xpcdm">小批次代码。</param>
        public DataTable Select_All_Zy(string xpcdm)
        {
            try
            {
                string sql = "select zyId,zyDm,zyMc,zyXsmc,zySl,sfZyFc,sfqy,zyBz from zk_zydz_zyxx where xpcDm =@xpcDm";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter XpcDm = new SqlParameter("@xpcDm", SqlDbType.VarChar);
                XpcDm.Value = xpcdm;
                lisP.Add(XpcDm);


                this._dbHelper.BeginTran();
                return this._dbHelper.execReTab_Tran(sql, lisP);
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbHelper.EndTran(false);
            }
            return null;
        }
        #endregion

        #region 删除数据。
        /// <summary>
        /// 删除数据。
        /// </summary>
        /// <param name="type">类型标识(@、县区；#、大批次；$、小批次；%、志愿)。</param>
        /// <param name="flagId">标识Id.</param>
        public bool deleteData(string type, string flagId)
        {
            try
            {
                this._dbHelper.BeginTran();
                string sql = "";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter Xqdm = new SqlParameter("@id", SqlDbType.VarChar);
                Xqdm.Value = flagId;
                lisP.Add(Xqdm);
                switch (type)
                {
                    case "1"://县区
                        //删除在当前县区下的所有志愿。
                        sql = "delete from zk_zydz_zyxx where zyDm in(select xpcDm from zk_zydz_xpcxx where dpcDm in(select dpcDm from zk_zydz_dpcxx where xqdm=@id))";
                        this._dbHelper.execSql_Tran(sql, lisP);
                        //删除当前县区下所有的小批次信息。
                        sql = "delete from zk_zydz_xpcxx where dpcDm in(select dpcDm from zk_zydz_dpcxx where xqdm=@id)";
                        this._dbHelper.execSql_Tran(sql, lisP);
                        //删除当前县区下所有的大批次信息。
                        sql = "delete from zk_zydz_dpcxx where xqdm=@id";
                        this._dbHelper.execSql_Tran(sql, lisP);
                        //删除当前县区信息。
                        sql = "delete from zk_zydz_xq where xqdm=@id";
                        this._dbHelper.execSql_Tran(sql, lisP);
                        break;
                    case "2"://大批次。
                        //删除当前大批次下的所有志愿。
                        sql = "delete from zk_zydz_zyxx where zyDm in(select xpcDm from zk_zydz_xpcxx where dpcDm=@id)";
                        this._dbHelper.execSql_Tran(sql, lisP);
                        //删除当前大批次下所有的小批次信息。
                        sql = "delete from zk_zydz_xpcxx where dpcDm=@id";
                        this._dbHelper.execSql_Tran(sql, lisP);
                        //删除当前大批次下所有的大批次信息。
                        sql = "delete from zk_zydz_dpcxx where dpcId=@id";
                        this._dbHelper.execSql_Tran(sql, lisP);
                        break;
                    case "3"://小批次
                        //删除当前小批次下的所有志愿。
                        sql = "delete from zk_zydz_zyxx where zyDm=@id";
                        this._dbHelper.execSql_Tran(sql, lisP);
                        //删除当前小批次下所有的小批次信息。
                        sql = "delete from zk_zydz_xpcxx where xpcId=@id";
                        this._dbHelper.execSql_Tran(sql, lisP);
                        break;
                    case "4"://志愿
                        //删除当前志愿信息。
                        sql = "delete from zk_zydz_zyxx where zyId=@id";
                        this._dbHelper.execSql_Tran(sql, lisP);
                        break;
                }
                this._dbHelper.EndTran(true);
                return true;
            }
            catch (Exception exe)
            {
                this._dbHelper.EndTran(false);
            }
            return false;
        }
        #endregion

        #region 数据验证区域。
        /// <summary>
        /// 查询当前县区代码是否已存在。
        /// </summary>
        /// <param name="id">县区的标识Id.</param>
        /// <returns></returns>
        public int selectZyDzXq(string id)
        {
            try
            {
                string sql = "select 1 from zk_zydz_xq where xpdm=@id";

                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter Xqdm = new SqlParameter("@xqdm", SqlDbType.VarChar);
                Xqdm.Value = id;
                lisP.Add(Xqdm);

                this._dbHelper.BeginTran();
                return this._dbHelper.execSql_Tran(sql, lisP);
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbHelper.EndTran(false);
            }
            return -1;
        }
        /// <summary>
        /// 查询当前大批次代码是否已存在。
        /// </summary>
        /// <param name="id">大批次的标识Id.</param>
        /// <returns></returns>
        public int selectZyDzDpc(string id)
        {
            try
            {
                string sql = "select 1 from zk_zydz_dpcxx where dpcId =@dpcId";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter DpcId = new SqlParameter("@dpcId", SqlDbType.VarChar);
                DpcId.Value = id;
                lisP.Add(DpcId);

                this._dbHelper.BeginTran();
                DataTable tab= this._dbHelper.execReTab_Tran(sql, lisP);
                if (tab != null && tab.Rows.Count > 0)
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbHelper.EndTran(false);
            }
            return -1;
        }
        /// <summary>
        /// 查询当前小批次代码是否已存在。
        /// </summary>
        /// <param name="id">小批次的标识Id.</param>
        /// <returns></returns>
        public int selectZyDzXpc(string id)
        {
            try
            {
                string sql = "select 1 from zk_zydz_xpcxx where xpcId =@xpcId";

                List<SqlParameter> lisP = new List<SqlParameter>();

                SqlParameter XpcId = new SqlParameter("@xpcId", SqlDbType.VarChar);

                XpcId.Value = id;
                lisP.Add(XpcId);

                this._dbHelper.BeginTran();
                return this._dbHelper.execSql_Tran(sql, lisP);
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbHelper.EndTran(false);
            }
            return -1;
        }
        /// <summary>
        /// 查询当前志愿代码是否已存在。
        /// </summary>
        /// <param name="id">志愿的标识Id.</param>
        /// <returns></returns>
        public int selectZyDzZy(string id)
        {
            try
            {
                string sql = "select 1 from zk_zydz_zyxx where zyId =@zyId";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter ZyId = new SqlParameter("@zyId", SqlDbType.VarChar);
                ZyId.Value = id;
                lisP.Add(ZyId);

                this._dbHelper.BeginTran();
                return this._dbHelper.execSql_Tran(sql, lisP);
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbHelper.EndTran(false);
            }
            return -1;
        }
        #endregion


        /// <summary>
        /// 查询志愿订制的县区(zk_zydz_xq) 返回县区代码
        /// </summary>
        public string Select_zy_xqdm(string xqdm)
        {
            try
            {
                string sql = "";
                sql = "select  * from zk_zydz_xq "; 
                this._dbHelper.BeginTran();
                DataTable dt= this._dbHelper.execReTab_Tran(sql);
                if (dt.Rows.Count == 1)
                {
                    if (dt.Rows[0]["xqdm"].ToString() == xqdm.Substring(0, 2) + "00")
                    {
                        return dt.Rows[0]["xqdm"].ToString();
                    }
                    else
                    {
                        return xqdm;
                    }
                }
                else
                {
                    return xqdm;
                }
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbHelper.EndTran(false);
            }
            return null;
        }



        /// <summary>
        /// 查询志愿数量
        /// </summary>
        public DataTable Select_zy_Num(string xqdm)
        {
            try
            {
                string sql = "";
                sql = "select c.zyId, c.zySl as czySl,c.sfZyFc as csfZyFc,c.sfqy as csfqy,b.xpcMc,c.zyDm,b.pcDm,c.zyMc,xpcId from zk_zydz_dpcxx a,zk_zydz_xpcxx b,zk_zydz_zyxx c where a.dpcId=b.dpcDm and b.xpcId=c.xpcDm and a.sfqy=1 and b.sfqy=1 and c.sfqy=1 and a.xqdm=@xqdm  order by c.zySl desc";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter Xqdm = new SqlParameter("@xqdm", SqlDbType.VarChar);
                Xqdm.Value = xqdm;
                lisP.Add(Xqdm);


                this._dbHelper.BeginTran();
                return this._dbHelper.execReTab_Tran(sql, lisP);
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbHelper.EndTran(false);
            }
            return null;
        }

        /// <summary>
        /// 查询大批次信息s。
        /// </summary>
        /// <param name="xqdm">县区代码。</param>
        public DataTable Select_All_DpcIsPass(string xqdm)
        {
            try
            {
                string sql = "select dpcId,dpcDm,dpcMc,dpcXsMc,xpcSl,sfqy,dpcBz from zk_zydz_dpcxx where xqdm =@xqdm and sfqy=1";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter Xqdm = new SqlParameter("@xqdm", SqlDbType.VarChar);
                Xqdm.Value = xqdm;
                lisP.Add(Xqdm);

                this._dbHelper.BeginTran();
                return this._dbHelper.execReTab_Tran(sql, lisP);
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbHelper.EndTran(false);
            }
            return null;
        }
        /// <summary>
        /// 查询小批次
        /// </summary>
        /// <param name="dpcdm">大批次代码。</param>
        public DataTable Select_All_XpcIsPass(string dpcdm)
        {
            try
            {
                string sql = "select xpcId,xpcDm,xpcMc,xpcXsMc,zySl,sfqy,xpcBz,pcDm from zk_zydz_xpcxx where dpcdm =@dpcdm and sfqy=1";

                List<SqlParameter> lisP = new List<SqlParameter>();

                SqlParameter DpDm = new SqlParameter("@dpcdm", SqlDbType.VarChar);

                DpDm.Value = dpcdm;
                lisP.Add(DpDm);
                this._dbHelper.BeginTran();
                return this._dbHelper.execReTab_Tran(sql, lisP);
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbHelper.EndTran(false);
            }
            return null;
        }

        /// <summary>
        /// 查询志愿信息
        /// </summary>
        /// <param name="xpcdm">小批次代码。</param>

        public DataTable Select_All_ZyIsPass(string xpcdm)
        {
            try
            {
                string sql = "select zyId,zyDm,zyMc,zyXsmc,zySl,sfZyFc,sfqy,zyBz from zk_zydz_zyxx where xpcDm =@xpcDm and sfqy=1 order by zySl desc";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter XpcDm = new SqlParameter("@xpcDm", SqlDbType.VarChar);
                XpcDm.Value = xpcdm;
                lisP.Add(XpcDm);


                this._dbHelper.BeginTran();
                return this._dbHelper.execReTab_Tran(sql, lisP);
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbHelper.EndTran(false);
            }
            return null;
        }


        /// <summary>
        /// 查询大批次下所有的
        /// </summary>
        public DataTable Select_zy_ALLNum(int dpcId)
        {
            try
            {
                string sql = "select * from zk_zydz_dpcxx a,zk_zydz_xpcxx b,zk_zydz_zyxx c where a.dpcId=b.dpcDm and b.xpcId=c.xpcDm and a.sfqy=1 and b.sfqy=1 and c.sfqy=1 and a.dpcId=@dpcId";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter XpcDm = new SqlParameter("@dpcId", SqlDbType.Int);
                XpcDm.Value = dpcId;
                lisP.Add(XpcDm);
                this._dbHelper.BeginTran();
                return this._dbHelper.execReTab_Tran(sql, lisP);
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbHelper.EndTran(false);
            }
            return null;
        }
        /// <summary>
        /// 查询小批次下面专业总数信息
        /// </summary>
        /// <param name="xpcdm">小批次代码。</param>
        public DataTable Select_All_ZyIsPassALLNum(string xpcdm)
        {
            try
            {
                string sql = "select sum(zySl) as sumNum  from zk_zydz_zyxx where xpcDm =@xpcDm and sfqy=1 order by zySl desc";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter XpcDm = new SqlParameter("@xpcDm", SqlDbType.VarChar);
                XpcDm.Value = xpcdm;
                lisP.Add(XpcDm);


                this._dbHelper.BeginTran();
                return this._dbHelper.execReTab_Tran(sql, lisP);
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbHelper.EndTran(false);
            }
            return null;
        }

        /// <summary>
        /// 查询小批次计划学校信息
        /// </summary>
        /// <param name="dpcId"></param>
        /// <returns></returns>
        public DataTable Select_zy_XX(string pcdm, string xqdm)
        {
            try
            {
                string sql = "select distinct zsxxmc , xxdm from View_zk_zsjh where  pcdm=@pcdm and   xqdm in(" + xqdm + ") order by xxdm asc  ";
                List<SqlParameter> lisP = new List<SqlParameter>() { 
                   new SqlParameter("@pcdm", pcdm)
               };
                this._dbHelper.BeginTran();
                return this._dbHelper.execReTab_Tran(sql, lisP);
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbHelper.EndTran(false);
            }
            return null;
        }
        /// <summary>
        /// 查询小批次计划学校专业信息
        /// </summary>
        /// <param name="dpcId"></param>
        /// <returns></returns>
        public DataTable Select_zy_XXZY(string pcdm, string xqdm, string xxdm)
        {
            try
            {
                string sql = "select  zymc, zydm from View_zk_zsjh_zy  where  pcdm=@pcdm and  xqdm in(" + xqdm + ")  and  xxdm=@xxdm ";
                List<SqlParameter> lisP = new List<SqlParameter>() { 
                   new SqlParameter("@pcdm", pcdm), new SqlParameter("@xxdm", xxdm)
               };
                this._dbHelper.BeginTran();
                return this._dbHelper.execReTab_Tran(sql, lisP);
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbHelper.EndTran(false);
            }
            return null;
        }
        /// <summary>
        /// 查询小批次计划学校专业信息
        /// </summary>
        /// <param name="dpcId"></param>
        /// <returns></returns>
        public DataTable Select_zy_kszyxx(string ksh)
        {
            try
            {
                string sql = " select * from zk_kszyxx where ksh=@ksh ";
                List<SqlParameter> lisP = new List<SqlParameter>() { 
                   new SqlParameter("@ksh", ksh)
               };
                this._dbHelper.BeginTran();
                return this._dbHelper.execReTab_Tran(sql, lisP);
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbHelper.EndTran(false);
            }
            return null;
        }
        /// <summary>
        /// 根据考生号zk_kszyxx删除
        /// </summary>
        /// <param name="xqdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool delete_zk_kszyxx(string ksh)
        {
            string sql = "delete  zk_kszyxx where ksh=@ksh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@ksh",ksh)};
            this._dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }

        public DataTable Select_zy(string xqdm)
        {
            try
            {
                string sql = "";
                sql = "select c.zyId, c.zySl as czySl,c.sfZyFc as csfZyFc,c.sfqy as csfqy,b.xpcMc,c.zyDm,b.pcDm,c.zyMc,xpcId,dpcId from zk_zydz_dpcxx a,zk_zydz_xpcxx b,zk_zydz_zyxx c where a.dpcId=b.dpcDm and b.xpcId=c.xpcDm and a.sfqy=1 and b.sfqy=1 and c.sfqy=1 and a.xqdm=@xqdm  order by a.dpcId";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter Xqdm = new SqlParameter("@xqdm", SqlDbType.VarChar);
                Xqdm.Value = xqdm;
                lisP.Add(Xqdm);


                this._dbHelper.BeginTran();
                return this._dbHelper.execReTab_Tran(sql, lisP);
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbHelper.EndTran(false);
            }
            return null;
        }
        /// <summary>
        /// 个人志愿填报记录
        /// </summary>
        /// <param name="xqdm"></param>
        /// <returns></returns>
        public DataTable Select_Viewzy(string ksh)
        {
            try
            {
                string sql = "";
                sql = "  select * from View_zy_zydzxx where ksh=@ksh";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter Xqdm = new SqlParameter("@ksh", SqlDbType.VarChar);
                Xqdm.Value = ksh;
                lisP.Add(Xqdm);


                this._dbHelper.BeginTran();
                return this._dbHelper.execReTab_Tran(sql, lisP);
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbHelper.EndTran(false);
            }
            return null;
        }

        /// <summary>
        /// 个人志愿填报记录联合查询
        /// </summary>
        /// <param name="xqdm"></param>
        /// <returns></returns>
        public DataTable Select_Viewzyxx(string ksh, string xqdm)
        {
            try
            {
                string sql = "";
                sql = "  select * from View_zy_xx where ksh=@ksh and xqdm=@xqdm";
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@ksh", ksh), new SqlParameter("@xqdm", xqdm) };
                this._dbHelper.BeginTran();
                return this._dbHelper.execReTab_Tran(sql, lisP);
            }
            catch (Exception exe)
            {
            }
            finally
            {
                this._dbHelper.EndTran(false);
            }
            return null;
        }


        /// <summary>
        /// 根据县区统计
        /// </summary>
        /// <param name="xqdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable Select_xqtj()
        {

            string sql = "exec proc_zy_xqtj";
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);
            return dt;
        }

        /// <summary>
        /// 班级统计
        /// </summary>
        /// <param name="xqdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable Select_bjtj(string xxdm, string xqdm)
        {

            string sql = "exec proc_zy_bjtj @xxdm,@xqdm";
            List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@xxdm", xxdm), new SqlParameter("@xqdm", xqdm) };

            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            return dt;
        }
        /// <summary>
        /// 学校统计
        /// </summary>
        /// <param name="xqdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable Select_xxtj(string xqdm)
        {

            string sql = "exec proc_zy_xxtj @xqdm";
            List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@xqdm", xqdm) };

            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            return dt;
        }

        /// <summary>
        /// 根据县区确认统计
        /// </summary>
        /// <param name="xqdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable Select_xqtj_qr()
        {

            string sql = "exec proc_zy_xqtj_qr";
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);
            return dt;
        }
        /// <summary>
        /// 学校统计
        /// </summary>
        /// <param name="xqdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable Select_xxtj_qr(string xqdm)
        {

            string sql = "exec proc_zy_xxtj_qr @xqdm";
            List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@xqdm", xqdm) };

            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            return dt;
        }
        /// <summary>
        /// 按条件县区统计
        /// </summary>
        /// <param name="xqdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable Select_xqtj_Where(string where)
        {
            string sql = "exec proc_zy_xqtj_where @where";
            List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@where", where) };

            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            return dt;
        }
        /// <summary>
        /// 学校统计
        /// </summary>
        /// <param name="xqdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable Select_xxtj_Where(string where, string xqdm)
        {

            string sql = "exec proc_zy_xxtj_where @where,@xqdm";
            List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@xqdm", xqdm), new SqlParameter("@where", where) };

            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            return dt;
        }
        /// <summary>
        /// 学校统计
        /// </summary>
        /// <param name="xqdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable Select_bjtj_Where(string where, string xqdm, string bmddm)
        {

            string sql = "exec proc_zy_bjtj_where @where,@bmddm,@xqdm";
            List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@xqdm", xqdm), new SqlParameter("@where", where), new SqlParameter("@bmddm", bmddm) };

            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            return dt;
        }
    }
}
