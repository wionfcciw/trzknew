using System;
using System.Collections.Generic;
  
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Model;
using BLL;
using BLL.system;

namespace SincciKC.webUI.news
{
    public partial class NewsList1 : SPage //wf 2018-08-16 修改，单点登录统一控制 System.Web.UI.Page
    {

        public int categoryId = Convert.ToInt32(config.sink("ID", config.MethodType.Get, 255, 1, config.DataType.Int));
        int page = Convert.ToInt32(config.sink("page", config.MethodType.Get, 255, 1, config.DataType.Int));
        int PageSize = 15;
        public string NewsID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }

        }


        #region  绑定信息
        /// <summary>
        /// 根据种类ID获取类型信息
        /// </summary>
        private void BindData()
        {
            if (categoryId > 0)
            {
                if (page == 0)
                    page = 1;

                string CategoryName = new Method().PE_NewsCategoryDisp(categoryId).CategoryName;
                int recordCount = 0;
                this.lblCategoryName.Text = CategoryName;

                this.NewsContent.InnerHtml = new BLL_news().ShowArticleList(categoryId, PageSize, 40, 1, page, 1, ref recordCount).ToString();

                config.CustomInfoHTML_List(AspNetPager1, recordCount, PageSize);

            }
            else
            {
                Response.Redirect("/");
            }
        }

        #endregion

        #region AspNetPager1_PageChanged
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }
        #endregion

    }
}