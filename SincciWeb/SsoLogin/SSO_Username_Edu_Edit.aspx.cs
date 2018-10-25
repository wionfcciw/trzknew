using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SincciKC.SsoLogin
{
    public partial class SSO_Username_Edu_Edit : BPage
    {
        private SqlDbHelper_1 helper = new SqlDbHelper_1();
        private string error = "";
        private bool bReturn = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string xqdm = Request.QueryString["xqdm"].ToString();
                Loadxq();
                Init(xqdm);
            }
        }

        private void Loadxq()
        {
            DropDownList1.DataSource = GetXqInfo();
            DropDownList1.DataTextField = "xqmc";
            DropDownList1.DataValueField = "xqdm";
            DropDownList1.DataBind();
            this.DropDownList1.Items.Insert(0, new ListItem("-请选择-", ""));
        }

        private DataTable GetXqInfo()
        {
            string sql = "select * from zk_xqdm where xqdm<>'500'";
            DataTable dt = helper.selectTab(sql, ref error, ref bReturn);
            return dt;
        }

        private void Init(string xqdm)
        {
            if(xqdm=="zyb")
            {
                xianqu.Visible = false;
                DropDownList1.Visible = false;
                TextBox2.Text = xqdm;
            }
            //即是县区账号。
            else
            {
                DropDownList1.Enabled = false;
                TextBox2.Enabled = false;
                TextBox2.Text = xqdm;
                DataTable dt= GetXqInfo();
                DropDownList1.SelectedValue = dt.Rows[0]["xqdm"].ToString();
            }
            TextBox3.Text = GetSSOInfoByXqdm(xqdm);
        }

        private string GetSSOInfoByXqdm(string xqdm)
        {
            string sql = "select organization_id from Sys_SSOUserContrast where sys_name='" + xqdm + "'";
            string schoolid = helper.ExecuteScalar(sql, ref error, ref bReturn).ToString();
            return schoolid;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //此处定义用户名
            string sys_name= "";
            sys_name = Request.QueryString["xqdm"].ToString();
            //
            string user_id = TextBox3.Text;
            string user_type="1";
            string sql = "update Sys_SSOUserContrast set organization_id='" + user_id + "',type='" + user_type + "' where sys_name='" + sys_name + "'";
            int i=helper.ExecuteNonQuery(sql,ref error,ref bReturn);
            //如果更新结果有变化，则代表保存成功
            if (i > 0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'操作成功！' ,title:'提示'});setTimeout(function(){window.parent.location.href=window.parent.location.href;window.parent.ymPrompt.close();},2000);</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'操作失败！请咨询系统管理员。' ,title:'提示'});</script>");
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}