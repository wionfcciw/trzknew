using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BLL
{
    /// <summary>
    /// 验证
    /// </summary>
    public class Functions
    {
        #region 验证数字和字母或下划线
        /// <summary>
        /// 判断是否为字母
        /// </summary>
        /// <param name="wordString">要判断的字符</param>
        /// <returns></returns>
        public static bool IsLetter(string wordString)
        {
            return Regex.IsMatch(wordString, @"^[A-Za-z]+$");
        }

        /// <summary>
        /// 判断是否为字母或数字
        /// </summary>
        /// <param name="wordString">要判断的字符</param>
        /// <returns></returns>
        public static bool IsLetterOrNumber(string wordString)
        {
            return Regex.IsMatch(wordString, @"^[A-Za-z0-9]+$");
        }

        /// <summary>
        /// 判断是否为不为负数的整数
        /// </summary>
        /// <param name="wordString">要判断的字符</param>
        /// <returns></returns>
        public static bool IsNumber(string wordString)
        {
            return Regex.IsMatch(wordString, @"^[0-9]+$");
        }

        /// <summary>
        /// 判断不为小数的整数
        /// </summary>
        /// <param name="wordString">要判断的字符</param>
        /// <returns></returns>
        public static bool IsNumberSign(string wordString)
        {
            return Regex.IsMatch(wordString, @"^[+-]?[0-9]+$");
        }

        /// <summary>
        /// 判断是否为正确的价格格式
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsPrice(string wordString)
        {
            return Regex.IsMatch(wordString, @"^(([0-9]+\.[0-9]*[0-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$");
        }

        /// <summary>
        /// 验证是否Int数据类型
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsInt(string wordString)
        {
            if (IsNumberSign(wordString))//判断是否正负整数
            {
                if (IsNumber(wordString))//判断是否不为负数
                {
                    if (wordString.Length > 10)//判断是否大于正Int32的字符长度
                    {
                        return false;
                    }
                    else
                    {
                        long temp = long.Parse(wordString);
                        return temp > int.MaxValue ? false : true;
                    }
                }
                else
                {
                    if (wordString.Length > 11)
                    {
                        return false;
                    }
                    else
                    {
                        long temp = long.Parse(wordString);
                        return temp < int.MinValue ? false : true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 验证是不是正常字符 字母，数字，下划线的组合
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsNormalChar(string wordString)
        {
            return Regex.IsMatch(wordString, @"[\w\d_]+", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证字符串长度是否在指定范围内
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <param name="begin">大于等于</param>
        /// <param name="end">小于等于</param>
        /// <param name="isByte">是否以字节方式验证，默认值为true</param>
        /// <returns></returns>
        public static bool LengthInRange(string wordString, int begin, int end, bool isByte = true)
        {
            if (isByte)
            {
                int length = Regex.Replace(wordString, @"[^\x00-\xff]", "OK").Length;
                if ((length >= begin) && (length <= end))
                {
                    return true;
                };
            }
            else
            {
                if (wordString.Length >= begin && wordString.Length <= end)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region 验证常用联系方式
        /// <summary>
        /// 验证是否是邮箱
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsEmail(string wordString)
        {
            return Regex.IsMatch(wordString, @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证是否有邮箱
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <returns></returns>
        public static bool HasEmail(string wordString)
        {
            return Regex.IsMatch(wordString, @"[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)", RegexOptions.IgnoreCase);
        }


        /// <summary>
        /// 验证是否是中国电话，格式010-85849685
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsTel(string wordString)
        {
            return Regex.IsMatch(wordString, @"^\d{3,4}-?\d{6,8}$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证6个数字的邮政编码
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsPostCode(string wordString)
        {
            return Regex.IsMatch(wordString, @"^\d{6}$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证是否为一级域名
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsOneDomain(string wordString)
        {
            return Regex.IsMatch(wordString, @"^http:\/\/w{3}\.\w+\.\w+(\.\w+)?$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证二级域名
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsSecondDomain(string wordString)
        {
            return Regex.IsMatch(wordString, @"^http:\/\/\w+\.\w+\.\w+(\.\w+)?$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证是否是手机号
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsMobile(string wordString)
        {
            return Regex.IsMatch(wordString, @"^1[3|4|5|8][0-9]\d{4,8}$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证是否有手机号
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <returns></returns>
        public static bool HasMobile(string wordString)
        {
            return Regex.IsMatch(wordString, @"1[3458]\d{9}", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证是否是IP
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsIp(string wordString)
        {
            return Regex.IsMatch(wordString, @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证是否有IP
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <returns></returns>
        public static bool HasIp(string wordString)
        {
            return Regex.IsMatch(wordString, @"(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证是否是QQ
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsQQ(string wordString)
        {
            return Regex.IsMatch(wordString, @"^[1-9]\d{4,9}$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证是否有QQ
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <returns></returns>
        public static bool HasQQ(string wordString)
        {
            return Regex.IsMatch(wordString, @"[1-9]\d{4,9}", RegexOptions.IgnoreCase);
        }
        #endregion

        #region 验证中英文
        /// <summary>
        /// 是否是中文字符
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsChinese(string wordString)
        {
            return Regex.IsMatch(wordString, @"^[\u4e00-\u9fa5]+$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 是否有中文字符
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <returns></returns>
        public static bool HasChinese(string wordString)
        {
            return Regex.IsMatch(wordString, @"[\u4e00-\u9fa5]+", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证是否是英文
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsEnglish(string wordString)
        {
            return Regex.IsMatch(wordString, @"^[A-Z]|[a-z]+$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证是否有英文
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <returns></returns>
        public static bool HasEnglish(string wordString)
        {
            return Regex.IsMatch(wordString, @"[A-Z]|[a-z]+", RegexOptions.IgnoreCase);
        }
        #endregion

        #region 验证身份证
        /// <summary>
        /// 验证身份证是否有效
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsIdCard(string wordString)
        {
            if (wordString.Length == 18)
            {
                bool check = IsIdCard18(wordString);
                return check;
            }
            if (wordString.Length == 15)
            {
                bool check = IsIdCard15(wordString);
                return check;
            }
            return false;
        }

        /// <summary>
        /// 验证18位的身份证是否有效
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsIdCard18(string wordString)
        {
            long n;
            if (long.TryParse(wordString.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(wordString.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(wordString.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = wordString.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time;
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] ai = wordString.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(wi[i]) * int.Parse(ai[i].ToString());
            }
            int y;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != wordString.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准
        }

        /// <summary>
        /// 验证18位的身份证是否有效
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsIdCard15(string wordString)
        {
            long n;
            if (long.TryParse(wordString, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(wordString.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = wordString.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time;
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            return true;//符合15位身份证标准
        }

        /// <summary>
        /// 检查身份证ID和出生日期是否一致
        /// </summary>
        /// <param name="carId">身份证ID</param>
        /// <param name="borthday">出生日期</param>
        /// <returns></returns>
        public static bool CheckBorthday(string carId, DateTime borthday)
        {
            string cid;
            if (carId.Length == 15)
            {
                cid = "19" + carId.Substring(6, 2) + "-" + carId.Substring(8, 2) + "-" + carId.Substring(10, 2);
            }
            else
            {
                cid = carId.Substring(6, 4) + "-" + carId.Substring(10, 2) + "-" + carId.Substring(12, 2);
            }
            DateTime date1 = DateTime.Parse(cid);
            if (date1.Equals(borthday)) return true;
            return false;
        }

        #endregion

        #region 验证URL地址
        /// <summary>
        /// 验证是否是URL地址
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsUrl(string wordString)
        {
            return Regex.IsMatch(wordString, @"^[a-zA-z]+://(\w+(-\w+)*)(\.(\w+(-\w+)*))*(\?\S*)?$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证是否有URL地址
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <returns></returns>
        public static bool HasUrl(string wordString)
        {
            return Regex.IsMatch(wordString, @"[a-zA-z]+://(\w+(-\w+)*)(\.(\w+(-\w+)*))*(\?\S*)?", RegexOptions.IgnoreCase);
        }

        #endregion

        #region 验证日期

        /// <summary>
        /// 判断是否是正确的日期格式
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsDate(string wordString)
        {
            return Regex.IsMatch(wordString, @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
        }

        /// <summary>
        /// 判断是否是正确的时间格式
        /// </summary>
        /// <param name="wordString">要判断的字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsTime(string wordString)
        {
            return Regex.IsMatch(wordString, @"^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
        }
        #endregion
    }
}
