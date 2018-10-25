using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using System.Text;
using Model;
using System.IO;

namespace SincciKC
{
  
    /// <summary>
    /// 阅档
    /// </summary>
    public partial class LQDAOForm : BPage
    {
        /// <summary>
        /// 页面加载。
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Permission();
               

                //if (UserType != 1 )
                //{
                //    Response.Write("你没有页面查看的权限！");
                //    Response.End();
                //}
              
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
        }

       

        protected void btnExcelFileImport_Click(object sender, EventArgs e)
        {
           
            if (fuExcelFileImport.HasFile)
            {
                //判断文件格式
                string fileExtension = Path.GetExtension(fuExcelFileImport.FileName).ToLower();
                string[] allowedExtensions = { ".xls", ".xlsx" };

                if (allowedExtensions.Count(x => x == fileExtension) == 0)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                        "<script>ymPrompt.alert({message:'操作失败！,原因是：上传的文件不是Excel格式文件。' ,title:'提示'});</script>");
                   
                    return;
                }

                string path = Server.MapPath("~/tmpUpLoadFile/") + config.FormatDateToStringId(DateTime.Now) + fileExtension;
                fuExcelFileImport.PostedFile.SaveAs(path);
                BLL_LQK_Ks_Xx zk = new BLL_LQK_Ks_Xx();
                string result = zk.ImportExcelData(path);
                string   E_record = "导入录取:" + config.FormatDateToStringId(DateTime.Now) + fileExtension;
                EventMessage.EventWriteDB(1, E_record);
                msgWindow.InnerHtml = result;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                    "<script>$(function () {if(confirm('导入完成是否需要查看导入日志？'))$('#msgWindow').window('open');});</script>");
            }
        }
    }
}