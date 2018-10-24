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
    public partial class kdqkjkls_Add : BPage
    {
        private BLL_zk_kdqk bll = new BLL_zk_kdqk();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int UserType = SincciLogin.Sessionstu().UserType;
                string Department = SincciLogin.Sessionstu().U_department;
                BLL_zk_zdxx zdxx = new BLL_zk_zdxx();
                GetData();
            }
        }
     
        //加载页面默认数据
        private void GetData()
        {
            if (Request.QueryString["ID"] != "0")
            {
                DataTable dt = bll.Select_jkls(" ID=" + Request.QueryString["ID"].ToString());
                if (dt.Rows.Count == 1)
                {
                    txtksh.Text = dt.Rows[0]["sfzh"].ToString();
                    txtName.Text = dt.Rows[0]["xm"].ToString();

                    txtdw.Text = dt.Rows[0]["gzdw"].ToString();
                    pj1.Text = dt.Rows[0]["pj1"].ToString();
                    pj2.Text = dt.Rows[0]["pj2"].ToString();
                    pj3.Text = dt.Rows[0]["pj3"].ToString();
                    pj4.Text = dt.Rows[0]["pj4"].ToString();
                    pj5.Text = dt.Rows[0]["pj5"].ToString();
                    pj6.Text = dt.Rows[0]["pj6"].ToString();
                    pj7.Text = dt.Rows[0]["pj7"].ToString();
                    txtbz.Text = dt.Rows[0]["bz"].ToString();
                    txtksh.Enabled = false;
                    txtName.Enabled = false;
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
            Model_zk_kdjkls xqdmModel = new Model_zk_kdjkls
            {
                Sfzh = txtksh.Text.Trim(),
                Xm = txtName.Text.Trim(),
                Xqdm = SincciLogin.Sessionstu().UserName.Substring(1, 4),
                Kddm = kddm,
                Gzdw = txtdw.Text,
                Pj1 = pj1.Text.Trim(),
                Pj2 = pj2.Text.Trim(),
                Pj3 = pj3.Text.Trim(),
                Pj4 = pj4.Text.Trim(),
                Pj5 = pj5.Text.Trim(),
                Pj6 = pj6.Text.Trim(),
                Pj7 = pj7.Text.Trim(),
                Bz = txtbz.Text.Trim()
            };
            if (config.CheckChar(Request.QueryString["ID"].ToString()) == "0")
            {
                //添加数据
                DataTable dt = bll.Select_jkls(" sfzh='" + txtksh.Text.Trim() + "' and kddm='"+kddm+"' ");
                if (dt.Rows.Count != 0)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "",
                        "<script>alert('该监考老师已经存在！');</script>");
                    return;
                }
                result = bll.Insert_zk_kdjkls(xqdmModel);
                E_record = "新增：考点监考老师信息" + xqdmModel.Kddm + "," + xqdmModel.Sfzh;
            }
            else
            {
                //修改数据
                xqdmModel.ID = Convert.ToInt32(Request.QueryString["ID"].ToString());
                result = bll.update_zk_kdjkls(xqdmModel);
                E_record = "修改：考点监考老师信息" + xqdmModel.Kddm + "," + xqdmModel.Sfzh;
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
    }
}