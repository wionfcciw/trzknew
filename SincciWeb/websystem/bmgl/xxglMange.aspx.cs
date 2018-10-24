using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using Model;
using Model;
namespace SincciKC.websystem.bmgl
{
    public partial class xxglMange : BPage
    {
        public Model_zk_ksxxgl info = new Model_zk_ksxxgl();
        public DataTable dt = new DataTable();
        public string ksh = "";
        public string pic = "";
        public string bmddm = "";
        public string Bmdxqdm = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                  ksh = Request.QueryString["ksh"].ToString();
                  showinfo(ksh);
            }
        }

        private void showinfo(string ksh)
        {
            info = new BLL_zk_ksxxgl().ViewDisp(ksh, SincciLogin.Sessionstu().U_department, SincciLogin.Sessionstu().UserType);


            this.SysYear.Text = info.Kaocimc;
            this.lblksh.Text = info.Ksh;
            this.lblxm.Text = info.Xm;
            this.lblsfzh.Text = info.Sfzh;

            this.lblzzmmdm.Text = info.Zzmmmc;
            this.lblxb.Text = info.Xbmc;
            this.lblmzdm.Text = info.Mzdm;

            this.lblcsrq.Text = info.Csrq;
            this.lbllxdh.Text = info.Lxdh;
            this.lblyddh.Text = info.Zkzh;

            this.lblxjh.Text = info.Bklb.ToString();
            this.lblxsbh.Text = info.Bjdm;
            this.lblbmddm.Text = info.Bmdmc;

            //this.lblxqdm.Text = info.Byzxxqmc;
            //this.lblbyzxdm.Text = info.Byzxmc;


            //this.lblbjdm.Text = info.Bjmc;
            this.lblkaoci.Text = info.Kaocimc;

            this.lblkslbdm.Text = info.Kslbmc;
            int UserType = SincciLogin.Sessionstu().UserType;
            if (UserType == 8 || UserType == 9)
            {
                BLL_LQK_Ks_Xx lqk = new BLL_LQK_Ks_Xx();
                if (lqk.selectksh(" ksh='" + info.Ksh + "'").Rows[0]["td_zt"].ToString() != "5")
                {
                    //this.lblhjdq.Visible = false;
                    //this.lblhjdz.Visible = false;

                    //this.lbljtdq.Visible = false;
                    this.lbljtdz.Visible = false;
                } 
            }
            //this.lblhjdq.Text = info.Hjdq;
            //this.lblhjdz.Text = info.Hjdz;

            //this.lbljtdq.Text = info.Jtdq;
            this.lbljtdz.Text = info.Jtdz;

            this.lblsjr.Text = info.Xm;
            this.lblyzbm.Text = info.Yzbm;
            //this.lblbz.Text = info.Bz;

            this.lblcrhkh.Text = info.Txdz;

            //this.lblbzTitle.Text = new BLL_zk_szzdybz().Disp(info.Bmdxqdm).Bzmc;

            pic = info.Pic.ToString();
            bmddm = info.Bmddm;
            Bmdxqdm = info.Bmdxqdm;
        }

    }
}