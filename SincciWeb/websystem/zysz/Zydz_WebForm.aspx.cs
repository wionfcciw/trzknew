using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

using BLL;
using System.Data;


namespace SincciKC
{
    /// <summary>
    /// 志愿定制。
    /// </summary>
    public partial class Zydz_WebForm : BPage
    {
        /// <summary>
        /// 页面加载。
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //页面权限
                Permission();
                BindTV();
                bindPcType();
                bindPgType();
            }
        }

        #region "判断页面权限"
        /// <summary>
        /// 页面权限
        /// </summary>
        private void Permission()
        {

            //查看
            if (!new Method().CheckButtonPermission(PopedomType.A2))
            {
                Response.Write("你没有页面查看的权限！");
                Response.End();
            }
            //新增
            if (!new Method().CheckButtonPermission(PopedomType.A4))
            {
                this.btnAddXq.Enabled = false;
                this.btnAdd.Enabled = false;
                this.btnOk.Enabled = false;
            }
            //修改
            if (!new Method().CheckButtonPermission(PopedomType.A8))
            {
                this.btnUpdate.Visible = false;
            }
            //删除
            if (!new Method().CheckButtonPermission(PopedomType.A16))
            {
                this.btnDelete.Visible = false;
            }

        }
        #endregion

        #region TreeView数据绑定。
        /// <summary>
        /// 绑定县区信息到显示树。
        /// </summary>
        private void BindTV()
        {
            DataTable dt = new DataTable();

            BLL_zk_zydz zk = new BLL_zk_zydz();
            dt = zk.Select_All_ZydzXq();
            this.tvDisplay.Nodes.Clear();
            foreach (DataRow row in dt.Rows)
            {
                TreeNode node = new TreeNode();
                node.Text = "[" + row["xqdm"] + "]" + row["xqmc"].ToString();
                node.Value = "@" + row["xqdm"];
                bindTreeNode_dpc(zk, row["xqdm"].ToString(), node);
                this.tvDisplay.Nodes.Add(node);
            }
            this.tvDisplay.ExpandAll();
        }

        /// <summary>
        /// 绑定大批次信息s。
        /// </summary>
        /// <param name="zk">操作类。</param>
        /// <param name="hjdm">父级代码。</param>
        /// <param name="hNode">当前节点</param>
        private void bindTreeNode_dpc(BLL_zk_zydz zk, string hjdm, TreeNode hNode)
        {
            DataTable tab = zk.Select_All_Dpc(hjdm);
            if (tab == null)
            {
                return;
            }
            foreach (DataRow row in tab.Rows)
            {
                TreeNode node = new TreeNode();
                node.Text = "[" + row["dpcDm"] + "]" + row["dpcMc"].ToString();
                node.Value = "#" + row["dpcId"];
                bindTreeNode_xpc(zk, row["dpcId"].ToString(), node);
                hNode.ChildNodes.Add(node);
            }
        }

        /// <summary>
        /// 绑定小批次信息s。
        /// </summary>
        /// <param name="zk">操作类。</param>
        /// <param name="hjdm">父级代码。</param>
        /// <param name="hNode">当前节点</param>
        private void bindTreeNode_xpc(BLL_zk_zydz zk, string hjdm, TreeNode hNode)
        {
            DataTable tab = zk.Select_All_Xpc(hjdm);
            if (tab == null)
            {
                return;
            }
            foreach (DataRow row in tab.Rows)
            {
                TreeNode node = new TreeNode();
                node.Text = "[" + row["xpcDm"] + "]" + row["xpcMc"].ToString();
                node.Value = "$" + row["xpcId"];
                bindTreeNode_zy(zk, row["xpcId"].ToString(), node);
                hNode.ChildNodes.Add(node);
            }
        }

        /// <summary>
        /// 绑定志愿。
        /// </summary>
        /// <param name="zk">操作类。</param>
        /// <param name="hjdm">父级代码。</param>
        /// <param name="hNode">当前节点</param>
        private void bindTreeNode_zy(BLL_zk_zydz zk, string hjdm, TreeNode hNode)
        {
            DataTable tab = zk.Select_All_Zy(hjdm);
            foreach (DataRow row in tab.Rows)
            {
                TreeNode node = new TreeNode();
                node.Text = "[" + row["zyDm"] + "]" + row["zyMc"].ToString();
                node.Value = "%" + row["zyId"];
                hNode.ChildNodes.Add(node);
            }
            Table tale = new Table();
        }

        /// <summary>
        /// 绑定批次类型。
        /// </summary>
        private void bindPcType()
        {
            BLL_zk_zdxx zdxx = new BLL_zk_zdxx();
            int totalRecord = 0;
            DataTable tab = zdxx.ExecuteProc(1, "zdlbdm='PcLx'", 500, 1, ref totalRecord);
            this.ddlPcLx.DataSource = tab;
            this.ddlPcLx.DataTextField = "zlbmc";
            this.ddlPcLx.DataValueField = "zlbdm";
            this.ddlPcLx.DataBind();
        }

        /// <summary>
        /// 绑定普高类型。
        /// </summary>
        private void bindPgType()
        {
            BLL_zk_zdxx zdxx = new BLL_zk_zdxx();
            int totalRecord = 0;
            DataTable tab = zdxx.ExecuteProc(1, "zdlbdm='PgLx'", 500, 1, ref totalRecord);
            this.ddlPgLx.DataSource = tab;
            this.ddlPgLx.DataTextField = "zlbmc";
            this.ddlPgLx.DataValueField = "zlbdm";
            this.ddlPgLx.DataBind();
        }

        #endregion

        #region 功能事件区。
        /// <summary>
        /// 点击新增。
        /// </summary>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            FilterShow(2, int.Parse(this.hfFlag.Value.Trim()) + 1);
            this.hfEditorType.Value = (int.Parse(this.hfFlag.Value.Trim()) + 1).ToString();
        }
        /// <summary>
        /// 点击修改。
        /// </summary>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            FilterShow(3, int.Parse(this.hfFlag.Value.Trim()));
            this.hfEditorType.Value = this.hfFlag.Value;
        }
        /// <summary>
        /// 点击删除。
        /// </summary>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.hfId.Value.Trim().Length < 1)
            {
                //请选择需要删除的数据。
                return;
            }
            //确认删除。
            BLL_zk_zydz zk = new BLL_zk_zydz();
            if (zk.deleteData(this.hfFlag.Value, this.hfId.Value))
            {
                //删除成功！ 
                BindTV();
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'删除成功！' ,title:'操作提示'}); </script>");
            }
            else
            {
                //删除失败！
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'删除失败！' ,title:'操作提示'}); </script>");
            }
        }

        /// <summary>
        /// 过滤显示项。
        /// </summary>
        /// <param name="iFlag">操作标识(1、查看；2、新增；3、修改)。</param>
        /// <param name="type">类型标识：1、县区；2、大批次；3、小批次；4、志愿</param>
        private void FilterShow(int iFlag, int type)
        {
            clearText();
            this.hfOperationType.Value = iFlag.ToString();
            if (iFlag == 1)
            {
                //显示
                this.tdShow.Visible = true;
                //编辑
                this.tdEditor.Visible = false;
            }
            else
            {
                //显示
                this.tdShow.Visible = false;
                //编辑
                this.tdEditor.Visible = true;
            }
            //根据类型标识。
            BLL_zk_zydz zk = new BLL_zk_zydz();
            if (iFlag == 2)
            {
                //新增。
                this.txtPcDm.ReadOnly = false;
            }
            else if (iFlag == 3)
            {
                //修改。
                this.txtPcDm.ReadOnly = true;
            }
            else
            {
                this.tr_8.Visible = false;
                this.tr_9.Visible = false;
            }
            this.tr_8_8.Visible = false;
            this.tr_9_9.Visible = false;
            switch (type)
            {
                case 1://县区
                    #region 根据操作类型显示相应的项
                    switch (iFlag)
                    {
                        case 1://查看
                            #region 显示
                            this.tr_1.Visible = true;
                            this.tr_2.Visible = true;
                            #endregion

                            #region 填充值。
                            this.lbl_101.Text = "县区代码:";
                            this.lbl_102.Text = "县区名称:";

                            this.lbl_201.Text = this.hfDm.Value;
                            this.lbl_202.Text = this.hfMc.Value;

                            #endregion

                            #region 隐藏
                            this.tr_3.Visible = false;
                            this.tr_4.Visible = false;
                            this.tr_5.Visible = false;
                            this.tr_6.Visible = false;
                            this.tr_7.Visible = false;
                            tr_10.Visible = false;
                            tr_11.Visible = false;
                            #endregion
                            break;
                        case 2://新增
                            this.txtPcDm.ReadOnly = false;
                            #region 显示
                            this.tr_0_0.Visible = true;
                            #endregion

                            #region 填充值。
                            BLL_zk_xqdm xqdm = new BLL_zk_xqdm();
                            this.ddlXqXx.DataSource = xqdm.selectxqdm();
                            this.ddlXqXx.DataTextField = "xqmc";
                            this.ddlXqXx.DataValueField = "xqdm";
                            this.ddlXqXx.DataBind();

                            this.lbl_300.Text = "县区信息:";
                            #endregion

                            #region 隐藏
                            this.tr_1_1.Visible = false;
                            this.tr_2_2.Visible = false;
                            this.tr_3_3.Visible = false;
                            this.tr_4_4.Visible = false;
                            this.tr_5_5.Visible = false;
                            this.tr_6_6.Visible = false;
                            this.tr_7_7.Visible = false;
                            this.tr_12_12.Visible = false;
                            #endregion
                            break;
                        case 3://修改
                            this.txtPcDm.ReadOnly = true;
                            #region 显示
                            this.tr_1_1.Visible = true;
                            this.tr_2_2.Visible = true;
                            #endregion

                            #region 填充值。
                            this.lbl_301.Text = "县区代码:";
                            this.lbl_302.Text = "县区名称:";
                            #endregion

                            #region 隐藏
                            this.tr_0_0.Visible = false;
                            this.tr_3_3.Visible = false;
                            this.tr_4_4.Visible = false;
                            this.tr_5_5.Visible = false;
                            this.tr_6_6.Visible = false;
                            this.tr_7_7.Visible = false;
                            this.tr_12_12.Visible = false;
                            #endregion
                            break;
                    }
                    #endregion
                    break;
                case 2://大批次
                    #region 根据操作类型显示相应的项
                    Model_zk_zydz_dpcxx dpc = zk.Select_Dpc(this.hfId.Value);
                    switch (iFlag)
                    {
                        case 1://查看
                            #region 显示
                            this.tr_1.Visible = true;
                            this.tr_2.Visible = true;
                            this.tr_3.Visible = true;
                            this.tr_4.Visible = true;

                            this.tr_6.Visible = true;
                            this.tr_7.Visible = true;
                            this.tr_12.Visible = false;
                            tr_10.Visible = true;
                            tr_11.Visible = true;
                            #endregion

                            #region 填充值。
                            this.lbl_101.Text = "大批次代码:";
                            this.lbl_102.Text = "大批次名称:";
                            this.lbl_103.Text = "显示名称:";
                            this.lbl_104.Text = "小批次数:";

                            this.lbl_106.Text = "是否启用:";
                            this.lbl_107.Text = "备注:";

                            //#####################
                            this.lbl_201.Text = dpc.DpcDm;
                            this.lbl_202.Text = dpc.DpcMc;
                            this.lbl_203.Text = dpc.DpcXsMc;
                            this.lbl_204.Text = dpc.XpcSl.ToString();

                            this.lbl_206.Text = dpc.Sfqy ? "已启用" : "未启用";
                            this.txt_207.Text = dpc.DpcBz;
                            lbl_210.Text = dpc.Stime.ToString();
                            lbl_211.Text = dpc.Etime.ToString();
                            #endregion

                            #region 隐藏
                            this.tr_5.Visible = false; this.tr_12.Visible = false;
                            #endregion
                            break;
                        case 2://新增
                            this.txtPcDm.ReadOnly = false;
                            #region 显示
                            this.tr_1_1.Visible = true;
                            this.tr_2_2.Visible = true;
                            this.tr_3_3.Visible = true;
                            this.tr_4_4.Visible = true;
                            this.tr_6_6.Visible = true;
                            this.tr_7_7.Visible = true;
                            tr_10_10.Visible = true;
                            tr_11_11.Visible = true;
                            #endregion

                            #region 填充值。
                            this.lbl_301.Text = "大批次代码:";
                            this.lbl_302.Text = "大批次名称:";
                            this.lbl_303.Text = "显示名称:";
                            this.lbl_304.Text = "小批次数:";

                            this.lbl_306.Text = "是否启用:";
                            this.lbl_307.Text = "备注:";
                            #endregion

                            #region 隐藏
                            this.tr_0_0.Visible = false;
                            this.tr_5_5.Visible = false;
                            this.tr_12_12.Visible = false;
                            #endregion
                            break;
                        case 3://修改
                            this.txtPcDm.ReadOnly = true;
                            #region 显示
                            this.tr_1_1.Visible = true;
                            this.tr_2_2.Visible = true;
                            this.tr_3_3.Visible = true;
                            this.tr_4_4.Visible = true;
                            this.tr_6_6.Visible = true;
                            this.tr_7_7.Visible = true;
                            tr_10_10.Visible = true;
                            tr_11_11.Visible = true;
                            #endregion

                            #region 填充值。
                            this.lbl_301.Text = "大批次代码:";
                            this.lbl_302.Text = "大批次名称:";
                            this.lbl_303.Text = "显示名称:";
                            this.lbl_304.Text = "小批次数:";

                            this.lbl_306.Text = "是否启用:";
                            this.lbl_307.Text = "备注:";
                             StartTime.Text = dpc.Stime.ToString();
                            EndTime.Text = dpc.Etime.ToString();
                            //#####################
                            this.txtPcDm.Text = dpc.DpcDm;
                            this.txtPcMc.Text = dpc.DpcMc;
                            this.txtShowName.Text = dpc.DpcXsMc;
                            this.txtCount.Text = dpc.XpcSl.ToString();

                            this.rbl_Sfqy.SelectedIndex = dpc.Sfqy ? 0 : 1;
                            this.txtBz.Text = dpc.DpcBz;
                            #endregion

                            #region 隐藏
                            this.tr_0_0.Visible = false;
                            this.tr_5_5.Visible = false;
                            this.tr_12_12.Visible = false;
                            #endregion
                            break;
                    }
                    #endregion
                    break;
                case 3://小批次
                    #region 根据操作类型显示相应的项
                    Model_zk_zydz_xpcxx xpc = zk.Select_Xpc(this.hfId.Value);
                    switch (iFlag)
                    {
                        case 1://查看
                            #region 显示
                            this.tr_1.Visible = true;
                            this.tr_2.Visible = true;
                            this.tr_3.Visible = true;
                            this.tr_4.Visible = true;
                            this.tr_5.Visible = true;
                            this.tr_6.Visible = true;
                            this.tr_7.Visible = true;
                            this.tr_8.Visible = true;
                            this.tr_9.Visible = true;
                               tr_10.Visible = false;
                            tr_11.Visible = false;
                            #endregion

                            #region 填充值。
                            this.lbl_101.Text = "小批次代码:";
                            this.lbl_102.Text = "小批次名称:";
                            this.lbl_103.Text = "显示名称:";
                            this.lbl_104.Text = "志愿数:";
                            this.lbl_105.Text = "学校服从:";
                            this.lbl_106.Text = "是否启用:";
                            this.lbl_107.Text = "备注:";

                            //#####################
                            this.lbl_201.Text = xpc.XpcDm;
                            this.lbl_202.Text = xpc.XpcMc;
                            this.lbl_203.Text = xpc.XpcXsMc;
                            this.lbl_204.Text = xpc.ZySl.ToString();
                            this.lbl_205.Text = xpc.XxFc.ToString();
                            this.lbl_206.Text = xpc.Sfqy ? "已启用" : "未启用";
                            this.txt_207.Text = xpc.XpcBz;
                            this.lbl_208.Text = xpc.PgPc ? "是" : "否";
                            this.lbl_209.Text = xpc.PcLbName;
                            #endregion

                            #region 隐藏
                            this.tr_12.Visible = false;
                            #endregion
                            break;
                        case 2://新增
                            this.txtPcDm.ReadOnly = false;
                            #region 显示
                            this.tr_1_1.Visible = true;
                            this.tr_2_2.Visible = true;
                            this.tr_3_3.Visible = true;
                            this.tr_4_4.Visible = true;
                            this.tr_5_5.Visible = true;
                            this.tr_6_6.Visible = true;
                            this.tr_7_7.Visible = true;
                            this.tr_8_8.Visible = true;
                            this.tr_9_9.Visible = true;
                            //学校服从
                            this.txtFc.Visible = true;
                                 
                            #endregion

                            #region 填充值。
                            this.lbl_301.Text = "小批次代码:";
                            this.lbl_302.Text = "小批次名称:";
                            this.lbl_303.Text = "显示名称:";
                            this.lbl_304.Text = "志愿数:";
                            this.lbl_305.Text = "学校服从:";
                            this.lbl_306.Text = "是否启用:";
                            this.lbl_307.Text = "备注:";
                            #endregion

                            #region 隐藏
                            this.tr_0_0.Visible = false;
                            this.tr_12_12.Visible = false;
                            //专业服从
                            this.rbl_Zyfc.Visible = false;
                             tr_10_10.Visible = false;
                                  tr_11_11.Visible = false;
                            #endregion
                            break;
                        case 3://修改
                            this.txtPcDm.ReadOnly = true;
                            #region 显示
                            this.tr_1_1.Visible = true;
                            this.tr_2_2.Visible = true;
                            this.tr_3_3.Visible = true;
                            this.tr_4_4.Visible = true;
                            this.tr_5_5.Visible = true;
                            this.tr_6_6.Visible = true;
                            this.tr_7_7.Visible = true;
                            this.tr_8_8.Visible = true;
                            this.tr_9_9.Visible = true;
                            //学校服从
                            this.txtFc.Visible = true;
                            #endregion

                            #region 填充值。
                            this.lbl_301.Text = "小批次代码:";
                            this.lbl_302.Text = "小批次名称:";
                            this.lbl_303.Text = "显示名称:";
                            this.lbl_304.Text = "志愿数:";
                            this.lbl_305.Text = "学校服从:";
                            this.lbl_306.Text = "是否启用:";
                            this.lbl_307.Text = "备注:";

                            //#####################
                            this.txtPcDm.Text = xpc.XpcDm;
                            this.txtPcMc.Text = xpc.XpcMc;
                            this.txtShowName.Text = xpc.XpcXsMc;
                            this.txtCount.Text = xpc.ZySl.ToString();
                            this.txtFc.Text = xpc.XxFc;
                            this.rbl_Sfqy.SelectedIndex = xpc.Sfqy ? 0 : 1;
                            this.txtBz.Text = xpc.XpcBz;
                            this.ddlPgLx.SelectedIndex = xpc.PgPc ? 1 : 0;
                            this.ddlPcLx.SelectedIndex = xpc.PcLb - 1;
                            #endregion

                            #region 隐藏
                            this.tr_0_0.Visible = false;
                            this.tr_12_12.Visible = false;
                            //专业服从
                            this.rbl_Zyfc.Visible = false;
                             tr_10_10.Visible = false;
                                  tr_11_11.Visible = false;
                            #endregion
                            break;
                    }
                    #endregion
                    break;
                case 4://志愿
                    #region 根据操作类型显示相应的项
                    Model_zk_zydz_zyxx zy = zk.Select_Zy(this.hfId.Value);
                    switch (iFlag)
                    {
                        case 1://查看
                            #region 显示
                            this.tr_1.Visible = true;
                            this.tr_2.Visible = true;
                            this.tr_3.Visible = true;
                            this.tr_4.Visible = true;
                            this.tr_5.Visible = true;
                            this.tr_6.Visible = true;
                            this.tr_7.Visible = true;
                            this.tr_12_12.Visible = true;
                            tr_12.Visible = true;
                            tr_10.Visible = false;
                            tr_11.Visible = false;
                            #endregion

                            #region 填充值。
                            this.lbl_101.Text = "志愿代码:";
                            this.lbl_102.Text = "志愿名称:";
                            this.lbl_103.Text = "显示名称:";
                            this.lbl_104.Text = "专业数:";
                            this.lbl_105.Text = "是否专业服从:";
                            this.lbl_106.Text = "是否启用:";
                            this.lbl_107.Text = "备注:";

                            //#####################
                            this.lbl_201.Text = zy.ZyDm;
                            this.lbl_202.Text = zy.ZyMc;
                            this.lbl_203.Text = zy.ZyXsmc;
                            this.lbl_204.Text = zy.ZySl.ToString();
                            this.lbl_205.Text = zy.SfZyFc ? "是" : "否";
                            this.lbl_206.Text = zy.Sfqy ? "已启用" : "未启用";
                            this.lbl_212.Text = zy.Sfxxfc ? "是" : "否";
                            this.txt_207.Text = zy.ZyBz;
                            #endregion

                            #region 隐藏
                          
                            #endregion
                            break;
                        case 2://新增
                            this.txtPcDm.ReadOnly = false;
                            #region 显示
                            this.tr_1_1.Visible = true;
                            this.tr_2_2.Visible = true;
                            this.tr_3_3.Visible = true;
                            this.tr_4_4.Visible = true;
                            this.tr_5_5.Visible = true;
                            this.tr_6_6.Visible = true;
                            this.tr_7_7.Visible = true;
                            this.tr_12_12.Visible = true;
                            //专业服从
                            this.rbl_Zyfc.Visible = true;

                            #endregion

                            #region 填充值。
                            this.lbl_301.Text = "志愿代码:";
                            this.lbl_302.Text = "志愿名称:";
                            this.lbl_303.Text = "显示名称:";
                            this.lbl_304.Text = "专业数:";
                            this.lbl_305.Text = "是否专业服从:";
                            this.lbl_306.Text = "是否启用:";
                            this.lbl_307.Text = "备注:";
                            #endregion

                            #region 隐藏
                            this.tr_0_0.Visible = false;
                            //学校服从
                            this.txtFc.Visible = false;
                             tr_10_10.Visible = false;
                                  tr_11_11.Visible = false;
                            #endregion
                            break;
                        case 3://修改
                            this.txtPcDm.ReadOnly = true;
                            #region 显示
                            this.tr_1_1.Visible = true;
                            this.tr_2_2.Visible = true;
                            this.tr_3_3.Visible = true;
                            this.tr_4_4.Visible = true;
                            this.tr_5_5.Visible = true;
                            this.tr_6_6.Visible = true;
                            this.tr_7_7.Visible = true;
                            //专业服从
                            this.rbl_Zyfc.Visible = true;
                            #endregion

                            #region 填充值。
                            this.lbl_301.Text = "志愿代码:";
                            this.lbl_302.Text = "志愿名称:";
                            this.lbl_303.Text = "显示名称:";
                            this.lbl_304.Text = "专业数:";
                            this.lbl_305.Text = "是否专业服从:";
                            this.lbl_306.Text = "是否启用:";
                            this.lbl_307.Text = "备注:";

                            //#####################
                            this.txtPcDm.Text = zy.ZyDm;
                            this.txtPcMc.Text = zy.ZyMc;
                            this.txtShowName.Text = zy.ZyXsmc;
                            this.txtCount.Text = zy.ZySl.ToString();
                            this.rbl_Zyfc.SelectedIndex = zy.SfZyFc ? 0 : 1;
                            this.rbl_Sfqy.SelectedIndex = zy.Sfqy ? 0 : 1;
                            this.rbl_Sfxxfc.SelectedIndex = zy.Sfxxfc ? 0 : 1;
                            this.txtBz.Text = zy.ZyBz;
                            #endregion

                            #region 隐藏
                            this.tr_0_0.Visible = false;
                            //学校服从
                            this.txtFc.Visible = false;
                             tr_10_10.Visible = false;
                                  tr_11_11.Visible = false;
                            #endregion
                            break;
                    }
                    #endregion
                    break;
            }
        }

        /// <summary>
        /// 清空已填充的数据。
        /// </summary>
        private void clearText()
        {
            this.txtPcDm.Text = "";
            this.txtPcMc.Text = "";
            this.txtShowName.Text = "";
            this.txtCount.Text = "";
            this.txtFc.Text = "";
            this.rbl_Zyfc.SelectedIndex = 1;
            this.rbl_Sfqy.SelectedIndex = 0;
            this.txtBz.Text = "";
        }

        /// <summary>
        /// 当改变节点选中项时。
        /// </summary>
        protected void tvDisplay_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeNode node = this.tvDisplay.SelectedNode;
            int begin = node.Text.IndexOf('[');
            int end = node.Text.IndexOf(']');
            this.hfDm.Value = node.Text.Substring(begin + 1, end - begin - 1);
            this.hfMc.Value = node.Text.Substring(end + 1);
            this.hfId.Value = node.Value.Substring(1);
            this.hfFlag.Value = node.Value.Substring(0, 1);
            switch (node.Value.Substring(0, 1))
            {
                case "@"://县区。
                    this.hfFlag.Value = "1";
                    this.btnAdd.Visible = true;
                    this.btnAdd.Text = "新增大批次";
                    break;
                case "#"://大批次。
                    this.hfFlag.Value = "2";
                    this.btnAdd.Visible = true;
                    this.btnAdd.Text = "新增小批次";
                    break;
                case "$"://小批次。
                    this.hfFlag.Value = "3";
                    this.btnAdd.Visible = true;
                    this.btnAdd.Text = "新增志愿";
                    break;
                case "%"://志愿。
                    this.hfFlag.Value = "4";
                    this.btnAdd.Visible = false;
                    break;
            }
            FilterShow(1, int.Parse(this.hfFlag.Value));
        }
        #endregion

        #region 点击确定时。
        /// <summary>
        /// 点击确定时。
        /// </summary>
        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                return;
            }
            //操作标识(1、查看；2、新增；3、修改)
            BLL_zk_zydz zk = new BLL_zk_zydz();
            int iCount = 0;
            int.TryParse(this.txtCount.Text.Trim(), out iCount);
            switch (this.hfEditorType.Value)
            {
                case "1"://县区
                    #region 县区。
                    Model_zk_zydz_zydzxq xq = new Model_zk_zydz_zydzxq();
                    switch (this.hfOperationType.Value)
                    {
                        case "2"://新增
                            xq.Xqdm = this.ddlXqXx.SelectedItem.Value;
                            xq.Xqmc = this.ddlXqXx.SelectedItem.Text;
                            if (zk.Insert_ZydzXq(xq))
                            {
                                //新增成功！
                                BindTV();
                                this.tdEditor.Visible = false;
                                this.tdShow.Visible = false;
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'新增成功！' ,title:'操作提示'}); </script>");
                            }
                            else
                            {
                                //新增失败！
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'新增失败！' ,title:'操作提示'}); </script>");
                            }
                            break;
                        case "3"://修改
                            xq.Xqdm = this.txtPcDm.Text.Trim();
                            xq.Xqmc = this.txtPcMc.Text.Trim();
                            if (zk.Update_ZyDzXq(xq))
                            {
                                //修改成功！
                                BindTV();
                                FilterShow(1, 1);
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'修改成功！' ,title:'操作提示'}); </script>");
                            }
                            else
                            {
                                //修改失败！
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'修改成功！' ,title:'操作提示'}); </script>");
                            }
                            break;
                    }
                    #endregion
                    break;
                case "2"://大批次
                    #region 大批次。
                    Model_zk_zydz_dpcxx dpc = new Model_zk_zydz_dpcxx();
                    dpc.DpcMc = this.txtPcMc.Text.Trim();
                    dpc.DpcXsMc = this.txtShowName.Text.Trim();
                    dpc.XpcSl = int.Parse(this.txtCount.Text.Trim());
                    dpc.Sfqy = this.rbl_Sfqy.SelectedIndex == 0 ? true : false;
                    dpc.DpcBz = this.txtBz.Text.Trim();
                      dpc.Stime = Convert.ToDateTime(StartTime.Text);
                            dpc.Etime = Convert.ToDateTime(EndTime.Text);
                    switch (this.hfOperationType.Value)
                    {
                        case "2"://新增
                            dpc.DpcId = this.hfId.Value + this.txtPcDm.Text.Trim();
                            dpc.Xqdm = this.hfId.Value;
                            dpc.DpcDm = this.txtPcDm.Text.Trim();
                          
                            if (zk.Insert_Dpc(dpc))
                            {
                                //新增成功！
                                BindTV();
                                FilterShow(1, 1);
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'新增成功！' ,title:'操作提示'});  </script>");
                            }
                            else
                            {
                                //新增失败！
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'新增失败！' ,title:'操作提示'});  </script>");
                            }
                            break;
                        case "3"://修改
                            dpc.DpcId = this.hfId.Value;
                            if (zk.Update_Dpc(dpc))
                            {
                                //修改成功！
                                BindTV();
                                FilterShow(1, 2);
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'修改成功！' ,title:'操作提示'}); </script>");
                            }
                            else
                            {
                                //修改失败！
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'修改失败！' ,title:'操作提示'}); </script>");
                            }
                            break;
                    }
                    #endregion
                    break;
                case "3"://小批次
                    #region 小批次。
                    Model_zk_zydz_xpcxx xpc = new Model_zk_zydz_xpcxx();
                    xpc.XpcMc = this.txtPcMc.Text.Trim();
                    xpc.XpcXsMc = this.txtShowName.Text.Trim();
                    xpc.ZySl = int.Parse(this.txtCount.Text.Trim());
                    xpc.XxFc = this.txtFc.Text.Trim();
                    xpc.Sfqy = this.rbl_Sfqy.SelectedIndex == 0 ? true : false;
                    xpc.XpcBz = this.txtBz.Text.Trim();
                    xpc.PgPc = this.ddlPgLx.SelectedIndex == 0 ? false : true;
                    xpc.PcLb = this.ddlPcLx.SelectedIndex + 1;

                    switch (this.hfOperationType.Value)
                    {
                        case "2"://新增
                            xpc.XpcId = this.hfId.Value + this.txtPcDm.Text.Trim();
                            xpc.XpcDm = this.txtPcDm.Text.Trim();
                            xpc.DpcDm = this.hfId.Value;
                            xpc.PcDm = this.hfDm.Value.Trim() + this.txtPcDm.Text.Trim();
                            if (zk.Insert_Xpc(xpc))
                            {
                                //新增成功！
                                BindTV();
                                FilterShow(1, 2);
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'新增成功！' ,title:'操作提示'}); </script>");
                            }
                            else
                            {
                                //新增失败！
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'新增失败！' ,title:'操作提示'}); </script>");
                            }
                            break;
                        case "3"://修改
                            xpc.XpcId = this.hfId.Value;
                            if (zk.Update_Xpc(xpc))
                            {
                                //修改成功！
                                BindTV();
                                FilterShow(1, 3);
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'修改成功！' ,title:'操作提示'}); </script>");
                            }
                            else
                            {
                                //修改失败！
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'修改失败！' ,title:'操作提示'}); </script>");
                            }
                            break;
                    }
                    #endregion
                    break;
                case "4"://志愿
                    #region 志愿。
                    Model_zk_zydz_zyxx zy = new Model_zk_zydz_zyxx();
                    zy.ZyDm = this.txtPcDm.Text.Trim();
                    zy.ZyMc = this.txtPcMc.Text.Trim();
                    zy.ZyXsmc = this.txtShowName.Text.Trim();
                    zy.ZySl = iCount;

                    zy.SfZyFc = this.rbl_Zyfc.SelectedIndex == 0 ? true : false;
                    zy.Sfqy = this.rbl_Sfqy.SelectedIndex == 0 ? true : false;
                    zy.Sfxxfc = this.rbl_Sfxxfc.SelectedIndex == 0 ? true : false;
                    zy.ZyBz = this.txtBz.Text.Trim();

                    switch (this.hfOperationType.Value)
                    {
                        case "2"://新增
                            zy.ZyId = this.hfId.Value + this.txtPcDm.Text.Trim();
                            zy.XpcDm = this.hfId.Value;
                            if (zk.Insert_Zy(zy))
                            {
                                //新增成功！
                                BindTV();
                                FilterShow(1, 3);
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'新增成功！' ,title:'操作提示'}); </script>");
                            }
                            else
                            {
                                //新增失败！
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'新增失败！' ,title:'操作提示'}); </script>");
                            }
                            break;
                        case "3"://修改
                            zy.ZyId = this.hfId.Value;
                            if (zk.Update_Zy(zy))
                            {
                                //修改成功！
                                BindTV();
                                FilterShow(1, 4);
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'修改成功！' ,title:'操作提示'}); </script>");

                            }
                            else
                            {
                                //修改失败！
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'新增失败！' ,title:'操作提示'}); </script>");
                            }
                            break;
                    }
                    #endregion
                    break;
            }
        }
        #endregion

        #region 取消。
        /// <summary>
        /// 取消。
        /// </summary>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.hfFlag.Value == "1" && this.hfOperationType.Value == "2")
            {
                this.tdEditor.Visible = false;
                this.tdShow.Visible = false;
            }
            else
            {
                FilterShow(1, int.Parse(this.hfFlag.Value.Trim()));
            }
        }
        #endregion

        /// <summary>
        /// 新增市、县区信息。
        /// </summary>
        protected void btnAddXq_Click(object sender, EventArgs e)
        {
            this.hfFlag.Value = "1";
            this.hfEditorType.Value = this.hfFlag.Value;
            FilterShow(2, 1);
            if (this.tvDisplay.SelectedNode != null)
            {
                this.tvDisplay.SelectedNode.Selected = false;
            }
        }

        /// <summary>
        /// 验证数据的有效性(true、验证数据有误；false、验证数据通过)。
        /// </summary>
        private bool ValidateData()
        {
            BLL_zk_zydz zk = new BLL_zk_zydz();
            int iTemp = 0;
            switch (this.hfEditorType.Value)
            {
                case "1"://县区
                    if (this.ddlXqXx.SelectedItem == null)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请选择县区信息！' ,title:'警告提示'});</script>");
                        return true;
                    }
                    if (this.hfOperationType.Value == "2")
                    {
                        switch (zk.selectZyDzXq(this.ddlXqXx.SelectedValue))
                        {
                            case -1://数据库执行时出错。
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'数据库操作错误！' ,title:'警告提示'});</script>");
                                return true;
                            case 0://无记录。
                                break;
                            default://有记录。
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'当前县区代码已存在！' ,title:'警告提示'});</script>");
                                return true;
                        }
                    }
                    break;
                case "2"://大批次
                    //大批次代码。
                    if (this.txtPcDm.Text.Trim().Length < 1)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请输入大批次代码！' ,title:'警告提示'});</script>");
                        return true;
                    }
                    if (this.txtPcDm.Text.Trim().Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'大批次代码最多2个数字！' ,title:'警告提示'});</script>");
                        return true;
                    }
                    //大批次名称。
                    if (this.txtPcMc.Text.Trim().Length < 1)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请输入大批次名称！' ,title:'警告提示'});</script>");
                        return true;
                    }
                    if (this.txtPcMc.Text.Trim().Length > 25)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'大批次名称最多25个汉字或字符！' ,title:'警告提示'});</script>");
                        return true;
                    }

                    //大批次显示名称。
                    if (this.txtShowName.Text.Trim().Length < 1)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请输入大批次的显示名称！' ,title:'警告提示'});</script>");
                        return true;
                    }
                    if (this.txtPcMc.Text.Trim().Length > 25)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'大批次的显示名称最多25个汉字或字符！' ,title:'警告提示'});</script>");
                        return true;
                    }
                    //小批次数。
                    if (this.txtCount.Text.Trim().Length < 1)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请输入小批次数！' ,title:'警告提示'});</script>");
                        return true;
                    }
                    if (!int.TryParse(this.txtCount.Text.Trim(), out iTemp))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'小批次数是数字，请重新输入！' ,title:'警告提示'});</script>");
                        return true;
                    }
                    if (iTemp > 99)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'小批次数最多2个数字！' ,title:'警告提示'});</script>");
                        return true;
                    }

                    if (this.hfOperationType.Value == "2")
                    {
                        switch (zk.selectZyDzDpc(this.hfId.Value + this.txtPcDm.Text.Trim()))
                        {
                            case -1://数据库执行时出错。
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'数据库操作错误！' ,title:'警告提示'});</script>");
                                return true;
                            case 0://无记录。
                                break;
                            default://有记录。
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'当前大批次代码已存在！' ,title:'警告提示'});</script>");
                                return true;
                        }
                    }
                    break;
                case "3"://小批次
                    //小批次代码。
                    if (this.txtPcDm.Text.Trim().Length < 1)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请输入小批次代码！' ,title:'警告提示'});</script>");
                        return true;
                    }
                    if (this.txtPcDm.Text.Trim().Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'小批次代码最多2个数字！' ,title:'警告提示'});</script>");
                        return true;
                    }

                    //小批次名称。
                    if (this.txtPcMc.Text.Trim().Length < 1)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请输入小批次名称！' ,title:'警告提示'});</script>");
                        return true;
                    }
                    if (this.txtPcMc.Text.Trim().Length > 25)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'小批次名称最多25个汉字或字符！' ,title:'警告提示'});</script>");
                        return true;
                    }

                    //小批次显示名称。
                    if (this.txtShowName.Text.Trim().Length < 1)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请输入小批次显示的名称！' ,title:'警告提示'});</script>");
                        return true;
                    }
                    if (this.txtPcMc.Text.Trim().Length > 25)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'小批次的显示名称最多25个汉字或字符！' ,title:'警告提示'});</script>");
                        return true;
                    }
                    //志愿数。
                    if (this.txtCount.Text.Trim().Length < 1)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请输入志愿数！' ,title:'警告提示'}); </script>");
                        return true;
                    }
                    if (!int.TryParse(this.txtCount.Text.Trim(), out iTemp))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'志愿数是数字，请重新输入！' ,title:'警告提示'});</script>");
                        return true;
                    }
                    if (iTemp > 99)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'志愿数最多2个数字！' ,title:'警告提示'});</script>");
                        return true;
                    }

                    if (this.hfOperationType.Value == "2")
                    {
                        switch (zk.selectZyDzXpc(this.hfId.Value + this.txtPcDm.Text.Trim()))
                        {
                            case -1://数据库执行时出错。
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'数据库操作错误！' ,title:'警告提示'}); </script>");
                                return true;
                            case 0://无记录。
                                break;
                            default://有记录。
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'当前小批次代码已存在！' ,title:'警告提示'}); </script>");
                                return true;
                        }
                    }
                    break;
                case "4"://志愿
                    //志愿代码。
                    if (this.txtPcDm.Text.Trim().Length < 1)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请输志愿代码！' ,title:'警告提示'}); </script>");
                        return true;
                    }
                    if (this.txtPcDm.Text.Trim().Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'志愿代码最多2个数字！' ,title:'警告提示'});</script>");
                        return true;
                    }
                    //志愿名称。
                    if (this.txtPcMc.Text.Trim().Length < 1)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请输入志愿名称！' ,title:'警告提示'}); </script>");
                        return true;
                    }
                    if (this.txtPcMc.Text.Trim().Length > 25)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'志愿名称最多25个汉字或字符！' ,title:'警告提示'});</script>");
                        return true;
                    }
                    //志愿显示名称。
                    if (this.txtShowName.Text.Trim().Length < 1)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请输入志愿显示的名称！' ,title:'警告提示'}); </script>");
                        return true;
                    }
                    if (this.txtPcMc.Text.Trim().Length > 25)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'志愿的显示名称最多25个汉字或字符！' ,title:'警告提示'});</script>");
                        return true;
                    }
                    //专业数
                    if (this.txtCount.Text.Trim().Length < 1)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请输入专业数！' ,title:'警告提示'}); </script>");
                        return true;
                    }
                    if (!int.TryParse(this.txtCount.Text.Trim(), out iTemp))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'专业数是数字，请重新输入！' ,title:'警告提示'});</script>");
                        return true;
                    }
                    if (iTemp > 99)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'专业数最多2个数字！' ,title:'警告提示'});</script>");
                        return true;
                    }

                    if (this.hfOperationType.Value == "2")
                    {
                        switch (zk.selectZyDzZy(this.hfId.Value + this.txtPcDm.Text.Trim()))
                        {
                            case -1://数据库执行时出错。
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'数据库操作错误！' ,title:'警告提示'}); </script>");
                                return true;
                            case 0://无记录。
                                break;
                            default://有记录。
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'当前志愿代码已存在！' ,title:'警告提示'});  </script>");
                                return true;
                        }
                    }
                    break;
            }
            return false;
        }
    }
}