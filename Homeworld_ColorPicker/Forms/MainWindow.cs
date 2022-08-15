using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Homeworld_ColorPicker.Forms
{
    using Objects;
    using Controls;
    
    /// <summary>
    /// The Main window of the application.  Runs the DirectoryDialog and, if necessary, the BigExtractor dialog before first show.
    /// Program closes if either of these dialogs return DialogResult.Cancel. 
    /// </summary>
    public partial class MainWindow : Form
    {
        private const
        int POS_START = 12,
            SPACE_BETWEEN_SWATCHES = 56,
            SWATCH_SIZE = 50;

        /// <summary>
        /// The current instance of the game to work on.
        /// </summary>
        private
        GameInstance instance;

        /// <summary>
        /// The dialog used to set or change the directories for Homeworld root, Remastered Toolkit, and Profile.
        /// </summary>
        private
        DirectoryDialog directoryDialog;

        /// <summary>
        /// A form that allows the user to pick a custom colour.
        /// </summary>
        private
        ColorDialog customColorDialog = new ColorDialog();

        /// <summary>
        /// A form that allows the user to pick a badge.
        /// </summary>
        private
        BadgePickerDialog badgeDialog;

        /// <summary>
        /// The box showing the currently selected colour.
        /// </summary>
        private
        ColourBox currentColourBox;

        /// <summary>
        /// A sorted array of all player colour swatches.
        /// </summary>
        private readonly
        ColourBox[] colorSwatches = new ColourBox[CONST.NUM_PLAYER_COLORS];

        /// <summary>
        /// A list of all the team panels present on all tab pages.
        /// Does not include the global tab page.
        /// </summary>
        private
        List<TeamPanel> allTeamPanels = new List<TeamPanel> ();

        private
        HomeworldTabControl mainTabControl;

        // CONSTRUCTOR
        //----------------------------------------

        /// <summary>
        /// Constructor for MainWindow.
        /// </summary>
        public MainWindow()
        {
            directoryDialog = new DirectoryDialog();
            

            bool continueRunning = ShowDirectoryDialog(IO.ConfigManager.ReadConfig()) == DialogResult.OK;

            if (continueRunning && !IO.ExtractedDataManager.VerifyRequiredFiles(instance))
            {
                BigExtractorDialog extractionDialog = new BigExtractorDialog(instance);
                continueRunning = extractionDialog.ShowDialog() == DialogResult.OK;
            }

            // close before load if directory dialog canceled
            if (!continueRunning)
            {
                Load += (s, e) => Close();
            }

            //----------

            InitializeComponent();
        }

        /// <summary>
        /// Override of OnLoad.
        /// Initializes all custom controls.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnLoad(EventArgs e)
        {
            InitColourSwatches();
            InitCurrentColour();
            LoadProfileColours();

            badgeDialog = new BadgePickerDialog(instance);
            customColorButton.Font = new Font(CONST.CUSTOM_FONT, 11);

            base.OnLoad(e);

            this.Controls.Remove(levelTabControl);
            InitTabControl(GetHomeworld2Campaign());
        }

        // UI
        //----------------------------------------

        /// <summary>
        /// Initializes all player color swatches.
        /// Gets and assigns player colors from <c>PLAYERCFG.LUA</c>.
        /// Adds Click events to all ColorBoxes.
        /// </summary>
        private void InitColourSwatches()
        {
            
            int posX = POS_START;

            for (int i=0; i<CONST.NUM_PLAYER_COLORS; i++)
            {
                colorSwatches[i] = new ColourBox();
                InitSwatch(ref colorSwatches[i], posX);
                colorPanel.Controls.Add(colorSwatches[i]);

                posX += SPACE_BETWEEN_SWATCHES;
            }
        }

        //----------------------------------------

        /// <summary>
        /// Sets all ColourBox properties for a colour swatch
        /// </summary>
        /// <param name="box">The box to apply the properties too</param>
        /// <param name="posX">The X position to assign the box</param>
        private void InitSwatch(ref ColourBox box, int posX)
        {
            box.BorderStyle = BorderStyle.Fixed3D;
            box.Location = new Point(posX, POS_START);
            box.Size = new Size(SWATCH_SIZE, SWATCH_SIZE);
            box.TabStop = false;
            box.SetLeftClickAction(GetBoxColour);
        }

        //----------------------------------------

        /// <summary>
        /// Initializes the current color ColorBox.
        /// Sets the color to the first player color.
        /// </summary>
        private void InitCurrentColour()
        {
            currentColourBox = new ColourBox();
            currentColourBox.BorderStyle = BorderStyle.Fixed3D;
            currentColourBox.Location = new Point(12, 68);
            currentColourBox.Name = "currentColorSwatch";
            currentColourBox.Size = new Size(890, 50);
            currentColourBox.TabIndex = 17;
            currentColourBox.TabStop = false;
            colorPanel.Controls.Add(currentColourBox);
        }

        //----------------------------------------

        /// <summary>
        /// Loads all the player colours from their PLAYER.CFG file.
        /// </summary>
        private void LoadProfileColours()
        {
            HomeworldColour[] playerColors = IO.ColorReader.GetPlayerColors(instance.HomeworldRootDir + instance.ProfilePath);

            int i = 0;
            foreach(var color in playerColors)
            {
                colorSwatches[i++].Colour = color;
            }

            currentColourBox.Colour = playerColors[0];
        }

        //----------------------------------------

        private HomeworldCampaign GetHomeworld2Campaign()
        {
            HomeworldCampaign campaign = new HomeworldCampaign();

            int levelNum = 0;
            foreach (string levelPath in CONST.HW2_TEAMCOLOR_PATHS)
            {
                string path = CONST.DIR_HW2_RM_EXTRACTED_DATA_PATH + levelPath + CONST.FILE_TEAMCOLOUR_LUA;
                TeamColour[] teamColours = IO.TeamColourReader.ReadTeamColourLua(path);

                Team[] teams = new Team[teamColours.Length];

                int teamNum = 0;
                foreach (TeamColour colours in teamColours)
                {
                    Team team = new Team(CONST.DICT_HW2_LEVEL_TEAM_NAMES[new Tuple<int, int>(levelNum, teamNum)]);
                    team.Colours = colours;
                    teams[teamNum++] = team;
                }

                campaign.Add(new HomeworldLevel(teams, levelNum++));
            }

            return campaign;
        }

        private void InitTabControl(HomeworldCampaign campaign)
        {
            BoxActions defaultActions = new BoxActions(SetBoxColour, GetBoxColour, null, SetBadge, null, null);
            BoxActions globalActions = new BoxActions(SetGlobalColour, GetBoxColour, null, SetGlobalBadge, null, null);

            mainTabControl = new HomeworldTabControl(GetHomeworld2Campaign(), defaultActions, globalActions);
            mainTabControl.Dock = DockStyle.Fill;
            mainTabControl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            mainTabControl.Location = new Point(0, 142);
            mainTabControl.Name = "levelTabControl";
            mainTabControl.SelectedIndex = 0;
            mainTabControl.Size = new Size(1017, 615);
            mainTabControl.TabIndex = 18;

            this.Controls.Add(mainTabControl);
            this.Controls.SetChildIndex(mainTabControl, 0);
        }

        // EVENTS
        //----------------------------------------

        /// <summary>
        /// Opens a ColorDialog for the user to set a custom color.
        /// Assigns the custom color to the current color if DialogResult is OK.
        /// </summary>
        /// <param name="sender">The object that called the event</param>
        /// <param name="e">The mouse event arguments</param>
        private void SetCustomColor(object sender, MouseEventArgs e)
        {
            customColorDialog.Color = currentColourBox.BackColor;
            customColorDialog.FullOpen = true;

            if(customColorDialog.ShowDialog() == DialogResult.OK)
            {
                currentColourBox.Colour = new HomeworldColour(customColorDialog.Color);
            }
        }

        //----------------------------------------

        /// <summary>
        /// Sets the current colour from another ColourBox
        /// </summary>
        /// <param name="box">The ColourBox to copy the colour from</param>
        private void GetBoxColour(ColourBox box)
        {
            currentColourBox.Colour = box.Colour;
        }

        //----------------------------------------

        /// <summary>
        /// Sets a ColourBox to the current colour
        /// </summary>
        /// <param name="box">The ColourBox you want to set</param>
        private void SetBoxColour(ColourBox box)
        {
            box.Colour = currentColourBox.Colour;
        }

        //----------------------------------------

        /// <summary>
        /// Sets the selected badge on the BadgeDialog form then shows the dialog.
        /// </summary>
        /// <param name="box">The BadgeBox used to set the selected badge</param>
        private void SetBadge(BadgeBox box)
        {
            badgeDialog.SetSelectedBadge(box.Path);
            if(badgeDialog.ShowDialog() == DialogResult.OK)
            {
                box.SetImage(badgeDialog.ImagePath);
            }
        }

        private void SetGlobalColour(ColourBox box)
        {
            SetBoxColour(box);

            TeamPanel parent = (TeamPanel)box.Parent;

            List<TeamPanel> teams = mainTabControl.TeamGroups[parent.Team.Type];

            foreach (TeamPanel panel in teams)
            {
                if (panel.Team.Type == parent.Team.Type)
                {
                    panel.Colours = parent.Colours;
                }
            }
        }

        private void ExportTeamColours(object sender, EventArgs e)
        {
            IO.CampaignWriter.WriteCampaignData(mainTabControl.GetCampaignData(), instance);
        }

        private void SetGlobalBadge(BadgeBox box)
        {
            SetBadge(box);

            TeamPanel parent = (TeamPanel)box.Parent;

            List<TeamPanel> teams = mainTabControl.TeamGroups[parent.Team.Type];

            foreach (TeamPanel panel in teams)
            {
                if (panel.Team.Type == parent.Team.Type)
                {
                    panel.Colours = parent.Colours;
                }
            }
        }

        // DIALOGS
        //----------------------------------------

        /// <summary>
        /// Shows the DirectoryDialog.
        /// If result is OK, creates a new game instance from the provided directories.
        /// </summary>
        /// <param name="rootDirectories">The current root directories being used</param>
        /// <returns>The DialogResult returned by the DirectoryDialog</returns>
        private DialogResult ShowDirectoryDialog(RootDirectoryData rootDirectories)
        {
            directoryDialog.SetRootDirectories(rootDirectories);

            DialogResult result = directoryDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                instance = directoryDialog.GameInstance;
                badgeDialog = new BadgePickerDialog(instance);
            }

            return result;
        }
    }
}