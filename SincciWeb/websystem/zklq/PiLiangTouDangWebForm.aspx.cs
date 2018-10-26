using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using System.Text;
using System.IO;
using Model;

namespace SincciKC
{
    /*
     * 批量投档说明：
     * 一、显示内容如下：
     *  1、加载当前系统中所有的批次信息（小批次）；
     *  2、同步显示当前批次的投档算法（从基本条件中查询）；
     *  3、加载显示各县区的最低分数控制线；
     *  4、加载显示当前批次的所有招生学校信息。
     * 二、批量投档如下：
     *  1、查询各县区的最低分数控制线数据；
     *  2、查询当前批次的投档条件；
     *  3、查询非指标生招生计划；
     *  4、判断当前批次是否有指标生；
     *  5、如果有，则查询指标生的招生计划；
     *  6、根据当前批次的条件查询所有考生信息，按分数的高低排序（平行志愿算法）。
     *  7、向学校投档非指标生、指标生。
     */

    /// <summary>
    /// 批量投档操作
    /// </summary>
    public partial class PiLiangTouDangWebForm : BPage
    {
        private BLL_zk_xqdm bllxqdm = new BLL_zk_xqdm();
        /// <summary>
        /// 页面加载。
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //int UserType = SincciLogin.Sessionstu().UserType;
                //if (UserType != 1)
                //{
                //    Response.Write("<script language='javascript'>window.parent.location.href='/ht/login.aspx';</script>");
                //    return;
                //}
                Permission();
                loadPcInfo();
                loadXqZdFsKzx();
                loadToudangInfo();
                Showxq();
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
            //单校投档
            if (!new Method().CheckButtonPermission(PopedomType.A4))
            {
                this.divdxtoud.Visible = false;
            }
            //批量发档
            if (!new Method().CheckButtonPermission(PopedomType.A8))
            {
                this.btnPl_Fd.Visible = false;
            }
            //单校发档
            if (!new Method().CheckButtonPermission(PopedomType.A16))
            {
                this.btn_dxFd.Visible = false;
            }
            //导出
            if (!new Method().CheckButtonPermission(PopedomType.A32))
            {
                this.btn_daochu.Visible = false;
            }
            //更改计划库
            if (!new Method().CheckButtonPermission(PopedomType.A64))
            {
                this.btn_jhs.Visible = false;
            }
        }
        #endregion
        /// <summary>
        /// 加载批次信息。
        /// </summary>
        private void loadPcInfo()
        {
            BLL_zk_Pctd_tj_Info zk = new BLL_zk_Pctd_tj_Info();
            DataTable tab = zk.selectPcdm();
            this.ddlXpcInfo.DataSource = tab;
            this.ddlXpcInfo.DataTextField = "xpc_mc";
            this.ddlXpcInfo.DataValueField = "xpc_id";
            this.ddlXpcInfo.DataBind();

            bindData_tdzy_sf();
            string str = this.ddlXpcInfo.SelectedItem.Text;
            int begin = 0;
            int end = 0;
            begin = str.IndexOf('[');
            end = str.IndexOf(']');
            string pcdm = str.Substring(begin + 1, end - begin - 1);
            string[] strs = this.ddlXpcInfo.SelectedValue.Split('_');
            Loadzsxx(pcdm, strs[0].Substring(0, 3));
        }
        /// <summary>
        /// 志愿算法。
        /// </summary>
        private void bindData_tdzy_sf()
        {
            BLL_zk_PiLiangTouDang zk = new BLL_zk_PiLiangTouDang();
            string[] strs = this.ddlXpcInfo.SelectedValue.Split('_');
            DataTable tab = zk.select_tdzy_sf(strs[0]);
            if (tab == null || tab.Rows.Count < 1)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'还没有定制投档条件，请定制投档条件后再进行此项操作！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                this.btn_beginTd.Enabled = false;
                this.btnCancel_Td.Enabled = false;

                this.btnPl_Fd.Enabled = false;

                this.ddl_tdsf.DataSource = tab;
                ListItem item = new ListItem();
                item.Text = "未定义投档条件";
                item.Value = "-1";
                this.ddl_tdsf.DataBind();
                this.ddl_tdsf.Items.Add(item);
            }
            else
            {
                this.btn_beginTd.Enabled = true;
                this.btnCancel_Td.Enabled = true;

                this.btnPl_Fd.Enabled = true;

                this.ddl_tdsf.DataSource = tab;
                this.ddl_tdsf.DataTextField = "tdsfmc";
                this.ddl_tdsf.DataValueField = "tdsf";
                this.ddl_tdsf.DataBind();
                if (ddl_tdsf.SelectedValue == "1")
                {
                    divzy.Visible = true;
                    Loadzyxx();
                }
                else
                {
                    divzy.Visible = false;
                }
            }
        }
        /// <summary>
        /// 加载小批次志愿信息
        /// </summary>
        private void Loadzyxx()
        {
            BLL_zk_zydz zk = new BLL_zk_zydz();
            DataTable tab = zk.Select_All_Zy(ddlXpcInfo.SelectedValue.ToString().Split('_')[0]);
            this.ddl_zy.DataSource = tab;
            this.ddl_zy.DataTextField = "zyMc";
            this.ddl_zy.DataValueField = "zyDm";
            this.ddl_zy.DataBind();
        }
        /// <summary>
        /// 加载各县区最低分数控制线的信息。
        /// </summary>
        private void loadXqZdFsKzx()
        {
            BLL_zk_PiLiangTouDang zk = new BLL_zk_PiLiangTouDang();
            DataTable tab = zk.selectXqZdFsx();
            if (tab == null)
            {
                this.span_showXqInfo.InnerHtml = "";
                return;
            }
            StringBuilder stb = new StringBuilder();
            stb.Append("&nbsp;");
            int i = 0;
            foreach (DataRow row in tab.Rows)
            {
                i++;
                if (i == 6)
                {
                    stb.Append("<br />");
                    stb.Append("&nbsp;");
                }
                stb.Append("[");
                stb.Append(row["yc_Xqdm"].ToString());
                stb.Append("]");
                stb.Append(row["yc_XqMc"].ToString());
                stb.Append(":");
                stb.Append("<input name='txt_");
                stb.Append(row["yc_Xqdm"].ToString());
                stb.Append("' type='text' style='width:40px;' value='");
                stb.Append(row["yc_ZdFensuKzx"].ToString());
                stb.Append("' />");
                stb.Append("&nbsp;&nbsp;");
            }
            this.span_showXqInfo.InnerHtml = stb.ToString();
        }
        /// <summary>
        /// 加载投档信息。
        /// </summary>
        private void loadToudangInfo()
        {
            BLL_zk_PiLiangTouDang zk = new BLL_zk_PiLiangTouDang();
            if (this.ddlXpcInfo.SelectedIndex < 0)
            {
                // this.tr_head.Visible = true;
            }

            //string str = this.ddlXpcInfo.SelectedItem.Text;
            //int begin = 0;
            //int end = 0;

            //begin = str.IndexOf('[');
            //end = str.IndexOf(']');
            string[] strs = this.ddlXpcInfo.SelectedValue.Split('_');
            string pcdm = strs[0].Substring(3);

            string xqdm = strs[0].Substring(0, 3);
            DataTable tab = new DataTable();
            if (pcdm != "01")
                pcdm = "11";
            if (ddl_tdsf.SelectedValue.ToString() == "1")
            {
                tab = zk.select_pc_touDang_Info(pcdm, xqdm);
            }
            else
            {
                tab = zk.select_pc_touDang_Info(pcdm, xqdm);
            }
            DataTable sfxqtab = zk.select_sfxq(pcdm);
            if (sfxqtab.Rows.Count > 0)
            {
                chxq.Checked = Convert.ToBoolean(sfxqtab.Rows[0]["sfxq"]);
            }
            int iSum = 0;
            int iYiSum = 0;
            int iTemp = 0;
            foreach (DataRow row in tab.Rows)
            {
                int.TryParse(row["jhs"].ToString(), out iTemp);
                iSum += iTemp;
                int.TryParse(row["yitd_fzbs_sl"].ToString(), out iTemp);
                iYiSum += iTemp;
                int.TryParse(row["yitd_zbs_sl"].ToString(), out iTemp);
                iYiSum += iTemp;
            }
            this.span_TongJiInfo.InnerText = "计划总人数:" + iSum.ToString() + " 已预投人数:" + iYiSum.ToString();
            txtNum.Text = iSum.ToString();
            this.repDisplay.DataSource = tab;
            this.repDisplay.DataBind();
            return;
        }
        /// <summary>
        /// 当改变当前批次时。
        /// </summary>
        protected void ddlXpcInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadToudangInfo();
            bindData_tdzy_sf();
            string str = this.ddlXpcInfo.SelectedItem.Text;
            int begin = 0;
            int end = 0;
            begin = str.IndexOf('[');
            end = str.IndexOf(']');
            string pcdm = str.Substring(begin + 1, end - begin - 1);
            string[] strs = this.ddlXpcInfo.SelectedValue.Split('_');
            Loadzsxx(pcdm, strs[0].Substring(0, 3));
        }
        /// <summary>
        /// 招生学校
        /// </summary>
        private void Loadzsxx(string pcdm, string xqdm)
        {
            BLL_LQK_Ks_Xx zk = new BLL_LQK_Ks_Xx();
            if (pcdm != "01")
                pcdm = "11";
            DataTable tab = zk.Select_zk_zsxx(pcdm, " a.xqdm='" + xqdm + "'");
            this.ddl_xx.DataSource = tab;
            this.ddl_xx.DataTextField = "zsxxmc";
            this.ddl_xx.DataValueField = "xxdm";
            this.ddl_xx.DataBind();
            this.ddl_xx.Items.Insert(0, new ListItem("-请选择-", ""));
        }
        /// <summary>
        /// 开始投档。
        /// </summary>
        protected void btn_beginTd_Click(object sender, EventArgs e)
        {
            BLL_zk_PiLiangTouDang zk = new BLL_zk_PiLiangTouDang();
            DataTable tab = zk.selectXqZdFsx();
            int sf = 0;
            if (!int.TryParse(this.ddl_tdsf.SelectedValue, out sf))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'当前批次未定制投档条件！' ,title:'警告提示'}); </script>");
                return;
            }
            if (tab != null)
            {
                decimal dec = 0;
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    if (!decimal.TryParse(Request.Form[("txt_" + tab.Rows[i]["yc_Xqdm"].ToString())].Trim(), out dec))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'县区的最低分数线必须是数值类型！' ,title:'警告提示'});</script>");
                        return;
                    }
                    tab.Rows[i]["yc_ZdFensuKzx"] = dec.ToString();
                }
                zk.UPXqZdFsx(tab);
                string str = this.ddlXpcInfo.SelectedItem.Text;
                int begin = 0;
                int end = 0;
                begin = str.IndexOf('[');
                end = str.IndexOf(']');

                string[] strs = this.ddlXpcInfo.SelectedValue.Split('_');
                double tdnum = Convert.ToDouble(txtbl.Text.Trim()) * Convert.ToDouble(txtNum.Text.Trim());
                int iRe = zk.Start_TouDang(strs[0], str.Substring(begin + 1, end - begin - 1), tab, ddl_zy.SelectedValue.ToString(), Convert.ToInt32(tdnum), "", Convert.ToDouble(txtbl.Text.Trim()), 1, strs[0].Substring(0, 3), 0, chxq.Checked, 1, "");
                switch (iRe)
                {
                    case 0:
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'投档操作成功！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                        loadToudangInfo();
                        break;
                    case -1:
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'投档操作失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                        break;
                    case 2:
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请将当前批准设置投档条件后再进行此操作！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                        break;
                    default:
                        break;
                }
                loadXqZdFsKzx();
            }

        }

        /// <summary>
        /// 点击导出所有考生当前投档状态信息。
        /// </summary>
        protected void btnImport_td_ksxx_Click(object sender, EventArgs e)
        {
            //string str = this.ddlXpcInfo.SelectedItem.Text;
            //int begin = 0;
            //int end = 0;
            //begin = str.IndexOf('[');
            //end = str.IndexOf(']');
            //string pcdm = str.Substring(begin + 1, end - 1);

            //string where = String.Format(" where pcdm='{0}'", pcdm);
            BLL_LQK_Ks_Xx lqk = new BLL_LQK_Ks_Xx();
            if (!lqk.Import_lqk())
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'导出数据失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
            }

        }

        protected void btnCancel_Td_Click(object sender, EventArgs e)
        {
            string txt = this.ddlXpcInfo.SelectedItem.Text.Trim();
            int begin = txt.IndexOf('[');
            int end = txt.IndexOf(']');
            string pcdm = txt.Substring(begin + 1, end - begin - 1);
            BLL_zk_PiLiangTouDang pltd = new BLL_zk_PiLiangTouDang();
            if (pcdm != "01")
                pcdm = "11";

            if (ddl_xx.SelectedValue != "")
            {
                if (pltd.Cancel_PC_TD_XX(pcdm, ddl_xx.SelectedValue))
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'取消投档成功！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'取消投档失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                }
            }
            else
            {
                if (pltd.Cancel_PC_TD(pcdm))
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'取消投档成功！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'取消投档失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                }
            }


            loadToudangInfo();
        }
        /// <summary>
        /// 发档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPl_Fd_Click(object sender, EventArgs e)
        {
            string txt = this.ddlXpcInfo.SelectedItem.Text.Trim();
            int begin = txt.IndexOf('[');
            int end = txt.IndexOf(']');
            string pcdm = txt.Substring(begin + 1, end - begin - 1);
            BLL_zk_PiLiangTouDang pltd = new BLL_zk_PiLiangTouDang();

            if (ddl_tdsf.SelectedValue.ToString() == "1")
            {
                if (pltd.FA_PC_TD(pcdm))
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'发档成功！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'发档失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                }
            }
            else
            {
                if (pltd.FA_PC_TD(pcdm))
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'发档成功！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'发档失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                }
            }

            loadToudangInfo();
        }
        /// <summary>
        /// 单校发档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_dxFd_Click(object sender, EventArgs e)
        {
            string str = this.ddlXpcInfo.SelectedItem.Text;
            int begin = 0;
            int end = 0;
            begin = str.IndexOf('[');
            end = str.IndexOf(']');
            string pcdm = str.Substring(begin + 1, end - begin - 1);
            if (pcdm != "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>opUp('" + pcdm + "', '单校发档') ;</script>");
                loadToudangInfo();
            }
        }
        /// <summary>
        /// 单校发档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_xxfd_Click(object sender, EventArgs e)
        {
            BLL_zk_PiLiangTouDang zk = new BLL_zk_PiLiangTouDang();
            DataTable tab = zk.selectXqZdFsx();
            int sf = 0;
            if (!int.TryParse(this.ddl_tdsf.SelectedValue, out sf))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'当前批次未定制投档条件！' ,title:'警告提示'}); </script>");
                return;
            }
            if (tab != null)
            {
                decimal dec = 0;
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    if (!decimal.TryParse(Request.Form[("txt_" + tab.Rows[i]["yc_Xqdm"].ToString())].Trim(), out dec))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'县区的最低分数线必须是数值类型！' ,title:'警告提示'});</script>");
                        return;
                    }
                    tab.Rows[i]["yc_ZdFensuKzx"] = dec.ToString();
                }
                zk.UPXqZdFsx(tab);
                string str = this.ddlXpcInfo.SelectedItem.Text;
                int begin = 0;
                int end = 0;
                begin = str.IndexOf('[');
                end = str.IndexOf(']');
                string[] strs = this.ddlXpcInfo.SelectedValue.Split('_');
                int tdnum = Convert.ToInt32(txtdxnum.Text.Trim());
                string xqdm = "";
                int jhs = 0;

                if (Request.Form["bmddm"] != null)
                {
                    xqdm = Request.Form["bmddm"].ToString().Trim();
                }
                else
                {

                    DataTable pqtab = zk.select_lqjh(ddl_xx.SelectedValue.ToString());
                    if (pqtab.Rows.Count == 0)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'该招生学校无县区信息！' ,title:'警告提示'});</script>");
                        return;
                    }
                    string xq1 = "";
                    string xq2 = "";

                    for (int i = 0; i < pqtab.Rows.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                xq1 = pqtab.Rows[i]["xqdm"].ToString() + ";" + pqtab.Rows[i]["jhs"].ToString();
                                break;
                            case 1:
                                xq2 = pqtab.Rows[i]["xqdm"].ToString() + ";" + pqtab.Rows[i]["jhs"].ToString();
                                break;
                            default:
                                break;
                        }
                    }
                    if (xq2 != "")
                    {
                        if (xq1.Length > xq2.Length)
                        {
                            xqdm = xq2.Split(';')[0].ToString();
                            jhs = Convert.ToInt32(xq2.Split(';')[1].ToString());
                        }
                        else
                        {
                            xqdm = xq1.Split(';')[0].ToString();
                            jhs = Convert.ToInt32(xq1.Split(';')[1].ToString());
                        }
                    }
                    else
                    {
                        xqdm = xq1.Split(';')[0].ToString();
                        jhs = Convert.ToInt32(xq1.Split(';')[1].ToString());
                    }
                    if (tdnum == -1)
                    {
                        DataTable tabjhs = zk.select_jhs(ddl_xx.SelectedValue.ToString());
                        tdnum = Convert.ToInt32(tabjhs.Rows[0]["yitd_fzbs_sl"]) + jhs;
                    }
                }
                txtfsx.Text = xqdm;
                //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'" + tdnum + "！' ,title:'警告提示'}); </script>");

                int iRe = zk.Start_TouDang(strs[0], str.Substring(begin + 1, end - begin - 1), tab, ddl_zy.SelectedValue.ToString(), Convert.ToInt32(tdnum), ddl_xx.SelectedValue.ToString(), 1, 2, strs[0].Substring(0, 3), 0, false, 1, xqdm);
                switch (iRe)
                {
                    case 0:
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'投档操作成功！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                        loadToudangInfo();
                        break;
                    case -1:
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'投档操作失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                        break;
                    case 2:
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请将当前批准设置投档条件后再进行此操作！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                        break;
                    default:
                        break;
                }
                loadXqZdFsKzx();
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_daochu_Click(object sender, EventArgs e)
        {
            string strDate1 = DateTime.Now.ToString("yyyyMMddHHmmss");

            base.Response.Clear();

            base.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");

            base.Response.AddHeader("content-disposition", "attachment;filename=" + strDate1.ToString() + ".xls");

            base.Response.Charset = "gb2312";//gb2312,utf-8,UTF7
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            //base.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");

            //Response.ContentType指定文件类型 可以为application/ms-excel、application/ms-word、application/ms-txt、application/ms-html 或其他浏览器可直接支持文档

            base.Response.ContentType = "application/vnd.xls";

            this.EnableViewState = false;

            //　定义一个输入流

            StringWriter writer = new StringWriter();

            HtmlTextWriter Htmlwriter = new HtmlTextWriter(writer);

            this.repDisplay.RenderControl(Htmlwriter);

            //this 表示输出本页，你也可以绑定datagrid,或其他支持obj.RenderControl()属性的控件

            base.Response.Write(writer.ToString());

            base.Response.End();

        }
        /// <summary>
        /// 更新计划库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_jhs_Click(object sender, EventArgs e)
        {
            BLL_zk_PiLiangTouDang zk = new BLL_zk_PiLiangTouDang();
            if (zk.gxjhk())
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'更新成功！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'更新失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
            }
        }

        protected void btn_zbstd_Click(object sender, EventArgs e)
        {
            BLL_zk_PiLiangTouDang zk = new BLL_zk_PiLiangTouDang();
            DataTable tab = zk.selectXqZdFsx();
            int sf = 0;
            if (!int.TryParse(this.ddl_tdsf.SelectedValue, out sf))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'当前批次未定制投档条件！' ,title:'警告提示'}); </script>");
                return;
            }
            if (tab != null)
            {
                decimal dec = 0;
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    if (!decimal.TryParse(Request.Form[("txt_" + tab.Rows[i]["yc_Xqdm"].ToString())].Trim(), out dec))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'县区的最低分数线必须是数值类型！' ,title:'警告提示'});</script>");
                        return;
                    }
                    tab.Rows[i]["yc_ZdFensuKzx"] = dec.ToString();
                }
                zk.UPXqZdFsx(tab);
                string str = this.ddlXpcInfo.SelectedItem.Text;
                int begin = 0;
                int end = 0;
                begin = str.IndexOf('[');
                end = str.IndexOf(']');

                string[] strs = this.ddlXpcInfo.SelectedValue.Split('_');
                double tdnum = Convert.ToDouble(txtbl.Text.Trim()) * Convert.ToDouble(txtNum.Text.Trim());
                int iRe = zk.Start_TouDang(strs[0], str.Substring(begin + 1, end - begin - 1), tab, ddl_zy.SelectedValue.ToString(), Convert.ToInt32(tdnum), "", Convert.ToDouble(txtbl.Text.Trim()), 1, strs[0].Substring(0, 3), 1, false, 1, "");
                switch (iRe)
                {
                    case 0:
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'投档操作成功！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                        loadToudangInfo();
                        break;
                    case -1:
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'投档操作失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                        break;
                    case 2:
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请将当前批准设置投档条件后再进行此操作！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                        break;
                    default:
                        break;
                }
                loadXqZdFsKzx();
            }
        }
        private BLL_zk_hege bllhege = new BLL_zk_hege();
        protected void repDisplay_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Insert")
            {
                string id = e.CommandArgument.ToString();
                bllhege.Insert_zk_TJS_XXSD(id, "");
                loadToudangInfo();
                //插入数据库
            }
            else if (e.CommandName == "Del")
            {
                string id = e.CommandArgument.ToString();
                bllhege.DeleteData_zk_TJS_XXSD(id);
                loadToudangInfo();
            }
        }

        protected void btn_jq_Click(object sender, EventArgs e)
        {

            string strksh = "";
            if (Request.Form["CheckBox1"] != null)
            {
                strksh = Request.Form["CheckBox1"].ToString();

                if (strksh.Length > 0)
                {
                    for (int i = 0; i < strksh.Split(',').Length; i++)
                    {
                        bllhege.Insert_zk_TJS_XXSD(strksh.Split(',')[i], "");
                    }
                    loadToudangInfo();
                }
            }

        }

        protected void btn_jqqx_Click(object sender, EventArgs e)
        {
            string strksh = "";
            if (Request.Form["CheckBox1"] != null)
            {
                strksh = Request.Form["CheckBox1"].ToString();

                if (strksh.Length > 0)
                {

                    bllhege.DeleteData_zk_TJS_XXSD(strksh);

                    loadToudangInfo();
                }
            }
        }

        protected void btnpzt_Click(object sender, EventArgs e)
        {
            BLL_zk_PiLiangTouDang zk = new BLL_zk_PiLiangTouDang();
            //string str = this.ddlXpcInfo.SelectedItem.Text;
            //int begin = 0;
            //int end = 0;
            //begin = str.IndexOf('[');
            //end = str.IndexOf(']');
            //string pcdm = str.Substring(begin + 1, end - begin - 1);
            string pcdm = "11";
            if (zk.Gxpzt(pcdm))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'更新成功！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                loadToudangInfo();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'更新失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
            }
        }

        protected void chxq_CheckedChanged(object sender, EventArgs e)
        {
            BLL_zk_PiLiangTouDang zk = new BLL_zk_PiLiangTouDang();
            int sfxq = chxq.Checked ? 1 : 0;
            string str = this.ddlXpcInfo.SelectedItem.Text;
            int begin = 0;
            int end = 0;
            begin = str.IndexOf('[');
            end = str.IndexOf(']');
            string pcdm = str.Substring(begin + 1, end - begin - 1);
            if (zk.sfxq(sfxq, pcdm))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'设置成功！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                loadToudangInfo();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'设置失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
            }
        }

        protected void btn_pesTd_Click(object sender, EventArgs e)
        {
            BLL_zk_PiLiangTouDang zk = new BLL_zk_PiLiangTouDang();
            DataTable tab = zk.selectXqZdFsx();
            int sf = 0;
            if (!int.TryParse(this.ddl_tdsf.SelectedValue, out sf))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'当前批次未定制投档条件！' ,title:'警告提示'}); </script>");
                return;
            }
            if (tab != null)
            {
                decimal dec = 0;
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    if (!decimal.TryParse(Request.Form[("txt_" + tab.Rows[i]["yc_Xqdm"].ToString())].Trim(), out dec))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'县区的最低分数线必须是数值类型！' ,title:'警告提示'});</script>");
                        return;
                    }
                    tab.Rows[i]["yc_ZdFensuKzx"] = dec.ToString();
                }
                zk.UPXqZdFsx(tab);
                string str = this.ddlXpcInfo.SelectedItem.Text;
                int begin = 0;
                int end = 0;
                begin = str.IndexOf('[');
                end = str.IndexOf(']');

                string[] strs = this.ddlXpcInfo.SelectedValue.Split('_');
                double tdnum = Convert.ToDouble(txtbl.Text.Trim()) * Convert.ToDouble(txtNum.Text.Trim());
                int iRe = zk.Start_TouDang(strs[0], str.Substring(begin + 1, end - begin - 1), tab, ddl_zy.SelectedValue.ToString(), Convert.ToInt32(tdnum), "", Convert.ToDouble(txtbl.Text.Trim()), 1, strs[0].Substring(0, 3), 0, chxq.Checked, 2, "");
                switch (iRe)
                {
                    case 0:
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'投档操作成功！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                        loadToudangInfo();
                        break;
                    case -1:
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'投档操作失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                        break;
                    case 2:
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请将当前批准设置投档条件后再进行此操作！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                        break;
                    default:
                        break;
                }
                loadXqZdFsKzx();
            }
        }
        private void Showxq()
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            DataTable dt = bllxqdm.SelectXqdm(Department, UserType);

            StringBuilder sb = new StringBuilder();

            sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

            if (dt.Rows.Count > 0)
            {
                bmdinfo.Visible = true;
                sb.Append("<tr>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("   <td>");
                    sb.Append("<input name='bmddm' type='checkbox' value='" + dt.Rows[i]["xqdm"].ToString() + "'>" + dt.Rows[i]["xqmc"].ToString() + "|");
                    sb.Append("   </td>");

                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            this.bmdinfo.InnerHtml = sb.ToString();
        }

        protected void btn_xxfd_pe_Click(object sender, EventArgs e)
        {
            BLL_zk_PiLiangTouDang zk = new BLL_zk_PiLiangTouDang();
            DataTable tab = zk.selectXqZdFsx();
            int sf = 0;
            if (!int.TryParse(this.ddl_tdsf.SelectedValue, out sf))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'当前批次未定制投档条件！' ,title:'警告提示'}); </script>");
                return;
            }
            if (tab != null)
            {
                decimal dec = 0;
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    if (!decimal.TryParse(Request.Form[("txt_" + tab.Rows[i]["yc_Xqdm"].ToString())].Trim(), out dec))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'县区的最低分数线必须是数值类型！' ,title:'警告提示'});</script>");
                        return;
                    }
                    tab.Rows[i]["yc_ZdFensuKzx"] = dec.ToString();
                }
                zk.UPXqZdFsx(tab);
                string str = this.ddlXpcInfo.SelectedItem.Text;
                int begin = 0;
                int end = 0;
                begin = str.IndexOf('[');
                end = str.IndexOf(']');
                string[] strs = this.ddlXpcInfo.SelectedValue.Split('_');
                int tdnum = Convert.ToInt32(txtdxnum.Text.Trim());
                string xqdm = "";
                if (Request.Form["bmddm"] != null)
                {
                    xqdm = Request.Form["bmddm"].ToString().Trim();
                }
                int iRe = zk.Start_TouDang(strs[0], str.Substring(begin + 1, end - begin - 1), tab, ddl_zy.SelectedValue.ToString(), Convert.ToInt32(tdnum), ddl_xx.SelectedValue.ToString(), 1, 2, strs[0].Substring(0, 3), 0, false, 2, xqdm);
                switch (iRe)
                {
                    case 0:
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'投档操作成功！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                        loadToudangInfo();
                        break;
                    case -1:
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'投档操作失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                        break;
                    case 2:
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请将当前批准设置投档条件后再进行此操作！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                        break;
                    default:
                        break;
                }
                loadXqZdFsKzx();
            }
        }

        protected void btn_xxfd_pzt_Click(object sender, EventArgs e)
        {
            BLL_zk_PiLiangTouDang zk = new BLL_zk_PiLiangTouDang();
            DataTable tab = zk.selectXqZdFsx();
            int sf = 0;
            if (!int.TryParse(this.ddl_tdsf.SelectedValue, out sf))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'当前批次未定制投档条件！' ,title:'警告提示'}); </script>");
                return;
            }
            if (tab != null)
            {
                decimal dec = 0;
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    if (!decimal.TryParse(Request.Form[("txt_" + tab.Rows[i]["yc_Xqdm"].ToString())].Trim(), out dec))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'县区的最低分数线必须是数值类型！' ,title:'警告提示'});</script>");
                        return;
                    }
                    tab.Rows[i]["yc_ZdFensuKzx"] = dec.ToString();
                }
                zk.UPXqZdFsx(tab);
                string str = this.ddlXpcInfo.SelectedItem.Text;
                int begin = 0;
                int end = 0;
                begin = str.IndexOf('[');
                end = str.IndexOf(']');
                string[] strs = this.ddlXpcInfo.SelectedValue.Split('_');
                int tdnum = Convert.ToInt32(txtdxnum.Text.Trim());
                string xqdm = "";
                int jhs = 0;
                if (Request.Form["bmddm"] != null)
                {
                    xqdm = Request.Form["bmddm"].ToString().Trim();
                }
                else
                {
                    string lxx = ddl_xx.SelectedValue.ToString();
                    if (lxx == "001" || lxx == "003" || lxx == "004")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('该招生学校本县配转统请选择县区！');</script>");
                        return;
                    }

                    DataTable pqtab = zk.select_lqjh(ddl_xx.SelectedValue.ToString());
                    if (pqtab.Rows.Count == 0)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'该招生学校无县区信息！' ,title:'警告提示'});</script>");
                        return;
                    }
                    string xq1 = "";
                    string xq2 = "";
                    for (int i = 0; i < pqtab.Rows.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                xq1 = pqtab.Rows[i]["xqdm"].ToString() + ";" + pqtab.Rows[i]["jhs"].ToString();
                                break;
                            case 1:
                                xq2 = pqtab.Rows[i]["xqdm"].ToString() + ";" + pqtab.Rows[i]["jhs"].ToString();
                                break;
                            default:
                                break;
                        }
                    }
                    if (xq2 != "")
                    {
                        if (xq1.Length > xq2.Length)
                        {
                            xqdm = xq2.Split(';')[0].ToString();
                            jhs = Convert.ToInt32(xq2.Split(';')[1].ToString());
                        }
                        else
                        {
                            xqdm = xq1.Split(';')[0].ToString();
                            jhs = Convert.ToInt32(xq1.Split(';')[1].ToString());
                        }
                    }
                    else
                    {
                        xqdm = xq1.Split(';')[0].ToString();
                        jhs = Convert.ToInt32(xq1.Split(';')[1].ToString());
                    }
                    if (tdnum == -1)
                    {
                        DataTable tabjhs = zk.select_jhs(ddl_xx.SelectedValue.ToString());
                        tdnum = Convert.ToInt32(tabjhs.Rows[0]["yitd_fzbs_sl"]) + jhs;
                    }
                }
                txtfsx.Text = xqdm;
                int iRe = zk.Start_TouDang(strs[0], str.Substring(begin + 1, end - begin - 1), tab, ddl_zy.SelectedValue.ToString(), Convert.ToInt32(tdnum), ddl_xx.SelectedValue.ToString(), 1, 2, strs[0].Substring(0, 3), 0, false, 3, xqdm);
                switch (iRe)
                {
                    case 0:
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'投档操作成功！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                        loadToudangInfo();
                        break;
                    case -1:
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'投档操作失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                        break;
                    case 2:
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请将当前批准设置投档条件后再进行此操作！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                        break;
                    default:
                        break;
                }
                loadXqZdFsKzx();
            }
        }

        protected void btn_ql_Click(object sender, EventArgs e)
        {
            BLL_zk_PiLiangTouDang zk = new BLL_zk_PiLiangTouDang();
            string str = this.ddlXpcInfo.SelectedItem.Text;
            int begin = 0;
            int end = 0;
            begin = str.IndexOf('[');
            end = str.IndexOf(']');
            string pcdm = str.Substring(begin + 1, end - begin - 2);
            if (zk.GxpZY(pcdm))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'更新成功！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                loadToudangInfo();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'更新失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //string serverpath = Server.MapPath("/Template/");
            //string filename = "录取统计表.xls";
            //string fileID = DateTime.Now.ToString("MMddmmss");
            //string CreatePath = Server.MapPath("/Temp/" + fileID);
            //if (!Directory.Exists(CreatePath))
            //{
            //    Directory.CreateDirectory(CreatePath);
            //}
            //File.Copy(serverpath + "lqtj.xls", CreatePath + "\\" + filename, true);


            //BLL_lqtj blltj = new BLL_lqtj();
            //string path = CreatePath + "\\" + filename;
            //blltj.Lqxx_Export(path, "", "", "", 1);
            //Response.Redirect(CreatePath + "/" + filename, false);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            BLL_zk_PiLiangTouDang zk = new BLL_zk_PiLiangTouDang();
            DataTable tab = zk.selectXqZdFsx();
            int sf = 0;
            if (!int.TryParse(this.ddl_tdsf.SelectedValue, out sf))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'当前批次未定制投档条件！' ,title:'警告提示'}); </script>");
                return;
            }
            if (tab != null)
            {
                decimal dec = 0;
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    if (!decimal.TryParse(Request.Form[("txt_" + tab.Rows[i]["yc_Xqdm"].ToString())].Trim(), out dec))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'县区的最低分数线必须是数值类型！' ,title:'警告提示'});</script>");
                        return;
                    }
                    tab.Rows[i]["yc_ZdFensuKzx"] = dec.ToString();
                }
                zk.UPXqZdFsx(tab);
                string str = this.ddlXpcInfo.SelectedItem.Text;
                int begin = 0;
                int end = 0;
                begin = str.IndexOf('[');
                end = str.IndexOf(']');
                string[] strs = this.ddlXpcInfo.SelectedValue.Split('_');
                int tdnum = Convert.ToInt32(txtdxnum.Text.Trim());
                string xqdm = "";
                int jhs = 0;
                if (Request.Form["bmddm"] != null)
                {
                    xqdm = Request.Form["bmddm"].ToString().Trim();
                }
                else
                {

                    DataTable pqtab = zk.select_lqjh(ddl_xx.SelectedValue.ToString());
                    if (pqtab.Rows.Count == 0)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'该招生学校无县区信息！' ,title:'警告提示'});</script>");
                        return;
                    }
                    string xq1 = "";
                    string xq2 = "";

                    for (int i = 0; i < pqtab.Rows.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                xq1 = pqtab.Rows[i]["xqdm"].ToString() + ";" + pqtab.Rows[i]["jhs"].ToString();
                                break;
                            case 1:
                                xq2 = pqtab.Rows[i]["xqdm"].ToString() + ";" + pqtab.Rows[i]["jhs"].ToString();
                                break;
                            default:
                                break;
                        }
                    }
                    if (xq2 != "")
                    {
                        if (xq1.Length > xq2.Length)
                        {
                            xqdm = xq1.Split(';')[0].ToString();
                            jhs = Convert.ToInt32(xq1.Split(';')[1].ToString());
                        }
                        else
                        {
                            xqdm = xq2.Split(';')[0].ToString();
                            jhs = Convert.ToInt32(xq2.Split(';')[1].ToString());
                        }
                    }
                    else
                    {
                        xqdm = xq1.Split(';')[0].ToString();
                        jhs = Convert.ToInt32(xq1.Split(';')[1].ToString());
                    }
                    if (tdnum == -1)
                    {
                        DataTable tabjhs = zk.select_jhs(ddl_xx.SelectedValue.ToString());
                        tdnum = Convert.ToInt32(tabjhs.Rows[0]["yitd_fzbs_sl"]) + jhs;
                    }
                }
                txtfsx.Text = xqdm;
                //   Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'" + xqdm + "！' ,title:'警告提示'}); </script>");
                int iRe = zk.Start_TouDang(strs[0], str.Substring(begin + 1, end - begin - 1), tab, ddl_zy.SelectedValue.ToString(), Convert.ToInt32(tdnum), ddl_xx.SelectedValue.ToString(), 1, 2, strs[0].Substring(0, 3), 0, false, 1, xqdm);
                switch (iRe)
                {
                    case 0:
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'投档操作成功！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                        loadToudangInfo();
                        break;
                    case -1:
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'投档操作失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                        break;
                    case 2:
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请将当前批准设置投档条件后再进行此操作！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                        break;
                    default:
                        break;
                }
                loadXqZdFsKzx();
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            BLL_zk_PiLiangTouDang zk = new BLL_zk_PiLiangTouDang();
            DataTable tab = zk.selectXqZdFsx();
            int sf = 0;
            if (!int.TryParse(this.ddl_tdsf.SelectedValue, out sf))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'当前批次未定制投档条件！' ,title:'警告提示'}); </script>");
                return;
            }
            if (tab != null)
            {
                decimal dec = 0;
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    if (!decimal.TryParse(Request.Form[("txt_" + tab.Rows[i]["yc_Xqdm"].ToString())].Trim(), out dec))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'县区的最低分数线必须是数值类型！' ,title:'警告提示'});</script>");
                        return;
                    }
                    tab.Rows[i]["yc_ZdFensuKzx"] = dec.ToString();
                }
                zk.UPXqZdFsx(tab);
                string str = this.ddlXpcInfo.SelectedItem.Text;
                int begin = 0;
                int end = 0;
                begin = str.IndexOf('[');
                end = str.IndexOf(']');
                string[] strs = this.ddlXpcInfo.SelectedValue.Split('_');
                int tdnum = Convert.ToInt32(txtdxnum.Text.Trim());
                string xqdm = "";
                int jhs = 0;
                if (Request.Form["bmddm"] != null)
                {
                    xqdm = Request.Form["bmddm"].ToString().Trim();
                }
                else
                {
                    string lxx = ddl_xx.SelectedValue.ToString();
                    if (lxx == "003" || lxx == "004")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('该招生学校本县配转统请选择县区！');</script>");
                        return;
                    }
                    DataTable pqtab = zk.select_lqjh(ddl_xx.SelectedValue.ToString());
                    if (pqtab.Rows.Count == 0)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'该招生学校无县区信息！' ,title:'警告提示'});</script>");
                        return;
                    }

                    string xq1 = "";
                    string xq2 = "";
                    for (int i = 0; i < pqtab.Rows.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                xq1 = pqtab.Rows[i]["xqdm"].ToString() + ";" + pqtab.Rows[i]["jhs"].ToString();
                                break;
                            case 1:
                                xq2 = pqtab.Rows[i]["xqdm"].ToString() + ";" + pqtab.Rows[i]["jhs"].ToString();
                                break;
                            default:
                                break;
                        }
                    }
                    if (xq2 != "")
                    {
                        if (xq1.Length > xq2.Length)
                        {
                            xqdm = xq1.Split(';')[0].ToString();
                            jhs = Convert.ToInt32(xq1.Split(';')[1].ToString());
                        }
                        else
                        {
                            xqdm = xq2.Split(';')[0].ToString();
                            jhs = Convert.ToInt32(xq2.Split(';')[1].ToString());
                        }
                    }
                    else
                    {
                        xqdm = xq1.Split(';')[0].ToString();
                        jhs = Convert.ToInt32(xq1.Split(';')[1].ToString());
                    }
                    if (tdnum == -1)
                    {
                        DataTable tabjhs = zk.select_jhs(ddl_xx.SelectedValue.ToString());
                        tdnum = Convert.ToInt32(tabjhs.Rows[0]["yitd_fzbs_sl"]) + jhs;
                    }
                }
                txtfsx.Text = xqdm;
                int iRe = zk.Start_TouDang(strs[0], str.Substring(begin + 1, end - begin - 1), tab, ddl_zy.SelectedValue.ToString(), Convert.ToInt32(tdnum), ddl_xx.SelectedValue.ToString(), 1, 2, strs[0].Substring(0, 3), 0, false, 3, xqdm);
                switch (iRe)
                {
                    case 0:
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'投档操作成功！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                        loadToudangInfo();
                        break;
                    case -1:
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'投档操作失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                        break;
                    case 2:
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请将当前批准设置投档条件后再进行此操作！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                        break;
                    default:
                        break;
                }
                loadXqZdFsKzx();
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            BLL_zk_PiLiangTouDang zk = new BLL_zk_PiLiangTouDang();
            string xxdm = ddl_xx.SelectedValue;
            DataTable dt = zk.select_tzfsx();
            DataRow[] dr = dt.Select("lqxx='" + xxdm + "' and ts='统招生' ");
            int fs = 50;
            if (dr.Length > 0)
            {
                bool a = zk.UPXqZdFsx_2(dr, fs);
                if (a)
                {
                    string strfsx = "";
                    List<string> listfsx = new List<string>();
                    List<string> listxx = new List<string>() { "001", "002", "003", "004", "005", "006", "007", "008", "009", "010", "011", "012", "013" };//有配额生的学校   
                    for (int k = 0; k < listxx.Count; k++)
                    {
                        dr = dt.Select("lqxx='" + listxx[k] + "' and ts='统招生' ");
                        string xq1 = "";
                        string sffsx = "";
                        if (listxx[k] != "001" && listxx[k] != "002" && listxx[k] != "003" && listxx[k] != "004")
                        {
                            if (dr.Length == 2)
                            {
                                if (dr[0]["name"].ToString().Length > dr[1]["name"].ToString().Length)
                                {
                                    xq1 = dr[1]["name"].ToString();
                                    sffsx = dr[1]["n"].ToString();
                                }
                                else
                                {
                                    xq1 = dr[0]["name"].ToString();
                                    sffsx = dr[0]["n"].ToString();
                                }

                            }
                            else if (dr.Length == 1)
                            {
                                xq1 = dr[0]["name"].ToString();
                                sffsx = dr[0]["n"].ToString();
                            }
                        }
                        if (xq1 == "")
                        {

                            for (int i = 0; i < dr.Length; i++)
                            {
                                string xqdm = dr[i]["name"].ToString();
                                decimal fsx = Convert.ToDecimal(dr[i]["n"].ToString()) - fs;
                                for (int j = 0; j < xqdm.Split(',').Length; j++)
                                {
                                    if (xqdm.Split(',')[j] == "502" || xqdm.Split(',')[j] == "505" || xqdm.Split(',')[j] == "503" || xqdm.Split(',')[j] == "504")
                                    {
                                        if (listxx[k] == "003" || listxx[k] == "004")
                                        {
                                            continue;
                                        }
                                    }
                                    string xxx = listxx[k] + "*" + xqdm.Split(',')[j] + "=" + Convert.ToInt32(fsx) + ";";
                                    if (!listfsx.Contains(xxx))
                                    {
                                        strfsx = strfsx + xxx;
                                        listfsx.Add(xxx);
                                    }
                                }
                            }


                        }
                        else
                        {
                            for (int t = 0; t < xq1.Split(',').Length; t++)
                            {
                                string xxx = listxx[k] + "*" + xq1.Split(',')[t] + "=" + Convert.ToInt32(Convert.ToDecimal(sffsx)).ToString() + ";";
                                if (!listfsx.Contains(xxx))
                                {
                                    strfsx = strfsx + xxx;
                                    listfsx.Add(xxx);
                                }
                            }

                        }

                    }
                    txtfsx.Text = strfsx;
                    loadXqZdFsKzx();
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('修改配额生分数线成功!');  </script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('修改配额生分数线失败!');  </script>");
                }
            }

        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            BLL_byxxlqXx byxx = new BLL_byxxlqXx();
            if (byxx.XX_UP_PASS_ALL())
            {
                loadXqZdFsKzx();
                loadToudangInfo();
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('成功!');  </script>");

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('失败!');  </script>");

            }
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            BLL_zk_PiLiangTouDang zk = new BLL_zk_PiLiangTouDang();

            string pcdm = "11";

            if (zk.Gxpzt004(pcdm))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'更新成功！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                loadToudangInfo();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'更新失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
            }
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            BLL_zk_PiLiangTouDang zk = new BLL_zk_PiLiangTouDang();
            DataTable tab = zk.selectXqZdFsx();
            int sf = 0;
            if (!int.TryParse(this.ddl_tdsf.SelectedValue, out sf))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'当前批次未定制投档条件！' ,title:'警告提示'}); </script>");
                return;
            }
            if (tab != null)
            {
                decimal dec = 0;
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    if (!decimal.TryParse(Request.Form[("txt_" + tab.Rows[i]["yc_Xqdm"].ToString())].Trim(), out dec))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'县区的最低分数线必须是数值类型！' ,title:'警告提示'});</script>");
                        return;
                    }
                    tab.Rows[i]["yc_ZdFensuKzx"] = dec.ToString();
                }
                zk.UPXqZdFsx(tab);
                string str = this.ddlXpcInfo.SelectedItem.Text;
                int begin = 0;
                int end = 0;
                begin = str.IndexOf('[');
                end = str.IndexOf(']');
                string[] strs = this.ddlXpcInfo.SelectedValue.Split('_');
                string strxxdm = "";
                if (Request.Form["CheckBox1"] != null)
                {
                    strxxdm = Request.Form["CheckBox1"].ToString();
                    if (strxxdm.Length > 0)
                    {
                        string tishi = "";
                        for (int j = 0; j < strxxdm.Split(',').Length; j++)
                        {
                            int tdnum = -1;
                            string xqdm = "";
                            int jhs = 0;
                            string xxdm = strxxdm.Split(',')[j];
                            if (Request.Form["bmddm"] != null)
                            {
                                xqdm = Request.Form["bmddm"].ToString().Trim();
                            }
                            else
                            {
                                DataTable pqtab = zk.select_lqjh(xxdm);
                                if (pqtab.Rows.Count == 0)
                                {
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'该招生学校无县区信息！' ,title:'警告提示'});</script>");
                                    return;
                                }
                                string xq1 = "";
                                string xq2 = "";

                                for (int i = 0; i < pqtab.Rows.Count; i++)
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            xq1 = pqtab.Rows[i]["xqdm"].ToString() + ";" + pqtab.Rows[i]["jhs"].ToString();
                                            break;
                                        case 1:
                                            xq2 = pqtab.Rows[i]["xqdm"].ToString() + ";" + pqtab.Rows[i]["jhs"].ToString();
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                if (xq2 != "")
                                {
                                    if (xq1.Length > xq2.Length)
                                    {
                                        xqdm = xq2.Split(';')[0].ToString();
                                        jhs = Convert.ToInt32(xq2.Split(';')[1].ToString());
                                    }
                                    else
                                    {
                                        xqdm = xq1.Split(';')[0].ToString();
                                        jhs = Convert.ToInt32(xq1.Split(';')[1].ToString());
                                    }
                                }
                                else
                                {
                                    xqdm = xq1.Split(';')[0].ToString();
                                    jhs = Convert.ToInt32(xq1.Split(';')[1].ToString());
                                }
                                if (tdnum == -1)
                                {
                                    DataTable tabjhs = zk.select_jhs(xxdm);
                                    tdnum = Convert.ToInt32(tabjhs.Rows[0]["yitd_fzbs_sl"]) + jhs;
                                }
                            }
                            txtfsx.Text = xqdm;
                            int iRe = zk.Start_TouDang(strs[0], str.Substring(begin + 1, end - begin - 1), tab, ddl_zy.SelectedValue.ToString(), Convert.ToInt32(tdnum), xxdm, 1, 2, strs[0].Substring(0, 3), 0, false, 1, xqdm);

                            switch (iRe)
                            {
                                case 0:
                                    tishi += xxdm + "投档成功!</br>";
                                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'投档操作成功！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                                    //loadToudangInfo();
                                    break;
                                case -1:
                                    tishi += xxdm + "投档失败!</br>";
                                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'投档操作失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                                    break;

                                default:
                                    break;
                            }

                        }
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'" + tishi + "' ,title:'警告提示'});</script>");
                        loadToudangInfo();
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请勾选学校！' ,title:'警告提示'});</script>");
                    return;
                }
            }
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            BLL_zk_PiLiangTouDang zk = new BLL_zk_PiLiangTouDang();
            DataTable tab = zk.selectXqZdFsx();
            int sf = 0;
            if (!int.TryParse(this.ddl_tdsf.SelectedValue, out sf))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'当前批次未定制投档条件！' ,title:'警告提示'}); </script>");
                return;
            }
            if (tab != null)
            {
                decimal dec = 0;
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    if (!decimal.TryParse(Request.Form[("txt_" + tab.Rows[i]["yc_Xqdm"].ToString())].Trim(), out dec))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'县区的最低分数线必须是数值类型！' ,title:'警告提示'});</script>");
                        return;
                    }
                    tab.Rows[i]["yc_ZdFensuKzx"] = dec.ToString();
                }
                zk.UPXqZdFsx(tab);
                string str = this.ddlXpcInfo.SelectedItem.Text;
                int begin = 0;
                int end = 0;
                begin = str.IndexOf('[');
                end = str.IndexOf(']');
                string[] strs = this.ddlXpcInfo.SelectedValue.Split('_');
                int tdnum = -1;
                string xqdm = "";
                int jhs = 0;

                string strxxdm = "";
                if (Request.Form["CheckBox1"] != null)
                {
                    strxxdm = Request.Form["CheckBox1"].ToString();
                    if (strxxdm.Length > 0)
                    {
                        string tishi = "";
                        for (int j = 0; j < strxxdm.Split(',').Length; j++)
                        {
                            tdnum = -1;
                            string xxdm = strxxdm.Split(',')[j];

                            if (Request.Form["bmddm"] != null)
                            {
                                xqdm = Request.Form["bmddm"].ToString().Trim();
                            }
                            else
                            {

                                DataTable pqtab = zk.select_lqjh(xxdm);
                                if (pqtab.Rows.Count == 0)
                                {
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'该招生学校无县区信息！' ,title:'警告提示'});</script>");
                                    return;
                                }
                                string xq1 = "";
                                string xq2 = "";

                                for (int i = 0; i < pqtab.Rows.Count; i++)
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            xq1 = pqtab.Rows[i]["xqdm"].ToString() + ";" + pqtab.Rows[i]["jhs"].ToString();
                                            break;
                                        case 1:
                                            xq2 = pqtab.Rows[i]["xqdm"].ToString() + ";" + pqtab.Rows[i]["jhs"].ToString();
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                if (xq2 != "")
                                {
                                    if (xq1.Length > xq2.Length)
                                    {
                                        xqdm = xq1.Split(';')[0].ToString();
                                        jhs = Convert.ToInt32(xq1.Split(';')[1].ToString());
                                    }
                                    else
                                    {
                                        xqdm = xq2.Split(';')[0].ToString();
                                        jhs = Convert.ToInt32(xq2.Split(';')[1].ToString());
                                    }
                                }
                                else
                                {
                                    xqdm = xq1.Split(';')[0].ToString();
                                    jhs = Convert.ToInt32(xq1.Split(';')[1].ToString());
                                }
                                if (tdnum == -1)
                                {
                                    DataTable tabjhs = zk.select_jhs(xxdm);
                                    tdnum = Convert.ToInt32(tabjhs.Rows[0]["yitd_fzbs_sl"]) + jhs;
                                }
                            }
                            txtfsx.Text = xqdm;
                            //   Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'" + xqdm + "！' ,title:'警告提示'}); </script>");
                            int iRe = zk.Start_TouDang(strs[0], str.Substring(begin + 1, end - begin - 1), tab, ddl_zy.SelectedValue.ToString(), Convert.ToInt32(tdnum), xxdm, 1, 2, strs[0].Substring(0, 3), 0, false, 1, xqdm);

                            switch (iRe)
                            {
                                case 0:
                                    tishi += xxdm + "投档成功!</br>";
                                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'投档操作成功！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                                    //loadToudangInfo();
                                    break;
                                case -1:
                                    tishi += xxdm + "投档失败!</br>";
                                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'投档操作失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                                    break;

                                default:
                                    break;
                            }

                        }
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'" + tishi + "' ,title:'警告提示'});</script>");
                        loadToudangInfo();
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请勾选学校！' ,title:'警告提示'});</script>");
                    return;
                }
            }
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            BLL_zk_PiLiangTouDang zk = new BLL_zk_PiLiangTouDang();
            DataTable tab = zk.selectXqZdFsx();
            int sf = 0;
            if (!int.TryParse(this.ddl_tdsf.SelectedValue, out sf))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'当前批次未定制投档条件！' ,title:'警告提示'}); </script>");
                return;
            }
            if (tab != null)
            {
                decimal dec = 0;
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    if (!decimal.TryParse(Request.Form[("txt_" + tab.Rows[i]["yc_Xqdm"].ToString())].Trim(), out dec))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'县区的最低分数线必须是数值类型！' ,title:'警告提示'});</script>");
                        return;
                    }
                    tab.Rows[i]["yc_ZdFensuKzx"] = dec.ToString();
                }
                zk.UPXqZdFsx(tab);
                string str = this.ddlXpcInfo.SelectedItem.Text;
                int begin = 0;
                int end = 0;
                begin = str.IndexOf('[');
                end = str.IndexOf(']');
                string[] strs = this.ddlXpcInfo.SelectedValue.Split('_');
                string strxxdm = "";
                if (Request.Form["CheckBox1"] != null)
                {
                    strxxdm = Request.Form["CheckBox1"].ToString();
                    if (strxxdm.Length > 0)
                    {
                        string tishi = "";
                        for (int j = 0; j < strxxdm.Split(',').Length; j++)
                        {
                            int tdnum = -1;
                            string xqdm = "";


                            string xxdm = strxxdm.Split(',')[j];

                            if (Request.Form["bmddm"] != null)
                            {
                                xqdm = Request.Form["bmddm"].ToString().Trim();
                            }
                            int iRe = zk.Start_TouDang(strs[0], str.Substring(begin + 1, end - begin - 1), tab, ddl_zy.SelectedValue.ToString(), Convert.ToInt32(tdnum), xxdm, 1, 2, strs[0].Substring(0, 3), 0, false, 2, xqdm);
                            switch (iRe)
                            {
                                case 0:
                                    tishi += xxdm + "投档成功!</br>";
                                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'投档操作成功！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                                    //loadToudangInfo();
                                    break;
                                case -1:
                                    tishi += xxdm + "投档失败!</br>";
                                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'投档操作失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                                    break;

                                default:
                                    break;
                            }

                        }
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'" + tishi + "' ,title:'警告提示'});</script>");
                        loadToudangInfo();
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请勾选学校！' ,title:'警告提示'});</script>");
                    return;
                }
            }
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            BLL_zk_PiLiangTouDang zk = new BLL_zk_PiLiangTouDang();
            DataTable tab = zk.selectXqZdFsx();
            int sf = 0;
            if (!int.TryParse(this.ddl_tdsf.SelectedValue, out sf))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'当前批次未定制投档条件！' ,title:'警告提示'}); </script>");
                return;
            }
            if (tab != null)
            {
                decimal dec = 0;
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    if (!decimal.TryParse(Request.Form[("txt_" + tab.Rows[i]["yc_Xqdm"].ToString())].Trim(), out dec))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'县区的最低分数线必须是数值类型！' ,title:'警告提示'});</script>");
                        return;
                    }
                    tab.Rows[i]["yc_ZdFensuKzx"] = dec.ToString();
                }
                zk.UPXqZdFsx(tab);
                string str = this.ddlXpcInfo.SelectedItem.Text;
                int begin = 0;
                int end = 0;
                begin = str.IndexOf('[');
                end = str.IndexOf(']');
                string[] strs = this.ddlXpcInfo.SelectedValue.Split('_');
                string strxxdm = "";
                if (Request.Form["CheckBox1"] != null)
                {
                    strxxdm = Request.Form["CheckBox1"].ToString();
                    if (strxxdm.Length > 0)
                    {
                        string tishi = "";
                        for (int j = 0; j < strxxdm.Split(',').Length; j++)
                        {
                            string xxdm = strxxdm.Split(',')[j];
                            int tdnum = -1;
                            string xqdm = "";
                            int jhs = 0;
                            if (Request.Form["bmddm"] != null)
                            {
                                xqdm = Request.Form["bmddm"].ToString().Trim();
                            }
                            else
                            {
                                string lxx = ddl_xx.SelectedValue.ToString();
                                if (lxx == "001" || lxx == "003" || lxx == "004")
                                {
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('该招生学校本县配转统请选择县区！');</script>");
                                    return;
                                }

                                DataTable pqtab = zk.select_lqjh(xxdm);
                                if (pqtab.Rows.Count == 0)
                                {
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'该招生学校无县区信息！' ,title:'警告提示'});</script>");
                                    return;
                                }
                                string xq1 = "";
                                string xq2 = "";
                                for (int i = 0; i < pqtab.Rows.Count; i++)
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            xq1 = pqtab.Rows[i]["xqdm"].ToString() + ";" + pqtab.Rows[i]["jhs"].ToString();
                                            break;
                                        case 1:
                                            xq2 = pqtab.Rows[i]["xqdm"].ToString() + ";" + pqtab.Rows[i]["jhs"].ToString();
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                if (xq2 != "")
                                {
                                    if (xq1.Length > xq2.Length)
                                    {
                                        xqdm = xq2.Split(';')[0].ToString();
                                        jhs = Convert.ToInt32(xq2.Split(';')[1].ToString());
                                    }
                                    else
                                    {
                                        xqdm = xq1.Split(';')[0].ToString();
                                        jhs = Convert.ToInt32(xq1.Split(';')[1].ToString());
                                    }
                                }
                                else
                                {
                                    xqdm = xq1.Split(';')[0].ToString();
                                    jhs = Convert.ToInt32(xq1.Split(';')[1].ToString());
                                }
                                if (tdnum == -1)
                                {
                                    DataTable tabjhs = zk.select_jhs(xxdm);
                                    tdnum = Convert.ToInt32(tabjhs.Rows[0]["yitd_fzbs_sl"]) + jhs;
                                }
                            }
                            txtfsx.Text = xqdm;
                            int iRe = zk.Start_TouDang(strs[0], str.Substring(begin + 1, end - begin - 1), tab, ddl_zy.SelectedValue.ToString(), Convert.ToInt32(tdnum), xxdm, 1, 2, strs[0].Substring(0, 3), 0, false, 3, xqdm);
                            switch (iRe)
                            {
                                case 0:
                                    tishi += xxdm + "投档成功!</br>";
                                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'投档操作成功！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                                    //loadToudangInfo();
                                    break;
                                case -1:
                                    tishi += xxdm + "投档失败!</br>";
                                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'投档操作失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                                    break;

                                default:
                                    break;
                            }

                        }
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'" + tishi + "' ,title:'警告提示'});</script>");
                        loadToudangInfo();
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请勾选学校！' ,title:'警告提示'});</script>");
                    return;
                }
            }
        }

        protected void Button11_Click(object sender, EventArgs e)
        {
            BLL_zk_PiLiangTouDang zk = new BLL_zk_PiLiangTouDang();
            DataTable tab = zk.selectXqZdFsx();
            int sf = 0;
            if (!int.TryParse(this.ddl_tdsf.SelectedValue, out sf))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'当前批次未定制投档条件！' ,title:'警告提示'}); </script>");
                return;
            }
            if (tab != null)
            {
                decimal dec = 0;
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    if (!decimal.TryParse(Request.Form[("txt_" + tab.Rows[i]["yc_Xqdm"].ToString())].Trim(), out dec))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'县区的最低分数线必须是数值类型！' ,title:'警告提示'});</script>");
                        return;
                    }
                    tab.Rows[i]["yc_ZdFensuKzx"] = dec.ToString();
                }
                zk.UPXqZdFsx(tab);
                string str = this.ddlXpcInfo.SelectedItem.Text;
                int begin = 0;
                int end = 0;
                begin = str.IndexOf('[');
                end = str.IndexOf(']');
                string[] strs = this.ddlXpcInfo.SelectedValue.Split('_');
                string strxxdm = "";
                if (Request.Form["CheckBox1"] != null)
                {
                    strxxdm = Request.Form["CheckBox1"].ToString();
                    if (strxxdm.Length > 0)
                    {
                        string tishi = "";
                        for (int j = 0; j < strxxdm.Split(',').Length; j++)
                        {
                            string xxdm = strxxdm.Split(',')[j];

                            int tdnum = -1;
                            string xqdm = "";
                            int jhs = 0;
                            if (Request.Form["bmddm"] != null)
                            {
                                xqdm = Request.Form["bmddm"].ToString().Trim();
                            }
                            else
                            {
                                string lxx = ddl_xx.SelectedValue.ToString();
                                if (lxx == "003" || lxx == "004")
                                {
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('该招生学校本县配转统请选择县区！');</script>");
                                    return;
                                }
                                DataTable pqtab = zk.select_lqjh(xxdm);
                                if (pqtab.Rows.Count == 0)
                                {
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'该招生学校无县区信息！' ,title:'警告提示'});</script>");
                                    return;
                                }

                                string xq1 = "";
                                string xq2 = "";
                                for (int i = 0; i < pqtab.Rows.Count; i++)
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            xq1 = pqtab.Rows[i]["xqdm"].ToString() + ";" + pqtab.Rows[i]["jhs"].ToString();
                                            break;
                                        case 1:
                                            xq2 = pqtab.Rows[i]["xqdm"].ToString() + ";" + pqtab.Rows[i]["jhs"].ToString();
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                if (xq2 != "")
                                {
                                    if (xq1.Length > xq2.Length)
                                    {
                                        xqdm = xq1.Split(';')[0].ToString();
                                        jhs = Convert.ToInt32(xq1.Split(';')[1].ToString());
                                    }
                                    else
                                    {
                                        xqdm = xq2.Split(';')[0].ToString();
                                        jhs = Convert.ToInt32(xq2.Split(';')[1].ToString());
                                    }
                                }
                                else
                                {
                                    xqdm = xq1.Split(';')[0].ToString();
                                    jhs = Convert.ToInt32(xq1.Split(';')[1].ToString());
                                }
                                if (tdnum == -1)
                                {
                                    DataTable tabjhs = zk.select_jhs(xxdm);
                                    tdnum = Convert.ToInt32(tabjhs.Rows[0]["yitd_fzbs_sl"]) + jhs;
                                }
                            }
                            txtfsx.Text = xqdm;
                            int iRe = zk.Start_TouDang(strs[0], str.Substring(begin + 1, end - begin - 1), tab, ddl_zy.SelectedValue.ToString(), Convert.ToInt32(tdnum), xxdm, 1, 2, strs[0].Substring(0, 3), 0, false, 3, xqdm);
                            switch (iRe)
                            {
                                case 0:
                                    tishi += xxdm + "投档成功!</br>";
                                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'投档操作成功！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                                    //loadToudangInfo();
                                    break;
                                case -1:
                                    tishi += xxdm + "投档失败!</br>";
                                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'投档操作失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
                                    break;

                                default:
                                    break;
                            }
                        }
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'" + tishi + "' ,title:'警告提示'});</script>");
                        loadToudangInfo();
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请勾选学校！' ,title:'警告提示'});</script>");
                    return;
                }
            }
        }
    }


}