using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows.Forms;
using Grasshopper.Input;
using SharpDX.Multimedia;
using SharpDX.RawInput;
using KeyState = SharpDX.RawInput.KeyState;

namespace Grasshopper.SharpDX.Input
{
    class InputContext : IInputContext
    {
        private readonly Subject<MouseEvent> _mouseSubject = new Subject<MouseEvent>();
        private readonly Subject<KeyboardEvent> _keyboardSubject = new Subject<KeyboardEvent>();

        public InputContext()
        {
            MouseEvents = _mouseSubject.AsObservable();
            KeyboardEvents = _keyboardSubject.AsObservable();

            Device.RegisterDevice(UsagePage.Generic, UsageId.GenericMouse, DeviceFlags.None);
            Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard, DeviceFlags.None);
            Device.MouseInput += OnMouseInput;
            Device.KeyboardInput += OnKeyboardInput;
        }

        public IObservable<MouseEvent> MouseEvents { get; private set; }
        public IObservable<KeyboardEvent> KeyboardEvents { get; private set; }

        private void OnMouseInput(object sender, MouseInputEventArgs args)
        {
            PostMouseEvent(new MouseEvent(null, args.X, args.Y, 0, 0));
        }

        private void OnKeyboardInput(object sender, KeyboardInputEventArgs args)
        {
            Key key;
            switch(args.Key)
            {
                case Keys.Cancel: key = Key.Cancel; break;
                case Keys.Back: key = Key.Back; break;
                case Keys.Tab: key = Key.Tab; break;
                case Keys.Clear: key = Key.Clear; break;
                case Keys.Enter: key = Key.Enter; break;
                case Keys.ShiftKey: key = Key.ShiftKey; break;
                case Keys.ControlKey: key = Key.ControlKey; break;
                case Keys.Menu: key = Key.Menu; break;
                case Keys.Pause: key = Key.Pause; break;
                case Keys.CapsLock: key = Key.CapsLock; break;
                case Keys.Escape: key = Key.Escape; break;
                case Keys.Space: key = Key.Space; break;
                case Keys.PageUp: key = Key.PageUp; break;
                case Keys.PageDown: key = Key.PageDown; break;
                case Keys.End: key = Key.End; break;
                case Keys.Home: key = Key.Home; break;
                case Keys.Left: key = Key.Left; break;
                case Keys.Up: key = Key.Up; break;
                case Keys.Right: key = Key.Right; break;
                case Keys.Down: key = Key.Down; break;
                case Keys.Select: key = Key.Select; break;
                case Keys.PrintScreen: key = Key.PrintScreen; break;
                case Keys.Insert: key = Key.Insert; break;
                case Keys.Delete: key = Key.Delete; break;
                case Keys.Help: key = Key.Help; break;
                case Keys.D0: key = Key.D0; break;
                case Keys.D1: key = Key.D1; break;
                case Keys.D2: key = Key.D2; break;
                case Keys.D3: key = Key.D3; break;
                case Keys.D4: key = Key.D4; break;
                case Keys.D5: key = Key.D5; break;
                case Keys.D6: key = Key.D6; break;
                case Keys.D7: key = Key.D7; break;
                case Keys.D8: key = Key.D8; break;
                case Keys.D9: key = Key.D9; break;
                case Keys.A: key = Key.A; break;
                case Keys.B: key = Key.B; break;
                case Keys.C: key = Key.C; break;
                case Keys.D: key = Key.D; break;
                case Keys.E: key = Key.E; break;
                case Keys.F: key = Key.F; break;
                case Keys.G: key = Key.G; break;
                case Keys.H: key = Key.H; break;
                case Keys.I: key = Key.I; break;
                case Keys.J: key = Key.J; break;
                case Keys.K: key = Key.K; break;
                case Keys.L: key = Key.L; break;
                case Keys.M: key = Key.M; break;
                case Keys.N: key = Key.N; break;
                case Keys.O: key = Key.O; break;
                case Keys.P: key = Key.P; break;
                case Keys.Q: key = Key.Q; break;
                case Keys.R: key = Key.R; break;
                case Keys.S: key = Key.S; break;
                case Keys.T: key = Key.T; break;
                case Keys.U: key = Key.U; break;
                case Keys.V: key = Key.V; break;
                case Keys.W: key = Key.W; break;
                case Keys.X: key = Key.X; break;
                case Keys.Y: key = Key.Y; break;
                case Keys.Z: key = Key.Z; break;
                case Keys.LWin: key = Key.LWin; break;
                case Keys.RWin: key = Key.RWin; break;
                case Keys.Apps: key = Key.Apps; break;
                case Keys.Sleep: key = Key.Sleep; break;
                case Keys.NumPad0: key = Key.NumPad0; break;
                case Keys.NumPad1: key = Key.NumPad1; break;
                case Keys.NumPad2: key = Key.NumPad2; break;
                case Keys.NumPad3: key = Key.NumPad3; break;
                case Keys.NumPad4: key = Key.NumPad4; break;
                case Keys.NumPad5: key = Key.NumPad5; break;
                case Keys.NumPad6: key = Key.NumPad6; break;
                case Keys.NumPad7: key = Key.NumPad7; break;
                case Keys.NumPad8: key = Key.NumPad8; break;
                case Keys.NumPad9: key = Key.NumPad9; break;
                case Keys.Multiply: key = Key.Multiply; break;
                case Keys.Add: key = Key.Add; break;
                case Keys.Separator: key = Key.Separator; break;
                case Keys.Subtract: key = Key.Subtract; break;
                case Keys.Decimal: key = Key.Decimal; break;
                case Keys.Divide: key = Key.Divide; break;
                case Keys.F1: key = Key.F1; break;
                case Keys.F2: key = Key.F2; break;
                case Keys.F3: key = Key.F3; break;
                case Keys.F4: key = Key.F4; break;
                case Keys.F5: key = Key.F5; break;
                case Keys.F6: key = Key.F6; break;
                case Keys.F7: key = Key.F7; break;
                case Keys.F8: key = Key.F8; break;
                case Keys.F9: key = Key.F9; break;
                case Keys.F10: key = Key.F10; break;
                case Keys.F11: key = Key.F11; break;
                case Keys.F12: key = Key.F12; break;
                case Keys.F13: key = Key.F13; break;
                case Keys.F14: key = Key.F14; break;
                case Keys.F15: key = Key.F15; break;
                case Keys.F16: key = Key.F16; break;
                case Keys.F17: key = Key.F17; break;
                case Keys.F18: key = Key.F18; break;
                case Keys.F19: key = Key.F19; break;
                case Keys.F20: key = Key.F20; break;
                case Keys.F21: key = Key.F21; break;
                case Keys.F22: key = Key.F22; break;
                case Keys.F23: key = Key.F23; break;
                case Keys.F24: key = Key.F24; break;
                case Keys.NumLock: key = Key.NumLock; break;
                case Keys.Scroll: key = Key.Scroll; break;
                case Keys.LShiftKey: key = Key.LShiftKey; break;
                case Keys.RShiftKey: key = Key.RShiftKey; break;
                case Keys.LControlKey: key = Key.LControlKey; break;
                case Keys.RControlKey: key = Key.RControlKey; break;
                case Keys.LMenu: key = Key.LMenu; break;
                case Keys.RMenu: key = Key.RMenu; break;
                case Keys.BrowserBack: key = Key.BrowserBack; break;
                case Keys.BrowserForward: key = Key.BrowserForward; break;
                case Keys.BrowserRefresh: key = Key.BrowserRefresh; break;
                case Keys.BrowserStop: key = Key.BrowserStop; break;
                case Keys.BrowserSearch: key = Key.BrowserSearch; break;
                case Keys.BrowserFavorites: key = Key.BrowserFavorites; break;
                case Keys.BrowserHome: key = Key.BrowserHome; break;
                case Keys.VolumeMute: key = Key.VolumeMute; break;
                case Keys.VolumeDown: key = Key.VolumeDown; break;
                case Keys.VolumeUp: key = Key.VolumeUp; break;
                case Keys.MediaNextTrack: key = Key.MediaNextTrack; break;
                case Keys.MediaPreviousTrack: key = Key.MediaPreviousTrack; break;
                case Keys.MediaStop: key = Key.MediaStop; break;
                case Keys.MediaPlayPause: key = Key.MediaPlayPause; break;
                case Keys.LaunchMail: key = Key.LaunchMail; break;
                case Keys.SelectMedia: key = Key.SelectMedia; break;
                case Keys.LaunchApplication1: key = Key.LaunchApplication1; break;
                case Keys.LaunchApplication2: key = Key.LaunchApplication2; break;
                case Keys.OemSemicolon: key = Key.OemSemicolon; break;
                case Keys.Oemplus: key = Key.Oemplus; break;
                case Keys.Oemcomma: key = Key.Oemcomma; break;
                case Keys.OemMinus: key = Key.OemMinus; break;
                case Keys.OemPeriod: key = Key.OemPeriod; break;
                case Keys.OemQuestion: key = Key.OemQuestion; break;
                case Keys.Oemtilde: key = Key.Oemtilde; break;
                case Keys.OemOpenBrackets: key = Key.OemOpenBrackets; break;
                case Keys.OemPipe: key = Key.OemPipe; break;
                case Keys.OemCloseBrackets: key = Key.OemCloseBrackets; break;
                case Keys.OemQuotes: key = Key.OemQuotes; break;
                case Keys.Oem8: key = Key.Oem8; break;
                case Keys.OemBackslash: key = Key.OemBackslash; break;
                case Keys.ProcessKey: key = Key.ProcessKey; break;
                case Keys.Packet: key = Key.Packet; break;
                case Keys.Attn: key = Key.Attn; break;
                case Keys.Crsel: key = Key.Crsel; break;
                case Keys.Exsel: key = Key.Exsel; break;
                case Keys.EraseEof: key = Key.EraseEof; break;
                case Keys.Play: key = Key.Play; break;
                case Keys.Zoom: key = Key.Zoom; break;
                case Keys.NoName: key = Key.NoName; break;
                case Keys.Pa1: key = Key.Pa1; break;
                case Keys.OemClear: key = Key.OemClear; break;
                case Keys.Shift: key = Key.Shift; break;
                case Keys.Control: key = Key.Control; break;
                case Keys.Alt: key = Key.Alt; break;
                default: return;
            }
            
            Grasshopper.Input.KeyState state;
            switch(args.State)
            {
                case KeyState.SystemKeyUp:
                case KeyState.KeyUp:
                    state = Grasshopper.Input.KeyState.Up;
                    break;
                case KeyState.SystemKeyDown:
                case KeyState.KeyDown:
                    state = Grasshopper.Input.KeyState.Down;
                    break;
                default:
                    return;
            }
            var ev = new KeyboardEvent(key, state);
            PostKeyboardEvent(ev);
        }

        public void PostMouseEvent(MouseEvent mouseEvent)
        {
            _mouseSubject.OnNext(mouseEvent);
        }
        
        public void PostKeyboardEvent(KeyboardEvent keyboardEvent)
        {
            _keyboardSubject.OnNext(keyboardEvent);
        }

        public void Dispose()
        {
            _mouseSubject.Dispose();
        }
    }
}
