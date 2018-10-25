using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;
using System.Data.SqlClient;
using System.IO;
using LinqToExcel;
using LinqToExcel.Query;

namespace BLL
{
    /// <summary>
    /// 会考报名管理
    /// </summary>
    public class BLL_Hkbm
    {

        private SqlDbHelper_1 _dbHelper = new SqlDbHelper_1();
        private string error = "";
        private bool bReturn = false;

        /// <summary>
        /// 执行分页存储过程，返回记录总数和当前页的数据。
        /// </summary>
        /// <param name="where">执行的条件</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="totalRecord">返回的记录总数</param>
        public DataTable ExecuteProcList(string where, int pageSize, int pageIndex, ref int totalRecord)
        {
            //string sql="select  from AppraisalPersonnel";

            //数据库表名
            string tabName = "View_zk_kshkxxgl";
            //要查询的字段
            string reField = "ksh,kaoci,kaocimc,xm,xjh,sfzh,bmdmc,xxqr,xqqr,bjmc,pic";
            //排序字段
            string orderStr = " ksh";
            //排序标识（0、升序；1、降序）
            int orderType = 0;
            //执行成功与失败的标识（true、表示执行成功；false、表示执行失败）
            bool bFlag = false;
            decimal dec = 0;
            DataSet dst = _dbHelper.ExecuteProc2(tabName, reField, orderStr, "", where, pageSize, pageIndex, orderType, 0, ref dec, ref totalRecord, ref bFlag);
            if (bFlag)
            {
                if (dst.Tables.Count > 0)
                {
                    return dst.Tables[0];
                }
            }
            return null;
        }
        /// <summary>
        /// 根据报名号得到DATATABLE
        /// </summary>
        /// <param name="ksh">报名号</param>
        /// <returns></returns>
        public DataTable getDataTableByKsh(string ksh)
        {
            string sql = "select * from View_zk_kshkxxgl where ksh ='" + ksh + "'";
            return _dbHelper.selectTab(sql, ref ksh, ref bReturn);
        }
        /// <summary>
        /// 删除信息备份
        /// </summary>
        /// <param name="info"> </param>
        /// <returns></returns>
        public bool insetDelBf(string name, string where)
        {
            string sql = "insert  into zk_kshkxxgl_bak  select *, '" + name + "' as 'username',GETDATE() as 'delTime' from zk_kshkxxgl where " + where;

            int a = _dbHelper.execSql_Tran(sql);
            if (a > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除考生数据 只有未确认的考生才可以删除
        /// </summary> 
        public bool Deleteks(string where)
        {
            this._dbHelper.BeginTran();
            insetDelBf(config.Get_UserName, where);
            string sql = "delete from  zk_kshkxxgl  where isnull(xxqr,0)=0 and   " + where;
            int a = _dbHelper.execSql_Tran(sql);
            try
            {
                if (a > 0)
                {
                    this._dbHelper.EndTran(true);
                    return true;
                }
                this._dbHelper.EndTran(false);
                return false;
            }
            catch (Exception)
            {
                this._dbHelper.EndTran(false);
                return false;
            }
        }



        /// <summary>
        /// 查询该毕业中学的最大流水号
        /// </summary>
        /// <param name="xqdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable topselbmddm(string bmddm)
        {

            string sctbmd = " select top 1 ksh  from zk_ksxxgl where bmddm=@bmddm order by right(ksh,4) desc; ";
            List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@bmddm", bmddm) };
            DataTable dt = _dbHelper.selectTab(sctbmd, lisP, ref   error, ref   bReturn);
            return dt;
        }

        /// <summary>

        #region Excel数据导入到SqlServer数据库方法

        /// <summary>
        /// Excel数据导入到SqlServer数据库方法
        /// </summary>
        /// <param name="excelFilePath">excel文件存放路径</param>
        /// <param name="importSqlCmd">需要用于导入数据的sql语句</param>
        /// <param name="importParams">用于导入数据的sql参数列表</param>
        /// <param name="isZL">是否增量导入</param>
        /// <returns></returns>
        public string ImportExcelData(string excelFilePath, string Department)
        {


            StringBuilder resultMsg = new StringBuilder();
            if (!File.Exists(excelFilePath))
                return "导入失败，原因是：输入的文件路径不正确或指定的文件不存在。";

            ExcelQueryFactory excelFile = new ExcelQueryFactory(excelFilePath);
            ExcelQueryable<Row> excel = excelFile.Worksheet(0);



            string sqlCmd = "insert into zk_kshkxxgl(ksh,kaoci,xjh,xm,sfzh,bmdxqdm,bmddm,bz,bjdm,xsbh) ";
            sqlCmd = sqlCmd + " Values(@ksh,@kaoci,@xjh,@xm,@sfzh,@bmdxqdm,@bmddm,@bz,@bjdm,@xsbh) ";
            string checkSql = "Select ksh From zk_kshkxxgl Where ksh=@ksh;Select sfzh From zk_kshkxxgl Where sfzh=@sfzh; select top 1 ksh  from zk_kshkxxgl where bmddm='" + Department + "'order by right(ksh,4) desc; select  ksh  from zk_kshkxxgl_bak where ksh=@ksh";
            // 
            int i = 0;
            int lost = 0;
            int finish = 0;
            foreach (var element in excel)
            {
                try
                {
                    if (element.ColumnNames.Count() != 10 && element.ColumnNames.Count() != 9)
                        return "导入失败，列数不对，您输入的是：" + element.ColumnNames.Count() + "列。";
                    string baseStr = "";

                    //空数据则跳过
                    if (string.IsNullOrEmpty(element[0].ToString().Trim()))
                        continue;
                    if (element.ColumnNames.Count() == 10)
                    {
                        #region 有报名号的
                        List<SqlParameter> param = new List<SqlParameter> {                                           
                        new SqlParameter("@kaoci",element[1].ToString()),
                        new SqlParameter("@xjh",element[2].ToString().Trim()),
                        new SqlParameter("@ksh",element[0].ToString().Trim()),
                        new SqlParameter("@sfzh",element[3].ToString().Trim()),
                        new SqlParameter("@xm",element[4].ToString().Trim()),
                        new SqlParameter("@bmdxqdm",element[5].ToString().Trim()),
                        new SqlParameter("@bmddm",element[6].ToString().Trim()),
                        new SqlParameter("@bjdm",element[7].ToString().Trim()),
                        new SqlParameter("@xsbh",element[8].ToString().Trim()),
                        new SqlParameter("@bz",config.CheckChar(element[9].ToString().Trim())),
                        };
                        DataSet tmpDataSet = _dbHelper.selectDataSet(checkSql, param, ref error, ref bReturn);
                        if (element[0].ToString().Trim().Length != 12)
                        {
                            resultMsg.Append("第" + (i + 1) + "行错误，原因：报名号【" + element[0].ToString() + "】不是12位。\r\n");
                            lost++;
                            i++;
                            continue;
                        }
                        else
                        {
                            string strc = isKsh(element[0].ToString(), element[1].ToString(), element[6].ToString().Trim());
                            if (strc != "")
                            {
                                resultMsg.Append("第" + (i + 1) + "行错误，原因：" + strc + "\r\n");
                                lost++;
                                i++;
                                continue;
                            }
                        }
                        if (element[1].ToString().Trim().Length != 2)
                        {


                            resultMsg.Append("第" + (i + 1) + "行错误，原因：考次【" + element[1].ToString() + "】不是2位。\r\n");
                            lost++;
                            i++;
                            continue;
                        }
                        else
                        {
                            string strc = isKc(element[1].ToString());
                            if (strc != "")
                            {
                                resultMsg.Append("第" + (i + 1) + "行错误，原因：" + strc + "\r\n");
                                lost++;
                                i++;
                                continue;
                            }
                        }
                        if (element[2].ToString().Trim().Length != 19)
                        {
                            resultMsg.Append("第" + (i + 1) + "行错误，原因：学籍号【" + element[2].ToString() + "】不是19位。\r\n");
                            lost++;
                            i++;
                            continue;
                        }
                        if (element[3].ToString().Trim().Length != 18)
                        {
                            resultMsg.Append("第" + (i + 1) + "行错误，原因：身份证号【" + element[3].ToString() + "】不是18位。\r\n");
                            lost++;
                            i++;
                            continue;
                        }
                        else
                        {
                            string strc = isSfzh(element[3].ToString());
                            if (strc != "")
                            {
                                resultMsg.Append("第" + (i + 1) + "行错误，原因：" + strc + "\r\n");
                                lost++;
                                i++;
                                continue;
                            }
                        }
                        if (element[4].ToString().Trim().Length == 0)
                        {
                            resultMsg.Append("第" + (i + 1) + "行错误，原因：姓名【" + element[4].ToString() + "】不能为空。\r\n");
                            lost++;
                            i++;
                            continue;
                        }
                        if (element[5].ToString().Trim().Length != 4)
                        {
                            resultMsg.Append("第" + (i + 1) + "行错误，原因：县区代码【" + element[5].ToString() + "】不是4位。\r\n");
                            lost++;
                            i++;
                            continue;
                        }
                        else
                        {
                            string strc = isXqdm(element[5].ToString());
                            if (strc != "")
                            {
                                resultMsg.Append("第" + (i + 1) + "行错误，原因：" + strc + "\r\n");
                                lost++;
                                i++;
                                continue;
                            }
                        }
                        if (element[6].ToString().Trim().Length != 6)
                        {
                            resultMsg.Append("第" + (i + 1) + "行错误，原因：毕业中学代码【" + element[6].ToString() + "】不是6位。\r\n");
                            lost++;
                            i++;
                            continue;
                        }
                        else
                        {
                            string strc = isXxdm(element[6].ToString());
                            if (strc != "")
                            {
                                resultMsg.Append("第" + (i + 1) + "行错误，原因：" + strc + "\r\n");
                                lost++;
                                i++;
                                continue;
                            }
                        }
                        if (element[7].ToString().Trim().Length != 2)
                        {
                            resultMsg.Append("第" + (i + 1) + "行错误，原因：班级【" + element[7].ToString() + "】不是2位。\r\n");
                            lost++;
                            i++;
                            continue;
                        }
                        if (element[8].ToString().Trim().Length > 0)
                        {
                            if (element[8].ToString().Trim().Length != 22)
                            {
                                resultMsg.Append("第" + (i + 1) + "行错误，原因：学生编号【" + element[8].ToString() + "】不是22位。\r\n");
                                lost++;
                                i++;
                                continue;
                            }
                        }

                        //如果报名号存在
                        if (tmpDataSet.Tables[0] != null && tmpDataSet.Tables[0].Rows.Count > 0)
                        {
                            resultMsg.Append("第" + (i + 1) + "行错误，原因：报名号【" + element[0].ToString() + "】已存在。\r\n");
                            lost++;
                            i++;
                            continue;
                        }
                        //如果报名号存在
                        if (tmpDataSet.Tables[3] != null && tmpDataSet.Tables[3].Rows.Count > 0)
                        {
                            resultMsg.Append("第" + (i + 1) + "行错误，原因：报名号【" + element[0].ToString() + "】已禁用。\r\n");
                            lost++;
                            i++;
                            continue;
                        }
                        //如果身份证号是否存在
                        if (tmpDataSet.Tables[1] != null && tmpDataSet.Tables[1].Rows.Count > 0)
                        {
                            resultMsg.Append("第" + (i + 1) + "行错误，原因：身份证号【" + element[3].ToString() + "】已存在。\r\n");
                            lost++;
                            i++;
                            continue;
                        }
                        _dbHelper.ExecuteNonQuery(sqlCmd, param, ref error, ref bReturn);
                        #endregion
                    }
                    else
                    {
                        //if (Department.Length != 6)
                        //    return "导入失败，只能由毕业中学导入该数据。";
                        #region 没有报名号的

                        List<SqlParameter> paramone = new List<SqlParameter> {                                           
                            new SqlParameter("@ksh",""),
                            new SqlParameter("@sfzh",element[2].ToString().Trim())
                         };
                        DataSet tmpDataSet = _dbHelper.selectDataSet(checkSql, paramone, ref error, ref bReturn);
                        if (element[0].ToString().Trim().Length != 2)
                        {
                            resultMsg.Append("第" + (i + 1) + "行错误，原因：考次【" + element[0].ToString() + "】不是2位。\r\n");
                            lost++;
                            i++;
                            continue;
                        }
                        else
                        {
                            string strc = isKc(element[0].ToString());
                            if (strc != "")
                            {
                                resultMsg.Append("第" + (i + 1) + "行错误，原因：" + strc + "\r\n");
                                lost++;
                                i++;
                                continue;
                            }
                        }

                        if (element[1].ToString().Trim().Length != 19)
                        {
                            resultMsg.Append("第" + (i + 1) + "行错误，原因：学籍号【" + element[1].ToString() + "】不是19位。\r\n");
                            lost++;
                            i++;
                            continue;
                        }
                        if (element[2].ToString().Trim().Length != 18)
                        {
                            resultMsg.Append("第" + (i + 1) + "行错误，原因：身份证号【" + element[2].ToString() + "】不是18位。\r\n");
                            lost++;
                            i++;
                            continue;
                        }
                        else
                        {
                            string strc = isSfzh(element[2].ToString());
                            if (strc != "")
                            {
                                resultMsg.Append("第" + (i + 1) + "行错误，原因：" + strc + "\r\n");
                                lost++;
                                i++;
                                continue;
                            }
                        }
                        if (element[3].ToString().Trim().Length == 0)
                        {
                            resultMsg.Append("第" + (i + 1) + "行错误，原因：姓名【" + element[3].ToString() + "】不能为空。\r\n");
                            lost++;
                            i++;
                            continue;
                        }
                        if (element[4].ToString().Trim().Length != 4)
                        {
                            resultMsg.Append("第" + (i + 1) + "行错误，原因：县区代码【" + element[4].ToString() + "】不是4位。\r\n");
                            lost++;
                            i++;
                            continue;
                        }
                        else
                        {
                            string strc = isXqdm(element[4].ToString());
                            if (strc != "")
                            {
                                resultMsg.Append("第" + (i + 1) + "行错误，原因：" + strc + "\r\n");
                                lost++;
                                i++;
                                continue;
                            }
                        }
                        if (element[5].ToString().Trim().Length != 6)
                        {
                            resultMsg.Append("第" + (i + 1) + "行错误，原因：毕业中学代码【" + element[5].ToString() + "】不是6位。\r\n");
                            lost++;
                            i++;
                            continue;
                        }
                        else
                        {
                            string strc = isXxdm(element[5].ToString());
                            if (strc != "")
                            {
                                resultMsg.Append("第" + (i + 1) + "行错误，原因：" + strc + "\r\n");
                                lost++;
                                i++;
                                continue;
                            }
                        }
                        if (element[6].ToString().Trim().Length != 2)
                        {
                            resultMsg.Append("第" + (i + 1) + "行错误，原因：班级【" + element[6].ToString() + "】不是2位。\r\n");
                            lost++;
                            i++;
                            continue;
                        }
                        if (element[7].ToString().Trim().Length > 0)
                        {
                            if (element[7].ToString().Trim().Length != 22)
                            {
                                resultMsg.Append("第" + (i + 1) + "行错误，原因：学生编号【" + element[7].ToString() + "】不是22位。\r\n");
                                lost++;
                                i++;
                                continue;
                            }
                        }

                        //如果身份证号是否存在
                        if (tmpDataSet.Tables[1] != null && tmpDataSet.Tables[1].Rows.Count > 0)
                        {
                            resultMsg.Append("第" + (i + 1) + "行错误，原因：身份证号【" + element[2].ToString() + "】已存在。\r\n");
                            lost++;
                            i++;
                            continue;
                        }
                        string lsh = "";
                        int lshnum = 0; //流水号
                        string sctbmd = " select top 1 ksh  from zk_kshkxxgl where bmddm='" + element[5].ToString().Trim() + "'order by right(ksh,4) desc; ";
                        DataSet dtbmd = _dbHelper.selectDataSet(sctbmd, ref error, ref bReturn);
                        //如果该毕业中学有考生存在.
                        if (dtbmd.Tables[0] != null && dtbmd.Tables[0].Rows.Count > 0)
                        {
                            lshnum = Convert.ToInt32(dtbmd.Tables[0].Rows[0]["ksh"].ToString().Substring(dtbmd.Tables[0].Rows[0]["ksh"].ToString().Length - 4, 4));
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
                        string kshstr = element[0].ToString().Trim() + element[5].ToString() + lsh;//报名号已使用则+1
                        while (true)
                        {
                            if (topselbak(kshstr).Rows.Count > 0)
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
                                kshstr = element[0].ToString().Trim() + element[5].ToString() + lsh;
                            }
                            else
                                break;
                        }
                        List<SqlParameter> paramtwo = new List<SqlParameter> {                                           
                        new SqlParameter("@kaoci",element[0].ToString()),
                        new SqlParameter("@xjh",element[1].ToString().Trim()),
                        new SqlParameter("@ksh",element[0].ToString().Trim()+element[5].ToString()+lsh),
                        new SqlParameter("@sfzh",element[2].ToString().Trim()),
                        new SqlParameter("@xm",element[3].ToString().Trim()),
                        new SqlParameter("@bmdxqdm",element[4].ToString().Trim()),
                        new SqlParameter("@bmddm",element[5].ToString().Trim()),
                        new SqlParameter("@bjdm",element[6].ToString().Trim()),
                        new SqlParameter("@xsbh",element[7].ToString().Trim()),
                        new SqlParameter("@bz",config.CheckChar(element[8].ToString().Trim())),
                        };

                        if (lsh.Length > 4)
                        {
                            error = "流水号大于4位!";
                        }
                        else
                        {
                            _dbHelper.ExecuteNonQuery(sqlCmd, paramtwo, ref error, ref bReturn);
                        }
                        #endregion
                    }


                    if (!string.IsNullOrEmpty(error))
                    {
                        resultMsg.Append(error + "\r\n");
                        lost++;
                    }
                    else
                    {
                        resultMsg.Append(baseStr + "第" + (i + 1) + "行数据导入成功。\r\n");
                        finish++;
                    }

                    resultMsg.Append("---------------------------------------------------\r\n\r\n");
                    i++;
                }
                catch (Exception)
                {

                    return "数据有误!";
                }
            }

            resultMsg.Append("共处理:" + i + "数据。 成功：" + finish + "条数据。失败：" + lost + "条数据。\r\n");

            File.Delete(excelFilePath);

            return resultMsg.ToString();


        }
        /// <summary>
        /// 权限控制
        /// </summary>
        /// <returns></returns>
        public string whereRole(string str)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string where = "";
            switch (UserType)
            {
                //系统管理员
                case 1:
                    where = "";
                    break;
                //市招生办
                case 2:
                    where = "";
                    break;
                //区招生办
                case 3:
                    if (Department == str.Trim().Substring(0, 4))
                    {
                        where = "";
                    }
                    else
                    {
                        where = "导入的数据跟你所属县区不符.";
                    }

                    break;
                //学校用户 
                case 4:
                    if (Department == str.Trim())
                    {
                        where = "";
                    }
                    else
                    {
                        where = "导入的数据跟你所属县区不符.";
                    }

                    break;
                //班级用户 
                case 5:
                    where = "*";
                    break;
                default:
                    where = "*";
                    break;
            }
            return where;
        }
        /// <summary>
        /// 判断县区
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string isXqdm(string str)
        {
            if (new BLL_zk_xqdm().selectxqdmKs("").Select(" xqdm='" + str.Trim() + "'").Length == 0)
            {
                return "县区代码【" + str + "】的代码信息,尚未定义.\r\n";
            }
            return "";
        }
        /// <summary毕业中学县区
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string isXxdm(string str)
        {
            if (new BLL_zk_xxdm().Select_zk_xxdm().Select(" xxdm='" + str.Trim() + "'").Length == 0)
            {
                return "毕业中学代码【" + str + "】的代码信息,尚未定义.\r\n";
            }
            return "";
        }
        /// <summary>
        /// 判断报名号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string isKsh(string str, string kc, string bmddm)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;

            if (new BLL_zk_kcdm().Select_zk_kcdm().Select(" kcdm='" + str.Substring(0, 2) + "'").Length == 0)
            {
                return "报名号【" + str + "】的考次信息,尚未定义.\r\n";
            }
            if (str.Substring(0, 2) != kc)
            {
                return "报名号【" + str + "】的前二位数与考次代码【" + kc + "】信息不匹配.\r\n";
            }
            if (str.Substring(2, 6) != bmddm)
            {
                return "报名号【" + str + "】与学校代码【" + bmddm + "】信息不匹配.\r\n";
            }
            else if (new BLL_zk_xxdm().Select_zk_xxdm().Select(" xxdm='" + str.Substring(2, 6) + "'").Length == 0)
            {
                return "报名号【" + str + "】的学校信息,尚未定义.\r\n";
            }
            else
            {
                switch (UserType)
                {
                    //系统管理员
                    case 1:
                        return "";
                    //市招生办
                    case 2:
                        return "";
                    //区招生办
                    case 3:
                        if (Department == str.Trim().Substring(2, 4))
                        {
                            return "";
                        }
                        else
                        {
                            return "导入的考生不属于您所属县区.";
                        }

                    //学校用户 
                    case 4:
                        if (Department == str.Trim().Substring(2, 6))
                        {
                            return "";
                        }
                        else
                        {
                            return "导入的考生不属于您所属学校.";
                        }
                    default:
                        return "*";
                }

            }



        }
        /// <summary>
        /// 判断考次
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string isKc(string str)
        {
            if (new BLL_zk_kcdm().Select_zk_kcdm().Select(" kcdm='" + str.Trim() + "'").Length == 0)
            {
                return "报名号【" + str + "】的考次信息,尚未定义.\r\n";
            }
            else if (str != systemparam.hkKc)
            {
                return "报名号【" + str + "】的考次信息不服合要求.\r\n";

            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 判断身份证号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string isSfzh(string str)
        {
            if (!config.CheckIDCard(str))
            {
                return "身份证【" + str + "】不正确.\r\n";

            }
            else
            {
                return "";
            }

        }
        /// <summary>
        /// 执行分页存储过程，返回记录总数和当前页的数据。
        /// </summary>
        /// <param name="where">执行的条件</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="totalRecord">返回的记录总数</param>
        public DataTable selBak(string where, int pageSize, int pageIndex, ref int totalRecord)
        {
            //string sql="select  from AppraisalPersonnel";

            //数据库表名
            string tabName = "zk_kshkxxgl_bak";
            //要查询的字段
            string reField = " * ";
            //排序字段
            string orderStr = " delTime ";
            //排序标识（0、升序；1、降序）
            int orderType = 1;
            //执行成功与失败的标识（true、表示执行成功；false、表示执行失败）
            bool bFlag = false;
            decimal dec = 0;
            DataSet dst = _dbHelper.ExecuteProc2(tabName, reField, orderStr, "", where, pageSize, pageIndex, orderType, 0, ref dec, ref totalRecord, ref bFlag);
            if (bFlag)
            {
                if (dst.Tables.Count > 0)
                {
                    return dst.Tables[0];
                }
            }
            return null;
        }

        #endregion

        /// <summary>
        /// 查询备份表是否存在报名号
        /// </summary>
        /// <param name="xqdm"></param>
        /// <param name="error"></param>
        /// <param name="bReturn"></param>
        /// <returns></returns>
        public DataTable topselbak(string ksh)
        {
            string sctbmd = " select  ksh  from zk_kshkxxgl_bak where ksh=@ksh";
            List<SqlParameter> lisP = new List<SqlParameter>() { new SqlParameter("@ksh", ksh) };
            DataTable dt = _dbHelper.selectTab(sctbmd, lisP, ref   error, ref   bReturn);
            return dt;
        }
        /// <summary>
        /// 重置考生状态
        /// </summary> 
        public bool ResetTag(string set, string where)
        {
            string sql = "update zk_kshkxxgl set " + set + " where " + where;
            _dbHelper.ExecuteNonQuery(sql, ref error, ref bReturn);
            if (bReturn)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 导出数据全部DBF
        /// </summary> 
        public DataSet ExportDBFALL(string where)
        {
            string sql = "select * from View_zk_kshkxxgl  where " + where + "  order by ksh asc ";

            return _dbHelper.selectDataSet(sql, ref error, ref bReturn);
        }

        /// <summary>
        /// 考生照相
        /// </summary> 
        public bool KsPhoto(string ksh)
        {

            string sql = " update zk_kshkxxgl set pic=1,picsj=GETDATE() where ksh=@ksh  ";
            List<SqlParameter> lisP = new List<SqlParameter>();
            SqlParameter Ksh = new SqlParameter("@ksh", SqlDbType.VarChar);
            Ksh.Value = ksh;
            lisP.Add(Ksh);

            string error = "";
            bool bReturn = false;
            int i = _dbHelper.ExecuteNonQuery(sql, lisP, ref   error, ref   bReturn);
            if (i == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 导出数据excel
        /// </summary> 
        public DataSet ExportEXCELKsh(string where)
        {

            string sql = "select ksh as '报名号', kaoci as '考次',xjh as '学籍号',sfzh as '身份证号',xm as '姓名',bmdxqdm as '县区代码(毕业中学所在县区)' ,bmddm as '学校(毕业中学)',bjdm as '班级',ISNULL( xsbh,'') as '学生编码', ISNULL(bz,'') as '备注' from zk_kshkxxgl  where " + where + " order by ksh asc ";

            return _dbHelper.selectDataSet(sql, ref error, ref bReturn);
        }
    }
}
