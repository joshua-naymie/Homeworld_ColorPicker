using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.IO
{
    using Objects;

    /// <summary>
    /// Used to read a teamcolour.lua file.
    /// </summary>
    public class TeamColourReader
    {
        private const
        int RED   = 0,
            GREEN = 1,
            BLUE  = 2;

        private const
        string TEAM_FORMAT = "[{0}]",
               LUA_COMMENT = "--";

        // BASE METHOD
        //----------------------------------------

        /// <summary>
        /// Reads a teamcolour.lua file and returns an array of teams as TeamColour objects.
        /// </summary>
        /// <param name="path">The path of the teamcolour.lua file to read</param>
        /// <returns>All team colour data present in the file</returns>
        /// <exception cref="FileNotFoundException">Thrown if the path is not valid</exception>
        public static TeamColour[] ReadTeamColourLua(string path)
        {
            if (!Util.PathExists(path))
            {
                throw new FileNotFoundException("Cannot find file " + path);
            }

            List<TeamColour> teamColours = new List<TeamColour>();

            int currentTeam = 0,
                currentIndex = 0,
                nextTeamIndex;

            string text = File.ReadAllText(path);
            text = RemoveComments(text);

            while(-1 != (currentIndex = FindNextTeamData(text, currentIndex, currentTeam)))
            {
                nextTeamIndex = FindNextTeamStart(text, currentIndex, currentTeam+1);
                if(nextTeamIndex == -1)
                {
                    nextTeamIndex = text.Length;
                }

                HomeworldColour baseColour = GetNextColour(text, ref currentIndex, nextTeamIndex);
                HomeworldColour stripeColour = GetNextColour(text, ref currentIndex, nextTeamIndex);
                string badgePath = GetNextPath(text, ref currentIndex, nextTeamIndex);
                HomeworldColour trailColour = GetNextColour(text, ref currentIndex, nextTeamIndex);
                string trailPath = GetNextPath(text, ref currentIndex, nextTeamIndex);

                teamColours.Add(new TeamColour(baseColour, stripeColour, trailColour, badgePath, trailPath));

                currentTeam++;
            }

            return teamColours.ToArray();
        }

        // REMOVE COMMENTS
        //----------------------------------------

        /// <summary>
        /// Removes any comments from the lua file text.
        /// </summary>
        /// <param name="text">The text to remove all comments from</param>
        /// <returns>The text with all comments removed</returns>
        private static string RemoveComments(string text)
        {
            int commentIndex = 0;

            while (commentIndex != -1)
            {
                commentIndex = text.IndexOf(LUA_COMMENT);

                if (commentIndex > -1)
                {
                    int newLineIndex = text.IndexOf("\n", commentIndex);

                    // removes text between comment start and new line character
                    if (newLineIndex > -1)
                    {
                        text = text.Remove(commentIndex, newLineIndex - commentIndex);
                    }

                    // removes text between comment start end of text
                    else
                    {
                        text = text.Remove(commentIndex);
                    }
                }
            }
            return text;
        }

        // FIND TEAMS
        //----------------------------------------

        /// <summary>
        /// Finds the index of a specific team's data if it exists past the current index.
        /// Does this by detecting the first '{' after the team number. (eg: [0])
        /// </summary>
        /// <param name="text">The text to search</param>
        /// <param name="currentIndex">The current index of the entire reader</param>
        /// <param name="currentTeam">The team number to search for (eg: [2])</param>
        /// <returns>The index of the next team data</returns>
        private static int FindNextTeamData(string text, int currentIndex, int currentTeam)
        {
            currentIndex = FindNextTeamStart(text, currentIndex, currentTeam);
            if(currentIndex < 0)
            {
                return currentIndex;
            }

            return text.IndexOf('{', currentIndex) + 1;
        }

        //--------------------

        /// <summary>
        /// Finds the index of a specific team number if it exists past the current index. (eg: [1])
        /// </summary>
        /// <param name="text">The text to search</param>
        /// <param name="currentIndex">The current index of the entire reader</param>
        /// <param name="currentTeam">The team number to search for (eg: [1])</param>
        /// <returns></returns>
        private static int FindNextTeamStart(string text, int currentIndex, int currentTeam)
        {
            return text.IndexOf(String.Format(TEAM_FORMAT, currentTeam), currentIndex);
        }

        // COLOUR
        //----------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="currentIndex"></param>
        /// <param name="nextTeamIndex"></param>
        /// <returns></returns>
        private static HomeworldColour GetNextColour(string text, ref int currentIndex, int nextTeamIndex)
        {
            string colourText = GetColourText(text, ref currentIndex, nextTeamIndex);
            if(colourText == null)
            {
                return null;
            }

            return ParseColour(colourText);
        }

        //--------------------

        /// <summary>
        /// Gets the substring for the next colour proceeding the currentIndex.
        /// Does not include either set of braces.
        /// </summary>
        /// <param name="text">The text to get the colour substring from</param>
        /// <param name="currentIndex">The current index of the entire reader</param>
        /// <param name="nextTeamIndex">The index for the next team number</param>
        /// <returns></returns>
        private static string GetColourText(string text, ref int currentIndex, int nextTeamIndex)
        {
            string colourText;

            int startIndex = text.IndexOf("{", currentIndex),
                endIndex = text.IndexOf("}", startIndex + 1); // +1 incase startIndex is -1

            if(endIndex > nextTeamIndex // ends in next teams data section
            || startIndex == -1)        // open brace not found
            {
                currentIndex = nextTeamIndex; //skip to next team
                return null;
            }
            else
            {
                startIndex++; // exclude opening brace
                currentIndex = endIndex;

                return text.Substring(startIndex, endIndex - startIndex);
            }
        }

        //--------------------

        /// <summary>
        /// Parses a HomeworlColour from a colour substring | Format: '.365,.553,.667'.
        /// Handles white space but not any braces.
        /// </summary>
        /// <param name="colourText">The colour substring to parse</param>
        /// <returns>A HomeworldColour object with the parsed colour values</returns>
        private static HomeworldColour ParseColour(string colourText)
        {
            string[] values = colourText.Split(",");

            float r = float.Parse(values[RED].Trim()),
                  g = float.Parse(values[GREEN].Trim()),
                  b = float.Parse(values[BLUE].Trim());

            return new HomeworldColour(r, g, b);
        }

        // DIRECTORY
        //----------------------------------------

        /// <summary>
        /// Gets the next file path value.
        /// </summary>
        /// <param name="text">The text to search</param>
        /// <param name="currentIndex">The current index of the entire reader</param>
        /// <param name="nextTeamIndex">The index for the next team number</param>
        /// <returns>The file path value</returns>
        private static string GetNextPath(string text, ref int currentIndex, int nextTeamIndex)
        {
            int startIndex = text.IndexOf("\"", currentIndex),
                endIndex = text.IndexOf("\"", startIndex + 1); // +1 incase startIndex is -1


            if (endIndex > nextTeamIndex // ends in next teams data section
            || startIndex == -1)         // open brace not found
            {
                currentIndex = nextTeamIndex; //skip to next team
                return null;
            }
            else
            {
                startIndex++; // exclude first quotation character
                currentIndex = endIndex;

                return text.Substring(startIndex, endIndex - startIndex);
            }
        }
    }
}