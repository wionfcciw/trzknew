using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SincciKC.websystem.zklq
{
    public partial class Listpage : BPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Permission();
                //绑定PageSize数据
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

        }
        #endregion

        #region "绑定数据"
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGv()
        {

            BLL_zk_PiLiangTouDang zk = new BLL_zk_PiLiangTouDang();
            DataTable tab = zk.GetTableDatas(this.ischeked.Checked);
            this.repDisplay.DataSource = tab;
            this.repDisplay.DataBind();

        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGv();
        }
    }
}