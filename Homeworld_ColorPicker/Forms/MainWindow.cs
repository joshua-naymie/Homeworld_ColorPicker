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
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            StartDialog t = new StartDialog();
            t.ShowDialog();

            //----------

            InitializeComponent();
            customColorButton.Text = t.getRootDirectory();
        }
    }
}