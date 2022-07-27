using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.IO
{
    public static class ExtractedDataManager
    {
        //private
        //HomeworldVersion version;

        //private
        //RemasteredGame game;

        //public ExtractedDataManager(HomeworldVersion version)
        //{
        //    this.version = version;
        //}

        //public void ClearDataDirectory()
        //{
        //    switch(version)
        //    {

        //    }
        //}

        /// <summary>
        /// Verifies all teamcolour.lua files for Homeworld 2 exist in the correct directories within the HW2_DATA directory.
        /// </summary>
        /// <returns>True if all files exist in the correct directories, false otherwise</returns>
        public static bool VerifyHW2Remastered()
        {
            string rootPath = GC.DIR_DOCUMENTS_PATH + GC.DIR_HW2_RM_EXTRACTED_DATA;

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
    }
}
