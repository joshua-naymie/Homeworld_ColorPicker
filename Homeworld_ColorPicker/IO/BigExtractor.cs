using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.IO
{
    internal class BigExtractor
    {
        private const
        string ARCHIVE_PARAM_FORMAT = "{0}{1} -a {2}",
               ARCHIVE_PATH_FORMAT = "{0}{1}",
               ARCHIVE_ARGS_FORMAT = "{0}{1}";

        private const
        string DIR_GAME_DATA = @"\Data",
               FILE_HW2_BIG = @"\Homeworld2.big";
        
        public static void ExtractBigFile(string homeworldRoot, string toolkitRoot, string outputPath)
        {
            System.Diagnostics.Process extractor = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = @"D:\Steam\steamapps\common\Homeworld\GBXTools\WorkshopTool\Archive.exe",
                    Arguments = " -a \"E:\\Games\\Steam\\steamapps\\common\\Homeworld\\Homeworld2Classic\\Data\\Homeworld2.big\" -e \"G:\\Documents\\Homeworld ColorPicker\\t\"",
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                }
            };

            extractor.Start();
            while (!extractor.StandardOutput.EndOfStream)
            {
                System.Diagnostics.Debug.WriteLine(extractor.StandardOutput.ReadLine());
            }
        }
    }
}