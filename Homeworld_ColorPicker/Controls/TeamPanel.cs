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
        // STATIC
        //----------------------------------------

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
        /// Static Constructor for TeamPanel.
        /// Initializes the custom label font.
        /// </summary>
        static TeamPanel()
        {
            LABEL_FONT = new Font(CONST.CUSTOM_FONT, LABEL_FONT_SIZE);
        }

        // INSTANCE
        //----------------------------------------

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
        /// The button used to call the ResetPressed event.
        /// </summary>
        private
        Button resetButton;

        /// <summary>
        /// The list of TeamPanels to reset when this TeamPanel's reset button is clicked.
        /// This TeamPanel is added on creation but others can be added after.
        /// Used by global tab page to reset multiple TeamPanels.
        /// </summary>
        private
        List<TeamPanel> panelsToReset = new List<TeamPanel>();

        /// <summary>
        /// The deafult team settings for this TeamPanel
        /// </summary>
        private
        Team defaultTeam;

        // PROPERTIES
        //----------------------------------------

        /// <summary>
        /// Paired with Team property.
        /// </summary>
        private Team _Team;

        /// <summary>
        /// The teamdata associated with this team
        /// </summary>
        public Team Team
        {
            get
            {
                _Team.Colours = Colours;
                return _Team;
            }
            private set
            {
                _Team = new Team(value);
                _Team.Colours = value.Colours;
            }
        }

        /// <summary>
        /// The team colours and paths currently set.
        /// </summary>
        public TeamColour Colours
        { 
            get 
            { 
                return new TeamColour(baseColourBox.Colour, stripeColourBox.Colour, trailColourBox.Colour, badge.Path, _Team.Colours.TrailPath);
            }
            set
            {
                badge.SetImage(value.BadgePath);

                baseColourBox.Colour = value.BaseColour;
                stripeColourBox.Colour = value.StripeColour;
                trailColourBox.Colour = value.TrailColour;
            }
        }

        // CONSTRUCTORS
        //----------------------------------------

        /// <summary>
        /// Constructor for TeamPanel.
        /// </summary>
        /// <param name="team">The team attributes for the team.</param>
        /// <param name="teamColours">The team colours for the team.</param>
        public TeamPanel(Team team)
        {
            this.Team = team;
            this.defaultTeam = team;

            InitControls(_Team.Colours);
        }

        //--------------------

        /// <summary>
        /// Constructor for TeamPanel.
        /// Allows for default TeamColour to be set independantly from the currently displayed team colours.
        /// </summary>
        /// <param name="team">The team attributes for the team.</param>
        /// <param name="teamColours">The team colours for the team.</param>
        /// <param name="defaultColours">The default team colours for the team.</param>
        public TeamPanel(Team team, TeamColour teamColours)
        {
            this.Team = team;

            InitControls(teamColours);
        }

        // CONTROL SETUP
        //----------------------------------------

        /// <summary>
        /// Initializes all sub controls for the panel.
        /// </summary>
        /// <param name="teamColours">The teamcolours to be assigned to the ClickableBoxes.</param>
        private void InitControls(TeamColour teamColours)
        {
            int startX = START_POS_X,
                posY = START_POS_Y;

            this.Padding = new Padding(0, PANEL_PADDING, 0, PANEL_PADDING);
            this.Width = PANEL_WIDTH;
            this.Height = PANEL_HEIGHT;

            // LABEL
            Label teamName = new Label();
            teamName.Text = $"{_Team.Name}:";    
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
            resetButton = new Button();
            resetButton.Location = new Point(startX, posY);
            resetButton.Size = new Size(BOX_SIZE, BOX_SIZE);
            resetButton.Text = TEXT_RESET_BUTTON;
            resetButton.BackColor = Color.Transparent;
            resetButton.Font = LABEL_FONT;
            resetButton.Click += ResetPressed;

            panelsToReset.Add(this);

            //----------

            colourBoxes[BASE_INDEX] = baseColourBox;
            colourBoxes[STRIPE_INDEX] = stripeColourBox;
            colourBoxes[TRAIL_INDEX] = trailColourBox;

            this.Controls.Add(teamName);
            this.Controls.Add(badge);
            this.Controls.Add(baseColourBox);
            this.Controls.Add(stripeColourBox);
            this.Controls.Add(trailColourBox);
            this.Controls.Add(resetButton);
        }

        //----------------------------------------

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

        //----------------------------------------

        /// <summary>
        /// Sets all the ClickableBox actions for the TeamPanel.
        /// All ColourBoxes are assigned the same actions.
        /// </summary>
        /// <param name="actions">The ClickableBox actions to assign.</param>
        public void SetBoxActions(BoxActions actions)
        {

            foreach (ColourBox box in colourBoxes)
            {
                box.SetLeftClickAction(actions.ColourLeftClick);
                box.SetRightClickAction(actions.ColourRightClick);
                box.SetMiddleClickAction(actions.ColourMiddleClick);
            }

            badge.SetLeftClickAction(actions.BadgeLeftClick);
            badge.SetRightClickAction(actions.BadgeRightClick);
            badge.SetMiddleClickAction(actions.BadgeMiddleClick);
        }

        // RESET BUTTON
        //----------------------------------------

        /// <summary>
        /// Called when the reset button is clicked.
        /// Resets all panels in the panelsToReset list.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">The arguments of the event.</param>
        public void ResetPressed(object? sender, EventArgs e)
        {
            foreach(TeamPanel panel in panelsToReset)
            {
                panel.ResetPanel();
            }
        }

        //----------------------------------------

        /// <summary>
        /// Resets the panel to the TeamColours assigned on creation.
        /// </summary>
        public void ResetPanel()
        {
            this.Colours = defaultTeam.Colours;
        }

        //----------------------------------------

        /// <summary>
        /// Adds a range of TeamPanels to the list of panels to reset.
        /// Used to allow the global tab page to reset multiple TeamPanels.
        /// </summary>
        /// <param name="panels">The list of TeamPanels to add to the reset list.</param>
        public void AddPanelsToReset(List<TeamPanel> panels)
        {
            panelsToReset.AddRange(panels);
        }
    }
}