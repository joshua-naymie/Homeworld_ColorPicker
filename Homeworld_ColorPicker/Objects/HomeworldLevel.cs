using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Objects
{
    public class HomeworldLevel
    {
        public int LevelNum { get; }

        public Team[] Teams { get; }

        public HomeworldLevel(Team[] teams, int levelNum)
        {
            Teams = teams;
            LevelNum = levelNum;
        }
    }
}
