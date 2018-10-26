using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 系统参数
    /// </summary>
    public class systemparam
    {
        /// <summary>
        ///录取通知书 邮寄地址 最多相同个数
        /// </summary>
        public static int lqtzsNumber = int.Parse(ConfigurationManager.AppSettings["lqtzsNumber"]);

        /// <summary>
        /// 联系电话 最多相同个数
        /// </summary>
        public static int lxdhNumber = int.Parse(ConfigurationManager.AppSettings["lxdhNumber"]);

        /// <summary>
        /// 是否启用检查身份证相同 1是 0否
        /// </summary>
        public static int sfzhTag = int.Parse(ConfigurationManager.AppSettings["sfzhTag"]);

        /// <summary>
        /// 图片存放路径
        /// </summary>
        public static string picPath = Convert.ToString(ConfigurationManager.AppSettings["picPath"]);
        /// <summary>
        /// 会考图片存放路径
        /// </summary>
        public static string hkPicPath = Convert.ToString(ConfigurationManager.AppSettings["hkPicPath"]);
        /// <summary>
        /// 会考图片存放路径
        /// </summary>
        public static string hkKc = Convert.ToString(ConfigurationManager.AppSettings["hkKc"]);


    }
}
