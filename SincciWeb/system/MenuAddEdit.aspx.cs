using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
namespace SincciKC.system
{
    public partial class MenuAddEdit :  BPage
    {
        #region "Page_Load"
        int  id = Convert.ToInt32(config.sink("id", config.MethodType.Get, 255, 1, config.DataType.Int));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            { 

                if (id > 0)
                {
                    binData(id);
                } 
               
            }
        }
        #endregion  

        #region "绑定修改数据"
        
        /// <summary>
        /// 绑定修改数据
        /// </summary>
        /// <param name="Moduleid"></param>
        private void binData(int id)
        {
            Sys_MenuTable mt = new Method().Sys_MenuDisp(id);
            this.txtName.Text = mt.M_Name;
            this.txtUrl.Text = mt.M_Url;             
            this.ddlTag.SelectedValue = mt.M_Tag.ToString();
            this.Hid.Value = id.ToString();
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
            Sys_MenuTable sat = new Sys_MenuTable();
            string id = this.Hid.Value.Trim();
            if (id.Length > 0)
            {
                sat.DataTable_Action_ = "Update";
                sat.id = int.Parse(id);
            }
            else
            {
                sat.DataTable_Action_ = "Insert";
            }
            sat.M_Name = config.CheckChar(this.txtName.Text.Trim());
            sat.M_Url = this.txtUrl.Text.Trim();
            sat.M_Tag = int.Parse(this.ddlTag.SelectedValue);

            int fa = new Method().Sys_MenuInsertUpdateDelete(sat);
            string E_record = "新增: 网站菜单数据：" + sat.M_Name + "";
            if (fa > 0)
            {
                EventMessage.EventWriteDB(1, E_record);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.succeedInfo({message:'操作成功！' ,title:'操作提示'});setTimeout(function(){window.parent.location.href=window.parent.location.href;window.parent.ymPrompt.close();},1000);</script>");

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'操作失败！' ,title:'提示'});</script>");
            }

        }
        #endregion

    }
}