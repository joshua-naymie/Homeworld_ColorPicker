using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.IO
{
    using Objects;
    using Exceptions;
    public static class ExtractedDataManager
    {
        public static bool VerifyRequiredFiles(GameInstance instance)
        {
            switch(instance.Version)
            {
                case HomeworldVersion.HWR:
                    return VerifyRemasteredRequiredFiles(instance);
                    break;

                case HomeworldVersion.HW2:
                    // not currently supported
                    return false;
                    break;

                case HomeworldVersion.HW1:
                    // not currently supported
                    return false;
                    break; 
            }

            throw new InvalidVersionException(instance.Version);
        }

        private static bool VerifyRemasteredRequiredFiles(GameInstance instance)
        {
            switch(instance.RemasteredGame)
            {
                case RemasteredGame.HW2:
                    return VerifyHW2RemasteredRequiredFiles();
                    break;

                case RemasteredGame.HW1:
                    // not currently supported
                    return false;
                    break;

                default:
                    throw new InvalidRemasteredGameException(instance.RemasteredGame);
            }
        }

        /// <summary>
        /// Verifies all teamcolour.lua files for Homeworld 2 exist in the correct directories within the HW2_DATA directory.
        /// </summary>
        /// <returns>True if all files exist in the correct directories, false otherwise</returns>
        private static bool VerifyHW2RemasteredRequiredFiles()
        {
            string rootPath = GC.DIR_DOCUMENTS_PATH + GC.DIR_HW2_RM_DATA;

            bool allFilesFound = true;


            for(int i=0; i < GC.HW2_TEAMCOLOR_PATHS.Length; i++)
            {
                string filePath = rootPath + GC.HW2_TEAMCOLOR_PATHS[i] + GC.FILE_TEAMCOLOUR_LUA;

                if(!Util.CheckPathExists(filePath))
                {
                    allFilesFound = false;
                    i = GC.HW2_TEAMCOLOR_PATHS.Length;
                }
            }

            return allFilesFound;
        }

        public static void MoveRequiredFiles(GameInstance instance)
        {
            switch (instance.Version)
            {
                case HomeworldVersion.HWR:
                    MoveRemasteredRequiredFiles(instance);
                    break;

                case HomeworldVersion.HW2:
                    // not currently supported
                    break;

                case HomeworldVersion.HW1:
                    // not currently supported
                    break;

                default:
                    throw new InvalidVersionException(instance.Version);
            }
        }

        private static void MoveRemasteredRequiredFiles(GameInstance instance)
        {
            switch (instance.RemasteredGame)
            {
                case RemasteredGame.HW2:
                    MoveHW2RemasteredRequiredFiles();
                    break;

                case RemasteredGame.HW1:
                    // not currently supported
                    break;

                default:
                    throw new InvalidRemasteredGameException(instance.RemasteredGame);
            }
        }

        private static void MoveHW2RemasteredRequiredFiles()
        {
            Util.ClearDirectory(GC.DIR_HW2_RM_DATA_PATH);
            MoveTeamcolorFiles(GC.HW2_TEAMCOLOR_PATHS, GC.DIR_HW2_RM_DATA_PATH);
        }

        private static void MoveTeamcolorFiles(string[] paths, string moveDir)
        {
            foreach (string levelPath in paths)
            {
                if (!Util.CheckPathExists(moveDir + levelPath))
                {
                    System.IO.Directory.CreateDirectory(moveDir + levelPath);
                }

                File.Move((GC.DIR_EXTRACTION_OUTPUT_PATH + levelPath + GC.FILE_TEAMCOLOUR_LUA), (moveDir + levelPath + GC.FILE_TEAMCOLOUR_LUA));
            }
        }
    }
}
