using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Controls
{
    using Objects;

    /// <summary>
    /// Generates and displays HomeworldTabPages.
    /// Includes a global tab page to affect change on every other tab page.
    /// </summary>
    internal class HomeworldTabControl : TabControl
    {
        private const
        string GLOBAL_TAB_TEXT = "Global";

        /// <summary>
        /// The actions used for every ClickableBox on the level tabs.
        /// </summary>
        private
        BoxActions defaultActions;
        
        /// <summary>
        /// The actions used for every ClickableBox on the global tab.
        /// </summary>
        private
        BoxActions globalActions;

        /// <summary>
        /// A list of all tab pages in this tab control.
        /// </summary>
        private
        List<HomeworldTabPage> tabs = new List<HomeworldTabPage>();

        /// <summary>
        /// A dictionary of TeamPanel lists.
        /// Used for the global tab page.
        /// Each entry in the dictionary represents a unique group of teams with a common race.
        /// eg: Hiigaran, Bentusi, Vaygr, Progenitor
        /// </summary>
        public Dictionary<TeamType, List<TeamPanel>> TeamGroups { get; } = new Dictionary<TeamType, List<TeamPanel>>();

        //----------------------------------------

        /// <summary>
        /// Constructor for HomeworldTabControl.
        /// Generates all level tab pages as well as the Global tab page.
        /// </summary>
        /// <param name="campaign">The collection of every level.</param>
        /// <param name="defaultActions">The actions for every ClickableBox used in the level tabs.</param>
        /// <param name="globalActions">The actions for every ClickableBox used in the global tab.</param>
        public HomeworldTabControl(HomeworldCampaign campaign, BoxActions defaultActions, BoxActions globalActions) : base()
        {
            this.defaultActions = defaultActions;
            this.globalActions = globalActions;

            GenerateLevelTabs(campaign);
            GenerateGlobalTab();
            foreach (var tab in tabs)
            {
                this.TabPages.Add(tab);
            }
        }

        //----------------------------------------

        /// <summary>
        /// Generates HomeworldTabPages for everu level in the campaign.
        /// </summary>
        /// <param name="campaign">The collection of levels used to generate the level tab pages.</param>
        private void GenerateLevelTabs(HomeworldCampaign campaign)
        {
            foreach(HomeworldLevel level in campaign)
            {
                HomeworldTabPage levelTabPage = new HomeworldTabPage(level, defaultActions);
                tabs.Add(levelTabPage);

                foreach(TeamPanel teamPanel in levelTabPage.TeamPanels)
                {
                    if(!TeamGroups.ContainsKey(teamPanel.Team.Type))
                    {
                        TeamGroups.Add(teamPanel.Team.Type, new List<TeamPanel>());
                    }

                    TeamGroups[teamPanel.Team.Type].Add(teamPanel);
                }
            }
        }

        //----------------------------------------

        /// <summary>
        /// Generates the global tab page.
        /// Custom actions assigned to the ClickableBoxes and reset buttons.
        /// </summary>
        private void GenerateGlobalTab()
        {
            Team[] uniqueTeams = new Team[TeamGroups.Count];

            int counter = 0;
            TeamType[] allTeamTypes = (TeamType[]) Enum.GetValues(typeof(TeamType));

            foreach (TeamType uniqueTeam in allTeamTypes)
            {
                Team team = new Team(uniqueTeam, CONST.HW2_TEAM_NAMES[uniqueTeam]);
                team.Colours = TeamGroups[uniqueTeam][0].Colours;

                uniqueTeams[counter++] = team;
            }

            HomeworldLevel globalLevel = new HomeworldLevel(uniqueTeams, -1);
            HomeworldTabPage globalPage = new HomeworldTabPage(globalLevel, globalActions);
            globalPage.Text = GLOBAL_TAB_TEXT;

            // setup global reset TeamPanels
            foreach(TeamPanel panel in globalPage.TeamPanels)
            {
                panel.AddPanelsToReset(TeamGroups[panel.Team.Type]);
            }

            this.TabPages.Add(globalPage);
        }
    }
}