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
    public partial class XX_FD : BPage
    {
        private BLL_zk_kscj bll = new BLL_zk_kscj();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
            string pcdm = "";
            if (Request.QueryString["pcdm"] != "")
            {
                pcdm = Request.QueryString["pcdm"].ToString();
                BLL_zk_PiLiangTouDang zk = new BLL_zk_PiLiangTouDang();
                DataTable tab = zk.select_fd_noxx(pcdm);
                this.ddl_xx.DataSource = tab;
                this.ddl_xx.DataTextField = "zsxxmc";
                this.ddl_xx.DataValueField = "lqxx";
                this.ddl_xx.DataBind();
                this.ddl_xx.Items.Insert(0, new ListItem("-请选择-", ""));
            }
        }
        //操作记录
        public string E_record = "";
        protected void btnEnter_Click(object sender, EventArgs e)
        {
             string pcdm = "";
             string xxdm = ddl_xx.SelectedValue;
             string jy = ddljy.SelectedValue;
             if (xxdm.Length == 0)
             {
                 return;
             }
             if (Request.QueryString["pcdm"] != "")
             {
                 pcdm = Request.QueryString["pcdm"].ToString();
                 BLL_zk_PiLiangTouDang zk = new BLL_zk_PiLiangTouDang();
                 if (zk.FA_PC_TD_XX(pcdm, xxdm, jy))
                 {
                     Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'单校发档成功！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                 }
                 else
                 {
                     Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'单校发档失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                 }
             }
             GetData();
        }
    }
}