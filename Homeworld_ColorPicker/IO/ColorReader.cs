﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.IO
{
    using Objects;

    /// <summary>
    /// Reads the <c>PLAYERCFG.LUA</c> file at a given path and parses all player colors into HomeworldColors.
    /// </summary>
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

        //----------------------------------------

        /// <summary>
        /// Gets all the player colors from the <c>PLAYERCFG.LUA</c> file as HomeworldColors.
        /// </summary>
        /// <param name="path">The path to the <c>PLAYERCFG.LUA</c> file</param>
        /// <returns>The 16 player colors</returns>
        public static HomeworldColor[] GetPlayerColors(string path)
        {
            string file = System.IO.File.ReadAllText(path);

            int startIndex = file.IndexOf(COLOR_SWATCHES_START) + COLOR_SWATCHES_START.Length;

            return ReadSwatches(file, startIndex);
        }

        //----------------------------------------

        /// <summary>
        /// Finds substrings of all 16 player color swatches in the <c>PLAYERCFG.LUA</c> file.
        /// Has them parsed into HomeworldColors
        /// </summary>
        /// <param name="file">The text of the entire <c>PLAYERCFG.LUA</c> file</param>
        /// <param name="startIndex">The index of the first color swatch</param>
        /// <returns>An array of all 16 player colors as HomeworldColors</returns>
        private static HomeworldColor[] ReadSwatches(string file, int startIndex)
        {
            HomeworldColor[] colors = new HomeworldColor[NUMBER_OF_SWATCHES];

            int nextIndex;
            string currentColor;

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

        //----------------------------------------

        /// <summary>
        /// Parse an individual color from the R,G,B values with comma delimiters.
        /// Displays an error message if the color cannot be parsed.
        /// </summary>
        /// <param name="color">The string representing the R,G,B values delimited by commas</param>
        /// <returns>A HomeworldColor with the parsed R,G,B values or the default HomeworldColor values if the color cannot be parsed</returns>
        private static HomeworldColor ParseColor(string color)
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