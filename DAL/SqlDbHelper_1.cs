using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;
using System.Web;
using Model;
namespace DAL
{
    /// <summary>
    /// 数据库操作类。
    /// </summary>
    public class SqlDbHelper_1
    {
        #region SQL数据连接字符串。
        /// <summary>
        /// SQL数据连接字符串。
        /// </summary>
        private   string _sqlConStr = ConfigurationManager.AppSettings["con"];
        /// <summary>
        /// 用于事务处理的数据库连接类。
        /// </summary>
        private SqlConnection _con = null;
        /// <summary>
        /// 用于事务处理的数据库执行类。
        /// </summary>
        private SqlCommand _cmd = null;
        /// <summary>
        /// 用于数据库操作的事务类。
        /// </summary>
        private SqlTransaction _tran = null;
        #endregion

        /// <summary>
        /// 构造方法。
        /// </summary>
        public SqlDbHelper_1()
        {
        }


        #region "显示数据 分页通用函数"
        /// <summary>
        /// 显示数据 分页通用函数
        /// </summary>
        /// <param name="qp">分页实体类</param>
        /// <returns></returns>
        public DataSet GetDataset(QueryParam qp, out int RecordCount)
        {
            DataSet ds = new DataSet();

            RecordCount = 0;
            using (SqlConnection con = new SqlConnection(_sqlConStr))
            {
                try
                {
                    con.Open();
                    SqlCommand sqlCmd = new SqlCommand("pd_GetDataset", con);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                    sqlCmd.Parameters.Add("@TableList", SqlDbType.VarChar, 400).Value = qp.ReturnFields;
                    sqlCmd.Parameters.Add("@TableName", SqlDbType.VarChar, 30).Value = qp.TableName;
                    sqlCmd.Parameters.Add("@SelectWhere", SqlDbType.VarChar, 700).Value = qp.Where;
                    sqlCmd.Parameters.Add("@SelectOrderId", SqlDbType.VarChar, 20).Value = qp.OrderId;
                    sqlCmd.Parameters.Add("@SelectOrder", SqlDbType.VarChar, 200).Value = qp.Order;
                    sqlCmd.Parameters.Add("@intPageNo", SqlDbType.Int).Value = qp.PageIndex;
                    sqlCmd.Parameters.Add("@intPageSize", SqlDbType.Int).Value = qp.PageSize;
                    sqlCmd.Parameters.Add("@RecordCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                    sqlCmd.Parameters.Add("RowCount", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    da.Fill(ds);

                    RecordCount = (int)sqlCmd.Parameters["@RecordCount"].Value;  //求出总记录数，该值是output出来的值 

                    con.Close();
                    con.Dispose();

                }
                catch (Exception ex)
                {
                    con.Close();
                    con.Dispose();
                    writeErrorInfo(ex.Message);
                }

            }
            return ds;

        }
        #endregion


        #region 分页存储过程。
        /// <summary>
        /// 执行分页存储过程，返回记录总数和当前页的数据。
        /// </summary>
        /// <param name="tabName">数据库表名</param>
        /// <param name="reField">要查询的字段</param>
        /// <param name="orderStr">排序字段</param>
        /// <param name="staFields">统计字段</param>
        /// <param name="where">执行的条件</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="orderType">排序标识（0、升序；1、降序）</param>
        /// <param name="staType">统计类型(0、不统计；1、统计总和;2、计算平均值；)</param>
        /// <param name="staResults">返回统计的结果。</param>
        /// <param name="totalRecord">返回的记录总数</param>
        /// <param name="bFlag">执行成功与失败的标识（true、表示执行成功；false、表示执行失败）</param>
        public DataSet ExecuteProc2(string tabName, string reField, string orderStr, string staFields, string where, int pageSize, int pageIndex, int orderType, int staType, ref decimal staResults, ref int totalRecord, ref bool bFlag)
        {
            DataSet ds = new DataSet();
            int n = 0;//查询后返回的行数保存存储过程中的输出参数
            //创建连接对象 using代码片段好处在于离开作用域后立刻从内存中释放对象
            using (SqlConnection con = new SqlConnection(_sqlConStr))
            {
                try
                {
                    con.Open();//打开数据库连接
                 //   SqlTransaction tran = con.BeginTransaction();
                    SqlCommand cmd = con.CreateCommand();
                 //   cmd.Transaction = tran;
                    cmd.CommandText = "[pd_GetDataset]";
                    //指定当前执行语句的类型是存储过程。
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        //字段名称
                        SqlParameter Fields = new SqlParameter("@TableList", SqlDbType.VarChar, 1024);
                        Fields.Direction = ParameterDirection.Input;
                        Fields.Value = reField;
                        //表名。
                        SqlParameter TabeName = new SqlParameter("@TableName", SqlDbType.VarChar, 500);
                        TabeName.Direction = ParameterDirection.Input;
                        TabeName.Value = tabName;
                        
                        //条件
                        SqlParameter SearchWhere = new SqlParameter("@SelectWhere", SqlDbType.VarChar, 1024);
                        SearchWhere.Direction = ParameterDirection.Input;
                        SearchWhere.Value = where;

                        SqlParameter SelectOrderId = new SqlParameter("@SelectOrderId", SqlDbType.VarChar, 50);
                        SelectOrderId.Direction = ParameterDirection.Input;
                        SelectOrderId.Value = orderStr;

                        SqlParameter SelectOrder = new SqlParameter("@SelectOrder", SqlDbType.VarChar, 50);
                        SelectOrder.Direction = ParameterDirection.Input;
                        if (orderType == 0)
                        {
                            SelectOrder.Value = " order by  " + orderStr + " asc ";
                        }
                        else
                        {
                            SelectOrder.Value = " order by  " + orderStr + " desc ";
                        }
                       

                        //当前第几页
                        SqlParameter page = new SqlParameter("@intPageNo", SqlDbType.Int, 4);
                        page.Direction = ParameterDirection.Input;
                        page.Value = pageIndex;

                        //每页显示记录数
                        SqlParameter pageNumber = new SqlParameter("@intPageSize", SqlDbType.Int, 4);
                        pageNumber.Direction = ParameterDirection.Input;
                        pageNumber.Value = pageSize;
                 
                         
                      
                        //返回记录的总数
                        SqlParameter OutCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
                        OutCount.Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(TabeName);
                        cmd.Parameters.Add(Fields);
                        cmd.Parameters.Add(SearchWhere);
                        cmd.Parameters.Add(SelectOrderId);
                        cmd.Parameters.Add(SelectOrder);
                        cmd.Parameters.Add(pageNumber);
                        cmd.Parameters.Add(page);
                        cmd.Parameters.Add(OutCount);

                        SqlDataAdapter dap = new SqlDataAdapter(cmd);
                       
                        dap.Fill(ds);


                        //接受执行存储过程后的返回值
                        //if (pageIndex == 1)
                        //{
                        int.TryParse(cmd.Parameters["@RecordCount"].Value.ToString(), out totalRecord);
                        // }
                        
                        cmd.Parameters.Clear();  //清空
                       
                        if (cmd != null)
                            cmd.Dispose();
                        con.Close();
                        con.Dispose();
                        bFlag = true;


                    }
                    catch (Exception exe)
                    {
                        con.Close();
                        con.Dispose();
                        bFlag = false;
                        writeErrorInfo(exe.Message);
                    } 
                  
                }
                catch (Exception exe)
                {
                    writeErrorInfo(exe.Message);
                }
                finally
                {
                    try
                    {
                        con.Close(); con.Dispose();
                    }
                    catch (Exception exe) { }
                }
            }
            return ds;
             
        }
        #endregion


        #region 执行一条SQL语句返回受影响的记录数。
        /// <summary>
        /// 执行一条SQL语句返回受影响的记录数。
        /// </summary>
        /// <param name="sqlStr">需要执行的SQL语句。</param>
        /// <param name="lisP">需要的参数。</param>
        /// <param name="error">执行不成功返回的错误内容。</param>
        /// <param name="bReturn">执行返回的标识(true、表示执行成功；false、表示执行失败)</param>
        public int ExecuteNonQuery(string sqlStr, List<SqlParameter> lisP, ref string error, ref bool bReturn)
        {
            int iReturn = 0;
            using (SqlConnection con = new SqlConnection(_sqlConStr))
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = sqlStr;
                    AddParas(cmd, lisP);
                    iReturn = cmd.ExecuteNonQuery();
                    bReturn = true;
                    cmd.Parameters.Clear();  //清空
                    if (cmd != null)
                        cmd.Dispose();
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    bReturn = false;
                    writeErrorInfo(ex.Message);
                }
                finally
                {

                    con.Close();
                    con.Dispose();

                }
                return iReturn;
            }
        }
        /// <summary>
        /// 执行一条SQL语句返回受影响的记录数。
        /// </summary>
        /// <param name="sqlStr">需要执行的SQL语句。</param>
        /// <param name="error">执行不成功返回的错误内容。</param>
        /// <param name="bReturn">执行返回的标识(true、表示执行成功；false、表示执行失败)</param>
        public int ExecuteNonQuery(string sqlStr, ref string error, ref bool bReturn)
        {
            int iReturn = 0;
            using (SqlConnection con = new SqlConnection(_sqlConStr))
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = sqlStr;
                    iReturn = cmd.ExecuteNonQuery();
                    bReturn = true;
                    cmd.Parameters.Clear();  //清空
                    if (cmd != null)
                        cmd.Dispose();
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    bReturn = false;
                    writeErrorInfo(ex.Message);
                }
                finally
                { 
                    con.Close();
                    con.Dispose(); 
                }
                return iReturn;
            }
        }
        #endregion

        #region 将相应的参数添加到对应的参数列表中。
        /// <summary>
        /// 将相应的参数添加到对应的参数列表中。
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="lisP"></param>
        /// <returns></returns>
        private SqlCommand AddParas(SqlCommand cmd, List<SqlParameter> lisP)
        {
            if (lisP == null)
            {
                return cmd;
            }
            foreach (SqlParameter item in lisP)
            {
                cmd.Parameters.Add(item);
            }
            return cmd;
        }
        #endregion

        #region 执行一条查询SQL语句返回第一行第一列。
        /// <summary>
        /// 执行一条查询SQL语句返回第一行第一列。
        /// </summary>
        /// <param name="sqlStr">需要执行的SQL语句。</param>
        /// <param name="lisP">需要的参数。</param>
        /// <param name="error">执行不成功返回的错误内容。</param>
        /// <param name="bReturn">执行返回的标识(true、表示执行成功；false、表示执行失败)</param>
        public object ExecuteScalar(string sqlStr, List<SqlParameter> lisP, ref string error, ref bool bReturn)
        {
            object oReturn = null;
            using (SqlConnection con = new SqlConnection(_sqlConStr))
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = sqlStr;
                    AddParas(cmd, lisP);
                    oReturn = cmd.ExecuteScalar();
                    bReturn = true;
                    cmd.Parameters.Clear();  //清空
                    if (cmd != null)
                        cmd.Dispose();
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    bReturn = false;
                    writeErrorInfo(ex.Message);
                }
                finally
                { 
                    con.Close();
                    con.Dispose(); 
                }
                return oReturn;
            }
        }
        /// <summary>
        /// 执行一条查询SQL语句返回第一行第一列。
        /// </summary>
        /// <param name="sqlStr">需要执行的SQL语句。</param>
        /// <param name="error">执行不成功返回的错误内容。</param>
        /// <param name="bReturn">执行返回的标识(true、表示执行成功；false、表示执行失败)</param>
        public object ExecuteScalar(string sqlStr, ref string error, ref bool bReturn)
        {
            object oReturn = null;
            using (SqlConnection con = new SqlConnection(_sqlConStr))
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = sqlStr;
                    oReturn = cmd.ExecuteScalar();
                    bReturn = true;
                    cmd.Parameters.Clear();  //清空
                    if (cmd != null)
                        cmd.Dispose();
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    bReturn = false;
                    writeErrorInfo(ex.Message);
                }
                finally
                {
                    con.Close();
                    con.Dispose();

                }
                return oReturn;
            }
        }
        #endregion

        #region 根据SQL语句查询返回一个DataTable类型。
        /// <summary>
        /// 根据SQL语句查询返回一个DataTable类型。
        /// </summary>
        /// <param name="sqlStr">需要执行的SQL语句。</param>
        /// <param name="lisP">执行SQL语句需要的参数</param>
        /// <param name="error">执行失败返回的错误消息。</param>
        /// <param name="bReturn">执行标识(true、表示执行成功；false、表示执行失败)</param>
        public DataTable selectTab(string sqlStr, List<SqlParameter> lisP, ref string error, ref bool bReturn)
        {
            DataTable tab = new DataTable();
            using (SqlConnection con = new SqlConnection(_sqlConStr))
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        int timeout = con.ConnectionTimeout;
                        con.Open();
                    }
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = sqlStr;
                    AddParas(cmd, lisP);

                    SqlDataAdapter dap = new SqlDataAdapter(cmd);

                    dap.Fill(tab);
                    cmd.Parameters.Clear();  //清空
                    if (cmd != null)
                        cmd.Dispose();
                    bReturn = true;

                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    bReturn = false;
                    writeErrorInfo(ex.Message);
                    return null;
                }
                finally
                {
                    con.Close();
                    con.Dispose(); 
                }
            }
            return tab;
        }
        #endregion

        #region 执行一条查询语句，返回一个DataTable对象。
        /// <summary>
        /// 执行一条查询语句。
        /// </summary>
        /// <param name="sql">需要执行的SQL</param>
        /// <param name="error">执行发生异常时返回。</param>
        /// <param name="bReturn">执行是否成功返回的标识（true、执行成功；false、执行失败）。</param>
        public DataTable selectTab(string sql, ref string error, ref bool bReturn)
        {
            DataTable tab = new DataTable();
            using (SqlConnection con = new SqlConnection(_sqlConStr))
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        int timeout = con.ConnectionTimeout;
                        con.Open();
                    }
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = sql;

                    SqlDataAdapter dap = new SqlDataAdapter(cmd);

                    dap.Fill(tab);
                    cmd.Parameters.Clear();  //清空
                    if (cmd != null)
                        cmd.Dispose();
                    bReturn = true;

                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    bReturn = false;
                    writeErrorInfo(ex.Message);
                    return null;
                }
                finally
                {
                    con.Close();
                    con.Dispose();

                }
            }
            return tab;
        }
        #endregion

        #region 根据SQL语句查询返回一个DataSet类型。
        /// <summary>
        /// 根据SQL语句查询返回一个DataSet类型。
        /// </summary>
        /// <param name="sqlStr">需要执行的SQL语句。</param>
        /// <param name="lisP">执行SQL语句需要的参数</param>
        /// <param name="error">执行失败返回的错误消息。</param>
        /// <param name="bReturn">执行标识(true、表示执行成功；false、表示执行失败)</param>
        public DataSet selectDataSet(string sqlStr, List<SqlParameter> lisP, ref string error, ref bool bReturn)
        {
            DataSet tab = new DataSet();
            using (SqlConnection con = new SqlConnection(_sqlConStr))
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        int timeout = con.ConnectionTimeout;
                        con.Open();
                    }
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = sqlStr;
                    AddParas(cmd, lisP);

                    SqlDataAdapter dap = new SqlDataAdapter(cmd);
                   
                    dap.Fill(tab);
                    cmd.Parameters.Clear();  //清空
                    if (cmd != null)
                        cmd.Dispose();
                    bReturn = true;
                   
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    bReturn = false;
                    writeErrorInfo(ex.Message);
                }
                finally
                { 
                    con.Close(); 
                    con.Dispose(); 
                } 
            } 
            return tab;
        }
        #endregion

        #region 执行一条查询语句，返回一个DataSet对象。
        /// <summary>
        /// 执行一条查询语句。
        /// </summary>
        /// <param name="sql">需要执行的SQL</param>
        /// <param name="error">执行发生异常时返回。</param>
        /// <param name="bReturn">执行是否成功返回的标识（true、执行成功；false、执行失败）。</param>
        public DataSet selectDataSet(string sql, ref string error, ref bool bReturn)
        {
            DataSet tab = new DataSet();
            using (SqlConnection con = new SqlConnection(_sqlConStr))
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        int timeout = con.ConnectionTimeout;
                        con.Open();
                    }
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = sql;

                    SqlDataAdapter dap = new SqlDataAdapter(cmd);

                    dap.Fill(tab);
                    cmd.Parameters.Clear();  //清空
                    if (cmd != null)
                        cmd.Dispose();
                    bReturn = true;

                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    bReturn = false;
                    writeErrorInfo(ex.Message);
                }
                finally
                { 
                    con.Close();
                    con.Dispose(); 
                }

            }
            return tab;
        }
        #endregion

        #region 分页存储过程。
        /// <summary>
        /// 执行分页存储过程，返回记录总数和当前页的数据。
        /// </summary>
        /// <param name="tabName">数据库表名</param>
        /// <param name="reField">要查询的字段</param>
        /// <param name="orderStr">排序字段</param>
        /// <param name="staFields">统计字段</param>
        /// <param name="where">执行的条件</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="orderType">排序标识（0、升序；1、降序）</param>
        /// <param name="staType">统计类型(0、不统计；1、统计总和;2、计算平均值；)</param>
        /// <param name="staResults">返回统计的结果。</param>
        /// <param name="totalRecord">返回的记录总数</param>
        /// <param name="bFlag">执行成功与失败的标识（true、表示执行成功；false、表示执行失败）</param>
        public DataSet ExecuteProc(string tabName, string reField, string orderStr, string staFields, string where, int pageSize, int pageIndex, int orderType, int staType, ref decimal staResults, ref int totalRecord, ref bool bFlag)
        {
            DataSet ds = new DataSet();
            int n = 0;//查询后返回的行数保存存储过程中的输出参数
            //创建连接对象 using代码片段好处在于离开作用域后立刻从内存中释放对象
            using (SqlConnection con = new SqlConnection(_sqlConStr))
            {
                try
                {
                    con.Open();//打开数据库连接
                    //SqlTransaction tran = con.BeginTransaction();
                    SqlCommand cmd = con.CreateCommand();
                    //cmd.Transaction = tran;
                    cmd.CommandText = "[proc_Page]";
                    //指定当前执行语句的类型是存储过程。
                    cmd.CommandType = CommandType.StoredProcedure;

                    //表名。
                    SqlParameter TabeName = new SqlParameter("@TabeName", SqlDbType.VarChar, 500);
                    TabeName.Direction = ParameterDirection.Input;
                    TabeName.Value = tabName;
                    //字段名称
                    SqlParameter Fields = new SqlParameter("@Fields", SqlDbType.VarChar, 1024);
                    Fields.Direction = ParameterDirection.Input;
                    Fields.Value = reField;
                    //排序字段
                    SqlParameter OrderFields = new SqlParameter("@OrderFields", SqlDbType.VarChar, 1024);
                    OrderFields.Direction = ParameterDirection.Input;
                    OrderFields.Value = orderStr;

                    //统计字段
                    SqlParameter StatisticsFields = new SqlParameter("@StatisticsFields", SqlDbType.VarChar, 200);
                    StatisticsFields.Direction = ParameterDirection.Input;
                    StatisticsFields.Value = staFields;
                    //条件
                    SqlParameter SearchWhere = new SqlParameter("@SearchWhere", SqlDbType.VarChar, 1024);
                    SearchWhere.Direction = ParameterDirection.Input;
                    SearchWhere.Value = where;
                    //每页显示记录数
                    SqlParameter pageNumber = new SqlParameter("@pageNumber", SqlDbType.Int, 4);
                    pageNumber.Direction = ParameterDirection.Input;
                    pageNumber.Value = pageSize;
                    //当前第几页
                    SqlParameter page = new SqlParameter("@page", SqlDbType.Int, 4);
                    page.Direction = ParameterDirection.Input;
                    page.Value = pageIndex;
                    //升序或降序（0、升序；1、降序）
                    SqlParameter SortType = new SqlParameter("@SortType", SqlDbType.Int, 4);
                    SortType.Direction = ParameterDirection.Input;
                    SortType.Value = orderType;
                    //统计类型(0、不统计；1、统计总和;2、计算平均值；)
                    SqlParameter StatisticsType = new SqlParameter("@StatisticsType", SqlDbType.Int, 4);
                    StatisticsType.Direction = ParameterDirection.Input;
                    StatisticsType.Value = staType;
                    //返回统计的结果
                    SqlParameter StaResults = new SqlParameter("@StatisticsResults", SqlDbType.Decimal, 9);
                    StaResults.Direction = ParameterDirection.Output;
                    //返回记录的总数
                    SqlParameter OutCount = new SqlParameter("@OutCount", SqlDbType.Int, 4);
                    OutCount.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(TabeName);
                    cmd.Parameters.Add(Fields);
                    cmd.Parameters.Add(OrderFields);
                    cmd.Parameters.Add(StatisticsFields);
                    cmd.Parameters.Add(SearchWhere);
                    cmd.Parameters.Add(pageNumber);
                    cmd.Parameters.Add(page);
                    cmd.Parameters.Add(SortType);
                    cmd.Parameters.Add(StatisticsType);
                    cmd.Parameters.Add(StaResults);
                    cmd.Parameters.Add(OutCount);

                    SqlDataAdapter dap = new SqlDataAdapter(cmd);

                    dap.Fill(ds);


                    //接受执行存储过程后的返回值
                    //if (pageIndex == 1)
                    //{
                    int.TryParse(cmd.Parameters["@OutCount"].Value.ToString(), out totalRecord);
                    // }
                    if (staType > 0)
                    {
                        decimal.TryParse(cmd.Parameters["@StatisticsResults"].Value.ToString(), out staResults);
                    }
                    cmd.Parameters.Clear();  //清空
                    if (cmd != null)
                        cmd.Dispose();
                    con.Close();
                    con.Dispose();
                    bFlag = true;


                }
                catch (Exception exe)
                {
                    con.Close();
                    con.Dispose();
                    bFlag = false;
                    writeErrorInfo(exe.Message);
                } 
            }
            return ds;
        }
        #endregion

        //#region 验证数据有效性的存储过程。
        ///// <summary>
        ///// 验证数据有效性的存储过程(验证通过返回true；否则返回false)。
        ///// </summary>
        ///// <param name="tabName">表名</param>
        ///// <param name="vField">条件</param>
        ///// <param name="bFlag">执行成功与失败的标识（true、表示执行成功；false、表示执行失败）</param>
        //public int Validate_Proc(string tabName, string strWhere, ref bool bFlag)
        //{
        //    int n = 0;//查询后返回的行数保存存储过程中的输出参数
        //    //创建连接对象 using代码片段好处在于离开作用域后立刻从内存中释放对象
        //    using (SqlConnection con = new SqlConnection(_sqlConStr))
        //    {
        //        con.Open();//打开数据库连接
        //        using (SqlCommand cmd = new SqlCommand("proc_Validate", con))
        //        {
        //            try
        //            {
        //                //什么作用不记得了只记得调用存储过程该语句不能少
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                //表名。
        //                SqlParameter tblName = new SqlParameter("@tblName", SqlDbType.VarChar, 255);
        //                tblName.Direction = ParameterDirection.Input;
        //                tblName.Value = tabName;
        //                //需要验证证的字段。
        //                SqlParameter where = new SqlParameter("@where", SqlDbType.VarChar, 1000);
        //                where.Direction = ParameterDirection.Input;
        //                where.Value = strWhere;
        //                //返回验证的结果（验证通过返回‘0’；否则返回‘非0’）
        //                SqlParameter reFlag = new SqlParameter("@reFlag", SqlDbType.Int, 4);
        //                reFlag.Direction = ParameterDirection.Output;

        //                cmd.Parameters.Add(tblName);
        //                cmd.Parameters.Add(where);
        //                cmd.Parameters.Add(reFlag);

        //                cmd.ExecuteNonQuery();
        //                //接受执行存储过程后的返回值
        //                int iFlag = (int)cmd.Parameters["@reFlag"].Value;
        //                bFlag = true;
        //                return iFlag;
        //            }
        //            catch (Exception ex)
        //            {
        //                bFlag = false;
        //                writeErrorInfo(ex.Message);
        //            }
        //            return -1;
        //        }
        //    }
        //}
        //#endregion

        #region 事务处理
        /// <summary>
        /// 开始事务。
        /// </summary>
        public void BeginTran()
        {
            _con = new SqlConnection(_sqlConStr);
            if (_con.State == ConnectionState.Closed)
            {
                _con.Open();
                _cmd = _con.CreateCommand();
                _cmd.CommandTimeout = 5;
                _tran = _con.BeginTransaction();
                _cmd.Transaction = _tran;
            }
        }
        /// <summary>
        /// 执行一条查询SQL语句返回第一行第一列。
        /// </summary>
        /// <param name="sqlStr">需要执行的SQL语句。</param>
        public object ExecuteScalar_Tran(string sqlStr)
        {
            _cmd.CommandText = sqlStr;
            _cmd.Parameters.Clear();
            return _cmd.ExecuteScalar();
        }
        /// <summary>
        /// 执行一条查询SQL语句返回第一行第一列。
        /// </summary>
        /// <param name="sqlStr">需要执行的SQL语句。</param>
        /// <param name="lisP">执行SQL语句需要的参数</param>
        public object ExecuteScalar_Tran(string sqlStr, List<SqlParameter> lisP)
        {
            _cmd.CommandText = sqlStr;
            _cmd.Parameters.Clear();
            AddParas(_cmd, lisP);
            return _cmd.ExecuteScalar();
        }

        /// <summary>
        /// 执行SQL语句。
        /// </summary>
        /// <param name="sqlStr">需要执行的SQL语句。</param>
        /// <param name="lisP">执行SQL语句需要的参数</param>
        public int execSql_Tran(string sqlStr, List<SqlParameter> lisP)
        {
            _cmd.CommandText = sqlStr;
            _cmd.Parameters.Clear();
            AddParas(_cmd, lisP);
            return _cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 执行SQL语句。
        /// </summary>
        /// <param name="sqlStr">需要执行的SQL语句。</param>
        /// <param name="lisP">执行SQL语句需要的参数</param>
        /// <param name="error">执行失败返回的错误消息。</param>
        /// <param name="bReturn">执行标识(true、表示执行成功；false、表示执行失败)</param>
        public int execSql_Tran(string sqlStr)
        {
            _cmd.CommandText = sqlStr;
            _cmd.Parameters.Clear();
            return _cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 执行SQL语句。
        /// </summary>
        /// <param name="sqlStr">需要执行的SQL语句。</param>
        /// <param name="lisP">执行SQL语句需要的参数</param>
        /// <param name="error">执行失败返回的错误消息。</param>
        /// <param name="bReturn">执行标识(true、表示执行成功；false、表示执行失败)</param>
        public DataTable execReTab_Tran(string sqlStr, List<SqlParameter> lisP)
        {
            _cmd.CommandText = sqlStr;
            _cmd.Parameters.Clear();
            AddParas(_cmd, lisP);

            SqlDataAdapter dap = new SqlDataAdapter(_cmd);
            DataTable tab = new DataTable();
            dap.Fill(tab);
            return tab;
        }

        /// <summary>
        /// 执行SQL语句。
        /// </summary>
        /// <param name="sqlStr">需要执行的SQL语句。</param>
        /// <param name="lisP">执行SQL语句需要的参数</param>
        /// <param name="error">执行失败返回的错误消息。</param>
        /// <param name="bReturn">执行标识(true、表示执行成功；false、表示执行失败)</param>
        public DataTable execReTab_Tran(string sqlStr)
        {
            _cmd.CommandText = sqlStr;
            _cmd.Parameters.Clear();

            SqlDataAdapter dap = new SqlDataAdapter(_cmd);
            DataTable tab = new DataTable();
            dap.Fill(tab);
            return tab;
        }

        /// <summary>
        /// 结束事务。
        /// </summary>
        /// <param name="bFlag">true、表示提交事务；false、表示加滚事务。</param>
        public bool EndTran(bool bFlag)
        {
            try
            {
                if (bFlag)
                {
                    _tran.Commit();
                }
                else
                {
                    _tran.Rollback();
                }
                if (_con.State == ConnectionState.Open)
                {
                    _con.Close();
                    _con.Dispose();
                }

            }
            catch (Exception ex)
            {
                writeErrorInfo(ex.Message);
            }
            return false;
        }
        #endregion

        #region "将一个DataTable转换成实体类List"

        /// <summary>
        /// 将一个DataTable转换成实体类List
        /// </summary>
        /// <param name="dt">要转换的DataTable</param>
        /// <returns>返回一个实体类列表</returns>
        public List<T> DT2EntityList<T>(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            List<T> entityList = new List<T>();
            T entity = default(T);
            foreach (DataRow dr in dt.Rows)
            {
                entity = Activator.CreateInstance<T>();
                PropertyInfo[] pis = entity.GetType().GetProperties();
                foreach (PropertyInfo pi in pis)
                {
                    if (dt.Columns.Contains(pi.Name))
                    {
                        if (!pi.CanWrite)
                        {
                            continue;
                        }
                        if (dr[pi.Name] != DBNull.Value)
                        {
                            pi.SetValue(entity, dr[pi.Name], null);
                        }
                    }
                }
                entityList.Add(entity);
            }
            return entityList;
        }
        #endregion
        
        #region "获取页面url"
        /// <summary>
        /// 获取当前访问页面地址 /Index.aspx 
        /// </summary>
        public string getScriptName()
        {
            return HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"].ToString();            
        }
        #endregion

        #region "用户登录Session"
        /// <summary>
        /// 判断用户Session是否存在，返回Session实体类
        /// </summary> 
        public   Sys_SessionEntity Sessionstu()
        {
            Sys_SessionEntity stu = new Sys_SessionEntity();
            if (HttpContext.Current.Session["stu"] != null)
            {
                stu = HttpContext.Current.Session["stu"] as Sys_SessionEntity;
            }
            else
            {
                
            }
            return stu;
        }
        #endregion



        #region 将错误信息写入数据库。
        /// <summary>
        /// 将错误信息写入数据库。
        /// </summary>
        /// <param name="eMsg">错误详细信息。</param>
        /// <param name="cmd">当前操作的SqlCommand对象。</param>
        public void writeErrorInfo(string eMsg)
        {
            using (SqlConnection con = new SqlConnection(_sqlConStr))
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string sql = "insert into ErrorLogInfo(ePageUrl,eUser,eDateTime,eRemark) values(@ePageUrl,@eUser,@eDateTime,@eRemark)";

                    SqlParameter ePageUrl = new SqlParameter("@ePageUrl", SqlDbType.VarChar);
                    ePageUrl.Value = getScriptName();

                    SqlParameter eUser = new SqlParameter("@eUser", SqlDbType.VarChar);
                    eUser.Value = Sessionstu().UserName;

                    SqlParameter eDateTime = new SqlParameter("@eDateTime", SqlDbType.VarChar);
                    eDateTime.Value = DateTime.Now;

                    SqlParameter eRemark = new SqlParameter("@eRemark", SqlDbType.VarChar);
                    eRemark.Value = eMsg;

                    SqlCommand cmdNew = con.CreateCommand();
                    cmdNew.Parameters.Clear();
                    cmdNew.Parameters.Add(ePageUrl);
                    cmdNew.Parameters.Add(eUser);
                    cmdNew.Parameters.Add(eDateTime);
                    cmdNew.Parameters.Add(eRemark);

                    cmdNew.CommandText = sql;
                    cmdNew.ExecuteNonQuery();
                }
                catch (Exception exe)
                {
                    con.Close();
                    con.Dispose();
                }
                finally
                {
                    con.Close();
                    con.Dispose();

                }
            }
        }
        #endregion

        #region 把datatable转化成json格式字符串

        /// <summary>
        /// 把datatable转化成json格式字符串
        /// </summary>
        /// <param name="dt">需要转化的datatable</param>
        /// <returns>返回json格式字符串</returns>
        public static string CreateJsonParameters(DataTable dt, int rowsTotal)
        {
            StringBuilder JsonString = new StringBuilder();
            //json格式头                   
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("{ \"total\":\"" + rowsTotal + "\",");
                JsonString.Append("\"rows\":[ ");
                //循环一次获取表中列名和对应的数据值
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{ ");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\"");
                        }
                    }

                    /*结束字符串*/
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("} ");

                    }
                    else
                    {
                        JsonString.Append("}, ");
                    }
                } JsonString.Append("]}"); return JsonString.ToString();
            }
            /*没有数据或者操作失败返回空字符串*/
            else { return ""; }
        }

        /// <summary>
        /// 把datatable转化成json格式字符串
        /// </summary>
        /// <param name="dt">需要转化的datatable</param>
        /// <returns>返回json格式字符串</returns>
        public static string CreateJsonParameters(DataTable dt)
        {
            StringBuilder JsonString = new StringBuilder();
            //json格式头                   
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("{");
                JsonString.Append("\"rows\":[ ");
                //循环一次获取表中列名和对应的数据值
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{ ");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\"");
                        }
                    }

                    /*结束字符串*/
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("} ");

                    }
                    else
                    {
                        JsonString.Append("}, ");
                    }
                } JsonString.Append("]}");
                return JsonString.ToString();
            }
            /*没有数据或者操作失败返回空字符串*/
            else { return ""; }
        }

        /// <summary>
        /// 把datatable转化成json格式字符串
        /// </summary>
        /// <param name="dt">需要转化的datatable</param>
        /// <returns>返回json格式字符串</returns>
        public static string CreateJsonParametersForFlex(DataTable dt)
        {
            StringBuilder JsonString = new StringBuilder();
            //json格式头                   
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("[ ");
                //循环一次获取表中列名和对应的数据值
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{ ");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\"");
                        }
                    }

                    /*结束字符串*/
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("} ");

                    }
                    else
                    {
                        JsonString.Append("}, ");
                    }
                } JsonString.Append("]");
                return JsonString.ToString();
            }
            /*没有数据或者操作失败返回空字符串*/
            else { return ""; }
        }

        #endregion
    }
}