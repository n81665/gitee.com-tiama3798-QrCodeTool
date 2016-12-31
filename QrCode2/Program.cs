using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Gma.QrCodeNet;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace QrCode2
{
    class Program
    {
        static void Main(string[] args)
        {

            //Generate1();
            //Generate2();

            //Generate3();
            //Generate4();
            Generate5();


            Console.WriteLine("ok");
            Console.Read();
        }
        static string url = "http://blog.csdn.net/u011127019";
//生成二维码
static void Generate1()
{
    //创建二维码生成类
    QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
    QrCode qrCode = qrEncoder.Encode(url);
    //输出显示在控制台
    for (int j = 0; j < qrCode.Matrix.Height; j++)
    {
        for (int i = 0; i < qrCode.Matrix.Width; i++)
        {
            char charToPoint = qrCode.Matrix[i, j] ? '█' : ' ';
            Console.Write(charToPoint);
        }
        Console.WriteLine();
    }
}
//生成图片
static void Generate2()
{
    QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
    QrCode qrCode = qrEncoder.Encode(url);
    //保存成png文件
    string filename = @"H:\桌面\截图\url.png";
    GraphicsRenderer render = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);
    using (FileStream stream = new FileStream(filename, FileMode.Create))
    {
        render.WriteToStream(qrCode.Matrix, ImageFormat.Png, stream);
    }
}
//生成中文二维码
static void Generate3()
{
    QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
    QrCode qrCode = qrEncoder.Encode("我是小天马");
    //保存成png文件
    string filename = @"H:\桌面\截图\cn.png";
    GraphicsRenderer render = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);

    Bitmap map = new Bitmap(500, 500);
    Graphics g = Graphics.FromImage(map);
    g.FillRectangle(Brushes.Red, 0, 0, 500, 500);
    render.Draw(g, qrCode.Matrix, new Point(20, 20));
    map.Save(filename, ImageFormat.Png);
}
//设置二维码大小
static void Generate4()
{
    QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
    QrCode qrCode = qrEncoder.Encode("我是小天马");
    //保存成png文件
    string filename = @"H:\桌面\截图\size.png";
    //ModuleSize 设置图片大小  
    //QuietZoneModules 设置周边padding
    /*
        * 5----150*150    padding:5
        * 10----300*300   padding:10
        */
    GraphicsRenderer render = new GraphicsRenderer(new FixedModuleSize(10, QuietZoneModules.Two), Brushes.Black, Brushes.White);

    Point padding = new Point(10, 10);
    DrawingSize dSize = render.SizeCalculator.GetSize(qrCode.Matrix.Width);
    Bitmap map = new Bitmap(dSize.CodeWidth + padding.X, dSize.CodeWidth + padding.Y);
    Graphics g = Graphics.FromImage(map);
    render.Draw(g, qrCode.Matrix, padding);
    map.Save(filename, ImageFormat.Png);
}


//生成带Logo的二维码
static void Generate5()
{
    QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
    QrCode qrCode = qrEncoder.Encode("我是小天马");
    //保存成png文件
    string filename = @"H:\桌面\截图\logo.png";
    GraphicsRenderer render = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);

    DrawingSize dSize = render.SizeCalculator.GetSize(qrCode.Matrix.Width);
    Bitmap map = new Bitmap(dSize.CodeWidth, dSize.CodeWidth);
    Graphics g = Graphics.FromImage(map);
    render.Draw(g, qrCode.Matrix);
    //追加Logo图片 ,注意控制Logo图片大小和二维码大小的比例
    Image img = Image.FromFile(@"F:\JavaScript_Solution\QrCode\QrCode\Images\101.jpg");

    Point imgPoint = new Point((map.Width - img.Width) / 2, (map.Height - img.Height) / 2);
    g.DrawImage(img, imgPoint.X, imgPoint.Y, img.Width, img.Height);
    map.Save(filename, ImageFormat.Png);
}

        //目前当前类库中没有找到识别二维码的方法

    }
}
