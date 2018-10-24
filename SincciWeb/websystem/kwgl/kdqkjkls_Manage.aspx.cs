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
namespace SincciKC.websystem.kwgl
{
    public partial class kdqkjkls_Manage : BPage
    {
        /// <summary>
        /// 县区代码控制类
        /// </summary>
        BLL_zk_xqdm BLL_xqdm = new BLL_zk_xqdm();
        /// <summary>
        /// 考点代码控制类
        /// </summary>
        BLL_zk_kdqk BLL_kd = new BLL_zk_kdqk();

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
                btnNew.Visible = false;
            }
            //删除
            if (!new Method().CheckButtonPermission(PopedomType.A8))
            {
                btnDelete.Visible = false;
            }
            //修改
            if (!new Method().CheckButtonPermission(PopedomType.A16))
            {
                btnEdit.Visible = false;
            }
            //导出数据
            if (!new Method().CheckButtonPermission(PopedomType.A32))
            {
                this.btndaoAll.Visible = false;
            }
            //导入数据
            if (!new Method().CheckButtonPermission(PopedomType.A64))
            {
                this.divdaor.Visible = false;
            }
            ////新增
            //if (!new Method().CheckButtonPermission(PopedomType.A32))
            //{
            //    this.btnAdd.Visible = false;
            //}

        }
        #endregion
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGv()
        {
            BLL_zk_zdxx zdxx = new BLL_zk_zdxx();
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            if (UserType == 7)
            {
                Department = SincciLogin.Sessionstu().UserName.Substring(1, 4);
                UserType = 3;
            }
            this.listkemu.DataSource = BLL_xqdm.SelectXqdm(Department, UserType);
            this.listkemu.DataTextField = "xqmc";
            this.listkemu.DataValueField = "xqdm";
            this.listkemu.DataBind();
            this.listkemu.Items.Insert(0, new ListItem("-请选择-", ""));
            int RecordCount = 0;
            int pageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            string strWhere = createWhere();
            DataTable tab = BLL_kd.ExecuteProc_kdjkls(strWhere, pageSize, AspNetPager1.CurrentPageIndex, ref RecordCount);
            this.repDisplay.DataSource = tab;
            this.repDisplay.DataBind();

            //分页
            config.AspNetPagerCustomInfoHTML2(AspNetPager1, RecordCount, pageSize);
        }

 
        /// <summary>
        /// 创建查询条件。
        /// </summary>
        /// <returns></returns>
        private string createWhere()
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
          
            //管理部门权限
            string where = "";
            switch (UserType)
            {
                //系统管理员
                case 1:
                    where = " 1=1 and";
                    break;
                //市招生办
                case 2:
                    where = " 1=1  and";
                    break;
                //区招生办
                case 3:
                    where = " '06'+ left(kddm,2)='" + Department + "' and ";
                    break;
                //学校用户 
                case 4:
                    where = " 1<>1 and";
                    break;
                //班级用户 
                case 5:
                    where = " 1<>1 and";
                    break;
                //班级用户 
                case 7:
                    where = " kddm='" + SincciLogin.Sessionstu().UserName.Substring(3)+"' and ";
                    break;
                default:
                    where = " 1<>1 and";
                    break;  
            }
            if (listkemu.SelectedValue.Length > 0)
            {
                where = where + " xqdm='" + listkemu.SelectedValue + "' and ";
            }
            if (listleib.SelectedValue.Length > 0)
            {
                where = where + " kddm='" + listleib.SelectedValue + "' and ";
            }
            if (txtksh.Text.Trim().Length > 0)
            {
                where = where + " xm like ('%" + txtksh.Text.Trim() + "%') and ";
            }
            where = where + " 1=1 ";

            return where;
        }
       

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

     


        /// <summary>
        /// 上下一页
        /// </summary> 
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            BindGv();
        }
       
   
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (Request.Form["CheckBox1"] != null)
            {
                string ids = Request.Form["CheckBox1"].ToString();

                if (ids.Length > 0)
                {
                  
                    string where =  " ID in(" + ids + ")";
                    if (BLL_kd.Delete_zk_kdjkls(where))
                    {
                        string E_record = "删除: 考点监考老师数据：" + ids + "";
                        EventMessage.EventWriteDB(1, E_record);
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('操作成功！')</script>");
                        BindGv();
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('操作失败！')</script>");
                    }

                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择您所需要删除的数据！')</script>");

            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (Request.Form["CheckBox1"] != null)
            {
                string[] ids = Request.Form["CheckBox1"].ToString().Split(',');

                if (ids.Length == 1)
                {

                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>opUp('" + ids[0] + "', '信息修改') ;</script>");
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGv();
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btndaoAll_Click(object sender, EventArgs e)
        {

            string strDate1 = DateTime.Now.ToString("yyyyMMddHHmmss");
            GridView gvOrders = new GridView();
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "GB2312";
            string name = "考点监考老师" + strDate1;
            name = System.Web.HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8);
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + name + ".xls");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");  //设置输出流为简体中文 
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            gvOrders.AllowPaging = false;
            gvOrders.AllowSorting = false;
            gvOrders.DataSource = BLL_kd.daochu_zk_kdjkls(quanxian());
            gvOrders.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            gvOrders.DataBind();
            gvOrders.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }
        private string quanxian()
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
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
                    where = " 1=1  ";
                    break;
                //区招生办
                case 3:
                    where = " '06'+ left(kddm,2)='" + Department + "'  ";
                    break;
                //学校用户 
                case 4:
                    where = " 1<>1 ";
                    break;
                //班级用户 
                case 5:
                    where = " 1<>1 ";
                    break;
                //考点
                case 7:
                    where = " kddm='" + SincciLogin.Sessionstu().UserName.Substring(3) + "'  ";
                    break;
                default:
                    where = " 1<>1 ";
                    break;
            }
            return where;
        }

       

       
        

        protected void btnExcelFileImport_Click(object sender, EventArgs e)
        {
            if (SincciLogin.Sessionstu().UserType == 7)
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
                        BindGv();
                        return;
                    }

                    string path = Server.MapPath("~/tmpUpLoadFile/") + config.FormatDateToStringId(DateTime.Now) + fileExtension;
                    fuExcelFileImport.PostedFile.SaveAs(path);
                    string result = BLL_kd.ImportExcelData(path, SincciLogin.Sessionstu().UserName);
                    string E_record = "导入：考点监考老师:" + config.FormatDateToStringId(DateTime.Now) + fileExtension;
                    EventMessage.EventWriteDB(1, E_record);

                    msgWindow.InnerHtml = result;
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                        "<script>$(function () {if(confirm('导入完成是否需要查看导入日志？'))$('#msgWindow').window('open');});</script>");
                }

                BindGv();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('您没权限导入数据!');</script>");
            }
        }

        protected void listkemu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listkemu.SelectedValue.Length > 0)
            {
                int UserType = SincciLogin.Sessionstu().UserType;
                string Department = SincciLogin.Sessionstu().U_department;

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
                        where = " 1=1  ";
                        break;
                    //区招生办
                    case 3:
                        where = " '06'+ left(kddm,2)='" + Department + "' ";
                        break;
                    //学校用户 
                    case 4:
                        where = " 1<>1 ";
                        break;
                    //班级用户 
                    case 5:
                        where = " 1<>1 ";
                        break;
                    //班级用户 
                    case 7:
                        where = " kddm='" + SincciLogin.Sessionstu().UserName.Substring(3) + "'  ";
                        break;
                    default:
                        where = " 1<>1 ";
                        break;
                }
                this.listleib.DataSource = BLL_kd.Select_kd(" xqdm='" + listkemu.SelectedValue + "' and " + where);
                this.listleib.DataTextField = "kdmc";
                this.listleib.DataValueField = "kddm";
                this.listleib.DataBind();
                this.listleib.Items.Insert(0, new ListItem("-请选择-", ""));
            }
        }



    }
}