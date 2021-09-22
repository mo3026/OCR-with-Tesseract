using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Hooks.Window.Mouse.Hook
{
    [StructLayout(LayoutKind.Sequential)]
    public class MSLLHOOKSTRUCT
    {
        public NativeAPI.POINT pt;
        public int mouseData;
        public int flags;
        public int time;
        public IntPtr dwExtraInfo;
    }
}
