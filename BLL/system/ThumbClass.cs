using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace BLL
{

    /// <summary>
    ///ThumbClass 的摘要说明
    /// </summary>
    public class ThumbClass
    { 

        /// <summary>
        /// 剪切图片
        /// </summary>
        /// <param name="originalImagePath">原地址</param>
        /// <param name="thumbnailPath">生成后地址</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="mode">模式 HW、 W、H</param>
        public   void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);
            int towidth = width;//90
            int toheight = height;//60

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;//图片实际宽度
            int oh = originalImage.Height;//图片实际高度

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    if (ow > 0 && oh > 0)
                    {
                        if (ow / oh >= towidth / toheight)
                        {
                            if (ow > towidth)
                            {
                                ow = towidth;
                                oh = (oh * towidth) / ow;
                            }
                            else
                            {
                                ow = ow;
                                oh = oh;
                            }
                        }
                        else
                        {
                            if (oh > toheight)
                            {
                                oh = toheight;
                                ow = (ow * toheight) / originalImage.Height;
                            }
                            else
                            {
                                ow = ow;
                                oh = oh;
                            }
                        }
                    }

                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
    }
}
