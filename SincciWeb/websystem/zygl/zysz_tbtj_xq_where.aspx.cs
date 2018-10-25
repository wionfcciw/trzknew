using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using Model;
using System.IO;

namespace SincciKC.websystem.zysz
{
    public partial class zysz_tbtj_xq_where : BPage
    {
        public string str = "";
        public string tiaojian = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int UserType = SincciLogin.Sessionstu().UserType;
                string Department = SincciLogin.Sessionstu().U_department;

                Permission();

                if (UserType == 4)
                    Response.Redirect("zysz_tbtj_xx_where.aspx?tiaojian=0_0_0&xqdm=" + Department.Substring(0, 4) + "&where=&title=学校志愿填报分析");
                if (UserType == 5)
                    Response.Redirect("zysz_tbtj_bj_where.aspx?tiaojian=0_0_0&bmddm=" + Department.Substring(0, 6) + "&xqdm=" + Department.Substring(0, 4) + "&where=&title=班级填报志愿分析");

                ShowLoad();
                BindGv();
                tiaojian = listxb.SelectedValue + "_" + listmz.SelectedValue + "_" + listlb.SelectedValue;
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

            //导出数据
            if (!new Method().CheckButtonPermission(PopedomType.A4))
            {
                this.btnExport.Visible = false;
            }

            //导出不合格名单
            if (!new Method().CheckButtonPermission(PopedomType.A8))
            {
                this.btnExport.Visible = false;
            }
        }
        #endregion
        /// <summary>
        /// 加载条件
        /// </summary>
        private void ShowLoad()
        {
            BLL_zk_zdxx bzd = new BLL_zk_zdxx();

            listxb.DataSource = bzd.selectZdxx("xb");//性别
            listxb.DataTextField = "zlbmc";
            listxb.DataValueField = "zlbdm";
            listxb.DataBind();

            listxb.Items.Insert(0, new ListItem("全部", "0"));

            listmz.Items.Insert(0, new ListItem("全部", "0"));
            listmz.Items.Insert(1, new ListItem("汉族", "01"));
            listmz.Items.Insert(2, new ListItem("少数民族", "02"));

            listlb.DataSource = bzd.selectZdxx("kslb");//性别
            listlb.DataTextField = "zlbmc";
            listlb.DataValueField = "zlbdm";
            listlb.DataBind();
            listlb.Items.Insert(0, new ListItem("全部", "0"));
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
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;

            string str = whereRole(Department, UserType);
            if (str.IndexOf("*") == -1)
            {
                if (str == "")
                {
                    this.Repeater1.DataSource = bll.Select_xqtj_Where(Where());
                    this.Repeater1.DataBind();
                }
                else
                {
                    DataTable dt = bll.Select_xqtj_Where(Where());
                    DataTable dtnew = dt.Clone();
                    DataRow[] dr = dt.Select(" xqdm='" + str + "'");
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
            if (fanwei.Length >= 4)
            {
                fanwei = fanwei.Substring(0, 4);
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
                    where = fanwei;
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
        /// <summary>
        /// 条件
        /// </summary>
        private string Where()
        {
            string where = "";
            //xb 1男2女
            if (listxb.SelectedValue != "0")
            {
                where = where + "xbdm=" + listxb.SelectedValue + " and ";
            }
            if (listmz.SelectedValue != "0")
            {
                if (listmz.SelectedValue == "01")
                {
                    where = where + "mzdm='01' and ";
                }
                else
                {
                    where = where + "mzdm!='01' and ";
                }

            }
            if (listlb.SelectedValue != "0")
            {
                where = where + "kslbdm='" + listlb.SelectedValue + "' and ";
            }
            if (where.Length > 0)
            {
                where = where.Substring(0, where.Length - 4);

            }
            str = where;
            str = str.Replace("'", "_");
            return where;
        }
        /// <summary>
        /// 分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            tiaojian = listxb.SelectedValue + "_" + listmz.SelectedValue + "_" + listlb.SelectedValue;
            BindGv();
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

        /// <summary>
        /// 导出不合格名单
        /// </summary> 
        protected void btnExportHKcj_Click(object sender, EventArgs e)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;

            string where = "";
            switch (UserType)
            {

                //系统管理员
                case 1:
                    where = " (Swdj='D' or Dldj='D') ";
                    break;
                //市招生办
                case 2:
                    where = " (Swdj='D' or Dldj='D') ";
                    break;
                //区招生办
                case 3:
                    where = " (Swdj='D' or Dldj='D') and right(left(ksh,6),4) = '" + Department + "'  ";
                    break;
                //学校用户 
                case 4:
                    where = " (Swdj='D' or Dldj='D') and right(left(ksh,8),6) = '" + Department + "' ";
                    break;
                //班级用户 
                case 5:
                    where = " 1<>1 ";
                    break;
                default:
                    where = " 1<>1";
                    break;
            }

            GridView gvOrders = new GridView();
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "GB2312";
            string name = "导出会考不合格名单" + DateTime.Now.ToLongDateString();
            name = System.Web.HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8);
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + name + ".xls");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");  //设置输出流为简体中文 
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            gvOrders.AllowPaging = false;
            gvOrders.AllowSorting = false;
            gvOrders.DataSource = new BLL_zk_kshkcj().ExportEXCELKsh(where);
            gvOrders.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            gvOrders.DataBind();
            gvOrders.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}