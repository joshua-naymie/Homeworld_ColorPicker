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
    /// <typeparam name="T">The child class to be passed on mouse events.</typeparam>
    public abstract class ClickableBox<T> : PictureBox
    {
        /// <summary>
        /// The action that will be invoked when a left click event is triggered.
        /// Can be null, will not be invoked.
        /// </summary>
        protected
        Action<T>? leftClickAction;

        /// <summary>
        /// The action that will be invoked when a right click event is triggered.
        /// Can be null, will not be invoked.
        /// </summary>
        protected
        Action<T>? rightClickAction;

        /// <summary>
        /// The action that will be invoked when a middle click event is triggered.
        /// Can be null, will not be invoked.
        /// </summary>
        protected
        Action<T>? middleClickAction;

        //----------------------------------------

        /// <summary>
        /// Constructor for Clickable Box.
        /// </summary>
        public ClickableBox()
        {
            this.Click += BoxClicked;
        }

        // MOUSE EVENT ACTIONS
        //----------------------------------------

        /// <summary>
        /// Sets the action taken when a left click even is triggered.
        /// </summary>
        /// <param name="action">The action taken when a left click even is triggered.</param>
        public void SetLeftClickAction(Action<T>? action)
        {
            leftClickAction = action;
        }

        //--------------------

        /// <summary>
        /// Sets the action taken when a right click even is triggered.
        /// </summary>
        /// <param name="action">The action taken when a right click even is triggered.</param>v
        public void SetRightClickAction(Action<T>? action)
        {
            rightClickAction = action;
        }

        //--------------------

        /// <summary>
        /// Sets the action taken when a middle click even is triggered.
        /// </summary>
        /// <param name="action">The action taken when a middle click even is triggered.</param>
        public void SetMiddleClickAction(Action<T>? action)
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
