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


namespace SincciKC.websystem.kwgl
{
    public partial class ShiJuan_tj : System.Web.UI.Page
    {
        public int UserType;
        public string Department;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Permission();
                
                BindGv();
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
        }
        #endregion
        /// <summary>
        /// 毕业中学控制类
        /// </summary>
        private BLL_zk_kd bll = new BLL_zk_kd();
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGv()
        {
            UserType = SincciLogin.Sessionstu().UserType;
            Department = SincciLogin.Sessionstu().U_department;
            string strwhere = whereRole(Department, UserType);

            this.Repeater1.DataSource = bll.Select_sj_tj(strwhere);
            this.Repeater1.DataBind();

        }
        /// <summary>
        /// 权限控制
        /// </summary>
        /// <param name="fanwei">管理范围</param>
        /// <param name="UserType">用户类型</param>
        /// <returns></returns>
        public string whereRole(string fanwei, int UserType)
        {
            
            string where = "";
            switch (UserType)
            {
                //系统管理员
                case 1:
                    where = " 1=1 ";
                    break;
                //市招生办
                case 2:
                    where = " 1=1  ";
                    break;
                //区招生办
                case 3:
                    where = " b.xqdm='" + fanwei + "' ";
                    break;
                //学校用户 
                case 4:
                    where = " 1<>1  ";
                    break;
                //班级用户 
                case 5:
                    where = " 1<>1  ";
                    break;
                default:
                    where = " 1<>1  ";
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

    }
}