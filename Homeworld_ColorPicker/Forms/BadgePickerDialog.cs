using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Homeworld_ColorPicker.Controls;
using Homeworld_ColorPicker.Objects;

namespace Homeworld_ColorPicker.Forms
{
    public partial class BadgePickerDialog : Form
    {
        private const
        int SELECTED_BADGE_SIZE = 100,
            BADGE_SIZE = 75,
            START_POS = 12,
            BADGE_MARGIN = 6,
            BADGE_TOTAL_SPACE = BADGE_SIZE + BADGE_MARGIN,
            BADGE_PANEL_MAX_WIDTH = START_POS + (BADGE_SIZE * 6) + (BADGE_MARGIN * 6),
            
            LABEL_FONT_SIZE = 15,
            LABEL_POS_X = 118,
            LABEL_POS_Y = 23;

        private const
        string CUSTOM_BADGE_PATH = @"\Data\Badges\",
               TGA_EXTENSION = ".tga";


        /// <summary>
        /// The instance of Homeworld currently being worked on.
        /// </summary>
        private
        GameInstance instance;

        /// <summary>
        /// The label for displaying the path of the currently selected badge.
        /// </summary>
        private
        Label selectedPathLabel = new Label();

        /// <summary>
        /// The BadgeBox to display the currently selected badge.
        /// </summary>
        private
        BadgeBox selectedBadgeBox = new BadgeBox();

        /// <summary>
        /// An array of all badges found.
        /// Includes default badges and any found in the badge directory of the Homeworld instance.
        /// </summary>
        private
        List<BadgeBox> boxes = new List<BadgeBox>();

        // PROPERTIES
        //----------------------------------------

        /// <summary>
        /// The path to the image currently selected on the dialog.
        /// Read-only.
        /// </summary>
        public string ImagePath { get { return selectedBadgeBox.Path; } }

        // CONSTRUCTOR
        //----------------------------------------

        /// <summary>
        /// Constructor for BadgePickerDialog.
        /// </summary>
        /// <param name="instance">The instance of Homeworld being to work on.</param>
        public BadgePickerDialog(GameInstance instance)
        {
            InitializeComponent();
            this.instance = instance;
            InitTopComponents();
            badgesPanel.Controls.Clear();
            GetDefaultBadges();
            GetCustomBadges();
            SetBoxLocations();
            this.Width += SystemInformation.VerticalScrollBarWidth;
        }

        // INIT CONTROLS
        //----------------------------------------

        /// <summary>
        /// Initializes selected-badge BadgeBox and Label.
        /// Adds them to the top panel.
        /// </summary>
        private void InitTopComponents()
        {
            selectedBadgeBox.BorderStyle = BorderStyle.Fixed3D;
            selectedBadgeBox.Location = new Point(START_POS, START_POS);
            selectedBadgeBox.Name = "selectedBadgePictureBox";
            selectedBadgeBox.Size = new Size(SELECTED_BADGE_SIZE, SELECTED_BADGE_SIZE);
            selectedBadgeBox.TabStop = false;

            selectedPathLabel.AutoSize = true;
            selectedPathLabel.Location = new Point(LABEL_POS_X, LABEL_POS_Y);
            selectedPathLabel.Name = "selectedPathLabel";
            selectedPathLabel.Font = new Font(CONST.CUSTOM_FONT, LABEL_FONT_SIZE);

            topPanel.Controls.Add(selectedBadgeBox);
            topPanel.Controls.Add(selectedPathLabel);
        }

        //--------------------

        /// <summary>
        /// Creates a new BadgeBox for each default badge in Resources.
        /// Adds them to the list of all boxes.
        /// </summary>
        private void GetDefaultBadges()
        {
            foreach(var entry in CONST.DEFAULT_BADGES_HWRM)
            {
                boxes.Add(new BadgeBox(entry.Key));
            }
        }

        //--------------------

        /// <summary>
        /// Creates a new BadgeBox for each custom badge in the custom badge directory.
        /// Adds any file that ends in .tga.
        /// </summary>
        private void GetCustomBadges()
        {
            string path = instance.HomeworldRootDir + CUSTOM_BADGE_PATH;

            foreach (FileInfo file in new DirectoryInfo(path).GetFiles())
            {
                if(file.Extension.Equals(TGA_EXTENSION))
                {
                    boxes.Add(new BadgeBox(file.FullName));
                }
            }
        }

        // UI ACTIONS
        //----------------------------------------

        /// <summary>
        /// Adds all BadgeBoxes to the badges panel.
        /// Handles spacing and line breaks.
        /// </summary>
        private void SetBoxLocations()
        {
            int posX = START_POS,
                posY = START_POS;

            foreach(BadgeBox box in boxes)
            {
                box.BorderStyle = BorderStyle.Fixed3D;
                box.Location = new Point(posX, posY);
                box.Size = new Size(BADGE_SIZE, BADGE_SIZE);
                box.SetLeftClickAction(BadgeBoxClicked);
                box.SetImage(box.Path);

                if((posX += BADGE_TOTAL_SPACE) > BADGE_PANEL_MAX_WIDTH)
                {
                    posX = START_POS;
                    posY += BADGE_TOTAL_SPACE;
                }
            }

            badgesPanel.Controls.AddRange(boxes.ToArray());
        }

        //----------------------------------------

        /// <summary>
        /// Called when the back colour TrackBar is changed.
        /// Updates all BadgeBoxes BackColors with the new gray value.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void SetBadgeBackground(object sender, EventArgs e)
        {
            float grayValue = ((float)backgroundTrackBar.Value / backgroundTrackBar.Maximum) * byte.MaxValue;
            Color background = Color.FromArgb((int)grayValue, (int)grayValue, (int)grayValue);

            selectedBadgeBox.BackColor = background;

            foreach (BadgeBox box in boxes)
            {
                box.BackColor = background;
            }
        }

        //----------------------------------------

        /// <summary>
        /// Sets the text on the path Label.
        /// Alters custom badge paths to start with "DATA:Badges/" instead of actual directory on disk.
        /// </summary>
        /// <param name="path">The file path to set the label text to.</param>
        private void SetPathLabelText(string path)
        {
            if(!path.ToUpper().StartsWith("DATA:"))
            {
                path = $"DATA:Badges/{path.Substring(instance.HomeworldRootDir.Length + CUSTOM_BADGE_PATH.Length)}";
            }

            selectedPathLabel.Text = path;
        }

        //----------------------------------------

        /// <summary>
        /// Used to set the currently selected badge.
        /// Updates the BadgeBox that shows the selected badge and the label displaying the path to the badge.
        /// </summary>
        /// <param name="path">The path to the image file of the badge.</param>
        public void SetSelectedBadge(string path)
        {
            selectedBadgeBox.SetImage(path);
            selectedPathLabel.Text = path;
        }

        // EVENTS
        //----------------------------------------

        /// <summary>
        /// Called when a BadgeBox is clicked.
        /// Sets the current-badge BadgeBox image and the path Label text.
        /// </summary>
        /// <param name="box">The BadgeBox that was clicked.</param>
        private void BadgeBoxClicked(BadgeBox box)
        {
            selectedBadgeBox.SetImage(box.Path);
            SetPathLabelText(box.Path);
        }
    }
}