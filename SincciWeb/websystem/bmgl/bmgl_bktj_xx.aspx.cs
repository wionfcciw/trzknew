using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using System.IO;

namespace SincciKC.websystem.bmgl
{
    public partial class bmgl_bktj_xx : BPage
    {
        public string xqdm = "";
        public string kaoci = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                  xqdm = Request.QueryString["xqdm"];
                  kaoci = Request.QueryString["kaoci"];
                  drpLoad();
                  BindGv();
            }
        }
        /// <summary>
        /// 加载考次
        /// </summary>
        private void drpLoad()
        {
            BLL_zk_kcdm bllkc = new BLL_zk_kcdm();
            drpKaoci.DataSource = bllkc.Select_zk_kcdm();
            drpKaoci.DataTextField = "kcmc";
            drpKaoci.DataValueField = "kcdm";
            drpKaoci.DataBind();
            drpKaoci.SelectedValue = kaoci;
        }
        /// <summary>
        /// 志愿定制BLL
        /// </summary>
        private BLL_xxgl bll = new BLL_xxgl();
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
                    this.Repeater1.DataSource = bll.Select_xxtj(xqdm, kaoci);
                    this.Repeater1.DataBind();
                }
                else
                {
                    DataTable dt = bll.Select_xxtj(xqdm, kaoci);
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
            if (fanwei.Length >= 6)
            {
                fanwei = fanwei.Substring(0, 6);
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            xqdm = Request.QueryString["xqdm"];
            kaoci = drpKaoci.SelectedValue;
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>hideTest('" + Request.QueryString["xqdm"] + "','学校报考人数统计表打印','" + drpKaoci.SelectedValue + "');</script>");
                 
        }

        protected void btntj_Click(object sender, EventArgs e)
        {
            xqdm = Request.QueryString["xqdm"];
            kaoci = drpKaoci.SelectedValue;
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string str = whereRole(Department, UserType);
            if (str.IndexOf("*") == -1)
            {
                if (str == "")
                {
                    this.Repeater1.DataSource = bll.Select_xxtj(xqdm, drpKaoci.SelectedValue);
                    this.Repeater1.DataBind();
                }
                else
                {
                    DataTable dt = bll.Select_xxtj(xqdm, drpKaoci.SelectedValue);
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
    }
}