using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model;
using System.Data.SqlClient;
using System.Data;
using BLL;
using System.IO;
using LinqToExcel;
using LinqToExcel.Query;
namespace BLL
{
    /// <summary>
    /// 学校代码控制类
    /// </summary>
    public class BLL_zk_xxdm
    {
        private SqlDbHelper_1 _dbHelper = new SqlDbHelper_1();

        private string error = "";
        private bool bReturn = false;


        /// <summary>
        /// 毕业学校新增
        /// </summary>
        /// <param name="item"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool Insert_zk_xxdm(Model_zk_xxdm item )
        {
            string sql = "insert into zk_xxdm(xxdm,xqdm,xxmc,xxlxdm) values(@xxdm,@xqdm,@xxmc,@xxlxdm)";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xxdm",item.Xxdm),
 			 new SqlParameter("@xqdm",item.Xqdm),
 			 new SqlParameter("@xxmc",item.Xxmc),
             new SqlParameter("@xxlxdm",item.Xxlxdm)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 批量毕业学校新增
        /// </summary>
        /// <param name="Listitem"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool Insert_zk_xxdm(List<Model_zk_xxdm> Listitem, ref string error, ref bool bReturn)
        {
            string sql = "insert into zk_xxdm(xxdm,xqdm,xxmc) values(@xxdm,@xqdm,@xxmc)";
            List<SqlParameter> lisP = new List<SqlParameter>();
            SqlParameter Xxdm = new SqlParameter("@xxdm", SqlDbType.VarChar);
            SqlParameter Xqdm = new SqlParameter("@xqdm", SqlDbType.VarChar);
            SqlParameter Xxmc = new SqlParameter("@xxmc", SqlDbType.VarChar);
            foreach (Model_zk_xxdm item in Listitem)
            {
                Xxdm.Value = item.Xxdm;
                Xqdm.Value = item.Xqdm;
                Xxmc.Value = item.Xxmc;
                lisP.Clear();
                lisP.Add(Xxdm);
                lisP.Add(Xqdm);
                lisP.Add(Xxmc);
                _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            }
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 查询所有学校
        /// </summary> 
        public DataTable Select_zk_xxdm()
        {
            string sql = "select * from zk_xxdm order by xqdm ,xxdm ";
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
            string tabName = "zk_xxdm as A inner join zk_xqdm as B on A.xqdm=b.xqdm left join zk_zdxxLB C on A.xxlxdm=C.zlbdm and C.zdlbdm='xxlx'";
            //要查询的字段
            string reField = " C.zlbmc,xxdm,xxmc,xqdm='['+A.xqdm+'] '+B.xqmc ";
            //排序字段
            string orderStr = " xxdm ";
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
        /// 根据县区代码查询 考生填报
        /// </summary> 
        public DataTable Select_xxdmKs(string xqdm)
        {
            string error = "";
            bool bReturn = false;

            Model_zk_xxdm info = new Model_zk_xxdm();
            string sql = "select xqdm,xxdm,xxmc  from zk_xxdm where xqdm=@xqdm order by xxdm ";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",xqdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);

            return dt;
        }

        /// <summary>
        /// 根据县区代码查询
        /// </summary>
        /// <param name="xqdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable Select_zk_xxdmXQ(string xqdm)
        {
            string error = "";
            bool bReturn = false;

            Model_zk_xxdm info = new Model_zk_xxdm();
            string sql = "select xqdm,xxdm,xxmc='['+xxdm+']'+xxmc  from zk_xxdm where xqdm=@xqdm order by xxdm ";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",xqdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);

            return dt;
        }

        #region "根据县区代码查询 加上管理部门权限"
        /// <summary>
        /// 根据县区代码查询 加上管理部门权限
        /// </summary>
        /// <param name="xqdm">县区代码</param>
        /// <param name="fanwei">管理范围</param>
        /// <param name="UserType">用户类型</param>
        /// <returns></returns>
        public DataTable Select_zk_xxdmXQ(string xqdm, string fanwei, int UserType)
        {
            string sql = "";
            string where = whereRole(fanwei, UserType);
            if (where.Length == 0)
            {
                sql = "select xqdm,xxdm,xxmc='['+xxdm+']'+xxmc  from zk_xxdm where xqdm=@xqdm order by xxdm ";
            }
            else
            {
                sql = "select xqdm,xxdm,xxmc='['+xxdm+']'+xxmc  from zk_xxdm where xqdm=@xqdm and " + where + " order by xxdm ";
            }

            Model_zk_xxdm info = new Model_zk_xxdm();

            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",xqdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);

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
            if (fanwei.Length > 5)
            {
                fanwei = fanwei.Substring(0, 5);
            }
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
                    where = "  xxdm like '" + fanwei.Substring(1,2) + "%' ";
                    break;
                //学校用户 
                case 4:
                    where = " xxdm = '" + fanwei + "' ";
                    break;
                //班级用户 
                case 5:
                    where = " xxdm = '" + fanwei + "' ";
                    break;
                default:
                    where = " 1<>1";
                    break;
            }
            return where;
        }
        #endregion

        /// <summary>
        /// 根据县区代码查询
        /// </summary>
        /// <param name="xqdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable Select_zk_xxdmXQ(string xqdm, string where)
        {
            string sql = "";

            Model_zk_xxdm info = new Model_zk_xxdm();
            if (where.Length == 0)
            {
                sql = "select xqdm,xxdm,xxmc='['+xxdm+']'+xxmc  from zk_xxdm where xqdm=@xqdm  order by xxdm ";
            }
            else
            {
                sql = "select xqdm,xxdm,xxmc='['+xxdm+']'+xxmc  from zk_xxdm where xqdm=@xqdm and " + where + "  order by xxdm ";
            }
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",xqdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);

            return dt;
        }


        ///// <summary>
        ///// 根据县区代码查询 加上权限判断
        ///// </summary>
        ///// <param name="xqdm"></param>
        ///// <param name="error"></param>
        ///// <param name="bReturn"></param>
        ///// <returns></returns>
        //public DataTable Select_zk_xxdmXQ(string xqdm )
        //{
        //    int UserType = SincciLogin.Sessionstu().UserType;
        //    string fanwei = SincciLogin.Sessionstu().U_department;

        //    if (fanwei.Substring(2, 2) == "00")
        //        fanwei = fanwei.Substring(0, 2);

        //    Model_zk_xxdm info = new Model_zk_xxdm();
        //    string sql = "";

        //    switch (UserType)
        //    {

        //        //系统管理员
        //        case 1:
        //            sql = "select xqdm,xxdm,xxmc='['+xxdm+']'+xxmc  from zk_xxdm where xqdm=@xqdm order by xxdm asc ";
        //            break;
        //        //市招生办
        //        case 2:
        //            sql = "select xqdm,xxdm,xxmc='['+xxdm+']'+xxmc  from zk_xxdm where xqdm=@xqdm order by xxdm  asc ";
        //            break;
        //        //区招生办
        //        case 3:
        //            sql = "select xqdm,xxdm,xxmc='['+xxdm+']'+xxmc  from zk_xxdm where xqdm=@xqdm and xxdm like '" + fanwei + "%' order by xxdm  asc ";
        //            break;
        //        //学校用户 
        //        case 4:
        //            sql = "select xqdm,xxdm,xxmc='['+xxdm+']'+xxmc  from zk_xxdm where xqdm=@xqdm and xxdm like '" + fanwei + "%'  ";
        //            break;
        //        //班级用户 
        //        case 5:
        //            sql = "select xqdm,xxdm,xxmc='['+xxdm+']'+xxmc  from zk_xxdm where xqdm=@xqdm and xxdm like '" + fanwei + "%' ";
        //            break;
        //    } 

        //    List<SqlParameter> lisP = new List<SqlParameter>(){
        //     new SqlParameter("@xqdm",xqdm)};
        //    DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);

        //    return dt;
        //} 

        /// <summary>
        /// 根据学校代码查询
        /// </summary>
        /// <param name="xxdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public Model_zk_xxdm Select_zk_xxdm(string xxdm)
        {
            Model_zk_xxdm info = new Model_zk_xxdm();
            string sql = "select * from zk_xxdm where xxdm=@xxdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xxdm",xxdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
                info = _dbHelper.DT2EntityList<Model_zk_xxdm>(dt)[0];
            return info;
        }
        /// <summary>
        /// 自定义修改
        /// </summary>
        /// <param name="set"></param>
        /// <param name="where"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool update_zk_xxdm(string set, string where )
        {
            string sql = "update  zk_xxdm set " + set + " where " + where;
            _dbHelper.ExecuteNonQuery(sql, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 根据毕业学校代码修改
        /// </summary>
        /// <param name="item"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool update_zk_xxdm(Model_zk_xxdm item)
        {
            string sql = "update  zk_xxdm set xqdm=@xqdm,xxmc=@xxmc,xxlxdm=@xxlxdm where xxdm=@xxdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",item.Xqdm),
 			 new SqlParameter("@xxmc",item.Xxmc),
 			 new SqlParameter("@xxdm",item.Xxdm),
              new SqlParameter("@xxlxdm",item.Xxlxdm)

			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 根据毕业学校代码修改名称
        /// </summary>
        /// <param name="item"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool update_zk_xxdmMc(Model_zk_xxdm item )
        {
            string sql = "update  zk_xxdm set xxmc=@xxmc where xxdm=@xxdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 		 
 			 new SqlParameter("@xxmc",item.Xxmc),
 			 new SqlParameter("@xxdm",item.Xxdm)
			};
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
        public bool delete_zk_xxdm(string xxdm, ref string error, ref bool bReturn)
        {
            string sql = "delete  zk_xxdm where xxdm=@xxdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xxdm",xxdm)};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 根据县区代码删除
        /// </summary>
        /// <param name="xqdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool delete_zk_xxdmXQ(string xqdm, ref string error, ref bool bReturn)
        {
            string sql = "delete  zk_xxdm where xqdm=@xqdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",xqdm)};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }

        /// <summary>
        /// 根据学校代码查询 返回实体类
        /// </summary> 
        public Model_zk_xxdm Disp(string xxdm)
        {
            Model_zk_xxdm info = new Model_zk_xxdm();
            string sql = "select * from zk_xxdm where xxdm=@xxdm";
            List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@xxdm", xxdm) };
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref error, ref bReturn);
            if (dt.Rows.Count > 0)
                info = _dbHelper.DT2EntityList<Model_zk_xxdm>(dt)[0];
            return info;
        }

        /// <summary>
        /// 根据多个县区代码删除指定数据
        /// </summary>
        /// <param name="xqdms">需要删除的县区代码列表</param>
        /// <returns></returns>
        public bool DeleteDataByxxdms(List<string> xqdms)
        {
            string inStr = "";

            foreach (var str in xqdms)
                inStr += "'" + str + "',";

            string sqlCmd = "Delete zk_xxdm Where xxdm In (" + inStr.Substring(0, inStr.Length - 1) + ")";
            _dbHelper.ExecuteNonQuery(sqlCmd, ref error, ref bReturn);

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
           
            string checkSql = "Select qxId From zk_xqdm Where xqdm=@xqdm;Select xxdm From zk_xxdm Where xxdm=@xxdm And xqdm=@xqdm;";
            bool bReturn = true;

            int i = 0;
            int lost = 0;
            int finish = 0;

            foreach (var element in excel)
            {
                if (element.ColumnNames.Count() != 4)
                    return "导入失败，原因是：输入的文件路径格式不对应，目标格式为3列，您输入的是：" + element.ColumnNames.Count() + "列的格式。";

                string errMsg = "";
                string baseStr = "系统时间：" + DateTime.Now.ToString("yyyy年MM月dd日hh时mm分ss秒") + "。";

                //空数据则跳过
                if (string.IsNullOrEmpty(element[0].ToString().Trim()))
                    continue;
                string xxlx = "";
                if (element[3].ToString().Trim() == "个别考生")
                    xxlx = "2";
                else if (element[3].ToString().Trim() == "应届")
                    xxlx = "1";
                List<SqlParameter> param = new List<SqlParameter> {
                                                new SqlParameter("@xqdm",element[0].ToString().Trim()),
                                                new SqlParameter("@xxdm",element[1].ToString().Trim()),
                                                new SqlParameter("@xxmc",element[2].ToString().Trim()),
                                                new SqlParameter("@xxlxdm",xxlx)
                }; 

                DataSet tmpDataSet = _dbHelper.selectDataSet(checkSql, param, ref errMsg, ref bReturn);

                ////验证学校代码
                //if (string.IsNullOrEmpty(element[1].ToString().Trim()))
                //      errMsg = "第" + (i + 1) + "条数据有误：学校代码为空。"; 
                //验证长度
                if (element[0].ToString().Trim().Length != 3)
                    errMsg = "第" + (i + 1) + "条数据有误，原因是：县区代码" + element[0].ToString().Trim() + "不是3位。";
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
                        else
                        {
                            //需要导入的学校所属县区是否存在
                            if (tmpDataSet.Tables[0].Rows.Count == 0)
                                errMsg = "第" + (i + 1) + "条数据导入时发生错误原因是：该县区不存在请验证本条数据。";
                        }        
                    }
                    catch (Exception)
                    {
                        errMsg = "第" + (i + 1) + "条数据有误，原因是：县区代码" + element[0].ToString().Trim() + "有误。";
                    }

                  
                }
                if (element[3].ToString().Trim().Length == 0 || element[3].ToString().Trim() != "个别考生" && element[3].ToString().Trim() != "应届")
                    errMsg = "第" + (i + 1) + "条数据有误,原因是：学校类型" + element[3].ToString().Trim() + "有错。";
                //验证长度
                if (element[1].ToString().Trim().Length != 5)
                    errMsg = "第" + (i + 1) + "条数据有误,原因是：学校代码" + element[1].ToString().Trim() + "不是5位。";
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
                            //需要导入的学校是否已存在于指定县区
                            if (tmpDataSet.Tables[1].Rows.Count > 0)
                            {
                                if (isZL)
                                {
                                    errMsg += "第" + (i + 1) + "条数据导入时发生错误原因是：该数据已存在（存在的学校代码为：" + element[1].ToString().Trim() + "）。";
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(errMsg))
                                    {
                                        sqlCmd = "UpDate zk_xxdm Set xxmc=@xxmc,xxlxdm=@xxlxdm Where xqdm=@xqdm And xxdm=@xxdm";
                                        _dbHelper.ExecuteNonQuery(sqlCmd, param, ref errMsg, ref bReturn);
                                    }
                                }
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(errMsg))
                                {
                                    sqlCmd = "Insert Into zk_xxdm(xqdm,xxdm,xxmc,xxlxdm)Values(@xqdm,@xxdm,@xxmc,@xxlxdm)";
                                    _dbHelper.ExecuteNonQuery(sqlCmd, param, ref errMsg, ref bReturn);
                                }
                            }

                        }
                    }
                    catch (Exception)
                    {
                        errMsg = "第" + (i + 1) + "条数据有误，原因是：学校代码" + element[1].ToString().Trim() + "有误。";
                    }
                   
                }
                ////如果上面的验证都通过的话就继续执行添加（增量）或修改数据（非增量）
                //if (string.IsNullOrEmpty(errMsg))
                //{
                //        sqlCmd = "Insert Into zk_xxdm(xqdm,xxdm,xxmc,xxlxdm)Values(@xqdm,@xxdm,@xxmc,@xxlxdm)";
                //        _dbHelper.ExecuteNonQuery(sqlCmd, param, ref errMsg, ref bReturn);
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

                return "文件上传失败,请检查重新上传！";
            }
        }

        #endregion
        /// <summary>
        /// 查询所有学校
        /// </summary> 
        public bool Select_zk_xxdm2222(string xqdm,string xxdm,string bjdm)
        {
            string sql = "insert into zk_bjdm (xqdm,xxdm,bjdm,bjmc) VALUES ('" + xqdm + "','" + xxdm + "','" + bjdm + "','" + bjdm + "班" + "')";
            _dbHelper.ExecuteNonQuery(sql, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
    }
}
