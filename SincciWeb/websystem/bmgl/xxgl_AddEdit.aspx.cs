using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
using Model;
using System.Data;
namespace SincciKC.websystem.bmgl
{
    public partial class xxgl_AddEdit : BPage
    {
        #region "Page_Load"

        /// <summary>
        /// 县区代码控制类
        /// </summary>
        BLL_zk_xqdm bllxqdm = new BLL_zk_xqdm();
        /// <summary>
        /// 学校代码控制类
        /// </summary>
        BLL_zk_xxdm bllxxdm = new BLL_zk_xxdm();
        /// <summary>
        /// 班级代码
        /// </summary>
        BLL_zk_bjdm bllbjdm = new BLL_zk_bjdm();
        /// <summary>
        /// 考生信息管理
        /// </summary>
        BLL_zk_ksxxgl bllks = new BLL_zk_ksxxgl();
        /// <summary>
        /// 考次代码控制类
        /// </summary>
        BLL_zk_kcdm bllkc = new BLL_zk_kcdm();
        /// <summary>
        /// 字典信息
        /// </summary>
        BLL_zk_zdxx zdxx = new BLL_zk_zdxx();

     
         

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int UserType = SincciLogin.Sessionstu().UserType;
                string Department = SincciLogin.Sessionstu().U_department;

            
                binJcsj();

              
            }
        }
        #endregion

        #region "绑定修改数据"

        private void binJcsj()
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;

            //考次
            this.ddlkc.DataSource = bllkc.Select_zk_kcdm();
            this.ddlkc.DataTextField = "kcmc";
            this.ddlkc.DataValueField = "kcdm";
            this.ddlkc.DataBind();

            //性别
            this.ddlxb.DataSource = zdxx.selectData("xb");
            this.ddlxb.DataTextField = "zlbmc";
            this.ddlxb.DataValueField = "zlbdm";
            this.ddlxb.DataBind();
            // this.ddlxb.Items.Insert(0, new ListItem("请选择", ""));
            //考生来源
            this.ddlkslb.DataSource = zdxx.selectData("KSLB");
            this.ddlkslb.DataTextField = "zlbmc";
            this.ddlkslb.DataValueField = "zlbdm";
            this.ddlkslb.DataBind();
            //  this.ddlkslbdm.Items.Insert(0, new ListItem("请选择", ""));

            //县区代码
            this.ddlxqdm.DataSource = bllxqdm.SelectXqdmbmgl(Department, 1);
            this.ddlxqdm.DataTextField = "xqmc";
            this.ddlxqdm.DataValueField = "xqdm";
            this.ddlxqdm.DataBind();
            this.ddlxqdm.Items.Insert(0, new ListItem("请选择", "0"));

            this.ddbmdxq.DataSource = bllxqdm.SelectXqdm(Department, UserType);
            this.ddbmdxq.DataTextField = "xqmc";
            this.ddbmdxq.DataValueField = "xqdm";
            this.ddbmdxq.DataBind();
            this.ddbmdxq.Items.Insert(0, new ListItem("请选择", "0"));
        } 

        /// <summary>
        /// 绑定修改数据
        /// </summary>
        /// <param name="Userid"></param>
        private void binData(string ksh)
        {
            Model_zk_ksxxgl info = bllks.zk_ksxxglDisp(ksh);
            this.ddlkc.SelectedValue = info.Kaoci;
            this.ddlxqdm.SelectedValue = info.Bmdxqdm;
            D_Xxdm(info.Bmdxqdm);
            this.ddlxxdm.SelectedValue = info.Bmddm;
            D_Bjdm(info.Bmddm);
            this.ddlbjdm.SelectedValue = info.Bjdm;
         //   this.txtksh.Text = info.Ksh;
            this.txtxj.Text = info.Xjh;
            this.txtsfzh.Text = info.Sfzh;
            this.txtxm.Text = info.Xm;
            this.ddlxb.SelectedValue = info.Xbdm.ToString();
            //this.ckbzbs.Checked = info.Sfzbs == 1 ? true : false;
            this.ddlkslb.SelectedValue = info.Kslbdm;
             
        }
        #endregion
          

        #region "选择事件"
         
        /// <summary>
        /// 选择县区
        /// </summary> 
        protected void ddlxqdm_SelectedIndexChanged(object sender, EventArgs e)
        {
            string xqdm = this.ddlxqdm.SelectedValue;
            if (xqdm != "0")
            {
                if (xqdm.Substring(xqdm.Length - 2) == "99")
                {
                    ddlxxdm.Visible = false;
                    txtxxdm.Visible = true;
                }
                else
                {
                    ddlxxdm.Visible = true;
                    txtxxdm.Visible = false;
                    txtxxdm.Text = "";
                    D_Xxdm(xqdm);
                }
            }
        }

        /// <summary>
        /// 选择学校
        /// </summary>
        protected void ddlxxdm_SelectedIndexChanged(object sender, EventArgs e)
        {
            string xxdm = this.ddlxxdm.SelectedValue;
            D_Bjdm(xxdm);
        }

        #endregion 

        #region "选择县区时加载 学校数据"
        /// <summary>
        /// 选择县区时加灾 学校数据
        /// </summary>
        /// <param name="xqdm"></param>
        private void D_Xxdm(string xqdm)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;

            this.ddlxxdm.DataSource = bllxxdm.Select_zk_xxdmXQ(xqdm, Department, UserType);
            this.ddlxxdm.DataTextField = "xxmc";
            this.ddlxxdm.DataValueField = "xxdm";
            this.ddlxxdm.DataBind();
            this.ddlxxdm.Items.Insert(0, new ListItem("请选择", "0"));

        }
        #endregion

        #region " 选择学校时加载 班级信息"
        /// <summary>
        /// 选择学校时加载 班级信息
        /// </summary> 
        private void D_Bjdm(string xxdm)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;

            this.ddlbjdm.DataSource = bllbjdm.Select_zk_bjdm(xxdm, Department, UserType);
            this.ddlbjdm.DataTextField = "bjmc";
            this.ddlbjdm.DataValueField = "bjdm";
            this.ddlbjdm.DataBind();
            this.ddlbjdm.Items.Insert(0, new ListItem("请选择", "0"));
        }
        #endregion



        private BLL_xxgl bll = new BLL_xxgl();

        #region "保存数据"
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model_zk_ksxxgl item = new Model_zk_ksxxgl();
            int lshnum = 0;
            string lsh = "";
            DataTable dt = bll.topselbmddm(ddbmdxx.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                lshnum = Convert.ToInt32(dt.Rows[0]["ksh"].ToString().Substring(dt.Rows[0]["ksh"].ToString().Length - 4));
                lshnum++;
            }
            else
            {
                lshnum++;
            }
            lsh = lshnum.ToString();
            for (int j = 0; j < 4; j++)
            {
                if (lsh.ToString().Length < 4)
                {
                    lsh = "0" + lsh;
                }
                else
                    break;
            }
            string ksh = ddlkc.SelectedValue + ddbmdxx.SelectedValue + lsh;
            while (true)
            {
                if (bll.topselbak(ksh).Rows.Count > 0)
                {
                    lshnum++;
                    lsh = lshnum.ToString();
                    for (int j = 0; j < 4; j++)
                    {
                        if (lsh.ToString().Length < 4)
                        {
                            lsh = "0" + lsh;
                        }
                        else
                            break;
                    }
                    ksh = ddlkc.SelectedValue + ddbmdxx.SelectedValue + lsh;
                }
                else
                    break;
            }

            item.Ksh = ddlkc.SelectedValue + ddbmdxx.SelectedValue + lsh;
            item.Kaoci = ddlkc.SelectedValue;
            item.Xm = config.CheckChar(txtxm.Text);
            item.Xjh = config.CheckChar(txtxj.Text);
            item.Sfzh = config.CheckChar(txtsfzh.Text);
            item.Xbdm = Convert.ToInt32(ddlxb.SelectedValue);
            if (ddlxqdm.SelectedValue.ToString().Substring(ddlxqdm.SelectedValue.Length - 2) == "99")
            {
                item.Byzxdm = ddlxqdm.SelectedValue + "99";
                item.Byzxmc = txtxxdm.Text;
            }
            else
            {
                item.Byzxdm = ddlxxdm.SelectedValue;
                item.Byzxmc = ddlxxdm.SelectedItem.Text.Split(']')[1];
            }
            if (ddlbjdm.SelectedValue != "0")
                item.Bjdm = ddlbjdm.SelectedValue;

            item.Bmdxqdm = ddbmdxq.SelectedValue;
            item.Bmddm =  ddbmdxx.SelectedValue;
            item.Kslbdm = ddlkslb.SelectedValue;
            item.Xsbh = txtxsbh.Text.Trim();
            item.Pwd = item.Ksh;
            if (checkinfo(item))
            {
                if (new BLL_zk_ksxxgl().Insert_ksxx(item))
                {
                    string E_record = "新增: 考生数据：" + item.Ksh + "";
                    EventMessage.EventWriteDB(1, E_record);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.succeedInfo({message:'操作成功！' ,title:'操作提示'});setTimeout(function(){window.parent.location.href=window.parent.location.href;window.parent.ymPrompt.close();},2000);</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'操作失败！' ,title:'提示'});</script>");
                }
            }
        }
        #endregion

        //检查数据完整性
        private bool checkinfo(Model_zk_ksxxgl item)
        {

            //判断
            string Errmsg = "";

            if (item.Xm.Length == 0)
                Errmsg += "·姓名为能为空！ <br>";
            if (item.Sfzh.Length == 0)
                Errmsg += "·证件号码不能为空！<br>";
           
            if (item.Xbdm <= 0)
                Errmsg += "·请选择性别！ <br>";

            if (item.Xjh.Length > 0)
            {
                if (item.Xjh.Length != 19)
                {
                    Errmsg += "·学籍号应为19位！ <br>";
                }
            }
              
          

            if (item.Byzxmc.Length == 0)
                Errmsg += "·毕业中学不能为空！ <br>";
           
            //if (item.Bjdm.Length == 0)
            //    Errmsg += "·请班级不能为空！ <br>";
            if (item.Bmdxqdm.Length == 0)
                Errmsg += "·请选择毕业中学县区！ <br>";

            if (item.Bmddm.Length == 0)
                Errmsg += "·请选择毕业中学学校！ <br>";
            if (item.Kslbdm.Length == 0)
                Errmsg += "·请选择考生类别！ <br>";
            if (item.Xsbh.Length > 0)
            {
                if (item.Xsbh.Length != 22)
                {
                    Errmsg += "·学生编号应为22位！ <br>";
                }
            }

              
            if (Errmsg.Length > 0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'" + Errmsg + "' ,width:300,height:260,title:'提示'});</script>");
                return false;
            }
            else
            {
                if (systemparam.sfzhTag == 1)
                {
                    //检测身份证号有没有重复。                    
                    if (new BLL_zk_ksxxgl().checksfzh(item.Sfzh, item.Ksh))
                    {

                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'身份证号已被使用!' ,width:220,height:190,title:'提示'});</script>");
                        return false;
                    }
                }

            }
            return true;

        }
        /// <summary>
        /// 毕业中学县区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddbmdxq_SelectedIndexChanged(object sender, EventArgs e)
        {
            string xqdm = this.ddbmdxq.SelectedValue;

            D_bmdXxdm(xqdm);
        }
        /// <summary>
        /// 选择县区时加灾 学校数据
        /// </summary>
        /// <param name="xqdm"></param>
        private void D_bmdXxdm(string xqdm)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;

            this.ddbmdxx.DataSource = bllxxdm.Select_zk_xxdmXQ(xqdm, Department, UserType);
            this.ddbmdxx.DataTextField = "xxmc";
            this.ddbmdxx.DataValueField = "xxdm";
            this.ddbmdxx.DataBind();
            this.ddbmdxx.Items.Insert(0, new ListItem("请选择", "0"));
        }

    }
}