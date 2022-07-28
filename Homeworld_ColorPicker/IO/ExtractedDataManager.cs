using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.IO
{
    using Objects;
    using Exceptions;

    /// <summary>
    /// Manages the extraction output and data directories.
    /// </summary>
    public static class ExtractedDataManager
    {
        // VERIFY FILES
        //----------------------------------------

        /// <summary>
        /// Verifies that all required files are present in the appropriate data directory, based off the version of Homeworld.
        /// </summary>
        /// <param name="instance">The instance of Homeworld to work on</param>
        /// <returns>True if all the required files are present in the correct location, false otherwise</returns>
        /// <exception cref="InvalidVersionException">Thrown if the versin of Homeworld is not supported or is invalid.</exception>
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

        //--------------------


        /// <summary>
        /// Verifies that all required files are present in the appropriate data directory, based off the Remastered game.
        /// </summary>
        /// <param name="instance">The instance of Homeworld to work on</param>
        /// <returns>True if all the required files are present in the correct location, false otherwise.</returns>
        /// <exception cref="InvalidRemasteredGameException">Thrown if the Remastered game is not supported or is invalid</exception>
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

        //----------------------------------------

        /// <summary>
        /// Verifies all required files for Homeworld 2 Remastered exist in the correct directories within the Homeworld 2 Remastered data directory.
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

        // MOVE REQUIRED
        //----------------------------------------

        /// <summary>
        /// Moves only required files from the extraction output directory to the appropriate data folder, based on the Homeworld version.
        /// </summary>
        /// <param name="instance">The instance of Homeworld to work on</param>
        /// <exception cref="InvalidVersionException">Thrown if the version of Homeworld is not supported or is invalid</exception>
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

        //--------------------

        /// <summary>
        /// Moves only required files from the extraction output directory to the appropriate Homeworld Remastered data folder, based on the Remastered gameversion.
        /// </summary>
        /// <param name="instance">The instance of Homeworld to work on</param>
        /// <exception cref="InvalidRemasteredGameException">Thrown if the Remastered game is not supported or is invalid</exception>
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

        //----------------------------------------

        /// <summary>
        /// Clears the Homeworld 2 Remastered data directory and moves over only required files from the extraction output directory.
        /// </summary>
        private static void MoveHW2RemasteredRequiredFiles()
        {
            Util.ClearDirectory(GC.DIR_HW2_RM_DATA_PATH);
            MoveTeamcolorFiles(GC.HW2_TEAMCOLOR_PATHS, GC.DIR_HW2_RM_DATA_PATH);
        }

        // MOVE ALL
        //----------------------------------------

        /// <summary>
        /// Moves all files and subdirectories from the extraction output directory to the appropriate Homeworld data directory, based off the Homeworld version.
        /// </summary>
        /// <param name="instance">The instance of Homeworld to work on</param>
        /// <exception cref="InvalidVersionException">Thrown if the Homeworld version is not supported or is invalid</exception>
        public static void MoveAllFiles(GameInstance instance)
        {

            switch(instance.Version)
            {
                case HomeworldVersion.HWR:
                    MoveAllRemasteredFiles(instance);
                    break;

                case HomeworldVersion.HW2:
                case HomeworldVersion.HW1:
                default:
                    throw new InvalidVersionException(instance.Version);
            }
        }

        //--------------------

        /// <summary>
        /// Moves all files and subdirectories from the extraction output directory to the appropriate Homeworld Remastered data directory, based off the Remastered game.
        /// </summary>
        /// <param name="instance">The instance of Homeworld to work on</param>
        /// <exception cref="InvalidRemasteredGameException">Thrown if the Remastered game is not supported or is invalid</exception>
        private static void MoveAllRemasteredFiles(GameInstance instance)
        {
            switch (instance.RemasteredGame)
            {
                case RemasteredGame.HW2:
                    Util.ClearDirectory(GC.DIR_HW2_RM_DATA_PATH);
                    MoveAllFilesTo(GC.DIR_HW2_RM_DATA_PATH);
                    break;

                case RemasteredGame.HW1:
                    throw new InvalidRemasteredGameException("HW1 is not yet implemented to move all files.");
                    break;

                default:
                    throw new InvalidRemasteredGameException(instance.RemasteredGame);
            }
        }

        // ACTION METHODS
        //----------------------------------------

        /// <summary>
        /// Moves all teamcolour.lua files from the extraction output directory to a Homeworld data directory while maintaining the directory structure.
        /// </summary>
        /// <param name="paths">The paths of the teamcolour.lua files from the extraction output directory | eg: \leveldata\campaign\ascension\m01_tanis</param>
        /// <param name="moveDir">The path to the Homeworld data directory to move the teamcolour.lua files to</param>
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
        
        //----------------------------------------
        
        /// <summary>
        /// Moves all files and subdirectories from the extraction output directory to a specific Homeworld data directory.
        /// </summary>
        /// <param name="dataDirectory">The path to the Homeworld data directory to move everything to</param>
        private static void MoveAllFilesTo(string dataDirectory)
        {
            DirectoryInfo directory = new DirectoryInfo(GC.DIR_EXTRACTION_OUTPUT_PATH);

            directory.EnumerateFiles().ToList().ForEach(file =>
            {
                //string path = dataDirectory + file.Name;
                file.MoveTo(dataDirectory + "\\" + file.Name, true);
            });

            directory.EnumerateDirectories().ToList().ForEach(dir =>
            {
                //string path = dataDirectory + dir.Name;
                dir.MoveTo(dataDirectory + "\\" + dir.Name);
            });
        }

        //----------------------------------------

        /// <summary>
        /// Deletes all contents of the extraction output directory.
        /// </summary>
        public static void ClearOutputDir()
        {
            Util.ClearDirectory(GC.DIR_EXTRACTION_OUTPUT_PATH);
        }
    }
}