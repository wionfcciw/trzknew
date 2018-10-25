using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DAL;
using Model;
using System.IO;
using LinqToExcel;
using LinqToExcel.Query;
namespace BLL
{
  public  class BLL_zk_zsjh
    {
      private SqlDbHelper_1 _dbHelper = new SqlDbHelper_1();
        private string error = "";
        private bool bReturn = false;
      /// <summary>
      /// 新增
      /// </summary>
      /// <param name="item"></param>
      /// <param name="error"></param>
      /// <param name="bReturn"></param>
      /// <returns></returns>
        public bool Insert_zk_zsjh(Model_zk_zsjh item)
        {
            string sql = "insert into zk_zsjh(xqdm,xxdm,zydm,xzdm,jhs,pcdm,xxlbdm,bz) values(@xqdm,@xxdm,@zydm,@xzdm,@jhs,@pcdm,@xxlbdm,@bz)";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@lsh",item.Lsh),
 			 new SqlParameter("@xqdm",item.Xqdm),
 			 new SqlParameter("@xxdm",item.Xxdm),
 			 new SqlParameter("@zydm",item.Zydm),
 			 new SqlParameter("@xzdm",item.Xzdm),
 			 new SqlParameter("@jhs",item.Jhs),
 			 new SqlParameter("@pcdm",item.Pcdm),
 			 new SqlParameter("@xxlbdm",item.Xxlbdm),
 			 new SqlParameter("@bz",item.Bz)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
      /// <summary>
      /// 批量新增
      /// </summary>
      /// <param name="Listitem"></param>
      /// <param name="error"></param>
      /// <param name="bReturn"></param>
      /// <returns></returns>
        public bool Insert_zk_zsjh(List<Model_zk_zsjh> Listitem)
        {
            string sql = "insert into zk_zsjh(xqdm,xxdm,zydm,xzdm,jhs,pcdm,xxlbdm,bz) values(@xqdm,@xxdm,@zydm,@xzdm,@jhs,@pcdm,@xxlbdm,@bz)";
            List<SqlParameter> lisP = new List<SqlParameter>();
            //SqlParameter Lsh = new SqlParameter("@lsh", SqlDbType.Int);
            SqlParameter Xqdm = new SqlParameter("@xqdm", SqlDbType.VarChar);
            SqlParameter Xxdm = new SqlParameter("@xxdm", SqlDbType.VarChar);
            SqlParameter Zydm = new SqlParameter("@zydm", SqlDbType.VarChar);
            SqlParameter Xzdm = new SqlParameter("@xzdm", SqlDbType.VarChar);
            SqlParameter Jhs = new SqlParameter("@jhs", SqlDbType.Int);
            SqlParameter Pcdm = new SqlParameter("@pcdm", SqlDbType.VarChar);
            SqlParameter Xxlbdm = new SqlParameter("@xxlbdm", SqlDbType.VarChar);
            SqlParameter Bz = new SqlParameter("@bz", SqlDbType.VarChar);
            foreach (Model_zk_zsjh item in Listitem)
            {
                //Lsh.Value = item.Lsh;
                Xqdm.Value = item.Xqdm;
                Xxdm.Value = item.Xxdm;
                Zydm.Value = item.Zydm;
                Xzdm.Value = item.Xzdm;
                Jhs.Value = item.Jhs;
                Pcdm.Value = item.Pcdm;
                Xxlbdm.Value = item.Xxlbdm;
                Bz.Value = item.Bz;
                lisP.Clear();
                //lisP.Add(Lsh);
                lisP.Add(Xqdm);
                lisP.Add(Xxdm);
                lisP.Add(Zydm);
                lisP.Add(Xzdm);
                lisP.Add(Jhs);
                lisP.Add(Pcdm);
                lisP.Add(Xxlbdm);
                lisP.Add(Bz);
                _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            }
            if (bReturn) return true;
            else return false;
        }
      /// <summary>
      /// 查询所有
      /// </summary>
      /// <param name="error"></param>
      /// <param name="bReturn"></param>
      /// <returns></returns>
        public DataTable Select_zk_zsjh(ref string error, ref bool bReturn)
        {
            string sql = "select * from zk_zsjh";
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);
            return dt;
        }

      /// <summary>
      /// 根据流水号查询
      /// </summary>
      /// <param name="ksh"></param>
      /// <param name="error"></param>
      /// <param name="bReturn"></param>
      /// <returns></returns>
        public Model_zk_zsjh Select_zk_zsjh(string lsh)
        {
            Model_zk_zsjh info = new Model_zk_zsjh();
            string sql = "select * from zk_zsjh where lsh=@lsh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@lsh",lsh)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
                info = _dbHelper.DT2EntityList<Model_zk_zsjh>(dt)[0];
            return info;
        }
      /// <summary>
      /// 根据县区查询
      /// </summary>
      /// <param name="xqdm"></param>
      /// <param name="error"></param>
      /// <param name="bReturn"></param>
      /// <returns></returns>
        public DataTable Select_zk_zsjhXQ(string xqdm)
        {
            Model_zk_zsjh info = new Model_zk_zsjh();
            string sql = "select * from zk_zsjh where xqdm=@xqdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",xqdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            
            return dt;
        }
      /// <summary>
      /// 自定义修改
      /// </summary>
      /// <param name="set"></param>
      /// <param name="where"></param>
      /// <param name="error"></param>
      /// <param name="bReturn"></param>
      /// <returns></returns>
        public bool update_zk_zsjh(string set, string where)
        {
            string sql = "update  zk_zsjh set " + set + " where " + where;
            _dbHelper.ExecuteNonQuery(sql, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
      /// <summary>
      /// 根据流水号修改
      /// </summary>
      /// <param name="item"></param>
      /// <param name="error"></param>
      /// <param name="bReturn"></param>
      /// <returns></returns>
        public bool update_zk_zsjh(Model_zk_zsjh item)
        {
            string sql = "update  zk_zsjh set xqdm=@xqdm,xxdm=@xxdm,zydm=@zydm,xzdm=@xzdm,jhs=@jhs,pcdm=@pcdm,xxlbdm=@xxlbdm,bz=@bz where lsh=@lsh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",item.Xqdm),
 			 new SqlParameter("@xxdm",item.Xxdm),
 			 new SqlParameter("@zydm",item.Zydm),
 			 new SqlParameter("@xzdm",item.Xzdm),
 			 new SqlParameter("@jhs",item.Jhs),
 			 new SqlParameter("@pcdm",item.Pcdm),
 			 new SqlParameter("@xxlbdm",item.Xxlbdm),
 			 new SqlParameter("@bz",item.Bz),
 			 new SqlParameter("@lsh",item.Lsh)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
      /// <summary>
      /// 根据流水号删除
      /// </summary>
      /// <param name="lsh"></param>
      /// <param name="error"></param>
      /// <param name="bReturn"></param>
      /// <returns></returns>
        public bool delete_zk_zsjh(string lsh)
        {
            string sql = "delete  zk_zsjh where lsh=@lsh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@lsh",lsh)};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
      /// <summary>
      /// 根据县区删除
      /// </summary>
      /// <param name="xqdm"></param>
      /// <param name="error"></param>
      /// <param name="bReturn"></param>
      /// <returns></returns>
        public bool delete_zk_zsjhXQ(string xqdm)
        {
            string sql = "delete  zk_zsjh where xqdm=@xqdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",xqdm)};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }

        /// <summary>
        /// 根据数据标识查询 返回实体类
        /// </summary> 
        public Model_zk_zsjh Disp(string lsh)
        {
            Model_zk_zsjh info = new Model_zk_zsjh();
            string sql = "select * from zk_zsjh where lsh=@lsh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@lsh",lsh)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
                info = _dbHelper.DT2EntityList<Model_zk_zsjh>(dt)[0];
            return info;
        }

          /// <summary>
          /// 依据id集合删除数据
          /// </summary>
          /// <param name="IDS"></param>
          /// <returns></returns>
        public bool DeleteDataByIDS(List<string> IDS)
        {
            string inStr = "";

            foreach (var str in IDS)
                inStr += str + ",";

            string sqlCmd = "Delete zk_zsjh Where lsh In (" + inStr.Substring(0, inStr.Length - 1) + ")";
            _dbHelper.ExecuteNonQuery(sqlCmd, ref error, ref bReturn);

            if (bReturn) return true;
            else return false;
        }

        #region 执行分页存储过程，返回记录总数和当前页的数据。
        /// <summary>
        /// 执行分页存储过程，返回记录总数和当前页的数据。
        /// </summary>
        /// <param name="where">执行的条件</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="totalRecord">返回的记录总数</param>
        public DataTable ExecuteProc(string where, int pageSize, int pageIndex, ref int totalRecord)
        {
            //string sql="select  from AppraisalPersonnel";

            //数据库表名
            string tabName = " View_zsjh ";
            //要查询的字段
            string reField = " * ";
            //排序字段
            string orderStr = "pcdm asc , xxdm asc ,zydm ";
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

        #endregion 

        private string xxdm = "";
        #region Excel数据导入到SqlServer数据库方法

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

            string sqlCmd = "Insert Into zk_zsjh(xqdm,xxdm,zydm,xzdm,jhs,pcdm,bz)Values(@xqdm,@xxdm,@zydm,@xzdm,@jhs,@pcdm,@bz)";
            string checkSql = "Select xqdm From zk_xqdm Where xqdm=@xqdm;Select zsxxdm From zk_zsxxdm Where zsxxdm=@xxdm;";
            checkSql += "Select zydm From zk_zyk Where xxdm=@xxdm And zydm=@zydm;Select xzdm From View_XZ Where xzdm=@xzdm;";
            checkSql += "Select lsh From zk_zsjh Where xqdm=@xqdm And xxdm=@xxdm And zydm=@zydm And pcdm=@pcdm;";
            bool bReturn = true;

            int i = 0;
            int lost = 0;
            int finish = 0;
            try
            {
                foreach (var element in excel)
                {
                    if (element.ColumnNames.Count() != 7)
                        return "导入失败，文件格式不是7列。";

                    string errMsg = "";
                    string baseStr = "";


                    //空数据则跳过
                    if (string.IsNullOrEmpty(element[0].ToString().Trim()))
                        continue;

                    List<SqlParameter> param = new List<SqlParameter> {
                                                new SqlParameter("@xqdm",element[0].ToString().Trim()),
                                                new SqlParameter("@xxdm",element[1].ToString().Trim()),
                                                new SqlParameter("@zydm",element[2].ToString().Trim()),
                                                new SqlParameter("@xzdm",element[3].ToString().Trim()),
                                                new SqlParameter("@jhs",element[4].ToString().Trim()),
                                                new SqlParameter("@pcdm",element[5].ToString().Trim()),                                             
                                                new SqlParameter("@bz",element[6].ToString().Trim())  };

                    //table顺序：0县区，1招生学校，2专业，3学制，4是否相同数据
                    DataSet tmpDataSet = _dbHelper.selectDataSet(checkSql, param, ref errMsg, ref bReturn);

                    if (!IsNumeric(element[4].ToString().Trim()))
                        errMsg = "第" + (i + 1) + "条数据有误，原因是：计划数不是数字：" + element[4].ToString().Trim() + "。<br /> ";
                    //需要导入的县区是否已存在
                    if (tmpDataSet.Tables[0].Rows.Count == 0)
                        errMsg += "第" + (i + 1) + "条数据有误，原因是：该县区不存：" + element[0].ToString().Trim() + "。<br />";

                    //需要导入的学校是否已存在
                    if (tmpDataSet.Tables[1].Rows.Count == 0)
                        errMsg += "第" + (i + 1) + "条数据有误，原因是：学校代码不存在：" + element[1].ToString().Trim() + "。<br />";

                    if (element[2].ToString().Trim().Length > 0)
                    {
                        //需要导入的专业是否已存在
                        if (tmpDataSet.Tables[2].Rows.Count == 0)
                            errMsg += "第" + (i + 1) + "条数据有误，原因是：该专业不存：" + element[2].ToString().Trim() + "。<br />";
                    }

                    if (element[3].ToString().Trim().Length > 0)
                    {
                        //需要导入的学制是否已存在
                        if (tmpDataSet.Tables[3].Rows.Count == 0)
                            errMsg += "第" + (i + 1) + "条数据有误，原因是：学制不存：" + element[3].ToString().Trim() + "。<br />";
                    }
                    //批次代码
                    if (element[5].ToString().Trim().Length == 0)
                        errMsg += "第" + (i + 1) + "条数据有误，批次代码不能为空。<br />";

                    //数据是否相同
                    if (tmpDataSet.Tables[4].Rows.Count > 0)
                        errMsg += "第" + (i + 1) + "条数据有误，该学校的招生计划已存在。<br />";


                    //如果上面的验证都通过的话就继续执行添加（增量）或修改数据（非增量）
                    if (string.IsNullOrEmpty(errMsg))
                    {
                        _dbHelper.ExecuteNonQuery(sqlCmd, param, ref errMsg, ref bReturn);
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
                  //      resultMsg.Append(baseStr + "第" + (i + 1) + "行数据导入成功。<br />");
                        finish++;
                    }





                 //   resultMsg.Append("---------------------------------------------------<br /><br />");
                    i++;
                }
            }
            catch (Exception ex)
            {
                resultMsg.Append("第" + (i + 1) + "行数据导入时发生错误。<br />");
                _dbHelper.writeErrorInfo(ex.Message);
            }

            resultMsg.Append("共处理:" + i + "数据导入完毕。共成功导入：" + finish + "条数据。导入失败：" + lost + "条数据。<br />");

            File.Delete(excelFilePath);

            return resultMsg.ToString();
           
        }

        #endregion

        bool IsNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg1
            = new System.Text.RegularExpressions.Regex(@"^[-]?\d+[.]?\d*$");
            return reg1.IsMatch(str);
        }
        /// <summary>
        /// 导出数据
        /// </summary> 
        public DataTable ExportData()
        {
            Model_zk_zsjh info = new Model_zk_zsjh();
            string sql = "  select * from View_zsjh order by pcdm asc ,xxdm asc ,zydm asc";
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);

            return dt;
        }


        #region 执行分页存储过程，返回记录总数和当前页的数据。
        /// <summary>
        /// 执行分页存储过程，返回记录总数和当前页的数据。
        /// </summary>
        /// <param name="where">执行的条件</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="totalRecord">返回的记录总数</param>
        public DataTable ExecuteProc_View_zk_lqjhk(string where, int pageSize, int pageIndex, ref int totalRecord)
        {
            //string sql="select  from AppraisalPersonnel";

            //数据库表名
            string tabName = " View_zk_lqjhk ";
            //要查询的字段
            string reField = " * ";
            //排序字段
            string orderStr = "pcdm asc , xxdm asc ,zydm ";
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

        #endregion 
        /// <summary>
        /// 根据数据标识查询 返回实体类
        /// </summary> 
        public Model_zk_zsjh Disp_zk_lqjhk(string lsh)
        {
            Model_zk_zsjh info = new Model_zk_zsjh();
            string sql = "select * from zk_lqjhk where lsh=@lsh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@lsh",lsh)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
                info = _dbHelper.DT2EntityList<Model_zk_zsjh>(dt)[0];
            return info;
        }

        /// <summary>
        /// 根据流水号修改
        /// </summary>
        /// <param name="item"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool update_zk_lqjhk(Model_zk_zsjh item)
        {
            string sql = "update  zk_lqjhk set xqdm=@xqdm,xxdm=@xxdm,zydm=@zydm,xzdm=@xzdm,jhs=@jhs,pcdm=@pcdm  where lsh=@lsh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",item.Xqdm),
 			 new SqlParameter("@xxdm",item.Xxdm),
 			 new SqlParameter("@zydm",item.Zydm),
 			 new SqlParameter("@xzdm",item.Xzdm),
 			 new SqlParameter("@jhs",item.Jhs),
 			 new SqlParameter("@pcdm",item.Pcdm),
 		 
 			 new SqlParameter("@lsh",item.Lsh)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }


        public DataTable GetTableDatas()
        {
            string sql = @"SELECT a.*,b.zsxxmc FROM zk_zsjh_xq a left join zk_zsxxdm b on a.xxdm=b.zsxxdm order by xxdm,xqdm";
            string Eree = "";
            bool ispass = false;
            DataTable dt = _dbHelper.selectTab(sql, ref Eree, ref ispass);
            return dt;
        }

        public DataTable GetRowUpdate(string id)
        {
            string sql = @"SELECT * FROM zk_zsjh_xq where id=" + id;
            string Eree = "";
            bool ispass = false;
            DataTable dt = _dbHelper.selectTab(sql, ref Eree, ref ispass);
            return dt;
        }

        public bool UpdateJhs(string id, string jhs, string xqdm,string xxdm)
        {
            try
            {
                string sql = "UPDATE dbo.zk_zsjh_xq SET jhs=" + jhs + ",xqdm='" + xqdm + "',xxdm='" + xxdm + "' WHERE id=" + id;
                string Eree = "";
                bool ispass = false;
                _dbHelper.ExecuteNonQuery(sql, ref Eree, ref ispass);
                return ispass;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool DeleteJhs(string id)
        {
            try
            {
                string sql = "DELETE FROM zk_zsjh_xq WHERE id=" + id;
                string Eree = "";
                bool ispass = false;
                _dbHelper.ExecuteNonQuery(sql, ref Eree, ref ispass);
                return ispass;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        /// <summary>
        /// 依据id集合删除数据
        /// </summary>
        /// <param name="IDS"></param>
        /// <returns></returns>
        public bool DeleteDataByIDS_jh(List<string> IDS)
        {
            string inStr = "";

            foreach (var str in IDS)
                inStr += str + ",";

            string sqlCmd = "Delete zk_zsjh_xq Where id In (" + inStr.Substring(0, inStr.Length - 1) + ")";
            _dbHelper.ExecuteNonQuery(sqlCmd, ref error, ref bReturn);

            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="item"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool Insert_zk_zsjh_xq(Model_zk_zsjh item)
        {
            string sql = "insert into zk_zsjh_xq(xqdm,xxdm,jhs,pcdm) values(@xqdm,@xxdm,@jhs,@pcdm)";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 		 
 			 new SqlParameter("@xqdm",item.Xqdm),
 			 new SqlParameter("@xxdm",item.Xxdm),
 	 
 			 new SqlParameter("@jhs",item.Jhs),
 			 new SqlParameter("@pcdm",item.Pcdm) 
 		 
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
    }
}
