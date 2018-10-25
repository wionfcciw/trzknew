using System;
using System.Collections.Generic;
 
using System.Text;

namespace BLL
{
    public class NumberToEnglish
    {
        /// <summary>
        /// 数字转换为字母
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        public static string NumberToEnglishChar(int Num)
        {
            string str = string.Empty;
            string strReturn = string.Empty;

            if (Num == 0) return "";

            float f = (Num % 26 == 0) ? 26 : Num % 26;

            switch ((int)f)
            {
                case 1:
                    str = "A";
                    break;
                case 2:
                    str = "B";
                    break;
                case 3:
                    str = "C";
                    break;
                case 4:
                    str = "D";
                    break;
                case 5:
                    str = "E";
                    break;
                case 6:
                    str = "F";
                    break;
                case 7:
                    str = "G";
                    break;
                case 8:
                    str = "H";
                    break;
                case 9:
                    str = "I";
                    break;
                case 10:
                    str = "J";
                    break;
                case 11:
                    str = "K";
                    break;
                case 12:
                    str = "L";
                    break;
                case 13:
                    str = "M";
                    break;
                case 14:
                    str = "N";
                    break;
                case 15:
                    str = "O";
                    break;
                case 16:
                    str = "P";
                    break;
                case 17:
                    str = "Q";
                    break;
                case 18:
                    str = "R";
                    break;
                case 19:
                    str = "S";
                    break;
                case 20:
                    str = "T";
                    break;
                case 21:
                    str = "U";
                    break;
                case 22:
                    str = "V";
                    break;
                case 23:
                    str = "W";
                    break;
                case 24:
                    str = "X";
                    break;
                case 25:
                    str = "Y";
                    break;
                case 26:
                    str = "Z";
                    break;
            }

            if (Num >= 26)
            {
                str = NumberToEnglishChar(((int)Num / 26) - ((f == 26) ? 1 : 0)) + str;
            }

            strReturn = str;

            return strReturn;
        }

        /// <summary>
        /// 字母转换为数字
        /// </summary>
        /// <param name="strEnglishChar"></param>
        /// <returns></returns>
        public static int EnglishCharToNumber(string strEnglishChar)
        {
            string EnglishChar = strEnglishChar.ToUpper();
            int L = EnglishChar.Length;
            if (L == 0) return 0;

            int Num = 0;
            int iReturn = 0;

            for (int i = 0; i < L; i++)
            {
                string str = EnglishChar.Substring(i, 1);
                switch (str)
                {
                    case "A":
                        Num = 1;
                        break;
                    case "B":
                        Num = 2;
                        break;
                    case "C":
                        Num = 3;
                        break;
                    case "D":
                        Num = 4;
                        break;
                    case "E":
                        Num = 5;
                        break;
                    case "F":
                        Num = 6;
                        break;
                    case "G":
                        Num = 7;
                        break;
                    case "H":
                        Num = 8;
                        break;
                    case "I":
                        Num = 9;
                        break;
                    case "J":
                        Num = 10;
                        break;
                    case "K":
                        Num = 11;
                        break;
                    case "L":
                        Num = 12;
                        break;
                    case "M":
                        Num = 13;
                        break;
                    case "N":
                        Num = 14;
                        break;
                    case "O":
                        Num = 15;
                        break;
                    case "P":
                        Num = 16;
                        break;
                    case "Q":
                        Num = 17;
                        break;
                    case "R":
                        Num = 18;
                        break;
                    case "S":
                        Num = 19;
                        break;
                    case "T":
                        Num = 20;
                        break;
                    case "U":
                        Num = 21;
                        break;
                    case "V":
                        Num = 22;
                        break;
                    case "W":
                        Num = 23;
                        break;
                    case "X":
                        Num = 24;
                        break;
                    case "Y":
                        Num = 25;
                        break;
                    case "Z":
                        Num = 26;
                        break;
                }
                iReturn += ((int)System.Math.Pow(26, (L - 1 - i))) * Num;
            }

            return iReturn;
        }
    }
}
