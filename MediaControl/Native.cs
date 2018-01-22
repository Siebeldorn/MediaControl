using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace PartyControl
{
    static class Native
    {
        #region Mouse

        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms646260(v=vs.85).aspx
        /// </summary>
        [Flags]
        public enum MouseButton : uint
        {
            /// <summary>The dx and dy parameters contain normalized absolute coordinates. If not set, those parameters contain relative data.</summary>
            Absolute = 0x8000,
            /// <summary>The left button is down.</summary>
            LeftDown = 0x0002,
            /// <summary>The left button is up.</summary>
            LeftUp= 0x0004,
            /// <summary>The middle button is down.</summary>
            MiddleDown = 0x0020,
            /// <summary>The middle button is up.</summary>
            MiddleUp = 0x0040,
            /// <summary>Movement occurred.</summary>
            Move = 0x0001,
            /// <summary>The right button is down.</summary>
            RightDown = 0x0008,
            /// <summary>The right button is up.</summary>
            RightUp = 0x0010,
            /// <summary> The wheel has been moved, if the mouse has a wheel. The amount of movement is specified in dwData.</summary>
            Wheel = 0x0800,
            /// <summary>An X button was pressed.</summary>
            XDown = 0x0080,
            /// <summary>An X button was released.</summary>
            XUp = 0x0100,
            /// <summary>The wheel button is tilted.</summary>
            WheelTilt = 0x01000
        }

        public static void InjectMouseInput(MouseButton button)
        {
            mouse_event((uint)button, 0, 0, 0, 0);
        }
        
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);

        #endregion Mouse

        #region Keyboard

        /// <summary>
        /// https://msdn.microsoft.com/de-de/library/windows/desktop/dd375731(v=vs.85).aspx
        /// </summary>
        [Flags]
        public enum KeyboardButton : uint
        {
            /// <summary>Volume Mute key</summary>
            VolumeMute = 0xAD,
            /// <summary>Volume Down key</summary>
            VolumeDown = 0xAE,
            /// <summary>Volume Up key</summary>
            VolumeUp = 0xAF,
            /// <summary>Next Track key</summary>
            NextTrack = 0xB0,
            /// <summary>Previous Track key</summary>
            PrevTrack = 0xB1,
            /// <summary>Stop Media key</summary>
            MediaStop = 0xB2,
            /// <summary>Play/Pause Media key</summary>
            MediaPlayPause = 0xB3,
        }

        /// <summary>
        /// https://msdn.microsoft.com/de-de/library/windows/desktop/ms646304(v=vs.85).aspx
        /// </summary>
        [Flags]
        private enum KeyboardInputFlag
        {
            /// <summary>If specified, the scan code was preceded by a prefix byte having the value 0xE0 (224).</summary>
            Extended = 0x0001,

            /// <summary>If specified, the key is being released. If not specified, the key is being depressed.</summary>
            Up = 0x0002,
        }
        
        public static void InjectKeyboardInput(Keys key)
        {
            keybd_event((byte)key, 0, (uint)KeyboardInputFlag.Extended, IntPtr.Zero);
            keybd_event((byte)key, 0, (uint)KeyboardInputFlag.Up, IntPtr.Zero);
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern void keybd_event(byte virtualKey, byte scanCode, uint flags, IntPtr extraInfo);

        #endregion Keyboard
    }
}
