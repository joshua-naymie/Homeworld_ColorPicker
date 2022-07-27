using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Homeworld_ColorPicker.IO
{
    public class BigExtractor
    {
        private const
        string ARCHIVE_ARGS_FORMAT = "-a \"{0}{1}\" -e \"{2}\"",
               ARCHIVE_PATH_FORMAT = "{0}{1}";

        private const
        string FILE_HW2_BIG_PATH = @"\Data\HW2Campaign.big",
               KERNEL_32_DLL = "kernel32.dll";

        private const
        int CTLR_C_EVENT = 0;

        //CTRL+C REQUIREMENTS
        //----------------------------------------

        [Flags]
        public enum ThreadAccess : int
        {
            TERMINATE = (0x0001),
            SUSPEND_RESUME = (0x0002),
            GET_CONTEXT = (0x0008),
            SET_CONTEXT = (0x0010),
            SET_INFORMATION = (0x0020),
            QUERY_INFORMATION = (0x0040),
            SET_THREAD_TOKEN = (0x0080),
            IMPERSONATE = (0x0100),
            DIRECT_IMPERSONATION = (0x0200)
        }

        [DllImport(KERNEL_32_DLL)]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport(KERNEL_32_DLL)]
        static extern uint SuspendThread(IntPtr hThread);
        [DllImport(KERNEL_32_DLL)]
        static extern int ResumeThread(IntPtr hThread);
        [DllImport(KERNEL_32_DLL, CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool CloseHandle(IntPtr handle);

        [DllImport(KERNEL_32_DLL)]
        private static extern
        bool GenerateConsoleCtrlEvent(uint dwCtrlEvent, uint dwProcessGroupId);

        [DllImport(KERNEL_32_DLL, SetLastError = true)]
        private static extern
        bool AttachConsole(uint dwProcessId);

        [DllImport(KERNEL_32_DLL, SetLastError = true, ExactSpelling = true)]
        private static extern
        bool FreeConsole();

        [DllImport(KERNEL_32_DLL)]
        private static extern
        bool SetConsoleCtrlHandler(ConsoleCtrlDelegate HandlerRoutine, bool Add);

        delegate Boolean ConsoleCtrlDelegate(uint CtrlType);

        //----------------------------------------

        private
        string homeworldRoot,
               toolkitRoot,
               extractDirectory;

        private
        Action<string> textOutput;

        private
        System.Diagnostics.Process extractor;

        /// <summary>
        /// Constructor for BigExtractor.
        /// </summary>
        /// <param name="homeworldRoot">The path to the homeworld root directory</param>
        /// <param name="toolkitRoot">The path to the toolkit root directory</param>
        /// <param name="extractDirectory">The path to the directory to extract the .big file contents to</param>
        /// <param name="textOutput">The method to pass any text output from the Archive.exe process</param>
        public BigExtractor(string homeworldRoot, string toolkitRoot, string extractDirectory, Action<string> textOutput)
        {
            this.homeworldRoot = homeworldRoot;
            this.toolkitRoot = toolkitRoot;
            this.extractDirectory = extractDirectory;

            this.textOutput = textOutput;

            extractor = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = String.Format(ARCHIVE_PATH_FORMAT, toolkitRoot, GC.FILE_ARCHIVE_EXE_PATH),
                    Arguments = String.Format(ARCHIVE_ARGS_FORMAT, homeworldRoot, FILE_HW2_BIG_PATH, extractDirectory),
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                }
            };
        }

        /// <summary>
        /// Starts extracting the requested .big file.
        /// Shows a MessageBox if an error occurs.
        /// </summary>
        public void ExtractBigFile()
        {
            try
            {
                extractor.Start();
                while (!extractor.StandardOutput.EndOfStream)
                {
                    textOutput.Invoke(extractor.StandardOutput.ReadLine());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occured during extraction.\n\n" + e.Message);
            }
        }

        /// <summary>
        /// Suspends all threads of the extraction process.
        /// </summary>
        public void SuspendExtraction()
        {
            if(extractor != null)
            {
                foreach(ProcessThread thread in extractor.Threads)
                {
                    IntPtr threadHandle = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                    
                    //if(pOpenThread != IntPtr.Zero)
                    //{
                    //    System.Diagnostics.Debug.WriteLine("NOT FOUND");
                    //    continue;
                    //}
                    //System.Diagnostics.Debug.WriteLine("FOUND");

                    SuspendThread(threadHandle);
                    CloseHandle(threadHandle);
                }
            }
        }

        /// <summary>
        /// Resumes all threads of the extraction process.
        /// </summary>
        public void ResumeExtraction()
        {
            if(extractor != null)
            {
                foreach (ProcessThread thread in extractor.Threads)
                {
                    IntPtr threadHandle = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);

                    int i = 0;

                    do
                    {
                        i = ResumeThread(threadHandle);
                    } while (i > 0);

                    CloseHandle(threadHandle);
                }
            }
        }

        /// <summary>
        /// Sends a Ctrl+C command to the Archive.exe extraction process.
        /// </summary>
        public void CancelExtraction()
        {
            if(extractor != null)
            {
                if (AttachConsole((uint)extractor.Id))
                {
                    SetConsoleCtrlHandler(null, true);
                    try
                    {
                        if(!GenerateConsoleCtrlEvent(CTLR_C_EVENT, 0))
                        {
                            MessageBox.Show("Could not stop extraction!");
                        }
                        extractor.WaitForExit();
                    }
                    finally
                    {
                        SetConsoleCtrlHandler(null, false);
                        FreeConsole();
                    }
                }
            }
        }

        /// <summary>
        /// Kills the Archive.exe process.
        /// </summary>
        public void KillProcess()
        {
            if (extractor != null && !extractor.HasExited)
            {
                extractor.Kill();
            }
        }
    }
}