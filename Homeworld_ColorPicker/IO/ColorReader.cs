using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.IO
{
    using Objects;
    public class ColorReader
    {
        private const
        byte RED = 0,
             GREEN = 1,
             BLUE = 2;

        private const
        char COLOR_SWATCH_START = '{',
             COLOR_SWATCH_END = '}';

        private const
        int NUMBER_OF_SWATCHES = 16;

        private const
        string COLOR_SWATCHES_START = "colorswatches = {",
            
               MESSAGE_COULD_NOT_PARSE_COLOR = "Error: Could not parse color!";


        public HomeworldColor[] GetPlayerColors(string path)
        {
            string file = System.IO.File.ReadAllText(path);

            int startIndex = file.IndexOf(COLOR_SWATCHES_START) + COLOR_SWATCHES_START.Length;

            return ReadSwatches(file, startIndex);
        }

        private HomeworldColor[] ReadSwatches(string file, int startIndex)
        {
            int nextIndex;
            string currentColor;
            HomeworldColor[] colors = new HomeworldColor[NUMBER_OF_SWATCHES];

            for(int i=0; i<NUMBER_OF_SWATCHES; i++)
            {
                nextIndex = file.IndexOf(COLOR_SWATCH_END, startIndex);
                //System.Diagnostics.Debug.WriteLine("si: " + startIndex + " | ni: " + nextIndex);// + "\n");

                currentColor = file.Substring(file.IndexOf(COLOR_SWATCH_START, startIndex)+1, nextIndex);
                colors[i] = ParseColor(currentColor);

                startIndex = nextIndex + 1; 
            }

            return colors;
        }

        private HomeworldColor ParseColor(string color)
        {
            string[] values = color.Split(',');

            //System.Diagnostics.Debug.WriteLine("R: " + values[RED].Trim() + "\nG: " + values[GREEN].Trim() + "\nB: " + values[BLUE].Trim() + "\n\n");

            try
            {
                byte r = Byte.Parse(values[RED].Trim()),
                     g = Byte.Parse(values[GREEN].Trim()),
                     b = Byte.Parse(values[BLUE].Trim());

                return new HomeworldColor(r, g, b);
            }
            catch(Exception e)
            {
                MessageBox.Show(MESSAGE_COULD_NOT_PARSE_COLOR);

                return new HomeworldColor();
            }
        }
    }
}