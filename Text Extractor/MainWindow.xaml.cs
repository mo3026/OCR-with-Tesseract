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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace Text_Extractor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Hooks.Window.Window windows;
        IntPtr selectedControl = IntPtr.Zero;
        Snap win;
        bool snapShown = false;

        public MainWindow()
        {
            InitializeComponent();

            windows = new Hooks.Window.Window();
            windows.KeyDown += Windows_KeyDown;
            windows.KeyUp += windows_KeyUp;
            windows.InstallKeyboardHook();
            windows.InstallMouseHook();

            win = new Snap();
            win.TextCaptured += Win_TextCaptured;
        }

        private void Windows_KeyDown(Hooks.Window.Keyboard.Hook.KeyboardHookEventArgs e)
        {
            if (e.Key == Hooks.Window.Keyboard.VirtualKeyCode.Q)
            {
                if (!snapShown)
                {
                    snapShown = true;
                    win.ShowDialog();
                }
            }
        }

        private void Win_TextCaptured(string Text)
        {
            ContentText.Document.Blocks.Clear();
            ContentText.CaretPosition.InsertTextInRun(Text);
        }

        void windows_KeyUp(Hooks.Window.Keyboard.Hook.KeyboardHookEventArgs e)
        {
            if (e.Key == Hooks.Window.Keyboard.VirtualKeyCode.Q)
            {
                win.Hide();
                snapShown = false;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            win.Close();
        }
    }
}
