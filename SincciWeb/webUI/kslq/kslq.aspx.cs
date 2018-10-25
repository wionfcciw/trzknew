using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using Model;
using BLL.system;
namespace SincciKC.webUI.kslq
{
    public partial class kslq : SPage //wf 2018-08-16 修改，单点登录统一控制 System.Web.UI.Page
    {
        /// <summary>
        /// 志愿定制BLL
        /// </summary>
        private BLL_zk_zydz bll = new BLL_zk_zydz();
   
        public Model_zk_zkcj model = new Model_zk_zkcj();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["kaosheng"] != null)
                {
                    if (BLL_Ks_Session.ksLogCheck())
                    {

                        string ksh = BLL_Ks_Session.ksSession().ksh;
                        lblksh.Text = ksh;
                        DataTable dpcdt = new DataTable();
                        dpcdt = bll.Select_zy_kszyxx_order(ksh); 
                        if (dpcdt.Rows.Count > 0)
                        {
                            hfdpcdm.Value = dpcdt.Rows[0]["pcdm"].ToString().Substring(0, 1);
                            hfdxxdm.Value = dpcdt.Rows[0]["xxdm"].ToString();
                            //Showrpt(ksh, lblxxdm.Text);
                            //lblbsj.Visible = false;
                            //btnsel.Visible = true;
                            //tim.Enabled = true;
                        }
                        else
                        {
                            div1.Visible = false;
                            lblbsj.Visible = true;
                        }
                     
                    }
                    else
                    {
                        Response.Write("<script>alert('请先登录！'); history.back(); </script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('请先登录！'); history.back(); </script>");
                }
            }
        }
       

     


    }
}