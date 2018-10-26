using System;
using System.Collections.Generic;
 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using Model;
using BLL;
using System.Data;
namespace SincciWeb.system
{
    public partial class UserAddEdit : BPage
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

       int Userid = Convert.ToInt32(config.sink("Userid", config.MethodType.Get, 255, 1, config.DataType.Int));
       
      // public  int UserTypeID = Convert.ToInt32(config.sink("UserTypeID", config.MethodType.Get, 255, 1, config.DataType.Int));

       protected void Page_Load(object sender, EventArgs e)
       {
           if (!Page.IsPostBack)
           {
               int UserType = SincciLogin.Sessionstu().UserType;
               string Department = SincciLogin.Sessionstu().U_department;
               //绑定用户类型数据
               UserTypeddl(UserType);
            
      //    U_Type_Check(UserType);

               if (Userid > 0)
               {
                   binData(Userid);
                   this.txtU_loginname.Enabled = false;        
                   this.trpwd.Visible = false;
               }
              
           }
       }
        #endregion    

        #region "绑定数据"
        /// <summary>
        /// 绑定用户类型数据
        /// </summary>
       private void UserTypeddl(int UserTypeID)
        {
            QueryParam qp = new QueryParam();
            qp.Order = " order by TypeID asc ";
            qp.OrderId = " TypeID ";
            qp.Where = string.Format(" TypeID>={0} ", UserTypeID);
            int RecordCount = 0;
            this.ddlUserType.DataSource = new Method().Sys_UserTypeList(qp, out RecordCount);
            this.ddlUserType.DataTextField = "T_Name";
            this.ddlUserType.DataValueField = "TypeID";
            this.ddlUserType.DataBind();
            this.ddlUserType.Items.Insert(0, new ListItem("请选择", "0"));
        }
        /// <summary>
        /// 左边角色信息
        /// </summary> 
        private void RoleLstLeft(int UserTypeID)
        {
            QueryParam qp = new QueryParam();
            qp.Order = " order by Roleid asc ";
            qp.OrderId = " Roleid ";
            qp.Where = string.Format(" Roleid in(select A_roleid from Sys_RoleUsertType where A_UserTypeID='{0}' ) ", UserTypeID);
            int RecordCount = 0;
            this.lstLeft.DataSource = new Method().Sys_rolesList(qp, out RecordCount);
            this.lstLeft.DataTextField = "R_name";
            this.lstLeft.DataValueField = "Roleid";
            this.lstLeft.DataBind();       
        }
        /// <summary>
        /// 右边选中的角色信息
        /// </summary> 
        private void RoleLstRight(string UserName)
        {
            QueryParam qp = new QueryParam();
            qp.Order = " order by R_roleid asc ";
            qp.OrderId = " R_roleid ";
            qp.Where = string.Format(" R_UserName='{0}' ", UserName);

            int RecordCount = 0;
            DataTable dt = new Method().sys_UserRolesList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                List<sys_UserRolesTable> lst = new Method().DT2EntityList<sys_UserRolesTable>(dt);

                for (int i = 0; i < lst.Count; i++)
                {
                    sys_UserRolesTable var = lst[i];

                    Sys_rolesTable mt = new Sys_rolesTable();
                    mt = new Method().Sys_rolesDisp(var.R_RoleID);
                    if (mt.R_name.Length > 0)
                    {
                        this.lstRight.Items.Add(new ListItem(mt.R_name, mt.Roleid.ToString()));
                    }
                }
            } 
        }

        #endregion

        #region "lst 增加和删除 事件"
        /// <summary>
        /// lst 增加和删除 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
          
            bool flag = true;
            for (int i = 0; i < lstRight.Items.Count; i++)
            {
                if (lstRight.Items[i].Value == this.lstLeft.SelectedValue.ToString())
                {
                    flag = false;                   
                }
            }
            if (flag)
            {
                if (lstLeft.SelectedItem != null)
                {
                    this.lstRight.Items.Add(new ListItem(this.lstLeft.SelectedItem.ToString(), this.lstLeft.SelectedValue.ToString()));
                }
            }

        }
        protected void btnDel_Click(object sender, EventArgs e)
        {
            this.lstRight.Items.Remove(this.lstRight.SelectedItem);
            //this.lstRight.Items.Remove(this.lstRight.SelectedValue);
        }

        #endregion  
        
        #region "选择事件"

        /// <summary>
        /// 选择用户类型
        /// </summary> 
        protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //初始化
            this.ddlxqdm.Items.Clear();
            this.ddlxxdm.Items.Clear();
            this.ddlbjdm.Items.Clear();
            this.ddlxxdm.Visible = false;
            this.ddlbjdm.Visible = false;
          
            U_Xqdm(Convert.ToInt32(ddlUserType.SelectedValue));
            RoleLstLeft(Convert.ToInt32(ddlUserType.SelectedValue));
        }

        /// <summary>
        /// 选择县区
        /// </summary> 
        protected void ddlxqdm_SelectedIndexChanged(object sender, EventArgs e)
        {
            string xqdm = this.ddlxqdm.SelectedValue;
            D_Xxdm(xqdm);

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

        #region "选择用户类型时加载县区信息"
        /// <summary>
        /// 选择用户类型时加载县区信息
        /// </summary>
        /// <param name="UserType">选择用户类型的值</param>
        private void U_Xqdm(int UserType)
        {
             string Department = SincciLogin.Sessionstu().U_department;

            string fanwei = Department;
            if (Department.Length == 3 && Department.Substring(1, 2) == "00")
            {
                fanwei = Department.Substring(0, 1); 
            }
            if (fanwei.Length >= 5)
            {
                fanwei = Department.Substring(0, 4); 
            }

            string where = "";
            switch (UserType)
            {

                //系统管理员
                case 1:
                    where = " xqdm like '__0'  ";
                    break;
                //市招生办
                case 2:
                    where = " xqdm like '__0'  ";
                    break;
                //区招生办
                case 3:
                    where = " xqdm!='500' and  xqdm like '" + fanwei + "%'  ";
                    break;
                //学校用户  
                case 4:
                    where = " xqdm!='500' and xqdm like '" + fanwei + "%'";

                    
                    //显示学校下拉框
                    this.ddlxxdm.Visible = true;
                    this.ddlxxdm.Items.Insert(0, new ListItem("请选择", ""));
                    break;
                //班级用户 
                case 5:
                    where = " xqdm!='500' and xqdm like '" + fanwei + "%' ";
                   
                    //显示学校下拉框
                    this.ddlxxdm.Visible = true;
                    this.ddlxxdm.Items.Insert(0, new ListItem("请选择", ""));
                    //显示班级下拉框
                    this.ddlbjdm.Visible = true;
                    this.ddlbjdm.Items.Insert(0, new ListItem("请选择", ""));
                    break;
            }

            this.ddlxqdm.DataSource = bllxqdm.selectxqdm(where);
            this.ddlxqdm.DataTextField = "xqmc";
            this.ddlxqdm.DataValueField = "xqdm";
            this.ddlxqdm.DataBind();
            this.ddlxqdm.Items.Insert(0, new ListItem("请选择", ""));
        }
        #endregion

        #region "选择县区时加灾 学校数据"
        /// <summary>
        /// 选择县区时加灾 学校数据
        /// </summary>
        /// <param name="xqdm"></param>
        private void D_Xxdm(string xqdm)
        {
            #region  "权限条件判断"

            //string where = Department;
            //string fanwei = "";
            //if (Department.Length == 4 && Department.Substring(2, 2) == "00")
            //{
            //    fanwei = Department.Substring(0, 2);
            //}
            //if (Department.Length > 6)
            //{
            //    fanwei = Department.Substring(0, 6);
            //}
           
            //switch (UserType)
            //{ 
            //    //区招生办
            //    case 3:
            //        where = " xxdm like '" + fanwei + "%' ";
            //        break;
            //    //学校用户 
            //    case 4:
            //        where = " xxdm like '" + fanwei + "%' ";                     
            //        break;
            //    //班级用户 
            //    case 5:
            //        where = " xxdm like '" + fanwei  + "%' ";                     
            //        break;
            //}
            #endregion
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;

          //  this.ddlxxdm.DataSource = bllxxdm.Select_zk_xxdmXQ(xqdm,where);
            this.ddlxxdm.DataSource = bllxxdm.Select_zk_xxdmXQ(xqdm, Department, UserType);
            this.ddlxxdm.DataTextField = "xxmc";
            this.ddlxxdm.DataValueField = "xxdm";
            this.ddlxxdm.DataBind();
            this.ddlxxdm.Items.Insert(0, new ListItem("请选择", "")); 

        }
        #endregion

        #region " 选择学校时加载 班级信息"
        /// <summary>
        /// 选择学校时加载 班级信息
        /// </summary> 
        private void D_Bjdm(string xxdm)
        {
            #region  "权限条件判断"
            //string where = Department;
            //string fanwei = "";
            //if (Department.Length > 6)
            //    fanwei = Department.Substring(6);
            //switch (UserType)
            //{ 
            //    //班级用户 
            //    case 5:
            //        where = " bjdm ='" + fanwei + "' ";
            //        break;
            //}
            #endregion
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;

            //this.ddlbjdm.DataSource = bllbjdm.Select_zk_bjdm(xxdm,where);
            this.ddlbjdm.DataSource = bllbjdm.Select_zk_bjdm(xxdm, Department, UserType);
            this.ddlbjdm.DataTextField = "bjmc";
            this.ddlbjdm.DataValueField = "bjdm";
            this.ddlbjdm.DataBind();
        }
        #endregion 

        /// <summary>
        /// 用户类型检查权限
        /// </summary>
        /// <param name="UserType"></param>
        private void U_Type_Check(int UserType)
        { 

            switch (UserType)
            {

                //系统管理员
                case 1:                    
                    break;
                //市招生办
                case 2:                    
                    break;
                //区招生办
                case 3:
                    this.txtU_loginname.Enabled = false;                     
                    break;
                //学校用户 
                case 4:
                    this.txtU_loginname.Enabled = false;                     
                    this.ddlxxdm.Visible = true;                     
                    break;
                //班级用户 
                case 5:
                    this.txtU_loginname.Enabled = false;
                    this.ddlxqdm.Enabled = false;
                    this.ddlxxdm.Visible = true;
                    this.ddlxxdm.Enabled = false;                  
                    this.ddlbjdm.Visible = true;
                    this.ddlbjdm.Enabled = false;
                    this.lstLeft.Enabled = false;
                    this.lstRight.Enabled = false;
                    this.btnAdd.Enabled = false;
                    this.btnDel.Enabled = false;
                    this.ddlUserType.Enabled = false;
                    this.ddlTag.Enabled = false;
                    break;
                default:
                    break;
            }
        }


        #region "绑定修改数据"
        /// <summary>
        /// 绑定修改数据
        /// </summary>
        /// <param name="Userid"></param>
        private void binData(int Userid)
        {
            string xqdm = "";
            string xxdm = "";

            sys_UserTable mt = new sys_UserTable();
            mt = new Method().sys_UserDisp(Userid);
            this.txtU_loginname.Text = mt.U_LoginName;
            this.txtU_phone.Text = mt.U_phone;
            this.txtU_xm.Text = mt.U_xm;
            this.ddlUserType.SelectedValue = mt.U_usertype.ToString();
            this.ddlUserType.Enabled = false;

            this.ddlxb.SelectedValue = mt.U_xb;
            this.ddlTag.SelectedValue = mt.U_tag.ToString();
            txt6.Text = mt.Lxr;
            txt1.Text = mt.Lxdh2;
            txt2.Text = mt.Lxdh3;
            txt3.Text = mt.Txdz;
            txt4.Text = mt.Yb;
            txt5.Text = mt.Zw;
            U_Xqdm(mt.U_usertype);
          //  U_Type(mt.U_usertype);

            if (mt.U_usertype == 5)
            {
                  xqdm = mt.U_department.Substring(0, 4);
                  xxdm = mt.U_department.Substring(0, 6);

                this.ddlxxdm.Visible = true;
                this.ddlbjdm.Visible = true;
               
                D_Xxdm(xqdm);
                D_Bjdm(xxdm);
                
                this.ddlxqdm.SelectedValue = xqdm;               
                this.ddlxxdm.SelectedValue = xxdm;               
                this.ddlbjdm.SelectedValue = mt.U_department.Substring(6);

                this.ddlxqdm.Enabled = false;
                this.ddlxxdm.Enabled = false;
                this.ddlbjdm.Enabled = false;

            }
            else if (mt.U_usertype == 4)
            {
                this.ddlxxdm.Visible = true;
                xqdm ="5"+ mt.U_department.Substring(0, 2);

                D_Xxdm(xqdm);

                this.ddlxqdm.SelectedValue = xqdm;               
                this.ddlxxdm.SelectedValue = mt.U_department;

                this.ddlxqdm.Enabled = false;
                this.ddlxxdm.Enabled = false; 
            }
            else
            {
                this.ddlxqdm.SelectedValue = mt.U_department;
                this.ddlxqdm.Enabled = false; 
            }

            // this.ddlScope.SelectedValue = mt.U_department.ToString();

            RoleLstRight(mt.U_LoginName);
            RoleLstLeft(mt.U_usertype);
        }
        #endregion

        #region "保存数据"
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            sys_UserTable sat = new sys_UserTable();
            if (Userid > 0)
            {
                sat.UserID = Userid;
                sat.DB_Option_Action_ = "Update";
            }
            else
            {
                sat.U_Password = config.CheckChar(this.txtU_password.Text.Trim());
                sat.DB_Option_Action_ = "Insert";
            }

            sat.U_xm = config.CheckChar(this.txtU_xm.Text.Trim());
            sat.U_LoginName = config.CheckChar(this.txtU_loginname.Text.Trim());
            sat.U_phone = config.CheckChar(this.txtU_phone.Text.Trim());
            sat.U_xb = this.ddlxb.SelectedValue;
            sat.U_usertype = Convert.ToInt32(this.ddlUserType.SelectedValue);
            sat.U_tag = Convert.ToInt32(this.ddlTag.SelectedValue);

            sat.Lxr = config.CheckChar(this.txt6.Text.Trim());
            sat.Lxdh2 = config.CheckChar(this.txt1.Text.Trim());
            sat.Lxdh3 = config.CheckChar(this.txt2.Text.Trim());
            sat.Txdz = config.CheckChar(this.txt3.Text.Trim());
            sat.Yb = config.CheckChar(this.txt4.Text.Trim());
            sat.Zw = config.CheckChar(this.txt5.Text.Trim());
         
            if (sat.U_usertype == 5) //班级
            {
                sat.U_department = this.ddlxxdm.SelectedValue + this.ddlbjdm.SelectedValue;
            }
            else if (sat.U_usertype == 4) //学校
            {
                sat.U_department = this.ddlxxdm.SelectedValue;

            }
            else
            {
                sat.U_department = this.ddlxqdm.SelectedValue;
            }

            int fa = new Method().Sys_usersInsertUpdateDelete(sat);
            if (fa > 0)
            {
                if (sat.DB_Option_Action_ == "Insert")
                {
                    Userid = fa;
                }
                //删除用户角色数据
                new Method().sys_UserRoles_Move(config.CheckChar(this.txtU_loginname.Text.Trim()));
                //增加用户角色数据
                sys_UserRolesTable urt = new sys_UserRolesTable();
                urt.R_UserName = config.CheckChar(this.txtU_loginname.Text.Trim());
                urt.DB_Option_Action_ = "Insert";
                foreach (ListItem li in lstRight.Items)
                {
                    urt.R_RoleID = Convert.ToInt32(li.Value.Trim());
                    new Method().Sys_UserRolesInsertUpdateDelete(urt);
                }
                string E_record = "新增: 用户管理数据：" + urt.R_UserName + "";
                EventMessage.EventWriteDB(1, E_record);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.succeedInfo({message:'操作成功！' ,title:'操作提示'});setTimeout(function(){window.parent.location.href=window.parent.location.href;window.parent.ymPrompt.close();},1000);</script>");
                // Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.succeedInfo({message:'操作成功！' ,title:'操作提示'});setTimeout(function(){window.ymPrompt.close();window.parent.closeTab('" + title + "');},1000); </script>");
                // Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script> </script>");

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ymPrompt.alert({message:'操作失败！' ,title:'提示'});</script>");
            }

        }
        #endregion

    }
}