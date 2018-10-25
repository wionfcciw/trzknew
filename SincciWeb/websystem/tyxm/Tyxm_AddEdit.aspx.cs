using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using System.Data;
 

namespace SincciKC.websystem.tyxm
{
    public partial class Tyxm_AddEdit : BPage
    {
        BLL_tyxm bll = new BLL_tyxm();
        //操作记录
        public string E_record = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                GetData();
        }

        //加载页面默认数据
        private void GetData()
        {
            if (config.CheckChar(Request.QueryString["ID"]) != "0")
            {
                DataTable dt = bll.seleczk_tyks(" id=" + config.CheckChar(Request.QueryString["ID"]));
                dlistxx.SelectedValue = dt.Rows[0]["lxId"].ToString();
                txtXqmc.Text = dt.Rows[0]["name"].ToString();
                dlistxx.Enabled = false;
            }
        }

        protected void btnEnter_Click(object sender, EventArgs e)
        {
          
            bool result = true;

          
            if (config.CheckChar(Request.QueryString["ID"].ToString()) == "0")
            {
                //添加数据
                result = bll.Insert_zk_tyks(dlistxx.SelectedValue, config.CheckChar(txtXqmc.Text.Trim()));
                E_record = "新增: 体育项目：" + dlistxx.SelectedItem.Text + "," + config.CheckChar(txtXqmc.Text.Trim()) + "";
            }
            else
            {
                //修改数据
                string id = config.CheckChar(Request.QueryString["ID"].ToString());
                result = bll.Update_zk_tyks(id, config.CheckChar(txtXqmc.Text.Trim()));
                E_record = "修改: 体育项目：" + dlistxx.SelectedItem.Text + "," + config.CheckChar(txtXqmc.Text.Trim()) + "";
            }

            if (result)
            {
                EventMessage.EventWriteDB(1, E_record);

                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                    "<script>ymPrompt.succeedInfo({message:'操作成功！' ,title:'操作提示'});setTimeout(function(){window.parent.location.href=window.parent.location.href;},1000);</script>");
            }
            else
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                    "<script>ymPrompt.alert({message:'操作失败！' ,title:'提示'});</script>");
        }
    }
}