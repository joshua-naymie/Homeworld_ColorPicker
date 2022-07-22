using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Controls
{
    using Objects;

    public class ColorBox : ClickableBox<ColorBox>
    {
        private
        HomeworldColor currentColor;

        public ColorBox(PictureBox box) : base(box)
        {
            this.currentColor = new HomeworldColor();

            this.box.BackColor = currentColor.ToColor();
            this.box.Click += BoxClicked;
        }

        public ColorBox(PictureBox box, HomeworldColor currentColor) : base(box)
        {
            this.currentColor = currentColor;

            this.box.BackColor = currentColor.ToColor();
            this.box.Click += BoxClicked;
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