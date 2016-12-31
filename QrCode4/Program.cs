using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.IO;


using ThoughtWorks.QRCode;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ThoughtWorks.QRCode.ExceptionHandler;


namespace QrCode4
{
    class Program
    {
        static void Main(string[] args)
        {
            // string filename = @"H:\桌面\截图\size.png";
            //解析微博失败
            string filename = @"H:\桌面\截图\weibo.png";
            //string filename = @"H:\桌面\截图\qrcode1.png";
            //string filename = @"H:\桌面\截图\qrcode2.png";
            //string filename = @"H:\桌面\截图\qrcode3.png";
            string result = Read1(filename);
            Console.WriteLine(result);

            Console.Read();
        }

        //读取二维码
        static string Read1(string filename)
        {
            QRCodeDecoder decoder = new QRCodeDecoder();
            QRCodeBitmapImage img = new QRCodeBitmapImage(new Bitmap(filename));

            string result = decoder.decode(img,Encoding.UTF8);

            return result;
        }
    }
}
