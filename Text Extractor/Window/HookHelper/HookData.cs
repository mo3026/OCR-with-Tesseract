using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Hooks.Window.HookHelper
{
    public class HookData
    {
        public readonly uint threadID;
        public readonly IntPtr moduleHandle;
        public readonly NativeAPI.HookType hookType;
        public readonly IntPtr hookPtr;
        public readonly GCHandle gcHandle;

        internal HookData(uint threadID, IntPtr moduleHandle, NativeAPI.HookType hookType, IntPtr hookPtr, GCHandle gcHandle)
        {
            this.threadID = threadID;
            this.moduleHandle = moduleHandle;
            this.hookType = hookType;
            this.hookPtr = hookPtr;
            this.gcHandle = gcHandle;
        }
    }
}
