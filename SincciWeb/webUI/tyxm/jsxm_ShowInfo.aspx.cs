using System;
using System.Collections.Generic;
 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
using System.Data;
using BLL.system;
namespace SincciKC.webUI.tyxm
{
    public partial class jsxm_ShowInfo : SPage //wf 2018-08-16 修改，单点登录统一控制 System.Web.UI.Page
    {
        public Model_zk_ksxxgl info = new Model_zk_ksxxgl();
        public string ksh = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["kaosheng"] != null)
                {
                    if (BLL_Ks_Session.ksLogCheck())
                    {

                        ksh = BLL_Ks_Session.ksSession().ksh;
                        string xqdm = ksh.Substring(2, 4);
                        if (!new Bll_zkbm_Time().zkbm_time(xqdm,4))
                        {
                            Response.Write("<script>alert('现在不是网上报名时间！'); history.back(); </script>");
                        }
                        else
                        {
                            OnStart();
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('超时请重新登录！'); window.location.href='/' </script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('超时请重新登录！'); window.location.href='/' </script>");
                }
            }
        }

        private BLL_jsxm bll = new BLL_jsxm();
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void OnStart()
        {
            DataTable kstab = bll.selecksh(ksh);
            lblbmd.Text = kstab.Rows[0]["bmdmc"].ToString();
            lblkaoci.Text = kstab.Rows[0]["kaocimc"].ToString();
            lblksh.Text = kstab.Rows[0]["ksh"].ToString();
            lblxm.Text = kstab.Rows[0]["xm"].ToString();
            lblsfzh.Text = kstab.Rows[0]["sfzh"].ToString();
            lblbj.Text = kstab.Rows[0]["bjmc"].ToString();

           
            lblchoud.Text = kstab.Rows[0]["bxmc"].ToString();
         
            if (kstab.Rows[0]["ksjsqr"].ToString() == "1")
            {
                this.btnKSQueren.Visible = true;
            }
            if (kstab.Rows[0]["ksjsqr"].ToString() == "2")
            {
                this.btnSave.Visible = false;
            }




        }

        /// <summary>
        /// 修改考生资料
        /// </summary> 
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Response.Redirect("jsxm_Input.aspx", false);
        }

        /// <summary>
        /// 考生确认
        /// </summary> 
        protected void btnKSQueren_Click(object sender, EventArgs e)
        {
            if (Session["kaosheng"] != null)
            {
                if (BLL_Ks_Session.ksLogCheck())
                {
                    ksh = BLL_Ks_Session.ksSession().ksh;
                    if (bll.Update_ksqr(ksh))
                    {
                        Response.Write("<script>alert('考生信息确认成功！'); window.location.href='jsxm_ShowInfo.aspx' </script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('考生信息确认失败！'); window.location.href='jsxm_ShowInfo.aspx'</script>");
                    }

                }
                else
                {
                    Response.Write("<script>alert('超时请重新登录！'); window.location.href='/' </script>");
                }
            }
            else
            {
                Response.Write("<script>alert('超时请重新登录！'); window.location.href='/' </script>");
            }

        }

        /// <summary>
        /// 返回上一页
        /// </summary> 
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("jsxm_Help.aspx", false);
        }


    }
}