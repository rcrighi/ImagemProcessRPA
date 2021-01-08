using AForge.Imaging.Filters;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

namespace AutomationProcess
{
    public static class ImageProcess
    {
        /// <summary>
        /// Convert string base64 to Image 
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static Bitmap Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);

            return (Bitmap)image;
        }

        /// <summary>
        /// Capture photo of the current screen
        /// </summary>
        /// <returns></returns>
        public static Bitmap PrintScreen()
        {
            var image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            var gfx = Graphics.FromImage(image);
            gfx.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            return image;
        }

        /// <summary>
        /// Image Search by a reference
        /// </summary>
        /// <param name="imgSource"></param>
        /// <param name="imgSearch"></param>
        /// <returns></returns>
        public static Point ImageSearch(Bitmap imgSource, Bitmap imgReference)
        {

            var retorno = new Point();

            Image<Bgr, byte> source = new Image<Bgr, byte>(imgSource);
            Image<Bgr, byte> template = new Image<Bgr, byte>(imgReference);
            Image<Bgr, byte> imageToShow = source.Copy();

            using (Image<Gray, float> result = source.MatchTemplate(template, TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                if (maxValues[0] > 0.8)
                {
                    retorno = maxLocations[0];
                }
            }
            return retorno;
        }

        /// <summary>
        /// Crop Imge from bitmap 
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="cropX"></param>
        /// <param name="cropY"></param>
        /// <param name="cropWidth"></param>
        /// <param name="cropHeight"></param>
        /// <returns></returns>
        public  static Bitmap CropBitmap(Bitmap bitmap, int cropX, int cropY, int cropWidth, int cropHeight)
        {
            Rectangle rect = new Rectangle(cropX, cropY, cropWidth, cropHeight);
            Bitmap cropped = bitmap.Clone(rect, bitmap.PixelFormat);
            return cropped;
        }

        /// <summary>
        /// Crop part of the screen
        /// </summary>
        /// <param name="cropX"></param>
        /// <param name="cropY"></param>
        /// <param name="cropWidth"></param>
        /// <param name="cropHeight"></param>
        /// <returns></returns>
        public static Bitmap CropBitmap(int cropX, int cropY, int cropWidth, int cropHeight)
        {
            var bitmap  = PrintScreen();
            Rectangle rect = new Rectangle(cropX, cropY, cropWidth, cropHeight);
            Bitmap cropped = bitmap.Clone(rect, bitmap.PixelFormat);
            return cropped;
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="imgToResize"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Image ResizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        public static string reconhecerCaptcha(Image img)
        {
            Bitmap imagem = new Bitmap(img);
            imagem = imagem.Clone(new Rectangle(0, 0, img.Width, img.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Erosion erosion = new Erosion();
            Dilatation dilatation = new Dilatation();
            Invert inverter = new Invert();
            ColorFiltering cor = new ColorFiltering();
            cor.Blue = new AForge.IntRange(200, 255);
            cor.Red = new AForge.IntRange(200, 255);
            cor.Green = new AForge.IntRange(200, 255);
            Opening open = new Opening();
            BlobsFiltering bc = new BlobsFiltering();
            Closing close = new Closing();
            GaussianSharpen gs = new GaussianSharpen();
            ContrastCorrection cc = new ContrastCorrection();
            bc.MinHeight = 10;
            FiltersSequence seq = new FiltersSequence(gs, inverter, open, inverter, bc, inverter, open, cc, cor, bc, inverter);
            //pictureBox.Image = seq.Apply(imagem);
            string reconhecido = OCR((Bitmap)seq.Apply(imagem));
            return reconhecido;
        }

        public static string OCR(Bitmap b)
        {
            string res = "";
            using (var engine = new TesseractEngine(@"C:\RODRIGO\PROJETOS\ALTRAN\AeC - Framewors\AeC.Automacao.ImageProcess\AeC.ImageProcess\tessdata", "eng", EngineMode.TesseractOnly))
            {
                engine.SetVariable("tessedit_char_whitelist", "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvxz");
                engine.SetVariable("tessedit_unrej_any_wd", true);

                using (var page = engine.Process(b, PageSegMode.SingleLine))
                    res = page.GetText();
            }
            return res;
        }
    }
}
