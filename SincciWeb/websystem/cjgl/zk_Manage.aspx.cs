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
using Model;
using System.Text;

namespace SincciKC.websystem.cjgl
{
    public partial class zk_Manage : BPage
    {
        /// <summary>
        /// 班级代码控制类
        /// </summary>
        BLL_zk_bjdm bllbjdm = new BLL_zk_bjdm();
        /// <summary>
        /// 县区代码控制类
        /// </summary>
        BLL_zk_xqdm BLL_xqdm = new BLL_zk_xqdm();
        /// <summary>
        /// 学校代码控制类
        /// </summary>
        BLL_zk_xxdm bll = new BLL_zk_xxdm();
        /// <summary>
        /// 会考成绩控制类
        /// </summary>
        BLL_zk_kshkcj bllhkcj = new BLL_zk_kshkcj();

        BLL_zk_kszkcj bllxxgl = new BLL_zk_kszkcj();

     
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
            DataTable tab = bllxxgl.ExecuteProcHKCJ(strWhere, pageSize, AspNetPager1.CurrentPageIndex, ref RecordCount);
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
            dlistSq.DataSource = BLL_xqdm.SelectXqdm(Department, UserType);
            dlistSq.DataTextField = "xqmc";
            dlistSq.DataValueField = "xqdm";
            dlistSq.DataBind();
            this.dlistSq.Items.Insert(0, new ListItem("-请选择-", ""));
        }

        /// <summary>
        /// 创建查询条件。
        /// </summary>
        /// <returns></returns>
        private string createWhere()
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string str = "";
            string shiqu = dlistSq.SelectedValue;
            string xuexiao = dlistXx.SelectedValue;
         //   string banji = dlistBj.SelectedValue;
            string KeyWord = config.CheckChar(this.txtName.Text.Trim().ToString());

            if (shiqu.Length > 0)
            {
                str = str + " bmdxqdm='" + shiqu + "' and ";
            }
            if (xuexiao.Length > 0)
            {
                str = str + " bmddm='" + xuexiao + "' and ";
            }
            //if (banji.Length > 0)
            //{
            //    str = str + " bjdm='" + banji + "' and ";
            //}
            if (KeyWord.Length > 0)
            {
                str = str + " (a.ksh='" + KeyWord + "' or b.xm like '%" + KeyWord + "%' or b.sfzh='" + KeyWord + "') and ";
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
            if (str.Length > 0)
            {
                str = str + where;
            }
            else
            {
                str = where;
            }

            
            return str;
        }

       
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            BindGv();
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
          
            //修改
            if (!new Method().CheckButtonPermission(PopedomType.A4))
            {
                this.btnEdit.Visible = false;
            }
            //删除
            if (!new Method().CheckButtonPermission(PopedomType.A8))
            {
                btnDelete.Visible = false;
            }
            //导入数据
            if (!new Method().CheckButtonPermission(PopedomType.A16))
            {divdaor.Visible = false;
            }

        }
        #endregion

        /// <summary>
        /// 点击删除。
        /// </summary>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            BLL_zk_xxdm bll = new BLL_zk_xxdm();

            bool result = bllxxgl.DeleteDatakshkcj(hfDelIDS.Value.Split(',').ToList());

            if (result)
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                    "<script>ymPrompt.succeedInfo({message:'操作成功！' ,title:'操作提示'});setTimeout(function(){window.location.href=window.location.href;},1000);</script>");
            else
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                    "<script>ymPrompt.alert({message:'操作失败！' ,title:'提示'});</script>");
        }

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
                    
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>opdg('" + ids[0] + "', '修改数据') ;</script>");
              
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

        /// <summary>
        /// 上下一页
        /// </summary> 
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
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

                string result = new BLL_zk_kszkcj().ImportExcelData(path);
                FileStream fs = new FileStream(Server.MapPath("/tmpUpLoadFile/" + "考生成绩导入日志" + config.Get_UserName + ".txt"), FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.Write(result);
                sw.Flush();
                sw.Close();
                string E_record = "导入: 考生中考成绩数据:" + config.FormatDateToStringId(DateTime.Now) + fileExtension;
                EventMessage.EventWriteDB(1, E_record);
                // msgWindow.InnerHtml = result;
                //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                //        "<script>$(function () {if(confirm('导入完成是否需要查看导入日志？'))$('#msgWindow').window('open');});</script>");
                //}
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('上传成功！ ')</script>");
          
            }
            BindGv();
        }
        /// <summary>
        /// 县区下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dlistSq_SelectedIndexChanged(object sender, EventArgs e)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string xqdm = this.dlistSq.SelectedValue;
            dlistXx.DataSource = bll.Select_zk_xxdmXQ(xqdm, Department, UserType);
            dlistXx.DataTextField = "xxmc";
            dlistXx.DataValueField = "xxdm";
            dlistXx.DataBind();
            this.dlistXx.Items.Insert(0, new ListItem("-请选择-", ""));
        }
      

        protected void Button1_Click(object sender, EventArgs e)
        {
            string txtFileName = "考生成绩导入日志" + config.Get_UserName + ".txt";
            string txtFileNameRAR = "考生成绩导入日志" + config.Get_UserName + ".zip";
            string destFileName = Server.MapPath(String.Format("/tmpUpLoadFile/" + txtFileName));

            if (File.Exists(destFileName))
            {
                string destFileNameRAR = Server.MapPath(String.Format("/tmpUpLoadFile/" + "考生成绩导入日志" + config.Get_UserName + ".zip"));
                ZipClass zip = new ZipClass();
                zip.ZipFile(destFileName, destFileNameRAR);
                Response.Redirect("/tmpUpLoadFile/" + txtFileNameRAR);

                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script> window.parent.addTab2('会考成绩导入日志', '/tmpUpLoadFile/" + txtFileName + "');</script>");

                string E_record = "下载: 考生成绩上传日志记录" + config.Get_UserName + "";
                EventMessage.EventWriteDB(1, E_record);
            }
        }
    }
}