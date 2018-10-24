using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using System.Data;
using System.Text;
using System.IO;
namespace SincciKC.websystem.kwgl
{
    public partial class KaoDian_Bmd : BPage
    {

        /// <summary>
        /// BLL信息管理
        /// </summary>
        BLL_xxgl bllxxgl = new BLL_xxgl();

        /// <summary>
        /// 县区代码控制类
        /// </summary>
        BLL_zk_xqdm bllxqdm = new BLL_zk_xqdm();
        /// <summary>
        /// 考点代码控制类
        /// </summary>
        BLL_zk_kd BLL_kd = new BLL_zk_kd();    
        /// <summary>
        /// 学校代码控制类
        /// </summary>
        BLL_zk_xxdm bllxxdm = new BLL_zk_xxdm();
        /// <summary>
        /// 班级代码控制类
        /// </summary>
        BLL_zk_bjdm bllbjdm = new BLL_zk_bjdm();

        public string kddm = config.sink("kddm", config.MethodType.Get, 255, 1, config.DataType.Str).ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Permission();
                //绑定PageSize数据
                this.ddlPageSize.DataSource = new config().PageSizelist();
                this.ddlPageSize.DataBind();

                this.lblkddm.Text = kddm;


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
            DataTable tab = BLL_kd.ExecuteProc_View_kd_bmd(strWhere, pageSize, AspNetPager1.CurrentPageIndex, ref RecordCount);
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

            dlistSq.DataSource = bllxqdm.SelectXqdm(Department, UserType);
            dlistSq.DataTextField = "xqmc";
            dlistSq.DataValueField = "xqdm";
            dlistSq.DataBind();
            this.dlistSq.Items.Insert(0, new ListItem("-请选择-", ""));

        }
        /// <summary>
        /// 选择县区后加载学校数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            //if (this.dlistBj.Items.Count > 0)
            //{
            //    this.dlistBj.Items.Clear();
            //    this.dlistBj.Items.Insert(0, new ListItem("-请选择-", ""));
            //}
        }
        /// <summary>
        /// 选择学校后加载班级数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dlistXx_SelectedIndexChanged(object sender, EventArgs e)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;

            string xxdm = this.dlistXx.SelectedValue;
            DataTable dt = bllbjdm.Select_zk_bjdm(xxdm, Department, UserType);
            //dlistBj.DataSource = dt;
            //dlistBj.DataTextField = "bjmc";
            //dlistBj.DataValueField = "bjdm";
            //dlistBj.DataBind();
            //this.dlistBj.Items.Insert(0, new ListItem("-全部-", ""));

            StringBuilder sb = new StringBuilder();

            sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
          
            if (dt.Rows.Count > 0)
            {
                bmdinfo.Visible = true;
                 sb.Append("<tr>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                   

                    sb.Append("   <td>");
                    sb.Append("<input name='bmddm' type='checkbox' value='" + dt.Rows[i]["bjdm"].ToString() + "'>" + dt.Rows[i]["bjmc"].ToString() + "|");
                    sb.Append("   </td>");
                  
                }
                  sb.Append("</tr>");
            }
            sb.Append("</table>");
            this.bmdinfo.InnerHtml = sb.ToString();
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
            string kddm = this.lblkddm.Text.Trim().ToString();

            string whereStr = ""; 

            whereStr += " kddm='" + kddm + "' And ";
            

            if (!string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                whereStr += " ( ksh='" + config.CheckChar(txtName.Text.Trim().ToString()) + "' Or xm Like '%" + config.CheckChar(txtName.Text.Trim().ToString()) + "%') and ";
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
                //区招生办
                case 3:
                    where = " bmdxqdm = '" + Department + "'  ";
                    break;
                //学校用户 
                case 4:
                    where = " bmddm = '" + Department + "' ";
                    break;
                //班级用户 
                case 5:
                    where = " bjdm = '" + Department.Substring(6) + "' and bmddm='" + Department.Substring(0, 6) + "' ";
                    break;
                default:
                    where = " 1<>1";
                    break;
            }
            if (whereStr.Length > 0)
            {
                whereStr = whereStr + where;
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
        /// 添加毕业中学或考生到考点
        /// </summary> 
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string ksh = this.txtksh.Text.Trim().ToString();
            string kddm = this.lblkddm.Text.Trim().ToString();
            
            string xqdm = this.dlistSq.SelectedValue;
            string xxdm = this.dlistXx.SelectedValue;
            string bjdm = "";
            if (Request.Form["bmddm"] != null)
            {
                bjdm = Request.Form["bmddm"].ToString().Trim(); 
            }
            


            if (ksh.Length > 0) //添加单个报名号
            { 
                //管理部门权限
                string where = quanxian();
                where = " a.ksh ='" + ksh + "' and " + where;
                DataTable dt = BLL_kd.select_ksh(where);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["kddm"].ToString().Length > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('报名号已编排在：" + dt.Rows[0]["kddm"].ToString() + "考点，不可以添加！')</script>");
                    }
                    else
                    {
                        if (BLL_kd.Insert_kd_bmd(kddm, ksh))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('添加成功！')</script>");
                            BindGv();
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('添加失败！" + kddm + " ," + ksh + "')</script>");
                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('查找不到该报名号！')</script>");
                }

            }
            else  //添加班级或者整个毕业中学
            {
                 string where = quanxian();
                 if (xxdm.Length == 0)
                 {
                     Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择毕业中学！')</script>");
                 }
                 else
                 {
                     where = " kddm is null and  bmddm ='" + xxdm + "' and " + where;
                     if (bjdm.Length > 0)
                         where = "  bjdm in (" + bjdm + ") and " + where;
                   
                     DataTable dt = BLL_kd.select_ksh(where);
                     if (dt.Rows.Count > 0)
                     {
                         for (int i = 0; i < dt.Rows.Count; i++)
                         {
                             ksh = dt.Rows[i]["ksh"].ToString();
                             BLL_kd.Insert_kd_bmd(kddm, ksh);
                         }
                         Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('添加成功！')</script>");
                         BindGv();
                     }
                     else
                     {
                         Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('没有考生可以添加！')</script>");
                     }
                 }
            } 

        } 

        /// <summary>
        /// 删除考生
        /// </summary> 
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (Request.Form["CheckBox1"] != null)
            {
                string ids = Request.Form["CheckBox1"].ToString();

                if (ids.Length > 0)
                {
                    string str = "";
                    for (int i = 0; i < ids.Split(',').Length; i++)
                    {
                        str = str + "'" + ids.Split(',')[i] + "',";
                    }
                    str = str.Remove(str.Length - 1);
                    //管理部门权限
                    string where = "  ksh in(" + str + ") ";
                    if (BLL_kd.Deleteks(where))
                    {
                       string E_record = "删除: 考点考生：" + ids + "";
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
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择您所需要删除的用户,\"已确认\"的用户不能删除！')</script>");

            }
        }

        #region "管理部门权限"
        /// <summary>
        /// 管理部门权限
        /// </summary> 
        private string quanxian()
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
                    where = " bmdxqdm = '" + Department + "'  ";
                    break;
                //学校用户 
                case 4:
                    where = " bmddm = '" + Department + "' ";
                    break;
                //班级用户 
                case 5:
                    where = " bjdm = '" + Department.Substring(6) + "' and bmddm='" + Department.Substring(0, 6) + "' ";
                    break;
                default:
                    where = " 1<>1";
                    break;
            }
            return where;
        }
        #endregion

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

                string result = BLL_kd.ImportExcelData(path, lblkddm.Text);
                FileStream fs = new FileStream(Server.MapPath("/tmpUpLoadFile/" + "考点考生导入日志" + config.Get_UserName + ".txt"), FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.Write(result);
                sw.Flush();
                sw.Close();
                string E_record = "导入: 考点考生数据:" + config.FormatDateToStringId(DateTime.Now) + fileExtension;
                EventMessage.EventWriteDB(1, E_record);
                // msgWindow.InnerHtml = result;
                //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                //        "<script>$(function () {if(confirm('导入完成是否需要查看导入日志？'))$('#msgWindow').window('open');});</script>");
                //}
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('上传成功！ ')</script>");

            }
            BindGv();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string txtFileName = "考点考生导入日志" + config.Get_UserName + ".txt";
            string txtFileNameRAR = "考点考生导入日志" + config.Get_UserName + ".zip";
            string destFileName = Server.MapPath(String.Format("/tmpUpLoadFile/" + txtFileName));

            if (File.Exists(destFileName))
            {
                string destFileNameRAR = Server.MapPath(String.Format("/tmpUpLoadFile/" + "考点考生导入日志" + config.Get_UserName + ".zip"));
                ZipClass zip = new ZipClass();
                zip.ZipFile(destFileName, destFileNameRAR);
                Response.Redirect("/tmpUpLoadFile/" + txtFileNameRAR);

                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script> window.parent.addTab2('会考成绩导入日志', '/tmpUpLoadFile/" + txtFileName + "');</script>");

                string E_record = "下载: 考点考生上传日志记录" + config.Get_UserName + "";
                EventMessage.EventWriteDB(1, E_record);
            }
        }

       
    }
}