﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;

namespace Lock
{
    
    
    class hook
    {

        [StructLayout(LayoutKind.Sequential)]
        public class HookStruck
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }
        //设置钩子
        [DllImport("user32.dll")]
        private static extern int SetWindowsHookEx(int idHook, HookHandle lpfn, IntPtr hInstance, int threadId);

        //取消钩子
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern bool UnhookWindowsHookEx(int idHook);

        //调用下一个钩子
        [DllImport("user32.dll")]
        private static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

        //获取当前线程ID
        [DllImport("kernel32.dll")]
        private static extern int GetCurrentThreadId();

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string name);

        private IntPtr _hookWindowPtr = IntPtr.Zero;

        //钩子内部处理事件委托，当捕获键盘输入时调用该委托的方法
        private delegate int HookHandle(int nCode, int wParam, IntPtr lParam);

        private const int WH_KEYBOARD_LL = 13;//键盘

        //
        public delegate void ProcessKeyHandle(HookStruck param, out bool handle);

        public event ProcessKeyHandle ClientMethod;
        
        

        //接收SetWindowsHookEx返回值
        private int _hHookValue = 0;

        //勾子程序处理事件
        private HookHandle _KeyBoardHookProcedure;

        //安装钩子
        public void InstallHook()
        {
            if (_hHookValue == 0)
            {
                _KeyBoardHookProcedure = new HookHandle(OnHookProc);
                _hookWindowPtr = GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName);
                _hHookValue = SetWindowsHookEx(WH_KEYBOARD_LL, _KeyBoardHookProcedure, _hookWindowPtr, 0);
            }
            if (_hHookValue == 0)
            {
                UnhookWindowsHookEx(_hHookValue);
            }
        }

        public void UninstallHook()
        {
            if (_hHookValue != 0)
            {
                bool ret = UnhookWindowsHookEx(_hHookValue);
                if (ret)
                {
                    _hHookValue = 0;
                }
            }
        }
        private int OnHookProc(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                HookStruck hookStruct = (HookStruck)Marshal.PtrToStructure(lParam, typeof(HookStruck));
                if (ClientMethod != null)
                {
                    bool handle=false;
                    ClientMethod(hookStruct, out handle);
                    if (handle)
                    {
                        return 1;
                    }
                }
            }
            return CallNextHookEx(_hHookValue, nCode, wParam, lParam);

        }





        //public delegate int HookProc(int nCode, int wParam, IntPtr lParam);
        //public int hHook = 0;
        //public const int WH_KEYBOARD_LL = 13;

        ////LowLevel键盘截获，如果是WH_KEYBOARD＝2，并不能对系统键盘截取，Acrobat Reader会在你截取之前获得键盘。 
        //HookProc KeyBoardHookProcedure;

        ////键盘Hook结构函数 
        //[StructLayout(LayoutKind.Sequential)]
        //public class KeyBoardHookStruct
        //{
        //    public int vkCode;
        //    public int scanCode;
        //    public int flags;
        //    public int time;
        //    public int dwExtraInfo;
        //}
        //#region DllImport
        ////设置钩子 
        //[DllImport("user32.dll")]
        //public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        //[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        ////抽掉钩子 
        //public static extern bool UnhookWindowsHookEx(int idHook);
        //[DllImport("user32.dll")]
        ////调用下一个钩子 
        //public static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

        //[DllImport("kernel32.dll")]
        //public static extern int GetCurrentThreadId();

        //[DllImport("kernel32.dll")]
        //public static extern IntPtr GetModuleHandle(string name);

        //public void Hook_Start()
        //{
        //    // 安装键盘钩子 
        //    if (hHook == 0)
        //    {
        //        KeyBoardHookProcedure = new HookProc(KeyBoardHookProc);

        //        //hHook = SetWindowsHookEx(2, 
        //        //            KeyBoardHookProcedure, 
        //        //          GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), GetCurrentThreadId()); 

        //        hHook = SetWindowsHookEx(WH_KEYBOARD_LL,
        //                  KeyBoardHookProcedure,
        //                GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), 0);

        //        //如果设置钩子失败. 
        //        if (hHook == 0)
        //        {
        //            Hook_Clear();
        //            //throw new Exception("设置Hook失败!"); 
        //        }
        //    }
        //}

        ////取消钩子事件 
        //public void Hook_Clear()
        //{
        //    bool retKeyboard = true;
        //    if (hHook != 0)
        //    {
        //        retKeyboard = UnhookWindowsHookEx(hHook);
        //        hHook = 0;
        //    }
        //    //如果去掉钩子失败. 
        //    if (!retKeyboard) throw new Exception("UnhookWindowsHookEx failed.");
        //}

        ////这里可以添加自己想要的信息处理 
        //public  int KeyBoardHookProc(int nCode, int wParam, IntPtr lParam)
        //{
        //    if (nCode >= 0)
        //    {
        //        KeyBoardHookStruct kbh = (KeyBoardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyBoardHookStruct));
        //        if (kbh.vkCode == 91)  // 截获左win(开始菜单键) 
        //        {
        //            return 1;
        //        }
        //        if (kbh.vkCode == 92)// 截获右win 
        //        {

        //            return 1;
        //        }
        //        if (kbh.vkCode == (int)Keys.Escape && (int)Control.ModifierKeys == (int)Keys.Control) //截获Ctrl+Esc 
        //        {
        //            return 1;
        //        }
        //        if (kbh.vkCode == (int)Keys.F4 && (int)Control.ModifierKeys == (int)Keys.Alt)  //截获alt+f4 
        //        {
        //            return 1;
        //        }
        //        if (kbh.vkCode == (int)Keys.Tab && (int)Control.ModifierKeys == (int)Keys.Alt) //截获alt+tab 
        //        {
        //            return 1;
        //        }
        //        if (kbh.vkCode == (int)Keys.Escape && (int)Control.ModifierKeys == (int)Keys.Control + (int)Keys.Shift) //截获Ctrl+Shift+Esc 
        //        {
        //            return 1;
        //        }
        //        if (kbh.vkCode == (int)Keys.Space && (int)Control.ModifierKeys == (int)Keys.Alt)  //截获alt+空格 
        //        {
        //            return 1;
        //        }
        //        if (kbh.vkCode == 241)                  //截获F1 
        //        {
        //            return 1;
        //        }
        //        if ((int)Control.ModifierKeys == (int)Keys.Control + (int)Keys.Alt + (int)Keys.Delete)      //截获Ctrl+Alt+Delete 
        //        {
        //            return 1;
        //        }
        //        //if ((int)Control.ModifierKeys == (int)Keys.Control + (int)Keys.Shift)      //截获Ctrl+Shift 
        //        //{ 
        //        //    return 1; 
        //        //} 

        //        //if (kbh.vkCode == (int)Keys.Space && (int)Control.ModifierKeys == (int)Keys.Control + (int)Keys.Alt)  //截获Ctrl+Alt+空格 
        //        //{ 
        //        //    return 1; 
        //        //} 
        //    }
        //    return CallNextHookEx(hHook, nCode, wParam, lParam);
        //}

        //#endregion

        //private void Form2_Load(object sender, EventArgs e)
        //{
        //    Hook_Start();

        //    //通过修改注册表来屏蔽任务管理器 
        //    //try 
        //    //{ 
        //    //    RegistryKey r = Registry.CurrentUser.OpenSubKey("Software//Microsoft//Windows//CurrentVersion//Policies//System", true); 
        //    //    r.SetValue("DisableTaskMgr", "1");  //屏蔽任务管理器 
        //    //} 
        //    //catch 
        //    //{ 
        //    //    RegistryKey r = Registry.CurrentUser.CreateSubKey("Software//Microsoft//Windows//CurrentVersion//Policies//System"); 
        //    //    r.SetValue("DisableTaskMgr", "0"); 
        //    //} 
        //} 
    }
}
