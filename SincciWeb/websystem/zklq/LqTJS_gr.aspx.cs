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


namespace SincciKC.websystem.zklq
{
    public partial class LqTJS_gr : BPage
    {
 
      
        /// <summary>
        /// 会考成绩控制类
        /// </summary>
        BLL_zk_hege bll= new BLL_zk_hege();
 

        
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Permission();
                //绑定PageSize数据
                this.ddlPageSize.DataSource = new config().PageSizelist();
                this.ddlPageSize.DataBind();

               

             
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
            DataTable tab = bll.Execute_TJS(strWhere, pageSize, AspNetPager1.CurrentPageIndex, ref RecordCount);
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
         
            //string xuexiao = dlistXx.SelectedValue;
            //if (xuexiao.Length > 0)
            //{
            //    str = str + " type='" + xuexiao + "' and ";
            //}
          

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

       
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
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
          
         
            ////删除
            //if (!new Method().CheckButtonPermission(PopedomType.A8))
            //{
            //    btnDelete.Visible = false;
            //}
            ////导入数据
            //if (!new Method().CheckButtonPermission(PopedomType.A16))
            //{
            //    this.ImportData.Visible = false;
            //}
           
            ////导出数据
            //if (!new Method().CheckButtonPermission(PopedomType.A64))
            //{
            //    this.ExPortData.Visible = false;
            //}
        }
        #endregion

        #region "点击删除"
        /// <summary>
        /// 点击删除。
        /// </summary>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            bool result = bll.DeleteData_Tjs1(hfDelIDS.Value.Split(',').ToList());

            if (result)
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                    "<script>ymPrompt.succeedInfo({message:'操作成功！' ,title:'操作提示'});setTimeout(function(){window.location.href=window.location.href;},1000);</script>");
            else
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                    "<script>ymPrompt.alert({message:'操作失败！' ,title:'提示'});</script>");
        }
        #endregion

        #region"修改数据"
        /// <summary>
        /// 修改数据
        /// </summary> 
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (Request.Form["CheckBox1"] != null)
            {
                string[] ids = Request.Form["CheckBox1"].ToString().Split(',');

                if (ids.Length == 1)
                {
                    Model_zk_ksxxgl model = new BLL_zk_ksxxgl().zk_ksxxglDisp(ids[0]);
                    if (model.Xxqr == 1)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('学校已确认该考生的信息,不能修改!');</script>");

                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>opdg2('" + ids[0] + "', '修改数据') ;</script>");
                    }
                }
                else if (ids.Length > 1)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('不能对多个考生进行修改!');</script>");

                }

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择需要修改的考生!');</script>");

            }
        }
        #endregion

        #region "上下一页"
        /// <summary>
        /// 上下一页
        /// </summary> 
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        { 
            BindGv();
        }
        #endregion

        #region "Excel文件导入"
        /// <summary>
        /// Excel文件导入
        /// </summary> 
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

                string result = new BLL_zk_hege().ImportExcelData_Tjs(path);
                string E_record = "导入: 推荐生数据:" + config.FormatDateToStringId(DateTime.Now) + fileExtension;
                EventMessage.EventWriteDB(1, E_record);
                msgWindow.InnerHtml = result;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
              "<script>$(function () {if(confirm('导入完成是否需要查看导入日志？'))$('#msgWindow').window('open');});</script>");
           //     Page.ClientScript.RegisterStartupScript(Page.GetType(), "",  "<script>alert('导入完成!');</script>");
            }

            BindGv();
        }
        #endregion

      
       

        #region "导出数据"
        /// <summary>
        /// 导出数据
        /// </summary> 
        protected void btnExport_Click(object sender, EventArgs e)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
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
                    where = " right(left(a.ksh,6),4) = '" + Department + "'  ";
                    break;
                //学校用户 
                case 4:
                    where = " right(left(a.ksh,8),6) = '" + Department + "' ";
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
            string name = "导出会考成绩" + DateTime.Now.ToLongDateString();
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
        #endregion

        #region "下载日志"
        /// <summary>
        /// 下载日志
        /// </summary> 
        protected void btnDTxt_Click(object sender, EventArgs e)
        {
            if (File.Exists(Server.MapPath("~\\Temp\\" + config.Get_UserName + ".txt")))
            {
                new config().DownloadTxt(Server.MapPath("~\\Temp\\" + config.Get_UserName + ".txt"));
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                                  "<script>alert('没有导入日志!');</script>");
            }
        }
        #endregion
 
    }
}