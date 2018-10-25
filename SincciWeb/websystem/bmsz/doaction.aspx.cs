using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using BLL;
using Model;
namespace SincciKC.websystem.bmsz
{
    public partial class doaction : BPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string filename = Request.QueryString["f"].Trim();
            string result = Request.QueryString["e"].Trim();
            if (result == "99")
            {
                string ksh = filename.Substring(filename.LastIndexOf('/') + 1, filename.IndexOf('.') - (filename.LastIndexOf('/') + 1));
                string xqdm = ksh.Substring(2, 4);
                string bmddm = ksh.Substring(2, 6);
               
                Response.Write("<script> window.location.href='Photograph.aspx?ksh=" + ksh + "&xqdm=" + xqdm + "&bmddm=" + bmddm + "&pic=1'</script>");

              // Response.Redirect("Photograph.aspx?ksh=" + ksh + "&xqdm=" + xqdm + "&bmddm=" + bmddm + "");
               // Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.succeedInfo({message:'照相成功！' ,title:'操作提示'});setTimeout(function(){ window.location.href=='Photograph.aspx?ksh=" + ksh + "&xqdm=" + xqdm + "&bmddm=" + bmddm + "';window.ymPrompt.close();},1000);</script>");

              // Response.Write("<script>alert('Sussce'); window.location.href='Photograph.aspx?ksh=" + ksh + "&xqdm=" + xqdm + "&bmddm=" + bmddm + "'</script>");
            }
            else if (result == "02")
            {
                Response.Write("<script>alert('请先选择左边考生再点拍照上传！'); window.location.href='Photograph.aspx'</script>");
            }
            else
            {
                Response.Write("<script>alert('照相失败" + result + "!'); window.location.href='Photograph.aspx'</script>");

                //  Response.Write("<script>alert('Sussce'); window.location.href='Photograph.aspx'</script>");
            }
        }
    }
}