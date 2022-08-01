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
                TeamColour[] testLevel = IO.TeamColourReader.ReadTeamColourLua(@"G:\Documents\Homeworld ColorPicker\HW2_RM\leveldata\campaign\ascension\m01_tanis\teamcolour.lua");
                //TabPage page = Services.LevelTabGenerator.GenerateTabPage(testLevel, TeamColourBoxClicked);

                //foreach (var pb in this.Controls.OfType<Panel>())
                //{
                //    foreach (var pb in this.Controls.OfType<PictureBox>())
                //    {
                //        //do stuff
                //    }
                //}

                //levelTabControl.Controls.Add();//
            }
        }

        private void InitCustomFont()
        {
            //System.IntPtr data = Marshal.AllocCoTaskMem(Properties.Resources.Microgramma_Font.Length);

            //Byte[] fontData = new Byte[Properties.Resources.Microgramma_Font.Length];

            //byte[] fontStream = Properties.Resources.Microgramma_Font;

            //Marshal.Copy(fontData, 0, data, (int)fontStream.Length);

            //uint cFonts = 0;
            //AddFontMemResourceEx(data, (uint)fontData.Length, IntPtr.Zero, ref cFonts);

            //pfc.AddMemoryFont(data, (int)fontStream.Length);

            //MessageBox.Show("FONTS: " + pfc.Families.Length);

            //Marshal.FreeCoTaskMem(data);

            //Create your private font collection object.
            //System.Drawing.Text.PrivateFontCollection pfc = new System.Drawing.Text.PrivateFontCollection();




            //Select your font from the resources
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

            //colorSwatches[0] = new ColourBox(colorSwatch1);
            //colorSwatches[1] = new ColourBox(colorSwatch2);
            //colorSwatches[2] = new ColourBox(colorSwatch3);
            //colorSwatches[3] = new ColourBox(colorSwatch4);
            //colorSwatches[4] = new ColourBox(colorSwatch5);
            //colorSwatches[5] = new ColourBox(colorSwatch6);
            //colorSwatches[6] = new ColourBox(colorSwatch7);
            //colorSwatches[7] = new ColourBox(colorSwatch8);
            //colorSwatches[8] = new ColourBox(colorSwatch9);
            //colorSwatches[9] = new ColourBox(colorSwatch10);
            //colorSwatches[10] = new ColourBox(colorSwatch11);
            //colorSwatches[11] = new ColourBox(colorSwatch12);
            //colorSwatches[12] = new ColourBox(colorSwatch13);
            //colorSwatches[13] = new ColourBox(colorSwatch14);
            //colorSwatches[14] = new ColourBox(colorSwatch15);
            //colorSwatches[15] = new ColourBox(colorSwatch16);

            //System.Diagnostics.Debug.WriteLine(homeworldDirPath + userProfile.GetPath() + GC.FILE_PLAYERCFG_LUA);

            HomeworldColour[] playerColors = IO.ColorReader.GetPlayerColors(instance.HomeworldRootDir + instance.ProfilePath);
            int i = 0;
            foreach (ColourBox swatch in colorSwatches)
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
            currentColorBox = new ColourBox();
            currentColorSwatch.BorderStyle = BorderStyle.Fixed3D;
            currentColorSwatch.Location = new Point(12, 68);
            currentColorSwatch.Name = "currentColorSwatch";
            currentColorSwatch.Size = new Size(890, 50);
            currentColorSwatch.TabIndex = 17;
            currentColorSwatch.TabStop = false;
            SetCurrentColor(colorSwatches[0].GetColor());
            panel1.Controls.Add(currentColorSwatch);
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
                SetCurrentColor(new HomeworldColour(customColorDialog.Color));
            }
        }

        private void ColorSwatchClicked(ColourBox swatch)
        {
            SetCurrentColor(swatch.GetColor());
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

        private void TeamColourBoxClicked(ColourBox box)
        {
            box.SetColor(currentColorBox.GetColor());
        }
    }
}