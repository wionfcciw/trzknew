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
    public partial class SSO_Username_School_Add : System.Web.UI.Page
    {
        private SqlDbHelper_1 helper = new SqlDbHelper_1();
        private string error = "";
        private bool bReturn = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadXq();
            }
        }

        protected void LoadXq()
        {
            DropDownList1.DataSource = GetXqInfo();
            DropDownList1.DataTextField = "xqmc";
            DropDownList1.DataValueField = "xqdm";
            DropDownList1.DataBind();
            this.DropDownList1.Items.Insert(0, new ListItem("-请选择-", ""));
        }

        /// <summary>
        ///获取县区信息 铜仁市本身不取
        /// </summary>
        /// <returns></returns>
        protected DataTable GetXqInfo()
        {
            string sql = "select * from zk_xqdm where xqdm<>'500'";
            DataTable dt = helper.selectTab(sql, ref error, ref bReturn);
            return dt;
        }

        /// <summary>
        /// 县区更改完毕后，出现学校列表，第一列为请选择，这样可以保证选择之后才出现显示学校账号信息。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string xqdm = this.DropDownList1.SelectedValue;
            DropDownList2.DataSource = GetXxInfo(xqdm);
            DropDownList2.DataTextField = "xxmc";
            
            DropDownList2.DataValueField = "xxdm";
            DropDownList2.DataBind();
            this.DropDownList2.Items.Insert(0, new ListItem("-请选择-", ""));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xqdm"></param>
        /// <returns></returns>
        protected DataTable GetXxInfo(string xqdm)
        {
            string sql = "select xxdm,xxmc from zk_xxdm where xqdm='" + xqdm + "'";
            DataTable dt = helper.selectTab(sql, ref error, ref bReturn);
            return dt;
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //如果选中的内容是请选择，那么文本框中不显示任何内容，否则查找对应的学校名，将用户名显示出来。
            if(DropDownList2.SelectedIndex==0)
            {
                TextBox2.Text = "";
            }
            else
            {
                string xxdm = DropDownList2.SelectedValue;
                TextBox2.Text =xxdm;
                ChangeStateByXxdm(xxdm);
            }
        }

        private void ChangeStateByXxdm(string xxdm)
        {
            string sql = "select * from Sys_SSOUserContrast where sys_name='" + xxdm + "'";
            DataTable dt=helper.selectTab(sql, ref error, ref bReturn);
            //说明已经存在数据，则新增不允许进行，将userid查询出来并显示，而且保存按钮置成灰色。否则不做任何的处理。
            if(dt.Rows.Count>0)
            {
                TextBox2.Enabled = false;
                TextBox3.Enabled = false;
                TextBox3.Text = dt.Rows[0]["organization_id"].ToString();
                btnSave.Enabled = false;
            }
            else
            {
                TextBox2.Enabled = false;
                TextBox3.Enabled = true;
                TextBox3.Text = "";
                btnSave.Enabled = true;
            }
        }

        /// <summary>
        /// 保存，保存之前先要判断userid号是否正确，目前的方法只有userid是否满足32位，如果小于32位则证明填写有误。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //此处的学校代码是已经查出来的，直接会显示在文本框中
            string xxdm = TextBox2.Text;
            //需要校验平台schoolid是否是正确的，此处验证是否长度是32位。
            string schoolid = TextBox3.Text;
            //类型为学校所以为2，教育局为1。
            string type="2";
            if(schoolid.Length!=32)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'操作失败！平台账号的长度不是32位' ,title:'提示'});</script>");
            }
            //说明平台账户的userid是32位，此时可以通过认证，存储到数据库中。
            else
            {
                string sql = "insert into Sys_SSOUserContrast (sys_name,organization_id,type) values ('" + xxdm + "','" + schoolid + "','"+type+"')";
                int i= helper.ExecuteNonQuery(sql,ref error,ref bReturn);
                if(i>0)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.succeedInfo({message:'操作成功！' ,title:'提示'});setTimeout(function(){window.parent.location.href=window.parent.location.href;window.parent.ymPrompt.close();},2000);</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'操作失败！请咨询系统管理员。' ,title:'提示'});</script>");
                }
            }
        }
    }
}