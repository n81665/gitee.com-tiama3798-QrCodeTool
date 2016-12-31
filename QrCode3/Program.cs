using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.IO;
using System.Drawing;
using ZXing;
using ZXing.QrCode;
using ZXing.Common;
using static ZXing.RGBLuminanceSource;
using System.Collections;
using System.Drawing.Imaging;
using ZXing.QrCode.Internal;

namespace QrCode3
{
    class Program
    {
        static void Main(string[] args)
        {

            //1.读取二维码内容
            //读取字母成功
            //string filename = @"H:\桌面\截图\url.png";
            //string filename = @"H:\桌面\截图\weibo.png";
            //string filename = @"H:\桌面\截图\qrcode1.png";
            //string filename = @"H:\桌面\截图\qrcode2.png";
            //string filename = @"H:\桌面\截图\qrcode3.png";
            //对于有logo的二维码只返回字符串内容
            //string filename = @"H:\桌面\截图\qrcode4.png";
            //string filename = @"H:\桌面\截图\qrcode5.png";
            //string filename = @"H:\桌面\截图\qrcode6.png";
            //识别条形码
            string filename = @"H:\桌面\截图\generate2.png";
            string result = Read1(filename);
            Console.WriteLine(result);



            //生成二维码 
            Generate1("https://www.baidu.com/");
            Generate1("ionic是一个强大的混合式/hybrid HTML5移动开发框架，特点是使用标准的HTML、CSS和JavaScript，开发跨平台的应用 ，只需要几步就可以快速创建您的Ionic应用，创建应用从这里开始");

            //生成带图片的二维码
            Generate3("https://www.baidu.com/");
            Generate3("ionic是一个强大的混合式/hybrid HTML5移动开发框架，特点是使用标准的HTML、CSS和JavaScript，开发跨平台的应用 ，只需要几步就可以快速创建您的Ionic应用，创建应用从这里开始");


            //生成条形码
            Generate2("1234567890");

            Console.Read();
        }
        /// <summary>
        /// 读取二维码
        /// 读取失败，返回空字符串
        /// </summary>
        /// <param name="filename">指定二维码图片位置</param>
        static string Read1(string filename)
        {
            BarcodeReader reader = new BarcodeReader();
            reader.Options.CharacterSet = "UTF-8";
            Bitmap map = new Bitmap(filename);
            Result result = reader.Decode(map);
            return result == null ? "" : result.Text;
        }
        #region 生成二维码
        /// <summary>
        /// 生成二维码,保存成图片
        /// </summary>
        static void Generate1(string text)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            QrCodeEncodingOptions options = new QrCodeEncodingOptions();
            options.DisableECI = true;
            //设置内容编码
            options.CharacterSet = "UTF-8";
            //设置二维码的宽度和高度
            options.Width = 500;
            options.Height = 500;
            //设置二维码的边距,单位不是固定像素
            options.Margin = 1;
            writer.Options = options;

            Bitmap map = writer.Write(text);
            string filename = @"H:\桌面\截图\generate1.png";
            map.Save(filename, ImageFormat.Png);
            map.Dispose();
        }

        /// <summary>
        /// 生成带Logo的二维码
        /// </summary>
        /// <param name="text"></param>
        static void Generate3(string text)
        {
            //Logo 图片
            Bitmap logo = new Bitmap(@"H:\桌面\截图\102.jpg");
            //构造二维码写码器
            MultiFormatWriter writer = new MultiFormatWriter();
            Dictionary<EncodeHintType, object> hint = new Dictionary<EncodeHintType, object>();
            hint.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
            hint.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);

            //生成二维码 
            BitMatrix bm = writer.encode(text, BarcodeFormat.QR_CODE, 300, 300, hint);
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            Bitmap map = barcodeWriter.Write(bm);


            //获取二维码实际尺寸（去掉二维码两边空白后的实际尺寸）
            int[] rectangle = bm.getEnclosingRectangle();

            //计算插入图片的大小和位置
            int middleW = Math.Min((int)(rectangle[2] / 3.5), logo.Width);
            int middleH = Math.Min((int)(rectangle[3] / 3.5), logo.Height);
            int middleL = (map.Width - middleW) / 2;
            int middleT = (map.Height - middleH) / 2;

            //将img转换成bmp格式，否则后面无法创建Graphics对象
            Bitmap bmpimg = new Bitmap(map.Width, map.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmpimg))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(map, 0, 0);
            }
            //将二维码插入图片
            Graphics myGraphic = Graphics.FromImage(bmpimg);
            //白底
            myGraphic.FillRectangle(Brushes.White, middleL, middleT, middleW, middleH);
            myGraphic.DrawImage(logo, middleL, middleT, middleW, middleH);

            //保存成图片
            bmpimg.Save(@"H:\桌面\截图\generate3.png", ImageFormat.Png);
        }
        #endregion


        #region 生成条形码
        static void Generate2(string text)
        {
            BarcodeWriter writer = new BarcodeWriter();
            //使用ITF 格式，不能被现在常用的支付宝、微信扫出来
            //如果想生成可识别的可以使用 CODE_128 格式
            //writer.Format = BarcodeFormat.ITF;
            writer.Format = BarcodeFormat.CODE_128;
            EncodingOptions options = new EncodingOptions()
            {
                Width = 150,
                Height = 50,
                Margin = 2
            };
            writer.Options = options;
            Bitmap map = writer.Write(text);
            string filename = @"H:\桌面\截图\generate2.png";
            map.Save(filename, ImageFormat.Png);
        }
        #endregion



        #region 失败方法
        static string Read2(string filename)
        {
            MultiFormatReader reader = new MultiFormatReader();

            Bitmap map = new Bitmap(filename);
            FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);
            stream.Close();

            LuminanceSource source = new RGBLuminanceSource(data, map.Width, map.Height, BitmapFormat.ARGB32);
            BinaryBitmap bina = new BinaryBitmap(new HybridBinarizer(source));
            //注意 必须是utf-8
            //Hashtable hints = new Hashtable();
            //hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
            Dictionary<DecodeHintType, object> dic = new Dictionary<DecodeHintType, object>();
            dic.Add(DecodeHintType.CHARACTER_SET, "UTF-8");
            Result result = reader.decode(bina, dic);

            return result.Text;
        }
        static string Read3(string filename)
        {
            QRCodeReader reader = new QRCodeReader();
            Bitmap map = new Bitmap(filename);
            Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            map.Save(stream, ImageFormat.Png);

            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);

            stream.Close();
            LuminanceSource source = new RGBLuminanceSource(data, map.Width, map.Height);
            Binarizer bina = new HybridBinarizer(source);
            BinaryBitmap bMap = new BinaryBitmap(bina);
            Result result = reader.decode(bMap);

            return result.Text;
        }
        #endregion

    }
}

