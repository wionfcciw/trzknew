using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
using Model;

namespace SincciKC.websystem.jcsj
{
    public partial class SjzdLb_AddEdit : BPage
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
            if (config.CheckChar(Request.QueryString["lsh"]) != "0")
            {
                Model_zk_zdxxLB model = bll.DispZdlb(Request.QueryString["lsh"].ToString());
                txtZlbdm.Text = model.Zlbdm;
                txtZlbmc.Text = model.Zlbmc;
                chkZlbzt.Checked = model.Zlbzt == 0 ? false : true;
            }
        }

        public string E_record = "";
        /// <summary>
        /// 保存数据
        /// </summary> 
        protected void btnEnter_Click(object sender, EventArgs e)
        {
            Model_zk_zdxxLB model = new Model_zk_zdxxLB
            {
                //  Lsh = int.Parse(Request.QueryString["lsh"].Trim().ToString()),
                Zdlbdm = Request.QueryString["zdlbdm"],
                Zlbdm = config.CheckChar(txtZlbdm.Text.Trim().ToString()),
                Zlbmc = config.CheckChar(txtZlbmc.Text.Trim().ToString()),
                Zlbzt = chkZlbzt.Checked == true ? 1 : 0
            };

            bool result = true;

            //县区代码为空则说明本次操作为添加操作
            if (config.CheckChar(Request.QueryString["lsh"].ToString()) == "0")
            {
                //添加数据
                result = bll.Insert_zk_zdxxLB(model);
                E_record = "新增：数据字典类别数据" + model.Zlbdm + "," + model.Zlbmc;
            }
            else
            {
                //修改数据
                model.Lsh = int.Parse(Request.QueryString["lsh"].ToString());
                result = bll.update_zk_zdxxLB(model);
                E_record = "修改：数据字典类别数据" + model.Zlbdm + "," + model.Zlbmc;
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