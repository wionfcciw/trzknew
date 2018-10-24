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
    public partial class HSDAOForm : BPage
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
            
                string result = zk.ImportExcelData2(path, ref listksh);
                
                string   E_record = "导入回收:" + config.FormatDateToStringId(DateTime.Now) + fileExtension;
                EventMessage.EventWriteDB(1, E_record);
                msgWindow.InnerHtml = result;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                    "<script>$(function () {if(confirm('导入完成是否需要查看导入日志？'))$('#msgWindow').window('open');});</script>");
                SHowload(listksh);
              
            }
        }
        /// <summary>
        /// 报名号集合
        /// </summary>
        private List<string> listksh = new List<string>();
 
        private void SHowload(List<string> listksh)
        {

            if (listksh.Count > 0)
            {
                string ksh = "";
                for (int i = 0; i < listksh.Count; i++)
                {
                    ksh = ksh + listksh[i] + ",";
                }
                ksh = ksh.Remove(ksh.Length - 1);
                string str = " ksh in (" + ksh + ")";
                DataTable tab = new DataTable();
                BLL_LQK_Ks_Xx zk = new BLL_LQK_Ks_Xx();
                tab = zk.selectksh(str);

                repDisplay.DataSource = tab;

                this.repDisplay.DataBind();
            }
              
        }
        protected void repDisplay_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (ViewState["SortDirection"] == null)
                ViewState["SortDirection"] = "DESC";
            if (ViewState["SortDirection"].ToString() == "ASC")
                ViewState["SortDirection"] = "DESC";
            else
                ViewState["SortDirection"] = "ASC";
            ViewState["SortExpression"] = e.SortExpression;
        }

        protected void btn_hs_Click(object sender, EventArgs e)
        {
            if (Request.Form["CheckBox1"] != null)
            {
                string ids = Request.Form["CheckBox1"].ToString();
                if (ids.Length > 0)
                {
                    BLL_LQK_Ks_Xx zk = new BLL_LQK_Ks_Xx();
                    int UserType = SincciLogin.Sessionstu().UserType;
                    string Department = SincciLogin.Sessionstu().U_department;
                    string where = "  ksh in(" + ids + ") ";
                    string where2 = "  ksh in(" + ids + ") ";
                    switch (UserType)
                    {
                        case 3:
                            where = " left(ksh,6)='14" + Department + "' and left(pcdm,1)='1'  and ksh in(" + ids + ") ";
                            break;
                        default:
                            break;
                    }

                    if (zk.ksh_hs(where, where2))
                    {
                        string E_record = "回收: 考生数据：" + ids + "";
                        EventMessage.EventWriteDB(1, E_record);
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('操作成功！')</script>");
                        SHowload(listksh);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('操作失败！')</script>");
                    }

                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择您所需要回收的用户！')</script>");

            }
        }

        
      
    }
}