using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationProcess;

namespace Test
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            
            Bitmap b = new Bitmap(@"C:\Users\Altran\Pictures\1.bmp");

            //var imagem = b.IsExist();

            //var pos = b.Possition();

            //var base64 = b.ToBase64();
            //b.Click();
            //b.Click(0, 500);

            //var exist = b.IsExist();

            //WindowProcess.SetFocus(@"UOL");
            //var Handle = WindowProcess.GetHandle("Vivo");
            //WindowProcess.SetFocus(Handle);

            //var handle =  WindowProcess.GetHandle("UOL");
            //WindowProcess.SetFocus(handle);

            //var wb = WebProcess.Browser("TIM");

            var capt = ImageProcess.reconhecerCaptcha(b);

            var texto = ImageProcess.OCR(b);


        }
    }
}
