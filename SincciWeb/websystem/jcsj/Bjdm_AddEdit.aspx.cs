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
    public partial class Bjdm_AddEdit : BPage
    {
        BLL_zk_bjdm bll = new BLL_zk_bjdm();
        BLL_zk_xqdm xqBll = new BLL_zk_xqdm();
        BLL_zk_xxdm xxBLL = new BLL_zk_xxdm();

 
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
            ddlXqdm.DataSource = xqBll.SelectXqdm(Department, UserType);
            ddlXqdm.DataTextField = "xqmc";
            ddlXqdm.DataValueField = "xqdm";
            ddlXqdm.DataBind();
            ddlXqdm.Items.Insert(0, new ListItem("--请选择--", ""));
            ddlXxdm.Items.Insert(0, new ListItem("--请选择--", ""));

            if (config.CheckChar(Request.QueryString["lsh"]) != "0")
            {
                Model_zk_bjdm model = bll.Disp(Request.QueryString["lsh"].ToString());
                Model_zk_xqdm xqModel = xqBll.Disp(model.xqdm);
                Model_zk_xxdm xxModel = xxBLL.Disp(model.xxdm);

                ddlXqdm.Visible = false;
                ddlXxdm.Visible = false;
                txtBjdm.Visible = false;
                lblXqdm.Text = "[" + xqModel.Xqdm + "]" + xqModel.Xqmc;
                lblXxdm.Text = "[" + xxModel.Xxdm + "]" + xxModel.Xxmc;
                lblBjdm.Text = model.Bjdm;
                txtBjmc.Text = model.Bjmc;
            }
            else
            {
                lblXqdm.Visible = false;
                lblXxdm.Visible = false;
                lblBjdm.Visible = false;
            }
        }
        public string E_record = "";
        /// <summary>
        /// 保存数据
        /// </summary> 
        protected void btnEnter_Click(object sender, EventArgs e)
        {
            Model_zk_bjdm xqdmModel = new Model_zk_bjdm
            {
                xqdm = ddlXqdm.SelectedItem.Value.Trim(),
                xxdm = ddlXxdm.SelectedItem.Value.Trim(),
                Bjdm = config.CheckChar(txtBjdm.Text.Trim()),
                Bjmc = config.CheckChar(txtBjmc.Text.Trim())
            };

            bool result = true;
            if (bll.Select_zk_bjdm(ddlXxdm.SelectedItem.Value.Trim()," bjdm='"+config.CheckChar(txtBjdm.Text.Trim())+"'").Rows.Count>0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                 "<script>alert('该班级已存在,请重新输入！');</script>");
                txtBjdm.Focus();
                return;
            }
            //县区代码为空则说明本次操作为添加操作
            if (config.CheckChar(Request.QueryString["lsh"].ToString()) == "0")
            {
                //添加数据
                result = bll.Insert_zk_bjdm(xqdmModel);
                E_record = "新增：班级数据" + xqdmModel.xxdm + "," + xqdmModel.Bjdm;
            }
            else
            {
                //修改数据
                xqdmModel.Lsh = int.Parse(config.CheckChar(Request.QueryString["lsh"].ToString()));
                result = bll.update_zk_bjdm(xqdmModel);
                E_record = "修改：班级数据" + xqdmModel.xxdm + "," + xqdmModel.Bjdm;
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

        protected void ddlXqdm_SelectedIndexChanged(object sender, EventArgs e)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string xqdm = ddlXqdm.SelectedValue;

            ddlXxdm.DataSource = new BLL_zk_xxdm().Select_zk_xxdmXQ(xqdm, Department, UserType);
            ddlXxdm.DataTextField = "xxmc";
            ddlXxdm.DataValueField = "xxdm";
            ddlXxdm.DataBind();
            ddlXxdm.Items.Insert(0, new ListItem("--请选择--", ""));
        }
    }
}