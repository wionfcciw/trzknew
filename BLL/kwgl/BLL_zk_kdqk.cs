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
    public class BLL_zk_kdqk
    {
        private SqlDbHelper_1 _dbHelper = new SqlDbHelper_1();
        private string error = "";
        private bool bReturn = false;
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="item">实体类</param>
        /// <returns></returns>
        public bool Insert_zk_kdqk(Model_zk_kdqk item)
        {
            string sql = "insert into zk_kdqk(zkzh,kddm,kmdm,kcqkdm,kcqk,bz) values(@zkzh,@kddm,@kmdm,@kcqkdm,@kcqk,@bz)";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			
 			 new SqlParameter("@zkzh",item.Zkzh),
 			 new SqlParameter("@kddm",item.Kddm),
 			 new SqlParameter("@kmdm",item.Kmdm),
 			 new SqlParameter("@kcqkdm",item.Kcqkdm),
 			 new SqlParameter("@kcqk",item.Kcqk),
 			 new SqlParameter("@bz",item.Bz)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 根据考点查询改考生是否存在
        /// </summary>
        /// <param name="xqdm"></param> 
        public DataTable Select_kdks(string where)
        {
            string error = "";
            bool bReturn = false;
            string sql = "select  * from   View_kczwinfo where " + where;
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);
            return dt;
        }
        public bool update_zk_kdqk(Model_zk_kdqk item)
        {
            string sql = "update  zk_kdqk set zkzh=@zkzh,kddm=@kddm,kmdm=@kmdm,kcqkdm=@kcqkdm,kcqk=@kcqk,bz=@bz where ID=@ID";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			  new SqlParameter("@ID",item.ID),
 			 new SqlParameter("@zkzh",item.Zkzh),
 			 new SqlParameter("@kddm",item.Kddm),
 			 new SqlParameter("@kmdm",item.Kmdm),
 			 new SqlParameter("@kcqkdm",item.Kcqkdm),
 			 new SqlParameter("@kcqk",item.Kcqk),
 			 new SqlParameter("@bz",item.Bz)
 			 
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
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
        public DataTable ExecuteProc_View_ksz(string where, int pageSize, int pageIndex, ref int totalRecord)
        {
           

            //数据库表名
            string tabName = " View_kdqk ";
            //要查询的字段
            string reField = " * ";
            //排序字段
            string orderStr = " zkzh,kddm ";
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
        /// 根据考点查询改考生是否存在
        /// </summary>
        /// <param name="xqdm"></param> 
        public DataTable Select_ks(string where)
        {
            string error = "";
            bool bReturn = false;
            string sql = "select a.*,b.xm from zk_kdqk a left join View_kczwinfo b on a.zkzh=b.zkzh where " + where;
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);
            return dt;
        }
        public bool Delete(string where)
        {
            string sql = " delete zk_kdqk where " + where;

            _dbHelper.ExecuteNonQuery(sql, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="xqdm"></param> 
        public DataTable daochu(string where)
        {
            string error = "";
            bool bReturn = false;
            string sql = "select zkzh,xm,kddm,kmmc,kcqkmc,kcqk,bz from View_kdqk where " + where + " order by zkzh,kddm";
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);
            return dt;
        }
        public bool Insert_zk_kdqkzt(Model_zk_kdqkzt item)
        {
            string sql = "insert into zk_kdqkzt(kddm,kmdm,type) values(@kddm,@kmdm,@type)";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@kddm",item.Kddm),
 			 new SqlParameter("@kmdm",item.Kmdm),
 			 new SqlParameter("@type",item.Type)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 根据考点查询无情况
        /// </summary>
        /// <param name="xqdm"></param> 
        public DataTable Select_kdqkzt(string where)
        {
            string error = "";
            bool bReturn = false;
            string sql = "select * from zk_kdqkzt where " + where;
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);
            return dt;
        }
        /// <summary>
        /// 查询考试时间
        /// </summary>
        /// <param name="xqdm"></param> 
        public DataTable Select_zk_kstime()
        {
            string error = "";
            bool bReturn = false;
            string sql = "select * from zk_kstime ";
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);
            return dt;
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
        public string ImportExcelData(string excelFilePath,string str)
        {
            if (!File.Exists(excelFilePath))
                return "导入失败，原因是：输入的文件路径不正确或指定的文件不存在。";

            ExcelQueryFactory excelFile = new ExcelQueryFactory(excelFilePath);
            ExcelQueryable<Row> excel = excelFile.Worksheet(0);
            StringBuilder resultMsg = new StringBuilder();

            string sqlCmd = "insert into zk_kdjkls(xqdm,kddm,sfzh,xm,gzdw,pj1,pj2,pj3,pj4,pj5,pj6,pj7,bz) values(@xqdm,@kddm,@sfzh,@xm,@gzdw,@pj1,@pj2,@pj3,@pj4,@pj5,@pj6,@pj7,@bz)";
            string checkSql = "Select * From zk_kdjkls Where xqdm=@xqdm and sfzh=@sfzh and kddm=@kddm ;";
            bool bReturn = true;

            int i = 0;
            int lost = 0;
            int finish = 0;
            try
            {
                foreach (var element in excel)
                {
                    if (element.ColumnNames.Count() != 11)
                        return "导入失败，文件格式不是11列。";

                    string errMsg = "";
                    string baseStr = "";


                    //空数据则跳过
                    if (string.IsNullOrEmpty(element[0].ToString().Trim()))
                        continue;

                    List<SqlParameter> param = new List<SqlParameter> {
                                             new SqlParameter("@xqdm",str.Substring(1,4)),
 			 new SqlParameter("@kddm",str.Substring(3)),
 			 new SqlParameter("@sfzh",element[0].ToString().Trim()),
 			 new SqlParameter("@xm",element[1].ToString().Trim()),
              new SqlParameter("@gzdw",element[2].ToString().Trim()),
 			 new SqlParameter("@pj1",element[3].ToString().Trim()),
 			 new SqlParameter("@pj2",element[4].ToString().Trim()),
 			 new SqlParameter("@pj3",element[5].ToString().Trim()),
 			 new SqlParameter("@pj4",element[6].ToString().Trim()),
 			 new SqlParameter("@pj5",element[7].ToString().Trim()),
 			 new SqlParameter("@pj6",element[8].ToString().Trim()),
 			 new SqlParameter("@pj7",element[9].ToString().Trim()),
 			 new SqlParameter("@bz",element[10].ToString().Trim())  };

                    
                    DataSet tmpDataSet = _dbHelper.selectDataSet(checkSql, param, ref errMsg, ref bReturn);

                  
                    //需要导入的老师是否存在
                    if (tmpDataSet.Tables[0].Rows.Count != 0)
                        errMsg += "第" + (i + 1) + "条数据有误，原因是：该老师已存在：" + element[0].ToString().Trim() + "。<br />";

                   
                    if (element[1].ToString().Trim().Length == 0)
                    {
                     
                            errMsg += "第" + (i + 1) + "条数据有误，原因是：姓名不能为空：" + element[2].ToString().Trim() + "。<br />";
                    }
                    if (element[2].ToString().Trim().Length == 0)
                    {

                        errMsg += "第" + (i + 1) + "条数据有误，原因是：工作单位不能为空：" + element[2].ToString().Trim() + "。<br />";
                    }
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
                _dbHelper.writeErrorInfo(ex.Message);
            }

            resultMsg.Append("共处理:" + i + "数据导入完毕。共成功导入：" + finish + "条数据。导入失败：" + lost + "条数据。<br />");

            File.Delete(excelFilePath);

            return resultMsg.ToString();

        }

        #endregion

        #region 执行分页存储过程，返回记录总数和当前页的数据。
        /// <summary>
        /// 执行分页存储过程，返回记录总数和当前页的数据。
        /// </summary>
        /// <param name="where">执行的条件</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="totalRecord">返回的记录总数</param>
        public DataTable ExecuteProc_kdjkls(string where, int pageSize, int pageIndex, ref int totalRecord)
        {


            //数据库表名
            string tabName = " zk_kdjkls ";
            //要查询的字段
            string reField = " * ";
            //排序字段
            string orderStr = " xqdm,kddm ";
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

        public bool Insert_zk_kdjkls(Model_zk_kdjkls item )
        {
            string sql = "insert into zk_kdjkls(xqdm,kddm,sfzh,xm,gzdw,pj1,pj2,pj3,pj4,pj5,pj6,pj7,bz) values(@xqdm,@kddm,@sfzh,@xm,@gzdw,@pj1,@pj2,@pj3,@pj4,@pj5,@pj6,@pj7,@bz)";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",item.Xqdm),
 			 new SqlParameter("@kddm",item.Kddm),
 			 new SqlParameter("@sfzh",item.Sfzh),
 			 new SqlParameter("@xm",item.Xm),
             new SqlParameter("@gzdw",item.Gzdw),
 			 new SqlParameter("@pj1",item.Pj1),
 			 new SqlParameter("@pj2",item.Pj2),
 			 new SqlParameter("@pj3",item.Pj3),
 			 new SqlParameter("@pj4",item.Pj4),
 			 new SqlParameter("@pj5",item.Pj5),
 			 new SqlParameter("@pj6",item.Pj6),
 			 new SqlParameter("@pj7",item.Pj7),
 			 new SqlParameter("@bz",item.Bz)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }

        public bool update_zk_kdjkls(Model_zk_kdjkls item )
        {
            string sql = "update  zk_kdjkls set gzdw=@gzdw, xqdm=@xqdm,kddm=@kddm,sfzh=@sfzh,xm=@xm,pj1=@pj1,pj2=@pj2,pj3=@pj3,pj4=@pj4,pj5=@pj5,pj6=@pj6,pj7=@pj7,bz=@bz where ID=@ID";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			  new SqlParameter("@ID",item.ID),
 			 new SqlParameter("@xqdm",item.Xqdm),
 			 new SqlParameter("@kddm",item.Kddm),
 			 new SqlParameter("@sfzh",item.Sfzh),
 			 new SqlParameter("@xm",item.Xm),
               new SqlParameter("@gzdw",item.Gzdw),
 			 new SqlParameter("@pj1",item.Pj1),
 			 new SqlParameter("@pj2",item.Pj2),
 			 new SqlParameter("@pj3",item.Pj3),
 			 new SqlParameter("@pj4",item.Pj4),
 			 new SqlParameter("@pj5",item.Pj5),
 			 new SqlParameter("@pj6",item.Pj6),
 			 new SqlParameter("@pj7",item.Pj7),
 			 new SqlParameter("@bz",item.Bz),
 		 
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }

        /// <summary>
        /// 根据考点查询老师
        /// </summary>
        /// <param name="xqdm"></param> 
        public DataTable Select_jkls(string where)
        {
            string error = "";
            bool bReturn = false;
            string sql = "select  * from   zk_kdjkls where " + where;
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);
            return dt;
        }

        /// <summary>
        /// 考点查询 
        /// </summary>
        /// <param name="xqdm"></param> 
        public DataTable Select_kd(string where)
        {
            string error = "";
            bool bReturn = false;
            string sql = "select  * from   View_kdzs where " + where;
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);
            return dt;
        }
        public bool Delete_zk_kdjkls(string where)
        {
            string sql = " delete zk_kdjkls where " + where;

            _dbHelper.ExecuteNonQuery(sql, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="xqdm"></param> 
        public DataTable daochu_zk_kdjkls(string where)
        {
            string error = "";
            bool bReturn = false;
            string sql = "select xqdm,kddm,sfzh,xm,gzdw,pj1,pj2,pj3,pj4,pj5,pj6,pj7,bz from zk_kdjkls where " + where + " order by xqdm,kddm";
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);
            return dt;
        }
        /// <summary>
        /// 导出数据无情况
        /// </summary>
        /// <param name="xqdm"></param> 
        public DataTable daochuwqk()
        {
            string error = "";
            bool bReturn = false;
            string sql = "SELECT a.kddm,b.zlbmc FROM zk_kdqkzt a left join zk_zdxxLB b on a.kmdm=b.zlbdm and b.zdlbdm='kskm' order by kddm";
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);
            return dt;
        }
    }

}
