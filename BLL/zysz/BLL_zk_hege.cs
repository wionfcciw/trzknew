using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using LinqToExcel;
using LinqToExcel.Query;
using System.Web;
namespace BLL
{
    public class BLL_zk_hege
    {
        private SqlDbHelper_1 _dbHelper = new SqlDbHelper_1();
        private string error = "";
        private bool bReturn = false;
        /// <summary>
        /// 执行分页存储过程，返回记录总数和当前页的数据。会考成绩 
        /// </summary>
        /// <param name="where">执行的条件</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="totalRecord">返回的记录总数</param>
        public DataTable ExecuteProc3(string where, int pageSize, int pageIndex, ref int totalRecord)
        {
            //string sql="select  from AppraisalPersonnel";

            //数据库表名
            string tabName = " zk_hege  ";
            //要查询的字段
            string reField = "  * ";
            //排序字段
            string orderStr = " ksh,type ";
            //排序标识（0、升序；1、降序）
            int orderType = 0;
            //执行成功与失败的标识（true、表示执行成功；false、表示执行失败）
            bool bFlag = false;
            decimal dec = 0;
            DataSet dst = _dbHelper.ExecuteProc(tabName, reField, orderStr, "", where, pageSize, pageIndex, orderType, 0, ref dec, ref totalRecord, ref bFlag);
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
        /// 查询
        /// </summary>
        /// <returns></returns>
        public DataTable Selzk_hege(string ksh, int type)
        {
            string sql = "select * from zk_hege where ksh=@ksh and type=@type";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@ksh",ksh),new SqlParameter("@type",type)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            return dt;
        }

        /// <summary>
        /// 查询个条数据      1男儿幼儿师范 2师范配额生  3普高艺术、体育特长生4普高国际班5师范(音乐、美术、学前教育专业)6三星普高国际班
        /// </summary> 
        public bool Disp(string ksh,int type)
        {
            string sql = "select * from zk_hege where ksh=@ksh and type=@type";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@ksh",ksh),new SqlParameter("@type",type)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
                return true;
            else
            return false;
        }

        /// <summary>
        /// 按报名号查询zk_ksxxgl,zk_kshkcj联表查询
        /// </summary>
        /// <param name="ksh"></param>
        /// <returns></returns>
        public DataTable Select_zk_kshkcj(string ksh)
        {
            Model_zk_kshkcj info = new Model_zk_kshkcj();
            string sql = " select  xxqr, ksh  from zk_ksxxgl where ksh=@ksh ";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@ksh",ksh)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            return dt;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Insert_zk_zk_hege(string ksh, string xm, int type)
        {
            string sql = "insert into zk_hege(ksh,xm,type) values(@ksh,@xm,@type)";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@ksh",ksh),
 			 new SqlParameter("@type",type),
              new SqlParameter("@xm",xm)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 自定义修改
        /// </summary>
        /// <param name="set"></param>
        /// <param name="where"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool update_zk_kshkcj(string set, string where)
        {
            string sql = "update  zk_kshkcj set " + set + " where " + where;
            _dbHelper.ExecuteNonQuery(sql, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
  
      

        /// <summary>
        /// 根据报名号多个删除
        /// </summary>
        /// <param name="xqdms">报名号</param>
        /// <returns></returns>
        public bool DeleteDatakshkcj(List<string> ksh)
        {
            string inStr = "";

            foreach (var str in ksh)
            {
                inStr += "'" + str + "',";

            }

            string sqlCmd = "Delete zk_hege Where id In (" + inStr.Substring(0, inStr.Length - 1) + ") ";
            _dbHelper.ExecuteNonQuery(sqlCmd, ref error, ref bReturn);

            if (bReturn) return true;
            else return false;
        }

        #region Excel数据导入到SqlServer数据库方法
        /// <summary>
        /// Excel数据导入到SqlServer数据库方法
        /// </summary>
        /// <param name="excelFilePath">excel文件存放路径</param>
        /// <param name="importSqlCmd">需要用于导入数据的sql语句</param>
        /// <param name="importParams">用于导入数据的sql参数列表</param>
        /// <param name="isZL">是否增量导入</param>
        /// <returns></returns>
        public string ImportExcelData(string excelFilePath, string type)
        {
            string Content = "";
            string strPath = "";
            string FileName = "";
            StringBuilder resultMsg = new StringBuilder();
            try
            {

             

                if (!File.Exists(excelFilePath))
                    return "导入失败，原因是：输入的文件路径不正确或指定的文件不存在。";
                ExcelQueryFactory excelFile = new ExcelQueryFactory(excelFilePath);
                ExcelQueryable<Row> excel = excelFile.Worksheet(0);
                SqlDbHelper_1 dbHelp = new SqlDbHelper_1();
          

               
                string sqlCmd2 = "";
                string checkSql = "  select   ksh  from zk_hege where ksh=@ksh and type=@type;select  ksh,xm  from zk_ksxxgl where ksh=@ksh ; ";

              
                StringBuilder sb2 = new StringBuilder();

                Content = "";
                strPath = "Temp";
                FileName = config.Get_UserName + ".txt";
                string mPath = HttpContext.Current.Server.MapPath("~\\" + strPath + "\\" + FileName + "");
                if (File.Exists(mPath))
                    File.Delete(mPath);


                bool bReturn = true;
                string errMsg = "";
                int i = 0;
                int lost = 0;
                int finish = 0;

                foreach (var element in excel)
                {
                    try
                    {

                        if (element.ColumnNames.Count() != 2)
                        {
                           resultMsg.Append("导入失败，原因是：导入的格式不正确,格式为2列，您导入的是：" + element.ColumnNames.Count() + "列的格式。");
                       //     new config().WritTxt(Content, strPath, FileName);
                           return resultMsg.ToString();
                        }

                        errMsg = "";
                        //  string baseStr = "";// "系统时间：" + DateTime.Now.ToString("yyyy年MM月dd日hh时mm分ss秒") + "。";

                        //空数据则跳过
                        if (string.IsNullOrEmpty(element[0].ToString().Trim()))
                            continue;



                        sqlCmd2 = "insert into zk_hege(ksh,xm,type) values('" + element[0].ToString().Trim() + "', '" + element[1].ToString().Trim() + "'," + type + ") ; ";

                        List<SqlParameter> dataParams = new List<SqlParameter> {
                                              new SqlParameter("@ksh",element[0].ToString().Trim()),                                           
                                                new SqlParameter("@xm",element[1].ToString().Trim()),
                                                new SqlParameter("@type",type)  };

                        DataSet tmpDataSet = _dbHelper.selectDataSet(checkSql, dataParams, ref error, ref bReturn);

      
                        //如果上面的验证都通过的话就继续执行添加（增量）或修改数据（非增量）
                        if (tmpDataSet.Tables[0] != null && tmpDataSet.Tables[0].Rows.Count > 0)
                        {

                            resultMsg.Append("第" + (i + 1) + "行数据有误，原因是：考生[" + element[0].ToString().Trim() + "]已存在名单内！<br />");
                          //      new config().WritTxt(Content, strPath, FileName);
                                lost++;
                                i++;
                                continue;
                          
                        }
                        if (tmpDataSet.Tables[1] == null || tmpDataSet.Tables[1].Rows.Count == 0)
                        {
                            resultMsg.Append("第" + (i + 1) + "行数据有误，原因是：考生[" + element[0].ToString().Trim() + "]信息不存在！<br />");
                            lost++;
                            i++;
                            continue;

                        }
                        else
                        {
                            if (tmpDataSet.Tables[1].Rows[0]["xm"].ToString() != element[1].ToString().Trim())
                            {
                                resultMsg.Append("第" + (i + 1) + "行数据有误，原因是：考生姓名[" + element[0].ToString().Trim() + "]与报名号不对应！<br />");
                                lost++;
                                i++;
                                continue;
                            }
                        }
                        if (element[0].ToString().Trim().Length != 12)
                        {
                            resultMsg.Append("第" + (i + 1) + "行错误，原因：报名号【" + element[0].ToString() + "】不是12位。<br />");
                       //     new config().WritTxt(Content, strPath, FileName);
                            lost++;
                            i++;
                            continue;
                        }
                     

                        sb2.Append(sqlCmd2);
                        if (sb2.Length > 0)
                        {

                            _dbHelper.ExecuteNonQuery(sb2.ToString(), ref   errMsg, ref   bReturn);
                            sb2.Clear();
                        }
             

                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            resultMsg.Append("第" + (i + 1) + "行数据导入时发生错误。" + errMsg + "<br />");
                        //    new config().WritTxt(Content, strPath, FileName);
                            lost++;
                        }
                        else
                        {
                          resultMsg.Append("第" + (i + 1) + "行数据导入成功。 ");
                        //    new config().WritTxt(Content, strPath, FileName);
                            finish++;
                        }

                        resultMsg.Append("--------------------------------------------------- <br />");
                        //new config().WritTxt(Content, strPath, FileName);
                        i++;

                    }
                    catch (Exception ex)
                    {
                        resultMsg.Append( "第" + (i + 1) + "行数据导入时发生错误。 ");
                    //    new config().WritTxt(Content, strPath, FileName);
                        lost++;
                        i++;
                        continue;
                    }
                }
             
           

               resultMsg.Append("共处理:" + i + "数据导入完毕。导入成功：" + finish + "条数据。导入失败：" + lost + "条数据。 ");

             //   new config().WritTxt(Content, strPath, FileName);
                // File.Delete(excelFilePath);

                return resultMsg.ToString();
                
            }
            catch (Exception ex)
            {
                resultMsg.Append("文件上传失败,请检查重新上传！" + ex);
               // new config().WritTxt(Content, strPath, FileName);
                return "";
            }

        }

        private bool checzhjp(string value)
        {
            switch (value)
            {
                case "A":
                    return true;
                case "B":
                    return true;
                case "C":
                    return true;
                case "D":
                    return true;
                default:
                    return false;
            }
        }

        #endregion

        #region "导出数据"
        /// <summary>
        /// 导出数据excel
        /// </summary> 
        public DataSet ExportEXCELKsh(string where)
        {

            //string sql = " select a.ksh as '报名号', b.xm as '姓名',a.Swdj as '生物',a.Dldj as '地理' from zk_kshkcj as a,zk_ksxxgl as b where a.ksh=b.ksh and  " + where + " ";
            string sql = " select b.ksh as '报名号', b.xm as '姓名',b.bjdm as '班级',ISNULL( a.Swdj,'D') as '生物',ISNULL(a.Dldj,'D') as '地理' from zk_ksxxgl as b left join zk_kshkcj as a on b.ksh=a.ksh  where " + where;
            return _dbHelper.selectDataSet(sql, ref error, ref bReturn);
        }

        #endregion


        public DataTable Execute_TJS(string where, int pageSize, int pageIndex, ref int totalRecord)
        {
            //string sql="select  from AppraisalPersonnel";

            //数据库表名
            string tabName = "  zk_TJS_gr   ";
            //要查询的字段
            string reField = "  * ";
            //排序字段
            string orderStr = " ksh  ";
            //排序标识（0、升序；1、降序）
            int orderType = 0;
            //执行成功与失败的标识（true、表示执行成功；false、表示执行失败）
            bool bFlag = false;
            decimal dec = 0;
            DataSet dst = _dbHelper.ExecuteProc(tabName, reField, orderStr, "", where, pageSize, pageIndex, orderType, 0, ref dec, ref totalRecord, ref bFlag);
            if (bFlag)
            {
                if (dst.Tables.Count > 0)
                {
                    return dst.Tables[0];
                }
            }
            return null;
        }

        public DataTable Execute_TJS2(string where, int pageSize, int pageIndex, ref int totalRecord)
        {
            //string sql="select  from AppraisalPersonnel";

            //数据库表名
            string tabName = "  zk_TJS_sl   ";
            //要查询的字段
            string reField = "  * ";
            //排序字段
            string orderStr = " tjxxdm  ";
            //排序标识（0、升序；1、降序）
            int orderType = 0;
            //执行成功与失败的标识（true、表示执行成功；false、表示执行失败）
            bool bFlag = false;
            decimal dec = 0;
            DataSet dst = _dbHelper.ExecuteProc(tabName, reField, orderStr, "", where, pageSize, pageIndex, orderType, 0, ref dec, ref totalRecord, ref bFlag);
            if (bFlag)
            {
                if (dst.Tables.Count > 0)
                {
                    return dst.Tables[0];
                }
            }
            return null;
        }
        public DataTable Execute_TJS3(string where, int pageSize, int pageIndex, ref int totalRecord)
        {
            //string sql="select  from AppraisalPersonnel";

            //数据库表名
            string tabName = "  zk_TJS_wjs   ";
            //要查询的字段
            string reField = "  * ";
            //排序字段
            string orderStr = " ksh  ";
            //排序标识（0、升序；1、降序）
            int orderType = 0;
            //执行成功与失败的标识（true、表示执行成功；false、表示执行失败）
            bool bFlag = false;
            decimal dec = 0;
            DataSet dst = _dbHelper.ExecuteProc(tabName, reField, orderStr, "", where, pageSize, pageIndex, orderType, 0, ref dec, ref totalRecord, ref bFlag);
            if (bFlag)
            {
                if (dst.Tables.Count > 0)
                {
                    return dst.Tables[0];
                }
            }
            return null;
        }
        public DataTable Execute_TJS4(string where, int pageSize, int pageIndex, ref int totalRecord)
        {
            //string sql="select  from AppraisalPersonnel";

            //数据库表名
            string tabName = "  zk_TJS_yt   ";
            //要查询的字段
            string reField = "  * ";
            //排序字段
            string orderStr = " ksh  ";
            //排序标识（0、升序；1、降序）
            int orderType = 0;
            //执行成功与失败的标识（true、表示执行成功；false、表示执行失败）
            bool bFlag = false;
            decimal dec = 0;
            DataSet dst = _dbHelper.ExecuteProc(tabName, reField, orderStr, "", where, pageSize, pageIndex, orderType, 0, ref dec, ref totalRecord, ref bFlag);
            if (bFlag)
            {
                if (dst.Tables.Count > 0)
                {
                    return dst.Tables[0];
                }
            }
            return null;
        }


        public string ImportExcelData_Tjs(string excelFilePath )
        {
            string Content = "";
            string strPath = "";
            string FileName = "";
            StringBuilder resultMsg = new StringBuilder();
            try
            {



                if (!File.Exists(excelFilePath))
                    return "导入失败，原因是：输入的文件路径不正确或指定的文件不存在。";
                ExcelQueryFactory excelFile = new ExcelQueryFactory(excelFilePath);
                ExcelQueryable<Row> excel = excelFile.Worksheet(0);
                SqlDbHelper_1 dbHelp = new SqlDbHelper_1();
                string sqlCmd2 = "";
                string checkSql = "  select   ksh  from zk_TJS_gr where ksh=@ksh and tjxxdm=@tjxxdm;select  ksh,xm  from zk_ksxxgl where ksh=@ksh ; ";
 
                StringBuilder sb2 = new StringBuilder();

                //Content = "";
                //strPath = "Temp";
                //FileName = config.Get_UserName + ".txt";
                //string mPath = HttpContext.Current.Server.MapPath("~\\" + strPath + "\\" + FileName + "");
                //if (File.Exists(mPath))
                //    File.Delete(mPath);


                bool bReturn = true;
                string errMsg = "";
                int i = 0;
                int lost = 0;
                int finish = 0;

                foreach (var element in excel)
                {
                    try
                    {

                        if (element.ColumnNames.Count() != 5)
                        {
                            resultMsg.Append("导入失败，原因是：导入的格式不正确,格式为5列，您导入的是：" + element.ColumnNames.Count() + "列的格式。");
                            //     new config().WritTxt(Content, strPath, FileName);
                            return resultMsg.ToString();
                        }

                        errMsg = "";
                        //  string baseStr = "";// "系统时间：" + DateTime.Now.ToString("yyyy年MM月dd日hh时mm分ss秒") + "。";

                        //空数据则跳过
                        if (string.IsNullOrEmpty(element[0].ToString().Trim()))
                            continue;



                        sqlCmd2 = "insert into zk_TJS_gr(ksh,xm,xqdm,tjxxdm,fs) values('" + element[0].ToString().Trim() + "', '" + element[1].ToString().Trim() + "', '" + element[2].ToString().Trim() + "', '" + element[3].ToString().Trim() + "', '" + element[4].ToString().Trim() + "') ; ";

                        List<SqlParameter> dataParams = new List<SqlParameter> {
                                              new SqlParameter("@ksh",element[0].ToString().Trim()),                                           
                                                new SqlParameter("@xm",element[1].ToString().Trim()),
                                                      new SqlParameter("@tjxxdm",element[3].ToString().Trim()) 
                                              };

                        DataSet tmpDataSet = _dbHelper.selectDataSet(checkSql, dataParams, ref error, ref bReturn);


                        //如果上面的验证都通过的话就继续执行添加（增量）或修改数据（非增量）
                        if (tmpDataSet.Tables[0] != null && tmpDataSet.Tables[0].Rows.Count > 0)
                        {

                            resultMsg.Append("第" + (i + 1) + "行数据有误，原因是：考生[" + element[0].ToString().Trim() + "]已存在名单内！<br />");
                            //      new config().WritTxt(Content, strPath, FileName);
                            lost++;
                            i++;
                            continue;

                        }
                        if (tmpDataSet.Tables[1] == null || tmpDataSet.Tables[1].Rows.Count == 0)
                        {
                            resultMsg.Append("第" + (i + 1) + "行数据有误，原因是：考生[" + element[0].ToString().Trim() + "]信息不存在！<br />");
                            lost++;
                            i++;
                            continue;

                        }
                        else
                        {
                            if (tmpDataSet.Tables[1].Rows[0]["xm"].ToString() != element[1].ToString().Trim())
                            {
                                resultMsg.Append("第" + (i + 1) + "行数据有误，原因是：考生姓名[" + element[0].ToString().Trim() + "]与报名号不对应！<br />");
                                lost++;
                                i++;
                                continue;
                            }
                        }
                        if (element[0].ToString().Trim().Length != 12)
                        {
                            resultMsg.Append("第" + (i + 1) + "行错误，原因：报名号【" + element[0].ToString() + "】不是12位。<br />");
                            //     new config().WritTxt(Content, strPath, FileName);
                            lost++;
                            i++;
                            continue;
                        }


                        sb2.Append(sqlCmd2);
                        if (sb2.Length > 0)
                        {

                            _dbHelper.ExecuteNonQuery(sb2.ToString(), ref   errMsg, ref   bReturn);
                            sb2.Clear();
                        }


                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            resultMsg.Append("第" + (i + 1) + "行数据导入时发生错误。" + errMsg + "<br />");
                            //    new config().WritTxt(Content, strPath, FileName);
                            lost++;
                        }
                        else
                        {
                         //   resultMsg.Append("第" + (i + 1) + "行数据导入成功。 ");
                            //    new config().WritTxt(Content, strPath, FileName);
                            finish++;
                        }

                    //    resultMsg.Append("--------------------------------------------------- <br />");
                        //new config().WritTxt(Content, strPath, FileName);
                        i++;

                    }
                    catch (Exception ex)
                    {
                        resultMsg.Append("第" + (i + 1) + "行数据导入时发生错误。 ");
                        //    new config().WritTxt(Content, strPath, FileName);
                        lost++;
                        i++;
                        continue;
                    }
                }
 
                resultMsg.Append("共处理:" + i + "数据导入完毕。导入成功：" + finish + "条数据。导入失败：" + lost + "条数据。 ");

                //   new config().WritTxt(Content, strPath, FileName);
                // File.Delete(excelFilePath);

                return resultMsg.ToString();

            }
            catch (Exception ex)
            {
                resultMsg.Append("文件上传失败,请检查重新上传！" + ex);
                // new config().WritTxt(Content, strPath, FileName);
                return "";
            }

        }
        /// <summary>
        /// 根据报名号多个删除
        /// </summary>
        /// <param name="xqdms">报名号</param>
        /// <returns></returns>
        public bool DeleteData_Tjs1(List<string> ksh)
        {
            string inStr = "";

            foreach (var str in ksh)
            {
                inStr += "'" + str + "',";

            }

            string sqlCmd = "Delete zk_TJS_gr Where id In (" + inStr.Substring(0, inStr.Length - 1) + ") ";
            _dbHelper.ExecuteNonQuery(sqlCmd, ref error, ref bReturn);

            if (bReturn) return true;
            else return false;
        }



        public string ImportExcelData_Tjs2(string excelFilePath)
        {
            string Content = "";
            string strPath = "";
            string FileName = "";
            StringBuilder resultMsg = new StringBuilder();
            try
            {



                if (!File.Exists(excelFilePath))
                    return "导入失败，原因是：输入的文件路径不正确或指定的文件不存在。";
                ExcelQueryFactory excelFile = new ExcelQueryFactory(excelFilePath);
                ExcelQueryable<Row> excel = excelFile.Worksheet(0);
                SqlDbHelper_1 dbHelp = new SqlDbHelper_1();
                string sqlCmd2 = "";
                string checkSql = "  select   tjxxdm  from zk_TJS_sl where tjxxdm=@tjxxdm and  syxxdm=@syxxdm; ";

                StringBuilder sb2 = new StringBuilder();

                //Content = "";
                //strPath = "Temp";
                //FileName = config.Get_UserName + ".txt";
                //string mPath = HttpContext.Current.Server.MapPath("~\\" + strPath + "\\" + FileName + "");
                //if (File.Exists(mPath))
                //    File.Delete(mPath);


                bool bReturn = true;
                string errMsg = "";
                int i = 0;
                int lost = 0;
                int finish = 0;

                foreach (var element in excel)
                {
                    try
                    {

                        if (element.ColumnNames.Count() != 6)
                        {
                            resultMsg.Append("导入失败，原因是：导入的格式不正确,格式为6列，您导入的是：" + element.ColumnNames.Count() + "列的格式。");
                            //     new config().WritTxt(Content, strPath, FileName);
                            return resultMsg.ToString();
                        }

                        errMsg = "";
                        //  string baseStr = "";// "系统时间：" + DateTime.Now.ToString("yyyy年MM月dd日hh时mm分ss秒") + "。";

                        //空数据则跳过
                        if (string.IsNullOrEmpty(element[0].ToString().Trim()))
                            continue;
                        sqlCmd2 = "insert into zk_TJS_sl(xqdm,syxxdm,tjxxdm,num,fs,pcdm) values('" + element[0].ToString().Trim() + "', '" + element[1].ToString().Trim() + "', '" + element[2].ToString().Trim() + "', " + element[3].ToString().Trim() + ", " + element[4].ToString().Trim() + ", '" + element[5].ToString().Trim() + "') ; ";

                        List<SqlParameter> dataParams = new List<SqlParameter> {
                                              new SqlParameter("@syxxdm",element[1].ToString().Trim()),
                                                new SqlParameter("@tjxxdm",element[2].ToString().Trim())
                                              };

                        DataSet tmpDataSet = _dbHelper.selectDataSet(checkSql, dataParams, ref error, ref bReturn);


                        //如果上面的验证都通过的话就继续执行添加（增量）或修改数据（非增量）
                        if (tmpDataSet.Tables[0] != null && tmpDataSet.Tables[0].Rows.Count > 0)
                        {

                            resultMsg.Append("第" + (i + 1) + "行数据有误，原因是：学校[" + element[1].ToString().Trim() + "]已存在名单内！<br />");
                            //      new config().WritTxt(Content, strPath, FileName);
                            lost++;
                            i++;
                            continue;

                        }
                        
                     

                        sb2.Append(sqlCmd2);
                        if (sb2.Length > 0)
                        {

                            _dbHelper.ExecuteNonQuery(sb2.ToString(), ref   errMsg, ref   bReturn);
                            sb2.Clear();
                        }


                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            resultMsg.Append("第" + (i + 1) + "行数据导入时发生错误。" + errMsg + "<br />");
                            //    new config().WritTxt(Content, strPath, FileName);
                            lost++;
                        }
                        else
                        {
                            //   resultMsg.Append("第" + (i + 1) + "行数据导入成功。 ");
                            //    new config().WritTxt(Content, strPath, FileName);
                            finish++;
                        }

                        //    resultMsg.Append("--------------------------------------------------- <br />");
                        //new config().WritTxt(Content, strPath, FileName);
                        i++;

                    }
                    catch (Exception ex)
                    {
                        resultMsg.Append("第" + (i + 1) + "行数据导入时发生错误。 ");
                        //    new config().WritTxt(Content, strPath, FileName);
                        lost++;
                        i++;
                        continue;
                    }
                }

                resultMsg.Append("共处理:" + i + "数据导入完毕。导入成功：" + finish + "条数据。导入失败：" + lost + "条数据。 ");

                //   new config().WritTxt(Content, strPath, FileName);
                // File.Delete(excelFilePath);

                return resultMsg.ToString();

            }
            catch (Exception ex)
            {
                resultMsg.Append("文件上传失败,请检查重新上传！" + ex);
                // new config().WritTxt(Content, strPath, FileName);
                return "";
            }

        }
        /// <summary>
        /// 根据报名号多个删除
        /// </summary>
        /// <param name="xqdms">报名号</param>
        /// <returns></returns>
        public bool DeleteData_Tjs2(List<string> ksh)
        {
            string inStr = "";

            foreach (var str in ksh)
            {
                inStr += "'" + str + "',";

            }

            string sqlCmd = "Delete zk_TJS_sl Where id In (" + inStr.Substring(0, inStr.Length - 1) + ") ";
            _dbHelper.ExecuteNonQuery(sqlCmd, ref error, ref bReturn);

            if (bReturn) return true;
            else return false;
        }



        public string ImportExcelData_Wjs(string excelFilePath)
        {
            string Content = "";
            string strPath = "";
            string FileName = "";
            StringBuilder resultMsg = new StringBuilder();
            try
            {



                if (!File.Exists(excelFilePath))
                    return "导入失败，原因是：输入的文件路径不正确或指定的文件不存在。";
                ExcelQueryFactory excelFile = new ExcelQueryFactory(excelFilePath);
                ExcelQueryable<Row> excel = excelFile.Worksheet(0);
                SqlDbHelper_1 dbHelp = new SqlDbHelper_1();
                string sqlCmd2 = "";
                string checkSql = "  select   ksh  from zk_TJS_wjs where ksh=@ksh ;select  ksh,xm  from zk_ksxxgl where ksh=@ksh ; ";

                StringBuilder sb2 = new StringBuilder();

                //Content = "";
                //strPath = "Temp";
                //FileName = config.Get_UserName + ".txt";
                //string mPath = HttpContext.Current.Server.MapPath("~\\" + strPath + "\\" + FileName + "");
                //if (File.Exists(mPath))
                //    File.Delete(mPath);


                bool bReturn = true;
                string errMsg = "";
                int i = 0;
                int lost = 0;
                int finish = 0;

                foreach (var element in excel)
                {
                    try
                    {

                        if (element.ColumnNames.Count() != 4)
                        {
                            resultMsg.Append("导入失败，原因是：导入的格式不正确,格式为4列，您导入的是：" + element.ColumnNames.Count() + "列的格式。");
                            //     new config().WritTxt(Content, strPath, FileName);
                            return resultMsg.ToString();
                        }

                        errMsg = "";
                        //  string baseStr = "";// "系统时间：" + DateTime.Now.ToString("yyyy年MM月dd日hh时mm分ss秒") + "。";

                        //空数据则跳过
                        if (string.IsNullOrEmpty(element[0].ToString().Trim()))
                            continue;



                        sqlCmd2 = "insert into zk_TJS_wjs(ksh,xm,xqdm,fs) values('" + element[0].ToString().Trim() + "', '" + element[1].ToString().Trim() + "', '" + element[2].ToString().Trim() + "', '" + element[3].ToString().Trim() + "') ; ";

                        List<SqlParameter> dataParams = new List<SqlParameter> {
                                              new SqlParameter("@ksh",element[0].ToString().Trim()),                                           
                                                new SqlParameter("@xm",element[1].ToString().Trim()) 
                                              };

                        DataSet tmpDataSet = _dbHelper.selectDataSet(checkSql, dataParams, ref error, ref bReturn);


                        //如果上面的验证都通过的话就继续执行添加（增量）或修改数据（非增量）
                        if (tmpDataSet.Tables[0] != null && tmpDataSet.Tables[0].Rows.Count > 0)
                        {

                            resultMsg.Append("第" + (i + 1) + "行数据有误，原因是：考生[" + element[0].ToString().Trim() + "]已存在名单内！<br />");
                            //      new config().WritTxt(Content, strPath, FileName);
                            lost++;
                            i++;
                            continue;

                        }
                        if (tmpDataSet.Tables[1] == null || tmpDataSet.Tables[1].Rows.Count == 0)
                        {
                            resultMsg.Append("第" + (i + 1) + "行数据有误，原因是：考生[" + element[0].ToString().Trim() + "]信息不存在！<br />");
                            lost++;
                            i++;
                            continue;

                        }
                        else
                        {
                            if (tmpDataSet.Tables[1].Rows[0]["xm"].ToString() != element[1].ToString().Trim())
                            {
                                resultMsg.Append("第" + (i + 1) + "行数据有误，原因是：考生姓名[" + element[0].ToString().Trim() + "]与报名号不对应！<br />");
                                lost++;
                                i++;
                                continue;
                            }
                        }
                        if (element[0].ToString().Trim().Length != 12)
                        {
                            resultMsg.Append("第" + (i + 1) + "行错误，原因：报名号【" + element[0].ToString() + "】不是12位。<br />");
                            //     new config().WritTxt(Content, strPath, FileName);
                            lost++;
                            i++;
                            continue;
                        }


                        sb2.Append(sqlCmd2);
                        if (sb2.Length > 0)
                        {

                            _dbHelper.ExecuteNonQuery(sb2.ToString(), ref   errMsg, ref   bReturn);
                            sb2.Clear();
                        }


                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            resultMsg.Append("第" + (i + 1) + "行数据导入时发生错误。" + errMsg + "<br />");
                            //    new config().WritTxt(Content, strPath, FileName);
                            lost++;
                        }
                        else
                        {
                            //   resultMsg.Append("第" + (i + 1) + "行数据导入成功。 ");
                            //    new config().WritTxt(Content, strPath, FileName);
                            finish++;
                        }

                        //    resultMsg.Append("--------------------------------------------------- <br />");
                        //new config().WritTxt(Content, strPath, FileName);
                        i++;

                    }
                    catch (Exception ex)
                    {
                        resultMsg.Append("第" + (i + 1) + "行数据导入时发生错误。 ");
                        //    new config().WritTxt(Content, strPath, FileName);
                        lost++;
                        i++;
                        continue;
                    }
                }

                resultMsg.Append("共处理:" + i + "数据导入完毕。导入成功：" + finish + "条数据。导入失败：" + lost + "条数据。 ");

                //   new config().WritTxt(Content, strPath, FileName);
                // File.Delete(excelFilePath);

                return resultMsg.ToString();

            }
            catch (Exception ex)
            {
                resultMsg.Append("文件上传失败,请检查重新上传！" + ex);
                // new config().WritTxt(Content, strPath, FileName);
                return "";
            }

        }
        /// <summary>
        /// 根据报名号多个删除
        /// </summary>
        /// <param name="xqdms">报名号</param>
        /// <returns></returns>
        public bool DeleteData_Tjs3(List<string> ksh)
        {
            string inStr = "";

            foreach (var str in ksh)
            {
                inStr += "'" + str + "',";

            }

            string sqlCmd = "Delete zk_TJS_wjs Where id In (" + inStr.Substring(0, inStr.Length - 1) + ") ";
            _dbHelper.ExecuteNonQuery(sqlCmd, ref error, ref bReturn);

            if (bReturn) return true;
            else return false;
        }




        public string ImportExcelData_Yt(string excelFilePath)
        {
            string Content = "";
            string strPath = "";
            string FileName = "";
            StringBuilder resultMsg = new StringBuilder();
            try
            {



                if (!File.Exists(excelFilePath))
                    return "导入失败，原因是：输入的文件路径不正确或指定的文件不存在。";
                ExcelQueryFactory excelFile = new ExcelQueryFactory(excelFilePath);
                ExcelQueryable<Row> excel = excelFile.Worksheet(0);
                SqlDbHelper_1 dbHelp = new SqlDbHelper_1();
                string sqlCmd2 = "";
                string checkSql = "  select   ksh  from zk_TJS_yt where ksh=@ksh ;select  ksh,xm  from zk_ksxxgl where ksh=@ksh ; ";

                StringBuilder sb2 = new StringBuilder();

                //Content = "";
                //strPath = "Temp";
                //FileName = config.Get_UserName + ".txt";
                //string mPath = HttpContext.Current.Server.MapPath("~\\" + strPath + "\\" + FileName + "");
                //if (File.Exists(mPath))
                //    File.Delete(mPath);


                bool bReturn = true;
                string errMsg = "";
                int i = 0;
                int lost = 0;
                int finish = 0;

                foreach (var element in excel)
                {
                    try
                    {

                        if (element.ColumnNames.Count() != 4)
                        {
                            resultMsg.Append("导入失败，原因是：导入的格式不正确,格式为4列，您导入的是：" + element.ColumnNames.Count() + "列的格式。");
                            //     new config().WritTxt(Content, strPath, FileName);
                            return resultMsg.ToString();
                        }

                        errMsg = "";
                        //  string baseStr = "";// "系统时间：" + DateTime.Now.ToString("yyyy年MM月dd日hh时mm分ss秒") + "。";

                        //空数据则跳过
                        if (string.IsNullOrEmpty(element[0].ToString().Trim()))
                            continue;



                        sqlCmd2 = "insert into zk_TJS_yt(ksh,xm,xqdm,fs) values('" + element[0].ToString().Trim() + "', '" + element[1].ToString().Trim() + "', '" + element[2].ToString().Trim() + "', '"  + element[3].ToString().Trim() + "') ; ";

                        List<SqlParameter> dataParams = new List<SqlParameter> {
                                              new SqlParameter("@ksh",element[0].ToString().Trim()),                                           
                                                new SqlParameter("@xm",element[1].ToString().Trim()) 
                                              };

                        DataSet tmpDataSet = _dbHelper.selectDataSet(checkSql, dataParams, ref error, ref bReturn);


                        //如果上面的验证都通过的话就继续执行添加（增量）或修改数据（非增量）
                        if (tmpDataSet.Tables[0] != null && tmpDataSet.Tables[0].Rows.Count > 0)
                        {

                            resultMsg.Append("第" + (i + 1) + "行数据有误，原因是：考生[" + element[0].ToString().Trim() + "]已存在名单内！<br />");
                            //      new config().WritTxt(Content, strPath, FileName);
                            lost++;
                            i++;
                            continue;

                        }
                        if (tmpDataSet.Tables[1] == null || tmpDataSet.Tables[1].Rows.Count == 0)
                        {
                            resultMsg.Append("第" + (i + 1) + "行数据有误，原因是：考生[" + element[0].ToString().Trim() + "]信息不存在！<br />");
                            lost++;
                            i++;
                            continue;

                        }
                        else
                        {
                            if (tmpDataSet.Tables[1].Rows[0]["xm"].ToString() != element[1].ToString().Trim())
                            {
                                resultMsg.Append("第" + (i + 1) + "行数据有误，原因是：考生姓名[" + element[0].ToString().Trim() + "]与报名号不对应！<br />");
                                lost++;
                                i++;
                                continue;
                            }
                        }
                        if (element[0].ToString().Trim().Length != 12)
                        {
                            resultMsg.Append("第" + (i + 1) + "行错误，原因：报名号【" + element[0].ToString() + "】不是12位。<br />");
                            //     new config().WritTxt(Content, strPath, FileName);
                            lost++;
                            i++;
                            continue;
                        }


                        sb2.Append(sqlCmd2);
                        if (sb2.Length > 0)
                        {

                            _dbHelper.ExecuteNonQuery(sb2.ToString(), ref   errMsg, ref   bReturn);
                            sb2.Clear();
                        }


                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            resultMsg.Append("第" + (i + 1) + "行数据导入时发生错误。" + errMsg + "<br />");
                            //    new config().WritTxt(Content, strPath, FileName);
                            lost++;
                        }
                        else
                        {
                            //   resultMsg.Append("第" + (i + 1) + "行数据导入成功。 ");
                            //    new config().WritTxt(Content, strPath, FileName);
                            finish++;
                        }

                        //    resultMsg.Append("--------------------------------------------------- <br />");
                        //new config().WritTxt(Content, strPath, FileName);
                        i++;

                    }
                    catch (Exception ex)
                    {
                        resultMsg.Append("第" + (i + 1) + "行数据导入时发生错误。 ");
                        //    new config().WritTxt(Content, strPath, FileName);
                        lost++;
                        i++;
                        continue;
                    }
                }

                resultMsg.Append("共处理:" + i + "数据导入完毕。导入成功：" + finish + "条数据。导入失败：" + lost + "条数据。 ");

                //   new config().WritTxt(Content, strPath, FileName);
                // File.Delete(excelFilePath);

                return resultMsg.ToString();

            }
            catch (Exception ex)
            {
                resultMsg.Append("文件上传失败,请检查重新上传！" + ex);
                // new config().WritTxt(Content, strPath, FileName);
                return "";
            }

        }
        /// <summary>
        /// 根据报名号多个删除
        /// </summary>
        /// <param name="xqdms">报名号</param>
        /// <returns></returns>
        public bool DeleteData_Tjs4(List<string> ksh)
        {
            string inStr = "";

            foreach (var str in ksh)
            {
                inStr += "'" + str + "',";

            }

            string sqlCmd = "Delete zk_TJS_yt Where id In (" + inStr.Substring(0, inStr.Length - 1) + ") ";
            _dbHelper.ExecuteNonQuery(sqlCmd, ref error, ref bReturn);

            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        ///  
        /// </summary>
        /// <param name="ksh"></param>
        /// <returns></returns>
        public DataTable Select_zk_TJS_sl(string xqdm)
        {
            Model_zk_kshkcj info = new Model_zk_kshkcj();
            string sql = " select * from zk_TJS_sl where xqdm=@xqdm ";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",xqdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            return dt;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="ksh"></param>
        /// <returns></returns>
        public DataTable Select_zk_kszkcjTOP(string where, string top)
        {
            Model_zk_kshkcj info = new Model_zk_kshkcj();
            string sql = " select top " + top + " a.*,b.xm from zk_kszkcj a left join zk_ksxxgl b on a.ksh=b.ksh where  " + where + " order by cj desc";
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);
            return dt;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Insert_zk_TJS_gr(string ksh, string xm, string xqdm, string tjxxdm, string fs)
        {
            string sql = "insert into zk_TJS_gr(ksh,xm,xqdm,tjxxdm,fs) values(@ksh,@xm,@xqdm,@tjxxdm,@fs)";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@ksh",ksh),
 			 new SqlParameter("@xqdm",xqdm),
              new SqlParameter("@tjxxdm",tjxxdm),
               new SqlParameter("@fs",fs),
              new SqlParameter("@xm",xm)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="ksh"></param>
        /// <returns></returns>
        public DataTable Select_zk_TJS_gr(string ksh,string tjxxdm)
        {
            Model_zk_kshkcj info = new Model_zk_kshkcj();
            string sql = " select * from zk_TJS_gr where ksh=@ksh and tjxxdm=@tjxxdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@ksh",ksh),	 new SqlParameter("@tjxxdm",tjxxdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            return dt;
        }


        public string ImportExcelData_Tdsd(string excelFilePath)
        {
            string Content = "";
            string strPath = "";
            string FileName = "";
            StringBuilder resultMsg = new StringBuilder();
            try
            {



                if (!File.Exists(excelFilePath))
                    return "导入失败，原因是：输入的文件路径不正确或指定的文件不存在。";
                ExcelQueryFactory excelFile = new ExcelQueryFactory(excelFilePath);
                ExcelQueryable<Row> excel = excelFile.Worksheet(0);
                SqlDbHelper_1 dbHelp = new SqlDbHelper_1();
                string sqlCmd2 = "";
                string checkSql = "  select   ksh  from zk_TJS_TDSD where ksh=@ksh ;select  ksh,xm  from zk_ksxxgl where ksh=@ksh ; ";

                StringBuilder sb2 = new StringBuilder();

                //Content = "";
                //strPath = "Temp";
                //FileName = config.Get_UserName + ".txt";
                //string mPath = HttpContext.Current.Server.MapPath("~\\" + strPath + "\\" + FileName + "");
                //if (File.Exists(mPath))
                //    File.Delete(mPath);


                bool bReturn = true;
                string errMsg = "";
                int i = 0;
                int lost = 0;
                int finish = 0;

                foreach (var element in excel)
                {
                    try
                    {

                        if (element.ColumnNames.Count() != 3)
                        {
                            resultMsg.Append("导入失败，原因是：导入的格式不正确,格式为3列，您导入的是：" + element.ColumnNames.Count() + "列的格式。");
                            //     new config().WritTxt(Content, strPath, FileName);
                            return resultMsg.ToString();
                        }

                        errMsg = "";
                        //  string baseStr = "";// "系统时间：" + DateTime.Now.ToString("yyyy年MM月dd日hh时mm分ss秒") + "。";

                        //空数据则跳过
                        if (string.IsNullOrEmpty(element[0].ToString().Trim()))
                            continue;



                        sqlCmd2 = "insert into zk_TJS_TDSD(ksh,xm,xqdm) values('" + element[0].ToString().Trim() + "', '" + element[1].ToString().Trim() + "', '" + element[2].ToString().Trim() + "') ;update zk_lqk set td_zt=6 where  isnull(td_zt,0)=0 and ksh='" + element[0].ToString().Trim() + "'";

                        List<SqlParameter> dataParams = new List<SqlParameter> {
                                              new SqlParameter("@ksh",element[0].ToString().Trim()),                                           
                                                new SqlParameter("@xm",element[1].ToString().Trim()) 
                                              };

                        DataSet tmpDataSet = _dbHelper.selectDataSet(checkSql, dataParams, ref error, ref bReturn);


                        //如果上面的验证都通过的话就继续执行添加（增量）或修改数据（非增量）
                        if (tmpDataSet.Tables[0] != null && tmpDataSet.Tables[0].Rows.Count > 0)
                        {

                            resultMsg.Append("第" + (i + 1) + "行数据有误，原因是：考生[" + element[0].ToString().Trim() + "]已存在名单内！<br />");
                            //      new config().WritTxt(Content, strPath, FileName);
                            lost++;
                            i++;
                            continue;

                        }
                        if (tmpDataSet.Tables[1] == null || tmpDataSet.Tables[1].Rows.Count == 0)
                        {
                            resultMsg.Append("第" + (i + 1) + "行数据有误，原因是：考生[" + element[0].ToString().Trim() + "]信息不存在！<br />");
                            lost++;
                            i++;
                            continue;

                        }
                        else
                        {
                            if (tmpDataSet.Tables[1].Rows[0]["xm"].ToString() != element[1].ToString().Trim())
                            {
                                resultMsg.Append("第" + (i + 1) + "行数据有误，原因是：考生姓名[" + element[0].ToString().Trim() + "]与报名号不对应！<br />");
                                lost++;
                                i++;
                                continue;
                            }
                        }
                        if (element[0].ToString().Trim().Length != 12)
                        {
                            resultMsg.Append("第" + (i + 1) + "行错误，原因：报名号【" + element[0].ToString() + "】不是12位。<br />");
                            //     new config().WritTxt(Content, strPath, FileName);
                            lost++;
                            i++;
                            continue;
                        }


                        sb2.Append(sqlCmd2);
                        if (sb2.Length > 0)
                        {

                            _dbHelper.ExecuteNonQuery(sb2.ToString(), ref   errMsg, ref   bReturn);
                            sb2.Clear();
                        }


                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            resultMsg.Append("第" + (i + 1) + "行数据导入时发生错误。" + errMsg + "<br />");
                            //    new config().WritTxt(Content, strPath, FileName);
                            lost++;
                        }
                        else
                        {
                            //   resultMsg.Append("第" + (i + 1) + "行数据导入成功。 ");
                            //    new config().WritTxt(Content, strPath, FileName);
                            finish++;
                        }

                        //    resultMsg.Append("--------------------------------------------------- <br />");
                        //new config().WritTxt(Content, strPath, FileName);
                        i++;

                    }
                    catch (Exception ex)
                    {
                        resultMsg.Append("第" + (i + 1) + "行数据导入时发生错误。 ");
                        //    new config().WritTxt(Content, strPath, FileName);
                        lost++;
                        i++;
                        continue;
                    }
                }

                resultMsg.Append("共处理:" + i + "数据导入完毕。导入成功：" + finish + "条数据。导入失败：" + lost + "条数据。 ");

                //   new config().WritTxt(Content, strPath, FileName);
                // File.Delete(excelFilePath);

                return resultMsg.ToString();

            }
            catch (Exception ex)
            {
                resultMsg.Append("文件上传失败,请检查重新上传！" + ex);
                // new config().WritTxt(Content, strPath, FileName);
                return "";
            }

        }
        /// <summary>
        /// 根据报名号多个删除
        /// </summary>
        /// <param name="xqdms">报名号</param>
        /// <returns></returns>
        public bool DeleteData_Tjs5(List<string> ksh)
        {
            string inStr = "";

            foreach (var str in ksh)
            {
                inStr += "'" + str + "',";

            }
            _dbHelper.BeginTran();
            string sql = " update zk_lqk set td_zt=0 where   ksh in (select ksh from zk_TJS_TDSD Where id In (" + inStr.Substring(0, inStr.Length - 1) + ")  )";
            string sqlCmd = "Delete zk_TJS_TDSD Where id In (" + inStr.Substring(0, inStr.Length - 1) + ") ";
            try
            {
                int iCount = 0;
                iCount = _dbHelper.execSql_Tran(sql);
            
                if (iCount > 0)
                {
                    if (_dbHelper.execSql_Tran(sqlCmd)>0)
                    {
                        _dbHelper.EndTran(true);
                        return true;
                    } 
                }

                _dbHelper.EndTran(false);
                return false;
            }
            catch (Exception exe)
            {
                _dbHelper.EndTran(false);
                return false;
            }
            return false;
        }

        public DataTable Execute_TJS5(string where, int pageSize, int pageIndex, ref int totalRecord)
        {
            //string sql="select  from AppraisalPersonnel";

            //数据库表名
            string tabName = "  zk_TJS_TDSD   ";
            //要查询的字段
            string reField = "  * ";
            //排序字段
            string orderStr = " ksh  ";
            //排序标识（0、升序；1、降序）
            int orderType = 0;
            //执行成功与失败的标识（true、表示执行成功；false、表示执行失败）
            bool bFlag = false;
            decimal dec = 0;
            DataSet dst = _dbHelper.ExecuteProc(tabName, reField, orderStr, "", where, pageSize, pageIndex, orderType, 0, ref dec, ref totalRecord, ref bFlag);
            if (bFlag)
            {
                if (dst.Tables.Count > 0)
                {
                    return dst.Tables[0];
                }
            }
            return null;
        }

        public DataTable Execute_TJS6(string where, int pageSize, int pageIndex, ref int totalRecord)
        {
            //string sql="select  from AppraisalPersonnel";

            //数据库表名
            string tabName = "  zk_TJS_XXSD   ";
            //要查询的字段
            string reField = "  * ";
            //排序字段
            string orderStr = " xxdm  ";
            //排序标识（0、升序；1、降序）
            int orderType = 0;
            //执行成功与失败的标识（true、表示执行成功；false、表示执行失败）
            bool bFlag = false;
            decimal dec = 0;
            DataSet dst = _dbHelper.ExecuteProc(tabName, reField, orderStr, "", where, pageSize, pageIndex, orderType, 0, ref dec, ref totalRecord, ref bFlag);
            if (bFlag)
            {
                if (dst.Tables.Count > 0)
                {
                    return dst.Tables[0];
                }
            }
            return null;
        }
        public string ImportExcelData_XXsd(string excelFilePath)
        {
            string Content = "";
            string strPath = "";
            string FileName = "";
            StringBuilder resultMsg = new StringBuilder();
            try
            {



                if (!File.Exists(excelFilePath))
                    return "导入失败，原因是：输入的文件路径不正确或指定的文件不存在。";
                ExcelQueryFactory excelFile = new ExcelQueryFactory(excelFilePath);
                ExcelQueryable<Row> excel = excelFile.Worksheet(0);
                SqlDbHelper_1 dbHelp = new SqlDbHelper_1();
                string sqlCmd2 = "";
                string checkSql = "  select   *  from zk_TJS_XXSD where xxdm=@xxdm ;  ";

                StringBuilder sb2 = new StringBuilder();

                //Content = "";
                //strPath = "Temp";
                //FileName = config.Get_UserName + ".txt";
                //string mPath = HttpContext.Current.Server.MapPath("~\\" + strPath + "\\" + FileName + "");
                //if (File.Exists(mPath))
                //    File.Delete(mPath);


                bool bReturn = true;
                string errMsg = "";
                int i = 0;
                int lost = 0;
                int finish = 0;

                foreach (var element in excel)
                {
                    try
                    {

                        if (element.ColumnNames.Count() != 2)
                        {
                            resultMsg.Append("导入失败，原因是：导入的格式不正确,格式为2列，您导入的是：" + element.ColumnNames.Count() + "列的格式。");
                            //     new config().WritTxt(Content, strPath, FileName);
                            return resultMsg.ToString();
                        }

                        errMsg = "";
                        //  string baseStr = "";// "系统时间：" + DateTime.Now.ToString("yyyy年MM月dd日hh时mm分ss秒") + "。";

                        //空数据则跳过
                        if (string.IsNullOrEmpty(element[0].ToString().Trim()))
                            continue;



                        sqlCmd2 = "insert into zk_TJS_XXSD(xxdm,xxmc) values('" + element[0].ToString().Trim() + "', '" + element[1].ToString().Trim() + "') ; ";

                        List<SqlParameter> dataParams = new List<SqlParameter> {
                                              new SqlParameter("@xxdm",element[0].ToString().Trim()) 
                                              };

                        DataSet tmpDataSet = _dbHelper.selectDataSet(checkSql, dataParams, ref error, ref bReturn);


                        //如果上面的验证都通过的话就继续执行添加（增量）或修改数据（非增量）
                        if (tmpDataSet.Tables[0] != null && tmpDataSet.Tables[0].Rows.Count > 0)
                        {

                            resultMsg.Append("第" + (i + 1) + "行数据有误，原因是：招生学校[" + element[0].ToString().Trim() + "]已存在名单内！<br />");
                            //      new config().WritTxt(Content, strPath, FileName);
                            lost++;
                            i++;
                            continue;

                        }
                        sb2.Append(sqlCmd2);
                        if (sb2.Length > 0)
                        {

                            _dbHelper.ExecuteNonQuery(sb2.ToString(), ref   errMsg, ref   bReturn);
                            sb2.Clear();
                        }


                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            resultMsg.Append("第" + (i + 1) + "行数据导入时发生错误。" + errMsg + "<br />");
                        
                            lost++;
                        }
                        else
                        {
                        
                            finish++;
                        }
                 
                        i++;

                    }
                    catch (Exception ex)
                    {
                        resultMsg.Append("第" + (i + 1) + "行数据导入时发生错误。 ");
                        //    new config().WritTxt(Content, strPath, FileName);
                        lost++;
                        i++;
                        continue;
                    }
                }

                resultMsg.Append("共处理:" + i + "数据导入完毕。导入成功：" + finish + "条数据。导入失败：" + lost + "条数据。 ");

                //   new config().WritTxt(Content, strPath, FileName);
                // File.Delete(excelFilePath);

                return resultMsg.ToString();

            }
            catch (Exception ex)
            {
                resultMsg.Append("文件上传失败,请检查重新上传！" + ex);
                // new config().WritTxt(Content, strPath, FileName);
                return "";
            }

        }

        /// <summary>
        /// 根据报名号多个删除
        /// </summary>
        /// <param name="xqdms">报名号</param>
        /// <returns></returns>
        public bool DeleteData_Tjs6(List<string> ksh)
        {
            string inStr = "";

            foreach (var str in ksh)
            {
                inStr += "'" + str + "',";

            }
            _dbHelper.BeginTran();
            string sqlCmd = "Delete zk_TJS_XXSD Where id In (" + inStr.Substring(0, inStr.Length - 1) + ") ";
            try
            {
         
            
                    if (_dbHelper.execSql_Tran(sqlCmd) > 0)
                    {
                        _dbHelper.EndTran(true);
                        return true;
                    }
           
                _dbHelper.EndTran(false);
                return false;
            }
            catch (Exception exe)
            {
                _dbHelper.EndTran(false);
                return false;
            }
            return false;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Insert_zk_TJS_XXSD(string xxdm, string xxmc)
        {
            string sql = "insert into zk_TJS_XXSD(xxdm,xxmc) values(@xxdm,@xxmc)";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xxdm",xxdm),
 			 new SqlParameter("@xxmc",xxmc) 
           
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 根据报名号多个删除
        /// </summary>
        /// <param name="xqdms">报名号</param>
        /// <returns></returns>
        public bool DeleteData_zk_TJS_XXSD(string xxdm)
        {
        
            _dbHelper.BeginTran();
            string sqlCmd = "Delete zk_TJS_XXSD Where xxdm in (" + xxdm + ")";
            try
            {


                if (_dbHelper.execSql_Tran(sqlCmd) > 0)
                {
                    _dbHelper.EndTran(true);
                    return true;
                }

                _dbHelper.EndTran(false);
                return false;
            }
            catch (Exception exe)
            {
                _dbHelper.EndTran(false);
                return false;
            }
           
        }
        public DataTable Execute_ZBSXX(string where, int pageSize, int pageIndex, ref int totalRecord)
        {
            //string sql="select  from AppraisalPersonnel";

            //数据库表名
            string tabName = "  View_ZBSXX   ";
            //要查询的字段
            string reField = "  * ";
            //排序字段
            string orderStr = " xxdm,zsxxdm  ";
            //排序标识（0、升序；1、降序）
            int orderType = 0;
            //执行成功与失败的标识（true、表示执行成功；false、表示执行失败）
            bool bFlag = false;
            decimal dec = 0;
            DataSet dst = _dbHelper.ExecuteProc(tabName, reField, orderStr, "", where, pageSize, pageIndex, orderType, 0, ref dec, ref totalRecord, ref bFlag);
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
        /// 删除多个指标生
        /// </summary>
        /// <returns></returns>
        public bool DelData_ZBS(List<string> lsh)
        {
            string inStr = "";

            foreach (var str in lsh)
            {
                inStr += "'" + str + "',";

            }

            string sqlCmd = "Delete zk_zbsxx Where lsh In (" + inStr.Substring(0, inStr.Length - 1) + ") ";
            _dbHelper.ExecuteNonQuery(sqlCmd, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
    }
}
