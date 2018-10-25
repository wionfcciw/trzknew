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
    public partial class Fsx : BPage
    {
        private BLL_LQK_Ks_Xx zk = new BLL_LQK_Ks_Xx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Permission();
                this.Repeater1.DataSource = zk.SelectLqtj();
                this.Repeater1.DataBind();
          
            }
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
           

            this.Repeater1.DataSource = zk.SelectLqtj();
            this.Repeater1.DataBind();
          
            
        }
    }
}