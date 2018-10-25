using System;
using System.Collections.Generic;
 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
namespace SincciKC.system
{
    public partial class main : BPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                new OnlineDataBase().OnlineAccess(SincciLogin.Sessionstu().UserName);
            }
        }
    }
}