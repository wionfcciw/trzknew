using System;
using System.Collections.Generic;
 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using BLL;
using Model;

namespace SincciWeb.system
{
    public partial class RolePermissionSet : BPage
    {
        #region "Page_Load"
        int Roleid =Convert.ToInt32(config.sink("Roleid", config.MethodType.Get, 255, 1, config.DataType.Int));
        string R_name = Convert.ToString(config.sink("R_name", config.MethodType.Get, 255, 1, config.DataType.Str));
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Permission();

                if (Roleid > 0)
                {
                    RoleModule(Roleid);
                    binData();                 
                    this.lblRoleName.Text = R_name; 
                }  
            }
        }
        #endregion

        #region "判断页面权限"
        /// <summary>
        /// 页面权限
        /// </summary>
        private void Permission()
        {
            ////查看
            //if (!new Method().CheckButtonPermission(PopedomType.List))
            //{
            //    Response.Write("你没有页面查看的权限！");
            //    Response.End();
            //}
        }
        #endregion

        
        #region "绑定数据"
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void binData()
        {
            int Moduleid = Convert.ToInt32(this.ddlModule.SelectedValue);
            if (Moduleid > 0)
            {
                QueryParam qp = new QueryParam();
                qp.Where = string.Format(" A_moduleid={0} and A_tag={1} ", Moduleid,(int)Tag.Open);
                qp.Order = " order by A_order asc";
                int RecordCount = 0;
                //this.Application_Sub.DataSource = new Method().sys_applicationsList(qp, out RecordCount);
                //this.Application_Sub.DataBind();

                DataTable dt = new Method().sys_applicationsList(qp, out RecordCount);
                if (RecordCount > 0)
                {
                    List<Sys_applicationsTable> list = new Method().DT2EntityList<Sys_applicationsTable>(dt);

                    this.Application_Sub.DataSource = list;
                    this.Application_Sub.DataBind();
                }

            }

        } 

        /// <summary>
        /// 绑定角色模块
        /// </summary>
        /// <param name="qp"></param>
        private void RoleModule(int Roleid)
        {
            QueryParam qp = new QueryParam();
            qp.Order = " order by A_moduleid asc";
            qp.OrderId = " A_moduleid ";
            qp.Where = string.Format(" A_roleid={0} ", Roleid);
            int RecordCount = 0;
            DataTable dt = new Method().sys_RoleModuleList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                SortedList sl = new SortedList();

                List<sys_RoleModuleTable> lst = new Method().DT2EntityList<sys_RoleModuleTable>(dt);

                for (int i = 0; i < lst.Count; i++)
                {
                    sys_RoleModuleTable var = lst[i];

                    Sys_moduleTable mt = new Sys_moduleTable();
                    mt = new Method().sys_ModuleDisp(var.A_moduleid);
                    if (mt.M_modulename.Length > 0)
                    {
                        sl.Add(mt.M_order, mt.M_modulename + "|" + mt.Moduleid.ToString());
                    }

                }

                //排序 SortedList排序
                foreach (DictionaryEntry de in sl)
                {
                    string[] ar = de.Value.ToString().Split('|');
                    this.ddlModule.Items.Add(new ListItem(ar[0], ar[1]));
                }

            }
            if (this.ddlModule.Items.Count <= 0)
                this.ddlModule.Items.Add(new ListItem("--请先设置模块--", "0"));

            
        }

        #endregion 

        #region "绑定权限列表 Application_Sub_ItemDataBound 事件"
        /// <summary>
        /// 绑定权限列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Sub_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int Moduleid=Convert.ToInt32(this.ddlModule.SelectedValue);
            Sys_applicationsTable s_Mt = (Sys_applicationsTable)e.Item.DataItem;

            //获得当前设定角色权限
            sys_RolePermissionTable s_Rp = new Method().sys_RolePermissionDisp(Roleid, Moduleid, s_Mt.A_pagecode);

            DataList dlist = (DataList)e.Item.FindControl("PermissionList");
            if (dlist != null)
            {
                List<sys_PermissionItem> lst = GetPermissionList(s_Mt.A_pagecode);
                FormatPermission(lst, s_Mt.A_pagecode, s_Rp.P_Value);
                dlist.DataSource = lst;
                dlist.DataBind();
            }

        }
        #endregion

        #region "选择模块 ddlModule_SelectedIndexChanged事件 "
        /// <summary>
        /// 选择模块事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            binData();
        }
        #endregion

        #region "保存数据"
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int Moduleid = Convert.ToInt32(this.ddlModule.SelectedValue);

            new Method().sys_RolePermission_Move(Roleid, Moduleid);
            string TempPageCodeString = "";
            string[] ArrayInt;
            int PageCodeValue = 0;
            sys_RolePermissionTable s_Rt = new sys_RolePermissionTable();
            s_Rt.DB_Option_Action_ = "Insert";
            s_Rt.P_moduleid = Moduleid;
            s_Rt.P_RoleID = Roleid;
            foreach (string var in Request.Form)
            {
                if (var.Length > 8)
                {

                    TempPageCodeString = var.Substring(0, 8);
                    if (TempPageCodeString == "PageCode")
                    {
                        PageCodeValue = 0;
                        TempPageCodeString = var.Substring(8, var.Length - 8);
                        ArrayInt = Request.Form[var].Split(',');
                        for (int i = 0; i < ArrayInt.Length; i++)
                        {
                            //判断当前用户是否享有当前权限
                            if (new UserData().CheckPageCode(config.Get_UserName, Moduleid, TempPageCodeString, Convert.ToInt32(ArrayInt[i])))
                            {
                                PageCodeValue = PageCodeValue + Convert.ToInt32(ArrayInt[i]);
                            }
                        }
                        s_Rt.P_PageCode = TempPageCodeString;
                        s_Rt.P_Value = PageCodeValue;
                        new Method().sys_RolePermissionInsertUpdate(s_Rt);

                    }

                }

            }
            string E_record = "设置: 角色管理设置应用权限数据：" + s_Rt.P_moduleid + "";
            EventMessage.EventWriteDB(1, E_record);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.succeedInfo({message:'操作成功！' ,title:'操作提示'});setTimeout(function(){ window.ymPrompt.close();},1000);</script>");
            binData();
        }
        #endregion

        #region "获得应用的权限集合"
        /// <summary>
        /// 获得应用的权限集合
        /// </summary>
        /// <returns>返回应用权限列表</returns>
        public List<sys_PermissionItem> GetPermissionList(string PageCode)
        {
            int RecordCount=0;
            QueryParam qp=new QueryParam();
            qp.Order="order by PermissionValue";
            qp.PageIndex=1;
            qp.PageSize=int.MaxValue;
            qp.Where=string.Format(" PageCode='{0}'",PageCode);

            DataTable dt = new Method().Sys_PermissionList(qp, out RecordCount);

            List<sys_PermissionItem> lst = new List<sys_PermissionItem>();
            if (RecordCount > 0)
            {
                List<Sys_PermissionTable> lst2 = new Method().DT2EntityList<Sys_PermissionTable>(dt);

                for (int i = 0; i < lst2.Count; i++)
                {
                    Sys_PermissionTable p = lst2[i];
                    lst.Add(new sys_PermissionItem(p.PermissionName, p.PermissionValue, false));
                }

            }
            return lst;
        }

        /// <summary>
        /// 获得权限显示文本
        /// </summary>
        /// <param name="pagecode">模块Pagecode</param>
        /// <param name="permissionName">权限名称</param>
        /// <param name="permissionValue">权限值</param>
        /// <param name="allow">是否允许</param>
        /// <returns>显示文本</returns>
        public void FormatPermission(List<sys_PermissionItem> lst, string pagecode, int userpermissionValue)
        {
            string rightString = "";
            string wrongString = "";
            string dispString = "";
            string SelectTxt = "";

            foreach (sys_PermissionItem var in lst)
            {
                dispString = "";
                SelectTxt = "";
                var.Allow = (userpermissionValue & var.PermissionValue) == var.PermissionValue;

                if (var.Allow)
                {
                    dispString = rightString;
                    SelectTxt = "checked";
                }
                else
                {
                    dispString = wrongString;
                    SelectTxt = "";
                }

                dispString = string.Format("<input type=checkbox id='PageCode{0}' name='PageCode{0}' value='{1}'  {2} >{3}", pagecode, var.PermissionValue, SelectTxt, var.PermissionName);

                var.DispTxt = dispString;
            }
        }
        #endregion

    }
}