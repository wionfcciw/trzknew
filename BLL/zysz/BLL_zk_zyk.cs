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
namespace BLL
{
    /// <summary>
    /// 专业库控制类
    /// </summary>
    public class BLL_zk_zyk
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
        public bool Insert_zk_zyk(Model_zk_zyk item)
        {
            string sql = "insert into zk_zyk(xxdm,zydm,zymc,bz) values(@xxdm,@zydm,@zymc,@bz)";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xxdm",item.Xxdm),
 			 new SqlParameter("@zydm",item.Zydm),
             new SqlParameter("@zymc",item.Zymc),
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
        public bool Insert_zk_zyk(List<Model_zk_zyk> Listitem)
        {
            string sql = "insert into zk_zyk(zydm,zymc,xxdm) values(@zydm,@zymc,@xxdm)";
            List<SqlParameter> lisP = new List<SqlParameter>();
            SqlParameter Zydm = new SqlParameter("@zydm", SqlDbType.VarChar);
            SqlParameter Zymc = new SqlParameter("@zymc", SqlDbType.VarChar);
            SqlParameter Xxdm = new SqlParameter("@xxdm", SqlDbType.VarChar);
            foreach (Model_zk_zyk item in Listitem)
            {
                Zydm.Value = item.Zydm;
                Zymc.Value = item.Zymc;
                Xxdm.Value = item.Xxdm;
                lisP.Clear();
                lisP.Add(Zydm);
                lisP.Add(Zymc);
                lisP.Add(Xxdm);
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
        public DataTable Select_zk_zyk(ref string error, ref bool bReturn)
        {
            string sql = "select * from zk_zyk";
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);
            return dt;
        }
        /// <summary>
        /// 根据专业代码查询
        /// </summary>
        /// <param name="pcdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public Model_zk_zyk Select_zk_zyk(string pcdm)
        {
            Model_zk_zyk info = new Model_zk_zyk();
            string sql = "select * from zk_zyk where pcdm=@pcdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@pcdm",pcdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
                info = _dbHelper.DT2EntityList<Model_zk_zyk>(dt)[0];
            return info;
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
            Model_zk_zyk info = new Model_zk_zyk();
            string sql = "select  zydm,zydm+zymc zymc  from zk_zyk where xxdm=@xxdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xxdm",xxdm)};
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
        public bool update_zk_zyk(string set, string where)
        {
            string sql = "update  zk_zyk set " + set + " where " + where;
            _dbHelper.ExecuteNonQuery(sql, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 根据专业代码修改
        /// </summary>
        /// <param name="item"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool update_zk_zyk(Model_zk_zyk item)
        {
            //xxdm,zydm,zymc,bz
            string sql = "update  zk_zyk set zymc=@zymc,bz=@bz where xxdm=@xxdm And zydm=@zydm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xxdm",item.Xxdm),
 			 new SqlParameter("@zymc",item.Zymc),
             new SqlParameter("@bz",item.Bz),
 			 new SqlParameter("@zydm",item.Zydm)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 根据专业代码删除
        /// </summary>
        /// <param name="zydm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool delete_zk_zyk(string zydm)
        {
            string sql = "delete  zk_zyk where zydm=@zydm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@zydm",zydm)};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 根据学校代码删除
        /// </summary>
        /// <param name="xxdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool delete_zk_zykXX(string xxdm)
        {
            string sql = "delete  zk_zyk where xxdm=@xxdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xxdm",xxdm)};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }

        /// <summary>
        /// 根据学校代码查询 返回实体类
        /// </summary> 
        public Model_zk_zyk Disp(string xxdm,string zydm)
        {
            Model_zk_zyk info = new Model_zk_zyk();
            string sql = "select * from zk_zyk where xxdm=@xxdm And zydm=@zydm";
            List<SqlParameter> lisP = new List<SqlParameter>() {  new SqlParameter("@xxdm", xxdm),new SqlParameter("@zydm", zydm) };
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
            if (dt.Rows.Count > 0)
                info = _dbHelper.DT2EntityList<Model_zk_zyk>(dt)[0];
            return info;
        }

        /// <summary>
        /// 根据多个考次代码删除指定数据
        /// </summary>
        /// <param name="zsxxdm">需要删除的县区代码列表</param>
        /// <returns></returns>
        public void DeleteDataByIDS(List<string> zsxxdm)
        {
            _dbHelper.BeginTran();

            foreach (var str in zsxxdm)
            {
                string[] inStr = str.Split('|');
                string sqlCmd = "Delete zk_zyk Where xxdm=@xxdm And zydm=@zydm";
                List<SqlParameter> list = new List<SqlParameter>{
                    new SqlParameter("@xxdm",inStr[0]),
                    new SqlParameter("@zydm",inStr[1])
                };

                _dbHelper.execSql_Tran(sqlCmd, list);
            }

            _dbHelper.EndTran(true);
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
            string tabName = " View_zk_zyk ";
            //要查询的字段
            string reField = "   xxdm, dmmc='['+xxdm+']'+zsxxmc ,zydm,zymc ";
            //排序字段
            string orderStr = "  xxdm asc ,zydm ";
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

            string sqlCmd = "Insert Into zk_zyk(xxdm,zydm,zymc,bz)Values(@xxdm,@zydm,@zymc,@bz) ";
            string checkSql = "Select zsxxdm From zk_zsxxdm Where zsxxdm=@xxdm;Select xxdm From zk_zyk Where xxdm=@xxdm And zydm=@zydm ";
            bool bReturn = true;

            int i = 0;
            int lost = 0;
            int finish = 0;


            try
            {

                foreach (var element in excel)
                {
                    if (element.ColumnNames.Count() != 4)
                        return "导入失败，原因是：输入的文件路径格式不对应，目标格式为4列，您输入的是：" + element.ColumnNames.Count() + "列的格式。";

                    string errMsg = "";
                    string baseStr = ""; //"系统时间：" + DateTime.Now.ToString("yyyy年MM月dd日hh时mm分ss秒") + "。";

                    //空数据则跳过
                    if (string.IsNullOrEmpty(element[0].ToString().Trim()))
                        continue;

                    List<SqlParameter> param = new List<SqlParameter> {
                                                new SqlParameter("@xxdm",element[0].ToString().Trim()),
                                                new SqlParameter("@zydm",element[1].ToString().Trim()),
                                                new SqlParameter("@zymc",element[2].ToString().Trim()),
                                                new SqlParameter("@bz",element[3].ToString().Trim()) };

                    DataSet tmpDataSet = _dbHelper.selectDataSet(checkSql, param, ref errMsg, ref bReturn);



                    if (element[0].ToString().Trim().Length == 0)
                    {
                        errMsg += "第" + (i + 1) + "条数据导入失败：学校代码不能为空! ";
                    }
                    if (element[1].ToString().Trim().Length == 0)
                    {
                        errMsg += "第" + (i + 1) + "条数据导入失败：专业代码不能为空! ";
                    }
                    if (element[2].ToString().Trim().Length == 0)
                    {
                        errMsg += "第" + (i + 1) + "条数据导入失败：专业名称不能为空! ";
                    }

                    if (tmpDataSet.Tables[0].Rows.Count == 0)
                    {
                        errMsg += "第" + (i + 1) + "条数据导入失败：找不到学校【" + element[0].ToString() + "】,请检查学校代码是否正确! ";
                    }
                    if (tmpDataSet.Tables[1].Rows.Count > 0)
                    {
                        errMsg += "第" + (i + 1) + "条数据导入失败：该学校【" + element[0].ToString() + "】的【" + element[1].ToString() + "】专业已存在！";
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
                        resultMsg.Append(" " + errMsg + "<br />");

                        lost++;
                    }
                    else
                    {
                       // resultMsg.Append(baseStr + "第" + (i + 1) + "行数据导入成功。<br />");
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
    }
}
