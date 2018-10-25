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
    /// 班级代码控制类
    /// </summary>
    public class BLL_zk_bjdm
    {
        private SqlDbHelper_1 _dbHelper = new SqlDbHelper_1();
        private string error = "";
        private bool bReturn = false;
        /// <summary>
        /// 插入班级信息
        /// </summary>
        /// <param name="item">班级类</param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool Insert_zk_bjdm(Model_zk_bjdm item)
        {
            string sql = "insert into zk_bjdm(xqdm,bjdm,xxdm,bjmc) values(@xqdm,@bjdm,@xxdm,@bjmc)";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",item.xqdm),
 			 new SqlParameter("@bjdm",item.Bjdm),
 			 new SqlParameter("@xxdm",item.xxdm),
 			 new SqlParameter("@bjmc",item.Bjmc)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 批量插入班级信息
        /// </summary>
        /// <param name="item">班级类</param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool Insert_zk_bjdm(List<Model_zk_bjdm> Listitem)
        {
            string sql = "insert into zk_bjdm( bjdm,xxdm,bjmc) values( @bjdm,@xxdm,@bjmc)";
            List<SqlParameter> lisP = new List<SqlParameter>();
         
            SqlParameter Bjdm = new SqlParameter("@bjdm", SqlDbType.VarChar);
            SqlParameter xxdm = new SqlParameter("@xxdm", SqlDbType.VarChar);
            SqlParameter Bjmc = new SqlParameter("@bjmc", SqlDbType.VarChar);
            foreach (Model_zk_bjdm item in Listitem)
            {
             
                Bjdm.Value = item.Bjdm;
                xxdm.Value = item.xxdm;
                Bjmc.Value = item.Bjmc;
                lisP.Clear();
                lisP.Add(Bjdm);
                lisP.Add(xxdm);
                lisP.Add(Bjmc);
                _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            }
            if (bReturn) return true;
            else return false;
        }


        /// <summary>
        /// 查询班级表 
   
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable Select_zk_bjdm(ref string error, ref bool bReturn)
        {
            string sql = "select * from zk_bjdm";
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
        public DataTable ExecuteProc(string where, int pageSize, int pageIndex, ref int totalRecord)
        {
            //string sql="select  from AppraisalPersonnel";

            //数据库表名
            string tabName = "zk_bjdm as A left join zk_xxdm as B  on A.xxdm=B.xxdm left join  zk_xqdm as C on A.xqdm=C.xqdm ";
            //要查询的字段
            string reField = "  lsh,xqdm='['+A.xqdm+']'+C.xqmc,xxdm='['+B.xxdm+']'+B.xxmc,bjdm,bjmc  ";
            //排序字段
            string orderStr = " A.xxdm asc,bjdm ";
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
        /// 根据毕业学校代码查询班级
        /// </summary>
        /// <param name="xxdm">毕业学校代码</param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable Select_zk_bjdm(string xxdm)
        {
            Model_zk_bjdm info = new Model_zk_bjdm();
            string sql = "select * from zk_bjdm where xxdm=@xxdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xxdm",xxdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            return dt;
        }

        #region "根据毕业学校代码查询班级加上管理部门权限"

        /// <summary>
        /// 根据毕业学校代码查询班级加上管理部门权限
        /// </summary>
        /// <param name="xxdm">毕业学校代码</param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable Select_zk_bjdm(string xxdm, string fanwei, int UserType)
        {
            string sql = "";
            string where = whereRole(fanwei, UserType);
            if (where.Length == 0)
            {
                sql = "select * from zk_bjdm where xxdm=@xxdm ";
            }
            else
            {
                sql = "select * from zk_bjdm where xxdm=@xxdm and " + where + " ";
            }


            Model_zk_bjdm info = new Model_zk_bjdm();
          
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xxdm",xxdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
            return dt;
        }

        /// <summary>
        /// 管理部门权限控制
        /// </summary>
        /// <param name="fanwei">管理范围</param>
        /// <param name="UserType">用户类型</param>
        /// <returns></returns>
        public string whereRole(string fanwei, int UserType)
        { 
            string where = "";
            switch (UserType)
            {
                //系统管理员
                case 1:
                    where = "";
                    break;
                //市招生办
                case 2:
                    where = "";
                    break;
                //区招生办
                case 3:
                    where = " xxdm like '" + fanwei + "%'  ";
                    break;
                //学校用户 
                case 4:
                    where = " xxdm like '" + fanwei + "%' ";
                    break;
                //班级用户 
                case 5:
                    where = " xxdm = '" + fanwei.Substring(0,6) + "'  and bjdm='" + fanwei.Substring(6,2) + "' ";
                    break;
                default:
                    where = " 1<>1 ";
                    break;
            }
            return where;
        }
        #endregion


        /// <summary>
        /// 根据 学校代码和班级代码查询
        /// </summary> 
        public DataTable Select_zk_bjdm_bj(string xxdm,string bjdm)
        {
            Model_zk_bjdm info = new Model_zk_bjdm();
            string sql = "select  A.*,B.xxmc from zk_bjdm as A ,zk_xxdm as B where A.xxdm=b.xxdm and A.xxdm=@xxdm  and bjdm=@bjdm ";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xxdm",xxdm),
            new SqlParameter("@bjdm",bjdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            return dt;
        }



        /// <summary>
        /// 根据毕业学校代码查询班级
        /// </summary>
        /// <param name="xxdm">毕业学校代码</param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable Select_zk_bjdm(string xxdm,string where )
        {
            Model_zk_bjdm info = new Model_zk_bjdm();
            string sql = "";
            if (where.Length == 0)
            {
                sql = "select * from zk_bjdm where xxdm=@xxdm";
            }
            else
            {
                sql = "select * from zk_bjdm where xxdm=@xxdm and " + where + " ";
            }
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xxdm",xxdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            return dt;
        }

        /// <summary>
        /// 根据ID代码查询班级
        /// </summary>
        /// <param name="xxdm">ID</param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public Model_zk_bjdm Select_zk_bjdmID(string lsh)
        {
            Model_zk_bjdm info = new Model_zk_bjdm();
            string sql = "select * from zk_bjdm where lsh=@lsh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@lsh",lsh)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
                info = _dbHelper.DT2EntityList<Model_zk_bjdm>(dt)[0];
            return info;
        }
        /// <summary>
        /// 自定义条件修改班级
        /// </summary>
        /// <param name="set"></param>
        /// <param name="where"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool update_zk_bjdm(string set, string where)
        {
            string sql = "update  zk_bjdm set " + set + " where " + where;
            _dbHelper.ExecuteNonQuery(sql, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 根据班级ID修改班级名称
        /// </summary>
        /// <param name="item"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool update_zk_bjdm(Model_zk_bjdm item)
        {
            string sql = "update  zk_bjdm set bjmc=@bjmc where lsh=@lsh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@bjmc",item.Bjmc),
 			 new SqlParameter("@lsh",item.Lsh)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 根据班级ID删除
        /// </summary>
        /// <param name="lsh"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool delete_zk_bjdm(string lsh)
        {
            string sql = "delete  zk_bjdm where lsh=@lsh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@lsh",lsh)};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }

        /// <summary>
        /// 根据多个考次代码删除指定数据
        /// </summary>
        /// <param name="xqdms">需要删除的县区代码列表</param>
        /// <returns></returns>
        public bool DeleteDataByLsh(List<string> xqdms)
        {
            string inStr = "";

            foreach (var str in xqdms)
                inStr += "'" + str + "',";

            string sqlCmd = "Delete zk_bjdm Where lsh In (" + inStr.Substring(0, inStr.Length - 1) + ")";
            _dbHelper.ExecuteNonQuery(sqlCmd, ref error, ref bReturn);

            if (bReturn) return true;
            else return false;
        }

        /// <summary>
        /// 根据班级ID查询 返回实体类
        /// </summary> 
        public Model_zk_bjdm Disp(string lsh)
        {
            Model_zk_bjdm info = new Model_zk_bjdm();
            string sql = "select * from zk_bjdm where lsh=@lsh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@lsh",lsh)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
                info = _dbHelper.DT2EntityList<Model_zk_bjdm>(dt)[0];
            return info;
        }

        /// <summary>
        /// 根据学校代码删除
        /// </summary>
        /// <param name="xxdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool delete_zk_bjdmXX(string xxdm)
        {
            string sql = "delete  zk_bjdm where xxdm=@xxdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xxdm",xxdm)};
            _dbHelper.ExecuteNonQuery(sql, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }

        /// <summary>
        /// 权限控制
        /// </summary>
        /// <returns></returns>
        public string whereRole(string str)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string where = "";
            switch (UserType)
            {
                //系统管理员
                case 1:
                    where = "";
                    break;
                //市招生办
                case 2:
                    where = "";
                    break;
                //区招生办
                case 3:
                    if (Department == str.Trim().Substring(0, 4))
                    {
                        where = "";
                    }
                    else
                    {
                        where = "导入的数据跟你所属县区不符.";
                    }

                    break;
                //学校用户 
                case 4:
                    if (Department == str.Trim())
                    {
                        where = "";
                    }
                    else
                    {
                        where = "导入的数据跟你所属县区不符.";
                    }

                    break;
                //班级用户 
                case 5:
                    where = "*";
                    break;
                default:
                    where = "*";
                    break;
            }
            return where;
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
        public string ImportExcelData(string excelFilePath, bool isZL)
        {
            try
            {

           
            if (!File.Exists(excelFilePath))
                return "导入失败，原因是：输入的文件路径不正确或指定的文件不存在。";

            ExcelQueryFactory excelFile = new ExcelQueryFactory(excelFilePath);
            ExcelQueryable<Row> excel = excelFile.Worksheet(0);
            
            StringBuilder resultMsg = new StringBuilder();

            string sqlCmd = "";
            string checkSql = "Select xxdm From zk_xxdm Where xqdm=@xqdm And xxdm=@xxdm;Select lsh From zk_bjdm Where xqdm=@xqdm And xxdm=@xxdm And bjdm=@bjdm;";
            bool bReturn = true;

            int i = 0;
            int lost = 0;
            int finish = 0;

            foreach (var element in excel)
            {
                if (element.ColumnNames.Count() != 4)
                    return "导入失败，原因是：输入的文件路径格式不对应，目标格式为4列，您输入的是：" + element.ColumnNames.Count() + "列的格式。";

                string errMsg = "";
                string baseStr = ""; // "系统时间：" + DateTime.Now.ToString("yyyy年MM月dd日hh时mm分ss秒") + "。";

                //空数据则跳过
                if (string.IsNullOrEmpty(element[0].ToString().Trim()))
                    continue;

                List<SqlParameter> param = new List<SqlParameter> {
                                                new SqlParameter("@xqdm",element[0].ToString().Trim()),
                                                new SqlParameter("@xxdm",element[1].ToString().Trim()),
                                                new SqlParameter("@bjdm",element[2].ToString().Trim()),
                                                new SqlParameter("@bjmc",element[3].ToString().Trim())
                };

                DataSet tmpDataSet = _dbHelper.selectDataSet(checkSql, param, ref errMsg, ref bReturn);

                if (element[0].ToString().Trim().Length != 4)
                    errMsg = "第" + (i + 1) + "条数据有误，原因是：县区代码" + element[0].ToString().Trim() + "不是4位。";
                else
                {
                    try
                    {
                        Convert.ToInt32(element[0].ToString().Trim());
                        string strc = whereRole(element[0].ToString().Trim());
                        if (strc != "")
                        {
                            errMsg = "第" + (i + 1) + "条数据有误，原因是：" + strc;
                        }
                    }
                    catch (Exception)
                    {
                        errMsg = "第" + (i + 1) + "条数据有误，原因是：县区代码" + element[0].ToString().Trim() + "有误。";
                    }

                   
                }      //验证长度
                if (element[1].ToString().Trim().Length != 6)
                    errMsg = "第" + (i + 1) + "条数据有误,原因是：学校代码" + element[1].ToString().Trim() + "不是6位。";
                else
                {
                    try
                    {
                        Convert.ToInt32(element[1].ToString().Trim());
                        string strc = whereRole(element[1].ToString().Trim());
                        if (strc != "")
                        {
                            errMsg = "第" + (i + 1) + "条数据有误，原因是：" + strc;
                        }
                        else
                        {
                            if (tmpDataSet.Tables[0].Rows.Count == 0)
                                errMsg = "第" + (i + 1) + "条数据导入时发生错误原因是：该学校不存在请验证本条数据。";
                        }    
                    }
                    catch (Exception)
                    {
                        errMsg = "第" + (i + 1) + "条数据有误，原因是：学校代码" + element[1].ToString().Trim() + "有误。";
                
                    }
               
                }
                if (element[3].ToString().Trim().Length == 0)
                    errMsg = "第" + (i + 1) + "条数据有误，原因是：班级名称" + element[3].ToString().Trim() + "不能为空。";
                if (element[2].ToString().Trim().Length == 0)
                    errMsg = "第" + (i + 1) + "条数据有误，原因是：班级代码" + element[2].ToString().Trim() + "不能为空。";
                else
                {
                    try
                    {
                        Convert.ToInt32(element[2].ToString().Trim());
                    }
                    catch (Exception)
                    {
                          errMsg = "第" + (i + 1) + "条数据有误，原因是：班级代码" + element[2].ToString().Trim() + "有误。";
                    }
                    //需要导入的班级是否已存在于指定学校
                    if (tmpDataSet.Tables[1].Rows.Count > 0 )
                    {
                        if (isZL)
                        {
                            errMsg += "第" + (i + 1) + "条数据导入时发生错误原因是：该数据已存在（存在的学校代码为：" + element[1].ToString().Trim() + "）。班级代码为：" + element[2].ToString().Trim() + "。";
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(errMsg))
                            {
                                sqlCmd = "UpDate zk_bjdm Set bjmc=@bjmc Where xqdm=@xqdm And xxdm=@xxdm And bjdm=@bjdm";
                                _dbHelper.ExecuteNonQuery(sqlCmd, param, ref errMsg, ref bReturn);
                            }
                        }    
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(errMsg))
                        {
                            sqlCmd = "Insert Into zk_bjdm(xqdm,xxdm,bjdm,bjmc)Values(@xqdm,@xxdm,@bjdm,@bjmc)";
                            _dbHelper.ExecuteNonQuery(sqlCmd, param, ref errMsg, ref bReturn);
                        }
                       
                    }
                }

            
                //如果上面的验证都通过的话就继续执行添加（增量）或修改数据（非增量）
                //if (string.IsNullOrEmpty(errMsg))
                //{
                 
                //    _dbHelper.ExecuteNonQuery(sqlCmd, dataParams, ref errMsg, ref bReturn);
                //}

                if (!string.IsNullOrEmpty(errMsg))
                {
                  //  resultMsg.Append(baseStr + "第" + (i + 1) + "行数据导入时发生错误，原因是：<br />");
                    resultMsg.Append( errMsg + "<br />");

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

            resultMsg.Append("共处理:" + i + "数据导入完毕。共成功导入：" + finish + "条数据。导入失败：" + lost + "条数据。<br />");

            File.Delete(excelFilePath);

            return resultMsg.ToString();
            }
            catch (Exception)
            {

                return "数据有错!";
            }
        }

        #endregion
    }
}
