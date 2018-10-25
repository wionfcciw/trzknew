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
    public partial class ZCXXWebForm : BPage
    {
        /// <summary>
        /// 页面加载。
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Permission();
                int UserType = SincciLogin.Sessionstu().UserType;
                if (UserType != 9)
                {
                    Response.Write("你没有页面查看的权限！");
                    Response.End();
                    return;
                }
                string Department = SincciLogin.Sessionstu().UserName;
                string xxdm = Department;
              //  loadPcInfo(xxdm);
                Loadzy(xxdm);
                loadToudangInfo();
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



        }
        #endregion
        /// <summary>
        /// 加载批次信息。
        /// </summary>
        private void loadPcInfo(string xxdm)
        {
            //BLL_zc_xx zk = new BLL_zc_xx();
            //DataTable tab = zk.selectPcdm(xxdm);
            //this.ddlXpcInfo.DataSource = tab;
            //this.ddlXpcInfo.DataTextField = "xpc_mc";
            //this.ddlXpcInfo.DataValueField = "xpcid";
            //this.ddlXpcInfo.DataBind();

            
        }
        /// <summary>
        /// 加载专业信息
        /// </summary>
        private void Loadzy(string xxdm)
        {
            BLL_zc_xx bll = new BLL_zc_xx();
            DataTable tab = bll.Select_zk_zykXX(xxdm);
            this.ddlzy.DataSource = tab;
            this.ddlzy.DataTextField = "zymc";
            this.ddlzy.DataValueField = "zydm";
            this.ddlzy.DataBind();
        }
       
      
        /// <summary>
        /// 加载该学校投档信息。
        /// </summary>
        private void loadToudangInfo()
        {
            BLL_zc_xx zk = new BLL_zc_xx();
            //if (this.ddlXpcInfo.SelectedIndex < 0)
            //{
            //    return;
            //   // this.tr_head.Visible = true;
            //}
            string Department = SincciLogin.Sessionstu().UserName;
            string xxdm = Department;
           
            //string str = this.ddlXpcInfo.SelectedItem.Text;
            //int begin = 0;
            //int end = 0;
            //begin = str.IndexOf('[');
            //end = str.IndexOf(']');
            //string pcdm = str.Substring(begin + 1, end - begin - 1);
            DataTable tab = new DataTable();

            tab = zk.selectksh(xxdm);
            if (ViewState["SortDirection"] == null)
                repDisplay.DataSource = tab;
            else
            {
                tab.DefaultView.Sort = ViewState["SortExpression"].ToString() + " " +
                 ViewState["SortDirection"].ToString() + ",cj desc";
                this.repDisplay.DataSource = tab;
            }
            
            repDisplay.DataBind();
        }
       
        /// <summary>
        /// 预录
        /// </summary>
        protected void btn_beginTd_Click(object sender, EventArgs e)
        {
             BLL_zc_xx zk = new BLL_zc_xx();
             string Department = SincciLogin.Sessionstu().UserName;
             string xxdm = Department;
            // string str = this.ddlXpcInfo.SelectedItem.Text;
            //int begin = 0;
            //int end = 0;
            //begin = str.IndexOf('[');
            //end = str.IndexOf(']');
            //string pcdm = str.Substring(begin + 1, end - begin - 1);
             if (txtksh.Text.Trim()=="")
             {
                   Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请输入需要注册的考生!');</script>");

             }
             else
             {
                 Model_zk_lqk lqk = new BLL_zk_lqk().Select_zk_lqk(txtksh.Text.Trim());
                 if (lqk.Ksh != "")
                 {
                     DataTable zc = zk.Select_zcxx(lqk.Ksh);
                     if (zc == null)
                     {
                         if (zk.insert_zc(lqk.Ksh, xxdm, ddlzy.SelectedValue.ToString()))
                         {
                             Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('添加成功!');</script>");
                         }
                         else
                         {
                             Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('添加失败!');</script>");
                         }
                     }
                     else
                     {
                         if (zc.Rows.Count > 0)
                         {
                             Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('该考生已被注册!');</script>");
                         }
                         else
                         {
                             if (  zk.insert_zc(lqk.Ksh, xxdm, ddlzy.SelectedValue.ToString()))
                             {
                                 Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('添加成功!');</script>");
                             }
                             else
                             {
                                 Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('添加失败!');</script>");
                             }
                         }
                     }
                    
            
                 }
                 else
                 {
                     Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('查询不到该考生信息!');</script>");

                 }
             }
             loadToudangInfo();
        }

        /// <summary>
        /// 点击导出所有考生当前投档状态信息。
        /// </summary>
        protected void btnImport_td_ksxx_Click(object sender, EventArgs e)
        {
            //string str = this.ddlXpcInfo.SelectedItem.Text;
            //int begin = 0;
            //int end = 0;
            //begin = str.IndexOf('[');
            //end = str.IndexOf(']');
            //string pcdm = str.Substring(begin + 1, end - 1);

            //string where = String.Format(" where pcdm='{0}'", pcdm);
            BLL_LQK_Ks_Xx lqk = new BLL_LQK_Ks_Xx();
            if (!lqk.Import_lqk())
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'导出数据失败！' ,title:'警告提示'}); setTimeout(function(){ ymPrompt.close();},3000); </script>");
            }

        }
        /// <summary>
        /// 预退
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Td_Click(object sender, EventArgs e)
        {
            BLL_zc_xx zk = new BLL_zc_xx();
            string strksh = "";
            if (Request.Form["CheckBox1"] != null)
            {
                strksh = Request.Form["CheckBox1"].ToString();

                if (strksh.Length > 0)
                {
                    if (zk.del_zcxx(strksh))
                    {

                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('删除成功!');</script>");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('删除失败!');</script>");
                    }

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择考生!');</script>");
                }
            
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择考生!');</script>");
            }
            loadToudangInfo();
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPl_Fd_Click(object sender, EventArgs e)
        {
            BLL_zc_xx zk = new BLL_zc_xx();
            DataTable tab = new DataTable();
            string Department = SincciLogin.Sessionstu().UserName;
            string xxdm = Department;
          
            //string str = this.ddlXpcInfo.SelectedItem.Text;
            //int begin = 0;
            //int end = 0;
            //begin = str.IndexOf('[');
            //end = str.IndexOf(']');
            //string pcdm = str.Substring(begin + 1, end - begin - 1);
            tab = zk.selectksh(xxdm);
            if (tab != null)
            {
                if (tab.Rows.Count > 0)
                {
                    if (zk.update_zcxx(xxdm, 2))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('上传成功!');</script>");

                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('上传失败!');</script>");
                    }
                }
            }
            loadToudangInfo();
        }
        

        protected void ddlXpcInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadToudangInfo();
        }

        protected void btnExcelFileImport_Click(object sender, EventArgs e)
        {
            BLL_zc_xx zk = new BLL_zc_xx();
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
               
                //string str = this.ddlXpcInfo.SelectedItem.Text;
                //int begin = 0;
                //int end = 0;
                //begin = str.IndexOf('[');
                //end = str.IndexOf(']');
                //string pcdm = str.Substring(begin + 1, end - begin - 1);
                string result = zk.ImportExcelData(path);
                string E_record = "导入：注册考生:" + config.FormatDateToStringId(DateTime.Now) + fileExtension;
                EventMessage.EventWriteDB(1, E_record);

                msgWindow.InnerHtml = result;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                    "<script>$(function () {if(confirm('导入完成是否需要查看导入日志？'))$('#msgWindow').window('open');});</script>");
            }
            loadToudangInfo();
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
            loadToudangInfo();
            
        }

        protected void repDisplay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "if(this!=prev){c=this.style.backgroundColor;this.style.backgroundColor='#D8F3C6'}");//当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseout", "if(this!=prev){this.style.backgroundColor=c}");//当鼠标移开时还原背景色
                e.Row.Attributes["style"] = "Cursor:hand";//设置悬浮鼠标指针形状为"小手"
                e.Row.Attributes.Add("onclick", "RowClick(this); ");
            }
        }
    }
}