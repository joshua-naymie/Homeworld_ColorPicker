using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Homeworld_ColorPicker.IO
{
    using Objects;

    /// <summary>
    /// Handles the process for executing the Archive.exe toolkit program.
    /// </summary>
    public sealed class BigExtractor
    {
        // IMPORTS
        //----------------------------------------

        [Flags]
        public enum ThreadAccess : int
        {
                       TERMINATE = 0x0001,
                  SUSPEND_RESUME = 0x0002,
                     GET_CONTEXT = 0x0008,
                     SET_CONTEXT = 0x0010,
                 SET_INFORMATION = 0x0020,
               QUERY_INFORMATION = 0x0040,
                SET_THREAD_TOKEN = 0x0080,
                     IMPERSONATE = 0x0100,
            DIRECT_IMPERSONATION = 0x0200
        }

        // thread management
        [DllImport(KERNEL_32_DLL)]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport(KERNEL_32_DLL)]
        static extern uint SuspendThread(IntPtr hThread);
        [DllImport(KERNEL_32_DLL)]
        static extern int ResumeThread(IntPtr hThread);
        [DllImport(KERNEL_32_DLL, CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool CloseHandle(IntPtr handle);

        // console control (Ctrl+C)
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

        // CONSTANTS
        //----------------------------------------

        private const
        string ARCHIVE_ARGS_FORMAT = "-a \"{0}{1}\" -e \"{2}\"",
               ARCHIVE_PATH_FORMAT = "{0}{1}";

        private const
        string FILE_HW2_RM_BIG_PATH = @"\Data\HW2Campaign.big",
               FILE_HW1_RM_BIG_PATH = @"\Data\HW1Campaign.big",
               KERNEL_32_DLL = "kernel32.dll";

        private const
        int CTLR_C_EVENT = 0;

        // INSTANCE
        //----------------------------------------

        /// <summary>
        /// The method to pass any output from the Archive.exe process.
        /// </summary>
        private
        Action<string> textOutputMethod;

        /// <summary>
        /// The process for executing the Archive.exe file.
        /// </summary>
        private
        System.Diagnostics.Process extractor;

        /// <summary>
        /// Whether the process has been started or not.
        /// Check this before killing the process.
        /// </summary>
        public
        bool HasStarted { get; private set; } = false;

        // CONSTRUCTOR
        //----------------------------------------

        /// <summary>
        /// Constructor for BigExtractor.
        /// </summary>
        /// <param name="instance">The game instance to work on</param>
        /// <param name="textOutputMethod">The method to pass any text output from the Archive.exe process</param>
        /// <exception cref="Exceptions.InvalidRemasteredGameException">Thrown if the Remastered game is not supported or invalid</exception>
        /// <exception cref="NotImplementedException">Thrown if the Homeworld version is not supported or invalid</exception>
        public BigExtractor(GameInstance instance, Action<string> textOutputMethod)
        {
            this.textOutputMethod = textOutputMethod;

            string bigFilePath;

            switch(instance.Version)
            {
                case HomeworldVersion.HWR:
                    switch(instance.RemasteredGame)
                    {
                        case RemasteredGame.HW2:
                            bigFilePath = FILE_HW2_RM_BIG_PATH;
                            break;

                        case RemasteredGame.HW1:
                            bigFilePath = FILE_HW1_RM_BIG_PATH;
                            break;

                        default:
                            throw new Exceptions.InvalidRemasteredGameException("No .big file path found for " + instance.RemasteredGame);
                    }
                    break;

                default:
                    throw new NotImplementedException("No .big extraction implementated for " + instance.Version);

            }

            extractor = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = String.Format(ARCHIVE_PATH_FORMAT, instance.ToolkitRootDir, CONST.FILE_ARCHIVE_EXE_PATH),
                    Arguments = String.Format(ARCHIVE_ARGS_FORMAT, instance.HomeworldRootDir, bigFilePath, CONST.DIR_EXTRACTION_OUTPUT_PATH),
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                }
            };
        }

        // PROCESS MANAGEMENT
        //----------------------------------------

        /// <summary>
        /// Starts extracting the requested .big file.
        /// Shows a MessageBox if an error occurs.
        /// </summary>
        public void ExtractBigFile()
        {
            try
            {
                extractor.Start();
                HasStarted = true;
                while (!extractor.StandardOutput.EndOfStream)
                {
                    textOutputMethod.Invoke(extractor.StandardOutput.ReadLine());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occured during extraction.\n\n" + e.Message);
            }
        }

        //----------------------------------------

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

                    SuspendThread(threadHandle);
                    CloseHandle(threadHandle);
                }
            }
        }

        //----------------------------------------

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
                        System.Diagnostics.Debug.WriteLine($"i: {i}");
                    } while (i > 0);

                    CloseHandle(threadHandle);
                }
            }
        }

        //----------------------------------------

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

        //----------------------------------------

        /// <summary>
        /// Kills the Archive.exe process if it has been instantiated and has not exited.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if called before the process has been started</exception>
        public void KillProcess()
        {
            try
            {
                if (extractor != null && !extractor.HasExited)
                {
                    extractor.Kill();
                }
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException("Attempting to kill extractor before started. Check HasStarted field before trying to kill.");
            }
        }
    }
}