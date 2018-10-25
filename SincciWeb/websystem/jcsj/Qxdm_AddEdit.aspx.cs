using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
 

namespace SincciKC.websystem.jcsj
{
    public partial class Qxdm_AddEdit : BPage
    {
        BLL_zk_xqdm bll = new BLL_zk_xqdm();
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
                Model_zk_xqdm xqdmModel = bll.Disp(Request.QueryString["ID"].ToString());
                txtXqdm.Text = xqdmModel.Xqdm;
                txtXqmc.Text = xqdmModel.Xqmc;
                this.txtXqdm.Enabled = false;
            }
        }

        protected void btnEnter_Click(object sender, EventArgs e)
        {
            Model_zk_xqdm xqdmModel = new Model_zk_xqdm
            {
                Xqdm = config.CheckChar(txtXqdm.Text.Trim()),
                Xqmc = config.CheckChar(txtXqmc.Text.Trim())
            };

            bool result = true;

            //县区代码为空则说明本次操作为添加操作
            if (config.CheckChar(Request.QueryString["ID"].ToString()) == "0")
            {
                //添加数据
                result = bll.Insert_zk_xqdm(xqdmModel);
                E_record = "新增: 县区数据：" + xqdmModel.Xqdm +","+xqdmModel.Xqmc+ "";
            }
            else
            {
                //修改数据
                xqdmModel.QxId = bll.Disp(config.CheckChar(Request.QueryString["ID"].ToString())).QxId;
                result = bll.update_zk_xqdm(xqdmModel);
                E_record = "修改: 县区数据：" + xqdmModel.Xqdm + "," + xqdmModel.Xqmc + "";
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