﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Homeworld_ColorPicker
{
    using Objects;
    using System.Drawing.Text;
    using System.Runtime.InteropServices;

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

        public const
        string DIR_EXTRACTION_OUTPUT = @"\output";

        /// <summary>
        /// The directory where extracted data from Homeworld 2 Remastered is stored in the Documents directory.
        /// </summary>
        public const
        string DIR_HW2_RM_DATA = @"\HW2_RM";

        /// <summary>
        /// The path to the <c>Profiles</c> directory from the Homeworld root directory
        /// </summary>
        public const
        string DIR_PROFILES_PATH = @"\Bin\Profiles";

        /// <summary>
        /// The path to the HW2Campaign.big file from the Homeworld Remastered root directory.
        /// </summary>
        public const
        string FILE_HW2_RM_BIG = @"\Data\HW2Campaign.big";

        /// <summary>
        /// The path to the HW1Campaign.big file from the Homeworld Remastered root directory.
        /// </summary>
        public const
        string FILE_HW1_RM_BIG = @"\Data\HW1Campaign.big";

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

        /// <summary>
        /// The paths to all Homeworld 2 level directories where teamcolour.lua files are stored.
        /// eg: \leveldata\campaign\ascension\[level_dir]
        /// </summary>
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

        /// <summary>
        /// The path to the programs working directory in the users My Documents directory.
        /// eg: C:\Users\[username]\Documents\[working_dir]
        /// </summary>
        public static readonly
        string DIR_DOCUMENTS_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + DIR_DOCUMENTS_COLORPICKER;

        /// <summary>
        /// The path to the directory where Homeworld 2 Remastered data files are stored in the Homeworld ColorPicker working directory.
        /// eg: C:\Users\[username]\Documents\[working_dir]\[hw2rm_data_dir]
        /// </summary>
        public static readonly
        string DIR_HW2_RM_DATA_PATH = DIR_DOCUMENTS_PATH + DIR_HW2_RM_DATA;

        /// <summary>
        /// The path to the directory where .big files are extracted in the Homeworld ColorPicker working directory.
        /// eg: C:\Users\[username]\Documents\[working_dir]\[output_dir]
        /// </summary>
        public static readonly
        string DIR_EXTRACTION_OUTPUT_PATH = DIR_DOCUMENTS_PATH + DIR_EXTRACTION_OUTPUT;

        private static readonly
        string[] LEVEL_NAMES = { "Tanis",
                                 "Angel Moon",
                                 "Sarum",
                                 "Gehenna Outskirts",
                                 "Gehenna",
                                 "Karos Graveyard",
                                 "Progenitor Foundry",
                                 "Dreadnaught Berth",
                                 "Vaygr Assembly Point",
                                 "Great Wastelands",
                                 "Bentusi Ruins",
                                 "Thaddis Sabbah",
                                 "Balcora Gate",
                                 "Balcora",
                                 "Return to Hiigara" };


        private const
        string TEAM_NAME_PLAYER = "Player",
               TEAM_NAME_VAYGR = "Vaygr",
               TEAM_NAME_VAYGR_ELITE = "Vaygr Elite",
               TEAM_NAME_DEFENCE_STATIONS = "Defence Stations",
               TEAM_NAME_DEFENCE_FLEET = "Defence Fleet",
               TEAM_NAME_CREW_TRANSPORTS = "Crew Transports",
               TEAM_NAME_FERIN_SHA = "Ferin Sha Fleet",
               TEAM_NAME_TALORN_SOBAN = "Talorn Soban",
               TEAM_NAME_ELITE_HIIGARAN = "Elite Hiigaran",
               TEAM_NAME_SHIPYARD_NAABAL = "Shipyard Naabal",
               TEAM_NAME_COMMAND_STATION = "Command Station",
               TEAM_NAME_MOVERS = "Movers",
               TEAM_NAME_KEEPERS = "Keepers",
               TEAM_NAME_BENTUSI = "Bentusi",
               TEAM_NAME_MAKAAN = "Makaan",
               TEAM_NAME_HIIGARAN_NAVY = "Hiigaran Navy";


        public static readonly
        Dictionary<Tuple<int, int>, Team> DICT_HW2_LEVEL_TEAM_NAMES = new Dictionary<Tuple<int, int>, Team>()
        {
            // level 1
            { new Tuple<int, int>(0,0), new Team(TeamId.Player, TEAM_NAME_PLAYER) },
            { new Tuple<int, int>(0,1), new Team(TeamId.DefenceStation, TEAM_NAME_DEFENCE_STATIONS) },
            { new Tuple<int, int>(0,2), new Team(TeamId.DefenceFleet, TEAM_NAME_DEFENCE_FLEET) },
            { new Tuple<int, int>(0,3), new Team(TeamId.Vaygr, TEAM_NAME_VAYGR) },

            // level  2
            { new Tuple<int, int>(1,0), new Team(TeamId.Player, TEAM_NAME_PLAYER) },
            { new Tuple<int, int>(1,1), new Team(TeamId.Vaygr, TEAM_NAME_VAYGR) },
            { new Tuple<int, int>(1,2), new Team(TeamId.Hiigaran, TEAM_NAME_CREW_TRANSPORTS) },
            { new Tuple<int, int>(1,3), new Team(TeamId.Sobani, TEAM_NAME_FERIN_SHA) },
            { new Tuple<int, int>(1,4), new Team(TeamId.TalornSoban, TEAM_NAME_TALORN_SOBAN) },
            { new Tuple<int, int>(1,5), new Team(TeamId.HiigaranElite, TEAM_NAME_ELITE_HIIGARAN) },

            // level 3
            { new Tuple<int, int>(2,0), new Team(TeamId.Player, TEAM_NAME_PLAYER) },
            { new Tuple<int, int>(2,1), new Team(TeamId.Vaygr, TEAM_NAME_VAYGR) },
            { new Tuple<int, int>(2,2), new Team(TeamId.Hiigaran, TEAM_NAME_SHIPYARD_NAABAL) },
            { new Tuple<int, int>(2,3), new Team(TeamId.Vaygr, TEAM_NAME_VAYGR) },

            // level 4
            { new Tuple<int, int>(3,0), new Team(TeamId.Player, TEAM_NAME_PLAYER) },
            { new Tuple<int, int>(3,1), new Team(TeamId.Vaygr, TEAM_NAME_VAYGR) },
            { new Tuple<int, int>(3,2), new Team(TeamId.Vaygr, TEAM_NAME_VAYGR) },
            { new Tuple<int, int>(3,3), new Team(TeamId.Vaygr, TEAM_NAME_VAYGR) },
            { new Tuple<int, int>(3,4), new Team(TeamId.Vaygr, TEAM_NAME_COMMAND_STATION) },

            // level 5
            { new Tuple<int, int>(4,0), new Team(TeamId.Player, TEAM_NAME_PLAYER) },
            { new Tuple<int, int>(4,1), new Team(TeamId.Vaygr, TEAM_NAME_VAYGR) },
            { new Tuple<int, int>(4,2), new Team(TeamId.Vaygr, TEAM_NAME_VAYGR) },

            // level 6
            { new Tuple<int, int>(5,0), new Team(TeamId.Player, TEAM_NAME_PLAYER) },
            { new Tuple<int, int>(5,1), new Team(TeamId.Progenitor, TEAM_NAME_MOVERS) },
            { new Tuple<int, int>(5,2), new Team(TeamId.Progenitor, TEAM_NAME_MOVERS) },

            // level 7
            { new Tuple<int, int>(6,0), new Team(TeamId.Player, TEAM_NAME_PLAYER) },
            { new Tuple<int, int>(6,1), new Team(TeamId.Vaygr, TEAM_NAME_VAYGR) },
            { new Tuple<int, int>(6,2), new Team(TeamId.Progenitor, TEAM_NAME_MOVERS) },

            // level  8
            { new Tuple<int, int>(7,0), new Team(TeamId.Player, TEAM_NAME_PLAYER) },
            { new Tuple<int, int>(7,1), new Team(TeamId.Progenitor, TEAM_NAME_KEEPERS) },

            // level 9
            { new Tuple<int, int>(8,0), new Team(TeamId.Player, TEAM_NAME_PLAYER) },
            { new Tuple<int, int>(8,1), new Team(TeamId.Vaygr, TEAM_NAME_VAYGR) },
            { new Tuple<int, int>(8,2), new Team(TeamId.Vaygr, TEAM_NAME_VAYGR) },
            { new Tuple<int, int>(8,3), new Team(TeamId.VaygrElite, TEAM_NAME_VAYGR_ELITE) },
            { new Tuple<int, int>(8,4), new Team(TeamId.TalornSoban, TEAM_NAME_TALORN_SOBAN) },
            { new Tuple<int, int>(8,5), new Team(TeamId.Hiigaran, TEAM_NAME_SHIPYARD_NAABAL) },

            // level 10
            { new Tuple<int, int>(9,0), new Team(TeamId.Player, TEAM_NAME_PLAYER) },
            { new Tuple<int, int>(9,1), new Team(TeamId.Progenitor, TEAM_NAME_KEEPERS) },
            { new Tuple<int, int>(9,2), new Team(TeamId.Bentusi, TEAM_NAME_BENTUSI) },

            // level 11
            { new Tuple<int, int>(10,0), new Team(TeamId.Player, TEAM_NAME_PLAYER) },
            { new Tuple<int, int>(10,1), new Team(TeamId.Vaygr, TEAM_NAME_VAYGR) },
            { new Tuple<int, int>(10,2), new Team(TeamId.Vaygr, TEAM_NAME_VAYGR) },
            { new Tuple<int, int>(10,3), new Team(TeamId.Vaygr, TEAM_NAME_VAYGR) },

            // level 12
            { new Tuple<int, int>(11,0), new Team(TeamId.Player, TEAM_NAME_PLAYER) },
            { new Tuple<int, int>(11,1), new Team(TeamId.VaygrElite, TEAM_NAME_VAYGR_ELITE) },
            { new Tuple<int, int>(11,2), new Team(TeamId.Vaygr, TEAM_NAME_VAYGR) },

            // level 13
            { new Tuple<int, int>(12,0), new Team(TeamId.Player, TEAM_NAME_PLAYER) },
            { new Tuple<int, int>(12,1), new Team(TeamId.Vaygr, TEAM_NAME_VAYGR) },
            { new Tuple<int, int>(12,2), new Team(TeamId.Vaygr, TEAM_NAME_VAYGR) },
            { new Tuple<int, int>(12,3), new Team(TeamId.VaygrElite, TEAM_NAME_VAYGR_ELITE) },

            // level 14
            { new Tuple<int, int>(13,0), new Team(TeamId.Player, TEAM_NAME_PLAYER) },
            { new Tuple<int, int>(13,1), new Team(TeamId.VaygrElite, TEAM_NAME_MAKAAN) },
            { new Tuple<int, int>(13,2), new Team(TeamId.VaygrElite, TEAM_NAME_MAKAAN) },

            // level 15
            { new Tuple<int, int>(14,0), new Team(TeamId.Player, TEAM_NAME_PLAYER) },
            { new Tuple<int, int>(14,1), new Team(TeamId.Vaygr, TEAM_NAME_VAYGR) },
            { new Tuple<int, int>(14,2), new Team(TeamId.Vaygr, TEAM_NAME_VAYGR) },
            { new Tuple<int, int>(14,3), new Team(TeamId.Vaygr, TEAM_NAME_VAYGR) },
            { new Tuple<int, int>(14,4), new Team(TeamId.Vaygr, TEAM_NAME_VAYGR) },
            { new Tuple<int, int>(14,5), new Team(TeamId.Hiigaran, TEAM_NAME_HIIGARAN_NAVY) },
        };

        public static readonly
        Font CUSTOM_FONT;

        static GC()
        {
            CUSTOM_FONT = InitHomeworlFont();
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        static Font InitHomeworlFont()
        {
            PrivateFontCollection fontCollection = new PrivateFontCollection();

            //Select your font from the resources
            int fontLength = Properties.Resources.Microgramma_Font.Length;

            // create a buffer to read in to
            byte[] fontdata = Properties.Resources.Microgramma_Font;

            // create an unsafe memory block for the font data
            System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);

            // copy the bytes to the unsafe memory block
            Marshal.Copy(fontdata, 0, data, fontLength);

            uint cFonts = 0;
            AddFontMemResourceEx(data, (uint)fontdata.Length, IntPtr.Zero, ref cFonts);

            // pass the font to the font collection
            fontCollection.AddMemoryFont(data, fontLength);

            Marshal.FreeCoTaskMem(data);

            return new Font(fontCollection.Families[0], 13);
        }
    }
}
