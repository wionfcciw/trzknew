using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;
using System.Data.SqlClient;
using System.IO;
using LinqToExcel;
using LinqToExcel.Query;

namespace BLL
{
    public class BLL_zc_xx
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
        public DataTable selectksh(string lqxx)
        {
            try
            {
                string sql = "  SELECT  a.ksh,b.xm,c.cj, a.lqzy+ d.zymc as lqzy,c.types FROM  zk_zcxx a left join zk_ksxxgl b on a.ksh=b.ksh left join zk_lqk c on a.ksh=c.ksh left join zk_zyk d on a.lqxx=d.xxdm and a.lqzy=d.zydm where a.lqxx=@lqxx   and a.types=0";
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@lqxx", lqxx) };
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
                string sql = "update zk_lqk set td_zt=xx_zt,xq_zt=xx_zt   where lqxx=@lqxx and  pcdm=@pcdm and types=2";
                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@lqxx", lqxx), new SqlParameter("@pcdm", pcdm) };
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
        /// 修改lqk信息。
        /// </summary>
        /// <returns></returns>
        public bool update_lqk(string xxdm, string pcdm, string zysx, string ksh,string zydm)
        {

            string sql = "update zk_lqk set  xqdm='500',lqxx='" + xxdm + "',pcdm='" + pcdm + "',zydm='00',lqzy='" + zydm + "',sf_zbs=0,sf_tfgj=0,td_zt=2,zysx=" + zysx + ",xx_zt=4,types=2 where ksh='" + ksh + "' and isnull(td_zt,0)=0";
            _dbA.ExecuteNonQuery(sql, ref error, ref bReturn);
            if (bReturn)
            {
                return true;
            }
            return false;

        }

        /// <summary>
        /// Excel数据导入到SqlServer数据库方法
        /// </summary>
        /// <param name="excelFilePath">excel文件存放路径</param>
        /// <param name="importSqlCmd">需要用于导入数据的sql语句</param>
        /// <param name="importParams">用于导入数据的sql参数列表</param>
        /// <param name="isZL">是否增量导入</param>
        /// <returns></returns>
        public string ImportExcelData(string excelFilePath)
        {
            if (!File.Exists(excelFilePath))
                return "导入失败，原因是：输入的文件路径不正确或指定的文件不存在。";

            ExcelQueryFactory excelFile = new ExcelQueryFactory(excelFilePath);
            ExcelQueryable<Row> excel = excelFile.Worksheet(0);
            StringBuilder resultMsg = new StringBuilder();

           // string sqlCmd = "update zk_lqk set  xqdm='500',lqxx=@lqxx,pcdm=@pcdm,zydm='00',lqzy=@lqzy,sf_zbs=0,sf_tfgj=0,td_zt=2,zysx=1,xx_zt=4,types=2 where ksh=@ksh and isnull(td_zt,0)=0";
            string sqlCmd = "insert into zk_zcxx (ksh,lqxx,lqzy,types) values (@ksh,@lqxx,@lqzy,0); ";
            string checkSql = "select * from zk_lqk where ksh=@ksh; select * from  zk_zyk where xxdm=@lqxx and zydm=@lqzy;select * from zk_zcxx where ksh=@ksh;";
            
            bool bReturn = true;

            int i = 0;
            int lost = 0;
            int finish = 0;
            try
            {
                foreach (var element in excel)
                {
                    if (element.ColumnNames.Count() != 3)
                        return "导入失败，文件格式不是3列。";

                    string errMsg = "";
                    string baseStr = "";


                    //空数据则跳过
                    if (string.IsNullOrEmpty(element[0].ToString().Trim()))
                        continue;

                    List<SqlParameter> param = new List<SqlParameter> {
                                                new SqlParameter("@lqxx",element[1].ToString().Trim()),
                                                new SqlParameter("@lqzy",element[2].ToString().Trim()),
                                             
                                                new SqlParameter("@ksh",element[0].ToString().Trim())
                                              };
                    DataSet tmpDataSet = _dbA.selectDataSet(checkSql, param, ref errMsg, ref bReturn);

                    if (tmpDataSet.Tables[0].Rows.Count == 0)
                        errMsg += "第" + (i + 1) + "条数据有误，原因是：该报名号不存在：" + element[0].ToString().Trim() + "。<br />";
                    
                    if (element[1].ToString().Trim() != SincciLogin.Sessionstu().UserName)
                        errMsg += "第" + (i + 1) + "条数据有误，原因是：该招生学校代码有误：" + element[1].ToString().Trim() + "。<br />";
                    if (tmpDataSet.Tables[2].Rows.Count > 0)
                        errMsg += "第" + (i + 1) + "条数据有误，原因是：该报名号已注册：" + element[0].ToString().Trim() + "。<br />";

                    ////需要导入的考生状态
                    //if (Convert.ToInt32(tmpDataSet.Tables[0].Rows[0]["td_zt"]) != 0)
                    //    errMsg += "第" + (i + 1) + "条数据有误，原因是：该报名号状态已锁定：" + element[0].ToString().Trim() + "。<br />";

                    //需要学校是否有该专业
                    if (tmpDataSet.Tables[1].Rows.Count == 0)
                        errMsg += "第" + (i + 1) + "条数据有误，原因是：该专业不存在：" + element[2].ToString().Trim() + "。<br />";
                   
                    if (string.IsNullOrEmpty(errMsg))
                    {
                        _dbA.ExecuteNonQuery(sqlCmd, param, ref errMsg, ref bReturn);
                        if (!bReturn)
                        {
                            errMsg += "第" + (i + 1) + "行数据导入时发生错误.<br />";
                        }
                    }


                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        resultMsg.Append("" + errMsg + "<br />");
                        lost++;
                    }
                    else
                    {
                        resultMsg.Append(baseStr + "第" + (i + 1) + "行数据导入成功。<br />");
                        finish++;
                    }





                    resultMsg.Append("---------------------------------------------------<br /><br />");
                    i++;
                }
            }
            catch (Exception ex)
            {
                resultMsg.Append("第" + (i + 1) + "行数据导入时发生错误。<br />");
                _dbA.writeErrorInfo(ex.Message);
            }

            resultMsg.Append("共处理:" + i + "数据导入完毕。共成功导入：" + finish + "条数据。导入失败：" + lost + "条数据。<br />");

            File.Delete(excelFilePath);

            return resultMsg.ToString();

        }

        /// <summary>
        /// 根据考生是否注册
        /// </summary>
        /// <param name="xxdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable Select_zcxx(string ksh)
        {

            string sql = "select * from zk_zcxx where ksh=@ksh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@ksh",ksh)};
            DataTable dt = _dbA.selectTab(sql, lisP, ref   error, ref   bReturn);

            return dt;
        }
        /// <summary>
        /// 修改lqk信息状态。
        /// </summary>
        /// <returns></returns>
        public bool update_lqk(string ksh,int type)
        {

            string sql = "update zk_lqk set  types=" + type + " where ksh='" + ksh + "' ";
            _dbA.ExecuteNonQuery(sql, ref error, ref bReturn);
            if (bReturn)
            {
                return true;
            }
            return false;

        }
        /// <summary>
        /// 插入注册表
        /// </summary>
        /// <returns></returns>
        public bool insert_zc(string ksh, string xxdm, string zydm)
        {

            string sql = " insert into zk_zcxx (ksh,lqxx,lqzy,types) values (@ksh,@xxdm,@zydm,0)";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@ksh",ksh), new SqlParameter("@xxdm",xxdm), new SqlParameter("@zydm",zydm)};
            _dbA.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 删除注册表信息。
        /// </summary>
        /// <returns></returns>
        public bool del_zcxx(string ksh)
        {

            string sql = "delete zk_zcxx  where ksh in (" + ksh + ") ";
            _dbA.ExecuteNonQuery(sql, ref error, ref bReturn);
            if (bReturn)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///上传注册信息
        /// </summary>
        /// <returns></returns>
        public bool update_zcxx(string lqxx, int type)
        {

            //string sql = "update zk_lqk set  types=" + type + " where ksh in (select ksh from zk_zcxx  where lqxx='" + lqxx + "')";

            //考生轨迹
            string sqlgj = " insert into   zk_kslqgj (ksh,username,type,times)  (ksh,username,type,times)   select ksh,'" + SincciLogin.Sessionstu().UserName + "',4,getdate() from zk_zcxx   where lqxx='" + lqxx + "' and isnull(types,0)=0";
            _dbA.ExecuteNonQuery(sqlgj, ref error, ref bReturn);
            string sql = "update zk_zcxx set  types=" + type + " where lqxx='" + lqxx + "' and isnull(types,0)=0 ";
            _dbA.ExecuteNonQuery(sql, ref error, ref bReturn);

           
            if (bReturn)
            {
                return true;
            }
            return false;

        }
    }
}
