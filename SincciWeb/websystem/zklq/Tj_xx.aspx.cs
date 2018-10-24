using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
using System.Data;
using System.IO;

namespace SincciKC.websystem.zklq
{
    public partial class Tj_xx : BPage
    {
        public string kaoci = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Permission();
              
                string Department = SincciLogin.Sessionstu().UserName;

                string xxdm = Department;
                int UserType = SincciLogin.Sessionstu().UserType;

                if (UserType != 8 && UserType != 9 &&  UserType != 1)
                {
                    Response.Write("<script language='javascript'>window.parent.location.href='/ht/login.aspx';</script>");
                    return;
                }
                if (Request.QueryString["lqxx"] != null)
                {
                    xxdm = Request.QueryString["lqxx"].ToString();
                }
                loadPcInfo(xxdm);
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
            //excel
            if (!new Method().CheckButtonPermission(PopedomType.A8))
            {
                btnExcel.Visible = false;
            }
            //所有毕业中学
            if (!new Method().CheckButtonPermission(PopedomType.A16))
            {
                btnAll.Visible = false;
            }
            //打印
            if (!new Method().CheckButtonPermission(PopedomType.A32))
            {
                btnPrint.Visible = false;
            }
        }
        #endregion
        /// <summary>
        /// 志愿定制BLL
        /// </summary>
        private BLL_xxlq bll = new BLL_xxlq();
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGv()
        {
            DataTable dtLast = new DataTable();
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().UserName;
            if (Request.QueryString["lqxx"] != null)
            {
                Department = Request.QueryString["lqxx"].ToString();
            }
            string pcdm=ddlXpcInfo.SelectedValue;
            dtLast = bll.tj_xx(" lqxx='" + Department + "'   and pcdm='" + pcdm + "'", " xxdm='" + Department + "'  and pcdm='" + pcdm + "'");
            Repeater1.DataSource = dtLast;
            Repeater1.DataBind();
        }
        /// <summary>
        /// 加载批次信息。
        /// </summary>
        private void loadPcInfo(string xxdm)
        {
            BLL_xxlq zk = new BLL_xxlq();
            DataTable tab = zk.selectPcdm(xxdm);
            this.ddlXpcInfo.DataSource = tab;
            this.ddlXpcInfo.DataTextField = "xpc_mc";
            this.ddlXpcInfo.DataValueField = "pcDm";
            this.ddlXpcInfo.DataBind();
            this.ddlXpcInfo.Items.Insert(0, new ListItem("-请选择-", ""));
        }
        /// <summary>
        /// 权限控制
        /// </summary>
        /// <param name="fanwei">管理范围</param>
        /// <param name="UserType">用户类型</param>
        /// <returns></returns>
        public string whereRole(string fanwei, int UserType)
        {
            if (fanwei.Length  >= 4)
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
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExcel_Click(object sender, EventArgs e)
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
        /// <summary>
        /// 导出所有毕业中学
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAll_Click(object sender, EventArgs e)
        {
            string strDate1 = DateTime.Now.ToString("yyyyMMddHHmmss");
            GridView gvOrders = new GridView();
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "GB2312";
            string name = "所有毕业中学" + strDate1;
            name = System.Web.HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8);
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + name + ".xls");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");  //设置输出流为简体中文 
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            gvOrders.AllowPaging = false;
            gvOrders.AllowSorting = false;
            gvOrders.DataSource = new BLL_xxgl().Select_alltj();
            gvOrders.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            gvOrders.DataBind();
            gvOrders.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }
        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btntj_Click(object sender, EventArgs e)
        {
            BindGv();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //kaoci = drpKaoci.SelectedValue;
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script> hideTest('县区报考人数统计表打印','" + drpKaoci.SelectedValue + "');</script>");

        }

        protected void ddlXpcInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGv();
        }

       
    }
}