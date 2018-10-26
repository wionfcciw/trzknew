using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;
using System.Data;

namespace SincciKC.websystem.zysz
{
    public partial class zysz_tbtj_bj : BPage
    {
        private string xqdm = "";
        private string bmddm = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                xqdm = Request.QueryString["xqdm"];
                bmddm = Request.QueryString["bmddm"];
                BindGv();
            }
        }
        /// <summary>
        /// 志愿定制BLL
        /// </summary>
        private BLL_zk_zydz bll = new BLL_zk_zydz();
        /// <summary>
        /// 权限控制
        /// </summary>
        /// <param name="fanwei">管理范围</param>
        /// <param name="UserType">用户类型</param>
        /// <returns></returns>
        public string whereRoleBJ(string fanwei, int UserType)
        {
            if (fanwei.Length >= 8)
            {
                fanwei = fanwei.Substring(6);
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
                    where = "";
                    break;
                //班级用户 
                case 5:
                    where = fanwei;
                    break;
                default:
                    where = "*";
                    break;
            }
            return where;
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGv()
        {
         
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string str = whereRoleBJ(Department, UserType);
            if (str.IndexOf("*") == -1)
            {
                if (str == "")
                {
                    this.Repeater1.DataSource = bll.Select_bjtj(bmddm, xqdm);
                    this.Repeater1.DataBind();
                }
                else
                {
                    DataTable dt = bll.Select_bjtj(bmddm, xqdm);
                    DataTable dtnew = dt.Clone();
                    DataRow[] dr = dt.Select(" bjdm='" + str + "'");
                    foreach (DataRow item in dr)
                    {
                        dtnew.ImportRow(item);
                    }
                    this.Repeater1.DataSource = dtnew;
                    this.Repeater1.DataBind();
                }
            }
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
                    where = " ( ISNULL(zyksqr,0)=0 ) ";
                    break;
                //市招生办
                case 2:
                    where = " ( ISNULL(zyksqr,0)=0 ) ";
                    break;
                //区招生办
                case 3:
                    where = " ( ISNULL(zyksqr,0)=0 ) and right(left(ksh,6),4) = '" + Department + "'  ";
                    break;
                //学校用户 
                case 4:
                    where = " ( ISNULL(zyksqr,0)=0 ) and right(left(ksh,8),6) = '" + Department + "' ";
                    break;
                //班级用户 
                case 5:
                    where = " ( ISNULL(zyksqr,0)=0 ) and right(left(ksh,8),6) = '" + Department.Substring(0,6) + "' and bjdm='" + Department.Substring(6) + "'";
                    break;
                default:
                    where = " 1<>1";
                    break;
            }

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
           // gvOrders.DataSource = new BLL_zk_ksxxgl().ExportNoZy(where);
            gvOrders.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            gvOrders.DataBind();
            gvOrders.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}