using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

using BLL;
using System.Data;
using System.IO;
namespace SincciKC.websystem.zysz
{
    public partial class Zyk_Manage : BPage
    {
        /// <summary>
        /// 专业库控制类
        /// </summary>
        BLL_zk_zyk bllzyk = new BLL_zk_zyk();
        /// <summary>
        /// 招生学校控制类
        /// </summary>
        BLL_zk_zsxxdm bllzsxx = new BLL_zk_zsxxdm();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Permission();
                //绑定PageSize数据
                this.ddlPageSize.DataSource = new config().PageSizelist();
                this.ddlPageSize.DataBind();
                BinZsxx();
                BindGv();
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGv()
        {

            int RecordCount = 0;
            int pageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);

            string strWhere = createWhere();
            DataTable tab = bllzyk.ExecuteProc(strWhere, pageSize, AspNetPager1.CurrentPageIndex, ref RecordCount);
            this.Repeater1.DataSource = tab;
            this.Repeater1.DataBind();

            //分页
            config.AspNetPagerCustomInfoHTML2(AspNetPager1, RecordCount, pageSize);
        }
        /// <summary>
        /// 加载招生学校数据
        /// </summary>
        private void BinZsxx()
        {
            this.ddlzsxx.DataSource = bllzsxx.Select_zk_zsxxdm();
            this.ddlzsxx.DataTextField = "zsxxmcc";
            this.ddlzsxx.DataValueField = "zsxxdm";
            this.ddlzsxx.DataBind();
            this.ddlzsxx.Items.Insert(0, new ListItem("-请选择-", ""));
        }

        /// <summary>
        /// 创建查询条件。
        /// </summary>
        /// <returns></returns>
        private string createWhere()
        {
            string keyWord = config.CheckChar(txtName.Text.Trim());
            string zsxx = this.ddlzsxx.SelectedItem.Value.Trim();
            string result = "";
            string and = "";

            if (!string.IsNullOrEmpty(zsxx))
            {
                result = " xxdm='" + zsxx + "' ";
                and = " And ";
            }

            if(!string.IsNullOrEmpty(keyWord))
                result += and + " (zydm='" + keyWord + "' Or zymc Like '%" + keyWord + "%') ";

           

            return result;
        }

        #region "查询"
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGv();
        }


        #endregion

        #region "选择 PageSize SelectedIndexChanged事件"
        /// <summary>
        /// 选择 PageSize 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGv();
        }
        #endregion

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
            // 新增
            if (!new Method().CheckButtonPermission(PopedomType.A4))
            {
                this.btnNew.Visible = false;
            }
            //删除
            if (!new Method().CheckButtonPermission(PopedomType.A8))
            {
                this.btnDelete.Visible = false;
            }
            //导入
            if (!new Method().CheckButtonPermission(PopedomType.A16))
            {
                btnExcelFileImport.Visible = false;
            }
            ////排序
            //if (!new Method().CheckButtonPermission(PopedomType.Orderby))
            //{
            //    this.Enabled = false;
            //}
            ////打印
            //if (!new Method().CheckButtonPermission(PopedomType.Print))
            //{
            //    this.btnPrint.Enabled = false;
            //}
            ////备用A
            //if (!new Method().CheckButtonPermission(PopedomType.A))
            //{
            //    this.btnA.Enabled = false;
            //}
            ////备用B
            //if (!new Method().CheckButtonPermission(PopedomType.B))
            //{
            //    this.btnB.Enabled = false;
            //}
        }
        #endregion
        private string E_record = "";
        /// <summary>
        /// 点击删除。
        /// </summary>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            bllzyk.DeleteDataByIDS(hfDelIDS.Value.Split(',').ToList());
            E_record = "删除：专业库数据:" + hfDelIDS.Value;
            EventMessage.EventWriteDB(1, E_record);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                "<script>ymPrompt.succeedInfo({message:'操作成功！' ,title:'操作提示'});setTimeout(function(){window.location.href=window.location.href;},1000);</script>");
        }

        /// <summary>
        /// 点击新增。
        /// </summary>
        protected void btnNew_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 上下一页
        /// </summary> 
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            BindGv();
        }
        /// <summary>
        /// 选择招生学校
        /// </summary> 
        protected void ddlzsxx_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGv();
        }

        //Excel文件导入
        protected void btnExcelFileImport_Click(object sender, EventArgs e)
        {
            //tmpUpLoadFile
            if (fuExcelFileImport.HasFile)
            {
                //判断文件格式
                string fileExtension = Path.GetExtension(fuExcelFileImport.FileName).ToLower();
                string[] allowedExtensions = { ".xls", ".xlsx" };

                if (allowedExtensions.Count(x => x == fileExtension) == 0)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                        "<script>ymPrompt.alert({message:'操作失败！,原因是：上传的文件不是Excel格式文件。' ,title:'提示'});</script>");
                    BindGv();
                    return;
                }

                string path = Server.MapPath("~/tmpUpLoadFile/") + config.FormatDateToStringId(DateTime.Now) + fileExtension;
                fuExcelFileImport.PostedFile.SaveAs(path);

                string result = bllzyk.ImportExcelData(path );
                E_record = "导入：专业库数据:" + config.FormatDateToStringId(DateTime.Now) + fileExtension;
                EventMessage.EventWriteDB(1, E_record);
                msgWindow.InnerHtml = result;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                    "<script>$(function () {if(confirm('导入完成是否需要查看导入日志？'))$('#msgWindow').window('open');});</script>");
            }

            BindGv();
        }
    }
}