using System;
using System.Collections.Generic; 
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DAL;
using Model;
using BLL;
namespace BLL
{
    /// <summary>
    /// 区县代码控制类。
    /// </summary>
    public class BLL_zk_xqdm
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
        public bool Insert_zk_xqdm(Model_zk_xqdm item)
        {
            string sql = "insert into zk_xqdm(xqdm,xqmc) values(@xqdm,@xqmc)";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			
 			 new SqlParameter("@xqdm",item.Xqdm),
 			 new SqlParameter("@xqmc",item.Xqmc)
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
        public bool Insert_zk_xqdm(List<Model_zk_xqdm> Listitem)
        {
            string sql = "insert into zk_xqdm(xqdm,xqmc) values(@xqdm,@xqmc)";
            List<SqlParameter> lisP = new List<SqlParameter>();

            SqlParameter xqdm = new SqlParameter("@xqdm", SqlDbType.VarChar);
            SqlParameter xqmc = new SqlParameter("@xqmc", SqlDbType.VarChar);
            foreach (Model_zk_xqdm item in Listitem)
            {

                xqdm.Value = item.Xqdm;
                xqmc.Value = item.Xqmc;
                lisP.Clear();
                lisP.Add(xqdm);
                lisP.Add(xqmc);
                _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            }
            if (bReturn) return true;
            else return false;
        }
        public bool update_zk_xqdm(string set, string where)
        {
            string sql = "update  zk_xqdm set " + set + " where " + where;
            _dbHelper.ExecuteNonQuery(sql, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 根据ID修改
        /// </summary>
        /// <param name="item"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool update_zk_xqdm(Model_zk_xqdm item)
        {
            string sql = "update  zk_xqdm set xqdm=@xqdm,xqmc=@xqmc where qxId=@qxId";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",item.Xqdm),
 			 new SqlParameter("@xqmc",item.Xqmc),
 			 new SqlParameter("@qxId",item.QxId)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="qxId"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool delete_zk_xqdm(string qxId)
        {
            string sql = "delete  zk_xqdm where qxId=@qxId";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@qxId",qxId)};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }

        /// <summary>
        /// 根据多个县区代码删除指定数据
        /// </summary>
        /// <param name="xqdms">需要删除的县区代码列表</param>
        /// <returns></returns>
        public bool DeleteDataByXqdms(List<string> xqdms)
        {
            string inStr = "";
            foreach (var str in xqdms)
                inStr += "'" + str + "',";
            string sqlCmd = "Delete zk_xqdm Where qxId In (Select qxId From zk_xqdm Where xqdm In(" + inStr.Substring(0, inStr.Length - 1) + "))";
            _dbHelper.ExecuteNonQuery(sqlCmd, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }

        ///// <summary>
        ///// 公共构造方法。
        ///// </summary>
        //public BLL_zk_xqdm() { }

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
            string tabName = "zk_xqdm";
            //要查询的字段
            string reField = "xqdm,xqmc";
            //排序字段
            string orderStr = "xqdm";
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

        ///// <summary>
        ///// 查询县区信息
        ///// </summary>
        ///// 查询条件
        //public DataTable selectxqdm(string where)
        //{
        //    string sql = "";

        //    if (where == "")
        //    {
        //        sql = "select xqdm,xqmc='['+xqdm+']'+xqmc from zk_xqdm order by xqdm asc ";
        //    }
        //    else
        //    {
        //        sql = "select xqdm,xqmc='['+xqdm+']'+xqmc from zk_xqdm where " + where;
        //    }

        //    DataTable tab = _dbHelper.selectTab(sql, ref error, ref bReturn);
        //    return tab;
        //}


        /// <summary>
        /// 查询县区信息 条件 考生填报
        /// </summary>
        /// 查询条件
        public DataTable selectxqdmKs(string where)
        {
            string sql = "";
            if (where.Length == 0)
            {
                sql = "select xqdm,xqmc from zk_xqdm  order by xqdm asc";
            }
            else
            {
                sql = "select xqdm,xqmc from zk_xqdm where   " + where + " order by xqdm asc";
            }

            DataTable tab = _dbHelper.selectTab(sql, ref error, ref bReturn);
            return tab;
        }



        /// <summary>
        /// 查询县区信息 条件
        /// </summary>
        /// 查询条件
        public DataTable selectxqdm(string where)
        {
            string sql = "";
            if (where.Length == 0)
            {
                sql = "select xqdm,xqmc='['+xqdm+']'+xqmc from zk_xqdm  order by xqdm asc";
            }
            else
            {
                sql = "select xqdm,xqmc='['+xqdm+']'+xqmc from zk_xqdm where   " + where + " order by xqdm asc";
            }

            DataTable tab = _dbHelper.selectTab(sql, ref error, ref bReturn);
            return tab;
        }


        /// <summary>
        /// 查询县区信息 0表示全部 1除去全市和其他
        /// </summary>
        /// 查询条件type 0表示全部 1除去全市和其他
        public DataTable selectxqdm(int type )
        {
            string sql = "";

            if (type == 0)
            {
                sql = "select xqdm,xqmc='['+xqdm+']'+xqmc from zk_xqdm order by xqdm asc ";
            }
            else
            {
                sql = "select xqdm,xqmc='['+xqdm+']'+xqmc from zk_xqdm where right(xqdm,2) not in('00','99') order by xqdm asc ";
            }

            DataTable tab = _dbHelper.selectTab(sql, ref error, ref bReturn);
            return tab;
        }

        /// <summary>
        /// 查询县区信息
        /// </summary>      
        public DataTable selectxqdm()
        {
            string Eree = "";
            bool ispass = false;
            string sql = "";
            sql = "select xqdm,xqmc,xqmcc='['+xqdm+']'+xqmc from zk_xqdm order by xqdm asc ";

            DataTable tab = _dbHelper.selectTab(sql, ref Eree, ref ispass);
             
            if (ispass)
            {
                return tab;
            }
            else
            {
                return null;
            }
        }
         
 

        /// <summary>
        /// 根据县区代码查询 返回实体类
        /// </summary> 
        public Model_zk_xqdm Disp(string xqdm)
        {
            Model_zk_xqdm info = new Model_zk_xqdm();
            string sql = "select * from zk_xqdm where xqdm=@xqdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",xqdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
                info = _dbHelper.DT2EntityList<Model_zk_xqdm>(dt)[0];
            return info;
        }

        #region "县区查询加上权限判断 "
        /// <summary>
        /// 县区查询加上权限判断
        /// </summary>
        /// <param name="fanwei"></param>
        /// <param name="UserType"></param>
        /// <returns></returns>
        public DataTable SelectXqdm(string fanwei, int UserType)
        {
            string sql = "";
            string where = whereRole(fanwei, UserType, 2);
            if (where.Length == 0)
            {
                sql = "select xqdm,xqmc='['+xqdm+']'+xqmc from zk_xqdm  order by xqdm asc ";
            }
            else
            {
                //毕业中学县区不要3区
                string strxq = " ";
                sql = "select xqdm,xqmc='['+xqdm+']'+xqmc from zk_xqdm where   " + where + strxq + " order by xqdm asc";
            }

            DataTable tab = _dbHelper.selectTab(sql, ref error, ref bReturn);
            return tab; 
        }
       /// <summary>
       /// 管理部门权限控制
       /// </summary>
       /// <param name="fanwei">管理范围</param>
       /// <param name="UserType">用户类型</param>
       /// <returns></returns>
        public string whereRole(string fanwei, int UserType, int type)
        {           
            if (fanwei.Length > 3)
            {
                fanwei = 5 + fanwei.Substring(0, 2);
            } 
            string where = "";
            switch (UserType)
            {
                //系统管理员
                case 1:
                    if (type==1)
                    {
                        where = " right(xqdm,1) not in('0') ";
                    }
                    else
                    {
                        where = " right(xqdm,2) not in('00') ";
                    }
                   
                    break;
                //市招生办
                case 2:
                    if (type == 1)
                    {
                        where = " right(xqdm,1) not in('0') ";
                 
                    }else
                        where = " right(xqdm,2) not in('00') ";
                    break;
                //区招生办
                case 3:
                    where = " xqdm = '" + fanwei + "'  ";
                    break;
                //学校用户 
                case 4:
                    where = " xqdm = '" + fanwei + "' ";
                    break;
                //班级用户 
                case 5:
                    where = " xqdm = '" + fanwei + "' ";
                    break;
                default:
                     where=" 1<>1";
                    break;
            }
            return where;
        }


        /// <summary>
        /// 县区查询
        /// </summary>
        /// <param name="fanwei"></param>
        /// <param name="UserType"></param>
        /// <returns></returns>
        public DataTable SelectXqdmbmgl(string fanwei, int UserType)
        {
            string sql = "";
            string where = whereRole(fanwei, UserType, 2);
            if (where.Length == 0)
            {
                sql = "select xqdm,xqmc='['+xqdm+']'+xqmc from zk_xqdm  order by xqdm asc ";
            }
            else
            {
                //毕业中学县区不要3区
                string str = " and xqdm not in ('3202','3203','3204')";
                sql = "select xqdm,xqmc='['+xqdm+']'+xqmc from zk_xqdm where   " + where + str + " order by xqdm asc";
            }

            DataTable tab = _dbHelper.selectTab(sql, ref error, ref bReturn);
            return tab;
        }
        #endregion

    }
}
