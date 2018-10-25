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
    public partial class Zhpj_AddEdit : BPage
    {
        BLL_zk_kshkcj bll = new BLL_zk_kshkcj();
       
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
                DataTable  dt = bll.zk_zhpj(Request.QueryString["ksh"].ToString());
                if (  dt.Rows.Count>0)
                {
                   ddpzList.SelectedValue = dt.Rows[0]["Ddpzgmsy"].ToString();
                   jlList.SelectedValue = dt.Rows[0]["Jlhznl"].ToString();
                   xxxgList.SelectedValue = dt.Rows[0]["Xxxgxxnl"].ToString();  
                   ydList.SelectedValue = dt.Rows[0]["Ydjk"].ToString();  
                   smList.SelectedValue = dt.Rows[0]["Smbx"].ToString(); 
                   cxysList.SelectedValue = dt.Rows[0]["Cxyssjnl"].ToString();
                   lblksh.Text = dt.Rows[0]["Ksh"].ToString(); 
                   lblName.Text = dt.Rows[0]["xm"].ToString(); 
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
                 //修改数据
                 zk_kszhpj model = new zk_kszhpj();
                 model.Ksh = Request.QueryString["ksh"].ToString();
                 model.Ddpzgmsy = ddpzList.SelectedValue;
                 model.Jlhznl = jlList.SelectedValue;
                 model.Xxxgxxnl = xxxgList.SelectedValue;
                 model.Ydjk = ydList.SelectedValue;
                 model.Smbx = smList.SelectedValue;
                 model.Cxyssjnl = cxysList.SelectedValue;
               
                 result = bll.KsZhpj(model);
                 E_record = "修改: 综合评价数据：" + model.Ksh + "";
             }
             if (result)
             {
                 EventMessage.EventWriteDB(1, E_record);
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