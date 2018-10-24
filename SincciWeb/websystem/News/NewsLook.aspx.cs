using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
 

namespace SincciWeb.NewsList
{
    public partial class NewsLook : BPage
    {
        #region "Page_Load"
        int newsID = Convert.ToInt32(config.sink("NewsID", config.MethodType.Get, 255, 1, config.DataType.Int));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                OnStart();
            }
        }
        #endregion

        #region "绑定数据"
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="qp"></param>
        private void OnStart()
        {
            PE_NewsListTable fam = new PE_NewsListTable();
            fam = new Method().PE_NewsListDisp(newsID);
            this.lblTitle.Text = "发布日期：" + fam.PublishTime.ToString() + "&nbsp;&nbsp;浏览次数：" + fam.Number.ToString();
            this.lblContent.Text = fam.Content.Trim().ToString();
        }
        #endregion
    }
}