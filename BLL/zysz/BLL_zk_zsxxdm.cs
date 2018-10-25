using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model;
using System.Data.SqlClient;
using System.Data;
using LinqToExcel;
using System.IO;
using LinqToExcel.Query;
namespace BLL
{
    /// <summary>
    /// 招生学校 控制类
    /// </summary>
  public  class BLL_zk_zsxxdm
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
        public bool Insert_zk_zsxxdm(Model_zk_zsxxdm item)
        {
            string sql = "insert into zk_zsxxdm(zsxxdm,zsxxmc,bz) values(@zsxxdm,@zsxxmc,@bz)";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@zsxxdm",item.Zsxxdm),
 			 new SqlParameter("@zsxxmc",item.Zsxxmc),
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
        public bool Insert_zk_zsxxdm(List<Model_zk_zsxxdm> Listitem)
        {
            //string sql = "insert into zk_zsxxdm(pcdm,xxdm,zsqxdm,zsjhs) values(@pcdm,@xxdm,@zsqxdm,@zsjhs)";
            //List<SqlParameter> lisP = new List<SqlParameter>();
            //SqlParameter Pcdm = new SqlParameter("@pcdm", SqlDbType.VarChar);
            //SqlParameter Xxdm = new SqlParameter("@xxdm", SqlDbType.VarChar);
            //SqlParameter Zsqxdm = new SqlParameter("@zsqxdm", SqlDbType.VarChar);
            //SqlParameter Zsjhs = new SqlParameter("@zsjhs", SqlDbType.Int);
            //foreach (Model_zk_zsxxdm item in Listitem)
            //{
            //    Pcdm.Value = item.Pcdm;
            //    Xxdm.Value = item.Xxdm;
            //    Zsqxdm.Value = item.Zsqxdm;
            //    Zsjhs.Value = item.Zsjhs;
            //    lisP.Clear();
            //    lisP.Add(Pcdm);
            //    lisP.Add(Xxdm);
            //    lisP.Add(Zsqxdm);
            //    lisP.Add(Zsjhs);
            //    _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            //}
            //if (bReturn) return true;
            //else return false;
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
            string sql = "select zsxxdm,zsxxmcc='['+zsxxdm+']'+zsxxmc,zsxxmc from zk_zsxxdm order by zsxxdm ";
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);
            return dt;
        }
      /// <summary>
      /// 根据批次代码查询
      /// </summary>
      /// <param name="pcdm"></param>
      /// <param name="error"></param>
      /// <param name="bReturn"></param>
      /// <returns></returns>
        public DataTable Select_zk_zsxxdm(string pcdm)
        {
            Model_zk_zsxxdm info = new Model_zk_zsxxdm();
            string sql = "select * from zk_zsxxdm where pcdm=@pcdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@pcdm",pcdm)};
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
        public bool update_zk_zsxxdm(string set, string where)
        {
            string sql = "update  zk_zsxxdm set " + set + " where " + where;
            _dbHelper.ExecuteNonQuery(sql, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
      /// <summary>
      /// 根据批次代码修改
      /// </summary>
      /// <param name="item"></param>
      /// <param name="error"></param>
      /// <param name="bReturn"></param>
      /// <returns></returns>
        public bool update_zk_zsxxdm(Model_zk_zsxxdm item)
        {
            string sql = "update  zk_zsxxdm set zsxxmc=@zsxxmc,bz=@bz where zsxxdm=@zsxxdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@zsxxmc",item.Zsxxmc),
 			 new SqlParameter("@bz",item.Bz),
 			 new SqlParameter("@zsxxdm",item.Zsxxdm)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
      /// <summary>
      /// 根据批次代码删除
      /// </summary>
      /// <param name="pcdm"></param>
      /// <param name="error"></param>
      /// <param name="bReturn"></param>
      /// <returns></returns>
        public bool delete_zk_zsxxdm(string pcdm)
        {
            string sql = "delete  zk_zsxxdm where pcdm=@pcdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@pcdm",pcdm)};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }

        /// <summary>
        /// 根据多个考次代码删除指定数据
        /// </summary>
        /// <param name="zsxxdm">需要删除的县区代码列表</param>
        /// <returns></returns>
        public bool DeleteDataByZsxxdm(List<string> zsxxdm)
        {
            string inStr = "";

            foreach (var str in zsxxdm)
                inStr += "'" + str + "',";

            string sqlCmd = "Delete zk_zsxxdm Where zsxxdm In (" + inStr.Substring(0, inStr.Length - 1) + ")";
            _dbHelper.ExecuteNonQuery(sqlCmd, ref error, ref bReturn);

            if (bReturn) return true;
            else return false;
        }

        /// <summary>
        /// 返回实体类
        /// </summary> 
        public Model_zk_zsxxdm Disp(string zsxxdm)
        {
            Model_zk_zsxxdm info = new Model_zk_zsxxdm();
            string sql = "select * from zk_zsxxdm where zsxxdm=@zsxxdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@zsxxdm",zsxxdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
            {
                info.Zsxxdm = dt.Rows[0]["zsxxdm"].ToString();
                info.Zsxxmc = dt.Rows[0]["zsxxmc"].ToString();
                info.Bz = dt.Rows[0]["bz"].ToString();

            }

            return info;
        }

      /// <summary>
      /// 查询学制信息
      /// </summary>
      /// <returns></returns>
        public DataTable GetXueZhiInfo()
        {
            string sqlCmd = "Select * From View_XZ";
            return _dbHelper.selectTab(sqlCmd,ref error,ref bReturn);
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
            string tabName = " zk_zsxxdm ";
            //要查询的字段
            string reField = " * ";
            //排序字段
            string orderStr = " zsxxdm ";
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

            string sqlCmd = " Insert Into zk_zsxxdm(zsxxdm,zsxxmc,bz) Values(@zsxxdm,@zsxxmc,@bz) ";
            string checkSql = " Select zsxxdm From zk_zsxxdm Where zsxxdm=@zsxxdm ";
            bool bReturn = true;

            int i = 0;
            int lost = 0;
            int finish = 0;

            try
            {

                foreach (var element in excel)
                {
                    if (element.ColumnNames.Count() != 3)
                        return "导入失败，原因是：输入的文件路径格式不对应，目标格式为3列，您输入的是：" + element.ColumnNames.Count() + "列的格式。";

                    string errMsg = "";
                    string baseStr = ""; // "系统时间：" + DateTime.Now.ToString("yyyy年MM月dd日hh时mm分ss秒") + "。";

                    //空数据则跳过
                    if (string.IsNullOrEmpty(element[0].ToString().Trim()))
                        continue;

                    List<SqlParameter> param = new List<SqlParameter> {
                                                new SqlParameter("@zsxxdm",element[0].ToString().Trim()),
                                                new SqlParameter("@zsxxmc",element[1].ToString().Trim()) ,
                                                new SqlParameter("@bz",element[2].ToString().Trim()) };

                    DataSet tmpDataSet = _dbHelper.selectDataSet(checkSql, param, ref errMsg, ref bReturn);


                    if (element[0].ToString().Trim().Length == 0)
                    {
                        errMsg += "第" + (i + 1) + "条数据导入失败：学校代码不能为空! ";
                    }
                    if (element[1].ToString().Trim().Length == 0)
                    {
                        errMsg += "第" + (i + 1) + "条数据导入失败：学校名称不能为空! ";
                    }
                    if (tmpDataSet.Tables[0].Rows.Count > 0)
                    {
                        errMsg += "第" + (i + 1) + "条数据导入失败：学校代码【" + element[0].ToString() + "】已存在! ";
                    }


                    //如果上面的验证都通过的话就继续执行添加（增量）或修改数据（非增量）
                    if (string.IsNullOrEmpty(errMsg))
                    {
                        _dbHelper.ExecuteNonQuery(sqlCmd, param, ref errMsg, ref bReturn);

                        if (!bReturn)
                        {
                            errMsg = "第" + (i + 1) + "行数据导入时发生错误.";
                        }
                    }

                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        resultMsg.Append(baseStr + "第" + (i + 1) + "行数据导入时发生错误，原因是：<br />");
                        resultMsg.Append("      " + errMsg + "<br />");

                        lost++;
                    }
                    else
                    {
                     //   resultMsg.Append(baseStr + "第" + (i + 1) + "行数据导入成功。<br />");
                        finish++;
                    }




                  //  resultMsg.Append("---------------------------------------------------<br /><br />");
                    i++;
                }

            }
            catch (Exception ex)
            {
                resultMsg.Append("第" + (i + 1) + "行数据导入时发生错误。<br />");
                _dbHelper.writeErrorInfo(ex.Message);
            }
            resultMsg.Append("共处理:" + i + "数据导入完毕。<br>共成功导入：" + finish + "条数据。导入失败：" + lost + "条数据。<br />");

            File.Delete(excelFilePath);
            return resultMsg.ToString();
        }

        #endregion
    }
}
