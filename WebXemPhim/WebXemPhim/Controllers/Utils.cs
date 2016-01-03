using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;

namespace WebXemPhim.Controllers
{
    public static class Utils
    {
        // Thay đổi kích thước ảnh
        // Banner: 1200x400px
        // Thumnails: 400x300px

        public static Image resizeImage(Image img, int width, int height)
        {
            //Bitmap b = new Bitmap(width, height);
            //Graphics g = Graphics.FromImage((Image)b);

            //g.InterpolationMode = InterpolationMode.Bicubic;    // Specify here
            //g.DrawImage(img, 0, 0, width, height);
            //g.Dispose();

            //return (Image)b;
            return img.GetThumbnailImage(width, height, null, IntPtr.Zero);
        }

        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}