using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using System.Data;

using System.Web.UI.HtmlControls;
using System.Text;

namespace SincciKC.websystem.kwgl
{
    public partial class KaoDian_AddEdit : BPage
    {
        /// <summary>
        /// 县区代码控制类
        /// </summary>
        BLL_zk_xqdm BLL_xqdm = new BLL_zk_xqdm();
        /// <summary>
        /// 考点代码控制类
        /// </summary>
        BLL_zk_kd BLL_kd = new BLL_zk_kd();

        string kddm = Convert.ToString(config.sink("kddm", config.MethodType.Get, 255, 1, config.DataType.Str));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Loadsq();
                if (kddm.Length > 0)
                {
                    loadData(kddm);
                }
            }
        }


        /// <summary>
        /// 加载市区信息
        /// </summary>
        private void Loadsq()
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;

            this.ddlxqdm.DataSource = BLL_xqdm.SelectXqdm(Department, UserType);
            this.ddlxqdm.DataTextField = "xqmc";
            this.ddlxqdm.DataValueField = "xqdm";
            this.ddlxqdm.DataBind();

            this.ddlxqdm.Items.Insert(0, new ListItem("-请选择-", ""));
        }

        private void loadData(string kddm)
        {



            Model_zk_kd info = BLL_kd.ViewDisp(kddm);
            this.txtkddm.Text = info.Kddm;
            this.txtkddm.Enabled = false;
            this.ddlxqdm.SelectedValue = info.Xqdm;
            this.ddlxqdm.Enabled = false;
            this.txtkdmc.Text = info.Kdmc;
            this.lbllsh.Text = info.Lsh.ToString();

            //this.bmdinfo.InnerHtml = Binxxdm(info.Xqdm);

            //DataTable dt = new BLL_zk_kd_xx().Select(info.Kddm);
            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        this.CheckBoxList1.Items.Insert(0, new ListItem('[' + dt.Rows[i]["xxdm"].ToString().Trim() + ']' + dt.Rows[i]["xxmc"].ToString().Trim(), dt.Rows[i]["xxdm"].ToString().Trim()));
            //        CheckBoxList1.Items[0].Selected = true;
            //    }
            //}



        }

        private string Binxxdm(string xqdm)
        {
          

            StringBuilder sb = new StringBuilder();

            sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            DataTable dt = BLL_kd.Select_zk_xxdmXQ(xqdm);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("<tr>");

                    string minksh = "";
                    string maxksh = "";
                    string xxdm = "";

                    xxdm = dt.Rows[i]["xxdm"].ToString().Trim();

                    DataTable dt1 = BLL_kd.Select_ksh_M(xxdm);
                    if (dt1.Rows.Count > 0)
                    {
                        minksh = dt1.Rows[0]["minksh"].ToString().Trim();
                        maxksh = dt1.Rows[0]["maxksh"].ToString().Trim();

                    }
                    dt1 = null;

                    DataTable dt2 = BLL_kd.Select_kd_xx(kddm, xxdm);
                    if (dt2.Rows.Count > 0)
                    {

                        sb.Append("   <td>");
                        sb.Append(" <input name='bmddm' type='checkbox'  checked='checked' value='" + dt.Rows[i]["xxdm"].ToString() + "'>" + dt.Rows[i]["xxmc"].ToString() + "&nbsp;&nbsp;");
                        sb.Append("   </td>");
                        sb.Append("   <td>");
                        sb.Append("起始考号：<input name='bksh' type='text' size='13' value='" + dt2.Rows[0]["Bksh"].ToString() + "' maxlength='14'>&nbsp;&nbsp;");
                        sb.Append("   </td>");
                        sb.Append("   <td>");
                        sb.Append("结束考号：<input name='eksh' type='text' size='13'  value='" + dt2.Rows[0]["Eksh"].ToString() + "' maxlength='14'>");
                        sb.Append("   </td>");
                    }
                    else
                    {
                        sb.Append("   <td>");
                        sb.Append("<input name='bmddm' type='checkbox' value='" + dt.Rows[i]["xxdm"].ToString() + "'>" + dt.Rows[i]["xxmc"].ToString() + " &nbsp;&nbsp;");
                        sb.Append("   </td>");
                        sb.Append("   <td>");
                        sb.Append("起始考号：<input name='bksh' type='text' size='13' value='" + minksh + "' maxlength='14'>&nbsp;&nbsp;");
                        sb.Append("   </td>");
                        sb.Append("   <td>");
                        sb.Append("结束考号：<input name='eksh' type='text' size='13' value='" + maxksh + "' maxlength='14'>");
                        sb.Append("   </td>");
                    }
                    dt2 = null;
                    sb.Append("</tr>");
                }

            }
            sb.Append("</table>");
            return sb.ToString();
        }

       /// <summary>
       /// 保存数据
       /// </summary> 
        protected void btnEnter_Click(object sender, EventArgs e)
        {
            string xqdm = this.ddlxqdm.SelectedValue;
            string kddm = this.txtkddm.Text.Trim().ToString();
            string kdmc = this.txtkdmc.Text.Trim().ToString();
            string lsh = this.lbllsh.Text.Trim().ToString();
             
             
             string  sBmd ="";
            string  sBksh = "";
            string  sEksh ="" ;

        

            Model_zk_kd item = new Model_zk_kd();
            item.Xqdm = xqdm;
            item.Kddm = kddm;
            item.Kdmc = kdmc;
            if (lsh.Length > 0)
                item.Lsh = int.Parse(lsh);

            if (xqdm.Length == 0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请选择县区！' ,title:'提示'});</script>");
            }
            else
            {
                if (kddm.Length != 4)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请输入四位的考点代码！' ,title:'提示'});</script>");
                }
                else
                {

                    if (kdmc.Length == 0)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请输入代码名称！' ,title:'提示'});</script>");
                    }
                    else
                    {

                        string str = "";
                        if (BLL_kd.Insert_zk_kd(item, sBmd, sBksh, sEksh, ref str))
                        {
                            string E_record = "新增/修改: 考点：" + item.Kddm + item.Kdmc + " ";

                            EventMessage.EventWriteDB(1, E_record);
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.succeedInfo({message:'操作成功！' ,title:'操作提示'});setTimeout(function(){window.parent.location.href=window.parent.location.href;window.parent.ymPrompt.close();},1000);</script>");

                        }
                        else if (str.Length > 0)
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'" + str + "！' ,title:'提示'});</script>");
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'操作失败！' ,title:'提示'});</script>");
                        }

                    }
                }
            }
        }
        /// <summary>
        /// 选择区显示相对应的毕业中学
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlxqdm_SelectedIndexChanged(object sender, EventArgs e)
        { 
            string xqdm = this.ddlxqdm.SelectedValue;

     //       this.bmdinfo.InnerHtml = Binxxdm(xqdm);

            if (this.ddlxqdm.SelectedValue.Length > 0)
            {
                this.txtkddm.Text = this.ddlxqdm.SelectedValue.Substring(2);
            }
        }
    }
}