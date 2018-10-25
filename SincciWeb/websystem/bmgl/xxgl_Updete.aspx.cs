using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using System.Data;

namespace SincciKC.websystem.bmgl
{
    public partial class xxgl_Updete : BPage
    {
        public string ksh = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ksh =config.CheckChar(Request.QueryString["ksh"].ToString());
                sjrd();
                OnStart();
                int UserType = SincciLogin.Sessionstu().UserType;
                if (UserType == 1)
                {
                    txtcrhkh.Enabled = true;
                    txtxjh.Enabled = true;
                }
            }
        }

        #region 绑定数据

        /// <summary>
        /// 绑定数据字典数据
        /// </summary>
        private void sjrd()
        {
            BLL_zk_zdxx zdxx = new BLL_zk_zdxx();

            this.ddlxb.DataSource = zdxx.selectData("xb");
            this.ddlxb.DataTextField = "zlbmc";
            this.ddlxb.DataValueField = "zlbdm";
            this.ddlxb.DataBind();
            this.ddlxb.Items.Insert(0, new ListItem("请选择", ""));

            this.ddlmzdm.DataSource = zdxx.selectData("mz");
            this.ddlmzdm.DataTextField = "zlbmc";
            this.ddlmzdm.DataValueField = "zlbdm";
            this.ddlmzdm.DataBind();
            this.ddlmzdm.Items.Insert(0, new ListItem("请选择", ""));

            this.ddlzzmmdm.DataSource = zdxx.selectData("zzmm");
            this.ddlzzmmdm.DataTextField = "zlbmc";
            this.ddlzzmmdm.DataValueField = "zlbdm";
            this.ddlzzmmdm.DataBind();
            this.ddlzzmmdm.Items.Insert(0, new ListItem("请选择", ""));



            this.ddlkslbdm.DataSource = zdxx.selectData("KSLB");
            this.ddlkslbdm.DataTextField = "zlbmc";
            this.ddlkslbdm.DataValueField = "zlbdm";
            this.ddlkslbdm.DataBind();
            this.ddlkslbdm.Items.Insert(0, new ListItem("请选择", ""));

            this.ddlzjlb.DataSource = zdxx.selectData("zjlb");
            this.ddlzjlb.DataTextField = "zlbmc";
            this.ddlzjlb.DataValueField = "zlbdm";
            this.ddlzjlb.DataBind();


            BLL_zk_xqdm xqdm = new BLL_zk_xqdm();
            //毕业中学县区不要3区
            string strxq = " and xqdm not in ('3202','3203','3204')";
            this.ddlxqdm.DataSource = xqdm.selectxqdmKs(" right(xqdm,2) not in('00') " + strxq);
            this.ddlxqdm.DataTextField = "xqmc";
            this.ddlxqdm.DataValueField = "xqdm";
            this.ddlxqdm.DataBind();
            this.ddlxqdm.Items.Insert(0, new ListItem("请选择", ""));
            //有三区的不要市区
            string strhj = " and xqdm != '0601'";
            this.ddlhjdq.DataSource = xqdm.selectxqdmKs(" right(xqdm,2) not in('00') " + strhj);
            this.ddlhjdq.DataTextField = "xqmc";
            this.ddlhjdq.DataValueField = "xqdm";
            this.ddlhjdq.DataBind();
            this.ddlhjdq.Items.Insert(0, new ListItem("请选择", ""));

            this.ddljtdq.DataSource = xqdm.selectxqdmKs(" right(xqdm,2) not in('00') " + strhj);
            this.ddljtdq.DataTextField = "xqmc";
            this.ddljtdq.DataValueField = "xqdm";
            this.ddljtdq.DataBind();
            this.ddljtdq.Items.Insert(0, new ListItem("请选择", ""));
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        private void OnStart()
        {
            Model_zk_ksxxgl info = new BLL_zk_ksxxgl().ViewDisp(ksh);

           
            this.lblksh.Text = info.Ksh;
            this.txtxm.Text = info.Xm;
            this.ddlzjlb.SelectedValue = info.Zjlb.ToString();
            this.txtsfzh.Text = info.Sfzh;

            this.ddlzzmmdm.SelectedValue = info.Zzmmdm;
            this.ddlxb.SelectedValue = info.Xbdm.ToString();
            this.ddlmzdm.SelectedValue = info.Mzdm;

            this.txtcsrq.Text = info.Csrq;
            this.txtlxdh.Text = info.Lxdh;
            this.txtyddh.Text = info.Yddh;

            this.txtxjh.Text = info.Xjh;
            this.lblxsbh.Text = info.Xsbh;
            this.lblbmddm.Text = info.Bmdmc;
            ddbj(info.Bmddm);

            //毕业学校
            string byzxdm = info.Byzxdm;
            string xqdm = "";

            if (byzxdm.Length > 0)
            {
                xqdm = byzxdm.Substring(0, 4);
                this.ddlxqdm.SelectedValue = xqdm;
                ByxxdmDll(xqdm);
                this.ddlbyzxdm.SelectedValue = info.Byzxdm;
                this.txtbyzxdm.Text = info.Byzxmc;
            }
            else //设置默认值
            {
                xqdm = info.Bmdxqdm;
               
                this.ddlxqdm.SelectedValue = xqdm;
                ByxxdmDll(xqdm);
                byzxdm = info.Bmddm;
                this.ddlbyzxdm.SelectedValue = info.Bmddm;
                // this.txtbyzxdm.Text = info.Byzxmc;
            }
           // ddbj(byzxdm);
            this.ddlbjdm.SelectedValue = info.Bjdm;
           // this.lblbjdm.Text = info.Bjdm;

            this.lblkaoci.Text = info.Kaocimc;
            this.ddlkslbdm.SelectedValue = info.Kslbdm;



            this.ddlhjdq.SelectedValue = info.Hjdqdm;
            this.txthjdz.Text = info.Hjdz;
            this.HDhjdq.Value = info.Hjdq;

            this.ddljtdq.SelectedValue = info.Jtdqdm;
            this.txtjtdz.Text = info.Jtdz;
            this.HDjtdq.Value = info.Jtdq;

            this.txtsjr.Text = info.Sjr;
            this.txtyzbm.Text = info.Yzbm;
            this.txtbz.Text = info.Bz;

            this.txtcrhkh.Text = info.Crhkh;
            this.lblbzTitle.Text = new BLL_zk_szzdybz().Disp(info.Bmdxqdm).Bzmc;



            //if (info.Ksqr == 2)
            //{
            //    this.btnSave.Visible = false;
            //    Response.Redirect("zkbm_ShowInfo.aspx", false);
            //}

        }

        /// <summary>
        /// 显示毕业中学数据
        /// </summary> 
        protected void ddlxqdm_SelectedIndexChanged(object sender, EventArgs e)
        {
            string xqdm = this.ddlxqdm.SelectedValue;
            ByxxdmDll(xqdm);
        }
        /// <summary>
        /// 通过县区代码查找毕业学校
        /// </summary> 
        private void ByxxdmDll(string xqdm)
        {
            DataTable dt = new BLL_zk_xxdm().Select_xxdmKs(xqdm);
            if (dt.Rows.Count > 0)
            {
                this.ddlbyzxdm.Visible = true;
                this.txtbyzxdm.Visible = false;
                this.txtbyzxdm.Text = "";

                this.ddlbyzxdm.DataSource = dt;
                this.ddlbyzxdm.DataTextField = "xxmc";
                this.ddlbyzxdm.DataValueField = "xxdm";
                this.ddlbyzxdm.DataBind();
                this.ddlbyzxdm.Items.Insert(0, new ListItem("请选择", ""));


                //this.ddlbjdm.Visible = true;
                //this.lblbjdm.Visible = false;
            }
            else
            {
                this.ddlbyzxdm.Visible = false;
                this.ddlbyzxdm.Items.Clear();
                this.txtbyzxdm.Visible = true;
                //this.ddlbjdm.Items.Clear();
                //this.ddlbjdm.Visible = false;
                //this.lblbjdm.Visible = true;
            }
        }

        #endregion

        #region 保存数据
        /// <summary>
        /// 保存数据
        /// </summary> 
        protected void btnSave_Click(object sender, EventArgs e)
        {

            string ksh = config.CheckChar(Request.QueryString["ksh"].ToString());
            string Xm = config.CheckChar(this.txtxm.Text.Trim().ToString());

                    //证件类别
                    string Zjlb = this.ddlzjlb.SelectedValue;
                    string Sfzh = config.CheckChar(this.txtsfzh.Text.Trim());

                    string Zzmmdm = this.ddlzzmmdm.SelectedValue;
                    int Xbdm = 0;
                    if (this.ddlxb.SelectedValue.Length > 0)
                        Xbdm = Convert.ToInt32(this.ddlxb.SelectedValue);
                    string Mzdm = this.ddlmzdm.SelectedValue;
                    string Csrq = "";
                    if (Request.Form["txtcsrq"] != null)
                        Csrq = Request.Form["txtcsrq"].Trim();
                    string Lxdh = config.CheckChar(this.txtlxdh.Text.Trim().ToString());
                    string Yddh = config.CheckChar(this.txtyddh.Text.Trim().ToString());

                    string Xjh = config.CheckChar(this.txtxjh.Text.Trim().ToString());

                    string Byzxdm = this.ddlbyzxdm.SelectedValue;
                    string Byzxmc = "";
                    if (Byzxdm.Length > 0)
                    {
                        Byzxmc = this.ddlbyzxdm.SelectedItem.ToString();
                    }
                    else
                    {
                        Byzxdm = this.ddlxqdm.SelectedValue + "99";
                        Byzxmc = this.txtbyzxdm.Text.Trim().ToString();
                    }
                    string Bjdm = this.ddlbjdm.SelectedValue;
                    if (Bjdm.Length == 0)
                        Bjdm = this.lblbjdm.Text.Trim();

                    string Kslb = this.ddlkslbdm.SelectedValue;

                    string Hjdq = this.ddlhjdq.SelectedItem.ToString();
                    string Hjdqdm = this.ddlhjdq.SelectedValue;
                    string Hjdz = config.CheckChar(this.txthjdz.Text.Trim().ToString());

                    string Jtdq = this.ddljtdq.SelectedItem.ToString();
                    string Jtdqdm = this.ddljtdq.SelectedValue;
                    string Jtdz = config.CheckChar(this.txtjtdz.Text.Trim().ToString());


                    string Sjr = config.CheckChar(this.txtsjr.Text.Trim().ToString());
                    string Yzbm = config.CheckChar(this.txtyzbm.Text.Trim().ToString());


                    string Bz = config.CheckChar(this.txtbz.Text.Trim().ToString());
                    string Crhkh = config.CheckChar(this.txtcrhkh.Text.Trim().ToString());


                    Model_zk_ksxxgl item = new Model_zk_ksxxgl();

                    item.Ksh = ksh;
                    item.Xm = Xm;
                    item.Zjlb = Zjlb;
                    item.Sfzh = Sfzh;

                    item.Zzmmdm = Zzmmdm;
                    item.Xbdm = Xbdm;
                    item.Mzdm = Mzdm;

                    item.Csrq = Csrq;
                    item.Lxdh = Lxdh;
                    item.Yddh = Yddh;

                    item.Xjh = Xjh;
                    item.Byzxdm = Byzxdm;
                    item.Byzxmc = Byzxmc;
                    item.Bjdm = Bjdm;

                    item.Kslbdm = Kslb;

                    item.Hjdq = Hjdq;
                    item.Hjdqdm = Hjdqdm;
                    item.Hjdz = Hjdz;
                    item.Jtdq = Jtdq;
                    item.Jtdqdm = Jtdqdm;
                    item.Jtdz = Jtdz;

                    item.Sjr = Sjr;
                    item.Yzbm = Yzbm;

                    item.Bz = Bz;
                    item.Crhkh = Crhkh;
                    if (SincciLogin.Sessionstu().UserType != 1)
                    {
                        item.Ksqr = 1;
                        item.Ksqrsj = DateTime.Now;
                    }
                    else
                    {
                        Model_zk_ksxxgl mm = new BLL_zk_ksxxgl().ViewDisp(ksh);
                        item.Ksqr = mm.Ksqr;
                        item.Ksqrsj = mm.Ksqrsj;
                    }
                  

                    if (checkinfo(item))
                    {
                        if (new BLL_zk_ksxxgl().zk_ksxxglEdit(item))
                        {
                            string E_record = "修改: 考生数据：" + item.Ksh + "";
                            EventMessage.EventWriteDB(1, E_record);
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.succeedInfo({message:'操作成功！' ,title:'操作提示'});</script>");
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.succeedInfo({message:'操作失败！' ,title:'提示'});</script>");
             
                        }
                    }
              
        }
        //检查数据完整性
        private bool checkinfo(Model_zk_ksxxgl item)
        {

            //判断
            string Errmsg = "";

            if (item.Xm.Length == 0)
                Errmsg += "·姓名为能为空！ <br>";
            if (item.Sfzh.Length == 0)
                Errmsg += "·证件号码不能为空！<br>";

            if (item.Zzmmdm.Length == 0)
                Errmsg += "·请选择政治面貌！<br>";
            if (item.Xbdm <= 0)
                Errmsg += "·请选择性别！ <br>";
            if (item.Mzdm.Length == 0)
                Errmsg += "·请选择民族！ <br>";


            if (item.Csrq.Length == 0)
                Errmsg += "·出生日期不能为空！<br>";

            if (item.Yddh.Length == 0)
                Errmsg += "·家长移动电话不能为空！ <br>";
            if (item.Lxdh == item.Yddh)
                Errmsg += "·家庭固定电话和家长移动电话不能相同！ <br>";

            //if (item.Xjh.Length == 0)
            //    Errmsg += "·请学籍号不能为空！ <br>";

            if (item.Byzxmc.Length == 0)
                Errmsg += "·请毕业中学不能为空！ <br>";
            //if (item.Bjdm.Length == 0)
            //    Errmsg += "·请班级不能为空！ <br>";


            if (item.Hjdq.Replace("请选择", "").Length == 0)
                Errmsg += "·户籍所在地所属地区不能为空！ <br>";
           
            if (item.Hjdz.Length == 0)
                Errmsg += "·户籍所在地详细地址不能为空！ <br>";

            if (item.Hjdz.Length != 0 && item.Hjdq.Replace("请选择", "").Length != 0)
            {
                if (item.Hjdz.Substring(0, item.Hjdq.Length) != item.Hjdq)
                    Errmsg += "·户籍所在县区与选择不一致！ <br>";
            }
            

            if (item.Jtdq.Replace("请选择", "").Length == 0)
                Errmsg += "·通讯地址地区不能为空！ <br>";
            if (item.Jtdz.Length == 0)
                Errmsg += "·通讯地址详细地址不能为空！ <br>";
            if (item.Jtdz.Length != 0 && item.Jtdq.Replace("请选择", "").Length != 0)
            {
                if (item.Jtdz.Substring(0, item.Jtdq.Length) != item.Jtdq)
                    Errmsg += "·通讯地址所在县区与选择不一致！ <br>";
            }
          

            if (item.Yzbm.Length == 0)
                Errmsg += "·邮政编码不能为空！ <br>";
            if (item.Sjr.Length == 0)
                Errmsg += "·收件人不能为空！ <br>";


            if (Errmsg.Length > 0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'" + Errmsg + "' ,width:350,height:310,title:'提示'});</script>");
                return false;
            }
            else
            {
                if (systemparam.sfzhTag == 1)
                {
                    //检测身份证号有没有重复。                    
                    if (new BLL_zk_ksxxgl().checksfzh(item.Sfzh, item.Ksh))
                    {

                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'身份证号已被使用!' ,width:220,height:190,title:'提示'});</script>");
                        return false;
                    }
                }
                ////检测录取通知书邮寄地址 相同个数
                //if (new BLL_zk_ksxxgl().CheckTxdzNumber(item.Txdz) > systemparam.lqtzsNumber)
                //{
                //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'录取通知书邮寄地址相同超过" + systemparam.lqtzsNumber + "个!' ,width:220,height:190,title:'提示'});</script>");
                //    return false;
                //}
                ////联系电话 最多相同个数
                //if (new BLL_zk_ksxxgl().CheckLxdhNumber(item.Lxdh) > systemparam.lxdhNumber)
                //{
                //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'录取通知书邮寄地址相同超过" + systemparam.lqtzsNumber + "个!' ,width:220,height:190,title:'提示'});</script>");
                //    return false;
                //}

            }
            return true;

        }

        #endregion

 
        /// <summary>
        /// 选择学校显示班级信息
        /// </summary> 
        protected void ddlbyzxdm_SelectedIndexChanged(object sender, EventArgs e)
        {
            string xxdm = this.ddlbyzxdm.SelectedValue;
          //  ddbj(xxdm);

        }

        private void ddbj(string xxdm)
        {
            ddlbjdm.DataSource = new BLL_zk_bjdm().Select_zk_bjdm(xxdm);
            ddlbjdm.DataTextField = "bjmc";
            ddlbjdm.DataValueField = "bjdm";
            ddlbjdm.DataBind();
            this.ddlbjdm.Items.Insert(0, new ListItem("-请选择-", ""));
        }

        /// <summary>
        /// 选择户籍所在地
        /// </summary> 
        protected void ddlhjdq_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlhjdq.SelectedValue.Length > 0)
            {
                this.txthjdz.Text = this.ddlhjdq.SelectedItem.ToString();
                this.HDhjdq.Value = this.ddlhjdq.SelectedItem.ToString();
            }
        }
        /// <summary>
        /// 选择通讯地址
        /// </summary> 
        protected void ddljtdq_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddljtdq.SelectedValue.Length > 0)
            {
                this.txtjtdz.Text = this.ddljtdq.SelectedItem.ToString();
                this.HDjtdq.Value = this.ddljtdq.SelectedItem.ToString();
            }
        }
    }
}