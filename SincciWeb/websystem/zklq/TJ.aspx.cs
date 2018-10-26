using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SincciKC.websystem.zklq
{
    public partial class TJ : BPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Permission();
                loadPcInfo();
            }
        }
        BLL_LQK_Ks_Xx zk = new BLL_LQK_Ks_Xx();
        protected void ddlXpcInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlXpcInfo.SelectedValue.Length > 0)
            {
                Loadxx();
            }
        }
        /// <summary>
        /// 加载批次信息。
        /// </summary>
        private void loadPcInfo()
        {
            BLL_LQK_Ks_Xx zk = new BLL_LQK_Ks_Xx();
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string where = "";
            switch (UserType)
            {
                //系统管理员
                case 1:
                    where = where + " xqdm='500'   ";
                    break;
                //市招生办
                case 2:

                    where = " 1=1 ";

                    break;
                case 3:
                    where = where + " xqdm='500'   ";
                    break;
                //招生学校
                case 9:
                case 8:
                    where = where + " xqdm='500'   "; break;
                default:
                    where = " 1<>1";
                    break;
            }

            DataTable tab = zk.selectPcdm(where, " 1=1 ");
            this.ddlXpcInfo.DataSource = tab;
            this.ddlXpcInfo.DataTextField = "xpc_mc";
            this.ddlXpcInfo.DataValueField = "xpcid";
            this.ddlXpcInfo.DataBind();
            this.ddlXpcInfo.Items.Insert(0, new ListItem("-请选择-", ""));
        }
        /// <summary>
        /// 加载学校信息
        /// </summary>
        private void Loadxx()
        {
            string str = this.ddlXpcInfo.SelectedItem.Text;
            int begin = 0;
            int end = 0;
            begin = str.IndexOf('[');
            end = str.IndexOf(']');
            string pcdm = str.Substring(begin + 1, end - begin - 1);
            if (pcdm != "01" && pcdm != "")
            {
                pcdm = "11";
            }
            BLL_LQK_Ks_Xx zk = new BLL_LQK_Ks_Xx();
            DataTable tab = zk.Select_zk_zsxx(pcdm, " 1=1 ");
            this.dllxx.DataSource = tab;
            this.dllxx.DataTextField = "zsxxmc";
            this.dllxx.DataValueField = "xxdm";
            this.dllxx.DataBind();
            this.dllxx.Items.Insert(0, new ListItem("-请选择-", ""));
        }
        private void show01()
        {

        }
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

            //导出数据
            if (!new Method().CheckButtonPermission(PopedomType.A4))
            {
                this.btnExport.Visible = false;
            }

        }
        protected void btnExport_Click(object sender, EventArgs e)
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

            this.Repeater1.RenderControl(Htmlwriter);

            //this 表示输出本页，你也可以绑定datagrid,或其他支持obj.RenderControl()属性的控件

            base.Response.Write(writer.ToString());

            base.Response.End();
                 
        }

        protected void btntj_Click(object sender, EventArgs e)
        {
            if (ddlXpcInfo.SelectedValue != "" && dllxx.SelectedValue != "")
            {
                string str = this.ddlXpcInfo.SelectedItem.Text;
                int begin = 0;
                int end = 0;
                begin = str.IndexOf('[');
                end = str.IndexOf(']');
                string pcdm = str.Substring(begin + 1, end - begin - 1);
                string xxdm = dllxx.SelectedValue;
                string leix = ddllx.SelectedValue;
                this.Repeater1.DataSource = zk.SelectLqtj(pcdm, xxdm, leix);
                this.Repeater1.DataBind();
            }
            else
            {

            }
            
        }
    }
}