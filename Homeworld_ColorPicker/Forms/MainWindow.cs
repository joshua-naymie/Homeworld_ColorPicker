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
    using System.Runtime.InteropServices;
    using System.Drawing.Text;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    public partial class MainWindow : Form
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        private const
        int NUMBER_OF_SWATCHES = 16;

        private
        GameInstance instance;


        /// <summary>
        /// The dialog used to set or change the directories for Homeworld root, Remastered Toolkit, and Profile.
        /// </summary>
        private
        DirectoryDialog directoryDialog;

        private
        ColorDialog customColorDialog = new ColorDialog();

        private
        ColourBox currentColorBox;

        private
        ColourBox[] colorSwatches = new ColourBox[GC.NUM_PLAYER_COLORS];

        PrivateFontCollection pfc = new PrivateFontCollection();

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

            if (!continueRunning)
            {
                Load += (s, e) => Close();
            }

            //----------

            InitCustomFont();
            InitializeComponent();
            customColorButton.Font = GC.CUSTOM_FONT;//new Font(pfc.Families[0], 11);
            //label1.Font = new Font(pfc.Families[0], label1.Font.Size);
            //label1.Text = "HOMEWORLD 2";

            if (continueRunning)
            {
                InitColorSwatches();
                InitCurrentColor();
                InitTabPages();
            }

            Paloma.TargaImage t = new Paloma.TargaImage(@"G:\Documents\Homeworld ColorPicker\output.hwr2\badges\hiigaran.tga");
            pictureBox1.BackColor = Color.Green;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = t.Image;// ResizeImage(t.Image, 100, 100);
            System.Diagnostics.Debug.WriteLine(t.Image);
        }

        private void InitCustomFont()
        {
            // Select your font from the resources
            int fontLength = Properties.Resources.Microgramma_Font.Length;

            // create a buffer to read in to
            byte[] fontdata = Properties.Resources.Microgramma_Font;

            // create an unsafe memory block for the font data
            System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);

            // copy the bytes to the unsafe memory block
            Marshal.Copy(fontdata, 0, data, fontLength);

            uint cFonts = 0;
            AddFontMemResourceEx(data, (uint)fontdata.Length, IntPtr.Zero, ref cFonts);

            // pass the font to the font collection
            pfc.AddMemoryFont(data, fontLength);

            Marshal.FreeCoTaskMem(data);
        }

        /// <summary>
        /// Initializes all player color swatches.
        /// Gets and assigns player colors from <c>PLAYERCFG.LUA</c>.
        /// Adds Click events to all ColorBoxes.
        /// </summary>
        private void InitColorSwatches()
        {
            ColourBox colorSwatch1 = new ColourBox();
            ColourBox colorSwatch2 = new ColourBox();
            ColourBox colorSwatch3 = new ColourBox();
            ColourBox colorSwatch4 = new ColourBox();
            ColourBox colorSwatch5 = new ColourBox();
            ColourBox colorSwatch6 = new ColourBox();
            ColourBox colorSwatch7 = new ColourBox();
            ColourBox colorSwatch8 = new ColourBox();
            ColourBox colorSwatch9 = new ColourBox();
            ColourBox colorSwatch10 = new ColourBox();
            ColourBox colorSwatch11 = new ColourBox();
            ColourBox colorSwatch12 = new ColourBox();
            ColourBox colorSwatch13 = new ColourBox();
            ColourBox colorSwatch14 = new ColourBox();
            ColourBox colorSwatch15 = new ColourBox();
            ColourBox colorSwatch16 = new ColourBox();

            HomeworldColour[] playerColors = IO.ColorReader.GetPlayerColors(instance.HomeworldRootDir + instance.ProfilePath);
            int posX = 12;

            for (int i=0; i<NUMBER_OF_SWATCHES; i++)
            {
                colorSwatches[i] = new ColourBox(playerColors[i]);
                InitSwatch(ref colorSwatches[i], posX);
                colorPanel.Controls.Add(colorSwatches[i]);

                posX += 56;
            }
        }

        private void InitSwatch(ref ColourBox box, int posX)
        {
            box.BorderStyle = BorderStyle.Fixed3D;
            box.Location = new System.Drawing.Point(posX, 12);
            box.Name = "colorSwatch1";
            box.Size = new System.Drawing.Size(50, 50);
            box.TabIndex = 0;
            box.TabStop = false;
            box.SetLeftClickAction(SetCurrentFromBox);
        }


        /// <summary>
        /// Initializes the current color ColorBox.
        /// Sets the color to the first player color.
        /// </summary>
        private void InitCurrentColor()
        {
            currentColorBox = new ColourBox(colorSwatches[0].GetColor());
            currentColorBox.BorderStyle = BorderStyle.Fixed3D;
            currentColorBox.Location = new Point(12, 68);
            currentColorBox.Name = "currentColorSwatch";
            currentColorBox.Size = new Size(890, 50);
            currentColorBox.TabIndex = 17;
            currentColorBox.TabStop = false;
            //SetCurrentColor(colorSwatches[0].GetColor());
            colorPanel.Controls.Add(currentColorBox);
        }

        private void InitTabPages()
        {
            int levelNum = 0;
            foreach(string level in GC.HW2_TEAMCOLOR_PATHS)
            {
                string path = GC.DIR_HW2_RM_DATA_PATH + level + GC.FILE_TEAMCOLOUR_LUA;
                TeamColour[] testLevel = IO.TeamColourReader.ReadTeamColourLua(path);

                Services.LevelTabGenerator tabGenerator = new Services.LevelTabGenerator();
                tabGenerator.SetColourActions(SetBoxFromCurrent, SetCurrentFromBox, null);

                TabPage page = tabGenerator.GenerateTabPage(testLevel, levelNum++);
                levelTabControl.Controls.Add(page);
            }
        }

        /// <summary>
        /// Opens a ColorDialog for the user to set a custom color.
        /// Assigns the custom color to the current color if DialogResult is OK.
        /// </summary>
        /// <param name="sender">The object that called the event</param>
        /// <param name="e">The mouse event arguments</param>
        private void SetCustomColor(object sender, MouseEventArgs e)
        {
            customColorDialog.Color = currentColorBox.BackColor;
            customColorDialog.FullOpen = true;

            if(customColorDialog.ShowDialog() == DialogResult.OK)
            {
                SetCurrentColor(new HomeworldColour(customColorDialog.Color));
            }
        }

        private void SetCurrentFromBox(ColourBox swatch)
        {
            SetCurrentColor(swatch.GetColor());
            System.Diagnostics.Debug.WriteLine(swatch);
        }

        private void SetCurrentColor(HomeworldColour color)
        {
            currentColorBox.SetColor(color);
        }

        private DialogResult ShowDirectoryDialog(RootDirectoryData rootDirectories)
        {
            directoryDialog.SetRootDirectories(rootDirectories);

            DialogResult result = directoryDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                instance = directoryDialog.GetInstance();
            }

            return result;
        }

        private void SetBoxFromCurrent(ColourBox box)
        {
            box.SetColor(currentColorBox.GetColor());
            System.Diagnostics.Debug.WriteLine(box);
        }
    }
}