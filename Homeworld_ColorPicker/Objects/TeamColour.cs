using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Objects
{
    /// <summary>
    /// Represents all the attributes contained in a teamcolour.lua file.
    /// </summary>
    public class TeamColour
    {
        // CONSTANTS
        //----------------------------------------
        private const
        string TO_STRING_FORMAT = "-=COLOR=-\n"
                                + "base: {0}\n"
                                + "stripe: {1}\n"
                                + "trail: {2}\n"
                                + "-=PATH=-\n"
                                + "bagde: {3}\n"
                                + "trail: {4}";

        // PROPERTIES
        //----------------------------------------

        /// <summary>
        /// The file path to the team's badge texture.
        /// </summary>
        public string BadgePath { get; set; }

        /// <summary>
        /// The file path to the team's trail texture.
        /// </summary>
        public string TrailPath { get; set; }

        /// <summary>
        /// The base colour for the team.
        /// </summary>
        public HomeworldColour BaseColour { get; set; }

        /// <summary>
        /// The stripe colour for the team.
        /// </summary>
        public HomeworldColour StripeColour { get; set; }

        /// <summary>
        /// The trail colour for the team.
        /// </summary>
        public HomeworldColour TrailColour { get; set; }

        // CONSTRUCTOR
        //----------------------------------------

        /// <summary>
        /// Constructor for TeamColour.
        /// All required parameters are non-null.
        /// </summary>
        /// <param name="baseColor">The base colour for the team</param>
        /// <param name="stripeColor">The stripe colour for the team</param>
        /// <param name="trailColor">The trail colour for the team</param>
        /// <param name="badgePath">The file path to the team's badge texture</param>
        /// <param name="trailPath">The file path to the team's trail texture</param>
        public TeamColour(HomeworldColour baseColor, HomeworldColour stripeColor, HomeworldColour? trailColor, string badgePath, string? trailPath)
        {
            this.BaseColour = baseColor;
            this.StripeColour = stripeColor;
            this.TrailColour = trailColor == null ? baseColor : trailColor;

            this.BadgePath = badgePath;
            this.TrailPath = trailPath == null ? String.Empty : trailPath;
        }

        // UTIL
        //----------------------------------------

        /// <summary>
        /// Override for ToString.
        /// Outputs relevant about this TeamColour.
        /// </summary>
        /// <returns>BaseColour, StripeColour, TrailColour, BadgePath, TrailPath</returns>
        public override string ToString()
        {
            return String.Format(TO_STRING_FORMAT, BaseColour, StripeColour, TrailColour, BadgePath, TrailPath);
        }
    }
}