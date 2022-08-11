using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Objects
{
    public class Profile
    {
        // PROPERTIES
        //----------------------------------------

        /// <summary>
        /// The path of the profile from the game's root directory.
        /// </summary>
        public String Path { get; }

        /// <summary>
        /// The name associated with the profile
        /// </summary>
        public String Name { get;  }

        // CONSTRUCTOR
        //----------------------------------------

        /// <summary>
        /// Constructor for <c>Profile</c>.
        /// <c>path</c> and <c>name</c> cannot be changed after initialization.
        /// </summary>
        /// <param name="path">The path of the profile from the root directory | eg: <c>\Bin\Profiles\Profile1</c></param>
        /// <param name="name">The name of the profile found in the <c>name.dat</c> file</param>
        public Profile(string path, string name)
        {
            Path = path;
            Name = name;
        }

        // UTIL
        //----------------------------------------

        /// <summary>
        /// Override of the ToString method. Returns the profile name found in the name.dat file.
        /// </summary>
        /// <returns>The profile name</returns>
        public override string ToString()
        {
            return Name;
        }

    }
}
