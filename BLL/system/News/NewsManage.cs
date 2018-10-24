using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using Model;
namespace BLL 
{
    /// <summary>
    /// 新闻管理业务类
    /// </summary>
    public class NewsManage
    {
      

        /// <summary>
        /// 显示新闻列表
        /// </summary> 
        public DataTable ShowNewsList(QueryParam qp, out int RecordCount)
        {
            

            string ScopeID = new UserData().Get_sys_UserTable(SincciLogin.Sessionstu().UserName).U_department;
            int S_ParentID = new Method().Sys_ScopeDisp(ScopeID).S_ParentID;
            if (S_ParentID != 0)
            {
                if (qp.Where == null)
                {
                    qp.Where = string.Format(" scopeid in({0}) ", ScopeID + Scope_Tree.allidString(ScopeID));
                }
                else
                {
                    qp.Where = qp.Where + string.Format(" and scopeid in({0}) ", ScopeID + Scope_Tree.allidString(ScopeID));
                }
            }
            qp.Order = " order by NewsID desc";
            return new Method().PE_NewsList(qp, out RecordCount);
        }
        /// <summary>
        /// 返回栏目名称
        /// </summary>
        /// <param name="CategoryID">栏目ID</param>
        /// <returns></returns>
        public static string ShowCategoryName(int CategoryID,string AreaID)
        {
            string CategoryName = new Method().PE_NewsCategoryDisp(CategoryID).CategoryName;
            string ScopeName= new Method().Sys_ScopeDisp(AreaID).S_Name;
            if (CategoryName == "区县工作")
            {
                CategoryName = CategoryName + "->" + ScopeName;
            }
            return CategoryName;
        }

    }
}
