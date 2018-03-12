using System;

namespace POC
{
    class Program
    {

        //    private const int WH_KEYBOARD_LL = 13;
        //    private const int WM_KEYDOWN = 0x0100;
        //    private static LowLevelKeyboardProc _proc = HookCallback;
        //    private static IntPtr _hookID = IntPtr.Zero;

        static void Main(string[] args)
        {
           // _hookID = SetHook(_proc);
       //     Application.Run();
       //     UnhookWindowsHookEx(_hookID);

            //var controller = new Controller();
            //controller.SetupKeyboardHooks();
            //while (true)
            //{
            //    Thread.Sleep(10);
            //}
        }

        //    private static IntPtr SetHook(LowLevelKeyboardProc proc)
        //    {
        //        using (Process curProcess = Process.GetCurrentProcess())
        //        using (ProcessModule curModule = curProcess.MainModule)
        //        {
        //            return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
        //                GetModuleHandle(curModule.ModuleName), 0);
        //        }
        //    }

        //    private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        //    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        //    {
        //        if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
        //        {
        //            int vkCode = Marshal.ReadInt32(lParam);
        //            Console.WriteLine((Keys)vkCode);
        //        }

        //        return CallNextHookEx(_hookID, nCode, wParam, lParam);
        //    }

        //    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //    private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        //    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //    [return: MarshalAs(UnmanagedType.Bool)]
        //    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        //    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        //    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //    private static extern IntPtr GetModuleHandle(string lpModuleName);
    }


    public class Controller : IDisposable
    {
        private GlobalKeyboardHook _globalKeyboardHook;

        public void SetupKeyboardHooks()
        {
            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += OnKeyPressed;
        }

        private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            //Debug.WriteLine(e.KeyboardData.VirtualCode);

            if (e.KeyboardData.VirtualCode != GlobalKeyboardHook.VkSnapshot)
                return;

            // seems, not needed in the life.
            //if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.SysKeyDown &&
            //    e.KeyboardData.Flags == GlobalKeyboardHook.LlkhfAltdown)
            //{
            //    MessageBox.Show("Alt + Print Screen");
            //    e.Handled = true;
            //}
            //else

            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
            {
                Console.WriteLine("Print Screen");
                e.Handled = true;
            }
        }

        public void Dispose()
        {
            _globalKeyboardHook?.Dispose();
        }
    }
}

