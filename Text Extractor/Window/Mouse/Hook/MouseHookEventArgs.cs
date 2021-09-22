using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hooks.Window.Mouse.Hook
{
    public class MouseHookEventArgs : System.EventArgs
    {
        private MouseKeys key;
        private MSLLHOOKSTRUCT charkey;
        private bool suppressKey;

        public MouseKeys Key
        {
            get { return key; }
        }

        public NativeAPI.POINT POINT
        {
            get { return charkey.pt; }
        }

        public bool SuppressKey
        {
            get { return suppressKey; }
            set { suppressKey = value; }
        }

        public MouseHookEventArgs()
        {
            suppressKey = false;
        }

        public MouseHookEventArgs(MouseKeys Key, MSLLHOOKSTRUCT sk, bool SuppressKey)
        {
            key = Key;
            charkey = sk;
            suppressKey = SuppressKey;
        }
    }
}
