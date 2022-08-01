using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Objects
{
    /// <summary>
    /// Represents a color based on R,G,B values. Outputs data formatted to <c>teamcolor.lua</c> specifications.
    /// </summary>
    public class HomeworldColour
    {
        private
        float r, g, b;

        // CONSTRUCTORS
        //----------------------------------------

        /// <summary>
        /// Constructor for HomeworldColor.
        /// Initializes with 255 for R,G,B values
        /// </summary>
        public HomeworldColour()
        {
            r = 1;
            g = 1;
            b = 1;
        }

        //--------------------

        /// <summary>
        /// Constructor for HomeworldColor
        /// Initializes with specified R,G,B values.
        /// </summary>
        /// <param name="r">The red value</param>
        /// <param name="g">The green value</param>
        /// <param name="b">The blue value</param>
        public HomeworldColour(float r, float g, float b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        /// <summary>
        /// Constructor for HomeworldColor
        /// Initializes with specified R,G,B values.
        /// </summary>
        /// <param name="r">The red value</param>
        /// <param name="g">The green value</param>
        /// <param name="b">The blue value</param>
        public HomeworldColour(byte r, byte g, byte b)
        {
            this.r = (float)r / Byte.MaxValue;
            this.g = (float)g / Byte.MaxValue;
            this.b = (float)b / Byte.MaxValue;
        }

        public HomeworldColour(Color color)
        {
            this.r = (float)color.R / Byte.MaxValue;
            this.g = (float)color.G / Byte.MaxValue;
            this.b = (float)color.B / Byte.MaxValue;
        }

        // ACCESSORS
        //----------------------------------------

        /// <summary>
        /// Sets the R,G,B values using the values of another <c>HomeworldColor</c>
        /// </summary>
        /// <param name="color"></param>
        public void SetColor(HomeworldColour color)
        {
            SetRGB(color.r, color.g, color.b);
        }

        //--------------------

        /// <summary>
        /// Sets the Red, Green and Blue values of the color.
        /// </summary>
        /// <param name="r">The red value</param>
        /// <param name="g">The green value</param>
        /// <param name="b">The blue value</param>
        public void SetRGB(float r, float g, float b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        // UTIL
        //----------------------------------------

        /// <summary>
        /// Gets the current R,G,B values as a System.Drawing.Color object.
        /// </summary>
        /// <returns>The current R,G,B values as a System.Drawing.Color object</returns>
        public Color ToColor()
        {
            byte red   = (byte)Math.Round(r * Byte.MaxValue),
                 green = (byte)Math.Round(g * Byte.MaxValue),
                 blue  = (byte)Math.Round(b * Byte.MaxValue);

            return Color.FromArgb(red, green, blue);
        }

        //----------------------------------------

        /// <summary>
        /// Overrides the <c>ToString</c> method.
        /// Outputs R,G,B values in <c>teamcolor.lua</c> format | eg: { 1.000, 0.333, 0.500 }
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{{{0:0.000}, {1:0.000}, {2:0.000}}}", r, g, b);
        }

        //----------------------------------------

        /// <summary>
        /// Overrides the <c>Equals</c> method.
        /// Compares R, G, and B values.
        /// </summary>
        /// <param name="obj">The object to compare to</param>
        /// <returns>True if R,G,B are the same value, false otherwise</returns>
        public override bool Equals(object? obj)
        {
            if(obj != null
            && obj.GetType() == typeof(HomeworldColour))
            {
                if(this.r == ((HomeworldColour)obj).r
                && this.g == ((HomeworldColour)obj).g
                && this.b == ((HomeworldColour)obj).b)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
