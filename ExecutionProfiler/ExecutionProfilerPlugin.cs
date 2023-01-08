using Plugin;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Plugins.ExecutionProfiler
{
    public class ExecutionProfilerPlugin : iPlugin
    {
        public const int MemorySize = 2 * 1024 * 1024;

        private static int[] _executed;
        private static bool _profile = false;
        public static bool Active = false;
        public static FormProfiler Form;

        public static int MaxValue = 0;
        public static int MinAddress = int.MaxValue;
        public static int MaxAddress = 0;

        public static int[] Executed => _executed;
        public static bool Profile
        {
            get { return _profile; }
            set 
            { 
                _profile = value; 
            }
        }
        
        private iCSpect _cspect;
        private int _lastExecute;
        private WindowWrapper _hwndWrapper;
        
        public List<sIO> Init(iCSpect _CSpect)
        {
            _cspect = _CSpect;
            IntPtr handle = (IntPtr)_cspect.GetGlobal(eGlobal.window_handle);
            _hwndWrapper = new WindowWrapper(handle);

            _executed = new int[MemorySize];
            var io = new List<sIO>();
            for (int i=0; i < MemorySize; i++)
            {
                io.Add(new sIO(i, eAccess.Memory_EXE));
            }

            io.Add(new sIO("<ctrl><alt>e", eAccess.KeyPress, 0));

            return io;
        }

        public bool KeyPressed(int _id)
        {
            if (_id == 0)
            {
                if (Active) return true;
                Form = new FormProfiler(_cspect);
                Form.Show();
                return true;
            }
            
            return false;
        }

        public void Quit()
        {

        }

        public byte Read(eAccess _type, int _port, int _id, out bool _isvalid)
        {
            if (_type == eAccess.Memory_EXE)
            {
                _lastExecute = _port;
            }
            _isvalid = false;
            return 0;
        }

        public void Reset()
        {

        }

        public void Tick()
        {
            if (_profile)
            {
                _executed[_lastExecute]++;
                if (_executed[_lastExecute] > MaxValue) MaxValue = _executed[_lastExecute];
                if (_lastExecute < MinAddress) MinAddress = _lastExecute;
                if (_lastExecute > MaxAddress) MaxAddress = _lastExecute;                
            }
        }

        public bool Write(eAccess _type, int _port, int _id, byte _value)
        {
            
            return false;
        }
    }

    class WindowWrapper : IWin32Window
    {
        private IntPtr _WindowHandle;
        public IntPtr Handle
        {
            get { return _WindowHandle; }
        }

        public WindowWrapper(IntPtr _handle)
        {
            _WindowHandle = _handle;
        }
    }
}
