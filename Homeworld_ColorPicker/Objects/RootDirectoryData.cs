using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Objects
{
    public class RootDirectoryData
    {
        private const
        string DIR_DEFAULT_HOMEWORLD_PATH = @"C:\Program Files (x86)\Steam\steamapps\common\Homeworld\HomeworldRM",
               DIR_DEFAULT_TOOLKIT_PATH = @"C:\Program Files (x86)\Steam\steamapps\common\Homeworld\GBXTools";

        // PROPERTIES
        //----------------------------------------

        /// <summary>
        /// The root directory of the Homeworld Remastered game instance.
        /// Default: C:\Program Files (x86)\Steam\steamapps\common\Homeworld\HomeworldRM
        /// </summary>
        public string HomeworldRoot { get; }

        /// <summary>
        /// The root directory of the Remastered Toolkit direcory.
        /// Default: C:\Program Files (x86)\Steam\steamapps\common\Homeworld\GBXTools
        /// </summary>
        public string ToolkitRoot { get; }

        // CONSTRUCTORS
        //----------------------------------------

        /// <summary>
        /// Constructor for RootDirectoryData.
        /// Sets Homeworld root directory to C:\Program Files (x86)\Steam\steamapps\common\Homeworld\HomeworldRM.
        /// Sets Toolkit root directory to C:\Program Files (x86)\Steam\steamapps\common\Homeworld\GBXTools
        /// </summary>
        public RootDirectoryData()
        {
            HomeworldRoot = DIR_DEFAULT_HOMEWORLD_PATH;
            ToolkitRoot = DIR_DEFAULT_TOOLKIT_PATH;
        }

        //--------------------

        /// <summary>
        /// Constructor for RootDirectoryData.
        /// Parameters can be null and will be given default values.
        /// </summary>
        /// <param name="homeworldRoot">The root directory of the Homeworld Remastered game instance</param>
        /// <param name="toolkitRoot">The root directory of the Remastered Toolkit instance</param>
        public RootDirectoryData(string? homeworldRoot, string? toolkitRoot)
        {
            this.HomeworldRoot = homeworldRoot == null ? DIR_DEFAULT_HOMEWORLD_PATH : homeworldRoot;
            this.ToolkitRoot = toolkitRoot == null ? DIR_DEFAULT_TOOLKIT_PATH : toolkitRoot;
        }
    }
}