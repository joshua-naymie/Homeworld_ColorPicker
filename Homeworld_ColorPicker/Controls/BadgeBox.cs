using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Controls
{
    /// <summary>
    /// A ClickableBox made for Homeworld badges.
    /// Handles default and custom badges.
    /// </summary>
    public class BadgeBox : ClickableBox<BadgeBox>
    {
        /// <summary>
        /// The path to the badges image file.
        /// </summary>
        public string? Path { get; private set; }

        /// <summary>
        /// The unaltered image data for the bage.
        /// </summary>
        public Image? RawImage { get; private set; }

        // CONSTRUCTORS
        //----------------------------------------

        /// <summary>
        /// Constructor for BadgeBox.
        /// </summary>
        public BadgeBox()
        {
            this.BackColor = Color.Gray;
            this.Margin = new Padding(0, 0, 0, 0);
            this.Padding = new Padding(0, 0, 0, 0);
            this.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        //--------------------

        /// <summary>
        /// Constructor for BadgeBox.
        /// Allows for an image to be assigned upon creation.
        /// </summary>
        /// <param name="path">The path to the image.</param>
        public BadgeBox(string path) : this()
        {   
            SetImage(path);
        }

        // EVENTS
        //----------------------------------------

        /// <summary>
        /// Called when the Click event is triggered.
        /// Invokes any assigned actions based on the mouse button used.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        public override void BoxClicked(object? sender, EventArgs e)
        {
            MouseEventArgs em = (MouseEventArgs)e;

            switch (em.Button)
            {
                case MouseButtons.Left:
                    if (leftClickAction != null)
                    {
                        leftClickAction.Invoke(this);
                    }
                    break;

                case MouseButtons.Right:
                    if (rightClickAction != null)
                    {
                        rightClickAction.Invoke(this);
                    }
                    break;

                case MouseButtons.Middle:
                    if (middleClickAction != null)
                    {
                        middleClickAction.Invoke(this);
                    }
                    break;
            }
        }

        // SET IMAGE
        //----------------------------------------

        /// <summary>
        /// Sets the badge image from a tga file path.
        /// If it's a default badge, image is pulled form Resources.
        /// Otherwise, loaded from disk.
        /// </summary>
        /// <param name="path">The path to the image file.</param>
        public void SetImage(string path)
        {
            this.Path = path;

            if(CONST.DEFAULT_BADGES_HWRM.ContainsKey(path))
            {
                this.RawImage = CONST.DEFAULT_BADGES_HWRM[path];
            }
            else
            {
                this.RawImage = new Paloma.TargaImage(path).Image;
            }

            int tenPercent = this.Width / 10;

            this.Image = Util.ResizeImage(this.RawImage, this.Width-tenPercent, this.Height-tenPercent);
        }
    }
}