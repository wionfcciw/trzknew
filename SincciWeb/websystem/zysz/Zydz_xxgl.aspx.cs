using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

using System.Data;
using BLL;

namespace SincciKC.websystem.zysz
{
    public partial class Zydz_xxgl : BPage
    {
        public int page = Convert.ToInt32(config.sink("page", config.MethodType.Get, 255, 1, config.DataType.Int));
        public int pagesize = Convert.ToInt32(config.sink("pagesize", config.MethodType.Get, 255, 1, config.DataType.Int));
        /// <summary>
        /// BLL信息管理
        /// </summary>
        BLL_xxgl bllxxgl = new BLL_xxgl();
        /// <summary>
        /// 学校代码控制类
        /// </summary>
        BLL_zk_xxdm bllxxdm = new BLL_zk_xxdm();
        /// <summary>
        /// 班级代码控制类
        /// </summary>
        BLL_zk_bjdm bllbjdm = new BLL_zk_bjdm();
        /// <summary>
        /// 县区代码控制类
        /// </summary>
        BLL_zk_xqdm bllxqdm = new BLL_zk_xqdm();

        
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Permission();
                //绑定PageSize数据
                this.ddlPageSize.DataSource = new config().PageSizelist();
                this.ddlPageSize.DataBind();
                 
                if (pagesize > 0)
                {
                    this.ddlPageSize.SelectedValue = pagesize.ToString();
                }
                else
                {
                    pagesize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
                }

              

                BindGv();
                Loadsq();
            }
        }
        /// <summary>
        /// 加载市区信息
        /// </summary>
        private void Loadsq()
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            dlistSq.DataSource = bllxqdm.SelectXqdm(Department, UserType);
            dlistSq.DataTextField = "xqmc";
            dlistSq.DataValueField = "xqdm";
            dlistSq.DataBind();
            this.dlistSq.Items.Insert(0, new ListItem("-请选择-", ""));
        }
        /// <summary>
        /// 加载学校信息
        /// </summary>
        private void Loadxx()
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string xqdm = this.dlistSq.SelectedValue;
            dlistXx.DataSource = bllxxdm.Select_zk_xxdmXQ(xqdm, Department, UserType);
            dlistXx.DataTextField = "xxmc";
            dlistXx.DataValueField = "xxdm";
            dlistXx.DataBind();
            this.dlistXx.Items.Insert(0, new ListItem("-请选择-", ""));
        }
       
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGv()
        {
            if (page == 0)
                page = 1;
            int RecordCount = 0;
            string where = strwhere();
            pagesize =Convert.ToInt32( this.ddlPageSize.SelectedValue);
            this.Repeater1.DataSource = bllxxgl.ExecuteProcList(where, pagesize, AspNetPager1.CurrentPageIndex, ref RecordCount);
            this.Repeater1.DataBind();
            //分页

            config.AspNetPagerCustomInfoHTML2(AspNetPager1, RecordCount, pagesize);
        }

        #region "查询"
        protected void btnSearch_Click(object sender, EventArgs e)
        {


            BindGv();
        }
        /// <summary>
        /// 条件
        /// </summary>
        private string strwhere()
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string str = "";
            string shiqu = dlistSq.SelectedValue;
            string xuexiao = dlistXx.SelectedValue;
            string banji = dlistBj.SelectedValue;
            string KeyWord = config.CheckChar(this.txtName.Text.Trim().ToString());

            if (shiqu.Length > 0)
            {
                str = str + " bmdxqdm='" + shiqu + "' and ";
            }
            if (xuexiao.Length > 0)
            {
                str = str + " bmddm='" + xuexiao + "' and ";
            }
            if (banji.Length > 0)
            {
                str = str + " bjdm='" + banji + "' and ";
            }
            if (KeyWord.Length > 0)
            {
                str = str + " (ksh='" + KeyWord + "' or xm like '%" + KeyWord + "%' or sfzh='" + KeyWord + "') and ";
            }


            //管理部门权限
            string where = "";
            switch (UserType)
            {
                //系统管理员
                case 1:
                    where = " xxdy=1 ";
                    break;
                //市招生办
                case 2:
                    where = " xxdy=1 ";
                    break;
                //区招生办
                case 3:
                    where = " xxdy=1 and bmdxqdm = '" + Department + "'  ";
                    break;
                //学校用户 
                case 4:
                    where = " xxdy=1 and  bmddm = '" + Department + "' ";
                    break;
                //班级用户 
                case 5:
                    where = " xxdy=1 and  bjdm = '" + Department.Substring(6) + "' and bmddm='" + Department.Substring(0, 6) + "' ";
                    break;
                default:
                    where = " 1<>1";
                    break;
            }
            if (str.Length > 0)
            {
                str = str + where;
            }
            else
            {
                str = where;
            }

            return str;
        }
        #endregion

        #region "选择 PageSize SelectedIndexChanged事件"
        /// <summary>
        /// 选择 PageSize 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            pagesize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            BindGv();
           // Response.Redirect(config.GetScriptName + "?page=1&pagesize=" + this.ddlPageSize.SelectedValue.ToString() + "");
        }
        #endregion

        #region "判断页面权限"
        /// <summary>
        /// 页面权限
        /// </summary>
        private void Permission()
        {

            ////查看
            //if (!new Method().CheckButtonPermission(PopedomType.List))
            //{
            //    Response.Write("你没有页面查看的权限！");
            //    Response.End();
            //}
            ////新增
            //if (!new Method().CheckButtonPermission(PopedomType.New))
            //{
            //    this.btnNew.Visible = false;
            //}
            ////////修改
            //////if (!new Method().CheckButtonPermission(PopedomType.Edit))
            //////{
            //////    this.btnEdit.Enabled = false;
            ////}
            ////删除
            //if (!new Method().CheckButtonPermission(PopedomType.Delete))
            //{
            //    this.btnDelete.Visible = false;
            //}
            ////排序
            //if (!new Method().CheckButtonPermission(PopedomType.Orderby))
            //{
            //    this.Enabled = false;
            //}
            ////打印
            //if (!new Method().CheckButtonPermission(PopedomType.Print))
            //{
            //    this.btnPrint.Enabled = false;
            //}
            ////备用A
            //if (!new Method().CheckButtonPermission(PopedomType.A))
            //{
            //    this.btnA.Enabled = false;
            //}
            ////备用B
            //if (!new Method().CheckButtonPermission(PopedomType.B))
            //{
            //    this.btnB.Enabled = false;
            //}
        }
        #endregion

        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 市区下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dlistSq_SelectedIndexChanged(object sender, EventArgs e)
        {
            Loadxx();
        }

        #region "打印事件"
        /// <summary>
        /// 打印事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btndayin_Click(object sender, EventArgs e)
        {
            string str = "";

            for (int i = 0; i < Repeater1.Items.Count; i++)
			{
            
                CheckBox cbox = Repeater1.Items[i].FindControl("CheckBox1") as CheckBox;
                Label lblksh = Repeater1.Items[i].FindControl("lblksh") as Label;
                if (cbox.Checked)
                {
                    str = str + "'" + lblksh.Text + "',";
                }
			}
            if (str.Length > 0)
            {
                str = str.Remove(str.Length - 1);

                str = "ksh in (" + str + ")  ";  //只打印已经确认的
              //  Response.Redirect("Printxx.aspx?str=" + strch);
            }
            else
            {
            
                string shiqu = "";
                string xuexiao = "";
                string banji = "";
                //string zhuangtai = "";
                if (dlistSq.SelectedValue == "0" || dlistSq.SelectedValue == "")
                {
                    shiqu = "";
                }
                else
                {
                    shiqu = " bmdxqdm='" + dlistSq.SelectedValue + "'";
                    str = shiqu;
                }
                if (dlistXx.SelectedValue == "0" || dlistXx.SelectedValue == "")
                {
                    xuexiao = "";
                }
                else
                {
                    xuexiao = " byzxdm='" + dlistXx.SelectedValue + "'";
                    if (str == "")
                    {
                        str = str + xuexiao;
                    }
                    else
                    {
                        str = str + " and " + xuexiao;
                    }


                }
                if (dlistBj.SelectedValue == "0" || dlistBj.SelectedValue == "")
                {
                    banji = "";
                }
                else
                {
                    banji = " bjdm='" + dlistBj.SelectedValue + "'";
                    if (str == "")
                    {
                        str = str + banji;
                    }
                    else
                    {
                        str = str + " and " + banji;
                    }


                }
                //if (dlistZt.SelectedValue == "99" || dlistZt.SelectedValue == "")
                //{
                //    zhuangtai = "";
                //}
                //else
                //{
                //    zhuangtai = " ksqr=2 "; //只打印确认状态的 //+ dlistZt.SelectedValue;
                //    if (str == "")
                //    {
                //        str = zhuangtai;
                //    }
                //    else
                //    {
                //        str = str + " and " + zhuangtai;
                //    }

                //}

                if (txtName.Text.Trim() != "")
                {
                    if (str == "")
                    {
                        str = " (ksh='" + txtName.Text.Trim() + "' or xm='" + txtName.Text.Trim() + "' or sfzh='" + txtName.Text.Trim() + "') "; ;
                    }
                    else
                    {
                        str = str + " and (ksh='" + txtName.Text.Trim() + "' or xm='" + txtName.Text.Trim() + "' or sfzh='" + txtName.Text.Trim() + "') ";
                    }
                } 
            }
            if (str.Length > 0)
            {
                str = str + "  and ksqr=2 and pic=1 "; //只打已确认和已照相的考生。
            }
            if (str=="")
            {
              // Response.Write("<script >alert('请选择打印条件！');</script>");
               Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >alert('请选择打印条件！');</script>");
               // ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('请选择打印条件！');", true);
            }
            else
            {

                string err = "";
                bool ispass = false;
                bllxxgl.Insertzk_ksxxdy(config.Get_UserName, str );
                if (ispass)
                {

                    DataTable tabwhere = bllxxgl.seleczk_ksxxdy(" username='" + config.Get_UserName + "'" );
                    if (tabwhere.Rows.Count > 0)
                    {
                       // if (bllxxgl.updatezk_ksxxgl(tabwhere.Rows[0]["SelWhere"].ToString(), 1 ))
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >window.parent.addTab2('考生信息打印', '/websystem/bmgl/Printxx.aspx');</script>");
                    }
                  
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >alert('" + err + "');</script>");
                }
              //  ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "<script >window.parent.addTab2('考生信息打印', '/websystem/bmgl/Printxx.aspx?str=" + str + "');</script>", true);
            }
        }
        #endregion

        /// <summary>
        /// 学校下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dlistXx_SelectedIndexChanged(object sender, EventArgs e)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string xxdm = this.dlistXx.SelectedValue;
            dlistBj.DataSource = bllbjdm.Select_zk_bjdm(xxdm, Department, UserType);
            dlistBj.DataTextField = "bjmc";
            dlistBj.DataValueField = "bjdm";
            dlistBj.DataBind();
            this.dlistBj.Items.Insert(0, new ListItem("-请选择-", ""));
        }
        /// <summary>
        /// 上下一页
        /// </summary> 
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            BindGv();
        }
    }
}