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
        /// The directory where extracted data from Homeworld 2 Remastered is stored in the Documents directory.
        /// </summary>
        public const
        string DIR_HW2_RM_EXTRACTED_DATA = @"\HW2_RM";

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

        public const
        string FILE_TEAMCOLOUR_LUA = @"\teamcolour.lua";

        /// <summary>
        /// The path to the archive executable from the Toolkit root dir
        /// </summary>
        public const
        string FILE_ARCHIVE_EXE_PATH = @"\WorkshopTool\Archive.exe";

        /// <summary>
        /// The path 
        /// </summary>
        public const
        string DIR_HW2_TEAMCOLOR_01_TANIS = @"\leveldata\campaign\ascension\m01_tanis";

        public const
        string DIR_HW2_TEAMCOLOR_02_HIIGARA = @"\leveldata\campaign\ascension\m02_hiigara";

        public const
        string DIR_HW2_TEAMCOLOR_03_STAGING = @"\leveldata\campaign\ascension\m03_staging";

        public const
        string DIR_HW2_TEAMCOLOR_04_OUTERGEH = @"\leveldata\campaign\ascension\m04_outergeh";

        public const
        string DIR_HW2_TEAMCOLOR_05_GEHENNA = @"\leveldata\campaign\ascension\m05_gehenna";

        public const
        string DIR_HW2_TEAMCOLOR_06_KAROS = @"\leveldata\campaign\ascension\m06_karos";

        public const
        string DIR_HW2_TEAMCOLOR_07_VEIL_OF_FIRE = @"\leveldata\campaign\ascension\m07_veil_of_fire";

        public const
        string DIR_HW2_TEAMCOLOR_08_HANGAR = @"\leveldata\campaign\ascension\m08_hangar";

        public const
        string DIR_HW2_TEAMCOLOR_09_OUTER_HANGAR = @"\leveldata\campaign\ascension\m09_outer_hangar";

        public const
        string DIR_HW2_TEAMCOLOR_10_BENTUSI = @"\leveldata\campaign\ascension\m10_bentusi";

        public const
        string DIR_HW2_TEAMCOLOR_11_FINAL_CORE = @"\leveldata\campaign\ascension\m11_final_core";

        public const
        string DIR_HW2_TEAMCOLOR_12_RESCUE = @"\leveldata\campaign\ascension\m12_rescue";

        public const
        string DIR_HW2_TEAMCOLOR_13_BALCORA_GATE = @"\leveldata\campaign\ascension\m13_balcora_gate";

        public const
        string DIR_HW2_TEAMCOLOR_14_SAJUUK = @"\leveldata\campaign\ascension\m14_sajuuk";

        public const
        string DIR_HW2_TEAMCOLOR_15_HOMEWORLD = @"\leveldata\campaign\ascension\m15_homeworld";

        public static readonly
        string[] HW2_TEAMCOLOR_PATHS = { DIR_HW2_TEAMCOLOR_01_TANIS,
                                         DIR_HW2_TEAMCOLOR_02_HIIGARA,
                                         DIR_HW2_TEAMCOLOR_03_STAGING,
                                         DIR_HW2_TEAMCOLOR_04_OUTERGEH,
                                         DIR_HW2_TEAMCOLOR_05_GEHENNA,
                                         DIR_HW2_TEAMCOLOR_06_KAROS,
                                         DIR_HW2_TEAMCOLOR_07_VEIL_OF_FIRE,
                                         DIR_HW2_TEAMCOLOR_08_HANGAR,
                                         DIR_HW2_TEAMCOLOR_09_OUTER_HANGAR,
                                         DIR_HW2_TEAMCOLOR_10_BENTUSI,
                                         DIR_HW2_TEAMCOLOR_11_FINAL_CORE,
                                         DIR_HW2_TEAMCOLOR_12_RESCUE,
                                         DIR_HW2_TEAMCOLOR_13_BALCORA_GATE,
                                         DIR_HW2_TEAMCOLOR_14_SAJUUK,
                                         DIR_HW2_TEAMCOLOR_15_HOMEWORLD };

        public static readonly
        string DIR_DOCUMENTS_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + DIR_DOCUMENTS_COLORPICKER;
    }
}
