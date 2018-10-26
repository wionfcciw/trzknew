using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using System.Data;
namespace SincciKC.websystem.kwgl
{
    public partial class KaoDian_Cancel : BPage
    {
        /// <summary>
        /// 考点代码控制类
        /// </summary>
        BLL_zk_kd BLL_kd = new BLL_zk_kd();

        string kddm = Convert.ToString(config.sink("kddm", config.MethodType.Get, 255, 1, config.DataType.Str));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int UserType = SincciLogin.Sessionstu().UserType;
                if (UserType == 1 || UserType == 2 || UserType == 3)
                {
                    if (kddm.Length > 0)
                    {
                        loadData(kddm);
                    }
                }
                
            }
        }

        private void loadData(string kddm)
        {

            if (BLL_kd.Canal_zk_kd(kddm))
            {
                string E_record = "取消考点编排: 考点：" + kddm + " ";

                EventMessage.EventWriteDB(1, E_record);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.succeedInfo({message:'操作成功！' ,title:'操作提示'});setTimeout(function(){window.parent.location.href=window.parent.location.href;window.parent.ymPrompt.close();},1000);</script>");

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'操作失败！' ,title:'提示'});</script>");
            }

        }

    }
}