using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
namespace SincciKC.websystem.bmgl
{
    public partial class ShowKSPic : BPage
    {
        int pic = Convert.ToInt32(config.sink("pic", config.MethodType.Get, 255, 1, config.DataType.Int));
        string bmddm = config.sink("bmddm", config.MethodType.Get, 255, 1, config.DataType.Str).ToString();
        string Bmdxqdm = config.sink("Bmdxqdm", config.MethodType.Get, 255, 1, config.DataType.Str).ToString();
        string ksh = config.sink("ksh", config.MethodType.Get, 255, 1, config.DataType.Str).ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.show();
            }
        }

        protected void show()
        {

            try
            {
                string PicPath = "";
                Model_zk_ksxxgl info = new BLL_zk_ksxxgl().zk_ksxxglDisp(ksh);
                if (info.Pic == 1)
                {
                    PicPath = Server.MapPath("//13//" + info.Bmdxqdm + "//" + ksh + ".jpg");
                   // PicPath = systemparam.picPath + info.Bmdxqdm + "/" + ksh + ".jpg";
                }
                else
                {
                    PicPath = "/images/nopic.gif";
                }

                Response.ContentType = "image/jpeg";
                Response.TransmitFile(PicPath);

            }
            catch (Exception ex)
            {
                Response.Write("找不到该相片");
            }
            finally
            {

            }

        }
    }
}