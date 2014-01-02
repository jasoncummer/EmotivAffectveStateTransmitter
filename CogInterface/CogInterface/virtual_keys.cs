using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;
using System.Threading;

namespace CogInterface
{


    class virtual_keys
    {
        

        const int KEYEVENTF_EXTENDEDKEY = 0x1;
        const int KEYEVENTF_KEYUP = 0x2;
        const int VK_LBUTTON = 0x1;
        const int VK_RBUTTON = 0x2;
        const int VK_CANCEL = 0x3;
        const int VK_MBUTTON = 0x4;
        const int VK_BACK = 0x8;
        const int VK_TAB = 0x9;
        const int VK_CLEAR = 0xC;
        const int VK_RETURN = 0xD;
        const int VK_SHIFT = 0x10;
        const int VK_CONTROL = 0x11;
        const int VK_MENU = 0x12;
        const int VK_PAUSE = 0x13;
        const int VK_CAPITAL = 0x14;
        const int VK_ESCAPE = 0x1B;
        const int VK_SPACE = 0x20;
        const int VK_PRIOR = 0x21;
        const int VK_NEXT = 0x22;
        const int VK_END = 0x23;
        const int VK_HOME = 0x24;
        const int VK_LEFT = 0x25;
        const int VK_UP = 0x26;
        const int VK_RIGHT = 0x27;
        const int VK_DOWN = 0x28;
        const int VK_SELECT = 0x29;
        const int VK_PRINT = 0x2A;
        const int VK_EXECUTE = 0x2B;
        const int VK_SNAPSHOT = 0x2C;
        const int VK_INSERT = 0x2D;
        const int VK_DELETE = 0x2E;
        const int VK_HELP = 0x2F;
        const int VK_0 = 0x30;
        const int VK_1 = 0x31;
        const int VK_2 = 0x32;
        const int VK_3 = 0x33;
        const int VK_4 = 0x34;
        const int VK_5 = 0x35;
        const int VK_6 = 0x36;
        const int VK_7 = 0x37;
        const int VK_8 = 0x38;
        const int VK_9 = 0x39;
        const int VK_A = 0x41;
        const int VK_B = 0x42;
        const int VK_C = 0x43;
        const int VK_D = 0x44;
        const int VK_E = 0x45;
        const int VK_F = 0x46;
        const int VK_G = 0x47;
        const int VK_H = 0x48;
        const int VK_I = 0x49;
        const int VK_J = 0x4A;
        const int VK_K = 0x4B;
        const int VK_L = 0x4C;
        const int VK_M = 0x4D;
        const int VK_N = 0x4E;
        const int VK_O = 0x4F;
        const int VK_P = 0x50;
        const int VK_Q = 0x51;
        const int VK_R = 0x52;
        const int VK_S = 0x53;
        const int VK_T = 0x54;
        const int VK_U = 0x55;
        const int VK_V = 0x56;
        const int VK_W = 0x57;
        const int VK_X = 0x58;
        const int VK_Y = 0x59;
        const int VK_Z = 0x5A;
        const int VK_STARTKEY = 0x5B;
        const int VK_CONTEXTKEY = 0x5D;
        const int VK_NUMPAD0 = 0x60;
        const int VK_NUMPAD1 = 0x61;
        const int VK_NUMPAD2 = 0x62;
        const int VK_NUMPAD3 = 0x63;
        const int VK_NUMPAD4 = 0x64;
        const int VK_NUMPAD5 = 0x65;
        const int VK_NUMPAD6 = 0x66;
        const int VK_NUMPAD7 = 0x67;
        const int VK_NUMPAD8 = 0x68;
        const int VK_NUMPAD9 = 0x69;
        const int VK_MULTIPLY = 0x6A;
        const int VK_ADD = 0x6B;
        const int VK_SEPARATOR = 0x6C;
        const int VK_SUBTRACT = 0x6D;
        const int VK_DECIMAL = 0x6E;
        const int VK_DIVIDE = 0x6F;
        const int VK_F1 = 0x70;
        const int VK_F2 = 0x71;
        const int VK_F3 = 0x72;
        const int VK_F4 = 0x73;
        const int VK_F5 = 0x74;
        const int VK_F6 = 0x75;
        const int VK_F7 = 0x76;
        const int VK_F8 = 0x77;
        const int VK_F9 = 0x78;
        const int VK_F10 = 0x79;
        const int VK_F11 = 0x7A;
        const int VK_F12 = 0x7B;
        const int VK_F13 = 0x7C;
        const int VK_F14 = 0x7D;
        const int VK_F15 = 0x7E;
        const int VK_F16 = 0x7F;
        const int VK_F17 = 0x80;
        const int VK_F18 = 0x81;
        const int VK_F19 = 0x82;
        const int VK_F20 = 0x83;
        const int VK_F21 = 0x84;
        const int VK_F22 = 0x85;
        const int VK_F23 = 0x86;
        const int VK_F24 = 0x87;
        const int VK_NUMLOCK = 0x90;
        const int VK_OEM_SCROLL = 0x91;
        const int VK_OEM_1 = 0xBA;
        const int VK_OEM_PLUS = 0xBB;
        const int VK_OEM_COMMA = 0xBC;
        const int VK_OEM_MINUS = 0xBD;
        const int VK_OEM_PERIOD = 0xBE;
        const int VK_OEM_2 = 0xBF;
        const int VK_OEM_3 = 0xC0;
        const int VK_OEM_4 = 0xDB;
        const int VK_OEM_5 = 0xDC;
        const int VK_OEM_6 = 0xDD;
        const int VK_OEM_7 = 0xDE;
        const int VK_OEM_8 = 0xDF;
        const int VK_ICO_F17 = 0xE0;
        const int VK_ICO_F18 = 0xE1;
        const int VK_OEM102 = 0xE2;
        const int VK_ICO_HELP = 0xE3;
        const int VK_ICO_00 = 0xE4;
        const int VK_ICO_CLEAR = 0xE6;
        const int VK_OEM_RESET = 0xE9;
        const int VK_OEM_JUMP = 0xEA;
        const int VK_OEM_PA1 = 0xEB;
        const int VK_OEM_PA2 = 0xEC;
        const int VK_OEM_PA3 = 0xED;
        const int VK_OEM_WSCTRL = 0xEE;
        const int VK_OEM_CUSEL = 0xEF;
        const int VK_OEM_ATTN = 0xF0;
        const int VK_OEM_FINNISH = 0xF1;
        const int VK_OEM_COPY = 0xF2;
        const int VK_OEM_AUTO = 0xF3;
        const int VK_OEM_ENLW = 0xF4;
        const int VK_OEM_BACKTAB = 0xF5;
        const int VK_ATTN = 0xF6;
        const int VK_CRSEL = 0xF7;
        const int VK_EXSEL = 0xF8;
        const int VK_EREOF = 0xF9;
        const int VK_PLAY = 0xFA;
        const int VK_ZOOM = 0xFB;
        const int VK_NONAME = 0xFC;
        const int VK_PA1 = 0xFD;
        const int VK_OEM_CLEAR = 0xFE;

        //[DllImport("user32.dll")]
        [DllImport("user32.dll")]
        public static extern void keybd_event(Byte bVk, Byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        //Declare Sub keybd_event Lib "user32.dll" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As Long)

        public static void sendZero()
        {
            // Type the VK_NUMPAD_0 key.
            keybd_event(VK_NUMPAD0, 0, 0, (UIntPtr)0);  // press 0
            Thread.Sleep(500);
            keybd_event(VK_NUMPAD0, 0, KEYEVENTF_KEYUP, (UIntPtr)0);  // release 0
        }

        public static void sendOne()
        {
            // Type the VK_NUMPAD_1 key.
            keybd_event(VK_NUMPAD1, 0, 0, (UIntPtr)0);  // press 0
            Thread.Sleep(500);
            keybd_event(VK_NUMPAD1, 0, KEYEVENTF_KEYUP, (UIntPtr)0);  // release 0
        }

        public static void sendTwo()
        {
            // Type the VK_NUMPAD_2 key.
            keybd_event(VK_NUMPAD2, 0, 0, (UIntPtr)0);  // press 0
            Thread.Sleep(500);
            keybd_event(VK_NUMPAD2, 0, KEYEVENTF_KEYUP, (UIntPtr)0);  // release 0
        }

        public static void sendThree()
        {
            // Type the VK_NUMPAD_3 key.
            keybd_event(VK_NUMPAD3, 0, 0, (UIntPtr)0);  // press 0
            Thread.Sleep(500);
            keybd_event(VK_NUMPAD3, 0, KEYEVENTF_KEYUP, (UIntPtr)0);  // release 0

        }

        public static void sendFour()
        {
            // Type the VK_NUMPAD_4 key.
            keybd_event(VK_NUMPAD4, 0, 0, (UIntPtr)0);  // press 0
            Thread.Sleep(500);
            keybd_event(VK_NUMPAD4, 0, KEYEVENTF_KEYUP, (UIntPtr)0);  // release 0
        }

        public static void sendFive()
        {
            // Type the VK_NUMPAD_5 key.
            keybd_event(VK_NUMPAD5, 0, 0, (UIntPtr)0);  // press 0
            Thread.Sleep(500);
            keybd_event(VK_NUMPAD5, 0, KEYEVENTF_KEYUP, (UIntPtr)0);  // release 0
        }

        public static void sendSix()
        {
            // Type the VK_NUMPAD_6 key.
            keybd_event(VK_NUMPAD6, 0, 0, (UIntPtr)0);  // press 0
            Thread.Sleep(500);
            keybd_event(VK_NUMPAD6, 0, KEYEVENTF_KEYUP, (UIntPtr)0);  // release 0
        }

        public static void sendSeven()
        {
            // Type the VK_NUMPAD_7 key.
            keybd_event(VK_NUMPAD7, 0, 0, (UIntPtr)0);  // press 0
            Thread.Sleep(500);
            keybd_event(VK_NUMPAD7, 0, KEYEVENTF_KEYUP, (UIntPtr)0);  // release 0
        }

        public static void sendEight()
        {
            // Type the VK_NUMPAD_8 key.
            keybd_event(VK_NUMPAD8, 0, 0, (UIntPtr)0);  // press 0
            Thread.Sleep(500);
            keybd_event(VK_NUMPAD8, 0, KEYEVENTF_KEYUP, (UIntPtr)0);  // release 0
        }

        public static void sendNine()
        {
            // Type the VK_NUMPAD_9 key.
             
        }
           
        
    }

    // might be useful: http://bytes.com/topic/c-sharp/answers/252956-user32-dll

}
