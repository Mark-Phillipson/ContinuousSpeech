using System.Diagnostics;
using System.Runtime.InteropServices;
/// <summary>
/// Hook del teclado.
/// </summary>
public class KeyboardHook : IDisposable
{
    /// <summary>
    /// Identificador del hook de teclado.
    /// </summary>
    private static IntPtr _hookId;

    /// <summary>
    /// Número de hooks asociados.
    /// </summary>
    private static readonly List<KeyboardHook> HooksObjects = new List<KeyboardHook>();

    public KeyboardHook()
    {
        OnCreateKeyboardHook(this);
    }

    public KeyboardHook(Form form)
    {
        OnCreateKeyboardHook(this);

        form.Closed += (sender, e) => this.Dispose();
    }

    /// <summary>
    /// Ocurre al pulsar una tecla.
    /// </summary>
    public event EventHandler<KeyEventArgs> KeyDown;

    /// <summary>
    /// Ocurre al soltar una tecla.
    ///  </summary>
    public event EventHandler<KeyEventArgs> KeyUp;

    public void Dispose()
    {
        HooksObjects.Remove(this);

        if (HooksObjects.Count == 0)
        {
            UnhookWindowsHookEx(_hookId);
        }
    }

    /// <summary>
    /// Invocado al crear un objeto <see cref="KeyboardHook"/>.
    /// </summary>
    /// <param name="obj">Objeto <see cref="KeyboardHook"/>.</param>
    private static void OnCreateKeyboardHook(KeyboardHook obj)
    {
        if (HooksObjects.Count == 0)
        {
            _hookId = SetHook(KeyboardProc);
        }

        HooksObjects.Add(obj);
    }

    /// <summary>
    /// Establece el hook de teclado.
    /// </summary>
    /// <param name="proc">Función a invocar cuando se pulsa una tecla.</param>
    /// <returns>El identificador del hook.</returns>
    private static IntPtr SetHook(LowLevelKeyboardProc proc)
    {
        using (var curProcess = Process.GetCurrentProcess())
        using (var curModule = curProcess.MainModule)
        {
            var modHandle = GetModuleHandle(curModule.ModuleName);
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc, modHandle, 0);
        }
    }

    /// <summary>
    /// Mantenemos una referencia para evitar una excepción.
    /// </summary>
    private static readonly LowLevelKeyboardProc KeyboardProc = HookCallback;

    private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

    #region Native

    private const int WH_KEYBOARD_LL = 13;

    private const int WM_KEYDOWN = 0x0100;

    private const int WM_KEYUP = 0x0101;

    private const int WM_SYSKEYDOWN = 0x0104;

    private const int WM_SYSKEYUP = 0x0105;

    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0)
        {
            var keyDown = wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN;
            if (keyDown || wParam == (IntPtr)WM_KEYUP || wParam == (IntPtr)WM_SYSKEYUP)
            {
                var kbd = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));

                var vkCode = kbd.vkCode;
                var key = (Keys)vkCode;

                if (keyDown)
                {
                    if ((Keys)vkCode != Keys.Alt && (Control.ModifierKeys & Keys.Alt) != 0)
                    {
                        key |= Keys.Alt;
                    }

                    if ((Keys)vkCode != Keys.Control && (Control.ModifierKeys & Keys.Control) != 0)
                    {
                        key |= Keys.Control;
                    }

                    if ((Keys)vkCode != Keys.Shift && (Control.ModifierKeys & Keys.Shift) != 0)
                    {
                        key |= Keys.Shift;
                    }
                }
                else
                {
                    var flags = kbd.flags;
                    if (flags == KBDLLHOOKSTRUCTFlags.LLKHF_ALTDOWN)
                    {
                        key = key | Keys.Alt;
                    }
                    else if (flags == 0x00)
                    {
                        key = key | Keys.Control;
                    }
                }

                var e = new KeyEventArgs(key) { Handled = false };

                foreach (var hookObject in HooksObjects)
                {
                    if (keyDown)
                    {
                        hookObject.KeyDown?.Invoke(hookObject, e);
                    }
                    else
                    {
                        hookObject.KeyUp?.Invoke(hookObject, e);
                    }
                }
            }
        }

        return CallNextHookEx(_hookId, nCode, wParam, lParam);
    }

    [StructLayout(LayoutKind.Sequential)]
    public class KBDLLHOOKSTRUCT
    {
        public uint vkCode;
        public uint scanCode;
        public KBDLLHOOKSTRUCTFlags flags;
        public uint time;
        public UIntPtr dwExtraInfo;
    }

    [Flags]
    public enum KBDLLHOOKSTRUCTFlags : uint
    {
        LLKHF_EXTENDED = 0x01,
        LLKHF_INJECTED = 0x10,
        LLKHF_ALTDOWN = 0x20,
        LLKHF_UP = 0x80,
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod,
        uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    #endregion
}