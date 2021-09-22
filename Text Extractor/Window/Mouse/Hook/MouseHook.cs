using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Hooks.Window.Mouse.Hook
{
    public class MouseHook : HookHelper.HookHelper
    {
        public delegate void MouseHookEventHandler(MouseHookEventArgs e);
        public event MouseHookEventHandler MouseKeyDown;
        public event MouseHookEventHandler MouseKeyUp;
        public event MouseHookEventHandler MouseDoubleClick;
        public event MouseHookEventHandler MouseMove;
        public event MouseHookEventHandler MouseWheel;

        public MouseHook()
        {
            this.hookType = NativeAPI.HookType.WH_MOUSE_LL;
        }

        public void OnMouseKeyUp(MouseHookEventArgs e)
        {
            if (MouseKeyUp != null) MouseKeyUp(e);
        }

        public void OnMouseKeyDown(MouseHookEventArgs e)
        {
            if (MouseKeyDown != null) MouseKeyDown(e);
        }

        public void OnMouseMove(MouseHookEventArgs e)
        {
            if (MouseMove != null) MouseMove(e);
        }

        protected override bool HookCallBackResult(int code, IntPtr wparam, IntPtr lParam)
        {
            try
            {
                Mousemessages msg = (Mousemessages)wparam;
                MSLLHOOKSTRUCT hookRes = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                MouseHookEventArgs mh = new MouseHookEventArgs();

                if (code >= 0)
                {
                    if (Mousemessages.LeftButtonUp == msg)
                    {
                        mh = new MouseHookEventArgs(MouseKeys.LeftButton, hookRes, false);
                        OnMouseKeyUp(mh);
                        return true;
                    }
                    if (Mousemessages.MiddleButtonUp == msg)
                    {
                        mh = new MouseHookEventArgs(MouseKeys.MiddleButton, hookRes, false);
                        OnMouseKeyUp(mh);
                        return true;
                    }

                    if (Mousemessages.XButtonUp == msg && hookRes.mouseData == 131072)
                    {
                        mh = new MouseHookEventArgs(MouseKeys.XBUTTON2, hookRes, false);
                        OnMouseKeyUp(mh);
                        return true;
                    }
                    if (Mousemessages.XButtonUp == msg && hookRes.mouseData == 65536)
                    {
                        mh = new MouseHookEventArgs(MouseKeys.XBUTTON1, hookRes, false);
                        OnMouseKeyUp(mh);
                        return true;
                    }

                    if (Mousemessages.MouseMove == msg)
                    {
                        mh = new MouseHookEventArgs(MouseKeys.XBUTTON1, hookRes, false);
                        OnMouseMove(mh);
                        return true;
                    }
                    if (Mousemessages.LeftButtonDown == msg)
                    {
                        mh = new MouseHookEventArgs(MouseKeys.LeftButton, hookRes, false);
                        OnMouseKeyDown(mh);
                        return true;
                    }
                }
            }
            catch (Exception r)
            {
            }

            return true;
        }
    }
}
