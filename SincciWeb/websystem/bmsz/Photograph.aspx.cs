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
    public partial class Photograph : BPage
    {
        /// <summary>
        /// 报名号
        /// </summary>
        public string ksh = Convert.ToString(config.sink("ksh", config.MethodType.Get, 255, 1, config.DataType.Str));
        /// <summary>
        /// 毕业中学县区代码
        /// </summary>
        public string xqdm = Convert.ToString(config.sink("xqdm", config.MethodType.Get, 255, 1, config.DataType.Str));
        /// <summary>
        /// 毕业中学代码
        /// </summary>
        public string bmddm = Convert.ToString(config.sink("bmddm", config.MethodType.Get, 255, 1, config.DataType.Str));
        /// <summary>
        /// 图片存放地址
        /// </summary>
        public string picPath = "";

        public string url = HttpContext.Current.Request.Url.ToString().Substring(0, HttpContext.Current.Request.Url.ToString().LastIndexOf("/"));
        public string PicPath = "";
        int pic = Convert.ToInt32(config.sink("pic", config.MethodType.Get, 255, 1, config.DataType.Int));
        public string imgPath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (ksh.Length == 0)
                {
                    imgPath = "/images/nopic.gif";
                }
                else
                {
                    string FolderPath = systemparam.picPath + xqdm + "/" + bmddm;
                    picPath = FolderPath + "/" + ksh;
                    if (ksh.Length > 0)
                        config.FolderCreate(Server.MapPath("/" + FolderPath + ""));

                    Model.Model_zk_ksxxgl info = new Model.Model_zk_ksxxgl();
                    info = new BLL_zk_ksxxgl().ViewDisp(ksh);

                    this.lblksh.Text = ksh;
                    this.lblxm.Text = info.Xm;
                    this.lblsfzh.Text = info.Sfzh;
                    if (pic == 1)
                    {
                        imgPath = picPath + ".jpg";
                    }
                    else
                    {
                        imgPath = "/images/nopic.gif";
                    }
                }
            }
        }
    }
}