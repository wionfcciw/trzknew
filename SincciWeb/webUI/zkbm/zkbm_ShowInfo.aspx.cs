using System;
using System.Collections.Generic;
 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
using BLL.system;
namespace SincciKC.webUI.zkbm
{
    public partial class zkbm_ShowInfo : SPage //wf 2018-08-16 修改，单点登录统一控制 System.Web.UI.Page
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
                        if (!new Bll_zkbm_Time().zkbm_time(xqdm,1))
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


        /// <summary>
        /// 绑定数据
        /// </summary>
        private void OnStart()
        {
            info = new BLL_zk_ksxxgl().ViewDisp(ksh);
            if (info.Ksqr == 1)
            {
                this.btnKSQueren.Visible = true;
            }
            if (info.Ksqr == 2)
            {
                this.btnSave.Visible = false;
            }


            this.SysYear.Text = info.Kaocimc;
            this.lblksh.Text = info.Ksh;
            this.lblxm.Text = info.Xm;            
            this.lblsfzh.Text = info.Sfzh;

            this.lblzzmmdm.Text = info.Zzmmmc;
            this.lblxb.Text = info.Xbmc;
            this.lblmzdm.Text = info.Mzmc;

            this.lblcsrq.Text = info.Csrq;
            this.lbllxdh.Text = info.Lxdh;
            this.lblyddh.Text = info.Yddh;

            this.lblxjh.Text = info.Xjh;
            this.lblxsbh.Text = info.Xsbh;
            this.lblbmddm.Text = info.Bmdmc;

            this.lblxqdm.Text = info.Byzxxqmc;
            this.lblbyzxdm.Text = info.Byzxmc;


            this.lblbjdm.Text = info.Bjmc; 
            this.lblkaoci.Text = info.Kaocimc;

            this.lblkslbdm.Text= info.Kslbmc;

            this.lblhjdq.Text = info.Hjdq;
            this.lblhjdz.Text = info.Hjdz;

            this.lbljtdq.Text = info.Jtdq;
            this.lbljtdz.Text = info.Jtdz;

            this.lblsjr.Text = info.Sjr;
            this.lblyzbm.Text = info.Yzbm;
            this.lblbz.Text = info.Bz;
            this.lblcrhkh.Text = info.Crhkh;

            this.lblbzTitle.Text = new BLL_zk_szzdybz().Disp(info.Bmdxqdm).Bzmc;
        }

        /// <summary>
        /// 修改考生资料
        /// </summary> 
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Response.Redirect("zkbm_Input.aspx", false);
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
                    if (new BLL_zk_ksxxgl().KsQueren(ksh))
                    {
                        Response.Write("<script>alert('确认考生资料成功！'); window.location.href='zkbm_ShowInfo.aspx' </script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('确认考生资料失败！'); window.location.href='zkbm_ShowInfo.aspx'</script>");
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
            Response.Redirect("zkbm_Help.aspx", false);
        }

    }
}