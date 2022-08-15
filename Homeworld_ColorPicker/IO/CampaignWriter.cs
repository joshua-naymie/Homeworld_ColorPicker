using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.IO
{
    using Objects;

    public static class CampaignWriter
    {
        private const
        string TEAMCOLOUR_HEADER = "--Generated using Homeworld ColourPicker--\n\n\n",
               TEAMCOLOUR_START = "teamcolours =\n{\n",
               TEAM_FORMAT = "[{0}] = {{{1}, {2}, \"{3}\", {4}, \"{5}\"}}, -- {6}\n",
               TEAMCOLOUR_END = "}";

        private static
        StringBuilder output = new StringBuilder();

        public static void WriteCampaignData(HomeworldCampaign campaign, GameInstance instance)
        {
            foreach(HomeworldLevel level in campaign)
            {
                output.Clear();
                WriteLevelToFile(level);
            }
        }

        private static void WriteLevelToFile(HomeworldLevel level)
        {
            string path = CONST.DIR_HW2_RM_GENERATED_DATA_PATH + CONST.HW2_TEAMCOLOR_PATHS[level.LevelNum];
            if (!Util.PathExists(path))
            {
                new DirectoryInfo(path).Create();
            }

            output.Append(TEAMCOLOUR_HEADER);
            output.Append(TEAMCOLOUR_START);

            for(int i=0; i<level.Teams.Length; i++)
            {
                TeamColour colours = level.Teams[i].Colours;
                string baseColour = colours.BaseColour.ToString(),
                       stripeColour = colours.StripeColour.ToString(),
                       trailColour = colours.TrailColour.ToString(),
                       badgePath = colours.BadgePath,
                       trailPath = colours.TrailPath;

                output.Append(String.Format(TEAM_FORMAT, i, 
                                                         baseColour,
                                                         stripeColour,
                                                         badgePath,
                                                         trailColour,
                                                         trailPath,
                                                         level.Teams[i].Name));
            }
            output.Append(TEAMCOLOUR_END);

            path += CONST.FILE_TEAMCOLOUR_LUA;

            File.WriteAllTextAsync(path, output.ToString());
        }
    }
}
