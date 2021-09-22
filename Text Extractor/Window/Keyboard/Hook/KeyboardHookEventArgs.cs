using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hooks.Window.Keyboard.Hook
{
    public class KeyboardHookEventArgs : System.EventArgs
    {
        private Hooks.Window.Keyboard.VirtualKeyCode key;

        public VirtualKeyCode Key
        {
            get { return key; }
        }

        public KeyboardHookEventArgs(VirtualKeyCode Key)
        {
            key = Key;
        }
    }
}
