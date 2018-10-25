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
    public partial class ZsjhEdit : System.Web.UI.Page
    {

        private  BLL_zk_zsjh zk = new BLL_zk_zsjh();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["ID"];
                if (config.CheckChar(Request.QueryString["ID"]) != "0")
                {   
                    DataRow dr = zk.GetRowUpdate(id).Rows[0];
                    this.txtxxdm.Text = dr["xxdm"].ToString();
                    this.txtJhs.Text = dr["jhs"].ToString();
                    this.txtXqdm.Text = dr["xqdm"].ToString();
                }
            
            }
        }

        protected void btnEnter_Click(object sender, EventArgs e)
        {
              bool result = true;
              string E_record =  "";

              Model_zk_zsjh model = new Model_zk_zsjh
            {
                Xqdm =txtXqdm.Text.Trim(),
                Xxdm = txtxxdm.Text.Trim(),
                Jhs = int.Parse(config.CheckChar(string.IsNullOrEmpty(txtJhs.Text.Trim()) ? "0" : txtJhs.Text.Trim())),
                Pcdm = "11"
              
            };
            if (config.CheckChar(Request.QueryString["ID"].ToString()) == "0")
            {
                //添加数据
                result = zk.Insert_zk_zsjh_xq(model);
                  E_record = "新增: 考生前台招生计划：" + config.CheckChar(Request.QueryString["ID"].ToString()) + "";

            }
            else
            {

                result = zk.UpdateJhs(Request.QueryString["ID"], this.txtJhs.Text, this.txtXqdm.Text, txtxxdm.Text);
                E_record = "修改: 考生前台招生计划：" + config.CheckChar(Request.QueryString["ID"].ToString()) + "";
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