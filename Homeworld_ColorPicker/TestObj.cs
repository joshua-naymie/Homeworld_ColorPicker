using Homeworld_ColorPicker.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker
{
    class TestObj
    {
        private
        Objects.HomeworldColour colour;

        public TestObj(ref HomeworldColour colour)
        {
            this.colour = colour;
        }

        public void ChangeColour()
        {
            colour.SetRGB((float).222, (float).222, (float).222);
        }
    }
}
