using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProcess
{
    public static class BitmapExtension 
    {

        /// <summary>
        /// Convert image to base64
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="imageFormat"></param>
        /// <returns></returns>
        public static string ToBase64(this Bitmap bmp, ImageFormat imageFormat = null)
        {
            string base64String = string.Empty;
            if (imageFormat == null) imageFormat = ImageFormat.Png;

            MemoryStream memoryStream = new MemoryStream();
            bmp.Save(memoryStream, imageFormat);

            memoryStream.Position = 0;
            byte[] byteBuffer = memoryStream.ToArray();

            memoryStream.Close();

            base64String = Convert.ToBase64String(byteBuffer);
            byteBuffer = null;

            return base64String;
        }

        /// <summary>
        /// Return position of a image 
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static Point Possition(this Bitmap bmp)
        {
            Bitmap _screen = ImageProcess.PrintScreen();
            return ImageProcess.ImageSearch(_screen, bmp);
        }

        /// <summary>
        /// Execute mouse click in image
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="Button"></param>
        public static void Click(this Bitmap bmp, string Button = "LEFT", int Clicks=1, int Speed=-1)
        {
            Bitmap _screen = ImageProcess.PrintScreen();
            var pos=  ImageProcess.ImageSearch(_screen, bmp);

            AutoIt.AutoItX.MouseClick(Button, pos.X + 2, pos.Y + 2, Clicks, Speed);
        }

        public static void Click(this Bitmap bmp, int X, int Y, string Button = "LEFT",  int Clicks = 1, int Speed = -1)
        {
            Bitmap _screen = ImageProcess.PrintScreen();
            var pos = ImageProcess.ImageSearch(_screen, bmp);
            AutoIt.AutoItX.MouseClick(Button, pos.X + X, pos.Y + Y, Clicks, Speed);
        }


        public static bool IsExist(this Bitmap bmp)
        {
            bool result = false;
            Bitmap _screen = ImageProcess.PrintScreen();
            Point pos = ImageProcess.ImageSearch(_screen, bmp);
            if (pos.X != 0 && pos.Y != 0) result = true;
            return result;
        }

        
    }
}
