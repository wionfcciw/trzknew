using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Drawing;
using System.IO;
using Model;
using BLL;
namespace SincciKC.websystem.bmsz
{
    public partial class PhotoUpLost : BPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Permission();
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

            //上传
            if (!new Method().CheckButtonPermission(PopedomType.A4))
            {
                btnSure.Enabled = false;
                btnCancel.Enabled = false;
            }
            //下载
            if (!new Method().CheckButtonPermission(PopedomType.A8))
            {
                btnLogfile.Enabled = false;
            }
        }
        #endregion
        protected void btnLogfile_Click(object sender, EventArgs e)
        {
            try
            {
                 string txtFileName =config.Get_UserName + ".txt";
                string destFileName = Server.MapPath(String.Format("~\\logfile\\UpPhotoLog\\{0}", txtFileName));

               new config().DownloadTxt(destFileName);
               string  E_record = "下载: 上传日志记录" + config.Get_UserName + "";
               EventMessage.EventWriteDB(1, E_record);
            }
            catch (Exception ex)
            {  
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'找不到日志！' ,title:'提示'});</script>");
            }

        }

       

    }
}