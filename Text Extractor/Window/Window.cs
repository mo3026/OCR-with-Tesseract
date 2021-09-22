using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Collections;

namespace Hooks.Window
{
    [Serializable]
    public class Window
    {
        static Mouse.Hook.MouseHook mouseHook;
        bool mouseHooked = false;
        static Keyboard.Hook.KeyboardHook keyboardHook;
        bool keyboardHooked = false;

        public event Keyboard.Hook.KeyboardHook.KeyboardHookEventHandler KeyDown;
        public event Keyboard.Hook.KeyboardHook.KeyboardHookEventHandler KeyUp;

        public event Mouse.Hook.MouseHook.MouseHookEventHandler MouseKeyDown;
        public event Mouse.Hook.MouseHook.MouseHookEventHandler MouseKeyUp;
        public event Mouse.Hook.MouseHook.MouseHookEventHandler MouseMove;
        //public event Mouse.Hook.MouseHook.MouseHookEventHandler MouseDoubleClick;
        //public event Mouse.Hook.MouseHook.MouseHookEventHandler MouseWheel;

        private const int GWL_EXSTYLE = (-20);
        private const int WS_EX_TOOLWINDOW = 0x80;
        private const int WS_EX_APPWINDOW = 0x40000;
        private const int GW_OWNER = 4;

        public bool MouseHooked
        {
            get
            {
                return mouseHooked;
            }
        }

        public bool KeyboardHooked
        {
            get
            {
                return keyboardHooked;
            }
        }

        public Window()
        {
            keyboardHook = new Keyboard.Hook.KeyboardHook();
            keyboardHook.KeyDown += new Keyboard.Hook.KeyboardHook.KeyboardHookEventHandler(keyboardHook_KeyDown);
            keyboardHook.KeyUp += new Keyboard.Hook.KeyboardHook.KeyboardHookEventHandler(keyboardHook_KeyUp);

            mouseHook = new Mouse.Hook.MouseHook();
            mouseHook.MouseKeyDown += mouseHook_MouseKeyDown;
            mouseHook.MouseKeyUp += mouseHook_MouseKeyUp;
            mouseHook.MouseDoubleClick += mouseHook_MouseDoubleClick;
            mouseHook.MouseMove += mouseHook_MouseMove;
        }

        public void InstallKeyboardHook()
        {
            keyboardHooked = true;
            if (!keyboardHook.IsHookInstalled)
            {
                keyboardHook.InstallHook();
            }
        }

        public void UninstalKeyboardHook()
        {
            keyboardHooked = false;
            if (keyboardHook.IsHookInstalled) keyboardHook.UninstallHook();
        }

        protected void OnKeyDown(Keyboard.Hook.KeyboardHookEventArgs e)
        {
            if (KeyDown != null) KeyDown(e);
        }

        protected void OnKeyUp(Keyboard.Hook.KeyboardHookEventArgs e)
        {
            if (KeyUp != null) KeyUp(e);
        }

        void keyboardHook_KeyDown(Keyboard.Hook.KeyboardHookEventArgs e)
        {
            OnKeyDown(e);
        }

        void keyboardHook_KeyUp(Keyboard.Hook.KeyboardHookEventArgs e)
        {
            OnKeyUp(e);
        }

        public void InstallMouseHook()
        {
            mouseHooked = false;
            if (!mouseHook.IsHookInstalled)
            {
                mouseHook.InstallHook();
            }
        }

        public void UninstalMouseHook()
        {
            mouseHooked = false;
            if (mouseHook.IsHookInstalled) mouseHook.UninstallHook();
        }

        public void OnMouseKeyDown(Mouse.Hook.MouseHookEventArgs e)
        {
            if (MouseKeyDown != null) MouseKeyDown(e);
        }

        public void OnMouseKeyUp(Mouse.Hook.MouseHookEventArgs e)
        {
            if (MouseKeyUp != null) MouseKeyUp(e);
        }

        public void OnMouseMove(Mouse.Hook.MouseHookEventArgs e)
        {
            if (MouseMove != null) MouseMove(e);
        }

        /*public void OnMouseDoubleClick(Mouse.Hook.MouseHookEventArgs e)
        {
            if (MouseDoubleClick != null) MouseDoubleClick(e);
        }

        public void OnMouseWheel(Mouse.Hook.MouseHook.Mousemessages e)
        {
            if (MouseWheel != null) MouseWheel(e);
        }*/

        void mouseHook_MouseDoubleClick(Mouse.Hook.MouseHookEventArgs e)
        {
            //OnMouseDoubleClick(e);
        }

        void mouseHook_MouseKeyUp(Mouse.Hook.MouseHookEventArgs e)
        {
            OnMouseKeyUp(e);
        }

        void mouseHook_MouseKeyDown(Mouse.Hook.MouseHookEventArgs e)
        {
            OnMouseKeyDown(e);
        }

        void mouseHook_MouseMove(Mouse.Hook.MouseHookEventArgs e)
        {
            OnMouseMove(e);
        }
    }
}
