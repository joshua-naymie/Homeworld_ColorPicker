using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Services
{
    using Homeworld_ColorPicker.Controls;
    using Objects;
    public class LevelTabGenerator
    {
        /// <summary>
        /// Custom font used for controls on the tab pages.
        /// </summary>
        private static readonly
        Font CUSTOM_FONT;

        static LevelTabGenerator()
        {
            CUSTOM_FONT = new Font(CONST.CUSTOM_FONT, 13);
        }

        /// <summary>
        /// The action all ColourBoxes will take when left clicked.
        /// Can be null, no action will be invoked.
        /// </summary>
        private
        Action<ColourBox>? colourLeftClick;

        /// <summary>
        /// The action all ColourBoxes will take when right clicked.
        /// Can be null, no action will be invoked.
        /// </summary>
        private
        Action<ColourBox>? colourRightClick;

        /// <summary>
        /// The action all ColourBoxes will take when middle clicked.
        /// Can be null, no action will be invoked.
        /// </summary>
        private
        Action<ColourBox>? colourMiddleClick;


        /// <summary>
        /// The action all badgeBoxes will take when left clicked.
        /// Can be null, no action will be invoked.
        /// </summary>
        private
        Action<BadgeBox>? badgeLeftClick;

        /// <summary>
        /// The action all badgeBoxes will take when right clicked.
        /// Can be null, no action will be invoked.
        /// </summary>
        private
        Action<BadgeBox>? badgeRightClick;

        /// <summary>
        /// The action all badgeBoxes will take when middle clicked.
        /// Can be null, no action will be invoked.
        /// </summary>
        private
        Action<BadgeBox>? badgeMiddleClick;

        /// <summary>
        /// Used to set any actions taken when a generated ColourBox is left, right, and middle clicked.
        /// Actions can be null, will not be invoked.
        /// </summary>
        /// <param name="leftClickAction">The action to take on a left click event.</param>
        /// <param name="rightClickAction">The action to take on a right click event.</param>
        /// <param name="middleClickAction">The action to take on a middle click event.</param>
        public void SetColourActions(Action<ColourBox>? leftClickAction, Action<ColourBox>? rightClickAction, Action<ColourBox>? middleClickAction)
        {
            colourLeftClick = leftClickAction;
            colourRightClick = rightClickAction;
            colourMiddleClick = middleClickAction;
        }

        /// <summary>
        /// Used to set any actions taken when a generated BadgeBox is left, right, and middle clicked.
        /// Actions can be null, will not be invoked.
        /// </summary>
        /// <param name="leftClickAction">The action to take on a left click event.</param>
        /// <param name="rightClickAction">The action to take on a right click event.</param>
        /// <param name="middleClickAction">The action to take on a middle click event.</param>
        public void SetBadgeActions(Action<BadgeBox>? leftClickAction, Action<BadgeBox>? rightClickAction, Action<BadgeBox>? middleClickAction)
        {
            badgeLeftClick = leftClickAction;
            badgeRightClick = rightClickAction;
            badgeMiddleClick = middleClickAction;
        }


        /// <summary>
        /// Generates a TabPage for a single level.
        /// Includes all BadgeBoxes, ColourBoxes and reset button for each team in the level.
        /// Also includes column labels and level reset button.
        /// </summary>
        /// <param name="teams">TeamColours of every team in the level. Used to generate the TeamPanels.</param>
        /// <param name="levelNum">The level number this tab page represents.</param>
        /// <returns>A TabPage representing a single level from a Homeworld campaign with all controls added</returns>
        public TabPage GenerateTabPage(TeamColour[] teams, int levelNum)
        {
            TabPage page = new TabPage();
            page.BackColor = Color.White;
            page.Text = $"Level {levelNum+1}";
            page.Controls.Add(GenerateContentPanel(teams, levelNum));
            page.Controls.Add(GenerateHeader());

            return page;
        }

        /// <summary>
        /// Generates the content section of the tab page.
        /// This area has all the individual team's TeamPanels which generate all the team's controls.
        /// </summary>
        /// <param name="level">TeamColours of every team in the level. Used to generate the TeamPanels.</param>
        /// <param name="levelNum">The level number this tab page represents. Used for teamname dictionary lookup.</param>
        /// <returns>The content section of the tab page.</returns>
        private Panel GenerateContentPanel(TeamColour[] level, int levelNum)
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.AutoScroll = false;
            panel.HorizontalScroll.Enabled = false;
            panel.HorizontalScroll.Visible = false;
            panel.HorizontalScroll.Maximum = 0;
            panel.AutoScroll = true;

            int teamNum = 0,
                teamOffset = 0;

            foreach (TeamColour team in level)
            {
                TeamPanel teamPanel = new TeamPanel(CONST.DICT_HW2_LEVEL_TEAM_NAMES[new Tuple<int, int>(levelNum, teamNum++)], team);
                teamPanel.Location = new Point(0, teamOffset);
                teamPanel.SetColourBoxActions(colourLeftClick, colourRightClick, colourMiddleClick);
                teamPanel.SetBadgeBoxActions(badgeLeftClick, badgeRightClick, badgeMiddleClick);

                panel.Controls.Add(teamPanel);

                teamOffset += teamPanel.Height;
            }

            return panel;
        }


        /// <summary>
        /// Generates the header section of the tab page.
        /// This includes column-labels and level-reset button.
        /// </summary>
        /// <returns>The header sectino of the tab page.</returns>
        private Panel GenerateHeader()
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Top;
            panel.Height = 60;

            //----------

            // LEVEL-RESET BUTTON
            Button resetButton = new Button();
            resetButton.Font = CUSTOM_FONT;
            resetButton.Location = new System.Drawing.Point(6, 6);
            resetButton.Size = new System.Drawing.Size(170, 48);
            resetButton.Text = "Reset All";
            resetButton.UseVisualStyleBackColor = true;

            // BADGE LABEL
            Label badgeLabel = new Label();
            badgeLabel.Font = CUSTOM_FONT;
            badgeLabel.Location = new System.Drawing.Point(240, 14);
            badgeLabel.Text = "Badge";
            badgeLabel.Size = Util.GetLabelSize(badgeLabel);
            badgeLabel.BorderStyle = BorderStyle.FixedSingle;

            // BASE LABEL
            Label baseLabel = new Label();
            baseLabel.Font = CUSTOM_FONT;
            baseLabel.Location = new System.Drawing.Point(405, 14);
            baseLabel.Text = "Base";
            baseLabel.Size = Util.GetLabelSize(baseLabel);
            baseLabel.BorderStyle = BorderStyle.FixedSingle;

            // STRIPE LABEL
            Label stripeLabel = new Label();
            stripeLabel.Font = CUSTOM_FONT;
            stripeLabel.Location = new System.Drawing.Point(540, 10);
            stripeLabel.Text = "Stripe";
            stripeLabel.Size = Util.GetLabelSize(stripeLabel);
            stripeLabel.BorderStyle = BorderStyle.FixedSingle;

            // TRAIL LABEL
            Label trailLabel = new Label();
            trailLabel.Font = CUSTOM_FONT;
            trailLabel.Location = new System.Drawing.Point(710, 10);
            trailLabel.Text = "Trail";
            trailLabel.Size = Util.GetLabelSize(trailLabel);
            trailLabel.BorderStyle = BorderStyle.FixedSingle;

            //----------

            // ADD CONTROLS
            panel.Controls.Add(resetButton);
            panel.Controls.Add(badgeLabel);
            panel.Controls.Add(baseLabel);
            panel.Controls.Add(stripeLabel);
            panel.Controls.Add(trailLabel);

            return panel;
        }
    }
}