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
    public partial class kdqk_Manage : BPage
    {
        
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
                BLL_zk_zdxx zdxx = new BLL_zk_zdxx();
                this.listleib.DataSource = zdxx.selectData("kdqklb");
                this.listleib.DataTextField = "zlbmc";
                this.listleib.DataValueField = "zlbdm";
                this.listleib.DataBind();
                this.listleib.Items.Insert(0, new ListItem("请选择", ""));
                this.listkemu.DataSource = zdxx.selectData("kskm");
                this.listkemu.DataTextField = "zlbmc";
                this.listkemu.DataValueField = "zlbdm";
                this.listkemu.DataBind();
                this.listkemu.Items.Insert(0, new ListItem("请选择", ""));
                 BindGv();
                 Loads();
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
             //导出数据无情况
            if (!new Method().CheckButtonPermission(PopedomType.A64))
            {
                this.btndaocwqk.Visible = false;
            }
            
        }
        #endregion
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGv()
        {
        

            int RecordCount = 0;
            int pageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            string strWhere = createWhere();
            DataTable tab = BLL_kd.ExecuteProc_View_ksz(strWhere, pageSize, AspNetPager1.CurrentPageIndex, ref RecordCount);
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
                where = where + " kmdm='" + listkemu.SelectedValue + "' and ";
            }
            if (listleib.SelectedValue.Length > 0)
            {
                where = where + " kcqkdm='" + listleib.SelectedValue + "' and ";
            }
            if (txtksh.Text.Trim().Length > 0)
            {
                where = where + " zkzh='" + txtksh.Text.Trim() + "' and ";
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
                    if (BLL_kd.Delete(where))
                    {
                        string E_record = "删除: 考点情况考生数据：" + ids + "";
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
            string name = "考点情况" + strDate1;
            name = System.Web.HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8);
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + name + ".xls");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");  //设置输出流为简体中文 
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            gvOrders.AllowPaging = false;
            gvOrders.AllowSorting = false;
            gvOrders.DataSource = BLL_kd.daochu(quanxian());
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

        protected void btnok_Click(object sender, EventArgs e)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            if (UserType == 7)
            {
                string kddm = SincciLogin.Sessionstu().UserName.Substring(3);
                string where = "";
                string kmdm = "";
                DateTime time = Convert.ToDateTime(DateTime.Now.GetDateTimeFormats('M')[0].ToString());//
                DataTable dt = BLL_kd.Select_zk_kstime();
                DateTime t1 = Convert.ToDateTime(dt.Rows[0]["t1"].ToString().Trim());
                DateTime t2 = Convert.ToDateTime(dt.Rows[0]["t2"].ToString().Trim());
                DateTime t3 = Convert.ToDateTime(dt.Rows[0]["t3"].ToString().Trim());
                int a = DateTime.Compare(time, t1);
                int b = DateTime.Compare(time, t2);
                int c = DateTime.Compare(time, t3);
                string s1 = "";
                string x1 = "";
                if (a == 0)
                {
                      s1 = dt.Rows[0]["s1"].ToString().Trim();
                      x1 = dt.Rows[0]["x1"].ToString().Trim();
                }
                else if (b == 0)
                {
                    s1 = dt.Rows[0]["s2"].ToString().Trim();
                    x1 = dt.Rows[0]["x2"].ToString().Trim();
                }
                else if (c == 0)
                {
                    s1 = dt.Rows[0]["s3"].ToString().Trim();
                    x1 = dt.Rows[0]["x3"].ToString().Trim();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('现在不在考试期间!');</script>");
                    return;
                }

                if (s1 != "")
                {
                    string j1 = s1.Split(' ')[0].Trim();
                    string k1 = s1.Split(' ')[1].Trim();
                    DateTime j1a = Convert.ToDateTime(j1.Split('-')[0]);
                    DateTime j1b = Convert.ToDateTime(j1.Split('-')[1]);
                    if (DateTime.Compare(DateTime.Now, j1a) == 1)
                    {
                        TimeSpan ts = DateTime.Now - j1b;
                        if (ts.Hours < 2)
                        {
                            string rekm = rekmdm(k1);
                            if (rekm != "")
                            {
                                where = " kddm='" + kddm + "' and kmdm='" + rekm + "' ";
                                if (BLL_kd.Select_kdqkzt(where).Rows.Count == 0)
                                {
                                    Model_zk_kdqkzt model = new Model_zk_kdqkzt();
                                    model.Kddm = kddm;
                                    model.Kmdm = rekm;
                                    model.Type = 1;
                                    if (BLL_kd.Insert_zk_kdqkzt(model))
                                    {
                                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('信息提交成功!');</script>");

                                    }
                                    else
                                    {
                                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('信息提交失败!');</script>");

                                    }
                                }
                                else
                                {
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('您已经提交过信息!');</script>");

                                }
                            }
                        }
                    }
                }
                if (x1 != "")
                {
                    string j1 = x1.Split(' ')[0].Trim();
                    string k1 = x1.Split(' ')[1].Trim();
                    DateTime j1a = Convert.ToDateTime(j1.Split('-')[0]);
                    DateTime j1b = Convert.ToDateTime(j1.Split('-')[1]);
                    if (DateTime.Compare(DateTime.Now, j1a) == 1)
                    {
                        TimeSpan ts = DateTime.Now - j1b;
                        if (ts.Hours < 2)
                        {
                            string rekm = rekmdm(k1);
                            if (rekm != "")
                            {
                                where = " kddm='" + kddm + "' and kmdm='" + rekm + "' ";
                                if (BLL_kd.Select_kdqkzt(where).Rows.Count == 0)
                                {
                                    Model_zk_kdqkzt model = new Model_zk_kdqkzt();
                                    model.Kddm = kddm;
                                    model.Kmdm = kmdm;
                                    model.Type = 1;
                                    if (BLL_kd.Insert_zk_kdqkzt(model))
                                    {
                                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('信息提交成功!');</script>");

                                    }
                                    else
                                    {
                                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('信息提交失败!');</script>");

                                    }
                                }
                                else
                                {
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('您已经提交过信息!');</script>");

                                }
                            }
                        }
                    }
                }
           
               
            }     
        }

        /// <summary>
        /// 是否隐藏控件
        /// </summary>
        private void Loads()
        {
           
                 DateTime time = Convert.ToDateTime(DateTime.Now.GetDateTimeFormats('M')[0].ToString());//
                 DataTable dt = BLL_kd.Select_zk_kstime();
                 DateTime t1 = Convert.ToDateTime(dt.Rows[0]["t1"].ToString().Trim());
                 DateTime t2 = Convert.ToDateTime(dt.Rows[0]["t2"].ToString().Trim());
                 DateTime t3 = Convert.ToDateTime(dt.Rows[0]["t3"].ToString().Trim());
                 int a = DateTime.Compare(time, t1);
                 int b = DateTime.Compare(time, t2);
                 int c = DateTime.Compare(time, t3);
                 string s1 = "";
                 string x1 = "";
                 if (a == 0)
                 {
                     s1 = dt.Rows[0]["s1"].ToString().Trim();
                     x1 = dt.Rows[0]["x1"].ToString().Trim();
                 }
                 else if (b == 0)
                 {
                     s1 = dt.Rows[0]["s2"].ToString().Trim();
                     x1 = dt.Rows[0]["x2"].ToString().Trim();
                 }
                 else if (c == 0)
                 {
                     s1 = dt.Rows[0]["s3"].ToString().Trim();
                     x1 = dt.Rows[0]["x3"].ToString().Trim();
                 }
                 else
                 {
                     
                 }

                 if (s1 != "")
                 {
                     string j1 = s1.Split(' ')[0].Trim();
                     string k1 = s1.Split(' ')[1].Trim();
                     DateTime j1a = Convert.ToDateTime(j1.Split('-')[0]);
                     DateTime j1b = Convert.ToDateTime(j1.Split('-')[1]);
                     if (DateTime.Compare(DateTime.Now, j1a) == 1)
                     {
                         TimeSpan ts = DateTime.Now - j1b;
                         if (ts.Hours < 2)
                         {
                             btnNew.Enabled = true;
                             btnEdit.Enabled = true;
                             btnDelete.Enabled = true;
                             btnok.Enabled = true;
                         }
                     }
                 }
                 if (x1 != "")
                 {
                     string j1 = x1.Split(' ')[0].Trim();
                     string k1 = x1.Split(' ')[1].Trim();
                     DateTime j1a = Convert.ToDateTime(j1.Split('-')[0]);
                     DateTime j1b = Convert.ToDateTime(j1.Split('-')[1]);
                     if (DateTime.Compare(DateTime.Now, j1a) == 1)
                     {
                         TimeSpan ts = DateTime.Now - j1b;
                         if (ts.Hours < 2)
                         {
                             btnNew.Enabled = true;
                             btnEdit.Enabled = true;
                             btnDelete.Enabled = true;
                             btnok.Enabled = true;
                         }
                     }
                 }
 
           
        }
        /// <summary>
        /// 返回科目代码
        /// </summary>
        /// <returns></returns>
        private string rekmdm(string kmmc)
        {
           
            if (kmmc.IndexOf("语文") != -1)
            {
                return "1";
            }
            else if (kmmc.IndexOf("数学") != -1)
            {
                return "2";
            }
            else if (kmmc.IndexOf("英语") != -1)
            {
                return "3";
            }
            else if (kmmc.IndexOf("思品") != -1)
            {
                return "4";
            }
            else if (kmmc.IndexOf("物理") != -1)
            {
                return "5";
            }
            else
            {
                return "";
            }
        }

        protected void btndaocwqk_Click(object sender, EventArgs e)
        {

            string strDate1 = DateTime.Now.ToString("yyyyMMddHHmmss");
            GridView gvOrders = new GridView();
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "GB2312";
            string name = "考点无情况上报记录" + strDate1;
            name = System.Web.HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8);
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + name + ".xls");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");  //设置输出流为简体中文 
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            gvOrders.AllowPaging = false;
            gvOrders.AllowSorting = false;
            gvOrders.DataSource = BLL_kd.daochuwqk();
            gvOrders.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            gvOrders.DataBind();
            gvOrders.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }

        public string times( string km)
        {
            DateTime time = Convert.ToDateTime(DateTime.Now.GetDateTimeFormats('M')[0].ToString());//
            DataTable dt = BLL_kd.Select_zk_kstime();
            DateTime t1 = Convert.ToDateTime(dt.Rows[0]["t1"].ToString().Trim());
            DateTime t2 = Convert.ToDateTime(dt.Rows[0]["t2"].ToString().Trim());
            DateTime t3 = Convert.ToDateTime(dt.Rows[0]["t3"].ToString().Trim());
            int a = DateTime.Compare(time, t1);
            int b = DateTime.Compare(time, t2);
            int c = DateTime.Compare(time, t3);
            string s1 = "";
            string x1 = "";
            if (a == 0)
            {
                s1 = dt.Rows[0]["s1"].ToString().Trim();
                x1 = dt.Rows[0]["x1"].ToString().Trim();
            }
            else if (b == 0)
            {
                s1 = dt.Rows[0]["s2"].ToString().Trim();
                x1 = dt.Rows[0]["x2"].ToString().Trim();
            }
            else if (c == 0)
            {
                s1 = dt.Rows[0]["s3"].ToString().Trim();
                x1 = dt.Rows[0]["x3"].ToString().Trim();
            }
            else
            {

            }

            if (s1 != "")
            {
                string j1 = s1.Split(' ')[0].Trim();
                string k1 = s1.Split(' ')[1].Trim();
                DateTime j1a = Convert.ToDateTime(j1.Split('-')[0]);
                DateTime j1b = Convert.ToDateTime(j1.Split('-')[1]);
                if (DateTime.Compare(DateTime.Now, j1a) == 1)
                {
                    TimeSpan ts = DateTime.Now - j1b;
                    if (ts.Hours < 2)
                    {
                        if (km == k1)
                        {
                            return "";
                        }
                    }
                }
            }
            if (x1 != "")
            {
                string j1 = x1.Split(' ')[0].Trim();
                string k1 = x1.Split(' ')[1].Trim();
                DateTime j1a = Convert.ToDateTime(j1.Split('-')[0]);
                DateTime j1b = Convert.ToDateTime(j1.Split('-')[1]);
                if (DateTime.Compare(DateTime.Now, j1a) == 1)
                {
                    TimeSpan ts = DateTime.Now - j1b;
                    if (ts.Hours < 2)
                    {
                        if (km == k1)
                        {
                            return "";
                        }
                    }
                }
            }
            return "disabled=\"disabled\"";
        }
    }
}