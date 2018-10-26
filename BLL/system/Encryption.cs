using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections;

namespace BLL
{
    public class Encryption
    {
        /// <summary>
        /// 加密字符串。
        /// </summary>
        /// <param name="str">需要加密的字符串。</param>
        /// <param name="iFlag">标识（0、表示返回MD5加密后的加密结果；其它、表示为自加密方式后返回的加密结果）</param>
        public   string Encryp(string str,int iFlag)
        {
            string strR = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            if (iFlag == 0)
            {
                return strR;
            }
            else
            {
                return UpsetCoding(strR);
            }
        }

        /// <summary>
        /// 打乱编码。
        /// </summary>
        /// <param name="str">需要打乱的字符串。</param>
        private string UpsetCoding(string str)
        {
            if (str.Length != 32)
            {
                return str;
            }

            char[] ch = new char[32];
            char[] ch1 = new char[16];
            char[] ch2 = new char[16];
            for (int i = 0; i < str.Length; i++)
            {
                ch[i] = str[i];
            }

            int iT = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < ch.Length; j++)
                {
                    if (j % 2 == 1)
                    {
                        ch1[j / 2] = ch[j];
                    }
                    else
                    {
                        ch2[j / 2] = ch[j];
                    }
                }
                Array.Copy(ch1, 0, ch, 0, ch1.Length);
                Array.Copy(ch2, 0, ch, ch1.Length, ch2.Length);
                for (int j = 0; j < ch.Length; j++)
                {
                    if (j % 4 == 0)
                    {
                        iT=(((int)ch[j]) + 1);
                        if (iT > 57 && iT < 65)
                        {
                            iT = 65;
                        }
                        else if (iT > 90)
                        {
                            iT = 48;
                        }
                        ch[j] = (char)iT;
                    }
                }
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ch.Length; i++)
            {
                if (i > 0 && i % 6 == 0 && i < 30)
                {
                    sb.Append('-');
                }
                sb.Append(ch[i]);
            }
            return sb.ToString();
        }
    }
}
