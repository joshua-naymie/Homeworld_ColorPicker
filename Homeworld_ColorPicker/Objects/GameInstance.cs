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
        private
        HomeworldVersion version;

        /// <summary>
        /// The remastered game of Homeworld for this instance (HW1/HW2)
        /// </summary>
        private
        RemasteredGame game;

        /// <summary>
        /// The root directory of the Homeworld instance.
        /// </summary>
        private
        string homeworldRootDir;

        /// <summary>
        /// The root directory of the Remastered Toolkit.
        /// </summary>
        private
        string toolkitRootDir;

        /// <summary>
        /// The path to the profile directory from the Homeworld root directory.
        /// </summary>
        private
        string profilePath;

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
            this.homeworldRootDir = homeworldRootDir;
            this.toolkitRootDir = toolkitRootDir;
            this.version = version;
            this.game = game;
            this.profilePath = profilePath;
        }

        // ACCESSORS
        //----------------------------------------

        /// <summary>
        /// Gets the Homeworld root directory path.
        /// </summary>
        /// <returns>The Homeworld root directory path</returns>
        public string GetHomeworldRoot()
        {
            return homeworldRootDir;
        }


        //--------------------

        /// <summary>
        /// Gets the toolkit root directory path.
        /// </summary>
        /// <returns>The toolkit root directory path</returns>
        public string GetToolkitRoot()
        {
            return toolkitRootDir;
        }

        //--------------------

        /// <summary>
        /// Gets the version of Homeworld (HW1/HW2/HWR).
        /// </summary>
        /// <returns>The version of the Homeworld (HW1/HW2/HWR)</returns>
        public HomeworldVersion GetVersion()
        {
            return version;
        }

        //--------------------

        /// <summary>
        /// Gets the Remastered game (HW1/HW2) of this instance.
        /// </summary>
        /// <returns>The Remastered game (HW1/HW2)</returns>
        public RemasteredGame GetRemasteredGame()
        {
            return game;
        }

        //--------------------


        /// <summary>
        /// Gets the Profile object of this GameInstance.
        /// </summary>
        /// <returns></returns>
        public string GetProfilePath()
        {
            return profilePath;
        }
    }
}