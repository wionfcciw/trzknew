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
    public partial class Sjzd_AddEdit : BPage
    {
        BLL_zk_zdxx bll = new BLL_zk_zdxx();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                GetData();
        }

        //加载页面默认数据
        private void GetData()
        {
            if (Request.QueryString["zdlbdm"] != "0")
            {
                Model_zk_zdxx model = bll.Disp(Request.QueryString["zdlbdm"].ToString());
                lblZdlbdm.Text = model.Zdlbdm;
                txtZdlbmc.Text = model.Zdlbmc;

                txtZdlbdm.Visible = false;
            }
        }
        public string E_record = "";
        protected void btnEnter_Click(object sender, EventArgs e)
        {
            Model_zk_zdxx model = new Model_zk_zdxx
            {
                Zdlbdm = txtZdlbdm.Text.Trim(),
                Zdlbmc = txtZdlbmc.Text.Trim()
            };

            bool result = true;

            //县区代码为空则说明本次操作为添加操作
            if (Request.QueryString["zdlbdm"].ToString() == "0")
            {
                //添加数据
                result = bll.Insert_zk_zdxx(model);
                E_record = "新增：数据字典数据" + model.Zdlbmc;
            }
            else
            {
                //修改数据
                model.Zdlbdm = Request.QueryString["zdlbdm"].ToString();
                result = bll.update_zk_zdxx(model);
                E_record = "修改：数据字典数据" + model.Zdlbmc;
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