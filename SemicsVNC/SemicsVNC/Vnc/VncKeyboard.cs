using SemicsVNC.Utility;
using System;

namespace SemicsVNC.Vnc.Server
{
    /// <summary>
    /// This class can be used to add Windows mouse functionality to the VNC server part.
    /// </summary>
    ///
    [Flags]
    public enum WinApiKeyEventFlags : byte 
    {
        /// <summary>
        /// KEYDOWN
        /// </summary>
        KEYDOWN = 0,

        /// <summary>
        /// KEYPRESS
        /// </summary>
        KEYPRESS = 1,

        /// <summary>
        /// KEYUP
        /// </summary>
        KEYUP = 2
    }

    /// <summary>
    /// This class can be used to add Windows mouse functionality to the VNC server part.
    /// </summary>
    public class VncKeyboard
    {
        /*
         * The way VNC clients send the mouse state to the server is not directly compatible
         * with the way Windows mouse control works through it's API. VNC uses the X11
         * convention where the state of the mouse is send (i.e. left mouse button IS down)
         * rather then sending the changes in the state of the mouse (i.e. left mouse button
         * changed from unpressed to pressed) which is the way the Windows API works. Therefor
         * the field X11MouseState is used to keep track of the state to detect changes.
         */

        /// <summary>
        /// Initializes a new instance of the <see cref="VncKeyboard"/> class.
        /// Class that provides virtual mouse functionality to the VNC server.
        /// </summary>
        public VncKeyboard()
        {
        }

        /// <summary>A
        /// Callback function for mouse updates.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        public void OnKeyboardUpdate(object sender, KeyChangedEventArgs e)
        {
            KeySym newState = e.Keysym;
            /* 키보드 모든 키 */
            if (e.Pressed)
            {
                switch (newState)
                {
                    // 상수 : bvk파라메터 

                    case KeySym.A:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.B:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.C:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.D:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.E:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.F:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.G:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.H:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.I:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.J:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.K:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.L:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.M:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.N:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.O:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.P:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Q:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.R:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.S:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.T:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.U:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.V:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.W:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.X:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Y:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Z:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.a:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.b:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.c:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.d:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.e:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.f:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.g:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.h:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.i:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.j:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.k:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.l:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.m:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.n:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.o:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.p:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.q:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.r:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.s:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.t:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.u:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.v:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.w:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.x:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.y:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.z:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Backspace:
                        User32.Keybd_event(8, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Tab:
                        User32.Keybd_event(9, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.LineFeed:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Clear:
                        User32.Keybd_event(12, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Return:
                        User32.Keybd_event(13, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Pause:
                        User32.Keybd_event(19, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.ScrollLock:
                        User32.Keybd_event(145, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.SysReq:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Escape:
                        User32.Keybd_event(27, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Delete:
                        User32.Keybd_event(46, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Home:
                        User32.Keybd_event(36, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Left:
                        User32.Keybd_event(37, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Up:
                        User32.Keybd_event(38, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Right:
                        User32.Keybd_event(39, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Down:
                        User32.Keybd_event(40, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.PageUp:
                        User32.Keybd_event(33, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.PageDown:
                        User32.Keybd_event(34, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.End:
                        User32.Keybd_event(35, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Begin:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Select:
                        User32.Keybd_event(41, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Print:
                        User32.Keybd_event(44, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Execute:
                        User32.Keybd_event(43, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Insert:
                        User32.Keybd_event(45, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Undo:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Redo:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Menu:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Find:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Cancel:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Help:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Break:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.ModeSwitch:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Num_Lock:
                        User32.Keybd_event(144, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPadSpace:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPadTab:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPadEnter:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPadF1:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPadF2:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPadF3:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPadF4:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPadHome:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPadLeft:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPadUp:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPadRight:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPadDown:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPadPageUp:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPadPageDown:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPadEnd:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPadBegin:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPadInsert:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPadDelete:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPadEqual:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPadMultiply:
                        User32.Keybd_event(106, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPadAdd:
                        User32.Keybd_event(107, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPadSeparator:
                        User32.Keybd_event(108, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPadSubtract:
                        User32.Keybd_event(109, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPadDecimal:
                        User32.Keybd_event(110, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPadDivide:
                        User32.Keybd_event(111, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPad0:
                        User32.Keybd_event(96, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPad1:
                        User32.Keybd_event(97, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPad2:
                        User32.Keybd_event(98, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPad3:
                        User32.Keybd_event(99, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPad4:
                        User32.Keybd_event(100, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPad5:
                        User32.Keybd_event(101, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPad6:
                        User32.Keybd_event(102, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPad7:
                        User32.Keybd_event(103, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPad8:
                        User32.Keybd_event(104, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumPad9:
                        User32.Keybd_event(105, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.F1:
                        User32.Keybd_event(112, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.F2:
                        User32.Keybd_event(113, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.F3:
                        User32.Keybd_event(114, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.F4:
                        User32.Keybd_event(115, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.F5:
                        User32.Keybd_event(116, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.F6:
                        User32.Keybd_event(117, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.F7:
                        User32.Keybd_event(118, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.F8:
                        User32.Keybd_event(119, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.F9:
                        User32.Keybd_event(120, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.F10:
                        User32.Keybd_event(121, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.F11:
                        User32.Keybd_event(122, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.F12:
                        User32.Keybd_event(123, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.F13:
                        User32.Keybd_event(124, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.F14:
                        User32.Keybd_event(125, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.F15:
                        User32.Keybd_event(126, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.F16:
                        User32.Keybd_event(127, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.F17:
                        User32.Keybd_event(128, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.F18:
                        User32.Keybd_event(129, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.F19:
                        User32.Keybd_event(130, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.F20:
                        User32.Keybd_event(131, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.F21:
                        User32.Keybd_event(132, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.F22:
                        User32.Keybd_event(133, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.F23:
                        User32.Keybd_event(134, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.F24:
                        User32.Keybd_event(135, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.ShiftLeft:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.ShiftRight:     // 왼쪽 쉬프트
                        User32.Keybd_event(16, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.ControlLeft:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.ControlRight:   // 왼쪽 컨트롤
                        User32.Keybd_event(17, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.CapsLock:
                        User32.Keybd_event(20, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.ShiftLock:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.MetaLeft:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.MetaRight:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.AltLeft:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.AltRight:
                        User32.Keybd_event(18, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.SuperLeft:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.SuperRight:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.HyperLeft:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.HyperRight:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Space:
                        User32.Keybd_event(32, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Exclamation:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Quote:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.NumberSign:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Dollar:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Percent:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Ampersand:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Apostrophe:
                        User32.Keybd_event(222, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Plus:
                        User32.Keybd_event(187, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Comma:
                        User32.Keybd_event(188, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Minus:
                        User32.Keybd_event(189, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Period:
                        User32.Keybd_event(190, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Slash:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.D0:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.D1:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.D2:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.D3:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.D4:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.D5:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.D6:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.D7:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.D8:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.D9:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Colon:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Semicolon:
                        User32.Keybd_event(186, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Less:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Equal:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Greater:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Question:
                        User32.Keybd_event(191, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.At:
                        User32.Keybd_event((uint)newState, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Hangul:
                        User32.Keybd_event(21, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Hanja:
                        User32.Keybd_event(25, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.AsciiTilde: //물결표시
                        User32.Keybd_event(192, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.BracketLeft://대괄호 열기
                        User32.Keybd_event(219, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Bracketright: //대괄호 닫기
                        User32.Keybd_event(221, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                    case KeySym.Backslash:    //역슬래시
                        User32.Keybd_event(220, 0, (int)WinApiKeyEventFlags.KEYDOWN, 0);
                        break;
                }
            }
            else
            {
                switch(newState)    //keyup
                {
                    case KeySym.ShiftRight:
                        User32.Keybd_event(16, 0, (int)(WinApiKeyEventFlags.KEYUP | WinApiKeyEventFlags.KEYDOWN), 0);
                        break;
                    case KeySym.ControlRight:
                        User32.Keybd_event(17, 0, (int)(WinApiKeyEventFlags.KEYUP | WinApiKeyEventFlags.KEYDOWN), 0);
                        break;
                    case KeySym.AltRight:
                        User32.Keybd_event(18, 0, (int)(WinApiKeyEventFlags.KEYUP | WinApiKeyEventFlags.KEYDOWN), 0);
                        break;
                    case KeySym.CapsLock:
                        User32.Keybd_event(20, 0, (int)(WinApiKeyEventFlags.KEYUP | WinApiKeyEventFlags.KEYDOWN), 0);
                        break;
                }
            }
        }
    }
}
