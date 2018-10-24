using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Reflection;

namespace Model
{
    /// <summary>
    /// 清空字段。
    /// </summary>
    public class ClearField
    {
        /// <summary>
        /// 清空SQL参数字段绑定。
        /// </summary>
        /// <param name="lisP"></param>
        public static void clearSqlParameter(List<SqlParameter> lisP)
        {
            Type t = typeof(SqlParameter);
            FieldInfo info = t.GetField("_parent", BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (SqlParameter item in lisP)
            {
                info.SetValue(item, null);
            }
        }
    }
}
