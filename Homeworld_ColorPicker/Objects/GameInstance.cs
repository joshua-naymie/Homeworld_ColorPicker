using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Objects
{

    public class GameInstance
    {
        /// <summary>
        /// The version of homeworld for this instance (HW1/HW2/HWR).
        /// Currently only HWR is supported.
        /// </summary>
        public
        HomeworldVersion Version { get; }

        /// <summary>
        /// The remastered game of Homeworld for this instance (HW1/HW2)
        /// </summary>
        public
        RemasteredGame RemasteredGame { get; }

        /// <summary>
        /// The root directory of the Homeworld instance.
        /// </summary>
        public
        string HomeworldRootDir { get; }

        /// <summary>
        /// The root directory of the Remastered Toolkit.
        /// </summary>
        public
        string ToolkitRootDir { get; }

        /// <summary>
        /// The path to the profile directory from the Homeworld root directory.
        /// </summary>
        public
        string ProfilePath { get; }

        /// <summary>
        /// Constructor for GameInstance.
        /// All parameters are immutable after instantiation.
        /// </summary>
        /// <param name="homeworldRootDir">The Homeworld root directory</param>
        /// <param name="toolkitRootDir">The toolkit root directory</param>
        /// <param name="version">The version of Homeworld</param>
        /// <param name="game">The remastered game (HW1/HW2)</param>
        /// <param name="profilePath">The path to the users profile from Homeworld root dir</param>
        public GameInstance(string homeworldRootDir, string toolkitRootDir, HomeworldVersion version, RemasteredGame game, string profilePath)
        {
            this.HomeworldRootDir = homeworldRootDir;
            this.ToolkitRootDir = toolkitRootDir;
            this.Version = version;
            this.RemasteredGame = game;
            this.ProfilePath = profilePath;
        }
    }
}