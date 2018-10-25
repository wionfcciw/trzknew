using System;
using System.Collections.Generic;
 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections;

using Model;
using BLL;
using System.Data;
using System.IO;
using System.Linq; 
namespace SincciWeb.system
{
    public partial class UserManage : BPage
    {
        #region "Page_Load"
        int pagesize = Convert.ToInt32(config.sink("pagesize", config.MethodType.Get, 255, 1, config.DataType.Int));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //页面权限
                Permission();
                //绑定PageSize数据
                this.ddlPageSize.DataSource = new config().PageSizelist();
                this.ddlPageSize.DataBind();
                //初始化PageSize
                QueryParam qp = new QueryParam();


                if (pagesize>0)
                {
                    qp.PageSize = pagesize;
                    this.ddlPageSize.SelectedValue = pagesize.ToString();
                }
                else
                {
                    qp.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
                }
                UserTypeddl(SincciLogin.Sessionstu().UserType);
                OnStart(qp);
            }
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
                GridView1.Columns[12].Visible = false;
            }
            //初始化密码
            if (!new Method().CheckButtonPermission(PopedomType.A16))
            {
                this.btnReset.Visible = false;
            }
            //导入
            if (!new Method().CheckButtonPermission(PopedomType.A32))
            {
                divdaoru.Visible = false;
            }
            //删除
            if (!new Method().CheckButtonPermission(PopedomType.A64))
            {
                this.btnDelete.Visible = false;
            }
        }
        #endregion

        #region "绑定数据"
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="qp"></param>
        private void OnStart(QueryParam qp)
        {
            qp.Order = " order by U_usertype, U_department ";

            //加上范围控制
            string fanwei = SincciLogin.Sessionstu().U_department;
            int UserType = SincciLogin.Sessionstu().UserType;

            if (fanwei.Length == 3 && fanwei.Substring(1, 2) == "00")
                fanwei = fanwei.Substring(0, 1);
            else if (fanwei.Length == 3)
                fanwei = fanwei.Substring(1);

            if (UserType == 1)
            {
                qp.Where =  " U_usertype>=" + UserType + " ";
            }
            else
                qp.Where =   " U_department like '" + fanwei + "%' and U_usertype>=" + UserType + " ";
          
            
            int RecordCount = 0;
         //   ArrayList lst = UserData.UserArrayList(qp, out RecordCount);
            GridView1.DataSource = new UserData().UserArrayList(qp, out RecordCount);
            GridView1.DataBind();
            //分页
            config.CustomInfoHTML(AspNetPager1, RecordCount, qp.PageSize);
        }
        #endregion

        #region "删除数据 btnDelete操作"
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            bool flag = false;
            string str = "";
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
                if (cbox.Checked == true)
                {
                    sys_UserTable sat = new sys_UserTable();
                    sat.UserID = Convert.ToInt32(GridView1.DataKeys[i]["Userid"].ToString());
                    sat.DB_Option_Action_ = "Delete";
                    //删除角色
                    new Method().Sys_usersInsertUpdateDelete(sat);
                    flag = true;
                    str = str + sat.UserID + ",";
                }
            }
            string E_record = "删除: 用户管理数据：" + str + "";
           
            if (flag)
            {
                EventMessage.EventWriteDB(1, E_record);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.succeedInfo({message:'删除成功！' ,title:'操作提示'});setTimeout(function(){ ymPrompt.close();},1000);</script>");

                //操作完绑定数据
                QueryParam qp = new QueryParam();
                qp.PageIndex = AspNetPager1.CurrentPageIndex;
                qp.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
                OnStart(qp);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请选择信息！' ,title:'提示'});setTimeout(function(){ ymPrompt.close();},1000);</script>");

            }

        }
        #endregion

        /// <summary>
        /// 绑定用户类型数据
        /// </summary>
        private void UserTypeddl(int UserTypeID)
        {
            QueryParam qp = new QueryParam();
            qp.Order = " order by TypeID asc ";
            qp.OrderId = " TypeID ";
            qp.Where = string.Format(" TypeID>={0} ", UserTypeID);
            int RecordCount = 0;
            this.ddlType.DataSource = new Method().Sys_UserTypeList(qp, out RecordCount);
            this.ddlType.DataTextField = "T_Name";
            this.ddlType.DataValueField = "TypeID";
            this.ddlType.DataBind();
            this.ddlType.Items.Insert(0, new ListItem("请选择", ""));
        }

        #region "选择 PageSize SelectedIndexChanged事件"
        /// <summary>
        /// 选择 PageSize 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(config.GetScriptName + "?page=1&pagesize=" + this.ddlPageSize.SelectedValue.ToString() + "");
        }
        #endregion

        #region "全选/全不选 CheckedChanged事件"
        /// <summary>
        /// 全选/全不选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckbFull_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
                if (this.ckbFull.Checked)
                {
                    cbox.Checked = true;
                }
                else
                {
                    cbox.Checked = false;
                }
            }
        }
        #endregion

        #region "字段转换 RowDataBound事件"
        /// <summary>
        /// 字段转换 RowDataBound事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            //    if (e.Row.Cells[6].Text.Equals("0"))
            //    {
            //        e.Row.Cells[6].Text = "关闭";
            //        e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;
            //    }
            //    else
            //    {
            //        e.Row.Cells[6].Text = "开通";
            //    }
            //}
            ////修改权限
            //if (!new Method().CheckButtonPermission(PopedomType.A8))
            //{

            //    e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;
            //}
        }
        #endregion

        #region "初始化用户密码"
        /// <summary>
        /// 初始化用户密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            bool flag = false;
            string str = "";
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
                if (cbox.Checked == true)
                {
                    //初始化密码
                    sys_UserTable sat = new sys_UserTable();
                    sat.U_Password = "123456";
                    sat.UserID = Convert.ToInt32(GridView1.DataKeys[i]["Userid"].ToString());
                    sat.DB_Option_Action_ = "Reset";                   
                    new Method().Sys_usersInsertUpdateDelete(sat);
                    flag = true;
                    str = str + sat.UserID + ",";
                }
            }
            string E_record = "初始化: 用户管理密码数据：" + str + "";
            if (flag)
            {
             
                EventMessage.EventWriteDB(1, E_record);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.succeedInfo({message:'操作成功！' ,title:'操作提示'});setTimeout(function(){ ymPrompt.close();},1000);</script>");
                //操作完绑定数据
                QueryParam qp = new QueryParam();
                qp.PageIndex = AspNetPager1.CurrentPageIndex;
                qp.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
                OnStart(qp);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请选择信息！' ,title:'提示'});setTimeout(function(){ ymPrompt.close();},1000);</script>");

            } 
        }
        #endregion

        /// <summary>
        /// 管理部门名称转换
        /// </summary>
        /// <param name="U_usertype"></param>
        /// <param name="fanwei"></param>
        /// <returns></returns>
        public string fanweimc(int U_usertype, string fanwei)
        {
            BLL_zk_bjdm bllbjdm = new BLL_zk_bjdm();
            BLL_zk_xxdm bllxxdm = new BLL_zk_xxdm();
            BLL_zk_xqdm bllxqdm = new BLL_zk_xqdm();

            if (U_usertype == 5)
            {
                DataTable dt = bllbjdm.Select_zk_bjdm_bj(fanwei.Substring(0, 6), fanwei.Substring(6));
                if (dt.Rows.Count > 0)
                    return dt.Rows[0]["xxmc"].ToString() + dt.Rows[0]["bjmc"].ToString();
            }
            else if (U_usertype == 4)
            {
                return bllxxdm.Select_zk_xxdm(fanwei.Substring(0, 5)).Xxmc;
            }
            else
            {
                return bllxqdm.Disp(fanwei.Substring(0, 3)).Xqmc;
            }

            return "";
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            QueryParam qp = new QueryParam();
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
                    OnStart();
                    return;
                }

                string path = Server.MapPath("~/tmpUpLoadFile/") + config.FormatDateToStringId(DateTime.Now) + fileExtension;
                fuExcelFileImport.PostedFile.SaveAs(path);

                string result = new Method().ImportExcelData(path, SincciLogin.Sessionstu().UserType, SincciLogin.Sessionstu().U_department);

                msgWindow.InnerHtml = result;

                string E_record = "导入：帐号数据";
                EventMessage.EventWriteDB(1, E_record);

                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                    "<script>$(function () {if(confirm('导入完成是否需要查看导入日志？'))$('#msgWindow').window('open');});</script>");
            }

            OnStart();
        }
        

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="qp"></param>
        private void OnStart()
        {
            string type = this.ddlType.SelectedValue;
            string username = this.txtUser.Text.Trim().ToString();

            QueryParam qp = new QueryParam();
            string sql = "";
            qp.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            qp.PageIndex = AspNetPager1.CurrentPageIndex;
            qp.Order = " order by U_usertype, U_department ";

            //加上范围控制
            string fanwei = SincciLogin.Sessionstu().U_department;
            int UserType = SincciLogin.Sessionstu().UserType;

            if (fanwei.Length == 3 && fanwei.Substring(1, 2) == "00")
                fanwei = fanwei.Substring(0, 1);
            else if (fanwei.Length == 3)
                fanwei = fanwei.Substring(1);
            if (type.Length > 0)
                sql = " U_usertype='" + type + "' and ";
            if (username.Length > 0)
                sql = " U_loginname='" + username + "' and  ";

            //  qp.Where = sql  + "    U_usertype>=" + UserType + " ";
            if (UserType == 1)
            {
                qp.Where = sql + " U_usertype>=" + UserType + " ";
            }
            else
                qp.Where = sql + " U_department like '" + fanwei + "%' and U_usertype>=" + UserType + " ";
            int RecordCount = 0;
            GridView1.DataSource = new UserData().UserArrayList(qp, out RecordCount);
            GridView1.DataBind();
            //分页
            config.AspNetPagerCustomInfoHTML2(AspNetPager1, RecordCount, qp.PageSize);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            OnStart();
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            OnStart();
        }
    }
}