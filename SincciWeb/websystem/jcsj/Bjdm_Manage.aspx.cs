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
namespace SincciKC.websystem.jcsj
{
    public partial class Bjdm_Manage : BPage
    {
        /// <summary>
        /// 县区代码控制类
        /// </summary>
        BLL_zk_xqdm BLL_xqdm = new BLL_zk_xqdm();
        /// <summary>
        /// 班级代码控制类
        /// </summary>
        BLL_zk_bjdm bll = new BLL_zk_bjdm();

       
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Permission();
                //绑定PageSize数据
                this.ddlPageSize.DataSource = new config().PageSizelist();
                this.ddlPageSize.DataBind();

                int UserType = SincciLogin.Sessionstu().UserType;
                string Department = SincciLogin.Sessionstu().U_department;

                Loadsq();
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
            DataTable tab = new BLL_zk_bjdm().ExecuteProc(strWhere, pageSize, AspNetPager1.CurrentPageIndex, ref RecordCount);
            this.repDisplay.DataSource = tab;
            this.repDisplay.DataBind();

            //分页
            config.AspNetPagerCustomInfoHTML2(AspNetPager1, RecordCount, pageSize);
        }

        /// <summary>
        /// 加载市区信息
        /// </summary>
        private void Loadsq()
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            this.ddlxqdm.DataSource = BLL_xqdm.SelectXqdm(Department, UserType);
            this.ddlxqdm.DataTextField = "xqmc";
            this.ddlxqdm.DataValueField = "xqdm";
            this.ddlxqdm.DataBind();

            this.ddlxqdm.Items.Insert(0, new ListItem("-请选择-", ""));
        }

        

        #region "查询"
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            BindGv();
        }
        /// <summary>
        /// 创建查询条件。
        /// </summary>
        /// <returns></returns>
        private string createWhere()
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string isAnd = "";
            string whereStr = "";

            if (!string.IsNullOrEmpty(ddlxqdm.SelectedItem.Value.Trim()))
            {
                isAnd = " And ";
                whereStr += " A.xqdm Like '%" + ddlxqdm.SelectedItem.Value.Trim() + "%' ";
            }

            if (!string.IsNullOrEmpty(dlistXx.SelectedItem.Value.Trim()))
            {
                whereStr += isAnd + " A.xxdm Like '%" + dlistXx.SelectedItem.Value.Trim() + "%' ";
                isAnd = " And ";
            }

            if (!string.IsNullOrEmpty(txtName.Text.Trim()))
                whereStr += isAnd + "( A.bjdm Like '%" + config.CheckChar(txtName.Text.Trim()) + "%' Or A.bjmc Like '%" + config.CheckChar(txtName.Text.Trim()) + "%')";


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
                //区招生办
                case 3:
                    where = " A.xqdm = '" + Department + "'  ";
                    break;
                //学校用户 
                case 4:
                    where = " A.xxdm  = '" + Department + "' ";
                    break;
                //班级用户 
                case 5:
                    where = " A.bjdm = '" + Department.Substring(6) + "' and A.xxdm ='" + Department.Substring(0, 6) + "' ";
                    break;
                default:
                    where = " 1<>1";
                    break;
            }
            if (whereStr.Length > 0)
            {
                whereStr = whereStr + " and " + where;
            }
            else
            {
                whereStr = where;
            }

            return whereStr;
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
            //新增
            if (!new Method().CheckButtonPermission(PopedomType.A4))
            {
                this.btnNew.Visible = false;
            }
            //修改
            if (!new Method().CheckButtonPermission(PopedomType.A8))
            {
                this.btnEdit.Enabled = false;
            }
            //删除
            if (!new Method().CheckButtonPermission(PopedomType.A16))
            {
                this.btnDelete.Visible = false;
            }
            //导入数据
            if (!new Method().CheckButtonPermission(PopedomType.A32))
            {
                divdaor.Visible = false;
            }
        }
        #endregion

       public string E_record = "";

        /// <summary>
        /// 点击删除。
        /// </summary>
       protected void btnDelete_Click(object sender, EventArgs e)
       {
           BLL_zk_bjdm bll = new BLL_zk_bjdm();

           bool result = false;
           if (Request.Form["CheckBox1"] != null)
           {
               result = bll.DeleteDataByLsh(Request.Form["CheckBox1"].ToString().Split(',').ToList());
           }
           if (result)
           {
               E_record = "删除: 班级数据：" + Request.Form["CheckBox1"].ToString() + "";
               EventMessage.EventWriteDB(1, E_record);

               Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                     "<script>ymPrompt.succeedInfo({message:'操作成功！' ,title:'操作提示'});setTimeout(function(){window.location.href=window.location.href;},1000);</script>");
           }
           else
           {
               Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                   "<script>ymPrompt.alert({message:'操作失败！' ,title:'提示'});</script>");
           }
       }
 
        /// <summary>
        /// 上下一页
        /// </summary> 
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        { 
            BindGv();
        }

        /// <summary>
        /// 选择县区显示学校
        /// </summary> 
        protected void ddlxqdm_SelectedIndexChanged(object sender, EventArgs e)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string xqdm = this.ddlxqdm.SelectedValue;

            dlistXx.DataSource = new BLL_zk_xxdm().Select_zk_xxdmXQ(xqdm, Department, UserType);
            dlistXx.DataTextField = "xxmc";
            dlistXx.DataValueField = "xxdm";
            dlistXx.DataBind();
            this.dlistXx.Items.Insert(0, new ListItem("--请选择--", ""));

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

                string result = bll.ImportExcelData(path,chkIsZL.Checked);
                E_record = "导入数据：导入班级数据。";
                EventMessage.EventWriteDB(1, E_record);

                msgWindow.InnerHtml = result;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                    "<script>$(function () {if(confirm('导入完成是否需要查看导入日志？'))$('#msgWindow').window('open');});</script>");
            }

            BindGv();
        }
 
    }
}