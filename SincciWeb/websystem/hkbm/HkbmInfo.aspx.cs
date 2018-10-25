using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using Model;
namespace SincciKC.websystem.hkbm
{
    public partial class HkbmInfo : BPage
    {

        BLL_Hkbm bllHkbm = new BLL_Hkbm();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (null != Request.QueryString["ksh"])
                {
                    string ksh = Request.QueryString["ksh"].ToString();
                    showinfo(ksh);
                }
            }
        }

        private void showinfo(string ksh)
        {

            DataTable dt = bllHkbm.getDataTableByKsh(ksh);
            if (dt.Rows.Count > 0)
            {

                lblksh.Text = ksh;
                lblxm.Text = dt.Rows[0]["xm"].ToString();
                lblxqmc.Text = dt.Rows[0]["xqmc"].ToString();
                lblkaocimc.Text = dt.Rows[0]["kaocimc"].ToString();
                lblxjh.Text = dt.Rows[0]["xjh"].ToString();
                lblbjmc.Text = dt.Rows[0]["bjmc"].ToString();
                lblbmdmc.Text = dt.Rows[0]["bmdmc"].ToString();
                lblsfzh.Text = dt.Rows[0]["sfzh"].ToString();
                lblbz.Text = dt.Rows[0]["bz"].ToString();
                imgPic.ImageUrl = getPhotoPath(ksh, dt.Rows[0]["pic"].ToString(), dt.Rows[0]["bmdxqdm"].ToString());

            }
        }
        protected string getPhotoPath(string ksh, string pic, string bmdxqdm)
        {
            string PicPath = "";
            if (pic == "1")
            {
                PicPath = "../../14hk/" + bmdxqdm + "/" + ksh + ".jpg";
            }
            else
            {
                PicPath = "../../images/nopic.gif";
            }
            return PicPath;
        }

    }
}