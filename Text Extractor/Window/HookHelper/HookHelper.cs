using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace Hooks.Window.HookHelper
{
    public abstract class HookHelper
    {
        HookData hookDate;
        public bool IsHookInstalled { get; set; }
        protected NativeAPI.HookType hookType = NativeAPI.HookType.WH_GETMESSAGE;

        protected abstract bool HookCallBackResult(int code, IntPtr wparam, IntPtr lParam);

        public HookHelper()
        {
        }

        public bool InstallHook(IntPtr handle, string hookName)
        {
            NativeAPI.User32.HookProc hookCallBack = new NativeAPI.User32.HookProc(CallBackResult);
            GCHandle gcHandle = GCHandle.Alloc(hookCallBack, GCHandleType.Normal);//prevent to collect
            uint processId = 0;
            NativeAPI.User32.GetWindowThreadProcessId(handle, out processId);
            string moduleName = Process.GetProcessById((int)processId).MainModule.ModuleName;
            IntPtr moduleHandle = NativeAPI.Kernel32.GetModuleHandle(moduleName);
            IntPtr hookPtr = NativeAPI.User32.SetWindowsHookEx(this.hookType, hookCallBack, moduleHandle, 0);
            if (hookPtr == IntPtr.Zero) return false;
            hookDate = new HookData(0, moduleHandle, hookType, hookPtr, gcHandle);
            return true;
        }

        public bool InstallHook()
        {
            NativeAPI.User32.HookProc hookCallBack = new NativeAPI.User32.HookProc(CallBackResult);
            GCHandle gcHandle = GCHandle.Alloc(hookCallBack, GCHandleType.Normal);//prevent to collect
            string moduleName = Process.GetCurrentProcess().MainModule.ModuleName;
            IntPtr moduleHandle = NativeAPI.Kernel32.GetModuleHandle(moduleName);
            IntPtr hookPtr = NativeAPI.User32.SetWindowsHookEx(this.hookType, hookCallBack, moduleHandle, 0);
            if (hookPtr == IntPtr.Zero) return false;
            hookDate = new HookData(0, moduleHandle, hookType, hookPtr, gcHandle);
            IsHookInstalled = true;
            return true;
        }


        public bool UninstallHook()
        {
            return this.UninstallHook(hookDate);
        }

        public bool UninstallHook(HookData hook)
        {
            hook.gcHandle.Free();
            IsHookInstalled = false;
            hookDate = null;
            return NativeAPI.User32.UnhookWindowsHookEx(hook.hookPtr);
        }

        private IntPtr CallBackResult(int code, IntPtr wParam, IntPtr lParam)
        {
            try
            {
                if (code < 0)
                    return NativeAPI.User32.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);

                bool b = HookCallBackResult(code, wParam, lParam);
                if (!b)
                {
                    return new IntPtr(1);
                }
            }
            catch(Exception r)
            {
                System.Windows.Forms.MessageBox.Show(r.Message);
            }
            return NativeAPI.User32.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }
    }
}