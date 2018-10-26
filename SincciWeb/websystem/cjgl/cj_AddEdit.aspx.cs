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
    public partial class cj_AddEdit : BPage
    {
        private BLL_zk_kscj bll = new BLL_zk_kscj();
       
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
                DataTable  dt = bll.zk_cj(Request.QueryString["ksh"].ToString());
                if (  dt.Rows.Count>0)
                {
                   ddpzList.SelectedValue = dt.Rows[0]["zp1"].ToString();
                   jlList.SelectedValue = dt.Rows[0]["zp2"].ToString();
                   xxxgList.SelectedValue = dt.Rows[0]["zp3"].ToString();
                   ydList.SelectedValue = dt.Rows[0]["zp4"].ToString();
                   smList.SelectedValue = dt.Rows[0]["zp5"].ToString();
                   cxysList.SelectedValue = dt.Rows[0]["zp6"].ToString();
                   diliList.SelectedValue=dt.Rows[0]["dl7"].ToString();
                   swList.SelectedValue = dt.Rows[0]["sw8"].ToString();
                   txttl.Text = dt.Rows[0]["tl9"].ToString();
                   txtty1.Text = dt.Rows[0]["tyx10"].ToString();
                   txtty2.Text = dt.Rows[0]["tyt11"].ToString();
                   txtty3.Text = dt.Rows[0]["tyz12"].ToString();

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
                 Model_zk_kscj   model = new Model_zk_kscj();
                 model.Ksh = Request.QueryString["ksh"].ToString();
                 model.Zp1 = ddpzList.SelectedValue;
                 model.Zp2 = jlList.SelectedValue;
                 model.Zp3 = xxxgList.SelectedValue;
                 model.Zp4 = ydList.SelectedValue;
                 model.Zp5= smList.SelectedValue;
                 model.Zp6 = cxysList.SelectedValue;
                 model.Dl7= diliList.SelectedValue;
                model.Sw8= swList.SelectedValue;
                model.Tl9= txttl.Text;
                model.Tyx10= txtty1.Text;
               model.Tyt11  =txtty2.Text;
               model.Tyz12  =txtty3.Text;


                 result = bll.KsZhpj(model);
                 E_record = "修改: 考生成绩数据：" + model.Ksh + "";
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