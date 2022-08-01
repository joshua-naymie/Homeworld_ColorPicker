using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Objects
{
    public enum TeamId
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

    

    public class Team
    {
        public
        TeamId Id { get; }

        public
        string Name { get; }

        public Team(TeamId id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
