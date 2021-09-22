using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.Drawing;
using Tesseract;

namespace Text_Extractor
{
    public partial class Snap : Window
    {
        TesseractEngine engine;
        public int x;
        public int y;
        public int width;
        public int height;
        public bool isMouseDown = false;

        public delegate void TextCapturedEventHandler(String Text);
        public event TextCapturedEventHandler TextCaptured;

        public void OnTextCaptured(String e)
        {
            if (TextCaptured != null) TextCaptured(e);
        }

        public Snap()
        {
            InitializeComponent();
            engine = new Tesseract.TesseractEngine(@"./tessdata", "eng", Tesseract.EngineMode.Default);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = true;
            x = (int)e.GetPosition(null).X;
            y = (int)e.GetPosition(null).Y;
        }

        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (this.isMouseDown)
            {
                int curx = (int)e.GetPosition(null).X;
                int cury = (int)e.GetPosition(null).Y;
                int endpointX = curx - x;
                int endpointY = cury - y + 2;
                int newstartpointX = x;
                int newstartpointY = y;
                if (endpointX < 0)
                {
                    newstartpointX = x + endpointX;
                    endpointX = -endpointX;
                }
                if (endpointY < 0)
                {
                    newstartpointY = y + endpointY;
                    endpointY = -endpointY;
                }

                rect.Width = endpointX;
                rect.Height = endpointY;
                Canvas.SetLeft(rect, newstartpointX);
                Canvas.SetTop(rect, newstartpointY);
                CaptureScreen(newstartpointX - 6, newstartpointY - 6, endpointX - 2, endpointY);

                if (e.LeftButton == MouseButtonState.Released)
                {
                    this.isMouseDown = false;
                    rect.Width = 0;
                    rect.Height = 0;
                    Canvas.SetLeft(rect, 0);
                    Canvas.SetTop(rect, 0);
                    this.x = this.y = 0;
                }
            }
        }

        public void CaptureScreen(int x, int y, int width, int height)
        {
            try
            {
                if (width != 0 && height != 0)
                {
                    Bitmap image = new Bitmap(width, height);
                    Graphics g = Graphics.FromImage(image);
                    g.CopyFromScreen(x, y, 0, 0, image.Size, CopyPixelOperation.SourceCopy);
                    StartOCR(image);
                }
            }
            catch { }
        }

        public void StartOCR(Bitmap image)
        {
            Tesseract.Pix p = Tesseract.PixConverter.ToPix(image);
            p = p.ConvertRGBToGray(0.0f, 0.0f, 0.0f);
            p.Scale(4, 4);
            p.BinarizeOtsuAdaptiveThreshold(2000, 2000, 0, 0, 0f);
            using (var page = engine.Process(p))
            {
                OnTextCaptured(page.GetText().Trim());
            }
        }
    }
}
