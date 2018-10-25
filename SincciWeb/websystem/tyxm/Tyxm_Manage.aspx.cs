using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

using BLL;
using System.Data;
namespace SincciKC.websystem.tyxm
{
    public partial class Tyxm_Manage : BPage
    {
        //操作记录
        public string E_record = "";
        BLL_tyxm bll = new BLL_tyxm();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Permission();
                //绑定PageSize数据
                this.ddlPageSize.DataSource = new config().PageSizelist();
                this.ddlPageSize.DataBind();

              

                BindGv();
            }
        }
        #region "绑定数据"
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGv()
        {
           
            int RecordCount = 0;
            int pageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
          
            string strWhere = createWhere();
            DataTable tab = bll.ExecuteProcss(strWhere, pageSize, AspNetPager1.CurrentPageIndex, ref RecordCount);
            this.repDisplay.DataSource = tab;
            this.repDisplay.DataBind();

            //分页
            config.AspNetPagerCustomInfoHTML2(AspNetPager1, RecordCount, pageSize);
        }
        #endregion


        #region "查询"
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            BindGv();
        }

        /// <summary>
        /// 创建查询条件。
        /// </summary>
        /// <returns></returns>
        private string createWhere()
        {
            string whereStr = dlistxx.SelectedValue;
            if (whereStr.Length>0)
            {
                whereStr = " lxId=" + whereStr;
            }
            else
            {
                whereStr = "1=1";
            }
            return whereStr;
        }

        #endregion

        #region "选择 PageSize SelectedIndexChanged事件"
        /// <summary>
        /// 选择 PageSize 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGv();
        }
        #endregion

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
            //新增
            if (!new Method().CheckButtonPermission(PopedomType.A4))
            {
                this.btnNew.Visible = false;
            }
            //修改
            if (!new Method().CheckButtonPermission(PopedomType.A8))
            {
                this.btnEdit.Visible = false;
            } 
            //删除
            if (!new Method().CheckButtonPermission(PopedomType.A16))
            {
                this.btnDelete.Visible = false;
            } 

        }
        #endregion

        #region "删除数据"
        /// <summary>
        /// 点击删除。
        /// </summary>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            
            if (Request.Form["CheckBox1"] != null)
            {

                bool result = bll.Delete_zk_tyks(" id in (" + Request.Form["CheckBox1"].ToString() + ")");

                if (result)
                {
                    E_record = "删除：体育项目";
                    EventMessage.EventWriteDB(1, E_record);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                        "<script>ymPrompt.succeedInfo({message:'操作成功！' ,title:'操作提示'});setTimeout(function(){window.location.href=window.location.href;},1000);</script>");
                }
                else
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                        "<script>ymPrompt.alert({message:'操作失败！' ,title:'提示'});</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                 "<script>ymPrompt.alert({message:'请选择要删除的项目！' ,title:'提示'});</script>");
       
            }
            
        }
        #endregion

        #region"上下翻页"
        /// <summary>
        /// 上下翻页
        /// </summary> 
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            BindGv();
        }
        #endregion

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (Request.Form["CheckBox1"] != null)
            {
                string[] ids = Request.Form["CheckBox1"].ToString().Split(',');

                if (ids.Length == 1)
                {
                   
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>opdg('" + ids[0] + "', '修改数据') ;</script>");
            
                }
                else if (ids.Length > 1)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('不能对多个项目进行修改!');</script>");

                }

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择需要修改的项目!');</script>");

            }
        }


    }
}