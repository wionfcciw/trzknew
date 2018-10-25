using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
using BLL.system;


namespace SincciWeb
{
    public partial class NewsInfo : SPage //wf 2018-08-16 修改，单点登录统一控制 System.Web.UI.Page
    {
        #region  Page_Load
        string newsID = config.CheckChar(Convert.ToString(config.sink("NewsID", config.MethodType.Get, 255, 1, config.DataType.Str)));
        public string categoryId = string.Empty;
        public int Id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                 
            }
        }
        #endregion

        #region 初始化时绑定数据
        /// <summary>
        /// 初始化时绑定数据
        /// </summary>
        private void BindData()
        {
            if (!string.IsNullOrEmpty(newsID))
            {
                PE_NewsListTable sat = new Method().PE_NewsListDisp(newsID);

                categoryId = sat.CategoryID.ToString();
                Id = sat.NewsID;
                lblNewsType.Text = new Method().PE_NewsCategoryDisp(sat.CategoryID).CategoryName;
                lblTitle.Text = sat.Title;
                //lblCategoryId.Text = sat.CategoryID.ToString();
               lblTime.Text = sat.PublishTime.ToString();
               // lblAuthor.Text= sat.Editor;
                //lblNumber.Text = sat.Number.ToString();
                lblContent.InnerHtml = sat.Content;    
            }
        }
        #endregion
        
    }
}