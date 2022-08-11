using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Homeworld_ColorPicker.IO
{
    using Objects;
    internal class ConfigManager
    {
        private const
        int NAME = 0,
            VALUE = 1;

        private const
        string FILE_CONFIG = @"\config",
               KEY_HOMEWORLD = "HOMEWORLD",
               KEY_TOOLKIT = "TOOLKIT";

        private static readonly
        string FILE_CONFIG_PATH = CONST.DIR_DOCUMENTS_PATH + FILE_CONFIG;

        /// <summary>
        /// Reads the previous Homeworld and Remastered Toolkit root directories from the config file.
        /// </summary>
        /// <returns>The previous Homeworld and Remastered Toolkit root directories</returns>
        public static RootDirectoryData ReadConfig()
        {
            string? homeworldRootDir = null,
                    toolkitRootDir = null;

            if (!Util.PathExists(CONST.DIR_DOCUMENTS_PATH))
            {
                new DirectoryInfo(CONST.DIR_DOCUMENTS_PATH).Create();
            }

            if (File.Exists(FILE_CONFIG_PATH))
            {
                foreach (string line in File.ReadAllLines(FILE_CONFIG_PATH))
                {
                    string[] param = line.Split('=');

                    switch (param[NAME])
                    {
                        case KEY_HOMEWORLD:
                            homeworldRootDir = param[VALUE];
                            break;

                        case KEY_TOOLKIT:
                            toolkitRootDir = param[VALUE];
                            break;
                    }
                }
            }

            return new RootDirectoryData(homeworldRootDir, toolkitRootDir);
        }

        /// <summary>
        /// Writes the Homeworld and Remastered Toolkit root directories to the config file.
        /// </summary>
        /// <param name="data">The Homeworld and Remastered Toolkit root directories in a RootDirectoryData object</param>
        public static void WriteConfig(RootDirectoryData data)
        {
            string output = CreateParameter(KEY_HOMEWORLD, data.HomeworldRoot) 
                          + CreateParameter(KEY_TOOLKIT, data.ToolkitRoot);

            File.WriteAllTextAsync(FILE_CONFIG_PATH, output);
        }

        /// <summary>
        /// Formats a key-value pair for writing. | Format: KEY=VALUE
        /// </summary>
        /// <param name="key">The key to pair with the value</param>
        /// <param name="value">The value to pair with the key</param>
        /// <returns></returns>
        private static string CreateParameter(string key, string value)
        {
            return key + "=" + value + "\n";
        }
    }
}