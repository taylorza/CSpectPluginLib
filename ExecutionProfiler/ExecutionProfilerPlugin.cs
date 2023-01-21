using Plugin;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Plugins.ExecutionProfiler
{
    public enum ProfileMode { None, Sample10ms, SamplePerFrame, EveryExecution };

    public class ExecutionProfilerPlugin : iPlugin
    {
        public const int MemorySize = 2 * 1024 * 1024;
        public ExecutionProfilerPlugin() { }
        private static long _lastExecute;
        private static int[] _executed;
        private static ProfileMode _mode = ProfileMode.None;
        public static bool Active = false;
        public static FormProfiler Form;

        bool OpenExecutionProfiler = false;

        public static int MinAddress = int.MaxValue;
        public static int MaxAddress = 0;

        public static int[] Executed => _executed;
        public static ProfileMode Mode
        {
            get { return _mode; }
            set 
            { 
                _mode = value; 
            }
        }

        public static void TakeSample()
        {
            var address = Interlocked.Read(ref _lastExecute);
            Interlocked.Increment(ref _executed[address]);
            
            if (address < MinAddress) MinAddress = (int)address;
            if (address > MaxAddress) MaxAddress = (int)address;
        }
        
        private iCSpect _cspect;
        
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
                OpenExecutionProfiler = true;
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
                Interlocked.Exchange(ref _lastExecute, _port);
                if (_mode == ProfileMode.EveryExecution)
                {
                    _executed[_port]++;
                    if (_port < MinAddress) MinAddress = _port;
                    if (_port > MaxAddress) MaxAddress = _port;
                }
            }
            _isvalid = false;
            return 0;
        }

        public void Reset()
        {

        }

        public void Tick()
        {
            if (_mode == ProfileMode.SamplePerFrame)
            {
                _executed[_lastExecute]++;
                if (_lastExecute < MinAddress) MinAddress = (int)_lastExecute;
                if (_lastExecute > MaxAddress) MaxAddress = (int)_lastExecute;                
            }
        }

        public void OSTick()
        {
            if (OpenExecutionProfiler)
            {
                OpenExecutionProfiler = false;
                if (!Active)
                {
                    Active = true;
                    Form = new FormProfiler(_cspect);
                    Form.Show();
                }
            }
        }

        public bool Write(eAccess _type, int _port, int _id, byte _value)
        {
            
            return false;
        }
    }

    class WindowWrapper : IWin32Window
    {
        private readonly IntPtr _WindowHandle;
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
