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
        /// <summary>
        /// The dialog used to set or change the directories for Homeworld root, Remastered Toolkit, and Profile.
        /// </summary>
        private
        DirectoryDialog directoryDialog = new DirectoryDialog();

        private
        ColorDialog customColorDialog = new ColorDialog();

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
        }

        private void InitColorSwatches()
        {
            ColorBox[] colorSwatches = {
                                        new ColorBox(colorSwatch1),
                                        new ColorBox(colorSwatch2),
                                        new ColorBox(colorSwatch3),
                                        new ColorBox(colorSwatch4),
                                        new ColorBox(colorSwatch5),
                                        new ColorBox(colorSwatch6),
                                        new ColorBox(colorSwatch7),
                                        new ColorBox(colorSwatch8),
                                        new ColorBox(colorSwatch9),
                                        new ColorBox(colorSwatch10),
                                        new ColorBox(colorSwatch11),
                                        new ColorBox(colorSwatch12),
                                        new ColorBox(colorSwatch13),
                                        new ColorBox(colorSwatch14),
                                        new ColorBox(colorSwatch15),
                                        new ColorBox(colorSwatch16)
                                       };
        }

        private void SetCustomColor(object sender, MouseEventArgs e)
        {
            
            customColorDialog.FullOpen = true;

            if(customColorDialog.ShowDialog() == DialogResult.OK)
            {
                SetCurrentColor(customColorDialog.Color);
            }
        }

        private void SetCurrentColor(HomeworldColor color)
        {
            SetCurrentColor(color.ToColor());
        }

        private void SetCurrentColor(Color color)
        {
            currentColorSwatch.BackColor = color;
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