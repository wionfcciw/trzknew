using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SincciKC
{
    public partial class WebSel : System.Web.UI.Page
    {
        /// <summary>
        /// 志愿定制BLL
        /// </summary>
        private BLL_zk_zydz bll = new BLL_zk_zydz();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Timer1.Enabled = true;
                Timer1.Interval = Convert.ToInt32(dlistkslx.SelectedValue);
               
                BindGv();
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGv()
        {
            
            DataTable dpcdt = new DataTable();
            dpcdt = bll.Select_All_DpcIsPass("500");
            //判断大批次的填报时间
            for (int i = 0; i < dpcdt.Rows.Count; i++)
            {
                if (!(config.DateTimeCompare(DateTime.Now, Convert.ToDateTime(dpcdt.Rows[i]["stime"])) > 0 && config.DateTimeCompare(DateTime.Now, Convert.ToDateTime(dpcdt.Rows[i]["etime"])) < 0))
                {
                    dpcdt.Rows.Remove(dpcdt.Rows[i]);
                    i--;
                }
            }
            if (dpcdt.Rows.Count == 0)
            {
                divs.Visible = true;
                divdata.Visible = false;
                return;
            }
            else
            {
                divs.Visible = false;
            }
            string pcdm = dpcdt.Rows[0]["dpcDm"].ToString() + "1";
            List<string> pclist = new List<string>() { "11", "21", "31", "41" };//第二批次
            List<string> pclist2 = new List<string>() { "51", "61", "71", "81", "91" };//第二批次
            string pc = "";
            if (pclist.Contains(pcdm))
                pc = "11";
            if (pclist2.Contains(pcdm))
                pc = "21";
            if (pcdm == "01")
            {
                pc = "01";
            }
            DataTable tab = bll.Select_sel(pc,pcdm);
            if (tab.Rows.Count > 0)
            {
                tab.Columns.Add("xxmc");
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    tab.Rows[i]["xxmc"] = bll.Select_selxxmc(tab.Rows[i]["xxdm"].ToString()).Rows[0]["zsxxmc"].ToString();
                }
            }
            this.Repeater1.DataSource = tab;
            this.Repeater1.DataBind();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
          
            BindGv();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Timer1.Interval = Convert.ToInt32(dlistkslx.SelectedValue);
           
        }

       


    }
}