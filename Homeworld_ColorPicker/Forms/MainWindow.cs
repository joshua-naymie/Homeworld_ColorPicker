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
        ColorBox currentColorBox;

        private
        ColorBox[] colorSwatches = new ColorBox[GC.NUM_PLAYER_COLORS];

        System.Drawing.Text.PrivateFontCollection pfc = new System.Drawing.Text.PrivateFontCollection();

        /// <summary>
        /// Constructor for MainWindow.
        /// </summary>
        public MainWindow()
        {
            directoryDialog = new DirectoryDialog();

            bool continueRunning = ShowDirectoryDialog(IO.ConfigManager.ReadConfig()) == DialogResult.OK;

            if (continueRunning
            && !IO.ExtractedDataManager.VerifyRequiredFiles(instance))
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
            customColorButton.Font = new Font(pfc.Families[0], 11);
            label1.Font = new Font(pfc.Families[0], label1.Font.Size);
            label1.Text = "HOMEWORLD 2";

            if (continueRunning)
            {
                InitColorSwatches();
                InitCurrentColor();
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

            HomeworldColour[] playerColors = IO.ColorReader.GetPlayerColors(instance.HomeworldRootDir + instance.ProfilePath);
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
                SetCurrentColor(new HomeworldColour(customColorDialog.Color));
            }
        }

        private void ColorSwatchClicked(ColorBox swatch)
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

        private void ReadPlayerColors()
        {

        }
    }
}