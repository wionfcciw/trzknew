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
                string sql = "insert into zk_zydz_dpcxx(dpcId,xqdm,dpcDm,dpcMc,dpcXsMc,xpcSl,sfqy,dpcBz,Stime,Etime) values(@dpcId,@xqdm,@dpcDm,@dpcMc,@dpcXsMc,@xpcSl,@sfqy,@dpcBz,@Stime,@Etime)";

                List<SqlParameter> lisP = new List<SqlParameter>();

                SqlParameter DpcId = new SqlParameter("@dpcId", SqlDbType.VarChar);
                SqlParameter Xqdm = new SqlParameter("@xqdm", SqlDbType.VarChar);
                SqlParameter DpDm = new SqlParameter("@dpcDm", SqlDbType.VarChar);
                SqlParameter DpMc = new SqlParameter("@dpcMc", SqlDbType.VarChar);
                SqlParameter DpXsMc = new SqlParameter("@dpcXsMc", SqlDbType.VarChar);
                SqlParameter XpcSl = new SqlParameter("@xpcSl", SqlDbType.Int);
                SqlParameter Sfqy = new SqlParameter("@sfqy", SqlDbType.Bit);
                SqlParameter DpcBz = new SqlParameter("@dpcBz", SqlDbType.VarChar);
                SqlParameter Stime = new SqlParameter("@Stime", SqlDbType.DateTime);
                SqlParameter Etime = new SqlParameter("@Etime", SqlDbType.DateTime);
                DpcId.Value = item.DpcId;
                Xqdm.Value = item.Xqdm;
                DpDm.Value = item.DpcDm;
                DpMc.Value = item.DpcMc;
                DpXsMc.Value = item.DpcXsMc;
                XpcSl.Value = item.XpcSl;
                Sfqy.Value = item.Sfqy;
                DpcBz.Value = item.DpcBz;
                Stime.Value = item.Stime;
                Etime.Value = item.Etime;
                lisP.Add(DpcId);
                lisP.Add(Xqdm);
                lisP.Add(DpDm);
                lisP.Add(DpMc);
                lisP.Add(DpXsMc);
                lisP.Add(XpcSl);
                lisP.Add(Sfqy);
                lisP.Add(DpcBz);
                lisP.Add(Stime);
                lisP.Add(Etime);
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
                string sql = "insert into zk_zydz_zyxx(zyId,zyDm,xpcDm,zyMc,zyXsmc,zySl,sfZyFc,sfqy,zyBz,sfxxfc) values(@zyId,@zyDm,@xpcDm,@zyMc,@zyXsmc,@zySl,@sfZyFc,@sfqy,@zyBz,@sfxxfc)";

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
                SqlParameter Sfxxfc = new SqlParameter("@sfxxfc", SqlDbType.Bit);
                ZyId.Value = item.ZyId;
                ZyDm.Value = item.ZyDm;
                XpcDm.Value = item.XpcDm;
                ZyMc.Value = item.ZyMc;
                ZyXsmc.Value = item.ZyXsmc;
                ZySl.Value = item.ZySl;
                SfZyFc.Value = item.SfZyFc;
                Sfqy.Value = item.Sfqy;
                ZyBz.Value = item.ZyBz;
                Sfxxfc.Value = item.Sfxxfc;
                lisP.Add(ZyId);
                lisP.Add(ZyDm);
                lisP.Add(XpcDm);
                lisP.Add(ZyMc);
                lisP.Add(ZyXsmc);
                lisP.Add(ZySl);
                lisP.Add(SfZyFc);
                lisP.Add(Sfqy);
                lisP.Add(ZyBz);
                lisP.Add(Sfxxfc);
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
                string sql = "update zk_zydz_dpcxx set dpcMc=@dpcMc,dpcXsMc=@dpcXsMc,xpcSl=@xpcSl,sfqy=@sfqy,dpcBz=@dpcBz,stime=@stime,etime=@etime where dpcId=@dpcId";

                List<SqlParameter> lisP = new List<SqlParameter>();

                SqlParameter DpcMc = new SqlParameter("@dpcMc", SqlDbType.VarChar);
                SqlParameter DpXsMc = new SqlParameter("@dpcXsMc", SqlDbType.VarChar);
                SqlParameter XpcSl = new SqlParameter("@xpcSl", SqlDbType.Int);
                SqlParameter Sfqy = new SqlParameter("@sfqy", SqlDbType.Bit);
                SqlParameter DpcBz = new SqlParameter("@dpcBz", SqlDbType.VarChar);
                SqlParameter DpcId = new SqlParameter("@dpcId", SqlDbType.VarChar);
                SqlParameter stime = new SqlParameter("@stime", SqlDbType.VarChar);
                SqlParameter etime = new SqlParameter("@etime", SqlDbType.VarChar);
                DpcMc.Value = item.DpcMc;
                DpXsMc.Value = item.DpcXsMc;
                XpcSl.Value = item.XpcSl;
                Sfqy.Value = item.Sfqy;
                DpcBz.Value = item.DpcBz;
                DpcId.Value = item.DpcId;
                stime.Value = item.Stime;
                etime.Value = item.Etime;
                lisP.Add(DpcMc);
                lisP.Add(DpXsMc);
                lisP.Add(XpcSl);
                lisP.Add(Sfqy);
                lisP.Add(DpcBz);
                lisP.Add(DpcId);
                lisP.Add(stime);
                lisP.Add(etime);

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
                string sql = "update zk_zydz_zyxx set zyMc=@zyMc,zyXsmc=@zyXsmc,zySl=@zySl,sfZyFc=@sfZyFc,sfqy=@sfqy,zyBz=@zyBz,sfxxfc=@sfxxfc where zyId=@zyId";

                List<SqlParameter> lisP = new List<SqlParameter>();

                SqlParameter ZyMc = new SqlParameter("@zyMc", SqlDbType.VarChar);
                SqlParameter ZyXsmc = new SqlParameter("@zyXsmc", SqlDbType.VarChar);
                SqlParameter ZySl = new SqlParameter("@zySl", SqlDbType.Int);
                SqlParameter SfZyFc = new SqlParameter("@sfZyFc", SqlDbType.Bit);
                SqlParameter Sfqy = new SqlParameter("@sfqy", SqlDbType.Bit);
                SqlParameter ZyBz = new SqlParameter("@zyBz", SqlDbType.VarChar);
                SqlParameter ZyId = new SqlParameter("@zyId", SqlDbType.VarChar);
                SqlParameter Sfxxfc = new SqlParameter("@sfxxfc", SqlDbType.Bit);
                ZyMc.Value = item.ZyMc;
                ZyXsmc.Value = item.ZyXsmc;
                ZySl.Value = item.ZySl;
                SfZyFc.Value = item.SfZyFc;
                Sfqy.Value = item.Sfqy;
                ZyBz.Value = item.ZyBz;
                ZyId.Value = item.ZyId;
                Sfxxfc.Value = item.Sfxxfc;
                lisP.Add(ZyMc);
                lisP.Add(ZySl);
                lisP.Add(ZyXsmc);
                lisP.Add(SfZyFc);
                lisP.Add(Sfqy);
                lisP.Add(ZyBz);
                lisP.Add(ZyId);
                lisP.Add(Sfxxfc);
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
       
                string sql = "select xqdm,xqmc from zk_zydz_xq where xpdm=@xqdm";

                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter Xqdm = new SqlParameter("@xqdm", SqlDbType.VarChar);
                Xqdm.Value = xqdm;
                lisP.Add(Xqdm);

             
                DataTable tab = this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
                if (tab != null && tab.Rows.Count > 0)
                {
                    info = this._dbHelper.DT2EntityList<Model_zk_zydz_zydzxq>(tab)[0];
                    return info;
                }
                return info;
        }
        /// <summary>
        /// 查询大批次信息s。
        /// </summary>
        /// <param name="xqdm">县区代码。</param>
        public Model_zk_zydz_dpcxx Select_Dpc(string dpcdm)
        {
            Model_zk_zydz_dpcxx info = new Model_zk_zydz_dpcxx();

            string sql = "select dpcDm,dpcMc,dpcXsMc,xpcSl,sfqy,dpcBz,Stime,Etime from zk_zydz_dpcxx where dpcId =@dpcId";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter DpcId = new SqlParameter("@dpcId", SqlDbType.VarChar);
                DpcId.Value = dpcdm;
                lisP.Add(DpcId);

            
                DataTable tab = this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
                if (tab != null && tab.Rows.Count > 0)
                {
                    info = this._dbHelper.DT2EntityList<Model_zk_zydz_dpcxx>(tab)[0];
                    return info;
                }
                return info;
        }
        /// <summary>
        /// 查询小批次
        /// </summary>
        /// <param name="xpcdm">小批次代码。</param>
        public Model_zk_zydz_xpcxx Select_Xpc(string xpcdm)
        {
            Model_zk_zydz_xpcxx info = new Model_zk_zydz_xpcxx();
            
                string sql = "select xpcDm,xpcMc,xpcXsMc,zySl,xxFc,sfqy,xpcBz from zk_zydz_xpcxx where xpcId =@xpcId";

                List<SqlParameter> lisP = new List<SqlParameter>();

                SqlParameter XpcId = new SqlParameter("@xpcId", SqlDbType.VarChar);

                XpcId.Value = xpcdm;
                lisP.Add(XpcId);

            
                DataTable tab = this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
                if (tab != null && tab.Rows.Count > 0)
                {
                    info = this._dbHelper.DT2EntityList<Model_zk_zydz_xpcxx>(tab)[0];
                    return info;
                }
                return info;
         
        }
        /// <summary>
        /// 查询志愿信息
        /// </summary>
        /// <param name="zydm">小批次代码。</param>
        public Model_zk_zydz_zyxx Select_Zy(string zydm)
        {
            Model_zk_zydz_zyxx info = new Model_zk_zydz_zyxx();

            string sql = "select zyDm,zyMc,zyXsmc,zySl,sfZyFc,sfqy,zyBz,sfxxfc from zk_zydz_zyxx where zyId =@zyId";
            List<SqlParameter> lisP = new List<SqlParameter>();
            SqlParameter ZyId = new SqlParameter("@zyId", SqlDbType.VarChar);
            ZyId.Value = zydm;
            lisP.Add(ZyId);



            DataTable tab = this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
            if (tab != null && tab.Rows.Count > 0)
            {
                info = this._dbHelper.DT2EntityList<Model_zk_zydz_zyxx>(tab)[0];
                return info;
            }
            return info;
        }
        #endregion

        #region 根据条件查询所有内容操作。
        /// <summary>
        /// 查询志愿定制中的县区信息。
        /// </summary>
        public DataTable Select_All_ZydzXq()
        {
          
                string sql = "select xqdm,xqmc from zk_zydz_xq";

                  return this._dbHelper.selectTab(sql,   ref error, ref bReturn);
        }
        /// <summary>
        /// 查询大批次信息s。
        /// </summary>
        /// <param name="xqdm">县区代码。</param>
        public DataTable Select_All_Dpc(string xqdm)
        {
        
                string sql = "select dpcId,dpcDm,dpcMc,dpcXsMc,xpcSl,sfqy,dpcBz from zk_zydz_dpcxx where xqdm =@xqdm";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter Xqdm = new SqlParameter("@xqdm", SqlDbType.VarChar);
                Xqdm.Value = xqdm;
                lisP.Add(Xqdm);

                return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
         
        }
        /// <summary>
        /// 查询小批次
        /// </summary>
        /// <param name="dpcdm">大批次代码。</param>
        public DataTable Select_All_Xpc(string dpcdm)
        {
          
                string sql = "select xpcId,xpcDm,xpcMc,xpcXsMc,zySl,xxFc,sfqy,xpcBz from zk_zydz_xpcxx where dpcDm =@dpcDm";

                List<SqlParameter> lisP = new List<SqlParameter>();

                SqlParameter DpcDm = new SqlParameter("@dpcDm", SqlDbType.VarChar);

                DpcDm.Value = dpcdm;

                lisP.Add(DpcDm);
                return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
          
        }
        /// <summary>
        /// 查询志愿信息
        /// </summary>
        /// <param name="xpcdm">小批次代码。</param>
        public DataTable Select_All_Zy(string xpcdm)
        {
            
                string sql = "select zyId,zyDm,zyMc,zyXsmc,zySl,sfZyFc,sfqy,zyBz from zk_zydz_zyxx where xpcDm =@xpcDm";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter XpcDm = new SqlParameter("@xpcDm", SqlDbType.VarChar);
                XpcDm.Value = xpcdm;
                lisP.Add(XpcDm);


                return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
            
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
        /// <param name="xqdm">县区的标识Id.</param>
        /// <returns></returns>
        public int selectZyDzXq(string xqdm)
        {
            try
            {
                string sql = "select count(1) from zk_zydz_xq where xqdm=@xqdm";

                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter Xqdm = new SqlParameter("@xqdm", SqlDbType.VarChar);
                Xqdm.Value = xqdm;
                lisP.Add(Xqdm);

                this._dbHelper.BeginTran();
                return int.Parse(this._dbHelper.ExecuteScalar_Tran(sql, lisP).ToString());
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
        /// <param name="dpcId">大批次的标识Id.</param>
        /// <returns></returns>
        public int selectZyDzDpc(string dpcId)
        {
           
                string sql = "select count(1) from zk_zydz_dpcxx where dpcId =@dpcId";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter DpcId = new SqlParameter("@dpcId", SqlDbType.VarChar);
                DpcId.Value = dpcId;
                lisP.Add(DpcId);
                return int.Parse(this._dbHelper.ExecuteScalar(sql, lisP, ref error, ref bReturn).ToString());
      
           
        }
        /// <summary>
        /// 查询当前小批次代码是否已存在。
        /// </summary>
        /// <param name="xpcId">小批次的标识Id.</param>
        /// <returns></returns>
        public int selectZyDzXpc(string xpcId)
        {
          
                string sql = "select count(1) from zk_zydz_xpcxx where xpcId =@xpcId";

                List<SqlParameter> lisP = new List<SqlParameter>();

                SqlParameter XpcId = new SqlParameter("@xpcId", SqlDbType.VarChar);

                XpcId.Value = xpcId;
                lisP.Add(XpcId);

                return int.Parse(this._dbHelper.ExecuteScalar(sql, lisP, ref error, ref bReturn).ToString());
        }
        /// <summary>
        /// 查询当前志愿代码是否已存在。
        /// </summary>
        /// <param name="zyId">志愿的标识Id.</param>
        /// <returns></returns>
        public int selectZyDzZy(string zyId)
        {
          
                string sql = "select count(1) from zk_zydz_zyxx where zyId =@zyId";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter ZyId = new SqlParameter("@zyId", SqlDbType.VarChar);
                ZyId.Value = zyId;
                lisP.Add(ZyId);

               
                return int.Parse(this._dbHelper.ExecuteScalar(sql, lisP, ref error, ref bReturn).ToString());
           
        }
        #endregion

        /// <summary>
        /// 查询标示 服从
        /// </summary>
        /// <param name="dpcId"></param>
        /// <returns></returns>
        public DataTable Select_kjbs_fc(string xpcdm, string ksh)
        { 
                string sql = " select * from zk_kszyxx where kjbs like '" + xpcdm + "%' and ksh=@ksh ";
                List<SqlParameter> lisP = new List<SqlParameter>() { 
                 
                   new SqlParameter("@ksh", ksh)
               };
            return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
        }

        /// <summary>
        /// 查询标示 服从 zzsb
        /// </summary>
        /// <param name="dpcId"></param>
        /// <returns></returns>
        public DataTable Select_kjbs_fc_zzsb(string xpcdm, string ksh)
        {
           
                string sql = " select * from zk_zzsbxx where kjbs like '" + xpcdm + "%' and ksh=@ksh ";
                List<SqlParameter> lisP = new List<SqlParameter>() { 
                 
                   new SqlParameter("@ksh", ksh)
               };
                return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
        }
        /// <summary>
        /// 查询志愿订制的县区(zk_zydz_xq) 返回县区代码
        /// </summary>
        public string Select_zy_xqdm(string xqdm)
        {

            string sql = "";
            sql = "select  * from zk_zydz_xq ";
            DataTable dt = this._dbHelper.selectTab(sql, ref error, ref bReturn);
            if (dt.Rows.Count == 1)
            {
                if (dt.Rows[0]["xqdm"].ToString() == xqdm.Substring(0, 1) + "00")
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



        /// <summary>
        /// 查询志愿数量
        /// </summary>
        public DataTable Select_zy_Num(string xqdm)
        {
           
                string sql = "";
                sql = "select  a.stime,a.etime, c.sfxxfc, c.zyId, c.zySl as czySl,c.sfZyFc as csfZyFc,c.sfqy as csfqy,b.xpcMc,c.zyDm,b.pcDm,c.zyMc,xpcId from zk_zydz_dpcxx a,zk_zydz_xpcxx b,zk_zydz_zyxx c where a.dpcId=b.dpcDm and b.xpcId=c.xpcDm and a.sfqy=1 and b.sfqy=1 and c.sfqy=1 and a.xqdm=@xqdm  order by c.zySl desc";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter Xqdm = new SqlParameter("@xqdm", SqlDbType.VarChar);
                Xqdm.Value = xqdm;
                lisP.Add(Xqdm);
                return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
        }

        /// <summary>
        /// 查询大批次信息s。
        /// </summary>
        /// <param name="xqdm">县区代码。</param>
        public DataTable Select_All_DpcIsPass(string xqdm)
        {
       
                string sql = "select dpcId,dpcDm,dpcMc,dpcXsMc,xpcSl,sfqy,dpcBz,stime,etime from zk_zydz_dpcxx where xqdm =@xqdm and sfqy=1";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter Xqdm = new SqlParameter("@xqdm", SqlDbType.VarChar);
                Xqdm.Value = xqdm;
                lisP.Add(Xqdm);

                return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
        }

        /// <summary>
        /// 查询大批次信息s。
        /// </summary>
        /// <param name="xqdm">县区代码。</param>
        public DataTable Select_All_DpcIsPass_2(string xqdm, string dpcDm)
        {

            string sql = "select dpcId,dpcDm,dpcMc,dpcXsMc,xpcSl,sfqy,dpcBz,stime,etime from zk_zydz_dpcxx where xqdm =@xqdm and sfqy=1 and dpcDm=@dpcDm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",xqdm),new SqlParameter("@dpcDm",dpcDm)};

            return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
        }


        /// <summary>
        /// 查询小批次
        /// </summary>
        /// <param name="dpcdm">大批次代码。</param>
        public DataTable Select_All_XpcIsPass(string dpcdm)
        {
          
                string sql = "select xpcId,xpcDm,xpcMc,xpcXsMc,zySl,sfqy,xpcBz,pcDm from zk_zydz_xpcxx where dpcdm =@dpcdm and sfqy=1";

                List<SqlParameter> lisP = new List<SqlParameter>();

                SqlParameter DpDm = new SqlParameter("@dpcdm", SqlDbType.VarChar);

                DpDm.Value = dpcdm;
                lisP.Add(DpDm);
          return this._dbHelper.selectTab(sql, lisP,ref error,ref bReturn);
        }

        /// <summary>
        /// 查询志愿信息
        /// </summary>
        /// <param name="xpcdm">小批次代码。</param>

        public DataTable Select_All_ZyIsPass(string xpcdm)
        {
          
                string sql = "select zyId,zyDm,zyMc,zyXsmc,zySl,sfZyFc,sfqy,zyBz,sfxxfc from zk_zydz_zyxx where xpcDm =@xpcDm and sfqy=1 order by CONVERT(int, zydm),zySl desc";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter XpcDm = new SqlParameter("@xpcDm", SqlDbType.VarChar);
                XpcDm.Value = xpcdm;
                lisP.Add(XpcDm);


              return this._dbHelper.selectTab(sql, lisP,ref error,ref bReturn);
        }


        /// <summary>
        /// 查询大批次下所有的
        /// </summary>
        public DataTable Select_zy_ALLNum(int dpcId)
        {
        
                string sql = "select * from zk_zydz_dpcxx a,zk_zydz_xpcxx b,zk_zydz_zyxx c where a.dpcId=b.dpcDm and b.xpcId=c.xpcDm and a.sfqy=1 and b.sfqy=1 and c.sfqy=1 and a.dpcId=@dpcId";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter XpcDm = new SqlParameter("@dpcId", SqlDbType.Int);
                XpcDm.Value = dpcId;
                lisP.Add(XpcDm);
                return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
       
        }
        /// <summary>
        /// 查询小批次下面专业总数信息
        /// </summary>
        /// <param name="xpcdm">小批次代码。</param>
        public DataTable Select_All_ZyIsPassALLNum(string xpcdm)
        {
        
                string sql = "select sum(zySl) as sumNum  from zk_zydz_zyxx where xpcDm =@xpcDm and sfqy=1 order by zySl desc";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter XpcDm = new SqlParameter("@xpcDm", SqlDbType.VarChar);
                XpcDm.Value = xpcdm;
                lisP.Add(XpcDm);


              
                return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
         
        }

        /// <summary>
        /// 查询小批次计划学校信息
        /// </summary>
        /// <param name="dpcId"></param>
        /// <returns></returns>
        public DataTable Select_zy_XX(string pcdm, string xqdm,string where)
        {
         
                string sql = "select distinct zsxxmc , xxdm from View_zk_zsjh where  pcdm=@pcdm and   jhs>0 and xqdm ='" + xqdm + "' and " + where + "  order by xxdm asc  ";
                List<SqlParameter> lisP = new List<SqlParameter>() { 
                   new SqlParameter("@pcdm", pcdm)
               };
           
               return this._dbHelper.selectTab(sql, lisP,ref error,ref bReturn);
        
        }
        /// <summary>
        /// 查询小批次计划学校信息  自主申报
        /// </summary>
        /// <param name="dpcId"></param>
        /// <returns></returns>
        public DataTable Select_zy_XX_zzsb(string pcdm, string xqdm, string where)
        {
         
                string sql = " select * from zk_xxsyjh where pcdm=@pcdm";
                //string sql = "select a.xxdm,a.zsxxmc,ISNULL( b.jhs,0)-isnull(c.lqnum,0) as syjh  from (  select distinct zsxxmc , xxdm from View_zk_zsjh where  pcdm=@pcdm and   xqdm in(" + xqdm + ") and " + where + "  ) a left join (select jhs,xxdm from zs_xx_tj where pcdm=@pcdm) b on a.xxdm=b.xxdm " +
                //    "  left join ( select  lqxx,COUNT(lqzy) as lqnum  from zk_lqk where    td_zt=5 and pcdm=@pcdm group by lqxx) c on a.xxdm=c.lqxx" +
                //    " where (ISNULL( b.jhs,0)-isnull(c.lqnum,0))>0 order by xxdm asc  ";
                List<SqlParameter> lisP = new List<SqlParameter>() { 
                   new SqlParameter("@pcdm", pcdm)
               };
   
                return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
      
        }
               
        /// <summary>
        /// 查询小批次计划学校专业信息
        /// </summary>
        /// <param name="dpcId"></param>
        /// <returns></returns>
        public DataTable Select_zy_XXZY(string pcdm, string xqdm, string xxdm,string where)
        {
          
              string sql = "  select * from zk_xxzysyjh where pcdm=@pcdm and xxdm=@xxdm ";
//                string sql = "select a.*, (a.jhs - ISNULL(b.lqnum,0)) as syjh  from View_zsjh a  left join ( select  lqxx,lqzy,COUNT(lqzy)  as lqnum,td_zt  from zk_lqk where   td_zt=5 group by lqzy,lqxx,td_zt) b on a.xxdm=b.lqxx and a.zydm=b.lqzy " +
//" where  (a.jhs - ISNULL(b.lqnum,0))>0 and pcdm=@pcdm and  xqdm in(" + xqdm + ")  and  xxdm=@xxdm and " + where;
                List<SqlParameter> lisP = new List<SqlParameter>() { 
                   new SqlParameter("@pcdm", pcdm), new SqlParameter("@xxdm", xxdm)
               };
            
                return this._dbHelper.selectTab(sql, lisP,ref error,ref bReturn);
         
        }
        /// <summary>
        /// 查询小批次计划学校专业信息
        /// </summary>
        /// <param name="dpcId"></param>
        /// <returns></returns>
        public DataTable Select_zy_XXZY_zy(string pcdm, string xqdm, string xxdm, string where)
        {

            string sql = "select zydm,zymc  from View_zsjh where  pcdm=@pcdm  and xxdm=@xxdm and   xqdm in(" + xqdm + ") and " + where + " order by zydm asc  ";
            //                string sql = "select a.*, (a.jhs - ISNULL(b.lqnum,0)) as syjh  from View_zsjh a  left join ( select  lqxx,lqzy,COUNT(lqzy)  as lqnum,td_zt  from zk_lqk where   td_zt=5 group by lqzy,lqxx,td_zt) b on a.xxdm=b.lqxx and a.zydm=b.lqzy " +
            //" where  (a.jhs - ISNULL(b.lqnum,0))>0 and pcdm=@pcdm and  xqdm in(" + xqdm + ")  and  xxdm=@xxdm and " + where;
            List<SqlParameter> lisP = new List<SqlParameter>() { 
                   new SqlParameter("@pcdm", pcdm), new SqlParameter("@xxdm", xxdm)
               };

            return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);

        }
        /// <summary>
        /// 查询考生自愿信息
        /// </summary>
        /// <param name="dpcId"></param>
        /// <returns></returns>
        public DataTable Select_zy_kszyxx(string ksh)
        {
           
                string sql = " select * from zk_kszyxx where ksh=@ksh ";
                List<SqlParameter> lisP = new List<SqlParameter>() { 
                   new SqlParameter("@ksh", ksh)
               };
          
                return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
      
           
        }
        /// <summary>
        /// 根据报名号zk_kszyxx删除
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
        /// <summary>
        /// 根据报名号zk_zzsbxx删除
        /// </summary>
        /// <param name="xqdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool delete_zk_zzsbxx(string ksh)
        {
            string sql = "delete  zk_zzsbxx where ksh=@ksh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@ksh",ksh)};
            this._dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        public DataTable Select_zy(string xqdm)
        {
           
                string sql = "";
                sql = "select c.zyId, c.zySl as czySl,c.sfZyFc as csfZyFc,c.sfqy as csfqy,b.xpcMc,c.zyDm,b.pcDm,c.zyMc,xpcId,dpcId from zk_zydz_dpcxx a,zk_zydz_xpcxx b,zk_zydz_zyxx c where a.dpcId=b.dpcDm and b.xpcId=c.xpcDm and a.sfqy=1 and b.sfqy=1 and c.sfqy=1 and a.xqdm=@xqdm  order by a.dpcId";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter Xqdm = new SqlParameter("@xqdm", SqlDbType.VarChar);
                Xqdm.Value = xqdm;
                lisP.Add(Xqdm);

                return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
        
        }

        /// <summary>
        /// 自主申报填报记录
        /// </summary>
        /// <param name="xqdm"></param>
        /// <returns></returns>
        public DataTable Select_Viewzzsb(string ksh)
        {
         
                string sql = "";
                sql = "  select * from View_zzsb_zydzxx where ksh=@ksh";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter Xqdm = new SqlParameter("@ksh", SqlDbType.VarChar);
                Xqdm.Value = ksh;
                lisP.Add(Xqdm);


                return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
       
        }
        /// <summary>
        /// 个人志愿填报记录
        /// </summary>
        /// <param name="xqdm"></param>
        /// <returns></returns>
        public DataTable Select_Viewzy(string ksh)
        {
            
                string sql = "";
                sql = "  select * from View_zy_zydzxx where ksh=@ksh";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter Xqdm = new SqlParameter("@ksh", SqlDbType.VarChar);
                Xqdm.Value = ksh;
                lisP.Add(Xqdm);

                return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
        }

        /// <summary>
        /// 自主申报填报记录联合查询
        /// </summary>
        /// <param name="xqdm"></param>
        /// <returns></returns>
        public DataTable Select_Viewzzsbxx(string ksh, string xqdm)
        {
             
                string sql = "";
                sql = "  select * from View_zzsb_xx where ksh=@ksh and xqdm=@xqdm";
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@ksh", ksh), new SqlParameter("@xqdm", xqdm) };
              
                return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
        }
        /// <summary>
        /// 个人志愿填报记录联合查询
        /// </summary>
        /// <param name="xqdm"></param>
        /// <returns></returns>
        public DataTable Select_Viewzyxx(string ksh, string xqdm)
        {
          
                string sql = "";
                sql = "  select * from View_zy_xx where ksh=@ksh and xqdm=@xqdm";
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@ksh", ksh), new SqlParameter("@xqdm", xqdm) };
                return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
        }


        /// <summary>
        /// 根据县区统计
        /// </summary>
        /// <param name="xqdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable Select_xqtj(string pcdm)
        {

            string sql = "exec proc_zy_xqtj @pcdm";
            List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@pcdm", pcdm)  };

            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            
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
        public DataTable Select_xxtj(string xqdm, string pcdm)
        {

            string sql = "exec proc_zy_xxtj @xqdm,@pcdm";
            List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@xqdm", xqdm), new SqlParameter("@pcdm", pcdm) };

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
        /// <summary>
        /// 查询标示  
        /// </summary>
        /// <param name="dpcId"></param>
        /// <returns></returns>
        public DataTable Select_kjbs(string kjbs, string ksh)
        {
           
                string sql = " select * from View_zy_zydzxx where kjbs=@kjbs and ksh=@ksh ";
                List<SqlParameter> lisP = new List<SqlParameter>() { 
                 
                   new SqlParameter("@ksh", ksh),
                    new SqlParameter("@kjbs", kjbs)
               };
                return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
        }
        /// <summary>
        /// 查询标示  自主申报
        /// </summary>
        /// <param name="dpcId"></param>
        /// <returns></returns>
        public DataTable Select_kjbs_zzsb(string kjbs, string ksh)
        {
          
                string sql = " select * from View_zzsb_zydzxx where kjbs=@kjbs and ksh=@ksh ";
                List<SqlParameter> lisP = new List<SqlParameter>() { 
                 
                   new SqlParameter("@ksh", ksh),
                    new SqlParameter("@kjbs", kjbs)
               };
                return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
        }
        /// <summary>
        /// 查出需要打印的报名号
        /// </summary>
        /// <param name="xqdm"></param>
        /// <returns></returns>
        public DataTable Select_Viewksh(string where)
        {
           
                string sql = "";
                sql = "select ksh from View_ksxxgl where " + where;
                List<SqlParameter> lisP = new List<SqlParameter>();

                return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
        }
        /// <summary>
        /// 个人志愿填报记录打印
        /// </summary>
        /// <param name="xqdm"></param>
        /// <returns></returns>
        public DataTable Select_Viewdy(string ksh)
        {
           
                string sql = "";
                sql = " select a.*,b.bmdmc,b.xm,b.kaocimc from View_zy_zydzxx a left join View_ksxxNew b on a.ksh=b.ksh where a.ksh =@ksh order by a.ksh,a.pcdm,a.zysx";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter Xqdm = new SqlParameter("@ksh", SqlDbType.VarChar);
                Xqdm.Value = ksh;
                lisP.Add(Xqdm);
                return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
        }
        /// <summary>
        /// 修改志愿确认状态.
        /// </summary>
        /// <param name="ksh">报名号</param>
        /// <returns></returns>
        public bool zk_ksxxglZyksqr(string ksh, int type)
        {

            string error = "";
            bool bReturn = false;
            string sql = "update zk_ksxxgl set zyksqr=@type,zyksqrsj=getdate() where ksh=@ksh ";
            List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@ksh", ksh), new SqlParameter("@type", type) };

            int i = _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (i == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改自主申报确认状态.
        /// </summary>
        /// <param name="ksh">报名号</param>
        /// <returns></returns>
        public bool zk_ksxxglsbksqr(string ksh, int type)
        {

            string error = "";
            bool bReturn = false;
            string sql = "update zk_zzsbtzxx set sbksqr=@type,sbksqrsj=getdate() where ksh=@ksh ";
            List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@ksh", ksh), new SqlParameter("@type", type) };

            int i = _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (i == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改考生志愿信息打印状态。
        /// </summary>
        /// <returns></returns>
        public bool updatezk_ksxxglzy(string where, int type, int UserType)
        {
            if (UserType == 1 || UserType == 2 || UserType == 3)
            {
                return true;
            }
            else
            {
                string sql = "update zk_ksxxgl set zyxxdy=" + type + ", zyxxdysj=GETDATE()  where " + where;
                _dbHelper.ExecuteNonQuery(sql, ref error, ref bReturn);
                if (bReturn)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 修改考生志愿信息学校状态。
        /// </summary>
        /// <returns></returns>
        public bool updatezk_ksxxglzyxxqr(string where, int type, int UserType)
        {
            if ( UserType == 2 || UserType == 3)
            {
                return true;
            }
            else
            {
                string sql = "update zk_ksxxgl set  zyxxqr=" + type + ",zyxxqrsj=GETDATE() where " + where;
                _dbHelper.ExecuteNonQuery(sql, ref error, ref bReturn);
                if (bReturn)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 自主申报填报状态表
        /// </summary>
        /// <param name="xqdm"></param>
        /// <returns></returns>
        public DataTable Select_zk_zzsbtzxx(string ksh)
        {
          
                string sql = "";
                sql = "  select * from zk_zzsbtzxx where ksh=@ksh";
                List<SqlParameter> lisP = new List<SqlParameter>();
                SqlParameter Xqdm = new SqlParameter("@ksh", SqlDbType.VarChar);
                Xqdm.Value = ksh;
                lisP.Add(Xqdm);
                return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
        }

        /// <summary>
        /// 第一次登录.插入自主申报确认状态表.
        /// </summary>
        /// <param name="ksh">报名号</param>
        /// <returns></returns>
        public bool Insert_zzsbtzxx(string ksh,string xm)
        {

            string error = "";
            bool bReturn = false;
            string sql = "insert into  zk_zzsbtzxx (ksh,xm,sbksqr,sbksqrsj) values (@ksh,@xm,0,getdate()) ";
            List<SqlParameter> lisP = new List<SqlParameter>() { 
                new SqlParameter("@ksh", ksh), new SqlParameter("@xm", xm) };

            int i = _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (i == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 查出考生志愿
        /// </summary>
        /// <param name="xqdm"></param>
        /// <returns></returns>
        public DataTable Select_zzsbxx(string ksh)
        {
          
                string sql = "";
                sql = " select * from zk_zzsbxx where xxdm<>'' and ksh=@ksh" ;
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@ksh", ksh) };


                return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
        }

        /// <summary>
        /// 修改lqk信息。
        /// </summary>
        /// <returns></returns>
        public bool update_lqk(string xxdm, string pcdm, string zysx,string ksh)
        {

            string sql = "update zk_lqk set  xqdm='500',lqxx='" + xxdm + "',pcdm='" + pcdm + "',zydm='00',sf_zbs=0,sf_tfgj=0,td_zt=2,zysx=" + zysx + ",xx_zt=2,types=1 where ksh='" + ksh + "' and isnull(td_zt,0)=0";
            _dbHelper.ExecuteNonQuery(sql, ref error, ref bReturn);
            if (bReturn)
            {
                return true;
            }
            return false;
        
        }

        /// <summary>
        /// 查询个条数据：1、男儿幼儿师范；2、师范配额生；3、统招； 
        /// </summary> 
        public bool Disp(string ksh, int type)
        {
            string sql = "select * from zk_hege where ksh=@ksh and type=@type";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@ksh",ksh),new SqlParameter("@type",type)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 查出考生排名情况
        /// </summary>
        /// <param name="xqdm"></param>
        /// <returns></returns>
        public DataTable SelectKshPm(string ksh,string pcdm )
        {

            string sql = "";
            string xqdm = ksh.Substring(0, 3);
            if (pcdm != "0")
            {
                //DataTable sftab = _dbHelper.selectTab("SELECT xqdm FROM zk_zsjh_xq WHERE xxdm='" + xxdm + "' and xqdm LIKE ('%" + xqdm + "%')", ref error, ref bReturn);
                //sql = @"SELECT a.*, rank() OVER (partition BY a.pcdm, a.xxdm   ORDER BY  zf DESC) AS 'pm'     FROM View_pm4  a WHERE LEFT(a.ksh, 3) IN (" + sftab.Rows[0]["xqdm"] + ")";          
                ////if (Convert.ToBoolean(sftab.Rows[0]["xqdm"]))
                ////{
                ////    sql = @"     select * from View_pm1 where   ksh=@ksh  and  xxdm<>''  and  pcdm=@pcdm";
                ////}
                ////else
                ////{
                ////    sql = @"    select * from View_pm2 where   ksh=@ksh  and  xxdm<>''  and  pcdm=@pcdm";
                ////}
            }
            else
            {
                sql = @"    select * from View_pm2 where   ksh=@ksh  and  xxdm<>''  and  pcdm=@pcdm";
            }
            if (pcdm != "0")
            {
                sql = sql + @"  union all
 select pcdm,ksh,bmddm,bmdmc,xpcmc,zf,lrsj,maxnum,minnum,num,td_zt,lqtime,pm from View_pm3 
 where  ksh=@ksh  and  xxdm<>'' and  pcdm=@pcdm";
            }
            List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@ksh", ksh), new SqlParameter("@pcdm", pcdm + "1") };
            return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
        }

        public DataTable SelectKshPm_xx(string ksh, string pcdm, string xxdm, string bmddm, string xqdm)
        {
       
            string sql = "";
           
            //string xqdm = ksh.Substring(0, 3);
            string pm = "";
            string xqdmall = "";
            List<string> listxx = new List<string>() { "001", "002", "003", "004", "005", "006", "007", "008", "009", "010", "011", "012", "013" };//有配额生的学校   
            if (pcdm != "0")
            {
                DataTable sftab = _dbHelper.selectTab("SELECT xqdm,jhs FROM zk_zsjh_xq WHERE xxdm='" + xxdm + "' and xqdm LIKE ('%" + xqdm + "%')", ref error, ref bReturn);
                xqdmall = sftab.Rows[0]["xqdm"].ToString();
                pm = sftab.Rows[0]["jhs"].ToString();
                sql = " EXEC [proc_pm2] @ksh,@xxdm,@pcdm,@xqdmwhere,@jhs";
                if (listxx.Contains(xxdm))
                {
                    DataTable sftab2 = _dbHelper.selectTab("select top 1 ksh from zk_lqk where lqxx='" + xxdm + "' and pcdm='11'", ref error, ref bReturn);
                    if (sftab2.Rows.Count == 0) //表示没有录取过的学校
                    {
                        sql = " EXEC [proc_pm1] @pm,@ksh,@xxdm,@pcdm,@xqdmwhere,@jhs,@bmddm";
                    }
                }
            }
            else
            {
                if (xxdm == "045")
                {
                    sql = "  EXEC [proc_pm045] @ksh,@xxdm,@pcdm";
                }
                else
                    sql = "  EXEC [proc_pm3] @ksh,@xxdm,@pcdm";
            }
        
            List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@bmddm", bmddm), new SqlParameter("@jhs", pm), new SqlParameter("@xqdmwhere", xqdmall), new SqlParameter("@ksh", ksh), new SqlParameter("@pcdm", pcdm + "1"), new SqlParameter("@xxdm", xxdm), new SqlParameter("@pm", pm) };
         
            return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
        
        }
        public DataTable SelectKshPm_xx_2(string ksh, string pcdm, string xxdm, string bmddm, string xqdm, ref int pzt)
        {

            string sql = "";

            //string xqdm = ksh.Substring(0, 3);
            string pm = "";
            string xqdmall = "";
            List<string> listxx = new List<string>() { "001", "002", "003", "004", "005", "006", "007", "008", "009", "010", "011", "012", "013"};//有配额生的学校   
            if (pcdm != "0")
            {
                DataTable sftab = _dbHelper.selectTab("SELECT xqdm,jhs FROM zk_zsjh_xq WHERE xxdm='" + xxdm + "' and xqdm LIKE ('%" + xqdm + "%')", ref error, ref bReturn);
                xqdmall = sftab.Rows[0]["xqdm"].ToString();
                pm = sftab.Rows[0]["jhs"].ToString();
                sql = " EXEC [proc_pm2] @ksh,@xxdm,@pcdm,@xqdmwhere,@jhs";
                if (listxx.Contains(xxdm))
                {
                    List<string> pclist = new List<string>() { "11", "21", "31", "41" };//第二批次
                    List<string> pclist2 = new List<string>() { "51", "61", "71", "81", "91" };//第二批次
                    string pc = "";
                    if (pclist.Contains(pcdm + "1"))
                        pc = "11";
                    if (pclist2.Contains(pcdm + "1"))
                        pc = "21";

                    DataTable sftab2 = _dbHelper.selectTab("select top 1 ksh from zk_lqk where lqxx='" + xxdm + "' and sfpc='" + pc + "' and td_zt=5", ref error, ref bReturn);
                    if (sftab2.Rows.Count == 0) //表示没有录取过的学校
                    {
                        if (pc == "11")
                        {
                            sql = " EXEC [proc_pm11] @pm,@ksh,@xxdm,@pcdm,@xqdmwhere,@jhs,@bmddm";
                        }
                        else
                        {
                            sql = " EXEC [proc_pm1] @pm,@ksh,@xxdm,@pcdm,@xqdmwhere,@jhs,@bmddm";
                        }
                      
                    }
                    else
                    {
                        sql = " EXEC [proc_pm4] @ksh,@xxdm,@pcdm,@xqdmwhere,@jhs";
                        pzt = 1;
                    }
                }
            }
            else
            {
                sql = "  EXEC [proc_pm3] @ksh,@xxdm,@pcdm";
            }

            List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@bmddm", bmddm), new SqlParameter("@jhs", pm), new SqlParameter("@xqdmwhere", xqdmall), new SqlParameter("@ksh", ksh), new SqlParameter("@pcdm", pcdm + "1"), new SqlParameter("@xxdm", xxdm), new SqlParameter("@pm", pm) };

            return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);

        }
        /// <summary>
        /// 根据报名号zk_kszyxx删除
        /// </summary>
        /// <param name="xqdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool delete_zk_kszyxx(List<Model_zk_kszyxx> Listitem)
        {
            string sql = "";
            for (int i = 0; i < Listitem.Count; i++)
            {
                sql = "delete  zk_kszyxx where ksh=@ksh and pcdm=@pcdm and zysx=@zysx;";
                List<SqlParameter> lisP = new List<SqlParameter>(){
 		    	 new SqlParameter("@ksh",Listitem[i].Ksh),new SqlParameter("@pcdm",Listitem[i].Pcdm),new SqlParameter("@zysx",Listitem[i].Zysx)};
                this._dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            }
           
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 查询小批次计划学校专业信息
        /// </summary>
        /// <param name="dpcId"></param>
        /// <returns></returns>
        public DataTable Select_zy_kszyxx_order(string ksh)
        {

            string sql = " select pcdm,xxdm from zk_kszyxx where ksh=@ksh  order by pcdm desc";
            List<SqlParameter> lisP = new List<SqlParameter>() { 
                   new SqlParameter("@ksh", ksh)
               };

            return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);


        }


        /// <summary>
        /// 查询是否满计划学校
        /// </summary>
        /// <param name="dpcId"></param>
        /// <returns></returns>
        public DataTable Select_zy_XX_syjh( string xqdm, string where)
        {

            string sql = "select * from (SELECT a.xxdm,b.zsxxmc FROM zk_zsjh_xq a  LEFT JOIN zk_zsxxdm b ON a.xxdm=b.zsxxdm   WHERE xqdm LIKE ('%" + xqdm + "%') AND jhs>0  and " + where + " ) a order by xxdm asc  ";
            return this._dbHelper.selectTab(sql, ref error, ref bReturn);

        }
        /// <summary>
        /// 查询是否满计划学校
        /// </summary>
        /// <param name="dpcId"></param>
        /// <returns></returns>
        public DataTable Select_syjh(string xxdm, string xqdm)
        {

            DataTable sftab = _dbHelper.selectTab("SELECT xqdm,jhs FROM zk_zsjh_xq WHERE xxdm='" + xxdm + "' and xqdm LIKE ('%" + xqdm + "%')", ref error, ref bReturn);

            return sftab;

        }
      
        public bool delete_zk_kszyxx( Model_zk_kszyxx  Listitem,string ip)
        {
            try
            {
                string sql = "";
                this._dbHelper.BeginTran();
                sql = "delete  zk_kszyxx where ksh=@ksh and pcdm=@pcdm and zysx=@zysx;";
                List<SqlParameter> lisP = new List<SqlParameter>(){
 		    	 new SqlParameter("@ksh",Listitem.Ksh),new SqlParameter("@pcdm",Listitem.Pcdm),new SqlParameter("@zysx",Listitem.Zysx)};
                _dbHelper.execSql_Tran(sql, lisP);
                string sqlgj = " insert into  zk_kslqgj  (ksh,type,times,ip,xxdm,pcdm) values ('" + Listitem.Ksh + "',2,GETDATE(),'" + ip + "','" + Listitem.Xxdm + "','" + Listitem.Pcdm + "')";
                _dbHelper.execSql_Tran(sqlgj);
                this._dbHelper.EndTran(true);
                return true;
            }
            catch (Exception)
            {
                this._dbHelper.EndTran(false);
            }
            return false;

        }
        /// <summary>
        /// 查询录取结果
        /// </summary>
        /// <param name="dpcId"></param>
        /// <returns></returns>
        public DataTable Select_lqjg(string ksh)
        {
            string sql = " SELECT lqxx,b.zsxxmc,a.sftzs,td_zt,a.lqtime,pcdm FROM zk_lqk a JOIN zk_zsxxdm b ON a.lqxx=b.zsxxdm WHERE a.td_zt=5 and a.ksh=@ksh";
            List<SqlParameter> lisP = new List<SqlParameter>() { 
                   new SqlParameter("@ksh", ksh)
               };

            return this._dbHelper.selectTab(sql, lisP, ref error, ref bReturn);


        }

        /// <summary>
        /// 查询是否满计划学校
        /// </summary>
        /// <param name="dpcId"></param>
        /// <returns></returns>
        public DataTable Select_zy_XX_syjh2(string pcdm, string xqdm, string bklb, string mzdm, string kslbdm, string jzfp, string fsx, string dj,string xj)
        {

            string sql = @"SELECT xxdm,b.zsxxmc from dbo.zk_zsjh_where a JOIN dbo.zk_zsxxdm b ON a.xxdm=b.zsxxdm  WHERE zt=1 AND  pcdm='" + pcdm + @"' 
            AND xqdm LIKE ('%" + xqdm + @"%') AND bklb LIKE ('%" + bklb + @"%')
  AND mzdm LIKE ('%" + mzdm + @"%')  AND kslbdm LIKE ('%" + kslbdm + @"%')  AND xj LIKE ('%" + xj + @"%')  AND jzfp LIKE ('%" + jzfp + @"%')  AND " + fsx + @">=cj 
  AND dj LIKE ('%" + dj + @"%') order by xxdm";
            return this._dbHelper.selectTab(sql, ref error, ref bReturn);

        }

       
        public DataTable Select_sel(string pc,string pcdm)
        {
            string sql = "exec proc_" + pc + " @pcdm";
            List<SqlParameter> lisP = new List<SqlParameter>() { 
                   new SqlParameter("@pcdm", pcdm)
               };
            return this._dbHelper.selectTab(sql,lisP, ref error, ref bReturn);
        }
        public DataTable Select_selxxmc(string xxdm)
        {
            string sql = "select * from zk_zsxxdm where zsxxdm=@xxdm";
            List<SqlParameter> lisP = new List<SqlParameter>() { 
                   new SqlParameter("@xxdm", xxdm)
               };
            return this._dbHelper.selectTab(sql,lisP, ref error, ref bReturn);
        }
    }
}
