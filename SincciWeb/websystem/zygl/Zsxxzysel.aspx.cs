using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SincciKC.websystem.zygl
{
    public partial class Zsxxzysel : BPage
    {
        public int page = Convert.ToInt32(config.sink("page", config.MethodType.Get, 255, 1, config.DataType.Int));
        public int pagesize = Convert.ToInt32(config.sink("pagesize", config.MethodType.Get, 255, 1, config.DataType.Int));
        /// <summary>
        /// BLL信息管理
        /// </summary>
        BLL_xxgl bllxxgl = new BLL_xxgl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

                Permission();
                loadPcInfo();
                Showxq();
            }
        }
        private BLL_zk_xqdm bllxqdm = new BLL_zk_xqdm();
        private void Showxq()
        {
            
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            DataTable dt = bllxqdm.SelectXqdm(Department, UserType);
            chkxqdm.DataSource = dt;
            this.chkxqdm.DataTextField = "xqmc";
            this.chkxqdm.DataValueField = "xqdm";
            this.chkxqdm.DataBind();
          
        }
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

        protected void btntj_Click(object sender, EventArgs e)
        {
            BindGv();
        }
        private void BindGv()
        {
            if (page == 0)
                page = 1;
            int RecordCount = 0;
            if (ddlXpcInfo.SelectedValue != "" && dllxx.SelectedValue != "")
            {
            string where = "";
            string str = this.ddlXpcInfo.SelectedItem.Text;
            int begin = 0;
            int end = 0;
            begin = str.IndexOf('[');
            end = str.IndexOf(']');
            string pcdm = str.Substring(begin + 1, end - begin - 1);
            string xxdm = dllxx.SelectedValue;
            where = " c.pcdm='" + pcdm + "' and c.xxdm='" + xxdm + "' ";
            string xqdm = "";

            for (int i = 0; i < chkxqdm.Items.Count; i++)
            {
                if (chkxqdm.Items[i].Selected == true)
                {
                    xqdm += chkxqdm.Items[i].Value + ",";
                }
            }
            if (xqdm.Length>0)
            {
                xqdm = xqdm.Remove(xqdm.Length-1);
            }
            if (xqdm.Length > 0)
            {
                where = where + " and bmdxqdm in (" + xqdm + ")";
            }
            pagesize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            this.Repeater1.DataSource = bllxxgl.ExecuteProcList_zy2(where, pagesize, AspNetPager1.CurrentPageIndex, ref RecordCount);
            this.Repeater1.DataBind();
            //分页

            config.AspNetPagerCustomInfoHTML2(AspNetPager1, RecordCount, pagesize);
            }
        }
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            pagesize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            BindGv();
        }
        /// <summary>
        /// 加载批次信息。
        /// </summary>
        private void loadPcInfo()
        {
            BLL_LQK_Ks_Xx zk = new BLL_LQK_Ks_Xx();
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string where = "";
            switch (UserType)
            {
                //系统管理员
                case 1:
                    where = where + " xqdm='500'   ";
                    break;
                //市招生办
                case 2:

                    where = " 1=1 ";

                    break;
                case 3:
                    where = where + " xqdm='500'   ";
                    break;
                //招生学校
                case 9:
                case 8:
                    where = where + " xqdm='500'   "; break;
                default:
                    where = " 1<>1";
                    break;
            }

            DataTable tab = zk.selectPcdm(where, " 1=1 ");
            this.ddlXpcInfo.DataSource = tab;
            this.ddlXpcInfo.DataTextField = "xpc_mc";
            this.ddlXpcInfo.DataValueField = "xpcid";
            this.ddlXpcInfo.DataBind();
            this.ddlXpcInfo.Items.Insert(0, new ListItem("-请选择-", ""));
        }
        /// <summary>
        /// 加载学校信息
        /// </summary>
        private void Loadxx()
        {
            string str = this.ddlXpcInfo.SelectedItem.Text;
            int begin = 0;
            int end = 0;
            begin = str.IndexOf('[');
            end = str.IndexOf(']');
            string pcdm = str.Substring(begin + 1, end - begin - 1);
            BLL_LQK_Ks_Xx zk = new BLL_LQK_Ks_Xx();
            if (pcdm != "01" && pcdm != "")
            {
                pcdm = "11";
            }
            DataTable tab = zk.Select_zk_zsxx_sel(pcdm, " 1=1 ");
            this.dllxx.DataSource = tab;
            this.dllxx.DataTextField = "zsxxmc";
            this.dllxx.DataValueField = "xxdm";
            this.dllxx.DataBind();
            this.dllxx.Items.Insert(0, new ListItem("-请选择-", ""));
        }
        /// <summary>
        /// 上下一页
        /// </summary> 
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            BindGv();
        }
        protected void ddlXpcInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlXpcInfo.SelectedValue.Length > 0)
            {
                Loadxx();
            }
        }
    }
}