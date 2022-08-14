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
        /// The proper name of the team.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The global team this team belongs to.
        /// Used for the global page
        /// </summary>
        public TeamType Type { get; }

        /// <summary>
        /// The teams badge and colours.
        /// </summary>
        public TeamColour? Colours { get; set; }

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

        //----------------------------------------

        /// <summary>
        /// Creates a new team by copying values from another team.
        /// </summary>
        /// <param name="team">The team whose values will be copied.</param>
        public Team(Team team)
        {
            Type = team.Type;
            Name = team.Name;
        }

        //----------------------------------------

        /// <summary>
        /// Override of the ToString method.
        /// Outputs the Name and team Type.
        /// eg: Name: Ferin Sha Fleet | Type: Sobani
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Name: {Name} | Type: {Type}";
        }
    }
}