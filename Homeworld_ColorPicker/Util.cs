using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker
{
    internal class Util
    {
        public static bool checkPathExists(string path)
        {
            return Directory.Exists(path) || File.Exists(path);
        }
    }
}
