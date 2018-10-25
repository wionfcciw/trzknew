using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using System.Data;
using System.Text;
using System.Data.Odbc;
using System.IO;
namespace SincciKC.websystem.kwgl
{
    public partial class KsTime_Manage : BPage
    {
      
        /// <summary>
        /// 考点代码控制类
        /// </summary>
        BLL_zk_kd BLL_kd = new BLL_zk_kd();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Permission();
            
               
                BindGv();
            }
        }
        /// <summary>
        /// 页面权限
        /// </summary>
        private void Permission()
        {
            //查看
            if (!new Method().CheckButtonPermission(PopedomType.A2))
            {
                Response.Write("你没有页面查看的权限！");
                Response.End();
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGv()
        {
            Model_zk_kstime model = BLL_kd.zk_kstime();
          t1.Text=  model.T1  ;
          s1.Text=  model.S1  ;
          x1.Text=  model.X1 ;
          t2.Text=  model.T2  ;
          s2.Text=  model.S2  ;
          x2.Text=  model.X2 ;
          t3.Text=  model.T3  ;
          s3.Text=  model.S3 ;
          x3.Text = model.X3;
          m1.Text = model.M1;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Model_zk_kstime model = new Model_zk_kstime();
            model.T1 = t1.Text.Trim();
            model.S1 = s1.Text.Trim();
            model.X1 = x1.Text.Trim();
            model.T2 = t2.Text.Trim();
            model.S2 = s2.Text.Trim();
            model.X2 = x2.Text.Trim();
            model.T3 = t3.Text.Trim();
            model.S3 = s3.Text.Trim();
            model.X3 = x3.Text.Trim();
            model.M1 = m1.Text.Trim();
            if (BLL_kd.zk_kstime().T1 == "")
            {
                if (BLL_kd.Insert_zk_kstime(model))
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                  "<script>ymPrompt.succeedInfo({message:'保存成功！' ,title:'操作提示'});setTimeout(function(){window.location.href=window.location.href;},1000);</script>");

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                     "<script>ymPrompt.alert({message:'保存失败！' ,title:'提示'});</script>");
        
                }
            }
            else
            {
                if (BLL_kd.Up_zk_kstime(model))
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                               "<script>ymPrompt.succeedInfo({message:'保存成功！' ,title:'操作提示'});setTimeout(function(){window.location.href=window.location.href;},1000);</script>");

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                     "<script>ymPrompt.alert({message:'保存失败！' ,title:'提示'});</script>");

                }
            }
          
           
        }

        
    }
}