/**********************************
 * 
 *   Sql数据库操作类
 * 
 * ***********************************/

using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
using System.Collections.Generic;
using Model;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Text;
using DAL;
namespace BLL
{
    /// <summary>
    /// 获取记录值
    /// </summary>
    /// <param name="dr"></param>
    /// <returns></returns>
    public delegate object PopulateDelegate(IDataReader dr);
    // public delegate T PopulateDelegate<T>(IDataReader dr );


    /// <summary>
    /// 委托将DataReader转为实体类
    /// </summary>
    /// <param name="dr">记录集</param>
    /// <param name="Fileds">字段名列表</param>
    /// <returns></returns>
    public delegate object PopulateDelegate2(IDataReader dr, Dictionary<string, string> Fileds);


    /// <summary>
    /// Sql数据库操作类
    /// </summary>
    public class DBcon
    {


        private SqlConnection sqlCon = null;
        private SqlConnection sqlCon1 = null;
        private SqlCommand sqlCmd = null;
        //private SqlDataReader sqlReader = null;
        private SqlDataAdapter da = null;

        public string ConString = System.Configuration.ConfigurationManager.AppSettings["con"];
        public string ConString2 = System.Configuration.ConfigurationManager.AppSettings["con2"];

        SqlDbHelper_1 _sqldb = new SqlDbHelper_1();

        #region "打开关闭数据库"

        //*******************打开关闭数据库***********************
        /// <summary>
        /// 判断数据库是否打开，如打开，就关闭
        /// </summary>

        public void CloseSqlCon()
        {
            if (sqlCon != null)
            {
                sqlCon.Close();
                sqlCon.Dispose();
            }
            if (sqlCon1 != null)
            {
                sqlCon1.Close();
                sqlCon1.Dispose();
            }
        }

        /// <summary>
        /// 判断数据库是否关闭，如关闭就打开
        /// 
        /// </summary>
        public SqlConnection OpenSqlCon()
        {
            sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == System.Data.ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            return sqlCon;
        }

        #endregion

        #region "DataSet操作方法"

        /// <summary>
        /// 执行有返回值的存储过程
        /// </summary>
        /// <param name="sqlPara" >数组</param>
        /// <param name="procedureName" >存储过程名</param>
        /// <returns></returns>
        public DataSet ReturnDataSetProc(Hashtable _hashTable, string procedureName, string tableName)
        {
            DataSet dSet = new DataSet();
            try
            {

                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    connection.Open();
                    sqlCmd = new SqlCommand(procedureName, connection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    foreach (DictionaryEntry item in _hashTable)
                    {
                        sqlCmd.Parameters.Add(new SqlParameter(item.Key.ToString(), item.Value));
                    }

                    SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                    da.Fill(dSet, tableName);
                    sqlCmd.Parameters.Clear();  //清空
                }
            }
            catch (Exception ex)
            {
                throw new Exception("respon", ex);
            }
            return dSet;
        }



        /// <summary>
        /// 执行无返回值的存储过程
        /// </summary>
        public void ReturnNoProc(Hashtable _hashTable, string procedureName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    connection.Open();
                    sqlCmd = new SqlCommand(procedureName, connection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    //sqlCmd.CommandTimeout = 20;
                    foreach (DictionaryEntry item in _hashTable)
                    {
                        sqlCmd.Parameters.Add(new SqlParameter(item.Key.ToString(), item.Value));
                    }
                    sqlCmd.ExecuteNonQuery();
                    sqlCmd.Parameters.Clear();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("respon", ex);
                // throw new Exception("执行无返回值的存储过程有误");
            }
        }


        /// <summary>
        /// 执行无返回值的存储过程
        /// </summary>
        public void ReturnNoProc_kc(Hashtable _hashTable, string procedureName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    connection.Open();
                    sqlCmd = new SqlCommand(procedureName, connection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandTimeout = 420;
                    foreach (DictionaryEntry item in _hashTable)
                    {
                        sqlCmd.Parameters.Add(new SqlParameter(item.Key.ToString(), item.Value));
                    }
                    sqlCmd.ExecuteNonQuery();
                    sqlCmd.Parameters.Clear();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("respon", ex);
                //  throw new Exception("执行无返回值的存储过程有误");
            }
        }

        /// <summary>
        /// 执行无返回值的存储过程
        /// </summary>
        public void NoReturnzybk(SqlConnection sqlCon, SqlParameter[] sqlPara, string procedureName)
        {
            //OpenSqlCon();
            sqlCmd = new SqlCommand(procedureName, sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 120;
            if (sqlPara != null)
            {
                foreach (SqlParameter sqlpt in sqlPara)
                {
                    sqlCmd.Parameters.Add(sqlpt);
                }
            }
            sqlCmd.ExecuteNonQuery();
            sqlCmd.Parameters.Clear();
        }

        /// <summary>
        /// 执行无返回值的存储过程
        /// </summary>
        public void NoReturn(Hashtable _hashTable, string procedureName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    connection.Open();
                    sqlCmd = new SqlCommand(procedureName, connection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    foreach (DictionaryEntry item in _hashTable)
                    {
                        sqlCmd.Parameters.Add(new SqlParameter(item.Key.ToString(), item.Value));
                    }
                }
                sqlCmd.ExecuteNonQuery();
                sqlCmd.Parameters.Clear();  //清空
            }
            catch (Exception ex)
            {
                throw new Exception("ee", ex);
            }


        }


        /// <summary>
        /// 执行SQL查找语句
        /// </summary>
        /// <param name="ds">要填充的ds</param>
        /// <param name="strsql">查询语句</param>
        /// <param name="TableName">表名</param>
        /// <returns>返回填充好了的DataSet结果集</returns>
        public DataSet DataAdapterSearch(DataSet dsFill, string strsql, string TableName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    connection.Open();
                    da = new SqlDataAdapter(strsql, connection);
                    da.Fill(dsFill, TableName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("执行DataAdapterSearch(DataSet dsFill,string strsql, string TableName)查找语句错误", ex);
            }
            finally
            {
                da.Dispose();
            }
            return dsFill;
        }


        /// <summary>
        /// 执行SQL查找语句
        /// </summary>
        /// <param name="ds">要填充的ds</param>
        /// <param name="strsql">查询语句</param>
        /// <param name="TableName">表名</param>
        /// <returns>返回填充好了的DataSet结果集</returns>
        public DataSet DataAdapterSearch(string strsql, string TableName)
        {
            DataSet dsFill = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    connection.Open();
                    da = new SqlDataAdapter(strsql, connection);
                    da.Fill(dsFill, TableName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("执行DataAdapterSearch(string strsql, string TableName)查找语句错误", ex);
            }
            finally
            {
                da.Dispose();
            }
            return dsFill;
        }

        /// <summary>
        /// 执行SQL查找语句
        /// </summary>
        /// <param name="ds">要填充的ds</param>
        /// <param name="strsql">查询语句</param>
        /// <param name="TableName">表名</param>
        /// <returns>返回填充好了的DataSet结果集</returns>
        public DataSet DataAdapterSearch2(string strsql, string TableName)
        {
            DataSet dsFill = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConString2))
                {
                    connection.Open();
                    da = new SqlDataAdapter(strsql, connection);
                    da.Fill(dsFill, TableName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("执行DataAdapterSearch(string strsql, string TableName)查找语句错误", ex);
            }
            finally
            {
                da.Dispose();
            }
            return dsFill;
        }


        /// <summary>
        /// 数据库数据导入更新(传DataSet和DataTable的对象都可以)
        /// </summary>
        /// <param name="changedDs">DataSet对象</param>
        /// <param name="tableName">与数据库对应的表名</param>
        /// <param name="strSql">查询对应数据库表名的SQL语句</param>
        /// <returns>DataSet对象更新了的数据库表</returns>
        public DataSet UpdateDs(DataSet changedDs, string tableName, string strSql)
        {
            try
            {
                Hashtable ht = new Hashtable();
                SqlParameter pm = null;
                DataSet ds = new DataSet();
                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter("select * from jhk where 1 != 1", connection);
                    da.Fill(ds, "jhk2");



                    for (int i = 0; i < changedDs.Tables["jhk"].Columns.Count; i++)
                    {
                        if (ds.Tables["jhk2"].Columns.Contains(changedDs.Tables["jhk"].Columns[i].ColumnName))
                        {
                            ht.Add(i, changedDs.Tables["jhk"].Columns[i].ColumnName);
                        }
                        else
                        {
                            throw new Exception("当前导入列" + changedDs.Tables["jhk"].Columns[i].ColumnName + "在数据库中不存在，导入数据失败");
                        }
                    }
                    ds.Tables["jhk2"].Columns.Clear();
                    for (int j = 0; j < ht.Count; j++)
                    {
                        ds.Tables["jhk2"].Columns.Add(changedDs.Tables["jhk"].Columns[j].ColumnName);
                    }




                    DataRow[] row = new DataRow[changedDs.Tables["jhk"].Rows.Count];
                    changedDs.Tables["jhk"].Rows.CopyTo(row, 0);
                    for (int i = 0; i < row.Length; i++)
                    {
                        ds.Tables["jhk2"].Rows.Add(row[i].ItemArray);
                    }

                    da.InsertCommand = new SqlCommand(strSql, connection);
                    pm = da.InsertCommand.Parameters.Add("@zzxdh", SqlDbType.NVarChar);
                    pm.SourceColumn = "zzxdh";
                    pm.SourceVersion = DataRowVersion.Current;

                    pm = da.InsertCommand.Parameters.Add("@zzxmc", SqlDbType.NVarChar);
                    pm.SourceColumn = "zzxmc";
                    pm.SourceVersion = DataRowVersion.Current;

                    pm = da.InsertCommand.Parameters.Add("@xxjh", SqlDbType.NVarChar);
                    pm.SourceColumn = "xxjh";
                    pm.SourceVersion = DataRowVersion.Current;

                    pm = da.InsertCommand.Parameters.Add("@zydh", SqlDbType.NVarChar);
                    pm.SourceColumn = "zydh";
                    pm.SourceVersion = DataRowVersion.Current;

                    pm = da.InsertCommand.Parameters.Add("@zymc", SqlDbType.NVarChar);
                    pm.SourceColumn = "zymc";
                    pm.SourceVersion = DataRowVersion.Current;

                    pm = da.InsertCommand.Parameters.Add("@xz", SqlDbType.NVarChar);
                    pm.SourceColumn = "xz";
                    pm.SourceVersion = DataRowVersion.Current;

                    pm = da.InsertCommand.Parameters.Add("@zyjh", SqlDbType.NVarChar);
                    pm.SourceColumn = "zyjh";
                    pm.SourceVersion = DataRowVersion.Current;
                    //sqlCmdBuilder = new SqlCommandBuilder(da);

                    da.Update(ds, "jhk2");
                    ds.AcceptChanges();

                    //da.Update(changedDs, tableName);
                    // angedDs.AcceptChanges();
                    return ds;//返回更新了的数据库表
                }
            }
            catch
            {
                throw new Exception("数据导入更新失败");
            }
        }



        /// <summary>
        /// 执行SQL语句(非查找语句)
        /// </summary>
        /// <param name="strsql"></param>
        /// <returns></returns>
        public int CommandSql(string strsql)
        {
            SqlCommand cmd = null;
            int num = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    connection.Open();
                    cmd = new SqlCommand(strsql, connection);
                    num = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("数据库错误,执行SQL语句(非查找语句)", ex);
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
            }
            return num;
        }


        /// <summary>
        /// 显示数据 分页通用函数
        /// </summary>
        /// <param name="TableList">要显示字段</param>
        /// <param name="TableName">表名</param>
        /// <param name="SelectWhere">查询条件</param>
        /// <param name="SelectOrderId">主键</param>
        /// <param name="SelectOrder">排序</param>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">每页多少条记录</param>
        /// <returns></returns>
        public ArrayList Show_caozuo(string TableList, string TableName, string SelectWhere, string SelectOrderId, string SelectOrder, int pageNo, int pageSize)
        {
            try
            {
                ArrayList arrContain = new ArrayList();
                DataSet ds = new DataSet();
                using (SqlConnection connection = new SqlConnection(ConString))
                {

                    connection.Open();
                    SqlCommand sqlCmd = new SqlCommand("pd_GetDataset", connection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                    sqlCmd.Parameters.Add("@TableList", SqlDbType.VarChar, 400).Value = TableList;
                    sqlCmd.Parameters.Add("@TableName", SqlDbType.VarChar, 30).Value = TableName;
                    sqlCmd.Parameters.Add("@SelectWhere", SqlDbType.VarChar, 700).Value = SelectWhere;
                    sqlCmd.Parameters.Add("@SelectOrderId", SqlDbType.VarChar, 20).Value = SelectOrderId;
                    sqlCmd.Parameters.Add("@SelectOrder", SqlDbType.VarChar, 200).Value = SelectOrder;
                    sqlCmd.Parameters.Add("@intPageNo", SqlDbType.Int).Value = pageNo;
                    sqlCmd.Parameters.Add("@intPageSize", SqlDbType.Int).Value = pageSize;
                    sqlCmd.Parameters.Add("@RecordCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                    sqlCmd.Parameters.Add("RowCount", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;


                    da.Fill(ds);

                    int nRecordCount = (int)sqlCmd.Parameters["@RecordCount"].Value;  //求出总记录数，该值是output出来的值
                    int nRowCount = (int)sqlCmd.Parameters["RowCount"].Value;         //求出当前页中的记录数，在最后一页不等于pagesize，

                    arrContain.Add(ds);
                    arrContain.Add(nRecordCount);  //记录总数
                    arrContain.Add(nRowCount);    //行数


                }
                return arrContain;
            }
            catch (Exception ex)
            {
                throw new Exception("执行无返回值的存储过程有误" + ex);
            }
        }

        /// <summary>
        /// 显示数据 分页通用函数
        /// </summary>
        /// <param name="schoolCode"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ArrayList Show_caozuo2(string TableList, string TableName, string SelectWhere, string SelectOrderId, string SelectOrder, int pageNo, int pageSize)
        {
            try
            {
                ArrayList arrContain = new ArrayList();
                DataSet ds = new DataSet();
                using (SqlConnection connection = new SqlConnection(ConString2))
                {

                    connection.Open();
                    SqlCommand sqlCmd = new SqlCommand("pd_GetDataset", connection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                    sqlCmd.Parameters.Add("@TableList", SqlDbType.VarChar, 400).Value = TableList;
                    sqlCmd.Parameters.Add("@TableName", SqlDbType.VarChar, 30).Value = TableName;
                    sqlCmd.Parameters.Add("@SelectWhere", SqlDbType.VarChar, 700).Value = SelectWhere;
                    sqlCmd.Parameters.Add("@SelectOrderId", SqlDbType.VarChar, 20).Value = SelectOrderId;
                    sqlCmd.Parameters.Add("@SelectOrder", SqlDbType.VarChar, 200).Value = SelectOrder;
                    sqlCmd.Parameters.Add("@intPageNo", SqlDbType.Int).Value = pageNo;
                    sqlCmd.Parameters.Add("@intPageSize", SqlDbType.Int).Value = pageSize;
                    sqlCmd.Parameters.Add("@RecordCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                    sqlCmd.Parameters.Add("RowCount", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;


                    da.Fill(ds);

                    int nRecordCount = (int)sqlCmd.Parameters["@RecordCount"].Value;  //求出总记录数，该值是output出来的值
                    int nRowCount = (int)sqlCmd.Parameters["RowCount"].Value;         //求出当前页中的记录数，在最后一页不等于pagesize，

                    arrContain.Add(ds);
                    arrContain.Add(nRecordCount);  //记录总数
                    arrContain.Add(nRowCount);    //行数


                }
                return arrContain;
            }
            catch (Exception ex)
            {
                throw new Exception("执行无返回值的存储过程有误" + ex);
            }
        }
        #endregion

        #region "显示数据 分页通用函数"
        /// <summary>
        /// 显示数据 分页通用函数
        /// </summary>
        /// <param name="qp">分页实体类</param>
        /// <returns></returns>
        private ArrayList GetDataset(PopulateDelegate pd, QueryParam qp, out int RecordCount)
        {


            // List<T> arrContain = new List<T>();

            ArrayList arrContain = new ArrayList();
            RecordCount = 0;
            using (SqlConnection connection = new SqlConnection(ConString))
            {
                try
                {
                    connection.Open();
                    SqlCommand sqlCmd = new SqlCommand("pd_GetDataset", connection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    //SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                    sqlCmd.Parameters.Add("@TableList", SqlDbType.VarChar, 400).Value = qp.ReturnFields;
                    sqlCmd.Parameters.Add("@TableName", SqlDbType.VarChar, 30).Value = qp.TableName;
                    sqlCmd.Parameters.Add("@SelectWhere", SqlDbType.VarChar, 700).Value = qp.Where;
                    sqlCmd.Parameters.Add("@SelectOrderId", SqlDbType.VarChar, 20).Value = qp.OrderId;
                    sqlCmd.Parameters.Add("@SelectOrder", SqlDbType.VarChar, 200).Value = qp.Order;
                    sqlCmd.Parameters.Add("@intPageNo", SqlDbType.Int).Value = qp.PageIndex;
                    sqlCmd.Parameters.Add("@intPageSize", SqlDbType.Int).Value = qp.PageSize;
                    sqlCmd.Parameters.Add("@RecordCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                    sqlCmd.Parameters.Add("RowCount", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    SqlDataReader dr = sqlCmd.ExecuteReader();
                    //da.Fill(ds);

                    //int nRecordCount = (int)sqlCmd.Parameters["@RecordCount"].Value;  //求出总记录数，该值是output出来的值
                    //int nRowCount = (int)sqlCmd.Parameters["RowCount"].Value;         //求出当前页中的记录数，在最后一页不等于pagesize，

                    while (dr.Read())
                    {
                        arrContain.Add(pd(dr));
                    }

                    // 取记录总数 及页数
                    if (dr.NextResult())
                    {
                        if (dr.Read())
                        {
                            RecordCount = Convert.ToInt32(dr["RecordCount"]);
                        }
                    }

                    dr.Close();
                    dr.Dispose();
                    sqlCmd.Dispose();
                    connection.Close();

                    //arrContain.Add(nRecordCount);  //记录总数
                    //arrContain.Add(nRowCount);    //行数

                }
                catch (Exception ex)
                {

                    sqlCmd.Dispose();
                    connection.Close();
                    new DAL.SqlDbHelper_1().writeErrorInfo(ex.Message);
                }

            }
            return arrContain;

        }



        /// <summary>
        /// 显示数据 分页通用函数
        /// </summary>
        /// <param name="qp">分页实体类</param>
        /// <returns></returns>
        private ArrayList GetDataset2(PopulateDelegate2 pd, QueryParam qp, out int RecordCount)
        {

            ArrayList arrContain = new ArrayList();
            RecordCount = 0;
            using (SqlConnection connection = new SqlConnection(ConString))
            {
                try
                {
                    connection.Open();
                    SqlCommand sqlCmd = new SqlCommand("pd_GetDataset", connection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    sqlCmd.Parameters.Add("@TableList", SqlDbType.VarChar, 400).Value = qp.ReturnFields;
                    sqlCmd.Parameters.Add("@TableName", SqlDbType.VarChar, 30).Value = qp.TableName;
                    sqlCmd.Parameters.Add("@SelectWhere", SqlDbType.VarChar, 700).Value = qp.Where;
                    sqlCmd.Parameters.Add("@SelectOrderId", SqlDbType.VarChar, 20).Value = qp.OrderId;
                    sqlCmd.Parameters.Add("@SelectOrder", SqlDbType.VarChar, 200).Value = qp.Order;
                    sqlCmd.Parameters.Add("@intPageNo", SqlDbType.Int).Value = qp.PageIndex;
                    sqlCmd.Parameters.Add("@intPageSize", SqlDbType.Int).Value = qp.PageSize;
                    sqlCmd.Parameters.Add("@RecordCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                    sqlCmd.Parameters.Add("RowCount", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    SqlDataReader dr = sqlCmd.ExecuteReader();

                    Dictionary<string, string> Fileds = new Dictionary<string, string>();
                    if (dr.FieldCount > 0)
                    {
                        foreach (DataRow var in dr.GetSchemaTable().Select())
                        {
                            Fileds.Add(var[0].ToString(), var[0].ToString());
                        }
                    }
                    while (dr.Read())
                    {
                        arrContain.Add(pd(dr, Fileds));
                    }

                    // 取记录总数 及页数
                    if (dr.NextResult())
                    {
                        if (dr.Read())
                        {
                            RecordCount = Convert.ToInt32(dr["RecordCount"]);
                        }
                    }

                    dr.Close();
                    dr.Dispose();
                    sqlCmd.Dispose();
                    connection.Close();

                }
                catch (Exception ex)
                {

                    sqlCmd.Dispose();
                    connection.Close();
                    new DAL.SqlDbHelper_1().writeErrorInfo(ex.Message);
                }

            }
            return arrContain;

        }


        ///// <summary>
        ///// 显示数据 分页通用函数
        ///// </summary>
        ///// <param name="qp">分页实体类</param>
        ///// <returns></returns>
        //private DataSet GetDataset( QueryParam qp, out int RecordCount)
        //{


        //    // List<T> arrContain = new List<T>();
        //    DataSet ds = new DataSet();

        //    RecordCount = 0;
        //    using (SqlConnection connection = new SqlConnection(ConString))
        //    {
        //        try
        //        {
        //            connection.Open();
        //            SqlCommand sqlCmd = new SqlCommand("pd_GetDataset", connection);
        //            sqlCmd.CommandType = CommandType.StoredProcedure;
        //            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
        //            sqlCmd.Parameters.Add("@TableList", SqlDbType.VarChar, 400).Value = qp.ReturnFields;
        //            sqlCmd.Parameters.Add("@TableName", SqlDbType.VarChar, 30).Value = qp.TableName;
        //            sqlCmd.Parameters.Add("@SelectWhere", SqlDbType.VarChar, 700).Value = qp.Where;
        //            sqlCmd.Parameters.Add("@SelectOrderId", SqlDbType.VarChar, 20).Value = qp.OrderId;
        //            sqlCmd.Parameters.Add("@SelectOrder", SqlDbType.VarChar, 200).Value = qp.Order;
        //            sqlCmd.Parameters.Add("@intPageNo", SqlDbType.Int).Value = qp.PageIndex;
        //            sqlCmd.Parameters.Add("@intPageSize", SqlDbType.Int).Value = qp.PageSize;
        //            sqlCmd.Parameters.Add("@RecordCount", SqlDbType.Int).Direction = ParameterDirection.Output;
        //            sqlCmd.Parameters.Add("RowCount", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

        //            // SqlDataReader dr = sqlCmd.ExecuteReader();
        //            da.Fill(ds);

        //            RecordCount = (int)sqlCmd.Parameters["@RecordCount"].Value;  //求出总记录数，该值是output出来的值
        //            //int nRowCount = (int)sqlCmd.Parameters["RowCount"].Value;         //求出当前页中的记录数，在最后一页不等于pagesize，

        //          //  arrContain.Add(ds);

        //            connection.Close();
        //            connection.Dispose();

        //            //arrContain.Add(nRecordCount);  //记录总数
        //            //arrContain.Add(nRowCount);    //行数

        //        }
        //        catch (Exception ex)
        //        {
        //            connection.Close();
        //            connection.Dispose();

        //            new DAL.SqlDbHelper_1().writeErrorInfo(ex.Message);
        //        }

        //    }
        //    return ds; //arrContain;

        //}

        #endregion

        //---------------------------- 系统数据类 ----------------------------//

        #region "Sys_Users - Data"

        /// <summary>
        /// 新增/删除/修改 Sys_users (Sys_users)
        /// </summary>
        /// <param name="fam">sys_UserTable实体类(Sys_users)</param>
        /// <returns>-1:存储过程执行失败,-2:存在相同的主键,Insert:返回插入自动ID,Update:返回更新记录数,Delete:返回删除记录数</returns>
        public int Sys_usersInsertUpdateDelete(sys_UserTable fam)
        {
            int rInt = -1;
            using (SqlConnection Conn = new SqlConnection(ConString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.Sys_users_InsertUpdateDelete", Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //设置参数
                    cmd.Parameters.Add("@DataTable_Action_", SqlDbType.VarChar).Value = fam.DB_Option_Action_.ToString(); //操作方法 Insert:增加 Update:修改 Delete:删除

                    cmd.Parameters.Add("@Userid", SqlDbType.Int).Value = fam.UserID;  //用户ID标识
                    cmd.Parameters.Add("@U_xm", SqlDbType.VarChar).Value = fam.U_xm;  //用户中文姓名
                    cmd.Parameters.Add("@U_xb", SqlDbType.VarChar).Value = fam.U_xb;  //用户性别
                    cmd.Parameters.Add("@U_phone", SqlDbType.VarChar).Value = fam.U_phone;  //联系电话
                    cmd.Parameters.Add("@U_loginname", SqlDbType.VarChar).Value = fam.U_LoginName;  //用户登录名
                    cmd.Parameters.Add("@U_password", SqlDbType.VarChar).Value = fam.U_Password;  //用户登陆密码
                    cmd.Parameters.Add("@U_key", SqlDbType.VarChar).Value = fam.U_key;  //用户密匙号
                    cmd.Parameters.Add("@U_city", SqlDbType.VarChar).Value = fam.U_city;  //用户所在市
                    cmd.Parameters.Add("@U_area", SqlDbType.NVarChar).Value = fam.U_area;  //用户所在县区
                    cmd.Parameters.Add("@U_jigou", SqlDbType.VarChar).Value = fam.U_jigou;  //用户所属机构ID
                    cmd.Parameters.Add("@U_department", SqlDbType.VarChar).Value = fam.U_department;  //用户管辖范围
                    cmd.Parameters.Add("@U_usertype", SqlDbType.Int).Value = fam.U_usertype;  //用户类型
                    cmd.Parameters.Add("@U_tag", SqlDbType.Int).Value = fam.U_tag;  //用户状态
                    cmd.Parameters.Add("@U_ip", SqlDbType.VarChar).Value = fam.U_ip;  //用户登陆ip
                    if (fam.U_datetime.HasValue)
                        cmd.Parameters.Add("@U_datetime", SqlDbType.DateTime).Value = fam.U_datetime;  //用户登陆时间
                    else
                        cmd.Parameters.Add("@U_datetime", SqlDbType.DateTime).Value = DBNull.Value;  //用户登陆时间
                    cmd.Parameters.Add("@U_errnumber", SqlDbType.Int).Value = fam.U_errnumber;  //用户登陆密码错误次数
                    Conn.Open();
                    rInt = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    Conn.Dispose();
                    Conn.Close();
                }
                catch (Exception ex)
                {

                    Conn.Dispose();
                    Conn.Close();
                    new DAL.SqlDbHelper_1().writeErrorInfo(ex.Message);
                }

            }
            return rInt;
        }


        /// <summary>
        /// 用户表查询 返回数据
        /// </summary>
        /// <param name="qp"></param>
        /// <returns></returns>
        public DataSet Sys_UserList(QueryParam qp, out int RecordCount)
        {
            // PopulateDelegate mypd = new PopulateDelegate(Populatesys_User);

            return _sqldb.GetDataset(qp, out RecordCount);
        }

        /// <summary>
        /// 将记录集转为sys_UserTable实体类
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <returns>sys_UserTable</returns>
        protected object Populatesys_User(IDataReader dr)
        {
            sys_UserTable nc = new sys_UserTable();

            if (!Convert.IsDBNull(dr["Userid"])) nc.UserID = Convert.ToInt32(dr["Userid"]); // 用户ID标识
            if (!Convert.IsDBNull(dr["U_xm"])) nc.U_xm = Convert.ToString(dr["U_xm"]).Trim(); // 用户中文姓名
            if (!Convert.IsDBNull(dr["U_xb"])) nc.U_xb = Convert.ToString(dr["U_xb"]).Trim(); // 用户性别
            if (!Convert.IsDBNull(dr["U_phone"])) nc.U_phone = Convert.ToString(dr["U_phone"]).Trim(); // 联系电话            
            if (!Convert.IsDBNull(dr["U_loginname"])) nc.U_LoginName = Convert.ToString(dr["U_loginname"]).Trim(); // 用户登录名
            if (!Convert.IsDBNull(dr["U_password"])) nc.U_Password = Convert.ToString(dr["U_password"]).Trim(); // 用户登陆密码
            if (!Convert.IsDBNull(dr["U_key"])) nc.U_key = Convert.ToString(dr["U_key"]).Trim(); // 用户密匙号
            if (!Convert.IsDBNull(dr["U_city"])) nc.U_city = Convert.ToString(dr["U_city"]).Trim(); // 用户所在市
            if (!Convert.IsDBNull(dr["U_area"])) nc.U_area = Convert.ToString(dr["U_area"]).Trim(); // 用户所在县区
            if (!Convert.IsDBNull(dr["U_jigou"])) nc.U_jigou = Convert.ToString(dr["U_jigou"]).Trim(); // 用户所属机构ID
            if (!Convert.IsDBNull(dr["U_department"])) nc.U_department = Convert.ToString(dr["U_department"]); // 用户管辖范围
            if (!Convert.IsDBNull(dr["U_usertype"])) nc.U_usertype = Convert.ToInt32(dr["U_usertype"]); // 用户类型
            if (!Convert.IsDBNull(dr["U_tag"])) nc.U_tag = Convert.ToInt32(dr["U_tag"]); // 用户状态
            if (!Convert.IsDBNull(dr["U_ip"])) nc.U_ip = Convert.ToString(dr["U_ip"]).Trim(); // 用户登陆ip
            if (!Convert.IsDBNull(dr["U_datetime"])) nc.U_datetime = Convert.ToDateTime(dr["U_datetime"]); // 用户登陆时间
            if (!Convert.IsDBNull(dr["U_errnumber"])) nc.U_errnumber = Convert.ToInt32(dr["U_errnumber"]); // 用户登陆密码错误次数
            return nc;
        }
        #endregion

        #region "Sys_UserType - Data "

        /// <summary>
        /// 新增/删除/修改 Sys_UserType (Sys_UserType)
        /// </summary>
        /// <param name="fam">Sys_UserTypeEntity实体类(Sys_UserType)</param>
        /// <returns>-1:存储过程执行失败,-2:存在相同的主键,Insert:返回插入自动ID,Update:返回更新记录数,Delete:返回删除记录数</returns>
        public int Sys_UserTypeInsertUpdateDelete(Sys_UserTypeTable fam)
        {
            int rInt = -1;
            using (SqlConnection Conn = new SqlConnection(ConString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.Sys_UserType_InsertUpdateDelete", Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //设置参数
                    cmd.Parameters.Add("@DataTable_Action_", SqlDbType.VarChar).Value = fam.DataTable_Action_.ToString(); //操作方法 Insert:增加 Update:修改 Delete:删除

                    cmd.Parameters.Add("@TypeID", SqlDbType.Int).Value = fam.TypeID;  //用户类型ID
                    cmd.Parameters.Add("@T_Name", SqlDbType.VarChar).Value = fam.T_Name;  //用户类型
                    Conn.Open();
                    rInt = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    Conn.Dispose();
                    Conn.Close();
                }
                catch (Exception ex)
                {

                    Conn.Dispose();
                    Conn.Close();
                    new DAL.SqlDbHelper_1().writeErrorInfo(ex.Message);
                }
            }
            return rInt;
        }

        /// <summary>
        /// 返回Sys_UserType实体类的ArrayList对象 (Sys_UserType)
        /// </summary>
        /// <param name="qp">查询类(非安全函数,传入参数请进行Sql字符串过滤)</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>Sys_UserType实体类的ArrayList对象(Sys_UserType)</returns>
        public DataSet Sys_UserTypeList(QueryParam qp, out int RecordCount)
        {
            //PopulateDelegate mypd = new PopulateDelegate(PopulateSys_UserTypeTable);
            return _sqldb.GetDataset(qp, out RecordCount);
        }

        /// <summary>
        /// 将记录集转为Sys_UserTypeEntity实体类 (Sys_UserType)
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <param name="Fileds">字段名列表</param>
        /// <returns>Sys_UserTypeEntity</returns>
        protected Sys_UserTypeTable PopulateSys_UserTypeTable(IDataReader dr)
        {
            Sys_UserTypeTable nc = new Sys_UserTypeTable();

            if (!Convert.IsDBNull(dr["TypeID"])) nc.TypeID = Convert.ToInt32(dr["TypeID"]); // 用户类型ID
            if (!Convert.IsDBNull(dr["T_Name"])) nc.T_Name = Convert.ToString(dr["T_Name"]).Trim(); // 用户类型
            return nc;
        }

        #endregion

        #region "Sys_userroles - Data"

        /// <summary>
        /// 新增/删除/修改 Sys_userroles 
        /// </summary>
        /// <param name="fam">sys_UserRolesTable实体类(Sys_userroles)</param>
        /// <returns>-1:存储过程执行失败,-2:存在相同的主键,Insert:返回插入自动ID,Update:返回更新记录数,Delete:返回删除记录数</returns>
        public int Sys_UserRolesInsertUpdateDelete(sys_UserRolesTable fam)
        {
            int rInt = -1;
            using (SqlConnection Conn = new SqlConnection(ConString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.Sys_userroles_InsertUpdateDelete", Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //设置参数
                    cmd.Parameters.Add("@DataTable_Action_", SqlDbType.VarChar).Value = fam.DB_Option_Action_.ToString(); //操作方法 Insert:增加 Update:修改 Delete:删除
                    cmd.Parameters.Add("@R_roleid", SqlDbType.Int).Value = fam.R_RoleID;  //角色ID标识
                    cmd.Parameters.Add("@R_UserName", SqlDbType.VarChar).Value = fam.R_UserName;  //用户名称         
                    Conn.Open();
                    rInt = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    Conn.Dispose();
                    Conn.Close();
                }
                catch (Exception ex)
                {

                    Conn.Dispose();
                    Conn.Close();
                    new DAL.SqlDbHelper_1().writeErrorInfo(ex.Message);
                }
            }
            return rInt;
        }

        /// <summary>
        ///  返回 Sys_userrolesList  实体类的ArrayList对象
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="RecordCount"></param>
        /// <returns></returns>
        public DataSet sys_UserRolesList(QueryParam qp, out int RecordCount)
        {
            //PopulateDelegate mypd = new PopulateDelegate(PopulateSys_userroles);

            return _sqldb.GetDataset(qp, out RecordCount);
        }

        /// <summary>
        /// 将记录集转为Sys_userrolesTable实体类 (Sys_userroles)
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="Fileds"></param>
        /// <returns></returns>
        protected sys_UserRolesTable PopulateSys_userroles(IDataReader dr)
        {
            sys_UserRolesTable nc = new sys_UserRolesTable();

            if (!Convert.IsDBNull(dr["R_UserName"])) nc.R_UserName = Convert.ToString(dr["R_UserName"]); // 用户ID与sys_User表中R_UserName相关
            if (!Convert.IsDBNull(dr["R_RoleID"])) nc.R_RoleID = Convert.ToInt32(dr["R_RoleID"]); // 用户所属角色ID与Sys_Roles关联
            return nc;
        }


        #endregion

        #region "sys_Module - Data"

        /// <summary>
        /// 新增/删除/修改 Sys_module (Sys_module)
        /// </summary>
        /// <param name="fam">Sys_moduleEntity实体类(Sys_module)</param>
        /// <returns>-1:存储过程执行失败,-2:存在相同的主键,Insert:返回插入自动ID,Update:返回更新记录数,Delete:返回删除记录数</returns>
        public int Sys_moduleInsertUpdateDelete(Sys_moduleTable fam)
        {
            int rInt = -1;
            using (SqlConnection Conn = new SqlConnection(ConString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.Sys_module_InsertUpdateDelete", Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //设置参数
                    cmd.Parameters.Add("@DataTable_Action_", SqlDbType.VarChar).Value = fam.DataTable_Action_.ToString(); //操作方法 Insert:增加 Update:修改 Delete:删除

                    cmd.Parameters.Add("@Moduleid", SqlDbType.Int).Value = fam.Moduleid;  //功能模块ID标识
                    cmd.Parameters.Add("@M_modulename", SqlDbType.VarChar).Value = fam.M_modulename;  //模块名称
                    cmd.Parameters.Add("@M_order", SqlDbType.Int).Value = fam.M_order;  //模块排序
                    cmd.Parameters.Add("@M_tag", SqlDbType.VarChar).Value = fam.M_tag;  //模块开启标识,1开启,0关闭,默认1
                    cmd.Parameters.Add("@OrderType", SqlDbType.VarChar).Value = fam.OrderType;  //排序类别 1向上,2向下
                    Conn.Open();
                    rInt = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    Conn.Dispose();
                    Conn.Close();
                }
                catch (Exception ex)
                {

                    Conn.Dispose();
                    Conn.Close();
                    new DAL.SqlDbHelper_1().writeErrorInfo(ex.Message);
                }
            }
            return rInt;
        }

        /// <summary>
        /// 返回ModuleList实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_Module实体类的ArrayList对象</returns>
        public DataSet ModuleList(QueryParam qp, out int RecordCount)
        {
            // PopulateDelegate mypd = new PopulateDelegate(PopulateSys_module);

            return _sqldb.GetDataset(qp, out RecordCount);
        }

        /// <summary>
        /// 将记录集转为Sys_moduleEntity实体类 (Sys_module)
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <param name="Fileds">字段名列表</param>
        /// <returns>Sys_moduleEntity</returns>
        protected Sys_moduleTable PopulateSys_module(IDataReader dr)
        {
            Sys_moduleTable nc = new Sys_moduleTable();

            if (!Convert.IsDBNull(dr["Moduleid"])) nc.Moduleid = Convert.ToInt32(dr["Moduleid"]); // 功能模块ID标识
            if (!Convert.IsDBNull(dr["M_modulename"])) nc.M_modulename = Convert.ToString(dr["M_modulename"]).Trim(); // 模块名称
            if (!Convert.IsDBNull(dr["M_order"])) nc.M_order = Convert.ToInt32(dr["M_order"]); // 模块排序
            if (!Convert.IsDBNull(dr["M_tag"])) nc.M_tag = Convert.ToString(dr["M_tag"]).Trim(); // 模块开启标识,1开启,0关闭,默认1
            return nc;
        }
        #endregion

        #region "sys_RoleModule - Data"

        /// <summary>
        /// 新增/删除/修改 Sys_Rolemodule (Sys_rolemodules)
        /// </summary>
        /// <param name="fam">sys_RoleModuleTable实体类(Sys_module)</param>
        /// <returns>-1:存储过程执行失败,-2:存在相同的主键,Insert:返回插入自动ID,Update:返回更新记录数,Delete:返回删除记录数</returns>
        public int Sys_RoleModuleInsertUpdateDelete(sys_RoleModuleTable fam)
        {
            int rInt = -1;
            using (SqlConnection Conn = new SqlConnection(ConString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.Sys_rolemodules_InsertUpdateDelete", Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //设置参数
                    cmd.Parameters.Add("@DataTable_Action_", SqlDbType.VarChar).Value = fam.DB_Option_Action_.ToString(); //操作方法 Insert:增加 Update:修改 Delete:删除

                    cmd.Parameters.Add("@A_roleid", SqlDbType.Int).Value = fam.A_RoleID;  //角色ID
                    cmd.Parameters.Add("@A_moduleid", SqlDbType.VarChar).Value = fam.A_moduleid;  //模块ID  
                    cmd.Parameters.Add("@A_moduleids", SqlDbType.VarChar).Value = fam.A_moduleids;  //多个模块ID 用“,”隔开        
                    Conn.Open();
                    rInt = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    Conn.Dispose();
                    Conn.Close();
                }
                catch (Exception ex)
                {

                    Conn.Dispose();
                    Conn.Close();
                    new DAL.SqlDbHelper_1().writeErrorInfo(ex.Message);
                }
            }
            return rInt;
        }

        /// <summary>
        /// 返回RoleModuleList实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_RoleModule实体类的ArrayList对象</returns>
        public DataSet RoleModuleList(QueryParam qp, out int RecordCount)
        {
            //  PopulateDelegate mypd = new PopulateDelegate(PopulateSys_Rolemodule);

            return _sqldb.GetDataset(qp, out RecordCount);
        }

        /// <summary>
        /// 将记录集转为sys_RoleModuleTable实体类 (sys_RoleModule)
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <param name="Fileds">字段名列表</param>
        /// <returns>sys_RoleModuleTable</returns>
        protected sys_RoleModuleTable PopulateSys_Rolemodule(IDataReader dr)
        {
            sys_RoleModuleTable nc = new sys_RoleModuleTable();

            if (!Convert.IsDBNull(dr["A_roleid"])) nc.A_RoleID = Convert.ToInt32(dr["A_roleid"]); // 角色ID
            if (!Convert.IsDBNull(dr["A_moduleid"])) nc.A_moduleid = Convert.ToInt32(dr["A_moduleid"]); // 模块ID  
            return nc;
        }
        #endregion

        #region "Sys_RoleUsertType  - Data "

        /// <summary>
        /// 新增/删除/修改 Sys_RoleUsertType (Sys_RoleUsertType)
        /// </summary>
        /// <param name="fam">Sys_RoleUsertTypeTable实体类(Sys_RoleUsertType)</param>
        /// <returns>-1:存储过程执行失败,-2:存在相同的主键,Insert:返回插入自动ID,Update:返回更新记录数,Delete:返回删除记录数</returns>
        public Int32 Sys_RoleUsertTypeInsertUpdateDelete(Sys_RoleUsertTypeTable fam)
        {
            Int32 rInt = -1;
            using (SqlConnection Conn = new SqlConnection(ConString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.Sys_RoleUsertType_InsertUpdateDelete", Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //设置参数
                    cmd.Parameters.Add("@DataTable_Action_", SqlDbType.VarChar).Value = fam.DataTable_Action_.ToString(); //操作方法 Insert:增加 Update:修改 Delete:删除

                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = fam.id;  //id
                    cmd.Parameters.Add("@A_roleid", SqlDbType.Int).Value = fam.A_roleid;  //A_roleid
                    cmd.Parameters.Add("@A_UserTypeID", SqlDbType.Int).Value = fam.A_UserTypeID;  //A_UserTypeID
                    cmd.Parameters.Add("@A_UserTypes", SqlDbType.VarChar).Value = fam.A_UserTypes;  //A_UserTypeID
                    Conn.Open();
                    rInt = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    Conn.Dispose();
                    Conn.Close();
                }
                catch (Exception ex)
                {

                    Conn.Dispose();
                    Conn.Close();
                    new DAL.SqlDbHelper_1().writeErrorInfo(ex.Message);
                }
            }
            return rInt;
        }

        /// <summary>
        /// 返回Sys_RoleUsertTypeTable实体类的List对象 (Sys_RoleUsertType)
        /// </summary>
        /// <param name="qp">查询类(非安全函数,传入参数请进行Sql字符串过滤)</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>Sys_RoleUsertTypeTable实体类的List对象(Sys_RoleUsertType)</returns>
        public DataSet Sys_RoleUsertTypeList(QueryParam qp, out int RecordCount)
        {
            // PopulateDelegate mypd = new PopulateDelegate(PopulateSys_RoleUsertTypeTable);
            return _sqldb.GetDataset(qp, out RecordCount);
        }
        /// <summary>
        /// 将记录集转为Sys_RoleUsertTypeTable实体类 (Sys_RoleUsertType)
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <param name="Fileds">字段名列表</param>
        /// <returns>Sys_RoleUsertTypeTable</returns>
        protected Sys_RoleUsertTypeTable PopulateSys_RoleUsertTypeTable(IDataReader dr)
        {
            Sys_RoleUsertTypeTable nc = new Sys_RoleUsertTypeTable();

            if (!Convert.IsDBNull(dr["id"])) nc.id = Convert.ToInt32(dr["id"]); // id
            if (!Convert.IsDBNull(dr["A_roleid"])) nc.A_roleid = Convert.ToInt32(dr["A_roleid"]); // A_roleid
            if (!Convert.IsDBNull(dr["A_UserTypeID"])) nc.A_UserTypeID = Convert.ToInt32(dr["A_UserTypeID"]); // A_UserTypeID
            // if (Convert.IsDBNull(dr["A_UserTypes"])) nc.A_UserTypes = Convert.ToString(dr["A_UserTypes"]); // A_UserTypes
            return nc;
        }
        #endregion

        #region "Sys_applications - Data"

        /// <summary>
        /// 新增/删除/修改 Sys_applications (Sys_applications)
        /// </summary>
        /// <param name="fam">Sys_applicationsTable实体类(Sys_applications)</param>
        /// <returns>-1:存储过程执行失败,-2:存在相同的主键,Insert:返回插入自动ID,Update:返回更新记录数,Delete:返回删除记录数</returns>
        public int Sys_applicationsInsertUpdateDelete(Sys_applicationsTable fam)
        {
            int rInt = -1;
            using (SqlConnection Conn = new SqlConnection(ConString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.Sys_applications_InsertUpdateDelete", Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //设置参数
                    cmd.Parameters.Add("@DataTable_Action_", SqlDbType.VarChar).Value = fam.DataTable_Action_.ToString(); //操作方法 Insert:增加 Update:修改 Delete:删除

                    cmd.Parameters.Add("@Applicationid", SqlDbType.Int).Value = fam.Applicationid;  //应用ID标识
                    cmd.Parameters.Add("@A_moduleid", SqlDbType.Int).Value = fam.A_moduleid;  //所属模块ID
                    cmd.Parameters.Add("@A_appname", SqlDbType.VarChar).Value = fam.A_appname;  //应用名称
                    cmd.Parameters.Add("@A_url", SqlDbType.VarChar).Value = fam.A_url;  //该应用的URL地址
                    cmd.Parameters.Add("@A_order", SqlDbType.Int).Value = fam.A_order;  //应用排序
                    cmd.Parameters.Add("@A_picurl", SqlDbType.VarChar).Value = fam.A_picurl;  //应用的图标地址
                    cmd.Parameters.Add("@A_pagecode", SqlDbType.VarChar).Value = fam.A_pagecode;  //该应用的页面权限代码
                    cmd.Parameters.Add("@A_tag", SqlDbType.VarChar).Value = fam.A_tag;  //应用开启标识,1开启,0关闭
                    cmd.Parameters.Add("@OrderType", SqlDbType.VarChar).Value = fam.OrderType;  //排序类别 1向上,2向下
                    Conn.Open();
                    rInt = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    Conn.Dispose();
                    Conn.Close();
                }
                catch (Exception ex)
                {

                    Conn.Dispose();
                    Conn.Close();
                    new DAL.SqlDbHelper_1().writeErrorInfo(ex.Message);
                }
            }
            return rInt;
        }


        /// <summary>
        ///  返回 ApplicationsList  实体类的ArrayList对象
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="RecordCount"></param>
        /// <returns></returns>
        public DataSet ApplicationsList(QueryParam qp, out int RecordCount)
        {
            //PopulateDelegate mypd = new PopulateDelegate(PopulateSys_applications);

            return _sqldb.GetDataset(qp, out RecordCount);
        }

        /// <summary>
        /// 将记录集转为sys_applicationsTable实体类 (Sys_applications)
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="Fileds"></param>
        /// <returns></returns>
        protected Sys_applicationsTable PopulateSys_applications(IDataReader dr)
        {
            Sys_applicationsTable nc = new Sys_applicationsTable();

            if (!Convert.IsDBNull(dr["Applicationid"])) nc.Applicationid = Convert.ToInt32(dr["Applicationid"]); // 应用ID标识
            if (!Convert.IsDBNull(dr["A_moduleid"])) nc.A_moduleid = Convert.ToInt32(dr["A_moduleid"]); // 所属模块ID
            if (!Convert.IsDBNull(dr["A_appname"])) nc.A_appname = Convert.ToString(dr["A_appname"]).Trim(); // 应用名称
            if (!Convert.IsDBNull(dr["A_url"])) nc.A_url = Convert.ToString(dr["A_url"]).Trim(); // 该应用的URL地址
            if (!Convert.IsDBNull(dr["A_order"])) nc.A_order = Convert.ToInt32(dr["A_order"]); // 应用排序
            if (!Convert.IsDBNull(dr["A_picurl"])) nc.A_picurl = Convert.ToString(dr["A_picurl"]).Trim(); // 应用的图标地址
            if (!Convert.IsDBNull(dr["A_pagecode"])) nc.A_pagecode = Convert.ToString(dr["A_pagecode"]).Trim(); // 该应用的页面权限代码
            if (!Convert.IsDBNull(dr["A_tag"])) nc.A_tag = Convert.ToString(dr["A_tag"]).Trim(); // 应用开启标识,1开启,0关闭
            return nc;
        }


        #endregion

        #region "Sys_Permission (Sys_Permission) - Data"

        /// <summary>
        /// 新增/删除/修改 Sys_Permission (Sys_Permission)
        /// </summary>
        /// <param name="fam">Sys_PermissionTable实体类(Sys_Permission)</param>
        /// <returns>-1:存储过程执行失败,-2:存在相同的主键,Insert:返回插入自动ID,Update:返回更新记录数,Delete:返回删除记录数</returns>
        public Int32 Sys_PermissionInsertUpdateDelete(Sys_PermissionTable fam)
        {
            Int32 rInt = -1;
            using (SqlConnection Conn = new SqlConnection(ConString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.Sys_Permission_InsertUpdateDelete", Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //设置参数
                    cmd.Parameters.Add("@DataTable_Action_", SqlDbType.VarChar).Value = fam.DataTable_Action_.ToString(); //操作方法 Insert:增加 Update:修改 Delete:删除

                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = fam.id;  //自动增长ID
                    cmd.Parameters.Add("@PageCode", SqlDbType.VarChar).Value = fam.PageCode;  //页面代码
                    cmd.Parameters.Add("@PermissionName", SqlDbType.VarChar).Value = fam.PermissionName;  //权限名称
                    cmd.Parameters.Add("@PermissionValue", SqlDbType.Int).Value = fam.PermissionValue;  //权限值
                    Conn.Open();
                    rInt = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    Conn.Dispose();
                    Conn.Close();
                }
                catch (Exception ex)
                {

                    Conn.Dispose();
                    Conn.Close();
                    new DAL.SqlDbHelper_1().writeErrorInfo(ex.Message);
                }
            }
            return rInt;
        }

        /// <summary>
        /// 返回Sys_PermissionTable实体类的List对象 (Sys_Permission)
        /// </summary>
        /// <param name="qp">查询类(非安全函数,传入参数请进行Sql字符串过滤)</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>Sys_PermissionTable实体类的List对象(Sys_Permission)</returns>
        public DataSet Sys_PermissionList(QueryParam qp, out int RecordCount)
        {
            // PopulateDelegate  mypd = new PopulateDelegate(PopulateSys_PermissionTable);
            return _sqldb.GetDataset(qp, out RecordCount);
        }

        /// <summary>
        /// 将记录集转为Sys_PermissionTable实体类 (Sys_Permission)
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <param name="Fileds">字段名列表</param>
        /// <returns>Sys_PermissionTable</returns>
        protected Sys_PermissionTable PopulateSys_PermissionTable(IDataReader dr)
        {
            Sys_PermissionTable nc = new Sys_PermissionTable();

            if (!Convert.IsDBNull(dr["id"])) nc.id = Convert.ToInt32(dr["id"]); // 自动增长ID
            if (!Convert.IsDBNull(dr["PageCode"])) nc.PageCode = Convert.ToString(dr["PageCode"]).Trim(); // 页面代码
            if (!Convert.IsDBNull(dr["PermissionName"])) nc.PermissionName = Convert.ToString(dr["PermissionName"]).Trim(); // 权限名称
            if (!Convert.IsDBNull(dr["PermissionValue"])) nc.PermissionValue = Convert.ToInt32(dr["PermissionValue"]); // 权限值
            return nc;
        }
        #endregion

        #region "Sys_roles - Data"

        /// <summary>
        /// 新增/删除/修改 Sys_roles 
        /// </summary>
        /// <param name="fam">Sys_rolesTable实体类(Sys_roles)</param>
        /// <returns>-1:存储过程执行失败,-2:存在相同的主键,Insert:返回插入自动ID,Update:返回更新记录数,Delete:返回删除记录数</returns>
        public int Sys_rolesInsertUpdateDelete(Sys_rolesTable fam)
        {
            int rInt = -1;
            using (SqlConnection Conn = new SqlConnection(ConString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.Sys_roles_InsertUpdateDelete", Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //设置参数
                    cmd.Parameters.Add("@DataTable_Action_", SqlDbType.VarChar).Value = fam.DataTable_Action_.ToString(); //操作方法 Insert:增加 Update:修改 Delete:删除

                    cmd.Parameters.Add("@Roleid", SqlDbType.Int).Value = fam.Roleid;  //角色ID标识
                    cmd.Parameters.Add("@R_name", SqlDbType.VarChar).Value = fam.R_name;  //角色名称
                    cmd.Parameters.Add("@R_descript", SqlDbType.VarChar).Value = fam.R_descript;  //角色描述
                    Conn.Open();
                    rInt = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    Conn.Dispose();
                    Conn.Close();
                }
                catch (Exception ex)
                {

                    Conn.Dispose();
                    Conn.Close();
                    new DAL.SqlDbHelper_1().writeErrorInfo(ex.Message);
                }
            }
            return rInt;
        }

        /// <summary>
        /// 返回Sys_rolesTable实体类的List对象 (Sys_roles)
        /// </summary>
        /// <param name="qp">查询类(非安全函数,传入参数请进行Sql字符串过滤)</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>Sys_rolesTable实体类的List对象(Sys_roles)</returns>
        public DataSet Sys_rolesList(QueryParam qp, out int RecordCount)
        {
            // PopulateDelegate mypd = new PopulateDelegate(PopulateSys_rolesEntity);
            return _sqldb.GetDataset(qp, out RecordCount);
        }

        /// <summary>
        /// 将记录集转为Sys_rolesTable实体类 (Sys_roles)
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <param name="Fileds">字段名列表</param>
        /// <returns>Sys_rolesEntity</returns>
        protected Sys_rolesTable PopulateSys_rolesEntity(IDataReader dr)
        {
            Sys_rolesTable nc = new Sys_rolesTable();

            if (!Convert.IsDBNull(dr["Roleid"])) nc.Roleid = Convert.ToInt32(dr["Roleid"]); // 角色ID标识
            if (!Convert.IsDBNull(dr["R_name"])) nc.R_name = Convert.ToString(dr["R_name"]).Trim(); // 角色名称
            if (!Convert.IsDBNull(dr["R_descript"])) nc.R_descript = Convert.ToString(dr["R_descript"]).Trim(); // 角色描述
            return nc;
        }

        #endregion

        #region "sys_RolePermission - Data"

        /// <summary>
        /// 新增/删除/修改 sys_RolePermission
        /// </summary>
        /// <param name="fam">sys_RolePermissionTable实体类</param>
        /// <returns>返回0操正常</returns>
        public int sys_RolePermissionInsertUpdate(sys_RolePermissionTable fam)
        {
            int rInt = 0;
            using (SqlConnection Conn = new SqlConnection(ConString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Sys_rolepermission_InsertUpdateDelete", Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //设置参数
                    cmd.Parameters.Add("@DataTable_Action_", SqlDbType.NVarChar).Value = fam.DB_Option_Action_; //操作方法 Insert:增加 Update:修改 Delete:删除 Disp:显示单笔记录
                    cmd.Parameters.Add("@Pid", SqlDbType.Int).Value = fam.Pid;  //角色应用权限自动ID
                    cmd.Parameters.Add("@P_RoleID", SqlDbType.Int).Value = fam.P_RoleID;  //角色ID与sys_Roles表中RoleID相
                    cmd.Parameters.Add("@P_moduleid", SqlDbType.Int).Value = fam.P_moduleid;  //角色所属应用ID与sys_module
                    cmd.Parameters.Add("@P_PageCode", SqlDbType.VarChar).Value = fam.P_PageCode;  //角色应用中页面权限代码
                    cmd.Parameters.Add("@P_Value", SqlDbType.Int).Value = fam.P_Value;  //权限值
                    Conn.Open();
                    rInt = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    Conn.Dispose();
                    Conn.Close();
                }
                catch (Exception ex)
                {

                    Conn.Dispose();
                    Conn.Close();
                    new DAL.SqlDbHelper_1().writeErrorInfo(ex.Message);
                }
            }
            return rInt;
        }
        /// <summary>
        ///  返回 sys_RolePermissionList  实体类的ArrayList对象
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="RecordCount"></param>
        /// <returns></returns>
        public DataSet sys_RolePermissionList(QueryParam qp, out int RecordCount)
        {
            //  PopulateDelegate mypd = new PopulateDelegate(Populatesys_RolePermission);

            return _sqldb.GetDataset(qp, out RecordCount);
        }

        /// <summary>
        /// 将记录集转为sys_RolePermissionTable实体类
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <returns>sys_RolePermissionTable</returns>
        protected object Populatesys_RolePermission(IDataReader dr)
        {
            sys_RolePermissionTable nc = new sys_RolePermissionTable();

            if (!Convert.IsDBNull(dr["pid"])) nc.Pid = Convert.ToInt32(dr["pid"]); // 角色应用权限自动ID
            if (!Convert.IsDBNull(dr["P_RoleID"])) nc.P_RoleID = Convert.ToInt32(dr["P_RoleID"]); // 角色ID与sys_Roles表中RoleID相
            if (!Convert.IsDBNull(dr["P_moduleid"])) nc.P_moduleid = Convert.ToInt32(dr["P_moduleid"]); // 角色所属应用ID与sys_Module
            if (!Convert.IsDBNull(dr["P_PageCode"])) nc.P_PageCode = Convert.ToString(dr["P_PageCode"]).Trim(); // 角色应用中页面权限代码
            if (!Convert.IsDBNull(dr["P_Value"])) nc.P_Value = Convert.ToInt32(dr["P_Value"]); // 权限值
            return nc;
        }
        #endregion

        #region "sys_Event - Data"
        /// <summary>
        /// 新增/删除/修改 sys_Event
        /// </summary>
        /// <param name="fam">sys_EventTable实体类</param>
        /// <returns>返回0操正常</returns>
        public int sys_EventInsertUpdate(sys_EventTable fam)
        {
            int rInt = 0;
            using (SqlConnection Conn = new SqlConnection(ConString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sys_EventInsertUpdateDelete", Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //设置参数
                    cmd.Parameters.Add("@DB_Option_Action_", SqlDbType.NVarChar).Value = fam.DB_Option_Action_; //操作方法 Insert:增加 Update:修改 Delete:删除 Disp:显示单笔记录
                    cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = fam.EventID;  //事件ID号
                    cmd.Parameters.Add("@E_U_LoginName", SqlDbType.NVarChar).Value = fam.E_U_LoginName;  //用户名
                    cmd.Parameters.Add("@E_UserID", SqlDbType.Int).Value = fam.E_UserID;  //操作时用户ID与sys_Users中UserID
                    cmd.Parameters.Add("@E_DateTime", SqlDbType.DateTime).Value = fam.E_DateTime;  //事件发生的日期及时间
                    cmd.Parameters.Add("@E_ModuleID", SqlDbType.Int).Value = fam.E_ModuleID;  //所属模块程序ID与sys_Applicatio
                    cmd.Parameters.Add("@E_M_ModName", SqlDbType.NVarChar).Value = fam.E_M_ModName;  //所属模块名称
                    cmd.Parameters.Add("@E_A_AppName", SqlDbType.NVarChar).Value = fam.E_A_AppName;  //PageCode应用名称与sys_Module相同	
                    cmd.Parameters.Add("@E_A_PageCode", SqlDbType.VarChar).Value = fam.E_A_PageCode;  //发生事件时应用名称
                    cmd.Parameters.Add("@E_From", SqlDbType.NVarChar).Value = fam.E_From;  //来源
                    cmd.Parameters.Add("@E_Type", SqlDbType.Int).Value = fam.E_Type;  //日记类型,1:操作日记2:安全日志3	
                    cmd.Parameters.Add("@E_IP", SqlDbType.VarChar).Value = fam.E_IP;  //客户端IP地址
                    cmd.Parameters.Add("@E_Record", SqlDbType.NVarChar).Value = fam.E_Record;  //详细描述
                    Conn.Open();
                    rInt = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    Conn.Dispose();
                    Conn.Close();
                }
                catch (Exception ex)
                {

                    Conn.Dispose();
                    Conn.Close();
                    new DAL.SqlDbHelper_1().writeErrorInfo(ex.Message);
                }
            }
            return rInt;
        }

        /// <summary>
        ///  返回 sys_EventList  实体类的ArrayList对象
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="RecordCount"></param>
        /// <returns></returns>
        public DataSet sys_EventList(QueryParam qp, out int RecordCount)
        {
            //  PopulateDelegate mypd = new PopulateDelegate(Populatesys_sys_Event);

            return _sqldb.GetDataset(qp, out RecordCount);
        }

        /// <summary>
        /// 将记录集转为sys_EventTable实体类
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <returns>sys_EventTable</returns>
        protected object Populatesys_sys_Event(IDataReader dr)
        {
            sys_EventTable nc = new sys_EventTable();

            if (!Convert.IsDBNull(dr["EventID"])) nc.EventID = Convert.ToInt32(dr["EventID"]); //  事件ID号
            if (!Convert.IsDBNull(dr["E_U_LoginName"])) nc.E_U_LoginName = Convert.ToString(dr["E_U_LoginName"]).Trim(); //用户名
            if (!Convert.IsDBNull(dr["E_UserID"])) nc.E_UserID = Convert.ToInt32(dr["E_UserID"]); // 操作时用户ID与sys_Users中UserID
            if (!Convert.IsDBNull(dr["E_DateTime"])) nc.E_DateTime = Convert.ToDateTime(dr["E_DateTime"]); // 角色应用中页面权限代码
            if (!Convert.IsDBNull(dr["E_From"])) nc.E_From = Convert.ToString(dr["E_From"]).Trim(); // 来源
            if (!Convert.IsDBNull(dr["E_Type"])) nc.E_Type = Convert.ToInt32(dr["E_Type"]); //  日记类型,1:操作日记2:安全日志3
            if (!Convert.IsDBNull(dr["E_IP"])) nc.E_IP = Convert.ToString(dr["E_IP"]).Trim(); //客户端IP地址
            if (!Convert.IsDBNull(dr["E_Record"])) nc.E_Record = Convert.ToString(dr["E_Record"]).Trim(); // 详细描述

            return nc;
        }

        #endregion

        #region "sys_Online - Data"
        /// <summary>
        /// 新增/删除/修改 sys_Online
        /// </summary>
        /// <param name="fam">sys_OnlineTable实体类</param>
        /// <returns>返回0操正常</returns>
        public int sys_OnlineInsertUpdate(sys_OnlineTable fam)
        {
            int rInt = 0;
            using (SqlConnection Conn = new SqlConnection(ConString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sys_OnlineInsertUpdateDelete", Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //设置参数
                    cmd.Parameters.Add("@DB_Option_Action_", SqlDbType.NVarChar).Value = fam.DB_Option_Action_; //操作方法 Insert:增加 Update:修改 Delete:删除 Disp:显示单笔记录
                    cmd.Parameters.Add("@OnlineID", SqlDbType.Int).Value = fam.OnlineID;  //自动ID	
                    cmd.Parameters.Add("@O_SessionID", SqlDbType.VarChar).Value = fam.O_SessionID;  //用户SessionID
                    cmd.Parameters.Add("@O_UserName", SqlDbType.NVarChar).Value = fam.O_UserName;  //用户名	
                    cmd.Parameters.Add("@O_Ip", SqlDbType.VarChar).Value = fam.O_Ip;  //用户IP地址
                    cmd.Parameters.Add("@O_LoginTime", SqlDbType.DateTime).Value = fam.O_LoginTime;  //登陆时间
                    cmd.Parameters.Add("@O_LastTime", SqlDbType.DateTime).Value = fam.O_LastTime;  //最后访问时间
                    cmd.Parameters.Add("@O_LastUrl", SqlDbType.NVarChar).Value = fam.O_LastUrl;  //最后请求网站
                    Conn.Open();
                    rInt = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    Conn.Dispose();
                    Conn.Close();
                }
                catch (Exception ex)
                {

                    Conn.Dispose();
                    Conn.Close();
                    new DAL.SqlDbHelper_1().writeErrorInfo(ex.Message);
                }
            }
            return rInt;
        }


        /// <summary>
        ///  返回 sys_OnlineTableList  实体类的ArrayList对象
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="RecordCount"></param>
        /// <returns></returns>
        public DataSet sys_OnlineList(QueryParam qp, out int RecordCount)
        {
            // PopulateDelegate mypd = new PopulateDelegate(Populatesys_Online);

            return _sqldb.GetDataset(qp, out RecordCount);
        }

        /// <summary>
        /// 将记录集转为sys_OnlineTable实体类
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <returns>sys_OnlineTable</returns>
        protected object Populatesys_Online(IDataReader dr)
        {
            sys_OnlineTable nc = new sys_OnlineTable();

            if (!Convert.IsDBNull(dr["OnlineID"])) nc.OnlineID = Convert.ToInt32(dr["OnlineID"]); // 自动ID
            if (!Convert.IsDBNull(dr["O_SessionID"])) nc.O_SessionID = Convert.ToString(dr["O_SessionID"]).Trim(); // 用户SessionID
            if (!Convert.IsDBNull(dr["O_UserName"])) nc.O_UserName = Convert.ToString(dr["O_UserName"]).Trim(); // 用户名
            if (!Convert.IsDBNull(dr["O_Ip"])) nc.O_Ip = Convert.ToString(dr["O_Ip"]).Trim(); // 用户IP地址
            if (!Convert.IsDBNull(dr["O_LoginTime"])) nc.O_LoginTime = Convert.ToDateTime(dr["O_LoginTime"]); // 登陆时间
            if (!Convert.IsDBNull(dr["O_LastTime"])) nc.O_LastTime = Convert.ToDateTime(dr["O_LastTime"]); // 最后访问时间
            if (!Convert.IsDBNull(dr["O_LastUrl"])) nc.O_LastUrl = Convert.ToString(dr["O_LastUrl"]).Trim(); // 最后请求网站
            return nc;
        }

        #endregion

        #region "Sys_Scope -  Data "

        /// <summary>
        /// 新增/删除/修改 Sys_Scope (Sys_Scope)
        /// </summary>
        /// <param name="fam">Sys_ScopeTable实体类(Sys_Scope)</param>
        /// <returns>-1:存储过程执行失败,-2:存在相同的主键,Insert:返回插入自动ID,Update:返回更新记录数,Delete:返回删除记录数</returns>
        public int Sys_ScopeInsertUpdateDelete(Sys_ScopeTable fam)
        {
            int rInt = -1;
            using (SqlConnection Conn = new SqlConnection(ConString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.Sys_Scope_InsertUpdateDelete", Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //设置参数
                    cmd.Parameters.Add("@DataTable_Action_", SqlDbType.VarChar).Value = fam.DataTable_Action_.ToString(); //操作方法 Insert:增加 Update:修改 Delete:删除

                    cmd.Parameters.Add("@ScopeID", SqlDbType.Int).Value = fam.ScopeID;  //ID 递增
                    cmd.Parameters.Add("@S_Name", SqlDbType.VarChar).Value = fam.S_Name;  //名称
                    cmd.Parameters.Add("@S_Code", SqlDbType.VarChar).Value = fam.S_Code;  //代码
                    cmd.Parameters.Add("@S_ParentID", SqlDbType.Int).Value = fam.S_ParentID;  //父级ID
                    cmd.Parameters.Add("@S_Depth", SqlDbType.Int).Value = fam.S_Depth;  //深度
                    Conn.Open();
                    rInt = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    Conn.Dispose();
                    Conn.Close();
                }
                catch (Exception ex)
                {

                    Conn.Dispose();
                    Conn.Close();
                    new DAL.SqlDbHelper_1().writeErrorInfo(ex.Message);
                }
            }
            return rInt;
        }

        /// <summary>
        /// 返回Sys_ScopeTable实体类的ArrayList对象 (Sys_Scope)
        /// </summary>
        /// <param name="qp">查询类(非安全函数,传入参数请进行Sql字符串过滤)</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>Sys_ScopeTable实体类的ArrayList对象(Sys_Scope)</returns>
        public DataSet Sys_ScopeList(QueryParam qp, out int RecordCount)
        {
            // PopulateDelegate mypd = new PopulateDelegate(PopulateSys_ScopeEntity);
            return _sqldb.GetDataset(qp, out RecordCount);
        }

        /// <summary>
        /// 将记录集转为Sys_ScopeTable实体类 (Sys_Scope)
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <param name="Fileds">字段名列表</param>
        /// <returns>Sys_ScopeTable</returns>
        protected Sys_ScopeTable PopulateSys_ScopeEntity(IDataReader dr)
        {
            Sys_ScopeTable nc = new Sys_ScopeTable();

            if (!Convert.IsDBNull(dr["ScopeID"])) nc.ScopeID = Convert.ToInt32(dr["ScopeID"]); // ID 递增
            if (!Convert.IsDBNull(dr["S_Name"])) nc.S_Name = Convert.ToString(dr["S_Name"]).Trim(); // 名称          
            if (!Convert.IsDBNull(dr["S_Code"])) nc.S_Code = Convert.ToString(dr["S_Code"]).Trim(); // 代码
            if (!Convert.IsDBNull(dr["S_ParentID"])) nc.S_ParentID = Convert.ToInt32(dr["S_ParentID"]); // 父级ID
            if (!Convert.IsDBNull(dr["S_Depth"])) nc.S_Depth = Convert.ToInt32(dr["S_Depth"]); // 深度
            return nc;
        }
        #endregion

        #region "Sys_Menu (Sys_Menu) -  Data "

        /// <summary>
        /// 新增/删除/修改 Sys_Menu (Sys_Menu)
        /// </summary>
        /// <param name="fam">Sys_MenuTable实体类(Sys_Menu)</param>
        /// <returns>-1:存储过程执行失败,-2:存在相同的主键,Insert:返回插入自动ID,Update:返回更新记录数,Delete:返回删除记录数</returns>
        public Int32 Sys_MenuInsertUpdateDelete(Sys_MenuTable fam)
        {
            Int32 rInt = -1;
            using (SqlConnection Conn = new SqlConnection(ConString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.Sys_Menu_InsertUpdateDelete", Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //设置参数
                    cmd.Parameters.Add("@DataTable_Action_", SqlDbType.VarChar).Value = fam.DataTable_Action_.ToString(); //操作方法 Insert:增加 Update:修改 Delete:删除

                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = fam.id;  //自动增长ID
                    cmd.Parameters.Add("@M_Name", SqlDbType.VarChar).Value = fam.M_Name;  //菜单名称
                    cmd.Parameters.Add("@M_Url", SqlDbType.VarChar).Value = fam.M_Url;  //联接网址
                    cmd.Parameters.Add("@M_Order", SqlDbType.Int).Value = fam.M_Order;  //排序
                    cmd.Parameters.Add("@M_Tag", SqlDbType.Int).Value = fam.M_Tag;  //0关闭 1开通
                    cmd.Parameters.Add("@OrderType", SqlDbType.Int).Value = fam.OrderType;  //1向上,2向下
                    Conn.Open();
                    rInt = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    Conn.Dispose();
                    Conn.Close();
                }
                catch (Exception ex)
                {

                    Conn.Dispose();
                    Conn.Close();
                    new DAL.SqlDbHelper_1().writeErrorInfo(ex.Message);
                }
            }
            return rInt;
        }




        /// <summary>
        /// 返回Sys_MenuTable实体类的List对象 (Sys_Menu)
        /// </summary>
        /// <param name="qp">查询类(非安全函数,传入参数请进行Sql字符串过滤)</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>Sys_MenuTable实体类的List对象(Sys_Menu)</returns>
        public DataSet Sys_MenuList(QueryParam qp, out int RecordCount)
        {
            // PopulateDelegate  mypd = new PopulateDelegate(PopulateSys_MenuTable);
            return _sqldb.GetDataset(qp, out RecordCount);
        }


        public DataSet Sys_MenuList2(QueryParam qp, out int RecordCount)
        {
            return _sqldb.GetDataset(qp, out RecordCount);
        }


        /// <summary>
        /// 将记录集转为Sys_MenuTable实体类 (Sys_Menu)
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <param name="Fileds">字段名列表</param>
        /// <returns>Sys_MenuTable</returns>
        protected Sys_MenuTable PopulateSys_MenuTable(IDataReader dr)
        {
            Sys_MenuTable nc = new Sys_MenuTable();

            if (!Convert.IsDBNull(dr["id"])) nc.id = Convert.ToInt32(dr["id"]); // 自动增长ID
            if (!Convert.IsDBNull(dr["M_Name"])) nc.M_Name = Convert.ToString(dr["M_Name"]).Trim(); // 菜单名称
            if (!Convert.IsDBNull(dr["M_Url"])) nc.M_Url = Convert.ToString(dr["M_Url"]).Trim(); // 联接网址
            if (!Convert.IsDBNull(dr["M_Order"])) nc.M_Order = Convert.ToInt32(dr["M_Order"]); // 排序
            if (!Convert.IsDBNull(dr["M_Tag"])) nc.M_Tag = Convert.ToInt32(dr["M_Tag"]); // 0关闭 1开通
            return nc;
        }
        #endregion

        #region "更新表中字段值"
        /// <summary>
        /// 更新表中字段值
        /// </summary>
        /// <param name="Table">表名</param>
        /// <param name="Table_FiledsValue">需要更新值(不用带Set)</param>
        /// <param name="Wheres">更新条件(不用带Where)</param>
        /// <returns></returns>
        public int Update_Table_Fileds(string Table, string Table_FiledsValue, string Wheres)
        {
            int rInt = 0;
            using (SqlConnection Conn = new SqlConnection(ConString))
            {
                try
                {
                    string strSql = string.Format("Update {0} Set {1}  Where {2}", Table, Table_FiledsValue, Wheres);
                    SqlCommand cmd = new SqlCommand(strSql, Conn);
                    cmd.CommandType = CommandType.Text;
                    Conn.Open();
                    rInt = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    Conn.Dispose();
                    Conn.Close();
                }
                catch (Exception ex)
                {

                    Conn.Dispose();
                    Conn.Close();
                    new DAL.SqlDbHelper_1().writeErrorInfo(ex.Message);
                }
            }
            return rInt;
        }
        #endregion

        #region "获取表中字段值"
        /// <summary>
        /// 获取表中字段值(非安全函数,传入参数请进行Sql字符串过滤)
        /// </summary>
        /// <param name="table_name">表名</param>
        /// <param name="table_fileds">字段</param>
        /// <param name="where_fileds">查询条件字段</param>
        /// <param name="where_value">查询值</param>
        /// <returns>返回字段值</returns>
        public string get_table_fileds(string table_name, string table_fileds, string where_fileds, string where_value)
        {
            string rStr = "";
            using (SqlConnection Conn = new SqlConnection(ConString))
            {
                try
                {
                    string strSql = string.Format("select {0} from {1} where upper({2})='{3}'", table_fileds, table_name, where_fileds, where_value);
                    SqlCommand cmd = new SqlCommand(strSql, Conn);
                    cmd.CommandType = CommandType.Text;
                    Conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        rStr = dr[0].ToString();
                    }
                    dr.Close();
                    dr.Dispose();
                    cmd.Dispose();
                    Conn.Dispose();
                    Conn.Close();
                }
                catch (Exception ex)
                {

                    Conn.Dispose();
                    Conn.Close();
                    new DAL.SqlDbHelper_1().writeErrorInfo(ex.Message);
                }
            }
            return rStr;
        }
        #endregion

        //--------------End-----------------------// 


        #region "PE_NewsList - Data"

        /// <summary>
        /// 新增/删除/修改 PE_NewsList 
        /// </summary>
        /// <param name="fam">PE_NewsListEntity实体类(PE_NewsList)</param>
        /// <returns>-1:存储过程执行失败,-2:存在相同的主键,Insert:返回插入自动ID,Update:返回更新记录数,Delete:返回删除记录数</returns>
        public Int32 PE_NewsListInsertUpdateDelete(PE_NewsListTable fam)
        {
            Int32 rInt = -1;
            using (SqlConnection Conn = new SqlConnection(ConString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.PE_NewsList_InsertUpdateDelete", Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //设置参数
                    cmd.Parameters.Add("@DataTable_Action_", SqlDbType.VarChar).Value = fam.DataTable_Action_.ToString(); //操作方法 Insert:增加 Update:修改 Delete:删除

                    cmd.Parameters.Add("@NewsID", SqlDbType.Int).Value = fam.NewsID;  //ID
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = fam.Title;  //标题
                    cmd.Parameters.Add("@Content", SqlDbType.NText).Value = fam.Content;  //内容
                    cmd.Parameters.Add("@Urls", SqlDbType.NVarChar).Value = fam.Urls;  //链接地址
                    cmd.Parameters.Add("@Editor", SqlDbType.NVarChar).Value = fam.Editor;  //发布人
                    if (fam.PublishTime.HasValue)
                        cmd.Parameters.Add("@PublishTime", SqlDbType.DateTime).Value = fam.PublishTime;  //发布时间
                    else
                        cmd.Parameters.Add("@PublishTime", SqlDbType.DateTime).Value = DBNull.Value;  //发布时间              
                    cmd.Parameters.Add("@Number", SqlDbType.Int).Value = fam.Number;  //访问量
                    cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = fam.CategoryID;  //类型ID
                    cmd.Parameters.Add("@Show", SqlDbType.Int).Value = fam.Show;  //是否显示
                    cmd.Parameters.Add("@MarkTop", SqlDbType.Int).Value = fam.MarkTop;  //是否置顶
                    cmd.Parameters.Add("@MarkPass", SqlDbType.Int).Value = fam.MarkPass;  //是否审核
                    cmd.Parameters.Add("@MarkImp", SqlDbType.Int).Value = fam.MarkImp;  //是否重要
                    cmd.Parameters.Add("@GongGao", SqlDbType.Int).Value = fam.GongGao;  //公告
                    cmd.Parameters.Add("@marktype", SqlDbType.Int).Value = fam.MarkType;  //类型
                    cmd.Parameters.Add("@AreaID", SqlDbType.Int).Value = fam.AreaID;  //类型
                    cmd.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = fam.Remark;  //文章备注
                    cmd.Parameters.Add("@ScopeID", SqlDbType.Int).Value = fam.ScopeID;  //管辖范围ID
                    cmd.Parameters.Add("@N_NewID", SqlDbType.VarChar).Value = fam.N_NewID;  //唯一码

                    Conn.Open();
                    rInt = Convert.ToInt32(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    Conn.Dispose();
                    Conn.Close();
                }
                catch (Exception ex)
                {

                    Conn.Dispose();
                    Conn.Close();
                    new DAL.SqlDbHelper_1().writeErrorInfo(ex.Message);
                }
            }
            return rInt;
        }

        /// <summary>
        /// 返回PE_NewsListTable实体类的List对象 (PE_NewsList)
        /// </summary>
        /// <param name="qp">查询类(非安全函数,传入参数请进行Sql字符串过滤)</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>PE_NewsListTable实体类的List对象(flag)</returns>
        public DataSet PE_NewsList(QueryParam qp, out int RecordCount)
        {
            // PopulateDelegate mypd = new PopulateDelegate(PopulatePE_NewsListEntity);
            return _sqldb.GetDataset(qp, out RecordCount);
        }

        /// <summary>
        /// 返回PE_NewsListTable实体类的List对象 (PE_NewsList)
        /// </summary>
        /// <param name="qp">查询类(非安全函数,传入参数请进行Sql字符串过滤)</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>PE_NewsListTable实体类的List对象(flag)</returns>
        public DataSet PE_NewsList2(QueryParam qp, out int RecordCount)
        {
            // PopulateDelegate mypd = new PopulateDelegate(PopulatePE_NewsListEntity);
            return _sqldb.GetDataset(qp, out RecordCount);
        }

        /// <summary>
        /// 返回PE_NewsListTable实体类的List对象 (PE_NewsList)
        /// </summary>
        /// <param name="qp">查询类(非安全函数,传入参数请进行Sql字符串过滤)</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>PE_NewsListTable实体类的List对象(flag)</returns>
        public DataSet PE_NewsList(QueryParam qp)
        {
            // PopulateDelegate mypd = new PopulateDelegate(PopulateSingle_PE_NewsListEntity);
            int recordCount = 0;
            return _sqldb.GetDataset(qp, out recordCount);
        }

        /// <summary>
        /// 根据newsId获取PE_NewsListTable实体类
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <param name="categoryId">类型ID</param>
        /// <param name="oprateValue">输入字符</param>
        /// <returns></returns>
        public PE_NewsListTable GetPE_NewsListByID(int newsId, int categoryId, string oprateValue)
        {
            DataTable dt = new DataTable();
            PE_NewsListTable lst = new PE_NewsListTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConString))
                {

                    SqlCommand cmd = new SqlCommand("GetFistLastPage", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@NewsID", SqlDbType.Int).Value = newsId;
                    cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = categoryId;
                    cmd.Parameters.Add("@DataTable_Action_", SqlDbType.VarChar, 10).Value = oprateValue;

                    SqlDataAdapter sdt = new SqlDataAdapter(cmd);
                    sdt.Fill(dt);
                    // SqlDataReader dr = cmd.ExecuteReader();

                    //while (dr.Read())
                    //{
                    //    sat.Add(dr);
                    //}
                    // dr.Close();

                    cmd.Dispose();
                    conn.Close();
                    conn.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("系统正在维护,很抱歉。" + ex);
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                lst.NewsID = Convert.ToInt32(dt.Rows[0]["NewsID"].ToString());
                lst.Title = dt.Rows[0]["Title"].ToString();
                lst.Urls = dt.Rows[0]["Urls"].ToString();
            }
            return lst;
        }

        /// <summary>
        /// 将记录集转为PE_NewsListEntity实体类 (PE_NewsList)
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <param name="Fileds">字段名列表</param>
        /// <returns>PE_NewsListEntity</returns>
        protected PE_NewsListTable PopulatePE_NewsListEntity(IDataReader dr)
        {
            PE_NewsListTable nc = new PE_NewsListTable();

            if (!Convert.IsDBNull(dr["NewsID"])) nc.NewsID = Convert.ToInt32(dr["NewsID"]); // id
            if (!Convert.IsDBNull(dr["CategoryID"])) nc.CategoryID = Convert.ToInt32(dr["CategoryID"]); // CategoryID
            if (!Convert.IsDBNull(dr["Title"])) nc.Title = Convert.ToString(dr["Title"]).Trim(); // Title
            if (!Convert.IsDBNull(dr["Content"])) nc.Content = Convert.ToString(dr["Content"]).Trim(); // Content
            if (!Convert.IsDBNull(dr["Urls"])) nc.Urls = Convert.ToString(dr["Urls"]).Trim(); // Urls
            if (!Convert.IsDBNull(dr["Show"])) nc.Show = Convert.ToInt32(dr["Show"]); // show
            if (!Convert.IsDBNull(dr["MarkPass"])) nc.MarkPass = Convert.ToInt32(dr["MarkPass"]); // MarkPass
            if (!Convert.IsDBNull(dr["MarkTop"])) nc.MarkTop = Convert.ToInt32(dr["MarkTop"]); // marktop
            if (!Convert.IsDBNull(dr["MarkImp"])) nc.MarkImp = Convert.ToInt32(dr["MarkImp"]); // markimp
            //if (!Convert.IsDBNull(dr["PublishTime"])) nc.PublishTime = Convert.ToDateTime(dr["modif_date"]); // modif_date
            if (!Convert.IsDBNull(dr["PublishTime"])) nc.PublishTime = Convert.ToDateTime(dr["PublishTime"]); // c_date
            if (!Convert.IsDBNull(dr["GongGao"])) nc.GongGao = Convert.ToInt16(dr["GongGao"]); // gonggao
            if (!Convert.IsDBNull(dr["Editor"])) nc.Editor = Convert.ToString(dr["Editor"]).Trim(); // editor
            if (!Convert.IsDBNull(dr["Number"])) nc.Number = Convert.ToInt32(dr["Number"]); // number 点击量
            if (!Convert.IsDBNull(dr["AreaID"])) nc.AreaID = Convert.ToInt32(dr["AreaID"]); // 区ID
            if (!Convert.IsDBNull(dr["Remark"])) nc.Remark = Convert.ToString(dr["Remark"]).Trim(); // 文章备注
            if (!Convert.IsDBNull(dr["ScopeID"])) nc.ScopeID = Convert.ToInt32(dr["ScopeID"]); // 管辖范围
            if (!Convert.IsDBNull(dr["N_NewID"])) nc.N_NewID = Convert.ToString(dr["N_NewID"]); // 

            return nc;
        }

        /// <summary>
        /// 将记录集转为PE_NewsListEntity实体类 (PE_NewsList)
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <param name="Fileds">字段名列表</param>
        /// <returns>PE_NewsListEntity</returns>
        protected PE_NewsListTable PopulateSingle_PE_NewsListEntity(IDataReader dr)
        {
            PE_NewsListTable nc = new PE_NewsListTable();

            if (!Convert.IsDBNull(dr["NewsID"])) nc.NewsID = Convert.ToInt32(dr["NewsID"]); // id
            if (!Convert.IsDBNull(dr["CategoryID"])) nc.CategoryID = Convert.ToInt32(dr["CategoryID"]); // CategoryID
            if (!Convert.IsDBNull(dr["Title"])) nc.Title = Convert.ToString(dr["Title"]).Trim(); // Title
            if (!Convert.IsDBNull(dr["PublishTime"])) nc.PublishTime = Convert.ToDateTime(dr["PublishTime"]); // c_date
            // if (!Convert.IsDBNull(dr["AreaID"])) nc.AreaID = Convert.ToInt32(dr["AreaID"]); // 区ID
            if (!Convert.IsDBNull(dr["Remark"])) nc.Remark = Convert.ToString(dr["Remark"]); // 文章备注
            if (!Convert.IsDBNull(dr["Urls"])) nc.Urls = Convert.ToString(dr["Urls"]); // 绝对链接地址
            //  if (!Convert.IsDBNull(dr["TitleUrls"])) nc.TitleUrls = Convert.ToString(dr["TitleUrls"]); // 相对链接地址
            if (!Convert.IsDBNull(dr["N_NewID"])) nc.N_NewID = Convert.ToString(dr["N_NewID"]); // 

            return nc;
        }

        /// <summary>
        /// 执行一条查询语句。返回datatable
        /// </summary>
        /// <param name="sql">需要执行的SQL</param>
        /// <param name="error">执行发生异常时返回。</param>
        /// <param name="bReturn">执行是否成功返回的标识（true、执行成功；false、执行失败）。</param>
        public ArrayList ReturnDatSet(PopulateDelegate pd, QueryParam qp, out int RecordCount)
        {
            RecordCount = 1;
            ArrayList arrContain = new ArrayList();
            using (SqlConnection con = new SqlConnection(ConString))
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        int timeout = con.ConnectionTimeout;
                        con.Open();
                    }
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select top ");
                    sb.Append(qp.PageSize);
                    sb.Append(qp.ReturnFields);
                    sb.Append(" from ");
                    sb.Append(qp.TableName);
                    sb.Append(" where ");
                    sb.Append(qp.Where);
                    sb.Append(qp.Order);
                    //sb.ToString().TrimStart('{');
                    //sb.ToString().TrimEnd('}');
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = sb.ToString();

                    SqlDataReader dr = cmd.ExecuteReader();
                    //Dictionary<string, string> Fileds = new Dictionary<string, string>();
                    //if (dr.FieldCount > 0)
                    //{
                    //    foreach (DataRow var in dr.GetSchemaTable().Select())
                    //    {
                    //        Fileds.Add(var[0].ToString(), var[0].ToString());
                    //    }
                    //}
                    while (dr.Read())
                    {
                        arrContain.Add(pd(dr));
                    }

                    // RecordCount = 1;
                    // 取记录总数 及页数
                    if (dr.NextResult())
                    {
                        if (dr.Read())
                        {
                            RecordCount += 1;
                        }
                    }
                    return arrContain;
                    //SqlDataAdapter dap = new SqlDataAdapter(cmd);
                    //DataTable tab = new DataTable();
                    //dap.Fill(tab);
                    //bReturn = true;
                    //return tab;
                }
                catch (Exception ex)
                {
                    // error = ex.Message;
                    // bReturn = false;
                    // return null;
                }
                finally
                {
                    try
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                    }
                    catch (Exception) { }
                }
            }
            return arrContain;
        }
        #endregion

        #region "PE_NewsCategory - Data"

        /// <summary>
        /// 新增/删除/修改 PE_NewsCategory 
        /// </summary>
        /// <param name="fam">PE_NewsCategoryEntity实体类(PE_NewsCategory)</param>
        /// <returns>-1:存储过程执行失败,-2:存在相同的主键,Insert:返回插入自动ID,Update:返回更新记录数,Delete:返回删除记录数</returns>
        public Int32 PE_NewsCategoryInsertUpdateDelete(PE_NewsCategoryTable fam)
        {
            Int32 rInt = -1;
            using (SqlConnection Conn = new SqlConnection(ConString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.PE_NewsCategory_InsertUpdateDelete", Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //设置参数
                    cmd.Parameters.Add("@DataTable_Action_", SqlDbType.VarChar).Value = fam.DataTable_Action_.ToString(); //操作方法 Insert:增加 Update:修改 Delete:删除

                    cmd.Parameters.Add("@PCID", SqlDbType.Int).Value = fam.PCID;  //ID
                    cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar).Value = fam.CategoryName;  //类型名称
                    cmd.Parameters.Add("@ParentID", SqlDbType.Int).Value = fam.ParentID;  //父节点ID
                    cmd.Parameters.Add("@Level", SqlDbType.Int).Value = fam.Level;  //深度
                    Conn.Open();
                    rInt = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    Conn.Dispose();
                    Conn.Close();
                }
                catch (Exception ex)
                {
                    Conn.Dispose();
                    Conn.Close();
                    new DAL.SqlDbHelper_1().writeErrorInfo(ex.Message);
                }

            }
            return rInt;
        }

        /// <summary>
        /// 返回PE_NewsCategoryTable实体类的List对象 (PE_NewsCategory)
        /// </summary>
        /// <param name="qp">查询类(非安全函数,传入参数请进行Sql字符串过滤)</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>PE_NewsCategoryTable实体类的List对象(PE_NewsCategory)</returns>
        public DataSet PE_NewsCategoryList(QueryParam qp, out int RecordCount)
        {
            // PopulateDelegate mypd = new PopulateDelegate(PopulatePE_NewsCategoryEntity);
            return _sqldb.GetDataset(qp, out RecordCount);
        }

        /// <summary>
        /// 返回PE_NewsCategory表Table
        /// </summary>
        /// <returns></returns>
        public DataTable PE_NewsCategoryTable()
        {
            DataTable dt = new DataTable();
            return dt = DataAdapterSearch("select * from PE_NewsCategory", "PE_NewsCategory").Tables[0];
        }

        /// <summary>
        /// 将记录集转为PE_NewsCategoryEntity实体类 (PE_NewsCategory)
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <param name="Fileds">字段名列表</param>
        /// <returns>PE_NewsCategoryEntity</returns>
        protected PE_NewsCategoryTable PopulatePE_NewsCategoryEntity(IDataReader dr)
        {
            PE_NewsCategoryTable nc = new PE_NewsCategoryTable();

            if (!Convert.IsDBNull(dr["PCID"])) nc.PCID = Convert.ToInt32(dr["PCID"]); // ID
            if (!Convert.IsDBNull(dr["CategoryName"])) nc.CategoryName = Convert.ToString(dr["CategoryName"]).Trim(); // 类型名称
            if (!Convert.IsDBNull(dr["ParentID"])) nc.ParentID = Convert.ToInt32(dr["ParentID"]); // 父节点ID
            if (!Convert.IsDBNull(dr["Level"])) nc.Level = Convert.ToInt32(dr["Level"]); // 深度
            return nc;
        }

        #endregion


    }
}
