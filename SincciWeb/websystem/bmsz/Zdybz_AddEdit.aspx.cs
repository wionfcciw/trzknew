using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BLL;
using Model;

namespace SincciKC.websystem.bmsz
{
    public partial class Zdybz_AddEdit : BPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblksh.Text = config.CheckChar(Request.QueryString["xqdm"]);
                txtName.Text = config.CheckChar(Request.QueryString["bzmc"]);
            }
        }
        private BLL_zk_szzdybz bll = new BLL_zk_szzdybz();
        protected void btnEnter_Click(object sender, EventArgs e)
        {
            bool result = true;
            Model_zk_zdybz model = new Model_zk_zdybz();
            string xqdm = config.CheckChar(Request.QueryString["xqdm"]).Split(']')[0].Substring(1, config.CheckChar(Request.QueryString["xqdm"]).Split(']')[0].Length - 1);
            model.Bzmc = config.CheckChar(txtName.Text.Trim().ToString());
            model.Xqdm = config.CheckChar(xqdm);
            if (bll.Select_zk_zdybz(xqdm).Rows.Count > 0)
            { 
                result = bll.update_zk_zdybz(model);
            }
            else
            { 
                result = bll.Insert_zk_zdybz(model);
            }
           string E_record = "修改: 自定义备注字段：" + model.Xqdm + "";
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