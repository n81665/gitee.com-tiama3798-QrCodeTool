using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Drawing;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;

namespace QrCodeTool
{
    /// <summary>
    /// 二维码操作帮助类
    /// </summary>
    public class QrCodeHelper
    {
        /// <summary>
        /// 读取二维码/条形码
        /// 读取失败，返回空字符串
        /// </summary>
        /// <param name="filename">指定二维码图片位置</param>
        /// <returns></returns>
        public static string Decode(string filename)
        {
            //判断文件是否存在
            if (File.Exists(filename)==false)
            {
                throw new Exception(string.Format("识别二维码时，获取‘{0}’图片失败", filename));
            }
            Bitmap map = new Bitmap(filename);
            return Decode(map);
        }
        /// <summary>
        /// 读取二维码/条形码
        /// 读取失败，返回空字符串
        /// </summary>
        /// <param name="img">指定二维码图片</param>
        /// <returns></returns>
        public static string Decode(Bitmap img)
        {
            try
            {
                BarcodeReader reader = new BarcodeReader();
                reader.Options.CharacterSet = "UTF-8";
                Result result = reader.Decode(img);
                return result.Text;
            }
            catch (Exception ex)
            {
                throw new Exception("识别二维码时出错，msg：" + ex.Message);
            }
        }
    }
}
