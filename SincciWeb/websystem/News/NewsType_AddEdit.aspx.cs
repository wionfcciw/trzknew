using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
 

namespace SincciWeb.websystem.News
{
    public partial class NewsType_AddEdit : BPage
    {
        #region "Page_Load"
        int TypeID = Convert.ToInt32(config.sink("TypeID", config.MethodType.Get, 255, 1, config.DataType.Int));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Permission();

                if (TypeID > 0)
                {
                    binData(TypeID);
                } 
            }
        }
        #endregion


        #region "判断页面权限"
        /// <summary>
        /// 页面权限
        /// </summary>
        private void Permission()
        {

            ////查看
            //if (!new Method().CheckButtonPermission(PopedomType.List))
            //{
            //    Response.Write("你没有页面查看的权限！");
            //    Response.End();
            //}
            ////新增
            //if (!new Method().CheckButtonPermission(PopedomType.New))
            //{
            //    this.btnNew.Visible = false;
            //}
            ////修改
            //if (!new Method().CheckButtonPermission(PopedomType.Edit))
            //{
            //    this.btnEdit.Enabled = false;
            //}
            //删除
            //if (!new Method().CheckButtonPermission(PopedomType.Delete))
            //{
            //    this.btnDelete.Visible = false;
            //}
            //////排序
            //if (!new Method().CheckButtonPermission(PopedomType.Orderby))
            //{
            //    this.Enabled = false;
            //}
            ////打印
            //if (!new Method().CheckButtonPermission(PopedomType.Print))
            //{
            //    this.btnPrint.Enabled = false;
            //}
            ////备用A 初始化密码
            //if (!new Method().CheckButtonPermission(PopedomType.A))
            //{
            //    this.btnReset.Visible = false;
            //}
            ////备用B
            //if (!new Method().CheckButtonPermission(PopedomType.B))
            //{
            //    this.btnB.Enabled = false;
            //}
        }
        #endregion

        #region "绑定修改数据"
        /// <summary>
        /// 绑定修改数据
        /// </summary>
        /// <param name="Moduleid"></param>
        private void binData(int Applicationid)
        {
            PE_NewsCategoryTable mt = new PE_NewsCategoryTable();
            mt = new Method().PE_NewsCategoryDisp(Applicationid);

            this.lblID.Text = mt.PCID.ToString();
            this.lblParentID.Text = mt.ParentID.ToString();
            this.lblLevel.Text = mt.Level.ToString();
            this.txtName.Text = mt.CategoryName;
        }

        #endregion

        #region "保存数据"
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            PE_NewsCategoryTable sat = new PE_NewsCategoryTable();
            if (TypeID > 0)
            {
                sat.PCID = TypeID;
                sat.DataTable_Action_ = "Update";
                sat.ParentID =Convert.ToInt32(lblParentID.Text);
                sat.Level = Convert.ToInt32(lblLevel.Text);
            }
            else
            {
                sat.DataTable_Action_ = "Insert";
            }
            sat.CategoryName = config.CheckChar(this.txtName.Text.Trim());
           
            int fa = new Method().PE_NewsCategoryInsertUpdateDelete(sat);
            if (fa > 0)
            {
                string E_record = sat.DataTable_Action_ + " ： " + sat.CategoryName;
                EventMessage.EventWriteDB(1, E_record);

                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.succeedInfo({message:'操作成功！' ,title:'操作提示'});setTimeout(function(){window.parent.location.href=window.parent.location.href;window.parent.ymPrompt.close();},2000);</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'操作失败！' ,title:'提示'});</script>");
            }
        }
        #endregion
    }
}