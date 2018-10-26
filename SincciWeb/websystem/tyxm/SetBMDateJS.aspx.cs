using System;
using System.Collections.Generic;
 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Model;

using BLL;
using Model;
using System.Data;
namespace SincciKC.websystem.tyxm
{
    public partial class SetBMDateJS : BPage
    {
        /// <summary>
        /// 设置报名时间控制类
        /// </summary>
        BLL_zk_szbmsj Bll_szbmsj = new BLL_zk_szbmsj();
        /// <summary>
        /// 县区代码控制类
        /// </summary>
        BLL_zk_xqdm BLL_xqdm = new BLL_zk_xqdm();

        /// <summary>
        /// 设置报名时间实体类
        /// </summary>
        Model_zk_szbmsj item = new Model_zk_szbmsj();

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int UserType = SincciLogin.Sessionstu().UserType;
                string Department = SincciLogin.Sessionstu().U_department;


                Permission();                
                Loadsq();
                BindGv();

            }
        }
        #region "绑定数据"
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGv()
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            #region "管理部门权限控制"
            string fanwei = Department;
            if (Department.Length == 4 && Department.Substring(2, 2) == "00")
            {
                fanwei = Department.Substring(0, 2);
            } 

            string where = "";
            switch (UserType)
            {

                //系统管理员
                case 1:
                    where = "";
                    break;
                //市招生办
                case 2:
                    where = "";
                    break;
                //区招生办
                case 3:
                    where = " A.xqdm like '" + fanwei + "%'  ";
                    break;
                //学校用户 
                case 4:
                    where = " A.xqdm like '" + fanwei + "%' ";
                    break;
                //班级用户 
                case 5:
                    where = " A.xqdm like '" + fanwei + "%' ";

                    break;
            }
            #endregion

            DataTable tab = Bll_szbmsj.Select(where);
            this.repDisplay.DataSource = tab;
            this.repDisplay.DataBind();            
        }

        /// <summary>
        /// 加载市区信息
        /// </summary>
        private void Loadsq()
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            #region "管理部门权限控制"
            string fanwei = Department;
            if (Department.Length == 4 && Department.Substring(2, 2) == "00")
            {
                fanwei = Department.Substring(0, 2);
            }
            if (fanwei.Length > 4)
            {
                fanwei = Department.Substring(0, 4);
            }

            string where = "";
            switch (UserType)
            {

                //系统管理员
                case 1:
                    where = " right(xqdm,2) not in('99')  ";
                    break;
                //市招生办
                case 2:
                    where = " right(xqdm,2) not in('99')  ";
                    break;
                //区招生办
                case 3:
                    where = " right(xqdm,2) not in('00','99') and  xqdm like '" + fanwei + "%'  ";
                    break;
                //学校用户 
                case 4:
                    where = "right(xqdm,2) not in('00','99') and xqdm like '" + fanwei + "%'";
                    break;
                //班级用户 
                case 5:
                    where = " right(xqdm,2) not in('00','99') and xqdm like '" + fanwei + "%' ";

                    break;
            }
            #endregion

            this.ddlxqdm.DataSource = BLL_xqdm.selectxqdm(where);
            this.ddlxqdm.DataTextField = "xqmc";
            this.ddlxqdm.DataValueField = "xqdm";
            this.ddlxqdm.DataBind();

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
            //设置
            if (!new Method().CheckButtonPermission(PopedomType.A4))
            {
                this.btnSetDate.Visible = false;
            }
             
        }
        #endregion
        //操作记录
        public string E_record = "";
        /// <summary>
        /// 保存设置
        /// </summary> 
        protected void btnSetDate_Click(object sender, EventArgs e)
        {
            string xqdm = this.ddlxqdm.SelectedValue;
            string BeTime = Request.Form["StartTime"].Trim().ToString();
            string EnTime = Request.Form["EndTime"].Trim().ToString();
            if (BeTime.Length > 0 && EnTime.Length > 0)
            {
                item.Xqdm = xqdm;
                item.Kssj_js = Convert.ToDateTime(BeTime);
                item.Jssj_js = Convert.ToDateTime(EnTime);

                if (config.DateTimeCompare(item.Kssj_js, item.Jssj_js) < 0)
                {

                    if (Bll_szbmsj.SelectDisp(xqdm))
                    {
                        Bll_szbmsj.update_zk_szbmsjJS(item);
                        Response.Redirect("SetBMDateJS.aspx", false);
                    }
                    else
                    {
                        Bll_szbmsj.Insert_zk_szbmsj(item);

                        Response.Redirect("SetBMDateJS.aspx", false);
                    }
                    E_record = "设置: 报名时间：" + item.Xqdm + "";
                    EventMessage.EventWriteDB(1, E_record);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'开始时间不能大于结束时间！' ,title:'操作提示'}); </script>");
                    //BindGv();
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请选择时间！' ,title:'操作提示'}); </script>");

            }

        } 

    }
}