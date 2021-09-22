using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Hooks.Window.Keyboard.Hook
{
    public class KeyboardHook : HookHelper.HookHelper
    {
        public delegate void KeyboardHookEventHandler(KeyboardHookEventArgs e);
        public event KeyboardHookEventHandler KeyDown;
        public event KeyboardHookEventHandler KeyUp;

        public enum KeyboardMessages: uint
        {
            KeyDown = 0x0100,
            KeyUp = 0x0101
        }

        public KeyboardHook()
        {
            this.hookType = NativeAPI.HookType.WH_KEYBOARD_LL;
        }

        protected override bool HookCallBackResult(int code, IntPtr wparam, IntPtr lParam)
        {
            KeyboardMessages msg = (KeyboardMessages)wparam;
            KBDLLHookStruct hookRes = (KBDLLHookStruct)Marshal.PtrToStructure(lParam, typeof(KBDLLHookStruct));

            if (code >= 0)
            {
                    KeyboardHookEventArgs nn = new KeyboardHookEventArgs((VirtualKeyCode)hookRes.vkCode);
                    if (wparam == (IntPtr)KeyboardMessages.KeyDown)
                    {
                        OnKeyDown(nn);
                    }
                    if (wparam == (IntPtr)KeyboardMessages.KeyUp)
                    {
                        OnKeyUp(nn);
                    }
            }
            return true;
        }

        public void OnKeyDown(KeyboardHookEventArgs e)
        {
            if (KeyDown != null) KeyDown(e);
        }

        public void OnKeyUp(KeyboardHookEventArgs e)
        {
            if (KeyUp != null) KeyUp(e);
        }
    }
}
