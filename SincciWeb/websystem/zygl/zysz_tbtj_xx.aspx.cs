using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using System.IO;

namespace SincciKC.websystem.zysz
{
    public partial class zysz_tbtj_xx : BPage
    {
        public string xqdm = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                xqdm = Request.QueryString["xqdm"];
                BindGv();
            }
        }
        /// <summary>
        /// 志愿定制BLL
        /// </summary>
        private BLL_zk_zydz bll = new BLL_zk_zydz();
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGv()
        {
            DataTable dpcdt = new DataTable();
            dpcdt = bll.Select_All_DpcIsPass("500");
            //判断大批次的填报时间
            for (int i = 0; i < dpcdt.Rows.Count; i++)
            {
                if (!(config.DateTimeCompare(DateTime.Now, Convert.ToDateTime(dpcdt.Rows[i]["stime"])) > 0 && config.DateTimeCompare(DateTime.Now, Convert.ToDateTime(dpcdt.Rows[i]["etime"])) < 0))
                {
                    dpcdt.Rows.Remove(dpcdt.Rows[i]);
                    i--;
                }
            }
            if (dpcdt.Rows.Count == 0)
            {
                lblshow.Visible = true;
                divdata.Visible = false;
                return;
            }
            else
            {
                lblshow.Visible = false;
            }
            string pcdm = dpcdt.Rows[0]["dpcDm"].ToString() + "1";
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string str = whereRole(Department, UserType);
            if (str.IndexOf("*") == -1)
            {
                if (str == "")
                {
                    this.Repeater1.DataSource = bll.Select_xxtj(xqdm, pcdm);
                    this.Repeater1.DataBind();
                }
                else
                {
                    DataTable dt = bll.Select_xxtj(xqdm, pcdm);
                    DataTable dtnew = dt.Clone();
                    DataRow[] dr = dt.Select(" bmddm='" + str + "'");
                    foreach (DataRow item in dr)
                    {
                        dtnew.ImportRow(item);
                    }
                    this.Repeater1.DataSource = dtnew;
                    this.Repeater1.DataBind();
                }
            }
        }

        /// <summary>
        /// 权限控制
        /// </summary>
        /// <param name="fanwei">管理范围</param>
        /// <param name="UserType">用户类型</param>
        /// <returns></returns>
        public string whereRole(string fanwei, int UserType)
        {
            if (fanwei.Length >= 5)
            {
                fanwei = fanwei.Substring(0, 5);
            }
            string where = "";
            switch (UserType)
            {
                //系统管理员
                case 1:
                    where = "";
                    break;
                //市招生办
                case 2:
                    where = "";
                    break;
                //区招生办
                case 3:
                    where = "";
                    break;
                //学校用户 
                case 4:
                    where = fanwei;
                    break;
                //班级用户 
                case 5:
                    where = "*";
                    break;
                default:
                    where = "*";
                    break;
            }
            return where;
        }
        #region"导出数据"
        /// <summary>
        /// 导出数据
        /// </summary> 
        protected void btnExport_Click(object sender, EventArgs e)
        {


            string strDate1 = "统计表" + DateTime.Now.ToString("yyyyMMddHHmmss");

            strDate1 = System.Web.HttpUtility.UrlEncode(strDate1, System.Text.Encoding.UTF8);


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
        #endregion

        protected void btnNo_Click(object sender, EventArgs e)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;

            string where = "";
            switch (UserType)
            {

                //系统管理员
                case 1:
                    where = " 1=1  ";
                    break;
                //市招生办
                case 2:
                    where = " 1=1  ";
                    break;
                //区招生办
                case 3:
                    where = "   bmdxqdm = '" + Department + "'  ";
                    break;
                //学校用户 
                case 4:
                    where = "   bmddm = '" + Department + "' ";
                    break;
                //班级用户 
                case 5:
                    where = " 1<>1 ";
                    break;
                default:
                    where = " 1<>1";
                    break;
            }
            DataTable dpcdt = new DataTable();
            dpcdt = bll.Select_All_DpcIsPass("500");
            //判断大批次的填报时间
            for (int i = 0; i < dpcdt.Rows.Count; i++)
            {
                if (!(config.DateTimeCompare(DateTime.Now, Convert.ToDateTime(dpcdt.Rows[i]["stime"])) > 0 && config.DateTimeCompare(DateTime.Now, Convert.ToDateTime(dpcdt.Rows[i]["etime"])) < 0))
                {
                    dpcdt.Rows.Remove(dpcdt.Rows[i]);
                    i--;
                }
            }

            string pcdm = dpcdt.Rows[0]["dpcDm"].ToString() + "1";
            GridView gvOrders = new GridView();
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "GB2312";
            string name = "导出志愿未填报名单" + DateTime.Now.ToLongDateString();
            name = System.Web.HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8);
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + name + ".xls");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");  //设置输出流为简体中文 
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            gvOrders.AllowPaging = false;
            gvOrders.AllowSorting = false;
            gvOrders.DataSource = new BLL_zk_ksxxgl().ExportNoZy(where,pcdm);
            gvOrders.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            gvOrders.DataBind();
            gvOrders.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}