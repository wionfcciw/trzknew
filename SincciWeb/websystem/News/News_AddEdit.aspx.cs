using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
 

namespace SincciWeb.websystem.News
{
    public partial class News_AddEdit : BPage
    {
        #region Page_Load
        int TypeID = Convert.ToInt32(config.sink("TypeID", config.MethodType.Get, 255, 1, config.DataType.Int));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                new Method().BindTreeView(this.ddlCategorys);
                 
                BindData();
                 
            }
        }
        #endregion

        #region  绑定数据
        private void BindData()
        {
            if (TypeID > 0)
            {
                PE_NewsListTable info = new Method().PE_NewsListDisp(TypeID);
                txtTitle.Text = info.Title;
                txtSource.Text = info.Urls;
                //txtAuthor.Text = info.Editor;
                //txtClick.Text = info.Number < 1 ? "0" : info.Number.ToString();
                content1.InnerHtml = info.Content;
                ddlCategorys.SelectedValue = info.CategoryID.ToString();
                //txtBeiZhu.Text = info.Remark;
                
            }
            
        }
        #endregion

        #region  添加或修改
        protected void btnSave_Click(object sender, EventArgs e)
        {
            PE_NewsListTable info = new PE_NewsListTable();
            if (TypeID > 0)
            {
                info.NewsID = TypeID;
                info.DataTable_Action_ = "Update";
            }
            else
            {
                info.DataTable_Action_ = "Insert";
            }          
         
            if (ddlCategorys.SelectedIndex==0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请选择新闻分类！' ,title:'提示'});</script>");
                return;               
            }
             
                info.AreaID = 0;
          
            

            info.Title = txtTitle.Text.Trim();
            info.Urls = txtSource.Text.Trim();
            info.Editor = SincciLogin.Sessionstu().UserName;
            info.CategoryID = Convert.ToInt32(ddlCategorys.SelectedValue.ToString());
           // info.Remark = txtBeiZhu.Text.Trim();
            info.PublishTime = DateTime.Now;
           // info.Number = txtClick.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtClick.Text.Trim());
           // info.ScopeID = new UserData().Get_sys_UserTable(SincciLogin.Sessionstu().UserName).U_department;
            //当输入绝对地址时，内容可以不用输入
            if (string.IsNullOrEmpty(info.Urls))
            {
                info.Content = this.content1.Value.Trim();
            }

            int fa = new Method().PE_NewsListInsertUpdateDelete(info);
            if (fa > 0)
            {
                string E_record = info.DataTable_Action_ + " ： " + info.Title;

                EventMessage.EventWriteDB(1, E_record);

               // Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.succeedInfo({message:'操作成功！' ,title:'操作提示'});setTimeout(function(){window.parent.location.href=window.parent.location.href;window.parent.ymPrompt.close();},1000);</script>");
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.succeedInfo({message:'操作成功！' ,title:'操作提示'});setTimeout(function(){window.location.href=window.location.href ;window.ymPrompt.close();},1000);</script>");
                //ClearText();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'操作失败！' ,title:'提示'});</script>");
            }
        }
        #endregion 
    }
}