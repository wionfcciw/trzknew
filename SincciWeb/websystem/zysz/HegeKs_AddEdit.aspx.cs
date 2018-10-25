using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
 
using System.Data;
namespace SincciKC.websystem.zysz
{
    public partial class HegeKs_AddEdit : BPage
    {
        BLL_zk_hege bll = new BLL_zk_hege();
     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 
 
            }
        }
      
       
        //操作记录
        public string E_record = "";
        protected void btnEnter_Click(object sender, EventArgs e)
        {
        
            bool result = true; 

            string ksh = config.CheckChar(this.txtksh.Text.Trim().ToString());

         
            string xm = config.CheckChar(this.txtxm.Text.Trim().ToString());
            int type = Convert.ToInt32(dlistXx.SelectedValue);
         
            
            if (bll.Selzk_hege(ksh, type).Rows.Count > 0)
            {
        
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                "<script>alert('操作失败,该考生已经存在!');</script>");
                    return;
            }
            else
            {
                DataTable dt = new BLL_xxgl().seleckshgrxx(" ksh='" + ksh + "'");
                if (dt == null || dt.Rows.Count == 0)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
             "<script>alert('操作失败,不存在该考生的信息!');</script>");
                    return;
                }
                else
                {
                    if (dt.Rows[0]["xm"].ToString() != xm)
                    {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
             "<script>alert('操作失败,该考生的姓名与报名号不对应!');</script>");
                    return;
                    }
                }
                result = bll.Insert_zk_zk_hege(ksh,xm,type);
                E_record = "新增: 合格数据：" + ksh + "";
            } 

            if (result)
            {
                EventMessage.EventWriteDB(1, E_record);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                    "<script>alert('操作成功！');setTimeout(function(){window.parent.location.href=window.parent.location.href;},1000);</script>");
            }
            else
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                    "<script>alert('操作失败！');</script>");
        }
    }
}