using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Objects
{
    public class HomeworldLevel
    {
        short levelNum;

        private
        TeamColour[] teams;

        

        public TeamColour[] GetTeams()
        {
            return teams;
        }
    }
}
