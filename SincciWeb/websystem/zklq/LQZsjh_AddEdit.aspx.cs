using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;


namespace SincciKC.websystem.zklq
{
    public partial class LQZsjh_AddEdit : BPage
    {
        BLL_zk_zsjh bll = new BLL_zk_zsjh();
        BLL_zk_xqdm xqBLL = new BLL_zk_xqdm();
        BLL_zk_zsxxdm zsxxBLL = new BLL_zk_zsxxdm();
        BLL_zk_zyk zykBLL = new BLL_zk_zyk();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                GetData();
        }

        //加载页面默认数据
        private void GetData()
        {
            ddlXqdm.DataSource = xqBLL.selectxqdm();
            ddlXqdm.DataTextField = "xqmc";
            ddlXqdm.DataValueField = "xqdm";
            ddlXqdm.DataBind();

            ddlXxdm.DataSource = zsxxBLL.Select_zk_zsxxdm();
            ddlXxdm.DataTextField = "zsxxmcc";
            ddlXxdm.DataValueField = "zsxxdm";
            ddlXxdm.DataBind();
          


            ddlZydm.DataSource = zykBLL.Select_zk_zykXX(ddlXxdm.SelectedItem.Value.Trim());
            ddlZydm.DataTextField = "zymc";
            ddlZydm.DataValueField = "zydm";
            ddlZydm.DataBind();
            ddlZydm.Items.Insert(0, new ListItem("-请选择-", ""));
            ddlXzdm.DataSource = zsxxBLL.GetXueZhiInfo();
            ddlXzdm.DataTextField = "xzmc";
            ddlXzdm.DataValueField = "xzdm";
            ddlXzdm.DataBind();
            ddlXzdm.Items.Insert(0, new ListItem("-请选择-", ""));

            if (config.CheckChar(Request.QueryString["ID"]) != "0")
            {
                Model_zk_zsjh model = bll.Disp_zk_lqjhk(Request.QueryString["ID"].ToString());
                ddlXqdm.SelectedIndex = ddlXqdm.Items.IndexOf(ddlXqdm.Items.FindByValue(model.Xqdm));
                ddlXxdm.SelectedIndex = ddlXxdm.Items.IndexOf(ddlXxdm.Items.FindByValue(model.Xxdm));

                ddlZydm.DataSource = zykBLL.Select_zk_zykXX(ddlXxdm.SelectedItem.Value.Trim());
                ddlZydm.DataTextField = "zymc";
                ddlZydm.DataValueField = "zydm";
                ddlZydm.DataBind();
                ddlZydm.Items.Insert(0, new ListItem("-请选择-", ""));
                ddlZydm.SelectedIndex = ddlZydm.Items.IndexOf(ddlZydm.Items.FindByValue(model.Zydm));
                ddlXzdm.SelectedIndex = ddlXzdm.Items.IndexOf(ddlXzdm.Items.FindByValue(model.Xzdm));
                txtJhs.Text = model.Jhs.ToString();
                txtPcdm.Text = model.Pcdm;
              //  txtXxlbdm.Text = model.Xxlbdm;
             
            }
        }

        protected void btnEnter_Click(object sender, EventArgs e)
        {
            string zydm = "00";
            if (ddlZydm.SelectedItem != null)
            {
                zydm = ddlZydm.SelectedItem.Value.Trim();
            }
            if (zydm == "")
            {
                zydm = "00";
            }
            Model_zk_zsjh model = new Model_zk_zsjh
            {
                Xqdm = ddlXqdm.SelectedItem.Value.Trim(),
                Xxdm = ddlXxdm.SelectedItem.Value.Trim(),
                Zydm = zydm,
                Xzdm = ddlXzdm.SelectedItem.Value.Trim(),
                Jhs = int.Parse(config.CheckChar(string.IsNullOrEmpty(txtJhs.Text.Trim()) ? "0" : txtJhs.Text.Trim())),
                Pcdm = config.CheckChar(txtPcdm.Text.Trim()),
              //  Xxlbdm = config.CheckChar(txtXxlbdm.Text.Trim()),
               
            };

            bool result = true;

            //县区代码为空则说明本次操作为添加操作
            if (config.CheckChar(Request.QueryString["ID"].ToString()) == "0")
            {
                //添加数据
                result = bll.Insert_zk_zsjh(model);
            }
            else
            {
                //修改数据
                model.Lsh = bll.Disp_zk_lqjhk(config.CheckChar(Request.QueryString["ID"].ToString())).Lsh;
                result = bll.update_zk_lqjhk(model);
            }
            string E_record = "修改: 录取招生计划：" + config.CheckChar(Request.QueryString["ID"].ToString()) + "";

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
       //根据学校搜索相关专业
        protected void ddlXxdm_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlZydm.DataSource = zykBLL.Select_zk_zykXX(ddlXxdm.SelectedItem.Value.Trim());
            ddlZydm.DataTextField = "zymc";
            ddlZydm.DataValueField = "zydm";
            ddlZydm.DataBind();

        
                ddlZydm.Items.Insert(0, new ListItem("-请选择-", ""));
        }
    }
}