using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using LinqToExcel;
using LinqToExcel.Query;

namespace BLL
{
    /// <summary>
    /// 导出录取库考生信息。
    /// </summary>
    public class BLL_LQK_Ks_Xx
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
        /// 构造方法。
        /// </summary>
        public BLL_LQK_Ks_Xx() { }

        /// <summary>
        /// 导出录取库的数据。
        /// </summary>
        /// <param name="where">查询条件，条件请加where关键字。</param>
        public bool Import_lqk()
        {
            string sql = String.Format("select * from zk_xqdm  ");
            DataTable tab = this._dbA.selectTab(sql, ref this.error, ref this.bReturn);
            if (this.bReturn && tab != null)
            {
                DbfHelper export = new DbfHelper();
                export.TempletFile = "lqk_ks.dbf";
                export.FilePrefix = "ks_lqzt_";
                export.DataSource = tab;
                StringBuilder stb = new StringBuilder();
                for (int i = 0; i < tab.Columns.Count; i++)
                {
                    if (stb.Length > 0)
                    {
                        stb.Append(",");
                    }
                    stb.Append(tab.Columns[i].ColumnName);
                }
                export.Fields = stb.ToString();
                export.Export();
            }
            return true;
        }
        /// <summary>
        /// 查询当前学校的志愿批次信息。
        /// </summary>
        public DataTable selectPcdm(string xqdm,string where)
        {
            try
            {
                 string sql = " select  pcdm, xpcid,xpc_mc='{'+b.xqmc+'}'+'['+pcdm+']  '+xpcXsMc from zk_zydz_xpcxx a left join zk_xqdm b on LEFT( a.dpcdm,3)=b.xqdm where " + xqdm + "   ";//只做了这2个批次

                DataTable tab = _dbA.selectTab(sql, ref this.error, ref this.bReturn);
                return tab;
            }
            catch (Exception exe)
            {
            }
            return null;
        }
        /// <summary>
        /// 根据批次学校
        /// </summary>
        /// <param name="xxdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable Select_zk_zsxx(string pcdm,string where )
        {

            string sql = "select a.xxdm,b.zsxxdm+b.zsxxmc as zsxxmc from zk_lqjhk a left join zk_zsxxdm b on a.xxdm=b.zsxxdm where a.pcdm=@pcdm and " + where + "  group by xxdm,b.zsxxdm+b.zsxxmc order by xxdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@pcdm",pcdm)};
            DataTable tab = _dbA.selectTab(sql,lisP, ref this.error, ref this.bReturn);

            return tab;
        }

        /// <summary>
        /// 根据批次学校
        /// </summary>
        /// <param name="xxdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable Select_zk_zsxx_sel(string pcdm, string where)
        {

            string sql = "select a.xxdm,b.zsxxdm+b.zsxxmc as zsxxmc from zk_lqjhk a left join zk_zsxxdm b on a.xxdm=b.zsxxdm where a.pcdm=@pcdm and " + where + " and a.xxdm in (select xxdm from zk_zsjh_where where zt=1)  group by xxdm,b.zsxxdm+b.zsxxmc order by xxdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@pcdm",pcdm)};
            DataTable tab = _dbA.selectTab(sql, lisP, ref this.error, ref this.bReturn);

            return tab;
        }


        /// <summary>
        /// 加载考生信息
        /// </summary>
        public DataTable selectksh(string where)
        {
            try
            {
                string sql = "  select * from View_Lqxx where  " + where;
             
                DataTable tab = this._dbA.selectTab(sql,  ref error, ref bReturn);
                return tab;
            }
            catch (Exception exe)
            {
            }
            return null;
        }
        /// <summary>
        /// 执行分页存储过程，返回记录总数和当前页的数据。
        /// </summary>
        /// <param name="where">执行的条件</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="totalRecord">返回的记录总数</param>
        public DataTable ExecuteProcList(string where, int pageSize, int pageIndex, ref int totalRecord)
        {
            //string sql="select  from AppraisalPersonnel";

            //数据库表名
            string tabName = "View_Lqxx";
            //要查询的字段
            string reField = " * ";
            //排序字段
            string orderStr = " ksh";
            //排序标识（0、升序；1、降序）
            int orderType = 0;
            //执行成功与失败的标识（true、表示执行成功；false、表示执行失败）
            bool bFlag = false;
            decimal dec = 0;
            DataSet dst = _dbA.ExecuteProc2(tabName, reField, orderStr, "", where, pageSize, pageIndex, orderType, 0, ref dec, ref totalRecord, ref bFlag);
            if (bFlag)
            {
                if (dst.Tables.Count > 0)
                {
                    return dst.Tables[0];
                }
            }
            return null;
        }
        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="pcdm">ksh。 状态</param>
        public bool ksh_hs(string where,string where2)
        {
            try
            {

                string sql3 = "update zk_lqk set td_zt=0 ,lqxx='',lqzy='',pcdm='',zysx='',xx_zt=0,xq_zt=0,zydm='',xqdm='',types=0,jyzy='',td_pc='',daoru=0,sftzs=0 where " + where;

                string sql = " delete zk_kszyxx where " + where + "; update zk_ksxxgl set zyksqr=0 where " + where;

                _dbA.BeginTran();
                //考生轨迹
                string sqlgj = " insert into   zk_kslqgj (ksh,username,type,times,xxdm)    select ksh,'" + SincciLogin.Sessionstu().UserName + "',0,getdate(),lqxx from zk_lqk  where " + where;
                _dbA.execSql_Tran(sqlgj);

                int iCount3 = _dbA.execSql_Tran(sql3);
                int iCount = _dbA.execSql_Tran(sql);
                StringBuilder stb = new StringBuilder();

                if (iCount3 > 0 && iCount>0)
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
        /// 查询所有
        /// </summary>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable Select_zk_zsxxdm()
        {
            string sql = "select zsxxdm,zsxxmcc=zsxxdm+zsxxmc,zsxxmc from zk_zsxxdm order by zsxxdm ";
            DataTable dt = _dbA.selectTab(sql, ref   error, ref   bReturn);
            return dt;
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

            string sql = " select xxdm,zydm,zydm+zymc zymc from View_zk_zyk where xxdm=@xxdm order by zydm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xxdm",xxdm)};
            DataTable dt = _dbA.selectTab(sql, lisP, ref   error, ref   bReturn);

            return dt;
        }
        /// <summary>
        /// 考生直接录取
        /// </summary>
        /// <param name="pcdm">ksh。 状态</param>
        public bool ksh_UP(string ksh,string lqxx, string lqzy,string pcdm)
        {
            try
            {
                string sql = "update zk_lqk set td_zt=5 ,xxbz='',xqbz='',lqxx=@lqxx,lqzy=@lqzy,pcdm=@pcdm,zysx=1,xx_zt=0,xq_zt=5,zydm='',xqdm='500',types='0',jyzy='',td_pc='',lqtime=GETDATE()   where ksh=@ksh";
                _dbA.BeginTran();

                List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@ksh", ksh), new SqlParameter("@lqxx", lqxx), new SqlParameter("@lqzy", lqzy), new SqlParameter("@pcdm", pcdm) };
                //考生轨迹
                string sqlgj = " insert into   zk_kslqgj (ksh,username,type,times)  (ksh,username,type,times)   select ksh,'" + SincciLogin.Sessionstu().UserName + "',5,getdate() from zk_lqk   where ksh=@ksh ";
                _dbA.execSql_Tran(sqlgj, lisP);
                
                int iCount = _dbA.execSql_Tran(sql, lisP);
 
                StringBuilder stb = new StringBuilder();
                if (iCount > 0 )
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
        public DataTable Select_Print_lq(string where)
        {
          //  where = " pcdm='01' and lqxx='0105' and td_zt=5 and zysx=2";
            string sql = "  select * from View_lqDaoc where " + where + " order by lqzymc asc,zf desc";

            DataTable dt = _dbA.selectTab(sql, ref   error, ref   bReturn);

            return dt;
        }

        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="xxdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable Select_Print_shb(string pcdm,string lqxx,string start,string end)
        {
            string sql = "exec proc_spb @lqxx,@pcdm,@start,@end";
            if (pcdm == "01")
            {
                sql = "exec proc_spb2 @lqxx,@pcdm,@start,@end";
            }
         
            List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@pcdm", pcdm), new SqlParameter("@lqxx", lqxx),
                new SqlParameter("@start", start),new SqlParameter("@end", end)
             };

            DataTable dt = _dbA.selectTab(sql, lisP, ref   error, ref   bReturn);

            return dt;
        }
        /// <summary>
        /// 导入录取
        /// </summary>
        /// <param name="excelFilePath"></param>
        /// <returns></returns>
        public string ImportExcelData(string excelFilePath)
        {
            if (!File.Exists(excelFilePath))
                return "导入失败，原因是：输入的文件路径不正确或指定的文件不存在。";

            ExcelQueryFactory excelFile = new ExcelQueryFactory(excelFilePath);
            ExcelQueryable<Row> excel = excelFile.Worksheet(0);
            StringBuilder resultMsg = new StringBuilder();
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string sqltab = "select CONVERT(int, ISNULL(td_pc,0)) td_pc  from zk_lqk  group by td_pc   order by td_pc desc";
            string czx = "";
            bool aa = false;
            DataTable tab = _dbA.selectTab(sqltab, ref   czx, ref   aa);
            int tdpc = Convert.ToInt32(tab.Rows[0]["td_pc"].ToString());
            tdpc++;



            string checkSql = "Select a.ksh,td_zt,zzf From zk_lqk a join zk_zkcj b ON b.ksh = a.ksh Where a.ksh=@ksh;";
            bool bReturn = true;

            int i = 0;
            int lost = 0;
            int finish = 0;
            string aac = "";
            DataTable tabs111 = _dbA.selectTab(" EXEC dbo.proc_tj ", ref aac, ref bReturn);

            try
            {

                foreach (var element in excel)
                {
                    string errMsg = "";
                    string baseStr = "";
                    if (element.ColumnNames.Count() != 8)
                        return "导入失败，原因是：输入的文件路径格式不对应，目标格式为8列，您输入的是：" + element.ColumnNames.Count() + "列的格式。";

                   //"系统时间：" + DateTime.Now.ToString("yyyy年MM月dd日hh时mm分ss秒") + "。";

                    //空数据则跳过
                    if (string.IsNullOrEmpty(element[0].ToString().Trim()))
                        continue;

                    string sqlCmd = "update zk_lqk set cj=@cj,lqxx=@lqxx,pcdm=@pcdm,td_zt=5,lqzy=@lqzy, zydm='00',xx_zt=4,zysx=1,xq_zt=5,jyzy=0,xqdm=@xqdm,lqtime=GETDATE(),daoru=0 ,sftzs=@sftzs,sfpc=@sfpc where ksh=@ksh; insert into   zk_kslqgj (ksh,username,type,times,ip,xxdm,pcdm)    select ksh,'" + SincciLogin.Sessionstu().UserName + "',5,getdate(),'','" + element[1].ToString().Trim() + "','11' from zk_lqk   where ksh=@ksh ";
                    List<SqlParameter> param1= new List<SqlParameter> {
                                                new SqlParameter("@ksh",element[0].ToString().Trim())

                    };
                    DataSet tmpDataSet = _dbA.selectDataSet(checkSql, param1, ref errMsg, ref bReturn);
                    List<SqlParameter> param = new List<SqlParameter> {
                                                new SqlParameter("@ksh",element[0].ToString().Trim()),
                                                new SqlParameter("@lqxx",element[1].ToString().Trim()),
                                                new SqlParameter("@lqzy",element[2].ToString().Trim()),
                                                new SqlParameter("@pcdm",element[3].ToString().Trim()),
                                                new SqlParameter("@xqdm",element[4].ToString().Trim()),
                                                 new SqlParameter("@sftzs",element[5].ToString().Trim()),
                                                   new SqlParameter("@sfpc",element[7].ToString().Trim()),
                                                new SqlParameter("@cj",tmpDataSet.Tables[0].Rows[0]["zzf"].ToString()) 
                              
                    };
                    // Convert.ToInt32((jhsl * bil * 10 + 0.5) / 10);
                
                    
                    if (element[0].ToString().Trim().Length == 0)
                    {
                        errMsg += "第" + (i + 1) + "条数据导入失败：报名号不能为空! " + element[0].ToString().Trim();
                    }
                    else
                    {
                        if (tmpDataSet.Tables[0] != null)
                        {
                            if (tmpDataSet.Tables[0].Rows.Count == 0)
                            {
                                errMsg += "第" + (i + 1) + "条数据导入失败：找不到该考生【" + element[0].ToString() + "】! ";
                            }
                            else
                            {
                                if (tmpDataSet.Tables[0].Rows[0]["td_zt"].ToString() != "0")
                                {
                                    errMsg += "第" + (i + 1) + "条数据导入失败：该考生状态被锁定【" + element[0].ToString() + "】! ";
                                }
                            }
                        }
                        else
                        {
                            errMsg += "第" + (i + 1) + "条数据导入失败：找不到该考生【" + element[0].ToString() + "】! ";

                        }
                      
                    }    
                    if (element[1].ToString().Trim().Length == 0)
                    {
                        errMsg += "第" + (i + 1) + "条数据导入失败：录取学校不能为空! " + element[1].ToString().Trim();
                    }
               
                    if (element[3].ToString().Trim().Length == 0)
                    {
                        errMsg += "第" + (i + 1) + "条数据导入失败：批次代码不能为空! " + element[3].ToString().Trim();
                    }
                    else
                    {
                        switch (UserType)
                        {
                            case 3:
                                if (element[3].ToString().Trim().Substring(0, 1) != "1")
                                {
                                    errMsg += "第" + (i + 1) + "条数据导入失败：批次代码只能为高中批次! " + element[3].ToString().Trim();
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    if (element[4].ToString().Trim().Length == 0)
                    {
                        errMsg += "第" + (i + 1) + "条数据导入失败：县区代码不能为空! " + element[4].ToString().Trim();
                    }
                    if (element[5].ToString().Trim().Length == 0)
                    {
                        errMsg += "第" + (i + 1) + "条数据导入失败：录取类型不能为空! " + element[5].ToString().Trim();
                    }
                    if (element[7].ToString().Trim().Length == 0)
                    {
                        errMsg += "第" + (i + 1) + "条数据导入失败：sfpc不能为空! " + element[7].ToString().Trim();
                    }
                    if (element[5].ToString().Trim()=="4")
                    {
                        if (tabs111.Rows.Count == 0)
                        {
                            errMsg += "第" + (i + 1) + "条数据导入失败：无统招线! " + element[1].ToString().Trim();
                        }
                        else
                        {
                            int zzf = Convert.ToInt32(Convert.ToDecimal(tmpDataSet.Tables[0].Rows[0]["zzf"].ToString()));
                            if (element[6].ToString().Trim()!="")
                            {
                                int fsx = Convert.ToInt32(element[6].ToString().Trim());
                                int f = Convert.ToInt32((fsx * 0.7 * 10 + 0.5) / 10);
                                if (zzf < f)
                                {
                                    errMsg += "第" + (i + 1) + "条数据导入失败：该考生未达到该招生学校统招控制线! " + element[0].ToString().Trim();
                                }
                            }
                            else
                            {
                                DataRow[] dr = tabs111.Select(" ts='统招生' and name like '%" + element[0].ToString().Trim().Substring(0, 3) + "%' and lqxx='" + element[1].ToString().Trim() + "'");
                                if (dr.Length > 0)
                                {
                                    int fsx = Convert.ToInt32(Convert.ToDecimal(dr[0]["n"]));
                                    int f = Convert.ToInt32((fsx * 0.7 * 10 + 0.5) / 10);
                                    if (zzf < f)
                                    {
                                        errMsg += "第" + (i + 1) + "条数据导入失败：该考生未达到该招生学校统招控制线! " + element[0].ToString().Trim();
                                    }
                                }
                                else
                                {
                                    errMsg += "第" + (i + 1) + "条数据导入失败：没有该招生学校县区统招线! " + element[1].ToString().Trim();
                                }
                            }
                       
                        }    

                    }
                 
                  
                    //if (tmpDataSet.Tables[2].Rows.Count == 0)
                    //{
                    //    errMsg += "第" + (i + 1) + "条数据导入失败：该考生【" + element[0].ToString() + "】" + element[3].ToString() + "批次未报该学校【" + element[1].ToString() + "】！";
                    //}

                    //如果上面的验证都通过的话就继续执行添加（增量）或修改数据（非增量）
                    if (string.IsNullOrEmpty(errMsg))
                    {
                        _dbA.ExecuteNonQuery(sqlCmd, param, ref errMsg, ref bReturn);
                        if (!bReturn)
                        {
                            errMsg = "第" + (i + 1) + "行数据导入时发生错误.";
                        }
                    }

                    if (!string.IsNullOrEmpty(errMsg))
                    {
                    //    resultMsg.Append(baseStr + "第" + (i + 1) + "行数据导入时发生错误，原因是：<br />");
                        resultMsg.Append(" " + errMsg + "<br />");

                        lost++;
                    }
                    else
                    {
                     //   resultMsg.Append(baseStr + "第" + (i + 1) + "行数据导入成功。<br />");
                        finish++;
                    }

 
                //    resultMsg.Append("---------------------------------------------------<br /><br />");
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
        /// 导入回收
        /// </summary>
        /// <param name="excelFilePath"></param>
        /// <returns></returns>
        public string ImportExcelData2(string excelFilePath,ref List<string> listksh)
        {
            if (!File.Exists(excelFilePath))
                return "导入失败，原因是：输入的文件路径不正确或指定的文件不存在。";

            ExcelQueryFactory excelFile = new ExcelQueryFactory(excelFilePath);
            ExcelQueryable<Row> excel = excelFile.Worksheet(0);
            StringBuilder resultMsg = new StringBuilder();

           
           string checkSql = "Select ksh,td_zt From zk_lqk Where ksh=@ksh;";
            bool bReturn = true;
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            int i = 0;
            int lost = 0;
            int finish = 0;
          

            try
            {

                foreach (var element in excel)
                {
                    if (element.ColumnNames.Count() != 1)
                        return "导入失败，原因是：输入的文件路径格式不对应，目标格式为1列，您输入的是：" + element.ColumnNames.Count() + "列的格式。";

                    string errMsg = "";
                    string baseStr = ""; //"系统时间：" + DateTime.Now.ToString("yyyy年MM月dd日hh时mm分ss秒") + "。";

                    //空数据则跳过
                    if (string.IsNullOrEmpty(element[0].ToString().Trim()))
                        continue;

                    string ksh = element[0].ToString().Trim();
                    List<SqlParameter> param = new List<SqlParameter> {
                                                new SqlParameter("@ksh",element[0].ToString().Trim())
                                               
                    };

                    DataSet tmpDataSet = _dbA.selectDataSet(checkSql, param, ref errMsg, ref bReturn);
                    if (element[0].ToString().Trim().Length == 0)
                    {
                        errMsg += "第" + (i + 1) + "条数据导入失败：报名号不能为空! " + element[0].ToString().Trim();
                    }
                    else
                    {
                        if (tmpDataSet.Tables[0] != null)
                        {
                            if (tmpDataSet.Tables[0].Rows.Count == 0)
                            {
                                errMsg += "第" + (i + 1) + "条数据导入失败：找不到该考生【" + element[0].ToString() + "】! ";
                            }
                            else
                            {
                                switch (UserType)
                                {
                                    case 3:
                                        if (element[0].ToString().Substring(2, 4) != Department)
                                        {
                                            errMsg += "第" + (i + 1) + "条数据导入失败：该考生不属于您县区【" + element[0].ToString() + "】! ";
                                        }
                                        break;
                                    default:
                                        break;
                                }
                             
                            }    
                           
                        }
                        else
                        {
                            errMsg += "第" + (i + 1) + "条数据导入失败：找不到该考生【" + element[0].ToString() + "】! ";

                        }

                    }
                  
                    if (string.IsNullOrEmpty(errMsg))
                    {
                        if (!listksh.Contains(ksh))
                        {
                            listksh.Add(ksh);
                        }  
                       
                    }

                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        resultMsg.Append(baseStr + "第" + (i + 1) + "行数据导入时发生错误，原因是：<br />");
                        resultMsg.Append(" " + errMsg + "<br />");

                        lost++;
                    }
                    else
                    {
                 //       resultMsg.Append(baseStr + "第" + (i + 1) + "行数据导入成功。<br />");
                        finish++;
                    }


                //    resultMsg.Append("---------------------------------------------------<br /><br />");
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
        /// 导出数据全部DBF
        /// </summary> 
        public DataSet ExportDBFALL(string where )
        {
            //string sql = "select *,zsxxmc as lqxxmc from zk_lqk a left join zk_zsxxdm b on a.lqxx=b.zsxxdm where " + where + "  order by ksh asc ";
            string sql = @"SELECT ksh,zkzh,xm,xbmc,bmdxqdm,bmddm,bmdmc,sfzh,lqxx,lqxxmc,yw,sx,yy,lkzh,wkzh,
dsdj,ty,zhdj,jf,zf,CASE sftzs
WHEN 1 THEN (CASE pcdm WHEN '01' THEN '精准扶贫民族' ELSE '统招生' end) WHEN 2 THEN '配额生' WHEN 3 THEN '配转统' WHEN 4 THEN '特长生' WHEN 5 THEN '补录' ELSE '' END AS sftzs,CASE xjtype
WHEN 0 THEN '是' ELSE '否' END AS xjtype,CASE jzfp
WHEN 0 THEN '否' ELSE '是' END AS jzfp,bklb,kslbmc,sfpc FROM dbo.View_lqDaoc where " + where;
            return _dbA.selectDataSet(sql, ref error, ref bReturn);
        }
        /// <summary>
        /// 导入配额生（龙岩）指标生信息
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <returns>导入日志信息</returns>
        public string Import_ZBSXX(string filepath)
        {
            string error = "";
            string failMsg = "";
            Boolean bReturn = false;
            int importNum = 0;
            int errorNum = 0;
            SqlDbHelper_1 dbHelp = new SqlDbHelper_1();
            ExcelQueryFactory excelFile = new ExcelQueryFactory(filepath);
            ExcelQueryable<Row> excel = excelFile.Worksheet(0);
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string mssql = "insert into zk_zbsxx(xxdm,zsxxdm,zbssl,pcdm,xqdm,type) values(@byxxdm,@zsxxdm,@zbssl,@pcdm,@xqdm,@type)";
            //   string sql = "insert into zk_lq_zbs_jhk(pcdm,zsxxdm,xxdm,zbssl) values(@pcdm,@zsxxdm,@byxxdm,@zbssl)";
            int totalNum = 0;
            int outnum = 0;
            int fcount = 1;
            string importres = "";
            foreach (var element in excel)
            {
                if (element.ColumnNames.Count() != 5)
                    return "导入失败，原因是：输入的文件路径格式不对应，目标格式为5列，您输入的是：" + element.ColumnNames.Count() + "列的格式。";
                totalNum++;
                //空数据则跳过
                if (string.IsNullOrEmpty(element["byxxdm"].ToString().Trim()))
                {

                    errorNum++;
                    continue;
                }
                if (GetXXDM(element["byxxdm"].ToString().Trim()) == null)
                {
                    importres += "第" + fcount + "行数据导入失败，毕业学校代码不存在<br/>";
                }
                else if (GetZSXXDM(element["zsxxdm"].ToString().Trim()) == null)
                {
                    importres += "第" + fcount + "行数据导入失败，招生学校代码不存在<br/>";
                }
                else if (Int32.TryParse(element["zbssl"].ToString().Trim(), out outnum) == false)
                {
                    importres += "第" + fcount + "行数据导入失败，配额生数量必须为数字<br/>";
                }
                else if (element["pcdm"].ToString().Trim() != "11")
                {
                    importres += "第" + fcount + "行数据导入失败，批次代码必须为11<br/>";
                }
                //else if (Int32.TryParse(element["type"].ToString().Trim(), out outnum) == false)
                //{
                //    importres += "第" + fcount + "行数据导入失败，生源类型必须为数字<br/>";
                //}
                //else if (element["type"].ToString().Trim() != "1" && element["type"].ToString().Trim() != "2")
                //{
                //    importres += "第" + fcount + "行数据导入失败，生源类型必须为1或者2<br/>";
                //}
                else if (GetXQDM(element["xqdm"].ToString().Trim()) == null)
                {
                    importres += "第" + fcount + "行数据导入失败，县区代码不存在<br/>";
                }
                else if (element["xqdm"].ToString().Trim() != Department.Trim() && UserType == 3)
                {
                    importres += "第" + fcount + "行数据导入失败，你只能导入你的县区数据<br/>";
                }
                else
                {
                    List<SqlParameter> lisP = new List<SqlParameter>(){
			 new SqlParameter("@byxxdm", element["byxxdm"].ToString().Trim()),
			 new SqlParameter("@zsxxdm",element["zsxxdm"].ToString().Trim()),
			 new SqlParameter("@zbssl", element["zbssl"].ToString().Trim()),
			 new SqlParameter("@pcdm", element["pcdm"].ToString().Trim()),
             new SqlParameter("@xqdm",element["xqdm"].ToString().Trim()),
             new SqlParameter("@type",1)
			};
                    //        List<SqlParameter> lisP1 = new List<SqlParameter>(){
                    // new SqlParameter("@pcdm",  element["pcdm"].ToString().Trim()),
                    // new SqlParameter("@zsxxdm", element["zsxxdm"].ToString().Trim()),
                    // new SqlParameter("@byxxdm",element["byxxdm"].ToString().Trim()),
                    // new SqlParameter("@zbssl", element["zbssl"].ToString().Trim())
                    //};
                    string sql2 = "select * from zk_zbsxx where pcdm='" + element["pcdm"].ToString().Trim() + "' and zsxxdm='" + element["zsxxdm"].ToString().Trim() + "' and xxdm='" + element["byxxdm"].ToString().Trim() + "' and xqdm='" + element["xqdm"].ToString().Trim() + "'";
                    DataTable dt2 = dbHelp.selectTab(sql2, ref error, ref bReturn);
                    if (dt2 != null && dt2.Rows.Count > 0)
                    {
                        sql2 = " delete from zk_zbsxx where pcdm='" + element["pcdm"].ToString().Trim() + "' and zsxxdm='" + element["zsxxdm"].ToString().Trim() + "' and xxdm='" + element["byxxdm"].ToString().Trim() + "' and xqdm='" + element["xqdm"].ToString().Trim() + "'";

                        dbHelp.ExecuteNonQuery(sql2, ref error, ref bReturn);
                    }

                    int flag = dbHelp.ExecuteNonQuery(mssql, lisP, ref error, ref bReturn);
                    // int flag2 = dbHelp.ExecuteNonQuery(sql, lisP1, ref error, ref bReturn);
                    if (flag > 0)
                    {
                        importNum++;
                     //   importres += "第" + fcount + "行数据导入成功<br/>";
                    }
                    else
                    {
                        importres += "第" + fcount + "行数据导入失败<br/>";
                        failMsg = failMsg + "=================" + element[0].ToString().Trim();
                    }
                }
                fcount++;
            }
            string ResMsg = "找到" + totalNum + "数据,成功导入" + importNum + "数据，无效数据" + errorNum + "条。失败数据ID为" + failMsg;
            return importres;
        }
        /// <summary>
        /// 获取招生学校代码
        /// </summary>
        /// <param name="zsxxdm">招生学校代码</param>
        /// <returns>招生学校代码查询结果</returns>
        public object GetZSXXDM(string zsxxdm)
        {
            string sql = "select zsxxdm from zk_zsxxdm where zsxxdm=@zsxxdm";
            List<SqlParameter> lisp = new List<SqlParameter>() { new SqlParameter("@zsxxdm", zsxxdm) };
            return _dbA.ExecuteScalar(sql, lisp, ref error, ref bReturn);
        }
        /// <summary>
        /// 判断学校代码是否存在
        /// </summary>
        /// <param name="xxdm">学校代码</param>
        /// <returns>学校代码查询结果</returns>
        public object GetXXDM(string xxdm)
        {
            string sql = "select xxdm from zk_xxdm where xxdm=@xxdm";
            List<SqlParameter> lisp = new List<SqlParameter>() { new SqlParameter("@xxdm", xxdm) };
            return _dbA.ExecuteScalar(sql, lisp, ref error, ref bReturn);

        }
        /// <summary>
        /// 获取县区代码
        /// </summary>
        /// <param name="xqdm">县区代码</param>
        /// <returns>县区代码查询结果</returns>
        public object GetXQDM(string xqdm)
        {
            string sql = "select xqdm from zk_xqdm where xqdm=@xqdm";
            List<SqlParameter> lisp = new List<SqlParameter>() { new SqlParameter("@xqdm", xqdm) };
            return _dbA.ExecuteScalar(sql, lisp, ref error, ref bReturn);
        }
        /// <summary>
        /// 获取录取学校名称
        /// </summary>
        /// <param name="zsxxdm">招生学校代码</param>
        /// <returns>录取学校名称</returns>
        public string GetXXMC(string zsxxdm)
        {
            string sql = "select zsxxmc from zk_zsxxdm where zsxxdm=@zsxxdm";
            List<SqlParameter> lisp = new List<SqlParameter>() { new SqlParameter("@zsxxdm", zsxxdm) };
            return _dbA.ExecuteScalar(sql, lisp, ref error, ref bReturn).ToString();
        }

        /// <summary>
        /// 录取统计
        /// </summary> 
        public DataTable SelectLqtj(string pcdm,string xxdm,string leix)
        {
            string sql = @"select a.xxdm,a.xxmc,'' allnum, isnull(b.lqnum,0) lqnum,'' yqnum 
from zk_xxdm a left join (select bmddm,bmdmc,COUNT(1) lqnum from zk_lqk a join View_ksxxNew 
b on a.ksh=b.ksh where 
lqxx=@xxdm and pcdm=@pcdm and td_zt in (1,5)  and sftzs=@sftzs group by bmddm,bmdmc
)b on a.xxdm=b.bmddm  
union all
select '99999','合计',jhs,isnull(b.lqnum,0) lqnum,jhs-isnull(b.lqnum,0)  as yqnum  from zk_lqjhk a left join
(select lqxx,pcdm,COUNT(1) lqnum from zk_lqk where td_zt in (1,5)  and sftzs=@sftzs  group by lqxx,pcdm) b
on a.pcdm=b.pcdm and a.xxdm=b.lqxx
where  a.pcdm=@pcdm and xxdm=@xxdm";
            if (pcdm != "01" && leix == "2")
            {
                sql = @"select a.xxdm,a.xxmc,zbssl allnum, isnull(b.lqnum,0) lqnum,zbssl- isnull(b.lqnum,0) as yqnum,isnull(b.lqmax,0) as lqmax,isnull(b.lqmin,0) as lqmin 
        from View_ZBSXX a left join (select bmddm,bmdmc,COUNT(1) lqnum,MAX(cj) lqmax,MIN(cj) lqmin from zk_lqk a join View_ksxxNew  b on 
        a.ksh=b.ksh where lqxx=@xxdm and pcdm=@pcdm and td_zt in (1,5)   and sftzs=@sftzs  group by bmddm,bmdmc
        )b on a.xxdm=b.bmddm  WHERE   zsxxdm=@xxdm  
        union all
         select  '99999','合计',SUM(a.zbssl) jhs, SUM(ISNULL(b.lqnum,0)) lqnum,SUM(a.zbssl)-SUM(isnull(b.lqnum,0))  as yqnum,MAX(ISNULL(b.lqmax,0)) as lqmax,  min(ISNULL(b.lqmin,0)) as lqmin  from View_ZBSXX a left join 
        (select bmddm,bmdmc,COUNT(1) lqnum,MAX(cj) lqmax,MIN(cj) lqmin from zk_lqk a join View_ksxxNew  b on 
        a.ksh=b.ksh where lqxx=@xxdm and pcdm=@pcdm and td_zt in (1,5)  and sftzs=@sftzs  group by bmddm,bmdmc
        ) b
        on   a.xxdm=b.bmddm WHERE a.zsxxdm=@xxdm ";
            }
            List<SqlParameter> lisp = new List<SqlParameter>() { new SqlParameter("@xxdm", xxdm), new SqlParameter("@pcdm", pcdm), new SqlParameter("@sftzs", leix) };
            return _dbA.selectTab(sql, lisp, ref error, ref bReturn);
        }



        /// <summary>
        /// 录取统计
        /// </summary> 
        public DataTable SelectLqtj()
        {
            string sql = " exec proc_tj ";
            return _dbA.selectTab(sql, ref error, ref bReturn);
        }
      
    }
}
