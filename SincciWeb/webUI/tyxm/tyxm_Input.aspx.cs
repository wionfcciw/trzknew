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
    public partial class tyxm_Input : SPage //wf 2018-08-16 修改，单点登录统一控制 System.Web.UI.Page
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
                        if (!new Bll_zkbm_Time().zkbm_time(xqdm,3))
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
        private BLL_tyxm bll = new BLL_tyxm();


        private void ShowLoad()
        {
            DataTable kstab = bll.selecksh(ksh);
            if (kstab.Rows[0]["kstyqr"].ToString()=="2")
            {
                Response.Redirect("tyxm_ShowInfo.aspx", false);
            }

            lblbmd.Text = kstab.Rows[0]["bmdmc"].ToString();
            lblkaoci.Text = kstab.Rows[0]["kaocimc"].ToString();
            lblksh.Text = kstab.Rows[0]["ksh"].ToString();
            lblxm.Text = kstab.Rows[0]["xm"].ToString();
            lblsfzh.Text = kstab.Rows[0]["sfzh"].ToString();
            lblbj.Text = kstab.Rows[0]["bjmc"].ToString();
            lblxb.Text = kstab.Rows[0]["xbmc"].ToString();
            if (kstab.Rows[0]["cdxm"].ToString() == "")//抽
                bll.Insert_zk_kstyks(BLL_Ks_Session.ksSession().ksh, 1);
            //if (kstab.Rows[0]["cdxm"].ToString() != "")//抽
            //{
            //    btnchoud.Visible = false;
            //    chouding.InnerHtml = kstab.Rows[0]["cdmc"].ToString();
            //    lblcdz.Text = "您已抽取完成!";
            //}
            //else
            //{
            //    cdxmLoad();
            //}
            //if (kstab.Rows[0]["bxxm"].ToString() != "")//必
            //{
            //    bikao.InnerHtml = kstab.Rows[0]["bxmc"].ToString();
            //}
            //else
            //{
            //    bkxmLoad();
            //}
            zxxmLoad();
            if (kstab.Rows[0]["zxxm"].ToString() != "")//自
            {
                rdozixuan.SelectedValue = kstab.Rows[0]["zxxm"].ToString();
            }

            bxmLoad();
            if (kstab.Rows[0]["bxm"].ToString() != "" && kstab.Rows[0]["bxm"].ToString() != "0")//备选
            {
               rdobeixuan.SelectedValue = kstab.Rows[0]["bxm"].ToString();
            }
            zx3Load();
            if (kstab.Rows[0]["zxxm3"].ToString() != "" && kstab.Rows[0]["zxxm3"].ToString() != "0")//备选
            {
                rdozixuan3.SelectedValue = kstab.Rows[0]["zxxm3"].ToString();
            }
 
        }
        ///// <summary>
        ///// 必考
        ///// </summary>
        //private void bkxmLoad()
        //{
        //    string str = "";
        //    DataTable dt = bll.seleczk_tyks(" lxid=1" );
        //    for (int j = 0; j < dt.Rows.Count; j++)
        //    {
        //        str += (j + 1) + "、" + dt.Rows[j]["name"];
        //    }
        //    bikao.InnerHtml = str;
        //}
        ///// <summary>
        ///// 抽定
        ///// </summary>
        //private void cdxmLoad()
        //{
        //    string str = "";
        //    DataTable dt = bll.seleczk_tyks(" lxid=2");
        //    for (int j = 0; j < dt.Rows.Count; j++)
        //    {
        //        str += (j + 1) + "、" + dt.Rows[j]["name"];
        //    }
        //    chouding.InnerHtml = str;
        //}
        /// <summary>
        /// 自选
        /// </summary>
        private void zxxmLoad()
        {
            
            DataTable dt = bll.seleczk_tyks(" lxid=3");
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
        /// 备选
        /// </summary>
        private void bxmLoad()
        {

            DataTable dt = bll.seleczk_tyks(" lxid=4");
            for (int k = 0; k < dt.Rows.Count; k++)
            {
             if (k == dt.Rows.Count - 1)
                {
                    ListItem lis = new ListItem(dt.Rows[k]["name"].ToString(), dt.Rows[k]["id"].ToString());
                    rdobeixuan.Items.Add(lis);
                }
                else
                {
                    ListItem lis = new ListItem(dt.Rows[k]["name"].ToString() + " | ", dt.Rows[k]["id"].ToString());
                    rdobeixuan.Items.Add(lis);
                }
            }
            //ListItem lisv = new ListItem("取消选择", "");
            //rdobeixuan.Items.Add(lisv);
        }
        /// <summary>
        /// 自选3
        /// </summary>
        private void zx3Load()
        {

            DataTable dt = bll.seleczk_tyks(" lxid=5");
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                if (k == dt.Rows.Count - 1)
                {
                    ListItem lis = new ListItem(dt.Rows[k]["name"].ToString(), dt.Rows[k]["id"].ToString());
                    rdozixuan3.Items.Add(lis);
                }
                else
                {
                ListItem lis = new ListItem(dt.Rows[k]["name"].ToString() + " | ", dt.Rows[k]["id"].ToString());
                rdozixuan3.Items.Add(lis);
                }
            }
            //ListItem lisv = new ListItem("取消选择", "");
            //rdozixuan3.Items.Add(lisv);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
     //       if (bll.selzk_kstyks(" ksh='" + BLL_Ks_Session.ksSession().ksh + "'").Rows.Count == 0)
     //       {
     //           Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
     //"<script>ymPrompt.succeedInfo({message:'请先随机抽取\"抽定项目\"！' ,title:'操作提示'});</script>");
     //           return;
     //       }
            //if (btnchoud.Visible)
            //{
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
            //  "<script>ymPrompt.succeedInfo({message:'请先随机抽取\"抽定项目\"！' ,title:'操作提示'});</script>");
            //}
            //else
            //{
                if (rdozixuan.SelectedValue == "")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
        "<script>ymPrompt.succeedInfo({message:'请选择\"自选项目1\"！' ,title:'操作提示'});</script>");
                    return;
                }
                if (rdobeixuan.SelectedValue == "")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
        "<script>ymPrompt.succeedInfo({message:'请选择\"自选项目2\"！' ,title:'操作提示'});</script>");
                    return;
                }
                if (rdozixuan3.SelectedValue == "")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
        "<script>ymPrompt.succeedInfo({message:'请选择\"自选项目3\"！' ,title:'操作提示'});</script>");
                    return;
                }
                    string zix = rdozixuan.SelectedValue;
                    string bx = rdobeixuan.SelectedValue;
                    string zix3 = rdozixuan3.SelectedValue;
                    string bxid = bll.seleczk_tyks(" lxid=1").Rows[0]["id"].ToString();
                    bool resu = bll.Update_zk_kstyks(bxid, bx, zix,zix3, BLL_Ks_Session.ksSession().ksh);
                    if (resu)
                        Response.Redirect("tyxm_ShowInfo.aspx");
                    else
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
      "<script>ymPrompt.succeedInfo({message:'保存失败！' ,title:'操作提示'});</script>");
     
     
              
          //  }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("tyxm_Help.aspx", false);
        }
        /// <summary>
        /// 随机抽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnchoud_Click(object sender, EventArgs e)
        {
            ////得到项目
            //DataTable dt = bll.seleczk_tyks(" lxid=2");  
           
            //Random rd = new Random();
            //int sjs = rd.Next(dt.Rows.Count);
            ////插入数据库
            //bool result = bll.Insert_zk_kstyks(BLL_Ks_Session.ksSession().ksh, Convert.ToInt32(dt.Rows[sjs]["id"]));
            //if (result)
            //{
            //    btnchoud.Visible = false;
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
            //        "<script>ymPrompt.succeedInfo({message:'您已经随机抽签成功！' ,title:'操作提示'});setTimeout(function(){window.parent.location.href=window.parent.location.href;},1000);</script>");
            //}
            //else
            //{
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
            //        "<script>ymPrompt.succeedInfo({message:'随机抽取失败！' ,title:'操作提示'});</script>");
            //}
        }
        /// <summary>
        /// 自选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rdozixuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdobeixuan.SelectedValue != "")
            {
                if (rdobeixuan.SelectedItem.Text.Replace("|", "").Trim() == rdozixuan.SelectedItem.Text.Replace("|", "").Trim())
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'不能与自选项目2相同!' ,width:280,height:180,title:'提示'});</script>");
                    rdozixuan.SelectedItem.Selected = false;
                    return;
                }
            }
            if (rdozixuan3.SelectedValue != "")
            {
                if (rdozixuan3.SelectedItem.Text.Replace("|", "").Trim() == rdozixuan.SelectedItem.Text.Replace("|", "").Trim())
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'不能与自选项目3相同!' ,width:280,height:180,title:'提示'});</script>");
                    rdozixuan.SelectedItem.Selected = false;
                    return;
                }
            }
            if (rdozixuan.SelectedItem.Text.Replace("|", "").Trim().IndexOf("男") != -1)
            {
                if (lblxb.Text == "女")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'该项只能由男生选择!' ,width:280,height:180,title:'提示'});</script>");
                    rdozixuan.SelectedItem.Selected = false;
                    return;
                }
            }
            if (rdozixuan.SelectedItem.Text.Replace("|", "").Trim().IndexOf("女") != -1)
            {
                if (lblxb.Text == "男")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'该项只能由女生选择!' ,width:280,height:180,title:'提示'});</script>");
                    rdozixuan.SelectedItem.Selected = false;
                    return;
                }
            }
        }
        /// <summary>
        /// 备选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rdobeixuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdozixuan.SelectedValue != "")
            {
                if (rdobeixuan.SelectedItem.Text.Replace("|", "").Trim() == rdozixuan.SelectedItem.Text.Replace("|", "").Trim())
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'不能与自选项目1相同!' ,width:280,height:180,title:'提示'});</script>");
                    rdobeixuan.SelectedItem.Selected = false;
                    return;
                }
            }
            if (rdozixuan3.SelectedValue != "")
            {
                if (rdobeixuan.SelectedItem.Text.Replace("|", "").Trim() == rdozixuan3.SelectedItem.Text.Replace("|", "").Trim())
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'不能与自选项目3相同!' ,width:280,height:180,title:'提示'});</script>");
                    rdobeixuan.SelectedItem.Selected = false;
                    return;
                }
            }
            if (rdobeixuan.SelectedItem.Text.Replace("|", "").Trim().IndexOf("男") != -1)
            {
                if (lblxb.Text == "女")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'该项只能由男生选择!' ,width:280,height:180,title:'提示'});</script>");
                    rdobeixuan.SelectedItem.Selected = false;
                    return;
                }
            }
            if (rdobeixuan.SelectedItem.Text.Replace("|", "").Trim().IndexOf("女") != -1)
            {
                if (lblxb.Text == "男")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'该项只能由女生选择!' ,width:280,height:180,title:'提示'});</script>");
                    rdobeixuan.SelectedItem.Selected = false;
                    return;
                }
            }
        }
        /// <summary>
        /// 自选3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rdozixuan3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdobeixuan.SelectedValue != "")
            {
                if (rdobeixuan.SelectedItem.Text.Replace("|", "").Trim() == rdozixuan3.SelectedItem.Text.Replace("|", "").Trim())
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'不能与自选项目2相同!' ,width:280,height:180,title:'提示'});</script>");
                    rdozixuan3.SelectedItem.Selected = false;
                    return;
                }
            }
            if (rdozixuan3.SelectedValue != "")
            {
                if (rdozixuan.SelectedItem.Text.Replace("|", "").Trim() == rdozixuan3.SelectedItem.Text.Replace("|", "").Trim())
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'不能与自选项目1相同!' ,width:280,height:180,title:'提示'});</script>");
                    rdozixuan3.SelectedItem.Selected = false;
                    return;
                }
            }
            if (rdozixuan3.SelectedItem.Text.Replace("|", "").Trim().IndexOf("男") != -1)
            {
                if (lblxb.Text == "女")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'该项只能由男生选择!' ,width:280,height:180,title:'提示'});</script>");
                    rdozixuan3.SelectedItem.Selected = false;
                    return;
                }
            }
            if (rdozixuan3.SelectedItem.Text.Replace("|", "").Trim().IndexOf("女") != -1)
            {
                if (lblxb.Text == "男")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'该项只能由女生选择!' ,width:280,height:180,title:'提示'});</script>");
                    rdozixuan3.SelectedItem.Selected = false;
                    return;
                }
            }
        }
       

        
    }
}