using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SincciKC.websystem.zklq
{
    public partial class JhglList : System.Web.UI.Page
    {
        private BLL_zk_zsjh zk = new BLL_zk_zsjh();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Permission();
                //绑定PageSize数据
                BindGv();
            }
        }

        #region "判断页面权限"
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
        #endregion

        #region "绑定数据"
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGv()
        {

             
            DataTable tab = zk.GetTableDatas();
            this.repDisplay.DataSource = tab;
            this.repDisplay.DataBind();

        }

     

        #endregion

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            bool result = zk.DeleteDataByIDS_jh(hfDelIDS.Value.Split(',').ToList());
            string E_record = "删除：考生前端招生计划:" + hfDelIDS.Value;

            if (result)
            {
                EventMessage.EventWriteDB(1, E_record);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                    "<script>ymPrompt.succeedInfo({message:'操作成功！' ,title:'操作提示'});setTimeout(function(){window.location.href=window.location.href;},1000);</script>");
            }
            else
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                    "<script>ymPrompt.alert({message:'操作失败！' ,title:'提示'});</script>");
        }
    }
}