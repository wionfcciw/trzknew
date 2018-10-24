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
    public partial class kslq_Help : SPage //wf 2018-08-16 修改，单点登录统一控制 System.Web.UI.Page
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
                            lblpc.Text = dpcdt.Rows[0]["pcdm"].ToString().Substring(0, 1);
                            lblxxdm.Text = dpcdt.Rows[0]["xxdm"].ToString();
                            //Showrpt(ksh, lblxxdm.Text);
                            //lblbsj.Visible = false;
                            //btnsel.Visible = true;
                            //tim.Enabled = true;
                        }
                        else
                        {
                            div1.Visible = false;
                            lblbsj.Visible = true;
                            btnsel.Visible = false;
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
        /// <summary>
        ///排名情况统计
        /// </summary>
        private void Showrpt(string ksh,string xxdm)
        {
            //DataTable dt = bll.SelectKshPm_xx(ksh, lblpc.Text, xxdm);
            //DataTable tab = bll.Select_syjh(xxdm, ksh.Substring(0, 3));
            //if (tab.Rows.Count > 0)
            //{
            //    int jhs = Convert.ToInt32(tab.Rows[0]["jhs"].ToString());
            //    if (jhs <= 0)
            //    {
            //        lblshow.Visible = true;
            //        lblshow.Text = "（您所填报的招生学校计划已满，请选择其它学校填报！）<br/>";
            //    }
            //    else
            //    {
            //        lblshow.Visible = false;           
            //    }
            //}
            //this.Repeater1.DataSource = dt;
            //this.Repeater1.DataBind();


        }

        protected void tim_Tick(object sender, EventArgs e)
        {
            //DataTable dt = bll.SelectKshPm(lblksh.Text, lblpc.Text);
            //this.Repeater1.DataSource = dt;
            //this.Repeater1.DataBind();
            if (sfload.Text != "0")
            {
                int num = Convert.ToInt32(lblnum.Text);
                if (num == 0)
                {
                    num = 120;
                }
                if (num == 120)
                {
                    btnsel.Enabled = true;
                    tim.Enabled = false;
                    btnsel.Text = " 查询情况 ";
                }
                else
                {
                    btnsel.Text = " 查询情况(" + num + ") ";
                    btnsel.Enabled = false;
                }
                num--;
                lblnum.Text = num.ToString();
            }
            else
                sfload.Text = "1";
        }

       
        /// <summary>
        /// 下一页
        /// </summary> 
        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("Zytb_zsbf.aspx", false);
        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            if (BLL_Ks_Session.ksLogCheck())
            {
                Model_zk_ksSession model = BLL_Ks_Session.ksSession();
                string Zkzh = model.Zkzh;
                string bmddm = model.Bmddm;
                string ksh = model.ksh;
                string str = config.CheckChar(txtpwd.Text.Trim().ToString());
                if (Zkzh == txtpwd.Text)
                {
                    lblzkzh.Visible = false;
                    DataTable dt2 = bll.Select_lqjg(ksh);
                    if (dt2.Rows.Count > 0)
                    {
                        this.Repeater2.DataSource = dt2;
                        this.Repeater2.DataBind();
                        tim.Enabled = true;
                    }
                    else
                    {
                        DataTable tab = bll.Select_syjh(lblxxdm.Text, model.Bmdxqdm);
                        if (tab.Rows.Count > 0)
                        {
                            int jhs = Convert.ToInt32(tab.Rows[0]["jhs"].ToString());
                            if (jhs <= 0)
                            {
                                lblshow.Visible = true;
                                lblshow.Text = "（您所填报的招生学校计划已满，请选择其它学校填报！）<br/>";
                            }
                            else
                            {
                                DataTable dt = bll.SelectKshPm_xx(ksh, lblpc.Text, lblxxdm.Text, bmddm, model.Bmdxqdm);
                                this.Repeater1.DataSource = dt;
                                this.Repeater1.DataBind();
                                lblshow.Visible = false;
                            }
                        }
                        tim.Enabled = true;
                    }
                }
                else
                {
                    lblzkzh.Visible = true;
                }
            }
            else
            {
                Response.Write("<script>alert('登录超时，请重新登录！'); history.back(); </script>");
            }
        }

        ///// <summary>
        ///// 退出系统
        ///// </summary> 
        //protected void btnExit_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("/webUI/Exit.aspx", false);
        //}


    }
}