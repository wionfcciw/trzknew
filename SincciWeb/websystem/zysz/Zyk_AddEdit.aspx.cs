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
    public partial class Zyk_AddEdit : BPage
    {
        BLL_zk_zsxxdm bllzsxx = new BLL_zk_zsxxdm();
        BLL_zk_zyk bll = new BLL_zk_zyk();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                GetData();
        }

        //加载页面默认数据
        private void GetData()
        {
            ddlXxdm.DataSource = bllzsxx.Select_zk_zsxxdm();
            ddlXxdm.DataTextField = "zsxxmc";
            ddlXxdm.DataValueField = "zsxxdm";
            ddlXxdm.DataBind();
            ddlXxdm.Items.Insert(0, new ListItem("-请选择-", ""));

            if (config.CheckChar(Request.QueryString["ID"]) != "0")
            {
                Model_zk_zyk model = bll.Disp(config.CheckChar(Request.QueryString["ID"].ToString()),
                                              config.CheckChar(Request.QueryString["zydm"].ToString()));
                ddlXxdm.Visible = false;
                txtZydm.Visible = false;
                lblXxdm.Text = bllzsxx.Disp(model.Xxdm).Zsxxmc;
                lblZydm.Text = model.Zydm;
                txtZymc.Text = model.Zymc;
                txtBz.Text = model.Bz;
            }
        }

        protected void btnEnter_Click(object sender, EventArgs e)
        {
            Model_zk_zyk model = new Model_zk_zyk
            {
                Xxdm = config.CheckChar(ddlXxdm.SelectedItem.Value.Trim()),
                Zydm = config.CheckChar(txtZydm.Text.Trim()),
                Zymc = config.CheckChar(txtZymc.Text.Trim()),
                Bz = config.CheckChar(txtBz.Text.Trim())
            };

            bool result = true;

            //县区代码为空则说明本次操作为添加操作
            if (config.CheckChar(Request.QueryString["ID"].ToString()) == "0")
            {
                //添加数据
                result = bll.Insert_zk_zyk(model);
            }
            else
            {
                //修改数据
                model.Xxdm = config.CheckChar(Request.QueryString["ID"].ToString());
                model.Zydm = config.CheckChar(Request.QueryString["zydm"].ToString());
                
                result = bll.update_zk_zyk(model);
            }
            string E_record = "新增：专业库数据:" + model.Xxdm + "|" + model.Zydm;

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