using System;
using System.Collections.Generic;
 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

using System.Data;
using BLL;
using System.Text;
using System.Data.Odbc;
using System.IO;

using System.Configuration;

namespace SincciKC.websystem.zygl
{
    public partial class ZZSBxxgl : System.Web.UI.Page
    {
        public int page = Convert.ToInt32(config.sink("page", config.MethodType.Get, 255, 1, config.DataType.Int));
        public int pagesize = Convert.ToInt32(config.sink("pagesize", config.MethodType.Get, 255, 1, config.DataType.Int));
        //string url = HttpContext.Current.Request.Url.ToString().Substring(0, HttpContext.Current.Request.Url.ToString().LastIndexOf("/"));
        string url = "http://" + HttpContext.Current.Request.Url.Host + ":" + ConfigurationManager.AppSettings["duankou"];
     
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
        /// 加载学校信息
        /// </summary>
        private void Loadxx()
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


            BindGv();
        }
        /// <summary>
        /// 条件
        /// </summary>
        private string strwhere()
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string str = "";
            string shiqu = dlistSq.SelectedValue;
            string xuexiao = dlistXx.SelectedValue;
            string banji = dlistBj.SelectedValue;
            string KeyWord = config.CheckChar(this.txtName.Text.Trim().ToString());
            string tag = this.dlistZt.SelectedValue;

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
                if (Convert.ToInt32(tag) == 0)
                {
                    str = str + "  isnull(sbksqr,0)='0'  and ";
                }
                else if (Convert.ToInt32(tag) == 1)
                {
                    str = str + "  isnull(sbksqr,0) ='1'  and ";
                }
                else if (Convert.ToInt32(tag) == 2)
                {
                    str = str + "  isnull(sbksqr,0)='2' and ";
                }
                else if (Convert.ToInt32(tag) == 3)
                {
                    str = str + "  isnull(sbksqr,0)='1' and ";
                }
                else if (Convert.ToInt32(tag) == 4)
                {
                    str = str + "  isnull(zyxxdy,0)='0' and ";
                }
                else if (Convert.ToInt32(tag) == 5)
                {
                    str = str + "  isnull(zyxxdy,0)='1' and ";
                }
                else if (Convert.ToInt32(tag) == 6)
                {
                    str = str + "  isnull(zyxqqr,0)='0' and ";
                }
                else if (Convert.ToInt32(tag) == 7)
                {
                    str = str + "  isnull(zyxxqr,0)='1' and ";
                }
                else if (Convert.ToInt32(tag) == 8)
                {
                    str = str + "  isnull(zyxxqr,0)='0' and ";
                }
                else if (Convert.ToInt32(tag) == 9)
                {
                    str = str + "  isnull(sbksqr,0)='2' and isnull(zyxxdy,0)='0' and ";
                }
            }

            //管理部门权限
            string where = "";
            switch (UserType)
            {
                //系统管理员
                case 1:
                    where = " xxdy=1 ";
                    break;
                //市招生办
                case 2:
                    where = " xxdy=1 ";
                    break;
                //区招生办
                case 3:
                    where = " xxdy=1 and bmdxqdm = '" + Department + "'  ";
                    break;
                //学校用户 
                case 4:
                    where = " xxdy=1 and  bmddm = '" + Department + "' ";
                    break;
                //班级用户 
                case 5:
                    where = " xxdy=1 and  bjdm = '" + Department.Substring(6) + "' and bmddm='" + Department.Substring(0, 6) + "' ";
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
            ////打印
            //if (!new Method().CheckButtonPermission(PopedomType.A4))
            //{
            //    this.btndayin.Visible = false;
            //}
       
            //状态重置
            if (!new Method().CheckButtonPermission(PopedomType.A8))
            {
                this.btnResetTag.Visible = false;
            }
            //导出数据
            if (!new Method().CheckButtonPermission(PopedomType.A16))
            {
                this.btndaoAll.Visible = false;
            }
            ////县区确认
            //if (!new Method().CheckButtonPermission(PopedomType.A32))
            //{
            //    this.btnXqQr.Visible = false;
            //}

            //重置密码
            if (!new Method().CheckButtonPermission(PopedomType.A64))
            {
                this.btnReset.Visible = false;
            }
            //原状态
            if (!new Method().CheckButtonPermission(PopedomType.A128))
            {
                this.btnyuans.Visible = false;
            }
            ////学校确认
            //if (!new Method().CheckButtonPermission(PopedomType.A256))
            //{
            //    this.btnxxqr.Visible = false;
            //}
            ////志愿修改
            //if (!new Method().CheckButtonPermission(PopedomType.A512))
            //{
            //    this.btnZyUp.Visible = false;
            //}
             //重置为未填报
            if (!new Method().CheckButtonPermission(PopedomType.A1024))
            {
                this.btnwtb.Visible = false;
            }
            
        }
        #endregion

        
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
        /// 市区下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dlistSq_SelectedIndexChanged(object sender, EventArgs e)
        {
            Loadxx();
        }

        #region "打印事件"
        /// <summary>
        /// 打印事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btndayin_Click(object sender, EventArgs e)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;

            string str = "";

            if (Request.Form["CheckBox1"] != null)
            {
                str = Request.Form["CheckBox1"].ToString();
            }
            if (str.Length > 0)
            {
                str = " ksh in (" + str + ") and ";  //只打印已经确认的               
            }
            else
            {
                //string shiqu = dlistSq.SelectedValue;
                //string xuexiao = this.dlistXx.SelectedValue;
                //string banji = this.dlistBj.SelectedValue;
                //string name = config.CheckChar(txtName.Text.Trim());
                //if (shiqu.Length > 0)
                //{
                //    str += " bmdxqdm='" + shiqu + "' and ";
                //}
                //if (xuexiao.Length > 0)
                //{
                //    str += " bmddm='" + xuexiao + "' and ";
                //}
                //if (banji.Length > 0)
                //{
                //    str += " bjdm='" + banji + "' and ";
                //}

                //if (name.Length > 0)
                //{
                //    str += "  (ksh='" + name + "' or xm='" + name + "' or sfzh='" + name + "') and ";
                //}
            }
            //Response.Write(url);
            //Response.Write(HttpContext.Current.Request.Url.AbsoluteUri);
            //Response.Write(HttpContext.Current.Request.Url.Host);
            
           
            if (str.Length > 0)
            {
                str = str + "  ksqr=2 and sbksqr=2 and "; //只打已确认的考生。
            }
            if (str == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >alert('请选择打印条件！');</script>");
            }
            else
            {
                string where = quanxian();
                str = str + where;


                if (new BLL_zk_zydz().updatezk_ksxxglzy(str, 1, UserType))
                {
                    string strzkzh = "";//打印传值
                    DataTable dt = bllxxgl.seleckshgrxx(str);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        strzkzh = strzkzh + "?ksh=" + dt.Rows[i]["ksh"] + "&type=2|";

                    }
                    string E_record = "打印: 考生数据：" + str + "";
                    EventMessage.EventWriteDB(1, E_record);
                    ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript ", "<script id=clientEventHandlersVBS language=vbscript> GzzkWebPrint1.ShowMe \"" + url + "/websystem/zygl/Zyxxgl_Mange.aspx\",\"" + strzkzh + "\"  </script> ");
                    BindGv();
                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >alert('打印失败');</script>");
                }
           
             
            }
        }
        #endregion

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
        /// <summary>
        /// 上下一页
        /// </summary> 
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            BindGv();
        }

        /// <summary>
        /// 状态重置
        /// </summary> 
        protected void btnResetTag_Click(object sender, EventArgs e)
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
            if (strksh.Length > 0)
            {
                str = str + "  ksh in(" + strksh + ") and ";
            }
           
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

                #region "管理部门权限"
                string strset = "";
                switch (UserType)
                {
                    //系统管理员
                    case 1:
                        strset = " zyxqqr=0 ";
                        str = str + " and zyxqqr=1 ";
                        break;
                    //市招生办
                    case 2:
                        strset = " zyxqqr=0 ";
                        str = str + " and zyxqqr=1  ";
                        break;
                    //区招生办
                    case 3:
                        strset = " zyxxqr=0,zyxxdy=0  ";
                        str = str + " and zyxxqr=1 and isnull(zyxqqr,0)=0 ";
                        break;
                    //学校用户 
                    case 4:
                        strset = " sbksqr=1,zyxxdy=0";
                        str = str + " and  sbksqr=2 and  isnull(zyxxqr,0)=0 ";
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
                  string  E_record = "状态重置: 状态重置数据：" + str + "";
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
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择考生！ ')</script>");
            }
        }
        
        /// <summary>
        /// 县区确认
        /// </summary> 
        protected void btnXqQr_Click(object sender, EventArgs e)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;

            string str = "";
            string shiqu = dlistSq.SelectedValue;
            string xuexiao = dlistXx.SelectedValue;
            string banji = dlistBj.SelectedValue;
            string strksh = "";
            if (shiqu.Length > 0)
            {
                if (Request.Form["CheckBox1"] != null)
                {
                    strksh = Request.Form["CheckBox1"].ToString();
                }
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

                if (bllxxgl.XqQrKSZY(str))
                {
                    string E_record = "县区确认: 确认考生数据。";
                    EventMessage.EventWriteDB(1, E_record);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('操作成功！ ')</script>");
                    BindGv();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('操作失败！ ')</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择要确认的县区！ ')</script>");
            }
        }

        protected void btndaoAll_Click(object sender, EventArgs e)
        {

         

           
           
          
            #region 创建dbf副本
            string _f = "zyxx";
            string tbl = Server.MapPath("/Template/" + _f + ".dbf");
            string fileTemName = _f + "_" + DateTime.Now.ToString("ffff");
            string filetemPath = Server.MapPath("/Temp/" + fileTemName + ".dbf");
            File.Copy(tbl, filetemPath, true);

            #endregion

            #region 创建连接
            OdbcConnection conn = new System.Data.Odbc.OdbcConnection();
            string table = filetemPath;
            string connStr = @"Driver={Microsoft Visual FoxPro Driver};SourceType=DBF;SourceDB=" + table + ";Exclusive=No;NULL=NO;Collate=Machine;BACKGROUNDFETCH=NO;DELETED=NO";
            conn.ConnectionString = connStr;
            conn.Open();
            // string strOdbcConn = @" DRIVER=Microsoft Visual FoxProDriver;UID=;Collate=Machine;BackgroundFetch=Yes;      Exclusive=No;SourceType=DBF;SourceDB=" + strFilePath + ";";
            //OdbcConnection odbcConn = new OdbcConnection(strOdbcConn);
            //string sqlInsert = "Insert Into table1(DateFrom, Num) Values({^2005-09-10},10)";
            //OdbcCommand odbcComm = new OdbcCommand(sqlInsert, odbcConn);
            //odbcComm.Connection.Open();
            //odbcComm.ExecuteNonQuery();
            //odbcConn.Close();
            #endregion

            #region 写入
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            DataTable xqtab = bllxqdm.SelectXqdm(Department, UserType);
            for (int j = 0; j < xqtab.Rows.Count; j++)
            {
                DataTable tab = new BLL_zk_kszyxx().ExportData_zzsb(" left(ksh,6)='13'+'" + xqtab.Rows[j]["xqdm"].ToString() + "' and xxdm!='' ");
                //  string sql = "";
                StringBuilder sql = new StringBuilder();
                int num = 1;
                for (int i = 0; i < tab.Rows.Count; i++)
                {

                    sql.Append("Insert Into " + table + "(ksh,pcdm,xxdm,zy1,zy2,zysx,kjbs) Values('"
                        + tab.Rows[i]["ksh"].ToString() + "','"
                        + tab.Rows[i]["pcdm"].ToString() + "','"
                        + tab.Rows[i]["xxdm"].ToString() + "','"
                        + tab.Rows[i]["zy1"].ToString() + "','"
                        + tab.Rows[i]["zy2"].ToString() + "','"
                        + tab.Rows[i]["zysx"].ToString() + "','"
                        + tab.Rows[i]["kjbs"].ToString() + "');");
                    if (tab.Rows.Count > 10000)
                    {
                        if (i == num * 10000)
                        {
                            num++;
                            OdbcCommand cmd = new OdbcCommand();
                            cmd.Connection = conn;
                            cmd.CommandText = sql.ToString();
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                            sql = new StringBuilder();
                        }
                        if (i == tab.Rows.Count - 1)
                        {
                            OdbcCommand cmd = new OdbcCommand();
                            cmd.Connection = conn;
                            cmd.CommandText = sql.ToString();
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                            sql = new StringBuilder();
                        }
                    }
                    else
                    {
                        OdbcCommand cmd = new OdbcCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = sql.ToString();
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        sql = new StringBuilder();
                    }
                }
            }
           
            conn.Close();
            conn.Dispose();
            #endregion

            #region 弹出导出对话框
            string name = "考生信息导出" + DateTime.Now.ToLongDateString();
            name = System.Web.HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8);
            Response.ContentType = "application/x-zip-compressed";
            Response.Charset = "GB2312";

            Response.AddHeader("Content-Disposition", "attachment;filename=" + name + ".dbf");
            string filename = filetemPath;
            Response.WriteFile(filename);Response.Flush();Response.End();


            #endregion

            string E_record = "导出:导出数据!";
            EventMessage.EventWriteDB(1, E_record);
        }
        /// <summary>
        /// 导出志愿模版
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        private DataTable daochu(DataTable tabLod)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ksh");
            dt.Columns.Add("xm");
            dt.Columns.Add("xbmc");
            dt.Columns.Add("bmdmc");
            dt.Columns.Add("xqmc");
            dt.Columns.Add("bjmc");
            dt.Columns.Add("zy01_1");
            dt.Columns.Add("zy01_2");
            dt.Columns.Add("zy02_1");
            dt.Columns.Add("zy03_1");
            dt.Columns.Add("zy03_2");
            dt.Columns.Add("zy11_1");
            dt.Columns.Add("zy11_2");
            dt.Columns.Add("zy11_3");
            dt.Columns.Add("zy12_1");
            dt.Columns.Add("zy12_2");
            dt.Columns.Add("zy13_1");
            dt.Columns.Add("zy21_1");
            dt.Columns.Add("zy21_2");
            dt.Columns.Add("zy21_3");
            dt.Columns.Add("zy21_4");
            dt.Columns.Add("zy21_5");
            dt.Columns.Add("zy21_6");
            dt.Columns.Add("zy22_1");
            dt.Columns.Add("zy22_1_z1");
            dt.Columns.Add("zy22_2");
            dt.Columns.Add("zy22_2_z1");
            dt.Columns.Add("zy23_1");
            dt.Columns.Add("zy23_1_z1");
            dt.Columns.Add("zy23_1_z2");
            dt.Columns.Add("zy23_1_fc");
            dt.Columns.Add("zy23_2");
            dt.Columns.Add("zy23_2_z1");
            dt.Columns.Add("zy23_2_z2");
            dt.Columns.Add("zy23_2_fc");
            dt.Columns.Add("zy23_3");
            dt.Columns.Add("zy23_3_z1");
            dt.Columns.Add("zy23_3_z2");
            dt.Columns.Add("zy23_3_fc");
            dt.Columns.Add("zy24_1");
            dt.Columns.Add("zy24_2");
            dt.Columns.Add("zy24_3");
            dt.Columns.Add("zy24_4");
            dt.Columns.Add("zy24_5");
            dt.Columns.Add("zy24_6");

            dt.Columns.Add("zy25_1");
            dt.Columns.Add("zy25_2");
            dt.Columns.Add("zy25_3");

            dt.Columns.Add("zy26_1");
            dt.Columns.Add("zy26_2");
            dt.Columns.Add("zy26_3");
            dt.Columns.Add("zy26_4");

            dt.Columns.Add("zy27_1");
            dt.Columns.Add("zy27_1_z1");
            dt.Columns.Add("zy27_1_z2");
            dt.Columns.Add("zy27_1_fc");
            dt.Columns.Add("zy27_2");
            dt.Columns.Add("zy27_2_z1");
            dt.Columns.Add("zy27_2_z2");
            dt.Columns.Add("zy27_2_fc");
            dt.Columns.Add("zy27_3");
            dt.Columns.Add("zy27_3_z1");
            dt.Columns.Add("zy27_3_z2");
            dt.Columns.Add("zy27_3_fc");


            dt.Columns.Add("zy31_1");
            dt.Columns.Add("zy31_1_z1");
            dt.Columns.Add("zy31_1_z2");
            dt.Columns.Add("zy31_1_fc");
            dt.Columns.Add("zy31_2");
            dt.Columns.Add("zy31_2_z1");
            dt.Columns.Add("zy31_2_z2");
            dt.Columns.Add("zy31_2_fc");
            dt.Columns.Add("zy31_3");
            dt.Columns.Add("zy31_3_z1");
            dt.Columns.Add("zy31_3_z2");
            dt.Columns.Add("zy31_3_fc");

            dt.Columns.Add("zy32_1");
            dt.Columns.Add("zy32_2");
            dt.Columns.Add("zy32_3");
            dt.Columns.Add("zy32_4");
            dt.Columns.Add("zy32_5");
            dt.Columns.Add("zy32_6");

            DataTable tabnew = new BLL_zk_kszyxx().ExportKsh();
           
            for (int i = 0; i < tabnew.Rows.Count; i++)
            {
                DataRow[] drnew = tabLod.Select(" ksh='" + tabnew.Rows[i]["ksh"] + "'");
                if (drnew.Length!=47)
                {
                    continue;
                }
                DataTable tab = tabLod.Clone();
                foreach (DataRow item in drnew)
                {
                    tab.ImportRow(item);
                }
                DataRow dr = dt.NewRow();
                dr["ksh"] = tab.Rows[0]["ksh"];
                dr["xm"] = tab.Rows[0]["xm"];
                dr["xbmc"] = tab.Rows[0]["xbmc"];
                dr["bmdmc"] = tab.Rows[0]["bmddm"].ToString() + tab.Rows[0]["bmdmc"];
                dr["xqmc"] = tab.Rows[0]["bmdxqdm"].ToString() + tab.Rows[0]["xqmc"];
                dr["bjmc"] = tab.Rows[0]["bjdm"];
                dr["zy01_1"] = tab.Rows[0]["xxdm"].ToString() + tab.Rows[0]["zsxxmc"];
                dr["zy01_2"] = tab.Rows[1]["xxdm"].ToString() + tab.Rows[1]["zsxxmc"];
                dr["zy02_1"] = tab.Rows[2]["xxdm"].ToString() + tab.Rows[2]["zsxxmc"];
                dr["zy03_1"] = tab.Rows[3]["xxdm"].ToString() + tab.Rows[3]["zsxxmc"];
                dr["zy03_2"] = tab.Rows[4]["xxdm"].ToString() + tab.Rows[4]["zsxxmc"];
                dr["zy11_1"] = tab.Rows[5]["xxdm"].ToString() + tab.Rows[5]["zsxxmc"];
                dr["zy11_2"] = tab.Rows[6]["xxdm"].ToString() + tab.Rows[6]["zsxxmc"];
                dr["zy11_3"] = tab.Rows[7]["xxdm"].ToString() + tab.Rows[7]["zsxxmc"];
                dr["zy12_1"] = tab.Rows[8]["xxdm"].ToString() + tab.Rows[8]["zsxxmc"];
                dr["zy12_2"] = tab.Rows[9]["xxdm"].ToString() + tab.Rows[9]["zsxxmc"];
                dr["zy13_1"] = tab.Rows[10]["xxdm"].ToString() + tab.Rows[10]["zsxxmc"];
                dr["zy21_1"] = tab.Rows[11]["xxdm"].ToString() + tab.Rows[11]["zsxxmc"];
                dr["zy21_2"] = tab.Rows[12]["xxdm"].ToString() + tab.Rows[12]["zsxxmc"];
                dr["zy21_3"] = tab.Rows[13]["xxdm"].ToString() + tab.Rows[13]["zsxxmc"];
                dr["zy21_4"] = tab.Rows[14]["xxdm"].ToString() + tab.Rows[14]["zsxxmc"];
                dr["zy21_5"] = tab.Rows[15]["xxdm"].ToString() + tab.Rows[15]["zsxxmc"];
                dr["zy21_6"] = tab.Rows[16]["xxdm"].ToString() + tab.Rows[16]["zsxxmc"];
                dr["zy22_1"] = tab.Rows[17]["xxdm"].ToString() + tab.Rows[17]["zsxxmc"];
                dr["zy22_1_z1"] = tab.Rows[17]["zy1"].ToString() + tab.Rows[17]["zymc1"];
                dr["zy22_2"] = tab.Rows[18]["xxdm"].ToString() + tab.Rows[18]["zsxxmc"];
                dr["zy22_2_z1"] = tab.Rows[18]["zy1"].ToString() + tab.Rows[18]["zymc1"];
                dr["zy23_1"] = tab.Rows[19]["xxdm"].ToString() + tab.Rows[19]["zsxxmc"];
                dr["zy23_1_z1"] = tab.Rows[19]["zy1"].ToString() + tab.Rows[19]["zymc1"];
                dr["zy23_1_z2"] = tab.Rows[19]["zy2"].ToString() + tab.Rows[19]["zymc2"];
                dr["zy23_1_fc"] = tab.Rows[19]["zy1"].ToString() == "" ? "" : tab.Rows[19]["zyfc"].ToString() == "True" ? "服从" : "不服从";
                dr["zy23_2"] = tab.Rows[20]["xxdm"].ToString() + tab.Rows[20]["zsxxmc"];
                dr["zy23_2_z1"] = tab.Rows[20]["zy1"].ToString() + tab.Rows[20]["zymc1"];
                dr["zy23_2_z2"] = tab.Rows[20]["zy2"].ToString() + tab.Rows[20]["zymc2"];
                dr["zy23_2_fc"] = tab.Rows[20]["zy1"].ToString() == "" ? "" : tab.Rows[20]["zyfc"].ToString() == "True" ? "服从" : "不服从";
                dr["zy23_3"] = tab.Rows[21]["xxdm"].ToString() + tab.Rows[21]["zsxxmc"];
                dr["zy23_3_z1"] = tab.Rows[21]["zy1"].ToString() + tab.Rows[21]["zymc1"];
                dr["zy23_3_z2"] = tab.Rows[21]["zy2"].ToString() + tab.Rows[21]["zymc2"];
                dr["zy23_3_fc"] = tab.Rows[21]["zy1"].ToString() == "" ? "" : tab.Rows[21]["zyfc"].ToString() == "True" ? "服从" : "不服从";
                dr["zy24_1"] = tab.Rows[22]["xxdm"].ToString() + tab.Rows[22]["zsxxmc"];
                dr["zy24_2"] = tab.Rows[23]["xxdm"].ToString() + tab.Rows[23]["zsxxmc"];
                dr["zy24_3"] = tab.Rows[24]["xxdm"].ToString() + tab.Rows[24]["zsxxmc"];
                dr["zy24_4"] = tab.Rows[25]["xxdm"].ToString() + tab.Rows[25]["zsxxmc"];
                dr["zy24_5"] = tab.Rows[26]["xxdm"].ToString() + tab.Rows[26]["zsxxmc"];
                dr["zy24_6"] = tab.Rows[27]["xxdm"].ToString() + tab.Rows[27]["zsxxmc"];

                dr["zy25_1"] = tab.Rows[28]["xxdm"].ToString() + tab.Rows[28]["zsxxmc"];
                dr["zy25_2"] = tab.Rows[29]["xxdm"].ToString() + tab.Rows[29]["zsxxmc"];
                dr["zy25_3"] = tab.Rows[30]["xxdm"].ToString() + tab.Rows[30]["zsxxmc"];

                dr["zy26_1"] = tab.Rows[31]["xxdm"].ToString() + tab.Rows[31]["zsxxmc"];
                dr["zy26_2"] = tab.Rows[32]["xxdm"].ToString() + tab.Rows[32]["zsxxmc"];
                dr["zy26_3"] = tab.Rows[33]["xxdm"].ToString() + tab.Rows[33]["zsxxmc"];
                dr["zy26_4"] = tab.Rows[34]["xxdm"].ToString() + tab.Rows[34]["zsxxmc"];

                dr["zy27_1"] = tab.Rows[35]["xxdm"].ToString() + tab.Rows[35]["zsxxmc"];
                dr["zy27_1_z1"] = tab.Rows[35]["zy1"].ToString() + tab.Rows[35]["zymc1"];
                dr["zy27_1_z2"] = tab.Rows[35]["zy2"].ToString() + tab.Rows[35]["zymc2"];
                dr["zy27_1_fc"] = tab.Rows[35]["zy1"].ToString() == "" ? "" : tab.Rows[35]["zyfc"].ToString() == "True" ? "服从" : "不服从";
                dr["zy27_2"] = tab.Rows[36]["zy1"].ToString() + tab.Rows[36]["zymc1"];
                dr["zy27_2_z1"] = tab.Rows[36]["zy1"].ToString() + tab.Rows[36]["zymc1"];
                dr["zy27_2_z2"] = tab.Rows[36]["zy2"].ToString() + tab.Rows[36]["zymc2"];
                dr["zy27_2_fc"] = tab.Rows[36]["zy1"].ToString() == "" ? "" : tab.Rows[36]["zyfc"].ToString() == "True" ? "服从" : "不服从";
                dr["zy27_3"] = tab.Rows[37]["xxdm"].ToString() + tab.Rows[37]["zsxxmc"];
                dr["zy27_3_z1"] = tab.Rows[37]["zy1"].ToString() + tab.Rows[37]["zymc1"];
                dr["zy27_3_z2"] = tab.Rows[37]["zy2"].ToString() + tab.Rows[37]["zymc2"];
                dr["zy27_3_fc"] = tab.Rows[37]["zy1"].ToString() == "" ? "" : tab.Rows[37]["zyfc"].ToString() == "True" ? "服从" : "不服从";


                dr["zy31_1"] = tab.Rows[38]["xxdm"].ToString() + tab.Rows[38]["zsxxmc"];
                dr["zy31_1_z1"] = tab.Rows[38]["zy1"].ToString() + tab.Rows[38]["zymc1"];
                dr["zy31_1_z2"] = tab.Rows[38]["zy2"].ToString() + tab.Rows[38]["zymc2"];
                dr["zy31_1_fc"] = tab.Rows[38]["zy1"].ToString() == "" ? "" : tab.Rows[38]["zyfc"].ToString() == "True" ? "服从" : "不服从";
                dr["zy31_2"] = tab.Rows[39]["xxdm"].ToString() + tab.Rows[39]["zsxxmc"];
                dr["zy31_2_z1"] = tab.Rows[39]["zy1"].ToString() + tab.Rows[39]["zymc1"];
                dr["zy31_2_z2"] = tab.Rows[39]["zy2"].ToString() + tab.Rows[39]["zymc2"];
                dr["zy31_2_fc"] = tab.Rows[39]["zy1"].ToString() == "" ? "" : tab.Rows[39]["zyfc"].ToString() == "True" ? "服从" : "不服从";
                dr["zy31_3"] = tab.Rows[40]["xxdm"].ToString() + tab.Rows[40]["zsxxmc"];
                dr["zy31_3_z1"] = tab.Rows[40]["zy1"].ToString() + tab.Rows[40]["zymc1"];
                dr["zy31_3_z2"] = tab.Rows[40]["zy2"].ToString() + tab.Rows[40]["zymc2"];
                dr["zy31_3_fc"] = tab.Rows[40]["zy1"].ToString() == "" ? "" : tab.Rows[40]["zyfc"].ToString() == "True" ? "服从" : "不服从";

                dr["zy32_1"] = tab.Rows[41]["xxdm"].ToString() + tab.Rows[41]["zsxxmc"];
                dr["zy32_2"] = tab.Rows[42]["xxdm"].ToString() + tab.Rows[42]["zsxxmc"];
                dr["zy32_3"] = tab.Rows[43]["xxdm"].ToString() + tab.Rows[43]["zsxxmc"];
                dr["zy32_4"] = tab.Rows[44]["xxdm"].ToString() + tab.Rows[44]["zsxxmc"];
                dr["zy32_5"] = tab.Rows[45]["xxdm"].ToString() + tab.Rows[45]["zsxxmc"];
                dr["zy32_6"] = tab.Rows[46]["xxdm"].ToString() + tab.Rows[46]["zsxxmc"];
                dt.Rows.Add(dr);
            }

            return dt;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            if (Request.Form["CheckBox1"] != null)
            {
                string ids = Request.Form["CheckBox1"].ToString();

                if (ids.Length > 0)
                {
                    //管理部门权限
                    string where = quanxian();

                    where = where + " and ksh in(" + ids + ")";
                    if (bllxxgl.ResetPwd(where))
                    {

                        string  E_record = "密码重置: 密码重置数据：" + ids + "";
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

        protected void btnyuans_Click(object sender, EventArgs e)
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
            if (strksh.Length > 0)
            {
                str = str + "  ksh in(" + strksh + ") and ";
            }
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



                #region "管理部门权限"
                string strset = "";
                switch (UserType)
                {
                    //系统管理员
                    case 1:
                        strset = "  sbksqr=1 ";
                        break;
                    //市招生办
                    case 2:
                        strset = " 1<>1"; break;
                    //区招生办
                    case 3:
                        strset = " 1<>1"; break;
                    //学校用户 
                    case 4:
                        strset = " 1<>1"; break;
                    //班级用户 
                    case 5:
                        strset = " 1<>1"; break;
                    default:
                        strset = " 1<>1";
                        break;
                }

                #endregion
                if (bllxxgl.ResetTag(strset, str))
                {
                    string E_record = "状态重置: 状态重置数据：" + str + "";
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
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择要重置的考生 ')</script>");
            }
        }
        /// <summary>
        /// 学校确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnxxqr_Click(object sender, EventArgs e)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;

            string str = "";

            if (Request.Form["CheckBox1"] != null)
            {
                str = Request.Form["CheckBox1"].ToString();
            }
            if (str.Length > 0)
            {
                str = " ksh in (" + str + ") and ";  //只打印已经确认的               
            }
            else
            {
                //string shiqu = dlistSq.SelectedValue;
                //string xuexiao = this.dlistXx.SelectedValue;
                //string banji = this.dlistBj.SelectedValue;
                //string name = config.CheckChar(txtName.Text.Trim());
                //if (shiqu.Length > 0)
                //{
                //    str += " bmdxqdm='" + shiqu + "' and ";
                //}
                //if (xuexiao.Length > 0)
                //{
                //    str += " bmddm='" + xuexiao + "' and ";
                //}
                //if (banji.Length > 0)
                //{
                //    str += " bjdm='" + banji + "' and ";
                //}

                //if (name.Length > 0)
                //{
                //    str += "  (ksh='" + name + "' or xm='" + name + "' or sfzh='" + name + "') and ";
                //}
            }
            if (str.Length > 0)
            {
                str = str + "  ksqr=2 and sbksqr=2 and zyxxdy=1 and "; //只打已确认的考生。
            }
            if (str == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >alert('请选择学校确认条件！');</script>");
            }
            else
            {
                string where = quanxian();
                str = str + where;


                if (new BLL_zk_zydz().updatezk_ksxxglzyxxqr(str, 1, UserType))
                {
                    string E_record = "考生志愿确认: 学校确认：" + str + "";
                    EventMessage.EventWriteDB(1, E_record);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('学校确认成功！ ')</script>");
                    BindGv();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >alert('学校确认失败');</script>");
                }
            }
        }
        /// <summary>
        /// 志愿修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnZyUp_Click(object sender, EventArgs e)
        {
            if (Request.Form["CheckBox1"] != null)
            {
                string[] ids = Request.Form["CheckBox1"].ToString().Split(',');

                if (ids.Length == 1)
                {
                    Model_zk_ksxxgl model = new BLL_zk_ksxxgl().zk_ksxxglDisp(ids[0]);

                    if (SincciLogin.Sessionstu().UserType == 1 && (model.Zyksqr == 1 || model.Zyksqr==2))
                    {
                        Model_zk_ksSession ksSession = new Model_zk_ksSession();
                        ksSession.ksh = ids[0];
                        ksSession.xm = "";
                        ksSession.kaoci = "";
                        System.Web.HttpContext.Current.Session.Add("kszy", ksSession);
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>opUp('" + ids[0] + "', '考生志愿信息修改') ;</script>");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('该考生未填报!');</script>");

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

        protected void btnwtb_Click(object sender, EventArgs e)
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
            if (strksh.Length > 0)
            {
                str = str + "  ksh in(" + strksh + ") and ";
            }
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



                #region "管理部门权限"
                string strset = "";
                switch (UserType)
                {
                    //系统管理员
                    case 1:
                        strset = "  sbksqr=0 ";
                        break;
                    //市招生办
                    case 2:
                        strset = " 1<>1"; break;
                    //区招生办
                    case 3:
                        strset = " 1<>1"; break;
                    //学校用户 
                    case 4:
                        strset = " 1<>1"; break;
                    //班级用户 
                    case 5:
                        strset = " 1<>1"; break;
                    default:
                        strset = " 1<>1";
                        break;
                }

                #endregion
                if (bllxxgl.ResetTag(strset, str))
                {
                    bllxxgl.delete_zk_zzsbxx(str);
                    string E_record = "状态重置: 状态重置数据：" + str + "";
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
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择要重置的考生 ')</script>");
            }
        }
    }
        
}