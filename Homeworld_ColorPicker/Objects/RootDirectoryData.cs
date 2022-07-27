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

        private
        string homeworldRoot;

        private
        string toolkitRoot;

        public RootDirectoryData()
        {
            homeworldRoot = DIR_DEFAULT_HOMEWORLD_PATH;
            toolkitRoot = DIR_DEFAULT_TOOLKIT_PATH;
        }

        public RootDirectoryData(string? homeworldRoot, string? toolkitRoot)
        {
            this.homeworldRoot = homeworldRoot == null ? DIR_DEFAULT_HOMEWORLD_PATH : homeworldRoot;
            this.toolkitRoot = toolkitRoot == null ? DIR_DEFAULT_TOOLKIT_PATH : toolkitRoot;
        }

        public void SetHomeworldRoot(string homeworldRoot)
        {
            this.homeworldRoot = homeworldRoot;
        }

        public string GetHomeworldRoot()
        {
            return homeworldRoot;
        }

        public void SetToolkitRoot(string toolkitRoot)
        {
            this.toolkitRoot = toolkitRoot;
        }

        public string GetToolkitRoot()
        {
            return toolkitRoot;
        }
    }
}
