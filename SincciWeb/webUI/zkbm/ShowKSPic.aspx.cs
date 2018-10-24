using System;
using System.Collections.Generic;
 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
using BLL.system;
namespace SincciKC.webUI.zkbm
{
    public partial class ShowKSPic : SPage //wf 2018-08-16 修改，单点登录统一控制 System.Web.UI.Page
    {
        int pic = Convert.ToInt32(config.sink("pic", config.MethodType.Get, 255, 1, config.DataType.Int));
        string bmddm = config.sink("bmddm", config.MethodType.Get, 255, 1, config.DataType.Str).ToString();
        string Bmdxqdm = config.sink("Bmdxqdm", config.MethodType.Get, 255, 1, config.DataType.Str).ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["kaosheng"] != null)
                {
                    if (BLL_Ks_Session.ksLogCheck())
                    { 
                        this.show();
                    }
                    else
                    {
                        Response.Write("<script>alert('超时请重新登录！'); window.location.href='/' </script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('超时请重新登录！'); window.location.href='/' </script>");
                }
            }  
        }

        protected void show()
        {

            try
            {
                string PicPath="";
                string ksh = BLL_Ks_Session.ksSession().ksh;
                Model_zk_ksxxgl info = new BLL_zk_ksxxgl().zk_ksxxglDisp(ksh);
                if (info.Pic == 1)
                {
                    PicPath = Server.MapPath("//13//" + info.Bmdxqdm + "//" + ksh + ".jpg");
                 //   PicPath = systemparam.picPath + info.Bmdxqdm + "/" + ksh + ".jpg";
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