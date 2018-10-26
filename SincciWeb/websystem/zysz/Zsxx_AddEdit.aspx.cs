using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;


namespace SincciKC.websystem.zysz
{
    public partial class Zsxx_AddEdit : BPage
    {
        BLL_zk_zsxxdm bll = new BLL_zk_zsxxdm();

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
                Model_zk_zsxxdm model = bll.Disp(Request.QueryString["ID"].ToString());
                txtZsxxdm.Visible = false;
                lblZsxxdm.Text = model.Zsxxdm;
                txtZsxxmc.Text = model.Zsxxmc;
                txtBz.Text = model.Bz;
            }
        }

        protected void btnEnter_Click(object sender, EventArgs e)
        {
            Model_zk_zsxxdm model = new Model_zk_zsxxdm
            {
                Zsxxdm = config.CheckChar(txtZsxxdm.Text.Trim()),
                Zsxxmc = config.CheckChar(txtZsxxmc.Text.Trim()),
                Bz = config.CheckChar(txtBz.Text.Trim())
            };

            bool result = true;

            //县区代码为空则说明本次操作为添加操作
            if (config.CheckChar(Request.QueryString["ID"].ToString()) == "0")
            {
                //添加数据
                result = bll.Insert_zk_zsxxdm(model);
            }
            else
            {
                //修改数据
                model.Zsxxdm = bll.Disp(config.CheckChar(Request.QueryString["ID"].ToString())).Zsxxdm;
                result = bll.update_zk_zsxxdm(model);
            }
            string E_record = "新增: 招生学校：" + config.CheckChar(txtZsxxmc.Text.Trim()) + "";
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