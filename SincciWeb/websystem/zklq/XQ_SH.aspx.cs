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
namespace SincciKC.websystem.zklq
{
    public partial class XQ_SH : BPage
    {
        private BLL_zk_kscj bll = new BLL_zk_kscj();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                string Department = SincciLogin.Sessionstu().U_department;
                int UserType = SincciLogin.Sessionstu().UserType;
                if (UserType != 1)
                {
                    Response.Write("<script language='javascript'>window.parent.location.href='/ht/login.aspx';</script>");
                    return;
                }
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
                Model_zk_ksxxgl model = new BLL_zk_ksxxgl().ViewDisp(Request.QueryString["ksh"].ToString());
                lblksh.Text = model.Ksh;
                lblName.Text = model.Xm; 
            }
        }
        //操作记录
        public string E_record = "";
        protected void btnEnter_Click(object sender, EventArgs e)
        {
 
             bool result = false;

             //县区代码为空则说明本次操作为添加操作
             if (Request.QueryString["ksh"].ToString() != "")
             {
                 string str = " ksh='" + Request.QueryString["ksh"].ToString() + "'";
                 //修改数据
                 BLL_xqlq zk = new BLL_xqlq();
                 result = zk.XX_TD_YT(str, 2, txtqk.Text);
             }
             if (result)
             {
                 
                 Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                     "<script>alert('操作成功！');setTimeout(function(){window.parent.location.href=window.parent.location.href;},1000);</script>");
             }
             else
             {
                 Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                     "<script>alert('操作失败！');</script>");
             }
        }
    }
}