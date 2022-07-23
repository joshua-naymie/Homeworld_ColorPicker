using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homeworld_ColorPicker.Forms
{
    using Objects;
    using Controls;

    public partial class MainWindow : Form
    {
        private const
        int NUMBER_OF_SWATCHES = 16;

        private
        string homeworldDirPath;

        private
        string toolkitDirPath;

        private
        Profile userProfile;

        /// <summary>
        /// The dialog used to set or change the directories for Homeworld root, Remastered Toolkit, and Profile.
        /// </summary>
        private
        DirectoryDialog directoryDialog = new DirectoryDialog();

        private
        ColorDialog customColorDialog = new ColorDialog();

        private
        ColorBox currentColorBox;

        private
        ColorBox[] colorSwatches = new ColorBox[GC.NUM_PLAYER_COLORS];

        /// <summary>
        /// Constructor for MainWindow.
        /// </summary>
        public MainWindow()
        {
            if(ShowDirectoryDialog() == DialogResult.Cancel)
            {
                Load += (s, e) => Close();
            }
            
            //----------

            InitializeComponent();

            InitColorSwatches();
            InitCurrentColor();
        }

        /// <summary>
        /// Initializes all player color swatches.
        /// Gets and assigns player colors from <c>PLAYERCFG.LUA</c>.
        /// Adds Click events to all ColorBoxes.
        /// </summary>
        private void InitColorSwatches()
        {
            colorSwatches[0] = new ColorBox(colorSwatch1);
            colorSwatches[1] = new ColorBox(colorSwatch2);
            colorSwatches[2] = new ColorBox(colorSwatch3);
            colorSwatches[3] = new ColorBox(colorSwatch4);
            colorSwatches[4] = new ColorBox(colorSwatch5);
            colorSwatches[5] = new ColorBox(colorSwatch6);
            colorSwatches[6] = new ColorBox(colorSwatch7);
            colorSwatches[7] = new ColorBox(colorSwatch8);
            colorSwatches[8] = new ColorBox(colorSwatch9);
            colorSwatches[9] = new ColorBox(colorSwatch10);
            colorSwatches[10] = new ColorBox(colorSwatch11);
            colorSwatches[11] = new ColorBox(colorSwatch12);
            colorSwatches[12] = new ColorBox(colorSwatch13);
            colorSwatches[13] = new ColorBox(colorSwatch14);
            colorSwatches[14] = new ColorBox(colorSwatch15);
            colorSwatches[15] = new ColorBox(colorSwatch16);

            //System.Diagnostics.Debug.WriteLine(homeworldDirPath + userProfile.GetPath() + GC.FILE_PLAYERCFG_LUA);

            HomeworldColor[] playerColors = IO.ColorReader.GetPlayerColors(homeworldDirPath + userProfile.GetPath()
                                                                                            + GC.FILE_PLAYERCFG_LUA);
            int i = 0;
            foreach (ColorBox swatch in colorSwatches)
            {
                swatch.SetColor(playerColors[i++]);
                swatch.SetLeftClickAction(ColorSwatchClicked);
            }
        }

        /// <summary>
        /// Initializes the current color ColorBox.
        /// Sets the color to the first player color.
        /// </summary>
        private void InitCurrentColor()
        {
            currentColorBox = new ColorBox(currentColorSwatch);
            SetCurrentColor(colorSwatches[0].GetColor());
        }

        /// <summary>
        /// Opens a ColorDialog for the user to set a custom color.
        /// Assigns the custom color to the current color if DialogResult is OK.
        /// </summary>
        /// <param name="sender">The object that called the event</param>
        /// <param name="e">The mouse event arguments</param>
        private void SetCustomColor(object sender, MouseEventArgs e)
        {
            customColorDialog.Color = currentColorSwatch.BackColor;
            customColorDialog.FullOpen = true;

            if(customColorDialog.ShowDialog() == DialogResult.OK)
            {
                SetCurrentColor(new HomeworldColor(customColorDialog.Color));
            }
        }

        private void ColorSwatchClicked(ColorBox swatch)
        {
            SetCurrentColor(swatch.GetColor());
        }

        private void SetCurrentColor(HomeworldColor color)
        {
            currentColorBox.SetColor(color);
        }

        private DialogResult ShowDirectoryDialog()
        {
            DialogResult result = directoryDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                homeworldDirPath = directoryDialog.GetRootDirectory();
                toolkitDirPath = directoryDialog.GetToolkitDirectory();

                userProfile = directoryDialog.GetProfile();
            }

            return result;
        }

        private void ReadPlayerColors()
        {

        }
    }
}