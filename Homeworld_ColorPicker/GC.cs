using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker
{
    /// <summary>
    /// Global constants used throughout the program
    /// </summary>
    public static class GC
    {
        /// <summary>
        /// The number of player colors in <c>PLAYER.CFG</c>
        /// </summary>
        public const
        int NUM_PLAYER_COLORS = 16;


        /// <summary>
        /// The directory within 'My Documents' used by Homeworld ColorPicker for data extraction and persistance.
        /// </summary>
        public const
        string DIR_DOCUMENTS_COLORPICKER = @"\Homeworld ColorPicker";

        /// <summary>
        /// The path to the <c>Profiles</c> directory from the Homeworld root directory
        /// </summary>
        public const
        string DIR_PROFILES_PATH = @"\Bin\Profiles";

        /// <summary>
        /// The file name of the player config file from the <c>Profiles</c> directory
        /// </summary>
        public const
        string FILE_PLAYERCFG_LUA = @"\PLAYERCFG.LUA";

        /// <summary>
        /// The path to the archive executable from the Toolkit root dir
        /// </summary>
        public const
        string FILE_ARCHIVE_EXE_PATH = @"\WorkshopTool\Archive.exe";
    }
}
