using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using System.Data;
namespace SincciKC.websystem.kwgl
{
    public partial class KaoDian_Show : BPage
    {
        /// <summary>
        /// 县区代码控制类
        /// </summary>
        BLL_zk_xqdm BLL_xqdm = new BLL_zk_xqdm();
        /// <summary>
        /// 考点代码控制类
        /// </summary>
        BLL_zk_kd BLL_kd = new BLL_zk_kd();

        string kddm = Convert.ToString(config.sink("kddm", config.MethodType.Get, 255, 1, config.DataType.Str));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Loadsq();
                if (kddm.Length > 0)
                {
                    loadData(kddm);
                }
            }
        } 


        /// <summary>
        /// 加载市区信息
        /// </summary>
        private void Loadsq()
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;

            this.ddlxqdm.DataSource = BLL_xqdm.SelectXqdm(Department, UserType);
            this.ddlxqdm.DataTextField = "xqmc";
            this.ddlxqdm.DataValueField = "xqdm";
            this.ddlxqdm.DataBind();

            this.ddlxqdm.Items.Insert(0, new ListItem("-请选择-", ""));
        }

        private void loadData(string kddm)
        {
            Model_zk_kd info = BLL_kd.ViewDisp(kddm);
            this.txtkddm.Text = info.Kddm;
            this.txtkddm.Enabled = false;
            this.ddlxqdm.SelectedValue = info.Xqdm;
            this.ddlxqdm.Enabled = false;
            this.txtkdmc.Text = info.Kdmc;
            this.lbllsh.Text = info.Lsh.ToString();
             
            DataTable dt = new BLL_zk_kd_xx().Select(info.Kddm);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.CheckBoxList1.Items.Insert(0, new ListItem('[' + dt.Rows[i]["xxdm"].ToString().Trim() + ']' + dt.Rows[i]["xxmc"].ToString().Trim(), dt.Rows[i]["xxdm"].ToString().Trim()));
                    CheckBoxList1.Items[0].Selected = true;
                }
            }  
        }
    }
}