/************************************
*    业务逻辑类   		
*
*
*************************************/

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Text;
using System.IO;
using Model;
using LinqToExcel;
using LinqToExcel.Query;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using DAL; 
namespace BLL
{



    /// <summary>
    /// Method 业务逻辑类
    /// </summary>
    public class Method
    {
        private SqlDbHelper_1 _dbHelper = new SqlDbHelper_1();
        DBcon objDBcon = new DBcon();
        config config = new config();
        DataSet ds = new DataSet();

        #region "Excel数据导入到SqlServer数据库方法"
        /// <summary>
        /// Excel数据导入到SqlServer数据库方法
        /// </summary>
        /// <param name="excelFilePath">excel文件存放路径</param>
        /// <param name="importSqlCmd">需要用于导入数据的sql语句</param>
        /// <param name="importParams">用于导入数据的sql参数列表</param>
        /// <param name="isZL">是否增量导入</param>
        /// <returns></returns>
        public string ImportExcelData(string excelFilePath, int UserType, string fanwei)
        {
            if (!File.Exists(excelFilePath))
                return "导入失败，原因是：输入的文件路径不正确或指定的文件不存在。";

            ExcelQueryFactory excelFile = new ExcelQueryFactory(excelFilePath);
            ExcelQueryable<Row> excel = excelFile.Worksheet(0);

            StringBuilder resultMsg = new StringBuilder();

            int i = 0;
            int lost = 0;
            int finish = 0;


            foreach (var element in excel)
            {
                sys_UserTable fam = new sys_UserTable();
                fam.DB_Option_Action_ = "Insert";
                fam.U_LoginName = element[0].ToString().Trim();
                fam.U_xm = element[1].ToString().Trim();
                fam.U_Password = "123456";
                fam.U_tag = 1;
                fam.U_department = element[0].ToString().Trim();
                fam.U_phone = element[2].ToString().Trim();
                sys_UserRolesTable ur = new sys_UserRolesTable();
                ur.DB_Option_Action_ = "Insert";
                ur.R_UserName = element[0].ToString().Trim();

                if (element.ColumnNames.Count() != 3)
                    return "导入失败，原因是：格式不对，目标格式为3列，您输入的是：" + element.ColumnNames.Count() + "列的格式。";

                string errMsg = "";
                string baseStr = ""; //"系统时间：" + DateTime.Now.ToString("yyyy年MM月dd日hh时mm分ss秒") + "。";
                //空数据则跳过
                if (string.IsNullOrEmpty(element[0].ToString().Trim()))
                    continue;
                //市招生办
                if (UserType == 1 || UserType == 2)
                {
                    if (element[0].ToString().Trim().Length == 4)
                    {
                        fam.U_usertype = 3;
                        ur.R_RoleID = 3;
                    }
                    else if (element[0].ToString().Trim().Length == 6)
                    {
                        fam.U_usertype = 4;
                        ur.R_RoleID = 4;
                    }
                    else if (element[0].ToString().Trim().Length == 8)
                    {
                        fam.U_usertype = 5;
                        ur.R_RoleID = 5;
                    }
                    else if (element[0].ToString().Trim().Length == 7)
                    {
                        fam.U_usertype = 7;
                        ur.R_RoleID = 7;
                        fam.U_department = element[0].ToString().Trim().Substring(1, 4);
                    }
                    else if (element[0].ToString().Trim().Length == 5)//招生学校
                    {
                        string users = element[0].ToString().Trim().Substring(1);
                        if (users.Substring(0, 1) == "3")
                        {
                            fam.U_usertype = 9;
                            ur.R_RoleID = 9;
                        }
                        else
                        {
                            fam.U_usertype = 8;
                            ur.R_RoleID = 8;
                        }
                        fam.U_LoginName = users;
                        ur.R_UserName = users;
                        fam.U_department = "500";
                    }
                    else
                    {
                        errMsg = "第" + (i + 1) + "条数据有误，原因是：登录帐号" + element[0].ToString().Trim() + "有错。";
                    }

                }
                //区招生办
                if (UserType == 3)
                {

                    if (element[0].ToString().Trim().Length == 6 && element[0].ToString().Substring(0, 4) == fanwei)
                    {
                        fam.U_usertype = 4;
                        ur.R_RoleID = 4;
                    }
                    else if (element[0].ToString().Trim().Length == 8 && element[0].ToString().Substring(0, 4) == fanwei)
                    {
                        fam.U_usertype = 5;
                        ur.R_RoleID = 5;
                    }
                    else
                    {
                        errMsg = "第" + (i + 1) + "条数据有误，原因是：登录帐号" + element[0].ToString().Trim() + "有错。";
                    }

                }
                //学校
                if (UserType == 4)
                {

                    if (element[0].ToString().Trim().Length == 8 && element[0].ToString().Substring(0, 6) == fanwei)
                    {
                        fam.U_usertype = 5;
                        ur.R_RoleID = 5;
                    }
                    else
                    {
                        errMsg = "第" + (i + 1) + "条数据有误，原因是：登录帐号" + element[0].ToString().Trim() + "有错。";
                    }

                }

                int userid = sys_UserDisp(fam.U_LoginName).UserID;

                //需要导入的学校是否已存在于指定县区
                if (userid > 0)
                {
                    errMsg += "第" + (i + 1) + "条数据导有误，原因是：登录帐号" + element[0].ToString().Trim() + "已存在。";
                }

                //如果上面的验证都通过的话就继续执行添加 
                if (string.IsNullOrEmpty(errMsg))
                {
                    Sys_usersInsertUpdateDelete(fam);
                    Sys_UserRolesInsertUpdateDelete(ur);
                }


                if (!string.IsNullOrEmpty(errMsg))
                {
                    resultMsg.Append(errMsg);
                    lost++;
                }
                else
                {
                 //   resultMsg.Append(baseStr + "第" + (i + 1) + "行数据导入成功。<br />");
                    finish++;
                }

             //   resultMsg.Append("---------------------------------------------------<br /><br />");
                i++;
            }

            resultMsg.Append("共处理:" + i + "数据导入完毕。共成功导入：" + finish + "条数据。导入失败：" + lost + "条数据。<br />");

            File.Delete(excelFilePath);

            return resultMsg.ToString();
        }
        #endregion


        #region

        /// <summary>
        /// 查询模块信息方法
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModule()
        {
            Hashtable _hastable = new Hashtable();
            DataSet dxsp = objDBcon.ReturnDataSetProc(_hastable, "selectModule", "module");
            return dxsp;
        }
        /// <summary>
        /// 根据模块ID查询应用列表
        /// </summary>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        public DataSet GetAllApplicatioBymoduleid(int moduleid)
        {
            Hashtable _hastable = new Hashtable();
            _hastable.Add("@A_moduleid", moduleid);
            DataSet dxsp = objDBcon.ReturnDataSetProc(_hastable, "selectApplication", "Application");
            return dxsp;
        }
        /// <summary>
        /// 模块排序方法
        /// </summary>
        /// <returns></returns>
        public DataSet GetModuleoderby(int moduleID, int flag)
        {
            Hashtable _hastable = new Hashtable();
            _hastable.Add("@Moduleid", moduleID);
            _hastable.Add("@flag", flag);
            DataSet dxsp = objDBcon.ReturnDataSetProc(_hastable, "Pd_Position", "module");
            return dxsp;

        }
        /// <summary>
        /// 根据ID删除模块信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DataSet DeleteModulByID(int ID)
        {
            Hashtable _hastable = new Hashtable();
            _hastable.Add("@Moduleid", ID);
            DataSet dxsp = objDBcon.ReturnDataSetProc(_hastable, "pd_Delectmodule", "module");
            return dxsp;
        }
        /// <summary>
        /// 根据ID删除应用信息
        /// </summary>
        /// <returns></returns>
        public DataSet DeletApplicByID(int ID)
        {
            Hashtable _hastable = new Hashtable();
            _hastable.Add("@Applicationid", ID);
            DataSet dxsp = objDBcon.ReturnDataSetProc(_hastable, "pd_deleteApplictions", "Applictions");
            return dxsp;
        }
        /// <summary>
        /// 加一
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public DataSet Updatemoduleorderjiayi(int OrderID)
        {
            Hashtable _hastable = new Hashtable();
            _hastable.Add("@Moduleid", OrderID);
            DataSet dxsp = objDBcon.ReturnDataSetProc(_hastable, "AddmodelOrder", "module");
            return dxsp;
        }
        /// <summary>
        /// 减一
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public DataSet Updatemoduleorderjianyi(int OrderID)
        {
            Hashtable _hastable = new Hashtable();
            _hastable.Add("@Moduleid", OrderID);
            DataSet dxsp = objDBcon.ReturnDataSetProc(_hastable, "updateOrderModule", "module");
            return dxsp;
        }
        #endregion

        #region "删除用户帐号信息"
        /// <summary>
        /// 删除用户帐号信息
        /// </summary>
        public DataSet DelUserInfo(string Userid)
        {
            Hashtable hastable = new Hashtable();
            hastable.Add("@Userid", Userid);
            DataSet dxsp = objDBcon.ReturnDataSetProc(hastable, "Pd_DeleteUsers", "Sys_users");
            return dxsp;
        }
        #endregion

        #region
        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="Roleid"></param>
        /// <returns></returns>
        public DataSet DelRoles(string Roleid)
        {
            Hashtable hastable = new Hashtable();
            hastable.Add("@Roleid", Roleid);
            DataSet ds = objDBcon.ReturnDataSetProc(hastable, "Pd_DeleteRoles", "Sys_roles");
            return ds;
        }
        /// <summary>
        /// 根据P_roleid和P-modulesID查询apptionsname 和 P_valu
        /// </summary>
        /// <param name="moduleID"></param>
        /// <returns></returns>
        public DataSet GetApplicationNameBymoduleID(int roleid, int moduleID)
        {
            Hashtable _hastable = new Hashtable();
            _hastable.Add("@P_roleid", roleid);
            _hastable.Add("@P_moduleid", moduleID);
            DataSet ds = objDBcon.ReturnDataSetProc(_hastable, "SelectPagecodebymoduleID", "pagecode");
            return ds;
        }
        /// <summary>
        /// 根据Pid查询权限值
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public DataSet GetRolepermissionOfvaluByPid(int pid)
        {
            Hashtable _hastable = new Hashtable();
            _hastable.Add("@Pid", pid);
            DataSet dxsp = objDBcon.ReturnDataSetProc(_hastable, "Pd_SelectRolepermission", "Valu");
            return dxsp;
        }
        #endregion

        #region "添加用户信息"
        /// <summary>
        /// 添加用户信息
        /// </summary>
        public void Pd_AddUser()
        {
            Hashtable hashtable = new Hashtable();
            objDBcon.ReturnNoProc(hashtable, "Pd_AddUser");
        }
        #endregion

        #region
        /// <summary>
        /// 添加角色
        /// </summary>
        public void AddRoles()
        {
            Hashtable hashtable = new Hashtable();
            objDBcon.ReturnNoProc(hashtable, "Pd_AddRoles");
        }

        /// <summary>
        /// 查询机构
        /// </summary>
        /// <param name="quxian_id"></param>
        /// <returns></returns>
        public DataSet Selectjigou(string quxian_id)
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("@quxian_id", quxian_id);
            DataSet ds = objDBcon.ReturnDataSetProc(hashtable, "Pd_SelectM_jigou", "M_jigou");
            return ds;
        }

        /// <summary>
        /// 查询部门
        /// </summary>
        /// <param name="j_id"></param>
        /// <returns></returns>
        public DataSet Selectbumen(string j_id)
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("@j_id", j_id);
            DataSet ds = objDBcon.ReturnDataSetProc(hashtable, "Pd_SelectM_bumen", "M_bumen");
            return ds;
        }
        #endregion

        #region "用户角色代码转为名称"
        /// <summary>
        /// 用户角色代码转为名称
        /// </summary>
        public string SelectRoleName(string strRole)
        {
            string R_name = "";
            string[] arryRole = strRole.Split(',');
            bool flag = false;

            DataSet ds = objDBcon.DataAdapterSearch("select Roleid, R_name from Sys_roles order by Roleid asc", "Sys_roles");
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables["Sys_roles"].Rows.Count; i++)
                {
                    flag = false;
                    for (int j = 0; j < arryRole.Length; j++)
                    {
                        if (arryRole[j] == ds.Tables["Sys_roles"].Rows[i]["Roleid"].ToString().Trim())
                        {
                            R_name = R_name + "<img src='/images/closed.gif'>" + ds.Tables["Sys_roles"].Rows[i]["R_name"].ToString().Trim() + " <font color='red'>√</font>&nbsp;&nbsp;&nbsp;";
                            flag = true;
                        }

                    }
                    if (!flag)
                    {
                        R_name = R_name + "<img src='/images/closed.gif'>" + ds.Tables["Sys_roles"].Rows[i]["R_name"].ToString().Trim() + " <font color='#0000ff'>X</font>&nbsp;&nbsp;&nbsp;";
                    }
                }
            }
            return R_name;
        }
        #endregion

        #region "显示用户角色信息"
        /// <summary>
        /// 显示用户角色信息
        /// </summary>
        public DataSet SelecttblRole()
        {
            DataSet ds = objDBcon.DataAdapterSearch("select Roleid,R_name from Sys_roles order by Roleid", "Sys_roles");
            return ds;
        }
        #endregion

        #region "查找"用户信息
        /// <summary>
        /// 查找用户信息
        /// </summary>
        public DataSet SelectSys_users()
        {
            Hashtable hashtable = new Hashtable();
            DataSet dsuser = objDBcon.ReturnDataSetProc(hashtable, "Pd_SelectUser", "Sys_users");
            return dsuser;
        }
        #endregion

        #region
        /// <summary>
        /// 查找角色信息
        /// </summary>
        /// <returns></returns>
        public DataSet SelectRoles()
        {
            Hashtable hashtable = new Hashtable();
            DataSet ds = objDBcon.ReturnDataSetProc(hashtable, "Pd_SelectRoles", "Sys_roles");
            return ds;
        }
        /// <summary>
        /// 查询城市
        /// </summary>
        /// <returns></returns>
        public DataSet SelectCity()
        {
            Hashtable hashtable = new Hashtable();
            DataSet ds = objDBcon.ReturnDataSetProc(hashtable, "Pd_SelectCity", "Sys_city");
            return ds;
        }
        /// <summary>
        /// 查询县区
        /// </summary>
        /// <returns></returns>
        public DataSet Selectquxian(string cityid)
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("@cityid", cityid);
            DataSet ds = objDBcon.ReturnDataSetProc(hashtable, "Pd_Selectquxian", "Sys_quxian");
            return ds;
        }
        /// <summary>
        /// 根据条件查找用户信息
        /// </summary>
        /// <param name="U_loginname"></param>
        /// <returns></returns>
        public DataSet GetAllUserBy(string U_loginname, string U_xm)
        {
            Hashtable hastable = new Hashtable();
            hastable.Add("@U_loginname", U_loginname);
            hastable.Add("@U_xm", U_xm);
            DataSet dxsp = objDBcon.ReturnDataSetProc(hastable, "selectUserBy", "Sys_users");
            return dxsp;
        }
        /// <summary>
        /// 根据角色ID查询所属模块名称
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public DataSet GetAllModulnamebyroleid(int roleid)
        {
            Hashtable _hashtable = new Hashtable();
            _hashtable.Add("@A_roleid", roleid);
            DataSet dxsp = objDBcon.ReturnDataSetProc(_hashtable, "Getmodulnamebyroleid", "Sys_roemlodules");
            return dxsp;
        }
        /// <summary>
        /// 根据模块id删除角色模块表信息
        /// </summary>
        /// <param name="moduleID"></param>
        /// <returns></returns>
        public DataSet DeleterolemoudesByModuleID(int moduleID)
        {
            Hashtable _hashtable = new Hashtable();
            _hashtable.Add("@A_moduleid", moduleID);
            DataSet dxsp = objDBcon.ReturnDataSetProc(_hashtable, "Pd_DeleteromodlesByModulID", "Sys_roemlodules");
            return dxsp;
        }
        #endregion

        #region "修改用户帐号密码"
        /// <summary>
        /// 修改用户帐号密码
        /// </summary>
        public int UpdUserPwd(string U_loginname, string U_password)
        {

            return objDBcon.CommandSql("update Sys_users set U_password='" + U_password + "' where U_loginname='" + U_loginname + "' ");


        }
        #endregion

        #region
        /// <summary>
        /// 格式化字符串,符合SQL语句
        /// </summary>
        /// <param name="formatStr">需要格式化的字符串</param>
        /// <returns>字符串</returns>
        public static string inSQL(string formatStr)
        {
            string rStr = formatStr;
            if (formatStr != null && formatStr != string.Empty)
            {
                rStr = rStr.Replace("'", "''");
            }
            return rStr;
        }

        /// <summary>
        /// 用户登录时 查询
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public DataSet UserLogin(string UserName)
        {
            Hashtable _hashtable = new Hashtable();
            _hashtable.Add("@UserName", UserName);
            DataSet ds = objDBcon.ReturnDataSetProc(_hashtable, "Pd_UsersSelect", "Sys_users");
            _hashtable.Clear();
            return ds;
        }
        #endregion

        //以下是最新的 业务逻辑类

        //---------------------------- 系统业务类 ----------------------------//

        #region "sys_Event - Method"
        /// <summary>
        /// 新增/删除/修改 sys_Event
        /// </summary>
        /// <param name="fam">sys_EventTable实体类</param>       
        public int sys_EventInsertUpdate(sys_EventTable fam)
        {
            return objDBcon.sys_EventInsertUpdate(fam);
        }
        /// <summary>
        /// 返回sys_Event实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_Event实体类的ArrayList对象</returns>
        public DataTable sys_EventList(QueryParam qp, out int RecordCount)
        {
            DataTable dt = new DataTable();
            qp.TableName = "sys_Event";
            qp.ReturnFields = "*";
            if (qp.OrderId == null)
            {
                qp.OrderId = "EventID";
            }

            DataSet ds = objDBcon.sys_EventList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        #endregion

        #region "sys_UserTable - Method"

        ///// <summary>
        ///// 根据用户登录名,获取用户资料
        ///// </summary>
        ///// <param name="u_LoginName">用户名</param>
        ///// <returns>用户实体类</returns>
        //public sys_UserTable Get_sys_UserTable(string u_LoginName)
        //{

        //    QueryParam qp = new QueryParam();
        //    qp.TableName = "Sys_users";
        //    qp.ReturnFields = "*";
        //    qp.Where = string.Format("  U_loginname='{0}' ", u_LoginName);
        //    qp.OrderId = "Userid";
        //    qp.Order = " order by Userid ";
        //    qp.PageIndex = 1;
        //    qp.PageSize = 10;

        //    int RecordCount = 0;
        //    return objDBcon.UserList(qp, out RecordCount)[0] as sys_UserTable;
        //}

        /// <summary>
        /// 新增/删除/修改 sys_UserTable (Sys_users)
        /// </summary>
        /// <param name="fam">sys_UserTable实体类</param>
        /// <returns>-1:存储过程执行失败,-2:存在相同的主键,Insert:返回插入自动ID,Update:返回更新记录数,Delete:返回删除记录数</returns>
        public int Sys_usersInsertUpdateDelete(sys_UserTable fam)
        {
            return objDBcon.Sys_usersInsertUpdateDelete(fam);
        }

        /// <summary>
        /// 返回sys_UserTable实体类的DataSet对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_UserTable实体类的DataSet对象</returns>
        public DataTable sys_UserList(QueryParam qp, out int RecordCount)
        {
            DataTable dt = new DataTable();
            qp.TableName = "Sys_users";
            qp.ReturnFields = "*";
            if (qp.OrderId == null)
            {
                qp.OrderId = "Userid";
            }

              DataSet ds= objDBcon.Sys_UserList(qp, out RecordCount);
              if (RecordCount > 0)
              {
                  dt = ds.Tables[0];
              }
              return dt;
        }

        /// <summary>
        /// 根据ID返回 sys_UserTable实体类 单笔资料
        /// </summary>
        /// <param name="UserID">用户ID号</param>
        /// <returns>返回sys_UserTable实体类 UserID为0则无记录</returns>
        public sys_UserTable sys_UserDisp(int UserID)
        {
            sys_UserTable fam = new sys_UserTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = string.Format("  Userid = '{0}' ", UserID);
            int RecordCount = 0;
            DataTable dt = sys_UserList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = DT2EntityList<sys_UserTable>(dt)[0]; 
            }
            return fam;
        }
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
        /// <summary>
        /// 根据ID返回 sys_UserTable实体类 单笔资料
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <returns>返回sys_UserTable实体类 UserName为""则无记录</returns>
        public sys_UserTable sys_UserDisp(string UserName)
        {
            sys_UserTable fam = new sys_UserTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = string.Format("  U_loginname = '{0}' ", UserName);
            int RecordCount = 0;
            DataTable dt = sys_UserList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = DT2EntityList<sys_UserTable>(dt)[0]; 
            }
            return fam;
        }




        #endregion

        #region "sys_UserRoles - Method"

        /// <summary>
        /// 新增/删除/修改 sys_UserRolesTable (Sys_users)
        /// </summary>
        /// <param name="fam">sys_UserRolesTable实体类</param>
        /// <returns>-1:存储过程执行失败,-2:存在相同的主键,Insert:返回插入自动ID,Update:返回更新记录数,Delete:返回删除记录数</returns>
        public int Sys_UserRolesInsertUpdateDelete(sys_UserRolesTable fam)
        {
            return objDBcon.Sys_UserRolesInsertUpdateDelete(fam);
        }

        /// <summary>
        /// 返回sys_UserRolesTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_UserRolesTable实体类的ArrayList对象</returns>
        public DataTable sys_UserRolesList(QueryParam qp, out int RecordCount)
        {
            DataTable dt = new DataTable();
            qp.TableName = "Sys_userroles";
            if (qp.OrderId == null)
            {
                qp.OrderId = "R_UserName";
            }
            qp.ReturnFields = "*";
            if (qp.Order == null)
            {
                qp.Order = " order by R_UserName,R_roleid";
            }
            DataSet ds = objDBcon.sys_UserRolesList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 删除根据用户名,角色记录
        /// </summary>
        /// <param name="UserName">用户名</param>
        public void sys_UserRoles_Move(string UserName)
        {
            int RecordCount = 0;
            QueryParam qp = new QueryParam();
            qp.Where = string.Format("  R_UserName='{0}'  ", UserName);
            DataTable dt = sys_UserRolesList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                List<sys_UserRolesTable> lst = DT2EntityList<sys_UserRolesTable>(dt);

                for (int i = 0; i < lst.Count; i++)
                {
                    sys_UserRolesTable var = lst[i];

                    var.DB_Option_Action_ = "Delete";
                    Sys_UserRolesInsertUpdateDelete(var);
                }
            }
        }
        #endregion

        #region "Sys_UserType(Sys_UserType) - Method"

        /// <summary>
        /// 新增/删除/修改 Sys_UserTypeTable (Sys_UserType)
        /// </summary>
        /// <param name="fam">Sys_UserTypeTable实体类</param>
        /// <returns>-1:存储过程执行失败,-2:存在相同的主键,Insert:返回插入自动ID,Update:返回更新记录数,Delete:返回删除记录数</returns>
        public int Sys_UserTypeInsertUpdateDelete(Sys_UserTypeTable fam)
        {
            return objDBcon.Sys_UserTypeInsertUpdateDelete(fam);
        }

        /// <summary>
        /// 根据TypeID返回 Sys_UserTypeTable实体类 单笔资料 (Sys_UserType)
        /// </summary>
        /// <param name="TypeID">TypeID 用户类型ID</param>
        /// <returns>返回 Sys_UserTypeTable实体类 TypeID为0则无记录</returns>
        public Sys_UserTypeTable Sys_UserTypeDisp(int TypeID)
        {
            Sys_UserTypeTable fam = new Sys_UserTypeTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = string.Format("   {0}.{1} = {2}", "Sys_UserType", "TypeID", TypeID);
            int RecordCount = 0;
            DataTable dt = Sys_UserTypeList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = DT2EntityList<Sys_UserTypeTable>(dt)[0];
            }
            return fam;
        }

        /// <summary>
        /// 返回Sys_UserTypeTable实体类的ArrayList对象 (Sys_UserType)
        /// </summary>
        /// <param name="qp">查询类(非安全函数,传入参数请进行Sql字符串过滤)</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>Sys_UserTypeTable实体类的ArrayList对象(Sys_UserType)</returns>
        public DataTable Sys_UserTypeList(QueryParam qp, out int RecordCount)
        {
            DataTable dt = new DataTable();
            qp.TableName = "Sys_UserType";
            if (qp.OrderId == null)
            {
                qp.OrderId = "TypeID";
            }


            if (qp.ReturnFields == null)
            {
                qp.ReturnFields = "*";
            }

            DataSet ds= objDBcon.Sys_UserTypeList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        #endregion

        #region "sys_ModuleTable - Method"

        /// <summary>
        /// 新增/删除/修改sys_Module
        /// </summary>
        /// <param name="fam">sys_EventTable实体类</param>       
        public int sys_ModuleInsertUpdate(Sys_moduleTable fam)
        {
            return objDBcon.Sys_moduleInsertUpdateDelete(fam);
        }

        /// <summary>
        /// 根据ID返回 sys_ModuleTable实体类 单笔资料
        /// </summary>
        /// <param name="Moduleid">自动ID</param>
        /// <returns>返回sys_ModuleTable实体类 Moduleid为0则无记录</returns>
        public Sys_moduleTable sys_ModuleDisp(int Moduleid)
        {
            Sys_moduleTable fam = new Sys_moduleTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = " sys_Module.Moduleid = " + Moduleid;
            int RecordCount = 0;
            DataTable dt = sys_ModuleList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = DT2EntityList<Sys_moduleTable>(dt)[0];
            }
            return fam;
        }
        /// <summary>
        /// 返回sys_ModuleTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_ModuleTable实体类的ArrayList对象</returns>
        public DataTable sys_ModuleList(QueryParam qp, out int RecordCount)
        {
            DataTable dt = new DataTable();
            qp.TableName = "sys_Module ";
            qp.ReturnFields = "*";
            if (qp.OrderId == null)
            {
                qp.OrderId = "Moduleid";
            }
            DataSet ds= objDBcon.ModuleList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        /// <summary>
        /// 县区查询加上权限判断
        /// </summary>
        /// <param name="fanwei"></param>
        /// <param name="UserType"></param>
        /// <returns></returns>
        public DataTable SelectModuleList(  int UserType)
        {
            string sql = "select * from  Sys_rolemodules a left join sys_Module b on a.A_moduleid=b.Moduleid where  M_tag=1 and A_roleid=" + UserType + " order by m_order asc ";
            string error = "";
            bool bReturn=false;
            DataTable tab = _dbHelper.selectTab(sql, ref error, ref bReturn);
            return tab;
        }


        #endregion

        #region "sys_RoleModuleTable - Method"

        /// <summary>
        /// 新增/删除/修改Sys_rolemodules
        /// </summary>
        /// <param name="fam">sys_EventTable实体类</param>       
        public int sys_RoleModuleInsertUpdate(sys_RoleModuleTable fam)
        {
            return objDBcon.Sys_RoleModuleInsertUpdateDelete(fam);
        }


        /// <summary>
        /// 返回sys_RoleModuleTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_RoleModuleTable实体类的ArrayList对象</returns>
        public DataTable sys_RoleModuleList(QueryParam qp, out int RecordCount)
        {
            DataTable dt = new DataTable();
            qp.TableName = "Sys_rolemodules";
            qp.ReturnFields = "*";
            qp.OrderId = "A_moduleid";

            DataSet ds= objDBcon.RoleModuleList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;

        }
        #endregion

        #region "Sys_RoleUsertType(Sys_RoleUsertType) - Method"

        /// <summary>
        /// 新增/删除/修改 Sys_RoleUsertTypeTable (Sys_RoleUsertType)
        /// </summary>
        /// <param name="fam">Sys_RoleUsertTypeTable实体类</param>
        /// <returns>-1:存储过程执行失败,-2:存在相同的主键,Insert:返回插入自动ID,Update:返回更新记录数,Delete:返回删除记录数</returns>
        public   Int32 Sys_RoleUsertTypeInsertUpdateDelete(Sys_RoleUsertTypeTable fam)
        {
            return objDBcon.Sys_RoleUsertTypeInsertUpdateDelete(fam);
        } 

        /// <summary>
        /// 返回Sys_RoleUsertTypeTable实体类的List对象 (Sys_RoleUsertType)
        /// </summary>
        /// <param name="qp">查询类(非安全函数,传入参数请进行Sql字符串过滤)</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>Sys_RoleUsertTypeTable实体类的List对象(Sys_RoleUsertType)</returns>
        public DataTable Sys_RoleUsertTypeList(QueryParam qp, out int RecordCount)
        {
            DataTable dt = new DataTable();
            qp.TableName = "Sys_RoleUsertType";
            if (qp.OrderId == null)
            {
                qp.OrderId = "A_UserTypeID";
            }
            else if (qp.OrderId != "A_UserTypeID")
            {
                qp.OrderId += ",A_UserTypeID";
            }

            if (qp.ReturnFields == null)
            {
                qp.ReturnFields = "*";
            }
            else
            {
                qp.ReturnFields += ",";
                qp.ReturnFields += qp.OrderId;
            }
            DataSet ds=  objDBcon.Sys_RoleUsertTypeList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;

        }
        #endregion

        #region  "sys_applicationsTable - Method"

        /// <summary>
        /// 新增/删除/修改 Sys_applicationsTable (Sys_applications)
        /// </summary>
        /// <param name="fam">Sys_applicationsTable实体类</param>
        /// <returns>-1:存储过程执行失败,-2:存在相同的主键,Insert:返回插入自动ID,Update:返回更新记录数,Delete:返回删除记录数</returns>
        public int Sys_applicationsInsertUpdateDelete(Sys_applicationsTable fam)
        {
            return objDBcon.Sys_applicationsInsertUpdateDelete(fam);
        }


        /// <summary>
        /// 根据Applicationid返回 Sys_applicationsTable实体类 单笔资料 (Sys_applications)
        /// </summary>
        /// <param name="Applicationid">Applicationid 应用ID标识</param>
        /// <returns>返回 Sys_applicationsTable实体类 Applicationid为0则无记录</returns>
        public Sys_applicationsTable Sys_applicationsDisp(int Applicationid)
        {
            Sys_applicationsTable fam = new Sys_applicationsTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = string.Format("  {0}.{1} = {2}", "Sys_applications", "Applicationid", Applicationid);
            int RecordCount = 0;
            DataTable dt = sys_applicationsList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = DT2EntityList<Sys_applicationsTable>(dt)[0];
            }
            return fam;
        }

        /// <summary>
        ///  返回 sys_applicationsList  实体类的ArrayList对象
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="RecordCount"></param>
        /// <returns></returns>
        public DataTable sys_applicationsList(QueryParam qp, out int RecordCount)
        {
            DataTable dt = new DataTable();
            qp.TableName = " Sys_applications ";
            qp.ReturnFields = " * ";
            if (qp.OrderId == null)
            {
                qp.OrderId = "Applicationid";
            }
            DataSet ds= objDBcon.ApplicationsList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }


        /// <summary>
        /// 根据ModuleID获取当前用户拥有权限的子菜单项
        /// </summary>
        /// <param name="ModuleID">ModuleID</param>
        /// <returns></returns>
        public DataTable GetPermissionApplSub(int ModuleID)
        {
            DataTable dt = new DataTable();
            QueryParam qp = new QueryParam();
            qp.OrderId = " Applicationid ";
            qp.Order = " order by A_order asc ";
            qp.Where = string.Format("  A_tag=1  and A_moduleid={0} ", ModuleID);
            int RecordCount = 0;
            dt = sys_applicationsList(qp, out RecordCount);
            if (RecordCount > 0)
            { 
                Remove_MenuNoPermission(dt);
            }

            return dt;
        }

        /// <summary>
        /// 根据A_moduleid和M_PageCode返回 sys_applicationsTable实体类 单笔资料
        /// </summary>
        /// <param name="M_ApplicationID">功能模块ID号</param>
        /// <param name="M_PageCode">M_PageCode</param>
        /// <returns>返回sys_ModuleTable实体类 ModuleID为0则无记录</returns>
        public Sys_applicationsTable sys_ApplicationDisp(int A_moduleid, string M_PageCode)
        {
            Sys_applicationsTable fam = new Sys_applicationsTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = string.Format(" Sys_applications.A_moduleid = {0} and A_pagecode = '{1}'", A_moduleid, config.CheckChar(M_PageCode));
            int RecordCount = 0;
            DataTable dt = sys_applicationsList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = DT2EntityList<Sys_applicationsTable>(dt)[0];
            }
            return fam;
        }


        /// <summary>
        /// 根据用户访问的url地址，查找应用资料
        /// </summary>
        /// <returns></returns>
        public Sys_applicationsTable sys_applUrlDisp()
        {
            Sys_applicationsTable fam = new Sys_applicationsTable();
            QueryParam qp = new QueryParam();
            qp.OrderId = " Applicationid ";
            qp.PageSize = 1;
            qp.PageIndex = 1;
            qp.Where = string.Format(" A_tag=1 and A_url='{0}' ", config.GetScriptName);
            int RecordCount = 0;
            DataTable dt = sys_applicationsList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = DT2EntityList<Sys_applicationsTable>(dt)[0];
            }
            return fam;
        }


        #endregion

        #region "Sys_Permission(Sys_Permission) - Method"

        /// <summary>
        /// 新增/删除/修改 Sys_PermissionTable (Sys_Permission)
        /// </summary>
        /// <param name="fam">Sys_PermissionTable实体类</param>
        /// <returns>-1:存储过程执行失败,-2:存在相同的主键,Insert:返回插入自动ID,Update:返回更新记录数,Delete:返回删除记录数</returns>
        public   Int32 Sys_PermissionInsertUpdateDelete(Sys_PermissionTable fam)
        {
            return objDBcon.Sys_PermissionInsertUpdateDelete(fam);
        }

        /// <summary>
        /// 根据id返回 Sys_PermissionTable实体类 单笔资料 (Sys_Permission)
        /// </summary>
        /// <param name="id">id 自动增长ID</param>
        /// <returns>返回 Sys_PermissionTable实体类 id为0则无记录</returns>
        public   Sys_PermissionTable Sys_PermissionDisp(Int32 id)
        {
            Sys_PermissionTable fam = new Sys_PermissionTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = string.Format("   {0}.{1} = {2}", "Sys_Permission", "id", id);
            int RecordCount = 0;
            DataTable dt = Sys_PermissionList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = DT2EntityList<Sys_PermissionTable>(dt)[0];
            }
            return fam;
        }

        /// <summary>
        /// 返回Sys_PermissionTable实体类的List对象 (Sys_Permission)
        /// </summary>
        /// <param name="qp">查询类(非安全函数,传入参数请进行Sql字符串过滤)</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>Sys_PermissionTable实体类的List对象(Sys_Permission)</returns>
        public DataTable Sys_PermissionList(QueryParam qp, out int RecordCount)
        {
            DataTable dt = new DataTable();
            qp.TableName = "Sys_Permission";
            if (qp.OrderId == null)
            {
                qp.OrderId = "id";
            }
            else if (qp.OrderId != "id")
            {
                qp.OrderId += ",id";
            }

            if (qp.ReturnFields == null)
            {
                qp.ReturnFields = "*";
            }
            else
            {
                qp.ReturnFields += ",";
                qp.ReturnFields += qp.OrderId;
            }
            DataSet ds= objDBcon.Sys_PermissionList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        #endregion

        #region "Sys_roles(Sys_roles) - Method"

        /// <summary>
        /// 新增/删除/修改 Sys_rolesTable (Sys_roles)
        /// </summary>
        /// <param name="fam">Sys_rolesTable实体类</param>
        /// <returns>-1:存储过程执行失败,-2:存在相同的主键,Insert:返回插入自动ID,Update:返回更新记录数,Delete:返回删除记录数</returns>
        public int Sys_rolesInsertUpdateDelete(Sys_rolesTable fam)
        {
            return objDBcon.Sys_rolesInsertUpdateDelete(fam);
        }

        /// <summary>
        /// 根据Roleid返回 Sys_rolesTable实体类 单笔资料 (Sys_roles)
        /// </summary>
        /// <param name="Roleid">Roleid 角色ID标识</param>
        /// <returns>返回 Sys_rolesTable实体类 Roleid为0则无记录</returns>
        public Sys_rolesTable Sys_rolesDisp(int Roleid)
        {
            Sys_rolesTable fam = new Sys_rolesTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = string.Format(" {0}.{1} = {2}", "Sys_roles", "Roleid", Roleid);
            int RecordCount = 0;
            DataTable dt = Sys_rolesList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = DT2EntityList<Sys_rolesTable>(dt)[0];
            }
            return fam;
        }

        /// <summary>
        /// 返回Sys_rolesTable实体类的ArrayList对象 (Sys_roles)
        /// </summary>
        /// <param name="qp">查询类(非安全函数,传入参数请进行Sql字符串过滤)</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>Sys_rolesTable实体类的ArrayList对象(Sys_roles)</returns>
        public DataTable Sys_rolesList(QueryParam qp, out int RecordCount)
        {
            DataTable dt = new DataTable();
            qp.TableName = "Sys_roles";
            qp.ReturnFields = "*";
            if (qp.OrderId == null)
            {
                qp.OrderId = "Roleid";
            }

            DataSet ds= objDBcon.Sys_rolesList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        #endregion

        #region "sys_RolePermission - Method"

        /// <summary>
        /// 新增/删除/修改 sys_RolePermission
        /// </summary>
        /// <param name="fam">sys_RolePermissionTable实体类</param>
        /// <returns>返回0操正常</returns>
        public int sys_RolePermissionInsertUpdate(sys_RolePermissionTable fam)
        {
            return objDBcon.sys_RolePermissionInsertUpdate(fam);
        }

        /// <summary>
        /// 返回sys_RolePermissionTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_RolePermissionTable实体类的ArrayList对象</returns>
        public DataTable sys_RolePermissionList(QueryParam qp, out int RecordCount)
        {
            DataTable dt = new DataTable();
            qp.TableName = "Sys_rolepermission";
            qp.ReturnFields = "*";
            if (qp.OrderId == null)
            {
                qp.OrderId = "Pid";
            }
            DataSet ds= objDBcon.sys_RolePermissionList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 获取角色应用权限资料
        /// </summary>
        /// <param name="RoleID">角色ID</param>
        /// <param name="A_moduleid">模块ID</param>
        /// <param name="PageCode">PageCode</param>
        /// <returns></returns>
        public sys_RolePermissionTable sys_RolePermissionDisp(int RoleID, int A_moduleid, string PageCode)
        {
            sys_RolePermissionTable s_Rp = new sys_RolePermissionTable();

            QueryParam qp = new QueryParam();
            qp.Where = string.Format(" P_roleid= {0} and P_moduleid={1} and P_pagecode='{2}'", RoleID, A_moduleid, PageCode);
            int RecordCount = 0;
            DataTable dt = sys_RolePermissionList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                s_Rp = DT2EntityList<sys_RolePermissionTable>(dt)[0];
            }
            return s_Rp;
        }

        /// <summary>
        /// 删除根据角色ID, 模块ID,角色权限记录
        /// </summary>
        /// <param name="RoleID">角色Id</param>
        /// <param name="ModuleID">模块ID</param>
        public void sys_RolePermission_Move(int RoleID, int ModuleID)
        {
            int RecordCount = 0;
            QueryParam qp = new QueryParam();
            qp.Where = string.Format("  P_roleid={0} and P_moduleid = {1}", RoleID, ModuleID);
            DataTable dt = sys_RolePermissionList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                List<sys_RolePermissionTable> lst = DT2EntityList<sys_RolePermissionTable>(dt);

                for (int i = 0; i < lst.Count; i++)
                {
                    sys_RolePermissionTable var = lst[i];

                    var.DB_Option_Action_ = "Delete";
                    sys_RolePermissionInsertUpdate(var);
                }
            }
        }

        #endregion

        #region "sys_Online - Method"

        /// <summary>
        /// 新增/删除/修改 sys_Online
        /// </summary>
        /// <param name="fam">sys_OnlineTable实体类</param>
        /// <returns>返回0操正常</returns>
        public int sys_OnlineInsertUpdate(sys_OnlineTable fam)
        {
            return objDBcon.sys_OnlineInsertUpdate(fam);
        }

        /// <summary>
        /// 返回sys_OnlineTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_OnlineTable实体类的ArrayList对象</returns>
        public DataTable sys_OnlineList(QueryParam qp, out int RecordCount)
        {
            DataTable dt = new DataTable();
            qp.TableName = "sys_Online";
            qp.ReturnFields = "*";
            if (qp.OrderId == null)
            {
                qp.OrderId = "OnlineID";
            }

            DataSet ds = objDBcon.sys_OnlineList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        /// <summary>
        /// 根据ID返回 sys_OnlineTable实体类 单笔资料
        /// </summary>
        /// <param name="OnlineID">自动ID</param>
        /// <returns>返回sys_OnlineTable实体类 OnlineID为0则无记录</returns>
        public sys_OnlineTable sys_OnlineDisp(int OnlineID)
        {
            sys_OnlineTable fam = new sys_OnlineTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = "   sys_Online.OnlineID = " + OnlineID;
            int RecordCount = 0;
            DataTable dt = sys_OnlineList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = DT2EntityList<sys_OnlineTable>(dt)[0];
            }
            return fam;
        }

        /// <summary>
        /// 根据UserID返回 sys_OnlineTable实体类 单笔资料
        /// </summary>
        /// <param name="sessionid">用户sessionID</param>
        /// <returns>返回sys_OnlineTable实体类 OnlineID为0则无记录</returns>
        public sys_OnlineTable sys_OnlineDispSessionID(string sessionid)
        {
            sys_OnlineTable fam = new sys_OnlineTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = "   sys_Online.O_SessionID = '" + config.CheckChar(sessionid) + "'";
            int RecordCount = 0;
            DataTable dt = sys_OnlineList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = DT2EntityList<sys_OnlineTable>(dt)[0];
            }
            return fam;
        }

        /// <summary>
        /// 根据用户名读取在线用户
        /// </summary>
        /// <param name="O_UserName">用户名</param>
        /// <returns>返回sys_OnlineTable实例</returns>
        public sys_OnlineTable sys_OnlineDisp(string O_UserName)
        {
            sys_OnlineTable Online = new sys_OnlineTable();
            QueryParam qp = new QueryParam();
            qp.Where = string.Format(" O_UserName='{0}'", config.CheckChar(O_UserName));
            qp.PageIndex = 1;
            qp.PageSize = 1;
            int rInt = 0;
            DataTable dt = sys_OnlineList(qp, out rInt);
            if (rInt > 0)
            {
                Online = DT2EntityList<sys_OnlineTable>(dt)[0];
            }
            return Online;
        }


        /// <summary>
        /// 根据用户名读取在线用户
        /// </summary>
        /// <param name="O_UserName">用户名</param>
        /// <param name="sessionid">用户SessionID</param>
        /// <returns>返回sys_OnlineTable实例</returns>
        public sys_OnlineTable sys_OnlineDisp(string O_UserName, string sessionid)
        {
            sys_OnlineTable Online = new sys_OnlineTable();
            QueryParam qp = new QueryParam();
            qp.Where = string.Format("  O_SessionID='{0}' and O_UserName='{1}'", sessionid, config.CheckChar(O_UserName));
            qp.PageIndex = 1;
            qp.PageSize = 1;
            int rInt = 0;
            DataTable dt = sys_OnlineList(qp, out rInt);
            if (rInt == 1)
            {
                Online = DT2EntityList<sys_OnlineTable>(dt)[0];
            }
            return Online;
        }

        /// <summary>
        /// 检测用户sessionid是否在线
        /// </summary>
        /// <param name="sessionid">用户sessionid</param>
        /// <returns>true/false</returns>
        public bool CheckSessionIDOnline(string sessionid)
        {
            if (sys_OnlineDispSessionID(sessionid).OnlineID == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 检测用户名是否在线
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public bool CheckMemberOnline(string username)
        {
            if (sys_OnlineDisp(username).OnlineID == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 访问方法
        /// </summary>
        /// <param name="sessionid">用户SessionID</param>
        public void AccessMemberOnline(string sessionid)
        {
            sys_OnlineTable online = sys_OnlineDispSessionID(sessionid);
            online.O_LastTime = DateTime.Now;
            online.O_LastUrl = config.GetScriptUrl;
            online.DB_Option_Action_ = "Update";
            sys_OnlineInsertUpdate(online);
        }

        /// <summary>
        /// 删除在线用户
        /// </summary>
        /// <param name="sessionid">用户SessionID</param>
        public void RemoveMemberOnline(string sessionid)
        {
            sys_OnlineTable online = sys_OnlineDispSessionID(sessionid);
            online.DB_Option_Action_ = "Delete";
            sys_OnlineInsertUpdate(online);
        }

        /// <summary>
        /// 插入在线用户表
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="sessionid">用户sessionID</param>
        public void InsertMemberOnline(string username, string sessionid)
        {
            sys_OnlineTable online = new sys_OnlineTable();
            online.DB_Option_Action_ = "Insert";
            online.O_Ip = config.GetUserIP();
            online.O_LastTime = DateTime.Now;
            online.O_LastUrl = config.GetScriptUrl;
            online.O_LoginTime = online.O_LastTime;
            online.O_SessionID = sessionid;
            online.O_UserName = username;
            sys_OnlineInsertUpdate(online);
        }


        #endregion

        #region "Sys_Scope(Sys_Scope) - Method"

        /// <summary>
        /// 新增/删除/修改 Sys_ScopeTable (Sys_Scope)
        /// </summary>
        /// <param name="fam">Sys_ScopeTable实体类</param>
        /// <returns>-1:存储过程执行失败,-2:存在相同的主键,Insert:返回插入自动ID,Update:返回更新记录数,Delete:返回删除记录数</returns>
        public int Sys_ScopeInsertUpdateDelete(Sys_ScopeTable fam)
        {
            return objDBcon.Sys_ScopeInsertUpdateDelete(fam);
        }

        /// <summary>
        /// 根据ID返回 Sys_ScopeTable实体类 单笔资料 (Sys_Scope)
        /// </summary>
        /// <param name="ID">ID ID 递增</param>
        /// <returns>返回 Sys_ScopeTable实体类 ID为0则无记录</returns>
        public Sys_ScopeTable Sys_ScopeDisp(string ID)
        {
            Sys_ScopeTable fam = new Sys_ScopeTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = string.Format("   {0}.{1} = '{2}' ", "Sys_Scope", "ScopeID", ID);
            int RecordCount = 0;
            DataTable dt = Sys_ScopeList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = DT2EntityList<Sys_ScopeTable>(dt)[0];
            }
            return fam;
        }

        /// <summary>
        /// 返回Sys_ScopeTable实体类的ArrayList对象 (Sys_Scope)
        /// </summary>
        /// <param name="qp">查询类(非安全函数,传入参数请进行Sql字符串过滤)</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>Sys_ScopeTable实体类的ArrayList对象(Sys_Scope)</returns>
        public DataTable Sys_ScopeList(QueryParam qp, out int RecordCount)
        {
            DataTable dt = new DataTable();
            qp.TableName = "Sys_Scope";
            if (qp.OrderId == null)
            {
                qp.OrderId = "ScopeID";
            }
            else if (qp.OrderId != "ScopeID")
            {
                qp.OrderId += ",ScopeID";
            }

            if (qp.ReturnFields == null)
            {
                qp.ReturnFields = "*";
            }

            DataSet ds= objDBcon.Sys_ScopeList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        #endregion

        #region "Sys_Menu(Sys_Menu) - Method"

        /// <summary>
        /// 新增/删除/修改 Sys_MenuTable (Sys_Menu)
        /// </summary>
        /// <param name="fam">Sys_MenuTable实体类</param>
        /// <returns>-1:存储过程执行失败,-2:存在相同的主键,Insert:返回插入自动ID,Update:返回更新记录数,Delete:返回删除记录数</returns>
        public   Int32 Sys_MenuInsertUpdateDelete(Sys_MenuTable fam)
        {
            return objDBcon.Sys_MenuInsertUpdateDelete(fam);
        }

        /// <summary>
        /// 根据id返回 Sys_MenuTable实体类 单笔资料 (Sys_Menu)
        /// </summary>
        /// <param name="id">id 自动增长ID</param>
        /// <returns>返回 Sys_MenuTable实体类 id为0则无记录</returns>
        public   Sys_MenuTable Sys_MenuDisp(Int32 id)
        {
            Sys_MenuTable fam = new Sys_MenuTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = string.Format("   {0}.{1} = {2}", "Sys_Menu", "id", id);
            int RecordCount = 0;
            DataTable dt = Sys_MenuList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = DT2EntityList<Sys_MenuTable>(dt)[0];
            }
            return fam;
        }

        /// <summary>
        /// 返回Sys_MenuTable实体类的List对象 (Sys_Menu)
        /// </summary>
        /// <param name="qp">查询类(非安全函数,传入参数请进行Sql字符串过滤)</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>Sys_MenuTable实体类的List对象(Sys_Menu)</returns>
        public DataTable Sys_MenuList(QueryParam qp, out int RecordCount)
        {
            DataTable dt = new DataTable();
            qp.TableName = "Sys_Menu";
            if (qp.OrderId == null)
            {
                qp.OrderId = "id";
            }
            else if (qp.OrderId != "id")
            {
                qp.OrderId += ",id";
            }

            if (qp.ReturnFields == null)
            {
                qp.ReturnFields = "*";
            }
            else
            {
                qp.ReturnFields += ",";
                qp.ReturnFields += qp.OrderId;
            }
            DataSet ds= objDBcon.Sys_MenuList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        #endregion


        #region "移除用户无权限菜单项"
        /// <summary>
        /// 移除用户无权限菜单项
        /// </summary>
        /// <param name="lst"></param>
        public void Remove_MenuNoPermission(DataTable dt)
        {
            int iCount = dt.Rows.Count;
            for (int i = iCount - 1; i >= 0; i--)
            {
                if (!new UserData().CheckPageCode(config.Get_UserName, int.Parse(dt.Rows[i]["A_moduleid"].ToString()), dt.Rows[i]["A_pagecode"].ToString()))
                {
                    dt.Rows.RemoveAt(i);
                }
            }
        }
        #endregion

        #region "检测权限"
        /// <summary>
        /// 检测权限
        /// </summary>
        /// <param name="PT"></param>
        /// <returns></returns>
        public bool CheckButtonPermission(PopedomType PT)
        {
            Sys_applicationsTable Pis = sys_applUrlDisp();
            return new UserData().CheckPageCode(config.Get_UserName, Pis.A_moduleid, Pis.A_pagecode, (int)PT);

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
            return objDBcon.Update_Table_Fileds(Table, Table_FiledsValue, Wheres);
        }

        #endregion

        //--------------End-----------------------//

        #region "PE_NewsList - Method"

        /// <summary>
        /// 新增/删除/修改 PE_NewsListTable (PE_NewsList)
        /// </summary>
        /// <param name="fam">PE_NewsListTable实体类</param>
        /// <returns>-1:存储过程执行失败,-2:存在相同的主键,Insert:返回插入自动ID,Update:返回更新记录数,Delete:返回删除记录数</returns>
        public int PE_NewsListInsertUpdateDelete(PE_NewsListTable fam)
        {
            return objDBcon.PE_NewsListInsertUpdateDelete(fam);
        }

        /// <summary>
        /// 根据id返回 PE_NewsListTable实体类 单笔资料 (PE_NewsList)
        /// </summary>
        /// <param name="id">id </param>
        /// <returns>返回 PE_NewsListTable实体类 id为0则无记录</returns>
        public PE_NewsListTable PE_NewsListDisp(int id)
        {
            PE_NewsListTable fam = new PE_NewsListTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = string.Format("  {0}.{1} = {2}", "PE_NewsList", "NewsID", id);
            int RecordCount = 0;
            DataTable dt = PE_NewsList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = DT2EntityList<PE_NewsListTable>(dt)[0];
            }
            return fam;
        }

        /// <summary>
        /// 根据id返回 PE_NewsListTable实体类 单笔资料 (PE_NewsList)
        /// </summary>
        /// <param name="id">id </param>
        /// <returns>返回 PE_NewsListTable实体类 id为0则无记录</returns>
        public PE_NewsListTable PE_NewsListDisp(string N_NewId)
        {
            PE_NewsListTable fam = new PE_NewsListTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = string.Format("  {0}.{1} = '{2}'", "PE_NewsList", "N_NewID", N_NewId);
            int RecordCount = 0;
            DataTable dt = PE_NewsList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = DT2EntityList<PE_NewsListTable>(dt)[0];
            }
            return fam;
        }

        /// <summary>
        /// 根据newsId获取PE_NewsListTable实体类
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <param name="categoryId">类型ID</param>
        /// <param name="oprateValue">First为前一篇Last为下一篇</param>
        /// <returns>PE_NewsListTable实体类</returns>
        public PE_NewsListTable GetPE_NewsListByID(int newsId, int categoryId, string oprateValue)
        {
            PE_NewsListTable sat = objDBcon.GetPE_NewsListByID(newsId, categoryId, oprateValue);
            return sat;
        }

        /// <summary>
        /// 根据categoryID返回 ArrayList
        /// </summary>
        /// <param name="id">id </param>
        /// <returns>返回 ArrayList id为0则无记录</returns>
        public DataTable PE_NewsListDisp(QueryParam qp, out int recordCount, int id)
        {
            qp.Order = " order by NewsID desc";
            qp.Where = string.Format("  {0}.{1} = {2}", "PE_NewsList", "CategoryID", id);
            DataTable lst = PE_NewsList(qp, out recordCount);

            return lst;
        }

        /// <summary>
        /// 返回PE_NewsListTable实体类的ArrayList对象 (PE_NewsList)
        /// </summary>
        /// <param name="qp">查询类(非安全函数,传入参数请进行Sql字符串过滤)</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>PE_NewsListTable实体类的ArrayList对象(PE_NewsList)</returns>
        public DataTable PE_NewsList(QueryParam qp, out int RecordCount)
        {
            DataTable dt = new DataTable();
            qp.TableName = "PE_NewsList";
            if (qp.OrderId == null)
            {
                qp.OrderId = "NewsID";
            }
            else if (qp.OrderId != "NewsID")
            {
                qp.OrderId += ",NewsID";
            }

            if (qp.ReturnFields == null)
            {
                qp.ReturnFields = "*";
            }
            else
            {
                qp.ReturnFields += ",";
                qp.ReturnFields += qp.OrderId;
            }
            DataSet ds= objDBcon.PE_NewsList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 返回PE_NewsListTable实体类的ArrayList对象 (PE_NewsList)
        /// </summary>
        /// <param name="qp">查询类(非安全函数,传入参数请进行Sql字符串过滤)</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>PE_NewsListTable实体类的ArrayList对象(PE_NewsList)</returns>
        public DataTable PE_NewsList2(QueryParam qp, out int RecordCount)
        {
            DataTable dt = new DataTable();
            qp.TableName = " PE_NewsList n inner join Sys_Scope s on n.AreaID=s.ScopeID ";
            if (qp.OrderId == null)
            {
                qp.OrderId = "NewsID";
            }
            else if (qp.OrderId != "NewsID")
            {
                qp.OrderId += ",NewsID";
            }

            if (qp.ReturnFields == null)
            {
                qp.ReturnFields = " *";
            }
            else
            {
                qp.ReturnFields += ",";
                qp.ReturnFields += qp.OrderId;
            }

            DataSet ds= objDBcon.PE_NewsList2(qp, out RecordCount);
            if (RecordCount > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        public DataTable PE_NewsList(QueryParam qp)
        {
            DataTable dt = new DataTable();
            if (qp.OrderId == null)
            {
                qp.OrderId = "NewsID";
            }
            else if (qp.OrderId != "NewsID")
            {
                qp.OrderId += ",NewsID";
            }
            int RecordCount = 0;
            DataSet ds = objDBcon.PE_NewsList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 获取新闻表，用于绑定主页控件，返回三个字段,NewsID,Title,CategoryID
        /// </summary>
        /// <param name="qp"></param>
        /// <returns></returns>
        public DataTable GetPE_NewsList(QueryParam qp)
        {
            DataTable dt = new DataTable();
            qp.TableName = "PE_NewsList";
            qp.ReturnFields = " NewsID,Title,CategoryID,PublishTime,AreaID,Remark,N_NewID,Urls,('webUI/news/NewsInfo.aspx?NewsID='+CONVERT(varchar(200),N_NewID)) TitleUrls ";
            qp.Order = " order by NewsID desc";
            if (qp.OrderId == null)
            {
                qp.OrderId = "NewsID";
            }
            else if (qp.OrderId != "NewsID")
            {
                qp.OrderId += ",NewsID";
            }
            int RecordCount = 0;
            DataSet ds= objDBcon.PE_NewsList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        #endregion

        #region "PE_NewsCategory - Method"

        /// <summary>
        /// 新增/删除/修改 PE_NewsCategoryTable (PE_NewsCategory)
        /// </summary>
        /// <param name="fam">PE_NewsCategoryTable实体类</param>
        /// <returns>-1:存储过程执行失败,-2:存在相同的主键,Insert:返回插入自动ID,Update:返回更新记录数,Delete:返回删除记录数</returns>
        public int PE_NewsCategoryInsertUpdateDelete(PE_NewsCategoryTable fam)
        {
            return objDBcon.PE_NewsCategoryInsertUpdateDelete(fam);
        }

        /// <summary>
        /// 根据id返回 PE_NewsCategoryTable实体类 单笔资料 (PE_NewsCategory)
        /// </summary>
        /// <param name="id">id </param>
        /// <returns>返回 PE_NewsCategoryTable实体类 id为0则无记录</returns>
        public PE_NewsCategoryTable PE_NewsCategoryDisp(int id)
        {
            PE_NewsCategoryTable fam = new PE_NewsCategoryTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = string.Format("  {0}.{1} = {2}", "PE_NewsCategory", "PCID", id);
            int RecordCount = 0;
            DataTable dt = PE_NewsCategoryList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = DT2EntityList<PE_NewsCategoryTable>(dt)[0];
            }
            return fam;
        }

        /// <summary>
        /// 返回PE_NewsCategoryTable实体类的ArrayList对象 (PE_NewsCategory)
        /// </summary>
        /// <param name="qp">查询类(非安全函数,传入参数请进行Sql字符串过滤)</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>PE_NewsCategoryTable实体类的ArrayList对象(PE_NewsCategory)</returns>
        public DataTable PE_NewsCategoryList(QueryParam qp, out int RecordCount)
        {
            DataTable dt = new DataTable();
            qp.TableName = "PE_NewsCategory";
            if (qp.OrderId == null)
            {
                qp.OrderId = "PCID";
            }
            else if (qp.OrderId != "PCID")
            {
                qp.OrderId += ",PCID";
            }

            if (qp.ReturnFields == null)
            {
                qp.ReturnFields = "*";
            }
            else
            {
                qp.ReturnFields += ",";
                qp.ReturnFields += qp.OrderId;
            }
            DataSet ds= objDBcon.PE_NewsCategoryList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        #region  设置树列表

        /// <summary>
        /// 绑定树视图
        /// </summary>
        /// <param name="dt">数据源</param>
        public void BindTreeView(System.Web.UI.WebControls.DropDownList ddlCategorys)
        {
            DataTable dt = objDBcon.PE_NewsCategoryTable();

            ddlCategorys.Items.Clear();
            ddlCategorys.Items.Add(new ListItem("全部栏目", "0"));
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] nodeList = dt.Select("ParentID=0");
                for (int i = 0; i < nodeList.Length; i++)
                {
                    ddlCategorys.Items.Add(new ListItem(nodeList[i]["CategoryName"].ToString(), nodeList[i]["PCID"].ToString()));

                    BindChildNode(ddlCategorys, dt, nodeList[i]["PCID"].ToString());
                }
            }
            //if (Scope_Tree.LastLeve())
            //{
            //    ddlCategorys.Enabled = false;
            //    ddlCategorys.SelectedValue = "5";
            //}
        }

        /// <summary>
        /// 绑定子节点
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="parentValue">父值</param>
        private void BindChildNode(DropDownList ddlCategorys, DataTable dt, string parentValue)
        {
            DataRow[] nodeList = dt.Select(string.Format("{0}={1}", "ParentID", parentValue));
            for (int i = 0; i < nodeList.Length; i++)
            {
                //分隔符
                string strBlank = StringOfChar(int.Parse(nodeList[i]["Level"].ToString()), "&nbsp;&nbsp;");
                ddlCategorys.Items.Add(new ListItem(HttpUtility.HtmlDecode(strBlank) + nodeList[i]["CategoryName"].ToString(), nodeList[i]["PCID"].ToString()));
                //递归绑定其余子节点
                BindChildNode(ddlCategorys, dt, nodeList[i]["PCID"].ToString());
            }
        }

        /// <summary>
        /// 设置节点的分隔符
        /// </summary>
        /// <param name="strLong">树的深度</param>
        /// <param name="str">分隔符号</param>
        /// <returns></returns>
        private string StringOfChar(int strLong, string str)
        {
            string returnStr = string.Empty;
            if (strLong > 1)
            {
                for (int i = 0; i < strLong; i++)
                {
                    returnStr += str;
                }
                returnStr += "|-- ";
            }
            return returnStr;
        }
        #endregion

        #endregion

    }
   




}


