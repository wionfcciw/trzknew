using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Drawing.Drawing2D;
using System.Drawing;
namespace SincciWeb.ht
{
    public partial class Image : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetValidate();
                this.SetValidate();
            }
        }
        private string GetValidate()
        {
            string strRanNum = null;
            string[] strNum = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            Random rd = new Random();  //随机函数
            for (int i = 0; i < 4; i++)
            {
                strRanNum += strNum[rd.Next(strNum.Length)];
            }
            Session.Add("asd", strRanNum);
            return strRanNum;
        }

        string strRandom;
        public void SetValidate()
        {
            strRandom = this.GetValidate();
            Session["random"] = strRandom;//保存验证码
            Bitmap img = new Bitmap(50, 22);   //从Image类派生出，将图像转换为GDI+的标准格式以实现在像素级控制图像 （由一个个的点组成的图形）
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.White);
            g.DrawString(strRandom, new Font("fixedsys", 12, FontStyle.Bold), new SolidBrush(Color.Blue), 5, 5);
            Random rd = new Random();
            for (int i = 0; i < 100; i++)
            {
                //产生随机坐标
                int nX = rd.Next(img.Width);
                int nY = rd.Next(img.Height);
                //画前景噪音点
                img.SetPixel(nX, nY, Color.FromArgb(rd.Next()));
            }
            img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);  //这句代码将把你的图片保存成jpeg格式
            img.Dispose();
            g.Dispose();
        }
    }
}
