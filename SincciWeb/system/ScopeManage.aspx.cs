using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Model;
using BLL;
using System.Collections;  

namespace SincciKC.system
{
    public partial class ScopeManage : BPage
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Permission();

                Button del = new Button();
                del = (Button)Page.FindControl("delbutton");
                del.Attributes.Add("onclick", "return confirm('确定删除');");
                showlist();

            }

        }

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
            ////新增
            //if (!new Method().CheckButtonPermission(PopedomType.New))
            //{
            //    this.btnNew.Visible = false;
            //}

            ////修改
            //if (!new Method().CheckButtonPermission(PopedomType.Edit))
            //{
            //    this.btnEdit.Enabled = false;
            //}
            ////删除
            //if (!new Method().CheckButtonPermission(PopedomType.Delete))
            //{
            //    this.btnDelete.Visible = false;
            //}


        }
        #endregion

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void showlist()
        {

            //绑定treeview
            //infobase io=new infobase();   
            dropdownlist1.Items.Clear();
            ListItem list = new ListItem();
            list.Value = "0";
            list.Text = "全部类别";
            dropdownlist1.Items.Add(list);

          Scope_Tree.dropdownlistbind("0", dropdownlist1);

            //添加根节点
            TreeView1.Nodes.Clear();
            TreeNode node = new TreeNode();
            node.Text = "类别列表";
            node.Target = "0";
            node.Expanded = true;
            TreeView1.Nodes.Add(node);
            Scope_Tree.treeshow("0", node);
            TreeView1.DataBind();
        }

 

        /// <summary>
        /// 添加节点
        /// </summary> 
        protected void addbutton_Click1(object sender, EventArgs e)
        {
            //添加节点
            if (txtName.Text != "")
            {
                TreeNode TreeNode = TreeView1.SelectedNode; //.getnodefromindex(TreeView1.selectednodeindex);
                int treedepth = TreeNode.Depth;

                if (TreeNode.Target != null)
                {
                    int depth = treedepth + 1; //获得深度
                    int parterid = Int32.Parse(TreeNode.Target);//获得父类 id
                    string classname = txtName.Text.Trim().ToString();//获得类名
                    string Code = txtCode.Text.Trim().ToString(); //获得代码 

                    Sys_ScopeTable st = new Sys_ScopeTable();
                    st.DataTable_Action_ = "Insert";
                    st.S_Name = classname;
                    st.S_Code = Code;
                    st.S_ParentID = parterid;
                    st.S_Depth = depth;

                    new Method().Sys_ScopeInsertUpdateDelete(st);
                    //new DBcon().CommandSql("insert into Scope_Tree (Name,Code,ParentID,Depth)values('" + classname + "','" + Code + "'," + parterid + "," + depth + ")");

                    Response.Write("<script>alert('添加成功'); location.href=location.href; </script>");


                }
                else
                {
                    //Response.Write(TreeNode.target+"123<br>");
                }
            }
            else
            {
                Response.Write("请填写类别并选择节点");
            }
        }
        /// <summary>
        /// 修改节点
        /// </summary> 
        protected void editbutton_Click1(object sender, EventArgs e)
        {
            //修改节点
            if (txtName.Text.Trim() != "")
            {
                TreeNode TreeNode = TreeView1.SelectedNode;  //TreeView1.GetNodeFromIndex(TreeView1.selectednodeindex);
                int id = Int32.Parse(TreeNode.Target);
                string classname = txtName.Text.Trim().ToString();
                string Code = txtCode.Text.Trim().ToString();
                if (TreeNode.Target != null)
                {
                    Sys_ScopeTable st = new Sys_ScopeTable();
                    st.DataTable_Action_ = "Update";
                    st.S_Name = classname;
                    st.S_Code = Code;
                    st.ScopeID = id;
                    new Method().Sys_ScopeInsertUpdateDelete(st);
                   // new DBcon().CommandSql("  update Scope_Tree set Name='" + classname + "',Code='" + Code + "' where id=" + id + " ");
                    Response.Write("<script>alert('修改成功'); location.href=location.href; </script>");

                }
                else { }
            }
            else
            {
                Response.Write("请填写类别并选择节点");
            }
        }

        /// <summary>
        /// 删除
        /// </summary> 
        protected void delbutton_Click1(object sender, EventArgs e)
        {
            //删除   
            TreeNode TreeNode = TreeView1.SelectedNode;   //. getnodefromindex(TreeView1.selectednodeindex);
            int id = Int32.Parse(TreeNode.Target);
            string[] tmpid;
            tmpid = (id.ToString() + Scope_Tree.allid(TreeNode.ChildNodes).ToString()).Split(',');

            if (TreeNode.Target != "0")
            {
                //  Response.Write(" delete from  Scope_Tree   where id in(" + tmpid + ")");
                for (int i = 0; i < tmpid.Length; i++)
                {
                    Sys_ScopeTable st = new Sys_ScopeTable();
                    st.DataTable_Action_ = "Delete";
                    st.ScopeID = Convert.ToInt32(tmpid[i]);
                    new Method().Sys_ScopeInsertUpdateDelete(st);
                }

                //new DBcon().CommandSql("  delete  from  Scope_Tree   where id in(" + tmpid + ") ");
                Response.Write("<script>alert('删除成功'); location.href=location.href; </script>");
            }
            else
            {
                Response.Write("不能删除根节点");
            }
        }

        //移动节点 
        private void movebutton_click(object sender, EventArgs e)
        {//移动 
            //此功能实现的时候需要考虑移动中所有类的深度是否有子类问题
        }

        /// <summary>
        /// TreeView数据绑定后 数出子节点数目
        /// </summary> 
        protected void TreeView1_DataBound(object sender, EventArgs e)
        {
            Scope_Tree.allidNumber(this.TreeView1.Nodes);

        }





    }
}