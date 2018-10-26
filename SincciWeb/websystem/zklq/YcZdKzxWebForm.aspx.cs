using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

using System.Data;

namespace SincciKC
{
    /// <summary>
    /// 预测最低控制线。
    /// </summary>
    public partial class YcZdKzxWebForm : BPage
    {
        /// <summary>
        /// 页面加载时。
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int UserType = SincciLogin.Sessionstu().UserType;
                if (UserType != 1)
                {
                    Response.Write("<script language='javascript'>window.parent.location.href='/ht/login.aspx';</script>");
                    return;
                }
                loadXqData();
            }
        }

        /// <summary>
        /// 加载县区信息。
        /// </summary>
        private void loadXqData()
        {
            BLL_Yc_XqYcZdFsx yc = new BLL_Yc_XqYcZdFsx();
            DataTable tab = yc.selectYcXqInfo();

            //绑定合并县区。
            this.cbl_XqXx.DataSource = tab;
            this.cbl_XqXx.DataTextField = "yc_XqMc";
            this.cbl_XqXx.DataValueField = "yc_Xqdm";
            this.cbl_XqXx.DataBind();

            if (tab != null)
            {
                foreach (DataRow row in tab.Rows)
                {
                    if (row["Sf_Hb"].ToString().Trim() == "True")
                    {
                        for (int i = 0; i < this.cbl_XqXx.Items.Count; i++)
                        {
                            if (row["yc_Xqdm"].ToString() == this.cbl_XqXx.Items[i].Value)
                            {
                                this.cbl_XqXx.Items[i].Selected = true;
                            }
                        }
                    }
                }
                tab.Columns.Add("serial");
                tab.Columns.Add("yc_Bl_1");
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    tab.Rows[i]["serial"] = (i + 1).ToString();

                }
                //this.tr_Id.Visible = false;
            }
            //绑定县区预测比例、计划招生数量
            this.gvXqYcBlSz.DataSource = tab;
            this.gvXqYcBlSz.DataBind();

            //绑定预测分数线。
            this.gvZdKzs.DataSource = tab;
            this.gvZdKzs.DataBind();
        }

        /// <summary>
        /// 加载编辑数据。
        /// </summary>
        private void loadEditorData()
        {
            BLL_Yc_XqYcZdFsx yc = new BLL_Yc_XqYcZdFsx();
            DataTable tab;
            if (Session["Yc_ZdKzx"] == null)
            {
                tab = yc.selectYcXqInfo();
                if (tab != null)
                {
                    tab.Columns.Add("serial");
                    tab.Columns.Add("yc_Bl_1");
                    for (int i = 0; i < tab.Rows.Count; i++)
                    {
                        tab.Rows[i]["serial"] = (i + 1).ToString();
                    }
                }
                //绑定预测分数线。
                this.gvZdKzs.DataSource = tab;
                this.gvZdKzs.DataBind();
                Session["Yc_ZdKzx"] = tab;
            }
            else
            {
                tab = Session["Yc_ZdKzx"] as DataTable;
            }
            //绑定县区预测比例、计划招生数量
            this.gvXqYcBlSz.DataSource = tab;
            this.gvXqYcBlSz.DataBind();
        }

        /// <summary>
        /// 当在绑定县区设置预测比例的行数据后。
        /// </summary>
        protected void gvXqYcBlSz_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        /// <summary>
        /// 点击开始计算。
        /// </summary>
        protected void btnStartCount_Click(object sender, EventArgs e)
        {
            /*
             * 预测各县区最低分数控制线的方式说明（预测方式为按计划预测）：
             * 1、查询各县区的招生计划数、预测比例。
             * 2、判断各县区是否合并预测。
             * 3、查询考生志愿信息表、考生中考成绩表、小批次信息表，条件为当前县区的考生报考了普高批次的考生，然后按分数从高到低进行排序。
             * 4、比较招生计划数与考生的数量大小，如果考生的数量小于招生计划数，则最所有考生的最低分为最低分数控制线；否则按招生计划数的索引位置获取最低分数控制线。
             * 5、将当前县区最低分数控制线写入数据库中。
             */
            string val = this.hf_HbXqdm.Value.Trim();
            BLL_Yc_XqYcZdFsx zk = new BLL_Yc_XqYcZdFsx();
            int i = zk.StartCountZdKzx(val);
            switch (i)
            {
                case -1:
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'请设置预测分数线后！' ,title:'操作提示'}); setTimeout(function(){ ymPrompt.close();},2000); </script>");
                    break;
                case -2:
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'合并预测县区的预测比例不相同，请修改后再预测！' ,title:'操作提示'}); setTimeout(function(){ ymPrompt.close();},2000); </script>");
                    break;
                case 1: Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'预测完成！' ,title:'操作提示'}); setTimeout(function(){ ymPrompt.close();},2000); </script>");
                    break;
                case 2: Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'数据库操作错误！' ,title:'操作提示'}); setTimeout(function(){ ymPrompt.close();},2000); </script>");
                    break;
            }
            Session["Yc_ZdKzx"] = null;
            loadEditorData(); 
        }

        /// <summary>
        /// 编辑。
        /// </summary>
        protected void gvXqYcBlSz_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvXqYcBlSz.EditIndex = e.NewEditIndex;
            loadEditorData(); 
        }

        /// <summary>
        /// 取消
        /// </summary>
        protected void gvXqYcBlSz_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {        
            this.gvXqYcBlSz.EditIndex = -1;
            loadEditorData(); 
        }

        /// <summary>
        /// 修改。
        /// </summary>
        protected void gvXqYcBlSz_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string[] strs = this.hf_HbXqdm.Value.Trim().Split(',');
            //第一列的数据
            string xqdm = ((Label)(this.gvXqYcBlSz.Rows[e.RowIndex].Cells[1].Controls[1])).Text.Trim();
            //编辑第二列   
            string strJhs = ((TextBox)(this.gvXqYcBlSz.Rows[e.RowIndex].Cells[2].Controls[0])).Text.Trim();  
            //编辑第三列   
            string strBl = ((TextBox)(this.gvXqYcBlSz.Rows[e.RowIndex].Cells[3].Controls[0])).Text.Trim();
            int bl = 0;
            int jhs = 0;
            if (!int.TryParse(strJhs, out jhs))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'计划数必须是数字！' ,title:'操作提示'}); setTimeout(function(){ ymPrompt.close();},2000); </script>");
                return;
            }
            if (!int.TryParse(strBl, out bl))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'预测比例必须是数字！' ,title:'操作提示'}); setTimeout(function(){ ymPrompt.close();},2000); </script>");
                return;
            }
            bool bflag = false;
            foreach (string str in strs)
            {
                if (xqdm == str.Trim())
                {
                    bflag = true;
                    break;
                }
            }
            string hbxqdm = this.hf_HbXqdm.Value.Trim();
            if (bflag)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'修改当前县区预测比例时，会将关联合并预测县区同时修改！' ,title:'操作提示'}); setTimeout(function(){ ymPrompt.close();},2000); </script>");
            }

            this.gvXqYcBlSz.EditIndex = -1;

            BLL_Yc_XqYcZdFsx yc = new BLL_Yc_XqYcZdFsx();
            if (yc.updateYcInfo(xqdm, bl, jhs, hbxqdm, bflag))
            {
                Session["Yc_ZdKzx"] = null;
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'更新成功！' ,title:'操作提示'}); setTimeout(function(){ ymPrompt.close();},1000); </script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'更新失败！' ,title:'操作提示'}); setTimeout(function(){ ymPrompt.close();},2000); </script>");
            }
            loadEditorData(); 
        }
    }
}