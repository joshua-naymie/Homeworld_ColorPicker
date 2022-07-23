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
            //IO.ColorReader temp = new IO.ColorReader();
            //HomeworldColor[] t = temp.GetPlayerColors(@"E:\Games\Steam\steamapps\common\Homeworld\HomeworldRM\Bin\Profiles\Profile1\PLAYERCFG.LUA");

            //foreach(HomeworldColor c in t)
            //{
            //    System.Diagnostics.Debug.WriteLine(c);
            //}

            if(ShowDirectoryDialog() == DialogResult.Cancel)
            {
                Load += (s, e) => Close();
            }
            
            //----------

            InitializeComponent();

            InitColorSwatches();
            currentColorBox = new ColorBox(currentColorSwatch);
        }

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
            colorSwatches[9] = new ColorBox(colorSwatch10)l
            colorSwatches[10] = new ColorBox(colorSwatch11);
            colorSwatches[11] = new ColorBox(colorSwatch12);
            colorSwatches[12] = new ColorBox(colorSwatch13);
            colorSwatches[13] = new ColorBox(colorSwatch14);
            colorSwatches[14] = new ColorBox(colorSwatch15);
            colorSwatches[15] = new ColorBox(colorSwatch16);

            foreach (ColorBox swatch in colorSwatches)
            {
                swatch.SetLeftClickAction(ColorSwatchClicked);
            }
        }

        private void SetCustomColor(object sender, MouseEventArgs e)
        {
            customColorDialog.Color = currentColorSwatch
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
            currentColorSwatch.BackColor = color.ToColor();
        }

        private DialogResult ShowDirectoryDialog()
        {
            DialogResult result = directoryDialog.ShowDialog();
            if (result == DialogResult.OK)
            {

            }

            return result;
        }

        private void ReadPlayerColors()
        {

        }
    }
}