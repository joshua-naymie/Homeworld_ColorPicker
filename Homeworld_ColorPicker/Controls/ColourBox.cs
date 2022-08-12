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
    public class ColourBox : ClickableBox<ColourBox>
    {

        /// <summary>
        /// Private variable for Colour property.
        /// </summary>
        private
        HomeworldColour _Colour;
        
        /// <summary>
        /// The current colour of the ColourBox.
        /// </summary>
        public HomeworldColour Colour 
        {
            get { return _Colour; }
            set 
            {
                _Colour = value;
                BackColor = value.ToColor();
            } 
        }

        //----------------------------------------

        public ColourBox()
        {
            this.Colour = new HomeworldColour();

            base.BackColor = this.Colour.ToColor();
        }

        //--------------------

        /// <summary>
        /// Constructor for ColorBox, initializing with a specific color.
        /// </summary>
        /// <param name="box">The PictureBox to manage.</param>
        /// <param name="currentColor">The color to assign.</param>
        public ColourBox(HomeworldColour currentColor)
        {
            this.Colour = currentColor;
            this.BackColor = currentColor.ToColor();
        }

        //----------------------------------------

        /// <summary>
        /// ColorBox's implementation of the BoxClicked event.
        /// Passes this ColorBox to specific method depending on what mouse button is used.
        /// </summary>
        /// <param name="sender">The object calling the action.</param>
        /// <param name="e">The arguments of the event.</param>
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