using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Objects
{
    public class TeamColour
    {
        private const
        string TO_STRING_FORMAT = "-=COLOR=-\n"
                                + "base: {0}\n"
                                + "stripe: {1}\n"
                                + "trail: {2}\n"
                                + "-=PATH=-\n"
                                + "bagde: {3}\n"
                                + "trail: {4}";

        private
        string badgePath,
               trailPath;

        private
        HomeworldColour baseColor,
                       stripeColor,
                       trailColor;

        public TeamColour(HomeworldColour baseColor, HomeworldColour stripeColor, HomeworldColour? trailColor, string badgePath, string? trailPath)
        {
            this.baseColor = baseColor;
            this.stripeColor = stripeColor;
            this.trailColor = trailColor == null ? baseColor : trailColor;

            this.badgePath = badgePath;
            this.trailPath = trailPath == null ? String.Empty : trailPath;
        }

        public void SetBaseColor(byte r, byte g, byte b)
        {
            SetBaseColor(new HomeworldColour(r, g, b));
        }

        public void SetBaseColor(HomeworldColour baseColor)
        {
            this.baseColor = baseColor;
        }

        public HomeworldColour GetBaseColor()
        {
            return baseColor;
        }

        public ref HomeworldColour getBaseColorRef()
        {
            return ref baseColor;
        }


        public void SetStripeColor(byte r, byte g, byte b)
        {
            SetStripeColor(new HomeworldColour(r, g, b));
        }

        public void SetStripeColor(HomeworldColour stripeColor)
        {
            this.stripeColor = stripeColor;
        }


        public HomeworldColour GetStripeColor()
        {
            return stripeColor;
        }

        public void SetTrailColor(byte r, byte g, byte b)
        {
            SetTrailColor(new HomeworldColour(r, g, b));
        }

        public void SetTrailColor(HomeworldColour trailColor)
        {
            this.trailColor = trailColor;
        }

        public HomeworldColour GetTrailColor()
        {
            return trailColor;
        }

        public void SetBadgePath(string badgePath)
        {
            this.badgePath = badgePath;
        }

        public string GetBadgePath()
        {
            return badgePath;
        }

        public void SetTrailPath(string trailPath)
        {
            this.trailPath = trailPath;
        }

        public string GetTrailPath()
        {
            return trailPath;
        }

        public override string ToString()
        {
            return String.Format(TO_STRING_FORMAT, baseColor, stripeColor, trailColor, badgePath, trailPath);
        }
    }
}