using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Controls
{
    using Objects;

    public class ColorBox
    {
        private
        Action<ColorBox>? rightClickAction,
                          leftClickAction;

        private
        HomeworldColor currentColor;

        private
        PictureBox box;

        public ColorBox(PictureBox box)
        {
            this.box = box;
            this.currentColor = new HomeworldColor();

            this.box.BackColor = currentColor.ToColor();
            this.box.Click += BoxClicked;
        }

        public ColorBox(PictureBox box, HomeworldColor currentColor)
        {
            this.box = box;
            this.currentColor = currentColor;


            this.box.BackColor = currentColor.ToColor();
            this.box.Click += BoxClicked;
        }

        public void SetLeftClickAction(Action<ColorBox> action)
        {
            leftClickAction = action;
        }

        public void SetRightClickAction(Action<ColorBox> action)
        {
            rightClickAction = action;
        }

        public void SetColor(HomeworldColor color)
        {
            currentColor = color;
            box.BackColor = currentColor.ToColor();
        }

        public HomeworldColor GetColor()
        {
            return currentColor;
        }

        public void BoxClicked(object? sender, EventArgs e)
        {
            MouseEventArgs em = (MouseEventArgs) e;

            switch (em.Button)
            {
                case MouseButtons.Left:
                    if(leftClickAction != null)
                    {
                        leftClickAction.Invoke(this);
                    }
                    break;

                case MouseButtons.Right:
                    if(rightClickAction != null)
                    {
                        rightClickAction.Invoke(this);
                    }
                    break;
            }
        }
    }
}