using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using Model;

namespace SincciKC
{
    /// <summary>
    /// 投档定制。
    /// </summary>
    public partial class TouDangDinZiWebForm : BPage
    {
        /// <summary>
        /// 页面加载时。
        /// </summary>
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
                loadPcInfo();
            }
        }

        /// <summary>
        /// 加载批次信息。
        /// </summary>
        private void loadPcInfo()
        {
            BLL_zk_Pctd_tj_Info zk = new BLL_zk_Pctd_tj_Info();
            DataTable tab=zk.selectPcdm();
            for (int i = 0; i < tab.Rows.Count; i++)
            {
                TreeNode node = new TreeNode();
                node.Text = tab.Rows[i]["xpc_mc"].ToString();
                node.Value = tab.Rows[i]["xpc_id"].ToString();
                this.tvPcInfo.Nodes.Add(node);
            }
            this.tvPcInfo.ExpandAll();
        }

        /// <summary>
        /// 加载批次投档定制的条件。
        /// </summary>
        /// <param name="xpcId">小批次Id</param>
        /// <param name="pcdm">批次代码。</param>
        /// <param name="tjType">条件类型：0、基本条件；1、同分跟进；2、指标生；3、素质评价；4、其他条件；5、国际班控档线</param>
        private void loadPcTdTj(string xpcId,string pcdm, int tjType)
        {
            BLL_zk_Pctd_tj_Info zk = new BLL_zk_Pctd_tj_Info();

            string[] strs = xpcId.Split('_');
            if (strs.Length != 2)
            {
                return;
            }
            switch (tjType)
            {
                case 0://基本条件
                    #region 查询显示
                    //初始化数据。
                    Model_zk_Pc_TouDang_jbtj jbtj = zk.Select_Jbtj(strs[0]);
                    if (jbtj == null)
                    {
                        return;
                    }
                    this.RadioButtonList1.SelectedIndex = jbtj.Tdsf;
                    switch (jbtj.Zdkdx)
                    {
                        case 0:
                            this.RadioButton3.Checked = true;
                            break;
                        case 1:
                            this.RadioButton4.Checked = true;
                            break;
                        case 2:
                            this.RadioButton5.Checked = true;
                            this.TextBox1.Text = jbtj.Zdy_zdfs.ToString();
                            this.TextBox1.Enabled = true;
                            break;
                    }
                    this.RadioButtonList3.SelectedIndex = jbtj.SfZdfd;
                    this.RadioButtonList4.SelectedIndex = jbtj.Zsjhssfxt;
                    #endregion
                    break;
                case 1://同分跟进
                    #region 查询显示
                    Model_zk_Pc_TouDang_tfgj tfgj = zk.Select_Tfgj(strs[0]);
                    if (tfgj == null)
                    {
                        return;
                    }
                    this.RadioButtonList5.SelectedIndex = tfgj.Sftfgj;
                    if (this.RadioButtonList5.SelectedIndex==2)
                    {
                        CheckBoxList1.Enabled = true;

                        foreach (Model_zk_Pc_TouDang_tfgj_bjkm bj in tfgj.Bjkms)
                        {
                            lblcj.Text = "";
                            lblcjid.Text = "";
                            for (int i = 0; i < bj.Kmdm.Split(',').Length; i++)
                            {
                                CheckBoxList1.Items[Convert.ToInt32(bj.Kmdm.Split(',')[i])-1].Selected = true;
                                lblcj.Text = lblcj.Text + ">>" + CheckBoxList1.Items[Convert.ToInt32(bj.Kmdm.Split(',')[i])-1].Text;
                                lblcjid.Text = lblcjid.Text + "," + CheckBoxList1.Items[Convert.ToInt32(bj.Kmdm.Split(',')[i])-1].Value;

                            }
                            //foreach (ListItem item in this.CheckBoxList1.Items)
                            //{
                            //    if (item.Value == bj.Kmdm)
                            //    {
                            //        item.Selected = true;
                            //        break;
                            //    }
                            //}
                        }
                    }
                    else
                    {
                        this.CheckBoxList1.ClearSelection();
                        lblcj.Text = "";
                        lblcjid.Text = "";
                    }
                    #endregion
                    break;
                case 2://指标生
                    #region 查询显示
                    Model_zk_Pc_TouDang_zbs zbs = zk.Select_Zbs(strs[0]);
                    if (zbs == null)
                    {
                        return;
                    }
                    this.RadioButtonList6.SelectedIndex = zbs.Ywzbs;
                    if (zbs.Ywzbs == 0)
                    {
                        this.Panel1.Enabled = true;
                        switch (zbs.Zbslqfsxz)
                        {
                            case 0:
                                this.RadioButton1.Checked = true;
                                break;
                            case 1:
                                this.RadioButton2.Checked = true;
                                this.TextBox2.Text = zbs.Zdyfs.ToString();
                                this.RadioButtonList8.SelectedIndex = zbs.Syzbscl;
                                break;
                        }
                    }
                    #endregion
                    break;
                case 3://素质评价
                    #region 查询显示
                    Model_zk_Pc_TouDang_szpj szpj = zk.Select_Szpj(strs[0]);
                    if (szpj == null)
                    {
                        return;
                    }
                    this.RadioButtonList9.SelectedIndex = szpj.Zhszxztj;
                    if (this.RadioButtonList9.SelectedIndex == 1)
                    {
                        this.DropDownList1.Enabled = true;
                        this.TextBox3.Enabled = true;
                        this.DropDownList3.Enabled = true;

                        this.DropDownList1.SelectedIndex = szpj.Tjlx;
                        this.TextBox3.Text = szpj.Sl.ToString();
                        this.DropDownList3.SelectedItem.Text = szpj.Pjdenji;
                    }
                    #endregion
                    break;
                case 4://其他条件
                    #region 查询显示
                    Model_zk_Pc_TouDang_qttj qttj = zk.Select_Qttj(strs[0]);
                    if (qttj == null)
                    {
                        return;
                    }
                    this.RadioButtonList10.SelectedIndex = qttj.Jscjxzif;
                    this.RadioButtonList11.SelectedIndex = qttj.Ywhcjhb_jscj;
                    this.RadioButtonList12.SelectedIndex = qttj.HkcjhgXz;
                    this.RadioButtonList13.SelectedIndex = qttj.Xbxz == 0 ? 2 : qttj.Xbxz - 1;
                    #endregion
                    break;
                case 5://国际班控档线
                    #region 查询显示
                    List<Model_zk_Pc_TouDang_gjbkdx> gjbkdx = zk.Select_Gjbkdx(strs[0]);
                    if (strs[1] == "3")
                    {
                        this.txt_1.Enabled = true;
                        this.txt_2.Enabled = true;
                        this.txt_3.Enabled = true;
                        this.txt_4.Enabled = true;
                        this.txt_5.Enabled = true;
                    }
                    if (gjbkdx == null)
                    {
                        return;
                    }
                    if (strs[1] == "3")
                    {
                        //表示国际班
                        foreach (Model_zk_Pc_TouDang_gjbkdx item in gjbkdx)
                        {
                            switch (item.Xxdm)
                            {
                                case "8188":
                                    this.txt_1.Text = item.Kdxfs.ToString();
                                    break;
                                case "8288":
                                    this.txt_2.Text = item.Kdxfs.ToString();
                                    break;
                                case "8388":
                                    this.txt_3.Text = item.Kdxfs.ToString();
                                    break;
                                case "8488":
                                    this.txt_4.Text = item.Kdxfs.ToString();
                                    break;
                                case "0288":
                                    this.txt_5.Text = item.Kdxfs.ToString();
                                    break;
                            }
                        }
                    }
                    #endregion
                    break;
            }
        }

        #region 功能关联事件区域。
        /// <summary>
        /// 当改变过滤条件类型的选项时。
        /// </summary>
        protected void tvIfType_SelectedNodeChanged(object sender, EventArgs e)
        {
            FilterShowContent();
        }
        /// <summary>
        /// 过滤显示的内容。
        /// </summary>
        private void FilterShowContent()
        {
            this.tab_0.Visible = false;
            this.tab_1.Visible = false;
            this.tab_2.Visible = false;
            this.tab_3.Visible = false;
            this.tab_4.Visible = false;
            this.tab_5.Visible = false;
            switch (this.tvIfType.SelectedNode.Value)
            {
                case "0"://基本条件。
                    this.tab_0.Visible = true;
                    this.RadioButtonList1.SelectedIndex = 0;
                    this.RadioButton4.Checked = false;
                    this.RadioButton5.Checked = false;
                    this.RadioButton3.Checked = true;
                    this.TextBox1.Text = "";
                    this.RadioButtonList3.SelectedIndex = 1;
                    this.RadioButtonList4.SelectedIndex = 1;
                    loadPcTdTj(this.hf_xpcId.Value, this.hf_pcdm.Value, 0);
                    break;
                case "1"://同分跟进
                    this.tab_1.Visible = true;
                    this.RadioButtonList5.SelectedIndex = 1;
                    this.CheckBoxList1.Enabled = false;
                    this.CheckBoxList1.ClearSelection();
                    loadPcTdTj(this.hf_xpcId.Value, this.hf_pcdm.Value, 1);
                    break;
                case "2"://指标生
                    this.tab_2.Visible = true;
                    this.RadioButtonList6.SelectedIndex = 0;
                    this.RadioButton2.Checked = false;
                    this.RadioButton1.Checked = false;
                    this.Panel1.Enabled = false;
                    this.TextBox2.Enabled = false;
                    this.TextBox2.Text = "";
                    this.RadioButtonList8.Enabled = false;
                    this.RadioButtonList8.ClearSelection();
                    loadPcTdTj(this.hf_xpcId.Value, this.hf_pcdm.Value, 2);
                    break;
                case "3"://素质评价
                    this.tab_3.Visible = true;
                    this.RadioButtonList9.SelectedIndex = 0;
                    this.DropDownList1.SelectedIndex = 0;
                    this.DropDownList1.Enabled = false;
                    this.TextBox3.Text = "";
                    this.TextBox3.Enabled = false;
                    this.DropDownList3.SelectedIndex = 0;
                    this.DropDownList3.Enabled = false;

                    loadPcTdTj(this.hf_xpcId.Value, this.hf_pcdm.Value, 3);
                    break;
                case "4"://其他条件
                    this.tab_4.Visible = true;
                    this.RadioButtonList10.SelectedIndex = 0;
                    this.RadioButtonList11.SelectedIndex = 0;
                    this.RadioButtonList12.SelectedIndex = 0;
                    this.RadioButtonList13.SelectedIndex = 0;
                    loadPcTdTj(this.hf_xpcId.Value, this.hf_pcdm.Value, 4);
                    break;
                case "5"://国际班控档线
                    this.tab_5.Visible = true;
                    this.txt_1.Text = "";
                    this.txt_1.Enabled = false;
                    this.txt_2.Text = "";
                    this.txt_2.Enabled = false;
                    this.txt_3.Text = "";
                    this.txt_3.Enabled = false;
                    this.txt_4.Text = "";
                    this.txt_4.Enabled = false;
                    this.txt_5.Text = "";
                    this.txt_5.Enabled = false;
                    loadPcTdTj(this.hf_xpcId.Value, this.hf_pcdm.Value, 5);
                    break;
            }
        }

        /// <summary>
        /// 当改变同分跟进的方式时。
        /// </summary>
        protected void RadioButtonList5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.RadioButtonList5.SelectedIndex == 2)
            {
                this.CheckBoxList1.Enabled = true;
            }
            else
            {
                this.CheckBoxList1.Enabled = false;
                this.CheckBoxList1.ClearSelection();
                lblcj.Text = "";
                lblcjid.Text = "";

            }
        }

        /// <summary>
        /// 当改变指标生的选项时。
        /// </summary>
        protected void RadioButtonList6_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.RadioButtonList6.SelectedIndex)
            {
                case 0://有指标生
                    this.Panel1.Enabled = true;
                    this.RadioButton1.Enabled = true;
                    this.RadioButton2.Enabled = true;
                    this.RadioButtonList8.Enabled = true;
                    break;
                case 1://无指标生

                    this.Panel1.Enabled = false;
                    this.RadioButton1.Checked = false;
                    this.RadioButton2.Checked = false;
                    this.TextBox2.Text = "";
                    this.RadioButtonList8.Enabled = false;
                    break;
            }
        }

        /// <summary>
        /// 当改变指标生录取分数线限制方式时。
        /// </summary>
        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RadioButton2.Checked)
            {
                this.TextBox2.Enabled = true;
            }
            else
            {
                this.TextBox2.Enabled = false;
            }
        }

        /// <summary>
        /// 当改变素质评价限制条件时。
        /// </summary>
        protected void RadioButtonList9_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.RadioButtonList9.SelectedIndex)
            {
                case 0:
                    this.DropDownList1.Enabled = false;
                    this.TextBox3.Enabled = false;
                    this.DropDownList3.Enabled = false;
                    break;
                case 1:
                    this.DropDownList1.Enabled = true;
                    this.TextBox3.Enabled = true;
                    this.DropDownList3.Enabled = true;
                    break;
            }
        }

        /// <summary>
        /// 当改变最低控制线的限制条件时。
        /// </summary>
        protected void RadioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RadioButton5.Checked)
            {
                this.TextBox1.Enabled = true;
            }
            else
            {
                this.TextBox1.Enabled = false;
                this.TextBox1.Text = "";
            }
        }
        #endregion

        /// <summary>
        /// 数据有效性验证：true、表示数据有误；false、表示数据有效
        /// </summary>
        /// <param name="iFlag">0、基本条件；1、同分跟进；2、指标生；3、素质评价；4、其他条件；5、国际班控档线</param>
        /// <returns></returns>
        private bool ValidateData(int iFlag)
        {
            decimal dec = 0;
            int iTemp = 0;
            bool bFlag = true;
            switch (iFlag)
            {
                case 0://基本条件
                    #region 
                    if (this.RadioButton5.Checked)
                    {
                        if (!decimal.TryParse(this.TextBox1.Text.Trim(), out dec))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请输入指定的最低分数！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},2000); </script>");
                            return true;
                        }
                    }
                    #endregion
                    break;
                case 1://同分跟进
                    #region
                    if (this.RadioButtonList5.SelectedIndex == 2)
                    {
                        bFlag = true;
                        foreach (ListItem item in this.CheckBoxList1.Items)
                        {
                            if (item.Selected)
                            {
                                bFlag = false;
                            }
                        }
                        if (bFlag)
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请选择要比较的科目！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},2000); </script>");
                            return true;
                        }
                    }
                    #endregion
                    break;
                case 2://指标生
                    #region
                    if (this.RadioButton2.Checked)
                    {
                        if (!decimal.TryParse(this.TextBox2.Text.Trim(), out dec))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请输入不小于统招线下的分数！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},2000); </script>");
                            return true;
                        }
                    }
                    #endregion
                    break;
                case 3://素质评价
                    #region
                    if (this.RadioButtonList9.SelectedIndex == 1)
                    {
                        //if (this.DropDownList1.SelectedIndex < 0)
                        //{
                        //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请选择条件关系！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},2000); </script>");
                        //    return true;
                        //}
                        //if (!int.TryParse(this.TextBox3.Text.Trim(), out iTemp))
                        //{
                        //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请输入数量！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},2000); </script>");
                        //    return true;
                        //}

                        //if (this.DropDownList3.SelectedIndex < 0)
                        //{
                        //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请选择每件等级！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},2000); </script>");
                        //    return true;
                        //}
                    }
                    #endregion
                    break;
                case 4://其他条件
                    #region
                    #endregion
                    break;
                case 5://国际班控档线
                    #region
                    #endregion
                    break;
            }
            return false;
        }

        /// <summary>
        /// 点击保存时。
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int ite = -2;
            if (this.tab_0.Visible)
            {
                //基本条件。
                if (ValidateData(0))
                {
                    return;
                }
                ite = saveData(0);
            }
            else if (this.tab_1.Visible)
            {
                //同分跟进。
                if (ValidateData(1))
                {
                    return;
                }
                ite = saveData(1);
            }
            else if (this.tab_2.Visible)
            {
                //指标生。
                if (ValidateData(2))
                {
                    return;
                }
                ite = saveData(2);
            }
            else if (this.tab_3.Visible)
            {
                //素质评价。
                if (ValidateData(3))
                {
                    return;
                }
                ite = saveData(3);
            }
            else if (this.tab_4.Visible)
            {
                //其他条件。
                if (ValidateData(4))
                {
                    return;
                }
                ite = saveData(4);
            }
            else if (this.tab_5.Visible)
            {
                //国际班控档线。
                if (ValidateData(5))
                {
                    return;
                }
                ite = saveData(5);
            }
            else
            {
                //无。
            }
            //-1、表示没有执行操作；0、表示数据有误；1、表示执行成功；2、表示执行失败
            switch (ite)
            {
                case -1: 
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'没有执行任何操作！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},2000); </script>");
                    break;
                case 0:
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'数据验证不通过！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},2000); </script>");
                    break;
                case 1:
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'保存成功！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},2000); </script>");
                    break;
                case 2:
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'保存失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},2000); </script>");
                    break;
            }
        }

        /// <summary>
        /// 保存投档限制条件(-1、表示没有执行操作；0、表示数据有误；1、表示执行成功；2、表示执行失败)。
        /// </summary>
        /// <param name="tjType">条件类型标识：0、基本条件；1、同分跟进；2、指标生；3、素质评价；4、其他条件；5、国际班控档线</param>
        private int saveData(int tjType)
        {
            BLL_zk_Pctd_tj_Info zk = new BLL_zk_Pctd_tj_Info();
            decimal dec=0;
            int iTemp=0;
            switch (tjType)
            {
                case 0://基本条件
                    #region 查询显示
                    Model_zk_Pc_TouDang_jbtj jbtj = new Model_zk_Pc_TouDang_jbtj();
                    jbtj.XpcId = this.hf_xpcId.Value.Split('_')[0];
                    jbtj.Tdsf = this.RadioButtonList1.SelectedIndex;
                    jbtj.Zdkdx = this.RadioButton3.Checked ? 0 : this.RadioButton4.Checked ? 1 : 2;
                    if (jbtj.Zdkdx == 2)
                    {
                        if (!decimal.TryParse(this.TextBox1.Text.Trim(), out dec))
                        {
                            return 0;
                        }
                        jbtj.Zdy_zdfs = dec;
                    }
                    else
                    {
                        jbtj.Zdy_zdfs = 0;
                    }
                    jbtj.SfZdfd = this.RadioButtonList3.SelectedIndex;
                    jbtj.Zsjhssfxt = this.RadioButtonList4.SelectedIndex;

                    return zk.Insert_Jbtj(jbtj) ? 1 : 2;
                    #endregion
                case 1://同分跟进
                    #region 查询显示
                    Model_zk_Pc_TouDang_tfgj tfgj =new Model_zk_Pc_TouDang_tfgj();
                    tfgj.XpcId = this.hf_xpcId.Value.Split('_')[0];
                    tfgj.Sftfgj = this.RadioButtonList5.SelectedIndex;
                    if (tfgj.Sftfgj == 2)
                    {
                        //foreach (ListItem item in this.CheckBoxList1.Items)
                        //{
                        //    Model_zk_Pc_TouDang_tfgj_bjkm info = new Model_zk_Pc_TouDang_tfgj_bjkm();
                        //    info.XpcId = this.hf_xpcId.Value;
                        //    info.Kmdm = item.Value;
                        //    tfgj.Bjkms.Add(info);
                        //}
                        if (lblcjid.Text.Length > 0)
                        {
                            lblcjid.Text = lblcjid.Text.Substring(1);
                        }
                        Model_zk_Pc_TouDang_tfgj_bjkm info = new Model_zk_Pc_TouDang_tfgj_bjkm();
                        info.XpcId = this.hf_xpcId.Value;
                        info.Kmdm = lblcjid.Text;
                        tfgj.Bjkms.Add(info);
                        
                        if (tfgj.Bjkms.Count < 1)
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        tfgj.Bjkms.Clear();
                    }

                    return zk.Insert_Tfgj(tfgj)?1:2;
                    #endregion
                case 2://指标生
                    #region 查询显示
                    Model_zk_Pc_TouDang_zbs zbs = new Model_zk_Pc_TouDang_zbs();
                    zbs.XpcId = this.hf_xpcId.Value.Split('_')[0];
                    zbs.Ywzbs = this.RadioButtonList6.SelectedIndex;
                    zbs.Zbslqfsxz = this.RadioButton1.Checked ? 0 : RadioButton2.Checked ? 1 : -1;
                    if (zbs.Zbslqfsxz == 1)
                    {
                        if (!decimal.TryParse(this.TextBox2.Text.Trim(), out dec))
                        {
                            return 0;
                        }
                        zbs.Zdyfs = dec;
                        zbs.Syzbscl = this.RadioButtonList8.SelectedIndex;
                    }
                    else
                    {
                        zbs.Zdyfs = 0;
                    }

                    return zk.Insert_Zbs(zbs) ? 1 : 2;
                    #endregion
                case 3://素质评价
                    #region 查询显示
                    Model_zk_Pc_TouDang_szpj szpj = new Model_zk_Pc_TouDang_szpj();
                    szpj.XpcId = this.hf_xpcId.Value.Split('_')[0];
                    szpj.Zhszxztj = this.RadioButtonList9.SelectedIndex;
                    if (szpj.Zhszxztj == 1)
                    {
                        //szpj.Tjlx = this.DropDownList1.SelectedIndex;
                        //if (!int.TryParse(this.TextBox3.Text.Trim(), out iTemp))
                        //{
                        //    return 0;
                        //}
                        //szpj.Sl = iTemp;
                        //szpj.Pjdenji = this.DropDownList3.SelectedItem.Text.Trim();
                    }
                    return zk.Insert_Szpj(szpj) ? 1 : 2;
                    #endregion
                case 4://其他条件
                    #region 查询显示
                    Model_zk_Pc_TouDang_qttj qttj =new Model_zk_Pc_TouDang_qttj();
                    qttj.XpcId = this.hf_xpcId.Value.Split('_')[0];
                    qttj.Jscjxzif = this.RadioButtonList10.SelectedIndex;
                    qttj.Ywhcjhb_jscj = this.RadioButtonList11.SelectedIndex;
                    qttj.HkcjhgXz = this.RadioButtonList12.SelectedIndex;
                    qttj.Xbxz = this.RadioButtonList13.SelectedIndex == 2 ? 0 : this.RadioButtonList13.SelectedIndex + 1;

                    return zk.Insert_Qttj(qttj) ? 1 : 2;
                    #endregion
                case 5://国际班控档线
                    #region 查询显示
                    List<Model_zk_Pc_TouDang_gjbkdx> gjbkdx = new List<Model_zk_Pc_TouDang_gjbkdx>();
                    Model_zk_Pc_TouDang_gjbkdx item_1 = new Model_zk_Pc_TouDang_gjbkdx();
                    item_1.XpcId = this.hf_xpcId.Value.Split('_')[0];
                    item_1.Pcdm = this.hf_pcdm.Value;
                    item_1.Xxdm = "8188";
                    decimal.TryParse(this.txt_1.Text.Trim(),out dec);
                    item_1.Kdxfs = dec;
                    gjbkdx.Add(item_1);
                                        
                    Model_zk_Pc_TouDang_gjbkdx item_2 = new Model_zk_Pc_TouDang_gjbkdx();
                    item_2.XpcId = this.hf_xpcId.Value.Split('_')[0];
                    item_2.Pcdm = this.hf_pcdm.Value;
                    item_2.Xxdm = "8288";
                    decimal.TryParse(this.txt_2.Text.Trim(),out dec);
                    item_2.Kdxfs = dec;
                    gjbkdx.Add(item_2);
                                        
                    Model_zk_Pc_TouDang_gjbkdx item_3 = new Model_zk_Pc_TouDang_gjbkdx();
                    item_3.XpcId = this.hf_xpcId.Value.Split('_')[0];
                    item_3.Pcdm = this.hf_pcdm.Value;
                    item_3.Xxdm = "8388";
                    decimal.TryParse(this.txt_3.Text.Trim(),out dec);
                    item_3.Kdxfs = dec;
                    gjbkdx.Add(item_3);
                                        
                    Model_zk_Pc_TouDang_gjbkdx item_4 = new Model_zk_Pc_TouDang_gjbkdx();
                    item_4.XpcId = this.hf_xpcId.Value.Split('_')[0];
                    item_4.Pcdm = this.hf_pcdm.Value;
                    item_4.Xxdm = "8488";
                    decimal.TryParse(this.txt_4.Text.Trim(),out dec);
                    item_4.Kdxfs = dec;
                    gjbkdx.Add(item_4);
                                        
                    Model_zk_Pc_TouDang_gjbkdx item_5 = new Model_zk_Pc_TouDang_gjbkdx();
                    item_5.XpcId = this.hf_xpcId.Value.Split('_')[0];
                    item_5.Pcdm = this.hf_pcdm.Value;
                    item_5.Xxdm = "0288";
                    decimal.TryParse(this.txt_5.Text.Trim(),out dec);
                    item_5.Kdxfs = dec;
                    gjbkdx.Add(item_5);

                    return zk.Insert_Gjbkdx(gjbkdx) ? 1 : 2;
                    #endregion
            }
            return -1;
        }

        /// <summary>
        /// 当改变批次树节点的选中项时。
        /// </summary>
        protected void tvPcInfo_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeNode node=this.tvPcInfo.SelectedNode;
            this.hf_xpcId.Value = node.Value;
            int begin = node.Text.IndexOf('[');
            int end = node.Text.IndexOf(']');
            //"[11]"
            this.hf_pcdm.Value = node.Text.Substring(begin + 1, end - begin - 1);
            this.tvIfType.Nodes[0].Selected = true;
            FilterShowContent();
        }

        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = Request.Form["__EVENTTARGET"];
            int index = Convert.ToInt32(s.Substring(s.LastIndexOf("$") + 1));
            if (CheckBoxList1.Items[index].Selected)
            {
                lblcj.Text = lblcj.Text + ">>" + CheckBoxList1.Items[index].Text;
                lblcjid.Text = lblcjid.Text + "," + CheckBoxList1.Items[index].Value;
            }
            else
            {
                lblcj.Text = lblcj.Text.Replace(">>" + CheckBoxList1.Items[index].Text, "");
                lblcjid.Text = lblcjid.Text.Replace("," + CheckBoxList1.Items[index].Value, "");
            }
        
            //if ( CheckBoxList1.Items[CheckBoxList1.SelectedIndex].Text)
            //{
                
            //}
        }

      
    }
}