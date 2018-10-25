using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using Model;
using System.Data;
namespace SincciKC.websystem.cjgl
{
    public partial class Hkcj_AddEdit : BPage
    {
        BLL_zk_kshkcj bll = new BLL_zk_kshkcj();
        BLL_zk_xqdm xqdmBll = new BLL_zk_xqdm();
        private string error = "";
        private bool bReturn = false;
      
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
            //ddlXqdm.DataSource = xqdmBll.SelectXqdm(Department, UserType);
            //ddlXqdm.DataTextField = "xqmc";
            //ddlXqdm.DataValueField = "xqdm";
            //ddlXqdm.DataBind();

            if (Request.QueryString["ksh"] != "")
            {
                DataTable dt = bll.Select_zk_kshkcj(Request.QueryString["ksh"].ToString());
                if (dt.Rows.Count > 0)
                {
                    smList.SelectedValue = dt.Rows[0]["Swdj"].ToString();
                    cxysList.SelectedValue = dt.Rows[0]["Dldj"].ToString();
                    lblksh.Text = dt.Rows[0]["ksh"].ToString();
                }
            }
        }
        //操作记录
        public string E_record = "";
        protected void btnEnter_Click(object sender, EventArgs e)
        {
            //Model_zk_xxdm model = new Model_zk_xxdm
            //{
            //    Xxdm = config.CheckChar(txtByxxdm.Text.Trim()),
            //    Xxmc = config.CheckChar(txtXqmc.Text.Trim())
            //};

             bool result = true;

             //县区代码为空则说明本次操作为添加操作
             if (Request.QueryString["ksh"].ToString() != "")
             {
                 Model_zk_kshkcj model = new Model_zk_kshkcj();
                 if (bll.zk_kshkcj(Request.QueryString["ksh"].ToString()).Rows.Count > 0)
                 {
                     //修改数据
                   
                     model.Ksh = Request.QueryString["ksh"].ToString();
                     model.Swdj = smList.SelectedValue;
                     model.Dldj = cxysList.SelectedValue;
                     result = bll.update_zk_kshkcj(model);

                 }
                 else 
                 {
                    
                     model.Ksh = Request.QueryString["ksh"].ToString();
                     model.Swdj = smList.SelectedValue;
                     model.Dldj = cxysList.SelectedValue;
                     result = bll.Insert_zk_kshkcj(model);
                 }
                 E_record = "修改: 会考成绩数据：" + model.Ksh + "";
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