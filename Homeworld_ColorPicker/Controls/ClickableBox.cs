using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Controls
{
    /// <summary>
    /// Base class for PictureBox control.
    /// Manages mouse event actions and the PictureBox.
    /// </summary>
    /// <typeparam name="T">The child class to be passed on mouse events</typeparam>
    public abstract class ClickableBox<T>
    {
        protected
        Action<T>? leftClickAction,
                   rightClickAction,
                   middleClickAction;

        protected
        PictureBox box;

        //----------------------------------------

        /// <summary>
        /// Constructor for ClickableBox base class.
        /// Must include the PictureBox to be controlled
        /// </summary>
        /// <param name="box">The picture box to be controlled</param>
        public ClickableBox(PictureBox box)
        {
            this.box = box;

            this.box.Click += BoxClicked;
        }

        // MOUSE EVENT ACTIONS
        //----------------------------------------

        /// <summary>
        /// Sets the method called when the left mouse click is detected
        /// </summary>
        /// <param name="action">The method to call</param>
        public void SetLeftClickAction(Action<T> action)
        {
            leftClickAction = action;
        }

        //--------------------

        /// <summary>
        /// Sets the method called when the right mouse click is detected
        /// </summary>
        /// <param name="action">The method to call</param>
        public void SetRightClickAction(Action<T> action)
        {
            rightClickAction = action;
        }

        //--------------------

        /// <summary>
        /// Sets the method called when the middle mouse click is detected
        /// </summary>
        /// <param name="action">The method to call</param>
        public void SetMiddleClickAction(Action<T> action)
        {
            middleClickAction = action;
        }

        // EVENTS
        //----------------------------------------

        /// <summary>
        /// The event called when the box is clicked.
        /// Specific action depends on child class implementation.
        /// </summary>
        /// <param name="sender">The object that called the event</param>
        /// <param name="e">The arguments of the event</param>
        public abstract void BoxClicked(object? sender, EventArgs e);
    }
}
