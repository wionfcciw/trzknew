using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
 
using Model;
 
using System.Data;
using System.Web.UI.WebControls;
namespace BLL
{
    /// <summary>
    /// 系统范围 树业务类
    /// </summary>
    public class Scope_Tree
    {
        public static string returnvalue = "";//递归存储值

        /// <summary>
        /// 判断是不是管辖范围的最后一层
        /// </summary> 
        public static bool LastLeve()
        {
            string ScopeID = new UserData().Get_sys_UserTable(SincciLogin.Sessionstu().UserName).U_department;
            DataSet ds = returndataset(ScopeID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        } 

        #region "下拉框绑定数据 最底一层"
        //下拉框绑定数据 最底一层
        public static DropDownList dropdownlistbindlast(string parterid, DropDownList droplist)
        { 
            droplist = dropdownlistlast(parterid, droplist);  
            return droplist;
        } 
        private static DropDownList dropdownlistlast(string parterid, DropDownList droplist)
        {

            //递归类别
            DataSet ds = returndataset(parterid);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    DataSet ds2 = returndataset(ds.Tables[0].Rows[i]["ScopeID"].ToString());
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        dropdownlistlast(ds.Tables[0].Rows[i]["ScopeID"].ToString(), droplist);
                    }
                    else
                    {

                        string S_Depth = "";
                        ListItem list = new ListItem();
                        if (ds.Tables[0].Rows[i]["S_Depth"].ToString() == "0")
                        {
                           // S_Depth = "|";
                            list.Text = S_Depth + ds.Tables[0].Rows[i]["S_Name"].ToString(); // +"[" + ds.Tables[0].Rows[i]["S_Code"].ToString() + "]";
                        }
                        else
                        {

                            //for (int j = 1; j < Int32.Parse(ds.Tables[0].Rows[i]["S_Depth"].ToString()); j++)
                            //{
                            //    S_Depth += "　";

                            //}
                           // S_Depth += "|--";
                            list.Text = S_Depth + ds.Tables[0].Rows[i]["S_Name"].ToString(); // +"[" + ds.Tables[0].Rows[i]["S_Code"].ToString() + "]";
                        }
                        list.Value = ds.Tables[0].Rows[i]["ScopeID"].ToString();
                        droplist.Items.Add(list);
                    }

                }
            }

            return droplist;

        }
        #endregion


        //下拉框绑定数据
        public static  DropDownList dropdownlistbind(string parterid, DropDownList droplist)
        {
            //如果父ID不是0。

            droplist = dropdownlist(parterid, droplist);

            if (parterid.Length> 0)
            {
                droplist.Items.Insert(0, new ListItem(returnS_Name(parterid), parterid.ToString()));
            }

            return droplist;
        }

        private static  DropDownList dropdownlist(string parterid, DropDownList droplist)
        {

            //递归类别
            DataSet ds = returndataset(parterid);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string S_Depth = "";
                    ListItem list = new ListItem();
                    if (ds.Tables[0].Rows[i]["S_Depth"].ToString() == "0")
                    {
                        S_Depth = "|";
                        list.Text = S_Depth + ds.Tables[0].Rows[i]["S_Name"].ToString() + "[" + ds.Tables[0].Rows[i]["S_Code"].ToString() + "]";
                    }
                    else
                    {

                        for (int j = 1; j < Int32.Parse(ds.Tables[0].Rows[i]["S_Depth"].ToString()); j++)
                        {
                            S_Depth += "　";

                        }
                        S_Depth += "|--";
                        list.Text = S_Depth + ds.Tables[0].Rows[i]["S_Name"].ToString() + "[" + ds.Tables[0].Rows[i]["S_Code"].ToString() + "]";
                    }
                    list.Value = ds.Tables[0].Rows[i]["ScopeID"].ToString();
                    droplist.Items.Add(list);
                    DataSet ds2 = returndataset(ds.Tables[0].Rows[i]["ScopeID"].ToString());
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        dropdownlist(ds.Tables[0].Rows[i]["ScopeID"].ToString(), droplist);
                    }
                }
            } 

            return droplist;

        }

        public static void treeshow(string parterid, TreeNode TreeNode)
        {
            //递归显示树
           
            DataSet ds = returndataset(parterid);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    TreeNode node = new TreeNode();
                    node.Target = ds.Tables[0].Rows[i]["ScopeID"].ToString();
                    node.Text = ds.Tables[0].Rows[i]["S_Name"].ToString() + "[" + ds.Tables[0].Rows[i]["S_Code"].ToString() + "]";
                    TreeNode.ChildNodes.Add(node);

                    node.Expanded = true;
                    DataSet ds2 = returndataset(ds.Tables[0].Rows[i]["ScopeID"].ToString());
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        treeshow(ds.Tables[0].Rows[i]["ScopeID"].ToString(), node);
                    }
                }
            }
           
        }

        /// <summary>
        /// 查询出子级数据
        /// </summary> 
        private static DataSet returndataset(string parterid)
        {
            //查询出数据

            DataSet ds = new DataSet();

            ds = new DBcon().DataAdapterSearch("select *  from Sys_Scope where S_ParentID='" + parterid + "' order by ScopeID", "t");

            return ds;
        }
        /// <summary>
        /// 查询出父级数据
        /// </summary> 
        private static DataSet returndatasetParent(int ScopeID)
        {
            //查询出数据

            DataSet ds = new DataSet();

            ds = new DBcon().DataAdapterSearch("select *  from Sys_Scope where ScopeID='" + ScopeID + "' ", "t");

            return ds;
        }


        private static string returnS_Name(string ID)
        {
            //查询出数据

            return new Method().Sys_ScopeDisp(ID).S_Name;
        }


        /// <summary>
        /// 返回字符串格式：1,2,4
        /// </summary> 
        public static string allidString(string ParentID)
        {
            return allidLevel(ParentID, 0);

        }

       /// <summary>
        /// 遍历treeview节点(递归算法)格式：1,2,4
       /// </summary>
       /// <param name="ParentID">父级ID</param>
        /// <param name="level">递归到第几层</param>
       /// <returns></returns>
        private static string allidLevel(string ParentID, int level)
        {
            if (level == 0)
                returnvalue = ""; 
           
            DataSet ds = returndataset(ParentID);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                { 
                    returnvalue += "," + ds.Tables[0].Rows[i]["ScopeID"].ToString(); 
                    DataSet ds2 = returndataset(ds.Tables[0].Rows[i]["ScopeID"].ToString());
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        allidLevel(ds.Tables[0].Rows[i]["ScopeID"].ToString(), level + 1);

                    }
                }
            } 

            return returnvalue;
        }



        //public static  DropDownList ddlReply(int ScopeID, DropDownList droplist)
        //{
        //    string scopeid = "0";
        //    if (allParentString(ScopeID).Length > 1)
        //        scopeid = allParentString(ScopeID).Substring(1); 

        //    QueryParam qp = new QueryParam();
        //    qp.Order = " order by ScopeID asc";
        //    qp.Where = string.Format(" ScopeID in ({0})", scopeid);
        //    int RecordCount = 0;
        //    ArrayList lst = new Method().Sys_ScopeList(qp, out RecordCount);
        //    if (RecordCount > 0)
        //    {
        //        foreach (Sys_ScopeTable var in lst)
        //        { 
        //            droplist.Items.Add(new ListItem(var.S_Name,var.ScopeID.ToString()));
        //        } 
        //    }

        //    return droplist;
        //}


        /// <summary>
        /// 返回字符串格式：1,2,4
        /// </summary> 
        private static string allParentString(int ScopeID)
        {
            return allParentLevel(ScopeID, 0);

        }

        /// <summary>
        /// 遍历treeview节点(递归算法)格式：1,2,4
        /// </summary>
        /// <param name="ChildID">子ID</param>
        /// <param name="level">递归到第几层</param>
        /// <returns></returns>
        private static string allParentLevel(int ChildID, int level)
        {
            if (level == 0)
                returnvalue = "";

            DataSet ds = returndatasetParent(ChildID);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    returnvalue += "," + ds.Tables[0].Rows[i]["ScopeID"].ToString();

                    if (Convert.ToInt32(ds.Tables[0].Rows[i]["S_ParentID"].ToString()) > 0)
                    {
                        allParentLevel(Int32.Parse(ds.Tables[0].Rows[i]["S_ParentID"].ToString()), level + 1);

                    }
                }
            }

            return returnvalue;
        }


        /// <summary>
        /// 遍历treeview节点(递归算法)格式：1,2,4
        /// </summary> 
        public static string allid(TreeNodeCollection tnc)
        {//遍历treeview节点(递归算法)
            foreach (TreeNode node in tnc)
            {
                if (node.ChildNodes.Count != 0)
                {
                    returnvalue += "," + node.Target;
                    //Response.Write(node.Text+node.target+"<br>");
                    allid(node.ChildNodes);
                }
                else
                {
                    //Response.Write(node.Text+node.target+"<br>");
                    returnvalue += "," + node.Target;
                }
            }
            return returnvalue;
        }


        /// <summary>
        /// //遍历treeview节点(递归算法)算出子节点数目
        /// </summary>
        /// <param S_Name="tnc"></param>
        public static void allidNumber(TreeNodeCollection tnc)
        {//遍历treeview节点(递归算法)
            foreach (TreeNode node in tnc)
            {
                string ChildNodes = "";
                if (node.ChildNodes.Count > 0)
                    ChildNodes = "(" + node.ChildNodes.Count + ")";

                if (node.ChildNodes.Count != 0)
                {
                    node.Text = ChildNodes + node.Text;

                    allidNumber(node.ChildNodes);
                }
                else
                {
                    node.Text = ChildNodes + node.Text;
                }
            }
        }

      

    }
}
