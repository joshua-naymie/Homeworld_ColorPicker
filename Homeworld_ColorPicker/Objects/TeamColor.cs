using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Objects
{
    public class TeamColor
    {
        private
        string badge;

        private
        HomeworldColor baseColor,
                       stripeColor,
                       trailColor;

        public TeamColor(HomeworldColor baseColor, HomeworldColor stripeColor, HomeworldColor trailColor, String badge)
        {
            this.baseColor = baseColor;
            this.stripeColor = stripeColor;
            this.trailColor = trailColor;

            this.badge = badge;
        }

        public void SetBaseColor(byte r, byte g, byte b)
        {
            SetBaseColor(new HomeworldColor(r, g, b));
        }

        public void SetBaseColor(HomeworldColor baseColor)
        {
            this.baseColor = baseColor;
        }

        public HomeworldColor getBaseColor()
        {
            return baseColor;
        }


        public void SetStripeColor(byte r, byte g, byte b)
        {
            SetStripeColor(new HomeworldColor(r, g, b));
        }

        public void SetStripeColor(HomeworldColor stripeColor)
        {
            this.stripeColor = stripeColor;
        }


        public HomeworldColor getStripeColor()
        {
            return stripeColor;
        }

        public void SetTrailColor(byte r, byte g, byte b)
        {
            SetTrailColor(new HomeworldColor(r, g, b));
        }

        public void SetTrailColor(HomeworldColor trailColor)
        {
            this.trailColor = trailColor;
        }

        public HomeworldColor getTrailColor()
        {
            return trailColor;
        }
    }
}
