using Homeworld_ColorPicker.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Controls
{
    public class TeamPanel : Panel
    {
        private const
        int BOX_SIZE = 100,
            BASE_INDEX = 0,
            STRIPE_INDEX = 1,
            TRAIL_INDEX = 2,
            LABEL_FONT_SIZE = 13,

            START_POS_X = 240,
            START_POS_Y = 15,

            PANEL_PADDING = 15,
            PANEL_WIDTH = 985,
            PANEL_HEIGHT = 130,

            LABEL_POS_Y = 50,
            LABEL_SPACING = 6,

            BOX_SPACING = 150;

        private const
        string TEXT_RESET_BUTTON = "Reset";

        /// <summary>
        /// The custom font used on the label
        /// </summary>
        private static readonly
        Font LABEL_FONT;

        /// <summary>
        /// The teamdata associated with this team
        /// </summary>
        private
        Team team;

        /// <summary>
        /// The BadgeBox representing the team's badge
        /// </summary>
        private
        BadgeBox badge;

        /// <summary>
        /// The ColourBox representing the team's base colour.
        /// </summary>
        private
        ColourBox baseColourBox;

        /// <summary>
        /// The ColourBox representing the team's stripe colour.
        /// </summary>
        private
        ColourBox stripeColourBox;

        /// <summary>
        /// The ColourBox representing the team's trail colour.
        /// </summary>
        private
        ColourBox trailColourBox;

        /// <summary>
        /// An array of the 3 ColourBoxes representing the teams Base, Stripe and Trail colours in that order.
        /// </summary>
        private
        ColourBox[] colourBoxes = new ColourBox[3];

        /// <summary>
        /// Static Constructor for TeamPanel.
        /// Initializes the custom label font.
        /// </summary>
        static TeamPanel()
        {
            LABEL_FONT = new Font(CONST.CUSTOM_FONT, LABEL_FONT_SIZE);
        }

        /// <summary>
        /// Constructor for TeamPanel.
        /// </summary>
        /// <param name="team">The team attributes for the team</param>
        /// <param name="teamColours">The team colours for the team</param>
        public TeamPanel(Team team, TeamColour teamColours)
        {
            this.team = team;

            InitComponents(teamColours);
        }

        /// <summary>
        /// Initializes all sub controls for the panel.
        /// </summary>
        /// <param name="teamColours">The teamcolours to be assigned to the ClickableBoxes.</param>
        private void InitComponents(TeamColour teamColours)
        {
            int startX = START_POS_X,
                posY = START_POS_Y;

            this.Padding = new Padding(0, PANEL_PADDING, 0, PANEL_PADDING);
            this.Width = PANEL_WIDTH;
            this.Height = PANEL_HEIGHT;

            // LABEL
            Label teamName = new Label();
            teamName.Text = $"{team.Name}:";    
            teamName.Font = LABEL_FONT;
            teamName.Size = Util.GetLabelSize(teamName);
            teamName.Location = new Point(startX - teamName.Width - LABEL_SPACING, LABEL_POS_Y);

            // BADGE
            badge = new BadgeBox(teamColours.BadgePath);
            badge.Size = new Size(BOX_SIZE, BOX_SIZE);
            badge.BorderStyle = BorderStyle.Fixed3D;
            badge.Location = new Point(startX, posY);
            badge.SetImage(teamColours.BadgePath);

            startX += BOX_SPACING;

            // BASE
            baseColourBox = new ColourBox(teamColours.BaseColour);
            SetColourBoxProperties(baseColourBox, startX, posY);

            startX += BOX_SPACING;

            // STRIPE
            stripeColourBox = new ColourBox(teamColours.StripeColour);
            SetColourBoxProperties(stripeColourBox, startX, posY);

            startX += BOX_SPACING;

            // TRAIL
            trailColourBox = new ColourBox(teamColours.TrailColour);
            SetColourBoxProperties(trailColourBox, startX, posY);

            startX += BOX_SPACING;

            // RESET
            Button resetButton = new Button();
            resetButton.Location = new Point(startX, posY);
            resetButton.Size = new Size(BOX_SIZE, BOX_SIZE);
            resetButton.Text = TEXT_RESET_BUTTON;
            resetButton.BackColor = Color.Transparent;
            resetButton.Font = LABEL_FONT;

            //----------

            this.Controls.Add(teamName);
            this.Controls.Add(badge);
            this.Controls.Add(baseColourBox);
            this.Controls.Add(stripeColourBox);
            this.Controls.Add(trailColourBox);
            this.Controls.Add(resetButton);

            colourBoxes[BASE_INDEX] = baseColourBox;
            colourBoxes[STRIPE_INDEX] = stripeColourBox;
            colourBoxes[TRAIL_INDEX] = trailColourBox;

        }

        /// <summary>
        /// Sets all properties of a ColourBox.
        /// </summary>
        /// <param name="box">The ColourBox to assign properties to.</param>
        /// <param name="posX">The X position of the ColourBox.</param>
        /// <param name="posY">The Y position of the ColourBox.</param>
        private void SetColourBoxProperties(ColourBox box, int posX, int posY)
        {
            box.Size = new Size(BOX_SIZE, BOX_SIZE);
            box.BorderStyle = BorderStyle.Fixed3D;
            box.Location = new Point(posX, posY);
        }

        /// <summary>
        /// Sets all click actions for every ColourBox in the TeamPanel.
        /// Actions can be null and will not be invoked.
        /// </summary>
        /// <param name="leftClick">The action taken when a ColourBox is left clicked.</param>
        /// <param name="rightClick">The action taken when a ColourBox is right clicked.</param>
        /// <param name="middleClick">The action taken when a ColourBox is middle clicked.</param>
        public void SetColourBoxActions(Action<ColourBox>? leftClick, Action<ColourBox>? rightClick, Action<ColourBox>? middleClick)
        {

            foreach (ColourBox box in colourBoxes)
            {
                box.SetLeftClickAction(leftClick);
                box.SetRightClickAction(rightClick);
                box.SetMiddleClickAction(middleClick);
            }
        }

        /// <summary>
        /// Sets all click actions for every BadgeBox in the TeamPanel.
        /// Actions can be null and will not be invoked.
        /// </summary>
        /// <param name="leftClick">The action taken when a BadgeBox is left clicked.</param>
        /// <param name="rightClick">The action taken when a BadgeBox is right clicked.</param>
        /// <param name="middleClick">The action taken when a BadgeBox is middle clicked.</param>
        public void SetBadgeBoxActions(Action<BadgeBox>? leftClick, Action<BadgeBox>? rightClick, Action<BadgeBox>? middleClick)
        {
            badge.SetLeftClickAction(leftClick);
            badge.SetRightClickAction(rightClick);
            badge.SetMiddleClickAction(middleClick);
        }
    }
}