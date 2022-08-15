using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Controls
{
    using Objects;

    public class HomeworldTabPage : TabPage
    {
        // STATIC
        //----------------------------------------

        /// <summary>
        /// Custom font used for controls on the tab pages.
        /// </summary>
        private static readonly
        Font CUSTOM_FONT;

        static HomeworldTabPage()
        {
            CUSTOM_FONT = new Font(CONST.CUSTOM_FONT, 13);
        }

        // INSTANCE
        //----------------------------------------

        /// <summary>
        /// The level of the campaign this tab represents.
        /// </summary>
        private
        int levelNum;

        /// <summary>
        /// The Y position used when adding a TeamPanel to the content area.
        /// </summary>
        private
        int teamPanelPosY = 0;

        // PROPERTIES
        //----------------------------------------

        /// <summary>
        /// A list of all TeamPanels on this tab page.
        /// </summary>
        public List<TeamPanel> TeamPanels { get; } = new List<TeamPanel>();

        // CONSTRUCTOR
        //----------------------------------------

        /// <summary>
        /// Constructor for HomeworldTabPage.
        /// Creates the header and all content for the tab page.
        /// </summary>
        /// <param name="level">The Teams used to create TeamPanels for the content area.</param>
        /// <param name="actions">The actions for every TeamPanel's ClickableBoxes.</param>
        public HomeworldTabPage(HomeworldLevel level, BoxActions actions)
        {
            this.BackColor = Color.White;
            this.levelNum = level.LevelNum;
            this.Text = $"Level {level.LevelNum + 1}";

            this.Controls.Add(GenerateContent(level, actions));
            this.Controls.Add(GenerateHeader());
        }

        // CONTROL GENERATION
        //----------------------------------------

        /// <summary>
        /// Generates the content area with all TeamPanels added to it.
        /// </summary>
        /// <param name="level">The Teams used to create TeamPanels for this tab page.</param>
        /// <param name="actions">The actions of all TeamPanel's ClickableBoxes.</param>
        /// <returns>The content area panel with all TeamPanels added to it.</returns>
        private Panel GenerateContent(HomeworldLevel level, BoxActions actions)
        {
            Panel contentPanel = GenerateContentPanel();

            foreach (Team team in level.Teams)
            {
                contentPanel.Controls.Add(GenerateTeamPanel(team, actions));
            }

            return contentPanel;
        }

        //----------------------------------------

        /// <summary>
        /// Generates the content area of the tab page with the correct settings.
        /// </summary>
        /// <returns>The content area panel with the correct settings.</returns>
        private Panel GenerateContentPanel()
        {
            Panel contentPanel = new Panel();
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.AutoScroll = false;
            contentPanel.HorizontalScroll.Enabled = false;
            contentPanel.HorizontalScroll.Visible = false;
            contentPanel.HorizontalScroll.Maximum = 0;
            contentPanel.AutoScroll = true;

            return contentPanel;
        }

        //----------------------------------------

        /// <summary>
        /// Generates a TeamPanel from a Team object.
        /// Assigns all actions to the TeamPanel's ClickableBoxes.
        /// Automatically handles vertical spacing of TeamPanels.
        /// </summary>
        /// <param name="team">The team data to assign the TeamPanel.</param>
        /// <param name="actions">The actions for the TeamPanel's ClickableBoxes.</param>
        /// <returns>A TeamPanel initialized to be added to the content panel.</returns>
        private Panel GenerateTeamPanel(Team team, BoxActions actions)
        {
            TeamPanel panel = new TeamPanel(team);
            panel.Location = new Point(0, teamPanelPosY);
            panel.SetBoxActions(actions);
            this.TeamPanels.Add(panel);

            teamPanelPosY += panel.Height;

            return panel;
        }

        //----------------------------------------

        /// <summary>
        /// Generates the header panel of the tab page.
        /// Includes reset page button and all column labels.
        /// </summary>
        /// <returns>The header panel of the tab page.</returns>
        private Panel GenerateHeader()
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Top;
            panel.Height = 60;

            //----------

            // LEVEL-RESET BUTTON
            Button resetButton = new Button();
            resetButton.Font = CUSTOM_FONT;
            resetButton.Location = new Point(6, 6);
            resetButton.Size = new Size(170, 48);
            resetButton.Text = "Reset All";
            resetButton.UseVisualStyleBackColor = true;
            resetButton.Click += ResetPage;

            // BADGE LABEL
            Label badgeLabel = new Label();
            badgeLabel.Font = CUSTOM_FONT;
            badgeLabel.Location = new Point(247, 18);
            badgeLabel.Text = "Badge";
            badgeLabel.Size = Util.GetLabelSize(badgeLabel);

            // BASE LABEL
            Label baseLabel = new Label();
            baseLabel.Font = CUSTOM_FONT;
            baseLabel.Location = new Point(405, 18);
            baseLabel.Text = "Base";
            baseLabel.Size = Util.GetLabelSize(baseLabel);

            // STRIPE LABEL
            Label stripeLabel = new Label();
            stripeLabel.Font = CUSTOM_FONT;
            stripeLabel.Location = new Point(550, 18);
            stripeLabel.Text = "Stripe";
            stripeLabel.Size = Util.GetLabelSize(stripeLabel);

            // TRAIL LABEL
            Label trailLabel = new Label();
            trailLabel.Font = CUSTOM_FONT;
            trailLabel.Location = new Point(705, 18);
            trailLabel.Text = "Trail";
            trailLabel.Size = Util.GetLabelSize(trailLabel);

            //----------

            // ADD CONTROLS
            panel.Controls.Add(resetButton);
            panel.Controls.Add(badgeLabel);
            panel.Controls.Add(baseLabel);
            panel.Controls.Add(stripeLabel);
            panel.Controls.Add(trailLabel);

            return panel;
        }

        // EVENTS
        //----------------------------------------

        /// <summary>
        /// Invokes the Click action for every TeamPanel's resetButton on this page.
        /// Allows the global tab page to invoke resets on all tab pages.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">The arguments of the event.</param>
        public void ResetPage(object? sender, EventArgs e)
        {
            foreach(TeamPanel panel in TeamPanels)
            {
                panel.ResetPressed(sender, e);
            }
        }

        /// <summary>
        /// Gets the team data of all teams on this tab page.
        /// </summary>
        /// <returns>The team data for all teams on this level.</returns>
        public HomeworldLevel GetLevelData()
        {
            Team[] teams = new Team[TeamPanels.Count];

            for(int i = 0; i < TeamPanels.Count; i++)
            {
                teams[i] = TeamPanels[i].Team;
            }

            HomeworldLevel level = new HomeworldLevel(teams, levelNum);

            return level;
        }
    }
}