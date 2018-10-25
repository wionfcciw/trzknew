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
    public partial class SSO_Username_School_Edit : BPage
    {
        private SqlDbHelper_1 helper = new SqlDbHelper_1();
        private string error = "";
        private bool bReturn = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //通过传递来取得学校的代码
                string xxdm = Request.QueryString["xxdm"].ToString();
                DataTable dt = GetXxInfoByXxdm(xxdm);
                GetXqAndXx(dt);
                Init(xxdm, dt);
            }
        }

        private void GetXqAndXx(DataTable dt)
        {
            DropDownList1.DataSource = GetXqInfo();
            DropDownList1.DataTextField = "xqmc";
            DropDownList1.DataValueField = "xqdm";
            DropDownList1.DataBind();
            this.DropDownList1.Items.Insert(0, new ListItem("-请选择-", ""));
            string xqdm=dt.Rows[0]["xqdm"].ToString();
            DropDownList2.DataSource = GetXxInfo(xqdm);
            DropDownList2.DataTextField = "xxmc";
            DropDownList2.DataValueField = "xxdm";
            DropDownList2.DataBind();
            this.DropDownList2.Items.Insert(0, new ListItem("-请选择-", ""));
        }

        private DataTable GetXxInfo(string xqdm)
        {
            string sql = "select xxdm,xxmc from zk_xxdm where xqdm='" + xqdm + "'";
            DataTable dt = helper.selectTab(sql, ref error, ref bReturn);
            return dt;
        }

        private DataTable GetXqInfo()
        {
            string sql = "select * from zk_xqdm where xqdm<>'500'";
            DataTable dt = helper.selectTab(sql, ref error, ref bReturn);
            return dt;
        }
        private void Init( string xxdm,DataTable dt)
        {
            DropDownList1.SelectedValue = dt.Rows[0]["xqdm"].ToString();
            DropDownList2.SelectedValue = dt.Rows[0]["sys_name"].ToString();
            string schoolid = GetSSOInfoByXxdm(xxdm);
            TextBox2.Text = xxdm;
            TextBox3.Text = schoolid;
            TextBox2.Enabled = false;
        }
        private DataTable GetXxInfoByXxdm(string xxdm)
        {
            string sql = "select * from View_SSO_School_User where sys_name='" + xxdm + "'";
            DataTable dt = helper.selectTab(sql, ref error, ref bReturn);
            return dt;
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string xqdm = DropDownList1.SelectedValue;
            DropDownList2.DataSource = GetXxInfo(xqdm);
            DropDownList2.DataTextField = "xxmc";
            DropDownList2.DataValueField = "xxdm";
            DropDownList2.DataBind();
            this.DropDownList2.Items.Insert(0, new ListItem("-请选择-", ""));
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string xxdm = DropDownList2.SelectedValue;
            TextBox2.Text = xxdm;
            string schoolid=GetSSOInfoByXxdm(xxdm);
            TextBox3.Text = schoolid;
        }

        private string GetSSOInfoByXxdm(string xxdm)
        {
            string sql = "select organization_id from Sys_SSOUserContrast where sys_name='" + xxdm + "'";
            string schoolid=helper.ExecuteScalar(sql, ref error, ref bReturn).ToString();
            return schoolid;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //此处的学校代码是已经查出来的，直接会显示在文本框中
            string xxdm = TextBox2.Text;
            //需要校验平台schoolid是否是正确的，此处验证是否长度是32位。
            string schoolid = TextBox3.Text;
            //类型为学校所以为2，教育局为1。
            string type = "2";
            if (schoolid.Length != 32)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'操作失败！平台账号的长度不是32位' ,title:'提示'});</script>");
            }
            else
            {
                string sql = "update Sys_SSOUserContrast set organization_id='" + schoolid + "',type='" + type + "' where sys_name='"+xxdm+"'";
                int i = helper.ExecuteNonQuery(sql, ref error, ref bReturn);
                if (i > 0)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.succeedInfo({message:'操作成功！' ,title:'操作提示'});setTimeout(function(){window.parent.location.href=window.parent.location.href;window.parent.ymPrompt.close();},2000);</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.succeedInfo({message:'操作失败！' ,title:'提示'});</script>");
                }
            }
        }
    }
}