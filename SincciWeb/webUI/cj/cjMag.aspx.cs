using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
using System.Data;
using BLL.system;
namespace SincciKC.webUI.cj
{
    public partial class cjMag : SPage //wf 2018-08-16 修改，单点登录统一控制 System.Web.UI.Page
    {
        public Model_zk_zkcj model = new Model_zk_zkcj();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["kaosheng"] != null)
                {
                    if (BLL_Ks_Session.ksLogCheck())
                    {
                        string ksh = BLL_Ks_Session.ksSession().ksh;
                        string xqdm = BLL_Ks_Session.ksSession().Bmdxqdm;
                        if (!new Bll_zkbm_Time().zkbm_time(xqdm, 3))
                        {
                            Response.Write("<script>alert('现在不是成绩查询时间！'); history.back(); </script>");
                        }
                        else
                        {

                            //model = new BLL_zk_kszkcj().ViewDisp(ksh);
                            BLL_zk_kszkcj bllxxgl = new BLL_zk_kszkcj();
                            DataTable dtcj = new BLL_zk_kszkcj().zk_kshkcj(ksh);
                            repDisplay.DataSource = dtcj;
                            repDisplay.DataBind();
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('请先登录！'); history.back(); </script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('请先登录！'); history.back(); </script>");
                }
            }
        }


    }
}