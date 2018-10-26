using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
 
namespace SincciWeb.websystem.News
{
    public partial class News_Show : BPage
    {
        #region "Page_Load"
        int NewsID = Convert.ToInt32(config.sink("NewsID", config.MethodType.Get, 255, 1, config.DataType.Int));
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
            fam = new Method().PE_NewsListDisp(NewsID);
            this.lblTitle.Text = fam.Title;
            this.lblCreate.Text = "发布日期：" + fam.PublishTime + "&nbsp;&nbsp; 发布人：" + fam.Editor;
            this.lblurl.Text = fam.TitleUrls;
            this.lblContent.Text = fam.Content.Trim().ToString();
           // this.lblbz.Text = fam.Remark.Trim().ToString();

            string E_record =   "浏览新闻 ： " + fam.Title;

            EventMessage.EventWriteDB(1, E_record);


        }
        #endregion
    }
}