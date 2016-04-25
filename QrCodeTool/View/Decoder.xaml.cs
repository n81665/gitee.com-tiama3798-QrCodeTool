using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QrCodeTool.View
{
    /// <summary>
    /// Decoder.xaml 的交互逻辑
    /// </summary>
    public partial class Decoder : Window
    {
        public Decoder()
        {
            InitializeComponent();
            //启用拖拽
            this.AllowDrop = true;
        }
        //选择图片
        private void button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openDialog = new System.Windows.Forms.OpenFileDialog();
            openDialog.Title = "选择二维码";
            openDialog.Filter = "图片(*.png)|*.png|图片(*.jpg)|*.jpg";
            //openDialog.DefaultExt = "png";
            System.Windows.Forms.DialogResult result = openDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel)
                return;

            txtPath.Text = openDialog.FileName;
            ReadImage();
        }
        //识别
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ReadImage();
        }
        //执行读取操作
        private void ReadImage()
        {
            try
            {
                string filename = txtPath.Text;
                if (string.IsNullOrEmpty(filename))
                    throw new Exception("获取二维码图片为空");

                //读取图片内容
                BitmapImage img = new BitmapImage(new Uri(filename));
                imgTarget.Source = img;

                //转码操作
                string text = QrCodeHelper.Decode(filename);
                if (string.IsNullOrEmpty(text))
                    throw new Exception("解码失败");
                txtResult.Text = text;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       // https://git.oschina.net/tiama3798/QrCodeTool.git


        //文件拖入
        private void Window_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.Link;
            else
                e.Effects = DragDropEffects.None;
        }
        //拖入结束
        private void Window_Drop(object sender, DragEventArgs e)
        {
            //获取拖入的第一个文件
           Array data= e.Data.GetData(DataFormats.FileDrop) as System.Array;
            if (data != null&&data.Length>0)
            {
                string filename = data.GetValue(0).ToString();
                txtPath.Text = filename;
                ReadImage();
            }
        }
    }
}
