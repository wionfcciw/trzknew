using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model;
using System.Data.SqlClient;
using System.Data;

namespace BLL
{
   public class BLL_zk_szzdybz
    {
        private SqlDbHelper_1 _dbHelper = new SqlDbHelper_1();
        private string error = "";
        private bool bReturn = false;
        /// <summary>
        /// 查询定义备注字段
        /// </summary>
        /// <returns></returns>
        public DataTable SelectZydbz()
        {


            string sql = "select a.xqdm,ISNULL(b.bzmc,'') as bzmc,(a.xqmc+'['+a.xqdm+']') as xqmc from zk_xqdm a left join zk_zdybz b on a.xqdm=b.xqdm";
            DataTable dt = _dbHelper.selectTab(sql, ref   error, ref   bReturn);
            return dt;
        }
        /// <summary>
        /// 县区查询加上权限判断
        /// </summary>
        /// <param name="fanwei"></param>
        /// <param name="UserType"></param>
        /// <returns></returns>
        public DataTable SelectXqdm(string fanwei, int UserType)
        {
            string sql = "";
            string where = whereRole(fanwei, UserType);


            if (where.Length == 0)
            {
                sql = "select a.xqdm,ISNULL(b.bzmc,'') as bzmc,('['+a.xqdm+']'+a.xqmc) as xqmc from zk_xqdm a left join zk_zdybz b on a.xqdm=b.xqdm order by a.xqdm asc ";
            }
            else
            {
                sql = "select a.xqdm,ISNULL(b.bzmc,'') as bzmc,('['+a.xqdm+']'+a.xqmc) as xqmc from zk_xqdm a left join zk_zdybz b on a.xqdm=b.xqdm where " + where + " order by a.xqdm asc";
            }

            DataTable tab = _dbHelper.selectTab(sql, ref error, ref bReturn);
            return tab;

        }
        /// <summary>
        /// 管理部门权限控制
        /// </summary>
        /// <param name="fanwei">管理范围</param>
        /// <param name="UserType">用户类型</param>
        /// <returns></returns>
        public string whereRole(string fanwei, int UserType)
        {
            if (fanwei.Length > 4)
            {
                fanwei = fanwei.Substring(0, 4);
            }
            string where = "";
            switch (UserType)
            {
                //系统管理员
                case 1:
                    where = " right(a.xqdm,2) not in('99','00') ";
                    break;
                //市招生办
                case 2:
                    where = " right(a.xqdm,2) not in('99','00') ";
                    break;
                //区招生办
                case 3:
                    where = " a.xqdm = '" + fanwei + "'  ";
                    break;
                //学校用户 
                case 4:
                    where = " a.xqdm = '" + fanwei + "' ";
                    break;
                //班级用户 
                case 5:
                    where = " a.xqdm = '" + fanwei + "' ";
                    break;
                default:
                    where = " 1<>1";
                    break;
            }
            return where;
        } 
        /// <summary>
        /// 修改自定义备注
        /// </summary>
        /// <param name="item"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool update_zk_zdybz(Model_zk_zdybz item)
        {
            string sql = "update  zk_zdybz set   bzmc=@bzmc  where  xqdm=@xqdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",item.Xqdm),
 	 		 new SqlParameter("@bzmc",item.Bzmc)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="item"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public bool Insert_zk_zdybz(Model_zk_zdybz item)
        {
            string sql = "insert into zk_zdybz(xqdm,bzmc) values(@xqdm,@bzmc)";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@bzmc",item.Bzmc),
 			 new SqlParameter("@xqdm",item.Xqdm)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
        /// <summary>
        /// 通过是否存在记录
        /// </summary> 
        public DataTable Select_zk_zdybz(string xqdm)
        {
            string sql = "select * from zk_zdybz where xqdm=@xqdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",xqdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            return dt;
        }
        /// <summary>
        /// 通过xqdm 查询单个
        /// </summary> 
        public Model_zk_zdybz Disp(string xqdm)
        {
            Model_zk_zdybz info = new Model_zk_zdybz();
            string sql = "select * from zk_zdybz where xqdm=@xqdm";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@xqdm",xqdm)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
                info = _dbHelper.DT2EntityList<Model_zk_zdybz>(dt)[0];
            return info;
        }

    }
}
