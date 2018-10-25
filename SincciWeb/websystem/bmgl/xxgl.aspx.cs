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
namespace SincciKC.websystem.bmgl
{
    public partial class xxgl : BPage
    {
        public int page = Convert.ToInt32(config.sink("page", config.MethodType.Get, 255, 1, config.DataType.Int));
        public int pagesize = Convert.ToInt32(config.sink("pagesize", config.MethodType.Get, 255, 1, config.DataType.Int));
        /// <summary>
        /// BLL信息管理
        /// </summary>
        BLL_xxgl bllxxgl = new BLL_xxgl();
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

            
              
               // BindGv();
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
            this.Repeater1.DataSource = bllxxgl.ExecuteProcList(where, pagesize, AspNetPager1.CurrentPageIndex, ref RecordCount);
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
            string shiqu = dlistSq.SelectedValue;
            string xuexiao = dlistXx.SelectedValue;
            string banji = dlistBj.SelectedValue;
            string KeyWord = config.CheckChar(this.txtName.Text.Trim().ToString());
            string tag = this.dlistZt.SelectedValue;
            string kslx = dlistkslx.SelectedValue;
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
            if (tag.Length > 0)
            {
                if (tag == "3")
                {
                    str = str + "  isnull(xxdy,0)=1  and ";
                }
                else if (tag == "4")
                {
                    str = str + "  isnull(xxdy,0)=0  and ";
                }
                else
                {
                    str = str + "  isnull(ksqr,0)='" + tag + "'  and ";
                }
            }
            if (kslx.Length > 0)
            {
                str = str + "  isnull(kslbdm,0)='" + kslx + "'  and ";
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
            //新增
            if (!new Method().CheckButtonPermission(PopedomType.A4))
            {
                this.btnNew.Visible = false;
            }
            //修改
            if (!new Method().CheckButtonPermission(PopedomType.A8))
            {
                this.btnEdit.Visible = false;
            }
            //删除
            if (!new Method().CheckButtonPermission(PopedomType.A16))
            {
                this.btnDelete.Visible = false;
            }
            //打印确认
            if (!new Method().CheckButtonPermission(PopedomType.A32))
            {
                this.btndayin.Visible = false;
            }
            //密码重置
            if (!new Method().CheckButtonPermission(PopedomType.A64))
            {
                this.btnReset.Visible = false;
            }
            //状态重置
            if (!new Method().CheckButtonPermission(PopedomType.A128))
            {
                this.btnResetTag.Visible = false;
            }
            //导入数据
            if (!new Method().CheckButtonPermission(PopedomType.A256))
            {
                divdaor.Visible = false;
               
            }
            //导出数据
            if (!new Method().CheckButtonPermission(PopedomType.A512))
            {
                btndaoAll.Visible = false;
                
            }
            //确认
            if (!new Method().CheckButtonPermission(PopedomType.A1024))
            {
                btnQr.Visible = false;
               
            }
            ////取消确认
            //if (!new Method().CheckButtonPermission(PopedomType.A2048))
            //{
            //    this.btnQxqr.Visible = false;
            //}
            //导出报名号
            if (!new Method().CheckButtonPermission(PopedomType.A4096))
            {
                this.butDaochu.Visible = false;
            }
            //初始化
            if (!new Method().CheckButtonPermission(PopedomType.A8192))
            {

                this.btnchuS.Visible = false;
            }
            //强制修改
            if (!new Method().CheckButtonPermission(PopedomType.A16384))
            {
                this.btnqiangUp.Visible = false;
            }
            //检查相片
            if (!new Method().CheckButtonPermission(PopedomType.A32768))
            {
                this.btnCheckPic.Visible = false;
            }  
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
                    if (bllxxgl.Deleteks(where))
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

        #region "打印事件"
        /// <summary>
        /// 打印事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btndayin_Click(object sender, EventArgs e)
        {
            string str = "";
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string strksh = "";
            if (Request.Form["CheckBox1"] != null)
            {
                strksh = Request.Form["CheckBox1"].ToString();
            }
            if (strksh.Length > 0)
            {
                str = " ksh in (" + strksh + ") and ";  //只打印已经确认的               
            }
            else
            { 
                string shiqu = dlistSq.SelectedValue;
                string xuexiao = dlistXx.SelectedValue;
                string banji = dlistBj.SelectedValue;
                string name = config.CheckChar(txtName.Text.Trim());
                if (shiqu.Length > 0)
                {
                    str += " bmdxqdm='" + shiqu + "' and ";
                }
                if (xuexiao.Length > 0)
                {
                    str += " bmddm='" + xuexiao + "' and ";
                }
                if (banji.Length > 0)
                {
                    str += " bjdm='" + dlistBj.SelectedValue + "' and ";
                }

                if (name.Length > 0)
                {
                    str += "  (ksh='" + txtName.Text.Trim() + "' or xm='" + txtName.Text.Trim() + "' or sfzh='" + txtName.Text.Trim() + "') and ";
                }
            }
            if (str.Length > 0)
            {
                str = str + " pic=1 and isnull(ksqr,0)=2 and "; //只打已确认和已照相的考生。
            }
            if (str == "")
            { 
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >alert('请选择打印条件！');</script>"); 
            }
            else
            {
                string where = quanxian();
                str = str + where;

               bool ispass= bllxxgl.Insertzk_ksxxdy(config.Get_UserName, str );
               if (ispass)
               {
                   DataTable tabwhere = bllxxgl.seleczk_ksxxdy(" username='" + config.Get_UserName + "'");
                   if (tabwhere.Rows.Count > 0)
                   {
                       if (UserType == 4)
                       {
                           bllxxgl.deletedyfwh(str, 1);
                       }
                       bllxxgl.Insertdyfwh(UserType, tabwhere.Rows[0]["SelWhere"].ToString() + "   ");
                       if (bllxxgl.updatezk_ksxxgl(tabwhere.Rows[0]["SelWhere"].ToString(), 1, UserType))
                       {
                           E_record = "打印: 考生数据：" + Request.Form["CheckBox1"].ToString() + "";
                           EventMessage.EventWriteDB(1, E_record);
                           Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >window.parent.addTab2('考生信息打印', '/websystem/bmgl/Printxx.aspx');</script>");
                       }
                   }
               }
               else
               {
                   Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >alert('打印失败');</script>");
               }     
            }
        }
        #endregion

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
        /// 重置考生密码
        /// </summary>  
        protected void btnReset_Click(object sender, EventArgs e)
        {
            if (Request.Form["CheckBox1"] != null)
            {
                string  ids = Request.Form["CheckBox1"].ToString();

                if (ids.Length > 0)
                {
                    //管理部门权限
                    string where = quanxian();

                    where = where + " and ksh in(" + ids + ")";
                    if (bllxxgl.ResetPwd(where))
                    {

                        E_record = "密码重置: 密码重置数据：" + ids + "";
                        EventMessage.EventWriteDB(1, E_record);
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('重置成功！')</script>");

                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('重置失败！')</script>");
                    }
                }

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择需要重置的考生!');</script>");

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
            
             string strksh="";
             if (Request.Form["CheckBox1"] != null)
             {
                 strksh = Request.Form["CheckBox1"].ToString();

                 if (strksh.Length > 0)
                 {
                     str = str + "  ksh in(" + strksh + ") and ";

                     //if (shiqu.Length > 0)
                     //{
                     //    str = str + " bmdxqdm='" + shiqu + "' and ";
                     //}
                     //if (xuexiao.Length > 0)
                     //{
                     //    str = str + " bmddm='" + xuexiao + "' and ";
                     //}
                     //if (banji.Length > 0)
                     //{
                     //    str = str + " bjdm='" + banji + "' and ";
                     //}


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
                             str = str + " and  isnull(ksqr,0)=2 ";

                             break;
                         //市招生办
                         case 2:
                             strset = " xqqr=0 ";
                             str = str + " and  isnull(ksqr,0)=2 ";
                             break;
                         //区招生办
                         case 3:
                             strset = " xxqr=0 ";
                             str = str + " and  isnull(xqqr,0)=0 ";
                             break;
                         //学校用户 
                         case 4:
                             strset = " ksqr=1,xxdy=0 ";
                             str = str + " and  isnull(xxqr,0)=0  ";
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

                     if (bllxxgl.ResetTag(strset, str))
                     {
                         if (UserType == 4)
                         {
                             bllxxgl.deletedyfwh(strksh,0);
                         }

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

                string result = bllxxgl.ImportExcelData(path, Department);

                //Response.Clear();
                //Response.Buffer = false;
                //Response.Charset = "GB2312";
                //Response.ContentEncoding = System.Text.Encoding.UTF8;

                //Response.AddHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode("导入日志" + string.Format("{0:yyyyMMddHHmmss}", System.DateTime.Now) + ".txt"));
                ////指定返回的是一个不能被客户端读取的流，必须被下载      
                //Response.ContentType = "text/plain";
                ////把文件流发送到客户端     
                //Response.Write(result.ToString());
                //Response.Flush();
                ////停止页面的执行      
                //Response.End();
                //StreamWriter sw = File.CreateText(Server.MapPath("tmpUpLoadFile\\" + "导入日志" + string.Format("{0:yyyyMMddHHmmss}", System.DateTime.Now) + ".txt"));
                //sw.Write(result.ToString());
                //sw.Close();

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
                    strset = " xqqr=1 ";
                    str = str + " and  isnull(ksqr,0)=2 and isnull(xxdy,0)=1 and isnull(xxqr,0)=1";
                    break;
                //市招生办
                case 2:
                    strset = " xqqr=1 ";
                    str = str + " and  isnull(ksqr,0)=2 and isnull(xxdy,0)=1 and isnull(xxqr,0)=1";
                    break;
                //区招生办
                case 3:
                    strset = " xxqr=1,xxdy=1  ";
                    str = str + " and  isnull(ksqr,0)=2 ";
                    break;
                //学校用户 
                case 4:
                    strset = " xxqr=1,xxdy=1 ";
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

            if (bllxxgl.ResetTag(strset, str))
            {
                E_record = "全部确认:[" + UserType + "]状态全部确认";
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
                        str = str + " and  isnull(ksqr,0)=2 and isnull(xxdy,0)=1 and isnull(xxqr,0)=1";
                        break;
                    //学校用户 
                    case 4:
                        strset = " xxqr=1 ";
                        str = str + " and  isnull(ksqr,0)=2 and  isnull(xxdy,0)=1";
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

                if (bllxxgl.ResetTag(strset, str))
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

                if (bllxxgl.ResetTag(strset, str))
                {
                    E_record = "取消确认:[" + UserType + "]状态确认" + str;
                    EventMessage.EventWriteDB(1, E_record);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('取消确认成功！ ')</script>");
                    BindGv();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('取消确认失败！ ')</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择需要取消确认的考生！ ')</script>");
              
            }
        }

        protected void btnchuS_Click(object sender, EventArgs e)
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
                //if (shiqu.Length > 0)
                //{
                //    str = str + "  bmdxqdm='" + shiqu + "' and ";
                //}
                //if (xuexiao.Length > 0)
                //{
                //    str = str + "   bmddm='" + xuexiao + "' and ";
                //}
                //if (banji.Length > 0)
                //{
                //    str = str + "   bjdm='" + banji + "'  and";
                //}

                str = str + " 1=1";
                string strset = " ksqr=0,pic=0,xxdy=0,xxqr=0,xqqr=0,zyksqr=0,zyxxqr=0,zyxqqr=0,zyxxdy=0,pwd=ksh";
                if (bllxxgl.ResetTag(strset, str))
                {
                    bllxxgl.deletedyfwh(strksh,0);
                    E_record = "重置回初始状态:[" + strksh + "]";
                    EventMessage.EventWriteDB(1, E_record);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('重置成功！ ')</script>");
                    BindGv();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('重置失败！ ')</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择需要重置的考生！ ')</script>");
              
            }
        }
        /// <summary>
        /// 强制修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnqiangUp_Click(object sender, EventArgs e)
        {
            if (Request.Form["CheckBox1"] != null)
            {
                string[] ids = Request.Form["CheckBox1"].ToString().Split(',');

                if (ids.Length == 1)
                {
                  //  Model_zk_ksxxgl model = new BLL_zk_ksxxgl().zk_ksxxglDisp(ids[0]);

                    if (SincciLogin.Sessionstu().UserType == 1)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>opUp('" + ids[0] + "', '考生个人信息修改') ;</script>");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('您没有该操作权限!');</script>");

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


        #region"检查关联服务器相片"
        /// <summary>
        /// 检查关联服务器相片
        /// </summary> 
        protected void btnCheckPic_Click(object sender, EventArgs e)
        {
            string xqdm = this.dlistSq.SelectedValue;
            string xxdm = this.dlistXx.SelectedValue;
            if (xqdm.Length == 0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择县区！')</script>");
            }
            else
            {

                #region "管理部门权限"
                int UserType = SincciLogin.Sessionstu().UserType;
                string Department = SincciLogin.Sessionstu().U_department;
                string strset = "";
                switch (UserType)
                {
                    //系统管理员
                    case 1:
                        strset = " 1=1 ";
                        break;
                    //市招生办
                    case 2:
                        strset = " 1=1 ";
                        break;
                    //区招生办
                    case 3:
                        strset = " bmdxqdm='" + Department + "'  ";
                        break;
                    //学校用户 
                    case 4:
                        strset = " bmddm='" + Department + "' ";
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


                string where = " bmdxqdm='" + xqdm + "' and isnull(pic,0)=0 and " + strset;
                if (xxdm.Length > 0)
                    where = where + " and bmddm='" + xxdm + "' ";

                int totalRecord = 0;
                DataTable dt = new BLL_zk_ksxxgl().ExecuteProc(where, int.MaxValue, 1, ref   totalRecord);
                if (totalRecord > 0)
                {
                    if (dt.Rows.Count > 0)
                    {
                        string picurl = "";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            picurl = Server.MapPath("//13//" + dt.Rows[i]["bmdxqdm"].ToString() + "//" + dt.Rows[i]["ksh"].ToString() + ".jpg");
                            //Server.MapPath("\\pic\\" + dt.Rows[i]["bmdxqdm"].ToString() + "\\" + dt.Rows[i]["bmddm"].ToString() + "\\" + dt.Rows[i]["ksh"].ToString() + ".jpg");

                            if (File.Exists(picurl))
                            {
                                new BLL_zk_ksxxgl().KsPhoto(dt.Rows[i]["ksh"].ToString());
                            }
                        }
                    }
                }
                E_record = "检查相片: 检查服务器相片进行关联。";
                EventMessage.EventWriteDB(1, E_record);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('操作完成！')</script>");

                BindGv();
            }

        }
        #endregion

      

       

    }
}