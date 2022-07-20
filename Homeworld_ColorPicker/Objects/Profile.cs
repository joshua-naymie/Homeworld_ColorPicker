using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Objects
{
    public class Profile
    {
        private
        String path,
               name;

        /// <summary>
        /// Constructor for <c>Profile</c>.
        /// <c>path</c> and <c>name</c> cannot be changed after initialization.
        /// </summary>
        /// <param name="path">The path of the profile from the root directory | eg: <c>\Bin\Profiles\Profile1</c></param>
        /// <param name="name">The name of the profile found in the <c>name.dat</c> file</param>
        public Profile(string path, string name)
        {
            this.path = path;
            this.name = name;
        }

        /// <summary>
        /// Gets the path of the profile.
        /// </summary>
        /// <returns>The path of the profile</returns>
        public String GetPath()
        {
            return path;
        }

        /// <summary>
        /// Gets the name of the profile.
        /// </summary>
        /// <returns>The name of the profile</returns>
        public String getName()
        {
            return name;
        }

        /// <summary>
        /// Override of the ToString method. Returns the profile name found in the name.dat file.
        /// </summary>
        /// <returns>The profile name</returns>
        public override string ToString()
        {
            return name;
        }

    }
}
