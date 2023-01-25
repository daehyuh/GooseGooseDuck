using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;


namespace GooseGooseDuck
{
    internal class GetMemory
    {
        public Process process = new Process();

        string process_name = "Goose Goose Duck";
        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int MEM_COMMIT = 0x00001000;
        const int PAGE_READONLY = 0x04;
        const int PROCESS_WM_READ = 0x0010;
        const int PROCESS_VM_READ = 0x10;
        const int PROCESS_VM = 0x008;
        const int PROCESS_ALL_ACCESS = 0x1F0FFF;
        const int MAXIMUM_ALLOWED = 0x2000000;

        public ulong base_adress;
        public ulong UnityPlayer;
        public ulong GameAssembly;

        IntPtr processHandle;

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, ulong lpBaseAddress, byte[] lpBuffer, ulong dwSize, ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(int hProcess, ulong lpBaseAddress, byte[] lpBuffer, ulong dwSize, ref int lpNumberOfBytesWritten);

        public void get_handel()
        {
            process = Process.GetProcessesByName(process_name)[0];
            processHandle = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_WM_READ | PROCESS_ALL_ACCESS, false, process.Id);
            base_adress = (ulong)process.MainModule.BaseAddress;
            int index = 0;
            foreach (ProcessModule a in process.Modules)
            {
                if (a.ModuleName == "UnityPlayer.dll")
                {
                    UnityPlayer = (ulong)process.Modules[index].BaseAddress;
                    //break;
                }
                if (a.ModuleName == "GameAssembly.dll")
                {
                    GameAssembly = (ulong)process.Modules[index].BaseAddress;
                    //break;
                }
                index++;
            }
            ;
        }
        public byte[] get_memory_byte_arr(ulong adress, int byte_size) // sign_flag(0 = sign, 1 = unsign)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[byte_size];

            ReadProcessMemory((int)processHandle, adress, buffer, (ulong)buffer.Length, ref bytesRead);

            return buffer;
        }

        public ulong get_memory_ulong(ulong adress, int byte_size) // sign_flag(0 = sign, 1 = unsign)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[byte_size];

            ReadProcessMemory((int)processHandle, adress, buffer, (ulong)buffer.Length, ref bytesRead);
            return (ulong)BitConverter.ToInt64(buffer, 0);
        }
        public long get_memory_long(ulong adress, int byte_size) // sign_flag(0 = sign, 1 = unsign)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[byte_size];

            ReadProcessMemory((int)processHandle, adress, buffer, (ulong)buffer.Length, ref bytesRead);
            return (long)BitConverter.ToInt32(buffer, 0);
        }

        public float get_memory_float(ulong adress, int byte_size) // sign_flag(0 = sign, 1 = unsign)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[byte_size];

            ReadProcessMemory((int)processHandle, adress, buffer, (ulong)buffer.Length, ref bytesRead);
            return (float)BitConverter.ToSingle(buffer, 0);
        }

        internal string put_memory(ulong v)
        {
            throw new NotImplementedException();
        }

        public void put_memory(ulong adress, byte[] buffer)
        {
            int bytesRead = buffer.Length;

            WriteProcessMemory((int)processHandle, adress, buffer, (ulong)buffer.Length, ref bytesRead);
        }

        internal void put_memory(object p)
        {
            throw new NotImplementedException();
        }
    }

}
