using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Controls
{
    using Objects;

    /// <summary>
    /// A manager class for a PictureBox representing a team color.
    /// </summary>
    public class ColorBox : ClickableBox<ColorBox>
    {
        private
        HomeworldColor currentColor;

        /// <summary>
        /// Constructor for ColorBox, initializing with the default HomeworldColor color.
        /// </summary>
        /// <param name="box">The PictureBox to manage</param>
        public ColorBox(PictureBox box) : base(box)
        {
            this.currentColor = new HomeworldColor();

            this.box.BackColor = currentColor.ToColor();
            this.box.Click += BoxClicked;
        }

        /// <summary>
        /// Constructor for ColorBox, initializing with a specific color.
        /// </summary>
        /// <param name="box">The PictureBox to manage</param>
        /// <param name="currentColor">The color to assign</param>
        public ColorBox(PictureBox box, HomeworldColor currentColor) : base(box)
        {
            this.currentColor = currentColor;

            this.box.BackColor = currentColor.ToColor();
            this.box.Click += BoxClicked;
        }

        /// <summary>
        /// Sets the color of the ColorBox and updates the PictureBox's BackColor with it.
        /// </summary>
        /// <param name="color"></param>
        public void SetColor(HomeworldColor color)
        {
            currentColor = color;
            box.BackColor = currentColor.ToColor();
        }

        /// <summary>
        /// Gets the color currently assigned to the ColorBox and it's PictureBox.
        /// </summary>
        /// <returns>The current color</returns>
        public HomeworldColor GetColor()
        {
            return currentColor;
        }

        /// <summary>
        /// ColorBox's implementation of the BoxClicked event.
        /// Passes this ColorBox to specific method depending on what mouse button is used.
        /// </summary>
        /// <param name="sender">The object calling the action</param>
        /// <param name="e">The arguments of the event</param>
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
    }
}