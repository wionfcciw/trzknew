using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using Model;
using System.Data;
namespace SincciKC.websystem.kwgl
{
    public partial class kdqk_Add : BPage
    {
        private BLL_zk_kdqk bll = new BLL_zk_kdqk();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int UserType = SincciLogin.Sessionstu().UserType;
                string Department = SincciLogin.Sessionstu().U_department;
                BLL_zk_zdxx zdxx = new BLL_zk_zdxx();
                this.listleib.DataSource = zdxx.selectData("kdqklb");
                this.listleib.DataTextField = "zlbmc";
                this.listleib.DataValueField = "zlbdm";
                this.listleib.DataBind();
                this.listleib.Items.Insert(0, new ListItem("请选择", ""));
                this.listkemu.DataSource = zdxx.selectData("kskm");
                this.listkemu.DataTextField = "zlbmc";
                this.listkemu.DataValueField = "zlbdm";
                this.listkemu.DataBind();
                this.listkemu.Items.Insert(0, new ListItem("请选择", ""));
                GetData();

            
            }
        }
        /// <summary>
        /// 返回科目代码
        /// </summary>
        /// <returns></returns>
        private string rekmdm(string kmmc)
        {

            if (kmmc.IndexOf("语文") != -1)
            {
                return "1";
            }
            else if (kmmc.IndexOf("数学") != -1)
            {
                return "2";
            }
            else if (kmmc.IndexOf("英语") != -1)
            {
                return "3";
            }
            else if (kmmc.IndexOf("思品") != -1)
            {
                return "4";
            }
            else if (kmmc.IndexOf("物理") != -1)
            {
                return "5";
            }
            else
            {
                return "";
            }
        }
        //加载页面默认数据
        private void GetData()
        {
            if (Request.QueryString["ID"] != "0")
            {
                DataTable dt = bll.Select_ks("ID=" + Request.QueryString["ID"].ToString());
                if (dt.Rows.Count == 1)
                {
                    txtksh.Text = dt.Rows[0]["zkzh"].ToString();
                    txtName.Text = dt.Rows[0]["xm"].ToString();
                    listkemu.SelectedValue = dt.Rows[0]["kmdm"].ToString();
                    listleib.SelectedValue = dt.Rows[0]["kcqkdm"].ToString();
                    txtqk.Text = dt.Rows[0]["kcqk"].ToString();
                    txtbz.Text = dt.Rows[0]["bz"].ToString();
                    txtksh.Enabled = false;
                    txtName.Enabled = false;
                }
            }
            else
            {
                DateTime time = Convert.ToDateTime(DateTime.Now.GetDateTimeFormats('M')[0].ToString());//
                DataTable dt = bll.Select_zk_kstime();
                DateTime t1 = Convert.ToDateTime(dt.Rows[0]["t1"].ToString().Trim());
                DateTime t2 = Convert.ToDateTime(dt.Rows[0]["t2"].ToString().Trim());
                DateTime t3 = Convert.ToDateTime(dt.Rows[0]["t3"].ToString().Trim());
                int a = DateTime.Compare(time, t1);
                int b = DateTime.Compare(time, t2);
                int c = DateTime.Compare(time, t3);
                string s1 = "";
                string x1 = "";
                if (a == 0)
                {
                    s1 = dt.Rows[0]["s1"].ToString().Trim();
                    x1 = dt.Rows[0]["x1"].ToString().Trim();
                }
                else if (b == 0)
                {
                    s1 = dt.Rows[0]["s2"].ToString().Trim();
                    x1 = dt.Rows[0]["x2"].ToString().Trim();
                }
                else if (c == 0)
                {
                    s1 = dt.Rows[0]["s3"].ToString().Trim();
                    x1 = dt.Rows[0]["x3"].ToString().Trim();
                }
                else
                {

                }

                if (s1 != "")
                {
                    string j1 = s1.Split(' ')[0].Trim();
                    string k1 = s1.Split(' ')[1].Trim();
                    DateTime j1a = Convert.ToDateTime(j1.Split('-')[0]);
                    DateTime j1b = Convert.ToDateTime(j1.Split('-')[1]);
                    if (DateTime.Compare(DateTime.Now, j1a) == 1)
                    {
                        TimeSpan ts = DateTime.Now - j1b;
                        if (ts.Hours < 2)
                        {
                            string rekm = rekmdm(k1);
                            listkemu.SelectedValue = rekm;
                        }
                    }
                }
                if (x1 != "")
                {
                    string j1 = x1.Split(' ')[0].Trim();
                    string k1 = x1.Split(' ')[1].Trim();
                    DateTime j1a = Convert.ToDateTime(j1.Split('-')[0]);
                    DateTime j1b = Convert.ToDateTime(j1.Split('-')[1]);
                    if (DateTime.Compare(DateTime.Now, j1a) == 1)
                    {
                        TimeSpan ts = DateTime.Now - j1b;
                        if (ts.Hours < 2)
                        {
                            string rekm = rekmdm(k1);
                            listkemu.SelectedValue = rekm;
                        }
                    }
                }
            }
        }
        //操作记录
        public string E_record = "";
        protected void btnEnter_Click(object sender, EventArgs e)
        {
           


            string Department = SincciLogin.Sessionstu().UserName;
            string kddm = Department.Substring(3);
            bool result = true;
            //DataTable dt = bll.Select_kdks(" zkzh='" + txtksh.Text.Trim() + "' and kddm=" + kddm);
            //if (dt.Rows.Count == 0)
            //{
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
            //        "<script>alert('您所属考点不存在该考生！');</script>");
            //    return;
            //}
            //else
            //{
            //    //if (dt.Rows[0]["xm"].ToString() != txtName.Text.Trim())
            //    //{
            //    //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
            //    //   "<script>alert('该考试证号与姓名不一样！');</script>");
            //    //    return;
            //    //}
            //    txtName.Text = dt.Rows[0]["xm"].ToString();
            //}
            if (txtName.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
               "<script>alert('查询不到该考点考试证号！');</script>");
                return;
            }

            Model_zk_kdqk xqdmModel = new Model_zk_kdqk
            {
                Zkzh = txtksh.Text.Trim(),
                Kddm = kddm,
                Kmdm = listkemu.SelectedItem.Value.Trim(),
                Kcqkdm = listleib.SelectedItem.Value.Trim(),
                Kcqk = txtqk.Text.Trim(),
                Bz = txtbz.Text.Trim()
            };
            if (config.CheckChar(Request.QueryString["ID"].ToString()) == "0")
            {
                //添加数据
                result = bll.Insert_zk_kdqk(xqdmModel);
                E_record = "新增：考点情况数据" + xqdmModel.Kddm + "," + xqdmModel.Zkzh;
            }
            else
            {
                //修改数据
                xqdmModel.ID =Convert.ToInt32( Request.QueryString["ID"].ToString());
                result = bll.update_zk_kdqk(xqdmModel);
                E_record = "修改：考点情况数据" + xqdmModel.Kddm + "," + xqdmModel.Zkzh;
            }

            if (result)
            {
                EventMessage.EventWriteDB(1, E_record);

                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                    "<script>alert('操作成功！');setTimeout(function(){window.parent.location.href=window.parent.location.href;},1000);</script>");

            }
            else
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                    "<script>alert('操作失败！');</script>");
        }

        protected void txtksh_TextChanged(object sender, EventArgs e)
        {
            string Department = SincciLogin.Sessionstu().UserName;
            string kddm = Department.Substring(3);
            DataTable dt = bll.Select_kdks(" zkzh='" + txtksh.Text.Trim() + "' and kddm=" + kddm);
            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    txtName.Text = dt.Rows[0]["xm"].ToString();
                }
            }
        }
    }
}