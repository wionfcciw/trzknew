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
    public partial class Xxdm_AddEdit : BPage
    {
        BLL_zk_xxdm bll = new BLL_zk_xxdm();
        BLL_zk_xqdm xqdmBll = new BLL_zk_xqdm();
        //操作记录
        public string E_record = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int UserType = SincciLogin.Sessionstu().UserType;
                string Department = SincciLogin.Sessionstu().U_department;

                GetData();
            }
        }

        //加载页面默认数据
        private void GetData()
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            BLL_zk_zdxx zdxx = new BLL_zk_zdxx();

            this.xxList.DataSource = zdxx.selectData("xxlx");
            this.xxList.DataTextField = "zlbmc";
            this.xxList.DataValueField = "zlbdm";
            this.xxList.DataBind();
            this.xxList.Items.Insert(0, new ListItem("请选择", ""));

            ddlXqdm.DataSource = xqdmBll.SelectXqdm(Department, UserType);
            ddlXqdm.DataTextField = "xqmc";
            ddlXqdm.DataValueField = "xqdm";
            ddlXqdm.DataBind();
            this.ddlXqdm.Items.Insert(0, new ListItem("请选择", ""));

            if (config.CheckChar(Request.QueryString["xxdm"]) != "0")
            {
                Model_zk_xxdm model = bll.Disp(Request.QueryString["xxdm"].ToString());

                
                ddlXqdm.SelectedIndex = ddlXqdm.Items.IndexOf(ddlXqdm.Items.FindByValue(model.Xqdm));
                lblByxxdm.Text = model.Xxdm;
                xxList.SelectedValue = model.Xxlxdm;
                txtByxxdm.Visible = false;
                txtXqmc.Text = model.Xxmc;
                this.ddlXqdm.Enabled = false;
                this.xxList.SelectedValue = model.Xxlxdm;
            }
            else
                lblByxxdm.Visible = false;
        }

        protected void btnEnter_Click(object sender, EventArgs e)
        {

            if (ddlXqdm.SelectedIndex ==0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                 "<script>alert('请选择县区代码！');</script>");
                return;
            }
            if (xxList.SelectedIndex == 0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                 "<script>alert('请选择学校类型！');</script>");
                return;
            }
            Model_zk_xxdm model = new Model_zk_xxdm
            {
                Xxdm = config.CheckChar(txtByxxdm.Text.Trim()),
                Xxmc = config.CheckChar(txtXqmc.Text.Trim()),
                Xxlxdm = xxList.SelectedValue,
                Xqdm = ddlXqdm.SelectedItem.Value.Trim() 
            };

            bool result = true;

            //县区代码为空则说明本次操作为添加操作
            if (Request.QueryString["xxdm"].ToString() == "0")
            {
                //添加数据                
                result = bll.Insert_zk_xxdm(model);
                E_record = "添加: 学校数据：" + model.Xxdm + "";
            }
            else
            {
                //修改数据
                model.Xxdm = Request.QueryString["xxdm"].ToString();
                result = bll.update_zk_xxdm(model);
                E_record = "修改: 学校数据：" + model.Xxdm + "";
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

        /// <summary>
        /// 选择县区时
        /// </summary> 
        protected void ddlXqdm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlXqdm.SelectedValue.Length > 0)
                this.txtByxxdm.Text = this.ddlXqdm.SelectedValue;
        }
    }
}