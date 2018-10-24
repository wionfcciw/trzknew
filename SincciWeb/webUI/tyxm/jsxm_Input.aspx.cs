using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
using Model;
using BLL.system;
namespace SincciKC.webUI.tyxm
{
    public partial class jsxm_Input : SPage //wf 2018-08-16 修改，单点登录统一控制 System.Web.UI.Page
    {
        public string ksh = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["kaosheng"] != null)
                {
                    if (BLL_Ks_Session.ksLogCheck())
                    {

                       // this.lblksh.Text = BLL_Ks_Session.ksSession().ksh;
                       // this.txtxm.Text = BLL_Ks_Session.ksSession().xm;
                        ksh = BLL_Ks_Session.ksSession().ksh;
                        string xqdm = ksh.Substring(2, 4);
                        if (!new Bll_zkbm_Time().zkbm_time(xqdm,4))
                        {
                            Response.Write("<script>alert('现在不是网上报名时间！'); history.back(); </script>");
                        }
                        else
                        {
                            ShowLoad();
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


        private void ShowLoad()
        {
            DataTable kstab = bll.selecksh(ksh);
            if (kstab.Rows[0]["ksjsqr"].ToString()=="2")
            {
                Response.Redirect("jsxm_ShowInfo.aspx", false);
            }

            lblbmd.Text = kstab.Rows[0]["bmdmc"].ToString();
            lblkaoci.Text = kstab.Rows[0]["kaocimc"].ToString();
            lblksh.Text = kstab.Rows[0]["ksh"].ToString();
            lblxm.Text = kstab.Rows[0]["xm"].ToString();
            lblsfzh.Text = kstab.Rows[0]["sfzh"].ToString();
            lblbj.Text = kstab.Rows[0]["bjmc"].ToString();
           
           
            zxxmLoad();
            if (kstab.Rows[0]["bxxm"].ToString() != "")//自
            {
                rdozixuan.SelectedValue = kstab.Rows[0]["bxxm"].ToString();
            }
           
            
 
        }
        
        /// <summary>
        /// 自选
        /// </summary>
        private void zxxmLoad()
        {
            
            DataTable dt = bll.seleczk_jsks(" 1=1");
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                if (k == dt.Rows.Count - 1)
                {
                    ListItem lis = new ListItem(dt.Rows[k]["name"].ToString(), dt.Rows[k]["id"].ToString());
                    rdozixuan.Items.Add(lis);
                }
                else
                {
                    ListItem lis = new ListItem(dt.Rows[k]["name"].ToString() + " | ", dt.Rows[k]["id"].ToString());
                    rdozixuan.Items.Add(lis);
                }
            }
        }
       
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            
                if (rdozixuan.SelectedValue == "")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
        "<script>ymPrompt.succeedInfo({message:'请选择\"加试专业 ,title:'操作提示'});</script>");
          
                }
                else
                {
                    string zix = rdozixuan.SelectedValue;
                    DataTable dt = bll.selzk_ksjsks(" ksh='" + BLL_Ks_Session.ksSession().ksh + "'");
                    bool resu = false;
                    if (dt.Rows.Count == 0 || dt == null)
                    {
                         resu = bll.Insert_zk_ksjsks(BLL_Ks_Session.ksSession().ksh, Convert.ToInt32(zix));
                    }
                    else
                    {
                        resu = bll.Update_zk_ksjsks(zix, BLL_Ks_Session.ksSession().ksh);
                    }
                    
                    if (resu)
                        Response.Redirect("jsxm_ShowInfo.aspx");
                    else
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
      "<script>ymPrompt.succeedInfo({message:'保存失败！' ,title:'操作提示'});</script>");
     
     
                }
     
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("tyxm_Help.aspx", false);
        }

     

        
    }
}