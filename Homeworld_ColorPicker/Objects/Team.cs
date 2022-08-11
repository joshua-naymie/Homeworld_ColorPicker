using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Objects
{
    /// <summary>
    /// The unique types of teams present in the campaign.
    /// Teams on the global page will be populated with these unique teams.
    /// </summary>
    public enum TeamType
    {
        Player,
        Vaygr,
        Hiigaran,
        Bentusi,
        Progenitor,
        Sobani,
        TalornSoban,
        HiigaranElite,
        VaygrElite,
        DefenceStation,
        DefenceFleet
    }

    
    /// <summary>
    /// Holds which type of global team this team is a part of (used in the global tab) and the proper team name.
    /// </summary>
    public class Team
    {
        // PROPERTIES
        //----------------------------------------

        /// <summary>
        /// The global team this team belongs to.
        /// Used for the global page
        /// </summary>
        public TeamType Type { get; }

        /// <summary>
        /// The proper name of the team.
        /// </summary>
        public string Name { get; }

        // CONSTRUCTOR
        //----------------------------------------

        /// <summary>
        /// Constructor for Team.
        /// Must set TeamType and Name.
        /// </summary>
        /// <param name="type">The global team this team belongs to</param>
        /// <param name="name">The proper name of the team</param>
        public Team(TeamType type, string name)
        {
            Type = type;
            Name = name;
        }
    }
}