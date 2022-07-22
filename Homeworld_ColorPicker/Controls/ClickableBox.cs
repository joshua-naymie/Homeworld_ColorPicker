using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Controls
{
    public abstract class ClickableBox<T>
    {
        protected
        Action<T>? leftClickAction,
                   rightClickAction,
                   middleClickAction;

        protected
        PictureBox box;

        public ClickableBox(PictureBox box)
        {
            this.box = box;
        }

        public void SetLeftClickAction(Action<T> action)
        {
            leftClickAction = action;
        }

        public void SetRightClickAction(Action<T> action)
        {
            rightClickAction = action;
        }

        public void SetMiddleClickAction(Action<T> action)
        {
            middleClickAction = action;
        }

        public abstract void BoxClicked(object? sender, EventArgs e);
        //{
        //    MouseEventArgs em = (MouseEventArgs)e;

        //    switch (em.Button)
        //    {
        //        case MouseButtons.Left:
        //            if (leftClickAction != null)
        //            {
        //                leftClickAction.Invoke(this);
        //            }
        //            break;

        //        case MouseButtons.Right:
        //            if (rightClickAction != null)
        //            {
        //                rightClickAction.Invoke(this);
        //            }
        //            break;

        //        case MouseButtons.Middle:
        //            if (middleClickAction != null)
        //            {
        //                middleClickAction.Invoke(this);
        //            }
        //            break;
        //    }
        //}
    }
}
