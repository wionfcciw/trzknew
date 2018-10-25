using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
 

namespace SincciKC.websystem.jcsj
{
    public partial class Kaoci_AddEdit : BPage
    {
        BLL_zk_kcdm bll = new BLL_zk_kcdm();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                GetData();
        }

        //加载页面默认数据
        private void GetData()
        {
            if (config.CheckChar(Request.QueryString["kcdm"]) != "0")
            {
                Model_zk_kcdm model = bll.Disp(config.CheckChar(Request.QueryString["kcdm"].ToString()));
                txtKcdm.Text = model.Kcdm;
                txtKcmc.Text = model.Kcmc;
                this.txtKcdm.Enabled = false;
            }
        }
        public string E_record = "";
        /// <summary>
        /// 保存数据
        /// </summary> 
        protected void btnEnter_Click(object sender, EventArgs e)
        {
            Model_zk_kcdm xqdmModel = new Model_zk_kcdm
            {
                Kcdm = config.CheckChar(txtKcdm.Text.Trim()),
                Kcmc = config.CheckChar(txtKcmc.Text.Trim())
            };

            bool result = true;

            //县区代码为空则说明本次操作为添加操作
            if (config.CheckChar(Request.QueryString["kcdm"].ToString()) == "0")
            {
                //添加数据
                result = bll.Insert_zk_kcdm(xqdmModel);
                E_record = "新增：考次数据" + xqdmModel.Kcdm + "," + xqdmModel.Kcmc;
            }
            else
            {
                //修改数据
                xqdmModel.KcId = bll.Disp(config.CheckChar(Request.QueryString["kcdm"].ToString())).KcId;
                result = bll.update_zk_kcdmID(xqdmModel);
                E_record = "修改：考次数据" + xqdmModel.Kcdm + "," + xqdmModel.Kcmc;
            }

            if (result)
            {
                EventMessage.EventWriteDB(1, E_record);

                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                    "<script>alert('操作成功！');setTimeout(function(){window.parent.location.href=window.parent.location.href;},1000);</script>");
            }
            else
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                    "<script>alert('操作失败！');</script>");
        }
    }
}