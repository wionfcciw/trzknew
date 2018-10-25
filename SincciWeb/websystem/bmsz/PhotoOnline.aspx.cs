using System;
using System.Collections.Generic;
 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

using System.Data;
using BLL;
namespace SincciKC.websystem.bmsz
{
    public partial class PhotoOnline : BPage
    {
        #region "控制类"
        /// <summary>
        /// BLL信息管理
        /// </summary>
        BLL_xxgl bllxxgl = new BLL_xxgl();
        /// <summary>
        /// 学校代码控制类
        /// </summary>
        BLL_zk_xxdm bllxxdm=new BLL_zk_xxdm();
        /// <summary>
        /// 班级代码控制类
        /// </summary>
        BLL_zk_bjdm bllbjdm = new BLL_zk_bjdm();
        /// <summary>
        /// 县区代码控制类
        /// </summary>
        BLL_zk_xqdm bllxqdm = new BLL_zk_xqdm();
        #endregion

        public int page = Convert.ToInt32(config.sink("page", config.MethodType.Get, 255, 1, config.DataType.Int));
        public int pagesize = Convert.ToInt32(config.sink("pagesize", config.MethodType.Get, 255, 1, config.DataType.Int));
       
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

            dlistSq.DataSource = bllxqdm.SelectXqdm(Department,UserType);
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
            this.Repeater1.DataSource = bllxxgl.ExecuteProcPic(where, pagesize, AspNetPager1.CurrentPageIndex, ref RecordCount);
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
            string KeyWord = config.CheckChar(this.txtksh.Text.Trim().ToString());
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
                str = str + " (ksh='" + KeyWord + "' or xm like '%" + KeyWord + "%') and ";
            }

            //管理部门权限
            string where = "";
            switch (UserType)
            {                
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
            //Response.Redirect(config.GetScriptName + "?page=1&pagesize=" + this.ddlPageSize.SelectedValue.ToString() + "");
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
            
            ////排序
            //if (!new Method().CheckButtonPermission(PopedomType.Orderby))
            //{
            //    this.Enabled = false;
            //}
            ////打印
            //if (!new Method().CheckButtonPermission(PopedomType.Print))
            //{
            //    this.btnPrint.Enabled = false;
            //}
            ////备用A
            //if (!new Method().CheckButtonPermission(PopedomType.A))
            //{
            //    this.btnA.Enabled = false;
            //}
            ////备用B
            //if (!new Method().CheckButtonPermission(PopedomType.B))
            //{
            //    this.btnB.Enabled = false;
            //}
        }
        #endregion

       
        /// <summary>
        /// 选择县区加载学校数据
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

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            BindGv();
        }


    }
}