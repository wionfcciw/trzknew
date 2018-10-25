using System;
using System.Collections.Generic;
  
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Model;
using BLL;
namespace SincciKC.system
{
    public partial class ApplicationPermission : BPage
    {
        #region "Page_Load"
     public string PageCode = Convert.ToString(config.sink("PageCode", config.MethodType.Get, 255, 1, config.DataType.Str));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                OnStart();
                
            }
        }
        #endregion



        #region "绑定数据"
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="qp"></param>
        private void OnStart()
        {
            QueryParam qp = new QueryParam();
            qp.Order = " order by PermissionValue asc ";
            qp.Where = string.Format(" PageCode='{0}' ", PageCode);
            qp.PageSize = int.MaxValue;
            qp.PageIndex = 1;
            int RecordCount = 0; 
            GridView1.DataSource = new Method().Sys_PermissionList(qp, out RecordCount);
            GridView1.DataBind(); 
        }
 
        #endregion      

        #region "新增数据"
        /// <summary>
        /// 新增数据
        /// </summary> 
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (this.txtName.Text.Trim()=="")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('权限名称不能为空!');</script>");
                return;
            }
            if (this.txtValue.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('权限值不能为空!');</script>");
                return;
            }
            string name = config.CheckChar(this.txtName.Text.Trim().ToString());
            int value = int.Parse(config.CheckChar(this.txtValue.Text.Trim().ToString()));

            Sys_PermissionTable p = new Sys_PermissionTable();
            p.PageCode = PageCode;
            p.DataTable_Action_ = "Insert";
            p.PermissionName = name;
            p.PermissionValue = value;
         int fa=   new Method().Sys_PermissionInsertUpdateDelete(p);

            if (fa > 0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.succeedInfo({message:'操作成功！' ,title:'操作提示'});setTimeout(function(){window.location.href=window.location.href; },1000);</script>");

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'操作失败！' ,title:'提示'});</script>");
            }

        }
        #endregion

        /// <summary>
        /// 删除数据
        /// </summary> 
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            bool flag = false;
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
                if (cbox.Checked == true)
                {

                    Sys_PermissionTable sat = new Sys_PermissionTable();
                    sat.id = Convert.ToInt32(GridView1.DataKeys[i]["id"].ToString());
                    sat.DataTable_Action_ = "Delete";

                    new Method().Sys_PermissionInsertUpdateDelete(sat);
                    flag = true;
                     
                }
            }
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.succeedInfo({message:'删除成功！' ,title:'操作提示'});setTimeout(function(){ ymPrompt.close();},1000);</script>");

               
                OnStart();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请选择信息！' ,title:'提示'});setTimeout(function(){ ymPrompt.close();},1000);</script>");

            }
        }

      

    }
}