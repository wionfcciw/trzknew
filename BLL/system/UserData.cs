using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Model;
using System.Data;

namespace BLL
{
    /// <summary>
    /// 用户数据存储类
    /// </summary>
    public class UserData
    {
        #region "用户资料"

        /// <summary>
        /// 根据用户管辖范围 返回用户列表资料
        /// </summary> 
        public DataTable UserArrayList(QueryParam qp, out int RecordCount)
        { 
            return new Method().sys_UserList(qp, out RecordCount);
        }


        /// <summary>
        /// 返回单位名称
        /// </summary>
        /// <param name="UserName">考场代码 用户名</param>
        /// <returns></returns>
        public string get_UserName(string UserName)
        {

            return Get_sys_UserTable(UserName).U_xm;

        }

        /// <summary>
        /// 根据用户UserName获取用户资料 
        /// </summary>
        /// <param name="UserID">用户UserName</param>
        /// <returns></returns>
        public sys_UserTable Get_sys_UserTable(string UserName)
        {
            sys_UserTable sUT = new Method().sys_UserDisp(UserName);
            return sUT;
        }



        /// <summary>
        /// 根据用户ID,模块ID,PageCode,要检测权限数值
        /// </summary>
        /// <param name="UserID">用户名</param>
        /// <param name="P_ModuleID">模块ID</param>
        /// <param name="P_PageCode">PageCode</param>
        /// <param name="CheckPermissionValue">权限值</param>
        /// <returns></returns>
        public bool CheckPageCode(string UserName, int P_moduleid, string P_PageCode, int CheckPermissionValue)
        {
            if (SincciLogin.Sessionstu().UserType == 1)//判断用户是否为超级用户
                return true;
            bool bBool = false;

            //Hashtable UserPermission = Get_UserPermission(UserName);
            //if (UserPermission.Count > 0)
            //{
            //    string Key = string.Format("{0}-{1}", P_moduleid, P_PageCode);
            //    if (UserPermission.ContainsKey(Key))
            //    {
            //        if ((((sys_RolePermissionTable)UserPermission[Key]).P_Value & CheckPermissionValue) == CheckPermissionValue)
            //        {
            //            bBool = true;
            //        }
            //    }
            //}

            string sql = "select P_value from dbo.Sys_userroles as A,Sys_rolepermission as b,dbo.Sys_applications as C ";
            sql += " where A.R_roleid=b.P_roleid  and b.P_moduleid=C.A_moduleid and b.P_pagecode=C.A_pagecode and ";
            sql += string.Format("  A.R_UserName='{0}' and P_moduleid='{1}' and P_pagecode='{2}' ", UserName, P_moduleid, P_PageCode);
            string error = "";
            bool bReturn = false;
            DataTable dt = new DAL.SqlDbHelper_1().selectTab(sql, ref error, ref bReturn);
            if (dt.Rows.Count > 0)
            {
                int P_Value = int.Parse(dt.Rows[0]["P_value"].ToString());
                if ((P_Value & CheckPermissionValue) == CheckPermissionValue)
                {
                    bBool = true;
                }
            }


            return bBool;
        }
        //private static Cache _UserPermissionCache = HttpRuntime.Cache;
        /// <summary>
        /// 根据用户ID,模块ID,PageCode判断用户是否拥有当前权限
        /// </summary>
        /// <param name="UserID">用户名</param>
        /// <param name="P_moduleid">模块ID</param>
        /// <param name="P_PageCode">PageCode</param>
        /// <returns></returns>
        public bool CheckPageCode(string UserName, int P_moduleid, string P_PageCode)
        {
            if (SincciLogin.Sessionstu().UserType == 1) //判断用户是否为超级用户
                return true;
            bool bBool = false;


            //Hashtable UserPermission = Get_UserPermission(UserName);
            //if (UserPermission.Count > 0)
            //{
            //    string Key = string.Format("{0}-{1}", P_moduleid, P_PageCode);
            //    if (UserPermission.ContainsKey(Key))
            //    {
            //        bBool = true;
            //    }
            //}

            string sql = "select * from dbo.Sys_userroles as A,Sys_rolepermission as b,dbo.Sys_applications as C ";
            sql += " where A.R_roleid=b.P_roleid  and b.P_moduleid=C.A_moduleid and b.P_pagecode=C.A_pagecode and ";
            sql += string.Format("  A.R_UserName='{0}' and P_moduleid='{1}' and P_pagecode='{2}' ", UserName, P_moduleid, P_PageCode);
            string error = "";
            bool bReturn = false;
            DataSet ds = new DAL.SqlDbHelper_1().selectDataSet(sql, ref error, ref bReturn);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    bBool = true;
                }
            }
            return bBool;
        }

        /// <summary>
        /// 获取用户权限Hashtable
        /// </summary>
        /// <param name="UserName">用户UserName</param>
        /// <returns></returns>
        private static Hashtable Get_UserPermission(string UserName)
        {
            Hashtable _Temp = Get_sys_RolePermissionTable(UserName);
            return _Temp;
        }

        /// <summary>
        /// 根据用户ID,获取用户模块权限列表
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        private static Hashtable Get_sys_RolePermissionTable(string UserName)
        {
            Hashtable PageCodeList = new Hashtable();
            List<sys_RolePermissionTable> List = new List<sys_RolePermissionTable>();

          
                QueryParam qp = new QueryParam();
                qp.Where = string.Format(" R_UserName='{0}'", UserName);
                int RecordCount = 0;
                DataTable lst = new Method().sys_UserRolesList(qp, out RecordCount);

                for (int i = 0; i < List.Count; i++)
                {
                    string Key = string.Format("{0}-{1}", List[i].P_moduleid, List[i].P_PageCode);
                    if (PageCodeList.ContainsKey(Key))
                    {
                        sys_RolePermissionTable Rpt = (sys_RolePermissionTable)PageCodeList[Key];
                        if (Rpt.P_Value != List[i].P_Value)
                        {
                            //PageCodeList[Key] = List[i];
                            Rpt.P_Value = Rpt.P_Value | List[i].P_Value;
                        }
                    }
                    else
                    {
                        PageCodeList.Add(Key, List[i]);
                    }
                }

            return PageCodeList;
        }

        /// <summary>
        /// 根据用户角色ID,获取权限列表
        /// </summary>
        /// <param name="RoleID">角色ID</param>
        /// <param name="List">权限列表</param>
        private static void Get_RolesPermission(int RoleID, List<sys_RolePermissionTable> List)
        {
            QueryParam qp = new QueryParam();
            qp.Where = string.Format(" P_RoleID={0}", RoleID);
            int RecordCount = 0;
            DataTable lst = new Method().sys_RolePermissionList(qp, out RecordCount);

        }
        #endregion

        #region "获取当前登录用户信息"
        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        public   sys_UserTable GetUserDate
        {
            get
            {
                return Get_sys_UserTable(config.Get_UserName);
            }
        }
        #endregion

    }
}
