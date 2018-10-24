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
    public partial class XX_lq : BPage
    {
        private BLL_LQK_Ks_Xx bll = new BLL_LQK_Ks_Xx();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int UserType = SincciLogin.Sessionstu().UserType;
                //if (UserType != 1)
                //{
                //    Response.Write("<script language='javascript'>window.parent.location.href='/ht/login.aspx';</script>");
                //    return;
                //}
                GetData();
                loadPcInfo();
              
            }
        }
        /// <summary>
        /// 加载批次信息。
        /// </summary>
        private void loadPcInfo()
        {
            BLL_LQK_Ks_Xx zk = new BLL_LQK_Ks_Xx();
            DataTable tab = zk.selectPcdm(" xqdm='" + SincciLogin.Sessionstu().U_department + "'", " 1=1 ");
            this.ddlXpcInfo.DataSource = tab;
            this.ddlXpcInfo.DataTextField = "xpc_mc";
            this.ddlXpcInfo.DataValueField = "pcdm";
            this.ddlXpcInfo.DataBind();
            this.ddlXpcInfo.Items.Insert(0, new ListItem("-请选择-", ""));
        }
        /// <summary>
        /// 加载招生学校数据
        /// </summary>
        private void BinZsxx()
        {
            this.ddlzsxx.DataSource = bll.Select_zk_zsxxdm();
            this.ddlzsxx.DataTextField = "zsxxmcc";
            this.ddlzsxx.DataValueField = "zsxxdm";
            this.ddlzsxx.DataBind();
            this.ddlzsxx.Items.Insert(0, new ListItem("-请选择-", ""));
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
   
          
             if (Request.QueryString["ksh"].ToString() != "")
             {
                 string str =  Request.QueryString["ksh"].ToString()  ;
                 //修改数据
                
                 result = bll.ksh_UP(str, ddlzsxx.SelectedValue.ToString(), ddlzy.SelectedValue.ToString(),ddlXpcInfo.SelectedValue.ToString());
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

        protected void ddlzsxx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlzsxx.SelectedValue.Length>0)
            {

                this.ddlzy.DataSource = bll.Select_zk_zykXX(ddlzsxx.SelectedValue);
                this.ddlzy.DataTextField = "zymc";
                this.ddlzy.DataValueField = "zydm";
                this.ddlzy.DataBind();
                this.ddlzy.Items.Insert(0, new ListItem("-请选择-", ""));
            }
        }
        /// <summary>
        /// 批次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlXpcInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlXpcInfo.SelectedValue.Length > 0)
            {

                this.ddlzsxx.DataSource = bll.Select_zk_zsxx(ddlXpcInfo.SelectedValue, " 1=1 ");
                this.ddlzsxx.DataTextField = "zsxxmc";
                this.ddlzsxx.DataValueField = "xxdm";
                this.ddlzsxx.DataBind();
                this.ddlzsxx.Items.Insert(0, new ListItem("-请选择-", ""));
            }
        }
    }
}