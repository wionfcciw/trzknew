﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;

namespace SincciKC.websystem.jcsj
{
    public partial class Kaoci_Manage : BPage
    {
        //操作记录
        public string E_record = "";
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
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGv()
        {

            int RecordCount = 0;
            int pageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
           
            string strWhere = createWhere();           
            this.repDisplay.DataSource = new BLL_zk_kcdm().ExecuteProc(strWhere, pageSize, AspNetPager1.CurrentPageIndex, ref RecordCount);
            this.repDisplay.DataBind();

            //分页
            config.AspNetPagerCustomInfoHTML2(AspNetPager1, RecordCount, pageSize);
        }
      

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
            return " kcdm Like '%" + config.CheckChar(txtName.Text.Trim()) + "%' ";
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

        /// <summary>
        /// 点击删除。
        /// </summary>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            BLL_zk_kcdm bll = new BLL_zk_kcdm();
            if (Request.Form["CheckBox1"] != null)
            {
                bool result = bll.DeleteDataByKcdm(Request.Form["CheckBox1"].ToString().Split(',').ToList());

                if (result)
                {
                    E_record = "删除：考次数据";
                    EventMessage.EventWriteDB(1, E_record);

                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                        "<script>ymPrompt.succeedInfo({message:'操作成功！' ,title:'操作提示'});setTimeout(function(){window.location.href=window.location.href;},1000);</script>");
                }
                else
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                        "<script>ymPrompt.alert({message:'操作失败！' ,title:'提示'});</script>");
            }
        } 

        /// <summary>
        /// 上下一页
        /// </summary> 
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            BindGv();
        }

        

    }
}