using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LinqToExcel;
using LinqToExcel.Query;
using System.Data.SqlClient;
using DAL;
using Model;
using BLL;
using System.Data;
using System.IO;
namespace SincciKC.websystem.zklq
{
    public partial class ZBS_Import : BPage
    {
        BLL_zk_xxdm bllxxdm = new BLL_zk_xxdm();
        BLL_zk_hege bll = new BLL_zk_hege();
        BLL_zk_xqdm bllxqdm = new BLL_zk_xqdm();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Permission();
                this.ddlPageSize.DataSource = new config().PageSizelist();
                this.ddlPageSize.DataBind();
                Loadsq();

                BindGv();
            }
      
        }
         
        #region "绑定数据"
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGv()
        {

            int RecordCount = 0;
            int pageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);

            string strWhere = createWhere();
            DataTable tab = bll.Execute_ZBSXX(strWhere, pageSize, AspNetPager1.CurrentPageIndex, ref RecordCount);
            this.repDisplay.DataSource = tab;
            this.repDisplay.DataBind();

            //分页
            config.AspNetPagerCustomInfoHTML2(AspNetPager1, RecordCount, pageSize);
        }
        #endregion
        #region "创建查询条件"
        /// <summary>
        /// 创建查询条件。
        /// </summary>
        /// <returns></returns>
        private string createWhere()
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string str = "";
            string xqdm = dlistSq.SelectedValue;
            string xuexiao = dlistXx.SelectedValue;
            if (xuexiao!="-1")
            {
                str = str + " xxdm='" + xuexiao + "' and ";
            }
            else if (xuexiao == "-1" && xqdm != "-1")
            {
                str = str + " xqdm='" + xqdm + "' and ";
            }


            //管理部门权限
            string where = "";
            switch (UserType)
            {
                //系统管理员
                case 1:
                    where = " 1=1 ";
                    break;
                //市招生办
                case 2:
                    where = " 1=1 ";
                    break;
                case 3:
                    if (dlistSq.SelectedValue == "-1")
                    where = " xqdm='" + Department + "'";
                    break;
                default:
                    where = " 1<>1";
                    break;
            }
            if (str.Length > 0)
            {
                str = str + where;
            }
            else
            {
                str = where;
            }

            // Response.Write(str);
            return str;
        }
        #endregion
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
            if (!new Method().CheckButtonPermission(PopedomType.A4))
            {
                ImportBtn.Visible = false;
            }
        }

        protected void ImportBtn_Click(object sender, EventArgs e)
        {
           
            if (!ImportExecl.HasFile)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请选择导入文件');</script>");
            }
            else
            {
                string fileExtension = Path.GetExtension(ImportExecl.FileName).ToLower();
                string[] allowedExtensions = { ".xls", ".xlsx" };
                if (allowedExtensions.Count(x => x == fileExtension) == 0)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                        "<script>ymPrompt.alert({message:'操作失败！,原因是：导入的文件不是Excel格式文件。' ,title:'提示'});</script>");
                }
                else
                {
                    string filename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    string path = Server.MapPath("\\Template") + "\\" + filename + ImportExecl.FileName.Substring(ImportExecl.FileName.LastIndexOf("."), ImportExecl.FileName.Length - ImportExecl.FileName.LastIndexOf("."));
                    ImportExecl.SaveAs(path);
                    ZhiBiao(path);
                }
            }
        }
        /// <summary>
        ///指标表导入
        /// </summary>
        public void ZhiBiao(string filepath)
        {
            msgWindow.InnerHtml = new BLL_LQK_Ks_Xx().Import_ZBSXX(filepath);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
    "<script>$(function () {if(confirm('导入完成是否需要查看导入日志？'))$('#msgWindow').window('open');});</script>");

            BindGv();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            bool result = bll.DelData_ZBS(hfDelIDS.Value.Split(',').ToList());
            if (result)
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                    "<script>ymPrompt.succeedInfo({message:'操作成功！' ,title:'操作提示'});setTimeout(function(){window.location.href=window.location.href;},1000);</script>");
            else
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                    "<script>ymPrompt.alert({message:'操作失败！' ,title:'提示'});</script>");
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGv();
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindGv();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGv();
        }

        protected void dlistSq_SelectedIndexChanged(object sender, EventArgs e)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;

            string xqdm = this.dlistSq.SelectedValue;
            dlistXx.DataSource = bllxxdm.Select_zk_xxdmXQ(xqdm, Department, UserType);
            dlistXx.DataTextField = "xxmc";
            dlistXx.DataValueField = "xxdm";
            dlistXx.DataBind();
            this.dlistXx.Items.Insert(0, new ListItem("-请选择-", "")); 
        }

        /// <summary>
        /// 加载市区信息
        /// </summary>
        private void Loadsq()
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;

            dlistSq.DataSource = bllxqdm.SelectXqdm(Department, UserType);
            dlistSq.DataTextField = "xqmc";
            dlistSq.DataValueField = "xqdm";
            dlistSq.DataBind();
            this.dlistSq.Items.Insert(0, new ListItem("-请选择-", "-1"));
        }

    }
}