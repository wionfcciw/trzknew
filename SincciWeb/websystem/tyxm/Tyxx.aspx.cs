using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
using System.Data;
using System.IO;
using System.Text;

namespace SincciKC.websystem.tyxm
{
    public partial class Tyxx : System.Web.UI.Page
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
        BLL_tyxm bllxxgl = new BLL_tyxm();


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
            string banji = dlistBj.SelectedValue;
            string KeyWord = config.CheckChar(this.txtName.Text.Trim().ToString());
            string zhuangtai = dlistkslx.SelectedValue;

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
            if (zhuangtai.Length > 0)
            {
                str = str + " kstyqr=" + zhuangtai + " and ";
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
                    where = " right(left(ksh,6),4) = '" + Department + "'  ";
                    break;
                //学校用户 
                case 4:
                    where = " right(left(ksh,8),6) = '" + Department + "' ";
                    break;
                //班级用户 
                case 5:
                    where = " 1<>1 ";
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

            //导出
            if (!new Method().CheckButtonPermission(PopedomType.A4))
            {
                this.btndaoAll.Visible = false;
            }
            //删除
            if (!new Method().CheckButtonPermission(PopedomType.A8))
            {
                btnDelete.Visible = false;
            }
            //修改
            if (!new Method().CheckButtonPermission(PopedomType.A16))
            {
                this.btnEdit.Visible = false;
            } //确认
            if (!new Method().CheckButtonPermission(PopedomType.A32))
            {
                this.btndanOK.Visible = false;
            } //全部确认
            if (!new Method().CheckButtonPermission(PopedomType.A64))
            {
                btnAllOK.Visible = false;
            }
            //状态重置
            if (!new Method().CheckButtonPermission(PopedomType.A128))
            {
                btnResetTag.Visible = false;
            }
        }
        #endregion

        /// <summary>
        /// 点击删除。
        /// </summary>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
           

            bool result = bllxxgl.Delete_zk_kstyks(hfDelIDS.Value.Split(',').ToList());

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
                    Model_zk_ksxxgl model = new BLL_zk_ksxxgl().zk_ksxxglDisp(ids[0]);
                    if (model.Xxqr == 1)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('学校已确认该考生的信息,不能修改!');</script>");

                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>opdg('" + ids[0] + "', '修改数据') ;</script>");
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
        /// 上下一页
        /// </summary> 
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {

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
        /// <summary>
        /// 学校下拉框
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            string txtFileName = "会考成绩导入日志" + config.Get_UserName + ".txt";
            string txtFileNameRAR = "会考成绩导入日志" + config.Get_UserName + ".zip";
            string destFileName = Server.MapPath(String.Format("/tmpUpLoadFile/" + txtFileName));

            if (File.Exists(destFileName))
            {
                string destFileNameRAR = Server.MapPath(String.Format("/tmpUpLoadFile/" + "会考成绩导入日志" + config.Get_UserName + ".zip"));
                ZipClass zip = new ZipClass();
                zip.ZipFile(destFileName, destFileNameRAR);
                Response.Redirect("/tmpUpLoadFile/" + txtFileNameRAR);

                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script> window.parent.addTab2('会考成绩导入日志', '/tmpUpLoadFile/" + txtFileName + "');</script>");

                string E_record = "下载: 会考成绩上传日志记录" + config.Get_UserName + "";
                EventMessage.EventWriteDB(1, E_record);
            }
        }

        protected void btndaoAll_Click(object sender, EventArgs e)
        {

            string str = "";
            string shiqu = dlistSq.SelectedValue;
            string xuexiao = dlistXx.SelectedValue;
            string banji = dlistBj.SelectedValue;
             
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
                    where = " right(left(ksh,6),4) = '" + Department + "'  ";
                    break;
                //学校用户 
                case 4:
                    where = " right(left(ksh,8),6) = '" + Department + "' ";
                    break;
                //班级用户 
                case 5:
                    where = " 1<>1 ";
                    break;
                default:
                    where = " 1<>1";
                    break;
            }
            where = str + where;
            GridView gvOrders = new GridView();
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "GB2312";
            string name = "导出体育报名信息" + DateTime.Now.ToLongDateString();
            name = System.Web.HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8);
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + name + ".xls");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");  //设置输出流为简体中文 
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            gvOrders.AllowPaging = false;
            gvOrders.AllowSorting = false;
            gvOrders.DataSource = bllxxgl.selectyDaochu(where);
            gvOrders.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            gvOrders.DataBind();
            gvOrders.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }

        #region "全部确认"
        /// <summary>
        /// 全部确认
        /// </summary> 
        protected void btnAllOK_Click(object sender, EventArgs e)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string E_record = "";
            string str = "";

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
                    strset = " xqtyqr=0 ";
                    str = str + " and 1<>1 ";

                    break;
                //市招生办
                case 2:
                    strset = " xqtyqr=0 ";
                    str = str + " and 1<>1 ";
                    break;
                //区招生办
                case 3:
                    strset = " xqtyqr=1,xqqrsj=getdate() ";
                    str = str + " and xxtyqr=1 ";
                    break;
                //学校用户 
                case 4:
                    strset = " xxtyqr=1,xxqrsj=getdate()  ";
                    str = str + " and  kstyqr=2 ";
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


            if (bllxxgl.Update_xxqr(strset, str))
            {
                E_record = "确认:[" + SincciLogin.Sessionstu().U_department + "]全部确认!";
                EventMessage.EventWriteDB(1, E_record);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('确认成功！ ')</script>");
                BindGv();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('确认失败！ ')</script>");
            }
        }
        #endregion 
       
        #region "选择确认"
        /// <summary>
        /// 选择确认
        /// </summary> 
        protected void btndanOK_Click(object sender, EventArgs e)
        {

            string str = "";
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string strksh = "";
            if (Request.Form["CheckBox1"] != null)
            {
                strksh = Request.Form["CheckBox1"].ToString();

                if (strksh.Length > 0)
                {
                    str = str + "  ksh in(" + strksh + ") and ";
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
                        strset = " xqtyqr=0 ";
                        str = str + " and 1<>1 ";

                        break;
                    //市招生办
                    case 2:
                        strset = " xqtyqr=0 ";
                        str = str + " and 1<>1 ";
                        break;
                    //区招生办
                    case 3:
                        strset = " xqtyqr=1,xqqrsj=getdate()  ";
                        str = str + " and xxtyqr=1 ";
                        break;
                    //学校用户 
                    case 4:
                        strset = " xxtyqr=1,xxqrsj=getdate()  ";
                        str = str + " and  kstyqr=2 ";
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

                if (bllxxgl.Update_xxqr(strset,str))
                {

                    string E_record = "确认:[" + strksh + "]状态确认";
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
         #endregion 

        #region "权限"
        /// <summary>
        /// 权限
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
                    where = " ksh in (select ksh from zk_ksxxgl where bmdxqdm = '" + Department + "') ";
                    break;
                //学校用户 
                case 4:
                    where = " ksh in (select ksh from zk_ksxxgl where bmddm = '" + Department + "' ) ";
                    break;
                //班级用户 
                case 5:
                    where = " ksh in (select ksh from zk_ksxxgl where bjdm = '" + Department.Substring(6) + "' and bmddm='" + Department.Substring(0, 6) + "' )";
                    break;
                default:
                    where = " 1<>1";
                    break;
            }
            return where;
        }
        #endregion

        #region "重置状态"
        /// <summary>
        /// 重置状态
        /// </summary> 
        protected void btnResetTag_Click(object sender, EventArgs e)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string E_record = "";
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
                            strset = " xqtyqr=0 ";
                            str = str + " and  isnull(xqtyqr,0)=1 ";

                            break;
                        //市招生办
                        case 2:
                            strset = " xqtyqr=0 ";
                            str = str + " and  isnull(xqtyqr,0)=1 ";
                            break;
                        //区招生办
                        case 3:
                            strset = " xxtyqr=0 ";
                            str = str + " and  isnull(xqtyqr,0)=0 ";
                            break;
                        //学校用户 
                        case 4:
                            strset = " kstyqr=1  ";
                            str = str + " and  isnull(xxtyqr,0)=0  ";
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
        #endregion

    }
}