using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

using System.Data;
using BLL;

using System.IO;
using System.Text;
namespace SincciKC.websystem.hkbm
{
    public partial class Hkbm_Manage : BPage
    {
        public int page = Convert.ToInt32(config.sink("page", config.MethodType.Get, 255, 1, config.DataType.Int));
        public int pagesize = Convert.ToInt32(config.sink("pagesize", config.MethodType.Get, 255, 1, config.DataType.Int));
        /// <summary>
        /// BLL会考信息管理
        /// </summary>
        BLL_Hkbm bLL_Hkbm = new BLL_Hkbm();
        /// <summary>
        /// 学校代码控制类
        /// </summary>
        BLL_zk_xxdm bllxxdm = new BLL_zk_xxdm();
        /// <summary>
        /// 班级代码控制类
        /// </summary>
        BLL_zk_bjdm bllbjdm = new BLL_zk_bjdm();
        /// <summary>
        /// 县区代码控制类
        /// </summary>
        BLL_zk_xqdm bllxqdm = new BLL_zk_xqdm();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Permission();
                //绑定PageSize数据
                this.ddlPageSize.DataSource = new config().PageSizelist();
                this.ddlPageSize.DataBind();

                if (pagesize > 0)
                {
                    this.ddlPageSize.SelectedValue = pagesize.ToString();
                }
                else
                {
                    pagesize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
                }
                int UserType = SincciLogin.Sessionstu().UserType;
                string Department = SincciLogin.Sessionstu().U_department;



                BindGv();
                Loadsq();
            }
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
        /// 绑定数据
        /// </summary>
        private void BindGv()
        {
            if (page == 0)
                page = 1;
            int RecordCount = 0;
            string where = strwhere();
            pagesize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            this.Repeater1.DataSource = bLL_Hkbm.ExecuteProcList(where, pagesize, AspNetPager1.CurrentPageIndex, ref RecordCount);
            this.Repeater1.DataBind();
            //分页

            config.AspNetPagerCustomInfoHTML2(AspNetPager1, RecordCount, pagesize);
        }

        #region "查询"
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            BindGv();
        }
        /// <summary>
        /// 条件
        /// </summary>
        private string strwhere()
        {
            string str = "";
            string shiqu = dlistSq.SelectedValue;//县区
            string xuexiao = dlistXx.SelectedValue;//学校
            string banji = dlistBj.SelectedValue;//班次
            string KeyWord = config.CheckChar(this.txtName.Text.Trim().ToString());//关键字
            string tag_state = this.dlistZt.SelectedValue;//状态
            if (shiqu.Length > 0)
            {
                str = str + " bmdxqdm='" + shiqu + "' and ";
            }
            if (xuexiao.Length > 0)
            {
                str = str + " bmddm='" + xuexiao + "' and ";
            }
            if (banji.Length > 0)
            {
                str = str + " bjdm='" + banji + "' and ";
            }

            if (KeyWord.Length > 0)
            {
                str = str + " (ksh='" + KeyWord + "' or xm like '%" + KeyWord + "%' or sfzh='" + KeyWord + "') and ";
            }
            if (tag_state.Length > 0)
            {
                if (tag_state == "1")
                {
                    str = str + "  isnull(xxqr,0)=1  and ";
                }
                else if (tag_state == "2")
                {
                    str = str + "  isnull(xxqr,0)=0  and ";
                }
                else if (tag_state == "3")
                {
                    str = str + "  isnull(xqqr,0)=1  and ";
                }
                else
                {
                    str = str + "  isnull(xqqr,0)=2  and ";
                }
            }
            string where = quanxian();

            if (str.Length > 0)
            {
                str = str + where;
            }
            else
            {
                str = where;
            }

            //Response.Write(str);
            return str;
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
            pagesize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            BindGv();
            // Response.Redirect(config.GetScriptName + "?page=1&pagesize=" + this.ddlPageSize.SelectedValue.ToString() + "");
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

            //删除
            if (!new Method().CheckButtonPermission(PopedomType.A8))
            {
                this.btnDelete.Visible = false;
            }


            //导入数据
            if (!new Method().CheckButtonPermission(PopedomType.A16))
            {
                divdaor.Visible = false;

            }
            //导出数据
            if (!new Method().CheckButtonPermission(PopedomType.A32))
            {
                btndaoAll.Visible = false;

            }
            //确认
            if (!new Method().CheckButtonPermission(PopedomType.A64))
            {
                btnQr.Visible = false;

            }
            //全部确认
            if (!new Method().CheckButtonPermission(PopedomType.A128))
            {
                btnAllQr.Visible = false;

            }
            //重置状态
            if (!new Method().CheckButtonPermission(PopedomType.A256))
            {
                btnResetTag.Visible = false;

            }
             //导出EXCEL
            if (!new Method().CheckButtonPermission(PopedomType.A512))
            {
                btnexcel.Visible = false;

            }
            
            ////取消确认
            //if (!new Method().CheckButtonPermission(PopedomType.A2048))
            //{
            //    this.btnQxqr.Visible = false;
            //}
            ////导出报名号
            //if (!new Method().CheckButtonPermission(PopedomType.A4096))
            //{
            //    this.butDaochu.Visible = false;
            //}

        }
        #endregion
        //操作记录
        public string E_record = "";
        /// <summary>
        /// 删除考生数据
        /// </summary> 
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (Request.Form["CheckBox1"] != null)
            {
                string ids = Request.Form["CheckBox1"].ToString();

                if (ids.Length > 0)
                {
                    //管理部门权限
                    string where = quanxian();

                    where = where + " and ksh in(" + ids + ")";
                    if (bLL_Hkbm.Deleteks(where))
                    {
                        E_record = "删除: 考生数据：" + ids + "";
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
            dlistBj.DataSource = bllbjdm.Select_zk_bjdm(xxdm, Department, UserType);
            dlistBj.DataTextField = "bjmc";
            dlistBj.DataValueField = "bjdm";
            dlistBj.DataBind();
            this.dlistBj.Items.Insert(0, new ListItem("-请选择-", ""));
        }
        /// <summary>
        /// 上下一页
        /// </summary> 
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            BindGv();
        }



        /// <summary>
        /// 导入考生基本数据
        /// </summary> 
        protected void btnImport_Click(object sender, EventArgs e)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;

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

                string result = bLL_Hkbm.ImportExcelData(path, Department);


                FileStream fs = new FileStream(Server.MapPath("/tmpUpLoadFile/" + "考生信息导入日志" + config.Get_UserName + ".txt"), FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.Write(result);
                sw.Flush();
                sw.Close();
                //  msgWindow.InnerHtml = result;
                E_record = "导入: 考生数据:" + config.FormatDateToStringId(DateTime.Now) + fileExtension;
                EventMessage.EventWriteDB(1, E_record);

                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('上传成功！ ')</script>");
                BindGv();

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
                    Model_zk_ksxxgl model = new BLL_zk_ksxxgl().zk_ksxxglDisp(ids[0]);


                    if (model.Ksqr == 2)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('该考生已确认信息,不能修改!');</script>");
                    }
                    else
                    {
                        if (model.Xxdy == 1)
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('学校已打印信息,不能修改!');</script>");
                        }
                        else
                        {
                            if (model.Xxqr == 1)
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('学校已确认信息,不能修改!');</script>");
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>opUp('" + ids[0] + "', '考生个人信息修改') ;</script>");
                            }
                        }
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
        /// <summary>
        /// 全部确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAllQr_Click(object sender, EventArgs e)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;

            string str = "";
            string shiqu = dlistSq.SelectedValue;
            string xuexiao = dlistXx.SelectedValue;
            string banji = dlistBj.SelectedValue;
            string strksh = "";
            if (Request.Form["CheckBox1"] != null)
            {
                strksh = Request.Form["CheckBox1"].ToString();
            }
            //if (strksh.Length > 0)
            //{
            //    str = str + "  ksh in(" + strksh + ") and ";
            //}
            if (shiqu.Length > 0)
            {
                str = str + " bmdxqdm='" + shiqu + "' and ";
            }
            if (xuexiao.Length > 0)
            {
                str = str + " bmddm='" + xuexiao + "' and ";
            }
            if (banji.Length > 0)
            {
                str = str + " bjdm='" + banji + "' and ";
            }


            //管理部门权限
            string where = quanxian();

            if (str.Length > 0)
            {
                str = str + where;
            }
            else
            {
                str = where;
            }


            #region "管理部门权限"
            string strset = "";
            switch (UserType)
            {
                //系统管理员
                case 1:
                    strset = " 1<>1";
                    break;
                //市招生办
                case 2:
                    strset = " 1<>1";
                    break;
                //区招生办
                case 3:
                    strset = " xqqr=1 ";
                    str = str + "  and isnull(xxqr,0)=1";
                    break;
                //学校用户 
                case 4:
                    strset = " xxqr=1 ";
                    str = str + " ";
                    break;
                //班级用户 
                case 5:
                    strset = " 1<>1";
                    break;
                default:
                    strset = " 1<>1";
                    break;
            }

            #endregion

            if (bLL_Hkbm.ResetTag(strset, str))
            {
                if (strksh == "")
                {
                    strksh = "[" + shiqu + "]" + "[" + xuexiao + "]" + "[" + banji + "]";
                }
                E_record = "确认:[" + strksh + "]状态确认";
                EventMessage.EventWriteDB(1, E_record);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('确认成功！ ')</script>");
                BindGv();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('确认失败！ ')</script>");
            }


        }
        /// <summary>
        /// 选择确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQr_Click(object sender, EventArgs e)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;

            string str = "";
            string shiqu = dlistSq.SelectedValue;
            string xuexiao = dlistXx.SelectedValue;
            string banji = dlistBj.SelectedValue;
            string strksh = "";
            if (Request.Form["CheckBox1"] != null)
            {
                strksh = Request.Form["CheckBox1"].ToString();

                if (strksh.Length > 0)
                {
                    str = str + "  ksh in(" + strksh + ") and ";
                }
                if (shiqu.Length > 0)
                {
                    str = str + " bmdxqdm='" + shiqu + "' and ";
                }
                if (xuexiao.Length > 0)
                {
                    str = str + " bmddm='" + xuexiao + "' and ";
                }
                if (banji.Length > 0)
                {
                    str = str + " bjdm='" + banji + "' and ";
                }


                //管理部门权限
                string where = quanxian();

                if (str.Length > 0)
                {
                    str = str + where;
                }
                else
                {
                    str = where;
                }

                #region "管理部门权限"
                string strset = "";
                switch (UserType)
                {
                    //系统管理员
                    case 1:
                        strset = " 1<>1";
                        break;
                    //市招生办
                    case 2:
                        strset = " 1<>1";
                        break;
                    //区招生办
                    case 3:
                        strset = " xqqr=1 ";
                        str = str + "  and isnull(xxqr,0)=1";
                        break;
                    //学校用户 
                    case 4:
                        strset = " xxqr=1 ";
                        str = str + " ";
                        break;
                    //班级用户 
                    case 5:
                        strset = " 1<>1";
                        break;
                    default:
                        strset = " 1<>1";
                        break;
                }

                #endregion
                if (bLL_Hkbm.ResetTag(strset, str))
                {
                    if (strksh == "")
                    {
                        strksh = "[" + shiqu + "]" + "[" + xuexiao + "]" + "[" + banji + "]";
                    }
                    E_record = "确认:[" + strksh + "]状态确认";
                    EventMessage.EventWriteDB(1, E_record);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('确认成功！ ')</script>");
                    BindGv();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('确认失败！ ')</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择需要确认的考生!');</script>");
            }
        }
        /// <summary>
        /// 下载日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnrizhi_Click(object sender, EventArgs e)
        {
            //string txtFileName = "考生信息导入日志" + config.Get_UserName + ".txt";
            //string destFileName = Server.MapPath(String.Format("/tmpUpLoadFile/" + txtFileName));
            //  new config().DownloadTxt(destFileName);
            string txtFileName = "考生信息导入日志" + config.Get_UserName + ".txt";
            string txtFileNameRAR = "考生信息导入日志" + config.Get_UserName + ".zip";
            string destFileName = Server.MapPath(String.Format("/tmpUpLoadFile/" + txtFileName));
            if (File.Exists(destFileName))
            {
                string destFileNameRAR = Server.MapPath(String.Format("/tmpUpLoadFile/" + "考生信息导入日志" + config.Get_UserName + ".zip"));
                ZipClass zip = new ZipClass();
                zip.ZipFile(destFileName, destFileNameRAR);
                Response.Redirect("/tmpUpLoadFile/" + txtFileNameRAR);

                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>window.parent.addTab2('考生信息导入日志', '/tmpUpLoadFile/" + txtFileName + "');</script>");

                string E_record = "下载: 考生信息上传日志记录" + config.Get_UserName + "";
                EventMessage.EventWriteDB(1, E_record);
            }
        }
        /// <summary>
        /// 取消确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQxqr_Click(object sender, EventArgs e)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;

            string str = "";
            string shiqu = dlistSq.SelectedValue;
            string xuexiao = dlistXx.SelectedValue;
            string banji = dlistBj.SelectedValue;
            string strksh = "";
            if (Request.Form["CheckBox1"] != null)
            {
                strksh = Request.Form["CheckBox1"].ToString();

                if (strksh.Length > 0)
                {
                    str = str + "  ksh in(" + strksh + ") and ";
                }
                if (shiqu.Length > 0)
                {
                    str = str + " bmdxqdm='" + shiqu + "' and ";
                }
                if (xuexiao.Length > 0)
                {
                    str = str + " bmddm='" + xuexiao + "' and ";
                }
                if (banji.Length > 0)
                {
                    str = str + " bjdm='" + banji + "' and ";
                }


                //管理部门权限
                string where = quanxian();

                if (str.Length > 0)
                {
                    str = str + where;
                }
                else
                {
                    str = where;
                }
                #region "管理部门权限"
                string strset = "";
                switch (UserType)
                {
                    //系统管理员
                    case 1:
                        strset = " xqqr=0 ";
                        str = str + " and  isnull(ksqr,0)=2 and isnull(xxdy,0)=1 and isnull(xxqr,0)=1";
                        break;
                    //市招生办
                    case 2:
                        strset = " xqqr=0 ";
                        str = str + " and  isnull(xqqr,0)=1 and isnull(xxdy,0)=1 and isnull(xxqr,0)=1";
                        break;
                    //区招生办
                    case 3:
                        strset = " xxqr=0,xxdy=0  ";
                        str = str + " and  isnull(xxqr,0)=1 ";
                        break;
                    //学校用户 
                    case 4:
                        strset = " ksqr=1 ";
                        str = str + " and  isnull(ksqr,0)=2 ";
                        break;
                    //班级用户 
                    case 5:
                        strset = " 1<>1";
                        break;
                    default:
                        strset = " 1<>1";
                        break;
                }

                #endregion


            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择需要取消确认的考生！ ')</script>");

            }
        }

        /// <summary>
        /// 状态重置
        /// </summary> 
        protected void btnResetTag_Click(object sender, EventArgs e)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;

            string str = "";
            string strksh = "";
            if (Request.Form["CheckBox1"] != null)
            {
                strksh = Request.Form["CheckBox1"].ToString();

                if (strksh.Length > 0)
                {
                    str = str + "  ksh in(" + strksh + ") and ";
                    //管理部门权限
                    string where = quanxian();

                    if (str.Length > 0)
                    {
                        str = str + where;
                    }
                    else
                    {
                        str = where;
                    }


                    #region "管理部门权限"
                    string strset = "";
                    switch (UserType)
                    {
                        //系统管理员
                        case 1:
                            strset = " xqqr=0 ";
                            break;
                        //市招生办
                        case 2:
                            strset = " xqqr=0 ";
                            break;
                        //区招生办
                        case 3:
                            strset = " xxqr=0 ";
                            str = str + " and  isnull(xqqr,0)=0 ";
                            break;
                        //学校用户 
                        case 4:
                            strset = " 1<>1 ";
                            break;
                        //班级用户 
                        case 5:
                            strset = " 1<>1 ";
                            break;
                        default:
                            strset = " 1<>1";
                            break;
                    }
                    #endregion

                    if (bLL_Hkbm.ResetTag(strset, str))
                    {

                        E_record = "状态重置: 状态重置数据：" + strksh + "";
                        EventMessage.EventWriteDB(1, E_record);
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('重置成功！ ')</script>");
                        BindGv();
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('重置失败！ ')</script>");
                    }
                }

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择需要重置的考生!');</script>");

            }
        }

    }
}